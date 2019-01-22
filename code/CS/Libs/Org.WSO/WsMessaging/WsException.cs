using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO
{
  [XMap(XType = XType.Element)]
  public class WsException
  {
    [XMap]
    public string Message { get; set; }

    [XMap]
    public string Source { get; set; }

    [XMap(XType = XType.Element)]
    public WsTargetSite WsTargetSite { get; set; }

    [XMap]
    public string StackTrace { get; set; }

    [XMap(XType = XType.Element, Name = "WsException")]
    public WsException InnerException { get; set; }

    public WsException()
    {
      this.Message = String.Empty;
      this.Source = String.Empty;
      this.WsTargetSite = new WsTargetSite();
      this.StackTrace = String.Empty;
      this.InnerException = null;
    }

    public WsException(Exception ex)
    {
      this.Message = ex.Message;
      this.Source = ex.Source;
      this.StackTrace = System.Security.SecurityElement.Escape(ex.StackTrace);
      this.WsTargetSite = new WsTargetSite(ex);
      if (ex.InnerException != null)
        this.InnerException = GetInnerException(ex); 
    }

    private WsException GetInnerException(Exception ex)
    {
      WsException innerException = new WsException();

      innerException.Message = ex.Message;
      innerException.Source = ex.Source;
      innerException.StackTrace = System.Security.SecurityElement.Escape(ex.StackTrace);
      innerException.WsTargetSite = new WsTargetSite(ex);
      if (ex.InnerException != null)
        innerException.InnerException = GetInnerException(ex.InnerException); 

      return innerException;
    }

    public string GetExceptionString(WsException ex)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("WsException" + Environment.NewLine +
                "    Message     : " + ex.Message + Environment.NewLine +
                "    Source      : " + ex.Source + Environment.NewLine +
                "    StackTrace  : " + ex.StackTrace + Environment.NewLine + 
                "    TargetSite  : " + Environment.NewLine +
                "        DeclaringType.Assembly.FullName : " + ex.WsTargetSite.DeclaringTypeAssemblyName + Environment.NewLine +
                "        DeclaringType.FullName          : " + ex.WsTargetSite.DeclaringTypeFullName + Environment.NewLine +
                "        DeclaringType.ModuleName        : " + ex.WsTargetSite.DeclaringTypeModuleName + Environment.NewLine +
                "        MemberType                      : " + ex.WsTargetSite.MemberType + Environment.NewLine +
                "        Name                            : " + ex.WsTargetSite.Name + Environment.NewLine);

      if (ex.InnerException != null)
      {
          sb.Append("====InnerException====" + Environment.NewLine);
          sb.Append(GetExceptionString(ex.InnerException));
      }

      return sb.ToString();
    }

    public string ToReport()
    {
      int level = 0; 
      StringBuilder sb = new StringBuilder();

      sb.Append("Exception Level:" + level.ToString() + g.crlf +
                "  Message:" + this.Message + g.crlf +
                "  Source:" + this.Source + g.crlf +
                "  StackTrace:" + this.StackTrace + g.crlf +
                "  TargetSite: " + this.WsTargetSite.ToReport(level) + g.crlf);

      if (this.InnerException != null)
        sb.Append(GetExceptionReport(this.InnerException, level)); 

      return sb.ToString();
    }

    private string GetExceptionReport(WsException ex, int level)
    {
      level++;
      StringBuilder sb = new StringBuilder();

      sb.Append("Exception Level:" + level.ToString() + g.crlf +
                "  Message:" + ex.Message + g.crlf +
                "  Source:" + ex.Source + g.crlf +
                "  StackTrace:" + ex.StackTrace + g.crlf +
                "  TargetSite: " + ex.WsTargetSite.ToReport(level) + g.crlf);

      if (ex.InnerException != null)
        sb.Append(GetExceptionReport(ex.InnerException, level)); 

      return sb.ToString();
    }
  }
}
