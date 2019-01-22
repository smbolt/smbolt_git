using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Teleflora.Operations;

namespace Teleflora.Operations.MetricView
{
  public static class ConfigHelper
  {

    public static MetricViewConfiguration GetConfiguration(string fileName)
    {
      MetricViewConfiguration config = new MetricViewConfiguration();

      if (File.Exists(fileName))
      {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        config = (MetricViewConfiguration)formatter.Deserialize(stream);
        stream.Close();
      }
      else
      {


      }
      return config;
    }

    public static void PutConfiguration(MetricViewConfiguration config, string fileName)
    {
      if (fileName.CompareTo("New Profile") == 0)
        return;

      string configPath = Path.GetDirectoryName(fileName);
      if (!Directory.Exists(Path.GetDirectoryName(fileName)))
        Directory.CreateDirectory(Path.GetDirectoryName(fileName));

      FileStream stream = new FileStream(fileName, FileMode.Create);
      IFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, config);
      stream.Close();
    }



  }
}
