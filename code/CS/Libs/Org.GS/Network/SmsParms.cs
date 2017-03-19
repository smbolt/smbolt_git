using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS.Logging;

namespace Org.GS
{
  public class CdyneSmsParms
  {
    public bool UseHardCodedParms { get; set; }
    public string AlertID  { get; set; }
    public string AlertCode { get; set; }
    public string IPAddress { get; set; }
    public string AppName { get; set; }
    public string ClassName { get; set; }
    public string MethodName { get; set; }
    public string LocationCode { get; set; }
    public string CustomerID { get; set; }
    public string LicenseKey { get; set; }
    public string Endpoint { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public LogSeverity Severity { get; set; }
    public bool SuppressSmsSend { get; set; }

    public CdyneSmsParms()
    {
      this.UseHardCodedParms = false;
      this.AlertID = String.Empty;
      this.AlertCode = String.Empty;
      this.IPAddress = String.Empty;
      this.AppName = String.Empty;
      this.ClassName = String.Empty;
      this.MethodName = String.Empty;
      this.LocationCode = String.Empty;
      this.CustomerID = String.Empty;
      this.LicenseKey = String.Empty;
      this.Endpoint = String.Empty;
      this.PhoneNumber = String.Empty;
      this.Message = String.Empty;
      this.Severity = LogSeverity.INFO;
      this.SuppressSmsSend = false;
    }
  }
}
