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
using Org.GS;

namespace Org.CodeCompare
{
  public partial class frmCode : Form
  {
    private string _filePath;

    public frmCode(string filePath)
    {
      InitializeComponent();
      _filePath = filePath;


      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          Save();
          break;

        case "SaveAs":
          SaveAs();
          break;

        case "Close":
          this.Close();
          break;
      }
    }

    private void Save()
    {
      try
      {
        File.WriteAllText(_filePath, txtCode.Text);
        lblStatus.Text = _filePath + " - saved."; 
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to save the file." + g.crlf2 + ex.ToReport(), "File Utility - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SaveAs()
    {

    }

    private void InitializeForm()
		{
			this.Text = _filePath;

			if (!File.Exists(_filePath))
			{
				txtCode.Text = "*** FILE NOT FOUND ***" + g.crlf2 + "Path: " + _filePath;
				return;
			}

      string code = File.ReadAllText(_filePath);

      txtCode.Text = code;

      lblStatus.Text = _filePath + " - displayed."; 
    }

    private void txtCode_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.S:
          if (e.Control)
            Save();
          break;

        case Keys.C:
          if (e.Control)
            this.Close();
          break;
      }
    }
  }
}
