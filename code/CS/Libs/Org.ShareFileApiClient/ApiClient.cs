using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ShareFile.Api.Models;
using Org.WebApi;
using Org.GS;

namespace Org.ShareFileApiClient
{
  public enum FileProcessingOptions
  {
    None,
    SequenceDuplicates,
    OverwriteFile,
    Skip,
    ThrowException
  }

  public class ApiClient : IDisposable
  {
    public OAuth2Token _oAuth2Token;
    public OAuth2Token OAuth2Token { get { return _oAuth2Token; } }
    public bool _isDryRun;

    public event Action<string> ProgressMessage;

    public ApiClient(bool isDryRun = false)
    {
      _isDryRun = isDryRun;
    }

    public bool Authenticate(AuthParms authParms)
    {
      try
      {
        String uri = string.Format("https://{0}/oauth/token", authParms.HostName);
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "password");
        parameters.Add("client_id", authParms.ClientId);
        parameters.Add("client_secret", authParms.ClientSecret);
        parameters.Add("username", authParms.UserName);
        parameters.Add("password", authParms.Password);

        ArrayList bodyParameters = new ArrayList();
        foreach (KeyValuePair<string, string> kv in parameters)
        {
          bodyParameters.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(kv.Key), HttpUtility.UrlEncode(kv.Value.ToString())));
        }
        string requestBody = String.Join("&", bodyParameters.ToArray());

        HttpWebRequest request = WebRequest.CreateHttp(uri);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        using (var writer = new StreamWriter(request.GetRequestStream()))
        {
          writer.Write(requestBody);
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        JObject token = null;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          token = JObject.Parse(body);
        }

        _oAuth2Token = new OAuth2Token(token);

