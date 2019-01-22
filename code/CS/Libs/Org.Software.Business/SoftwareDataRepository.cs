using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Org.Software.Business.Models;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Software.Business
{
  public class SoftwareDataRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;

    public DateTime _importDate;

    public SoftwareDataRepository(ConfigDbSpec configDbSpec)
    {
      _importDate = DateTime.Now;

      _configDbSpec = configDbSpec;
      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");
      _connectionString = _configDbSpec.ConnectionString;
    }

    public List<SoftwareUpdatesForModuleVersion> GetSoftwareUpdatesForModuleVersion(int moduleCode, string currentVersion)
    {
      var list = new List<SoftwareUpdatesForModuleVersion>();

      try
      {
        string expandedVersionString = currentVersion.ToExpandedVersionString();

        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("dbo.sp_GetSoftwareUpdatesForModuleVersion", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@ModuleCode", moduleCode);
          cmd.Parameters.AddWithValue("@CurrentVersion", expandedVersionString);
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new SoftwareUpdatesForModuleVersion();
            entity.SoftwareModuleCode = Convert.ToInt32(reader["SoftwareModuleCode"]);
            entity.SoftwareModuleName = reader["SoftwareModuleCode"].ObjectToTrimmedString();
            entity.SoftwarePlatformId = Convert.ToInt32(reader["SoftwarePlatformId"]);
            entity.SoftwarePlatformString = reader["SoftwarePlatformString"].ObjectToTrimmedString();
            entity.PlatformDescription = reader["PlatformDescription"].ObjectToTrimmedString();
            entity.SoftwareVersionId = Convert.ToInt32(reader["SoftwareVersionId"]);
            entity.SoftwareModuleId = Convert.ToInt32(reader["SoftwareModuleId"]);
            entity.VersionValue = reader["VersionValue"].ObjectToTrimmedString();
            entity.RepositoryId = Convert.ToInt32(reader["RepositoryId"]);
            entity.RepositoryRoot = reader["RepositoryRoot"].ObjectToTrimmedString();
            list.Add(entity);
          }
          reader.Close();
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a list of software updates for module version.", ex);
      }
    }


    public List<FrameworkVersion> GetFrameworkVersions()
    {
      var list = new List<FrameworkVersion>();

      try
      {
        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("dbo.sp_GetFrameworkVersions", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new FrameworkVersion();
            entity.FrameworkVersionId = reader["FrameworkVersionId"].ToInt32();
            entity.SoftwareStatusId = reader["SoftwareStatusId"].ToInt32();
            entity.FrameworkVersionString = reader["FrameworkVersionString"].ObjectToTrimmedString();
            entity.Version = reader["Version"].ObjectToTrimmedString();
            entity.VersionNum = reader["VersionNum"].ObjectToTrimmedString();
            entity.ServicePackString = reader["ServicePackString"].ObjectToTrimmedString();
            list.Add(entity);
          }
          reader.Close();
        }

        return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a list of FrameworkVersions.", ex);
      }
    }

    public ModuleVersionForPlatform GetModuleVersionForPlatform(int moduleCode, string upgradeVersion, string upgradePlatformString)
    {
      var list = new List<ModuleVersionForPlatform>();
      try
      {
        EnsureConnection();

        using (SqlCommand cmd = new SqlCommand("sp_GetModuleVersionForPlatform", _conn))
        {
          cmd.CommandType = System.Data.CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@ModuleCode", moduleCode);
          cmd.Parameters.AddWithValue("@VersionValue", upgradeVersion);
          cmd.Parameters.AddWithValue("@PlatformString", upgradePlatformString);
          SqlDataReader reader = cmd.ExecuteReader();

          while (reader.Read())
          {
            var entity = new ModuleVersionForPlatform();



            //entity.FrameworkVersionId = reader["FrameworkVersionId"].ToInt32();
            //entity.SoftwareStatusId = reader["SoftwareStatusId"].ToInt32();
            //entity.FrameworkVersionString = reader["FrameworkVersionString"].ObjectToTrimmedString();
            //entity.Version = reader["Version"].ObjectToTrimmedString();
            //entity.VersionNum = reader["VersionNum"].ObjectToTrimmedString();
            //entity.ServicePackString = reader["ServicePackString"].ObjectToTrimmedString();
            list.Add(entity);
          }
          reader.Close();
        }

        if (list.Count > 1)
          throw new Exception("More than one ModuleVersionForPlatform was returned for module code '" + moduleCode.ToString() + "' " +
                              "upgrade version '" + upgradeVersion + "' upgrade platform string '" + upgradePlatformString + "' " +
                              "- only one should be returned.");

        return list.FirstOrDefault();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception has occurred attempting to get the module version for platform for module code '" + moduleCode.ToString() +
                            "' upgrade version '" + upgradeVersion + "' and upgrade platform string '" + upgradePlatformString + "'.", ex);
      }
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
