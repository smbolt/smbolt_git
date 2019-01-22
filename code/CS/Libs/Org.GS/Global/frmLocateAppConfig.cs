using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Org.GS
{
  public partial class frmLocateAppConfig : Form
  {
    bool _canUseDefaultAppConfig = false;

    public string LocatedConfigFile {
      get;
      set;
    }
    public bool UseInitAppConfig {
      get;
      set;
    }

    public frmLocateAppConfig(bool canUseDefaultAppConfig)
    {
      InitializeComponent();

      _canUseDefaultAppConfig = canUseDefaultAppConfig;

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "LocateAppConfig":
          this.LocateAppConfig();
          break;

        case "UseDefaultAppConfig":
          this.UseDefaultAppConfig();
          break;

        case "Cancel":
          this.CancelDialog();
          break;
      }
    }

    private void LocateAppConfig()
    {
      dlgFileOpen.InitialDirectory = @"C:\";

      if (dlgFileOpen.ShowDialog() != DialogResult.OK)
        return;
      this.LocatedConfigFile = dlgFileOpen.FileName;

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void UseDefaultAppConfig()
    {
      this.UseInitAppConfig = true;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void CancelDialog()
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void InitializeForm()
    {
      this.UseInitAppConfig = false;
      this.LocatedConfigFile = String.Empty;

      if (!_canUseDefaultAppConfig)
      {
        lblUseInitAppConfig.Visible = false;
        btnUseDefaultAppConfig.Visible = false;
      }
    }
  }
}
