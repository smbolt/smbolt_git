using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XMapHelper : IDisposable
  {
    public Type GetType(string className)
    {
      if(XmlMapper.Types == null || !XmlMapper.Types.ContainsKey(className))
        return null;

      return XmlMapper.Types[className];
    }

    public void Dispose()
    {

    }
  }
}
