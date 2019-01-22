using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D; 
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using Org.GS;
using Org.GS.Controls;

namespace Org.CTL
{
    public partial class ButtonBar : ControlBase
    {
        private Image _baseButtonImage;
        private ImageList _imageList;
        private ImageList _iconList;

        private Size _buttonImageSize;
        private Rectangle _buttonImageRect;
        private LinearGradientBrush _gradientBrush;
        private Font _buttonTextFont;

        private int _horzPad = 2;
        private int _vertPad = 0;
        private int _iconImageVertAdj = -6;
        
        private Size _buttonSize;
        [Description("Button size")]
        public Size ButtonSize
        {
            get
            {
                return this._buttonSize;
            }
            set
            {
                this._buttonSize = value;
                UpdateBaseImage();
                UpdateButtonImages();
            }
        }

        private Color _gradientColor1;
        [Description("Gradient color 1")]
        public Color GradientColor1
        {
            get
            {
                return this._gradientColor1;
            }
            set
            {
                this._gradientColor1 = value;
                UpdateBaseImage();
                UpdateButtonImages();
            }
        }

        private Color _gradientColor2;
        [Description("Gradient color 2")]
        public Color GradientColor2
        {
            get
            {
                return this._gradientColor2;
            }
            set
            {
                this._gradientColor2 = value;
                UpdateBaseImage();
                UpdateButtonImages();
            }
        }

        private VerticalPosition _labelVerticalPosition;
        [Description("Label vertical position")]
        public VerticalPosition LabelVerticalPosition
        {
            get
            {
                return this._labelVerticalPosition;
            }
            set
            {
                this._labelVerticalPosition = value;
                UpdateBaseImage();
                UpdateButtonImages();
            }
        }

        private ResourceManager _resourceManager;

        public ButtonBar()
        {
            InitializeComponent();
            InitializeButtonBar();
        }

        private void InitializeButtonBar()
        {
            _iconList = new ImageList();
            _iconList.ImageSize = new Size(32, 32);
            _iconList.ColorDepth = ColorDepth.Depth32Bit;

            Assembly assembly = Assembly.GetExecutingAssembly();
            this._resourceManager = new ResourceManager("Org.CTL.ButtonBarResources", assembly); 

            this._buttonSize = new Size(76, 54);
            _buttonImageSize = new Size(_buttonSize.Width - 4, _buttonSize.Height - 4);
            _buttonImageRect = new Rectangle(new Point(0, 0), _buttonImageSize);

            this._gradientColor1 = Color.FromArgb(139, 193, 247);
            this._gradientColor2 = Color.FromArgb(235, 244, 255);
            this._labelVerticalPosition = VerticalPosition.Bottom;

            _imageList = new ImageList();
            _imageList.ImageSize = _buttonImageSize;
            _imageList.ColorDepth = ColorDepth.Depth32Bit;

            _buttonTextFont = new Font("Microsoft Sans Serif", 10.0f, FontStyle.Bold);

            foreach (ButtonSpec buttonSpec in base.ControlSpecSet.Values.OfType<ButtonSpec>())
            {
                if (buttonSpec.Text.IsBlank())
                    buttonSpec.Text = buttonSpec.Tag; 

                ControlProperties cp = new ControlProperties();
                cp.Name = buttonSpec.Name;
                cp.Tag = buttonSpec.Tag;
                cp.Text = buttonSpec.Text;
                if (base.ControlPropertiesSet.ContainsKey(cp.Name))
                    throw new Exception("Duplicate control name '" + cp.Name + "'.");

                base.ControlPropertiesSet.Add(cp.Name, cp);
            }
            
            BuildImageResources();
            UpdateButtonImages();

            BuildButtons();
        }

