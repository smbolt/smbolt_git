using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GraphicReports
{
	public class PrintPageControl
	{
		public bool PrintAllPages { get; private set; }

		public List<int> PagesToPrint { get; private set; }
		public string PageControlString
		{
			set { SetPagesToPrint(value); }
		}

		public PrintPageControl()
		{
			this.PrintAllPages = true;
			this.PagesToPrint = new List<int>();
		}

		public void SetPagesToPrint(string value)
		{
			this.PagesToPrint = new List<int>();

			// parse the pages from the string
			// set populate the PagesToPrint collection...

		}

		public bool PrintPage(int pageToPrint)
		{
			if (this.PrintAllPages)
				return true;

			return this.PagesToPrint.Contains(pageToPrint); 
		}

	}
}
