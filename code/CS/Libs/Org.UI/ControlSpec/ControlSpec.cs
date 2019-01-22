using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;

namespace Org.UI
{
  [XMap(XType=XType.Element, CollectionElements="UIControl")]
  public class ControlSpec : List<UIControl>
  {
    private bool _controlsLoaded;
    public Dictionary<string, UIControl> UIControls {
      get;
      set;
    }
    public TaggedControlSet TaggedControlSet {
      get;
      set;
    }

    public ControlSpec()
    {
      _controlsLoaded = false;
      this.UIControls = new Dictionary<string,UIControl>();
      this.TaggedControlSet = new TaggedControlSet();
    }

    public void LoadControls()
    {
      if (_controlsLoaded)
        return;

      foreach(UIControl control in this)
      {
        if (this.UIControls.ContainsKey(control.Name))
          throw new Exception("ControlSpec contains duplicate control name '" + control.Name + "'.");

        this.UIControls.Add(control.Name, control);

      }

      _controlsLoaded = true;
    }

    public UIControl GetInitialFocus(string name)
    {
      if (this.UIControls[name] == null)
        return null;

      return GetInitialFocus(this.UIControls[name]);
    }

    private UIControl GetInitialFocus(UIControl control)
    {
      if (control.InitialFocus.ToLower() == "true")
        return control;

      foreach(var childControl in control)
      {
        UIControl focus = GetInitialFocus(childControl);
        if (focus != null)
          return focus;
      }

      return null;
    }

    public string GetControlMap(string name)
    {
      if (this.UIControls[name] == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();
      var control = this.UIControls[name];

      sb.Append(control.Name + " " + control.Type.Split(Constants.DotDelimiter).Last());
      if (control.Model.IsNotBlank())
        sb.Append(" Model:" + control.Model);
      if (control.ObjectReference == null)
        sb.Append(" NULL" + g.crlf);
      else
      {
        object o = control.ObjectReference;
        if (control.ObjectType.FullName.StartsWith("System.Windows.Forms."))
        {
          Control c = (Control) o;
          if (c.Tag != null)
            sb.Append(" Tag:" + c.Tag.ToString());
          sb.Append(" XY:" + c.Location.X.ToString() + "," + c.Location.Y.ToString());
          sb.Append(" WH:" + c.Size.Width.ToString() + "," + c.Size.Width.ToString());
          sb.Append(g.crlf);
        }
        else
          sb.Append(g.crlf);
      }

      int depth = 0;
      foreach(var childControl in control)
        sb.Append(GetControlMap(childControl, depth));

      sb.Append(g.crlf + "TaggedControlSet" + g.crlf);

      foreach(var tc in this.TaggedControlSet)
      {
        sb.Append("  Tag=" + tc.Key.PadTo(20));
        sb.Append("Type=" + tc.Value.UIControl.Type + " ");
        if (tc.Value.ControlObject == null)
          sb.Append("null)");
        else
        {
          if (tc.Value.ControlObjectType.FullName.StartsWith("System.Windows.Forms."))
          {
            Control c = (Control)tc.Value.ControlObject;
            sb.Append(" Name=" + c.Name + "  Tag=" + c.Tag.ToString());
          }
          else
          {
            sb.Append("ObjectType=" + tc.Value.ControlObjectType.FullName);
          }
        }
        sb.Append(g.crlf);
      }

      return sb.ToString();
    }

    private string GetControlMap(UIControl control, int depth)
    {
      depth++;
      StringBuilder sb = new StringBuilder();
      string indent = g.BlankString(depth * 4);

      sb.Append(indent + control.Name + " " + control.Type.Split(Constants.DotDelimiter).Last());
      if (control.ObjectReference == null)
        sb.Append(" NULL" + g.crlf);
      else
      {
        object o = control.ObjectReference;
        if (control.ObjectType.FullName.StartsWith("System.Windows.Forms."))
        {
          Control c = (Control) o;
          if (c.Tag != null)
            sb.Append(" Tag:" + c.Tag.ToString());
          sb.Append(" XY:" + c.Location.X.ToString() + "," + c.Location.Y.ToString());
          sb.Append(" WH:" + c.Size.Width.ToString() + "," + c.Size.Width.ToString());
          if (c.GetType().Name == "DataGridView")
          {
            sb.Append(g.crlf);
            DataGridView gv = (DataGridView) c;
            GridView gvSpec = control.GridView;
            foreach(var col in gvSpec)
            {
              sb.Append(indent + "    Col Name:" + col.Name + " Tag:" + col.Tag + (col.Visible ? String.Empty : " (not visible)") + g.crlf);
            }
          }
          sb.Append(g.crlf);
        }
      }

      foreach(var childControl in control)
      {
        sb.Append(GetControlMap(childControl, depth));
      }

      return sb.ToString();
    }
  }
}
