using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Teleflora.Operations.MetricView
{
  public class AvailableMetricsReport
  {
    AvailableMetricCollection amc;

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

    public string GetReport(MetricDataObjects dataObjects)
    {
      amc = new AvailableMetricCollection();

      string sql =
        "select count(*) as [Count], " +
        "max(mo.MetricCapturedDateTime) as [MaxCapturedDateTime], " +
        "max(mo.ReceivedFromObserverDateTime) as [MaxReceivedFromObserverDateTime], " +
        "mo.EnvironmentID as [EnvironmentID], " +
        "mo.MetricTypeID as [MetricTypeID], " +
        "mo.Target_SystemID as [TargetSystemID], " +
        "mo.Target_ApplicationID as [TargetApplicationID], " +
        "mo.MetricStateID as [MetricStateID], " +
        "mo.MetricID as [MetricID], " +
        "mo.AggregateTypeID as [AggregateTypeID], " +
        "mo.MetricValueTypeID as [MetricValueTypeID], " +
        "mo.IntervalID as [IntervalID], " +
        "mo.Observer_SystemID as [ObserverSystemID], " +
        "mo.Observer_ApplicationID as [ObserverApplicationID], " +
        "mo.Observer_ServerID as [ObserverServerID] " +
        "from tblMetricObserved mo " +
        "group by " +
        "mo.EnvironmentID, " +
        "mo.MetricTypeID, " +
        "mo.Target_SystemID, " +
        "mo.Target_ApplicationID, " +
        "mo.MetricStateID, " +
        "mo.MetricID, " +
        "mo.AggregateTypeID, " +
        "mo.MetricValueTypeID, " +
        "mo.IntervalID, " +
        "mo.Observer_SystemID, " +
        "mo.Observer_ApplicationID, " +
        "mo.Observer_ServerID " +
        "order by mo.EnvironmentID, " +
        "mo.MetricTypeID, " +
        "mo.Target_SystemID, " +
        "mo.Target_ApplicationID, " +
        "mo.MetricStateID, " +
        "mo.MetricID, " +
        "mo.AggregateTypeID, " +
        "mo.MetricValueTypeID, " +
        "mo.IntervalID, " +
        "mo.Observer_SystemID, " +
        "mo.Observer_ApplicationID, " +
        "mo.Observer_ServerID ";

      try
      {
        SqlDataAdapter da = new SqlDataAdapter(sql, dataObjects.Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          AvailableMetric am = new AvailableMetric();

          if (!row.IsNull("EnvironmentID"))
            am.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);

          if (!row.IsNull("MetricTypeID"))
            am.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);

          if (!row.IsNull("TargetSystemID"))
            am.TargetSystemID = Convert.ToInt32(row["TargetSystemID"]);

          if (!row.IsNull("TargetApplicationID"))
            am.TargetApplicationID = Convert.ToInt32(row["TargetApplicationID"]);

          if (!row.IsNull("MetricStateID"))
            am.MetricStateID = Convert.ToInt32(row["MetricStateID"]);

          if (!row.IsNull("MetricID"))
            am.MetricID = Convert.ToInt32(row["MetricID"]);

          if (!row.IsNull("AggregateTypeID"))
            am.AggregateTypeID = Convert.ToInt32(row["AggregateTypeID"]);

          if (!row.IsNull("MetricValueTypeID"))
            am.MetricValueTypeID = Convert.ToInt32(row["MetricValueTypeID"]);

          if (!row.IsNull("IntervalID"))
            am.IntervalID = Convert.ToInt32(row["IntervalID"]);

          if (!row.IsNull("ObserverSystemID"))
            am.ObserverSystemID = Convert.ToInt32(row["ObserverSystemID"]);

          if (!row.IsNull("ObserverApplicationID"))
            am.ObserverApplicationID = Convert.ToInt32(row["ObserverApplicationID"]);

          if (!row.IsNull("ObserverServerID"))
            am.ObserverServerID = Convert.ToInt32(row["ObserverServerID"]);

          if (!row.IsNull("Count"))
            am.Count = Convert.ToInt32(row["Count"]);

          if (!row.IsNull("MaxCapturedDateTime"))
            am.MaxMetricCapturedDateTime = Convert.ToDateTime(row["MaxCapturedDateTime"]);

          if (!row.IsNull("MaxReceivedFromObserverDateTime"))
            am.MaxReceivedFromObserverDateTime = Convert.ToDateTime(row["MaxReceivedFromObserverDateTime"]);

          amc.Add(am.MaxMetricCapturedDateTime.ToString("yyyy-MM-dd hh:mm:ss:ms") + amc.Count.ToString(), am);

        }

      }
      catch (SqlException ex)
      {
        _errorMessage = ex.Message;
        return("\r\n\r\nERROR\r\n\r\n" + _errorMessage);
      }
      catch (Exception ex)
      {
        _errorMessage = ex.Message;
        return("\r\n\r\nERROR\r\n\r\n" + _errorMessage);
      }

      return BuildReport(amc, dataObjects);
    }

    private string BuildReport(AvailableMetricCollection amc, MetricDataObjects dataObjects)
    {
      StringBuilder sb = new StringBuilder();

      foreach (KeyValuePair<string, AvailableMetric> kvpAM in amc)
      {
        sb.Append("Max Captured Date Time   = " +
                  kvpAM.Value.MaxMetricCapturedDateTime.ToString("yyyy-MM-dd hh:mm:ss:ms") + "\r\n");

        sb.Append("Count                    = " + kvpAM.Value.Count.ToString() + "\r\n");

        sb.Append("Max Recv from ObserverDT = " +
                  kvpAM.Value.MaxReceivedFromObserverDateTime.ToString("yyyy-MM-dd hh:mm:ss:ms") + "\r\n");

        sb.Append("   Environment           = " + kvpAM.Value.EnvironmentID.ToString("0000") +
                  " (" + dataObjects.Environments[kvpAM.Value.EnvironmentID].EnvironmentDesc + ")\r\n");

        sb.Append("   MetricTypeID          = " + kvpAM.Value.MetricTypeID.ToString("0000") +
                  " (" + dataObjects.MetricTypes[kvpAM.Value.MetricTypeID].MetricTypeDesc + ")\r\n");

        sb.Append("   TargetSystemID        = " + kvpAM.Value.TargetSystemID.ToString("0000") +
                  " (" + dataObjects.Systems[kvpAM.Value.TargetSystemID].SystemDesc + ")\r\n");

        sb.Append("   TargetApplicationID   = " + kvpAM.Value.TargetApplicationID.ToString("0000") +
                  " (" + dataObjects.Applications[kvpAM.Value.TargetApplicationID].ApplicationName + ") Type = " +
                  dataObjects.Applications[kvpAM.Value.TargetApplicationID].ApplicationTypeID.ToString("0000") + "\r\n");

        sb.Append("   MetricStateID         = " + kvpAM.Value.MetricStateID.ToString("0000") +
                  " (" + dataObjects.MetricStates[kvpAM.Value.MetricStateID].MetricStateDesc + ")\r\n");

        sb.Append("   MetricID              = " + kvpAM.Value.MetricID.ToString("0000") +
                  " (" + dataObjects.Metrics[kvpAM.Value.MetricID].MetricDesc + ")\r\n");

        sb.Append("   AggregateTypeID       = " + kvpAM.Value.AggregateTypeID.ToString("0000") +
                  " (" + dataObjects.AggregateTypes[kvpAM.Value.AggregateTypeID].AggregateTypeDesc + ")\r\n");

        sb.Append("   MetricValueTypeID     = " + kvpAM.Value.MetricValueTypeID.ToString("0000") +
                  " (" + dataObjects.MetricValueTypes[kvpAM.Value.MetricValueTypeID].MetricValueTypeDesc + ")\r\n");

        sb.Append("   IntervalID            = " + kvpAM.Value.IntervalID.ToString("0000") +
                  " (" + dataObjects.Intervals[kvpAM.Value.IntervalID].IntervalDesc + ")\r\n");

        sb.Append("   ObserverSystemID      = " + kvpAM.Value.ObserverSystemID.ToString("0000") +
                  " (" + dataObjects.Systems[kvpAM.Value.ObserverSystemID].SystemDesc + ")\r\n");

        sb.Append("   ObserverApplicationID = " + kvpAM.Value.ObserverApplicationID.ToString("0000") +
                  " (" + dataObjects.Applications[kvpAM.Value.ObserverApplicationID].ApplicationName + ") Type = " +
                  dataObjects.Applications[kvpAM.Value.ObserverApplicationID].ApplicationTypeID.ToString("0000") + "\r\n");

        sb.Append("   ObserverServerID      = " + kvpAM.Value.ObserverServerID.ToString("0000") +
                  " (" + dataObjects.Servers[kvpAM.Value.ObserverServerID].ServerDesc + ")\r\n");


        sb.Append("\r\n");

      }

      return sb.ToString();
    }

  }
}
