using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MeasuredValueType : MetricObject
  {
    [EntityMap]
    public int MeasuredValueTypeID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int MeasuredValueTypeCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string MeasuredValueTypeName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    public MeasuredValueType() : base(0, 0, String.Empty, String.Empty) { }

    public MeasuredValueType(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
