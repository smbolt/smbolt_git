using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class MetricSet : SortedDictionary<int, Metric>
    {
        private MetricsData _data;
        public MetricsData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public MetricSet(MetricsData data)
        {
            _data = data;
        }



        public void PopulateMetrics()
        {
            string sql =
                "select MetricID, MetricDesc " +
                "from tblMetric " +
                "order by MetricID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Metric m = new Metric();

                    if (!row.IsNull("MetricID"))
                        m.MetricID = Convert.ToInt32(row["MetricID"]);
                    if (!row.IsNull("MetricDesc"))
                        m.MetricDesc = Convert.ToString(row["MetricDesc"]);

                    this.Add(m.MetricID, m);
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
