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
    private Axion _parent { get; set; }

    public string AxionReport { get { return Get_AxionReport(); } }

    public AxResult(Axion parent)
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

    private string Get_AxionReport()
    {
      StringBuilder axionReport = new StringBuilder();

      axionReport.Append(this.ReportBase + g.crlf);

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
        axionReport.Append("The following files have been deleted:" + g.crlf);
        axionReport.Append("  FROM: '" + _parent.Tgt + "'" + g.crlf);
        foreach (var osFile in this.FilesDeleted)
          axionReport.Append("  '" + osFile.FileName + "'" + g.crlf);
        axionReport.Append(g.crlf);
      }

      if (this.FilesMoved.Count > 0)
      {
        axionReport.Append("The following files have been moved" + g.crlf);
        axionReport.Append("  FROM: '" + _parent.Src + "'" + g.crlf);
        axionReport.Append("  TO:   '" + _parent.Tgt + "'" + g.crlf);
        foreach (var osFile in this.FilesMoved)
          axionReport.Append("  '" + osFile.FileName + "'" + g.crlf);
        axionReport.Append(g.crlf);
      }

      if (this.FilesCopied.Count > 0)
      {
        axionReport.Append("The following files have been copied:" + g.crlf);
        axionReport.Append("  FROM: '" + _parent.Src + "'" + g.crlf);
        axionReport.Append("  TO:   '" + _parent.Tgt + "'" + g.crlf);
        foreach (var osFile in this.FilesCopied)
          axionReport.Append("  '" + osFile.FileName + "'" + g.crlf);
        axionReport.Append(g.crlf);
      }

      //if (this.WinServicesStarted.Count > 0)
      //{
      //  axionReport.Append("The following Windows Services have been started:" + g.crlf);
      //  foreach (var winService in this.WinServicesStarted)
      //    axionReport.Append("  " + winService.MachineName + "." + winService.Name + g.crlf);
      //  axionReport.Append(g.crlf);
      //}

      //if (this.WinServicesStopped.Count > 0)
      //{
      //  axionReport.Append("The following Windows Services have been stopped:" + g.crlf);
      //  foreach (var winService in this.WinServicesStopped)
      //    axionReport.Append("  " + winService.MachineName + "." + winService.Name + g.crlf);
      //  axionReport.Append(g.crlf);
      //}

      //if (this.WebSitesStarted.Count > 0)
      //{
      //  axionReport.Append("The following Web Sites have been started:" + g.crlf);
      //  foreach (var webSite in this.WebSitesStarted)
      //    axionReport.Append("  " + webSite.Name + g.crlf);
      //  axionReport.Append(g.crlf);
      //}

      //if (this.WebSitesStopped.Count > 0)
      //{
      //  axionReport.Append("The following Web Sites have been stopped:" + g.crlf);
      //  foreach (var webSite in this.WebSitesStopped)
      //    axionReport.Append("  " + webSite.Name + g.crlf);
      //  axionReport.Append(g.crlf);
      //}

      return axionReport.ToString();
    }

  }
}
