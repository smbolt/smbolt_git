using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Dx.Business;
//using Org.DxDocs;
using Org.GraphicReports;
using Org.GraphicReports.Business;
using Org.GS;

namespace Org.GraphicReportTest
{
  public partial class frmMain : Form
  {
    private a a;
    private List<string> _reportList;
    private string _initialReportName;
    private string _initialReportFilePath;
    private RigSet _rigSet;
    private RigSet _rptRigSet;
    private ReportParms _reportParms;
    private string _dataFileFullPath;
    private DateTime _reportStartDate;
    private DateTime _reportEndDate;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RunReport":
          RunReport();
          break;

        case "RefreshReport":
          RefreshReport();
          break;

        case "GetRigSetForSpan":
          GetRigSetForSpan();
          break;

        case "Browse":
          LocateDataFile();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunReport()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        if (_rigSet == null)
          _rptRigSet = GetRigSet(_dataFileFullPath);

        txtReport.Text = _rptRigSet.GetReport();

        _reportParms = new ReportParms();
        _reportParms.PageOrientation = PageOrientation.Landscape;
        pbReport.Size = _reportParms.ActualPageSize.ToSize();
        pnlShadow.Size = pbReport.Size;

        using (var gx = pbReport.CreateGraphics())
        {
          using (var reportEngine = new GraphicalReportEngine(_reportParms))
          {
            var reportImageSet = reportEngine.ProduceReport(null);
            if (reportImageSet.Count > 0)
            {
              var reportImage = reportImageSet.Values.First();
              Image img = reportImage.Image;
              gx.DrawImage(reportImage.Image, new Point(0, 0));
            }
          }
        }

        tabMain.SelectedTab = tabPageReport;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to create the '" + cboReports.Text + "' report." + g.crlf2 + ex.ToReport(),
                        "Graphic Report Test - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void RefreshReport()
    {
      Graphics gx = pbReport.CreateGraphics();
      gx.Clear(Color.White);
      Application.DoEvents();
      gx.Dispose();
      System.Threading.Thread.Sleep(40);

      pbReport.Size = _reportParms.ActualPageSize.ToSize();
      pnlShadow.Size = pbReport.Size;

      pbReport.Invalidate();
    }

    private RigSet GetRigSet(string fullFilePath)
    {
      try
      {
        int pad9Number = 9000000;


        var rigSet = new RigSet(_reportParms);

        string line = null;
        Rig rig = null;
        Pad pad = null;

        using (var sr = new StreamReader(fullFilePath))
        {
          while ((line = sr.ReadLine()) != null)
          {
            if (line != null)
            {
              string[] tokens = line.Split(Constants.CommaDelimiter);

              if (tokens[0] == "Rig")
                continue;

              string rigName = tokens[0].Trim();

              if (rigName == "0" || rigName.IsBlank())
                continue;

              if (!rigSet.ContainsKey(rigName))
              {
                rig = new Rig(rigSet);
                rig.Name = rigName;
                rigSet.Add(rig.Name, rig);
              }

              rig = rigSet[rigName];

              string padNumber = tokens[1];
              int padNbr = -1;
              if (padNumber.IsNumeric() && padNumber.ToInt32() > 0)
                padNbr = padNumber.ToInt32();

              if (padNbr < 1)
                continue;

              if (padNbr == 9)
              {
                padNbr = pad9Number++;
              }

              if (!rig.PadSet.ContainsKey(padNbr))
              {
                pad = new Pad(rig);
                pad.PadNumber = padNbr;
                pad.PadName = tokens[2].Trim();
                rig.PadSet.Add(pad.PadNumber, pad);
              }

              pad = rig.PadSet[padNbr];

              int wellPtr = 9;

              for (int i = 0; i < 6; i++)
              {
                int wellNumber = tokens[wellPtr].ToInt32();
                if (wellNumber > 0)
                {
                  var well = new Well(pad);
                  well.WellNumber = wellNumber;
                  well.WellName = tokens[wellPtr - 6].Trim();
                  well.SpudDate = tokens[wellPtr + 6].ToDateTime();
                  well.CompletionDate = tokens[wellPtr + 12].ToDateTime();
                  well.UnitNumber = tokens[wellPtr + 34].ToInt32();
                  well.UnitName = tokens[wellPtr + 40].Trim();
                  well.WellOrdinal = i + 1;

                  if (pad.WellSet.ContainsKey(well.WellOrdinal))
                  {
                    string what = "what";
                  }

                  pad.WellSet.Add(well.WellOrdinal, well);
                }
                wellPtr++;
              }
            }
          }
          sr.Close();
        }

        return rigSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to create the RigSet.", ex);
      }
    }

    private void GetRigSetForSpan()
    {
      if (_rigSet == null)
        _rigSet = GetRigSet(_dataFileFullPath);

      var span = new DateSpan(dtpStartDate.Value, dtpEndDate.Value);
      _rptRigSet = _rigSet.GetForDateSpan(span);
      txtReport.Text = _rptRigSet.GetReport();
    }

