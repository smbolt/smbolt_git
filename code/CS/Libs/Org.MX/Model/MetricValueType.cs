using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MetricValueType : MetricObject
  {
    [EntityMap]
    public int MetricValueTypeID
    {
      get {
        return base.ID;
      }
      set {
        base.ID = value;
      }
    }

    [EntityMap(Sequencer = true)]
    public int MetricValueTypeCode
    {
      get {
        return base.Code;
      }
      set {
        base.Code = value;
      }
    }

    [EntityMap]
    public string MetricValueTypeName
    {
      get {
        return base.Name;
      }
      set {
        base.Name = value;
      }
    }

    [EntityMap]
    public string MetricValueTypeDesc
    {
      get {
        return base.Description;
      }
      set {
        base.Description = value;
      }
    }
    public MetricValueType() : base(0, 0, String.Empty, String.Empty) { }

    public MetricValueType(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
