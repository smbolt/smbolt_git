using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Dx.Business;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
	public partial class frmCxExceptionDisplay : Form
	{
		public frmCxExceptionDisplay(CxException cx)
		{
			InitializeComponent();
			txtOut.Text = cx.ToReport();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
