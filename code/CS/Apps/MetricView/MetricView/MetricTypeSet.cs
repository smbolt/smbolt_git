using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class MetricTypeSet : SortedList<int, MetricType>
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

        public MetricTypeSet(MetricsData data)
        {
            _data = data;
        }

        public void PopulateMetricTypes()
        {
            string sql =
                "select MetricTypeID, MetricTypeDesc " +
                "from tblMetricType " +
                "order by MetricTypeID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MetricType mt = new MetricType();

                    if (!row.IsNull("MetricTypeID"))
                        mt.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
                    if (!row.IsNull("MetricTypeDesc"))
                        mt.MetricTypeDesc = Convert.ToString(row["MetricTypeDesc"]);

                    this.Add(mt.MetricTypeID, mt);
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
