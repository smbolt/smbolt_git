using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DB
{
  public class EntityModelMapSet : Dictionary<string, EntityModelMap>
  {
    public Dictionary<string, string> ModelToEntityIndex {
      get;
      set;
    }

    public EntityModelMapSet()
    {
      this.ModelToEntityIndex = new Dictionary<string,string>();
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();

      if (this.Count == 0)
        return "No maps";

      foreach (var entityModelMap in this.Values)
      {
        sb.Append("MAP:" + entityModelMap.Name + g.crlf);
        sb.Append("  Entity: " + entityModelMap.EntityType.FullName + g.crlf +
                  "  Model : " + entityModelMap.ModelType.FullName + g.crlf);

        if (entityModelMap.PropertiesLoaded)
        {
          sb.Append("  PropertyInfo Pairs" + g.crlf);
          foreach (var pip in entityModelMap.PropertyInfoPairSet)
          {
            sb.Append("    " + pip.Key.PadTo(20) + "  ");
            PropertyInfoPair pair = pip.Value;
            sb.Append("Entity:" + pair.EntityPropertyInfo.Name + "  type " + pair.EntityPropertyInfo.PropertyType.ToFullTypeName() + " <==> ");
            sb.Append("Model:" + pair.ModelPropertyInfo.Name + " type " + pair.ModelPropertyInfo.PropertyType.ToFullTypeName() + g.crlf);
          }
        }
        else
        {
          sb.Append("  NO PROPERTY INFORMATION PAIRS LOADED" + g.crlf);
        }


        sb.Append(g.crlf);
      }


      return sb.ToString();
    }
  }
}
