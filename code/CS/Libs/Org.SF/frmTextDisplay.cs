using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.SF
{
	public partial class frmTextDisplay : Form
	{
		public frmTextDisplay()
		{
			InitializeComponent();
			InitializeForm();
		}

		private void InitializeForm()
		{
			txtOut.Text = String.Empty;
		}

		public void SetText(string text)
		{
			txtOut.Text = text;
		}
	}
}