    private DxWorkbook GetWorkbook(string fullFilePath)
    {
      //var wb = Utility.GetWorkbook(fullFilePath);

      DxWorkbook wb = new DxWorkbook();
      wb.FilePath = fullFilePath;


      string line = null;

      using (var sr = new StreamReader(fullFilePath))
      {
        while ((line = sr.ReadLine()) != null)
        {
          if (line != null)
          {
            string[] tokens = line.Split(Constants.CommaDelimiter);


          }
        }
        sr.Close();
      }


      return wb;
    }

    private void LocateDataFile()
    {
      string lastDataFolder = g.CI("LastDataFolder");
      if (lastDataFolder.IsBlank())
        lastDataFolder = g.ImportsPath;

      if (!Directory.Exists(lastDataFolder))
        lastDataFolder = g.ImportsPath;

      dlgFileOpen.InitialDirectory = lastDataFolder;

      if (dlgFileOpen.ShowDialog() == DialogResult.OK)
      {
        _dataFileFullPath = dlgFileOpen.FileName;
        txtFilePath.Text = _dataFileFullPath;
      }
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

        btnRunReport.Enabled = false;
        btnBrowse.Enabled = false;

        _dataFileFullPath = String.Empty;

        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

        _reportList = g.GetList("ReportList");
        LoadReportList(_reportList);


        _reportParms = new ReportParms();
        _reportParms.PageOrientation = PageOrientation.Landscape;
        _reportParms.ReportDiagnostics.DiagnosticsActive = true;
        _reportParms.ReportDiagnostics.ShowMargins = true;

        if (g.CIExists("LeftMargin"))
          _reportParms.Margins.Left = g.CI("LeftMargin").ToFloat() * 100;
        if (g.CIExists("TopMargin"))
          _reportParms.Margins.Top = g.CI("TopMargin").ToFloat() * 100;
        if (g.CIExists("RightMargin"))
          _reportParms.Margins.Right = g.CI("RightMargin").ToFloat() * 100;
        if (g.CIExists("BottomMargin"))
          _reportParms.Margins.Bottom = g.CI("BottomMargin").ToFloat() * 100;
        if (g.CIExists("PageSize"))
          _reportParms.PageSize  = g.ToEnum<PageSize>(g.CI("PageSize"), PageSize.Letter);

        pbReport.Size = _reportParms.ActualPageSize.ToSize();
        pnlShadow.Size = _reportParms.ActualPageSize.ToSize();

        if (g.CIExists("PrintCalendar"))
          _reportParms.PrintCalendar = g.CI("PrintCalendar").ToBoolean();

        if (g.CIExists("ReportStartDate") && g.CIExists("ReportEndDate"))
        {
          _reportStartDate = g.CI("ReportStartDate").CCYYMMDDToDateTime();
          _reportEndDate = g.CI("ReportEndDate").CCYYMMDDToDateTime();
          dtpStartDate.Value = _reportStartDate;
          dtpEndDate.Value = _reportEndDate;
          _reportParms.DateSpan = new DateSpan(_reportStartDate, _reportEndDate);
        }

        _initialReportName = g.CI("InitialReportName");
        _initialReportFilePath = g.CI("InitialReportFilePath");

        if (_initialReportName.IsNotBlank())
        {
          for (int i = 0; i < cboReports.Items.Count; i++)
          {
            if (cboReports.Items[i].ToString() == _initialReportName)
            {
              cboReports.SelectedIndex = i;
              continue;
            }
          }
        }

        if (_initialReportFilePath.IsNotBlank())
        {
          _dataFileFullPath = _initialReportFilePath;
          txtFilePath.Text = _initialReportFilePath;
          _rigSet = GetRigSet(txtFilePath.Text);
          GetRigSetForSpan();
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + g.crlf2 + ex.ToReport(),
                        "Graphic Report Test - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void LoadReportList(List<string> reportList)
    {
      cboReports.Items.Clear();
      cboReports.Items.Add(String.Empty);
      foreach (string report in reportList)
        cboReports.Items.Add(report);
    }

    private void cboReports_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboReports.Text.IsBlank())
      {
        btnBrowse.Enabled = false;
        btnRunReport.Enabled = false;
        txtFilePath.Clear();
      }
      else
      {
        btnBrowse.Enabled = true;
        txtFilePath.Clear();
      }
    }

    private void txtFilePath_TextChanged(object sender, EventArgs e)
    {
      if (txtFilePath.Text.IsBlank())
      {
        btnRunReport.Enabled = false;
      }
      else
      {
        btnRunReport.Enabled = true;
      }
    }

    private void pbReport_Paint(object sender, PaintEventArgs e)
    {
      try
      {

        if (_rptRigSet == null)
        {
          ShowNoReport(e.Graphics);
          return;
        }

        e.Graphics.Clear(Color.White);

        _rptRigSet.DrawReport(e.Graphics);
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to draw the report." + g.crlf2 + ex.ToReport(), "Graphics Report Test - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ShowNoReport(Graphics gx)
    {
      gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gx.TextRenderingHint = TextRenderingHint.AntiAlias;
      gx.DrawString("No Rig Set", new Font("Calibri", 14.0F, FontStyle.Bold), Brushes.Black, new PointF(50.0F, 100.0F));
    }
  }
}
