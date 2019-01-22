using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO; 

namespace Teleflora.Operations.MetricView
{
    public class MetricObservationSet : System.Collections.Generic.SortedList<int, MetricObservation>
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

        private float _highMetricValue;
        public float HighMetricValue
        {
            get { return _highMetricValue; }
            set { _highMetricValue = value; }
        }

        private float _lowMetricValue;
        public float LowMetricValue
        {
            get { return _lowMetricValue; }
            set { _lowMetricValue = value; }
        }

        private string _legendLabel;
        public string LegendLabel
        {
            get { return _legendLabel; }
            set { _legendLabel = value; }
        }

        public MetricObservationSet(MetricsData data)
        {
            _data = data;
            _highMetricValue = 0F;
            _lowMetricValue = 0F;
            _legendLabel = String.Empty;
        }

        public MetricObservationSet()
        {
            _highMetricValue = 0F;
            _lowMetricValue = 0F;
            _legendLabel = String.Empty;
        }

        public long PopulateMetricObservationsFromFile(SpecificMetric sm, MetricQueryParms parms)
        {
            char[] delim = new char[] {','};

            long beginTicks = DateTime.Now.Ticks;
            this.LegendLabel = sm.LegendLabel;
            StreamReader sr = new StreamReader(sm.MetricFileName);

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                if (line.Substring(0, 1) != "*")
                {
                    string[] s = line.Split(delim);
                    DateTime obsvDateTime = Convert.ToDateTime(s[15]);

                    if (obsvDateTime >= parms.FromDateTime && obsvDateTime <= parms.ToDateTime &&
                        sm.ObserverServerID == Convert.ToInt32(s[4]) && sm.TargetApplicationID == Convert.ToInt32(s[6]))
                    {
                        if ((sm.UseYOYData && Convert.ToInt32(s[10]) == 3) ||
                            (!sm.UseYOYData && Convert.ToInt32(s[10]) == 2))
                        {
                            MetricObservation m = new MetricObservation();

                            m.MetricObservedID = Convert.ToInt32(s[0]);
                            m.ReceivedFromObserverDateTime = Convert.ToDateTime(s[1]);
                            m.ObserverSystemID = Convert.ToInt32(s[2]);
                            m.ObserverApplicationID = Convert.ToInt32(s[3]);
                            m.ObserverServerID = Convert.ToInt32(s[4]);
                            m.TargetSystemID = Convert.ToInt32(s[5]);
                            m.TargetApplicationID = Convert.ToInt32(s[6]);
                            m.EnvironmentID = Convert.ToInt32(s[7]);
                            m.AggregateTypeID = Convert.ToInt32(s[8]);
                            m.MetricID = Convert.ToInt32(s[9]);
                            m.MetricStateID = Convert.ToInt32(s[10]);
                            m.MetricTypeID = Convert.ToInt32(s[11]);
                            m.MetricValueTypeID = Convert.ToInt32(s[12]);
                            m.IntervalID = Convert.ToInt32(s[13]);
                            m.MetricValue = (float)Convert.ToDouble(s[14]);
                            if (sm.Multiplier != 0)
                                m.MetricValue = m.MetricValue * sm.Multiplier;
                            m.MetricCapturedDateTime = Convert.ToDateTime(s[15]);
                            m.MetricDuration = ConvertToTimeSpan(s[16]);

                            if (m.MetricValue > this.HighMetricValue)
                                this.HighMetricValue = m.MetricValue;

                            if (m.MetricValue < this.LowMetricValue)
                                this.LowMetricValue = m.MetricValue;

                            this.Add(this.Count, m);
                        }
                    }
                }
            }

