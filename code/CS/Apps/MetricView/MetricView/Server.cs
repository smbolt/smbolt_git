using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class Server
  {
    private int _serverID;
    public int ServerID
    {
      get {
        return _serverID;
      }
      set {
        _serverID = value;
      }
    }

    private string _serverDesc;
    public string ServerDesc
    {
      get {
        return _serverDesc;
      }
      set {
        _serverDesc = value;
      }
    }

    public Server()
    {
      _serverID = 0;
      _serverDesc = String.Empty;
    }
  }
}
