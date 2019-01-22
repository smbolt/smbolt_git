using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  public class NotifyRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;


    public NotifyRepository(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");
      _connectionString = _configDbSpec.ConnectionString;
    }

    public NotifyConfigSet GetNotifyConfigSet(int notifyConfigSetId)
    {
      try
      {
        EnsureConnection();

        var notifyConfigSet = new NotifyConfigSet();

        string sql = " SELECT [NotifyConfigSetID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyConfigSet]" + g.crlf +
                     " WHERE [NotifyConfigSetID] = " + notifyConfigSetId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyConfigSet;

          var r = ds.Tables[0].Rows[0];
          notifyConfigSet.NotifyConfigSetId = r["NotifyConfigSetID"].DbToInt32().Value;
          notifyConfigSet.Name = r["Name"].DbToString();
          notifyConfigSet.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyConfigSet.CreatedBy = r["CreatedBy"].DbToString();
          notifyConfigSet.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyConfigSet.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyConfigSet.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }
        return notifyConfigSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve the NotifyConfigSet with ID '" + notifyConfigSetId + "'.", ex);
      }
    }

    public NotifyConfigSet GetNotifyConfigSet(string name)
    {
      return GetNotifyConfigSets(new List<string>() {
        name
      }, false).FirstOrDefault();
    }

    public NotifyConfigSet GetNotifyConfigSet(string name, bool includeFullHierarchy)
    {
      return GetNotifyConfigSets(new List<string>() {
        name
      }, includeFullHierarchy).FirstOrDefault();
    }

    public List<NotifyConfigSet> GetNotifyConfigSets()
    {
      return GetNotifyConfigSets(null, false);
    }

    public List<NotifyConfigSet> GetNotifyConfigSets(bool includeFullHierarchy)
    {
      return GetNotifyConfigSets(null, includeFullHierarchy);
    }

    public List<NotifyConfigSet> GetNotifyConfigSets(List<string> names, bool includeFullHierarchy)
    {
      try
      {
        EnsureConnection();

        var notifyConfigSets = new List<NotifyConfigSet>();

        string whereClause = BuildNamesWhereClause("Name", names);

        string sql = "SELECT NotifyConfigSetID AS NotifyConfigSetID, " + g.crlf +
                     "  Name AS Name, " + g.crlf +
                     "  IsActive AS IsActive, " + g.crlf +
                     "  CreatedBy AS CreatedBy, " + g.crlf +
                     "  CreatedOn AS CreatedOn, " + g.crlf +
                     "  ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  ModifiedOn AS ModifiedOn " + g.crlf +
                     "FROM dbo.NotifyConfigSet " + g.crlf +
                     whereClause;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyConfigSets;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var ncs = new NotifyConfigSet();
            ncs.NotifyConfigSetId = r["NotifyConfigSetID"].DbToInt32().Value;
            ncs.Name = r["Name"].DbToString();
            ncs.IsActive = r["IsActive"].DbToBoolean().Value;
            ncs.CreatedBy = r["CreatedBy"].DbToString();
            ncs.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            ncs.ModifiedBy = r["ModifiedBy"].DbToString();
            ncs.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyConfigSets.Add(ncs);
          }
        }

        if (!includeFullHierarchy)
          return notifyConfigSets;

        foreach (var ncs in notifyConfigSets)
        {
          var notifyConfigs = GetNotifyConfigsForSet(ncs);
          foreach (var notifyConfig in notifyConfigs)
          {
            if (!ncs.ContainsKey(notifyConfig.Name))
              ncs.Add(notifyConfig.Name, notifyConfig);
          }

          foreach (var notifyConfig in ncs.Values)
          {
            notifyConfig.NotifyEventSet = GetNotifyEventSet(notifyConfig.NotifyConfigId);
            notifyConfig.NotifyEventGroups = GetNotifyEventGroups(notifyConfig.NotifyEventSet.EventIds);
            notifyConfig.NotifyGroupSet = GetNotifyGroupSet(notifyConfig.NotifyEventGroupsIds);

            foreach (var notifyGroup in notifyConfig.NotifyGroupSet.Values)
            {
              var notifyPersons = GetNotifyPersonsForGroup(notifyGroup.NotifyGroupId);
              foreach (var notifyPerson in notifyPersons)
              {
                notifyGroup.Add(notifyPerson);
              }
            }
          }
        }

        foreach (var notifyConfigSet in notifyConfigSets)
        {
          foreach (var notifyConfig in notifyConfigSet.Values)
          {
            foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
            {
              foreach (var notifyEventGroup in notifyConfig.NotifyEventGroups.Values)
              {
                if (notifyEventGroup.NotifyEventID == notifyEvent.NotifyEventId)
                {
                  foreach (var notifyGroup in notifyConfig.NotifyGroupSet.Values)
                  {
                    if (notifyGroup.NotifyGroupId == notifyEventGroup.NotifyGroupID)
                    {
                      var notifyGroupReference = new NotifyGroupReference();
                      notifyGroupReference.NotifyGroupName = notifyGroup.Name;
                      notifyGroupReference.IsActive = notifyGroup.IsActive;
                      if (!notifyEvent.Contains(notifyGroupReference))
                        notifyEvent.Add(notifyGroupReference);
                    }
                  }
                }
              }
            }
          }
        }

        return notifyConfigSets;
      }
      catch (Exception ex)
      {
        string nameList = String.Empty;
        if (names != null)
        {
          foreach (var name in names)
          {
            if (nameList.IsBlank())
              nameList += name;
            else
              nameList += "," + name;
          }
        }

        throw new Exception("An exception occured attempting to retrieve the NotifyConfigSet for names '" + nameList + "'.", ex);
      }
    }

    private string BuildNamesWhereClause(string columnName, List<string> names)
    {
      StringBuilder sb = new StringBuilder();

      if (names == null || names.Count == 0)
        return String.Empty;

      foreach (var name in names)
      {
        if (sb.Length == 0)
          sb.Append("WHERE " + columnName + " = '" + name + "' ");
        else
          sb.Append(g.crlf + "   OR " + columnName + " = '" + name + "' ");
      }

      return sb.ToString();
    }

    public NotifyConfig GetNotifyConfig(int notifyConfigId)
    {
      try
      {
        EnsureConnection();

        var notifyConfig = new NotifyConfig();

        string sql = " SELECT [NotifyConfigID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[SupportEmail] " + g.crlf +
                     " ,[SupportPhone] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[SendEmail] " + g.crlf +
                     " ,[SendSms] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyConfig] " + g.crlf +
                     " WHERE [NotifyConfigID] = " + notifyConfigId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyConfig;

          var r = ds.Tables[0].Rows[0];
          notifyConfig.NotifyConfigId = r["NotifyConfigID"].DbToInt32().Value;
          notifyConfig.Name = r["Name"].DbToString();
          notifyConfig.SupportEmail = r["SupportEmail"].DbToString();
          notifyConfig.SupportPhone = r["SupportPhone"].DbToString();
          notifyConfig.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyConfig.SendEmail = r["SendEmail"].DbToBoolean().Value;
          notifyConfig.SendSms = r["SendSms"].DbToBoolean().Value;
          notifyConfig.CreatedBy = r["CreatedBy"].DbToString();
          notifyConfig.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyConfig.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyConfig.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }
        return notifyConfig;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve the NotifyConfig for ID '" + notifyConfigId + "'.", ex);
      }
    }

    public List<NotifyConfig> GetNotifyConfigs()
    {
      return GetNotifyConfigs(true);
    }

    public List<NotifyConfig> GetNotifyConfigs(bool includeFullHierarchy)
    {
      try
      {
        EnsureConnection();

        var notifyConfigs = new List<NotifyConfig>();

        string sql = " SELECT [NotifyConfigID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[SupportEmail] " + g.crlf +
                     " ,[SupportPhone] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[SendEmail] " + g.crlf +
                     " ,[SendSms] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyConfig] " + g.crlf +
                     " ORDER BY [Name] ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyConfigs;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var nc = new NotifyConfig();
            nc.NotifyConfigId = r["NotifyConfigID"].DbToInt32().Value;
            nc.Name = r["Name"].DbToString();
            nc.SupportEmail = r["SupportEmail"].DbToString();
            nc.SupportPhone = r["SupportPhone"].DbToString();
            nc.IsActive = r["IsActive"].DbToBoolean().Value;
            nc.SendEmail = r["SendEmail"].DbToBoolean().Value;
            nc.SendSms = r["SendSms"].DbToBoolean().Value;
            nc.CreatedBy = r["CreatedBy"].DbToString();
            nc.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            nc.ModifiedBy = r["ModifiedBy"].DbToString();
            nc.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyConfigs.Add(nc);
          }
        }

        foreach (var notifyConfig in notifyConfigs)
        {
          notifyConfig.NotifyEventSet = GetNotifyEventSet(notifyConfig.NotifyConfigId);
          notifyConfig.NotifyEventGroups = GetNotifyEventGroups(notifyConfig.NotifyEventSet.EventIds);
          notifyConfig.NotifyGroupSet = GetNotifyGroupSet(notifyConfig.NotifyEventGroupsIds);

          foreach (var notifyGroup in notifyConfig.NotifyGroupSet.Values)
          {
            var notifyPersons = GetNotifyPersonsForGroup(notifyGroup.NotifyGroupId);
            foreach (var notifyPerson in notifyPersons)
            {
              notifyGroup.Add(notifyPerson);
            }
          }
        }

        foreach (var notifyConfig in notifyConfigs)
        {
          foreach (var notifyEvent in notifyConfig.NotifyEventSet.Values)
          {
            foreach (var notifyEventGroup in notifyConfig.NotifyEventGroups.Values)
            {
              if (notifyEventGroup.NotifyEventID == notifyEvent.NotifyEventId)
              {
                foreach (var notifyGroup in notifyConfig.NotifyGroupSet.Values)
                {
                  if (notifyGroup.NotifyGroupId == notifyEventGroup.NotifyGroupID)
                  {
                    var notifyGroupReference = new NotifyGroupReference();
                    notifyGroupReference.NotifyGroupName = notifyGroup.Name;
                    notifyGroupReference.IsActive = notifyGroup.IsActive;
                    if (!notifyEvent.Contains(notifyGroupReference))
                      notifyEvent.Add(notifyGroupReference);
                  }
                }
              }
            }
          }
        }
        return notifyConfigs;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured attempting to retrieve all NotifyConfigs.", ex);
      }
    }

    public List<NotifyConfig> GetNotifyConfigsForSet(NotifyConfigSet ncs)
    {
      try
      {
        EnsureConnection();

        var notifyConfigs = new List<NotifyConfig>();

        string sql = "SELECT nc.NotifyConfigID AS NotifyConfigID, " + g.crlf +
                     "  nc.Name AS Name, " + g.crlf +
                     "  nc.SupportEmail AS SupportEmail, " + g.crlf +
                     "  nc.SupportPhone AS SupportPhone, " + g.crlf +
                     "  nc.IsActive AS IsActive, " + g.crlf +
                     "  nc.SendEmail AS SendEmail, " + g.crlf +
                     "  nc.SendSms AS SendSms, " + g.crlf +
                     "  nc.CreatedBy AS CreatedBy, " + g.crlf +
                     "  nc.CreatedOn AS CreatedOn, " + g.crlf +
                     "  nc.ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  nc.ModifiedOn AS ModifiedOn " + g.crlf +
                     "FROM dbo.NotifyConfig nc " + g.crlf +
                     "INNER JOIN dbo.NotifyConfigXRef x ON x.NotifyConfigID = nc.NotifyConfigID " + g.crlf +
                     "WHERE x.NotifyConfigSetID = " + ncs.NotifyConfigSetId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyConfigs;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var nc = new NotifyConfig(ncs);
            nc.NotifyConfigId = r["NotifyConfigID"].DbToInt32().Value;
            nc.Name = r["Name"].DbToString();
            nc.SupportEmail = r["SupportEmail"].DbToString();
            nc.SupportPhone = r["SupportPhone"].DbToString();
            nc.IsActive = r["IsActive"].DbToBoolean().Value;
            nc.SendEmail = r["SendEmail"].DbToBoolean().Value;
            nc.SendSms = r["SendSms"].DbToBoolean().Value;
            nc.CreatedBy = r["CreatedBy"].DbToString();
            nc.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            nc.ModifiedBy = r["ModifiedBy"].DbToString();
            nc.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyConfigs.Add(nc);
          }
        }

        return notifyConfigs;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the NotifyConfigs for NotifyConfigSetID " + ncs.NotifyConfigSetId.ToString() + ".", ex);
      }
    }

    public NotifyEventSet GetNotifyEventSet(int notifyConfigID)
    {
      try
      {
        EnsureConnection();

        var nes = new NotifyEventSet();

        string sql = "SELECT NotifyEventID AS NotifyEventID, " + g.crlf +
                     "  NotifyConfigID as NotifyConfigID, " + g.crlf +
                     "  Name AS Name, " + g.crlf +
                     "  IsActive AS IsActive, " + g.crlf +
                     "  DefaultSubject AS DefaultSubject, " + g.crlf +
                     "  CreatedBy AS CreatedBy, " + g.crlf +
                     "  CreatedOn AS CreatedOn, " + g.crlf +
                     "  ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  ModifiedOn AS ModifiedOn " + g.crlf +
                     "FROM dbo.NotifyEvent " + g.crlf +
                     "WHERE NotifyConfigID = " + notifyConfigID.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return nes;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyEvent = new NotifyEvent();
            notifyEvent.NotifyEventId = r["NotifyEventID"].DbToInt32().Value;
            notifyEvent.NotifyConfigId = r["NotifyConfigID"].DbToInt32().Value;
            notifyEvent.Name = r["Name"].DbToString();
            notifyEvent.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyEvent.DefaultSubject = r["DefaultSubject"].DbToString();
            notifyEvent.CreatedBy = r["CreatedBy"].DbToString();
            notifyEvent.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyEvent.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyEvent.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            if (!nes.ContainsKey(notifyEvent.Name))
              nes.Add(notifyEvent.Name, notifyEvent);
          }
        }

        return nes;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyEventSet for NotifyConfigID " + notifyConfigID.ToString() + ".", ex);
      }
    }

    public NotifyEvent GetNotifyEvent(int eventId)
    {
      try
      {
        EnsureConnection();

        var notifyEvent = new NotifyEvent();

        string sql = " SELECT [NotifyEventID] " + g.crlf +
                     " ,[NotifyConfigID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[DefaultSubject] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyEvent] " + g.crlf +
                     " WHERE [NotifyEventID] = " + eventId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyEvent;

          var r = ds.Tables[0].Rows[0];
          notifyEvent.NotifyEventId = r["NotifyEventID"].DbToInt32().Value;
          notifyEvent.NotifyConfigId = r["NotifyConfigID"].DbToInt32().Value;
          notifyEvent.Name = r["Name"].DbToString();
          notifyEvent.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyEvent.DefaultSubject = r["DefaultSubject"].DbToString();
          notifyEvent.CreatedBy = r["CreatedBy"].DbToString();
          notifyEvent.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyEvent.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyEvent.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }

        return notifyEvent;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyEvent for NotifyEventId " + eventId + ".", ex);
      }
    }

    private Dictionary<int, NotifyEventGroup> GetNotifyEventGroups(List<int> eventIdList)
    {
      string eventIds = String.Empty;

      try
      {
        EnsureConnection();

        var notifyEventGroups = new Dictionary<int, NotifyEventGroup>();

        if (eventIdList.Count == 0)
          return notifyEventGroups;

        foreach (var eventId in eventIdList)
        {
          if (eventIds.IsBlank())
            eventIds += eventId.ToString();
          else
            eventIds += "," + eventId.ToString();
        }

        string sql = "SELECT g.NotifyEventGroupID AS NotifyEventGroupID, " + g.crlf +
                     "  g.NotifyEventID as NotifyEventID, " + g.crlf +
                     "  g.NotifyGroupID as NotifyGroupID, " + g.crlf +
                     "  g.IsActive AS IsActive, " + g.crlf +
                     "  g.CreatedBy AS CreatedBy, " + g.crlf +
                     "  g.CreatedOn AS CreatedOn, " + g.crlf +
                     "  g.ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  g.ModifiedOn AS ModifiedOn " + g.crlf +
                     "FROM dbo.NotifyEventGroup g " + g.crlf +
                     "WHERE NotifyEventID IN ( " + eventIds + " ) ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyEventGroups;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyEventGroup = new NotifyEventGroup();
            notifyEventGroup.NotifyEventGroupID = r["NotifyEventGroupID"].DbToInt32().Value;
            notifyEventGroup.NotifyEventID = r["NotifyEventID"].DbToInt32().Value;
            notifyEventGroup.NotifyGroupID = r["NotifyGroupID"].DbToInt32().Value;
            notifyEventGroup.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyEventGroup.CreatedBy = r["CreatedBy"].DbToString();
            notifyEventGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyEventGroup.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyEventGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            if (!notifyEventGroups.ContainsKey(notifyEventGroup.NotifyEventGroupID))
              notifyEventGroups.Add(notifyEventGroup.NotifyEventGroupID, notifyEventGroup);
          }
        }

        return notifyEventGroups;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyEventGroups for NotifyEventIDs " + eventIds + ".", ex);
      }
    }

    public NotifyEventGroup GetNotifyEventGroup(int notifyEventGroupId)
    {
      try
      {
        EnsureConnection();

        var notifyEventGroup = new NotifyEventGroup();

        string sql = " SELECT [NotifyEventGroupID] " + g.crlf +
                     " ,[NotifyEventID] " + g.crlf +
                     " ,[NotifyGroupID] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyEventGroup] " + g.crlf +
                     " WHERE [NotifyEventGroupID] = " + notifyEventGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyEventGroup;

          var r = ds.Tables[0].Rows[0];
          notifyEventGroup.NotifyEventGroupID = r["NotifyEventGroupID"].DbToInt32().Value;
          notifyEventGroup.NotifyEventID = r["NotifyEventID"].DbToInt32().Value;
          notifyEventGroup.NotifyGroupID = r["NotifyGroupID"].DbToInt32().Value;
          notifyEventGroup.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyEventGroup.CreatedBy = r["CreatedBy"].DbToString();
          notifyEventGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyEventGroup.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyEventGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }

        return notifyEventGroup;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyEventGroup for NotifyEventGroupID '" + notifyEventGroupId + "'.", ex);
      }
    }

    public NotifyGroupSet GetNotifyGroupSet(List<int> groupIdList)
    {
      string groupIds = String.Empty;

      try
      {
        EnsureConnection();

        var notifyGroupSet = new NotifyGroupSet();

        if (groupIdList.Count == 0)
          return notifyGroupSet;

        foreach (var eventId in groupIdList)
        {
          if (groupIds.IsBlank())
            groupIds += eventId.ToString();
          else
            groupIds += "," + eventId.ToString();
        }

        string sql = "SELECT NotifyGroupID AS NotifyGroupID, " + g.crlf +
                     "  Name as Name, " + g.crlf +
                     "  IsActive AS IsActive, " + g.crlf +
                     "  CreatedBy AS CreatedBy, " + g.crlf +
                     "  CreatedOn AS CreatedOn, " + g.crlf +
                     "  ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  ModifiedOn AS ModifiedOn " + g.crlf +
                     "FROM dbo.NotifyGroup " + g.crlf +
                     "WHERE NotifyGroupID IN ( " + groupIds + " ) ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyGroupSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyGroup = new NotifyGroup();
            notifyGroup.NotifyGroupId = r["NotifyGroupID"].DbToInt32().Value;
            notifyGroup.Name = r["Name"].DbToString();
            notifyGroup.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyGroup.CreatedBy = r["CreatedBy"].DbToString();
            notifyGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyGroup.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            if (!notifyGroupSet.ContainsKey(notifyGroup.Name))
              notifyGroupSet.Add(notifyGroup.Name, notifyGroup);
          }
        }

        return notifyGroupSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyGroupSet for NotifyGroupIDs " + groupIds + ".", ex);
      }
    }

    public NotifyGroupSet GetNotifyGroupSet(int notifyEventId)
    {
      try
      {
        EnsureConnection();

        var notifyGroupSet = new NotifyGroupSet();

        string sql = "SELECT ng.NotifyGroupID " + g.crlf +
                     " ,ng.Name " + g.crlf +
                     " ,ng.IsActive " + g.crlf +
                     " ,ng.CreatedBy " + g.crlf +
                     " ,ng.CreatedOn " + g.crlf +
                     " ,ng.ModifiedBy " + g.crlf +
                     " ,ng.ModifiedOn " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyGroup] ng " + g.crlf +
                     " INNER JOIN [Notifications].[dbo].[NotifyEventGroup] neg " + g.crlf +
                     "  ON neg.NotifyGroupID = ng.NotifyGroupID " + g.crlf +
                     " WHERE neg.NotifyEventID = " + notifyEventId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyGroupSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyGroup = new NotifyGroup();
            notifyGroup.NotifyGroupId = r["NotifyGroupID"].DbToInt32().Value;
            notifyGroup.Name = r["Name"].DbToString();
            notifyGroup.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyGroup.CreatedBy = r["CreatedBy"].DbToString();
            notifyGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyGroup.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            if (!notifyGroupSet.ContainsKey(notifyGroup.Name))
              notifyGroupSet.Add(notifyGroup.Name, notifyGroup);
          }
        }

        return notifyGroupSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyGroupSet for NotifyEventId '" + notifyEventId + "'.", ex);
      }
    }

    public NotifyGroup GetNotifyGroup(int groupId)
    {
      try
      {
        EnsureConnection();

        var notifyGroup = new NotifyGroup();

        string sql = " SELECT [NotifyGroupID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyGroup] " + g.crlf +
                     " WHERE [NotifyGroupID] = " + groupId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyGroup;

          var r = ds.Tables[0].Rows[0];
          notifyGroup.NotifyGroupId = r["NotifyGroupID"].DbToInt32().Value;
          notifyGroup.Name = r["Name"].DbToString();
          notifyGroup.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyGroup.CreatedBy = r["CreatedBy"].DbToString();
          notifyGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyGroup.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }

        return notifyGroup;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyGroup for NotifyGroupID '" + groupId + "'.", ex);
      }
    }

    public List<NotifyGroup> GetNotifyGroups()
    {
      return GetNotifyGroups(false);
    }

    public List<NotifyGroup> GetNotifyGroups(bool includeFullHierarchy)
    {
      try
      {
        EnsureConnection();

        var notifyGroups = new List<NotifyGroup>();

        string sql = "SELECT [NotifyGroupID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyGroup] " + g.crlf +
                     " ORDER BY [Name] ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyGroups;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyGroup = new NotifyGroup();
            notifyGroup.NotifyGroupId = r["NotifyGroupID"].DbToInt32().Value;
            notifyGroup.Name = r["Name"].DbToString();
            notifyGroup.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyGroup.CreatedBy = r["CreatedBy"].DbToString();
            notifyGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyGroup.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyGroups.Add(notifyGroup);
          }
        }

        if (!includeFullHierarchy)
          return notifyGroups;

        foreach (var notifyGroup in notifyGroups)
        {
          var notifyPersons = GetNotifyPersonsForGroup(notifyGroup.NotifyGroupId);
          foreach (var notifyPerson in notifyPersons)
            notifyGroup.Add(notifyPerson);
        }

        return notifyGroups;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get all NotifyGroups.", ex);
      }
    }

    public NotifyPersonGroup GetNotifyPersonGroup(int notifyPersonGroupId)
    {
      try
      {
        EnsureConnection();

        var notifyPersonGroup = new NotifyPersonGroup();

        string sql = " SELECT [NotifyPersonGroupID] " + g.crlf +
                     " ,[NotifyGroupID] " + g.crlf +
                     " ,[NotifyPersonID] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyPersonGroup] " + g.crlf +
                     " WHERE [NotifyPersonGroupId] = " + notifyPersonGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyPersonGroup;

          var r = ds.Tables[0].Rows[0];
          notifyPersonGroup.NotifyPersonGroupId = r["NotifyPersonGroupID"].DbToInt32().Value;
          notifyPersonGroup.NotifyGroupId = r["NotifyGroupID"].DbToInt32().Value;
          notifyPersonGroup.NotifyPersonId = r["NotifyPersonID"].DbToInt32().Value;
          notifyPersonGroup.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyPersonGroup.CreatedBy = r["CreatedBy"].DbToString();
          notifyPersonGroup.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyPersonGroup.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyPersonGroup.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }

        return notifyPersonGroup;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get NotifyPersonGroup for NotifyPersonGroupId '" + notifyPersonGroupId + "'.", ex);
      }
    }

    public List<NotifyPerson> GetNotifyPersonsForGroup(int groupId)
    {
      try
      {
        EnsureConnection();

        var notifyPersons = new List<NotifyPerson>();

        string sql = "SELECT p.NotifyPersonID AS NotifyPersonID, " + g.crlf +
                     "  p.Name as Name, " + g.crlf +
                     "  p.IsActive AS IsActive, " + g.crlf +
                     "  p.EmailIsActive AS EmailIsActive, " + g.crlf +
                     "  p.EmailAddress AS EmailAddress, " + g.crlf +
                     "  p.SmsIsActive AS SmsIsActive, " + g.crlf +
                     "  p.SmsNumber AS SmsNumber, " + g.crlf +
                     "  p.CreatedBy AS CreatedBy, " + g.crlf +
                     "  p.CreatedOn AS CreatedOn, " + g.crlf +
                     "  p.ModifiedBy AS ModifiedBy, " + g.crlf +
                     "  p.ModifiedOn AS ModifiedOn, " + g.crlf +
                     "  g.NotifyPersonGroupID " + g.crlf +
                     "FROM dbo.NotifyPerson p " + g.crlf +
                     "INNER JOIN dbo.NotifyPersonGroup g ON g.NotifyPersonID = p.NotifyPersonID " + g.crlf +
                     "WHERE NotifyGroupID = " + groupId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyPersons;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyPerson = new NotifyPerson();
            notifyPerson.NotifyPersonId = r["NotifyPersonID"].DbToInt32().Value;
            notifyPerson.Name = r["Name"].DbToString();
            notifyPerson.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyPerson.IsEmailActive = r["EmailIsActive"].DbToBoolean().Value;
            notifyPerson.EmailAddress = r["EmailAddress"].DbToString();
            notifyPerson.IsSmsActive = r["SmsIsActive"].DbToBoolean().Value;
            notifyPerson.SmsNumber = r["SmsNumber"].DbToString();
            notifyPerson.CreatedBy = r["CreatedBy"].DbToString();
            notifyPerson.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyPerson.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyPerson.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyPerson.NotifyPersonGroupId = r["NotifyPersonGroupID"].DbToInt32();
            notifyPersons.Add(notifyPerson);
          }
        }

        return notifyPersons;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyPersons for NotifyGroupID " + groupId.ToString() + ".", ex);
      }
    }

    public NotifyPerson GetNotifyPerson(int personId)
    {
      try
      {
        EnsureConnection();

        var notifyPerson = new NotifyPerson();

        string sql = " SELECT [NotifyPersonID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[EmailIsActive] " + g.crlf +
                     " ,[EmailAddress] " + g.crlf +
                     " ,[SmsIsActive] " + g.crlf +
                     " ,[SmsNumber] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyPerson] " + g.crlf +
                     " WHERE [NotifyPersonID] = " + personId;

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return notifyPerson;

          var r = ds.Tables[0].Rows[0];
          notifyPerson.NotifyPersonId = r["NotifyPersonID"].DbToInt32().Value;
          notifyPerson.Name = r["Name"].DbToString();
          notifyPerson.IsActive = r["IsActive"].DbToBoolean().Value;
          notifyPerson.IsEmailActive = r["EmailIsActive"].DbToBoolean().Value;
          notifyPerson.EmailAddress = r["EmailAddress"].DbToString();
          notifyPerson.IsSmsActive = r["SmsIsActive"].DbToBoolean().Value;
          notifyPerson.SmsNumber = r["SmsNumber"].DbToString();
          notifyPerson.CreatedBy = r["CreatedBy"].DbToString();
          notifyPerson.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
          notifyPerson.ModifiedBy = r["ModifiedBy"].DbToString();
          notifyPerson.ModifiedOn = r["ModifiedOn"].DbToDateTime();
        }

        return notifyPerson;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyPerson for NotifyPersonID '" + personId + "'.", ex);
      }
    }

    public List<NotifyPerson> GetNotifyPersons()
    {
      try
      {
        EnsureConnection();

        var notifyPersons = new List<NotifyPerson>();

        string sql = " SELECT [NotifyPersonID] " + g.crlf +
                     " ,[Name] " + g.crlf +
                     " ,[IsActive] " + g.crlf +
                     " ,[EmailIsActive] " + g.crlf +
                     " ,[EmailAddress] " + g.crlf +
                     " ,[SmsIsActive] " + g.crlf +
                     " ,[SmsNumber] " + g.crlf +
                     " ,[CreatedBy] " + g.crlf +
                     " ,[CreatedOn] " + g.crlf +
                     " ,[ModifiedBy] " + g.crlf +
                     " ,[ModifiedOn] " + g.crlf +
                     " FROM [Notifications].[dbo].[NotifyPerson] " + g.crlf +
                     " ORDER BY [Name] ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return notifyPersons;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var notifyPerson = new NotifyPerson();
            notifyPerson.NotifyPersonId = r["NotifyPersonID"].DbToInt32().Value;
            notifyPerson.Name = r["Name"].DbToString();
            notifyPerson.IsActive = r["IsActive"].DbToBoolean().Value;
            notifyPerson.IsEmailActive = r["EmailIsActive"].DbToBoolean().Value;
            notifyPerson.EmailAddress = r["EmailAddress"].DbToString();
            notifyPerson.IsSmsActive = r["SmsIsActive"].DbToBoolean().Value;
            notifyPerson.SmsNumber = r["SmsNumber"].DbToString();
            notifyPerson.CreatedBy = r["CreatedBy"].DbToString();
            notifyPerson.CreatedOn = r["CreatedOn"].DbToDateTime().Value;
            notifyPerson.ModifiedBy = r["ModifiedBy"].DbToString();
            notifyPerson.ModifiedOn = r["ModifiedOn"].DbToDateTime();
            notifyPersons.Add(notifyPerson);
          }
        }

        return notifyPersons;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get all NotifyPersons.", ex);
      }
    }

    public void UpdateNotifyConfigSet(NotifyConfigSet ncs)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyConfigSet] " + g.crlf +
                     " SET [Name] = '" + ncs.Name + "'" + g.crlf +
                     "    ,[IsActive] = " + ncs.IsActive.ToInt32() + g.crlf +
                     "    ,[ModifiedBy] = " + (ncs.ModifiedBy.IsBlank() ? "NULL" : "'" + ncs.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (ncs.ModifiedOn.HasValue ? "'" + ncs.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyConfigSetID] = " + ncs.NotifyConfigSetId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyConfigSet '" + ncs.Name + "'.", ex);
      }
    }

    public void UpdateNotifyConfig(NotifyConfig nc)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyConfig] " + g.crlf +
                     " SET [Name] = '" + nc.Name + "'" + g.crlf +
                     "    ,[SupportEmail] = " + (nc.SupportEmail.IsBlank() ? "NULL" : "'" + nc.SupportEmail + "'") + g.crlf +
                     "    ,[SupportPhone] = " + (nc.SupportPhone.IsBlank() ? "NULL" : "'" + nc.SupportPhone + "'") + g.crlf +
                     "    ,[IsActive] = " + nc.IsActive.ToInt32() + g.crlf +
                     "    ,[SendEmail] = " + nc.SendEmail.ToInt32() + g.crlf +
                     "    ,[SendSms] = " + nc.SendSms.ToInt32() + g.crlf +
                     "    ,[ModifiedBy] = " + (nc.ModifiedBy.IsBlank() ? "NULL" : "'" + nc.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (nc.ModifiedOn.HasValue ? "'" + nc.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyConfigID] = " + nc.NotifyConfigId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyConfig '" + nc.Name + "'.", ex);
      }
    }

    public void UpdateNotifyEvent(NotifyEvent ne)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyEvent] " + g.crlf +
                     " SET [Name] = '" + ne.Name + "'" + g.crlf +
                     "    ,[IsActive] = " + ne.IsActive.ToInt32() + g.crlf +
                     "    ,[DefaultSubject] = '" + ne.DefaultSubject + "'" + g.crlf +
                     "    ,[ModifiedBy] = " + (ne.ModifiedBy.IsBlank() ? "NULL" : "'" + ne.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (ne.ModifiedOn.HasValue ? "'" + ne.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyEventID] = " + ne.NotifyEventId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyEvent '" + ne.Name + "'.", ex);
      }
    }

    public void UpdateNotifyEventGroup(NotifyEventGroup neg)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyEventGroup] " + g.crlf +
                     " SET [IsActive] = " + neg.IsActive.ToInt32() + g.crlf +
                     "    ,[ModifiedBy] = " + (neg.ModifiedBy.IsBlank() ? "NULL" : "'" + neg.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (neg.ModifiedOn.HasValue ? "'" + neg.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyEventGroupID] = " + neg.NotifyEventGroupID;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyEventGroupID '" + neg.NotifyEventGroupID + "'.", ex);
      }
    }

    public void UpdateNotifyGroup(NotifyGroup ng)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyGroup] " + g.crlf +
                     " SET [Name] = '" + ng.Name + "'" + g.crlf +
                     "    ,[IsActive] = " + ng.IsActive.ToInt32() + g.crlf +
                     "    ,[ModifiedBy] = " + (ng.ModifiedBy.IsBlank() ? "NULL" : "'" + ng.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (ng.ModifiedOn.HasValue ? "'" + ng.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyGroupID] = " + ng.NotifyGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyGroup '" + ng.Name + "'.", ex);
      }
    }

    public void UpdateNotifyPersonGroup(NotifyPersonGroup npg)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyPersonGroup] " + g.crlf +
                     " SET [IsActive] = " + npg.IsActive.ToInt32() + g.crlf +
                     "    ,[ModifiedBy] = " + (npg.ModifiedBy.IsBlank() ? "NULL" : "'" + npg.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (npg.ModifiedOn.HasValue ? "'" + npg.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyPersonGroupID] = " + npg.NotifyPersonGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyEventGroupID '" + npg.NotifyPersonGroupId + "'.", ex);
      }
    }

    public void UpdateNotifyPerson(NotifyPerson np)
    {
      try
      {
        EnsureConnection();

        string sql = " UPDATE [Notifications].[dbo].[NotifyPerson] " + g.crlf +
                     " SET [Name] = '" + np.Name + "'" + g.crlf +
                     "    ,[IsActive] = " + np.IsActive.ToInt32() + g.crlf +
                     "    ,[EmailIsActive] = " + np.IsEmailActive.ToInt32() + g.crlf +
                     "    ,[EmailAddress] = " + (np.EmailAddress.IsBlank() ? "NULL" : "'" + np.EmailAddress + "'") + g.crlf +
                     "    ,[SmsIsActive] = " + np.IsSmsActive.ToInt32() + g.crlf +
                     "    ,[SmsNumber] = " + (np.SmsNumber.IsBlank() ? "NULL" : "'" + np.SmsNumber + "'") + g.crlf +
                     "    ,[ModifiedBy] = " + (np.ModifiedBy.IsBlank() ? "NULL" : "'" + np.ModifiedBy + "'") + g.crlf +
                     "    ,[ModifiedOn] = " + (np.ModifiedOn.HasValue ? "'" + np.ModifiedOn + "'" : "NULL") + g.crlf +
                     " WHERE [NotifyPersonID] = " + np.NotifyPersonId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update NotifyPerson '" + np.Name + "'.", ex);
      }
    }

    public int InsertNotifyConfigSet(NotifyConfigSet ncs)
    {
      try
      {
        EnsureConnection();

        int ncsId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyConfigSet] " + g.crlf +
                     "  ([Name], [IsActive], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES ('" + ncs.Name + "'," +
                     ncs.IsActive.ToInt32() + "," +
                     "'" + ncs.CreatedBy + "'," +
                     "'" + ncs.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          ncsId = cmd.ExecuteScalar().ToInt32();

        return ncsId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyConfigSet '" + ncs.Name + "'.", ex);
      }
    }

    public int InsertNotifyConfigXRef(int notifyconfigSetId, int notifyConfigId)
    {
      try
      {
        EnsureConnection();

        int ncXRefId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyConfigXRef] " + g.crlf +
                     "  ([NotifyConfigSetID], [NotifyConfigID], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES (" + notifyconfigSetId + "," +
                     notifyConfigId + "," +
                     "'" + g.SystemInfo.DomainAndUser + "'," +
                     "'" + DateTime.Now + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          ncXRefId = cmd.ExecuteScalar().ToInt32();

        return ncXRefId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyConfigXRef with NotifyConfigSetID '" + notifyconfigSetId +
                            "' and NotifyConfigID '" + notifyConfigId + "'.", ex);
      }
    }

    public int InsertNotifyConfig(NotifyConfig nc)
    {
      try
      {
        EnsureConnection();

        int ncId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyConfig] " + g.crlf +
                     "  ([Name], [SupportEmail], [SupportPhone], [IsActive], [SendEmail], [SendSms], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES ('" + nc.Name + "'," +
                     (nc.SupportEmail.IsBlank() ? "NULL" : "'" + nc.SupportEmail + "'") + "," +
                     (nc.SupportPhone.IsBlank() ? "NULL" : "'" + nc.SupportPhone + "'") + "," +
                     nc.IsActive.ToInt32() + "," +
                     nc.SendEmail.ToInt32() + "," +
                     nc.SendSms.ToInt32() + "," +
                     "'" + nc.CreatedBy + "'," +
                     "'" + nc.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          ncId = cmd.ExecuteScalar().ToInt32();

        return ncId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyConfigSet '" + nc.Name + "'.", ex);
      }
    }

    public int InsertNotifyEvent(NotifyEvent ne)
    {
      try
      {
        EnsureConnection();

        int neId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyEvent] " + g.crlf +
                     "  ([NotifyConfigID], [Name], [IsActive], [DefaultSubject], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES (" + ne.NotifyConfigId + "," +
                     "'" + ne.Name + "'," +
                     ne.IsActive.ToInt32() + "," +
                     "'" + ne.DefaultSubject + "'," +
                     "'" + ne.CreatedBy + "'," +
                     "'" + ne.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          neId = cmd.ExecuteScalar().ToInt32();

        return neId;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyEvent '" + ne.Name + "'.", ex);
      }
    }

    public int InsertNotifyEventGroup(NotifyEventGroup neg)
    {
      try
      {
        EnsureConnection();

        int negId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyEventGroup] " + g.crlf +
                     "  ([NotifyEventID], [NotifyGroupID], [IsActive], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES (" + neg.NotifyEventID + "," +
                     + neg.NotifyGroupID + "," +
                     neg.IsActive.ToInt32() + "," +
                     "'" + neg.CreatedBy + "'," +
                     "'" + neg.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          negId = cmd.ExecuteScalar().ToInt32();

        return negId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyEventGroup with NotifyEventID '" + neg.NotifyEventID +
                            "' and NotifyGroupID '" + neg.NotifyGroupID + "'.", ex);
      }
    }

    public int InsertNotifyGroup(NotifyGroup ng)
    {
      try
      {
        EnsureConnection();

        int ngId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyGroup] " + g.crlf +
                     "  ([Name], [IsActive], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES ('" + ng.Name + "'," +
                     ng.IsActive.ToInt32() + "," +
                     "'" + ng.CreatedBy + "'," +
                     "'" + ng.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          ngId = cmd.ExecuteScalar().ToInt32();

        return ngId;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyGroup '" + ng.Name + "'.", ex);
      }
    }

    public int InsertNotifyPersonGroup(NotifyPersonGroup npg)
    {
      try
      {
        EnsureConnection();

        int npgId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyPersonGroup] " + g.crlf +
                     "  ([NotifyGroupID], [NotifyPersonID], [IsActive], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES (" + npg.NotifyGroupId + "," +
                     +npg.NotifyPersonId + "," +
                     npg.IsActive.ToInt32() + "," +
                     "'" + npg.CreatedBy + "'," +
                     "'" + npg.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          npgId = cmd.ExecuteScalar().ToInt32();

        return npgId;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyPersonGroup with NotifyGroupID '" + npg.NotifyGroupId +
                            "' and NotifyPersonID '" + npg.NotifyPersonId + "'.", ex);
      }
    }

    public int InsertNotifyPerson(NotifyPerson np)
    {
      try
      {
        EnsureConnection();

        int npId;

        string sql = " INSERT INTO [Notifications].[dbo].[NotifyPerson] " + g.crlf +
                     "  ([Name], [IsActive], [EmailIsActive], [EmailAddress], [SmsIsActive], [SmsNumber], [CreatedBy], [CreatedOn]) " + g.crlf +
                     "  VALUES ('" + np.Name + "'," +
                     np.IsActive.ToInt32() + "," +
                     np.IsEmailActive.ToInt32() + "," +
                     (np.EmailAddress.IsBlank() ? "NULL" : "'" + np.EmailAddress + "'") + "," +
                     np.IsSmsActive.ToInt32() + "," +
                     (np.SmsNumber.IsBlank() ? "NULL" : "'"  + np.SmsNumber + "'") + "," +
                     "'" + np.CreatedBy + "'," +
                     "'" + np.CreatedOn + "'); " + " SELECT SCOPE_IDENTITY()";

        using (var cmd = new SqlCommand(sql, _conn))
          npId = cmd.ExecuteScalar().ToInt32();

        return npId;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert NotifyPerson '" + np.Name + "'.", ex);
      }
    }

    public void DeleteNotifyConfigSet(int notifyConfigSetId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyConfigSet] " + g.crlf +
                     " WHERE [NotifyConfigSetID] = " + notifyConfigSetId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyConfigSetID '" + notifyConfigSetId + "'.", ex);
      }
    }

    public void DeleteNotifyConfigXRef(int notifyConfigSetId, int notifyConfigId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyConfigXRef] " + g.crlf +
                     " WHERE [NotifyConfigSetID] = " + notifyConfigSetId + g.crlf +
                     " AND [NotifyConfigID] = " + notifyConfigId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyConfigXRef between NotifyConfigSetID '" + notifyConfigSetId +
                            "' and NotifyConfigID '" + notifyConfigId + "'.", ex);
      }
    }

    public void DeleteNotifyConfig(int notifyConfigId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyConfig] " + g.crlf +
                     " WHERE [NotifyConfigID] = " + notifyConfigId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyConfigID '" + notifyConfigId + "'.", ex);
      }
    }

    public void DeleteNotifyEvent(int notifyEventId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyEvent] " + g.crlf +
                     " WHERE [NotifyEventID] = " + notifyEventId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyEventID '" + notifyEventId + "'.", ex);
      }
    }

    public void DeleteNotifyEventGroup(int notifyEventGroupId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyEventGroup] " + g.crlf +
                     " WHERE [NotifyEventGroupID] = " + notifyEventGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyEventGroupID '" + notifyEventGroupId + "'.", ex);
      }
    }

    public void DeleteNotifyGroup(int notifyGroupId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyGroup] " + g.crlf +
                     " WHERE [NotifyGroupID] = " + notifyGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyGroupID '" + notifyGroupId + "'.", ex);
      }
    }

    public void DeleteNotifyPersonGroup(int notifyPersonGroupId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyPersonGroup] " + g.crlf +
                     " WHERE [NotifyPersonGroupID] = " + notifyPersonGroupId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyPersonGroupID '" + notifyPersonGroupId + "'.", ex);
      }
    }

    public void DeleteNotifyPerson(int notifyPersonId)
    {
      try
      {
        EnsureConnection();

        string sql = " DELETE FROM [Notifications].[dbo].[NotifyPerson] " + g.crlf +
                     " WHERE [NotifyPersonID] = " + notifyPersonId;

        using (var cmd = new SqlCommand(sql, _conn))
          cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete NotifyPersonID '" + notifyPersonId + "'.", ex);
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
