using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Diff;
using Org.Diff.DiffBuilder;
using Org.Diff.DiffBuilder.Model;
using Org.GS;

namespace FileCompareWorkbench2
{
  public partial class frmMain : Form
  {
    private bool _isFirstShowing;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = sender.ActionTag();

      switch (action)
      {
        case "BrowseFile1":
        case "BrowseFile2":
          BrowseFile(action);
          break;

        case "CompareFiles":
          CompareFiles();
          break;

        case "Exit":
          Close();
          break;
      }
    }

    private void BrowseFile(string action)
    {
      if (dlgOpenFile.ShowDialog() == DialogResult.OK)
      {
        if (action == "BrowseFile1")
          txtFile1.Text = dlgOpenFile.FileName;
        else
          txtFile2.Text = dlgOpenFile.FileName;
      }
    }

    private void CompareFiles()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        txtOut.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(200);

        var parms = GetFileCompareParms(txtFile1.Text, txtFile2.Text);

        using (var fileCompareEngine = new Org.Diff.FileCompareEngine())
        {
          var fileCompareResult = fileCompareEngine.CompareFiles(parms);

          switch (fileCompareResult.FileCompareStatus)
          {
            case Org.Diff.FileCompareStatus.Matched:
              txtOut.Text = "Files match";
              if (parms.CreateFileComparisionReport)
                txtOut.Text += g.crlf2 + fileCompareResult.FileCompareReport;
              break;

            case Org.Diff.FileCompareStatus.NotMatched:
              txtOut.Text = "Files do not match";
              if (parms.CreateFileComparisionReport)
                txtOut.Text += g.crlf2 + fileCompareResult.FileCompareReport;
              break;


            case Org.Diff.FileCompareStatus.CompareOperationFailed:
              txtOut.Text = "*** ERROR ***" + g.crlf2 +
                            "File Comparision Failed" + g.crlf2 +
                            fileCompareResult.Exception.ToReport();
              break;
          }
        }        

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to compare the files." + g.crlf2 + ex.ToReport(),
                        "FileComareWorkbench2 - File Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private FileCompareParms GetFileCompareParms(string file1, string file2)
    {
      var parms = new FileCompareParms();
      parms.LeftPath = file1;
      parms.RightPath = file2;

      switch (cboReportFormat.Text)
      {
        case "No report":
          parms.CreateFileComparisionReport = false;
          parms.FileCompareReportLayout = FileCompareReportLayout.Inline;
          break;

        case "Inline report":
          parms.CreateFileComparisionReport = true;
          parms.FileCompareReportLayout = FileCompareReportLayout.Inline;
          break;

        case "Side by side report":
          parms.CreateFileComparisionReport = true;
          parms.FileCompareReportLayout = FileCompareReportLayout.SideBySide;
          break;
      }

      return parms;
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrerd while attempting to initialize the application object 'a'." + g.crlf2 + 
                        ex.ToReport(), "FileCompareWorkbench2 - Application Object Initialization Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      try
      {
        _isFirstShowing = true;

        this.SetInitialSizeAndLocation();
        btnCompareFiles.Enabled = false;

        cboReportFormat.SelectedIndex = 0;

        txtFile1.Text = g.CI("File1");
        txtFile2.Text = g.CI("File2");
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(), 
                        "FileCompareWorkbench2 - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void TextValueChanged(object sender, EventArgs e)
    {
      CheckForChanges();
    }

    private void CheckForChanges()
    {
      if (txtFile1.Text.IsBlank() || !File.Exists(txtFile1.Text))
      {
        btnCompareFiles.Enabled = false;
        return;
      }

      if (txtFile2.Text.IsBlank() || !File.Exists(txtFile2.Text))
      {
        btnCompareFiles.Enabled = false;
        return;
      }

      btnCompareFiles.Enabled = true;
      btnCompareFiles.Focus();
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      btnBrowseFile1.Focus();
      CheckForChanges();

      _isFirstShowing = false;
    }
  }
}
