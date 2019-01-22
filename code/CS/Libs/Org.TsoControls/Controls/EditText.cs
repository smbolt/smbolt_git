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
	public partial class EditText : PanelControlBase
	{
		private PanelLineElement _pe;
		
		public new string Text
		{
			get { return txt.Text; }
			set { txt.Text = value; }
		}

		public new string Name
		{
			get { return txt.Name; }
			set
			{
				txt.Name = value;
				base.Name = value;
			}
		}

		public int SelectionStart
		{
			get { return txt.SelectionStart; }
			set { txt.SelectionStart = value; }
		}

		public new Color ForeColor
		{
			get { return txt.ForeColor; }
			set { txt.ForeColor = value; }
		}

		public EditLine ParentLine { get; private set; }

		public EditText(PanelLineElement pe, EditLine parentLine)
		{
			InitializeComponent();
			this.ParentLine = parentLine;
			_pe = pe;

			Initialize();
		}

		private void Initialize()
		{
			this.Name = _pe.Name; 
			this.Text = _pe.Text;
			this.Width = _pe.Width * 13;

			switch (_pe.Role)
			{
				case PanelLineElementRole.LineNumber:
					this.Text = "000000";
					break;

				case PanelLineElementRole.LineText:
					this.Text = "TEST TEXT";
					break;

				case PanelLineElementRole.LineLabel:
					this.Text = "TSOISPF ";
					break;
			}
		}

	}
}
