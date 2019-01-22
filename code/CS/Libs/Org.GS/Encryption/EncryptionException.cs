using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Org.GS
{
  [Serializable]
  public class EncryptionException : System.Exception
  {
    public EncryptionException(string message)
      : base(message)
    {
    }

    public override void GetObjectData(SerializationInfo info,
                                       StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}
