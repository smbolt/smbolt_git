using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Org.GS.Configuration;
using Org.GS;

namespace Org.FTP
{
  public class FtpEngine : IDisposable
  {
    public event Action<FtpProgress> FtpNotification;

    private ConfigFtpSpec _configFtpSpec;

    public FtpEngine(ConfigFtpSpec configFtpSpec)
    {
      _configFtpSpec = configFtpSpec;

      if (_configFtpSpec == null)
        throw new Exception("The ConfigFtpSpec passed to the FtpEngine is null.");

      if (!_configFtpSpec.IsReadyToConnect())
        throw new Exception("The ConfigFtpSpec passed to the FtpEngine is not ready to connect.");
    }

    public FileSystemItem GetDirectoryList(string remoteDirectory = "")
    {
      var folder = new FileSystemItem();
      folder.FileSystemItemType = FileSystemItemType.Folder;
      folder.Name = @"$REMOTE_FTP_ROOT$\" + remoteDirectory;

      try
      {
        int totalBytesTransferred = 0;
        DateTime startTime = DateTime.Now;

        string uri = "ftp://" + _configFtpSpec.FtpServer + "/" + remoteDirectory;
        var serverUri = new Uri(uri);

        var webRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + _configFtpSpec.FtpServer + "/" + remoteDirectory));
        webRequest.Credentials = new NetworkCredential(_configFtpSpec.FtpUserId, _configFtpSpec.FtpPassword);
        webRequest.KeepAlive = _configFtpSpec.FtpKeepAlive;
        webRequest.UseBinary = _configFtpSpec.FtpUseBinary;
        webRequest.Proxy = null;
        webRequest.UsePassive = _configFtpSpec.FtpUsePassive;
        webRequest.Method = "LIST";
        var response = (FtpWebResponse)webRequest.GetResponse();
        var stream = response.GetResponseStream();

        var ms2 = new MemoryStream();

        FtpProgress ftpProgress;

        long totalBytes = 0;
        int Length = 2048;
        Byte[] buffer = new Byte[Length];
        int bytesRead = stream.Read(buffer, 0, Length);
        totalBytesTransferred += bytesRead;

        while (bytesRead > 0)
        {
          ms2.Write(buffer, 0, bytesRead);
          bytesRead = stream.Read(buffer, 0, Length);

          ftpProgress = new FtpProgress();
          ftpProgress.FtpStatus = FtpStatus.Progressing;
          ftpProgress.TotalBytes = totalBytes;
          totalBytesTransferred += bytesRead;
          ftpProgress.StartDT = startTime;
          ftpProgress.Duration = DateTime.Now - ftpProgress.StartDT;
          ftpProgress.BytesTransferred += totalBytesTransferred;
          ftpProgress.ConfigFtpSpec = _configFtpSpec;
          if (FtpNotification != null)
              FtpNotification(ftpProgress);
        }
        stream.Close();

        byte[] buffer2 = new byte[ms2.Length];
        ms2.Position = 0;
        ms2.Read(buffer2, 0, buffer2.Length);
        ms2.Close();
        response.Close();

        string directoryList = new System.Text.ASCIIEncoding().GetString(buffer2);

        var remoteFiles = directoryList.ToFileSystemItemSet();

        foreach (var file in remoteFiles)
        {
          folder.FileSystemItemSet.Add(file); 
          file.Parent = folder;
        }

        if (FtpNotification != null)
        {
          ftpProgress = new FtpProgress();
          ftpProgress.FtpStatus = FtpStatus.Completed;
          totalBytesTransferred += bytesRead;
          ftpProgress.StartDT = startTime;
          ftpProgress.Duration = DateTime.Now - ftpProgress.StartDT;
          ftpProgress.BytesTransferred += totalBytesTransferred;
          FtpNotification(ftpProgress);
        }

