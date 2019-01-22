using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GR
{
	public class Margins
	{
		public float Left { get; set; }
		public float Right { get; set; }
		public float Top { get; set; }
		public float Bottom { get; set; }

		public Margins()
		{
			this.Left = 100.0F;
			this.Right = 100.0F;
			this.Top = 100.0F;
			this.Bottom = 100.0F;
		}

		public float GetClientWidth(float totalWidth)
		{
			float clientWidth = totalWidth - (this.Left + this.Right);
			return clientWidth < 0 ? 0 : clientWidth;
		}

		public float GetClientHeight(float totalHeight)
		{
			float clientHeight = totalHeight - (this.Top + this.Bottom);
			return clientHeight < 0 ? 0 : clientHeight;
		}
	}
}
