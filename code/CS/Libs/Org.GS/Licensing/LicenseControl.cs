using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Licensing
{
  public class LicenseControl
  {
    private Encryptor encryptor; 

    public string Version { get; set; }
    //public LicenseStatus LicenseStatus { get; set; }
    public DateTime LastCheck { get; set; }
    public string LicenseID { get; set; }

    public ComputerSignature ComputerSignature { get; set; }


    public LicenseControl()
    {
      this.encryptor = new Encryptor();
      this.Version = String.Empty;
      //this.LicenseStatus = LicenseStatus.NotSet;
      this.LastCheck = DateTime.MinValue;
      this.LicenseID = String.Empty;

      this.ComputerSignature = new ComputerSignature();

    }

    public void LoadFromPath(string path)
    {
      string fileName = path + @"\pl.xmlx";
      string unencryptedFileName = fileName.Replace(".xmlx", ".xml");

      if (File.Exists(unencryptedFileName))
        File.WriteAllText(fileName, this.encryptor.EncryptString(File.ReadAllText(unencryptedFileName)));

      string xml = this.encryptor.DecryptString(File.ReadAllText(fileName));
      XElement lcElement = XElement.Parse(xml);
    }
  }
}
