using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "sz", Abbr = "FontSz", AutoMap = true)]
    public class FontSize : DocumentElement
    {
        [Meta(XMatch = true)]
        public string Val { get; set; }
        public float PointSize { get { return Get_PointSize(); } }

        public FontSize() { }

        public FontSize(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.Val = xml.GetRequiredAttributeValue("val");
        }

        private float Get_PointSize()
        {
            if (this.Val.IsNotNumeric())
                return 0F;

            return this.Val.ToFloat().ToPointSize();
        }
    }
}
