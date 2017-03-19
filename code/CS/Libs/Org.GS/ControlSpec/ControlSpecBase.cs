using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Controls
{
  public class ControlSpecBase
  {
    private string _tag; 
    [XMap]
    public string Tag 
    {
      get { return this._tag; }
      set
      {
        if (this.Name == null)
          this.Name = value;
        this._tag = value;
      }
    }

    public string _name;
    [XMap(IsKey=true)]
    public string Name 
    {
      get { return this._name; }
      set { this._name = value; }
    }

    private string _text; 
    [XMap]
    public string Text 
    {
      get { return this._text; }
      set { this._text = value; }
    }

    [XMap]
    public virtual ControlType ControlType { get; set; }

    public ControlSpecBase()
    {
      this._tag = String.Empty;
      this._name = null;
      this._text = String.Empty;
    }
  }
}
