using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Security.Cryptography;

namespace Adsdi.EKV2
{
    [Serializable]
    public class EKV
    {
        public byte[] EKVBytes { get; set; }

        private string key = String.Empty;
        private string ks1 = String.Empty;
        private string ks2 = String.Empty;
        private int[] n1;
        private int[] n2;
        private int[] p1;
        private int[] p2;

        public EKV()
        {
        }

        public string GetKey(String Password)
        {
            Initialize_Arrays();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sbKey = new StringBuilder();
            StringBuilder sbPassword = new StringBuilder();

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            string ekvString = enc.GetString(EKVBytes);

            for (int i = 0; i < 62; i++)
            {
                sb1.Append(Convert.ToChar(Convert.ToInt32(ekvString.Substring(i * 6, 3))));
                sb2.Append(Convert.ToChar(Convert.ToInt32(ekvString.Substring(i * 6 + 3, 3))));
                if (i < 48)
                {
                    n1[i] = Convert.ToInt32(ekvString.Substring(372 + i * 3, 3));
                    n2[i] = Convert.ToInt32(ekvString.Substring(516 + i * 3, 3));
                }
                if (i < 16)
                {
                    p1[i] = Convert.ToInt32(ekvString.Substring(660 + i * 3, 3));
                    p2[i] = Convert.ToInt32(ekvString.Substring(708 + i * 3, 3));
                }
            }

            ks1 = sb1.ToString();
            ks2 = sb2.ToString();
            sb.Append("ks1 = " + ks1 + Environment.NewLine + Environment.NewLine + "ks2 = " + ks2 + Environment.NewLine + Environment.NewLine);

            for (int i = 0; i < n1.Length; i++)
            {
                int check = 0;

                if(i < 16)
                    check = n2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]) + ks1.IndexOf(Password[i]);
                else
                    check = n2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]);

                int result = n1[i] - check;

                sbKey.Append(Convert.ToChar(result).ToString());

                int pwx = 0;
                if (i < 16)
                    pwx = Convert.ToInt16(ks1.IndexOf(Password[i]));

                sb.Append(
                    n1[i].ToString("000") + " - " + Convert.ToInt16(ks2[i]).ToString("000") + " - " +
                    i.ToString("00") + " - " + Convert.ToInt16(ks1.IndexOf(ks2[i])).ToString("000") +
                    " - " + pwx.ToString("000") + " = " + n2[i].ToString("000") +
                    "   check = " + check.ToString("000") + "   result = " + result.ToString("000") + " = " + Convert.ToChar(result).ToString() +
                    Environment.NewLine);
            }
            sb.Append(Environment.NewLine + Environment.NewLine + "Key = " + sbKey.ToString());

            for (int i = 0; i < p1.Length; i++)
            {
                int check = p2[i] + ks2[i] + i + ks1.IndexOf(ks2[i]);
                int result = p1[i] - check;

                sbPassword.Append(Convert.ToChar(result).ToString());

                sb.Append(
                    p1[i].ToString("000") + " - " + Convert.ToInt16(ks2[i]).ToString("000") + " - " +
                    i.ToString("00") + " - " + Convert.ToInt16(ks1.IndexOf(ks2[i])).ToString("000") + " = " + p2[i].ToString("000") +
                    "   check = " + check.ToString("000") + "   result = " + result.ToString("000") + " = " + Convert.ToChar(result).ToString() +
                    Environment.NewLine);
            }
            sb.Append(Environment.NewLine + Environment.NewLine + "Password = " + sbPassword.ToString());

            string password = sbPassword.ToString();
            if (password != Password)
                return "PASSWORD_INVALID";

            return sbKey.ToString();
        }

        private void Initialize_Arrays()
        {
            n1 = new int[48];
            n2 = new int[48];
            p1 = new int[16];
            p2 = new int[16];

            for (int i = 0; i < 48; i++)
            {
                n1[i] = 0;
                n2[i] = 0;
                if (i < 16)
                {
                    p1[i] = 0;
                    p2[i] = 0;
                }
            }
        }

    }

}
