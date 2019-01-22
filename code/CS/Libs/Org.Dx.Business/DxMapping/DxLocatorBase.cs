using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxLocatorBase
  {
    public IndexType IndexType { get; protected set; }
    public CellSearchCriteriaSet CellSearchCriteriaSet { get; protected set; }
    protected string RawSearchSpec { get; private set; }
    public string Report { get { return Get_Report(); } }
    private DxSearchTarget _dxSearchTarget;

    public DxLocatorBase(string rawSearchSpec)
    {
      this.RawSearchSpec = rawSearchSpec;
    }

    public void ParseSpec()
    {
      try
      {
        switch (this.IndexType)
        {
          case IndexType.RowIndex:
            _dxSearchTarget = DxSearchTarget.DxRowIndex;
            break;
          case IndexType.ColumnIndex:
            _dxSearchTarget = DxSearchTarget.DxColumnIndex;
            break;
          default:
            _dxSearchTarget = DxSearchTarget.DxCell;
            break;
        }

        this.CellSearchCriteriaSet = new CellSearchCriteriaSet(this.RawSearchSpec, this.IndexType, _dxSearchTarget);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to parse the searchSpec '" + this.RawSearchSpec + ".", ex);
      }
    }
    
    private string Get_Report()
    {

      return "Locator Report.";
    }
  }
}
