using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Ops.Business
{
  public class TaskScheduleRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;

    public DateTime _importDate;

    public TaskScheduleRepository(ConfigDbSpec configDbSpec)
    {
      _importDate = DateTime.Now;

      _connectionString = configDbSpec.ConnectionString;
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
