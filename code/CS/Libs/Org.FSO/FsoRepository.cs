using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;


namespace Org.FSO
{
  public class FsoRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;
    private int _folderID;
    public int currentProjectID;

    public FsoRepository(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;

      if(!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");

      _connectionString = _configDbSpec.ConnectionString;
    }

    public ProjectSet GetProjects()
    {
      EnsureConnection();
      {
      }

      var projectSet = new ProjectSet();

      try
      {
        string sql = "SELECT ProjectID, " + g.crlf +
                     "  Name " + g.crlf +
                     "FROM LAND.Projects " + g.crlf +
                     "ORDER BY Name ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            Project p = new Project();
            p.ProjectID = reader["ProjectID"].ToInt32();
            p.Name = reader["Name"].ToString();
            if (!projectSet.ContainsKey(p.Name))
              projectSet.Add(p.Name, p); 
          }

          reader.Close();

          return projectSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the ProjectSet object.", ex);
      }
    }

    public Project GetProjectByName(string projectName)
    {
      EnsureConnection();

      Project p = null;

      try
      {
        string sql = "SELECT ProjectID, " + g.crlf +
                     "  Name " + g.crlf +
                     "FROM LAND.Projects " + g.crlf +
                     "WHERE Name = '" + projectName + "' ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            p = new Project();
            p.ProjectID = reader["ProjectID"].ToInt32();
            p.Name = reader["Name"].ToString();
          }
          reader.Close();

          return p;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a project by project name '" + projectName + "'.", ex);
      }
    }

    public void InsertNewProject(string projectName)
    {
      EnsureConnection();
      
      try
      {
        string sql = "INSERT INTO LAND.Projects " + g.crlf +
                     "( " + g.crlf +
                     "  Name " + g.crlf +
                     ") " + g.crlf +
                     "VALUES " + g.crlf +
                     "( " + g.crlf +
                     "  '" + projectName.ToLower() + "' " + g.crlf +
                     ") ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new project with project name '" + projectName + "'.", ex);
      }
    }

    public void DeleteProjectByName(string projectName)
    {
      bool transBegun = false;
      SqlTransaction trans = null;

      EnsureConnection();

      try
      {
        var project = GetProjectByName(projectName);

        trans = _conn.BeginTransaction();
        transBegun = true;

        DeleteEntireProject(project.ProjectID, trans);

        trans.Commit();

      }
      catch (Exception ex)
      {
        if (transBegun && _conn != null && _conn.State == ConnectionState.Open && trans != null)
          trans.Rollback();

        throw new Exception("An exception occurred while attempting to delete a project by project name '" + projectName + "'.", ex);
      }
    }

    private void DeleteEntireProject(int projectId, SqlTransaction trans)
    {
      string delSql = "DELETE FROM GPStaging.LAND.Projects WHERE ProjectID = " + projectId;
      using (var cmd = new SqlCommand(delSql, _conn, trans))
      {
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.ExecuteNonQuery();
      }
    }

    private void DeleteFilesForProject(int projectId, SqlTransaction trans)
    {

    }

    private void DeleteFoldersForProject(int projectId, SqlTransaction trans)
    {

    }

    private void DeleteExtensionsForProject(int projectId, SqlTransaction trans)
    {

    }

    private void DeleteTagsForProject(int projectId, SqlTransaction trans)
    {

    }

    private void DeleteDocumentTypesForProject(int projectId, SqlTransaction trans)
    {

    }

    public DocType GetDocTypeByName(string docTypeName)
    {
      EnsureConnection();

      DocType dt = null;

      try
      {
        string docTypeSql = "SELECT DocTypeID, ProjectID, DocName " + g.crlf +
                     "FROM LAND.DocumentType " + g.crlf +
                     "WHERE DocName = '" + docTypeName + "' ";

        using (var cmd = new SqlCommand(docTypeSql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            dt = new DocType();
            dt.DocTypeID = reader["DocTypeID"].ToInt32();
            dt.ProjectID = reader["ProjectID"].ToInt32();
            dt.DocName = reader["DocName"].ToString();
          }
          reader.Close();

          return dt;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a document type by name '" + docTypeName + "'.", ex);
      }
    }

    public void InsertNewDocType(string DocTypeName)
    {
      EnsureConnection();
      
      try
      {
        string sql =  "INSERT INTO GPStaging.LAND.DocumentType(ProjectID, DocName)" + g.crlf +
                      "VALUES(" + currentProjectID + ", '" + DocTypeName.ToLower() + "')";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new document type named '" + DocTypeName + "'.", ex);
      }
    }

    public DocTypeSet PopulateDocTypeGrid()
    {
      if(currentProjectID == null)
        return null;
      if(currentProjectID.Equals(""))
        return null;

      EnsureConnection();

      var dts = new DocTypeSet();
      int i = 1;
      try
      {
        string sql =  "SELECT Dt.DocTypeID, Dt.ProjectID, P.Name, Dt.DocName FROM GPStaging.LAND.DocumentType Dt" + g.crlf +
                      "JOIN GPStaging.LAND.Projects P ON Dt.ProjectID = P.ProjectID" + g.crlf + 
                      "WHERE Dt.ProjectID = " + currentProjectID;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader  = cmd.ExecuteReader();

          while (reader.Read())
          {
            var dt = new DocType();
            dt.DocTypeID = reader["DocTypeID"].ToInt32();
            dt.ProjectID = reader["ProjectID"].ToInt32();
            dt.ProjectName = reader["Name"].ToString();
            dt.DocName = reader["DocName"].ToString();
            dts.Add(i, dt);
            i++;
          }
          reader.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the Doc Type grid.", ex);
      }

      return dts;
    }

    public TagType GetTagTypeByName (string tagTypeName)
    {
      EnsureConnection();

      TagType tt = null;

      try
      {
        string tagTypeSql = "SELECT TagTypeID, ProjectID, TagName " + g.crlf +
                     "FROM LAND.TagType " + g.crlf +
                     "WHERE TagName = '" + tagTypeName + "' ";

        using (var cmd = new SqlCommand(tagTypeSql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            tt = new TagType();
            tt.TagTypeID = reader["TagTypeID"].ToInt32();
            tt.ProjectID = reader["ProjectID"].ToInt32();
            tt.TagName = reader["TagName"].ToString();
          }
          reader.Close();

          return tt;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a document type by name '" + tagTypeName + "'.", ex);
      }
    }

    public void InsertNewTagType(string TagTypeName)
    {
      if(currentProjectID == null)
        return;
      if(currentProjectID.Equals(""))
        return;
      EnsureConnection();
      
      try
      {
        string sql =  "INSERT INTO GPStaging.LAND.TagType(ProjectID, TagName)" + g.crlf +
                      "VALUES(" + currentProjectID + ", '" + TagTypeName.ToLower() + "')";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new document type named '" + TagTypeName + "'.", ex);
      }
    }

    public TagTypeSet PopulateTagTypeGrid()
    {
      if(currentProjectID.Equals(""))
        return null;

      EnsureConnection();

      var tts = new TagTypeSet();
      int i = 1;
      try
      {
        string sql =  "SELECT Tt.TagTypeID, Tt.ProjectID, P.Name, Tt.TagName FROM GPStaging.LAND.TagType Tt" + g.crlf +
                      "JOIN GPStaging.LAND.Projects P ON Tt.ProjectID = P.ProjectID" + g.crlf + 
                      "WHERE Tt.ProjectID = " + currentProjectID;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader  = cmd.ExecuteReader();

          while (reader.Read())
          {
            var tt = new TagType();
            tt.TagTypeID = reader["TagTypeID"].ToInt32();
            tt.ProjectID = reader["ProjectID"].ToInt32();
            tt.ProjectName = reader["Name"].ToString();
            tt.TagName = reader["TagName"].ToString();
            tts.Add(i, tt);
            i++;
          }
          reader.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the Doc Type grid.", ex);
      }

      return tts;
    }

    public void InsertNewRootFolder(string folderName, string pathName)
    {
      if(currentProjectID.Equals(""))
        return;

      EnsureConnection();

      try
      {
        string sqlRoot =  "INSERT INTO GPStaging.LAND.RootFolders(ProjectID, RootFolderName, RootFolderPath)" + g.crlf +
          "VALUES(" + currentProjectID + ", '" + folderName.ToLower() + "', '" + pathName.ToLower() + "')";

        using(var cmd = new SqlCommand(sqlRoot, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new root folder named '" + folderName + "'.", ex);
      }
    }

    public RootFolderSet GetRootFolderSet(string projectName)
    {
      if(currentProjectID == null)
        return null;

      EnsureConnection();

      var rfs = new RootFolderSet();

      try
      {
        string sqlRootCbo = "SELECT * FROM GPStaging.LAND.RootFolders" + g.crlf +
                            "WHERE ProjectID = " + currentProjectID;

        using(var cmd = new SqlCommand(sqlRootCbo, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          SqlDataReader reader = cmd.ExecuteReader();
          while(reader.Read())
          {
            var rf = new RootFolder();
            rf.RootFolderID = reader["RootFolderID"].ToInt32();
            rf.ProjectID = reader["ProjectID"].ToInt32();
            rf.RootFolderName = reader["RootFolderName"].ToString();
            rf.RootFolderPath = reader["RootFolderPath"].ToString();
            rfs.Add(rf.RootFolderID, rf);
          }
          reader.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get root folders and folder names for project '" + projectName + "'.", ex);
      }

      return rfs;
    }

    public int GetFolderID(int projectID, int rootFolderID, string rootFolderFullPath)
    {
      EnsureConnection();

      try
      {
        string sqlFolderID =  "SELECT FolderID FROM GPStaging.LAND.Folders" + g.crlf +
                              "WHERE ProjectID = " + projectID + " AND RootFolderID = " + rootFolderID + g.crlf +
                              "AND FolderFullPath = '" + rootFolderFullPath + "'";

        using(var cmd = new SqlCommand(sqlFolderID, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var folderId = cmd.ExecuteScalar();
          return folderId.ToInt32();
        }

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the FolderID for the following root folder '" + rootFolderFullPath + "'.", ex);
      }



    }

    public int GetRootFolderID(string path)
    {
      EnsureConnection();

      try
      {
        string sqlRootId =  "SELECT RootFolderID FROM GPStaging.LAND.Folders" + g.crlf +
                            "WHERE FolderFullPath = '" + path + "'";

        using (var cmd = new SqlCommand(sqlRootId, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var id = cmd.ExecuteScalar();
          return id.ToInt32();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to get the Root Folder ID for path " + path + ".", ex);
      }
    }

    public int GetRootFolderIDFromName(string name)
    {
      EnsureConnection();

      try
      {
        string sqlRootId =  "SELECT RootFolderID FROM GPStaging.LAND.RootFolders" + g.crlf +
                            "WHERE RootFolderName = '" + name + "'";

        using (var cmd = new SqlCommand(sqlRootId, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var id = cmd.ExecuteScalar();
          return id.ToInt32();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to get the Root Folder ID for Root Folder Name " + name + ".", ex);
      }
    }

    public int GetRootFolderIDFromPath(string path)
    {
      EnsureConnection();

      try
      {
        string sqlRootId =  "SELECT RootFolderID FROM GPStaging.LAND.RootFolders" + g.crlf +
                            "WHERE RootFolderPath = '" + path + "'";

        using (var cmd = new SqlCommand(sqlRootId, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var id = cmd.ExecuteScalar();
          return id.ToInt32();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to get the Root Folder ID for Root Folder path " + path + ".", ex);
      }
    }

    public int GetProjectIDFromPath(string path)
    {
      EnsureConnection();

      try
      {
        string sqlRootId =  "SELECT ProjectID FROM GPStaging.LAND.RootFolders" + g.crlf +
                            "WHERE RootFolderPath = '" + path + "'";

        using (var cmd = new SqlCommand(sqlRootId, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var id = cmd.ExecuteScalar();
          return id.ToInt32();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to get the Project ID for Root Folder " + path + ".", ex);
      }
    }

    public bool RootFolderExists(string path)
    {
      EnsureConnection();

      try
      {
        string sqlRoot =  "SELECT FolderFullPath FROM GPStaging.LAND.Folders" + g.crlf +
                          "WHERE FolderFullPath = '" + path + "'";

        using (var cmd = new SqlCommand(sqlRoot, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var name = cmd.ExecuteScalar();
          if(name == null)
            return false;

          return true;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to determine if the root folder '" + path + "' exists.", ex);
      }
    }

    public TaskResult LoadFileSystem(FileSystemItem fsi, int projectId, int rootFolderId, int? parentFolderId, int? parentFileId = null)
    {
      EnsureConnection();

      var taskResult = new TaskResult();

      try
      {
        string folderSql =  "INSERT INTO LAND.Folders(ProjectID, ParentFolderID, RootFolderID, FolderName, FolderFullPath)" + g.crlf +
                            "VALUES(@ProjectID, @ParentFolderID, @RootFolderId, @FolderName, @FolderFullPath); SELECT SCOPE_IDENTITY()";
        int folderId = 0;

        using(var cmd = new SqlCommand(folderSql, _conn))
        {
          cmd.Parameters.AddWithValue("@ProjectID", projectId);
          cmd.Parameters.AddWithValue("@ParentFolderID", parentFolderId.HasValue ? parentFolderId : 0);
          cmd.Parameters.AddWithValue("@RootFolderId", rootFolderId);
          cmd.Parameters.AddWithValue("@FolderName", fsi.FullName);
          cmd.Parameters.AddWithValue("@FolderFullPath", fsi.FullName);
          folderId = cmd.ExecuteScalar().ToInt32OrDefault(-1);
          if (folderId == -1)
            throw new Exception("The insert of a row to the LAND.Folders table returned a null FolderId"); 
        }

        foreach (var folder in fsi.FileSystemItemSet.Folders)
        {
          parentFolderId = folderId;

          if (fsi.IsRootFolder && folder.IsLastFolderUnderParent & !FileSystemItem.IsRecursionFinished)
            break;
          else
            LoadFileSystem(folder, projectId, rootFolderId, parentFolderId);
        }
        
        foreach(var file in fsi.FileSystemItemSet.Files)
        {
          string fileSql =  "INSERT INTO GPStaging.LAND.Files(ProjectID, FileName, FileFullPath, FileSize, FileExtension, RootFolderID, FolderID)" + g.crlf +
                            "VALUES(@ProjectID, @FileName, @FileFullPath, @FileSize, @FileExtension, @RootFolderID, @FolderID);";

          using(var cmd = new SqlCommand(fileSql, _conn))
          {
            cmd.Parameters.AddWithValue("@ProjectID", projectId);
            cmd.Parameters.AddWithValue("@FileName", file.FileNameWithoutExtension);
            cmd.Parameters.AddWithValue("@FileFullPath", file.FullName);
            cmd.Parameters.AddWithValue("@FileSize", file.Size);
            cmd.Parameters.AddWithValue("@FileExtension", file.Extension);
            cmd.Parameters.AddWithValue("@RootFolderID", rootFolderId);
            cmd.Parameters.AddWithValue("@FolderID", parentFolderId);
            cmd.ExecuteNonQuery();
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load fsi data.", ex);
      }

      return taskResult.Success();
    }

    public TaskResult LoadFilesFromRoot(FileSystemItem fsi, int projectId, int rootFolderId, int parentFolderId)
    {
      EnsureConnection();

      var taskResult = new TaskResult();

      try
      {
        foreach(var file in fsi.FileSystemItemSet.Files)
        {
          string fileSql =  "INSERT INTO GPStaging.LAND.Files(ProjectID, FileName, FileFullPath, FileSize, FileExtension, RootFolderID, FolderID)" + g.crlf +
                            "VALUES(@ProjectID, @FileName, @FileFullPath, @FileSize, @FileExtension, @RootFolderID, @FolderID);";

          using(var cmd = new SqlCommand(fileSql, _conn))
          {
            cmd.Parameters.AddWithValue("@ProjectID", projectId);
            cmd.Parameters.AddWithValue("@FileName", file.FileNameWithoutExtension);
            cmd.Parameters.AddWithValue("@FileFullPath", file.FullName);
            cmd.Parameters.AddWithValue("@FileSize", file.Size);
            cmd.Parameters.AddWithValue("@FileExtension", file.Extension);
            cmd.Parameters.AddWithValue("@RootFolderID", rootFolderId);
            cmd.Parameters.AddWithValue("@FolderID", parentFolderId);
            cmd.ExecuteNonQuery();
          }
        }
      }

      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert files at the root level.", ex);
      }

      return taskResult.Success();
    }

    public int LoadFileSystemRoot(FileSystemItem fsi, int projectId, int rootFolderId, int? parentFolderId, int? parentFileId = null)
    {
      EnsureConnection();

      try
      {
        string folderSql =  "INSERT INTO LAND.Folders(ProjectID, ParentFolderID, RootFolderID, FolderName, FolderFullPath)" + g.crlf +
                            "VALUES(@ProjectID, @ParentFolderID, @RootFolderId, @FolderName, @FolderFullPath); SELECT SCOPE_IDENTITY()";
        int folderId = 0;

        using(var cmd = new SqlCommand(folderSql, _conn))
        {
          cmd.Parameters.AddWithValue("@ProjectID", projectId);
          cmd.Parameters.AddWithValue("@ParentFolderID", parentFolderId.HasValue ? parentFolderId : 0);
          cmd.Parameters.AddWithValue("@RootFolderId", rootFolderId);
          cmd.Parameters.AddWithValue("@FolderName", fsi.FullName);
          cmd.Parameters.AddWithValue("@FolderFullPath", fsi.FullName);
          folderId = cmd.ExecuteScalar().ToInt32OrDefault(-1);
          if (folderId == -1)
            throw new Exception("The insert of a row to the LAND.Folders table returned a null FolderId"); 
        }
        return folderId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load fsi data.", ex);
      }
    }

    public TaskResult LoadFileSystem(OSFolder folder, List<string> ExtensionList, int rootFolderId)
    {
      if (folder.RootFolder.FolderID == null)
      {
        LoadRootFolder(folder, rootFolderId);
      }

      var taskResult = new TaskResult();

      try
      {
        EnsureConnection();

        string folderSql =  "INSERT INTO LAND.Folders(ProjectID, ParentFolderID, RootFolderID, FolderName, FolderFullPath)" + g.crlf +
                            "VALUES(@ProjectID, @ParentFolderID, @RootFolderId, @FolderName, @FolderFullPath); SELECT SCOPE_IDENTITY()";
                           
        using(var cmd = new SqlCommand(folderSql, _conn))
        {
          cmd.Parameters.AddWithValue("@ProjectID", folder.ProjectID.ToInt32());
          cmd.Parameters.AddWithValue("@ParentFolderID", folder.ParentFolder.FolderID);
          cmd.Parameters.AddWithValue("@RootFolderID", rootFolderId);
          cmd.Parameters.AddWithValue("@FolderName", folder.FolderName.Trim().ToLower());
          cmd.Parameters.AddWithValue("@FolderFullPath", folder.FullPath.Trim().ToLower());

          var folderID = cmd.ExecuteScalar();
          folder.FolderID = folderID.ToInt32();

          foreach(var folder2 in folder.OSFolderSet)
          {
            LoadFileSystem(folder2, ExtensionList, rootFolderId);
          }

          foreach(var file in folder.OSFileSet.Values)
          {
            if(!ExtensionList.Contains(file.FileExtension.Trim().ToLower()))
            {
              ExtensionList.Add(file.FileExtension.Trim().ToLower().ToString());
              LoadExtensionsToDb(file);
            }

            string fileSql =  "INSERT INTO LAND.Files(ProjectID, FileName, FileFullPath, FileSize, FileExtension, FolderID, RootFolderID, ExtensionID)" + g.crlf +
                              "VALUES(@ProjectID, @FileName, @FileFullPath, @FileSize, @FileExtension, @FolderID, @RootFolderID, @ExtensionID); SELECT SCOPE_IDENTITY()";

            using(var fileCmd = new SqlCommand(fileSql, _conn))
            {
              fileCmd.Parameters.AddWithValue("@ProjectID", file.ParentFolder.RootFolder.ProjectID.ToInt32());
              fileCmd.Parameters.AddWithValue("@FileName", file.FileName.Trim().ToLower());
              fileCmd.Parameters.AddWithValue("@FileFullPath", file.FullPath.Trim().ToLower());
              fileCmd.Parameters.AddWithValue("@FileSize", file.FileSize);
              fileCmd.Parameters.AddWithValue("@FileExtension", file.FileExtension.Trim().ToLower());
              fileCmd.Parameters.AddWithValue("@FolderID", file.ParentFolder.FolderID);
              fileCmd.Parameters.AddWithValue("@RootFolderID", rootFolderId);
              fileCmd.Parameters.AddWithValue("@ExtensionID", file.ExtensionID);

              var fileID = fileCmd.ExecuteScalar();
              file.FileID = fileID.ToInt32();
            }
          }
        }

        return taskResult.Success();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load file system data.", ex);
      }
    }

    //public TaskResult LoadRootFolder(FileSystemItem fsi, int rootFolderId, int projectId)
    //{
    //  EnsureConnection();
      
    //  var taskResult = new TaskResult();

    //  try
    //  {
    //    string folderSql =  "INSERT INTO LAND.Folders(ProjectID, ParentFolderID, RootFolderID, FolderName, FolderFullPath)" + g.crlf +
    //                        "VALUES(@ProjectID, @ParentFolderID, @RootFolderId, @FolderName, @FolderFullPath); SELECT SCOPE_IDENTITY()";

    //    using(var cmd = new SqlCommand(folderSql, _conn))
    //    {
    //      cmd.Parameters.AddWithValue("@ProjectID", projectId);
    //      cmd.Parameters.AddWithValue("@ParentFolderID", DBNull.Value);
    //      cmd.Parameters.AddWithValue("@RootFolderId", rootFolderId);
    //      cmd.Parameters.AddWithValue("@FolderName", fsi.FullName);
    //      cmd.Parameters.AddWithValue("@FolderFullPath", fsi.FullName);

    //      var fldrID = cmd.ExecuteScalar();
    //      _folderID = fldrID.ToInt32();
    //    }

    //    return taskResult.Success();
    //  }
    //  catch (Exception ex)
    //  {
    //    throw new Exception("An exception occurred while attempting to load Root Folder system data.", ex);
    //  }
    //}

    public TaskResult LoadRootFolder(OSFolder folder, int rootFolderId)
    {
      EnsureConnection();
      
      var taskResult = new TaskResult();

      try
      {
        string pFolderSql = "INSERT INTO LAND.Folders(ProjectID, ParentFolderID, RootFolderID, FolderName, FolderFullPath)" + g.crlf +
                            "VALUES(@ProjectID, @ParentFolderIDRoot, @RootFolderID, @FolderNameRoot, @FolderFullPathRoot); SELECT SCOPE_IDENTITY()";

        using(var parentCmd = new SqlCommand(pFolderSql, _conn))
        {
          var pfid = folder.ParentFolder.FolderID.ToInt32();
          parentCmd.Parameters.AddWithValue("@ProjectID", folder.ProjectID.ToInt32());
          parentCmd.Parameters.AddWithValue("@ParentFolderIDRoot", pfid);
          parentCmd.Parameters.AddWithValue("@RootFolderID", rootFolderId);
          parentCmd.Parameters.AddWithValue("@FolderNameRoot", folder.ParentFolder.FolderName.Trim().ToLower());
          parentCmd.Parameters.AddWithValue("@FolderFullPathRoot", folder.ParentFolder.FullPath.Trim().ToLower());

          var pFolderID = parentCmd.ExecuteScalar();
          folder.RootFolder.FolderID = pFolderID.ToInt32();
          parentCmd.Parameters.Clear();
        }

        return taskResult.Success();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load Root Folder system data.", ex);
      }
    }

    public TaskResult LoadExtensionsToDb(OSFile file)
    {
      var taskResult = new TaskResult();
      try
      {
        EnsureConnection();

        string extSql =  "INSERT INTO LAND.Extensions(ProjectID,ExtensionType)" + g.crlf +
                         "VALUES(@ProjectID, @ExtensionType); SELECT SCOPE_IDENTITY()";

        if (!file.FileExtension.IsBlank() && !file.FileExtension.StartsWith("."))
          file.FileExtension = "." + file.FileExtension.Trim().ToLower();

        using(var extCmd = new SqlCommand(extSql, _conn))
        {
          extCmd.Parameters.AddWithValue("@ProjectID", file.ParentFolder.ProjectID.ToInt32());
          extCmd.Parameters.AddWithValue("@ExtensionType", file.FileExtension.Trim().ToLower().ToString());

          var extID = extCmd.ExecuteScalar();
          file.ExtensionID = extID.ToInt32();
        }

        return taskResult.Success();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load file system data.", ex);
      }
    }

    public TaskResult DeleteFolderTable(int rootFolderID)
    {
      var taskResult = new TaskResult();

      try
      {
        EnsureConnection();

        string truncateSql =  "DELETE FROM [GPStaging].[LAND].[Folders]" + g.crlf +
                              "WHERE ProjectID = " + currentProjectID + " AND RootFolderID = " + rootFolderID;

        using(var cmd = new SqlCommand(truncateSql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An error occurred attempting to delete a root folder from the RootFolders table.", ex);
      }

      return taskResult.Success();
    }

    public TaskResult DeleteRootFolderTable(int rootFolderID)
    {
      var taskResult = new TaskResult();

      try
      {
        EnsureConnection();

        string truncateSql =  "DELETE FROM [GPStaging].[LAND].[RootFolders]" + g.crlf +
                              "WHERE ProjectID = " + currentProjectID + " AND RootFolderID = " + rootFolderID;

        using(var cmd = new SqlCommand(truncateSql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An error occurred attempting to delete a root folder from the RootFolders table.", ex);
      }

      return taskResult.Success();
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
