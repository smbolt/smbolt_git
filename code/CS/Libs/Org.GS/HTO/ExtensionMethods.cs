using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public static class HtoExtensionMethods
  {
    public static Hto ToHto(this JToken jt, Hto parent = null)
    {
      try
      {
        if (jt == null)
          return null;

        switch (jt.Type)
        {
          case JTokenType.Array: return ((JArray)jt).ToHto(parent);
          case JTokenType.Object: return ((JObject)jt).ToHto(parent);
          case JTokenType.Property: return ((JProperty)jt).ToHto(parent);
        }

        var hto = new Hto();
        hto.Parent = parent;
        hto.HtoSet = null;
        JValue jValue = jt as JValue;
        if (jValue == null)
          throw new Exception("The JToken is not a JContainer but cannot be cast to a JValue (should be one or the other).");
        hto.Id = new Guid().ToString();
        hto.Value = jValue.Value.ToString();
        return hto;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create an Hto object from the JToken '" + jt.Report(50) + "'.", ex);
      }
    }

    public static Hto ToHto(this JArray a, Hto parent = null)
    {
      if (a == null)
        return null;

      var hto = new Hto();
      hto.Parent = parent;
      hto.HtoSet = new HtoSet(hto, HtoSourceObjectType.JArray);

      foreach (JToken t in a.Children())
      {
        var tokenHto = new Hto();
        tokenHto.Parent = hto;

        if (t.IsElemental())
        {
          var jValue = t as JValue;
          if (jValue != null)
          {
            tokenHto.Value = jValue.ToString();
            hto.HtoSet.Add(tokenHto);
          }
        }
        else
        {
          hto.HtoSet.Add(t.ToHto(hto));
        }
      }

      return hto;
    }

    public static Hto ToHto(this JObject o, Hto parent = null)
    {
      if (o == null)
        return null;

      var hto = new Hto();
      hto.Parent = parent;
      hto.HtoSet = new HtoSet(hto, HtoSourceObjectType.JObject);

      foreach (JProperty p in o.Children())
      {
        if (p.Value.IsElemental())
        {
          var childHto = new Hto();
          childHto.Parent = hto;
          childHto.Id = p.Name;
          childHto.Value = p.Value.ToString();
          hto.HtoSet.Add(childHto);
        }
        else
        {
          var childHto = p.Value.ToHto(hto);
          childHto.Id = p.Name;
          hto.HtoSet.Add(childHto);
        }
      }

      return hto;
    }

    public static Hto ToHto(this JProperty p, Hto parent = null)
    {
      if (p == null)
        return null;

      var hto = new Hto();
      hto.Parent = parent;
      hto.HtoSet = new HtoSet(hto, HtoSourceObjectType.JProperty);

      hto.Id = p.Name;

      JToken propertyToken = p.Value;

      // here the property could be a string or it could be an HtoSet

      return hto;
    }

    public static string Report(this JToken jt, int maxLength = -1)
    {
      if (jt == null)
        return "JToken is null";

      string jsonString = jt.ToString(Formatting.Indented);

      if (maxLength == -1)
        return jsonString;

      return jsonString.TrimToMax(maxLength);
    }

    public static bool IsElemental(this JToken t)
    {
      if (t == null)
        return true;

      switch (t.Type)
      {
        case JTokenType.Array:
        case JTokenType.Object:
        case JTokenType.Property:
          return false;
      }

      return true;
    }
  }
}
