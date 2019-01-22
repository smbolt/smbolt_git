using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "Tsd")]
  public class ExtractSpec : Dictionary<string, Tsd>
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue = "PDF")]
    public FileType FileType {
      get;
      set;
    }

    [XMap]
    public string RecogSpecName {
      get;
      set;
    }

    [XMap]
    public string Desc {
      get;
      set;
    }

    [XMap]
    public string GlobalRoutines {
      get;
      set;
    }

    [XMap]
    public string ExtractOptions {
      get;
      set;
    }
    public OptionsList LevelExtractOptions
    {
      get
      {
        if (_optionsList == null)
          _optionsList = new OptionsList();
        return _optionsList;
      }
    }
    private OptionsList _optionsList {
      get;
      set;
    }

    private List<string> _globalRoutineList = new List<string>();
    public List<string> GlobalRoutineList {
      get {
        return _globalRoutineList;
      }
    }

    [XMap(XType = XType.Element)]
    public ExtractionMap ExtractionMap {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public bool RunExtract {
      get;
      set;
    }

    private bool _linesNumbered = false;
    public bool LinesNumbered {
      get {
        return _linesNumbered;
      }
    }

    public ExtractSpecSet ExtractSpecSet {
      get;
      set;
    }
    public string FullFilePath {
      get;
      set;
    }
    public static int LineNumber {
      get;
      set;
    }
    public static int BreakOnLine {
      get;
      set;
    }

    public ExtractSpec()
    {
      this.Name = String.Empty;
      this.FileType = FileType.PDF;
      this.RecogSpecName = String.Empty;
      this.Desc = String.Empty;
      this.GlobalRoutines = String.Empty;
      this.ExtractionMap = null;
      this.RunExtract = true;
      this.FullFilePath = String.Empty;
      this.ExtractOptions = String.Empty;
      _optionsList = new OptionsList();
    }

    public void AutoInit()
    {
      if (this.GlobalRoutines.IsNotBlank())
        _globalRoutineList = this.GlobalRoutines.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

      _optionsList = new OptionsList(this.ExtractOptions);
    }

    public void Reset()
    {
      foreach (var tsd in this.Values)
        ResetTsd(tsd);
    }

    private void ResetTsd(Tsd tsd)
    {
      foreach (var cmd in tsd)
      {
        cmd.ActiveToRun = true;
      }

      foreach (var childTsd in tsd.TsdSet.Values)
        ResetTsd(childTsd);
    }

    public void NumberLines()
    {
      ExtractSpec.LineNumber = 0;
      foreach (var tsd in this.Values)
        NumberLines(tsd);
    }

    private void NumberLines(Tsd tsd)
    {
      foreach (var cmd in tsd)
      {
        cmd.LineNumber = ExtractSpec.LineNumber++;
      }

      foreach (var childTsd in tsd.TsdSet.Values)
        NumberLines(childTsd);
    }

    public string ToReport(Cmdx cmdx = null)
    {
      StringBuilder sb = new StringBuilder();
      List<string> readList = new List<string>();

      var f = new ObjectFactory2();
      var xml = f.Serialize(this);

      int lineNumber = -1;
      if (cmdx != null && cmdx.LineNumber > 0)
        lineNumber = cmdx.LineNumber;

      string extractSpecString = xml.ToString().Replace(g.crlf, "\n");
      string[] extractSpecLines = extractSpecString.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      string errorLineIndicator = "LineNumber=\"" + lineNumber.ToString() + "\"";
      int begLine = -1;
      int endLine = lineNumber + 10;
      if (lineNumber < 10)
        begLine = 0;
      else
        begLine = lineNumber - 10;

      for (int i = begLine; i <= endLine; i++)
      {
        readList.Add("LineNumber=\"" + i.ToString() + "\"");
      }

      foreach (string extractSpecLine in extractSpecLines)
      {
        foreach (var line in readList)
        {
          if (extractSpecLine.Contains(line))
          {
            if (extractSpecLine.Contains(errorLineIndicator))
              sb.Append("*** NEXT LINE CONTAINS THE CODE THAT CAUSED THE ERROR ***" + g.crlf);
            sb.Append(extractSpecLine + g.crlf);
            break;
          }
          else
            continue;
        }
      }

      string report = sb.ToString();
      return report;
    }
  }
}
