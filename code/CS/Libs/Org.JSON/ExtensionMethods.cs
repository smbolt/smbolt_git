using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.GS;

namespace Org.Json
{
  public static class ExtensionMethods
  {
    public static string JsonSerialize(this object o)
    {
      try
      {
        if (o == null)
          return String.Empty;

        var settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.DefaultValueHandling = DefaultValueHandling.Ignore;
        string json = JsonConvert.SerializeObject(o, Formatting.Indented, settings);
        return json;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to serialize an object of type '" + o.GetType().FullName + "' to Json.", ex);
      }
    }

    public static object JsonDeserialize<T>(this string json)
    {
      try
      {
        if (json == null)
          return null;

        object o = JsonConvert.DeserializeObject<T>(json);

        return o;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create an object from a json string. " +
                            "The json string is '" + g.crlf + json.PadTo(200).Trim() + "'.", ex);
      }
    }
  }
}
