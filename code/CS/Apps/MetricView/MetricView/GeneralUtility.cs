using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public static class GeneralUtility
    {
        private const string _applicationName = "MetricView";
        private const string _applicationVersion = "1.0";
        private const string _applicationBuild = "16";

        public static string GetApplicationBuildString()
        {
            return _applicationName + " " +
                _applicationVersion + "." +
                _applicationBuild;
        }

        public static string StripSeqFromName(string name)
        {
            if (name.Length > 7 && name.Substring(0, 2) == "~[")
                return name.Substring(7, name.Length - 7);

            return name;
        }

        public static string GetSeqFromName(string name)
        {
            if (name.Length > 7 && name.Substring(0, 2) == "~[")
                return name.Substring(0, 7);

            return String.Empty;
        }

        public static bool IsNumeric(string number, bool IncludesDecimalPoint)
        {
            string n = number.Trim();
            int decimalPos = n.IndexOf('.');

            if (!IncludesDecimalPoint && decimalPos != -1)
                return false;

            if (decimalPos > -1)
            {
                n = n.Replace(".", String.Empty);
                decimalPos = n.IndexOf('.');
                if (decimalPos != -1)
                    return false;
            }

            foreach (char c in n)
            {
                if (c != '0' && c != '1' && c != '2' && c != '3' && c != '4' &&
                    c != '5' && c != '6' && c != '7' && c != '8' && c != '9')
                    return false;
            }

            return true;
        }


    }
}
