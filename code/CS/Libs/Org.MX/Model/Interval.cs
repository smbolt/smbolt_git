using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class Interval : MetricObject
  {
    [EntityMap]
    public int IntervalID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int IntervalCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string IntervalName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    [EntityMap]
    public string IntervalDesc
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    public Interval() : base(0, 0, String.Empty, String.Empty) { }

    public Interval(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
