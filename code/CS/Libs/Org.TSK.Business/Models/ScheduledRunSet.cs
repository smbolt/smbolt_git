using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class ScheduledRunSet : SortedList<DateTime, ScheduledRun>
  {
    private ScheduledTask _scheduledTask;
    private TaskSchedule _taskSchedule;

    public ScheduledRunSet(ScheduledTask scheduledTask, TaskSchedule taskSchedule)
    {
      _scheduledTask = scheduledTask;
      _taskSchedule = taskSchedule;
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("Schedule for Task       : " + _scheduledTask.TaskName + g.crlf);
      sb.Append("Schedule Name           : " + _taskSchedule.ScheduleName + g.crlf2);
      sb.Append("Run#        Schd Run Date / Time          Sched Element Id / Nbr       Status" + g.crlf);
      sb.Append("----        -----------------------       ----------------------       --------------------" + g.crlf);

      int count = 0;
      foreach(var entry in this)
      {
        count++;
        string scheduleElementNumber = entry.Value.TaskScheduleElement.TaskScheduleElementId.ToString("0000");

        sb.Append(count.ToString("0000") + "        " +
                  entry.Value.ScheduledRunDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "       " +
                  entry.Value.TaskScheduleElement.TaskScheduleElementId.ToString("000") + " / " +
                  scheduleElementNumber + "                   " +
                  entry.Value.ScheduledRunStatus.ToString() + g.crlf);
      }

      string report = sb.ToString();
      return report;
    }
  }
}
