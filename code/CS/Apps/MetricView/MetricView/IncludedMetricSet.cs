using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  [Serializable]
  public class IncludedMetricSet : SortedList<int, SpecificMetric>
  {
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

    private string _yAxisLabel;
    public string YAxisLabel
    {
      get {
        return _yAxisLabel;
      }
      set {
        _yAxisLabel = value;
      }
    }

    public IncludedMetricSet()
    {
      _useMostCurrentMetrics = true;
      _dataPoints = 100;
      _fromDateTime = DateTime.MinValue;
      _toDateTime = DateTime.MinValue;
      _yAxisLabel = String.Empty;
    }


  }
}
