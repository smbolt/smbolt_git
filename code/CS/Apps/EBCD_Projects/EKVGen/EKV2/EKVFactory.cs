using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Security.Cryptography;
using Adsdi.GS;
using Adsdi.EKV2;


namespace Adsdi
{
    [Serializable]
    public class EKVFactory
    {
        public string Key { get; set; }

        private Encryptor encryptor = new Encryptor();
        private ResourceManager resourceManager;

        public EKVFactory(string Password)
        {
            Key = BuildKeyFromPassword(Password);
            resourceManager = new ResourceManager("EKV2.Resource1", Assembly.GetExecutingAssembly());
        }

        private string BuildKeyFromPassword(string Password)
        {
            StringBuilder sb = new StringBuilder();
            string strToHash = Password.Trim();

            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(strToHash));

            while (sb.ToString().Length < 48)
            {
                for (int i = 0; i < hashedData.Length; i++)
                {
                    int c = Convert.ToInt32(hashedData[i]);
                    if((c > 47 && c < 58) || (c > 64 && c < 90) || (c > 96 && c < 123))
                        sb.Append(Convert.ToChar(hashedData[i]));
                    if (sb.Length == 48)
                        break;
                }

                strToHash += sb.ToString();
                hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(strToHash));
            }

            return sb.ToString();
        } 

        public string GetKey()
        {
            return Key;
        }


        public EKV GetEKV()
        {
            Initialize_Encryption();
            byte[] ekvByteArray = (byte[])resourceManager.GetObject("ekv");
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            string ekvString = encoding.GetString(ekvByteArray);
            return DeserializeEKV(ekvString);
        }

        private void Initialize_Encryption()
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] key = encoding.GetBytes(Key);
            encryptor.Initialize_EncryptionKeys(key);
        }

        private EKV DeserializeEKV(string ekvString)
        {
            Adsdi.EKV2.EKV ekv = new EKV();
            ekv.EKVBytes = encryptor.DecryptByteArray(ekvString);
            return ekv;
        }
    }
}

