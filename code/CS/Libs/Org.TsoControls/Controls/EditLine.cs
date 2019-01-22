using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.TsoControls.Controls
{
	public partial class EditLine : UserControl
	{
		public int LineNumber { get; private set; }
		public bool InDocument { get; set; }

		public EditLine(int lineNumber, string setName)
		{
			InitializeComponent();
			this.LineNumber = lineNumber;

			this.InDocument = setName == "EditLines";
		}

		public PanelControlBase GetControl(string name)
		{
			foreach (Control c in this.Controls)
			{
				if (c.Name == name)
					return c as PanelControlBase;
			}

			return null;
		}
	}
}
