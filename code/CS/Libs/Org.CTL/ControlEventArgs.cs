using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.CTL
{
    public class ControlEventArgs
    {
        public object Sender { get; set; }
        public ControlEventType ControlEventType { get; set; }
        public string ControlEventText { get; set; }

        public ControlEventArgs(object sender, ControlEventType type, string text)
        {
            this.Sender = sender;
            this.ControlEventType = type;
            this.ControlEventText = text; 
        }
    }
}
