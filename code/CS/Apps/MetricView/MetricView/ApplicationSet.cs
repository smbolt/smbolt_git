using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Teleflora.Operations.MetricView
{
    public class ApplicationSet : SortedList<int, TFApplication>
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

        public ApplicationSet(MetricsData data)
        {
            _data = data;
        }

        public void PopulateApplications()
        {
            string sql =
                "select ApplicationID, ApplicationName, ApplicationTypeID " +
                "from tblApplication " +
                "order by ApplicationID";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TFApplication a = new TFApplication();

                    if (!row.IsNull("ApplicationID"))
                        a.ApplicationID = Convert.ToInt32(row["ApplicationID"]);
                    if (!row.IsNull("ApplicationName"))
                        a.ApplicationName = Convert.ToString(row["ApplicationName"]);
                    if (!row.IsNull("ApplicationTypeID"))
                        a.ApplicationTypeID = Convert.ToInt32(row["ApplicationTypeID"]);

                    this.Add(a.ApplicationID, a);
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
