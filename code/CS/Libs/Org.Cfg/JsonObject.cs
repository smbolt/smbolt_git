using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.GS;

namespace Org.Cfg
{
  public class JsonObject
  {
    private object _o;
    private JObject _jo;
    public JObject JObject { get { return _jo; } }

    private string _json;

    public JsonObject()
    {
      _o = null;
    }

    public JsonObject(string json)
    {
      _json = json;

      if (_json != null)
        _jo = (JObject) JsonConvert.DeserializeObject(_json); 
    }
  }
}
