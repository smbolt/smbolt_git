using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Teleflora.Operations.MetricView
{
  public class AvailableMetricSet : SortedList<int, SpecificMetric>
  {
    private MetricsData _data;
    public MetricsData Data
    {
      get {
        return _data;
      }
      set {
        _data = value;
      }
    }

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

    private bool _useMostCurrentMetrics;
    public bool UseMostCurrentMetric
    {
      get {
        return _useMostCurrentMetrics;
      }
      set {
        _useMostCurrentMetrics = value;
      }
    }

    private int _dataPoints;
    public int DataPoints
    {
      get {
        return _dataPoints;
      }
      set {
        _dataPoints = value;
      }
    }

    private DateTime _fromDateTime;
    public DateTime FromDateTime
    {
      get {
        return _fromDateTime;
      }
      set {
        _fromDateTime = value;
      }
    }

    private DateTime _toDateTime;
    public DateTime ToDateTime
    {
      get {
        return _toDateTime;
      }
      set {
        _toDateTime = value;
      }
    }

    public AvailableMetricSet(MetricsData data)
    {
      _data = data;
      _errorMessage = String.Empty;
      _useMostCurrentMetrics = true;
      _dataPoints = 100;
      _fromDateTime = DateTime.MinValue;
      _toDateTime = DateTime.MinValue;
    }

    public void PopulateAvailableMetricsDistinct(MetricQueryParms parms)
    {
      string whereClause = GetWhereClause(parms);

      string sql =
        "select " +
        "mo.EnvironmentID as [EnvironmentID], " +
        "mo.MetricTypeID as [MetricTypeID] , " +
        "mo.Target_SystemID as [TgtSystemID], " +
        "mo.Target_ApplicationID as [TgtApplID], " +
        "mo.MetricStateID as [MetricStateID], " +
        "mo.MetricID as [MetricID], " +
        "mo.AggregateTypeID as [AggregateTypeID], " +
        "mo.MetricValueTypeID as [MetricValueTypeID], " +
        "mo.IntervalID as [IntervalID], " +
        "mo.Observer_SystemID as [ObsvSystemID], " +
        "mo.Observer_ApplicationID as [ObsvApplID], " +
        "mo.Observer_ServerID as [ObsvServerID], " +
        "Count(*) as [Count] " +
        "from tblMetricObserved mo " +
        whereClause + " " +
        "group by mo.EnvironmentID, " +
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
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          SpecificMetric am = new SpecificMetric();
          am.MetricSource = 0;
          if (!row.IsNull("EnvironmentID"))
            am.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
          if (!row.IsNull("MetricTypeID"))
            am.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
          if (!row.IsNull("TgtSystemID"))
            am.TargetSystemID = Convert.ToInt32(row["TgtSystemID"]);
          if (!row.IsNull("TgtApplID"))
            am.TargetApplicationID = Convert.ToInt32(row["TgtApplID"]);
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
          if (!row.IsNull("ObsvSystemID"))
            am.ObserverSystemID = Convert.ToInt32(row["ObsvSystemID"]);
          if (!row.IsNull("ObsvApplID"))
            am.ObserverApplicationID = Convert.ToInt32(row["ObsvApplID"]);
          if (!row.IsNull("ObsvServerID"))
            am.ObserverServerID = Convert.ToInt32(row["ObsvServerID"]);
          if (!row.IsNull("Count"))
            am.Count = Convert.ToInt32(row["Count"]);

          this.Add(this.Count, am);
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

    }



    public void PopulateAvailableMetricsList(MetricQueryParms parms)
    {
      string whereClause = GetWhereClause(parms);

      string sql =
        "select " +
        "mo.EnvironmentID as [EnvironmentID], " +
        "mo.MetricTypeID as [MetricTypeID] , " +
        "mo.Target_SystemID as [TgtSystemID], " +
        "mo.Target_ApplicationID as [TgtApplID], " +
        "mo.MetricStateID as [MetricStateID], " +
        "mo.MetricID as [MetricID], " +
        "mo.AggregateTypeID as [AggregateTypeID], " +
        "mo.MetricValueTypeID as [MetricValueTypeID], " +
        "mo.IntervalID as [IntervalID], " +
        "mo.Observer_SystemID as [ObsvSystemID], " +
        "mo.Observer_ApplicationID as [ObsvApplID], " +
        "mo.Observer_ServerID as [ObsvServerID], " +
        "Count(*) as [Count] " +
        "from tblMetricObserved mo " +
        whereClause + " " +
        "group by mo.EnvironmentID, " +
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
        "order by mo.Observer_ServerID, " +
        "mo.MetricStateID, " +
        "mo.MetricID, " +
        "mo.MetricValueTypeID, " +
        "mo.IntervalID ";

      try
      {
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          SpecificMetric am = new SpecificMetric();
          am.MetricSource = 0;
          if (!row.IsNull("EnvironmentID"))
            am.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
          if (!row.IsNull("MetricTypeID"))
            am.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
          if (!row.IsNull("TgtSystemID"))
            am.TargetSystemID = Convert.ToInt32(row["TgtSystemID"]);
          if (!row.IsNull("TgtApplID"))
            am.TargetApplicationID = Convert.ToInt32(row["TgtApplID"]);
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
          if (!row.IsNull("ObsvSystemID"))
            am.ObserverSystemID = Convert.ToInt32(row["ObsvSystemID"]);
          if (!row.IsNull("ObsvApplID"))
            am.ObserverApplicationID = Convert.ToInt32(row["ObsvApplID"]);
          if (!row.IsNull("ObsvServerID"))
            am.ObserverServerID = Convert.ToInt32(row["ObsvServerID"]);
          if (!row.IsNull("Count"))
            am.Count = Convert.ToInt32(row["Count"]);

          this.Add(this.Count, am);
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

    }

    public void PopulateAvailableMetrics(MetricQueryParms parms, MetricDataObjects mdo)
    {
      string whereClause = GetWhereClause(parms);

      string sql =
        "select " +
        "mo.EnvironmentID as [EnvironmentID], " +
        "mo.MetricTypeID as [MetricTypeID] , " +
        "mo.Target_SystemID as [Target_SystemID], " +
        "mo.Target_ApplicationID as [Target_ApplicationID], " +
        "mo.MetricStateID as [MetricStateID], " +
        "mo.MetricID as [MetricID], " +
        "mo.AggregateTypeID as [AggregateTypeID], " +
        "mo.MetricValueTypeID as [MetricValueTypeID], " +
        "mo.IntervalID as [IntervalID], " +
        "mo.Observer_SystemID as [Observer_SystemID], " +
        "mo.Observer_ApplicationID as [Observer_ApplID], " +
        "mo.Observer_ServerID as [Observer_ServerID], " +
        "999 as [Count] " +
        "from tblMetricObserved_Distinct mo " +
        whereClause + " " +
        "group by mo.EnvironmentID, " +
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
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.Connection);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          SpecificMetric am = new SpecificMetric();
          am.MetricSource = 0;
          if (!row.IsNull("EnvironmentID"))
            am.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
          if (!row.IsNull("MetricTypeID"))
            am.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
          if (!row.IsNull("Target_SystemID"))
            am.TargetSystemID = Convert.ToInt32(row["Target_SystemID"]);
          if (!row.IsNull("Target_ApplicationID"))
            am.TargetApplicationID = Convert.ToInt32(row["Target_ApplicationID"]);
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
          if (!row.IsNull("Observer_SystemID"))
            am.ObserverSystemID = Convert.ToInt32(row["Observer_SystemID"]);
          if (!row.IsNull("Observer_ApplID"))
            am.ObserverApplicationID = Convert.ToInt32(row["Observer_ApplID"]);
          if (!row.IsNull("Observer_ServerID"))
            am.ObserverServerID = Convert.ToInt32(row["Observer_ServerID"]);
          if (!row.IsNull("Count"))
            am.Count = Convert.ToInt32(row["Count"]);

          am.LegendLabel = mdo.Systems[am.TargetSystemID].SystemDesc + " - " +
                           mdo.Servers[am.ObserverServerID].ServerDesc + " - " +
                           mdo.Metrics[am.MetricID].MetricDesc;

          this.Add(this.Count, am);
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

    }


    private string GetWhereClause(MetricQueryParms parms)
    {
      bool IsAndNeeded = false;
      string whereClause = String.Empty;

      if (!parms.IsEnvironmentIDSpecified &&
          !parms.IsTargetSystemIDSpecified &&
          !parms.IsTargetApplicationIDSpecified &&
          !parms.IsServerListSpecified)
        return String.Empty;

      whereClause += " where ";

      if (parms.IsEnvironmentIDSpecified)
      {
        whereClause += " mo.EnvironmentID = " + parms.EnvironmentID.ToString().Trim() + " ";
        IsAndNeeded = true;
      }

      if (parms.IsTargetSystemIDSpecified)
      {
        if (IsAndNeeded)
          whereClause += " and ";

        whereClause += " mo.Target_SystemID = " + parms.TargetSystemID.ToString().Trim() + " ";
        IsAndNeeded = true;
      }

      if (parms.IsTargetApplicationIDSpecified)
      {
        if (IsAndNeeded)
          whereClause += " and ";

        whereClause += " mo.Target_ApplicationID = " + parms.TargetApplicationID.ToString().Trim() + " ";
        IsAndNeeded = true;
      }

      if (parms.IsServerListSpecified)
      {
        if (IsAndNeeded)
          whereClause += " and ";

        whereClause += " mo.Observer_ServerID in (" + parms.ServerList + ") ";
        IsAndNeeded = true;
      }

      if (parms.IsLimitedByDates)
      {
        if (IsAndNeeded)
          whereClause += " and ";

        whereClause += " mo.MetricCapturedDateTime >= '" + parms.FromDateTime.ToString(@"MM/dd/yyyy HH:mm:ss") + "' and " +
                       " mo.MetricCapturedDateTime <= '" + parms.ToDateTime.ToString(@"MM/dd/yyyy HH:mm:ss") + "' ";
        IsAndNeeded = true;
      }


      return whereClause;
    }

    public void PopulateAvailableESQLMGMT_Metrics(MetricQueryParms parms, MetricDataObjects mdo)
    {
      string sql =
        "select distinct " +
        "1 as EnvironmentID, " +
        "2 as MetricTypeID, " +
        "6 as Target_SystemID, " +
        "Target_ApplicationID = " +
        "case " +
        "when ObjectName = 'System'            then 302 " +
        "when ObjectName = 'Processor'         then 303 " +
        "when ObjectName = 'Memory'            then 304 " +
        "when ObjectName = 'Web Service'       then 305 " +
        "when ObjectName = 'Active Server Pages' then 306 " +
        "end, " +
        "1 as MetricStateID, " +
        "MetricID =  " +
        "case " +
        @"when CounterName = '% Processor Time' then 12 " +
        @"when CounterName = 'Bytes Total/sec'  then 13 " +
        @"when CounterName = 'Connection Attempts/sec' then 14 " +
        @"when CounterName = 'Current Connections' then 15 " +
        @"when CounterName = 'Errors/sec'       then 16 " +
        @"when CounterName = 'Interrupts/sec'   then 17 " +
        @"when CounterName = 'Not Found Errors/sec' then 18 " +
        @"when CounterName = 'Page Faults/sec'  then 19 " +
        @"when CounterName = 'Page Reads/sec'   then 20 " +
        @"when CounterName = 'Page Writes/sec'  then 21 " +
        @"when CounterName = 'Pages Input/sec'  then 22 " +
        @"when CounterName = 'Pages Output/sec' then 23 " +
        @"when CounterName = 'Pages/sec'        then 24 " +
        @"when CounterName = 'Processor Queue Length' then 25 " +
        @"when CounterName = 'Request Execution Time' then 26 " +
        @"when CounterName = 'Request Wait Time' then 27 " +
        @"when CounterName = 'Requests Executing' then 28 " +
        @"when CounterName = 'Requests Queued' then 29 " +
        @"when CounterName = 'Requests Rejected' then 30 " +
        @"when CounterName = 'Requests Timed Out' then 31 " +
        @"when CounterName = 'Requests/sec' then 32 " +
        "else 0 " +
        "end, " +
        "2 as AggregateTypeID, " +
        "5 as MetricValueTypeID, " +
        "10 as IntervalID, " +
        "6 as Observer_SystemID, " +
        "41 as Observer_ApplicationID, " +
        "Observer_ServerID = " +
        "case  " +
        @"when MachineName = '\\EPRODADMINWEB1' then 15 " +
        @"when MachineName = '\\EPRODCCS1'      then 77 " +
        @"when MachineName = '\\EPRODCCS2'      then 78 " +
        @"when MachineName = '\\EPRODGW1'       then 22 " +
        @"when MachineName = '\\EPRODGW2'       then 23 " +
        @"when MachineName = '\\EPRODMKDG1'     then 26 " +
        @"when MachineName = '\\EPRODSEARS'     then 35 " +
        @"when MachineName = '\\EPRODSQL11'     then 2 " +
        @"when MachineName = '\\EPRODSQL12'     then 3 " +
        @"when MachineName = '\\EPRODSQL13'     then 4 " +
        @"when MachineName = '\\EPRODSQL14'     then 5 " +
        @"when MachineName = '\\EPRODSQL15'     then 6 " +
        @"when MachineName = '\\EPRODSQL2'      then 75 " +
        @"when MachineName = '\\EPRODSQL3'      then 76 " +
        @"when MachineName = '\\EPRODSQL4'      then 61 " +
        @"when MachineName = '\\EPRODSQL5'      then 62 " +
        @"when MachineName = '\\EPRODSQL6'      then 63 " +
        @"when MachineName = '\\EPRODSQL7'      then 64 " +
        @"when MachineName = '\\EPRODSQL8'      then 65 " +
        @"when MachineName = '\\EPRODSQL9'      then 66 " +
        @"when MachineName = '\\EPRODUSAA'      then 40 " +
        @"when MachineName = '\\EPRODWEB1'      then 41 " +
        @"when MachineName = '\\EPRODWEB10'     then 50 " +
        @"when MachineName = '\\EPRODWEB11'     then 51 " +
        @"when MachineName = '\\EPRODWEB12'     then 52 " +
        @"when MachineName = '\\EPRODWEB2'      then 42 " +
        @"when MachineName = '\\EPRODWEB3'      then 43 " +
        @"when MachineName = '\\EPRODWEB4'      then 44 " +
        @"when MachineName = '\\EPRODWEB5'      then 45 " +
        @"when MachineName = '\\EPRODWEB6'      then 46 " +
        @"when MachineName = '\\EPRODWEB7'      then 47 " +
        @"when MachineName = '\\EPRODWEB8'      then 48 " +
        @"when MachineName = '\\EPRODWEB9'      then 49 " +
        @"when MachineName = '\\TFOKPRODTFE1'   then 10 " +
        @"when MachineName = '\\TFOKPRODTFE2'   then 11 " +
        "else 0 " +
        "end, " +
        "999 as Count " +
        "from CounterDetails " +
        "where  " +
        "(ObjectName = 'System' or " +
        "ObjectName = 'Processor' or " +
        "ObjectName = 'Memory' or  " +
        "ObjectName = 'Web Service' or " +
        "ObjectName = 'Active Server Pages') " +
        "and " +
        @"(CounterName = '% Processor Time' or " +
        @"CounterName = 'Bytes Total/sec'  or " +
        @"CounterName = 'Connection Attempts/sec' or " +
        @"CounterName = 'Current Connections' or " +
        @"CounterName = 'Errors/sec'       or " +
        @"CounterName = 'Interrupts/sec'   or " +
        @"CounterName = 'Not Found Errors/sec' or " +
        @"CounterName = 'Page Faults/sec'  or " +
        @"CounterName = 'Page Reads/sec'   or " +
        @"CounterName = 'Page Writes/sec'  or " +
        @"CounterName = 'Pages Input/sec'  or " +
        @"CounterName = 'Pages Output/sec' or " +
        @"CounterName = 'Pages/sec'        or " +
        @"CounterName = 'Processor Queue Length' or " +
        @"CounterName = 'Request Execution Time' or " +
        @"CounterName = 'Request Wait Time' or " +
        @"CounterName = 'Requests Executing' or " +
        @"CounterName = 'Requests Queued' or " +
        @"CounterName = 'Requests Rejected' or " +
        @"CounterName = 'Requests Timed Out' or " +
        @"CounterName = 'Requests/sec') " +
        "order by " +
        "EnvironmentID, " +
        "MetricTypeID,  " +
        "Target_SystemID,  " +
        "Target_ApplicationID,  " +
        "MetricStateID,  " +
        "MetricID,  " +
        "AggregateTypeID,  " +
        "MetricValueTypeID,  " +
        "IntervalID,  " +
        "Observer_SystemID,  " +
        "Observer_ApplicationID,  " +
        "Observer_ServerID ";


      try
      {
        SqlDataAdapter da = new SqlDataAdapter(sql, Data.ConnectionESQLMGMT);
        DataSet ds = new DataSet();
        da.Fill(ds);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
          SpecificMetric am = new SpecificMetric();
          am.MetricSource = 1;
          if (!row.IsNull("EnvironmentID"))
            am.EnvironmentID = Convert.ToInt32(row["EnvironmentID"]);
          if (!row.IsNull("MetricTypeID"))
            am.MetricTypeID = Convert.ToInt32(row["MetricTypeID"]);
          if (!row.IsNull("Target_SystemID"))
            am.TargetSystemID = Convert.ToInt32(row["Target_SystemID"]);
          if (!row.IsNull("Target_ApplicationID"))
            am.TargetApplicationID = Convert.ToInt32(row["Target_ApplicationID"]);
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
          if (!row.IsNull("Observer_SystemID"))
            am.ObserverSystemID = Convert.ToInt32(row["Observer_SystemID"]);
          if (!row.IsNull("Observer_ApplicationID"))
            am.ObserverApplicationID = Convert.ToInt32(row["Observer_ApplicationID"]);
          if (!row.IsNull("Observer_ServerID"))
            am.ObserverServerID = Convert.ToInt32(row["Observer_ServerID"]);
          if (!row.IsNull("Count"))
            am.Count = Convert.ToInt32(row["Count"]);

          am.LegendLabel = mdo.Systems[am.TargetSystemID].SystemDesc + " - " +
                           mdo.Servers[am.ObserverServerID].ServerDesc + " - " +
                           mdo.Metrics[am.MetricID].MetricDesc;

          if (am.ObserverServerID > 0)
            this.Add(this.Count, am);
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
    }

  }
}
