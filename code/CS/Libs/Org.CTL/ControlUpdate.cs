using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.CTL
{
    public class ControlUpdate
    {
        public string ControlName { get; set; }
        public ControlProperty ControlProperty { get; set; }
        public string StringValue { get; set; }
        public bool BooleanValue { get; set; }

        public ControlUpdate(string controlName, ControlProperty controlProperty, string stringValue)
        {
            this.ControlName = controlName;
            this.ControlProperty = controlProperty;
            this.StringValue = stringValue;
            this.BooleanValue = false;
        }

        public ControlUpdate(string controlName, ControlProperty controlProperty, bool booleanValue)
        {
            this.ControlName = controlName;
            this.ControlProperty = controlProperty;
            this.StringValue = String.Empty;
            this.BooleanValue = booleanValue;
        }
    }
}
