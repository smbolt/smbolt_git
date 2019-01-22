using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class IndexLocator : DxLocatorBase
  {
    public IndexLocator(IndexType indexType, string rawSearchSpec)
      : base(rawSearchSpec)
    {
      try
      {
        if (indexType == IndexType.NotUsed)
          throw new Exception("The DxIndexLocator class requires a valid IndexType (RowIndex or ColumnIndex), the parameter received is IndexType.NotUsed.");

        this.IndexType = indexType;
        base.CellSearchCriteriaSet = new CellSearchCriteriaSet();

        base.ParseSpec();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the DxIndexLocator using IndexType '" + indexType.ToString() +
                            " and RawSearchSpec '" + rawSearchSpec + "'.", ex);
      }
    }

    public bool RowMatch(DxRow row)
    {
      try
      {
        return base.CellSearchCriteriaSet.RowMatch(row);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to match the row '" + row.VerticalReport.Replace(g.crlf, "  ") +
                            "' using the DxIndexLocator object '" + this.Report + ".", ex);
      }
    }

    public bool ColumnMatch(DxColumn col)
    {
      try
      {
        return base.CellSearchCriteriaSet.ColumnMatch(col);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to match the column '" + col.Report.Replace(g.crlf, "  ") +
                            "' using the DxIndexLocator object '" + this.Report + ".", ex);
      }
    }
  }
}
