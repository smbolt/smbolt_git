using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Org.GS.Configuration;

namespace Org.GS.Database
{
  public class RequestProcessorBase : IDisposable
  {
    private SqlConnection _conn;
    public SqlConnection Connection { get { return Get_Connection(); } }
    private ConfigDbSpec _configDbSpec;

    public RequestProcessorBase(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
    }

    private void EnsureConnection()
    {
      try
      {
        if (!_configDbSpec.IsReadyToConnect())
          throw new Exception("The ConfigDbSpec object is not ready to connect.");

        if (_conn == null)
          _conn = new SqlConnection(_configDbSpec.ConnectionString);

        if (_conn.State != ConnectionState.Open)
          _conn.Open();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to ensure (or create) the database connection.", ex);
      }
    }

    protected string BuildSelectQuery(DbTable t)
    {
      var sb = new StringBuilder();

      sb.Append("SELECT " + g.crlf);

      for (int i = 0; i < t.DbColumnSet.Count; i++)
      {
        var dbColumn = t.DbColumnSet.Values.ElementAt(i);
        if (i > 0)
          sb.Append(", " + g.crlf);
        sb.Append("  " + dbColumn.Name);
      }

      sb.Append(g.crlf + "FROM " + t.TableName + " " + g.crlf);

      var sequencerColumn = t.DbColumnSet.Values.Where(c => c.IsSequencer).FirstOrDefault();
      if (sequencerColumn != null)
      {
        sb.Append("ORDER BY " + sequencerColumn.Name + " ");
      }

      string sql = sb.ToString();

      return sql;
    }

    private SqlConnection Get_Connection()
    {
      EnsureConnection();
      return _conn;
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
