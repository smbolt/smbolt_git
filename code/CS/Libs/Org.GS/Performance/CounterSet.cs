using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element, CollectionElements="Counter")]
  public class CounterSet : SortedList<string, Counter>
  {
    public bool IsLoaded { get; set; }

    public CounterSet()
    {
        this.IsLoaded = false;
    }

    public string GetKeyForCounterName(string counterNameIn)
    {
      string key = String.Empty;
      string instanceName = String.Empty;
      string categoryName = String.Empty;
      string counterName = String.Empty;

      if (counterNameIn.Contains("[") && counterNameIn.Contains("]"))
      {
        string[] keyParts = counterNameIn.Split(new char[] { ']' }, StringSplitOptions.RemoveEmptyEntries);

        if (keyParts.Length != 2)
          throw new Exception("Invalid counter name encountered '" + counterNameIn + "'."); 

        instanceName = keyParts[0].Replace("[", String.Empty).Trim();
        if (instanceName.IsBlank())
          throw new Exception("Invalid counter name encountered '" + counterNameIn + "' - blank instance name."); 

        counterName = keyParts[1].Trim(); 

        if (counterName.IsBlank())
          throw new Exception("Invalid counter name encountered '" + counterNameIn + "'."); 
      }
      else
      {
        counterName = counterNameIn;  
      }

      foreach (Counter c in this.Values)
      {
        if (c.CounterName == counterName)
        {
          if (instanceName.IsNotBlank())
          {
            if (c.InstanceName == instanceName)
            {
                return c.Key; 
            }
          }
          else
          {
            return c.Key;
          }
        }
      }

      throw new Exception("Could not locate counter '" + counterNameIn + "' in CounterSet."); 
    }
  }
}
