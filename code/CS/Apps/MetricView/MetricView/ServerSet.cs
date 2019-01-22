using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class ServerSet : SortedList<int, Server>
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

        public ServerSet(MetricsData data)
        {
            _data = data;
        }

        public void PopulateServers()
        {
            string sql =
                "select ServerID, ServerDesc " +
                "from tblServer " +
                "order by ServerID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Server s = new Server();

                    if (!row.IsNull("ServerID"))
                        s.ServerID = Convert.ToInt32(row["ServerID"]);
                    if (!row.IsNull("ServerDesc"))
                        s.ServerDesc = Convert.ToString(row["ServerDesc"]);

                    this.Add(s.ServerID, s);
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
