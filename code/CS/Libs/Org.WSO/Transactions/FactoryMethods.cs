using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Org.GS;


namespace Org.WSO.Transactions
{
  public static class FactoryMethods
  {
    public static T ToTransaction<T>(this JObject j)
    {
      try
      {
        if (j == null)
          return default(T);

        return j.ToObject<T>();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to convert a message transaction from a JObject to " +
                            "the type ' GET THE TYPE HERE '.", ex);
      }
    }

  }
}
