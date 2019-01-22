using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class MetricStateSet : SortedList<int, MetricState>
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

        public MetricStateSet(MetricsData data)
        {
            _data = data;
        }


        public void PopulateMetricStates()
        {
            string sql =
                "select MetricStateID, MetricStateDesc " +
                "from tblMetricState " +
                "order by MetricStateID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MetricState ms = new MetricState();

                    if (!row.IsNull("MetricStateID"))
                        ms.MetricStateID = Convert.ToInt32(row["MetricStateID"]);
                    if (!row.IsNull("MetricStateDesc"))
                        ms.MetricStateDesc = Convert.ToString(row["MetricStateDesc"]);

                    this.Add(ms.MetricStateID, ms);
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
