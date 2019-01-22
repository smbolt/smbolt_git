using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;

namespace Org.UI
{
  [XMap(XType=XType.Element, CollectionElements="UIControl")]
  public class UIControl : List<UIControl>
  {
    [XMap]
    public string Name {
      get;
      set;
    }
    [XMap]
    public string Text {
      get;
      set;
    }
    [XMap]
    public string Model {
      get;
      set;
    }
    [XMap]
    public string ModelSort {
      get;
      set;
    }
    [XMap]
    public string ListSource {
      get;
      set;
    }
    [XMap]
    public string ListData {
      get;
      set;
    }
    [XMap]
    public string Type {
      get;
      set;
    }
    [XMap]
    public bool AutoSize {
      get;
      set;
    }
    [XMap]
    public bool Required {
      get;
      set;
    }
    [XMap]
    public string Size {
      get;
      set;
    }
    [XMap]
    public string Location {
      get;
      set;
    }
    [XMap]
    public string Dock {
      get;
      set;
    }
    [XMap]
    public string BackColor {
      get;
      set;
    }
    [XMap]
    public string BorderStyle {
      get;
      set;
    }
    [XMap]
    public string TextAlign {
      get;
      set;
    }
    [XMap]
    public string MaxLength {
      get;
      set;
    }
    [XMap]
    public string TabIndex {
      get;
      set;
    }
    [XMap]
    public string Enabled {
      get;
      set;
    }
    [XMap]
    public string InitialFocus {
      get;
      set;
    }
    [XMap]
    public string EventSpec {
      get;
      set;
    }
    [XMap]
    public string Tag {
      get;
      set;
    }
    [XMap(XType=XType.Element)]
    public GridView GridView {
      get;
      set;
    }
    [XMap]
    public bool Debug {
      get;
      set;
    }

    public Type ObjectType {
      get;
      set;
    }
    public object ObjectReference {
      get;
      set;
    }
    public Type ParentObjectType {
      get;
      set;
    }
    public object ParentObjectReference {
      get;
      set;
    }
    public Type TopObjectType {
      get;
      set;
    }
    public object TopObjectReference {
      get;
      set;
    }

    public UIControl()
    {
      this.Name = String.Empty;
      this.Text = String.Empty;
      this.Model = String.Empty;
      this.ModelSort = String.Empty;
      this.ListSource = String.Empty;
      this.ListData = String.Empty;
      this.Type = "System.Object";
      this.AutoSize = false;
      this.Required = false;
      this.Size = "0,0";
      this.Location = "0,0";
      this.Dock = "None";
      this.BackColor = String.Empty;
      this.BorderStyle = "None";
      this.TextAlign = "";
      this.MaxLength = "";
      this.TabIndex = "";
      this.Enabled = "";
      this.InitialFocus = "";
      this.EventSpec = "";
      this.Tag = "";
      this.GridView = new GridView();
      this.Debug = false;

      this.ObjectType = null;
      this.ObjectReference = null;
      this.ParentObjectType = null;
      this.ParentObjectReference = null;
      this.TopObjectType = null;
      this.TopObjectReference = null;
    }

    public void AutoInit()
    {
      switch(this.Type)
      {
        case "Org.UI.NavButton":
          if (this.Name.IsBlank())
            this.Name = this.Text.Replace(" ", String.Empty);
          if (this.Tag.IsBlank())
            this.Tag = this.Name;
          break;
      }
    }

    public TaggedControlSet GetTaggedControlSet()
    {
      var tcs = new TaggedControlSet();

      if (this.Tag.IsNotBlank())
      {
        tcs.Add(this.Tag, new TaggedControl(this.Tag, this, this.ObjectReference));
      }

      foreach(UIControl childControl in this)
      {
        AddTaggedControls(childControl, tcs);
      }

      return tcs;
    }

    public void AddTaggedControls(UIControl control, TaggedControlSet tcs)
    {
      if (control.Tag.IsNotBlank())
      {
        if (tcs.ContainsKey(control.Tag))
          throw new Exception("TaggedControlSet already contains a TaggedControl with tag '" + control.Tag + "'.");

        tcs.Add(control.Tag, new TaggedControl(control.Tag, control, control.ObjectReference));
      }

      foreach(UIControl childControl in control)
      {
        AddTaggedControls(childControl, tcs);
      }
    }
  }
}
