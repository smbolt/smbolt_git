using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Org.Cfg
{
  public class CI : CO
  {
    [JsonIgnore]
    public override string CIType {
      get {
        return this.GetType().Name;
      }
    }

    [JsonIgnore]
    public override string CIName {
      get {
        return base.CIName;
      }
    }

    private string _k;
    public string K
    {
      get {
        return _k;
      }
      set {
        _k = value;
      }
    }

    [JsonIgnore]
    public string Key {
      get {
        return this.CIType + "." + this.K;
      }
    }

    public string V {
      get;
      set;
    }

    public CI()
    {
      this.K = String.Empty;
      this.V = String.Empty;
    }

    public CI(string k, string v)
    {
      this.K = k;
      base.CIName = k;
      this.V = v;
    }
  }
}
