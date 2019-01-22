using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Logging;
using Org.GS;

namespace Org.ShareFileApiClient
{
  public class SFFolder : SFBase
  {
    public SFFolderSet SFFolderSet {
      get;
      set;
    }
    public SFFileSet SFFileSet {
      get;
      set;
    }
    public override SFType SFType {
      get {
        return SFType.Folder;
      }
    }

    private static ApiParms _apiParms;
    private static FileManager _fm;
    private static SearchParms _searchParms;
    private static SFStats _sfStats;
    private bool _isDryRun;
    private Logger _logger;

    public SFFolder(ApiParms apiParms, SearchParms searchParms, Logger logger, bool isDryRun = false)
    {
      try
      {
        _apiParms = apiParms;
        _searchParms = searchParms;
        _logger = logger;
        _isDryRun = isDryRun;
        this.ParentFolder = null;
        RootFolder = this;

        _fm = new FileManager(apiParms,_logger, _isDryRun);

        this.SFFolderSet = new SFFolderSet();
        this.SFFileSet = new SFFileSet();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the SFFolder class which includes the creation of the FileManager and authentication to the ShareFile API.", ex);
      }
    }

    public SFFolder(SFFolder parentFolder)
    {
      this.ParentFolder = parentFolder;
      this.SFFolderSet = new SFFolderSet();
      this.SFFileSet = new SFFileSet();
    }

    public void GetAllContent(string folderId)
    {
      var sfFolder = _fm.GetFolderById(folderId, null);

      if (sfFolder == null)
        return;

      this.Id = sfFolder.Id;
      this.Name = sfFolder.Name;
      this.SFFolderSet = sfFolder.SFFolderSet;
      this.SFFileSet = sfFolder.SFFileSet;

      GetAllContent(this);
    }

    public void GetAllContent(SFFolder sfFolder)
    {
      var fc = _fm.GetFolderContent(sfFolder);

      foreach (var kvpFolder in fc.SFFolderSet)
      {
        GetAllContent(kvpFolder.Value);
        sfFolder.SFFolderSet.Add(kvpFolder.Key, kvpFolder.Value);
      }

      foreach (var kvpFile in fc.SFFileSet)
      {
        sfFolder.SFFileSet.Add(kvpFile.Key, kvpFile.Value);
      }
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();
      _sfStats = new SFStats();

      BuildLevelReport(sb, this, _sfStats);



      string report = "Folder Count  " + _sfStats.FolderCount.ToString("###,###,###,##0").PadToJustifyRight(20) + g.crlf +
                      "File Count    " + _sfStats.FileCount.ToString("###,###,###,##0").PadToJustifyRight(20) + g.crlf +
                      "Total Bytes   " + _sfStats.TotalBytes.ToString("###,###,###,##0").PadToJustifyRight(20) + g.crlf2 +
                      sb.ToString();
      return report;
    }

    private void BuildLevelReport(StringBuilder sb, SFFolder sfFolder, SFStats sfStats)
    {
      sb.Append(g.BlankString(sfFolder.Depth * 2) + "(" + sfFolder.Depth.ToString("00") + ") FLDR: " + sfFolder.Name + g.crlf);
      sfStats.FolderCount++;

      foreach (var file in sfFolder.SFFileSet.Values)
      {
        sfStats.FileCount++;
        sfStats.TotalBytes += file.Size;
        sb.Append(g.BlankString(file.Depth * 2) + "(" + file.Depth.ToString("00") + ") FILE: " + file.Name + "     (Size: " + file.Size.ToString("###,###,###,###,###,##0") + ")" + g.crlf);
      }

      foreach (var subFolder in sfFolder.SFFolderSet.Values)
        BuildLevelReport(sb, subFolder, sfStats);
    }
  }
}
