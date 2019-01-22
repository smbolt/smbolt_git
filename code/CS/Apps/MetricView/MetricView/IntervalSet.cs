using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
  public class IntervalSet : SortedList<int, Interval>
  {
    private MetricsData _data;
    public MetricsData Data
    {
      get {
        return _data;
      }
      set {
        _data = value;
      }
    }

    private string _errorMessage;
    public string ErrorMessage
    {
      get {
        return _errorMessage;
      }
      set {
        _errorMessage = value;
      }
    }

    public IntervalSet(MetricsData data)
    {
      _data = data;
    }

    public void PopulateIntervals()
    {
      string sql =
        "select IntervalID, IntervalDesc " +
        "from tblInterval " +
        "order by IntervalID";

      try
      {
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          Interval i = new Interval();

          if (!row.IsNull("IntervalID"))
            i.IntervalID = Convert.ToInt32(row["IntervalID"]);
          if (!row.IsNull("IntervalDesc"))
            i.IntervalDesc = Convert.ToString(row["IntervalDesc"]);

          this.Add(i.IntervalID, i);
        }
      }
      catch (SqlException ex)
      {
        _errorMessage = ex.Message;
      }
      catch (Exception ex)
      {
        _errorMessage = ex.Message;
      }
    }
  }
}