            long durationTicks = DateTime.Now.Ticks - beginTicks;
            return durationTicks;
        }

        public long PopulateMetricObservations(SpecificMetric sm, MetricQueryParms parms)
        {
            long beginTicks = DateTime.Now.Ticks;
            this.LegendLabel = sm.LegendLabel;

            string sql = BuildMetricsQuery(sm, parms);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (parms.UseMostCurrentMetric)  // query selects "top" from a descending sort, so we need to reverse the order
                {
                    for (int i = ds.Tables[0].Rows.Count - 1; i > -1; i--)
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        MetricObservation m = BuildMetricObservation(row);

                        if (sm.Multiplier != 0)
                            m.MetricValue = m.MetricValue * sm.Multiplier;

                        this.Add(this.Count, m);
                    }
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        MetricObservation m = BuildMetricObservation(row);

                        if (sm.Multiplier != 0)
                            m.MetricValue = m.MetricValue * sm.Multiplier;

                        this.Add(this.Count, m);
                    }
                }
            }
            catch (SqlException ex)
            {
                return -1;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                MessageBox.Show(_errorMessage);
            }


            long durationTicks = DateTime.Now.Ticks - beginTicks;
            return durationTicks;
        }



        private MetricObservation BuildMetricObservation(DataRow row)
        {
            MetricObservation m = new MetricObservation();

                //change made to patch integer overflow... 
            //if (!row.IsNull("MetricObservedID"))
            //    m.MetricObservedID = Convert.ToInt32(row["MetricObservedID"]);

            if (!row.IsNull("MetricObservedID"))
            {
                Int64 MetricObservedID64 = Convert.ToInt64(row["MetricObservedID"]);
                while (MetricObservedID64 > 2000000000)
                    MetricObservedID64 -= 1000000000;
                m.MetricObservedID = Convert.ToInt32(MetricObservedID64);
            }



            if (!row.IsNull("ReceivedFromObserverDateTime"))
                m.ReceivedFromObserverDateTime = Convert.ToDateTime(row["ReceivedFromObserverDateTime"]);
            if (!row.IsNull("Observer_SystemID"))
                m.ObserverSystemID = Convert.ToInt32(row["Observer_SystemID"]);
            if (!row.IsNull("Observer_ApplicationID"))
                m.ObserverApplicationID = Convert.ToInt32(row["Observer_ApplicationID"]);
            if (!row.IsNull("Observer_ServerID"))
                m.ObserverServerID = Convert.ToInt32(row["Observer_ServerID"]);
            if (!row.IsNull("Target_SystemID"))
                m.TargetSystemID = Convert.ToInt32(row["Target_SystemID"]);
            if (!row.IsNull("Target_ApplicationID"))
                m.TargetApplicationID = Convert.ToInt32(row["Target_ApplicationID"]);
            if (!row.IsNull("EnvironmentID"))
                m.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
            if (!row.IsNull("AggregateTypeID"))
                m.AggregateTypeID = Convert.ToInt32(row["AggregateTypeID"]);
            if (!row.IsNull("MetricID"))
                m.MetricID = Convert.ToInt32(row["MetricID"]);
            if (!row.IsNull("MetricStateID"))
                m.MetricStateID = Convert.ToInt32(row["MetricStateID"]);
            if (!row.IsNull("MetricTypeID"))
                m.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
            if (!row.IsNull("MetricValueTypeID"))
                m.MetricValueTypeID = Convert.ToInt32(row["MetricValueTypeID"]);
            if (!row.IsNull("IntervalID"))
                m.IntervalID = Convert.ToInt32(row["IntervalID"]);
            if (!row.IsNull("MetricValue"))
                m.MetricValue = (float)Convert.ToDouble(row["MetricValue"]);
            if (!row.IsNull("MetricCapturedDateTime"))
                m.MetricCapturedDateTime = Convert.ToDateTime(row["MetricCapturedDateTime"]);
            if (!row.IsNull("MetricDuration"))
                m.MetricDuration = ConvertToTimeSpan(row["MetricDuration"].ToString());

            if (m.MetricValue > this.HighMetricValue)
                this.HighMetricValue = m.MetricValue;

            if (m.MetricValue < this.LowMetricValue)
                this.LowMetricValue = m.MetricValue;

            return m;
        }

        private TimeSpan ConvertToTimeSpan(string duration)
        {
            TimeSpan ts = TimeSpan.MinValue;
            return ts;
        }

        private string BuildMetricsQuery(SpecificMetric sm, MetricQueryParms parms)
        {
            StringBuilder sb = new StringBuilder();

            if (parms.UseMostCurrentMetric)
            {
                sb.Append(" select top " + parms.DataPoints.ToString().Trim() + " ");
            }
            else
            {
                sb.Append(" select ");
            }

            sb.Append("MetricObservedID, " +
                    "ReceivedFromObserverDateTime, " +
                    "Observer_SystemID, " +
                    "Observer_ApplicationID, " +
                    "Observer_ServerID, " +
                    "Target_SystemID, " +
                    "Target_ApplicationID, " +
                    "EnvironmentID, " +
                    "AggregateTypeID, " +
                    "MetricID, " +
                    "MetricStateID, " +
                    "MetricTypeID, " +
                    "MetricValueTypeID, " +
                    "IntervalID, " +
                    "MetricValue, " +
                    "MetricCapturedDateTime, " +
                    "MetricDuration " +
                "from tblMetricObserved " +
                "where " +
                    "EnvironmentID = " + sm.EnvironmentID.ToString().Trim() + " and " +
                    "Target_SystemID = " + sm.TargetSystemID.ToString().Trim() + " and " +
                    "Target_ApplicationID = " + sm.TargetApplicationID.ToString().Trim() + " and " +
                    "Observer_SystemID = " + sm.ObserverSystemID.ToString().Trim() + " and " +
                    "Observer_ApplicationID = " + sm.ObserverApplicationID.ToString().Trim() + " and " +
                    "Observer_ServerID = " + sm.ObserverServerID.ToString().Trim() + " and " +
                    "MetricTypeID = " + sm.MetricTypeID.ToString().Trim() + " and " +
                    "MetricStateID = " + sm.MetricStateID.ToString().Trim() + " and " +
                    "MetricID = " + sm.MetricID.ToString().Trim() + " and " +
                    "AggregateTypeID = " + sm.AggregateTypeID.ToString().Trim() + " and " +
                    "MetricValueTypeID = " + sm.MetricValueTypeID.ToString().Trim() + " and " +
                    "IntervalID = " + sm.IntervalID.ToString().Trim() + " ");

            if (!parms.UseMostCurrentMetric)
            {
                sb.Append(" and MetricCapturedDateTime >= '" +
                    parms.FromDateTime.ToString("yyyy-MM-dd") + "T" + parms.FromDateTime.ToString("HH:mm:ss.fff") +
                    "' and MetricCapturedDateTime <= '" +
                    parms.ToDateTime.ToString("yyyy-MM-dd") + "T" + parms.ToDateTime.ToString("HH:mm:ss.fff") + "' ");
                sb.Append(" order by MetricCapturedDateTime ");
            }
            else
            {
                sb.Append(" order by MetricCapturedDateTime desc ");
            }


            return sb.ToString();
        }


        public long PopulateESQLMGMT_MetricObservations(SpecificMetric sm, MetricQueryParms parms)
        {
            long beginTicks = DateTime.Now.Ticks;
            this.LegendLabel = sm.LegendLabel;

            string sql = BuildESQLMGMT_MetricsQuery(sm, parms);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, Data.ConnectionESQLMGMT);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (parms.UseMostCurrentMetric)  // query selects "top" from a descending sort, so we need to reverse the order
                {
                    for (int i = ds.Tables[0].Rows.Count - 1; i > -1; i--)
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        MetricObservation m = BuildMetricObservation(row);
                        this.Add(this.Count, m);
                    }
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        MetricObservation m = BuildMetricObservation(row);
                        this.Add(this.Count, m);
                    }
                }
            }
            catch (SqlException ex)
            {
                _errorMessage = ex.Message;
                MessageBox.Show(_errorMessage);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                MessageBox.Show(_errorMessage);
            }

            long durationTicks = DateTime.Now.Ticks - beginTicks;
            return durationTicks;
        }

        private string BuildESQLMGMT_MetricsQuery(SpecificMetric sm, MetricQueryParms parms)
        {
            StringBuilder sb = new StringBuilder();

            if (parms.UseMostCurrentMetric)
            {
                sb.Append(" select top " + parms.DataPoints.ToString().Trim() + " ");
            }
            else
            {
                sb.Append(" select ");
            }

            sb.Append(" 0 as MetricObservedID, " +
                    "CounterData.CounterDateTime as ReceivedFromObserverDateTime, " +
                    sm.ObserverSystemID.ToString().Trim() + " as Observer_SystemID, " +
                    sm.ObserverApplicationID.ToString().Trim() + " as Observer_ApplicationID, " +
                    sm.ObserverServerID.ToString().Trim() + " as Observer_ServerID, " +
                    sm.TargetSystemID.ToString().Trim() + " as Target_SystemID, " +
                    sm.TargetApplicationID.ToString().Trim() + " as Target_ApplicationID, " +
                    sm.EnvironmentID.ToString().Trim() + " as EnvironmentID, " +
                    sm.AggregateTypeID.ToString().Trim() + " as AggregateTypeID, " +
                    sm.MetricID.ToString().Trim() + " as MetricID, " +
                    sm.MetricStateID.ToString().Trim() + " as MetricStateID, " +
                    sm.MetricTypeID.ToString().Trim() + " as MetricTypeID, " +
                    sm.MetricValueTypeID.ToString().Trim() + " as MetricValueTypeID, " +
                    sm.IntervalID.ToString().Trim() + " as IntervalID, " +
                    "cast(cast(CounterData.CounterValue as float)/1000 as float) as MetricValue, " +
                    "CounterData.CounterDateTime as MetricCapturedDateTime, " +
                    "null as MetricDuration " +
                "from CounterDetails join CounterData on CounterDetails.CounterID = CounterData.CounterID " +
                "where " +
                    "CounterDetails.MachineName = '" + GetMachineName(sm.ObserverServerID) + "' and " + 
                    "CounterDetails.CounterName = '" + GetCounterName(sm.MetricID) + "' and " + 
                    "CounterDetails.ObjectName = '" + GetObjectName(sm.TargetApplicationID) + "'  " );

            if (!parms.UseMostCurrentMetric)
            {
                sb.Append(" and CounterData.CounterDateTime >= '" +
                    parms.FromDateTime.ToString("yyyy-MM-dd") + " " + parms.FromDateTime.ToString("HH:mm:ss.fff") +
                    "' and CounterData.CounterDateTime <= '" +
                    parms.ToDateTime.ToString("yyyy-MM-dd") + " " + parms.ToDateTime.ToString("HH:mm:ss.fff") + "' ");
                sb.Append(" order by CounterData.CounterDateTime ");
            }
            else
            {
                sb.Append(" order by CounterData.CounterDateTime desc ");
            }


            return sb.ToString();
        }

        private string GetMachineName(int serverID)
        {
            string machineName = String.Empty;

            switch(serverID)
            {
                case 15:    machineName = @"\\EPRODADMINWEB1"; break;
                case 77:    machineName = @"\\EPRODCCS1"; break;
                case 22:    machineName = @"\\EPRODGW1"; break;
                case 23:    machineName = @"\\EPRODGW2"; break;
                case 26:    machineName = @"\\EPRODMKDG1"; break;
                case 35:    machineName = @"\\EPRODSEARS"; break;
                case 2:     machineName = @"\\EPRODSQL11"; break;
                case 3:     machineName = @"\\EPRODSQL12"; break;
                case 4:     machineName = @"\\EPRODSQL13"; break;
                case 6:     machineName = @"\\EPRODSQL15"; break;
                case 75:    machineName = @"\\EPRODSQL2"; break;
                case 76:    machineName = @"\\EPRODSQL3"; break;
                case 61:    machineName = @"\\EPRODSQL4"; break;
                case 62:    machineName = @"\\EPRODSQL5"; break;
                case 63:    machineName = @"\\EPRODSQL6"; break;
                case 64:    machineName = @"\\EPRODSQL7"; break;
                case 65:    machineName = @"\\EPRODSQL8"; break;
                case 66:    machineName = @"\\EPRODSQL9"; break;
                case 40:    machineName = @"\\EPRODUSAA"; break;
                case 41:    machineName = @"\\EPRODWEB1"; break;
                case 50:    machineName = @"\\EPRODWEB10"; break;
                case 51:    machineName = @"\\EPRODWEB11"; break;
                case 52:    machineName = @"\\EPRODWEB12"; break;
                case 42:    machineName = @"\\EPRODWEB2"; break;
                case 43:    machineName = @"\\EPRODWEB3"; break;
                case 44:    machineName = @"\\EPRODWEB4"; break;
                case 45:    machineName = @"\\EPRODWEB5"; break;
                case 46:    machineName = @"\\EPRODWEB6"; break;
                case 47:    machineName = @"\\EPRODWEB7"; break;
                case 48:    machineName = @"\\EPRODWEB8"; break;
                case 49:    machineName = @"\\EPRODWEB9"; break;
            }
            return machineName;
        }

        private string GetCounterName(int metricID)
        {
            string counterName = String.Empty;

            switch (metricID)
            {
                case 12: counterName = @"% Processor Time"; break;
                case 13: counterName = @"Bytes Total/sec"; break;
                case 14: counterName = @"Connection Attempts/sec"; break;
                case 15: counterName = @"Current Connections"; break;
                case 16: counterName = @"Errors/sec"; break;
                case 17: counterName = @"Interrupts/sec"; break;
                case 18: counterName = @"Not Found Errors/sec"; break;
                case 19: counterName = @"Page Faults/sec"; break;
                case 20: counterName = @"Page Reads/sec"; break;
                case 21: counterName = @"Page Writes/sec"; break;
                case 22: counterName = @"Pages Input/sec"; break;
                case 23: counterName = @"Pages Output/sec"; break;
                case 24: counterName = @"Pages/sec"; break;
                case 25: counterName = @"Processor Queue Length"; break;
                case 26: counterName = @"Request Execution Time"; break;
                case 27: counterName = @"Request Wait Time"; break;
                case 28: counterName = @"Requests Executing"; break;
                case 29: counterName = @"Requests Queued"; break;
                case 30: counterName = @"Requests Rejected"; break;
                case 31: counterName = @"Requests Timed Out"; break;
                case 32: counterName = @"Requests/sec"; break;
            }
            return counterName;
        }

        private string GetObjectName(int applicationID)
        {
            string objectName = String.Empty;

            switch (applicationID)
            {
                case 302: objectName = @"System"; break;
                case 303: objectName = @"Processor"; break;
                case 304: objectName = @"Memory"; break;
                case 305: objectName = @"Web Service"; break;
                case 306: objectName = @"Active Server Pages"; break;
            }
            return objectName;
        }

    }
}
