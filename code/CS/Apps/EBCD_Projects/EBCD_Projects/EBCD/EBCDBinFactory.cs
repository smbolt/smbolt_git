using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using Adsdi.GS;

namespace Adsdi.EBCD
{
  [Serializable]
  public class EBCDBinFactory
  {
    public string Key {
      get;
      set;
    }
    private Encryptor _encryptor = new Encryptor();
    private ResourceManager resourceManager;

    public EBCDBinFactory()
    {
      resourceManager = new ResourceManager("Adsdi.EBCD.Resource1", Assembly.GetExecutingAssembly());
    }

    public string GetKey()
    {
      return Key;
    }

    public EBCDBin GetEBCDBin()
    {
      byte[] binByteArray = (byte[])resourceManager.GetObject("bin");
      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      string binString = encoding.GetString(binByteArray);
      return DeserializeEBCDBin(binString);
    }

    private EBCDBin DeserializeEBCDBin(string binString)
    {
      EBCDBin bin = new EBCDBin();
      MemoryStream memStream = new MemoryStream(_encryptor.DecryptByteArray(binString));
      IFormatter formatter = new BinaryFormatter();
      bin = (EBCDBin)formatter.Deserialize(memStream);
      memStream.Close();
      return bin;
    }
  }
}

