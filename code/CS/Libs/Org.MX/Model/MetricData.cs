using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.MX.DataAccess;
using Org.GS.Configuration;
using Org.GS;

namespace Org.MX.Model
{
  public class MetricData
  {
    private ConfigDbSpec _configDbSpec;

    public MetricObjectSet<MetricEnvironment> MetricEnvironmentSet { get; private set; }
    public MetricObjectSet<Interval> IntervalSet { get; private set; }
    public MetricObjectSet<Location> LocationSet { get; private set; }
    public MetricObjectSet<MeasuredValueType> MeasuredValueTypeSet { get; private set; }
    public MetricObjectSet<MetricApp> MetricAppSet { get; private set; }
    public MetricObjectSet<MetricState> MetricStateSet { get; private set; }
    public MetricObjectSet<MetricType> MetricTypeSet { get; private set; }
    public MetricObjectSet<MetricValueType> MetricValueTypeSet { get; private set; }
    public MetricObjectSet<Server> ServerSet { get; private set; }
    public MetricObjectSet<System> SystemSet { get; private set; }
    public string Report { get { return Get_Report(); } }

    public MetricData(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      this.MetricEnvironmentSet = new MetricObjectSet<MetricEnvironment>();
      this.IntervalSet = new MetricObjectSet<Interval>();
      this.LocationSet = new MetricObjectSet<Location>();
      this.MeasuredValueTypeSet = new MetricObjectSet<MeasuredValueType>();
      this.MetricAppSet = new MetricObjectSet<MetricApp>();
      this.MetricStateSet = new MetricObjectSet<MetricState>();
      this.MetricTypeSet = new MetricObjectSet<MetricType>();
      this.MetricValueTypeSet = new MetricObjectSet<MetricValueType>();
      this.ServerSet = new MetricObjectSet<Server>();
      this.SystemSet = new MetricObjectSet<System>();
    }

    public void Load()
    {
      try
      {
        using (var dataProc = new RequestProcessor(_configDbSpec))
        {
          this.MetricEnvironmentSet = dataProc.Get<MetricEnvironment>();
          this.IntervalSet = dataProc.Get<Interval>();
          this.LocationSet = dataProc.Get<Location>();
          this.MeasuredValueTypeSet = dataProc.Get<MeasuredValueType>();
          this.MetricAppSet = dataProc.Get<MetricApp>();
          this.MetricStateSet = dataProc.Get<MetricState>();
          this.MetricValueTypeSet = dataProc.Get<MetricValueType>();
          this.MetricTypeSet = dataProc.Get<MetricType>();
          this.ServerSet = dataProc.Get<Server>();
          this.SystemSet = dataProc.Get<System>();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the MetricData object.", ex);
      }
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();
      sb.Append(this.MetricEnvironmentSet.Report);
      sb.Append(this.IntervalSet.Report);
      sb.Append(this.LocationSet.Report);
      sb.Append(this.MeasuredValueTypeSet.Report);
      sb.Append(this.MetricAppSet.Report);
      sb.Append(this.MetricStateSet.Report);
      sb.Append(this.MetricTypeSet.Report);
      sb.Append(this.MetricValueTypeSet.Report);
      sb.Append(this.ServerSet.Report);
      sb.Append(this.SystemSet.Report);

      string report = sb.ToString();
      return report;
    }
  }
}
