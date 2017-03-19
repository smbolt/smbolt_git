using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.SoftwareUpdates
{
  public partial class frmUpdateError : Form
  {
    private SoftwareUpdateError _error;
    private SoftwareUpdateErrorAction _errorAction;
    public SoftwareUpdateErrorAction ErrorAction
    {
      get { return _errorAction; }
    }    

    public frmUpdateError(SoftwareUpdateError error)
    {
      InitializeComponent();
      _error = error;
      _errorAction = SoftwareUpdateErrorAction.NotSet;
      InitializeForm();
    }

    private void InitializeForm()
    {
      lblTitle.BackColor = _error.BackgroundColor;
      pbError.BackColor = _error.BackgroundColor;
      pnlTop.BackColor = _error.BackgroundColor;

      this.Text = _error.Title;
      lblTitle.Text = _error.Title;
      txtErrorMessage.Text = _error.ErrorDetail;
      cboAction.DataSource = _error.ErrorActions;
      cboAction.Focus();
      btnOK.Enabled = false;
    }

    private void cboAction_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = cboAction.SelectedIndex > -1;

      switch(cboAction.Text)
      {
        case "":
          lblActionExplanation.Text = "Please select an action from the drop-down above to respond to the error.";
          break;

        case "Ignore":
          _errorAction = SoftwareUpdateErrorAction.Ignore;
          lblActionExplanation.Text = "The error will be ignored.  This program will check for software updates the next time it is started.";
          break;

        case "Disable software update checks for today":
          _errorAction = SoftwareUpdateErrorAction.DisableCheckingToday;
          lblActionExplanation.Text = "This program will not check for software updates again today.  " + 
                                      "Use the configuration wizard to change settings for software updates.";
          break;

        case "Disable software update checks for 2 days":
          _errorAction = SoftwareUpdateErrorAction.DisableChecking2Days;
          lblActionExplanation.Text = "This program will not check for software updates again today and tomorrow.  " + 
                                      "Use the configuration wizard to change settings for software updates.";
          break;

        case "Disable software update checks for 1 week":
          _errorAction = SoftwareUpdateErrorAction.DisableChecking1Week;
          lblActionExplanation.Text = "This program will not check for software updates until after 1 week has passed.  " + 
                                      "Use the configuration wizard to change settings for software updates.";
          break;

        case "Disable software update checks permanently":
          _errorAction = SoftwareUpdateErrorAction.DisableCheckingPermanently;
          lblActionExplanation.Text = "Software updates checking will be disabled.  Use the configuration wizard to change settings for software updates.";
          break;
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
