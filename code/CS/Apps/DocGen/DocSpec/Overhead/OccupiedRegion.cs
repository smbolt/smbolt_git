using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class OccupiedRegion
    {
        public string RegionName { get; set; }
        public int PageNumber { get; set; }
        public RectangleF RectF { get; set; }
        public DocumentElement DocumentElement { get; set; }

        public OccupiedRegion()
        {
            this.RegionName = String.Empty;
            this.PageNumber = 0;
            this.RectF = new RectangleF();
            this.DocumentElement = null;
        }

        public OccupiedRegion(string regionName, int pageNumber, RectangleF rectF, DocumentElement documentElement)
        {
            this.RegionName = regionName;
            this.PageNumber = pageNumber;
            this.RectF = rectF;
            this.DocumentElement = documentElement;
        }
    }
}
