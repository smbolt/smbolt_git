using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.DocGen.DocSpec
{
    public class OccupiedRegionSet : SortedList<string, OccupiedRegion>
    {
        public OccupiedRegionSet GetRegionSetAt(int x, int y)
        {
            OccupiedRegionSet ors = new OccupiedRegionSet();

            foreach (OccupiedRegion or in this.Values)
            {
                if (or.RectF.Contains((float)x, (float)y))
                {
                    ors.Add(or.RegionName, or); 
                }
            }

            return ors;
        }

    }
}