        return folder;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process a GetDirectoryList FTP function. " + 
                            "The remote directory being processed is '" + remoteDirectory + "'.", ex); 
      }
    }    

    //public void DownloadFile(FtpParms parms)
    //{
    //  try
    //  {
    //    int totalBytesTransferred = 0;
    //    DateTime startTime = DateTime.Now;

    //    string uri = "ftp://" + parms.FtpServer + "/" + parms.RemoteDirectory + "/" + parms.FileName;
    //    Uri serverUri = new Uri(uri);
    //    if (serverUri.Scheme != Uri.UriSchemeFtp)
    //      return;

    //    // first, get the file size
    //    FtpWebRequest reqFTP;
    //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + parms.FtpServer + "/" + parms.RemoteDirectory + "/" + parms.FileName));
    //    reqFTP.Credentials = new NetworkCredential(parms.UserId, parms.Password);
    //    reqFTP.KeepAlive = parms.KeepAlive;
    //    reqFTP.UseBinary = parms.UseBinary;
    //    reqFTP.Proxy = null;
    //    reqFTP.UsePassive = parms.UsePassive;
    //    reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
    //    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
    //    long totalBytes = response.ContentLength;

    //    // then, get the file
    //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + parms.FtpServer + "/" + parms.RemoteDirectory + "/" + parms.FileName));
    //    reqFTP.Credentials = new NetworkCredential(parms.UserId, parms.Password);
    //    reqFTP.KeepAlive = parms.KeepAlive;
    //    reqFTP.UseBinary = parms.UseBinary;
    //    reqFTP.Proxy = null;
    //    reqFTP.UsePassive = parms.UsePassive;
    //    reqFTP.Method = parms.Method;
    //    response = (FtpWebResponse)reqFTP.GetResponse();
    //    Stream responseStream = response.GetResponseStream();
    //    FileStream writeStream = new FileStream(parms.LocalDirectory + @"\" + parms.FileName, FileMode.Create);

    //    FtpProgress ftpProgress;

    //    int Length = 2048;
    //    Byte[] buffer = new Byte[Length];
    //    int bytesRead = responseStream.Read(buffer, 0, Length);
    //    totalBytesTransferred += bytesRead;

    //    while (bytesRead > 0)
    //    {
    //      writeStream.Write(buffer, 0, bytesRead);
    //      bytesRead = responseStream.Read(buffer, 0, Length);

    //      ftpProgress = new FtpProgress();
    //      ftpProgress.FtpStatus = FtpStatus.Progressing;
    //      ftpProgress.TotalBytes = totalBytes;
    //      totalBytesTransferred += bytesRead;
    //      ftpProgress.StartDT = startTime;
    //      ftpProgress.Duration = DateTime.Now - ftpProgress.StartDT;
    //      ftpProgress.BytesTransferred += totalBytesTransferred;
    //      ftpProgress.ConfigFtpSpec = _configFtpSpec;
    //      FtpNotification(ftpProgress);
    //    }
    //    writeStream.Close();
    //    response.Close();

    //    ftpProgress = new FtpProgress();
    //    ftpProgress.FtpStatus = FtpStatus.Completed;
    //    totalBytesTransferred += bytesRead;
    //    ftpProgress.StartDT = startTime;
    //    ftpProgress.Duration = DateTime.Now - ftpProgress.StartDT;
    //    ftpProgress.BytesTransferred += totalBytesTransferred;
    //    FtpNotification(ftpProgress);
    //  }
    //  catch (WebException wEx)
    //  {
    //    string message = wEx.Message;
    //  }
    //  catch (Exception ex)
    //  {
    //    string message = ex.Message;
    //  }
    //}


    public string TranslateMethodString(string ftpMethod)
    {
      switch (ftpMethod)
      {
        case "APPE": return "Append File";
        case "DELE": return "Delete File";
        case "RETR": return "Download File";
        case "MDTM": return "Get Time Stamp";
        case "SIZE": return "Get File Size";
        case "NLST": return "List Directory";
        case "LIST": return "List Directory Details";
        case "MKD":  return "Make Directory";
        case "PWD":  return "Print Working Directory";
        case "RMD":  return "Remove Directory";
        case "RENAME": return "Rename";
        case "STOR": return "Upload File";
        case "STOU": return "Upload File with Unique Name";
        default: return "Unknown";
      }
    }

    public void Dispose()
    {

    }
  }
}
