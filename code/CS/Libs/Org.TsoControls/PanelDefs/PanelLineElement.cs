using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TsoControls.PanelDefs
{
	[XMap(XType = XType.Element)]
	public class PanelLineElement
	{
		[XMap]
		public string Name { get; set; }

		[XMap(Name="Type", DefaultValue = "Label")]
		public PanelLineElementType Type { get; set; }

		[XMap(Name="Role", DefaultValue = "Spacer")]
		public PanelLineElementRole Role { get; set; }

		[XMap]
		public string Text { get; set; }

		[XMap]
		public int Width { get; set; }

		public PanelLineElement()
		{
			this.Name = String.Empty;
			this.Type = PanelLineElementType.Label;
			this.Role = PanelLineElementRole.Spacer;
			this.Text = String.Empty;
			this.Width = 0;
		}

		public void AutoInit()
		{
			if (this.Width == 0)
			{
				if (this.Text.IsNotBlank())
					this.Width = this.Text.Length;
			}
			else
			{
				if (this.Text.Length > this.Width)
					this.Width = this.Text.Length; 
			}			
		}
	}
}
