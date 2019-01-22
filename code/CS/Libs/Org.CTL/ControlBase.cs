using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
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
    public enum ControlOrientation
    {
        Horizontal = 0,
        Vertical = 1
    }

    public enum VerticalPosition
    {
        Middle = 0,
        Top, 
        Bottom
    }

    public enum ControlProperty
    {
        NotSet,
        Enabled
    }

    public partial class ControlBase : UserControl
    {
        public event Action<ControlEventArgs> ControlEvent;
        protected ControlSpecSet ControlSpecSet;

        private ControlOrientation _controlOrientation; 
        public ControlOrientation ControlOrientation
        {
            get { return this._controlOrientation; }
            set 
            { 
                this._controlOrientation = value;
                this.Size = new Size(this.Height, this.Width); 
            }
        }

        private XElement _controlConfig;
        [Description("String used for configuring the ButtonBar control.")]
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ConfigString
        {
            get
            {
                return f.Serialize(ControlSpecSet).ToString();
            }
            set
            {
                XElement controlSetElement = XElement.Parse(value);
                ControlSpecSet = f.Deserialize(controlSetElement) as ControlSpecSet;
                ProcessConfig();
            }
        }

        public ControlPropertiesSet ControlPropertiesSet { get; set; }

        private ObjectFactory2 f; 


        public ControlBase()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            f = new ObjectFactory2();
            this._controlOrientation = ControlOrientation.Horizontal;

            this.ControlSpecSet = new ControlSpecSet();
            this.ControlSpecSet.Add("button1", new ButtonSpec() { Tag = "button1" });

            XElement controlSpecSetElement = f.Serialize(this.ControlSpecSet);

            ControlSpecSet cs = f.Deserialize(controlSpecSetElement) as ControlSpecSet;
            this.ControlPropertiesSet = new ControlPropertiesSet();
        }


        private XElement GetConfigFromString(string config)
        {
            return XElement.Parse(config);
        }

        protected virtual void ProcessConfig()
        {
        }

        public void SendControlEvent(ControlEventArgs args)
        {
            if(ControlEvent != null)
                ControlEvent(args);
        }
    }
}
