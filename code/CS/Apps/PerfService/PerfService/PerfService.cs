using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Reflection;
using Org.WinSvc;
using Org.PF;
using Org.GS;

namespace Org.PerfService
{
  public partial class PerfService : ServiceBase
  {
    private PerfEngine perfEngine;

    public PerfService()
    {
      try
      {
        InitializeComponent();
        WinServiceParms winServiceParms = new WinServiceParms();
        winServiceParms.IsRunningAsWindowsService = true;
        perfEngine = new PerfEngine(winServiceParms);
      }
      catch (Exception ex)
      {
        TerminateService(ex.Message);
      }
    }

    protected override void OnStart(string[] args)
    {
      try
      {
        perfEngine.Start();
      }
      catch (Exception ex)
      {
        TerminateService(ex.Message);
      }
    }

    protected override void OnStop()
    {
      try
      {
        perfEngine.Stop();
      }
      catch (Exception ex)
      {
        TerminateService(ex.Message);
      }
    }

    protected override void OnContinue()
    {
      try
      {
        perfEngine.Resume();
      }
      catch (Exception ex)
      {
        TerminateService(ex.Message);
      }
    }

    protected override void OnPause()
    {
      try
      {
        perfEngine.Pause();
      }
      catch (Exception ex)
      {
        TerminateService(ex.Message);
      }
    }

    private void TerminateService(string message)
    {
      if (g.AppConfig.ContainsKey("AppLogPath"))
      {
        string path = g.AppConfig.GetCI("AppLogPath");
        File.AppendText(DateTime.Now.ToString("yyyyMMdd-HHmmss.fff") + "[E] Org PerfService has terminated with error: " + message);
      }

      base.Stop();
    }
  }
}
