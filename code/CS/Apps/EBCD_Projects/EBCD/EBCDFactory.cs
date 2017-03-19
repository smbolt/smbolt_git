using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Adsdi.GS;

namespace Adsdi.EBCD
{
    [Serializable]
    public class EBCDFactory
    {
        public string Key { get; set; }

        private Encryptor _encryptor = new Encryptor();

        private ResourceManager resourceManager;
        public EBCDFactory()
        {
            resourceManager = new ResourceManager("Adsdi.EBCD.Resource1", Assembly.GetExecutingAssembly());
        }

        public string GetKey()
        {
            return Key;
        }

        public EBCD GetEBCD()
        {
            byte[] ebcdByteArray = (byte[])resourceManager.GetObject("ebcd");
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string ebcdString = encoding.GetString(ebcdByteArray);
            return DeserializeEBCD(ebcdString);
        }


        private EBCD DeserializeEBCD(string ebcdString)
        {
            EBCD ebcd = new EBCD();
            MemoryStream memStream = new MemoryStream(_encryptor.DecryptByteArray(ebcdString));
            IFormatter formatter = new BinaryFormatter();
            ebcd = (EBCD)formatter.Deserialize(memStream);
            memStream.Close();
            return ebcd;
        }
    }
}

