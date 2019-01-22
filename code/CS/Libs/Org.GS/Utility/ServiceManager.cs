using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.ServiceModel;

namespace Org.GS
{
  [Serializable]
  public class ServiceManager : IDisposable
  {
    private bool _isDryRun;

    public ServiceManager(bool isDryRun = false)
    {
      _isDryRun = isDryRun;
    }

    public WinServiceSet GetWinServices()
    {
      try
      {
        WinServiceSet winServiceSet = new WinServiceSet();
        ServiceController[] services = ServiceController.GetServices();
        foreach (var service in services)
        {
          WinService svc = new WinService();
          svc.Name = service.ServiceName;
          svc.MachineName = service.MachineName;
          svc.WinServiceStatus = g.ToEnum(service.Status, WinServiceStatus.Unknown);
          svc.CanPauseAndContinue = service.CanPauseAndContinue;
          svc.CanStop = service.CanStop;
          winServiceSet.Add(svc);
        }

        return winServiceSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get a list of Windows Services.", ex);
      }
    }

    public WinService GetWinService(string serviceName)
    {
      WinService winService = new WinService();
      using (ServiceController serviceController = new ServiceController(serviceName))
      {
        try // will throw exception if any property does not exist
        {
          winService.Name = serviceController.ServiceName;
          winService.MachineName = serviceController.MachineName;
          winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
          winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
          winService.CanStop = serviceController.CanStop;
          return winService;
        }
        catch (Exception ex)
        {
          if (ex.Message.Contains(" was not found "))
            return null;
          else
            throw new Exception("An exception occurred attempting to get Windows Service '" + serviceName + "'.", ex);
        }
      }
    }

