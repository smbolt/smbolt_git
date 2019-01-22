using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Notify;
using Org.GS.Notifications;
using Org.GS.Configuration;
using Org.GS;

namespace Org.NotificationTest
{
  public partial class frmMain : Form
  {
    private a a;
    private SmtpParms _smtpParms;
    private NotificationsManager _notificationsManager;
    private NotifyConfigMode _notifyConfigMode;
    public NotifyConfigSets _notifyConfigSets;
    private ConfigDbSpec _notifyDbSpec;
    
    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ReloadConfigs":
          var notifyConfigMode = ckFromDatabase.Checked ? NotifyConfigMode.Database : NotifyConfigMode.AppConfig;
          LoadNotifyConfigs(notifyConfigMode);
          break;

        case "TestNotification":
          //txtOut.Text = String.Empty;
          //Application.DoEvents();
          //System.Threading.Thread.Sleep(50);
          if (ckSynchronous.Checked)
            TestNotificationSync();
          else
            TestNotificationAsync();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void TestNotificationSync()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        var enumatedTaskResult = GetTaskResult();
        var notifyConfigSet = GetNotifyConfigSet();
        var notification = new Notification(enumatedTaskResult, notifyConfigSet);

        TaskResult emailTaskResult = null;

        if (notification.NotificationStatus == NotificationStatus.ReadyToSend)
        {
          using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
          {
            notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
            emailTaskResult = notifyEngine.ProcessNotifications(notification);
          }
        }

        txtOut.Text += emailTaskResult.NotificationMessage + g.crlf2 + _notificationsManager.Report + g.crlf2;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;

        MessageBox.Show("An exception occurred while attempting to test notifications." + g.crlf2 + ex.ToReport(),
                        "NotificationTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      this.Cursor = Cursors.Default;
    }

    private void TestNotificationAsync()
    {
      this.Cursor = Cursors.WaitCursor;

      var enumatedTaskResult = GetTaskResult();
      var notifyConfigSet = GetNotifyConfigSet();
      var notification = new Notification(enumatedTaskResult, notifyConfigSet);
      
      if (notification.NotificationStatus == NotificationStatus.ReadyToSend)
      {
        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });
        }
      }

      //txtOut.Text = "Async send invoked";
      
      this.Cursor = Cursors.Default;
    }

    private void NotificationsAsyncComplete(Task<TaskResult> result)
    {
      switch (result.Status)
      {
        case TaskStatus.RanToCompletion:
          TaskResult taskResult = result.Result;
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              txtOut.Text += _notificationsManager.Report + g.crlf2;
              break;

            case TaskResultStatus.Warning:

              break;

            case TaskResultStatus.Failed:

              break;

            default:

              break;

          }
          break;


        case TaskStatus.Canceled:

          break;


        case TaskStatus.Faulted:

          break;


        default:

          break;
      }
    }


    private NotifyConfigSet GetNotifyConfigSet()
    {
      string notifyConfigSetName = cboNotifyConfigSets.Text;
      return _notifyConfigSets[notifyConfigSetName];
    }

    private TaskResult GetTaskResult()
    {
      var taskResult = new TaskResult();
      taskResult.TaskResultStatus = g.ToEnum<TaskResultStatus>(cboTaskResult.Text, TaskResultStatus.NotSet);
      taskResult.TaskName = cboTaskName.Text;
      return taskResult;
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

        _notificationsManager = new NotificationsManager("00");
        _notificationsManager.NotifyAction += NotifyActionHandler;

        var smtpSpec = g.GetSmtpSpec("Default");
        _smtpParms = new SmtpParms(smtpSpec);

        string notifyDbSpecPrefix = g.CI("NotifyDbSpecPrefix");
        _notifyDbSpec = g.GetDbSpec(notifyDbSpecPrefix);

        _notifyConfigMode = g.ToEnum<NotifyConfigMode>(g.CI("NotifyConfigMode"), NotifyConfigMode.AppConfig);

        NotifyConfigHelper.SetNotifyConfigDbSpec(_notifyDbSpec);

        LoadNotifyConfigs(_notifyConfigMode);

        _notifyConfigSets = NotifyConfigHelper.GetNotifyConfigs(_notifyConfigMode);

        cboNotifyConfigSets.Items.Clear();
        cboNotifyConfigSets.Items.Add(String.Empty);

        if (_notifyConfigSets != null)
        {
          foreach (var ncs in _notifyConfigSets)
          {
            cboNotifyConfigSets.Items.Add(ncs.Key);
          }
        }

        cboNotifyConfigSets.SelectedIndex = 0;
        cboTaskResult.SelectedIndex = 0;
        cboEventType.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "NotificationTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void NotifyActionHandler(NotifyEventArgs args)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)((() => UpdateUI(args))));
      }
      else
      {
        UpdateUI(args);
      }
    }

    private void UpdateUI(NotifyEventArgs args)
    {
      txtOut.Text += _notificationsManager.Report + g.crlf + args.NotifyEventType.ToString() + g.crlf2;
    }

    private void LoadNotifyConfigs(NotifyConfigMode notifyConfigMode)
    {
      _notifyConfigSets = NotifyConfigHelper.GetNotifyConfigs(notifyConfigMode);

      cboNotifyConfigSets.Items.Clear();
      cboNotifyConfigSets.Items.Add(String.Empty);

      if (_notifyConfigSets != null)
      {
        foreach (var ncs in _notifyConfigSets)
        {
          cboNotifyConfigSets.Items.Add(ncs.Key);
        }
      }

      cboNotifyConfigSets.SelectedIndex = 0;
      cboTaskName.Items.Clear();
    }

    private void cboNotifyConfigSets_SelectedIndexChanged(object sender, EventArgs e)
    {
      cboTaskName.Items.Clear();

      if (_notifyConfigSets == null)
        return;

      if (cboNotifyConfigSets.Text.IsBlank())
        return;

      string ncsName = cboNotifyConfigSets.Text;

      if (!_notifyConfigSets.ContainsKey(ncsName))
        return;

      var ncs = _notifyConfigSets[ncsName];

      cboTaskName.Items.Add(String.Empty);
      foreach (var nc in ncs.Values)
      {
        cboTaskName.Items.Add(nc.Name);
      }

      if (cboTaskName.Items.Count > 0)
        cboTaskName.SelectedIndex = 0;
    }
  }
}
