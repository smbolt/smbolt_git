using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Teleflora.Operations.MetricView
{
  public class MetricsData
  {
    //private const string connString = @"server=TFOKSQLRPT\SQL2K5RPTPOC; database=OpsMetrics; " +
    //    "integrated security=SSPI; Connection Timeout=60;";
    //private const string connString = @"server=SBOLTLT\BOLTSQL2K5; database=OpsMetrics; integrated security=SSPI";
    private string connString = @"Data Source=WIN2K8SVR1\SQL2K8EEMAIN1;Initial Catalog=OpsMetrics;Persist Security Info=True;User ID=smbolt;Password=gen126";
    private bool IsConnectionCreated = false;
    private bool IsConnectionESQLMGMTCreated = false;

    private const string connStringESQLMGMT = @"server=esqlmgmt.teleflora.net; database=PerformanceCounters; " +
        " User ID=sbolt; Password=bolt; Connection Timeout=60;";
    //private const string connStringESQLMGMT = @"server=SBOLTLT\BOLTSQL2K5; database=PerformanceCounters; integrated security=SSPI";

    private SqlConnection _connection;
    public SqlConnection Connection
    {
      get
      {
        if (!IsConnectionCreated)
          CreateConnection();
        return _connection;
      }
      set {
        _connection = value;
      }
    }

    private SqlConnection _connectionESQLMGMT;
    public SqlConnection ConnectionESQLMGMT
    {
      get
      {
        if (!IsConnectionESQLMGMTCreated)
          CreateConnectionESQLMGMT();
        return _connectionESQLMGMT;
      }
      set {
        _connectionESQLMGMT = value;
      }
    }

    public MetricsData()
    {
      connString = ConfigurationSettings.AppSettings["ConnectionString"];
    }

    public void CreateConnection()
    {
      _connection = new SqlConnection(connString);
      _connection.Open();
      IsConnectionCreated = true;
    }

    public void CreateConnectionESQLMGMT()
    {
      _connectionESQLMGMT = new SqlConnection(connStringESQLMGMT);
      _connectionESQLMGMT.Open();
      IsConnectionESQLMGMTCreated = true;
    }

    public void CloseConnection()
    {
      if (_connection.State != ConnectionState.Closed)
        _connection.Close();

      if (IsConnectionESQLMGMTCreated)
      {
        if (_connectionESQLMGMT.State != ConnectionState.Closed)
          _connectionESQLMGMT.Close();
      }
    }

  }
}
