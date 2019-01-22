using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class MetricType
  {
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

    private string _metricTypeDesc;
    public string MetricTypeDesc
    {
      get {
        return _metricTypeDesc;
      }
      set {
        _metricTypeDesc = value;
      }
    }

    public MetricType()
    {
      _metricTypeID = 0;
      _metricTypeDesc = String.Empty;
    }
  }
}
