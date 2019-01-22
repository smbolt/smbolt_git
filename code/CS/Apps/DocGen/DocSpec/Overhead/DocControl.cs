using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class DocControl
    {
        public DebugControl DebugControl { get; set; }
        public PrintControl PrintControl { get; set; }
        public DocInfo DocInfo { get; set; }

        public DocControl()
        {
            this.DebugControl = new DebugControl();
            this.PrintControl = new PrintControl();
            this.DocInfo = new DocInfo();
        }
    }
}
