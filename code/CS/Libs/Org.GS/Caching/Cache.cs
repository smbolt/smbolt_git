using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Cache
  {
    public Dictionary<string, object> Sets {
      get {
        return Get_Sets();
      }
    }

    public object this[string key]
    {
      get {
        return this.GetValue(key);
      }
      set {
        this.SetValue(key, value);
      }
    }

    public bool ContainsKey(string key)
    {
      return this[key] != null;
    }

    private object GetValue(string key)
    {
      MemoryCache memoryCache = MemoryCache.Default;
      if (!memoryCache.Contains(key))
        return String.Empty;

      return memoryCache.Get(key);
    }

    private bool SetValue(string key, object value)
    {
      MemoryCache memoryCache = MemoryCache.Default;
      DateTimeOffset offset = new DateTimeOffset(new DateTime(2050, 1, 1));
      if (memoryCache.Contains(key))
      {
        memoryCache[key] = value;
        return true;
      }

      return memoryCache.Add(key, value, offset);
    }

    public void Delete(string key)
    {
      MemoryCache memoryCache = MemoryCache.Default;
      if (memoryCache.Contains(key))
      {
        memoryCache.Remove(key);
      }
    }

    public void Clear()
    {
      MemoryCache memoryCache = MemoryCache.Default;
      List<string> allKeys = new List<string>();
      for (int i = 0; i < memoryCache.Count(); i++)
        allKeys.Add(memoryCache.ElementAt(i).Key);

      foreach (string key in allKeys)
        memoryCache.Remove(key);
    }

    public Dictionary<int, string> AsDictionary(string key)
    {
      object entry = this[key];
      if (entry == null)
        throw new Exception("Cache item '" + key + "' does not exist, cannot be cast to type 'Dictinary<int,string>'.");

      string typeName = entry.GetType().Name;

      if (typeName != "Dictionary`2")
        throw new Exception("Cache item '" + key + "' of type '" + typeName + "' cannot be cast to type 'Dictinary<int,string>'.");

      return (Dictionary<int, string>)entry;
    }

    public Dictionary<string, object> Get_Sets()
    {
      MemoryCache memoryCache = MemoryCache.Default;
      var cacheSets = memoryCache.Where(m => m.Value != null);

      Dictionary<string, object> sets = new Dictionary<string, object>();
      foreach (var cacheSet in cacheSets)
      {
        sets.Add(cacheSet.Key, cacheSet.Value);
      }

      return sets;
    }

  }
}