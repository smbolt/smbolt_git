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
using Org.TSK;
using Org.GS;

namespace Org.WinService
{
  public partial class WinService : ServiceBase
  {
    private TaskEngine taskEngine;

    public WinService()
    {
      try
      {
        InitializeComponent();
        TaskEngineParms taskEngineParms = new TaskEngineParms();
        WinServiceParms winServiceParms = new WinServiceParms();
        winServiceParms.IsRunningAsWindowsService = true;
        taskEngine = new TaskEngine(winServiceParms, taskEngineParms);
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
        taskEngine.Start();
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
        taskEngine.Stop();
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
        taskEngine.Resume();
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
        taskEngine.Pause();
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
        File.AppendAllText(path + @"\Error.log", DateTime.Now.ToString("yyyyMMdd-HHmmss.fff") + "[E] Org WinService has terminated with error: " + message);
      }

      base.Stop();
    }
  }
}
