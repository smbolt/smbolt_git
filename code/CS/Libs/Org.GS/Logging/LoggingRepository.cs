using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.GS.Logging
{
  public class LoggingRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;

    public LoggingRepository(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception("'" + configDbSpec + "' is not ready to connect.");
      _connectionString = _configDbSpec.ConnectionString;
    }

    public Dictionary<int, string> GetAppLogEntities()
    {
      try
      {
        EnsureConnection();

        var appLogEntities = new Dictionary<int, string>();

        string sql = " SELECT [EntityID] " + g.crlf +
                           " ,[EntityName] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogEntity]";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogEntities;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            int entityId = r["EntityID"].DbToInt32().Value;
            string entityName = r["EntityName"].DbToString();
            appLogEntities.Add(entityId, entityName);
          }
        }
        return appLogEntities;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogEntities.", ex);
      }
    }

    public string GetAppLogEntity(int id)
    {
      try
      {
        EnsureConnection();

        string entity;

        string sql = " SELECT [EntityName] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogEntity] " + g.crlf +
                     " WHERE [EntityID] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          entity = cmd.ExecuteScalar().ToString();

        return entity;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogEntity with ID " + id + ".", ex);
      }
    }

    public bool InsertAppLogEntity(int id, string description)
    {
      try
      {
        EnsureConnection();

        bool idInUse;

        string sql = " IF exists (SELECT * from [Logging].[dbo].[AppLogEntity] WHERE EntityID = " + id + ") " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     SELECT 'true' " + g.crlf +
                     "   END " + g.crlf +
                     " ELSE " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     INSERT INTO [Logging].[dbo].[AppLogEntity] " + g.crlf +
                     "       VALUES(" + id + ", '" + description + "') " + g.crlf +
                     "     SELECT 'false' " + g.crlf +
                     "   END ";

        using (var cmd = new SqlCommand(sql, _conn))
          idInUse = cmd.ExecuteScalar().DbToBoolean().Value;

        return idInUse;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to insert new Entity into AppLogEntity.", ex);
      }
    }

    public void UpdateAppLogEntity(int id, string description)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Logging].[dbo].[AppLogEntity] " + g.crlf +
                     "    SET [EntityName] = '" + description + "' " + g.crlf +
                     "  WHERE [EntityID] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to update EntityID '" + id + "' in AppLogEntity.", ex);
      }
    }

    public Dictionary<int, string> GetAppLogModules()
    {
      try
      {
        EnsureConnection();

        var appLogModules = new Dictionary<int, string>();

        string sql = " SELECT [ModuleID] " + g.crlf +
                           " ,[Description] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogModule]";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogModules;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            int moduleId = r["ModuleID"].DbToInt32().Value;
            string description = r["Description"].DbToString();
            appLogModules.Add(moduleId, description);
          }

          return appLogModules;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogModules.", ex);
      }
    }

    public string GetAppLogModule(int id)
    {
      try
      {
        EnsureConnection();

        string module;

        string sql = " SELECT [Description] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogModule] " + g.crlf +
                     " WHERE [ModuleID] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          module = cmd.ExecuteScalar().ToString();

        return module;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogModule with ID " + id + ".", ex);
      }
    }

    public bool InsertAppLogModule(int id, string description)
    {
      try
      {
        EnsureConnection();

        bool idInUse;

        string sql = " IF exists (SELECT * from [Logging].[dbo].[AppLogModule] WHERE ModuleID = " + id + ") " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     SELECT 'true' " + g.crlf +
                     "   END " + g.crlf +
                     " ELSE " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     INSERT INTO [Logging].[dbo].[AppLogModule] " + g.crlf +
                     "       VALUES(" + id + ", '" + description + "') " + g.crlf +
                     "     SELECT 'false' " + g.crlf +
                     "   END ";

        using (var cmd = new SqlCommand(sql, _conn))
          idInUse = cmd.ExecuteScalar().DbToBoolean().Value;

        return idInUse;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to insert new Module into AppLogModule.", ex);
      }
    }

    public void UpdateAppLogModule(int id, string description)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Logging].[dbo].[AppLogModule] " + g.crlf +
                     "    SET [Description] = '" + description + "' " + g.crlf +
                     "  WHERE [ModuleID] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to update ModuleID '" + id + "' in AppLogModule.", ex);
      }
    }

    public Dictionary<int, string> GetAppLogEvents()
    {
      try
      {
        EnsureConnection();

        var appLogEvents = new Dictionary<int, string>();

        string sql = " SELECT [EventCode] " + g.crlf +
                           " ,[Description] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogEvent]";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogEvents;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            int eventCode = r["EventCode"].DbToInt32().Value;
            string description = r["Description"].DbToString();
            appLogEvents.Add(eventCode, description);
          }

          return appLogEvents;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogEvents.", ex);
      }
    }

    public string GetAppLogEvent(int id)
    {
      try
      {
        EnsureConnection();

        string appLogEvent;

        string sql = " SELECT [Description] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogEvent] " + g.crlf +
                     " WHERE [EventCode] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          appLogEvent = cmd.ExecuteScalar().ToString();

        return appLogEvent;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogEvent with Code " + id + ".", ex);
      }
    }

    public bool InsertAppLogEvent(int id, string description)
    {
      try
      {
        EnsureConnection();

        bool idInUse;

        string sql = " IF exists (SELECT * from [Logging].[dbo].[AppLogEvent] WHERE EventCode = " + id + ") " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     SELECT 'true' " + g.crlf +
                     "   END " + g.crlf +
                     " ELSE " + g.crlf +
                     "   BEGIN " + g.crlf +
                     "     INSERT INTO [Logging].[dbo].[AppLogEvent] " + g.crlf +
                     "       VALUES(" + id + ", '" + description + "') " + g.crlf +
                     "     SELECT 'false' " + g.crlf +
                     "   END ";

        using (var cmd = new SqlCommand(sql, _conn))
          idInUse = cmd.ExecuteScalar().DbToBoolean().Value;

        return idInUse;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to insert new Event into AppLogEvent.", ex);
      }
    }

    public void UpdateAppLogEvent(int id, string description)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Logging].[dbo].[AppLogEvent] " + g.crlf +
                     "    SET [Description] = '" + description + "' " + g.crlf +
                     "  WHERE [EventCode] = " + id;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to update EventCode '" + id + "' in AppLogEvent.", ex);
      }
    }

    public Dictionary<string, string> GetAppLogSeverities()
    {
      try
      {
        EnsureConnection();

        var appLogSeverityCodes = new Dictionary<string, string>();

        string sql = " SELECT [SeverityCode] " + g.crlf +
                           " ,[Description] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogSeverity]";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogSeverityCodes;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            string severityCode = r["SeverityCode"].DbToString();
            string description = r["Description"].DbToString();
            appLogSeverityCodes.Add(severityCode, description);
          }

          return appLogSeverityCodes;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogSeverityCodes.", ex);
      }
    }

    public AppLogSet GetAppLogSet(string recordCount, DateTime? fromDt, DateTime? toDt, string severity, string message, string modules, string events, string entities, bool descending)
    {
      try
      {
        EnsureConnection();

        var appLogSet = new AppLogSet();
        string CTEs = String.Empty;
        string whereClause = String.Empty;
        string joinClause = String.Empty;

        switch (severity)
        {
          case "ALL":
            break;

          case "ALL BUT INFO":
            whereClause = " WHERE SeverityCode NOT LIKE 'INFO'" + g.crlf;
            break;

          default:
            whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " SeverityCode = '" + severity + "'" + g.crlf;
            break;
        }

        if (fromDt.HasValue && toDt.HasValue)
        {
          whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " LogDateTime BETWEEN '" + fromDt.Value.ToString("MM/dd/yyyy HH:mm:00") +
                                                                                    "' AND '" + toDt.Value.ToString("MM/dd/yyyy HH:mm:00") + "' " + g.crlf;
        }

        if (modules.IsNotBlank())
          whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " ModuleID IN(" + modules + ")" + g.crlf;
        if (events.IsNotBlank())
          whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " EventCode IN(" + events + ")" + g.crlf;
        if (entities.IsNotBlank())
          whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " EntityID IN(" + entities + ")" + g.crlf;
        if (message.IsNotBlank())
        {
          CTEs = " WITH Details AS " + g.crlf +
                 " ( SELECT d1.LogID, " + g.crlf +
                 "          STUFF(( SELECT LogDetail " + g.crlf +
                 "                  FROM Logging.dbo.AppLogDetail d2 " + g.crlf +
                 "                  WHERE d2.LogID = d1.LogID " + g.crlf +
                 "                  ORDER BY LogDetail " + g.crlf +
                 "                  FOR XML PATH, TYPE).value('.[1]','nvarchar(max)'), 1, 0, '') AS DetailMessage " + g.crlf +
                 "   FROM Logging.dbo.AppLogDetail d1 " + g.crlf +
                 "   GROUP BY LogID " + g.crlf +
                 " ), " + g.crlf +
                 " FullDetails AS " + g.crlf +
                 " ( SELECT al.LogID,  " + g.crlf +
                 "          al.[Message] + CASE WHEN d.DetailMessage is null THEN '' ELSE d.DetailMessage END AS [FullMessage] " + g.crlf +
                 "   FROM Logging.dbo.AppLog al " + g.crlf +
                 "   LEFT JOIN Details d " + g.crlf +
                 "          ON al.LogID = d.LogID " + g.crlf +
                 " ) " + g.crlf;
          whereClause += (whereClause.IsBlank() ? "WHERE" : "AND") + " FullMessage LIKE '%" + message + "%'" + g.crlf;
          joinClause = " INNER JOIN FullDetails fd " + g.crlf +
                       "         ON al.LogID = fd.LogID " + g.crlf;
        }

        string sql = CTEs +
                     " SELECT TOP " + recordCount + g.crlf +
                           "  al.[LogID] " + g.crlf +
                           " ,[LogDateTime] " + g.crlf +
                           " ,[SeverityCode] " + g.crlf +
                           " ,[Message] " + g.crlf +
                           " ,[ModuleID] " + g.crlf +
                           " ,[EventCode] " + g.crlf +
                           " ,[EntityID] " + g.crlf +
                           " ,[RunID] " + g.crlf +
                           " ,[UserName] " + g.crlf +
                           " ,[ClientHost] " + g.crlf +
                           " ,[ClientIP] " + g.crlf +
                           " ,[ClientUser] " + g.crlf +
                           " ,[ClientApplication] " + g.crlf +
                           " ,[ClientApplicationVersion] " + g.crlf +
                           " ,[TransactionName] " + g.crlf +
                           " ,[NotificationSent] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLog] al " + g.crlf +
                       joinClause +
                       whereClause +
                     " ORDER BY al.LogID " + (descending ? "DESC" : "ASC");

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            AppLog al = new AppLog();
            al.LogId = r["LogID"].DbToInt32().Value;
            al.LogDateTime = r["LogDateTime"].DbToDateTime().Value;
            al.SeverityCode = r["SeverityCode"].DbToString().ToEnum<LogSeverity>(LogSeverity.INFO);
            al.Message = r["Message"].DbToString();
            al.ModuleId = r["ModuleID"].DbToInt32();
            al.EventCode = r["EventCode"].DbToInt32();
            al.EntityId = r["EntityID"].DbToInt32();
            al.RunId = r["RunID"].DbToInt32();
            al.UserName = r["UserName"].DbToString();
            al.ClientHost = r["ClientHost"].DbToString();
            al.ClientIp = r["ClientIP"].DbToString();
            al.ClientUser = r["ClientUser"].DbToString();
            al.ClientApplication = r["ClientApplication"].DbToString();
            al.ClientApplicationVersion = r["ClientApplicationVersion"].DbToString();
            al.TransactionName = r["TransactionName"].DbToString();
            al.NotificationSent = r["NotificationSent"].DbToBoolean().Value;
            appLogSet.Add(al);
          } 
        }

        GetAppLogDetailSet(appLogSet);

        return appLogSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve AppLogs.", ex);
      }
    }

    private AppLogSet GetAppLogDetailSet(AppLogSet appLogSet)
    {
      try
      {
        EnsureConnection();

        string logIds = "";
        foreach (var appLog in appLogSet)
        {
          if (logIds.IsNotBlank())
            logIds += ",";
          logIds += appLog.LogId;
        }

        if (logIds.IsBlank())
          return appLogSet;

        string sql = " SELECT [LogDetailID] " + g.crlf +
                           " ,[LogID] " + g.crlf +
                           " ,[DetailType] " + g.crlf +
                           " ,[SetID] " + g.crlf +
                           " ,[LogDetail] " + g.crlf +
                     " FROM [Logging].[dbo].[AppLogDetail] " + g.crlf +
                     " WHERE LogID IN (" + logIds + ")";
        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return appLogSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            AppLogDetail ald = new AppLogDetail();
            ald.LogDetailId = r["LogDetailID"].DbToInt32().Value;
            ald.LogId = r["LogID"].DbToInt32().Value;
            ald.LogDetailType = r["DetailType"].DbToString().ToEnum(LogDetailType.NotSet);
            ald.SetId = r["SetId"].DbToInt32().Value;
            ald.LogDetail = r["LogDetail"].DbToString();
            appLogSet.First(appLog => appLog.LogId == ald.LogId).AppLogDetailSet.Add(ald);
          }
        }
        return appLogSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to retrieve AppLogDetails.", ex);
      }
    }

    public AppLogSet GetAppLogSetFromRunId(int runId)
    {
      try
      {
        EnsureConnection();

        AppLogSet appLogSet = new AppLogSet();

        string sql = " SELECT [LogID] " + g.crlf +
                           " ,[LogDateTime] " + g.crlf +
                           " ,[SeverityCode] " + g.crlf +
                           " ,[Message] " + g.crlf +
                           " ,[ModuleID] " + g.crlf +
                           " ,[EventCode] " + g.crlf +
                           " ,[EntityID] " + g.crlf +
                           " ,[UserName] " + g.crlf +
                           " ,[RunID] " + g.crlf +
                           " ,[ClientHost] " + g.crlf +
                           " ,[ClientIP] " + g.crlf +
                           " ,[ClientUser] " + g.crlf +
                           " ,[ClientApplication] " + g.crlf +
                           " ,[ClientApplicationVersion] " + g.crlf +
                           " ,[TransactionName] " + g.crlf +
                           " ,[NotificationSent] " + g.crlf +
                    " FROM [Logging].[dbo].[AppLog] " + g.crlf +
                    " WHERE [RunID] = " + runId + g.crlf +
                    " ORDER BY [LogDateTime] DESC";

        using (SqlCommand cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var log = new AppLog();

            log.LogId = reader["LogID"].DbToInt32().Value;
            log.LogDateTime = reader["LogDateTime"].DbToDateTime().Value;
            log.SeverityCode = reader["SeverityCode"].DbToString().ToEnum<LogSeverity>(LogSeverity.INFO);
            log.Message = reader["Message"].DbToString();
            log.ModuleId = reader["ModuleID"].DbToInt32();
            log.EventCode = reader["EventCode"].DbToInt32();
            log.EntityId = reader["EntityID"].DbToInt32();
            log.RunId = reader["RunID"].DbToInt32();
            log.UserName = reader["UserName"].DbToString();
            log.ClientHost = reader["ClientHost"].DbToString();
            log.ClientIp = reader["ClientIP"].DbToString();
            log.ClientUser = reader["ClientUser"].DbToString();
            log.ClientApplication = reader["ClientApplication"].DbToString();
            log.ClientApplicationVersion = reader["ClientApplicationVersion"].DbToString();
            log.TransactionName = reader["TransactionName"].DbToString();
            log.NotificationSent = reader["NotificationSent"].DbToBoolean().Value;
            appLogSet.Add(log);
          }
          reader.Close();
        }

        GetAppLogDetailSet(appLogSet);

        return appLogSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when attempting to retried AppLogs related to RunID '" + runId + "'.", ex);
      }
    }

    public int DeleteOldLogRecords(int retentionDaysAUDT, int retentionDaysDIAG, int retentionDaysINFO, int retentionDaysMAJR, int retentionDaysMINR, int retentionDaysSEVR,
                                    int retentionDaysTRAC, int retentionDaysWARN, string modulesToExlude, string eventsToExlude, string entitiesToExclude, bool isDryRun)
    {
      SqlTransaction trans = null;

      try
      {
        EnsureConnection();
        trans = _conn.BeginTransaction();

        int deletedRows;

        string modulesSql = String.Empty;
        string eventsSql = String.Empty;
        string entitiesSql = String.Empty;

        if (modulesToExlude.IsNotBlank())
          modulesSql = "   AND ModuleID NOT IN (" + modulesToExlude + ") " + g.crlf;
        if (eventsToExlude.IsNotBlank())
          eventsSql = "   AND EventCode NOT IN (" + eventsToExlude + ") " + g.crlf;
        if (entitiesToExclude.IsNotBlank())
          entitiesSql = "   AND EntityID NOT IN (" + entitiesToExclude + ") " + g.crlf;


        string sql = " SET NOCOUNT OFF " + g.crlf +
                     " DECLARE @Now DateTime = GetDate(); " + g.crlf +
                     " DELETE FROM [Logging].[dbo].[AppLog] " + g.crlf +
                           " WHERE ((SeverityCode LIKE 'AUDT' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysAUDT + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'DIAG' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysDIAG + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'INFO' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysINFO + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'MAJR' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysMAJR + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'MINR' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysMINR + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'SEVR' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysSEVR + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'TRAC' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysTRAC + ") " + g.crlf +
                           "    OR (SeverityCode LIKE 'WARN' AND DATEDIFF(day, LogDateTime, @Now) > " + retentionDaysWARN + ")) " + g.crlf +
                           modulesSql + eventsSql + entitiesSql + g.crlf +
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
        throw new Exception("An exception occurred attempting to delete old Log Records from the Logging database.", ex);
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
