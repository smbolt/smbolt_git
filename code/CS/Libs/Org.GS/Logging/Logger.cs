using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Configuration;

namespace Org.GS.Logging
{
  public class Logger : IDisposable
  {
    private static LogRecordSet _logRecordSet = new LogRecordSet();
    private static ConfigDbSpec _configDbSpec;
    private static SqlConnection _conn;

    public int ModuleId { get; set; }
    public int EntityId { get; set; }
    public string ClientHost { get; set; }
    public string ClientIp { get; set; }
    public string ClientUser { get; set; }
    public string ClientApplication { get; set; }
    public string ClientApplicationVersion { get; set; }
    public string TransactionName { get; set; }
    public int? RunId { get; set; }

    private object lockObject;

    private string _logFilePath = String.Empty;
    public string LogFilePath
    {
      get { return _logFilePath; }
    }

    public Logger()
    {
      this.lockObject = new object();
      if (LogContext.LogConfigDbSpec != null)
        _configDbSpec = LogContext.LogConfigDbSpec;

      ClearClientProperties();
      this.ModuleId = 0;
      this.EntityId = 0;
      this.RunId = null; 
    }

    public static void SetConfigDbSpec(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
    }

    public void Log(string message)
    {
      var logRecord = new LogRecord();
      logRecord.Message = message;
      Log(logRecord); 
    }

    public void Log(string message, string clientHost, string clientIp, string clientUser)
    {
      var l = new LogRecord();
      l.Message = message;
      l.ClientHost = clientHost;
      l.ClientIp = clientIp;
      l.ClientUser = clientUser;
      Log(l); 
    }

    public void Log(string message, string sessionId)
    {
      var l = new LogRecord();
      l.Message = message;
      l.SessionId = sessionId;
      Log(l);
    }

    public void Log(string message, int eventCode)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      Log(l);
    }

