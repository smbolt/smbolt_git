using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;

namespace Org.AppConfigManager
{
  public partial class frmMain : Form
  {
    private a a;
    private int formHorizontalSize = 98;
    private int formVerticalSize = 98;
    private string _bcExePath;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "LoadOriginalFile":
          LoadOriginalFile();
          break;

        case "LoadCompareFile":
          LoadCompareFile();
          break;

        case "SaveOriginalFile":
          SaveOriginalFile();
          break;

        case "SaveCompareFile":
          SaveCompareFile();
          break;

        case "SimpleCompare":
          SimpleCompare();
          break;

        case "SerializeAndCompare":
          SerializeAndCompare();
          break;

        case "DebugSerialization":
          DebugSerialization();
          break;

        case "LoadOrigAsAppConfig":
          LoadOrigAsAppConfig();
          break;

        case "TestMemoryLog":
          TestMemoryLog();
          break;

        case "Exit":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        txtOutput.Text = "An exception occurred during the initialization of the application object." + g.crlf2 +
                         "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                         "Exception Report:" + g.crlf + ex.ToReport();
        tabMain.SelectedTab = tabPageOutput;
        return;
      }

      ckStopAtMemoryLogCount.Checked = false;
      txtMemoryLogCount.Enabled = ckStopAtMemoryLogCount.Checked;

      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                           Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
      _bcExePath = g.CI("BCExePath");
    }

    private void LoadOriginalFile()
    {
      this.Cursor = Cursors.WaitCursor;

      string compareFolder = g.ExportsPath;
      string originalFilePath = compareFolder + @"\originalFile.xml";
      var originalXml = XElement.Parse(File.ReadAllText(originalFilePath));
      File.WriteAllText(originalFilePath, originalXml.ToString());
      txtOriginalFile.Text = File.ReadAllText(originalFilePath);
      txtOriginalFile.SelectionStart = 0;
      txtOriginalFile.SelectionLength = 0;
      tabMain.SelectedTab = tabPageOriginalFile;

      this.Cursor = Cursors.Default;
    }

    private void LoadCompareFile()
    {
      this.Cursor = Cursors.WaitCursor;

      string compareFolder = g.ExportsPath;
      string compareFilePath = compareFolder + @"\compareFile.xml";
      var compareXml = XElement.Parse(File.ReadAllText(compareFilePath));
      File.WriteAllText(compareFilePath, compareXml.ToString());
      txtCompareFile.Text = File.ReadAllText(compareFilePath);
      txtCompareFile.SelectionStart = 0;
      txtCompareFile.SelectionLength = 0;

      tabMain.SelectedTab = tabPageCompareFile;

      this.Cursor = Cursors.Default;
    }

    private void SaveOriginalFile()
    {
      this.Cursor = Cursors.WaitCursor;

      string compareFolder = g.ExportsPath;
      string originalFilePath = compareFolder + @"\originalFile.xml";
      var xml = XElement.Parse(txtOriginalFile.Text);
      File.WriteAllText(originalFilePath, xml.ToString());
      txtOriginalFile.Text = File.ReadAllText(originalFilePath);

      this.Cursor = Cursors.Default;
    }

    private void SaveCompareFile()
    {
      this.Cursor = Cursors.WaitCursor;

      string compareFolder = g.ExportsPath;
      string compareFilePath = compareFolder + @"\compareFile.xml";
      var xml = XElement.Parse(txtCompareFile.Text);
      File.WriteAllText(compareFilePath, xml.ToString());
      txtCompareFile.Text = File.ReadAllText(compareFilePath);

      this.Cursor = Cursors.Default;
    }

    private void SimpleCompare()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        g.ClearMemoryLog();

        g.AppConfig.ReloadFromFile();

        browserCompare.Navigate("about:blank");
        Application.DoEvents();

        string compareFolder = g.ExportsPath;

        File.Delete(compareFolder + @"\" + "compareResults.html");

        string originalFilePath = compareFolder + @"\originalFile.cmp";
        string compareFilePath = compareFolder + @"\compareFile.cmp";

        File.WriteAllText(originalFilePath, XElement.Parse(txtOriginalFile.Text).ToString());
        File.WriteAllText(compareFilePath, XElement.Parse(txtCompareFile.Text).ToString());

        string compareFileFullPath = compareFolder + @"\" + "compareResults.html";
        string bcScriptsPath = g.ImportsPath + @"\bcscriptHtml.txt";

        ProcessParms processParms = new ProcessParms();
        processParms.ExecutablePath = _bcExePath;
        processParms.Args = new string[] {
          "/silent",
          "@" + bcScriptsPath,
          originalFilePath,
          compareFilePath,
          compareFileFullPath
        };

        var processHelper = new ProcessHelper();
        var taskResult = processHelper.RunExternalProcess(processParms);

        if (File.Exists(compareFileFullPath))
          UpdateHtmlFonts(compareFileFullPath);

        browserCompare.Navigate(compareFileFullPath);
        tabMain.SelectedTab = tabPageCompareReport;
      }
      catch (Exception ex)
      {
        txtOutput.Text = "An exception occurred during the comparision of xml files." + g.crlf2 +
                         "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                         "Exception Report:" + g.crlf + ex.ToReport();
        tabMain.SelectedTab = tabPageOutput;
        return;
      }

      this.Cursor = Cursors.Default;
    }

    private void SerializeAndCompare()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        g.ClearMemoryLog();

        g.AppConfig.ReloadFromFile();

        browserCompare.Navigate("about:blank");
        Application.DoEvents();

        string compareFolder = g.ExportsPath;

        File.Delete(compareFolder + @"\" + "compareResults.html");

        string originalFilePath = compareFolder + @"\originalFile.cmp";
        string compareFilePath = compareFolder + @"\compareFile.cmp";

        File.WriteAllText(originalFilePath, XElement.Parse(txtOriginalFile.Text).ToString());

        var f = new ObjectFactory2();

        var o = f.Deserialize(XElement.Parse(txtOriginalFile.Text));
        var compareXml = f.Serialize(o);
        File.WriteAllText(compareFilePath, compareXml.ToString());

        string compareFileFullPath = compareFolder + @"\" + "compareResults.html";
        string bcScriptsPath = g.ImportsPath + @"\bcscriptHtml.txt";

        ProcessParms processParms = new ProcessParms();
        processParms.ExecutablePath = _bcExePath;
        processParms.Args = new string[] {
          "/silent",
          "@" + bcScriptsPath,
          originalFilePath,
          compareFilePath,
          compareFileFullPath
        };

        var processHelper = new ProcessHelper();
        var taskResult = processHelper.RunExternalProcess(processParms);

        if (taskResult.TaskResultStatus != TaskResultStatus.Success)
        {
          txtOutput.Text = taskResult.NotificationMessage;
          tabMain.SelectedTab = tabPageOutput;
          this.Cursor = Cursors.Default;
          return;
        }

        if (File.Exists(compareFileFullPath))
          UpdateHtmlFonts(compareFileFullPath);

        browserCompare.Navigate(compareFileFullPath);
        tabMain.SelectedTab = tabPageCompareReport;
      }
      catch (Exception ex)
      {
        txtOutput.Text = "An exception occurred during the comparision of xml files." + g.crlf2 +
                         "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                         "Exception Report:" + g.crlf + ex.ToReport();
        tabMain.SelectedTab = tabPageOutput;
        return;
      }

      this.Cursor = Cursors.Default;
    }

    private void DebugSerialization()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        g.ClearMemoryLog();

        browserCompare.Navigate("about:blank");
        Application.DoEvents();

        var f = new ObjectFactory2();
        f.LogToMemory = true;
        f.InDiagnosticsMode = true;

        if (ckStopAtMemoryLogCount.Checked && txtMemoryLogCount.Text.IsNumeric())
        {
          f.StopAtMemoryLogCount = true;
          f.MemoryLogCount = txtMemoryLogCount.Text.ToInt32();
        }

        f.SetDebugBreak(txtBreakOnThisElement.Text, cboBreakProcess.Text);

        switch (cboBreakProcess.Text)
        {
          case "Deserialization":
            f.DeserializeDebugBreak = txtBreakOnThisElement.Text.Trim();
            break;

          case "Serialization":
            f.SerializeDebugBreak = txtBreakOnThisElement.Text.Trim();
            break;

          case "Both":
            f.DeserializeDebugBreak = txtBreakOnThisElement.Text.Trim();
            f.SerializeDebugBreak = txtBreakOnThisElement.Text.Trim();
            break;
        }

        var o = f.Deserialize(XElement.Parse(txtOriginalFile.Text));
        var compareXml = f.Serialize(o);

        txtOutput.Text = "Deserialization and Serialization were successful." + g.crlf2 +
                         "Memory Log" + g.crlf2 +
                         g.MemoryLog;

        tabMain.SelectedTab = tabPageOutput;
      }
      catch (Exception ex)
      {
        txtOutput.Text = "An exception occurred during the comparision of xml files." + g.crlf2 +
                         "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                         "Exception Report:" + g.crlf + ex.ToReport();
        tabMain.SelectedTab = tabPageOutput;
        return;
      }

      this.Cursor = Cursors.Default;
    }

    private void LoadOrigAsAppConfig()
    {
      try
      {
        g.ClearMemoryLog();

        var appConfig = new AppConfig();
        appConfig.LoadFromString(txtOriginalFile.Text);

        MessageBox.Show("The original file text successfully deserialized into the AppConfig object.", "AppConfigManager - Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        txtOutput.Text = "An exception occurred attempting to deserialize the original file to AppConfig." + g.crlf2 +
                         "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                         "Exception Report:" + g.crlf + ex.ToReport();
        tabMain.SelectedTab = tabPageOutput;
        return;

      }
    }

    private void UpdateHtmlFonts(string path)
    {
      string htmlText = File.ReadAllText(path);
      htmlText = htmlText.Replace("body { font-family: sans-serif; font-size: 11pt;", "body { font-family: lucida console; font-size: 8pt;");
      htmlText = htmlText.Replace("monospace; font-size: 10pt; }", "lucida console; font-size: 8pt; }");
      File.WriteAllText(path, htmlText);
    }

    private void TestMemoryLog()
    {
      g.MemoryLogLimit = 300000;
      g.MemoryLogTruncateSize = 400000;

      for (int i = 0; i < 50000; i++)
      {
        g.LogToMemory("abcdefghijklmnopqrstuvwxy");
      }

      txtOriginalFile.Text = g.MemoryLog;

      tabMain.SelectedTab = tabPageOriginalFile;
    }

    private void ckStopAtMemoryLogCount_CheckedChanged(object sender, EventArgs e)
    {
      txtMemoryLogCount.Enabled = ckStopAtMemoryLogCount.Checked;
    }

  }
}
