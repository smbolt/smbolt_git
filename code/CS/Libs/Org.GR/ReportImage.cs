using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using Org.GS;

namespace Org.GR
{
	public class ReportImage : IDisposable
	{
		private bool _isDisposed;
		public bool IsDisposed { get { return _isDisposed; } }

		private ReportParms _reportParms;
		public int PageNumber { get; private set; }
		private Size _pageSize;

		private Image _image;
		public Image Image { get { return _image; } }

		public ReportImageSet ReportImageSet { get; set; }

		public ReportImage(int pageNumber, ReportParms reportParms)
		{
			_isDisposed = false;
			this.PageNumber = pageNumber;
			_reportParms = reportParms;
			_pageSize = new Size(Convert.ToInt32(_reportParms.ActualPageSize.Width), Convert.ToInt32(_reportParms.ActualPageSize.Height));
			_image = new Bitmap(_pageSize.Width, _pageSize.Height);
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;

			if (_image != null)
				_image.Dispose();

			_isDisposed = true;
		}
	}
}
