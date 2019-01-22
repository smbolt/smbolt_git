using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;


namespace Org.GS.Dynamic
{
  [XMap(XType=XType.Element, CollectionElements="Control")]
  public class ControlSet : List<Control>
  {
    public Control Parent {
      get;
      set;
    }
    public bool IsRoot
    {
      get {
        return this.Get_IsRoot();
      }
    }

    private Type _parentType;

    [XParm(Name="parent", ParmSource=XParmSource.Parent)]
    public ControlSet(object parent)
    {
      this._parentType = parent.GetType();
      if (this._parentType.Name == "Control")
        this.Parent = (Control) parent;
      else
        this.Parent = null;
    }

    public Control GetMainMenuControl()
    {
      foreach (Control c in this)
      {
        if (c.ControlType == ControlType.MainMenu)
          return c;
      }

      return null;
    }

    private bool Get_IsRoot()
    {
      if (_parentType.Name == "Control")
        return false;

      return true;
    }
  }
}