        return true;
      }
      catch (WebException wx)
      {
        if (wx.Message.ToLower().Contains("the operation has timed out"))
          return false;

        throw new Exception("A web exception occurred attempting to authenticate the ApiClient.", wx);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to authenticate the ApiClient.", ex);
      }
    }

    public void AddAuthorizationHeader(HttpWebRequest request, OAuth2Token token)
    {
      request.Headers.Add(string.Format("Authorization: Bearer {0}", token.AccessToken));
    }

    public string GetHostname(OAuth2Token token)
    {
      return string.Format("{0}.sf-api.com", token.Subdomain);
    }

    public TaskResult GetFolderById(string id, string targetFolder = "")
    {
      var taskResult = new TaskResult("GetFolderById");

      try
      {
        string uri = String.Empty;
        if (targetFolder.IsNotBlank())
          uri = string.Format("https://{0}/sf/v3/Items({1})/ByPath?path=/{2}", GetHostname(_oAuth2Token), id, targetFolder);
        else
          uri = string.Format("https://{0}/sf/v3/Items({1})", GetHostname(_oAuth2Token), id);

        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // check the status code... if not 200 then it will probably throw an exception... 
        //Console.WriteLine(response.StatusCode);

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          // comment out after debugging
          string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(body).ToString();
          JObject jo = JObject.Parse(body);

          ShareFile.Api.Models.Item folder = jo.ToObject<ShareFile.Api.Models.Item>();
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Object = folder; 
          taskResult.Data = jsonFmt;
          taskResult.EndDateTime = DateTime.Now;
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 172;
        taskResult.Message = "An exception occurred attempting to get a folder relative to an item identified by an ID.";
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public TaskResult GetFolderContents(string rootFolderId)
    {
      var taskResult = new TaskResult("GetFolderContents");

      try
      {
        string uri = string.Format("https://{0}/sf/v3/Items({1})/Children?includeDeleted=false", GetHostname(_oAuth2Token), rootFolderId);

        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(body).ToString();

          JObject jo = JObject.Parse(body);
          JArray ja = (JArray) jo["value"];
          var items = ja.ToObject<List<JObject>>();
          var files = new SortedList<string, ShareFile.Api.Models.File>();

          foreach (var fileObject in items)
          {
            var file = GetFileFromObject((JContainer)fileObject);
            if (file != null)
            {
              files.Add(file.Id, file);
            }
          }

          taskResult.EndDateTime = DateTime.Now;
          taskResult.Object = files.Values.ToList<ShareFile.Api.Models.File>();
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Data = jsonFmt;
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 150;
        taskResult.Message = "An exception occurred attempting to get the contents of the remote folder identified by ItemId '" + rootFolderId + "'.";
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public TaskResult ClearFolderContents(string rootFolderId)
    {
      var taskResult = new TaskResult("ClearFolderContents");

      try
      {
        SendMessageToHost("Retrieving files for deletion...");

        string uri = string.Format("https://{0}/sf/v3/Items({1})/Children?includeDeleted=false", GetHostname(_oAuth2Token), rootFolderId);

        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(body).ToString();

          JObject jo = JObject.Parse(body);
          JArray ja = (JArray)jo["value"];
          var items = ja.ToObject<List<JObject>>();
          var files = new List<ShareFile.Api.Models.File>();

          foreach (var fileObject in items)
          {
            var file = GetFileFromObject((JContainer)fileObject);
            if (file != null)
              files.Add(file);
          }

          int filesToDelete = 0;
          foreach (var file in files)
          {
            if (file.__type == "ShareFile.Api.Models.File")
              filesToDelete++;
          }

          SendMessageToHost("Files to delete: " + filesToDelete.ToString() + "."); 

          foreach (var file in files)
          {
            if (file.__type == "ShareFile.Api.Models.File")
            {
              SendMessageToHost("Deleting file '" + file.Name + "'."); 
              var deleteTaskResult = DeleteItem(_oAuth2Token, file.Id);
            }
          }

          taskResult.Object = files;
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Data = jsonFmt;
          taskResult.EndDateTime = DateTime.Now;
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 151;
        taskResult.Message = "An exception occurred attempting to clear the remote folder identified by ItemId '" + rootFolderId + "'.";
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public TaskResult DeleteRemoteFile(string rootFolderId, string remoteFileName)
    {
      var taskResult = new TaskResult("DeleteRemoteFile");

      try
      {
        string uri = string.Format("https://{0}/sf/v3/Items({1})/Children?includeDeleted=false", GetHostname(_oAuth2Token), rootFolderId);
        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(body).ToString();

          JObject jo = JObject.Parse(body);
          JArray ja = (JArray)jo["value"];
          var items = ja.ToObject<List<JObject>>();
          var files = new List<ShareFile.Api.Models.File>();

          foreach (var fileObject in items)
          {
            var file = GetFileFromObject((JContainer)fileObject);
            if (file != null && file.__type == "ShareFile.Api.Models.File")
            {
              if (remoteFileName.ToLower() == file.Name.ToLower())
                files.Add(file);
            }
          }

          int filesToDelete = files.Count;
          
          foreach (var file in files)
          {
            if (file.__type == "ShareFile.Api.Models.File")
            {
              SendMessageToHost("Deleting file '" + file.Name + "'.");
              var deleteTaskResult = DeleteItem(_oAuth2Token, file.Id);
            }
          }

          taskResult.Object = files;
          if (filesToDelete == 0)
          {
            taskResult.TaskResultStatus = TaskResultStatus.Warning;
            taskResult.Message = "Remote file + " + remoteFileName + " - does not exist.";
          }
          else
          {
            taskResult.TaskResultStatus = TaskResultStatus.Success;
          }

          taskResult.Data = jsonFmt;
          taskResult.EndDateTime = DateTime.Now;
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 152;
        taskResult.Message = "An exception occurred attempting to delete remote file named '" + remoteFileName + "' " + 
                             "from folder identified by ItemId '" + rootFolderId + "'.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    private ShareFile.Api.Models.File GetFileFromObject(Newtonsoft.Json.Linq.JContainer jc)
    {
      var file = new ShareFile.Api.Models.File();

      var oDataType = jc["odata.type"] as Newtonsoft.Json.Linq.JValue;
      if (oDataType != null)
        file.__type = oDataType.Value.ToString();

      var hashToken = jc["Hash"] as Newtonsoft.Json.Linq.JValue;
      if (hashToken != null)
        file.Hash = hashToken.Value.ToString();

      var virusStatus = jc["VirusStatus"] as Newtonsoft.Json.Linq.JValue;
      if (virusStatus != null)
        file.VirusStatus = g.ToEnum<ShareFile.Api.Models.FileVirusStatus>(virusStatus.Value.ToString(), FileVirusStatus.NotScanned); 

      var name = jc["Name"] as Newtonsoft.Json.Linq.JValue;
      if (name != null)
        file.Name = name.Value.ToString();

      var fileName = jc["FileName"] as Newtonsoft.Json.Linq.JValue;
      if (fileName != null)
        file.FileName = fileName.Value.ToString();

      var creationDate = jc["CreationDate"] as Newtonsoft.Json.Linq.JValue;
      if (creationDate != null)
        file.CreationDate = creationDate.ToString().ToDateTime();

      var expirationDate = jc["ExpirationDate"] as Newtonsoft.Json.Linq.JValue;
      if (expirationDate != null)
        file.ExpirationDate = expirationDate.ToString().ToDateTime();

      var description = jc["Description"] as Newtonsoft.Json.Linq.JValue;
      if (description != null)
        file.Description = description.Value.ToString();

      var creatorFirstName = jc["CreatorFirstName"] as Newtonsoft.Json.Linq.JValue;
      if (creatorFirstName != null)
        file.CreatorFirstName = creatorFirstName.Value.ToString();

      var creatorLastName = jc["CreatorLastName"] as Newtonsoft.Json.Linq.JValue;
      if (creatorLastName != null)
        file.CreatorLastName = creatorLastName.Value.ToString();

      var expirationDays = jc["ExpirationDays"] as Newtonsoft.Json.Linq.JValue;
      if (expirationDays != null)
        file.ExpirationDays = expirationDays.Value.ToInt32();

      var fileSizeBytes = jc["FileSizeBytes"] as Newtonsoft.Json.Linq.JValue;
      if (fileSizeBytes != null)
        file.FileSizeBytes = fileSizeBytes.Value.ToInt32();

      var creatorNameShort = jc["CreatorNameShort"] as Newtonsoft.Json.Linq.JValue;
      if (creatorNameShort != null)
        file.CreatorNameShort = creatorNameShort.Value.ToString();

      var id = jc["Id"] as Newtonsoft.Json.Linq.JValue;
      if (id != null)
        file.Id = id.Value.ToString();

      return file;
    }

    public TaskResult UploadFile(string parentId, string localPath, bool allowDuplicates, string taskName)
    {
      var taskResult = new TaskResult(taskName);
      taskResult.Data = localPath;

      try
      {
        if (!allowDuplicates)
        {
          if (FileExists(parentId, localPath))
          {
            string fileName = Path.GetFileName(localPath); 

            taskResult.TaskResultStatus = TaskResultStatus.Failed;
            taskResult.Message = "File name '" + fileName + "' already exists in remote folder and duplicate names are not allowed per configuration.";
            taskResult.Code = 153;
            taskResult.EndDateTime = DateTime.Now;
            return taskResult;
          }
        }

        if (_isDryRun)
          return taskResult.Success();

        String uri = string.Format("https://{0}/sf/v3/Items({1})/Upload", GetHostname(_oAuth2Token), parentId);
        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();

          JObject uploadConfig = JObject.Parse(body);
          string chunkUri = (string)uploadConfig["ChunkUri"];
          if (chunkUri != null)
          {
            UploadMultiPartFile("File1", new FileInfo(localPath), chunkUri);
            taskResult.EndDateTime = DateTime.Now;
            taskResult.TaskResultStatus = TaskResultStatus.Success;
            taskResult.Message = "File '" + localPath + "' successfully uploaded to ShareFile into folder '" + parentId + "'.";
          }
          // what if chunkUri is null???  is this an error?
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 154;
        taskResult.Message = "An exception occurred attempting to upload the file '" + localPath + " under the folder with id '" + parentId + "'.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public bool FileExists(string parentId, string localPath)
    {
      try
      {
        string fileName = Path.GetFileName(localPath);
        string uri = string.Format("https://{0}/sf/v3/Items({1})/ByPath?path=/{2}", GetHostname(_oAuth2Token), parentId, fileName);
        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
          String body = reader.ReadToEnd();
          string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(body).ToString();
          JObject jo = JObject.Parse(body);
          var file = GetFileFromObject((JContainer)jo);
          return true;
        }
      }
      catch (Exception ex)
      {
        if (ex.Message.ToLower().Contains("(404) not found"))
          return false;

        throw new Exception("An exception occurred attempting to determine whether a file to be uploaded already exists.", ex); 
      }
    }

    public void UploadMultiPartFile(string parameterName, FileInfo file, string uploadUrl)
    {
      try
      {
        string boundaryGuid = "upload-" + Guid.NewGuid().ToString("n");
        string contentType = "multipart/form-data; boundary=" + boundaryGuid;

        MemoryStream ms = new MemoryStream();
        byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundaryGuid + "\r\n");

        // Write MIME header
        ms.Write(boundaryBytes, 2, boundaryBytes.Length - 2);
        string header = String.Format(@"Content-Disposition: form-data; name=""{0}""; filename=""{1}""" +
            "\r\nContent-Type: application/octet-stream\r\n\r\n", parameterName, file.Name);
        byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
        ms.Write(headerBytes, 0, headerBytes.Length);

        // Load the file into the byte array
        using (FileStream source = file.OpenRead())
        {
          byte[] buffer = new byte[1024 * 1024];
          int bytesRead;

          while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
          {
            ms.Write(buffer, 0, bytesRead);
          }
        }

        // Write MIME footer
        boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundaryGuid + "--\r\n");
        ms.Write(boundaryBytes, 0, boundaryBytes.Length);

        byte[] postBytes = ms.ToArray();
        ms.Close();

        HttpWebRequest request = WebRequest.CreateHttp(uploadUrl);
        request.Timeout = 1000 * 60; // 60 seconds
        request.Method = "POST";
        request.ContentType = contentType;
        request.ContentLength = postBytes.Length;
        request.Credentials = CredentialCache.DefaultCredentials;

        using (Stream postStream = request.GetRequestStream())
        {
          int chunkSize = 48 * 1024;
          int remaining = postBytes.Length;
          int offset = 0;

          do
          {
            if (chunkSize > remaining) { chunkSize = remaining; }
            postStream.Write(postBytes, offset, chunkSize);

            remaining -= chunkSize;
            offset += chunkSize;

          } while (remaining > 0);

          postStream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode != HttpStatusCode.OK)
        {
          throw new Exception("HTTP status code other than 'OK' was returned when attempting to upload a file in the UploadMultiPartFile method - " +
                              "status code is '" + response.StatusCode.ToString() + " (" + response.StatusDescription + ")."); 
        }
        response.Close();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the UploadMultiPartFile method while attempting to upload a file.", ex);
      }
    }
    
    public TaskResult DeleteItem(OAuth2Token token, string itemId)
    {
      var taskResult = new TaskResult("DeleteItem");

      try
      {
        if (!_isDryRun)
        {
          String uri = string.Format("https://{0}/sf/v3/Items({1})", GetHostname(token), itemId);

          HttpWebRequest request = WebRequest.CreateHttp(uri);
          AddAuthorizationHeader(request, token);

          request.Method = "DELETE";

          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to delete item with ID = '" + itemId + "'.", ex); 
      }
    }

    public TaskResult DownloadFile(string fileId, string localPath, string archiveFolderId, bool archiveFiles, bool suppressDelete)
    {
      var taskResult = new TaskResult("DownloadFile");
      try
      {
        string uri = string.Format("https://{0}/sf/v3/Items({1})/Download", GetHostname(_oAuth2Token), fileId);
        HttpWebRequest request = WebRequest.CreateHttp(uri);
        AddAuthorizationHeader(request, _oAuth2Token);
        request.AllowAutoRedirect = true;
        
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (var source = new BufferedStream(response.GetResponseStream()))
        {
          using (var target = new FileStream(localPath, FileMode.Create))
          {
            int chunkSize = 1024 * 8;
            byte[] chunk = new byte[chunkSize];
            int len = 0;
            while ((len = source.Read(chunk, 0, chunkSize)) > 0)
            {
              target.Write(chunk, 0, len);
            }
          }
        }

        // until we can figure out how to get the actual CopyItem api working
        // we're re-uploading the just downloaded file into the archive directory
        if (archiveFiles)
        {
          var archiveTaskResult = UploadFile(archiveFolderId, localPath, true, "DownloadFile");
          archiveTaskResult.TaskName = "ArchiveFile";
          taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, archiveTaskResult); 
        }

        if (!suppressDelete)
        {
          var deleteTaskResult = DeleteItem(_oAuth2Token, fileId);
          taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, deleteTaskResult);
        }

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Code = 155;
        taskResult.Message = "An exception occurred attempting to download the file with Id '" + fileId + "'.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    private void SendMessageToHost(string message)
    {
      if (this.ProgressMessage != null)
      {
        ProgressMessage(message);
      }
    }

    public void Dispose()
    {

    }
  }
}
