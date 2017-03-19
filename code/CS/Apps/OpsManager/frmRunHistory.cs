using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;

namespace Org.OpsManager
{
  public partial class frmRunHistory : Form
  {
    OpsData _opsData;

    public frmRunHistory(OpsData opsData)
    {
      InitializeComponent();
      _opsData = opsData;
      InitializeRunHistoryGrid();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RefreshRunHistory":
          RefreshRunHistory();
          break;

        case "RunHistoryChange":
          ShowLogs();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.OK;
          break;

        case "Exit":
          Environment.Exit(0);
          break;
      }
    }

    private void RefreshRunHistory()
    {
      try
      {
        gvRunHistory.Rows.Clear();

        var runHistoryList = new List<RunHistory>();
        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
          runHistoryList = taskRepo.GetRunHistoryForTask(_opsData.CurrentScheduledTask.ScheduledTaskId);

        foreach (var rh in runHistoryList)
        {
          string runStats = "";
          if (rh.RunStats.Int1Label.IsNotBlank() || rh.RunStats.Int2Label.IsNotBlank() || rh.RunStats.Int3Label.IsNotBlank() || rh.RunStats.Int4Label.IsNotBlank() ||
              rh.RunStats.Int5Label.IsNotBlank() || rh.RunStats.Dec1Label.IsNotBlank() || rh.RunStats.Dec2Label.IsNotBlank() || rh.RunStats.Dec3Label.IsNotBlank() ||
              rh.RunStats.Dec4Label.IsNotBlank() || rh.RunStats.Dec5Label.IsNotBlank())
            runStats = "Y";
          gvRunHistory.Rows.Add(rh.TaskName
                               ,rh.ProcessorType
                               ,rh.ProcessorName + "_" + rh.ProcessorVersion
                               ,rh.ExecutionStatus
                               ,rh.RunStatus
                               ,rh.RunCode
                               ,rh.NoWorkDone ? "Y" : "N"
                               ,rh.StartDateTime
                               ,rh.EndDateTime
                               ,rh.RunHost
                               ,rh.RunUser
                               ,rh.Message
                               ,rh.RunUntilTask ? "Y" : "N"
                               ,rh.RunUntilPeriod
                               ,rh.RunUntilOffsetMinutes
                               ,runStats
                               ,rh.RunId);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to Display RunHistory to 'gvRunHistory'." + g.crlf2 +
                       "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ShowLogs()
    {
      try
      {
        if (gvRunHistory.SelectedRows.Count != 1)
          return;

        int runId = gvRunHistory.SelectedRows[0].Cells[16].Value.ToInt32();

        AppLogSet logSet = new AppLogSet();

        using (var loggingRepo = new LoggingRepository(_opsData.LoggingDbSpec))
        {
          logSet = loggingRepo.GetAppLogSetFromRunId(runId);
        }

        var sb = new StringBuilder();
        foreach (var log in logSet)
        {
          sb.Append(log.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff").PadTo(24) + g.crlf +
                    "-----------------------" + g.crlf +
                    "Event:".PadTo(14) + (log.EventCode.HasValue ? _opsData.AppLogEvents[log.EventCode.Value] : "") + g.crlf +
                    "Module:".PadTo(14) + (log.ModuleId.HasValue ? _opsData.AppLogModules[log.ModuleId.Value] : "") + g.crlf +          
                    "Entity:".PadTo(14) + (log.EntityId.HasValue ? _opsData.AppLogEntities[log.EntityId.Value] : "") + g.crlf +
                    "User Name:".PadTo(14) + log.UserName + g.crlf +
                    "Notification Sent: " + log.NotificationSent + g.crlf);

          if (log.ClientHost.IsNotBlank() || log.ClientIp.IsNotBlank() || log.ClientUser.IsNotBlank() || log.ClientApplication.IsNotBlank() ||
              log.ClientApplicationVersion.IsNotBlank() || log.TransactionName.IsNotBlank())
          {
            sb.Append("Client Data:".PadTo(14) + g.crlf +
                      "  Host:".PadTo(22) + log.ClientHost + g.crlf +
                      "  IP:".PadTo(22) + log.ClientIp + g.crlf +
                      "  User:".PadTo(22) + log.ClientUser + g.crlf +
                      "  Application:".PadTo(22) + log.ClientApplication + g.crlf +
                      "  App Version:".PadTo(22) + log.ClientApplicationVersion + g.crlf +
                      "  Transaction Name:".PadTo(22) + log.TransactionName + g.crlf2);
          }

          if (log.AppLogDetailSet.Count == 0)
          {
            sb.Append(log.SeverityCode + ":" + g.crlf + log.Message + g.crlf2);
            continue;
          }

          //Find Overflow SetID and list all other IDs
          List<int> setIds = new List<int>();
          int? overflowSetId = null;
          foreach (var logDetail in log.AppLogDetailSet)
          {
            if (logDetail.LogDetailType == LogDetailType.MSGX)
            {
              overflowSetId = logDetail.SetId;
              continue;
            }
            if (!setIds.Contains(logDetail.SetId))
              setIds.Add(logDetail.SetId);
          }

          //Append message and message overflows
          sb.Append(log.SeverityCode + ":" + g.crlf + log.Message);
          if (overflowSetId.HasValue)
          {
            var overflowLogDetails = log.AppLogDetailSet.Where(ld => ld.SetId == overflowSetId).OrderBy(ld => ld.LogDetailId).ToList();
            foreach (var logDetail in overflowLogDetails)
              sb.Append(logDetail.LogDetail);
          }
          sb.Append(g.crlf2);

          //Append all other LogDetail records
          LogDetailType logDetailType = LogDetailType.NotSet;
          foreach (var setId in setIds)
          {
            var logDetailsWithSetId = log.AppLogDetailSet.Where(ld => ld.SetId == setId).OrderBy(ld => ld.LogDetailId).ToList();
            logDetailType = logDetailsWithSetId[0].LogDetailType;

            sb.Append(logDetailType + ":" + g.crlf);

            foreach (var logDetail in logDetailsWithSetId)
              sb.Append(logDetail.LogDetail);

            sb.Append(g.crlf2);

          } 
        }
        txtLogDetails.Text = sb.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to show logs related to selected Run." + g.crlf2 +
                       "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeRunHistoryGrid()
    {
      gvRunHistory.ClearSelection();

      try
      {
          GridView view = _opsData.GridViewSet["RunHistory"];
          view.SetColumnWidths(gvRunHistory.ClientSize.Width);

          foreach (GridColumn gc in view)
          {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.Name = gc.Name;
            col.HeaderText = gc.Text;

            col.Width = gc.WidthPixels;

            switch (gc.Align)
            {
              case "Right":
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                break;
              case "Left":
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                break;
              case "Center":
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                break;
            }

            if (gc.Fill)
              col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gvRunHistory.Columns.Add(col);
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the Database grid using view 'RunHistory'." + g.crlf2 +
                        "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
