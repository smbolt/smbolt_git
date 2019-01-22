using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Adsdi.EKV2Gen;
using Adsdi.EBCD;
using Adsdi.WinAppSupport;

namespace EDBCBatch
{
    class Program
    {
        private static string keyToEncrypt = String.Empty;

        static void Main(string[] args)
        {            
            Initialize_Application();

            GenerateEKV();
            GenerateEBCD();
            CreateResourcesFile();

            Terminate_Application();
        }

        private static void GenerateEKV()
        {
            Console.WriteLine("");
            Console.WriteLine("EKV Generation Starting");
            Console.WriteLine("Reading EKVDEF XML File");
            Console.WriteLine("");
            string password = String.Empty;
            string keyToEmbed = String.Empty;
            string elementName = String.Empty;

            XmlTextReader r = new XmlTextReader(@"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Input\EKVDEF.xml");
            while (r.Read())
            {
                switch (r.NodeType)
                {
                    case XmlNodeType.Element:
                        elementName = r.Name;
                        break;

                    case XmlNodeType.Text:
                        switch(elementName)
                        {
                            case "Password":
                                password = r.Value.Trim();
                                break;
                            case "KeyToEmbed":
                                keyToEmbed = r.Value.Trim();
                                break;
                        }
                        break;
                }
            }
            r.Close();

            keyToEncrypt = Generator.GenerateEKV(password, keyToEmbed);
            Console.WriteLine(" -Password     : " + password);
            Console.WriteLine(" -KeyToEmbed   : " + keyToEmbed);
            Console.WriteLine(" -KeyToEncrypt : " + keyToEncrypt);
            Console.WriteLine("");
            Console.WriteLine("EKV Generation Complete");

        }

        private static void GenerateEBCD()
        {
            Console.WriteLine("");
            Console.WriteLine("EBCD Generation Starting");
            Console.WriteLine("Reading EBCD Definition XML File");

            string name = String.Empty;
            string version = String.Empty;
            string key = String.Empty;
            string value = String.Empty;
            SortedList<string, string> Config = new SortedList<string, string>();

            string elementName = String.Empty;

            XmlTextReader r = new XmlTextReader(@"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Input\EBCDDEF.xml");
            while (r.Read())
            {
                switch (r.NodeType)
                {
                    case XmlNodeType.Element:
                        elementName = r.Name;
                        break;

                    case XmlNodeType.Text:
                        switch (elementName)
                        {
                            case "Name":
                                name = r.Value.Trim();
                                Console.WriteLine("EBCD Name   : " + name);
                                break;
                            case "Version":
                                version = r.Value.Trim();
                                name = r.Value.Trim();
                                Console.WriteLine("EBCD Version: " + version);
                                Console.WriteLine("");
                                break;
                            case "Key":
                                key = r.Value.Trim();
                                Console.WriteLine("  Key       : " + key);
                                break;
                            case "Value":
                                value = r.Value.Trim();
                                Config.Add(key, value);
                                Console.WriteLine("  Value     : " + value);
                                Console.WriteLine("");
                                break;
                        }
                        break;
                }
            }
            r.Close();

            EBCD ebcd = new EBCD();
            ebcd.Name = name;
            ebcd.Version = version;
            ebcd.BuildDate = DateTime.Now;
            ebcd.Config = Config;


            Initialize_Encryption(keyToEncrypt);
            SerializeEBCD(ebcd, @"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Output\ebcd.bin");

            Console.WriteLine("");
            Console.WriteLine("EBCD Generation Complete");
        }

        private static void CreateResourcesFile()
        {
            Console.WriteLine("");
            Console.WriteLine("Generating Resources File");

            IResourceWriter writer = new ResourceWriter(@"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Output\EBCD.Resources");
            byte[] ekvBytes = File.ReadAllBytes(@"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Output\ekv.bin");
            writer.AddResource("ekv", ekvBytes);
            byte[] ebcdBytes = File.ReadAllBytes(@"C:\DevProjects\Main\Source\EBCD_Projects\EBCD_Data\Output\ebcd.bin");
            writer.AddResource("ebcd", ebcdBytes);
            writer.Close();

            Console.WriteLine("");
            Console.WriteLine("Resources File Generation Complete");
        }


        private static void Initialize_Encryption(string key)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(key);
            Encryption.Initialize_EncryptionKeys(keyBytes);
        }

        private static void SerializeEBCD(EBCD ebcd, string FullFilePath)
        {
            MemoryStream memStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memStream, ebcd);
            string encryptedEBCD = Encryption.EncryptByteArray(memStream.GetBuffer());
            StreamWriter sw = new StreamWriter(FullFilePath);
            sw.Write(encryptedEBCD);
            memStream.Close();
            sw.Close();
        }
        
        private static void Initialize_Application()
        {
            Console.WriteLine("*** EDBC BATCH BUILD STARTED AT " + DateTime.Now.ToString());
            Console.WriteLine("");
        }



        private static void Terminate_Application()
        {
            Console.WriteLine("");
            Console.WriteLine("** EDBC BATCH BUILD ENDED AT " + DateTime.Now.ToString());
            Console.ReadKey();
        }

    }
}
