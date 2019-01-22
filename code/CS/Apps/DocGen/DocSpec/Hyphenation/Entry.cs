using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class Entry
    {
        public string Text { get; set; }
        public string Flags { get; set; }

        public Entry(string[] tokens)
        {
            this.Text = tokens[0];
            if (tokens.Length > 1)
                this.Flags = tokens[1];
            else
                this.Flags = String.Empty;
        }
    }
}
