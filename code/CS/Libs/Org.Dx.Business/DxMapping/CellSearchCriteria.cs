using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class CellSearchCriteria
  {
    private DxSearchTarget _searchTarget;
    private CellSearchCriteriaSet _parent;
    private string _rawSearchSpec;
    public int? RowIndexSpec { get; private set; }
    public int? ColIndexSpec { get; private set; }
    public string Report { get { return Get_Report(); } }
    public TextNodeSpec TextNodeSpec { get; set; }

    public CellSearchCriteria(CellSearchCriteriaSet parent, DxSearchTarget searchTarget, string searchSpec)
    {
      try
      {
        _searchTarget = searchTarget;
        _parent = parent;
        _rawSearchSpec = searchSpec;

        ParseSpec();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the CellSearchCriteria object.", ex);
      }
    }

    private void ParseSpec()
    {
      if (_rawSearchSpec.IsBlank())
        throw new Exception("The searchSpec is " + (_rawSearchSpec == null ? "null" : "blank") + ".");

      this.RowIndexSpec = null;
      this.ColIndexSpec = null;

      int openPar = _rawSearchSpec.IndexOf("(");
      if (openPar == -1)
        throw new Exception("The cell-level searchSpec format is invalid - it must contain a cell index specifier (an integer cell index or '*' indicating 'any cell'" + 
                            " followed by a valid TextNodeSpec enclosed in parentheses - for example '*(Total)' or '5([dec]/ge:1000)' etc.");

      string indicesSpec = _rawSearchSpec.Substring(0, openPar).Trim();
      string tnsToken = _rawSearchSpec.Substring(openPar).Trim();

      // get the specified row and/or column indices to search in
      switch (_searchTarget)
      {
        case DxSearchTarget.DxRowIndex:
        case DxSearchTarget.DxColumnIndex:
          string indexToken = indicesSpec.Trim();
          if (indexToken != "*")
          {
            if (!indexToken.IsValidInteger())
              throw new Exception("The index token '" + indexToken + "' specified in the " + (_searchTarget == DxSearchTarget.DxColumnIndex ? " column " : " row ") +
                                  "search specification is invalid - it must be an integer or an asterisk.");

            // If we're searching for a column, the spec indicates "which row" and vice-versa.
            if (_searchTarget == DxSearchTarget.DxColumnIndex)
            {
              this.RowIndexSpec = indexToken.ToInt32();
            }
            else
            {
              this.ColIndexSpec = indexToken.ToInt32();
            }
          }
          break;

        case DxSearchTarget.DxCell:
          string[] indexTokens = indicesSpec.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
          if (indexTokens.Length != 2)
            throw new Exception("Both the row and column index tokens must be supplied when searching for a cell. Found '" + indicesSpec + "', expecting " +
                                "two integers separated by a comma (either or both integers may be replaced by an asterisk).");

          string rowIndexToken = indexTokens[0].Trim();
          string colIndexToken = indexTokens[1].Trim();

          if (rowIndexToken != "*")
          {
            if (!rowIndexToken.IsValidInteger())
            {
              throw new Exception("The row index token '" + rowIndexToken + "' specified in the cell search specification is invalid - " +
                    "it must be an integer or an asterisk.");
            }
            else
            {
              this.RowIndexSpec = rowIndexToken.ToInt32();
            }
          }

          if (colIndexToken != "*")
          {
            if (!colIndexToken.IsValidInteger())
            {
              throw new Exception("The column index token '" + colIndexToken + "' specified in the cell search specification is invalid - " +
                    "it must be an integer or an asterisk.");
            }
            else
            {
              this.ColIndexSpec = colIndexToken.ToInt32();
            }
          }
          break;
      }
      
      if (!tnsToken.StartsWith("(") || !tnsToken.EndsWith(")"))
        throw new Exception("The TextNodeSpec (second token) of the searchSpec '" + _rawSearchSpec + "' must be wrapped with parentheses in the searchSpec.");

      string tns = tnsToken.Substring(1, tnsToken.Length - 2);

      this.TextNodeSpec = new TextNodeSpec(tns);
    }
    
    private string Get_Report()
    {
      return _rawSearchSpec;
    }
  }
}
