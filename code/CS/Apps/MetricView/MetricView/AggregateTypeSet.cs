using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class AggregateTypeSet : SortedList<int, AggregateType>
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

        public AggregateTypeSet(MetricsData data)
        {
            _data = data;
        }


        public void PopulateAggregateTypes()
        {
            string sql =
                "select AggregateTypeID, AggregateTypeDesc " +
                "from tblAggregateType " +
                "order by AggregateTypeID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AggregateType at = new AggregateType();

                    if (!row.IsNull("AggregateTypeID"))
                        at.AggregateTypeID = Convert.ToInt32(row["AggregateTypeID"]);
                    if (!row.IsNull("AggregateTypeDesc"))
                        at.AggregateTypeDesc = Convert.ToString(row["AggregateTypeDesc"]);

                    this.Add(at.AggregateTypeID, at);
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
