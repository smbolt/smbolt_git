using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Ebcdic;
using Org.GS;

namespace Org.EbcdicConverter
{
  public partial class frmMain : Form
  {
    private string _initialDirectory;
    private string _lastInputFileAccessed;
    private DMapSet _dmapSet;


    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object control, EventArgs e)
    {
      switch (control.ActionTag())
      {
        case "Browse":
          BrowseForFile();
          break;

        case "RunCommand":
          RunCommand();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunCommand()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {



      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "EBCDIC Converter - Application Object 'a' Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

      }

      this.Cursor = Cursors.Default;
    }


    private void BrowseForFile()
    {
      dlgFileOpen.InitialDirectory = _initialDirectory.IsNotBlank() ? _initialDirectory : @"C:\";
      if (dlgFileOpen.ShowDialog() == DialogResult.OK)
      {
        txtFileName.Text = dlgFileOpen.FileName;
        _lastInputFileAccessed = txtFileName.Text;
        _initialDirectory = Path.GetDirectoryName(_lastInputFileAccessed);
        string lastFileAccessed = g.CI("LastInputFileAccessed");
        if (lastFileAccessed != _lastInputFileAccessed)
        {
          g.AppConfig.SetCI("LastInputFileAccessed", _lastInputFileAccessed);
          g.AppConfig.Save();
        }
      }

    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "EBCDIC Converter - Application Object 'a' Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.SetInitialSizeAndLocation();

        _initialDirectory = String.Empty;
        _lastInputFileAccessed = g.CI("LastInputFileAccessed");
        if (_lastInputFileAccessed.IsNotBlank())
        {
          _initialDirectory = Path.GetDirectoryName(_lastInputFileAccessed);
          txtFileName.Text = _lastInputFileAccessed;
          txtFileName.SelectionStart = 0;
          txtFileName.SelectionLength = 0;
        }

        using (var utility = new Utility())
        {
          _dmapSet = utility.LoadDMapSet(g.ImportsPath);
        }

        string commandsText = File.ReadAllText(g.ImportsPath + @"\ConvertFile.os");
        string condensed = commandsText.CondenseCode();


        //List<string> commands = commandsText.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
        //cboCommand.LoadItems(commands, true);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "EBCDIC Converter - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      
    }

    private void cboCommand_SelectedIndexChanged(object sender, EventArgs e)
    {
      cboCommand.SelectionStart = 0;
      cboCommand.SelectionLength = 0;
      btnRunCommand.Enabled = cboCommand.Text.IsNotBlank();

      if (cboCommand.Text.IsNotBlank())
        btnRunCommand.Focus();
    }
  }
}
