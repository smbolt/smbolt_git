using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [Serializable]
  public class SerializableObject
  {
    public virtual string Serialize()
    {
      return String.Empty;
    }
  }
}
