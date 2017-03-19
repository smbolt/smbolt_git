using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [XMap(XType = XType.Element)]
  public class ObjectWrapper
  {
    [XMap(IsRequired = true)]
    public string AssemblyQualifiedName { get; set; }
    [XMap]
    public string Object { get; set; }

    public string ObjectTypeShort { get { return Type.GetType(this.AssemblyQualifiedName).Name; } }

    public ObjectWrapper() { }
    public ObjectWrapper(object obj)
    {
      SetObject(obj);
    }

    public void SetObject(object obj)
    {
      if (obj == null)
        return;

      MemoryStream memoryStream = new MemoryStream();
      IFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Serialize(memoryStream, obj);
      byte[] streamBytes = memoryStream.ToArray();
      memoryStream.Dispose();
      memoryStream = null;

      this.AssemblyQualifiedName = obj.GetType().AssemblyQualifiedName;
      this.Object = Convert.ToBase64String(streamBytes);
    }

    public object GetObject()
    {
      byte[] objBytes = Convert.FromBase64String(this.Object);

      MemoryStream memoryStream = new MemoryStream(objBytes);
      IFormatter binaryFormatter = new BinaryFormatter();
      object obj = binaryFormatter.Deserialize(memoryStream);
      memoryStream.Dispose();
      memoryStream = null;

      return obj;
    }
  }
}
