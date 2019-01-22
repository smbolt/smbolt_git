using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class PageLayout
    {
        public Rectangle Header { get; set; }
        public Rectangle Main { get; set; }
        public Rectangle Footer { get; set; }

        public PageLayout()
        {
            this.Header = new Rectangle();
            this.Main = new Rectangle();
            this.Footer = new Rectangle();
        }
    }
}
