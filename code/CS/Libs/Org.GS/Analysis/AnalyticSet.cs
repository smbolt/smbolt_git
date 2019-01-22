using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Analysis
{
  public class AnalyticSet : Dictionary<int, Analytic>
  {
    public string RunAnalytics()
    {
      if (this.Count == 0)
        return "No items to analyze";

      int colCount = this.Values.First().DataArray.Length;

      var sb = new StringBuilder();

      // compute raw column density
      var colDensity = new Dictionary<int, int>();

      for (int c = 0; c < colCount - 1; c++)
      {
        // loop through each record
        for (int i = 0; i < this.Count - 1; i++)
        {
          var item = this.Values.ElementAt(i);

          if (!colDensity.ContainsKey(c))
            colDensity.Add(c, 0);
          colDensity[c] += item.DataArray[c];
        }
      }

      var colDensitySort = new Dictionary<int, int>();
      foreach (KeyValuePair<int, int> kvp in colDensity.OrderByDescending(kvp => kvp.Value))
      {
        colDensitySort.Add(kvp.Key, kvp.Value);
      }

      var sbColDensity = new StringBuilder();

      foreach (var kvp in colDensitySort)
      {
        int colNbr = kvp.Key;
        int density = kvp.Value;
        float percent = (float) density / this.Values.Count * 100f;
        sbColDensity.Append(colNbr.ToString("000") + " - " + Analytic.ColumnNames[colNbr].PadTo(30) + "  " + 
                            density.ToString("###,##0").PadToJustifyRight(7) + "    " + 
                            percent.ToString("#0.0000").PadToJustifyRight(7) + "%" + g.crlf);
      }

      string colDensityReport = sbColDensity.ToString();

      string report = sb.ToString();

      return colDensityReport;
    }
  }
}
