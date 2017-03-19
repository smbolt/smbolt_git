using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.DB
{
  public class CacheManager
  {
    public Dictionary<string, CacheEntry> CacheEntries { get; set; }

    public CacheManager()
    {
      this.CacheEntries = new Dictionary<string, CacheEntry>();
    }

    public CacheEntry GetModelCache(string modelName, string modelKey, RepositoryBase repositoryBase)
    {
      try
      {
        if (!this.CacheEntries.ContainsKey(modelName))
        {
          var cacheEntry = BuildCacheEntry(modelName, modelKey, repositoryBase);
          if (cacheEntry == null)
            throw new Exception("Failed to build cache entry for model '" + modelName + "' with key '" + modelKey + "'."); 
          this.CacheEntries.Add(modelName, cacheEntry);
        }

        return this.CacheEntries[modelName];
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to return the cache entry for model name '" + modelName + ".", ex); 
      }
    }

    private CacheEntry BuildCacheEntry(string modelName, string modelKey, RepositoryBase repositoryBase)
    {
      try
      {
        IList<ModelBase> modelList = repositoryBase.GetList(modelName, modelKey); 
        if (modelList.Count() > 0)
        {
          Type modelType = modelList[0].GetType(); 
          PropertyInfo keyPi = modelType.GetProperty(modelKey); 
          if (keyPi == null)
            throw new Exception("Could not locate the key property '" + modelKey + "' from model type '" + modelType.FullName + "'."); 
        
          var cacheEntry = new CacheEntry();
          cacheEntry.ModelName = modelName; 

          foreach(var model in modelList)
          {
            object keyObject = keyPi.GetValue(model); 
            if (keyObject == null)
              throw new Exception("The value of the key for the model of type + '" + modelType.FullName + "' is null - cache for this model cannot be built."); 

            string keyString = keyObject.ToString().Trim();
            if (cacheEntry.ModelSet.ContainsKey(keyString))
              throw new Exception("A duplicate key value '" + keyString + "' has been found in the set of models for type '" + 
                                   modelType.FullName + "' - cache for this model cannot be built.");
            cacheEntry.ModelSet.Add(keyString, model); 
          }

          cacheEntry.CreatedAt = DateTime.Now; 
          return cacheEntry; 
        }
        
        return null; 
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build the cache entry for model name '" + modelName + "' with key value '" + modelKey + "'.", ex); 
      }
    }
  }
}
