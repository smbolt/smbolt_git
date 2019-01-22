using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
  public class MetricDataObjects
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

    private EnvironmentSet _enviornments;
    public EnvironmentSet Environments
    {
      get {
        return _enviornments;
      }
      set {
        _enviornments = value;
      }
    }

    private SystemSet _systems;
    public SystemSet Systems
    {
      get {
        return _systems;
      }
      set {
        _systems = value;
      }
    }

    private ApplicationSet _applications;
    public ApplicationSet Applications
    {
      get {
        return _applications;
      }
      set {
        _applications = value;
      }
    }

    private AvailableMetricSet _availableMetrics;
    public AvailableMetricSet AvailableMetrics
    {
      get {
        return _availableMetrics;
      }
      set {
        _availableMetrics = value;
      }
    }

    private MetricTypeSet _metricTypes;
    public MetricTypeSet MetricTypes
    {
      get {
        return _metricTypes;
      }
      set {
        _metricTypes = value;
      }
    }

    private MetricStateSet _metricStates;
    public MetricStateSet MetricStates
    {
      get {
        return _metricStates;
      }
      set {
        _metricStates = value;
      }
    }

    private AggregateTypeSet _aggregateTypes;
    public AggregateTypeSet AggregateTypes
    {
      get {
        return _aggregateTypes;
      }
      set {
        _aggregateTypes = value;
      }
    }

    private MetricValueTypeSet _metricValueTypes;
    public MetricValueTypeSet MetricValueTypes
    {
      get {
        return _metricValueTypes;
      }
      set {
        _metricValueTypes = value;
      }
    }

    private ServerSet _servers;
    public ServerSet Servers
    {
      get {
        return _servers;
      }
      set {
        _servers = value;
      }
    }

    private IntervalSet _intervals;
    public IntervalSet Intervals
    {
      get {
        return _intervals;
      }
      set {
        _intervals = value;
      }
    }

    private MetricSet _metrics;
    public MetricSet Metrics
    {
      get {
        return _metrics;
      }
      set {
        _metrics = value;
      }
    }

    private MetricObservationSet _metricObservations;
    public MetricObservationSet MetricObservations
    {
      get {
        return _metricObservations;
      }
      set {
        _metricObservations = value;
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

    private float highMetricValue = 0F;
    private float lowMetricValue = 0F;

    public MetricDataObjects()
    {
      _data = new MetricsData();
      _data.CreateConnection();
    }

    public void CloseConnection()
    {
      Data.CloseConnection();
    }


    public void GetEnvironments()
    {
      EnvironmentSet envSet = new EnvironmentSet(Data);
      envSet.PopulateEnvironmentSet();
      _enviornments = envSet;
    }

    public void GetSystems()
    {
      SystemSet systemSet = new SystemSet(Data);
      systemSet.PopulateSystems();
      _systems = systemSet;
    }

    public void GetApplications()
    {
      ApplicationSet applicationSet = new ApplicationSet(Data);
      applicationSet.PopulateApplications();
      _applications = applicationSet;
    }

    public void GetAvailableMetrics(MetricQueryParms parms)
    {
      AvailableMetricSet availableMetricSet = new AvailableMetricSet(Data);
      availableMetricSet.PopulateAvailableMetrics(parms, this);

      foreach (KeyValuePair<int, SpecificMetric> smKVP in availableMetricSet)
      {
        smKVP.Value.LegendLabel =
          _systems[smKVP.Value.TargetSystemID].SystemDesc + @" - " +
          _servers[smKVP.Value.ObserverServerID].ServerDesc + " " +
          _metrics[smKVP.Value.MetricID].MetricDesc;
      }

      _availableMetrics = availableMetricSet;
    }

    public void GetAvailableESQLMGMT_Metrics(MetricQueryParms parms)
    {
      AvailableMetricSet availableMetricSet = new AvailableMetricSet(Data);
      availableMetricSet.PopulateAvailableESQLMGMT_Metrics(parms, this);

      foreach (KeyValuePair<int, SpecificMetric> smKVP in availableMetricSet)
      {
        smKVP.Value.LegendLabel =
          _systems[smKVP.Value.TargetSystemID].SystemDesc + @" - " +
          _applications[smKVP.Value.TargetApplicationID].ApplicationName + " " +
          _metrics[smKVP.Value.MetricID].MetricDesc;
      }

      _availableMetrics = availableMetricSet;

    }


    public void GetMetricTypes()
    {
      MetricTypeSet metricTypeSet = new MetricTypeSet(Data);
      metricTypeSet.PopulateMetricTypes();
      _metricTypes = metricTypeSet;
    }

    public void GetMetricStates()
    {
      MetricStateSet metricStateSet = new MetricStateSet(Data);
      metricStateSet.PopulateMetricStates();
      _metricStates = metricStateSet;
    }

    public void GetAggregateTypes()
    {
      AggregateTypeSet aggregateTypeSet = new AggregateTypeSet(Data);
      aggregateTypeSet.PopulateAggregateTypes();
      _aggregateTypes = aggregateTypeSet;
    }

    public void GetMetricValueTypes()
    {
      MetricValueTypeSet metricValueTypeSet = new MetricValueTypeSet(Data);
      metricValueTypeSet.PopulateMetricValueTypes();
      _metricValueTypes = metricValueTypeSet;
    }

    public void GetServers()
    {
      ServerSet serverSet = new ServerSet(Data);
      serverSet.PopulateServers();
      _servers = serverSet;
    }

    public void GetIntervals()
    {
      IntervalSet intervalSet = new IntervalSet(Data);
      intervalSet.PopulateIntervals();
      _intervals = intervalSet;
    }

    public void GetMetrics()
    {
      MetricSet metricSet = new MetricSet(Data);
      metricSet.PopulateMetrics();
      _metrics = metricSet;
    }

    public long GetMetricObservationSet(SpecificMetric sm, MetricQueryParms parms)
    {
      long duration = 0;

      MetricObservationSet metricObservationSet = new MetricObservationSet(Data);
      if (sm.MetricSource == 1)
        duration = metricObservationSet.PopulateESQLMGMT_MetricObservations(sm, parms);
      else
      {
        if (sm.IsMetricFromFile)
          duration = metricObservationSet.PopulateMetricObservationsFromFile(sm, parms);
        else
          duration = metricObservationSet.PopulateMetricObservations(sm, parms);
      }

      _metricObservations = metricObservationSet;

      return duration;
    }

    public MetricObservationSet[] GetMetricObsevationSets(IncludedMetricSet includedMetricSet)
    {
      MetricObservationSet[] mosSet = new MetricObservationSet[includedMetricSet.Count];
      StringBuilder sb = new StringBuilder();

      foreach (KeyValuePair<int, SpecificMetric> smKVP in includedMetricSet)
      {
        sb.Append(includedMetricSet.DataPoints.ToString("0000") +
                  smKVP.Value.EnvironmentID.ToString("0000") +
                  smKVP.Value.TargetSystemID.ToString("0000") +
                  smKVP.Value.TargetApplicationID.ToString("0000") +
                  smKVP.Value.ObserverSystemID.ToString("0000") +
                  smKVP.Value.ObserverApplicationID.ToString("0000") +
                  smKVP.Value.ObserverServerID.ToString("0000") +
                  smKVP.Value.MetricTypeID.ToString("0000") +
                  smKVP.Value.MetricStateID.ToString("0000") +
                  smKVP.Value.MetricID.ToString("0000") +
                  smKVP.Value.AggregateTypeID.ToString("0000") +
                  smKVP.Value.MetricValueTypeID.ToString("0000") +
                  smKVP.Value.IntervalID.ToString("0000"));
      }

      try
      {
        SqlCommand cmd = new SqlCommand("sp_GetMetricObservationSets", Data.Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@@parms", SqlDbType.VarChar, 800));
        cmd.Parameters[0].Value = sb.ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        int mosPtr = 0;

        foreach (DataTable dt in ds.Tables)
        {
          MetricObservationSet mos = new MetricObservationSet();
          for (int i = 0; i < dt.Rows.Count; i++)
          {
            DataRow row = dt.Rows[i];
            MetricObservation m = BuildMetricObservation(row);
            mos.Add(mos.Count, m);
          }
          mosSet[mosPtr++] = mos;
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

      return mosSet;
    }

    private MetricObservation BuildMetricObservation(DataRow row)
    {
      MetricObservation m = new MetricObservation();

      // patch to fix integer overflow

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

      if (m.MetricValue > highMetricValue)
        highMetricValue = m.MetricValue;

      if (m.MetricValue < lowMetricValue)
        lowMetricValue = m.MetricValue;

      return m;
    }


    private TimeSpan ConvertToTimeSpan(string duration)
    {
      TimeSpan ts = TimeSpan.MinValue;
      return ts;
    }

  }
}