    public TaskResult StartWinService(string winServiceName)
    {
      TaskResult taskResult = new TaskResult("StartWinService");
      WinService winService = new WinService();

      try
      {
        using (ServiceController serviceController = new ServiceController(winServiceName))
        {
          try // will throw exception if any property does not exist
          {
            winService.Name = serviceController.ServiceName;
            winService.MachineName = serviceController.MachineName;
            winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
            winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
            winService.CanStop = serviceController.CanStop;
            taskResult.Object = winService;
          }
          catch (Exception ex)
          {
            if (ex.Message.Contains(" was not found "))
              return taskResult.Failed(ex.Message);
            else
              return taskResult.Failed("An exception occurred attempting to manage the Windows Service." + g.crlf2 + ex.ToReport());
          }

          int secondsLimit = 30;

          switch (serviceController.Status)
          {
            case ServiceControllerStatus.Running:
              return taskResult.Success("Windows Service '" + winServiceName + "' is already Running.");

            case ServiceControllerStatus.StopPending:
            case ServiceControllerStatus.PausePending:
            case ServiceControllerStatus.ContinuePending:
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be Started when in '" + serviceController.Status.ToString() + "' status.");

            case ServiceControllerStatus.StartPending:
              return taskResult.Success("Windows Service '" + winServiceName + "' is already in StartPending status.");

            case ServiceControllerStatus.Paused:
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be Started when in Paused status - must be stopped first.");

            default:
              if (_isDryRun)
                return taskResult.Success("Windows Service '" + winServiceName + "' was ready to be Started, but returned because the command was a DryRun.");
              serviceController.Start();

              while (serviceController.Status != ServiceControllerStatus.Running && secondsLimit > 0)
              {
                secondsLimit--;
                System.Threading.Thread.Sleep(1000);
                serviceController.Refresh();
              }

              serviceController.Refresh();
              winService.WinServiceStatus = serviceController.Status.ToEnum<WinServiceStatus>(WinServiceStatus.Unknown);
              if (serviceController.Status != ServiceControllerStatus.Running)
              {
                if (serviceController.Status == ServiceControllerStatus.StartPending)
                  return taskResult.Warning("Service Start command was issued but the Windows Service + '" + winServiceName + "' is still in 'StartPending' status.");

                return taskResult.Failed("Service Start command was issued but the Windows Service + '" + winServiceName + "' is still in '" + serviceController.Status.ToString() + "' status.");
              }

              return taskResult.Success("Windows Service '" + winServiceName + "' was successfully Started.");
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while trying to start Windows Service '" + winServiceName + "'." + g.crlf2 + ex.ToReport());
      }
    }

    public TaskResult StopWinService(string winServiceName)
    {
      TaskResult taskResult = new TaskResult("StopWinService");
      WinService winService = new WinService();
      try
      {
        using (ServiceController serviceController = new ServiceController(winServiceName))
        {
          try // will throw exception if any property does not exist
          {
            winService.Name = serviceController.ServiceName;
            winService.MachineName = serviceController.MachineName;
            winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
            winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
            winService.CanStop = serviceController.CanStop;
            taskResult.Object = winService;

            if (serviceController.CanStop == false && serviceController.Status != ServiceControllerStatus.Stopped && serviceController.Status != ServiceControllerStatus.StopPending)
              return taskResult.Warning("Windows Service '" + winServiceName + "' cannot be stopped, or is already stopped.");
          }
          catch (Exception ex)
          {
            if (ex.Message.Contains(" was not found "))
              return taskResult.Failed(ex.Message);
            else
              return taskResult.Failed("An exception occurred attempting to manage the Windows Service." + g.crlf2 + ex.ToReport());
          }

          int secondsLimit = 30;

          switch (serviceController.Status)
          {
            case ServiceControllerStatus.Stopped:
              return taskResult.Success("Windows Service '" + winServiceName + "' is already Stopped.");

            case ServiceControllerStatus.StopPending:
              return taskResult.Success("Windows Service '" + winServiceName + "' is already in StopPending status.");

            case ServiceControllerStatus.PausePending:
            case ServiceControllerStatus.ContinuePending:
            case ServiceControllerStatus.StartPending:
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be Stopped when in '" + serviceController.Status.ToString() + "' status.");

            default:
              if (_isDryRun)
                taskResult.Success("Windows Service '" + winServiceName + "' was ready to be Stopped, but returned because the command was a DryRun.");
              serviceController.Stop();

              while (serviceController.Status != ServiceControllerStatus.Stopped && secondsLimit > 0)
              {
                secondsLimit--;
                System.Threading.Thread.Sleep(1000);
                serviceController.Refresh();
              }

              serviceController.Refresh();
              winService.WinServiceStatus = serviceController.Status.ToEnum<WinServiceStatus>(WinServiceStatus.Unknown);
              if (serviceController.Status != ServiceControllerStatus.Stopped)
                return taskResult.Warning("Service Stop command was issued but the Windows Service '" + winServiceName + "' is not yet in Stopped status.  Current status is '" + serviceController.Status.ToString() + "'.");

              return taskResult.Success("Windows Service '" + winServiceName + "' was successfully Stopped.");
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while trying to stop Windows Service '" + winServiceName + "'." + g.crlf2 + ex.ToReport());
      }
    }

    public TaskResult PauseWinService(string winServiceName)
    {
      TaskResult taskResult = new TaskResult("PauseService");
      WinService winService = new WinService();
      try
      {
        using (ServiceController serviceController = new ServiceController(winServiceName))
        {
          try // will throw exception if any property does not exist
          {
            winService.Name = serviceController.ServiceName;
            winService.MachineName = serviceController.MachineName;
            winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
            winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
            winService.CanStop = serviceController.CanStop;
            taskResult.Object = winService;

            if (serviceController.CanPauseAndContinue == false)
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be paused.");
          }
          catch (Exception ex)
          {
            if (ex.Message.Contains(" was not found "))
              return taskResult.Failed(ex.Message);
            else
              return taskResult.Failed("An exception occurred attempting to manage the Windows Service." + g.crlf2 + ex.ToReport());
          }

          int secondsLimit = 30;

          switch (serviceController.Status)
          {
            case ServiceControllerStatus.PausePending:
              return taskResult.Warning("Service Pause command not issued because the Windows Service '" + winServiceName + "' is currently in PausePending status.");

            case ServiceControllerStatus.Running:
              if (_isDryRun)
                return taskResult.Success("Windows Service '" + winServiceName + "' was ready to Pause, but returned because the command was a DryRun.");
              serviceController.Pause();

              while (serviceController.Status != ServiceControllerStatus.Paused && secondsLimit > 0)
              {
                secondsLimit--;
                System.Threading.Thread.Sleep(1000);
                serviceController.Refresh();
              }

              serviceController.Refresh();
              winService.WinServiceStatus = serviceController.Status.ToEnum<WinServiceStatus>(WinServiceStatus.Unknown);
              if (serviceController.Status != ServiceControllerStatus.Paused)
                return taskResult.Warning("Service Pause command was issued but the Windows Service '" + winServiceName + "' is not yet in Paused status.  Current status is '" + serviceController.Status.ToString() + "'.");

              return taskResult.Success("Windows Service '" + winServiceName + "' was successfully Paused.");

            default:
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be Paused when in '" + serviceController.Status.ToString() + "' status.");
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while trying to pause Windows Service '" + winServiceName + "'." + g.crlf2 + ex.ToReport());
      }
    }

    public TaskResult ResumeWinService(string winServiceName)
    {
      TaskResult taskResult = new TaskResult("ResumeWinService");
      WinService winService = new WinService();
      try
      {
        using (ServiceController serviceController = new ServiceController(winServiceName))
        {
          try // will throw exception if any property does not exist
          {
            winService.Name = serviceController.ServiceName;
            winService.MachineName = serviceController.MachineName;
            winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
            winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
            winService.CanStop = serviceController.CanStop;
            taskResult.Object = winService;
          }
          catch (Exception ex)
          {
            if (ex.Message.Contains(" was not found "))
              return taskResult.Failed(ex.Message);
            else
              return taskResult.Failed("An exception occurred attempting to manage the Windows Service." + g.crlf2 + ex.ToReport());
          }

          int secondsLimit = 30;

          switch (serviceController.Status)
          {
            case ServiceControllerStatus.Paused:
              if (_isDryRun)
                return taskResult.Success("Windows Service '" + winServiceName + "' was ready to Resume, but return because the command was a DryRun.");
              serviceController.Continue();
              while (serviceController.Status != ServiceControllerStatus.Running && secondsLimit > 0)
              {
                secondsLimit--;
                System.Threading.Thread.Sleep(1000);
                serviceController.Refresh();
              }

              serviceController.Refresh();
              winService.WinServiceStatus = serviceController.Status.ToEnum<WinServiceStatus>(WinServiceStatus.Unknown);
              if (serviceController.Status != ServiceControllerStatus.Running)
                return taskResult.Warning("Service Resume command was issued but the Windows Service '" + winServiceName + "' is not yet in Running status.  Current status is '" + serviceController.Status.ToString() + "'.");

              return taskResult.Success("Windows Service '" + winServiceName + "' was successfully Resumed.");

            case ServiceControllerStatus.ContinuePending:
              return taskResult.Warning("Service Resume command not issued because the Windows Service '" + winServiceName + "' is currently in ContinuePending status.");

            default:
              return taskResult.Failed("Windows Service '" + winServiceName + "' cannot be Resumed when in '" + serviceController.Status.ToString() + "' status.");
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred while trying to resume Windows Service '" + winServiceName + "'." + g.crlf2 + ex.ToReport());
      }
    }

    public TaskResult GetWinServiceStatus(string winServiceName)
    {
      TaskResult taskResult = new TaskResult("GetWinServiceStatus");
      WinService winService = new WinService();
      using (ServiceController serviceController = new ServiceController(winServiceName))
      {
        try // will throw exception if any property does not exist
        {
          winService.Name = serviceController.ServiceName;
          winService.MachineName = serviceController.MachineName;
          winService.WinServiceStatus = g.ToEnum(serviceController.Status, WinServiceStatus.Unknown);
          winService.CanPauseAndContinue = serviceController.CanPauseAndContinue;
          winService.CanStop = serviceController.CanStop;
          taskResult.Object = winService;
        }
        catch (Exception ex)
        {
          if (ex.Message.Contains(" was not found "))
            return taskResult.Failed(ex.Message);
          else
            return taskResult.Failed("An exception occurred attempting to manage the Windows Service." + g.crlf2 + ex.ToReport());
        }

        return taskResult.Success("Windows Service '" + winServiceName + "' is '" + serviceController.Status.ToString() + "'.");
      }
    }

    public void Dispose()
    {

    }
  }
}
