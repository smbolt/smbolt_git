using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Terminal.Screen;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class BmsMapSetFile
  {
    public string MapString {
      get;
      set;
    }
    public ScreenSpecSet ScreenSpecSet {
      get;
      set;
    }
    public BmsStatementSet BmsStatementSet {
      get;
      set;
    }
    public BmsMapSet BmsMapSet {
      get;
      set;
    }
    public BmsMapErrorSet BmsMapErrorSet {
      get;
      set;
    }
    public int HighestErrorCode {
      get {
        return Get_HighestErrorCode();
      }
    }

    public List<string> Errors {
      get;
      set;
    }

    public BmsMapSetFile(string mapString, BmsMapErrorSet bmsMapErrorSet)
    {
      try
      {
        this.BmsMapErrorSet = bmsMapErrorSet;
        this.MapString = mapString;
        this.Errors = new List<string>();

        this.BmsStatementSet = new BmsStatementSet();
        this.BmsMapSet = new BmsMapSet();
        this.BmsStatementSet.Load(mapString, this.BmsMapErrorSet);
        this.BmsStatementSet.CompileStatements();

        if (this.HighestErrorCode == 0)
          this.BuildMapSet();

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a new BmsMap object from the BMS string.", ex);
      }
    }

    private void BuildMapSet()
    {
      try
      {
        BmsMap bmsMap = null;

        // create and populate the map set
        foreach (var bmsStmt in this.BmsStatementSet.Values)
        {
          if (bmsStmt.BmsStatementType == BmsStatementType.Comment || bmsStmt.BmsStatementType == BmsStatementType.PRINT)
            continue;

          switch (bmsStmt.BmsStatementType)
          {
            case BmsStatementType.DFHMSD:
              this.BmsMapSet = new BmsMapSet();
              this.BmsMapSet.Bms_DFHMSD = bmsStmt.Bms_BASE as Bms_DFHMSD;
              break;

            case BmsStatementType.DFHMDI:
              bmsMap = new BmsMap();
              bmsMap.Bms_DFHMDI = bmsStmt.Bms_BASE as Bms_DFHMDI;
              bmsMap.Name = bmsMap.Bms_DFHMDI.Name;
              this.BmsMapSet.Add(bmsMap.Name, bmsMap);
              break;

            case BmsStatementType.DFHMDF:
              bmsMap.Bms_DFHMDF_Set.Add(bmsStmt.Bms_BASE as Bms_DFHMDF);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the BmsMapSet from the compiled BmsMapSetFile.", ex);
      }
    }

    public ScreenSpecSet GetScreenSpecSet()
    {
      try
      {
        var screenSpecSet = new ScreenSpecSet();

        foreach (var bmsMap in this.BmsMapSet.Values)
        {
          var screenSpec = new ScreenSpec();
          screenSpec.Name = bmsMap.Name;
          // rest of properties

          string skipTo = String.Empty;

          foreach (var bmsField in bmsMap.Bms_DFHMDF_Set)
          {
            if (skipTo.IsNotBlank())
            {
              if (bmsField.Name != skipTo)
                continue;
              skipTo = String.Empty;
            }

            var fieldSpec = new FieldSpec();
            fieldSpec.Bms_DFHMDF = bmsField;
            screenSpec.FieldSpecSet.Add(fieldSpec);

            skipTo = bmsField.SkipTo;
          }

          screenSpecSet.Add(screenSpec.Name, screenSpec);
        }

        return screenSpecSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the ScreenSpecSet from the BMS map.", ex);
      }
    }

    private int Get_HighestErrorCode()
    {
      if (this.BmsStatementSet == null || this.BmsStatementSet.Count == 0)
        return 16;

      return this.BmsStatementSet.Values.Max(s => s.ErrorCode);
    }
  }
}
