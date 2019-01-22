using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Org.GS;

namespace Org.GraphicReports
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


		public ReportImageSet ProduceReport(DrawingObjectSet drawingObjectSet)
		{
			_reportImageSet = new ReportImageSet();

			var reportImage = new ReportImage(1, _reportParms);

			using (Graphics gx = Graphics.FromImage(reportImage.Image))
			{
        List<string> spanList = GetSpan();


				gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				gx.TextRenderingHint = TextRenderingHint.AntiAlias;

				var blueBrush = new SolidBrush(Color.FromArgb(100, 149, 237));
        var whiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

				gx.FillRectangle(Brushes.White, new Rectangle(0, 0, reportImage.Image.Width, reportImage.Image.Height));

				gx.DrawString("Rig Schedule: 3/1/2016 - 6/1/2016", new Font("Calibri", 14.0F, FontStyle.Bold), Brushes.Green, new PointF(50.0F, 100.0F)); 
				gx.DrawLine(new Pen(Brushes.Black, 2.5F), new Point(50, 124), new Point(1000, 124));

				gx.FillRectangle(blueBrush, new Rectangle(50, 125, 75, 28));
				gx.DrawRectangle(Pens.Black, new Rectangle(50, 125, 75, 28));

        int monthYearFromLeft = 125;
        int dayFromLeft = 125;
        int monthYearFromTop = 125;
        int dayFromTop = 153;
        int monthYearHeight = 28;
        int dayHeight = 18;
        int dayWidth = 13;
        int pointFCenterline = 132;
        int pointFBeginPoint = 280;

        foreach(var monthDay in spanList)
        {
          string[] tokens = monthDay.Split(Constants.FSlashDelimiter);

          int month = tokens[0].ToInt32();
          int year = tokens[2].ToInt32();
          int day = tokens[1].ToInt32();
          string monthName = tokens[3];

          //int width = days * 13;

          //gx.FillRectangle(blueBrush, new Rectangle(monthYearFromLeft, monthYearFromTop, width, monthYearHeight));
          //gx.DrawRectangle(Pens.Black, new Rectangle(monthYearFromLeft, monthYearFromTop, width, monthYearHeight)); 
          //gx.DrawString(stringMonth + " " + year, new Font("Calibri", 8.0F, FontStyle.Bold), Brushes.Black, new PointF(pointFBeginPoint, pointFCenterline)); 

          //for(int d = 1; d <= days; d++)
          //{
          //  gx.FillRectangle(Brushes.White, new Rectangle(dayFromLeft, dayFromTop, dayWidth, dayHeight));
          //  gx.DrawRectangle(Pens.Black, new Rectangle(dayFromLeft, dayFromTop, dayWidth, dayHeight));

          //  gx.DrawString(d.ToString(), new Font("Calibri", 7.0F, FontStyle.Bold), Brushes.Black, new PointF(dayFromLeft + 1 , 156));

          //  dayFromLeft = dayFromLeft + dayWidth;
          //}

          //monthYearFromLeft = monthYearFromLeft + width;
          //pointFBeginPoint = pointFBeginPoint + width;
        }

        //gx.FillRectangle(blueBrush, new Rectangle(125, 125, 52, 28));
        //gx.DrawRectangle(Pens.Black, new Rectangle(125, 125, 52, 28)); 
        //gx.DrawString("February", new Font("Calibri", 8.0F, FontStyle.Bold), Brushes.Black, new PointF(128, 132)); 

        //gx.FillRectangle(whiteBrush, new Rectangle(50, 153, 75, 18));
        //gx.DrawRectangle(Pens.Black, new Rectangle(50, 153, 75, 18));

        //gx.FillRectangle(Brushes.White, new Rectangle(125, 153, 13, 18));
        //gx.DrawRectangle(Pens.Black, new Rectangle(125, 153, 13, 18));
        //gx.DrawString("26", new Font("Calibri", 7.0F, FontStyle.Bold), Brushes.Black, new PointF (126 , 156));

        //gx.FillRectangle(Brushes.White, new Rectangle(138, 153, 13, 18));
        //gx.DrawRectangle(Pens.Black, new Rectangle(138, 153, 13, 18));
        //gx.DrawString("27", new Font("Calibri", 7.0F, FontStyle.Bold), Brushes.Black, new PointF (139 , 156));

        //gx.FillRectangle(Brushes.White, new Rectangle(151, 153, 13, 18));
        //gx.DrawRectangle(Pens.Black, new Rectangle(151, 153, 13, 18));
        //gx.DrawString("28", new Font("Calibri", 7.0F, FontStyle.Bold), Brushes.Black, new PointF (152 , 156));

        //gx.FillRectangle(Brushes.White, new Rectangle(164, 153, 13, 18));
        //gx.DrawRectangle(Pens.Black, new Rectangle(164, 153, 13, 18));
        //gx.DrawString("29", new Font("Calibri", 7.0F, FontStyle.Bold), Brushes.Black, new PointF (165 , 156));
			}

			_reportImageSet.Add(_reportImageSet.Count + 1, reportImage); 

			return _reportImageSet;
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
        string year = tokens[2].Substring(0,4);
        string monthName = DateTime.Parse(month + '/' + "1" + '/' + "2016").ToString("MMMM");

        spanList.Add(month + "/" + day + "/" + year + "/" + monthName);
        startDate = startDate.AddDays(1);
      }
      return spanList;
    }

    private List<string> GetDays(List<string> spanList)
    {
      var dayList = new List<string>();
      foreach(var day in spanList)
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
