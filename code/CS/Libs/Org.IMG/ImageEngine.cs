using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using ImageProcessor;
using Org.GS;

namespace Org.IMG
{
	public class ImageEngine : IDisposable
	{
		private bool _isDisposed = false;
		public bool IsDisposed { get { return _isDisposed; } }
		private string _imgFmt;
		private string _imgExt;

		public ImageEngine()
		{

		}

		public void ClipImages(string imageFolder, string imgFmt)
		{
			_imgFmt = imgFmt;
			_imgExt = "." + _imgFmt;

			try
			{
				List<string> imageFiles = Directory.GetFiles(imageFolder, "*" + _imgExt).ToList();

				foreach (string imageFile in imageFiles)
					CreateClipImage2(imageFile); 
			}
			catch(Exception ex)
			{
				throw new Exception("An exception occurred while attempting to clip the images in the folder '" + imageFolder + "'.", ex); 
			}
			finally
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}


		public void CreateClipImage2(string imageFileName)
		{

			try
			{

				var fn = new FileNamer(imageFileName);
				fn.Extension = _imgExt;
				var clipFileName = fn.AppendBeforeExtension("_clip"); 

				using (var fs = new FileStream(imageFileName, FileMode.Open))
				{

					using (var imageFactory = new ImageProcessor.ImageFactory())
					{
						var img = imageFactory.Load(fs);
				    var clipRect = new Rectangle(0, 0, Convert.ToInt32(img.Image.Width * .6), Convert.ToInt32(img.Image.Height * .5));
            img.Crop(clipRect);
						img.Save(clipFileName); 
					}
				}

			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to clip image '" + imageFileName + "'.", ex); 
			}
		}



		public void CreateClipImage(string imageFileName)
		{
			try
			{
				Image origImg2;
				using (var fs = new FileStream(imageFileName, FileMode.Open))
				{
					origImg2 = Image.FromStream(fs);
					fs.Close();
				}

				Image clipImg2 = new Bitmap(Convert.ToInt32(origImg2.Width * .6), Convert.ToInt32(origImg2.Height * .5)); 
				var clipRect = new Rectangle(0, 0, Convert.ToInt32(origImg2.Width * .6), Convert.ToInt32(origImg2.Height * .5));
				using (var gx = Graphics.FromImage(clipImg2))
				{
					gx.DrawImage(origImg2, clipRect, clipRect, GraphicsUnit.Pixel); 
				}

				var fn = new FileNamer(imageFileName);
				fn.Extension = _imgExt;
				var clipFileName = fn.AppendBeforeExtension("_clip"); 
				clipImg2.Save(clipFileName);
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to clip image '" + imageFileName + "'.", ex); 
			}
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;


			_isDisposed = true;
		}

	}
}
