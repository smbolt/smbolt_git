using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "spacing", AutoMap= true, ChildOfSet = "A", Abbr = "SpcBtwLns")]
    public class SpacingBetweenLines : DocumentElement
    {
        [Meta(XMatch = true)]
        public string After { get; set; }
        [Meta(XMatch = true)]
        public bool? AfterAutoSpacing { get; set; }
        [Meta(XMatch = true)]
        public Int32? AfterLines { get; set; }
        [Meta(XMatch = true)]
        public string Before { get; set; }
        [Meta(XMatch = true)]
        public bool? BeforeAutoSpacing { get; set; }
        [Meta(XMatch = true)]
        public Int32? BeforeLines { get; set; }
        [Meta(XMatch = true)]
        public string Line { get; set; }
        [Meta(XMatch = true)]
        public LineSpacingRuleValues? LineRule { get; set; }

        public SpacingBetweenLines() { }

        public SpacingBetweenLines(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.After = xml.GetAttributeValueOrNull("after");
            this.AfterAutoSpacing = xml.GetBooleanAttributeValueOrNull("afterAutospacing");
            this.AfterLines = xml.GetInt32AttributeValueOrNull("afterLines");
            this.Before = xml.GetAttributeValueOrNull("before");
            this.BeforeAutoSpacing = xml.GetBooleanAttributeValueOrNull("beforeAutospacing");
            this.BeforeLines = xml.GetInt32AttributeValueOrNull("beforeLines");
            this.Line = xml.GetAttributeValueOrNull("line");
            this.LineRule = (LineSpacingRuleValues)xml.GetEnumAttributeOrNull("lineRule", typeof(LineSpacingRuleValues));
        }
    }
}