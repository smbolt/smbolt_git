using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using Adsdi.EKV2;
using Adsdi.EKV2Gen;
using Adsdi.GS;

namespace Adsdi.EKVGen
{
    public partial class frmMain : Form
    {
        private byte[] InitializationVector { get; set; }
        private byte[] SaltValue { get; set; }
        private byte[] PassPhrase { get; set; }
        private string HashAlgorithm { get; set; }
        private int PasswordIterations { get; set; }
        private static int KeySize { get; set; }
        private ResourceManager resourceManager;
        private string FullFilePath = @"C:\DevProjects\Main\Source\EBCD_Projects\EKVGen\EKV2\Resources\ekv.bin";

        private string EncryptionKeyToEmbedInEKV = String.Empty;
        private string EncryptionKeyToEncryptEKV = String.Empty;
        private string Password = String.Empty;

        private Encryptor encryptor = new Encryptor();

        private EKV2.EKV EKV;

        string ekvString = String.Empty;
        byte[] ekvBytes;

        string pluggedKey = "E4wWAwi7z7K4973Vtg8REVfv5CcQ6bgGTIbeW8PmMhYDxVtY";
        string key = String.Empty;
        string ks0 = "Hqd8xlhU9aNr3CmSyDMe6s0zWk7nYRtBZbFVuXGIf4jKo1LOv5APcQwT2JigEp";
        string ks1 = String.Empty;
        string ks2 = String.Empty;
        char[] cs = new char[62];
        int[] n1 = new int[48];
        int[] n2 = new int[48];

        string pw = String.Empty;
        int[] p1 = new int[16];
        int[] p2 = new int[16];

        Random r;

        public frmMain()
        {
            InitializeComponent();
            Initialize_Application();
        }

        private void btnGenerateEKV_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            EKVFactory factory = new EKVFactory(Password);
            EncryptionKeyToEncryptEKV = factory.GetKey();

            GenerateCustomSet();
            GenerateEKV();
            GenerateNumberSets();
        }

        private void btnGetEKV_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            EKVFactory factory = new EKVFactory(Password);
            EncryptionKeyToEncryptEKV = factory.GetKey();

            EKV ekv = factory.GetEKV();
            EncryptionKeyToEmbedInEKV = ekv.GetKey(txtPassword.Text.Trim());

