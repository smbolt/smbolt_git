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
using Org.FM;
using Org.GS;

namespace Org.FileMgmtService
{
  public partial class FileMgmtService : ServiceBase
  {
    private FileMgmtEngine fileMgmtEngine;

    public FileMgmtService()
    {
      try
      {
        InitializeComponent();
        WinServiceParms winServiceParms = new WinServiceParms();
        winServiceParms.IsRunningAsWindowsService = true;
        fileMgmtEngine = new FileMgmtEngine(winServiceParms);
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
        fileMgmtEngine.Start();
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
        fileMgmtEngine.Stop();
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
        fileMgmtEngine.Resume();
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
        fileMgmtEngine.Pause();
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
        File.AppendText(DateTime.Now.ToString("yyyyMMdd-HHmmss.fff") + "[E] Org FileMgmtService has terminated with error: " + message);
      }

      base.Stop();
    }
  }
}
