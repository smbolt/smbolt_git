using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TsoControls.PanelDefs;
using Org.GS;

namespace Org.TsoControls.Controls
{
	public partial class LabelText : PanelControlBase
	{
		private PanelLineElement _pe;

		public new string Text
		{
			get { return lbl.Text; }
			set { lbl.Text = value; }
		}

		public new string Name
		{
			get { return lbl.Name; }
			set
			{
				lbl.Name = value;
				base.Name = value;
			}
		}

		public new Color ForeColor
		{
			get { return lbl.ForeColor; }
			set { lbl.ForeColor = value; }
		}

		public EditLine ParentLine { get; private set; }

		public LabelText(PanelLineElement pe, EditLine parentLine)
		{
			InitializeComponent();
			_pe = pe;
			this.ParentLine = parentLine;

			Initialize();
		}

		private void Initialize()
		{
			this.Name = _pe.Name;
			this.Text = _pe.Text;
			this.Width = _pe.Width * 13;
		}
	}
}
