using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Org.GS.Logging
{
  public class LogRecord
  { 
    public long LogId { get; set; }
    public DateTime LogDateTime { get; set; }
    public LogSeverity SeverityCode { get; set; }
    public string Message { get; set; }
    public int ModuleId { get; set; }
    public int EventCode { get; set; }
    public string SessionId { get; set; }
    public int OrgId { get; set; }
    public int AccountId { get; set; }
    public int EntityId { get; set; }
    public int? RunId { get; set; }
    public string UserName { get; set; }
    public string ClientHost { get; set; }
    public string ClientIp { get; set; }
    public string ClientUser { get; set; }
    public string ClientApplication { get; set; }
    public string ClientApplicationVersion { get; set; }
    public string TransactionName { get; set; }
    public int LogWriteAttempt { get; set; }
    public bool NotificationSent { get; set; }
    public TimeSpan LogWriteWait { get; set; }
    public Exception Exception { get; set; }
    public LogDetailType LogDetailType { get; set; }
    public string LogDetail { get; set; }
    public LogDetailSet LogDetailSet { get; set; }

    public LogRecord()
    {
      Initialize();
    }

    private void Initialize()
    {
      this.LogId = 0;
      this.LogDateTime = DateTime.Now;
      this.SeverityCode = LogSeverity.INFO;
      this.Message = String.Empty;
      this.ModuleId = 0;
      this.EventCode = 0;
      this.SessionId = null; 
      this.OrgId = 0;
      this.AccountId = 0;
      this.EntityId = 0;
      this.RunId = null; 
      this.UserName = String.Empty;
      this.ClientHost = String.Empty;
      this.ClientIp = String.Empty;
      this.ClientUser = String.Empty;
      this.ClientApplication = String.Empty;
      this.ClientApplicationVersion = String.Empty;
      this.TransactionName = String.Empty;
      this.NotificationSent = false;
      this.LogWriteAttempt = 0;
      this.LogWriteWait = TimeSpan.Zero;
      this.Exception = null;
      this.LogDetailType = LogDetailType.NotSet;
      this.LogDetail = String.Empty;
      this.LogDetailSet = new LogDetailSet();
    }

    public void ProcessLogDetail()
    {
      int maxMsgLength = 500;
      int maxDetailLength = 2000;

      if (this.LogDetailSet == null)
        this.LogDetailSet = new LogDetailSet();

      // if there is nothing that needs LogDetail records to be built or inserted - just return.
      if (this.Message.Length <= maxMsgLength && this.LogDetail.IsBlank() && this.LogDetailSet.Count == 0 && this.Exception == null)
        return;

      // set up the set ids for already established LogDetail records.
      List<int> setIdsUsed = new List<int>();

      foreach (var dtl in this.LogDetailSet.Values)
      {
        if (!setIdsUsed.Contains(dtl.SetId))
          setIdsUsed.Add(dtl.SetId);
      }

      LogDetail logDetail = null;
      // if the message overflows the max length
      if (this.Message.Length > maxMsgLength)
      {
        int setId = setIdsUsed.Count == 0 ? 1 : setIdsUsed.Max() + 1;
        setIdsUsed.Add(setId);

        string messageDetail = this.Message.Trim().Substring(maxMsgLength);
        while (messageDetail.Length > maxDetailLength)
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = messageDetail.Substring(0, maxDetailLength);
          messageDetail = messageDetail.Substring(maxDetailLength);
          logDetail.LogDetailType = LogDetailType.MSGX;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
        }

        if (messageDetail.Length > 0)
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = messageDetail;
          logDetail.LogDetailType = LogDetailType.MSGX;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
        }

        this.Message = this.Message.Trim().Substring(0, maxMsgLength);
        logDetail = null;
      }
      else
        this.Message = this.Message.Trim();

      // move LogRecord.Detail data (string) into LogRecord.LogDetailSet objects
      if (this.LogDetail.IsNotBlank())
      {
        int setId = setIdsUsed.Count == 0 ? 1 : setIdsUsed.Max() + 1;
        setIdsUsed.Add(setId);

        string detail = this.LogDetail.Trim();
        while (detail.Length > maxDetailLength)
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = detail.Substring(0, maxDetailLength);
          detail = detail.Substring(maxDetailLength);
          logDetail.LogDetailType = LogDetailType.TEXT;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
        }

        if (detail.Length > 0)
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = detail;
          logDetail.LogDetailType = LogDetailType.TEXT;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
        }

        this.LogDetail = String.Empty;
      }

      // move LogRecord.Exception information into LogRecord.LogDetailSet objects
      if (this.Exception != null)
      {
        int setId = setIdsUsed.Count == 0 ? 1 : setIdsUsed.Max() + 1;
        setIdsUsed.Add(setId);

        string exceptionText = this.Exception.ToReport();

        while (exceptionText.Length > maxDetailLength)
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = exceptionText.Substring(0, maxDetailLength);
          logDetail.LogDetailType = LogDetailType.EXCP;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
          exceptionText = exceptionText.Substring(maxDetailLength);
        }

        if (exceptionText.IsNotBlank())
        {
          logDetail = new LogDetail();
          logDetail.SetId = setId;
          logDetail.DetailData = exceptionText;
          logDetail.LogDetailType = LogDetailType.EXCP;
          this.LogDetailSet.Add(this.LogDetailSet.Count, logDetail);
        }
        this.Exception = null; 
      }
    }
        
    public string GetLogEntry()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(this.LogDateTime.ToString("yyyyMMdd-HHmmss.fff") + " " +
      this.SeverityCode.ToString() + " " +
      this.OrgId.ToString("00000000") + " " +
      this.AccountId.ToString("0000000") + " " +
      this.EntityId.ToString("00000000") + " " +
      this.ModuleId.ToString("0000") + " " +
      this.EventCode.ToString("00000") + " " + 
      this.LogWriteAttempt.ToString("00") + " " + 
			this.LogWriteWait.ToString(@"ss\.ffff") + " " +
      this.Message + g.crlf);

      if (this.Exception != null)
      {
          sb.Append(g.BlankString(9) + "EXCEPTION:" + this.Exception.ToReport() + g.crlf);
      }

      if (this.LogDetailSet.Count > 0)
      {
        foreach (LogDetail ld in this.LogDetailSet.Values)
        {
            sb.Append(g.BlankString(9) + "DT" + ld.LogDetailId.ToString() + "(" + ld.LogDetailType.ToString() + ") " + ld.DetailData + g.crlf); 
        }
      }
      string logEntryString = sb.ToString();
      return logEntryString;
    }
  }
}
