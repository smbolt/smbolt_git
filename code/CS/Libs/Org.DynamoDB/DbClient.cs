using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Org.GS;
using System.Net.Sockets;

namespace Org.DynamoDB
{
  public class DbClient
  {
    private DbParms _dbParms;
    private AmazonDynamoDBConfig _ddbConfig;
    private AmazonDynamoDBClient _client;

    public DbClient(DbParms dbParms)
    {
      _dbParms = dbParms;
      AssertDbParms();
    }

    public void Connect()
    {
      try
      {
        if (_dbParms.UseLocalInstance)
        {
          var connectionResult = TryTcpConnectLocal(_dbParms);

          switch (connectionResult.DbConnectionStatus)
          {
            case DbConnectionStatus.Ready:
              return;

            case DbConnectionStatus.TcpPortNotListening:
              throw new Exception(connectionResult.Message);

            case DbConnectionStatus.Failed:
              throw new Exception("The DynamoDb connection attempt failed using host '" + _dbParms.Host + "' and port " +
                                  _dbParms.Port.ToString() + ".", connectionResult.Exception);

            default:
              throw new Exception("Unexpected connection status '" + connectionResult.DbConnectionStatus.ToString() + "' returned " +
                                  "from attempt to connect to host '" + _dbParms.Host + "' on port " + _dbParms.Port.ToString() +
                                  ".", connectionResult.Exception);
          }
        }
        else
        {
          throw new NotImplementedException("The usage of a non-local instance of DynamoDb is not yet implemented in the DbClient class.");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the DynamoDB instance is listening.", ex);
      }
    }

    private DbConnectionResult TryTcpConnectLocal(DbParms dbParms)
    {
      bool portIsListening = false;
      var connectionResult = new DbConnectionResult();

      try
      {
        using (var tcpClient = new TcpClient())
        {
          var result = tcpClient.BeginConnect(dbParms.Host, _dbParms.Port, null, null);
          portIsListening = result.AsyncWaitHandle.WaitOne(_dbParms.TcpConnectWaitMilliseconds);
          tcpClient.EndConnect(result);
        }

        if (!portIsListening)
        {
          connectionResult.DbConnectionStatus = DbConnectionStatus.TcpPortNotListening;
          connectionResult.Message = "No response from TCP port " + dbParms.Port.ToString() + " on host '" + dbParms.Host + "'.";
          return connectionResult;
        }

        _ddbConfig = new AmazonDynamoDBConfig();
        _ddbConfig.ServiceURL = _dbParms.SerivceURL;

        try
        {
          _client = new AmazonDynamoDBClient(_ddbConfig);
        }
        catch (Exception ex)
        {
          connectionResult.DbConnectionStatus = DbConnectionStatus.Failed;
          connectionResult.Exception = ex;
          return connectionResult;
        }

        connectionResult.DbConnectionStatus = DbConnectionStatus.Ready;
        return connectionResult;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to connect to TCP port '" + dbParms.Port.ToString() + "' on " +
                            "host '" + dbParms.Host + "'.", ex);
      }
    }

    private void AssertDbParms()
    {
      if (_dbParms == null)
        throw new Exception("The Org.DynamoDb.DbParms object is null.");

      if (_dbParms.Host.IsBlank())
        throw new Exception("The Host property of the DbParms object is null or blank.");

      if (_dbParms.Port == 0)
        throw new Exception("The Port property of the DbParms object is equal to '0' (default integer).");
    }

  }
}
