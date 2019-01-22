using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class EnvironmentSet : SortedDictionary<int, Environment>
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

        public EnvironmentSet(MetricsData data)
        {
            _data = data;
        }

        public void PopulateEnvironmentSet()
        {
            string sql = 
                "select EnvironmentID, EnvironmentDesc " +
                "from tblEnvironment " +
                "order by EnvironmentID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Environment e = new Environment();

                    if (!row.IsNull("EnvironmentID"))
                        e.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
                    if (!row.IsNull("EnvironmentDesc"))
                        e.EnvironmentDesc = Convert.ToString(row["EnvironmentDesc"]);

                    this.Add(e.EnvironmentID, e);
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
