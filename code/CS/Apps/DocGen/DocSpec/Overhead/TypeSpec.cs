using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class TypeSpec
    {
        public Type Type { get; set; }
        public string Spec { get; set; }

        public TypeSpec(string typeSpec)
        {
            if (typeSpec.CharCount('(') != 1 || typeSpec.CharCount(')') != 1)
                throw new Exception("Document query TypeSpec'" + typeSpec + "' is not valid.  Must include one and only one of each of the characters '(' and ')'.");

            List<string> tokens = typeSpec.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (tokens.Count < 1)
                throw new Exception("Document query TypeSpec '" + typeSpec + "' is not valid.  Must have the form of  'DocumentElementType(specification)'.");

            string typeName = tokens[0];

            Assembly assembly = Assembly.GetExecutingAssembly();
            this.Type = assembly.GetType("Org.DocGen.DocSpec." + typeName);

            if (this.Type == null)
                throw new Exception("The first token of the document query TypeSpec '" + typeSpec + "' is not a valid DocumentElement type (DocSpec.DeType).  Must have the form of  'DocumentElementType(specification)'.");

            if (tokens.Count > 1)
                this.Spec = tokens[1];
            else
                this.Spec = String.Empty;
        }
    }
}
