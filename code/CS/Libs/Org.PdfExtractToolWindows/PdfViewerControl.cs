using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW.ToolPanels;
using Org.GS;

namespace Org.PdfExtractToolWindows
{
	public partial class PdfViewerControl : ToolPanelBase
	{
		public PdfViewerControl()
			: base("PdfViewer")
		{
			InitializeComponent();
		}

		public void CloseDocument()
		{
			this.dxPdfViewer.CloseDocument();
		}

		public void LoadDocument(string fullFileName)
		{
			try
			{
				if (!File.Exists(fullFileName))
					throw new Exception("File '" + fullFileName + "' does not exist - cannot be loaded into the PDF Viewer.");

				this.dxPdfViewer.LoadDocument(fullFileName); 
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to load file '" + fullFileName + "' to the PDF Viewer.", ex); 
			}
		}
	}
}
