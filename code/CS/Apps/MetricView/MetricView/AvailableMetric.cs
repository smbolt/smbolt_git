using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class AvailableMetric
  {
    private int _environmentID;
    public int EnvironmentID
    {
      get {
        return _environmentID;
      }
      set {
        _environmentID = value;
      }
    }

    private int _metricTypeID;
    public int MetricTypeID
    {
      get {
        return _metricTypeID;
      }
      set {
        _metricTypeID = value;
      }
    }

    private int _targetSystemID;
    public int TargetSystemID
    {
      get {
        return _targetSystemID;
      }
      set {
        _targetSystemID = value;
      }
    }

    private int _targetApplicationID;
    public int TargetApplicationID
    {
      get {
        return _targetApplicationID;
      }
      set {
        _targetApplicationID = value;
      }
    }

    private int _metricStateID;
    public int MetricStateID
    {
      get {
        return _metricStateID;
      }
      set {
        _metricStateID = value;
      }
    }

    private int _metricID;
    public int MetricID
    {
      get {
        return _metricID;
      }
      set {
        _metricID = value;
      }
    }

    private int _aggregateTypeID;
    public int AggregateTypeID
    {
      get {
        return _aggregateTypeID;
      }
      set {
        _aggregateTypeID = value;
      }
    }

    private int _metricValueTypeID;
    public int MetricValueTypeID
    {
      get {
        return _metricValueTypeID;
      }
      set {
        _metricValueTypeID = value;
      }
    }

    private int _intervalID;
    public int IntervalID
    {
      get {
        return _intervalID;
      }
      set {
        _intervalID = value;
      }
    }

    private int _observerSystemID;
    public int ObserverSystemID
    {
      get {
        return _observerSystemID;
      }
      set {
        _observerSystemID = value;
      }
    }

    private int _observerApplicationID;
    public int ObserverApplicationID
    {
      get {
        return _observerApplicationID;
      }
      set {
        _observerApplicationID = value;
      }
    }

    private int _observerServerID;
    public int ObserverServerID
    {
      get {
        return _observerServerID;
      }
      set {
        _observerServerID = value;
      }
    }

    private long _count;
    public long Count
    {
      get {
        return _count;
      }
      set {
        _count = value;
      }
    }

    private DateTime _maxMetricCapturedDateTime;
    public DateTime MaxMetricCapturedDateTime
    {
      get {
        return _maxMetricCapturedDateTime;
      }
      set {
        _maxMetricCapturedDateTime = value;
      }
    }

    private DateTime _maxReceivedFromObserverDateTime;
    public DateTime MaxReceivedFromObserverDateTime
    {
      get {
        return _maxReceivedFromObserverDateTime;
      }
      set {
        _maxReceivedFromObserverDateTime = value;
      }
    }


    public AvailableMetric()
    {
      _environmentID = 0;
      _metricTypeID = 0;
      _targetSystemID = 0;
      _targetApplicationID = 0;
      _metricStateID = 0;
      _metricID = 0;
      _aggregateTypeID = 0;
      _metricValueTypeID = 0;
      _intervalID = 0;
      _observerSystemID = 0;
      _observerApplicationID = 0;
      _observerServerID = 0;
      _count = 0;
      _maxMetricCapturedDateTime = DateTime.MinValue;
      _maxReceivedFromObserverDateTime = DateTime.MinValue;

    }

  }
}
