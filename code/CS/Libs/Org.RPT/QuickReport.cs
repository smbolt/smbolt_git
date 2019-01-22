using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Org.GS;

namespace Org.RPT
{
  public class QuickReport
  {
    private ReportData _reportData;
    private ReportDef _reportDef;
    private ReportDefs _reportDefs;
    private float _scale;

    public List<Image> PageImages {
      get;
      set;
    }

    public QuickReport(ReportData reportData, ReportDef reportDef)
    {
      _reportData = reportData;
      _reportDef = reportDef;
      _scale = 1.0F;
      this.PageImages = new List<Image>();
      Image page1 = new Bitmap(Convert.ToInt32(reportDef.PageWidth), Convert.ToInt32(reportDef.PageHeight));
      page1.Tag = 1;
      this.PageImages.Add(page1);
    }

    public List<Image> DrawReport(float scale)
    {
      _scale = scale;

      if (g.AppConfig.GetBoolean("DebugReports"))
      {
        XElement reportDefsElement = XElement.Parse(File.ReadAllText(g.AppConfigPath + @"\ReportDefs.xml"));
        ObjectFactory2 f = new ObjectFactory2();
        _reportDefs = f.Deserialize(reportDefsElement) as ReportDefs;
      }

      Image page1 = this.PageImages.First();
      Graphics gr = Graphics.FromImage(page1);
      gr.ScaleTransform(1.0F, 1.0F);
      gr.TextContrast = 3;
      gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
      gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gr.TextRenderingHint = TextRenderingHint.AntiAlias;

      //_reportDef = _reportDefs["GeminiTicket"];

      _reportDef.LayoutReport(gr, _reportData, 1.0F);
      _reportDef.PrintReport(gr, _reportData, 1.0F);

      _reportDef.DumpTrace();

      gr.Dispose();

      return this.PageImages;
    }
  }
}
