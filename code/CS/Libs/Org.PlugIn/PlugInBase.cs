using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.PlugIn;
using Org.GS;
using Org.GS.AppDomainManagement;

namespace Org.PlugIn2
{
  public class PlugInBase : MarshalByRefObject, IPlugIn, IDisposable
  {
    public event Action<NotifyMessage> NotifyMessage;
    public event Action<ProgressMessage> ProgressUpdate;
    public virtual int EntityId {
      get {
        throw new Exception("EntityId property must be implemented in derived type.");
      }
    }
    public TaskRequest TaskRequest {
      get;
      set;
    }

    public virtual TaskResult ProcessTask()
    {
      throw new Exception("The ProcessTask method must be overridden in derived classes.");
    }

    public string Identify()
    {
      string appDomainName = AppDomain.CurrentDomain.FriendlyName;
      Type thisType = this.GetType();

      return this.GetType().FullName + " running in AppDomain '" + appDomainName + "'.";
    }

    protected void Notify(string message)
    {
      string defaultSubject = "Notification from " + this.TaskRequest.TaskName;
      string defaultEvent = "Default";
      Notify(message, defaultSubject, defaultEvent);
    }

    protected void Notify(string message, string subject)
    {
      string defaultEvent = "Default";
      Notify(message, subject, defaultEvent);
    }

    protected void Notify(string message, string subject, string eventName)
    {
      if (this.NotifyMessage == null)
        return;

      this.NotifyMessage(new NotifyMessage(message, subject, eventName));
    }

    protected void ProgressNotify(int completedItems, int totalItems)
    {
      if (this.ProgressUpdate == null)
        return;

      this.ProgressUpdate(new ProgressMessage(completedItems, totalItems));
    }

    protected void ProgressNotify(string activityName, string message, int completedItems, int totalItems)
    {
      if (this.ProgressUpdate == null)
        return;

      this.ProgressUpdate(new ProgressMessage(activityName, completedItems, totalItems, message));
    }

    public void Dispose() { }
  }
}
