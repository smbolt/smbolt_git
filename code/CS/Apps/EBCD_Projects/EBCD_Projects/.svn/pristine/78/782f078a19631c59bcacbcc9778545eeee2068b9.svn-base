using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EBCDBin
    {
        public string Key { get; set; }
        public byte[] CharArray { get; set; }
        private ResourceManager resourceManager;
        private Encryptor _encryptor = new Encryptor();

        public EBCDBin()
        {
            resourceManager = new ResourceManager("Adsdi.EBCD.Resource1", Assembly.GetExecutingAssembly());
        }

        public byte[] GetEKV2()
        {
            return (byte[])resourceManager.GetObject("ekv2");
        }

        public EBCD GetEBCD(string EncryptionKey)
        {
            Key = EncryptionKey;
            Initialize_Encryption();
            byte[] ebcdByteArray = (byte[])resourceManager.GetObject("ebcd");
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string ebcdString = encoding.GetString(ebcdByteArray);
            return DeserializeEBCD(ebcdString);
        }

        public EBCD BuildEBCDFromFile(byte[] EDBCByteArray, string EncryptionKey)
        {
            Key = EncryptionKey;
            Initialize_Encryption();
            byte[] ebcdByteArray = EDBCByteArray;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string ebcdString = encoding.GetString(ebcdByteArray);
            return DeserializeEBCD(ebcdString);
        }

        private void Initialize_Encryption()
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] key = encoding.GetBytes(Key);
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
