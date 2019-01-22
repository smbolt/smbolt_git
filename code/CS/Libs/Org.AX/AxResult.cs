using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.AX
{
  public class AxResult
  {
    public bool HasRun { get; set; }
    public bool IsDryRun { get; set; }
    public string ReportBase { get; set; }
    public List<WebSite> WebSitesAffected { get; set; }
    public List<WinService> WinServicesAffected { get; set; }
    public List<AppPool> AppPoolsAffected { get; set; }
    public List<OSFile> FilesCopied { get; set; }
    public List<OSFile> FilesMoved { get; set; }
    public List<OSFile> FilesDeleted { get; set; }
    private AxAction _parent { get; set; }

    public string AxActionReport { get { return Get_AxActionReport(); } }

    public AxResult(AxAction parent)
    {
      _parent = parent;
      this.HasRun = false;
      this.IsDryRun = parent.IsDryRun;
      this.WebSitesAffected = new List<WebSite>();
      this.WinServicesAffected = new List<WinService>();
      this.AppPoolsAffected = new List<AppPool>();
      this.FilesCopied = new List<OSFile>();
      this.FilesMoved = new List<OSFile>();
      this.FilesDeleted = new List<OSFile>();
    }

    private string Get_AxActionReport()
    {
      try
      {
        StringBuilder sb = new StringBuilder();

        sb.Append(this.ReportBase + g.crlf);

        //switch (_parent.AxionType)
        //{
        //  case AxionType.StartWinService:
        //  case AxionType.StopWinService:
        //  case AxionType.PauseWinService:
        //  case AxionType.ResumeWinService:
        //  case AxionType.GetWinServiceStatus:
        //    WinService winService = WinServicesAffected.FirstOrDefault();
        //    axionReport.Append("The Windows Service '" + winService.Name + "' is " + winService.WinServiceStatus);
        //    break;

        //  case AxionType.StartWebSite:
        //  case AxionType.StopWebSite:
        //  case AxionType.GetWebSiteStatus:
        //    WebSite webSite = WebSitesAffected.FirstOrDefault();
        //    axionReport.Append("The Web Site '" + webSite.Name + "' is " + webSite.WebSiteStatus);
        //    break;
        //}

        if (this.FilesDeleted.Count > 0)
        {
          sb.Append("The following files have been deleted:" + g.crlf);
          sb.Append("  FROM: '" + _parent.Tgt + "'" + g.crlf);
          foreach (var osFile in this.FilesDeleted)
            sb.Append("  '" + osFile.FileName + "'" + g.crlf);
          sb.Append(g.crlf);
        }

        if (this.FilesMoved.Count > 0)
        {
          sb.Append("The following files have been moved" + g.crlf);
          sb.Append("  FROM: '" + _parent.Src + "'" + g.crlf);
          sb.Append("  TO:   '" + _parent.Tgt + "'" + g.crlf);
          foreach (var osFile in this.FilesMoved)
            sb.Append("  '" + osFile.FileName + "'" + g.crlf);
          sb.Append(g.crlf);
        }

        if (this.FilesCopied.Count > 0)
        {
          sb.Append("The following files have been copied:" + g.crlf);
          sb.Append("  FROM: '" + _parent.Src + "'" + g.crlf);
          sb.Append("  TO:   '" + _parent.Tgt + "'" + g.crlf);
          foreach (var osFile in this.FilesCopied)
            sb.Append("  '" + osFile.FileName + "'" + g.crlf);
          sb.Append(g.crlf);
        }

        //if (this.WinServicesStarted.Count > 0)
        //{
        //  sb.Append("The following Windows Services have been started:" + g.crlf);
        //  foreach (var winService in this.WinServicesStarted)
        //    sb.Append("  " + winService.MachineName + "." + winService.Name + g.crlf);
        //  sb.Append(g.crlf);
        //}

        //if (this.WinServicesStopped.Count > 0)
        //{
        //  sb.Append("The following Windows Services have been stopped:" + g.crlf);
        //  foreach (var winService in this.WinServicesStopped)
        //    sb.Append("  " + winService.MachineName + "." + winService.Name + g.crlf);
        //  sb.Append(g.crlf);
        //}

        //if (this.WebSitesStarted.Count > 0)
        //{
        //  sb.Append("The following Web Sites have been started:" + g.crlf);
        //  foreach (var webSite in this.WebSitesStarted)
        //    sb.Append("  " + webSite.Name + g.crlf);
        //  sb.Append(g.crlf);
        //}

        //if (this.WebSitesStopped.Count > 0)
        //{
        //  sb.Append("The following Web Sites have been stopped:" + g.crlf);
        //  foreach (var webSite in this.WebSitesStopped)
        //    sb.Append("  " + webSite.Name + g.crlf);
        //  sb.Append(g.crlf);
        //}

        return sb.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("An Exception occurred while attempting to create the AxActionReport.", ex);
      }
    }

  }
}
