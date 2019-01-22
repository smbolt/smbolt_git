using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxCellLocator : DxLocatorBase
  {
    public DxCellLocator(DxFilter dxFilter, string rawSearchSpec)
      : base(rawSearchSpec)
    {
      try
      {
        base.CellSearchCriteriaSet = new CellSearchCriteriaSet();
        base.ParseSpec();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the DxCellLocator using RawSearchSpec '" + rawSearchSpec + "'.", ex);
      }
    }
  }


}
