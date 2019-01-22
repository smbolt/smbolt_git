using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements="Cmd")]
  public class Tsd : List<Cmd>
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(IsKey = true)]
    public string SpecialRoutine { get; set; }

    [XMap(DefaultValue="True")]
    public bool Active { get; set; }

    [XMap(DefaultValue = "False")]
    public bool Iterate { get; set; }

    [XMap]
    public string BreakOnLine { get; set; }

    [XMap(DefaultValue="False")]
    public bool IsReportUnit { get; set; }

    [XMap(DefaultValue="False")]
    public bool NewWorksheet { get; set; }

    [XMap]
    public string ExtractTemplate { get; set; }

    [XMap(DefaultValue="False")]
    public bool Debug { get; set; }

    [XMap(DefaultValue="False")]
    public bool SkipExtract { get; set; }

    [XMap]
    public string ExtractOptions { get; set; }
    public OptionsList LevelExtractOptions
    {
      get
      {
        if (_optionsList == null)
          _optionsList = new OptionsList();
        return _optionsList;
      }
    }
    private OptionsList _optionsList { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "Tsd", WrapperElement="TsdSet")]
    public TsdSet TsdSet { get; set; }

    public string Code { get { return Get_Code(); } }
    public int BeginPosition { get; set; }
    public int EndPosition { get; set; }
    public Tsd PriorSibling { get { return Get_PriorSibling(); } }
    public string FullXmlPath { get { return Get_FullXmlPath(); } }
    public string FullFilePath { get { return (this.ExtractSpec != null ? this.ExtractSpec.FullFilePath : String.Empty); } }
    public string Condition { get; set; }

    public List<Cmd> StructureCommands { get { return Get_StructureCommands(); } }
    public List<Cmd> TextExtractCommands { get { return Get_TextExtractCommands(); } }

    public ExtractSpec ExtractSpec; 
    public Tsd Parent { get; set; }
    public static ColumnIndexMap ColumnIndexMap { get; set; }

    public Tsd()
    {
      this.TsdSet = new TsdSet();
      this.Active = true;
      this.SpecialRoutine = String.Empty;
      this.Iterate = false;
      this.Debug = false;
      this.IsReportUnit = false;
      this.NewWorksheet = false;
      this.SkipExtract = false;
      this.BreakOnLine = String.Empty;
      this.ExtractOptions = String.Empty;
      _optionsList = new OptionsList();
    }

    public void AutoInit()
    {
      _optionsList = new OptionsList(this.ExtractOptions);
    }
    

    public string Get_Code()
    {
      var sb = new StringBuilder();

      foreach (var cmd in this)
      {
        if (sb.Length > 0)
          sb.Append(g.crlf); 
        sb.Append(cmd.Code); 
      }

      return sb.ToString();
    }

    private Tsd Get_PriorSibling()
    {
      if (this.Parent == null)
        return null;

      if (this.Parent.TsdSet == null || this.TsdSet.Count == 0)
        return null; 

      string thisName = this.Name;

      Tsd priorSibling = null;

      for (int i = 0; i < this.Parent.TsdSet.Count; i++)
      {
        var kvp = this.Parent.TsdSet.ElementAt(i); 
        if (kvp.Value.Name == thisName)
          return priorSibling;
        priorSibling = kvp.Value;
      }

      return null;
    }

    private string Get_FullXmlPath()
    {
      if (this.ExtractSpec == null)
        return String.Empty;

      string fullPath = this.Name;

      var  parent = this.Parent;
      while (parent != null)
      {
        fullPath = parent.Name + @"\" + fullPath;
        parent = parent.Parent;
      }

      return this.ExtractSpec.Name + @"\"  + fullPath;
    }

    public List<Cmd> Get_StructureCommands()
    {
      var list = new List<Cmd>();

      foreach (var cmd in this)
      {
        switch (cmd.Verb.ToLower())
        {
          case "settextstart":
          case "settextend":
          case "processingcommand":
          case "setxml":
            list.Add(cmd);
            break;

          case "replacetext":
            if (cmd.Parms.GetEntry("structure").IsNotBlank())
              list.Add(cmd);
            break;
        }
      }

      return list;
    }

    public List<Cmd> Get_TextExtractCommands()
    {
      var list = new List<Cmd>();

      foreach (var cmd in this)
      {
        switch (cmd.Verb.ToLower())
        {
          case "settextstart":
          case "settextend":
          case "setxml":
            break;

          case "replacetext":
            if (cmd.Parms.GetEntry("structure").IsBlank())
              list.Add(cmd);
            break;

          default:
						if (!cmd.Verb.ToLower().StartsWith("*"))
							list.Add(cmd);
            break;
        }
      }

      return list;
    }

    public bool ProcessOptionSet(string optionName)
    {


      return false;
    }

    public Tsd Clone()
    {
      var clone = new Tsd();
      clone.Name = this.Name;
      clone.Iterate = this.Iterate;
      clone.Debug = this.Debug;
      clone.ExtractSpec = this.ExtractSpec;

      foreach (var cmd in this)
      {
        clone.Add(cmd.Clone());
      }

      clone.Parent = this.Parent;
      return clone;
    }

  }
}
