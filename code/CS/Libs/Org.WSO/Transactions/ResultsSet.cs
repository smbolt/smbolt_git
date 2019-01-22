using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Org.GS;

namespace Org.WSO.Transactions
{
  [Serializable]
  public class ResultsSet : SerializableObject
  {
    public string StringProperty {
      get;
      set;
    }
    public int IntegerProperty {
      get;
      set;
    }
    public DateTime DateTimeProperty {
      get;
      set;
    }
    public decimal DecimalProperty {
      get;
      set;
    }

    public override string Serialize()
    {

      var ms = new MemoryStream();

      IFormatter f = new BinaryFormatter();
      f.Serialize(ms, this);


      return String.Empty;
    }
  }
}
