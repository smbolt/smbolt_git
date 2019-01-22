using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  public class WsPerformanceInfoSet : List<WsPerformanceInfo>
  {
    public string GetReport()
    {
      if (this.Count == 0)
        return "No performance data."; 

      StringBuilder sb = new StringBuilder();

      sb.Append("Performance Report" + g.crlf2);

      WsPerformanceInfo piFirst = this.FirstOrDefault();
      WsPerformanceInfo piLast = this.LastOrDefault();

      DateTime priorDt = DateTime.MinValue;
      foreach (WsPerformanceInfo pi in this)
      {
        string timeStamp = pi.DateTime.ToString("yyyyMMdd-HHmmss.fff");
        if (priorDt == DateTime.MinValue)
          priorDt = pi.DateTime;
        TimeSpan ts = pi.DateTime - priorDt;
        sb.Append(timeStamp + "    " + ts.TotalSeconds.ToString("000") + "." + ts.Milliseconds.ToString("000") +
          "    " + pi.Label + g.crlf);
        priorDt = pi.DateTime;
      }

      TimeSpan tsAll = piLast.DateTime - piFirst.DateTime;
      sb.Append(g.crlf + "                       " + tsAll.TotalSeconds.ToString("000") + "." + tsAll.Milliseconds.ToString("000") + 
        "    Total Elapsed Time" + g.crlf); 

      string report = sb.ToString();
      return report;
    }

    public void AddEntry(string entry)
    {
      this.Add(new WsPerformanceInfo(entry));
    }

    public XElement GetXml()
    {
      XElement perfInfoSet = new XElement("PerfInfoSet");

      foreach (WsPerformanceInfo pi in this)
      {
        XElement piElement = new XElement("PerfInfo");
        piElement.Add(new XAttribute("DateTime", pi.DateTime.ToString("yyyyMMdd-HHmmss.fff")));
        piElement.Add(new XAttribute("Label", pi.Label));
        perfInfoSet.Add(piElement);
      }

      return perfInfoSet;
    }

    public void LoadFromXml(XElement xml)
    {
      this.Clear();
      IEnumerable<XElement> piElements = xml.Elements("PerfInfo");
      foreach (XElement piElement in piElements)
      {
        WsPerformanceInfo pi = new WsPerformanceInfo();
        pi.DateTime = g.GetDateFromLongNumeric(piElement.Attribute("DateTime").Value);
        pi.Label = piElement.Attribute("Label").Value;
        this.Add(pi);
      }
    }
  }
}
