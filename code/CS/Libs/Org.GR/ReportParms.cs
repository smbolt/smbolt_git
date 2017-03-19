using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GR
{
	public class ReportParms
	{
		public DateSpan DateSpan;
		public ReportDiagnostics ReportDiagnostics { get; set; }
		public Margins Margins { get; set; }

		private PageSize _pageSize;
		public PageSize PageSize
		{
			get { return _pageSize; }
			set
			{
				_pageSize = value;
				SetPageSize();
			}
		}

		public SizeF ActualPageSize { get; private set; }

		private PageOrientation _pageOrientation;
		public PageOrientation PageOrientation
		{
			get { return _pageOrientation; }
			set
			{
				_pageOrientation = value;
				SetPageSize();
			}
		}
		public SizeF CustomPageSize { get; set; }

		public bool PrintCalendar { get; set; }
		public PrintPageControl PrintPageControl { get; set; }

		public ReportParms()
		{
			var dt = DateTime.Now;
			this.DateSpan = new DateSpan(new DateTime(dt.Year, dt.Month, dt.Day), new DateTime(dt.Year, dt.Month, dt.Day).AddMonths(3));
			this.ReportDiagnostics = new ReportDiagnostics();
			this.Margins = new Margins();
			_pageSize = PageSize.Letter;
			_pageOrientation = PageOrientation.Landscape;
			SetPageSize();
			this.PrintCalendar = true;
			this.PrintPageControl = new PrintPageControl();
		}

		private void SetPageSize()
		{
			float dim1 = 0.0F;  // width for portrait, height for landscape
			float dim2 = 0.0F;  // height for portrait, width for landscape

			switch (this.PageSize)
			{
				case PageSize.Legal:
					dim1 = 850.0F;
					dim2 = 1400.0F;
					break;

				case PageSize.Tabloid:
					dim1 = 1100.0F;
					dim2 = 1700.0F;
					break;

				case PageSize.A3:
					dim1 = 11.693F;
					dim2 = 16.535F;
					break;

				case PageSize.A4:
					dim1 = 8.268F;
					dim2 = 11.693F;
					break;

				case PageSize.Custom:
					this.ActualPageSize = new SizeF(this.CustomPageSize.Width, this.CustomPageSize.Height);
					return;

				default:
					dim1 = 850.0F;
					dim2 = 1100.0F;
					break;
			}

			if (_pageOrientation == PageOrientation.Portrait)
				this.ActualPageSize = new SizeF(dim1, dim2);
			else
				this.ActualPageSize = new SizeF(dim2, dim1);

		}
	}
}
