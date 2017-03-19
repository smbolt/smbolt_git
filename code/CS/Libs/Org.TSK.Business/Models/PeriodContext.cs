using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;

namespace Org.TSK.Business.Models
{
	public class PeriodContext
	{
		public int PeriodContextId { get; set; }
		public string Period { get; set; }
		public PeriodContexts PeriodContexts { get { return g.ToEnum<PeriodContexts>(this.PeriodContextId, PeriodContexts.NotSet); } }
	}
}
