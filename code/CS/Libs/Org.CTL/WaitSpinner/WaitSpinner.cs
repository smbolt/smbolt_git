using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace Org.CTL
{
    public enum WaitSpinnerStatus
    {
        NotSet,
        Spinning,
        Stopped,
        Success,
        Failure
    }

    public partial class WaitSpinner : UserControl
    {
        private int imageNumber = 0;
        private bool isInitialized = false;
        private Image[] _images;
        private Image _successImage;
        private Image _failureImage;

        private int _gridRowIndex;
        public int GridRowIndex
        {
            get { return _gridRowIndex; }
            set { _gridRowIndex = value; }
        }

        private WaitSpinnerStatus _status;
        public WaitSpinnerStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public WaitSpinner()
        {
            InitializeComponent();
            InitializeControl();
            _status = WaitSpinnerStatus.Stopped;
            _gridRowIndex = -1;
        }

        private void InitializeControl()
        {
            this.Size = new Size(48, 48);

            _images = new Image[16];
            ResourceManager resourceManager = new ResourceManager("Org.CTL.WaitSpinnerResources", Assembly.GetExecutingAssembly());
            _images[0] = (Bitmap)resourceManager.GetObject("spin1");
            _images[1] = (Bitmap)resourceManager.GetObject("spin2");
            _images[2] = (Bitmap)resourceManager.GetObject("spin3");
            _images[3] = (Bitmap)resourceManager.GetObject("spin4");
            _images[4] = (Bitmap)resourceManager.GetObject("spin5");
            _images[5] = (Bitmap)resourceManager.GetObject("spin6");
            _images[6] = (Bitmap)resourceManager.GetObject("spin7");
            _images[7] = (Bitmap)resourceManager.GetObject("spin8");
            _images[8] = (Bitmap)resourceManager.GetObject("spin9");
            _images[9] = (Bitmap)resourceManager.GetObject("spin10");
            _images[10] = (Bitmap)resourceManager.GetObject("spin11");
            _images[11] = (Bitmap)resourceManager.GetObject("spin12");
            _images[12] = (Bitmap)resourceManager.GetObject("spin13");
            _images[13] = (Bitmap)resourceManager.GetObject("spin14");
            _images[14] = (Bitmap)resourceManager.GetObject("spin15");
            _images[15] = (Bitmap)resourceManager.GetObject("spin16");

            _successImage = (Bitmap)resourceManager.GetObject("success");
            _failureImage = (Bitmap)resourceManager.GetObject("failure");

            pbMain.Image = new Bitmap(_images[0], pbMain.Size);

            isInitialized = true;            
        }

        public void StartSpinning()
        {
            timer1.Interval = 83;
            timer1.Enabled = true;
            imageNumber = 0;
            _status = WaitSpinnerStatus.Spinning;
        }

        public void StopSpinning()
        {
            timer1.Enabled = false;
            _status = WaitSpinnerStatus.Stopped;
        }

        public void ShowSuccess()
        {
            timer1.Enabled = false;
            pbMain.Image = new Bitmap(_successImage, pbMain.Size);
            _status = WaitSpinnerStatus.Success;
        }

        public void ShowFailure()
        {
            timer1.Enabled = false;
            pbMain.Image = new Bitmap(_failureImage, pbMain.Size);
            _status = WaitSpinnerStatus.Failure;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Image = new Bitmap(_images[imageNumber++], pbMain.Size);
            if (imageNumber == 16)
                imageNumber = 0;

            pbMain.Refresh();
            Application.DoEvents();
        }

        private void WaitSpinner_Resize(object sender, EventArgs e)
        {
            if (!isInitialized)
                return;

            pbMain.Image = new Bitmap(_images[0], pbMain.Size);
        }


    }
}
