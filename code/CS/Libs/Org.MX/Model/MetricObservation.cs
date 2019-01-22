using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.MX.Model
{
  public class MetricObservation
  {
    public int MetricObservationId {
      get;
      set;
    }
    public int ObserverSystemId {
      get;
      set;
    }
    public int ObserverAppId {
      get;
      set;
    }
    public int ObserverServerId {
      get;
      set;
    }
    public DateTime ReceivedFromObserverDateTime {
      get;
      set;
    }
    public int TargetSystemId {
      get;
      set;
    }
    public int TargetAppId {
      get;
      set;
    }
    public int TargetServerId {
      get;
      set;
    }
    public int EnvironmentId {
      get;
      set;
    }
    public int MeasuredValueTypeId {
      get;
      set;
    }
    public int MetricId {
      get;
      set;
    }
    public int MetricStateId {
      get;
      set;
    }
    public int MetricTypeId {
      get;
      set;
    }
    public int IntervalId {
      get;
      set;
    }
    public int MetricValueTypeId {
      get;
      set;
    }
    public DateTime MetricDateTime {
      get;
      set;
    }
    public decimal MetricValue {
      get;
      set;
    }
  }
}
