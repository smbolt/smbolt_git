using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DB
{
  public class CacheEntry
  {
    public string ModelName {
      get;
      set;
    }
    public SortedList<string, ModelBase> ModelSet {
      get;
      set;
    }
    public DateTime? CreatedAt {
      get;
      set;
    }

    public CacheEntry()
    {
      this.ModelName = String.Empty;
      this.ModelSet = new SortedList<string, ModelBase>();
      this.CreatedAt = (DateTime?)null;
    }
  }

  public static class CacheEntryExtensionMethods
  {
    public static List<string> ToValueList(this CacheEntry cacheEntry, string fieldName, bool required)
    {
      var list = new List<string>();

      if (!required)
        list.Add(String.Empty);

      foreach(ModelBase model in cacheEntry.ModelSet.Values)
      {
        if (!model.PropertyInfoPairSet.ContainsKey(fieldName))
          throw new Exception("PropertyInfoPairSet for model '" + cacheEntry.ModelName + "' does not contain an entry for " +
                              "field named '" + fieldName + " - cannot create ValueList.");
        PropertyInfoPair pip = model.PropertyInfoPairSet[fieldName];
        object value = pip.ModelPropertyInfo.GetValue(model);
        if (value != null)
          list.Add(value.ToString());
      }

      list.Sort();

      return list;
    }

  }
}
