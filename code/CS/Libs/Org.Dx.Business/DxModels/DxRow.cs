using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxRow : DxCellSet
  {
    public string Report { get { return base.Get_Report(); } }
    public string VerticalReport { get { return base.Get_VerticalReport(); } }

    public DxRow(DxRowSet dxRowSet)
      : base(dxRowSet)
    {
      this.DxObject = this;
    }

    public DxRow Clone(DxRowSet cloneDxRowSet)
    {
      try
      {
        var clone = new DxRow(cloneDxRowSet);
        foreach (var cell in this.Cells)
        {
          clone.Add(cell.Key, cell.Value.Clone(clone));
        }

        clone.EnsureParentage();
        return clone;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to clone the DxRow with vertical report '" + this.VerticalReport + "'.", ex);
      }
    }
  }
}
