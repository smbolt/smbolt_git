using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "tab", Abbr = "Tab", IsAttribute = true, AutoMap = true)]
    public class TabStop : DocumentElement
    {
        [Meta(XMatch = true)]
        public TabStopLeaderCharValues? Leader { get; set; }
        [Meta(XMatch = true)]
        public int Position { get; set; }
        [Meta(XMatch = true)]
        public TabStopValues Val { get; set; }

        public TabStop() { }

        public TabStop(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.Leader = (TabStopLeaderCharValues)xml.GetEnumAttributeOrDefault("leader", typeof(TabStopLeaderCharValues));
            this.Position = xml.GetRequiredAttributeInt32("pos");
            this.Val = (TabStopValues)xml.GetEnumAttributeOrDefault("val", typeof(TabStopValues));
        }
    }
}