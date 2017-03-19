using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.UI
{
  public class UIEventArgs
  {
    public string ControlType { get; set; }
    public string ControlName { get; set; }
    public string ControlText { get; set; }
    public string Tag { get; set; }
    public string EventName { get; set; }

    public UIEventArgs(string controlType, string controlName, string controlText, string tag, string eventName)
    {
      this.ControlType = controlType;
      this.ControlName = controlName;
      this.ControlText = controlText; 
      this.Tag = tag;
      this.EventName = eventName;
    }
  }
}