        private void UpdateButtonImages()
        {
            foreach (ToolStripButton b in ToolStrip.Items)
            {
                ButtonSpec buttonSpec = (ButtonSpec) base.ControlSpecSet[b.Name];

                Image img = BuildImage(buttonSpec);

                if (_imageList.Images.ContainsKey(buttonSpec.Tag))
                    _imageList.Images.RemoveByKey(buttonSpec.Tag);
                _imageList.Images.Add(buttonSpec.Tag, img);

                b.Image = this._imageList.Images[buttonSpec.Tag]; 
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ToolStripButton clickedButton = (ToolStripButton)sender;
            base.SendControlEvent(new ControlEventArgs(sender, ControlEventType.Click, clickedButton.Tag.ToString()));
        }

        private void BuildImageResources()
        {
            UpdateBaseImage();

            _labelVerticalPosition = VerticalPosition.Bottom;
        }

        private void UpdateBaseImage()
        {
            _gradientBrush = new LinearGradientBrush(_buttonImageRect, _gradientColor1, _gradientColor2, LinearGradientMode.Horizontal);
            _gradientBrush.SetSigmaBellShape(0.5f);

            _baseButtonImage = new Bitmap(_buttonImageSize.Width,_buttonImageSize.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(_baseButtonImage))
            {
                g.FillRectangle(_gradientBrush, _buttonImageRect); 
            }
        }

        private Image BuildImage(ButtonSpec buttonSpec)
        {
            Image img = (Bitmap)_baseButtonImage.Clone();

            string label = buttonSpec.Text;
            if (label.IsBlank())
                label = buttonSpec.Tag; 

            Image iconImage = this.GetIconImage(buttonSpec.Tag); 

            using (Graphics gr = Graphics.FromImage(img))
            {
                Size sz = TextRenderer.MeasureText(label, _buttonTextFont);

                Point pt = new Point((_buttonImageRect.Width / 2 - sz.Width / 2) + _horzPad, _buttonImageRect.Height / 2 - sz.Height / 2);

                switch (_labelVerticalPosition)
                {
                    case VerticalPosition.Top:
                        pt = new Point((_buttonImageRect.Width / 2 - sz.Width / 2) + _horzPad, _vertPad);
                        break;

                    case VerticalPosition.Bottom:
                        pt = new Point((_buttonImageRect.Width / 2 - sz.Width / 2) + _horzPad, _buttonImageRect.Height - (_vertPad + sz.Height));
                        break;
                }

                if (iconImage != null)
                    gr.DrawImage(iconImage, new Point(_buttonImageRect.Width / 2 - iconImage.Width / 2, (_buttonImageRect.Height / 2 - iconImage.Height / 2) + _iconImageVertAdj));
                gr.DrawString(label, _buttonTextFont, Brushes.Black, pt);
            }

            return img; 
        }

        protected override void ProcessConfig()
        {
            BuildButtons();
        }

        private void BuildButtons()
        {
            this.ToolStrip.Items.Clear();

            foreach (ButtonSpec buttonSpec in base.ControlSpecSet.Values.OfType<ButtonSpec>())
            {
                AddButton(buttonSpec);
            }
        }

        private void AddButton(ButtonSpec buttonSpec)
        {
            ToolStripButton b = new ToolStripButton();
            b.BackColor = Color.Gainsboro;
            b.DisplayStyle = ToolStripItemDisplayStyle.Image;
            b.ImageScaling = ToolStripItemImageScaling.None;

            b.TextAlign = ContentAlignment.BottomCenter;
            b.TextImageRelation = TextImageRelation.ImageAboveText;
            b.AutoSize = false;
            b.Name = buttonSpec.Name;
            b.ToolTipText = buttonSpec.Text;
            b.Tag = buttonSpec.Tag;
            b.Margin = new Padding(0, 1, 2, 2);
            b.Size = new Size(76, 54);

            if (!_imageList.Images.ContainsKey(buttonSpec.Tag))
                _imageList.Images.Add(buttonSpec.Tag, BuildImage(buttonSpec));
            b.Image = this._imageList.Images[b.Tag.ToString()];

            this.ToolStrip.Items.Add(b);

            b.Click += Button_Click;
        }

        public void HideButton(string tag)
        {
          foreach (ToolStripItem tsi in this.ToolStrip.Items)
          {
            string controlTag = tsi.Tag.ObjectToTrimmedString();
            if (controlTag == tag)
            {
              tsi.Visible = false;
              break;
            }
          }
        }

        public void ShowButton(string tag)
        {
          foreach (ToolStripItem tsi in this.ToolStrip.Items)
          {
            string controlTag = tsi.Tag.ObjectToTrimmedString();
            if (controlTag == tag)
            {
              tsi.Visible = true;
              break;
            }
          }
        }

        private Image GetIconImage(string tag)
        {
            if (_iconList.Images.ContainsKey(tag))
                return _iconList.Images[tag];

            Image iconImage = (Bitmap)this._resourceManager.GetObject(tag);
            if (iconImage != null)
                _iconList.Images.Add(tag, iconImage);

            if (!_iconList.Images.ContainsKey(tag))
                return null;

            return _iconList.Images[tag];
        }

        public void UpdateControl(List<ControlUpdate> controlUpdate)
        {
            if (controlUpdate == null)
                return;

            foreach (ControlUpdate u in controlUpdate)
            {
                ToolStripButton b = GetButtonByTag(u.ControlName);
                if (b != null)
                {
                    switch (u.ControlProperty)
                    {
                        case ControlProperty.Enabled:
                            b.Enabled = u.BooleanValue;
                            break;
                    }
                }
            }
        }

        private ToolStripButton GetButtonByTag(string tag)
        {
            foreach (ToolStripButton b in this.ToolStrip.Items)
            {
                if (b.Tag != null)
                {
                    if (b.Tag.ToString() == tag)
                        return b;
                }
            }

            return null;
        }
    }
}
