using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XmlReport
  {
    public XElement _originalXml;
    public XmlLineSet XmlLineSet {
      get;
      set;
    }
    public int ErrorCount {
      get {
        return Get_ErrorCount();
      }
    }
    public string Report {
      get {
        return Get_Report();
      }
    }

    public XmlReport(XElement xml)
    {
      _originalXml = xml;
      CreateLineSet();
    }

    public void AddErrorToLine(int lineNumber, XmlLineError xmlLineError)
    {
      if (this.XmlLineSet == null)
        throw new Exception("The XmlLineSet property is null.");

      if (!this.XmlLineSet.ContainsKey(lineNumber))
        throw new Exception("The XmlLine does not contain an entry for line number " + lineNumber.ToString() + ".");

      if (this.XmlLineSet[lineNumber].XmlLineErrors == null)
        this.XmlLineSet[lineNumber].XmlLineErrors = new List<XmlLineError>();

      this.XmlLineSet[lineNumber].XmlLineErrors.Add(xmlLineError);
    }

    public int GetLineNumberOfElement(XElement e, XmlReport rpt, int prevLineNumber)
    {
      if (e == null || rpt == null)
        return -1;

      string xml = e.ToString().Trim();

      if (xml.IsBlank())
        return -1;

      var xmlList = xml.Split(Constants.CrlfDelimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

      if (xmlList.Count < 1)
        return -1;

      string xmlLine = xmlList.First();

      foreach(var kvpLine in rpt.XmlLineSet)
      {
        if (kvpLine.Key > prevLineNumber)
        {
          if (kvpLine.Value.Line.Trim() == xmlLine)
            return kvpLine.Key;
        }
      }

      throw new Exception("The xml line '" + xmlLine + "' (this first line of the Xml element parameter) was not found within the XmlReport lines.");
    }

    private void CreateLineSet()
    {
      this.XmlLineSet = new XmlLineSet();

      string[] lines = _originalXml.ToXmlStringArray();

      foreach (var line in lines)
      {
        var reportLine = new XmlLine(line);
        this.XmlLineSet.Add(this.XmlLineSet.Count, reportLine);
      }
    }

    private int Get_ErrorCount()
    {
      if (this.XmlLineSet == null || this.XmlLineSet.Count == 0)
        return 0;

      int errorCount = 0;
      foreach (var line in this.XmlLineSet.Values)
      {
        if (line.XmlLineErrors != null)
          errorCount += line.XmlLineErrors.Count;
      }

      return errorCount;
    }

    private string Get_Report()
    {



      return "Report";
    }

  }
}