            txtEmbedKey.Text = EncryptionKeyToEmbedInEKV;
            txtEncryptKey.Text = EncryptionKeyToEncryptEKV;
        }

        private bool ValidateInput()
        {
            txtPassword.Text = txtPassword.Text.Trim();

            if (txtPassword.Text.Length != 16)
            {
                MessageBox.Show("Password length must be exactly 16 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return false;
            }

            txtEmbedKey.Text = txtEmbedKey.Text.Trim();
            if (txtEmbedKey.Text.Trim().Length > 0)
            {
                if (txtEmbedKey.Text.Length != 48)
                {
                    MessageBox.Show("Key length must be exactly 48 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmbedKey.SelectAll();
                    txtEmbedKey.Focus();
                    return false;
                }
            }

            EncryptionKeyToEmbedInEKV = txtEmbedKey.Text;
            Password = txtPassword.Text;

            return true;
        }


        // This method generates a custom set of character arrays
        private void GenerateCustomSet()
        {
            // Build ks1 from ks0 using Randomization
            for (int i = 0; i < 62; i++)
                cs[i] = '-';

            for (int i = 0; i < 62; i++)
            {
                bool IsCharPlaced = false;
                while (!IsCharPlaced)
                {
                    int j = r.Next(0, 62);
                    if (cs[j] == '-')
                    {
                        cs[j] = ks0[i];
                        IsCharPlaced = true;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 62; i++)
                sb.Append(cs[i].ToString());

            ks1 = sb.ToString();

            // Build ks2 from ks1 using Randomization
            for (int i = 0; i < 62; i++)
                cs[i] = '-';

            for (int i = 0; i < 62; i++)
            {
                bool IsCharPlaced = false;
                while (!IsCharPlaced)
                {
                    int j = r.Next(0, 62);
                    if (cs[j] == '-')
                    {
                        cs[j] = ks1[i];
                        IsCharPlaced = true;
                    }
                }
            }

            sb = new StringBuilder();
            for (int i = 0; i < 62; i++)
                sb.Append(cs[i].ToString());

            ks2 = sb.ToString();
        }

        // This method assembles the constructed and drives the serialization to disk
        private string BuildEKV()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 62; i++)
                sb.Append(Convert.ToInt16(ks1[i]).ToString("000") + Convert.ToInt16(ks2[i]).ToString("000"));

            for (int i = 0; i < 48; i++)
                sb.Append(n1[i].ToString("000"));

            for (int i = 0; i < 48; i++)
                sb.Append(n2[i].ToString("000"));

            for (int i = 0; i < 16; i++)
                sb.Append(p1[i].ToString("000"));

            for (int i = 0; i < 16; i++)
                sb.Append(p2[i].ToString("000"));

            string ekvString1 = sb.ToString();
            int checkSum = 0;
            for (int i = 0; i < ekvString1.Length; i++)
                checkSum += Int32.Parse(ekvString1.Substring(i, 1));

            ekvString1 += checkSum.ToString("00000");

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            ekvBytes = enc.GetBytes(ekvString1);

            EKV e = new EKV();
            e.EKVBytes = ekvBytes;

            SerializeEKV(e, FullFilePath);

            return ekvString1;
        }

        // This method generates the random values for the key and password
        private void GenerateEKV()
        {
            // Generate Key
            r = new Random(GetTicks());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 48; i++)
            {
                int n = r.Next(0, 62);
                sb.Append(ks2.Substring(n, 1));
            }
            key = sb.ToString();


            // Generate Password
            sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                int n = r.Next(0, 62);
                sb.Append(ks2.Substring(n, 1));
            }
            pw = sb.ToString();
            
            // If a desired Password was entered, use it rather than the generated value
            if (txtPassword.Text.Trim().Length > 0)
            {
                pw = txtPassword.Text;
            }


            // If a desired Key was entered, use it rather than the generated value
            if (txtEmbedKey.Text.Trim().Length > 0)
            {
                key = txtEmbedKey.Text;
            }

            EncryptionKeyToEmbedInEKV = key;
        }

        // This method uses the generated key and password values to create a set of integers which are used 
        // in conjunction with the two generated character arrays to reconstitute the key and password.
        private void GenerateNumberSets()
        {
            StringBuilder sb = new StringBuilder();

            // Build the number set "n2" for the key
            for (int i = 0; i < 48; i++)
            {   
                if(i < 16)
                    n2[i] = n1[i] - Convert.ToInt16(key[i]) - Convert.ToInt16(ks2[i]) - i - Convert.ToInt16(ks1.IndexOf(ks2[i])) - Convert.ToInt16(ks1.IndexOf(pw[i]));
                else
                    n2[i] = n1[i] - Convert.ToInt16(key[i]) - Convert.ToInt16(ks2[i]) - i - Convert.ToInt16(ks1.IndexOf(ks2[i]));
            }

            // Build teh number set "p2" for the password
            for (int i = 0; i < 16; i++)
                p2[i] = p1[i] - Convert.ToInt16(pw[i]) - Convert.ToInt16(ks2[i]) - i - Convert.ToInt16(ks1.IndexOf(ks2[i]));

            if (true)
            {
                sb.Append("Key Validation" + Environment.NewLine);
                for (int i = 0; i < key.Length; i++)
                {
                    int check = 0;
                    if (i < 16)
                        check = check = n2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]) + ks1.IndexOf(pw[i]);
                    else
                        check = check = n2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]);

                    int result = n1[i] - check;
                    int pwx = 0;
                    if (i < 16)
                        pwx = Convert.ToInt16(ks1.IndexOf(pw[i]));

                    sb.Append(key[i].ToString() + "   " + Convert.ToInt16(key[i]).ToString("000") +
                        "   " + n1[i].ToString("000") + " - " + Convert.ToInt16(key[i]).ToString("000") + " - " +
                        Convert.ToInt16(ks2[i]).ToString("000") + " - " +
                        i.ToString("00") + " - " + Convert.ToInt16(ks1.IndexOf(ks2[i])).ToString("000") +
                        " - " + pwx.ToString("000") + " = " + n2[i].ToString("000") +
                        "   check = " + check.ToString("000") + "   result = " + result.ToString("000") + " = " + Convert.ToChar(result).ToString() +
                        Environment.NewLine);
                }

                sb.Append(Environment.NewLine + "Password Validation" + Environment.NewLine);
                for (int i = 0; i < pw.Length; i++)
                {
                    int check = p2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]);
                    int result = p1[i] - check;

                    sb.Append(pw[i].ToString() + "   " + Convert.ToInt16(pw[i]).ToString("000") +
                        "   " + p1[i].ToString("000") + " - " + Convert.ToInt16(pw[i]).ToString("000") + " - " +
                        Convert.ToInt16(ks2[i]).ToString("000") + " - " +
                        i.ToString("00") + " - " + Convert.ToInt16(ks1.IndexOf(ks2[i])).ToString("000") + " = " + p2[i].ToString("000") +
                        "   check = " + check.ToString("000") + "   result = " + result.ToString("000") + " = " + Convert.ToChar(result).ToString() +
                        Environment.NewLine);
                }
                sb.Append(Environment.NewLine + Environment.NewLine);
            }

            ekvString = BuildEKV();

            if (true)
            {
                sb.Append("----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----0" +
                    Environment.NewLine);
                int lth = ekvString.Length;
                int charsShown = 0;
                int begin = 0;
                while (charsShown < lth)
                {
                    int charsToShow = lth - charsShown;
                    if (charsToShow > 100)
                        charsToShow = 100;
                    sb.Append(ekvString.Substring(begin, charsToShow) + Environment.NewLine);
                    charsShown += charsToShow;
                    begin += charsToShow;
                }
            }

            sb.Append(Environment.NewLine + Environment.NewLine +
                "             ----+----1----+----2----+----3----+----4----+----5----+----6--" + Environment.NewLine +
                "ks1 =        " + ks1 + Environment.NewLine + Environment.NewLine +
                "             ----+----1----+----2----+----3----+----4----+----5----+----6--" + Environment.NewLine +
                "ks2 =        " + ks2 + Environment.NewLine + Environment.NewLine +
                "             ----+----1----+----2----+----3----+----4----+---" + Environment.NewLine +
                "EmbedKey =   " + key + Environment.NewLine + Environment.NewLine +
                "             ----+----1----+----2----+----3----+----4----+---" + Environment.NewLine +
                "EncryptKey = " + EncryptionKeyToEncryptEKV + Environment.NewLine + Environment.NewLine +
                "             ----+----1----+-" + Environment.NewLine +
                "pw  =        " + pw + Environment.NewLine + Environment.NewLine);

            txtOut.Text = sb.ToString();

            txtEmbedKey.Text = key;
            txtEncryptKey.Text = EncryptionKeyToEncryptEKV;
        }


        # region Initializaton
        private void Initialize_Application()
        {
            txtPassword.Text = "abcdefghijklmnop";
            //txtEmbedKey.Text = pluggedKey;

            r = new Random(GetTicks());

            resourceManager = new ResourceManager("Adsdi.EKVGen.Properties.Resources", Assembly.GetExecutingAssembly());

            for (int i = 0; i < cs.Length; i++)
                cs[i] = '-';

            for (int i = 0; i < 48; i++)
            {
                int n = r.Next(333, 555);
                n1[i] = n;
                n2[i] = 0;
            }

            for (int i = 0; i < 16; i++)
            {
                int n = r.Next(333, 555);
                p1[i] = n;
                p2[i] = 0;
            }
        }

        public int GetTicks()
        {
            DateTime dt = DateTime.Now;
            long ticks = dt.Ticks;
            string ticksString = ticks.ToString();
            int n1 = 0;
            int offset = 4;
            while (n1 == 0)
            {
                n1 = dt.Second * Int32.Parse(ticksString.Substring(ticksString.Length - offset++, 1));
                if (offset > ticksString.Length)
                {
                    n1 = 117;
                    break;
                }
            }

            return n1;
        }

        #endregion

        #region Serialization and Encryption
        private void SerializeEKV(EKV ekv, string FullFilePath)
        {
            Initialize_Encryption();
            string encryptedEKV = encryptor.EncryptByteArray(ekv.EKVBytes);
            StreamWriter sw = new StreamWriter(FullFilePath);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            sw.Write(encryptedEKV);
            sw.Close();
        }

        private void Initialize_Encryption()
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] key = encoding.GetBytes(EncryptionKeyToEncryptEKV);
            encryptor.Initialize_EncryptionKeys(key);
        }

        #endregion

        private void ckDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDebug.Checked)
                this.Size = new Size(956,609);
            else 
                this.Size = new Size(467,255);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtEmbedKey.Clear();
            txtEncryptKey.Clear();
        }

        private void btnTestGenerator_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            txtOut.Text = Generator.GenerateEKV(Password, EncryptionKeyToEmbedInEKV);
        }

    }
}
