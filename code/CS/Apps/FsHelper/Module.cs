using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using Org.MOD;
using Org.MOD.Contracts;
using Org.MOD.Concrete;
using Org.SF;
using Org.GS;

namespace Org.FsHelper
{
  public class Module : WinAppModule
  {

    private a a;
    private frmMain _frmMain;

    public Module()
      : base(Assembly.GetExecutingAssembly())
    {
    }

    public override void Run()
    {
      try
      {
        a = new a();

        var fSplash = new frmSplash(base.ModuleName, base.SplashImage, base.ModuleNameAndVersionString, base.ModuleCopyrightString);

        fSplash.SetMessage(base.ModuleName + " is starting up...");
        fSplash.Show();
        Application.DoEvents();

        System.Threading.Thread.Sleep(500);
        fSplash.SetMessage(base.ModuleName + " is starting up...");
        Application.DoEvents();

        System.Threading.Thread.Sleep(500);
        fSplash.SetMessage(base.ModuleName + " initialization complete.");
        Application.DoEvents();

        System.Threading.Thread.Sleep(500);
        fSplash.Close();
        fSplash.Dispose();
        fSplash = null;

        //g.AppConfig.ConfigName = base.ModuleConfigName;
        g.AppConfig.ModuleName = base.ModuleName;
        g.AppConfig.ResetPcKeys();

        _frmMain = new frmMain(this);
        _frmMain.ShowDialog();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during module startup." + g.crlf2 + ex.ToReport(), "Module Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public override void ShutDownForAppDomainUnload()
    {
      try
      {
        _frmMain.ShutDownForAppDomainUnload();
        base.IsReadyForAppDomainUnload = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to ShutDownForAppDomainUnload." + g.crlf2 + ex.ToReport(), "Module Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}