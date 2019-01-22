using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace Org.GS.Configuration
{
  [Serializable]
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public class ConfigDbSpec : ConfigObjectBase
  {
    [OrgConfigItem] public string DbServer { get; set; }
    [OrgConfigItem] public string DbDsn { get; set; }
    [OrgConfigItem] public string DbName { get; set; }
    [OrgConfigItem] public string DbUserId { get; set; }
    [OrgConfigItem] public string DbPassword { get; set; }
    [OrgConfigItem] public bool DbPasswordEncoded { get; set; }
    [OrgConfigItem] public DatabaseType DbType { get; set; }      
    [OrgConfigItem] public bool DbUseWindowsAuth { get; set; }    
    [OrgConfigItem] public string DbEfProvider { get; set; }   
    [OrgConfigItem] public string DbEfMetadata { get; set; }

    public string VerifiedDbServer { get; set; }
    public string VerifiedDbDsn { get; set; }
    public string VerifiedDbName { get; set; }
    public string VerifiedDbUserId { get; set; }
    public string VerifiedDbPassword { get; set; }
    public bool VerifiedDbPasswordEncoded { get; set; }
    public DatabaseType VerifiedDbType { get; set; }
    public bool VerifiedDbUseWindowsAuth { get; set; }
    public string VerifiedDbEfProvider { get; set; }
    public string VerifiedDbEfMetadata { get; set; }

    private string OriginalDbServer;
    private string OriginalDbDsn;
    private string OriginalDbName;
    private string OriginalDbUserId;
    private string OriginalDbPassword;
    private bool OriginalDbPasswordEncoded;
    private DatabaseType OriginalDbType;
    private bool OriginalDbUseWindowsAuth;
    private string OriginalDbEfProvider;
    private string OriginalDbEfMetadata;

    public bool DbConnectionVerified { get; set; }
    public bool SkipDbConnectionConfig { get; set; }

    public string ConnectionString
    {
        get { return GetConnectionString(); }
    }

    public string DescriptionString
    {
        get { return GetDescriptionString(); }
    }

    public override bool IsUpdated
    {
      get { return IsObjectUpdated(); }
    }

    public ConfigDbSpec(string namingPrefix)
      :base (namingPrefix)
    {
      Initialize();
      SetDbType();
    }

    public ConfigDbSpec()
    {
      Initialize();
    }

    private void Initialize()
    {
      this.DbServer = String.Empty;
      this.DbDsn = String.Empty;
      this.DbName = String.Empty;
      this.DbUserId = String.Empty;
      this.DbPassword = String.Empty;
      this.DbPasswordEncoded = false;
      this.DbType = DatabaseType.SqlServer;
      this.DbUseWindowsAuth = false;
      this.DbEfProvider = String.Empty;
      this.DbEfMetadata = String.Empty;

      SetVerifiedProperties();
      SetOriginalProperties();

      this.DbConnectionVerified = false;
      this.SkipDbConnectionConfig = false;           
    }

    public bool CanAdvance()
    {
      if (this.SkipDbConnectionConfig)
         return true;

      if (this.DbConnectionVerified)
      {
        if (this.VerifiedDbServer == this.DbServer &&
          this.VerifiedDbName == this.DbName &&
          this.VerifiedDbDsn == this.DbDsn &&
          this.VerifiedDbUserId == this.DbUserId &&
          this.VerifiedDbPassword == this.DbPassword &&
          this.VerifiedDbPasswordEncoded == this.DbPasswordEncoded &&
          this.VerifiedDbType == this.DbType &&
          this.VerifiedDbEfProvider == this.DbEfProvider &&
          this.VerifiedDbEfMetadata == this.DbEfMetadata)
          return true;
      }

      return false;
    }

    private string GetDescriptionString()
    {
      switch (this.NamingPrefix.ToLower())
      {
        case "":
            return "No descriptions defined";
      }

      return "No descriptions defined";
    }

    public bool IsReadyToConnect()
    {
      switch (this.DbType)
      {
        case DatabaseType.OracleDsn:
        case DatabaseType.SqlServerDsn:
          return (this.DbDsn.IsNotBlank() && this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());

        case DatabaseType.SqlServerEF:
          if (this.DbUseWindowsAuth)
            return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbEfProvider.IsNotBlank() && this.DbEfMetadata.IsNotBlank());
          else
            return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbEfProvider.IsNotBlank() && this.DbEfMetadata.IsNotBlank() &&
                    this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());

        default:
          if (this.DbUseWindowsAuth)
            return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank());
          else
            return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());
      }
    }

    private void SetDbType()
    {
      switch (this.NamingPrefix)
      {
        case "Sql":
          this.DbType = DatabaseType.SqlServer;
          break;

        default:
          this.DbType = DatabaseType.SqlServer;
          break;
      }                    
    }

    public string GetConnectionString()
    {
      switch (this.DbType)
      {
        case DatabaseType.SqlServer:
          if (this.DbUseWindowsAuth)
            return "Server=" + this.DbServer + "; Database=" + this.DbName + "; Trusted_Connection=True;";
          else
            return "Data Source=" + this.DbServer + "; Initial Catalog=" + this.DbName + "; User Id=" + this.DbUserId + ";" + " Password=" + this.DbPassword + ";";

        case DatabaseType.OracleDsn:
        case DatabaseType.SqlServerDsn:
          return "DSN=" + this.DbDsn + ";" + "Provider=" + String.Empty + ";" + "UID=" + this.DbUserId + ";" + "PWD=" + this.DbPassword + ";";

        case DatabaseType.MySql:
          return "server=" + this.DbServer + "; uid=" + this.DbUserId +"; pwd=" + this.DbPassword + "; database=" + this.DbName + ";";

        case DatabaseType.SqlServerEF:
          return ""; 
      }

      return String.Empty;
    }

    private bool IsObjectUpdated()
    {
      if (this.OriginalDbServer == this.DbServer &&
        this.OriginalDbName == this.DbName &&
        this.OriginalDbDsn == this.DbDsn &&
        this.OriginalDbUserId == this.DbUserId &&
        this.OriginalDbPassword == this.DbPassword &&
        this.OriginalDbPasswordEncoded == this.DbPasswordEncoded &&
        this.OriginalDbType == this.DbType &&
        this.OriginalDbUseWindowsAuth == this.DbUseWindowsAuth &&
        this.OriginalDbEfProvider == this.DbEfProvider &&
        this.OriginalDbEfMetadata == this.DbEfMetadata)
        return false;

      return true;
    }

    public void SetAsVerified()
    {
      SetVerifiedProperties();
      this.DbConnectionVerified = true;
    }

    private void SetVerifiedProperties()
    {
      this.VerifiedDbServer = this.DbServer;
      this.VerifiedDbName = this.DbName;
      this.VerifiedDbDsn = this.DbDsn;
      this.VerifiedDbUserId = this.DbUserId;
      this.VerifiedDbPassword = this.DbPassword;
      this.VerifiedDbPasswordEncoded = this.DbPasswordEncoded;
      this.VerifiedDbType = this.DbType;
      this.VerifiedDbUseWindowsAuth = this.DbUseWindowsAuth;
      this.VerifiedDbEfProvider = this.DbEfProvider;
      this.VerifiedDbEfMetadata = this.DbEfMetadata;
    }

    public override void SetOriginalProperties()
    {
      this.OriginalDbServer = this.DbServer;
      this.OriginalDbName = this.DbName;
      this.OriginalDbDsn = this.DbDsn;
      this.OriginalDbUserId = this.DbUserId;
      this.OriginalDbPassword = this.DbPassword;
      this.OriginalDbPasswordEncoded = this.DbPasswordEncoded;
      this.OriginalDbType = this.DbType;
      this.OriginalDbUseWindowsAuth = this.DbUseWindowsAuth;
      this.OriginalDbEfProvider = this.DbEfProvider;
      this.OriginalDbEfMetadata = this.DbEfMetadata;
    }
  }
}
