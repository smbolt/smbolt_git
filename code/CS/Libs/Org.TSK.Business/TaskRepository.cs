using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOD = Org.TSK.Business.Models;
using Org.GS.ServiceManagement;
using Org.GS.Configuration;
using Org.GS;

namespace Org.TSK.Business
{
  public class TaskRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;

    public DateTime _importDate;

    public TaskRepository(ConfigDbSpec configDbSpec)
    {
      _importDate = DateTime.Now;
      _configDbSpec = configDbSpec;

      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");

      _connectionString = _configDbSpec.ConnectionString;
    }

    public void MigrateVariable(MOD.TaskParameter parmVariable)
    {
      bool transBegun = false;
      SqlTransaction trans = null;
      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        string sql = " SELECT COUNT(ParameterID) AS NameCount " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterName] LIKE '" + parmVariable.ParameterName + "'";

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
        {
          int nameCount = cmd.ExecuteScalar().DbToInt32().Value;

          if (nameCount > 0)
          {
            DialogResult dialogResult = MessageBox.Show("A Parameter Variable with the name '" + parmVariable.ParameterName + "' already exists. Do you want to delete this parameter and insert a new one?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.No)
              return;

            DeleteParametersByName(parmVariable.ParameterName, trans);
          }

          InsertTaskParameter(parmVariable, trans);
        }
        trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();

        throw new Exception("An exception occurred when trying to migrate parameter variable '" + parmVariable.ParameterName + "' to " + _configDbSpec.DbServer, ex);
      }
    }

    public void DeleteParametersByName(string parameterName, SqlTransaction trans)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterName] LIKE '" + parameterName + "'";

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when try to delete parameter with parameter name '" + parameterName + "'.", ex);
      }
    }

    public void MigrateParameterSet(MOD.TaskParameterSet parameterSet)
    {
      bool transBegun = false;
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        var taskParmList = new List<Org.TSK.Business.Models.TaskParameter>();

        string sql = " SELECT COUNT(ParameterSetName) AS NameCount " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterSetName] LIKE '" + parameterSet.ParameterSetName + "'";

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
        {
          int nameCount = cmd.ExecuteScalar().DbToInt32().Value;

          if (nameCount > 0)
          {
            DialogResult dialogResult = MessageBox.Show("A Parameter Set with the name '" + parameterSet.ParameterSetName + "' already exists. Do you want to delete this parameter set and insert a new one?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.No)
              return;

            DeleteParameterSet(parameterSet.ParameterSetName, trans);
          }

          foreach (var parameter in parameterSet.TaskParameters)
            InsertTaskParameter(parameter, trans);
        }
        trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();

        throw new Exception("An exception occurred when trying to migrate parameter set '" + parameterSet.ParameterSetName + "' to " + _configDbSpec.DbServer, ex);
      }
    }

    private void DeleteParameterSet(string parameterSetName, SqlTransaction trans)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterSetName] LIKE '" + parameterSetName + "'";

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to delete Parameter Set '" + parameterSetName + "'", ex);
      }
    }

    public void MigrateScheduledTask(MOD.ScheduledTask schedTask, List<MOD.TaskSchedule> taskSchedules, List<MOD.TaskParameter> taskParameters)
    {
      bool transBegun = false;
      int? existingTaskId = null;
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        string sql = " SELECT COUNT(TaskName) AS NameCount " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " WHERE [TaskName] LIKE '" + schedTask.TaskName + "'";

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
        {
          int nameCount = cmd.ExecuteScalar().DbToInt32().Value;

          if (nameCount > 0)
          {
            if (MessageBox.Show("A Scheduled Task with the name '" + schedTask.TaskName + "' already exists. " +
                                "Do you want to delete the schedules and parameters and replace them with the " +
                                "schedules and parameters from the other environment?", "Confirm Replacement of Schedules and Parameters",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
              return;
            }

            string sqlGetTask = "SELECT ScheduledTaskID " + g.crlf +
                                "FROM TaskScheduling.dbo.ScheduledTasks " + g.crlf +
                                "WHERE TaskName LIKE '" + schedTask.TaskName + "' ";

            using (var getCmd = new SqlCommand(sqlGetTask, _conn, trans))
            {
              existingTaskId = getCmd.ExecuteScalar().DbToInt32().Value;
            }

            DeleteAllSchedulesForTask(existingTaskId.Value, trans);
            DeleteAllParametersForTask(existingTaskId.Value, trans);
          }

          InsertMigratedTask(schedTask, existingTaskId, taskSchedules, taskParameters, trans);
        }

        trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();
        throw new Exception("An exception occurred when trying to migrate ScheduledTask '" + schedTask.TaskName + "'.", ex);
      }
    }

    private void InsertMigratedTask(MOD.ScheduledTask schedTask, int? existingTaskId, List<MOD.TaskSchedule> taskSchedules, List<MOD.TaskParameter> taskParameters, SqlTransaction trans)
    {
      int taskID = 0;

      try
      {
        if (existingTaskId.HasValue)
        {
          taskID = existingTaskId.Value;
        }
        else
        {
          string sqlTask = "INSERT INTO [TaskScheduling].[dbo].[ScheduledTasks] ([TaskName], [ProcessorTypeID], [ProcessorName], [ProcessorVersion], " + g.crlf +
                           "[AssemblyLocation], [StoredProcedureName], [IsActive], [RunUntilTask], [RunUntilPeriodContextID], [RunUntilOverride], " + g.crlf +
                           "[RunUntilOffsetMinutes], [IsLongRunning], [TrackHistory], [SuppressNotificationsOnSuccess], [ActiveScheduleId], " + g.crlf +
                           "[CreatedBy], [CreatedDate])" + g.crlf +
                           "VALUES ('" + schedTask.TaskName.ToString() + "'," + g.crlf +
                           schedTask.ProcessorTypeId.ToInt32() + "," + g.crlf +
                           "'" + schedTask.ProcessorName.ToString() + "'," + g.crlf +
                           "'" + schedTask.ProcessorVersion.ToString() + "'," + g.crlf +
                           "'" + schedTask.AssemblyLocation.GetStringValueOrNull() + "'," + g.crlf +
                           "'" + schedTask.StoredProcedureName.GetStringValueOrNull() + "'," + g.crlf +
                           0 + "," + g.crlf +
                           schedTask.RunUntilTask.ToInt32() + "," + g.crlf +
                           (schedTask.RunUntilPeriodContextID.HasValue ? schedTask.RunUntilPeriodContextID.ToString() : "NULL") + "," + g.crlf +
                           schedTask.RunUntilOverride.ToInt32() + "," + g.crlf +
                           (schedTask.RunUntilOffsetMinutes.HasValue ? schedTask.RunUntilOffsetMinutes.ToString() : "NULL") + "," + g.crlf +
                           schedTask.IsLongRunning.ToInt32() + "," + g.crlf +
                           schedTask.TrackHistory.ToInt32() + "," + g.crlf +
                           schedTask.SuppressNotificationsOnSuccess.ToInt32() + "," + g.crlf +
                           schedTask.ActiveScheduleId.ToInt32() + "," + g.crlf +
                           "'" + g.SystemInfo.DomainAndUser + "'," + g.crlf +
                           "'" + DateTime.Now + "'); " + " SELECT SCOPE_IDENTITY()";


          using (var cmd = new SqlCommand(sqlTask, _conn, trans))
            taskID = cmd.ExecuteScalar().ToInt32();
        }

        foreach (var schedule in taskSchedules)
        {
          int scheduleID = 0;

          string sqlSchedule = " INSERT INTO [TaskScheduling].[dbo].[TaskSchedules] " + g.crlf +
                               " ([ScheduledTaskID], [ScheduleName], [IsActive], " + g.crlf +
                               " [CreatedBy], [CreatedDate])" + g.crlf +
                               "VALUES (" + taskID + ", " +
                               "'" + schedule.ScheduleName + "', " +
                               schedule.IsActive.ToInt32() + ", " + g.crlf +
                               "'" + schedule.CreatedBy + "', " +
                               "'" + schedule.CreatedDate + "'); " + " SELECT SCOPE_IDENTITY()";

          using (var cmd = new SqlCommand(sqlSchedule, _conn, trans))
          {
            scheduleID = cmd.ExecuteScalar().ToInt32();
          }

          if (schedule.IsActive)
          {
            string sqlUpdateActiveScheduleId = "UPDATE  [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                                               "SET [ActiveScheduleID] = " + scheduleID + g.crlf +
                                               "WHERE [ScheduledTaskID] = " + taskID;

            using (var cmd = new SqlCommand(sqlUpdateActiveScheduleId, _conn, trans))
            {
              cmd.ExecuteNonQuery();
            }
          }

          foreach (var element in schedule.TaskScheduleElements)
          {
            element.TaskScheduleId = scheduleID;
            InsertTaskScheduleElement(element, trans);
          }
        }

        foreach (var parameter in taskParameters)
        {
          parameter.ScheduledTaskID = taskID;
          InsertTaskParameter(parameter, trans);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred inserting migrated task '" + schedTask.TaskName + "' into the TaskScheduling database.", ex);
      }
    }

    public List<MOD.ScheduledTask> GetTasksForScheduling(bool resolveParmSets, bool scheduleOnceNow = false)
    {
      return GetTasksForScheduling(null, resolveParmSets, scheduleOnceNow);
    }

    public List<MOD.ScheduledTask> GetTasksForScheduling(List<string> tasksToRun, bool scheduleOnceNow, bool resolveParmSets)
    {
      var scheduledTasks = new List<MOD.ScheduledTask>();

      try
      {
        var listSchedTasks = GetScheduledTasks(tasksToRun);

        if (tasksToRun != null)
        {
          var schedTasksToRun = new SortedList<int, MOD.ScheduledTask>();
          foreach (var kvp in listSchedTasks)
          {
            if (tasksToRun.Contains(kvp.Value.TaskName))
              schedTasksToRun.Add(kvp.Key, kvp.Value);
          }
          listSchedTasks = schedTasksToRun;
        }

        GetTasksTaskSchedule(listSchedTasks, scheduleOnceNow);

        foreach (var task in listSchedTasks.Values)
        {
          task.ParmSet = new ParmSet(); // parameters will be retrieved by the TaskEngine just before the task is dispatched.
          //task.ParmSet = GetTaskParms(task.ScheduledTaskId, resolveParmSets);
        }

        foreach (var task in listSchedTasks)
        {
          scheduledTasks.Add(task.Value);
        }

        return scheduledTasks;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all tasks for scheduling.", ex);
      }
    }

    public ParmSet GetTaskParms(int scheduledTaskId, bool resolveParmSets)
    {
      try
      {
        EnsureConnection();

        var taskParmSet = new ParmSet();

        string sql = "SELECT ParameterID, ScheduledTaskID, ParameterSetName, ParameterName, ParameterValue, DataType, CreatedBy, CreatedDate " + g.crlf +
                     "FROM dbo.ScheduledTaskParameters " + g.crlf +
                     "WHERE ScheduledTaskID = " + scheduledTaskId + " " + g.crlf +
                     "   OR ParameterID < 20000 " + g.crlf +
                     "ORDER BY ParameterID ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var parm = new Parm();

            parm.ParameterId = reader["ParameterID"].DbToInt32().Value;
            parm.ScheduledTaskId = reader["ScheduledTaskId"].DbToInt32();
            parm.ParameterSetName = reader["ParameterSetName"].DbToString();
            parm.ParameterName = reader["ParameterName"].DbToString();
            parm.ParameterValue = reader["ParameterValue"].DbToString();
            string parmType = reader["DataType"].DbToString();
            parm.ParameterType = parmType.ToType();
            taskParmSet.Add(parm);
          }

          reader.Close();
        }

        if (resolveParmSets)
        {
          foreach (var parm in taskParmSet)
          {
            if (parm.ParameterValue != null && parm.ParameterValue.ToString().StartsWith("ParmSet="))
            {
              string parmSetName = parm.ParameterValue.ToString().Trim().Replace("ParmSet=", String.Empty);
              string setName = parmSetName;
              bool getSimpleItem = false;
              Type complexType = null;
              object complexObject = null;
              string parmName = String.Empty;

              if (parmSetName.Contains("."))
              {
                getSimpleItem = true;
                string[] tokens = parmSetName.Split(Constants.DotDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 2)
                {
                  setName = tokens[0].Trim();
                  parmName = tokens[1].Trim();
                }
              }

              Parm parmToReplace = null;
              if (!getSimpleItem)
              {
                parmToReplace = parm;
                if (parmToReplace != null)
                {
                  complexType = parmToReplace.ParameterType;
                  if (complexType != null)
                  {
                    complexObject = Activator.CreateInstance(complexType);
                  }
                }
              }

              if (parm.ParameterValue != null && parm.ParameterValue.ToString().IsNotBlank())
              {
                if (parm.ParameterValue.ToString().Trim().StartsWith("ParmSet="))
                {
                  string pvsql = "SELECT ParameterID, ScheduledTaskID, ParameterSetName, ParameterName, ParameterValue, DataType, CreatedBy, CreatedDate " + g.crlf +
                                 "FROM dbo.ScheduledTaskParameters " + g.crlf +
                                 "WHERE ParameterSetName = " + "'" + setName + "'";

                  var parmSet = new ParmSet();
                  using (SqlCommand pvcmd = new SqlCommand(pvsql, _conn))
                  {
                    pvcmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader pvReader = pvcmd.ExecuteReader();

                    while (pvReader.Read())
                    {
                      var pvParm = new Parm();
                      pvParm.ParameterId = pvReader["ParameterID"].DbToInt32().Value;
                      pvParm.ScheduledTaskId = pvReader["ScheduledTaskId"].DbToInt32();
                      pvParm.ParameterSetName = pvReader["ParameterSetName"].DbToString();
                      pvParm.ParameterName = pvReader["ParameterName"].DbToString();
                      pvParm.ParameterValue = pvReader["ParameterValue"].DbToString();
                      string parmType = pvReader["DataType"].DbToString();
                      pvParm.ParameterType = parmType.ToType();
                      parmSet.Add(pvParm);
                    }

                    pvReader.Close();

                    foreach (var item in parmSet)
                    {
                      if (getSimpleItem)
                      {
                        if (item.ParameterName.IsNotBlank() && item.ParameterName.ToString().Trim() == parmName && item.ParameterValue != null)
                        {
                          object parmValue = null;
                          switch (item.ParameterType.Name)
                          {
                            case "String":
                              parmValue = item.ParameterValue.ToString().Trim();
                              break;

                            case "Int32":
                              parmValue = item.ParameterValue.ToString().ToInt32();
                              break;

                            case "Single":
                              parmValue = item.ParameterValue.ToString().ToFloat();
                              break;

                            case "Decimal":
                              parmValue = item.ParameterValue.ToString().ToDecimal();
                              break;

                            case "Boolean":
                              parmValue = item.ParameterValue.ToString().ToBoolean();
                              break;

                            default:
                              throw new Exception("The parameter type '" + item.ParameterType.Name + "' is not implemented in method GetTaskParms when processing the simple item '" +
                                                  parmName + "' for ParmSet name '" + setName + "' for scheduled task with ScheduledTaskId = " + scheduledTaskId.ToString() + ".");
                          }

                          parm.ParameterValue = parmValue;
                          break;
                        }
                      }
                      else
                      {
                        if (item.ParameterValue != null)
                        {
                          switch (complexType.Name)
                          {
                            case "List`1":
                              List<string> list = (List<string>)complexObject;
                              if (item.ParameterValue.ToString().IsNotBlank())
                                list.Add(item.ParameterValue.ToString().Trim());
                              break;

                            case "Dictionary`2":
                              Dictionary<string, string> dict = (Dictionary<string, string>)complexObject;
                              if (item.ParameterName.IsNotBlank() && item.ParameterValue.ToString().IsNotBlank())
                              {
                                if (!dict.ContainsKey(item.ParameterName))
                                  dict.Add(item.ParameterName, item.ParameterValue.ToString().Trim());
                              }
                              break;

                            case "ConfigDbSpec":
                            case "ConfigFtpSpec":
                            case "ConfigWsSpec":
                            case "ConfigSyncSpec":
                            case "ConfigSmtpSpec":
                              if (item.ParameterName.IsNotBlank() && item.ParameterValue.ToString().IsNotBlank())
                              {
                                System.Reflection.PropertyInfo pi = complexType.GetProperty(item.ParameterName);
                                if (pi != null)
                                {
                                  string propType = pi.PropertyType.Name;
                                  switch (propType)
                                  {
                                    case "String":
                                      pi.SetValue(complexObject, item.ParameterValue.ObjectToTrimmedString());
                                      break;

                                    case "Int32":
                                      pi.SetValue(complexObject, item.ParameterValue.ToInt32());
                                      break;

                                    case "Single":
                                      pi.SetValue(complexObject, item.ParameterValue.ToFloat());
                                      break;

                                    case "Decimal":
                                      pi.SetValue(complexObject, item.ParameterValue.ToDecimal());
                                      break;

                                    case "Boolean":
                                      pi.SetValue(complexObject, item.ParameterValue.ToBoolean());
                                      break;

                                    case "DatabaseType":
                                      pi.SetValue(complexObject, g.ToEnum<DatabaseType>(item.ParameterValue, DatabaseType.SqlServer));
                                      break;

                                    case "WebServiceBinding":
                                      pi.SetValue(complexObject, g.ToEnum<WebServiceBinding>(item.ParameterValue, WebServiceBinding.BasicHttp));
                                      break;

                                    default:
                                      throw new Exception("Invalid value specified '" + item.ParameterValue + "' for the schedule task parameter named '" +
                                                          item.ParameterName + "' in parameter set named '" + item.ParameterSetName + "' while attempting to " +
                                                          "build scheduled task parameters for the scheduled task with ScheduleTaskId = '" +
                                                          scheduledTaskId.ToString() + ".");
                                  }
                                }
                              }
                              break;
                          }
                        }
                      }
                    }

                    if (parmToReplace != null)
                      parmToReplace.ParameterValue = complexObject;
                  }
                }
              }
            }
          }
        }

        return taskParmSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get list of task parms.", ex);
      }
    }

    public MOD.TimeIntervalSet GetRunUntilExclusionIntervals(MOD.ScheduledTask st)
    {
      try
      {
        EnsureConnection();

        var runUntilExclusionIntervals = new MOD.TimeIntervalSet();

        string sql = " SELECT [StartDateTime], [EndDateTime] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[PeriodHistory] " + g.crlf +
                     " WHERE [ScheduledTaskID] = @ScheduledTaskID " + g.crlf +
                     "   AND [RunForPeriod] = 1 " + g.crlf +
                     "   AND [EndDateTime] > GETDATE() " + g.crlf +
                     "   AND [RunUntilTask] = @RunUntilTask " + g.crlf +
                     "   AND [RunUntilPeriodContextID] = @RunUntilPeriodContextID " + g.crlf +
                     "   AND [RunUntilOffsetMinutes] = @RunUntilOffsetMinutes ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;

          cmd.Parameters.AddWithValue("@ScheduledTaskID", st.ScheduledTaskId);
          cmd.Parameters.AddWithValue("@RunUntilTask", st.RunUntilTask);
          cmd.Parameters.AddWithValue("@RunUntilPeriodContextID", (object)st.RunUntilPeriodContextID ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunUntilOffsetMinutes", (object)st.RunUntilOffsetMinutes ?? 0);
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            DateTime startDateTime = reader["StartDateTime"].DbToDateTime().Value;
            DateTime endDateTime = reader["EndDateTime"].DbToDateTime().Value;

            runUntilExclusionIntervals.Add(new MOD.TimeInterval(startDateTime, endDateTime));
          }

          reader.Close();
        }
        return runUntilExclusionIntervals;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to get RunUntilExclusionIntervals from RunHistory for ScheduledTaskID '" + st.ScheduledTaskId + "'.", ex);
      }
    }

    public void GetTasksTaskSchedule(SortedList<int, MOD.ScheduledTask> listSchedTasks, bool scheduleOnceNow)
    {
      try
      {
        EnsureConnection();

        foreach (var task in listSchedTasks.Values)
        {
          if (!scheduleOnceNow && (!task.IsActive || !task.ActiveScheduleId.HasValue))
            continue;

          string sql = "SELECT * " + g.crlf +
                       "FROM [TaskScheduling].[dbo].[TaskSchedules] " + g.crlf +
                       "WHERE TaskScheduleID = " + task.ActiveScheduleId.Value.ToString();

          using (SqlCommand cmd = new SqlCommand(sql, _conn))
          {
            int taskSchedID = 0;

            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
              var entity = new Org.TSK.Business.Models.TaskSchedule();

              entity.TaskScheduleId = reader["TaskScheduleID"].DbToInt32().Value;
              entity.ScheduledTaskId = reader["ScheduledTaskID"].DbToInt32().Value;
              entity.ScheduleName = reader["ScheduleName"].DbToString();
              entity.IsActive = reader["IsActive"].DbToBoolean().Value;
              entity.CreatedBy = reader["CreatedBy"].DbToString();
              entity.CreatedDate = reader["CreatedDate"].DbToDateTime().Value;
              entity.ModifiedBy = g.SystemInfo.DomainAndUser;
              entity.ModifiedDate = DateTime.Now;
              task.TaskSchedule = entity;
              taskSchedID = entity.TaskScheduleId;
            }

            reader.Close();

            string elementSql = "SELECT * from [TaskScheduling].[dbo].[TaskScheduleElements] " + g.crlf +
                                "Where TaskScheduleID = " + taskSchedID;

            using (SqlCommand elementCmd = new SqlCommand(elementSql, _conn))
            {
              elementCmd.CommandType = System.Data.CommandType.Text;
              SqlDataReader elementReader = elementCmd.ExecuteReader();

              while (elementReader.Read())
              {
                var entity = new Org.TSK.Business.Models.TaskScheduleElement();

                entity.TaskScheduleElementId = elementReader["TaskScheduleElementID"].DbToInt32().Value;
                entity.TaskScheduleId = elementReader["TaskScheduleID"].DbToInt32().Value;
                entity.IsActive = elementReader["IsActive"].DbToBoolean().Value;
                entity.TaskExecutionType = g.ToEnum<TaskExecutionType>(elementReader["TaskScheduleExecutionTypeID"].DbToInt32().Value, TaskExecutionType.NotSet);
                entity.FrequencySeconds = elementReader["FrequencySeconds"].DbToDecimal();
                entity.IsClockAligned = elementReader["IsClockAligned"].DbToBoolean().Value;
                entity.ScheduleElementPriority = elementReader["ScheduleElementPriority"].DbToInt32();
                entity.StartDate = elementReader["StartDate"].DbToDateTime();
                entity.StartTime = elementReader["StartTime"].DbToTimeSpan();
                entity.EndDate = elementReader["EndDate"].DbToDateTime();
                entity.EndTime = elementReader["EndTime"].DbToTimeSpan();
                var intervalTypeID = elementReader["IntervalTypeID"];
                entity.IntervalType = intervalTypeID == DBNull.Value ? IntervalType.NotSet : g.ToEnum<IntervalType>(intervalTypeID.DbToInt32().Value, IntervalType.NotSet);
                entity.OnSunday = elementReader["OnSunday"].DbToBoolean().Value;
                entity.OnMonday = elementReader["OnMonday"].DbToBoolean().Value;
                entity.OnTuesday = elementReader["OnTuesday"].DbToBoolean().Value;
                entity.OnWednesday = elementReader["OnWednesday"].DbToBoolean().Value;
                entity.OnThursday = elementReader["OnThursday"].DbToBoolean().Value;
                entity.OnFriday = elementReader["OnFriday"].DbToBoolean().Value;
                entity.OnSaturday = elementReader["OnSaturday"].DbToBoolean().Value;
                entity.OnWorkDays = elementReader["OnWorkDays"].DbToBoolean().Value;
                entity.OnEvenDays = elementReader["OnEvenDays"].DbToBoolean().Value;
                entity.OnOddDays = elementReader["OnOddDays"].DbToBoolean().Value;
                entity.SpecificDays = elementReader["SpecificDays"].DbToString();
                entity.ExceptSpecificDays = elementReader["ExceptSpecificDays"].DbToBoolean().Value;
                entity.First = elementReader["First"].DbToBoolean().Value;
                entity.Second = elementReader["Second"].DbToBoolean().Value;
                entity.Third = elementReader["Third"].DbToBoolean().Value;
                entity.Fourth = elementReader["Fourth"].DbToBoolean().Value;
                entity.Fifth = elementReader["Fifth"].DbToBoolean().Value;
                entity.Last = elementReader["Last"].DbToBoolean().Value;
                entity.Every = elementReader["Every"].DbToBoolean().Value;
                var holidayActionID = elementReader["HolidayActionID"];
                entity.HolidayActions = holidayActionID == DBNull.Value ? HolidayActions.NotSet : g.ToEnum<HolidayActions>(holidayActionID.DbToInt32().Value, HolidayActions.NotSet);
                var periodContextID = elementReader["PeriodContextID"];
                entity.PeriodContexts = periodContextID == DBNull.Value ? PeriodContexts.NotSet : g.ToEnum<PeriodContexts>(periodContextID.DbToInt32().Value, PeriodContexts.NotSet);
                entity.ExecutionLimit = elementReader["ExecutionLimit"].DbToInt32();
                entity.MaxRunTimeSeconds = elementReader["MaxRunTimeSeconds"].DbToInt32();
                entity.CreatedBy = elementReader["CreatedBy"].DbToString();
                entity.CreatedDate = elementReader["CreatedDate"].DbToDateTime().Value;
                entity.ModifiedBy = g.SystemInfo.DomainAndUser;
                entity.ModifiedDate = DateTime.Now;
                entity.TaskSchedule = task.TaskSchedule;
                entity.ScheduledTask = task;
                task.TaskSchedule.TaskScheduleElements.Add(entity);
              }
              elementReader.Close();
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all task schedules for each task.", ex);
      }
    }

    public bool GetTaskActiveStatus(int scheduledTaskId)
    {
      try
      {
        EnsureConnection();

        string sql = "SELECT IsActive " + g.crlf +
                     "FROM TaskScheduling.dbo.ScheduledTasks " + g.crlf +
                     "WHERE ScheduledTaskID = " + scheduledTaskId.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          return cmd.ExecuteScalar().DbToBoolean().Value;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine the active status of ScheduledTaskId " +
                            scheduledTaskId.ToString() + ".", ex);
      }
    }

    public MOD.ScheduledTask GetScheduledTask(int taskId, bool resolveParmSets, bool scheduleOnceNow = false)
    {
      try
      {
        var schedTasks = GetScheduledTasks(null);

        if (!schedTasks.ContainsKey(taskId))
          return null;

        var schedTask = schedTasks[taskId];

        var taskGroup = this.GetScheduledTaskGroup(taskId);
        if (taskGroup != null)
        {
          schedTask.TaskGroupId = taskGroup.TaskGroupId;
          schedTask.TaskGroupName = taskGroup.TaskGroupName;
        }

        SortedList<int, MOD.ScheduledTask> schedTasks2 = new SortedList<int, MOD.ScheduledTask>();
        schedTasks2.Add(schedTask.ScheduledTaskId, schedTask);

        GetTasksTaskSchedule(schedTasks2, scheduleOnceNow);
        var firstScheduledTask = schedTasks2.Values.First();
        firstScheduledTask.ParmSet = new ParmSet();  // defer building parms until just before the Task Engine dispatches the task.
        firstScheduledTask.ParmSet = GetTaskParms(firstScheduledTask.ScheduledTaskId, resolveParmSets);
        return firstScheduledTask;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get ScheduledTask for id " + taskId.ToString() + ".", ex);
      }
    }

    public MOD.ScheduledTask GetScheduledTask(string taskName, bool inProd)
    {
      try
      {
        MOD.ScheduledTask entity = null;

        EnsureConnection();

        string sql =
          "SELECT t.ScheduledTaskID AS ScheduledTaskId " + g.crlf +
          "		,t.TaskName AS TaskName " + g.crlf +
          "    , t.ProcessorTypeID AS ProcessorTypeId " + g.crlf +
          "		,t.ProcessorName AS ProcessorName " + g.crlf +
          "		,t.ProcessorVersion AS ProcessorVersion " + g.crlf +
          "		,t.AssemblyLocation AS AssemblyLocation " + g.crlf +
          "		,t.StoredProcedureName AS StoredProcedureName " + g.crlf +
          "		,t.AssemblyName AS AssemblyName " + g.crlf +
          "		,t.CatalogName AS CatalogName " + g.crlf +
          "		,t.CatalogEntry AS CatalogEntry " + g.crlf +
          "		,t.ObjectTypeName AS ObjectTypeName " + g.crlf +
          "		,t.IsActive AS IsActive " + g.crlf +
          "		,t.RunUntilTask AS RunUntilTask " + g.crlf +
          "		,t.RunUntilPeriodContextID AS RunUntilPeriodContextID " + g.crlf +
          "		,t.RunUntilOverride AS RunUntilOverride " + g.crlf +
          "		,t.RunUntilOffsetMinutes AS RunUntilOffsetMinutes " + g.crlf +
          "		,t.IsLongRunning AS IsLongRunning " + g.crlf +
          "		,t.TrackHistory AS TrackHistory " + g.crlf +
          "		,t.SuppressNotificationsOnSuccess AS SuppressNotificationsOnSuccess " + g.crlf +
          "		,t.ActiveScheduleID AS ActiveScheduleId " + g.crlf +
          "		,t.CreatedBy AS CreatedBy " + g.crlf +
          "		,t.CreatedDate AS CreatedDate " + g.crlf +
          "		,t.ModifiedBy AS ModifiedBy " + g.crlf +
          "		,t.ModifiedDate AS ModifiedDate " + g.crlf +
          "FROM dbo.ScheduledTasks t " +
          "WHERE t.TaskName = '" + taskName + "' ";


        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            entity = new MOD.ScheduledTask(_importDate, _configDbSpec);
            entity.ScheduledTaskId = reader["ScheduledTaskId"].DbToInt32().Value;
            entity.TaskName = reader["TaskName"].DbToString();
            entity.ProcessorTypeId = reader["ProcessorTypeId"].DbToInt32().Value;
            entity.ProcessorName = reader["ProcessorName"].DbToString();
            entity.ProcessorVersion = reader["ProcessorVersion"].DbToString();
            entity.AssemblyLocation = reader["AssemblyLocation"].DbToString();
            if (!inProd)
            {
              entity.AssemblyName = reader["AssemblyName"].DbToString();
              entity.CatalogName = reader["CatalogName"].DbToString();
              entity.CatalogEntry = reader["CatalogEntry"].DbToString();
              entity.ObjectTypeName = reader["ObjectTypeName"].DbToString();
            }
            entity.StoredProcedureName = reader["StoredProcedureName"].DbToString();
            entity.IsActive = reader["IsActive"].DbToBoolean().Value;
            entity.RunUntilTask = reader["RunUntilTask"].DbToBoolean().Value;
            entity.RunUntilPeriodContextID = reader["RunUntilPeriodContextID"].DbToInt32();
            entity.RunUntilOverride = reader["RunUntilOverride"].DbToBoolean().Value;
            entity.RunUntilOffsetMinutes = reader["RunUntilOffsetMinutes"].DbToInt32();
            entity.IsLongRunning = reader["IsLongRunning"].DbToBoolean().Value;
            entity.TrackHistory = reader["TrackHistory"].DbToBoolean().Value;
            entity.ActiveScheduleId = reader["ActiveScheduleID"].DbToInt32();
            entity.SuppressNotificationsOnSuccess = reader["SuppressNotificationsOnSuccess"].DbToBoolean().Value;
            entity.CreatedBy = reader["CreatedBy"].DbToString();
            entity.CreatedDate = reader["CreatedDate"].DbToDateTime().Value;
            entity.ModifiedBy = reader["ModifiedBy"].DbToString();
            entity.ModifiedDate = reader["ModifiedDate"].DbToDateTime();
          }
          reader.Close();

          return entity;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get ScheduledTask by name '" + taskName + ".", ex);
      }
    }

    public bool ConnectionCheck()
    {
      try
      {
        EnsureConnection();

        if (_conn != null)
          return false;

        if (_conn.State == ConnectionState.Connecting)
        {
          int limitSeconds = 5;
          while (_conn.State == ConnectionState.Connecting)
          {
            System.Threading.Thread.Sleep(1000);
            limitSeconds--;
            if (limitSeconds < 1)
              break;
          }
        }

        if (_conn.State == ConnectionState.Open)
          return true;

        return false;
      }
      catch
      {
        return false;
      }
    }

    public List<string> GetTasknodes()
    {
      var taskNodes = new List<string>();

      try
      {

        EnsureConnection();

        string sql = "SELECT Distinct ParameterValue " + g.crlf +
                     "FROM TaskScheduling.dbo.ScheduledTaskParameters TS " + g.crlf +
                     "JOIN TaskScheduling.dbo.ScheduledTasks ST " + g.crlf +
                     "ON TS.ParameterName = '$TASKNODE$' AND " + g.crlf +
                     "ST.TaskGroupID IN (1,2,3) AND TS.ScheduledTaskID NOT IN (7,20,106,111) " + g.crlf +
                     "ORDER BY ParameterValue";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            taskNodes.Add(reader["ParameterValue"].DbToString());
          }
          reader.Close();
        }

        return taskNodes;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of task nodes from the database.", ex);
      }

    }


    public List<string> GetTaskNames()
    {
      var taskNames = new List<string>();

      try
      {

        EnsureConnection();

        string sql = "SELECT TaskName " + g.crlf +
                     "FROM ScheduledTasks " + g.crlf +
                     "ORDER BY TaskName ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            taskNames.Add(reader["TaskName"].DbToString());
          }
          reader.Close();
        }

        return taskNames;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of task names from the database.", ex);
      }
    }

    public SortedList<int, MOD.ScheduledTask> GetScheduledTasks(List<string> tasksToRun)
    {
      var list = new SortedList<int, MOD.ScheduledTask>();

      try
      {
        EnsureConnection();

        bool inProd = _connectionString.Contains("OKC1EDW0001");

        using (SqlCommand cmd = new SqlCommand("dbo.usp_GetScheduledTasks", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new MOD.ScheduledTask(_importDate, _configDbSpec);

            entity.ScheduledTaskId = reader["ScheduledTaskId"].DbToInt32().Value;
            entity.TaskName = reader["TaskName"].DbToString();
            entity.ProcessorTypeId = reader["ProcessorTypeId"].DbToInt32().Value;
            entity.ProcessorName = reader["ProcessorName"].DbToString();
            entity.ProcessorVersion = reader["ProcessorVersion"].DbToString();
            entity.AssemblyLocation = reader["AssemblyLocation"].DbToString();

            if (!inProd)
            {
              entity.AssemblyName = reader["AssemblyName"].DbToString();
              entity.CatalogName = reader["CatalogName"].DbToString();
              entity.CatalogEntry = reader["CatalogEntry"].DbToString();
              entity.ObjectTypeName = reader["ObjectTypeName"].DbToString();
            }
            entity.StoredProcedureName = reader["StoredProcedureName"].DbToString();
            entity.IsActive = reader["IsActive"].DbToBoolean().Value;
            entity.RunUntilTask = reader["RunUntilTask"].DbToBoolean().Value;
            entity.RunUntilPeriodContextID = reader["RunUntilPeriodContextID"].DbToInt32();
            entity.RunUntilOverride = reader["RunUntilOverride"].DbToBoolean().Value;
            entity.RunUntilOffsetMinutes = reader["RunUntilOffsetMinutes"].DbToInt32();
            entity.IsLongRunning = reader["IsLongRunning"].DbToBoolean().Value;
            entity.TrackHistory = reader["TrackHistory"].DbToBoolean().Value;
            entity.ActiveScheduleId = reader["ActiveScheduleID"].DbToInt32();
            entity.SuppressNotificationsOnSuccess = reader["SuppressNotificationsOnSuccess"].DbToBoolean().Value;
            entity.CreatedBy = reader["CreatedBy"].DbToString();
            entity.CreatedDate = reader["CreatedDate"].DbToDateTime().Value;
            entity.ModifiedBy = reader["ModifiedBy"].DbToString();
            entity.ModifiedDate = reader["ModifiedDate"].DbToDateTime();

            if (tasksToRun != null)
            {
              if (tasksToRun.Contains(entity.TaskName))
                list.Add(entity.ScheduledTaskId, entity);
            }
            else
            {
              list.Add(entity.ScheduledTaskId, entity);
            }
          }
          reader.Close();
        }

        GetTaskAssignments(list);

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all ScheduledTasks.", ex);
      }
    }

    private void GetTaskAssignments(SortedList<int, MOD.ScheduledTask> tasks)
    {
      try
      {
        if (tasks == null || tasks.Count == 0)
          return;

        EnsureConnection();

        var scheduledTaskIDs = tasks.Values.Select(t => t.ScheduledTaskId).ToList();

        string sql =
          "SELECT A.TaskServiceAssignmentID AS TaskServiceAssignmentID, " + g.crlf +
          "  A.TaskServiceID AS TaskServiceID, " + g.crlf +
          "  S.TaskServiceName AS TaskServiceName, " + g.crlf +
          "  S.HostID AS HostID, " + g.crlf +
          "  H.HostName AS HostName, " + g.crlf +
          "  A.ScheduledTaskID as ScheduledTaskID, " + g.crlf +
          "  T.TaskName as TaskName, " + g.crlf +
          "  A.IsActive as IsActive " + g.crlf +
          "FROM TaskScheduling.dbo.TaskServiceAssignments A " + g.crlf +
          "INNER JOIN TaskScheduling.dbo.ScheduledTasks T ON T.ScheduledTaskID = A.ScheduledTaskID " + g.crlf +
          "INNER JOIN TaskScheduling.dbo.TaskServices S ON S.TaskServiceID = A.TaskServiceID " + g.crlf +
          "INNER JOIN TaskScheduling.dbo.ServiceHosts H ON S.HostID = H.HostID " + g.crlf +
          "WHERE A.ScheduledTaskID IN ( " + scheduledTaskIDs.Int32ListToString() + " )";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new MOD.TaskAssignment();
            entity.TaskServiceAssignmentID = reader["TaskServiceAssignmentID"].DbToInt32().Value;
            entity.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
            entity.TaskServiceName = reader["TaskServiceName"].DbToString();
            entity.ServiceHostID = reader["HostID"].DbToInt32().Value;
            entity.HostName = reader["HostName"].DbToString();
            entity.ScheduledTaskID = reader["ScheduledTaskID"].DbToInt32().Value;
            entity.TaskName = reader["TaskName"].DbToString();
            entity.IsActive = reader["IsActive"].DbToBoolean().Value;
            var task = tasks.Values.Where(t => t.ScheduledTaskId == entity.ScheduledTaskID).FirstOrDefault();
            if (task.TaskAssignmentSet == null)
              task.TaskAssignmentSet = new MOD.TaskAssignmentSet();
            task.TaskAssignmentSet.Add(entity);
          }
          reader.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the TaskAssignments for a list of scheduled tasks.", ex);
      }
    }

    public void RemoveTaskFromServiceAssignment(string serviceName, int taskId)
    {
      try
      {
        EnsureConnection();

        string sql =
          "DELETE A " + g.crlf +
          "FROM TaskScheduling.dbo.TaskServiceAssignments A " + g.crlf +
          "INNER JOIN TaskScheduling.dbo.TaskServices S " + g.crlf +
          "   ON S.TaskServiceID = A.TaskServiceID " + g.crlf +
          "INNER JOIN TaskScheduling.dbo.ScheduledTasks ST " + g.crlf +
          "   ON ST.ScheduledTaskID = A.ScheduledTaskID " + g.crlf +
          "WHERE S.TaskServiceName = '" + serviceName + "' " + g.crlf +
          "  AND A.ScheduledTaskID = " + taskId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to remove TaskID " + taskId.ToString() + " from assignment to service '" + serviceName + "'.", ex);
      }
    }

    public SortedList<int, MOD.PeriodContext> GetPeriodContexts()
    {
      var list = new SortedList<int, MOD.PeriodContext>();

      try
      {
        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("dbo.usp_GetPeriodContexts", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new MOD.PeriodContext();
            entity.PeriodContextId = reader["PeriodContextID"].DbToInt32().Value;
            entity.Period = reader["Period"].DbToString();

            list.Add(entity.PeriodContextId, entity);
          }
          reader.Close();
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all PeriodContexts.", ex);
      }
    }

    public void SetTaskGroupForScheduledTask(int scheduledTaskId, int? taskGroupId)
    {
      try
      {
        EnsureConnection();

        string sql = "UPDATE TaskScheduling.dbo.ScheduledTasks " + g.crlf +
                     "  SET TaskGroupID = " + (taskGroupId.HasValue ? taskGroupId.ToString() : "NULL") + " " + g.crlf +
                     "WHERE ScheduledTaskID = " + scheduledTaskId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update the ScheduledTask with ScheduledTaskId " + scheduledTaskId.ToString() +
                            " with the TaskGroupId " + taskGroupId.ToString() + ".", ex);
      }
    }

    public MOD.ScheduledTaskGroup GetScheduledTaskGroup(int scheduledTaskId)
    {
      MOD.ScheduledTaskGroup group = null;

      try
      {
        EnsureConnection();

        string sql = "SELECT g.TaskGroupID, " + g.crlf +
                     "  g.TaskGroupName " + g.crlf +
                     "FROM ScheduledTaskGroup g " + g.crlf +
                     "INNER JOIN ScheduledTasks t ON t.TaskGroupID = g.TaskGroupID " + g.crlf +
                     "WHERE t.ScheduledTaskID = " + scheduledTaskId.ToString() + " ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            group = new MOD.ScheduledTaskGroup();
            group.TaskGroupId = reader["TaskGroupID"].DbToInt32().Value;
            group.TaskGroupName = reader["TaskGroupName"].DbToString().Trim();
          }
          reader.Close();
        }

        return group;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the ScheduledTaskGroup for ScheduledTaskId " + scheduledTaskId.ToString() + ".", ex);
      }
    }

    public SortedList<int, MOD.ScheduledTaskGroup> GetScheduledTaskGroups()
    {
      SortedList<int, MOD.ScheduledTaskGroup> taskGroupList = new SortedList<int, MOD.ScheduledTaskGroup>();

      try
      {
        EnsureConnection();

        string sql = "SELECT g.TaskGroupID, " + g.crlf +
                     "  g.TaskGroupName " + g.crlf +
                     "FROM ScheduledTaskGroup g ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var taskGroup = new MOD.ScheduledTaskGroup();
            taskGroup.TaskGroupId = reader["TaskGroupID"].DbToInt32().Value;
            taskGroup.TaskGroupName = reader["TaskGroupName"].DbToString().Trim();

            if (!taskGroupList.ContainsKey(taskGroup.TaskGroupId))
              taskGroupList.Add(taskGroup.TaskGroupId, taskGroup);
          }

          reader.Close();
        }

        return taskGroupList;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all ScheduledTaskGroup rows.", ex);
      }
    }

    public MOD.TaskServiceSet GetTaskServiceSet()
    {
      var taskServiceSet = new MOD.TaskServiceSet();

      try
      {
        EnsureConnection();

        string sql = "SELECT t.TaskServiceID AS TaskServiceID, " + g.crlf +
                     "  t.HostID AS HostID, " + g.crlf +
                     "  h.HostName AS HostName, " + g.crlf +
                     "  t.ParentServiceID AS ParentServiceID, " + g.crlf +
                     "  t.TaskServiceName AS TaskServiceName, " + g.crlf +
                     "  t.ServiceTypeID AS ServiceTypeID, " + g.crlf +
                     "  st.ServiceType AS ServiceType, " + g.crlf +
                     "  t.WCFServiceBinding AS WCFServiceBinding, " + g.crlf +
                     "  t.WCFServicePort AS WCFServicePort, " + g.crlf +
                     "  t.WCFServiceName AS WCFServiceName " + g.crlf +
                     "FROM TaskScheduling.dbo.TaskServices t " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ServiceHosts h " + g.crlf +
                     "   ON h.HostID = t.HostID " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ServiceTypes st " + g.crlf +
                     "   ON st.ServiceTypeID = t.ServiceTypeID ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var taskService = new MOD.TaskService();
            taskService.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
            taskService.HostID = reader["HostID"].DbToInt32().Value;
            taskService.HostName = reader["HostName"].DbToString().Trim();
            taskService.ParentServiceID = reader["ParentServiceID"].DbToInt32();
            taskService.TaskServiceName = reader["TaskServiceName"].DbToString().Trim();
            taskService.ServiceType = g.ToEnum<ServiceType>(reader["ServiceType"].DbToString(), ServiceType.Unidentified);
            string wcfServiceBinding = reader["WCFServiceBinding"].DbToString();
            taskService.WCFServiceBinding = wcfServiceBinding == null ? WebServiceBinding.NotSet : g.ToEnum<WebServiceBinding>(wcfServiceBinding, WebServiceBinding.NotSet);
            taskService.WCFServicePort = reader["WCFServicePort"].DbToString();
            taskService.WCFServiceName = reader["WCFServiceName"].DbToString();
            taskServiceSet.Add(taskService);
          }

          reader.Close();
        }

        return taskServiceSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the TaskServiceSet.", ex);
      }
    }

    public MOD.TaskAssignmentSet GetTaskAssignmentsForTaskName(string taskServiceName = "")
    {
      try
      {
        var taskAssignmentSet = new MOD.TaskAssignmentSet();

        EnsureConnection();

        string whereClause = String.Empty;
        if (taskServiceName.IsNotBlank())
          whereClause = "WHERE s.TaskServiceName = '" + taskServiceName + "' " + g.crlf;

        string sql = "SELECT a.TaskServiceAssignmentID AS TaskServiceAssignmentID, " + g.crlf +
                     "  a.TaskServiceID AS TaskServiceID, " + g.crlf +
                     "  s.TaskServiceName AS TaskServiceName, " + g.crlf +
                     "  s.HostID AS HostID, " + g.crlf +
                     "  h.HostName AS HostName, " + g.crlf +
                     "  a.ScheduledTaskID AS ScheduledTaskID, " + g.crlf +
                     "  st.TaskName AS TaskName, " + g.crlf +
                     "  a.IsActive AS IsActive " + g.crlf +
                     "FROM TaskScheduling.dbo.TaskServiceAssignments a " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.TaskServices s " + g.crlf +
                     "   ON s.TaskServiceID = a.TaskServiceID " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ServiceHosts h " + g.crlf +
                     "   ON h.HostID = s.HostID " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ScheduledTasks st " + g.crlf +
                     "   ON st.ScheduledTaskID = a.ScheduledTaskID " + g.crlf +
                     whereClause;

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var taskAssignment = new MOD.TaskAssignment();
            taskAssignment.TaskServiceAssignmentID = reader["TaskServiceAssignmentID"].DbToInt32().Value;
            taskAssignment.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
            taskAssignment.TaskServiceName = reader["TaskServiceName"].DbToString().Trim();
            taskAssignment.ServiceHostID = reader["HostID"].DbToInt32().Value;
            taskAssignment.HostName = reader["HostName"].DbToString().Trim();
            taskAssignment.ScheduledTaskID = reader["ScheduledTaskID"].DbToInt32().Value;
            taskAssignment.TaskName = reader["TaskName"].DbToString().Trim();
            taskAssignment.IsActive = reader["IsActive"].DbToBoolean().Value;
            taskAssignmentSet.Add(taskAssignment);
          }

          reader.Close();
        }

        return taskAssignmentSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the TaskAssignments for TaskServiceName " + taskServiceName + ".", ex);
      }
    }

    public MOD.TaskAssignmentSet GetTaskAssignmentsForTaskId(int? taskServiceId = null)
    {
      try
      {
        var taskAssignmentSet = new MOD.TaskAssignmentSet();

        EnsureConnection();

        string whereClause = String.Empty;
        if (taskServiceId.HasValue)
          whereClause = "WHERE a.TaskServiceID = " + taskServiceId.Value.ToString() + " " + g.crlf;

        string sql = "SELECT a.TaskServiceAssignmentID AS TaskServiceAssignmentID, " + g.crlf +
                     "  a.TaskServiceID AS TaskServiceID, " + g.crlf +
                     "  s.TaskServiceName AS TaskServiceName, " + g.crlf +
                     "  s.HostID AS HostID, " + g.crlf +
                     "  h.HostName AS HostName, " + g.crlf +
                     "  a.ScheduledTaskID AS ScheduledTaskID, " + g.crlf +
                     "  st.TaskName AS TaskName, " + g.crlf +
                     "  a.IsActive AS IsActive " + g.crlf +
                     "FROM TaskScheduling.dbo.TaskServiceAssignments a " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.TaskServices s " + g.crlf +
                     "   ON s.TaskServiceID = a.TaskServiceID " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ServiceHosts h " + g.crlf +
                     "   ON h.HostID = s.HostID " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ScheduledTasks st " + g.crlf +
                     "   ON st.ScheduledTaskID = a.ScheduledTaskID " + g.crlf +
                     whereClause;

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var taskAssignment = new MOD.TaskAssignment();
            taskAssignment.TaskServiceAssignmentID = reader["TaskServiceAssignmentID"].DbToInt32().Value;
            taskAssignment.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
            taskAssignment.TaskServiceName = reader["TaskServiceName"].DbToString().Trim();
            taskAssignment.ServiceHostID = reader["HostID"].DbToInt32().Value;
            taskAssignment.HostName = reader["HostName"].DbToString().Trim();
            taskAssignment.ScheduledTaskID = reader["ScheduledTaskID"].DbToInt32().Value;
            taskAssignment.TaskName = reader["TaskName"].DbToString().Trim();
            taskAssignment.IsActive = reader["IsActive"].DbToBoolean().Value;
            taskAssignmentSet.Add(taskAssignment);
          }

          reader.Close();
        }

        return taskAssignmentSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the TaskAssignments for TaskServiceID " + taskServiceId.ToString() + ".", ex);
      }
    }

    public void AssignTasksToService(int taskServiceId, List<int> scheduledTaskIds)
    {
      bool transBegun = false;
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();

        var taskAssignmentSet = GetTaskAssignmentsForTaskId(taskServiceId);

        trans = _conn.BeginTransaction();
        transBegun = true;

        // First, remove any task assignments that are no longer assigned to the service

        foreach (var existingTaskAssignment in taskAssignmentSet)
        {
          if (!scheduledTaskIds.Contains(existingTaskAssignment.ScheduledTaskID))
          {
            string sql = "DELETE FROM TaskScheduling.dbo.TaskServiceAssignments " + g.crlf +
                         "WHERE TaskServiceID = " + taskServiceId.ToString() + " " + g.crlf +
                         "  AND ScheduledTaskID = " + existingTaskAssignment.ScheduledTaskID.ToString() + " ";

            using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
            {
              cmd.CommandType = System.Data.CommandType.Text;
              cmd.ExecuteNonQuery();
            }
          }
        }

        // Then, insert any new task assignments for the service

        foreach (int scheduledTaskId in scheduledTaskIds)
        {
          // if it is not already assigned, then assign it
          if (taskAssignmentSet.Where(t => t.ScheduledTaskID == scheduledTaskId).Count() == 0)
          {

            string sql = "INSERT INTO TaskScheduling.dbo.TaskServiceAssignments " + g.crlf +
                         "( " + g.crlf +
                         "  TaskServiceID, " + g.crlf +
                         "  ScheduledTaskID, " + g.crlf +
                         "  IsActive " + g.crlf +
                         ") " + g.crlf +
                         "VALUES " + g.crlf +
                         "( " + g.crlf +
                         "  " + taskServiceId.ToString() + ", " + g.crlf +
                         "  " + scheduledTaskId.ToString() + ", " + g.crlf +
                         "  1 " + g.crlf +
                         ") ";

            using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
            {
              cmd.CommandType = System.Data.CommandType.Text;
              cmd.ExecuteNonQuery();
            }
          }
        }

        trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun)
          trans.Rollback();

        throw new Exception("An exception occurred while attempting to update the task assignments for TaskService " + taskServiceId.ToString() + ".", ex);
      }
    }

    public MOD.HostSet GetHostSet()
    {
      var hostSet = new MOD.HostSet();

      try
      {
        EnsureConnection();

        string sql = "SELECT HostID, " + g.crlf +
                     "  HostName " + g.crlf +
                     "FROM ServiceHosts " + g.crlf +
                     "ORDER BY HostName ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var host = new MOD.Host();
            host.HostID = reader["HostID"].DbToInt32().Value;
            host.HostName = reader["HostName"].DbToString().Trim();

            hostSet.Add(host.HostName, host);
          }

          reader.Close();
        }

        return hostSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the HostSet.", ex);
      }
    }

    public ServiceSpecSet GetServicesForHost(int hostID)
    {
      try
      {
        var serviceSpecSet = new ServiceSpecSet();

        EnsureConnection();

        string sql = "SELECT s.TaskServiceID AS TaskServiceID, " + g.crlf +
                     "  s.HostID AS HostID, " + g.crlf +
                     "  h.HostName AS HostName, " + g.crlf +
                     "  s.ParentServiceID AS ParentServiceID, " + g.crlf +
                     "  s.TaskServiceName AS TaskServiceName, " + g.crlf +
                     "  s.ServiceTypeID AS ServiceTypeID, " + g.crlf +
                     "  s.WCFServiceBinding AS WCFServiceBinding, " + g.crlf +
                     "  s.WCFServicePort AS WCFServicePort, " + g.crlf +
                     "  s.WCFServiceName AS WCFServiceName " + g.crlf +
                     "FROM TaskScheduling.dbo.TaskServices s " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ServiceHosts h " + g.crlf +
                     "   ON s.HostID = h.HostID " + g.crlf +
                     "WHERE s.HostID = " + hostID.ToString() + " " + g.crlf +
                     "ORDER BY TaskServiceName ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var serviceSpec = new ServiceSpec();
            serviceSpec.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
            serviceSpec.ServiceHostID = reader["HostID"].DbToInt32().Value;
            serviceSpec.ParentServiceID = reader["ParentServiceID"].DbToInt32();
            serviceSpec.Name = reader["TaskServiceName"].DbToString();
            serviceSpec.ServiceType = g.ToEnum<ServiceType>(reader["ServiceTypeID"].DbToInt32().Value, ServiceType.Unidentified);
            string webServiceBinding = reader["WCFServiceBinding"].DbToString();
            serviceSpec.WebServiceBinding = webServiceBinding == null ? WebServiceBinding.NotSet : g.ToEnum<WebServiceBinding>(webServiceBinding, WebServiceBinding.NotSet);
            serviceSpec.WsHost = reader["HostName"].DbToString();
            serviceSpec.WsPort = reader["WCFServicePort"].DbToInt32().ToString();
            serviceSpec.WsServiceName = reader["WCFServiceName"].DbToString();
            serviceSpec.ParentServiceSpecSet = serviceSpecSet;
            serviceSpecSet.Add(serviceSpec.Name, serviceSpec);
          }

          reader.Close();

          return serviceSpecSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build a list of services for host ID " + hostID.ToString() + ".", ex);
      }
    }


    public ServiceHostSet GetServiceHostSet(ServiceEnvironment env)
    {
      var hostSet = new ServiceHostSet();
      hostSet.ParentServiceEnvironment = env;

      try
      {
        EnsureConnection();

        string sql = "SELECT HostID, " + g.crlf +
                     "  HostName " + g.crlf +
                     "FROM TaskScheduling.dbo.ServiceHosts " + g.crlf +
                     "ORDER BY HostName ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var host = new ServiceHost();
            host.HostID = reader["HostID"].DbToInt32().Value;
            host.Name = reader["HostName"].DbToString().Trim();
            host.ParentServiceHostSet = hostSet;
            hostSet.Add(host.Name, host);
          }

          reader.Close();
        }

        var mgmtServices = new List<ServiceSpec>();

        foreach (var host in hostSet.Values)
        {
          sql = "SELECT s.TaskServiceID AS TaskServiceID, " + g.crlf +
                "  s.HostID AS HostID, " + g.crlf +
                "  s.ParentServiceID AS ParentServiceID, " + g.crlf +
                "  h.HostName AS HostName, " + g.crlf +
                "  s.TaskServiceName AS TaskServiceName, " + g.crlf +
                "  s.ServiceTypeID AS ServiceTypeID, " + g.crlf +
                "  t.ServiceType AS ServiceType, " + g.crlf +
                "  s.WCFServiceBinding AS WCFServiceBinding, " + g.crlf +
                "  s.WCFServicePort AS WCFServicePort, " + g.crlf +
                "  s.WCFServiceName AS WCFServiceName " + g.crlf +
                "FROM TaskScheduling.dbo.TaskServices s" + g.crlf +
                "INNER JOIN TaskScheduling.dbo.ServiceHosts h " + g.crlf +
                "   ON h.HostID = s.HostID " + g.crlf +
                "INNER JOIN TaskScheduling.dbo.ServiceTypes t" + g.crlf +
                "   ON t.ServiceTypeID = s.ServiceTypeID " + g.crlf +
                "WHERE s.HostID = " + host.HostID.ToString() + " ";

          using (SqlCommand cmd = new SqlCommand(sql, _conn))
          {
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
              var serviceSpec = new ServiceSpec();
              serviceSpec.TaskServiceID = reader["TaskServiceID"].DbToInt32().Value;
              serviceSpec.ServiceHostID = reader["HostID"].DbToInt32().Value;
              serviceSpec.ParentServiceID = reader["ParentServiceID"].DbToInt32();
              serviceSpec.Name = reader["TaskServiceName"].DbToString();
              serviceSpec.ServiceType = g.ToEnum<ServiceType>(reader["ServiceType"].DbToString(), ServiceType.Unidentified);
              string webServiceBinding = reader["WCFServiceBinding"].DbToString();
              serviceSpec.WebServiceBinding = webServiceBinding == null ? WebServiceBinding.NotSet : g.ToEnum<WebServiceBinding>(webServiceBinding, WebServiceBinding.NotSet);
              serviceSpec.WsHost = reader["HostName"].DbToString();
              serviceSpec.WsPort = reader["WCFServicePort"].DbToInt32().ToString();
              serviceSpec.WsServiceName = reader["WCFServiceName"].DbToString();

              if (serviceSpec.ParentServiceID != null)
              {
                mgmtServices.Add(serviceSpec);
              }
              else
              {
                serviceSpec.ParentServiceSpecSet = host.ServiceSpecSet;
                serviceSpec.ParentServiceSpecSet.ParentServiceHost = host;
                host.ServiceSpecSet.Add(serviceSpec.Name, serviceSpec);
              }
            }

            reader.Close();
          }
        }

        if (mgmtServices.Count > 0)
        {
          foreach (var mgmtService in mgmtServices)
          {
            foreach (var host in hostSet.Values)
            {
              if (host.HostID == mgmtService.ServiceHostID)
              {
                foreach (var serviceSpec in host.ServiceSpecSet.Values)
                {
                  if (serviceSpec.TaskServiceID == mgmtService.ParentServiceID)
                  {
                    mgmtService.ParentServiceSpecSet = serviceSpec.ServiceSpecSet;
                    mgmtService.ParentServiceSpecSet.ParentServiceHost = serviceSpec.ServiceHost;
                    serviceSpec.ServiceSpecSet.Add(mgmtService.Name, mgmtService);
                  }
                }
              }
            }
          }
        }

        return hostSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the HostSet.", ex);
      }
    }

    public int AddHost(MOD.Host host)
    {
      try
      {
        EnsureConnection();

        string sql = "INSERT INTO TaskScheduling.dbo.ServiceHosts " + g.crlf +
                     "( " + g.crlf +
                     "  HostName " + g.crlf +
                     ") " + g.crlf +
                     "VALUES " + g.crlf +
                     "( " + g.crlf +
                     "  '" + host.HostName.ToUpper().Trim() + "' " + g.crlf +
                     "); " + g.crlf +
                     "SELECT SCOPE_IDENTITY(); ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          return Convert.ToInt32(cmd.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add the new ServiceHost to the database.", ex);
      }
    }

    public int AddService(ServiceSpec serviceSpec)
    {
      try
      {
        EnsureConnection();

        string sql = "INSERT INTO TaskScheduling.dbo.TaskServices " + g.crlf +
                     "( " + g.crlf +
                     "  HostID, " + g.crlf +
                     "  ParentServiceID, " + g.crlf +
                     "  TaskServiceName, " + g.crlf +
                     "  ServiceTypeID, " + g.crlf +
                     "  WCFServiceBinding, " + g.crlf +
                     "  WCFServicePort, " + g.crlf +
                     "  WCFServiceName " + g.crlf +
                     ") " + g.crlf +
                     "VALUES " + g.crlf +
                     "( " + g.crlf +
                     "  " + serviceSpec.ServiceHostID.ToString() + ", " + g.crlf +
                     "  " + (serviceSpec.ParentServiceID.HasValue ? serviceSpec.ParentServiceID.ToString() : "NULL") + ", " + g.crlf +
                     "  '" + serviceSpec.Name + "', " + g.crlf +
                     "  " + Convert.ToInt32(serviceSpec.ServiceType).ToString() + ", " + g.crlf +
                     "  " + (serviceSpec.WebServiceBinding == WebServiceBinding.NotSet ? "NULL" : "'" + serviceSpec.WebServiceBinding.ToString() + "'") + ", " + g.crlf +
                     "  " + (serviceSpec.WsPort.IsBlank() ? "NULL" : serviceSpec.WsPort.Trim()) + ", " + g.crlf +
                     "  " + (serviceSpec.WsServiceName.IsBlank() ? "NULL" : "'" + serviceSpec.WsServiceName.Trim() + "'") + " " + g.crlf +
                     "); " + g.crlf +
                     "SELECT SCOPE_IDENTITY(); ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          return Convert.ToInt32(cmd.ExecuteScalar());
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add the new ServiceHost to the database.", ex);
      }
    }

    public void UpdateService(ServiceSpec serviceSpec)
    {
      try
      {
        EnsureConnection();

        string sql = "UPDATE TaskScheduling.dbo.TaskServices " + g.crlf +
                     "  SET HostID = " + serviceSpec.ServiceHostID.ToString() + ", " + g.crlf +
                     "      ParentServiceID = " + (serviceSpec.ParentServiceID.HasValue ? serviceSpec.ParentServiceID.Value.ToString() : "NULL") + ", " + g.crlf +
                     "      TaskServiceName = '" + serviceSpec.Name.Trim() + "', " + g.crlf +
                     "      ServiceTypeID = " + Convert.ToInt32(serviceSpec.ServiceType).ToString() + ", " + g.crlf +
                     "      WCFServiceBinding = " + (serviceSpec.WebServiceBinding == WebServiceBinding.NotSet ?
                         "NULL" : "'" + serviceSpec.WebServiceBinding.ToString().Trim() + "'") + ", " + g.crlf +
                     "      WCFServicePort = " + (serviceSpec.WsPort.IsBlank() ? "NULL" : serviceSpec.WsPort.Trim()) + ", " + g.crlf +
                     "      WCFServiceName = " + (serviceSpec.WsServiceName.IsBlank() ? "NULL" : "'" + serviceSpec.WsServiceName.Trim() + "'") + " " + g.crlf +
                     "WHERE TaskServiceID = " + serviceSpec.TaskServiceID.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update the service in the database.", ex);
      }
    }

    public void SetServiceTypes()
    {
      try
      {
        EnsureConnection();

        string sql = "INSERT INTO TaskScheduling.dbo.ServiceTypes " + g.crlf +
                     "( " + g.crlf +
                     "  ServiceTypeID, " + g.crlf +
                     "  ServiceType " + g.crlf +
                     ") " + g.crlf +
                     "VALUES " + g.crlf +
                     "( " + g.crlf +
                     "  1, " + g.crlf +
                     "  'WindowsService' " + g.crlf +
                     "), " + g.crlf +
                     "( " + g.crlf +
                     "  2, " + g.crlf +
                     "  'WCFWebService' " + g.crlf +
                     "), " + g.crlf +
                     "( " + g.crlf +
                     "  3, " + g.crlf +
                     "  'WebSite' " + g.crlf +
                     ") ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add the ServiceTypes to the database.", ex);
      }
    }

    public SortedList<int, MOD.TaskScheduleExecutionType> GetTaskScheduleExecutionTypes()
    {
      var list = new SortedList<int, MOD.TaskScheduleExecutionType>();

      try
      {
        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("dbo.usp_GetTaskScheduleExecutionTypes", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new MOD.TaskScheduleExecutionType();
            entity.TaskScheduleExecutionTypeId = reader["TaskScheduleExecutionTypeID"].DbToInt32().Value;
            entity.TaskExecutionTypeDesc = reader["ExecutionType"].DbToString();
            list.Add(entity.TaskScheduleExecutionTypeId, entity);
          }
          reader.Close();
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all TaskScheduleExecutionTypes.", ex);
      }
    }

    public SortedList<int, MOD.TaskScheduleIntervalType> GetTaskScheduleIntervalTypes()
    {
      var list = new SortedList<int, MOD.TaskScheduleIntervalType>();

      try
      {
        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("dbo.usp_GetTaskScheduleIntervalTypes", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new MOD.TaskScheduleIntervalType();
            entity.IntervalTypeId = reader["IntervalTypeID"].DbToInt32().Value;
            entity.IntervalTypeDesc = reader["IntervalType"].DbToString();
            list.Add(entity.IntervalTypeId, entity);
          }
          reader.Close();
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get all TaskScheduleIntervalTypes.", ex);
      }
    }

    public SortedList<int, MOD.TaskSchedule> GetTaskSchedules(SqlTransaction trans = null)
    {
      SortedList<int, MOD.TaskSchedule> list = GetTaskSchedules(null, trans);
      return list;
    }

    public SortedList<int, MOD.TaskSchedule> GetTaskSchedules(int? scheduledTaskID, SqlTransaction trans = null)
    {
      var list = new SortedList<int, MOD.TaskSchedule>();

      string whereClause = "";

      if (scheduledTaskID.HasValue)
        whereClause = " WHERE [ScheduledTaskID] = " + scheduledTaskID;

      try
      {
        EnsureConnection();

        string sql = "SELECT [TaskScheduleID] " +
                     ",[ScheduledTaskID] " +
                     ",[ScheduleName] " +
                     ",[IsActive] " +
                     ",[CreatedBy] " +
                     ",[CreatedDate] " +
                     ",[ModifiedBy] " +
                     ",[ModifiedDate] " +
                     " FROM [TaskScheduling].[dbo].[TaskSchedules]" +
                     whereClause;

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return list;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var taskSchedule = new MOD.TaskSchedule();
            taskSchedule.TaskScheduleId = r["TaskScheduleID"].ToInt32();
            taskSchedule.ScheduledTaskId = r["ScheduledTaskID"].ToInt32();
            taskSchedule.ScheduleName = r["ScheduleName"].DbToString();
            taskSchedule.IsActive = r["IsActive"].ToBoolean();
            taskSchedule.CreatedBy = r["CreatedBy"].DbToString();
            taskSchedule.CreatedDate = r["CreatedDate"].ToDateTime();
            taskSchedule.ModifiedBy = r["ModifiedBy"].DbToString();
            taskSchedule.ModifiedDate = r["ModifiedDate"].ToNullableDateTime();

            list.Add(taskSchedule.TaskScheduleId, taskSchedule);
          }
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get TaskSchedules from TaskScheduling Database", ex);
      }
    }

    public SortedList<int, MOD.TaskScheduleElement> GetScheduleElements(int taskScheduleId)
    {
      var list = new SortedList<int, MOD.TaskScheduleElement>();

      try
      {
        EnsureConnection();

        string sql = " SELECT [TaskScheduleElementID] " + g.crlf +
                     " ,[TaskScheduleID] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[TaskScheduleExecutionTypeID] " + g.crlf +
                     " ,[FrequencySeconds] " + g.crlf +
                     " ,[IsClockAligned] " + g.crlf +
                     " ,[ScheduleElementPriority] " + g.crlf +
                     " ,[StartDate] " + g.crlf +
                     " ,[StartTime] " + g.crlf +
                     " ,[EndDate] " + g.crlf +
                     " ,[EndTime] " + g.crlf +
                     " ,[IntervalTypeID] " + g.crlf +
                     " ,[OnSunday] " + g.crlf +
                     " ,[OnMonday] " + g.crlf +
                     " ,[OnTuesday] " + g.crlf +
                     " ,[OnWednesday] " + g.crlf +
                     " ,[OnThursday] " + g.crlf +
                     " ,[OnFriday] " + g.crlf +
                     " ,[OnSaturday] " + g.crlf +
                     " ,[OnWorkDays] " + g.crlf +
                     " ,[OnEvenDays] " + g.crlf +
                     " ,[OnOddDays] " + g.crlf +
                     " ,[SpecificDays] " + g.crlf +
                     " ,[ExceptSpecificDays] " + g.crlf +
                     " ,[First] " + g.crlf +
                     " ,[Second] " + g.crlf +
                     " ,[Third] " + g.crlf +
                     " ,[Fourth] " + g.crlf +
                     " ,[Fifth] " + g.crlf +
                     " ,[Last] " + g.crlf +
                     " ,[Every] " + g.crlf +
                     " ,[HolidayActionID] " + g.crlf +
                     " ,[PeriodContextID] " + g.crlf +
                     " ,[ExecutionLimit] " + g.crlf +
                     " ,[MaxRunTimeSeconds] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedDate] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedDate] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[TaskScheduleElements] " + g.crlf +
                     " WHERE [TaskScheduleID] = " + taskScheduleId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return list;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var element = new MOD.TaskScheduleElement();
            element.TaskScheduleElementId = r["TaskScheduleElementID"].DbToInt32().Value;
            element.TaskScheduleId = r["TaskScheduleID"].DbToInt32().Value;
            element.IsActive = r["IsActive"].DbToBoolean().Value;
            element.TaskExecutionType = r["TaskScheduleExecutionTypeID"].DbToInt32().ToEnum<TaskExecutionType>(TaskExecutionType.NotSet);
            element.FrequencySeconds = r["FrequencySeconds"].DbToInt32();
            element.IsClockAligned = r["IsClockAligned"].DbToBoolean().Value;
            element.ScheduleElementPriority = r["ScheduleElementPriority"].DbToInt32();
            element.StartDate = r["StartDate"].DbToDateTime();
            element.StartTime = r["StartTime"].DbToTimeSpan();
            element.EndDate = r["EndDate"].DbToDateTime();
            element.EndTime = r["EndTime"].DbToTimeSpan();
            element.IntervalType = r["IntervalTypeID"].DbToInt32().ToEnum<IntervalType>(IntervalType.NotSet);
            element.OnSunday = r["OnSunday"].DbToBoolean().Value;
            element.OnMonday = r["OnMonday"].DbToBoolean().Value;
            element.OnTuesday = r["OnTuesday"].DbToBoolean().Value;
            element.OnWednesday = r["OnWednesday"].DbToBoolean().Value;
            element.OnThursday = r["OnThursday"].DbToBoolean().Value;
            element.OnFriday = r["OnFriday"].DbToBoolean().Value;
            element.OnSaturday = r["OnSaturday"].DbToBoolean().Value;
            element.OnWorkDays = r["OnWorkDays"].DbToBoolean().Value;
            element.OnEvenDays = r["OnEvenDays"].DbToBoolean().Value;
            element.OnOddDays = r["OnOddDays"].DbToBoolean().Value;
            element.SpecificDays = r["SpecificDays"].DbToString();
            element.ExceptSpecificDays = r["ExceptSpecificDays"].DbToBoolean().Value;
            element.First = r["First"].DbToBoolean().Value;
            element.Second = r["Second"].DbToBoolean().Value;
            element.Third = r["Third"].DbToBoolean().Value;
            element.Fourth = r["Fourth"].DbToBoolean().Value;
            element.Fifth = r["Fifth"].DbToBoolean().Value;
            element.Last = r["Last"].DbToBoolean().Value;
            element.Every = r["Every"].DbToBoolean().Value;
            element.HolidayActions = r["HolidayActionID"].DbToInt32().ToEnum<HolidayActions>(HolidayActions.NotSet);
            element.PeriodContexts = r["PeriodContextID"].DbToInt32().ToEnum<PeriodContexts>(PeriodContexts.NotSet);
            element.ExecutionLimit = r["ExecutionLimit"].DbToInt32();
            element.MaxRunTimeSeconds = r["MaxRunTimeSeconds"].DbToInt32();
            element.CreatedBy = r["CreatedBy"].DbToString();
            element.CreatedDate = r["CreatedDate"].DbToDateTime().Value;
            element.ModifiedBy = r["ModifiedBy"].DbToString();
            element.ModifiedDate = r["ModifiedDate"].DbToDateTime();

            list.Add(element.TaskScheduleElementId, element);
          }
        }
        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get TaskScheduleElements from TaskScheduling Database", ex);
      }
    }

    public SortedList<int, MOD.TaskParameter> GetTaskParameters(int? scheduledTaskID)
    {
      var list = new SortedList<int, MOD.TaskParameter>();

      try
      {
        EnsureConnection();

        string whereClause = "";

        if (scheduledTaskID.HasValue)
          whereClause = " WHERE [ScheduledTaskID] = " + scheduledTaskID.ToString();
        else
          whereClause = " WHERE [ParameterID] BETWEEN 10001 AND 19999";

        string sql = "SELECT [ParameterID] " + g.crlf +
                     " ,[ScheduledTaskID] " + g.crlf +
                     " ,[ParameterName] " + g.crlf +
                     " ,[ParameterValue] " + g.crlf +
                     " ,[DataType] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedDate] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedDate] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     whereClause;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return list;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var taskParameter = new MOD.TaskParameter();
            taskParameter.ParameterID = r["ParameterID"].DbToInt32().Value;
            taskParameter.ScheduledTaskID = r["ScheduledTaskID"].DbToInt32();
            taskParameter.ParameterName = r["ParameterName"].DbToString();
            taskParameter.ParameterValue = r["ParameterValue"].DbToString();
            taskParameter.DataType = r["DataType"].DbToString();
            taskParameter.CreatedBy = r["CreatedBy"].DbToString();
            taskParameter.CreatedDate = r["CreatedDate"].DbToDateTime().Value;
            taskParameter.ModifiedBy = r["ModifiedBy"].DbToString();
            taskParameter.ModifiedDate = r["ModifiedDate"].DbToDateTime();

            list.Add(taskParameter.ParameterID, taskParameter);
          }
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get TaskParameters from TaskScheduling Database", ex);
      }
    }

    public SortedList<string, MOD.TaskParameterSet> GetParameterSets()
    {
      var set = new MOD.TaskParameterSet();

      var list = new SortedList<string, MOD.TaskParameterSet>();

      try
      {
        EnsureConnection();

        string sql = "SELECT [ParameterID] " + g.crlf +
                     " ,[ParameterSetName] " + g.crlf +
                     " ,[ParameterName] " + g.crlf +
                     " ,[ParameterValue] " + g.crlf +
                     " ,[DataType] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedDate] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedDate] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterSetName] IS NOT NULL ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return list;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var tp = new MOD.TaskParameter();
            tp.ParameterSetName = r["ParameterSetName"].DbToString();
            tp.ParameterID = r["ParameterID"].ToInt32();
            tp.ParameterName = r["ParameterName"].DbToString();
            tp.ParameterValue = r["ParameterValue"].DbToString();
            tp.DataType = r["DataType"].DbToString();
            tp.CreatedBy = r["CreatedBy"].DbToString();
            tp.CreatedDate = r["CreatedDate"].ToDateTime();
            tp.ModifiedBy = r["ModifiedBy"].DbToString();
            tp.ModifiedDate = r["ModifiedDate"].DbToDateTime();

            if (list.ContainsKey(tp.ParameterSetName))
            {
              list[tp.ParameterSetName].TaskParameters.Add(tp);
            }
            else
            {
              set = new MOD.TaskParameterSet();
              set.ParameterSetName = tp.ParameterSetName;
              set.TaskParameters.Add(tp);
              list.Add(tp.ParameterSetName, set);
            }
          }
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get TaskParameters from TaskScheduling Database", ex);
      }
    }

    public ParmSet GetParametersForTask(string taskName)
    {
      var parmSet = new ParmSet();

      try
      {
        EnsureConnection();

        string sql = "SELECT p.ParameterID " + g.crlf +
                     "  ,p.ScheduledTaskID " + g.crlf +
                     "  ,p.ParameterSetName " + g.crlf +
                     "  ,p.ParameterName " + g.crlf +
                     "  ,p.ParameterValue " + g.crlf +
                     "  ,p.DataType " + g.crlf +
                     "FROM TaskScheduling.dbo.ScheduledTaskParameters p " + g.crlf +
                     "INNER JOIN TaskScheduling.dbo.ScheduledTasks t ON t.ScheduledTaskID = p.ScheduledTaskID " + g.crlf +
                     "WHERE t.TaskName = '" + taskName + "' ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return parmSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var parm = new Parm();
            parm.ParameterId = r["ParameterID"].DbToInt32().Value;
            parm.ScheduledTaskId = r["ScheduledTaskID"].DbToInt32();
            parm.ParameterSetName = r["ParameterSetName"].DbToString();
            parm.ParameterName = r["ParameterName"].DbToString();
            parm.ParameterValue = r["ParameterValue"].DbToString();
            parm.ParameterType = r["DataType"].DbToString().ToType();
            parmSet.Add(parm);
          }
        }

        return ResolveParmSets(parmSet);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get TaskParameters from TaskScheduling Database", ex);
      }
    }

    public ParmSet ResolveParmSets(ParmSet parmSet)
    {
      try
      {
        var resolvedParmSet = new ParmSet();
        var parmsToAdd = new List<Parm>();

        int? scheduledTaskId = 0;
        var firstParm = parmSet.FirstOrDefault();
        if (firstParm != null)
          scheduledTaskId = firstParm.ScheduledTaskId.Value;

        foreach (var parm in parmSet)
        {
          if (parm.ParameterValue != null)
          {
            if (parm.ParameterValue.ToString().StartsWith("ParmSet="))
            {
              string parmSetName = parm.ParameterValue.ToString().Replace("ParmSet=", String.Empty);
              if (parmSetName.IsNotBlank())
              {
                parmsToAdd.AddRange(GetParmsForParmSet(parmSetName));
                parm.RemoveThisParm = true;
              }
            }
          }
        }

        foreach (var parm in parmSet)
        {
          if (!parm.RemoveThisParm)
            resolvedParmSet.Add(parm);
        }

        foreach (var parm in parmsToAdd)
          parm.ScheduledTaskId = scheduledTaskId;

        resolvedParmSet.AddRange(parmsToAdd);

        return resolvedParmSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to exchange parms that point to ParameterSets to the included parms in the ParameterSets.", ex);
      }
    }

    public ParmSet GetParmsForParmSet(string parmSetName)
    {
      var parmSet = new ParmSet();

      string sql = "SELECT p.ParameterID " + g.crlf +
                   "  ,p.ScheduledTaskID " + g.crlf +
                   "  ,p.ParameterSetName " + g.crlf +
                   "  ,p.ParameterName " + g.crlf +
                   "  ,p.ParameterValue " + g.crlf +
                   "  ,p.DataType " + g.crlf +
                   "FROM TaskScheduling.dbo.ScheduledTaskParameters p " + g.crlf +
                   "WHERE p.ParameterSetName = '" + parmSetName + "' ";

      using (var cmd = new SqlCommand(sql, _conn))
      {
        cmd.CommandType = System.Data.CommandType.Text;
        var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables.Count == 0)
          return parmSet;

        var dt = ds.Tables[0];
        foreach (DataRow r in dt.Rows)
        {
          var parm = new Parm();
          parm.ParameterId = r["ParameterID"].DbToInt32().Value;
          parm.ScheduledTaskId = r["ScheduledTaskID"].DbToInt32();
          parm.ParameterSetName = r["ParameterSetName"].DbToString();
          parm.ParameterName = r["ParameterName"].DbToString();
          parm.ParameterValue = r["ParameterValue"].DbToString();
          parm.ParameterType = r["DataType"].DbToString().ToType();
          parmSet.Add(parm);
        }
      }

      return parmSet;
    }

    public void AddScheduledTask(MOD.ScheduledTask st)
    {
      try
      {
        EnsureConnection();

        string sql = " INSERT INTO [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " ([TaskName], [ProcessorTypeID], [ProcessorName], [ProcessorVersion], [AssemblyLocation], " + g.crlf +
                     " [AssemblyName], [CatalogName], [CatalogEntry], [ObjectTypeName], " + g.crlf +
                     " [StoredProcedureName], [IsActive], [RunUntilTask], [RunUntilPeriodContextID], " + g.crlf +
                     " [RunUntilOverride], [RunUntilOffsetMinutes], [IsLongRunning], [TrackHistory], [SuppressNotificationsOnSuccess], " + g.crlf +
                     " [ActiveScheduleId], [CreatedBy], [CreatedDate]) " + g.crlf +
                     " VALUES ('" + st.TaskName + "'" +
                     " ," + st.ProcessorTypeId.ToString() +
                     " ," + (st.ProcessorName.IsNotBlank() ? "'" + st.ProcessorName + "'" : "NULL") +
                     " ," + (st.ProcessorVersion.IsNotBlank() ? "'" + st.ProcessorVersion + "'" : "NULL") +
                     " ," + (st.AssemblyLocation.IsNotBlank() ? "'" + st.AssemblyLocation + "'" : "NULL") +
                     " ," + (st.AssemblyName.IsNotBlank() ? "'" + st.AssemblyName + "'" : "NULL") +
                     " ," + (st.CatalogName.IsNotBlank() ? "'" + st.CatalogName + "'" : "NULL") +
                     " ," + (st.CatalogEntry.IsNotBlank() ? "'" + st.CatalogEntry + "'" : "NULL") +
                     " ," + (st.ObjectTypeName.IsNotBlank() ? "'" + st.ObjectTypeName + "'" : "NULL") +
                     " ," + (st.StoredProcedureName.IsNotBlank() ? "'" + st.StoredProcedureName + "'" : "NULL") +
                     " ," + st.IsActive.ToInt32() +
                     " ," + st.RunUntilTask.ToInt32() +
                     " ," + (st.RunUntilPeriodContextID.HasValue ? st.RunUntilPeriodContextID.ToString() : "NULL") + g.crlf +
                     " ," + st.RunUntilOverride.ToInt32() +
                     " ," + (st.RunUntilOffsetMinutes.HasValue ? st.RunUntilOffsetMinutes.ToString() : "NULL") +
                     " ," + st.IsLongRunning.ToInt32() +
                     " ," + st.TrackHistory.ToInt32() +
                     " ," + st.SuppressNotificationsOnSuccess.ToInt32() +
                     " ," + (st.ActiveScheduleId.HasValue ? st.ActiveScheduleId.ToString() : "NULL") +
                     " ,'" + st.CreatedBy + "'" +
                     " ,'" + st.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add ScheduledTask in TaskScheduling Database", ex);
      }
    }

    public int AddTaskSchedule(MOD.TaskSchedule ts, SqlTransaction trans = null)
    {
      int taskScheduleId = -1;

      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = " INSERT INTO [TaskScheduling].[dbo].[TaskSchedules] " + g.crlf +
                     " ([ScheduledTaskID], [ScheduleName], [IsActive], " + g.crlf +
                     " [CreatedBy], [CreatedDate]) " + g.crlf +
                     " VALUES (" + ts.ScheduledTaskId.ToString() +
                     " ,'" + ts.ScheduleName + "'" +
                     " ," + (ts.IsActive ? "1" : "0") +
                     " ,'" + ts.CreatedBy + "'" +
                     " ,'" + ts.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'); " +
                     " SELECT SCOPE_IDENTITY(); ";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          var returnValue = cmd.ExecuteScalar();
          taskScheduleId = returnValue.ToInt32();
          return taskScheduleId;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add TaskSchedule in TaskScheduling Database", ex);
      }
    }

    public bool SetTaskFrequencyTo(int scheduledTaskId, int seconds)
    {
      try
      {
        var scheduledTask = this.GetScheduledTask(scheduledTaskId, true);

        MOD.TaskScheduleElement scheduleElement = null;

        if (scheduledTask.TaskSchedule != null)
        {
          var taskSchedule = scheduledTask.TaskSchedule;
          if (taskSchedule.TaskScheduleElements.Count == 1)
          {
            scheduleElement = taskSchedule.TaskScheduleElements.First();
          }
        }

        if (scheduleElement == null)
          return false;

        scheduleElement.FrequencySeconds = seconds;

        UpdateTaskScheduleElement(scheduleElement);
        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update the frequency of ScheduledTaskID " +
                            scheduledTaskId.ToString() + " to " + seconds + " seconds.", ex);
      }
    }

    public void UpdateActiveScheduleID(int? activeScheduleID, int scheduledTaskID, SqlTransaction trans = null)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " SET [ActiveScheduleID] = " + (activeScheduleID.HasValue ? activeScheduleID.ToString() : "NULL") + g.crlf +
                     " WHERE [ScheduledTaskID] = " + scheduledTaskID.ToString();

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update ActiveScheduleID in TaskScheduling Database", ex);
      }
    }

    public void InsertTaskScheduleElement(MOD.TaskScheduleElement element)
    {
      InsertTaskScheduleElement(element, null);
    }

    public void InsertTaskScheduleElement(MOD.TaskScheduleElement tse, SqlTransaction trans)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = " INSERT INTO [TaskScheduling].[dbo].[TaskScheduleElements] " + g.crlf +
                     " ([TaskScheduleID], [IsActive], [TaskScheduleExecutionTypeID], [FrequencySeconds], " + g.crlf +
                     " [IsClockAligned], [ScheduleElementPriority], [StartDate], [StartTime], " + g.crlf +
                     " [EndDate], [EndTime], [IntervalTypeID], [OnSunday], [OnMonday], [OnTuesday], [OnWednesday], " + g.crlf +
                     " [OnThursday], [OnFriday], [OnSaturday], [OnWorkDays], [OnEvenDays], [OnOddDays], " + g.crlf +
                     " [SpecificDays], [ExceptSpecificDays], [First], [Second], [Third], [Fourth], [Fifth], " + g.crlf +
                     " [Last], [Every], [HolidayActionID], [PeriodContextID], [ExecutionLimit], [MaxRunTimeSeconds], " + g.crlf +
                     " [CreatedBy], [CreatedDate]) " + g.crlf +
                     " VALUES (" + tse.TaskScheduleId.ToString() + ", " +
                     tse.IsActive.ToInt32() + ", " +
                     tse.TaskExecutionType.ToInt32() + ", " +
                     (tse.FrequencySeconds.HasValue ? tse.FrequencySeconds.ToString() : "NULL") + ", " +
                     tse.IsClockAligned.ToInt32() + ", " +
                     (tse.ScheduleElementPriority.HasValue ? tse.ScheduleElementPriority.ToString() : "NULL") + ", " +
                     (tse.StartDate.HasValue ? "'" + tse.StartDate.ToDateTime().ToString("yyyy-MM-dd") + "'" : "NULL") + ", " +
                     (tse.StartTime.HasValue ? "'" + tse.StartTime.ToString() + "'" : "NULL") + ", " +
                     (tse.EndDate.HasValue ? "'" + tse.EndDate.ToDateTime().ToString("yyyy-MM-dd") + "'" : "NULL") + ", " +
                     (tse.EndTime.HasValue ? "'" + tse.EndTime.ToString() + "'" : "NULL") + ", " +
                     (tse.IntervalType == IntervalType.NotSet ? "NULL" : tse.IntervalType.ToInt32().ToString()) + ", " +
                     tse.OnSunday.ToInt32() + ", " +
                     tse.OnMonday.ToInt32() + ", " +
                     tse.OnTuesday.ToInt32() + ", " +
                     tse.OnWednesday.ToInt32() + ", " +
                     tse.OnThursday.ToInt32() + ", " +
                     tse.OnFriday.ToInt32() + ", " +
                     tse.OnSaturday.ToInt32() + ", " +
                     tse.OnWorkDays.ToInt32() + ", " +
                     tse.OnEvenDays.ToInt32() + ", " +
                     tse.OnOddDays.ToInt32() + ", " +
                     (tse.SpecificDays.IsBlank() ? "NULL" : "'" + tse.SpecificDays + "'") + ", " +
                     tse.ExceptSpecificDays.ToInt32() + ", " +
                     tse.First.ToInt32() + ", " +
                     tse.Second.ToInt32() + ", " +
                     tse.Third.ToInt32() + ", " +
                     tse.Fourth.ToInt32() + ", " +
                     tse.Fifth.ToInt32() + ", " +
                     tse.Last.ToInt32() + ", " +
                     tse.Every.ToInt32() + ", " +
                     (tse.HolidayActions == HolidayActions.NotSet ? "NULL" : tse.HolidayActions.ToInt32().ToString()) + ", " +
                     (tse.PeriodContexts == PeriodContexts.NotSet ? "NULL" : tse.PeriodContexts.ToInt32().ToString()) + ", " +
                     (tse.ExecutionLimit.HasValue ? tse.ExecutionLimit.ToString() : "NULL") + ", " +
                     (tse.MaxRunTimeSeconds.HasValue ? tse.MaxRunTimeSeconds.ToString() : "NULL") + ", " +
                     "'" + tse.CreatedBy + "', " +
                     "'" + tse.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add TaskScheduleElement in TaskScheduling Database", ex);
      }
    }

    public void InsertTaskParameter(MOD.TaskParameter tp, bool isDryRun = false)
    {
      bool transBegun = false;
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        InsertTaskParameter(tp, trans);

        if (isDryRun)
          trans.Rollback();
        else
          trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun = true && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();

        throw new Exception("An exception occurred attempting to add TaskParameter in TaskScheduling Database", ex);
      }
    }

    private void InsertTaskParameter(MOD.TaskParameter tp, SqlTransaction trans)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        tp.ParameterID = GetNextParameterID(tp, trans);

        string sql = " INSERT INTO [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     "([ParameterID] " + g.crlf +
                     " ,[ScheduledTaskID] " + g.crlf +
                     " ,[ParameterSetName] " + g.crlf +
                     " ,[ParameterName] " + g.crlf +
                     " ,[ParameterValue] " + g.crlf +
                     " ,[DataType] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedDate]) " + g.crlf +
                     " VALUES (" + tp.ParameterID.ToString() +
                     " ," + (tp.ScheduledTaskID.HasValue ? tp.ScheduledTaskID.ToString() : "NULL") +
                     " ," + (tp.ParameterSetName.IsBlank() ? "NULL" : "'" + tp.ParameterSetName + "'") +
                     " ,'" + tp.ParameterName + "'" +
                     " ," + (tp.ParameterValue.IsBlank() ? "NULL" : "'" + tp.ParameterValue + "'") +
                     " ,'" + tp.DataType + "'" +
                     " ,'" + tp.CreatedBy + "'" +
                     " ,'" + tp.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to add TaskParameter in TaskScheduling Database", ex);
      }
    }

    private int GetNextParameterID(MOD.TaskParameter tp, SqlTransaction trans)
    {
      try
      {
        EnsureConnection();

        var relevantParms = GetExistingParmsOfSameType(tp, trans);

        int parameterID = 0;

        if (relevantParms.Count == 0)
        {
          if (tp.ScheduledTaskID.HasValue)
            parameterID = 30001;
          else if (tp.ParameterSetName.IsNotBlank())
            parameterID = 20001;
          else
            parameterID = 10001;
          return parameterID;
        }

        //Task Parameters
        if (tp.ScheduledTaskID.HasValue)
        {
          var relatedParms = relevantParms.Where(parm => parm.ScheduledTaskID == tp.ScheduledTaskID).ToList();
          if (relatedParms.Count == 0)
          {
            int lastParameterID = relevantParms.Max(parm => parm.ParameterID);
            parameterID = (int)Math.Floor(((decimal)lastParameterID + 9) / 10) * 10 + 1;
            return parameterID;
          }

          parameterID = relatedParms.Max(parm => parm.ParameterID).ToInt32() + 1;
          int index = relevantParms.FindIndex(parm => parm.ParameterID == parameterID);
          if (index >= 0)
          {
            int lastParameterID = relevantParms.Max(parm => parm.ParameterID);
            int nextParameterID = (int)Math.Floor(((decimal)lastParameterID + 9) / 10) * 10 + 1;
            parameterID = MoveParametersToEndAndReturnNextID(nextParameterID, relatedParms, trans);
          }
        }
        //Parameter Sets
        else if (tp.ParameterSetName.IsNotBlank())
        {
          var relatedParms = relevantParms.Where(parm => parm.ParameterSetName == tp.ParameterSetName).ToList();
          if (relatedParms.Count == 0)
          {
            int lastParameterID = relevantParms.Max(parm => parm.ParameterID);
            parameterID = (int)Math.Floor(((decimal)lastParameterID + 9) / 10) * 10 + 1;
            return parameterID;
          }

          parameterID = relatedParms.Max(parm => parm.ParameterID).ToInt32() + 1;
          int index = relevantParms.FindIndex(parm => parm.ParameterID == parameterID);
          if (index >= 0)
          {
            int lastParameterID = relevantParms.Max(parm => parm.ParameterID);
            int nextParameterID = (int)Math.Floor(((decimal)lastParameterID + 9) / 10) * 10 + 1;
            parameterID = MoveParametersToEndAndReturnNextID(nextParameterID, relatedParms, trans);
          }
        }
        //Parameter Variables
        else
          parameterID = relevantParms.Max(parm => parm.ParameterID) + 1;

        return parameterID;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to Get Next Parameter ID from TaskScheduling Database", ex);
      }
    }

    private List<MOD.TaskParameter> GetExistingParmsOfSameType(MOD.TaskParameter tp, SqlTransaction trans)
    {
      try
      {
        var existingParms = new List<MOD.TaskParameter>();

        string whereClause = "WHERE [ParameterID] ";
        if (tp.ScheduledTaskID.HasValue)
          whereClause += "> 30000";
        else if (tp.ParameterSetName.IsNotBlank())
          whereClause += "BETWEEN 20001 AND 30000";
        else
          whereClause += "< 20001";

        string sql = " SELECT [ParameterID] " + g.crlf +
                     " ,[ScheduledTaskID] " + g.crlf +
                     " ,[ParameterSetName] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     whereClause;

        using (SqlCommand cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var parm = new MOD.TaskParameter();
            parm.ParameterID = reader["ParameterID"].ToInt32();
            parm.ScheduledTaskID = reader["ScheduledTaskID"].DbToInt32();
            parm.ParameterSetName = reader["ParameterSetName"].DbToString();

            existingParms.Add(parm);
          }
          reader.Close();
        }
        return existingParms;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when try to get existing parameters of the same type as parameter '" + tp.ParameterName + "'.", ex);
      }
    }

    private int MoveParametersToEndAndReturnNextID(int nextParameterID, List<MOD.TaskParameter> relatedParms, SqlTransaction trans)
    {
      try
      {
        foreach (var parm in relatedParms)
        {
          string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                       " SET [ParameterID] = " + nextParameterID.ToString() + g.crlf +
                       " WHERE [ParameterID] = " + parm.ParameterID.ToString();

          using (var cmd = new SqlCommand(sql, _conn, trans))
            cmd.ExecuteNonQuery();

          nextParameterID++;
        }

        return nextParameterID;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to move parameters to the end in the TaskScheduling Database", ex);
      }
    }

    public void UpdateScheduledTask(MOD.ScheduledTask st)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " SET [TaskName] = '" + st.TaskName + "'" + g.crlf +
                     " ,[ProcessorTypeID] = " + st.ProcessorTypeId.ToString() + g.crlf +
                     " ,[ProcessorName] = " + (st.ProcessorName.IsBlank() ? "NULL" : "'" + st.ProcessorName + "'") + g.crlf +
                     " ,[ProcessorVersion] = " + (st.ProcessorVersion.IsBlank() ? "NULL" : "'" + st.ProcessorVersion + "'") + g.crlf +
                     " ,[AssemblyLocation] = " + (st.AssemblyLocation.IsBlank() ? "NULL" : "'" + st.AssemblyLocation + "'") + g.crlf +
                     " ,[AssemblyName] = " + (st.AssemblyName.IsBlank() ? "NULL" : "'" + st.AssemblyName + "'") + g.crlf +
                     " ,[CatalogName] = " + (st.CatalogName.IsBlank() ? "NULL" : "'" + st.CatalogName + "'") + g.crlf +
                     " ,[CatalogEntry] = " + (st.CatalogEntry.IsBlank() ? "NULL" : "'" + st.CatalogEntry + "'") + g.crlf +
                     " ,[ObjectTypeName] = " + (st.ObjectTypeName.IsBlank() ? "NULL" : "'" + st.ObjectTypeName + "'") + g.crlf +
                     " ,[StoredProcedureName] = " + (st.StoredProcedureName.IsBlank() ? "NULL" : "'" + st.StoredProcedureName + "'") + g.crlf +
                     " ,[IsActive] = " + st.IsActive.ToInt32() + g.crlf +
                     " ,[RunUntilTask] = " + st.RunUntilTask.ToInt32() + g.crlf +
                     " ,[RunUntilPeriodContextID] = " + (st.RunUntilPeriodContextID.HasValue ? st.RunUntilPeriodContextID.ToString() : "NULL") + g.crlf +
                     " ,[RunUntilOverride] = " + st.RunUntilOverride.ToInt32() + g.crlf +
                     " ,[RunUntilOffsetMinutes] = " + (st.RunUntilOffsetMinutes.HasValue ? st.RunUntilOffsetMinutes.ToString() : "NULL") + g.crlf +
                     " ,[IsLongRunning] = " + st.IsLongRunning.ToInt32() + g.crlf +
                     " ,[TrackHistory] = " + st.TrackHistory.ToInt32() + g.crlf +
                     " ,[SuppressNotificationsOnSuccess] = " + st.SuppressNotificationsOnSuccess.ToInt32() + g.crlf +
                     " ,[ActiveScheduleId] = " + (st.ActiveScheduleId.HasValue ? st.ActiveScheduleId.ToString() : "NULL") + g.crlf +
                     " ,[ModifiedBy] = " + (st.ModifiedBy.IsBlank() ? "NULL" : "'" + st.ModifiedBy + "'") + g.crlf +
                     " ,[ModifiedDate] = " + (st.ModifiedDate.HasValue ? "'" + st.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                     " WHERE [ScheduledTaskID] = " + st.ScheduledTaskId;

        if (_connectionString.Contains("OKC1EDW0001"))
        {
          sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                " SET [TaskName] = '" + st.TaskName + "'" + g.crlf +
                " ,[ProcessorTypeID] = " + st.ProcessorTypeId.ToString() + g.crlf +
                " ,[ProcessorName] = " + (st.ProcessorName.IsBlank() ? "NULL" : "'" + st.ProcessorName + "'") + g.crlf +
                " ,[ProcessorVersion] = " + (st.ProcessorVersion.IsBlank() ? "NULL" : "'" + st.ProcessorVersion + "'") + g.crlf +
                " ,[AssemblyLocation] = " + (st.AssemblyLocation.IsBlank() ? "NULL" : "'" + st.AssemblyLocation + "'") + g.crlf +
                " ,[StoredProcedureName] = " + (st.StoredProcedureName.IsBlank() ? "NULL" : "'" + st.StoredProcedureName + "'") + g.crlf +
                " ,[IsActive] = " + st.IsActive.ToInt32() + g.crlf +
                " ,[RunUntilTask] = " + st.RunUntilTask.ToInt32() + g.crlf +
                " ,[RunUntilPeriodContextID] = " + (st.RunUntilPeriodContextID.HasValue ? st.RunUntilPeriodContextID.ToString() : "NULL") + g.crlf +
                " ,[RunUntilOverride] = " + st.RunUntilOverride.ToInt32() + g.crlf +
                " ,[RunUntilOffsetMinutes] = " + (st.RunUntilOffsetMinutes.HasValue ? st.RunUntilOffsetMinutes.ToString() : "NULL") + g.crlf +
                " ,[IsLongRunning] = " + st.IsLongRunning.ToInt32() + g.crlf +
                " ,[TrackHistory] = " + st.TrackHistory.ToInt32() + g.crlf +
                " ,[SuppressNotificationsOnSuccess] = " + st.SuppressNotificationsOnSuccess.ToInt32() + g.crlf +
                " ,[ActiveScheduleId] = " + (st.ActiveScheduleId.HasValue ? st.ActiveScheduleId.ToString() : "NULL") + g.crlf +
                " ,[ModifiedBy] = " + (st.ModifiedBy.IsBlank() ? "NULL" : "'" + st.ModifiedBy + "'") + g.crlf +
                " ,[ModifiedDate] = " + (st.ModifiedDate.HasValue ? "'" + st.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                " WHERE [ScheduledTaskID] = " + st.ScheduledTaskId;
        }

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update ScheduledTask in TaskScheduling Database", ex);
      }
    }

    public void UpdateTaskSchedule(MOD.TaskSchedule ts)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [TaskScheduling].[dbo].[TaskSchedules] " + g.crlf +
                     " SET [ScheduledTaskID] = " + ts.ScheduledTaskId.ToString() + g.crlf +
                     " ,[ScheduleName] = '" + ts.ScheduleName + "'" + g.crlf +
                     " ,[IsActive] = " + (ts.IsActive ? "1" : "0") + g.crlf +
                     " ,[ModifiedBy] = " + (ts.ModifiedBy.IsBlank() ? "NULL" : "'" + ts.ModifiedBy + "'") + g.crlf +
                     " ,[ModifiedDate] = " + (ts.ModifiedDate.HasValue ? "'" + ts.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                     " WHERE [TaskScheduleID] = " + ts.TaskScheduleId.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update TaskSchedule in TaskScheduling Database", ex);
      }
    }

    public void UpdateTaskScheduleElement(MOD.TaskScheduleElement tse)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [TaskScheduling].[dbo].[TaskScheduleElements] " + g.crlf +
                     " SET [IsActive] = " + (tse.IsActive ? "1" : "0") + g.crlf +
                     " ,[TaskScheduleExecutionTypeID] = " + tse.TaskExecutionType.ToInt32() + g.crlf +
                     " ,[FrequencySeconds] = " + (tse.FrequencySeconds.HasValue ? tse.FrequencySeconds.ToString() : "NULL") + g.crlf +
                     " ,[IsClockAligned] = " + (tse.IsClockAligned ? "1" : "0") + g.crlf +
                     " ,[ScheduleElementPriority] = " + (tse.ScheduleElementPriority.HasValue ? tse.ScheduleElementPriority.ToString() : "NULL") + g.crlf +
                     " ,[StartDate] = " + (tse.StartDate.HasValue ? "'" + tse.StartDate.ToString() + "'" : "NULL") + g.crlf +
                     " ,[StartTime] = " + (tse.StartTime.HasValue ? "'" + tse.StartTime.ToString() + "'" : "NULL") + g.crlf +
                     " ,[EndDate] = " + (tse.EndDate.HasValue ? "'" + tse.EndDate.ToString() + "'" : "NULL") + g.crlf +
                     " ,[EndTime] = " + (tse.EndTime.HasValue ? "'" + tse.EndTime.ToString() + "'" : "NULL") + g.crlf +
                     " ,[IntervalTypeID] = " + (tse.IntervalType == IntervalType.NotSet ? "NULL" : tse.IntervalType.ToInt32().ToString()) + g.crlf +
                     " ,[OnSunday] = " + tse.OnSunday.ToInt32() + g.crlf +
                     " ,[OnMonday] = " + tse.OnMonday.ToInt32() + g.crlf +
                     " ,[OnTuesday] = " + tse.OnTuesday.ToInt32() + g.crlf +
                     " ,[OnWednesday] = " + tse.OnWednesday.ToInt32() + g.crlf +
                     " ,[OnThursday] = " + tse.OnThursday.ToInt32() + g.crlf +
                     " ,[OnFriday] = " + tse.OnFriday.ToInt32() + g.crlf +
                     " ,[OnSaturday] = " + tse.OnSaturday.ToInt32() + g.crlf +
                     " ,[OnWorkDays] = " + tse.OnWorkDays.ToInt32() + g.crlf +
                     " ,[OnEvenDays] = " + tse.OnEvenDays.ToInt32() + g.crlf +
                     " ,[OnOddDays] = " + tse.OnOddDays.ToInt32() + g.crlf +
                     " ,[SpecificDays] = " + (tse.SpecificDays.IsBlank() ? "'" + tse.SpecificDays + "'" : "NULL") + g.crlf +
                     " ,[ExceptSpecificDays] = " + tse.ExceptSpecificDays.ToInt32() + g.crlf +
                     " ,[First] = " + tse.First.ToInt32() + g.crlf +
                     " ,[Second] = " + tse.Second.ToInt32() + g.crlf +
                     " ,[Third] = " + tse.Third.ToInt32() + g.crlf +
                     " ,[Fourth] = " + tse.Fourth.ToInt32() + g.crlf +
                     " ,[Fifth] = " + tse.Fifth.ToInt32() + g.crlf +
                     " ,[Last] = " + tse.Last.ToInt32() + g.crlf +
                     " ,[Every] = " + tse.Every.ToInt32() + g.crlf +
                     " ,[HolidayActionID] = " + (tse.HolidayActions == HolidayActions.NotSet ? "NULL" : tse.HolidayActions.ToInt32().ToString()) + g.crlf +
                     " ,[PeriodContextID] = " + (tse.PeriodContexts == PeriodContexts.NotSet ? "NULL" : tse.PeriodContexts.ToInt32().ToString()) + g.crlf +
                     " ,[ExecutionLimit] = " + (tse.ExecutionLimit.HasValue ? tse.ExecutionLimit.ToString() : "NULL") + g.crlf +
                     " ,[MaxRunTimeSeconds] = " + (tse.MaxRunTimeSeconds.HasValue ? tse.MaxRunTimeSeconds.ToString() : "NULL") + g.crlf +
                     " ,[ModifiedBy] = " + (tse.ModifiedBy.IsBlank() ? "'" + tse.ModifiedBy + "'" : "NULL") + g.crlf +
                     " ,[ModifiedDate] = " + (tse.ModifiedDate.HasValue ? "'" + tse.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                     " WHERE [TaskScheduleElementID] = " + tse.TaskScheduleElementId.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update ScheduledTask in TaskScheduling Database", ex);
      }
    }

    public void UpdateTaskParameter(MOD.TaskParameter tp, bool isDryRun = false)
    {
      bool transBegun = false;
      SqlTransaction trans = null;
      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        string scheduledTaskIdUpdate = "";
        string parameterSetNameUpdate = "";

        if (tp.ScheduledTaskID.HasValue && tp.ScheduledTaskID.Value > 0)
          scheduledTaskIdUpdate = " ,[ScheduledTaskID] = " + tp.ScheduledTaskID.ToString() + g.crlf;

        if (tp.ParameterSetName.IsNotBlank())
          parameterSetNameUpdate = " ,[ParameterSetName] = '" + tp.ParameterSetName.ToString() + "' " + g.crlf;

        string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " SET [ParameterName] = '" + tp.ParameterName + "'" + g.crlf +
                     scheduledTaskIdUpdate +
                     parameterSetNameUpdate +
                     " ,[ParameterValue] = " + (tp.ParameterValue.IsBlank() ? "NULL" : "'" + tp.ParameterValue + "'") + g.crlf +
                     " ,[DataType] = '" + tp.DataType + "'" + g.crlf +
                     " ,[ModifiedBy] = " + (tp.ModifiedBy.IsBlank() ? "NULL" : "'" + tp.ModifiedBy + "'") + g.crlf +
                     " ,[ModifiedDate] = " + (tp.ModifiedDate.HasValue ? "'" + tp.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                     " WHERE [ParameterID] = " + tp.ParameterID.ToString();

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }

        if (isDryRun)
          trans.Rollback();
        else
          trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();
        throw new Exception("An exception occurred attempting to update TaskParameter in TaskScheduling Database", ex);
      }
    }

    public void UpdateTaskParameterByNameAndSchedTaskId(MOD.TaskParameter tp, bool isDryRun = false)
    {
      bool transBegun = false;
      SqlTransaction trans = null;
      try
      {
        EnsureConnection();

        trans = _conn.BeginTransaction();
        transBegun = true;

        string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     "    SET [ParameterValue] = " + (tp.ParameterValue.IsBlank() ? "NULL" : "'" + tp.ParameterValue + "'") + g.crlf +
                     "       ,[DataType] = '" + tp.DataType + "'" + g.crlf +
                     "       ,[ModifiedBy] = " + (tp.ModifiedBy.IsBlank() ? "NULL" : "'" + tp.ModifiedBy + "'") + g.crlf +
                     "       ,[ModifiedDate] = " + (tp.ModifiedDate.HasValue ? "'" + tp.ModifiedDate.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" : "NULL") + g.crlf +
                     "  WHERE [ScheduledTaskID] = " + tp.ScheduledTaskID + g.crlf +
                     "    AND [ParameterName] = '" + tp.ParameterName + "'";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }

        if (isDryRun)
          trans.Rollback();
        else
          trans.Commit();
      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();
        throw new Exception("An exception occurred attempting to update TaskParameter in TaskScheduling Database", ex);
      }
    }

    public void DeleteScheduledTask(int scheduledTaskId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " WHERE [ScheduledTaskID] = " + scheduledTaskId.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete ScheduledTask in TaskScheduling Database", ex);
      }
    }

    public void DeleteTaskSchedule(int taskScheduleID, SqlTransaction trans = null)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = "DELETE FROM [TaskScheduling].[dbo].[TaskSchedules] " + g.crlf +
                     "WHERE [TaskScheduleID] = " + taskScheduleID.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete TaskSchedule " + taskScheduleID.ToString() + ".", ex);
      }
    }

    public void DeleteAllSchedulesForTask(int taskId, SqlTransaction trans)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        var schedules = GetTaskSchedules(taskId, trans);

        foreach (var schedule in schedules.Values)
        {
          DeleteAllTaskScheduleElementsForSchedule(schedule.TaskScheduleId, trans);
          DeleteTaskSchedule(schedule.TaskScheduleId, trans);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete all TaskSchedules for ScheduledTaskID " + taskId.ToString() + ".", ex);
      }
    }

    public void DeleteTaskScheduleElement(int taskScheduleElementID)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [TaskScheduling].[dbo].[TaskScheduleElements] " + g.crlf +
                     " WHERE [TaskScheduleElementID] = " + taskScheduleElementID.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete TaskSchedule in TaskScheduling Database", ex);
      }
    }

    public void DeleteAllTaskScheduleElementsForSchedule(int taskScheduleId, SqlTransaction trans = null)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = "DELETE FROM TaskScheduling.dbo.TaskScheduleElements " + g.crlf +
                     "WHERE TaskScheduleID = " + taskScheduleId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete TaskSchedule in TaskScheduling Database", ex);
      }
    }

    public void DeleteAllParametersForTask(int scheduledTaskId, SqlTransaction trans = null)
    {
      try
      {
        if (trans == null)
          EnsureConnection();

        string sql = "DELETE FROM TaskScheduling.dbo.ScheduledTaskParameters " + g.crlf +
                     "WHERE ScheduledTaskID = " + scheduledTaskId.ToString();

        using (var cmd = new SqlCommand(sql, _conn, trans))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete all ScheduledTaskParameters for ScheduledTaskID " + scheduledTaskId.ToString() + ".", ex);
      }
    }

    public void DeleteTaskParameter(int parameterID)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     " WHERE [ParameterID] = " + parameterID.ToString();

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete TaskParameter in TaskScheduling Database", ex);
      }
    }

    public void CopyScheduleAndParameters(int copyFromTaskId,  int copyToTaskId, bool copySchedule, bool copyParameters)
    {
      DateTime copyDateTime = DateTime.Now;
      SqlTransaction trans = null;

      try
      {
        if (copyFromTaskId == copyToTaskId)
          throw new Exception("The copyFromTaskId and copyToTaskId are the same (ScheduledTaskId " + copyFromTaskId.ToString() + ").");

        var copyFromTask = this.GetScheduledTask(copyFromTaskId, false);
        if (copyFromTask == null)
          throw new Exception("There is no ScheduledTask for the copyFromTaskId " + copyFromTaskId.ToString() + ".");

        var copyToTask = this.GetScheduledTask(copyToTaskId, false);
        if (copyToTask == null)
          throw new Exception("There is no ScheduledTask for the copyToTaskId " + copyToTaskId.ToString() + ".");

        EnsureConnection();

        trans = _conn.BeginTransaction();

        if (copySchedule)
        {
          DeleteAllSchedulesForTask(copyToTaskId, trans);

          if (copyFromTask.TaskSchedule != null)
          {
            var schedule = new MOD.TaskSchedule();
            schedule.ScheduledTaskId = copyToTaskId;
            schedule.ScheduleName = copyFromTask.TaskSchedule.ScheduleName;
            schedule.ScheduleNumber = copyFromTask.TaskSchedule.ScheduleNumber;
            schedule.IsActive = copyFromTask.IsActive;
            schedule.IsManaged = copyFromTask.IsManaged;
            schedule.CreatedBy = g.SystemInfo.DomainAndUser;
            schedule.CreatedDate = copyDateTime;
            int scheduleId = AddTaskSchedule(schedule, trans);

            foreach (var scheduleElement in copyFromTask.TaskSchedule.TaskScheduleElements)
            {
              var element = scheduleElement.Clone();
              element.TaskScheduleElementId = 0;
              element.TaskScheduleId = scheduleId;
              element.CreatedBy = g.SystemInfo.DomainAndUser;
              element.CreatedDate = copyDateTime;
              element.ModifiedBy = null;
              element.ModifiedDate = null;
              InsertTaskScheduleElement(element, trans);
            }

            UpdateActiveScheduleID(scheduleId, copyToTaskId, trans);
          }
        }

        if (copyParameters)
        {
          DeleteAllParametersForTask(copyToTaskId, trans);

          foreach (var copyFromParm in copyFromTask.ParmSet)
          {
            if (copyFromParm.ParameterId > 30000)
            {
              var taskParameter = new MOD.TaskParameter();
              taskParameter.ScheduledTaskID = copyToTaskId;
              taskParameter.ParameterSetName = copyFromParm.ParameterSetName;
              taskParameter.ParameterName = copyFromParm.ParameterName;
              taskParameter.DataType = GetParameterDataType(copyFromParm.ParameterType);
              taskParameter.ParameterValue = copyFromParm.ParameterValue.ToString();
              taskParameter.CreatedBy = g.SystemInfo.DomainAndUser;
              taskParameter.CreatedDate = copyDateTime;
              InsertTaskParameter(taskParameter, trans);
            }
          }
        }

        trans.Commit();
      }
      catch (Exception ex)
      {
        trans.Rollback();

        throw new Exception("An exception occurred while attempting to copy the task schedule and task parameters from ScheduledTaskId " +
                            copyFromTaskId.ToString() + " to " + copyToTaskId.ToString() + " - copySchedule = " + copySchedule.ToString() +
                            ", copyParameters = " + copyParameters.ToString() + ".", ex);
      }
    }

    private string GetParameterDataType(Type parameterType)
    {
      try
      {
        if (parameterType == null)
          throw new Exception("The value provided as parameterType (which is of type System.Type) is null.");


        if (parameterType.FullName.Contains("System.Collections.Generic.Dictionary`2"))
          return "Dictionary<string,string>";

        if (parameterType.FullName.Contains("System.Collections.Generic.List`1"))
          return "List<string>";

        return parameterType.FullName;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to convert the ParameterType to a string.", ex);
      }
    }

    public int InsertPeriodHistory(MOD.PeriodHistory ph)
    {
      try
      {
        EnsureConnection();

        int periodId;

        string sql = " DECLARE @PeriodHistoryID int " + g.crlf +
                     " IF EXISTS (SELECT * FROM [TaskScheduling].[dbo].[PeriodHistory]" + g.crlf +
                     "             WHERE [ScheduledTaskID] = @ScheduledTaskID " + g.crlf +
                     "               AND [StartDateTime] = @StartDateTime " + g.crlf +
                     "               AND [EndDateTime] = @EndDateTime) " + g.crlf +
                     " BEGIN " + g.crlf +
                     "  SELECT @PeriodHistoryID = PeriodID " + g.crlf +
                     "  FROM [TaskScheduling].[dbo].[PeriodHistory] " + g.crlf +
                     "  WHERE [ScheduledTaskID] = @ScheduledTaskID " + g.crlf +
                     "    AND [StartDateTime] = @StartDateTime " + g.crlf +
                     "    AND [EndDateTime] = @EndDateTime " + g.crlf +
                     " END " + g.crlf +
                     " ELSE BEGIN " + g.crlf +
                     "  INSERT INTO [TaskScheduling].[dbo].[PeriodHistory] " + g.crlf +
                     "   ([ScheduledTaskID],[TaskName],[RunForPeriod],[StartDateTime],[EndDateTime],[OverdueDateTime] " + g.crlf +
                     "   ,[RunUntilTask],[RunUntilPeriodContextID],[RunUntilOffsetMinutes],[OverdueNotificationSent] " + g.crlf +
                     "   ,[OverdueNoticeAcknowledged],[AcknowledgedBy],[AcknowledgedDate]) " + g.crlf +
                     "  VALUES " + g.crlf +
                     "   (@ScheduledTaskID, @TaskName, @RunForPeriod, @StartDateTime, @EndDateTime, @OverdueDateTime " + g.crlf +
                     "   ,@RunUntilTask, @RunUntilPeriodContextID, @RunUntilOffsetMinutes, @OverdueNotificationSent " + g.crlf +
                     "   ,@OverdueNoticeAcknowledged, @AcknowledgedBy, @AcknowledgedDate) " + g.crlf +
                     "  SET @PeriodHistoryID = @@IDENTITY " + g.crlf +
                     " END " + g.crlf +
                     " SELECT @PeriodHistoryID ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.Parameters.AddWithValue("@ScheduledTaskID", ph.ScheduledTaskId);
          cmd.Parameters.AddWithValue("@TaskName", ph.TaskName);
          cmd.Parameters.AddWithValue("@RunForPeriod", ph.RunForPeriod);
          cmd.Parameters.AddWithValue("@StartDateTime", ph.StartDateTime);
          cmd.Parameters.AddWithValue("@EndDateTime", ph.EndDateTime);
          cmd.Parameters.AddWithValue("@OverdueDateTime", (object)ph.OverdueDateTime ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunUntilTask", ph.RunUntilTask);
          cmd.Parameters.AddWithValue("@RunUntilPeriodContextID", (object)ph.RunUntilPeriodContextId ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunUntilOffsetMinutes", (object)ph.RunUntilOffsetMinutes ?? 0);
          cmd.Parameters.AddWithValue("@OverdueNotificationSent", ph.OverdueNotificationSent);
          cmd.Parameters.AddWithValue("@OverdueNoticeAcknowledged", ph.OverdueNoticeAcknowledged);
          cmd.Parameters.AddWithValue("@AcknowledgedBy", (object)ph.AcknowledgedBy ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@AcknowledgedDate", (object)ph.AcknowledgedDate ?? DBNull.Value);

          periodId = cmd.ExecuteScalar().ToInt32();
        }

        return periodId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to insert PeriodHisotry in the TaskScheduling Database", ex);
      }
    }

    public int InsertRunHistory(MOD.RunHistory rh)
    {
      try
      {
        EnsureConnection();

        int runId;

        string sql = " INSERT INTO [TaskScheduling].[dbo].[RunHistory] " + g.crlf +
                     "        ([PeriodHistoryID],[ScheduledTaskID],[TaskName],[ProcessorName],[ProcessorVersion],[ProcessorTypeID] " + g.crlf +
                     "        ,[ExecutionStatusID],[RunStatusID],[RunCode],[NoWorkDone],[StartDateTime],[EndDateTime] " + g.crlf +
                     "        ,[RunHost],[RunUser],[Message],[RunUntilTask],[RunUntilPeriodContextID],[RunUntilOffsetMinutes] " + g.crlf +
                     "        ,[Int1Label],[Int1Value],[Int2Label],[Int2Value],[Int3Label],[Int3Value],[Int4Label],[Int4Value],[Int5Label],[Int5Value] " + g.crlf +
                     "        ,[Dec1Label],[Dec1Value],[Dec2Label],[Dec2Value],[Dec3Label],[Dec3Value],[Dec4Label],[Dec4Value],[Dec5Label],[Dec5Value]) " + g.crlf +
                     " VALUES (@PeriodHistoryID, @ScheduledTaskID, @TaskName, @ProcessorName, @ProcessorVersion, @ProcessorTypeID " + g.crlf +
                     "        ,@ExecutionStatusID, @RunStatusID, @RunCode, @NoWorkDone, @StartDateTime, @EndDateTime " + g.crlf +
                     "        ,@RunHost, @RunUser, @Message, @RunUntilTask, @RunUntilPeriodContextID, @RunUntilOffsetMinutes " + g.crlf +
                     "        ,@Int1Label, @Int1Value, @Int2Label, @Int2Value, @Int3Label, @Int3Value, @Int4Label, @Int4Value, @Int5Label, @Int5Value " + g.crlf +
                     "        ,@Dec1Label, @Dec1Value, @Dec2Label, @Dec2Value, @Dec3Label, @Dec3Value, @Dec4Label, @Dec4Value, @Dec5Label, @Dec5Value) " + g.crlf +
                     " SELECT SCOPE_IDENTITY(); ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.Parameters.AddWithValue("@PeriodHistoryID", (object)rh.PeriodHistoryId ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@ScheduledTaskID", rh.ScheduledTaskId);
          cmd.Parameters.AddWithValue("@TaskName", rh.TaskName);
          cmd.Parameters.AddWithValue("@ProcessorName", (object)rh.ProcessorName ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@ProcessorVersion", (object)rh.ProcessorVersion ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@ProcessorTypeID", rh.ProcessorType.ToInt32());
          cmd.Parameters.AddWithValue("@ExecutionStatusID", rh.ExecutionStatus.ToInt32());
          cmd.Parameters.AddWithValue("@RunStatusID", rh.RunStatus.ToInt32());
          cmd.Parameters.AddWithValue("@RunCode", rh.RunCode);
          cmd.Parameters.AddWithValue("@NoWorkDone", rh.NoWorkDone);
          cmd.Parameters.AddWithValue("@StartDateTime", (object)rh.StartDateTime ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@EndDateTime", (object)rh.EndDateTime ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunHost", (object)rh.RunHost ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunUser", (object)rh.RunUser ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Message", (object)rh.Message ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@RunUntilTask", rh.RunUntilTask);
          cmd.Parameters.AddWithValue("@RunUntilPeriodContextID", rh.RunUntilPeriod.ToInt32());
          cmd.Parameters.AddWithValue("@RunUntilOffsetMinutes", (object)rh.RunUntilOffsetMinutes ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int1Label", (object)rh.RunStats.Int1Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int1Value", (object)rh.RunStats.Int1Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int2Label", (object)rh.RunStats.Int2Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int2Value", (object)rh.RunStats.Int2Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int3Label", (object)rh.RunStats.Int3Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int3Value", (object)rh.RunStats.Int3Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int4Label", (object)rh.RunStats.Int4Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int4Value", (object)rh.RunStats.Int4Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int5Label", (object)rh.RunStats.Int5Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int5Value", (object)rh.RunStats.Int5Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec1Label", (object)rh.RunStats.Dec1Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec1Value", (object)rh.RunStats.Dec1Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec2Label", (object)rh.RunStats.Dec2Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec2Value", (object)rh.RunStats.Dec2Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec3Label", (object)rh.RunStats.Dec3Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec3Value", (object)rh.RunStats.Dec3Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec4Label", (object)rh.RunStats.Dec4Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec4Value", (object)rh.RunStats.Dec4Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec5Label", (object)rh.RunStats.Dec5Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec5Value", (object)rh.RunStats.Dec5Value ?? DBNull.Value);

          runId = cmd.ExecuteScalar().ToInt32();
        }

        return runId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to insert RunHistory in TaskScheduling Database", ex);
      }
    }

    public void UpdateRunHistory(MOD.RunHistory rh)
    {
      try
      {
        EnsureConnection();

        string sql = "DECLARE @PeriodHistoryID int " + g.crlf +
                     "UPDATE [TaskScheduling].[dbo].[RunHistory] " + g.crlf +
                     " SET @PeriodHistoryID = [PeriodHistoryID], " + g.crlf +
                     " [ExecutionStatusID] = @ExecutionStatusID, " + g.crlf +
                     " [RunStatusID] = @RunStatusID, " + g.crlf +
                     " [RunCode] = @RunCode, " + g.crlf +
                     " [NoWorkDone] = @NoWorkDone, " + g.crlf +
                     " [StartDateTime] = @StartDateTime, " + g.crlf +
                     " [EndDateTime] = @EndDateTime, " + g.crlf +
                     " [Message] = @Message, " + g.crlf +
                     " [Int1Label] = @Int1Label, [Int1Value] = @Int1Value, " + g.crlf +
                     " [Int2Label] = @Int2Label, [Int2Value] = @Int2Value, " + g.crlf +
                     " [Int3Label] = @Int3Label, [Int3Value] = @Int3Value, " + g.crlf +
                     " [Int4Label] = @Int4Label, [Int4Value] = @Int4Value, " + g.crlf +
                     " [Int5Label] = @Int5Label, [Int5Value] = @Int5Value, " + g.crlf +
                     " [Dec1Label] = @Dec1Label, [Dec1Value] = @Dec1Value, " + g.crlf +
                     " [Dec2Label] = @Dec2Label, [Dec2Value] = @Dec2Value, " + g.crlf +
                     " [Dec3Label] = @Dec3Label, [Dec3Value] = @Dec3Value, " + g.crlf +
                     " [Dec4Label] = @Dec4Label, [Dec4Value] = @Dec4Value, " + g.crlf +
                     " [Dec5Label] = @Dec5Label, [Dec5Value] = @Dec5Value " + g.crlf +
                     " WHERE [RunID] = " + rh.RunId + g.crlf +

                     " IF @NoWorkDone = 0 " + g.crlf +
                     " BEGIN " + g.crlf +
                     "   UPDATE [TaskScheduling].[dbo].[PeriodHistory] " + g.crlf +
                     "   SET RunForPeriod = 1 " + g.crlf +
                     "   WHERE [PeriodID] = @PeriodHistoryID " + g.crlf +
                     " END " +
                     " SELECT @PeriodHistoryID ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.Parameters.AddWithValue("@ExecutionStatusID", rh.ExecutionStatus.ToInt32());
          cmd.Parameters.AddWithValue("@RunStatusID", rh.RunStatus.ToInt32());
          cmd.Parameters.AddWithValue("@RunCode", rh.RunCode);
          cmd.Parameters.AddWithValue("@NoWorkDone", rh.NoWorkDone);
          cmd.Parameters.AddWithValue("@StartDateTime", (object)rh.StartDateTime ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@EndDateTime", (object)rh.EndDateTime ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Message", (object)rh.Message ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int1Label", (object)rh.RunStats.Int1Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int1Value", (object)rh.RunStats.Int1Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int2Label", (object)rh.RunStats.Int2Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int2Value", (object)rh.RunStats.Int2Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int3Label", (object)rh.RunStats.Int3Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int3Value", (object)rh.RunStats.Int3Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int4Label", (object)rh.RunStats.Int4Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int4Value", (object)rh.RunStats.Int4Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int5Label", (object)rh.RunStats.Int5Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Int5Value", (object)rh.RunStats.Int5Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec1Label", (object)rh.RunStats.Dec1Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec1Value", (object)rh.RunStats.Dec1Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec2Label", (object)rh.RunStats.Dec2Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec2Value", (object)rh.RunStats.Dec2Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec3Label", (object)rh.RunStats.Dec3Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec3Value", (object)rh.RunStats.Dec3Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec4Label", (object)rh.RunStats.Dec4Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec4Value", (object)rh.RunStats.Dec4Value ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec5Label", (object)rh.RunStats.Dec5Label ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Dec5Value", (object)rh.RunStats.Dec5Value ?? DBNull.Value);

          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to update RunHistory with RunID '" + rh.RunId + "' in TaskScheduling Database", ex);
      }
    }

    public List<MOD.RunHistory> GetRunHistoryForTask(int taskId)
    {
      try
      {
        EnsureConnection();

        var runHistoryList = new List<MOD.RunHistory>();

        string sql = " SELECT [RunID] " + g.crlf +
                     " ,[ScheduledTaskID] " + g.crlf +
                     " ,[TaskName] " + g.crlf +
                     " ,[ProcessorName] " + g.crlf +
                     " ,[ProcessorVersion] " + g.crlf +
                     " ,[ProcessorTypeID] " + g.crlf +
                     " ,[ExecutionStatusID] " + g.crlf +
                     " ,[RunStatusID] " + g.crlf +
                     " ,[RunCode] " + g.crlf +
                     " ,[NoWorkDone] " + g.crlf +
                     " ,[StartDateTime] " + g.crlf +
                     " ,[EndDateTime] " + g.crlf +
                     " ,[RunHost] " + g.crlf +
                     " ,[RunUser] " + g.crlf +
                     " ,[Message] " + g.crlf +
                     " ,[RunUntilTask] " + g.crlf +
                     " ,[RunUntilPeriodContextID] " + g.crlf +
                     " ,[RunUntilOffsetMinutes] " + g.crlf +
                     " ,[Int1Label], [Int1Value] " + g.crlf +
                     " ,[Int2Label], [Int2Value] " + g.crlf +
                     " ,[Int3Label], [Int3Value] " + g.crlf +
                     " ,[Int4Label], [Int4Value] " + g.crlf +
                     " ,[Int5Label], [Int5Value] " + g.crlf +
                     " ,[Dec1Label], [Dec1Value] " + g.crlf +
                     " ,[Dec2Label], [Dec2Value] " + g.crlf +
                     " ,[Dec3Label], [Dec3Value] " + g.crlf +
                     " ,[Dec4Label], [Dec4Value] " + g.crlf +
                     " ,[Dec5Label], [Dec5Value] " + g.crlf +
                     " FROM [TaskScheduling].[dbo].[RunHistory] " + g.crlf +
                     " WHERE [ScheduledTaskID] = " + taskId;

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var runHistory = new MOD.RunHistory();

            runHistory.RunId = reader["RunID"].DbToInt32().Value;
            runHistory.ScheduledTaskId = reader["ScheduledTaskId"].DbToInt32().Value;
            runHistory.TaskName = reader["TaskName"].DbToString();
            runHistory.ProcessorName = reader["ProcessorName"].DbToString();
            runHistory.ProcessorVersion = reader["ProcessorVersion"].DbToString();
            runHistory.ProcessorType = reader["ProcessorTypeId"].DbToInt32().ToEnum<MOD.ProcessorType>(MOD.ProcessorType.NotSet);
            runHistory.ExecutionStatus = reader["ExecutionStatusId"].DbToInt32().ToEnum<MOD.ExecutionStatus>(MOD.ExecutionStatus.Initiated);
            runHistory.RunStatus = reader["RunStatusID"].DbToInt32().ToEnum<MOD.RunStatus>(MOD.RunStatus.Initiated);
            runHistory.RunCode = reader["RunCode"].DbToInt32().Value;
            runHistory.NoWorkDone = reader["NoWorkDone"].DbToBoolean().Value;
            runHistory.StartDateTime = reader["StartDateTime"].DbToDateTime();
            runHistory.EndDateTime = reader["EndDateTime"].DbToDateTime();
            runHistory.RunHost = reader["RunHost"].DbToString();
            runHistory.RunUser = reader["RunUser"].DbToString();
            runHistory.Message = reader["Message"].DbToString();
            runHistory.RunUntilTask = reader["RunUntilTask"].DbToBoolean().Value;
            runHistory.RunUntilPeriod = reader["RunUntilPeriodContextID"].DbToInt32().ToEnum<MOD.Period>(MOD.Period.NotSet);
            runHistory.RunUntilOffsetMinutes = reader["RunUntilOffsetMinutes"].DbToInt32();
            runHistory.RunStats.Int1Label = reader["Int1Label"].DbToString();
            runHistory.RunStats.Int1Value = reader["Int1Value"].DbToInt32();
            runHistory.RunStats.Int2Label = reader["Int2Label"].DbToString();
            runHistory.RunStats.Int2Value = reader["Int2Value"].DbToInt32();
            runHistory.RunStats.Int3Label = reader["Int3Label"].DbToString();
            runHistory.RunStats.Int3Value = reader["Int3Value"].DbToInt32();
            runHistory.RunStats.Int4Label = reader["Int4Label"].DbToString();
            runHistory.RunStats.Int4Value = reader["Int4Value"].DbToInt32();
            runHistory.RunStats.Int5Label = reader["Int5Label"].DbToString();
            runHistory.RunStats.Int5Value = reader["Int5Value"].DbToInt32();
            runHistory.RunStats.Dec1Label = reader["Dec1Label"].DbToString();
            runHistory.RunStats.Dec1Value = reader["Dec1Value"].DbToDecimal();
            runHistory.RunStats.Dec2Label = reader["Dec2Label"].DbToString();
            runHistory.RunStats.Dec2Value = reader["Dec2Value"].DbToDecimal();
            runHistory.RunStats.Dec3Label = reader["Dec3Label"].DbToString();
            runHistory.RunStats.Dec3Value = reader["Dec3Value"].DbToDecimal();
            runHistory.RunStats.Dec4Label = reader["Dec4Label"].DbToString();
            runHistory.RunStats.Dec4Value = reader["Dec4Value"].DbToDecimal();
            runHistory.RunStats.Dec5Label = reader["Dec5Label"].DbToString();
            runHistory.RunStats.Dec5Value = reader["Dec5Value"].DbToDecimal();

            runHistoryList.Add(runHistory);
          }

          reader.Close();
        }
        return runHistoryList;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get RunHistory for TaskID '" + taskId + "'.", ex);
      }
    }

    public Dictionary<string, DateTime> GetLastSuccessfulRun(List<MOD.ScheduledTask> scheduledTasks)
    {
      var lastSuccessfulRuns = new Dictionary<string, DateTime>();
      try
      {
        EnsureConnection();

        List<int> taskIds = scheduledTasks.Select(t => t.ScheduledTaskId).ToList();
        string commaDelimitedTaskIds = String.Join(",", taskIds);

        string sql = " SELECT [TaskName], MAX([StartDateTime]) as 'StartDateTime'" + g.crlf +
                     " FROM [TaskScheduling].[dbo].[RunHistory] " + g.crlf +
                     " WHERE [NoWorkDone] = 0 " + g.crlf +
                     " AND [RunStatusID] = 3 " + g.crlf +
                     " AND [ScheduledTaskID] IN(" + commaDelimitedTaskIds + ")" + g.crlf +
                     " GROUP BY [TaskName] ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            string taskName = reader["TaskName"].DbToString();
            DateTime lastRunTime = reader["StartDateTime"].DbToDateTime().Value;

            lastSuccessfulRuns.Add(taskName, lastRunTime);
          }

          reader.Close();
        }

        return lastSuccessfulRuns;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while getting last successful runs for each task.", ex);
      }
    }

    public List<string> GetMaps()
    {
      List<string> mapList = new List<string>();
      SqlTransaction trans = null;
      try
      {
        EnsureConnection();
        trans = _conn.BeginTransaction();

        string sql =  "SELECT ParameterValue AS MapName " + g.crlf +
                      "FROM TaskScheduling.dbo.ScheduledTaskParameters " + g.crlf +
                      "WHERE ParameterName = 'MapName' " + g.crlf +
                      "GROUP BY ParameterValue";
        using (var cmd = new SqlCommand(sql,_conn,trans))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            string mapName = reader["MapName"].DbToString();
            mapList.Add(mapName);
          }
          reader.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a list of map names from the database.", ex);
      }

      return mapList;
    }

    public string GetTaskName(string mapName)
    {
      try
      {
        EnsureConnection();

        string sql =  " SELECT TaskName " + g.crlf +
                      " FROM TaskScheduling.dbo.ScheduledTaskParameters STP " + g.crlf +
                      " JOIN TaskScheduling.dbo.ScheduledTasks ST " + g.crlf +
                      " ON STP.ScheduledTaskID = ST.ScheduledTaskID " + g.crlf +
                      " WHERE ParameterName = 'MapName' AND ParameterValue = '" + mapName + "'" + g.crlf +
                      " GROUP BY TaskName";

        using (var cmd = new SqlCommand(sql,_conn))
        {
          int multipleTasks = 0;
          string taskName = String.Empty;
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            taskName = reader["TaskName"].DbToString();
            multipleTasks++;
          }

          if (multipleTasks > 1)
            taskName = "Multiple tasks use this map.";
          reader.Close();
          return taskName;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to check if a task in active.", ex);
      }
    }

    public bool GetIsActive(string mapName)
    {
      try
      {
        EnsureConnection();

        string sql =  " SELECT IsActive " + g.crlf +
                      " FROM TaskScheduling.dbo.ScheduledTasks " + g.crlf +
                      " WHERE ScheduledTaskID = (SELECT ScheduledTaskID FROM TaskScheduling.dbo.ScheduledTaskParameters " + g.crlf +
                      " WHERE ParameterValue = '" + mapName + "')";

        using (var cmd = new SqlCommand(sql,_conn))
        {
          int multipleTasks = 0;
          string taskName = String.Empty;
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();
          bool? b = null;

          while (reader.Read())
          {
            b = reader["IsActive"].DbToBoolean();
            multipleTasks++;
          }

          bool isAct;
          if (b == null)
            isAct = false;
          else
            isAct = b.ToBoolean();
          return isAct;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to check if a task in active.", ex);
      }

    }

    public int DeleteOldOverdueTaskNotificationsSent(int retentionDaysOverdueTaskNotificationsSent, bool isDryRun)
    {
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();
        trans = _conn.BeginTransaction();

        int deletedRows;

        string sql = " SET NOCOUNT OFF " + g.crlf +
                     " DELETE FROM [TaskScheduling].[dbo].[ScheduledTaskParameters] " + g.crlf +
                     "       WHERE [ParameterSetName] = 'OverdueTaskNotificationsSent' " + g.crlf +
                     "         AND DATEDIFF(day, ParameterValue, GetDate()) > " + retentionDaysOverdueTaskNotificationsSent + g.crlf +
                     " SELECT @@ROWCOUNT ";

        using (var cmd = new SqlCommand(sql, _conn, trans))
          deletedRows = cmd.ExecuteScalar().ToInt32();

        if (isDryRun)
          trans.Rollback();
        else
          trans.Commit();

        return deletedRows;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete OverdueTaskNotificationsSent Parameters from the TaskScheduling database.", ex);
      }
    }

    public void TurnOffRunUntilOverride(int taskId)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [TaskScheduling].[dbo].[ScheduledTasks] " + g.crlf +
                     " SET [RunUntilOverride] = 0 " + g.crlf +
                     " WHERE [ScheduledTaskID] =  " + taskId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to turn off RunUntilOverride for ScheduledTaskID '" + taskId + "'.", ex);
      }
    }

    public string GetParameterValue(string parameterName)
    {
      try
      {
        EnsureConnection();

        string sql = "SELECT ParameterValue " + g.crlf +
                     "FROM TaskScheduling.dbo.ScheduledTaskParameters " + g.crlf +
                     "WHERE ParameterName = '" + parameterName + "' ";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          return cmd.ExecuteScalar().DbToString();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the value of the parameter '" + parameterName + "'.", ex);
      }
    }

    private void EnsureConnection()
    {
      try
      {
        if (_conn == null)
          _conn = new SqlConnection(_connectionString);

        if (_conn.State != ConnectionState.Open)
          _conn.Open();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to ensure (or create) the database connection.", ex);
      }
    }

    public void Dispose()
    {
      if (_conn == null)
        return;

      if (_conn.State == ConnectionState.Open)
        _conn.Close();
      _conn.Dispose();
      _conn = null;
    }
  }
}