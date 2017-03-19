using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.UI
{
  public class PanelField
  {
    public string Tag { get; set; }
    public object GridValue { get; set; }
    public bool IsDbNullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsRequired { get; set; }
    public object FormValue { get; set; }
    public bool IsDirty { get { return Get_IsDirty(); } }
    public bool IsComplete { get { return Get_IsComplete(); } }

    public PanelField()
    {
      this.Tag = String.Empty;
      this.GridValue = null; 
      this.IsDbNullable = false;
      this.IsPrimaryKey = false; 
      this.IsRequired = false;
      this.FormValue = null; 
    }

    private bool Get_IsDirty()
    {
      if (this.GridValue == null)
        return this.FormValue != null; 

      string gridValue = this.GridValue == null ? String.Empty : this.GridValue.ToString().Trim();
      string formValue = this.FormValue == null ? String.Empty : this.FormValue.ToString().Trim();

      return gridValue != formValue;
    }

    private bool Get_IsComplete()
    {
      if (!this.IsRequired)
        return true;

      if (this.FormValue == null)
        return false;

      return this.FormValue.ToString().Trim().Length > 0; 
    }
  }
}
