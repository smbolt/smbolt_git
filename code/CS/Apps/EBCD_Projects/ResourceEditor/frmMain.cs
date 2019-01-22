using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Adsdi.EKV2;
using Adsdi.EBCD;
using Adsdi.WinAppSupport;

namespace Adsdi.Tools.ResourceEditor
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
      Initialize_Application();
    }

    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      if (Terminate_Application())
        this.Close();
    }

    private void Initialize_Application()
    {
      txtEncryptionKey.Text = "6rhgUrNbtnf2jlxxSNyzwPGDr7xUnwntSoOhiDfHr6z5DnAh";
    }

    private bool Terminate_Application()
    {
      return true;
    }

    private void btnGetResourceFile_Click(object sender, EventArgs e)
    {
      dlgFileOpen.InitialDirectory = @"C:\_projects\Adsdi_Tools\EDBCBatch\Data\Output\";
      dlgFileOpen.Multiselect = false;
      if (dlgFileOpen.ShowDialog() == DialogResult.OK)
        txtResourceFile.Text = dlgFileOpen.FileName;

      ShowResourcesInFile(txtResourceFile.Text);
    }

    private void ShowResourcesInFile(string ResourceFile)
    {
      lbResources.Items.Clear();

      IResourceReader reader = new ResourceReader(ResourceFile);
      IDictionaryEnumerator en = reader.GetEnumerator();

      while (en.MoveNext())
      {
        lbResources.Items.Add(en.Key);
      }
      reader.Close();
    }

    private void btnShowResource_Click(object sender, EventArgs e)
    {
      if (lbResources.SelectedItems.Count == 0)
        return;

      string itemName = lbResources.SelectedItems[0].ToString();
      switch (itemName)
      {
        case "ekv":
          DisplayEKV();
          break;


        case "ebcd":
          DisplayEBCD();
          break;
      }
    }

    private void DisplayEKV()
    {
      byte[] ekvBytes = new byte[0];

      IResourceReader reader = new ResourceReader(txtResourceFile.Text);
      IDictionaryEnumerator en = reader.GetEnumerator();

      while (en.MoveNext())
      {
        if (en.Key.ToString() == "ekv")
        {
          ekvBytes = (byte[])en.Value;
          break;
        }
      }
      reader.Close();

      EKV ekv = GetEKV(ekvBytes);
      string key = ekv.GetKey("t9Qw5pXm7i2z4n1j");
    }

    private EKV GetEKV(byte[] ekvByteArray)
    {
      Initialize_Encryption();
      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      string ekvString = encoding.GetString(ekvByteArray);
      return DeserializeEKV(ekvString);
    }

    private void Initialize_Encryption()
    {
      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      byte[] key = encoding.GetBytes(txtEncryptionKey.Text.Trim());
      Encryption.Initialize_EncryptionKeys(key);
    }

    private EKV DeserializeEKV(string ekvString)
    {
      Adsdi.EKV2.EKV ekv = new EKV();
      ekv.EKVBytes = Encryption.DecryptByteArray(ekvString);
      return ekv;
    }

    private void DisplayEBCD()
    {
      byte[] ebcdBytes = new byte[0];

      IResourceReader reader = new ResourceReader(txtResourceFile.Text);
      IDictionaryEnumerator en = reader.GetEnumerator();

      while (en.MoveNext())
      {
        if (en.Key.ToString() == "ebcd")
        {
          ebcdBytes = (byte[])en.Value;
          break;
        }
      }
      reader.Close();

      EBCD.EBCD ebcd = GetEBCD(ebcdBytes);

    }

    private EBCD.EBCD GetEBCD(byte[] ebcdByteArray)
    {
      Initialize_Encryption();
      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
      string ebcdString = encoding.GetString(ebcdByteArray);
      return DeserializeEBCD(ebcdString);
    }

    private EBCD.EBCD DeserializeEBCD(string ebcdString)
    {
      EBCD.EBCD ebcd = new EBCD.EBCD();
      MemoryStream memStream = new MemoryStream(Encryption.DecryptByteArray(ebcdString));
      IFormatter formatter = new BinaryFormatter();
      ebcd = (EBCD.EBCD)formatter.Deserialize(memStream);
      memStream.Close();
      return ebcd;
    }


  }
}
