using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MetricState : MetricObject
  {
    [EntityMap]
    public int MetricStateID
    {
      get {
        return base.ID;
      }
      set {
        base.ID = value;
      }
    }

    [EntityMap(Sequencer = true)]
    public int MetricStateCode
    {
      get {
        return base.Code;
      }
      set {
        base.Code = value;
      }
    }

    [EntityMap]
    public string MetricStateName
    {
      get {
        return base.Name;
      }
      set {
        base.Name = value;
      }
    }

    [EntityMap]
    public string MetricStateDesc
    {
      get {
        return base.Description;
      }
      set {
        base.Description = value;
      }
    }

    public MetricState() : base(0, 0, String.Empty, String.Empty) { }

    public MetricState(int id, int code, string name, string description)
      : base(id, code, name, description) { }
  }
}
