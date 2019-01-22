using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.SF
{
  public partial class frmProgress : Form
  {
    private string _titleBarText;
    private string _description;
    private string _processName;

    private bool _processFinished = false;
    public bool ProcessFinished
    {
      get {
        return _processFinished;
      }
      set {
        _processFinished = value;
      }
    }

    private bool _userCanceled = false;
    public bool UserCanceled
    {
      get {
        return _userCanceled;
      }
      set {
        _userCanceled = value;
      }
    }

    public int ProgressValue
    {
      set
      {
        if (value > progressBar.Maximum)
          progressBar.Value = progressBar.Maximum;
        else
          progressBar.Value = value;
        Application.DoEvents();
      }
    }

    public string Message
    {
      set
      {
        lblDescription.Text = value;
        Application.DoEvents();
      }
    }

    public frmProgress(string titleBarText, string description, string processName)
    {
      InitializeComponent();

      _titleBarText = titleBarText;
      _description = description;
      _processName = processName;

      InitializeForm();
    }

    private void InitializeForm()
    {
      this.Text = _titleBarText;
      lblDescription.Text = _description;

      progressBar.Maximum = 100;
      progressBar.Value = 0;
    }

    private void frmDataLoadProgress_FormClosing(object sender, FormClosingEventArgs e)
    {
      CheckProcessCancel();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      CheckProcessCancel();
    }

    private void CheckProcessCancel()
    {
      if (_processFinished)
        return;

      if (MessageBox.Show("Are you sure you want to cancel the " + _processName + "?", _titleBarText + " - Confirm Cancel",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;

      _userCanceled = true;
    }
  }
}
