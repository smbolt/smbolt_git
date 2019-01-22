using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class DocNameManager
    {
        private SortedList<string, List<string>> NameBank;

        public DocNameManager()
        {
            this.NameBank = new SortedList<string, List<string>>();
        }

        public string GetNextName(DocumentElement e)
        {
            string prefix = this.GetPrefix(e);

            if (!this.NameBank.ContainsKey(prefix))
                this.NameBank.Add(prefix, new List<string>());

            string newName = prefix + this.NameBank[prefix].Count.ToString();
            this.NameBank[prefix].Add(newName);

            return newName;
        }

        private string GetPrefix(DocumentElement e)
        {
            Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(e.GetType(), typeof(Meta));

            if (metaAttribute != null)
                return metaAttribute.OxName;

            return e.GetType().Name.CamelCase();
        }
    }
}
