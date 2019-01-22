using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
  public class SystemSet : SortedDictionary<int, TFSystem>
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

    public SystemSet(MetricsData data)
    {
      _data = data;
    }


    public void PopulateSystems()
    {
      string sql =
        "select SystemID, SystemDesc " +
        "from tblSystem " +
        "order by SystemID";

      try
      {
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          TFSystem s = new TFSystem();

          if (!row.IsNull("SystemID"))
            s.SystemID = Convert.ToInt32(row["SystemID"]);
          if (!row.IsNull("SystemDesc"))
            s.SystemDesc = Convert.ToString(row["SystemDesc"]);

          this.Add(s.SystemID, s);
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
