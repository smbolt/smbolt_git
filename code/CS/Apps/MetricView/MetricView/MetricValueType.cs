using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class MetricValueType
  {
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

    private string _metricValueTypeDesc;
    public string MetricValueTypeDesc
    {
      get {
        return _metricValueTypeDesc;
      }
      set {
        _metricValueTypeDesc = value;
      }
    }

    public MetricValueType()
    {
      _metricValueTypeID = 0;
      _metricValueTypeDesc = String.Empty;
    }
  }
}
