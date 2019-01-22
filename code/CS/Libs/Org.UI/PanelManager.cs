using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.UI
{
  public enum PanelMode
  {
    NotSet,
    Display,
    Update,
    Add
  }

  public enum PanelState
  {
    Pristine,
    Dirty
  }

  public class PanelManager
  {
    public PanelMode PanelMode { get; set; }
    public PanelState PanelState { get; set; }
    public Dictionary<string, PanelField> PanelValues { get; set; }
    public bool IsDirty { get { return Get_IsDirty(); } }
    public bool IsComplete { get { return Get_IsComplete(); } }

    public PanelManager()
    {
      this.PanelMode = PanelMode.NotSet; 
      this.PanelState = PanelState.Pristine;
      this.PanelValues = new Dictionary<string, PanelField>();
    }

    public void RestoreGridValues()
    {
      foreach(var panelValue in this.PanelValues.Values)
      {
        panelValue.FormValue = panelValue.GridValue;
      }
    }

    private bool Get_IsDirty()
    {
      return this.PanelValues.Where(x => x.Value.IsDirty).Count() > 0; 
    }

    private bool Get_IsComplete()
    {
      return this.PanelValues.Where(x => !x.Value.IsComplete).Count() == 0; 
    }
  }
}
