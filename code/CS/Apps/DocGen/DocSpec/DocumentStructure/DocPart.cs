using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class DocPart
    {
        public string DocPartType { get; set; }
        public string Id { get; set; }
        public string Target { get; set; }
        public XElement DocPartXml { get; set; }

        public string DocPartPath 
        {
            get { return Get_DocPartPath(); }
        }

        public string DocPartFileName 
        {
            get { return Get_DocPartFileName(); }
        }

        public DocPart(string docPartType, string id, string target)
        {
            this.DocPartType = docPartType;
            this.Id = id;
            this.Target = target;
            this.DocPartXml = new XElement("Empty");
        }

        private string Get_DocPartPath()
        {
            if (this.Target == null)
                return String.Empty;

            return Path.GetDirectoryName(this.Target);
        }

        private string Get_DocPartFileName()
        {
            if (this.Target == null)
                return String.Empty;

            return Path.GetFileName(this.Target);
        }

    }
}
