using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  [Serializable]
  public class MetricGraphConfiguration
  {
    private IncludedMetricSet _includedMetrics;
    public IncludedMetricSet IncludedMetrics
    {
      get {
        return _includedMetrics;
      }
      set
      {
        _includedMetrics = value;
      }
    }

    private bool _useESQLMGMT;
    public bool UseESQLMGMT
    {
      get {
        return _useESQLMGMT;
      }
      set {
        _useESQLMGMT = value;
      }
    }


    private string _graphName;
    public string GraphName
    {
      get {
        return _graphName;
      }
      set
      {
        _graphName = value;
      }
    }

    private Point _graphLocation;
    public Point GraphLocation
    {
      get {
        return _graphLocation;
      }
      set
      {
        _graphLocation = value;
      }
    }

    private Size _graphSize;
    public Size GraphSize
    {
      get {
        return _graphSize;
      }
      set
      {
        _graphSize = value;
      }
    }

    private bool _isActive;
    public bool IsActive
    {
      get {
        return _isActive;
      }
      set
      {
        _isActive = value;
      }
    }

    private bool _isSelected;
    public bool IsSelected
    {
      get {
        return _isSelected;
      }
      set {
        _isSelected = value;
      }
    }

    private int _refreshInterval;
    public int RefreshInterval
    {
      get {
        return _refreshInterval;
      }
      set {
        _refreshInterval = value;
      }
    }


    public MetricGraphConfiguration()
    {
      _useESQLMGMT = false;
      _includedMetrics = new IncludedMetricSet();
      _graphName = String.Empty;
      _graphLocation = new Point(0, 0);
      _graphSize = new Size(0, 0);
      _isActive = false;
      _refreshInterval = 300000;
    }


  }
}