    public void Log(string message, int eventCode, int entityId)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      l.EntityId = entityId;
      Log(l);
    }

    public void Log(string message, int eventCode, int entityId, Exception ex)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      l.EntityId = entityId;
      l.Exception = ex;
      Log(l);
    }

    public void Log(string message, int eventCode, string clientHost, string clientIp, string clientUser)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      l.ClientHost = clientHost;
      l.ClientIp = clientIp;
      l.ClientUser = clientUser;
      Log(l);
    }

    public void Log(string message, string sessionId, int eventCode)
    {
      var l = new LogRecord();
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      Log(l);
    }

    public void Log(string message, int eventCode, int orgId, int accountId, int entityId)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.EntityId = entityId;
      Log(l);
    }

    public void Log(string message, string sessionId, int eventCode, int orgId, int accountId, int entityId)
    {
      var l = new LogRecord();
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.EntityId = entityId;
      Log(l);
    }

    public void Log(string message, int eventCode, Exception ex)
    {
      var l = new LogRecord();
      l.Message = message;
      l.EventCode = eventCode;
      l.Exception = ex;
      Log(l);
    }

    public void Log(string message, string sessionId, int eventCode, Exception ex)
    {
      var l = new LogRecord();
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.Exception = ex;
      Log(l);
    }

    public void Log(TaskResult r)
    {
      if (r.IsLogged)
        return;

      var l = new LogRecord();
      l.SeverityCode = r.LogSeverity;
      l.Message = r.Message;
      l.SessionId = r.SessionId;
      l.ModuleId = r.ModuleId;
      l.EventCode = r.Code;
      l.OrgId = r.OrgId;
      l.AccountId = r.AccountId;
      l.EntityId = r.EntityId;
      l.UserName = r.UserName;
      l.RunId = r.OriginalTaskRequest != null ? r.OriginalTaskRequest.RunId : null;
      l.Exception = r.Exception;
      l.LogDetailType = LogDetailType.TEXT;
      l.LogDetail = r.FullErrorDetail;
      Log(l);

      foreach (var childTaskResult in r.TaskResultSet.Values)
        Log(childTaskResult);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode, int entityId)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      l.EntityId = entityId;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode, int entityId, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      l.EntityId = entityId;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode, int orgId, int accountId)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, string sessionId, int eventCode, int orgId, int accountId)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, string sessionId, int eventCode, int orgId, int accountId, int entityId)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.EntityId = entityId;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, string sessionId, int eventCode, int orgId, int accountId, int entityId, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.EntityId = entityId;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode, int orgId, int accountId, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, string sessionId, int eventCode, int orgId, int accountId, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.OrgId = orgId;
      l.AccountId = accountId;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, int eventCode, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.EventCode = eventCode;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogSeverity severityCode, string message, string sessionId, int eventCode, Exception ex)
    {
      var l = new LogRecord();
      l.SeverityCode = severityCode;
      l.Message = message;
      l.SessionId = sessionId;
      l.EventCode = eventCode;
      l.Exception = ex;
      Log(l);
    }

    public void Log(LogRecord logRecord)
    {
      if (g.SuppressLogging)
        return;

      if (_configDbSpec == null && LogContext.LogConfigDbSpec != null)
      {
        if (LogContext.LogConfigDbSpec != null)
        {
          _configDbSpec = LogContext.LogConfigDbSpec;
        }
        else
        {
          if (LogContext.ConfigLogSpec != null && LogContext.ConfigLogSpec.LogDbSpecPrefix.IsNotBlank())
          {
            LogContext.LogConfigDbSpec = g.GetDbSpec(LogContext.ConfigLogSpec.LogDbSpecPrefix);
            _configDbSpec = LogContext.LogConfigDbSpec; 
          }
        }
      }

      if (logRecord.ModuleId == 0 && this.ModuleId != 0)
        logRecord.ModuleId = this.ModuleId;

      if (logRecord.EntityId == 0 && this.EntityId != 0)
        logRecord.EntityId = this.EntityId;

      if (logRecord.UserName.IsBlank())
        logRecord.UserName = g.SystemInfo.DomainAndUser;

      if (logRecord.ClientHost.IsBlank() && this.ClientHost.IsNotBlank())
        logRecord.ClientHost = this.ClientHost;

      if (logRecord.ClientIp.IsBlank() && this.ClientIp.IsNotBlank())
        logRecord.ClientIp = this.ClientIp;

      if (logRecord.ClientUser.IsBlank() && this.ClientUser.IsNotBlank())
        logRecord.ClientUser = this.ClientUser;

      if (logRecord.ClientApplication.IsBlank() && this.ClientApplication.IsNotBlank())
        logRecord.ClientApplication = this.ClientApplication;

      if (logRecord.ClientApplicationVersion.IsBlank() && this.ClientApplicationVersion.IsNotBlank())
        logRecord.ClientApplicationVersion = this.ClientApplicationVersion;

      if (logRecord.TransactionName.IsBlank() && this.TransactionName.IsNotBlank())
        logRecord.TransactionName = this.TransactionName;

      if (!logRecord.RunId.HasValue && this.RunId.HasValue)
        logRecord.RunId = this.RunId.Value;

      Task.Factory.StartNew(() =>
      {
        try
        {
          if (LogContext.ConfigLogSpec != null && LogContext.ConfigLogSpec.LogMethod == LogMethod.Database)
            logRecord.ProcessLogDetail();

          switch (LogContext.LogContextState)
          {
            case LogContextState.Normal:
              WriteLog(logRecord);
              break;

            default:
            if (Monitor.TryEnter(_logRecordSet, 1000))
            {
              try
              {
                if (_logRecordSet.Count < 1000)
                  _logRecordSet.Add(logRecord);
              }
              catch (Exception exception)
              {
                string exMessage = exception.Message;
              }
              finally
              {
                Monitor.Exit(_logRecordSet);
              }
            }
            break;
          }
        }
        catch(Exception ex)
        {
          //throw new Exception("An exception occurred while attempting to write a log record.", ex); 
        }
      });
    }
    
    private void WriteLog(LogRecord logRecord)
    {
      if (g.SuppressLogging)
        return;

      try
      {
        switch (LogContext.ConfigLogSpec.LogMethod)
        {
          case LogMethod.Database:
            WriteLogToDatabase(logRecord);
            break;

          case LogMethod.LocalFile:
            WriteLogToFile(logRecord);
            break;

          default:
            return;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to write the log record.", ex);
      }
    }


    private static object WriteLogToDatabase_LockObject = new object();
    private void WriteLogToDatabase(LogRecord l)
    {
      SqlTransaction trans = null;
      bool transBegun = false;

      if (Monitor.TryEnter(WriteLogToDatabase_LockObject, 2000))
      {
        try
        {          
          EnsureConnection();

          if (l.LogDetailSet.Count > 0)
          {
            trans = _conn.BeginTransaction();
            transBegun = true;
          }

          long logId = -1;

          // Insert AppLog
          string _storedproc = "dbo.ins_AppLog";

          using (SqlCommand cmd = new SqlCommand(_storedproc, _conn, trans))
          {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LogDateTime", l.LogDateTime);
            cmd.Parameters.AddWithValue("@SeverityCode", l.SeverityCode.ToString());
            cmd.Parameters.AddWithValue("@Message", l.Message);
            cmd.Parameters.AddWithValue("@ModuleId", l.ModuleId);
            cmd.Parameters.AddWithValue("@EventCode", l.EventCode);
            cmd.Parameters.AddWithValue("@EntityId", l.EntityId);
            cmd.Parameters.AddWithValue("@RunId", l.RunId);
            cmd.Parameters.AddWithValue("@UserName", l.UserName.IsBlank() ? null : l.UserName.Trim()); 
            cmd.Parameters.AddWithValue("@ClientHost", l.ClientHost.IsBlank() ? null : l.ClientHost.Trim());
            cmd.Parameters.AddWithValue("@ClientIp", l.ClientIp.IsBlank() ? null : l.ClientIp.Trim());
            cmd.Parameters.AddWithValue("@ClientUser", l.ClientUser.IsBlank() ? null : l.ClientUser.Trim()); 
            cmd.Parameters.AddWithValue("@ClientApplication", l.ClientApplication.IsBlank() ? null : l.ClientApplication.Trim()); 
            cmd.Parameters.AddWithValue("@ClientApplicationVersion", l.ClientApplicationVersion.IsBlank() ? null : l.ClientApplicationVersion.Trim());
            cmd.Parameters.AddWithValue("@TransactionName", l.TransactionName.IsBlank() ? null : l.TransactionName.Trim()); 
            cmd.Parameters.AddWithValue("@NotificationSent", l.NotificationSent ? 1 : 0); 
            SqlParameter logIdParm = new SqlParameter("@LogId", SqlDbType.BigInt);
            logIdParm.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(logIdParm);
            cmd.ExecuteNonQuery();
            logId = (long)logIdParm.Value;
          }

          if (transBegun)
          {
            long logDetailId = -1;

            foreach (var logDetail in l.LogDetailSet.Values)
            {
              if (logDetail.LogDetailType == LogDetailType.NotSet)
                logDetail.LogDetailType = LogDetailType.TEXT;

              _storedproc = "dbo.ins_AppLogDetail";

              using (SqlCommand cmd = new SqlCommand(_storedproc, _conn, trans))
              {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@LogId", logId);
                cmd.Parameters.AddWithValue("@DetailType", logDetail.LogDetailType.ToString());
                cmd.Parameters.AddWithValue("@SetId", logDetail.SetId); 
                cmd.Parameters.AddWithValue("@LogDetail", logDetail.DetailData);
                SqlParameter logDetailIdParm = new SqlParameter("@LogDetailId", SqlDbType.BigInt);
                logDetailIdParm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(logDetailIdParm);
                cmd.ExecuteNonQuery();
                logDetailId = (long)logDetailIdParm.Value;
              }
            }

            trans.Commit();
          }
        }
        catch (Exception ex)
        {
          if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          {
            trans.Rollback();
          }
          throw new Exception("An exception occurred attempting to write log record to the database.", ex);
        }
        finally
        {
          Monitor.Exit(WriteLogToDatabase_LockObject);
        }
      }
      else
      {
        throw new Exception("A lock could not be obtained within 2 seconds milliseconds for writing the log record to the database.");
      } 
    }

    private void WriteLogToFile(LogRecord logRecord)
    {
      if (!Directory.Exists(g.LogPath))
        return;  
                  
      DateTime InitialWriteAttemptDT = DateTime.Now;
      string logFullPath = String.Empty;

      if (Monitor.TryEnter(this.lockObject, 1000))
      {
        try
        {
          bool logRecordWritten = false;
          int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;

          int remainingWriteAttempts = 10;
          int attempt = 0;
          string lockDirectory = String.Empty;

          while (remainingWriteAttempts > 0 && !logRecordWritten)
          {
            bool gotToWriteAttempt = false;

            try
            {
              logRecord.LogWriteAttempt = attempt; 
              string logFileName = "app.log";

              switch (LogContext.ConfigLogSpec.LogFileFrequency)
              {
                case LogFileFrequency.Daily:
                  logFileName = DateTime.Now.ToString("yyyyMMdd") + @".log";
                  break;

                case LogFileFrequency.Hourly:
                  logFileName = DateTime.Now.ToString("yyyyMMdd-HH") + @".log";
                  break;
              }

              logFullPath = g.LogPath + @"\" + logFileName;
              _logFilePath = logFullPath; 

              logRecord.LogWriteWait = DateTime.Now - InitialWriteAttemptDT;
              string logFileEntry = FormatLogEntry(logRecord.GetLogEntry()) + g.crlf;
              gotToWriteAttempt = true;
              File.AppendAllText(logFullPath, logFileEntry);
              logRecordWritten = true;

              switch (LogContext.ConfigLogSpec.LogFileSizeManagementMethod)
              {
                case LogFileSizeManagementMethod.TotalBytes:
                  int logFileSizeMax = 10000000;
                  int logFileSizeTrim = 7500000;

                  if (LogContext.ConfigLogSpec.LogFileSizeMax != 0)
                    logFileSizeMax = LogContext.ConfigLogSpec.LogFileSizeMax;

                  if (LogContext.ConfigLogSpec.LogFileSizeTrim != 0)
                    logFileSizeTrim = LogContext.ConfigLogSpec.LogFileSizeTrim;

                  FileInfo logFileInfo = new FileInfo(logFullPath);
                  if (logFileInfo.Length > logFileSizeMax)
                    ShrinkLogFile(logFullPath, logFileSizeMax, logFileSizeTrim);
                  break;

                case LogFileSizeManagementMethod.Aging:
                  if (LogContext.ConfigLogSpec.LogFileSizeManagementAgent != LogFileSizeManagementAgent.Logger)
                    return;

                  int logFileAgeMaxDays = 20;
                  if (LogContext.ConfigLogSpec.LogFileAgeMaxDays != 0)
                    logFileAgeMaxDays = LogContext.ConfigLogSpec.LogFileAgeMaxDays;

                  break;
              }
            }
            catch(Exception ex)
            {
              string message = ex.Message;

              if (!gotToWriteAttempt)
                throw new Exception("An exception occurred during an attempt to write to the log file '" + logFullPath + "'.  Exception message is '" + ex.Message + ".'", ex);

              remainingWriteAttempts--;
              attempt++;
              System.Threading.Thread.Sleep(500);
            }
            finally
            {
            }
          }
        }
        finally
        {
          Monitor.Exit(this.lockObject);
        }
      }
    }

    private string FormatLogEntry(string logEntry)
    {
      try
      {
        while (logEntry.StartsWith(g.crlf))
          logEntry = logEntry.Substring(2);
        return logEntry.Replace(g.crlf, g.crlf + "    ").Trim();
      }
      catch
      {
        return logEntry.Replace(g.crlf, g.crlf + "    ").Trim();
      }
    }

    private void ShrinkLogFile(string logFullPath, int logFileSizeMax, int logFileSizeTrim)
    {
      int bytesRead = 0;

      string tempLogFullPath = Path.GetDirectoryName(logFullPath) + @"\Temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
      File.Copy(logFullPath, tempLogFullPath);
      File.Delete(logFullPath);
      StreamReader sr = File.OpenText(tempLogFullPath);
      StreamWriter sw = File.CreateText(logFullPath);

      int bytesToTrim = logFileSizeMax - logFileSizeTrim;

      while (!sr.EndOfStream)
      {
        string line = sr.ReadLine();
        bytesRead += line.Length;
        if (bytesRead > bytesToTrim)
        {
          line = sr.ReadLine();
          while (!sr.EndOfStream)
            sw.WriteLine(sr.ReadLine());
          break;
        }
      }

      sr.Close();
      sw.Flush();
      sw.Close();
      File.Delete(tempLogFullPath);
    }
 
    public void ClearLogFile()
    {
      if (LogContext.LogContextState != LogContextState.Normal)
        return;

      if (LogContext.ConfigLogSpec.LogMethod != LogMethod.LocalFile)
        return;

      if (!Directory.Exists(g.LogPath))
        return;

      Task.Factory.StartNew(() =>
      {
        DateTime InitialWriteAttemptDT = DateTime.Now;
        string logFullPath = String.Empty;

        lock (this.lockObject)
        {
          bool logFileCleared = false;
          int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;

          int remainingClearAttempts = 10;
          int attempt = 0;
          string lockDirectory = String.Empty;

          while (remainingClearAttempts > 0 && !logFileCleared)
          {
            bool gotToClearAttempt = false;

            try
            {
              string logFileName = "app.log";

              switch (LogContext.ConfigLogSpec.LogFileFrequency)
              {
                case LogFileFrequency.Daily:
                  logFileName = DateTime.Now.ToString("yyyyMMdd") + @".log";
                  break;

                case LogFileFrequency.Hourly:
                  logFileName = DateTime.Now.ToString("yyyyMMdd-HH") + @".log";
                  break;
              }

              logFullPath = g.LogPath + @"\" + logFileName;
              _logFilePath = logFullPath;

              gotToClearAttempt = true;
              File.WriteAllText(logFullPath, String.Empty);
              logFileCleared = true;
            }
            catch (Exception ex)
            {
              string message = ex.Message;

              if (!gotToClearAttempt)
                throw new Exception("An exception occurred during an attempt to write to the log file '" + logFullPath + "'.  Exception message is '" + ex.Message + ".'", ex);

              remainingClearAttempts--;
              attempt++;
              System.Threading.Thread.Sleep(500);
            }
            finally
            {
            }
          }
        }
      });
    }
        
    public void Flush()
    {
      if (LogContext.LogContextState != LogContextState.Normal)
        return;

      if (_logRecordSet == null)
        _logRecordSet = new LogRecordSet();

      if (_logRecordSet.Count == 0)
        return;

      if (Monitor.TryEnter(_logRecordSet, 1000))
      {
        try
        {
          Task.Factory.StartNew(() =>
          {
            foreach (LogRecord l in _logRecordSet)
              Log(l);

            _logRecordSet.Clear();
          });
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
        finally
        {
          Monitor.Exit(_logRecordSet); 
        }
      }
    }

    private LogException BuildLogException(Exception ex)
    {
      if (ex == null)
        return null;

      LogException lex = new LogException();

      lex.Message = ex.Message;
      lex.Source = ex.Source;
      lex.StackTrace = ex.StackTrace;
      lex.InnerLogException = BuildLogException(ex.InnerException);

      return lex;
    }

    private static object EnsureConnection_LockObject = new object();
    public static void EnsureConnection()
    {
      try
      {
        if (_conn != null)
        {
          if (_conn.State == ConnectionState.Open)
            return;
        }

        lock (EnsureConnection_LockObject)
        {
          if (_conn == null)
          {
            _conn = new SqlConnection(_configDbSpec.ConnectionString);
          }

          if (_conn.State != ConnectionState.Open && _conn.State != ConnectionState.Connecting)
            _conn.Open();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to open the logging database connection.", ex); 
      }
    }

    public void ClearClientProperties()
    {
      this.ClientHost = String.Empty;
      this.ClientIp = String.Empty;
      this.ClientUser = String.Empty;
      this.ClientApplication = String.Empty;
      this.ClientApplicationVersion = String.Empty;
      this.TransactionName = String.Empty;
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
