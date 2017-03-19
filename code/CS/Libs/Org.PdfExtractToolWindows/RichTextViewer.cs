using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW.ToolPanels;
using Org.GS;

namespace Org.PdfExtractToolWindows
{
	public partial class RichTextViewer : ToolPanelBase
	{
		public string Text
		{
			get
			{
				return this.txtData.Text;
			}
			set
			{
				this.txtData.Text = value;
			}
		}

		public Panel TopPanel { get { return base.TopPanel; } }

		public RichTextViewer()
			: base("RichTextViewer")
		{
			InitializeComponent();
		}

    public Control GetTopPanelControl(string controlName)
    {
      return this.TopPanel.Controls.OfType<Control>().Where(c => c.Name == controlName).FirstOrDefault();
    }
	}
}
