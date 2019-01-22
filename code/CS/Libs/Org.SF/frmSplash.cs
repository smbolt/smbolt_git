using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace Org.SF
{
  public partial class frmSplash : Form
  {
    private string _programName;

    public string VersionInfo
    {
      set {
        lblVersion.Text = value;
      }
    }

    public string CopyrightInfo
    {
      set {
        lblCopyright.Text = value;
      }
    }

    public bool Escaped {
      get;
      set;
    }
    public bool UseSmallSize
    {
      set
      {
        if (value)
          ResizeToSmallSize();
      }
    }
    public frmSplash(string programName, Image splashImage, string version, string copyright)
    {
      InitializeComponent();
      this.UseSmallSize = false;
      _programName = programName;
      pbMain.Image = splashImage;
      lblCopyright.Text = copyright;
      lblVersion.Text = version;

      InitializeForm();
    }

    private void InitializeForm()
    {

      this.Load += new EventHandler(this.frmSplash_Load);
      this.KeyUp += new KeyEventHandler(this.frmSplash_KeyUp);

    }

    private void frmSplash_Load(object sender, EventArgs e)
    {
      this.Refresh();
      Application.DoEvents();
      lblReset.Visible = false;
    }

    public void SetMessage(string Message)
    {
      lblMessage.Text = Message;
      Application.DoEvents();
    }

    private void frmSplash_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Escape:
        {
          this.Escaped = true;
          lblReset.Visible = true;
          SetMessage("Splash screen has been escaped...");
          break;
        }
      }
    }

    private void ResizeToSmallSize()
    {
      lblVersion.Location = new Point(0, 0);
      lblCopyright.Location = new Point(0, 0);
      lblReset.Location = new Point(0, 0);
      lblVersion.Visible = false;
      lblCopyright.Visible = false;
      lblReset.Visible = false;

      pbMain.Location = new Point(20, 20);
      lblMessage.Location = new Point(45, 130);
      lblMessage.Size = new Size(340, 23);
      this.Size = new Size(413, 175);
    }

    private void frmSplash_Shown(object sender, EventArgs e)
    {

    }
  }
}
