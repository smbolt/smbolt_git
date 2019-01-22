using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace Org.DocGen.DocSpec
{
    public static class ExtensionMethods2
    {
        [DebuggerStepThrough]
        public static SectionSet ToSectionSet(this DocumentElementSet value)
        {
            SectionSet ss = new SectionSet();

            foreach (DocumentElement de in value.Values)
            {
                if (de.DeType == DeType.Section)
                    ss.Add(de.Name, (Section)de);
            }

            return ss;
        }
    }
}
