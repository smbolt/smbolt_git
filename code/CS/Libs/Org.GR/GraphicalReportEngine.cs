using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Org.GS;

namespace Org.GR
{
  public class GraphicalReportEngine : IDisposable
  {
    private ReportParms _reportParms;
    private ReportImageSet _reportImageSet;

    public DateSpan Span;
    public GraphicalReportEngine(ReportParms reportParms)
    {
      _reportParms = reportParms;
    }


    public Image ProduceReport(DrawingObjectSet drawingObjectSet)
    {
      _reportImageSet = new ReportImageSet();

      var reportImage = new ReportImage(1, _reportParms);

      using (Graphics gx = Graphics.FromImage(reportImage.Image))
      {
        gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        gx.TextRenderingHint = TextRenderingHint.AntiAlias;

        var blueBrush = new SolidBrush(Color.FromArgb(100, 149, 237));
        var whiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        PointF origin = new PointF(0, 0);

        foreach (var dObj in drawingObjectSet.Values)
        {
          dObj.DrawObject(gx, 1.0F, origin);
        }
      }


      return reportImage.Image;
      //_reportImageSet.Add(_reportImageSet.Count + 1, reportImage);

      //var img = _reportImageSet.Values.First().Image;


      //img.Save(g.ExportsPath + @"\Image.bmp");

      //return _reportImageSet;
    }

    private List<string> GetSpan()
    {
      var span = new DateSpan(new DateTime(2016, 2, 26), new DateTime(2016, 4, 30));
      DateTime startDate = span.StartDateTime;
      DateTime endDate = span.EndDateTime;
      var spanList = new List<string>();

      while (startDate <= endDate)
      {
        string stDate = startDate.ToString();
        string[] tokens = stDate.Split(Constants.FSlashDelimiter);
        string month = tokens[0];
        string day = tokens[1];
        string year = tokens[2].Substring(0, 4);
        string monthName = DateTime.Parse(month + '/' + "1" + '/' + "2016").ToString("MMMM");

        spanList.Add(month + "/" + day + "/" + year + "/" + monthName);
        startDate = startDate.AddDays(1);
      }
      return spanList;
    }

    private List<string> GetDays(List<string> spanList)
    {
      var dayList = new List<string>();
      foreach (var day in spanList)
      {
        string[] tokens = day.Split(Constants.FSlashDelimiter);
        string stringMonth = tokens[3];

      }



      return dayList;
    }

    public void Dispose()
    {
      foreach (var reportImage in _reportImageSet.Values)
      {
        if (!reportImage.IsDisposed)
          reportImage.Dispose();
      }
    }
  }
}
