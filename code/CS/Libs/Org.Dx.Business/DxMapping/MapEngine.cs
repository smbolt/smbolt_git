using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.Dx.Business.TextProcessing;

namespace Org.Dx.Business
{
  public class MapEngine : IDisposable
  {
    private Dictionary<string, DxWorkbook> _dxWorkbookVersions;
    public Dictionary<string, DxWorkbook> DxWorkbookVersions {
      get {
        return _dxWorkbookVersions;
      }
    }

    private DxWorkbook _originalWorkbook;
    public DxWorkbook OriginalWorkbook {
      get {
        return _originalWorkbook;
      }
    }

    private DxWorkbook _currWb;
    private DxMapSet _mapSet;

    public static Dictionary<string, string> GlobalVariables;
    public static Dictionary<string, string> LocalVariables;

    public MapEngine()
    {
      _dxWorkbookVersions = new Dictionary<string, DxWorkbook>();
      GlobalVariables = new Dictionary<string,string>();
      LocalVariables = new Dictionary<string,string>();
    }

    public DxWorkbook MapDxWorkbook(DxWorkbook srcWb, DxMapSet mapSet)
    {
      try
      {
        if (srcWb == null)
          throw new Exception("The source workbook is null.");

        if (mapSet == null || mapSet.Count == 0)
          return _originalWorkbook;

        _mapSet = mapSet;

        InitializeMapping(srcWb, mapSet);
        return Run();
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to map the source workbook to the destination workbook.", ex);
      }
    }

    private DxWorkbook Run()
    {
      try
      {
        if (_mapSet.DxCommandSet.Count == 0)
        {
          var dxCommand = new DxCommand();
          dxCommand.Name = "DefaultMap";
          dxCommand.DxActionType = DxActionType.DataMappingAction;
          _mapSet.DxCommandSet.Add(dxCommand.Name, dxCommand);
        }

        foreach (var dxCommand in _mapSet.DxCommandSet.Values.Where(c => c.IsActive))
        {
          var dxAction = DxActionFactory.GetDxAction(this, new DxActionParms(dxCommand.DxActionType, dxCommand.DxActionParms)) as DxActionBase;
          dxAction.OnNewWorkbookVersion += NewWorkbookVersionCreated;
          _currWb = dxAction.Execute(_currWb, _mapSet);
        }

        _currWb.IsMapped = true;
        _currWb.MapPath = _mapSet.FullMapPath;

        return _currWb;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during DxWorkbook mapping.", ex);
      }
    }

    private void NewWorkbookVersionCreated(DxWorkbook wb)
    {
      _dxWorkbookVersions.Add("DxWorkbook[" + _dxWorkbookVersions.Count.ToString("00") + "]", wb);
    }

    private void InitializeMapping(DxWorkbook srcWb, DxMapSet mapSet)
    {
      _originalWorkbook = srcWb;
      _originalWorkbook.IsMapped = false;
      _dxWorkbookVersions.Add("ORIGINAL-SOURCE", _originalWorkbook.Clone());
      _currWb = _originalWorkbook;
      _mapSet.Validate();
    }

    public static void AddVariable(DxMapItem dxMapItem, string variableName, string variableValue)
    {
      try
      {
        var variableType = variableName.StartsWith("@") ? VariableType.Global : VariableType.Local;
        variableName = variableName.Substring(1);

        if (variableName.IsBlank())
          throw new Exception("Variable name cannot be blank - DxMapItem is '" + dxMapItem.Report + "'.");

        switch (variableType)
        {
          case VariableType.Global:
            if (GlobalVariables == null)
              GlobalVariables = new Dictionary<string, string>();

            if (GlobalVariables.ContainsKey(variableName))
              GlobalVariables[variableName] = variableValue;
            else
              GlobalVariables.Add(variableName, variableValue);
            break;

          case VariableType.Local:
            if (LocalVariables == null)
              LocalVariables = new Dictionary<string, string>();

            if (LocalVariables.ContainsKey(variableName))
              LocalVariables[variableName] = variableValue;
            else
              LocalVariables.Add(variableName, variableValue);
            break;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a variable from " + dxMapItem.Report + g.crlf + ex.ToReport());
      }
    }

    public static string GetVariableValue(DxMapItem mapItem)
    {
      if (!mapItem.Src.StartsWith("$"))
        throw new Exception("The DxMapItem Src property must start with '$' to reference a variable, " + mapItem.Report);

      string variableName = mapItem.Src.Substring(1);

      if (LocalVariables == null)
        LocalVariables = new Dictionary<string, string>();

      if (LocalVariables.ContainsKey(variableName))
        return LocalVariables[variableName];

      if (GlobalVariables == null)
        GlobalVariables = new Dictionary<string, string>();

      if (GlobalVariables.ContainsKey(variableName))
        return GlobalVariables[variableName];

      return String.Empty;
    }

    public static string GetVariableValue(string variableName)
    {
      if (!variableName.StartsWith("$"))
        throw new Exception("The variable name must start with '$' to reference a variable - value received is '" + variableName + "'.");

      variableName = variableName.Substring(1);

      if (LocalVariables == null)
        LocalVariables = new Dictionary<string, string>();

      if (LocalVariables.ContainsKey(variableName))
        return LocalVariables[variableName];

      if (GlobalVariables == null)
        GlobalVariables = new Dictionary<string, string>();

      if (GlobalVariables.ContainsKey(variableName))
        return GlobalVariables[variableName];

      return String.Empty;
    }

    public void Dispose()
    {
    }
  }
}
