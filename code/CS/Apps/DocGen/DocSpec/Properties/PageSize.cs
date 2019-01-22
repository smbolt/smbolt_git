using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "pgSz", AutoMap = true, Abbr="PgSz")]
    public class PageSize : DocumentElement
    {
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt16? Code { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32? Width { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32? Height { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public PageOrientationValues? Orient { get; set; }

        public PageSize() { }

        public PageSize(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
            {
                this.Width = 12240;
                this.Height = 15840;
                this.Orient = PageOrientationValues.Portrait;
                this.Code = null;
                return;
            }

            this.Code = xml.GetUInt16AttributeOrNull("code");
            this.Width = xml.GetUInt32AttributeOrNull("w");
            this.Height = xml.GetUInt32AttributeOrNull("h");
            this.Orient = (PageOrientationValues)xml.GetEnumAttributeOrNull("orient", typeof(PageOrientationValues));
        }

        public Size ToSize()
        {
            if (this.Width.HasValue && this.Height.HasValue)
                return new Size(Convert.ToInt32(this.Width.Value), Convert.ToInt32(this.Height.Value));

            return new Size(0, 0);
        }

        public Size ToSizeFromTwips()
        {
            if (this.Width.HasValue && this.Height.HasValue)
            {
                int width = Convert.ToInt32((float)this.Width.Value / 1440 * 100);
                int height = Convert.ToInt32((float)this.Height.Value / 1440 * 100);

                return new Size(width, height);
            }

            return new Size(0, 0);
        }

        public PageSize DeepClone()
        {
            return this;
        }
    }
}
