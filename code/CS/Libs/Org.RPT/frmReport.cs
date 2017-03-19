using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection;
using Org.GS;
using Org.RPT;

namespace Org.RPT
{
  public partial class frmReport : Form
  {        
    private ReportDefs _rDefs;
    private bool _isModal;
    private ReportData _rData;

    private List<Image> pageImages; 

    public frmReport()
    {
      InitializeComponent();
      InitializeApplication();
    }

    public frmReport(ReportData rData, ReportDefs rDefs)
    {
      _rData = rData;
      _rDefs = rDefs;

      InitializeComponent();
      InitializeApplication();

      _isModal = true;
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Refresh":
          LoadData();
          RefreshReport();
          break;

        case "Close":
          this.Close();
          break;
      }
    }

    private void LoadData()
    {
      _rData = new ReportData();

      _rData.Add("IssueId", "50001");
      _rData.Add("IssueSummary", "Bug found in software");
      _rData.Add("ProjectName", "New Product");
      _rData.Add("VersionName", "3.4.3");
      _rData.Add("Status", "Closed");
      _rData.Add("MagicNumber", "2001");
      _rData.Add("RiskLevel", "Medium");
      _rData.Add("ReportedBy", "Larry");
      _rData.Add("Resources", "Bill, George");
      _rData.Add("CustomerDesc", "Best Customer");
      _rData.Add("RelatedTickets", "63001,63002");
      _rData.Add("ETScriptsOrDbChangeReqd", "N");
      _rData.Add("SysParmsAddedorChanged", "N");
      _rData.Add("OffCycleRelease", "N");
      _rData.Add("Resolution", "Program Fixed");
      _rData.Add("IssueType", "Defect");
      _rData.Add("BudgetType", "Development");
      _rData.Add("ExecuTrakVersion", "5.0.0.0");
      _rData.Add("LongDescription", "A problem as described by the customer.");
      _rData.Add("Comments", "This ticket can be closed.");
    }

    private void RefreshReport()
    {
      this.DrawReport();
    }

    public void UpdateReportContent(ReportData rData, ReportDefs rDefs)
    {
      _rData = rData;
      _rDefs = rDefs;

      if (pbMain.Image != null)
      {
        Graphics gr = Graphics.FromImage(pbMain.Image);
        gr.Clear(Color.White);
        gr.Dispose();
        Application.DoEvents();
      }

      pbMain.Visible = true;
      DrawReport();
      pbMain.Invalidate();

    }

    private void DrawReport()
    {
      ReportDef rDef = _rDefs["GeminiTicket"];
      QuickReport rpt = new QuickReport(_rData, rDef);

      pageImages = rpt.DrawReport(1.0F);
      pbMain.Image = pageImages[0];
      pbMain.Invalidate();
    }

    private void frmQuickReport_Shown(object sender, EventArgs e)
    {
      if (!_isModal)
        return;

      pbMain.Visible = true;
      DrawReport();
    }

    private void InitializeApplication()
    {
      _isModal = false;
      pbMain.Size = new Size(850, 1100);
      pbMain.Location = new Point(50, 50);
      pbSpacer.Location = new Point(pbMain.Left, pbMain.Top + pbMain.Height);

      XmlMapper.AddAssembly(Assembly.GetExecutingAssembly());       

      string reportDefs = File.ReadAllText(g.ReportsPath + @"\ReportDefs.xml");
      XElement reportDefsXml = XElement.Parse(reportDefs); 
      var f = new ObjectFactory2();
      _rDefs = f.Deserialize(reportDefsXml) as ReportDefs;


    }
  }
}
