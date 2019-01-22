using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class AggregateType
  {
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

    private string _aggregateTypeDesc;
    public string AggregateTypeDesc
    {
      get {
        return _aggregateTypeDesc;
      }
      set {
        _aggregateTypeDesc = value;
      }
    }

    public AggregateType()
    {
      _aggregateTypeID = 0;
      _aggregateTypeDesc = String.Empty;
    }
  }
}
