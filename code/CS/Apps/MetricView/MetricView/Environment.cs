using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class Environment
  {
    private int _environmentID;
    public int EnvironmentID
    {
      get {
        return _environmentID;
      }
      set {
        _environmentID = value;
      }
    }

    private string _environmentDesc;
    public string EnvironmentDesc
    {
      get {
        return _environmentDesc;
      }
      set {
        _environmentDesc = value;
      }
    }

    public Environment()
    {
      _environmentID = 0;
      _environmentDesc = String.Empty;
    }
  }
}
