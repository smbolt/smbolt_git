using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class HtoFactory : IDisposable
  {

    public HtoFactory()
    {

    }

    public Hto CreateFromJsonFile(string path)
    {
      try
      {
        if (path.IsBlank())
          throw new Exception("The file path specified is blank or null.");

        if (!File.Exists(path))
          throw new Exception("The file does not exist at the path specified '" + path + "'.");

        string rawInput = File.ReadAllText(path);

        JToken jt = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject<object>(rawInput);

        var hto = jt.ToHto();

        return hto;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create an Hto object from the JSON file '" + path + "'.", ex);
      }
    }

    public void Dispose()
    {

    }
  }
}
