using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  [Serializable]
  public class MetricViewConfiguration
  {
    private MetricGraphCollection _metricGraphs;
    public MetricGraphCollection MetricGraphs
    {
      get {
        return _metricGraphs;
      }
      set {
        _metricGraphs = value;
      }
    }


    public MetricViewConfiguration()
    {
      _metricGraphs = new MetricGraphCollection();
      _metricGraphs.NextNumber = -1;
    }
  }
}
