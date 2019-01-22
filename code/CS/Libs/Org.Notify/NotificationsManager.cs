using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Notify
{
  public class NotificationsManager : IDisposable
  {
    public event Action<NotifyEventArgs> NotifyAction;

    private NotificationsManagerOptions _options;

    private int _totalRequested;
    public int TotalRequested { get { return _totalRequested; } }

    private int _totalNotExecuted;
    public int TotalNotExecuted { get { return _totalNotExecuted; } }

    private int _totalCompleted;
    public int TotalCompleted { get { return _totalCompleted; } }
    
    private int _totalFailed;
    public int TotalFailed { get { return _totalFailed; } }

    public int TotalInProgress { get { return Get_TotalInProgress(); } }

    private int _totalSyncRequested;
    public int TotalSyncRequested { get { return _totalSyncRequested; } }

    private int _totalSyncNotExecuted;
    public int TotalSyncNotExecuted { get { return _totalSyncNotExecuted; } }
    
    private int _totalSyncCompleted;
    public int TotalSyncCompleted { get { return _totalSyncCompleted; } }

    private int _totalSyncFailed;
    public int TotalSyncFailed { get { return _totalSyncFailed; } }

    public int TotalSyncInProgress { get { return Get_TotalSyncInProgress(); } }

    private int _totalAsyncRequested;
    public int TotalAsyncRequested { get { return _totalAsyncRequested; } }

    private int _totalAsyncNotExecuted;
    public int TotalAsyncNotExecuted { get { return _totalAsyncNotExecuted; } }
    
    private int _totalAsyncCompleted;
    public int TotalAsyncCompleted { get { return _totalAsyncCompleted; } }

    private int _totalAsyncFailed;
    public int TotalAsyncFailed { get { return _totalAsyncFailed; } }

    public int TotalAsyncInProgress { get { return Get_TotalAsyncInProgress(); } }

    private object _statisticsLockObject = new object();

    public string Report { get { return Get_Report(); } }

    public NotificationsManager()
    {
      var nmo = new NotificationsManagerOptions();
      Initialize(nmo);
    }

    public NotificationsManager(string options)
    {
      var nmo = new NotificationsManagerOptions(options);
      Initialize(nmo);
    }

    public NotificationsManager(NotificationsManagerOptions options)
    {
      Initialize(options);
    }

    private void Initialize(NotificationsManagerOptions options)
    {
      Reset(options);
    }

    private void Reset(NotificationsManagerOptions options)
    {
      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        _options = options;

        _totalRequested = 0;
        _totalCompleted = 0;
        _totalNotExecuted = 0;
        _totalFailed = 0;

        _totalSyncRequested = 0;
        _totalSyncCompleted = 0;
        _totalSyncNotExecuted = 0;
        _totalSyncFailed = 0;

        _totalAsyncRequested = 0;
        _totalAsyncCompleted = 0;
        _totalAsyncNotExecuted = 0;
        _totalAsyncFailed = 0;
      }
      else
      {
        HandleExceptions(new Exception("Failed to obtain the '_statisticsLockObject' lock for (re-)initializing the NotificationsManager."));
      }      
    }

    private int Get_TotalInProgress()
    {
      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        return _totalRequested - (_totalNotExecuted + _totalCompleted + _totalFailed);
      }
      else
      {
        HandleExceptions(new Exception("Failed to obtain the '_statisticsLockObject' lock for calculating total outstanding notification requests."));
        return 0;
      }
    }

    private int Get_TotalAsyncInProgress()
    {
      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        return _totalAsyncRequested - (_totalAsyncNotExecuted + _totalAsyncCompleted + _totalAsyncFailed);
      }
      else
      {
        HandleExceptions(new Exception("Failed to obtain the '_statisticsLockObject' lock for calculating total outstanding asynchronous notification requests."));
        return 0;
      }
    }

    private int Get_TotalSyncInProgress()
    {
      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        return _totalSyncRequested - (_totalSyncNotExecuted + _totalSyncCompleted + _totalSyncFailed);
      }
      else
      {
        HandleExceptions(new Exception("Failed to obtain the '_statisticsLockObject' lock for calculating total outstanding synchronous notification requests."));
        return 0;
      }
    }

    private void HandleExceptions(Exception ex)
    {
      if (_options.ThrowExceptions)
        throw new Exception("Failed to obtain the '_statisticsLockObject' lock for (re-)initializing the NotificationsManager.");

      //if (_options.LogExceptions)
      //  do some Logging...

    }

    public void NotifyActionHandler(NotifyEventArgs args)
    {
      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        switch (args.NotifyEventType)
        {
          case NotifyEventType.NotificationRequested:
            _totalRequested++;
            if (args.IsSynchronous)
              _totalSyncRequested++;
            else
              _totalAsyncRequested++;
            break;

          case NotifyEventType.NotificationCompleted:
            _totalCompleted++;
            if (args.IsSynchronous)
              _totalSyncCompleted++;
            else
              _totalAsyncCompleted++;
            break;

          case NotifyEventType.NotificationNotExecuted:
            _totalNotExecuted++;
            if (args.IsSynchronous)
              _totalSyncNotExecuted++;
            else
              _totalAsyncNotExecuted++;

            break;

          case NotifyEventType.NotificationFailed:
            _totalFailed++;
            if (args.IsSynchronous)
              _totalSyncFailed++;
            else
              _totalAsyncFailed++;
            break;
        }
      }
      else
      {
        HandleExceptions(new Exception("Unable to obtain the '_statisticsLockObject' to update counters in NotifyActionHandler."));
      }

      if (NotifyAction != null)
      {
        NotifyAction(args);

        if (this.TotalInProgress == 0)
          NotifyAction(new NotifyEventArgs(NotifyEventType.AllNotificationsFinished, args.IsSynchronous));
      }
    }

    private string Get_Report()
    {

      if (Monitor.TryEnter(_statisticsLockObject, 1000))
      {
        return "Notifications Report" + g.crlf2 +
               "Notifications Requested:       " + _totalRequested.ToString("###,##0") + g.crlf +
               "Notifications Not Executed:    " + _totalNotExecuted.ToString("###,##0") + g.crlf +
               "Notifications Completed:       " + _totalCompleted.ToString("###,##0") + g.crlf +
               "Notifications Failed:          " + _totalFailed.ToString("###,##0") + g.crlf2 +

               "Async Notifications Requested:     " + _totalAsyncRequested.ToString("###,##0") + g.crlf +
               "Async Notifications Not Executed:  " + _totalAsyncNotExecuted.ToString("###,##0") + g.crlf +
               "Async Notifications Completed:     " + _totalAsyncCompleted.ToString("###,##0") + g.crlf +
               "Async Notifications Failed:        " + _totalAsyncFailed.ToString("###,##0") + g.crlf2 +

               "Sync Notifications Requested:     " + _totalSyncRequested.ToString("###,##0") + g.crlf +
               "Sync Notifications Not Executed:  " + _totalSyncNotExecuted.ToString("###,##0") + g.crlf +
               "Sync Notifications Completed:     " + _totalSyncCompleted.ToString("###,##0") + g.crlf +
               "Sync Notifications Failed:        " + _totalSyncFailed.ToString("###,##0");
      }
      else
      {
        HandleExceptions(new Exception("Unable to obtain a lock on the '_statisticsLockObject' to create statitistics report."));
        return "Failed to obtain a lock on the '_statisticsLockObject' to create statistics report.";
      }
    }

    public void Dispose()
    {

    }

  }
}
