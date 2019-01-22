using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class MetricValueTypeSet : SortedList<int, MetricValueType>
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

        public MetricValueTypeSet(MetricsData data)
        {
            _data = data;
        }

        public void PopulateMetricValueTypes()
        {
            string sql =
                "select MetricValueTypeID, MetricValueTypeDesc " +
                "from tblMetricValueType " +
                "order by MetricValueTypeID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MetricValueType mvt = new MetricValueType();

                    if (!row.IsNull("MetricValueTypeID"))
                        mvt.MetricValueTypeID = Convert.ToInt32(row["MetricValueTypeID"]);
                    if (!row.IsNull("MetricValueTypeDesc"))
                        mvt.MetricValueTypeDesc = Convert.ToString(row["MetricValueTypeDesc"]);

                    this.Add(mvt.MetricValueTypeID, mvt);
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
