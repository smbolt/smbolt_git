using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Dynamic
{
  public enum ControlType
  {
    NotSet,
    MainMenu,
    MenuItem
  }

  public class ControlBase
  {
    [XMap(IsRequired = true)]
    public ControlType ControlType {
      get;
      set ;
    }

    private string _name;
    [XMap(IsRequired = true)]
    public string Name
    {
      get {
        return this._name;
      }
      set {
        _name = value;
      }
    }

    [XMap(DefaultValue = "-1")]
    public int AmpPos {
      get;
      set;
    }

    public string Text
    {
      get {
        return this._name;
      }
    }

    [XMap(DefaultValue = "False")]
    public bool MapEvent {
      get;
      set;
    }

    public Control Parent {
      get;
      set;
    }

    [XMap]
    public string Tag {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "Control", WrapperElement="ControlSet")]
    public ControlSet ControlSet {
      get;
      set;
    }

    public ControlBase()
    {
      this.ControlType = ControlType.NotSet;
      this.Name = String.Empty;
      this.AmpPos = -1;
      this.Parent = null;
      this.MapEvent = false;
      this.Tag = String.Empty;
      this.ControlSet = new ControlSet(this);
    }

    public string GetControlName()
    {
      string controlName = this.Name;

      ControlBase item = this;

      while (item.Parent != null)
      {
        string namePart = item.Parent.Name;
        if (namePart.ToUpper() != "MAIN")
          controlName = namePart + controlName;
        item = item.Parent;
      }

      return "mnu" + controlName;
    }

    public string GetTag()
    {
      string tag = String.Empty;

      if (this.Tag.IsNotBlank())
      {
        if (this.Tag.StartsWith("*"))
        {
          tag = this.Tag.ToUpper().Replace("*", String.Empty);
        }
        else
        {
          return this.Tag.ToUpper();
        }
      }
      else
      {
        tag = this.Name.ToUpper().Replace(" ", "_");
      }

      ControlBase item = this;

      while (item.Parent != null)
      {
        string namePart = item.Parent.Name.ToUpper();
        if (namePart != "MAIN")
          tag = namePart + "_" + tag;
        item = item.Parent;
      }

      return tag;
    }
  }
}
