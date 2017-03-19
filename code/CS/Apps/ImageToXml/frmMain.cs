using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Adsdi.GS;

namespace Adsdi.ImageToXml
{
    public partial class frmMain : Form
    {
        private string _imageFolder;
        private Image _image;

        public frmMain()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            _imageFolder = g.GetAppPath() + @"\Images";
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPageImage;
            pbMain.Image = null;
            Application.DoEvents();

            List<string> imagePaths = Directory.GetFiles(_imageFolder).ToList();
            if (imagePaths.Count > 0)
                LoadImage(imagePaths.Last());
        }

        private void btnSerializeImage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            tabMain.SelectedTab = tabPageData;
            Application.DoEvents();

            SerializeImage();


            this.Cursor = Cursors.Default;
        }

        private void SerializeImage()
        {
            Application.DoEvents();
            StringBuilder sb = new StringBuilder();

            Bitmap img = (Bitmap) _image;
            MemoryStream ms = new MemoryStream();

            int w = _image.Width;
            int h = _image.Height;

            DateTime dtBegin = DateTime.Now;
            int whiteCount = 0;
            int blackCount = 0;

            for (int y = 1000; y < h; y++)
            {
                for (int x = 1000; x < w; x++)
                {
                    Color c = img.GetPixel(x, y);
                    int totalRGB = c.R + c.G + c.B;

                    switch (totalRGB)
                    {
                        case 0:     // black pixel
                            byte xByte = (byte)x;
                            byte yByte = (byte)y;

                            short xShort = (short)x;
                            short yShort = (short)y;

                            //BitConverter.

                            BitArray b = new BitArray(4);
                            
                            blackCount++;
                            break;
                        case 765:   // white pixel
                            whiteCount++;
                            break;
                        default:
                            string errorMessage = "Invalid color found in bitmap: R=" + c.R.ToString() + " G=" + c.G.ToString() + " B=" + c.B.ToString();
                            MessageBox.Show(errorMessage, "ImageToXml - Error Processing Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sb = new StringBuilder("An error occurred processing the image." + g.crlf2 + errorMessage);
                            break;
                    }
                }
            }

            TimeSpan ts = DateTime.Now - dtBegin;

            txtOut.Text = "Finished Black:" + blackCount.ToString() + " White:" + whiteCount.ToString() + g.crlf +
                ts.Seconds.ToString() + "." + ts.Milliseconds.ToString("000");
            txtOut.SelectionStart = 0;
            txtOut.SelectionLength = 0;
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPageData;

            txtOut.Text = "Still working here";
        }

        private void LoadImage(string imagePath)
        {
            _image = Image.FromFile(imagePath);
            pbMain.Size = _image.Size;
            pnlImage.Size = _image.Size;
            pbMain.Image = _image;
            Application.DoEvents();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            pnlImage.Refresh();
            pbMain.Refresh();
        }

        private void btnSwitchToImageTab_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPageImage;
        }
    }
}
