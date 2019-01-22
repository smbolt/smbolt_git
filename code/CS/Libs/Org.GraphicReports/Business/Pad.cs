using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GraphicReports.Business
{
  public class Pad
	{
		public int PadNumber { get; set; }
		public string PadName { get; set; }
		public WellSet WellSet { get; set; }
		public Rig Rig { get; set; }

		public Pad(Rig rig)
		{
			this.PadNumber = 0;
			this.PadName = String.Empty;
			this.WellSet = new WellSet();
			this.Rig = rig;
		}

    public Pad GetForDateSpan(DateSpan dateSpan)
    {
      Pad padForSpan = null;

      foreach (var well in this.WellSet.Values)
      {
        DateSpan wellDateSpan = new DateSpan(well.SpudDate.Value, well.CompletionDate.Value);
        if (dateSpan.SpansIntersect(wellDateSpan))
        {
          if (padForSpan == null)
          {
            padForSpan = new Pad(this.Rig);
            padForSpan.PadNumber = this.PadNumber;
            padForSpan.PadName = this.PadName;
          }
          if (!padForSpan.WellSet.ContainsKey(well.WellOrdinal))
            padForSpan.WellSet.Add(well.WellOrdinal, well); 
        }

      }

      return padForSpan;
    }

	}
}
