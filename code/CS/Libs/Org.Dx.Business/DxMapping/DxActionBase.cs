using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxActionBase : IDxAction, IDisposable
  {
    public event Action<DxWorkbook> OnNewWorkbookVersion;
    protected MapEngine MapEngine { get; private set; }
    protected DxActionParms DxActionParms { get; private set; }
    protected DxWorkbook SourceWorkbook { get; set; }
    protected DxWorkbook TargetWorkbook { get; set; }
    protected DxMapSet DxMapSet { get; set; }

    public DxActionBase(MapEngine mapEngine, DxActionParms dxActionParms)
    {
      this.MapEngine = mapEngine;
      this.DxActionParms = dxActionParms;
    }

    public virtual DxWorkbook Execute(DxWorkbook srcWb, DxMapSet dxMapSet)
    {
      throw new NotImplementedException("The Execute method must be overridden in derived classes.");
    }

    public void FireOnNewWorkbookVersion(DxWorkbook wb)
    {
      if (this.OnNewWorkbookVersion != null && wb != null)
        OnNewWorkbookVersion(wb);
    }

    public void Dispose() { }
  }
}
