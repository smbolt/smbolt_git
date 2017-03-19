using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.UI
{
  public class TaggedControl
  {
    public string Tag { get; set; }
    public UIControl UIControl { get; set; }
    public object ControlObject { get; set; }
    public Type ControlObjectType { get; set; }
    public bool MapData { get { return Get_MapData(); } }
    public bool IsListControl { get { return Get_IsListControl(); } }

    public TaggedControl(string tag, UIControl uiControl, object controlObject)
    {
      this.Tag = tag;
      this.UIControl = uiControl; 
      this.ControlObject = controlObject;
      if (controlObject != null)
        this.ControlObjectType = controlObject.GetType(); 
    }
  }
}
