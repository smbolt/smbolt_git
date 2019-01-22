using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class DocInfo
    {
        public string PackagePath { get; set; }
        public string PackageName { get; set; }
        public string FullDocxPath { get; set; }
        public string CompanyName { get; set; }
        public string WordXmlPath { get { return Get_WordXmlPath(); } }

        public DocInfo() { }

        public DocInfo(string docPath, string packagePath, string packageName, string companyName)
        {
            this.PackagePath = packagePath;
            this.PackageName = packageName;
            this.FullDocxPath = docPath + @"\" + packageName.Trim() + ".docx";
            this.CompanyName = companyName;
        }

        private string Get_WordXmlPath()
        {
            string path = this.PackagePath + @"\" + this.PackageName + @"\word\w-document.xml"; 
            return path;
        }
    }
}
