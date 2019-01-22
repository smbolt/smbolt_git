using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MetricType : MetricObject
  {
    [EntityMap]
    public int MetricTypeID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int MetricTypeCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string MetricTypeName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    [EntityMap]
    public string MetricTypeDesc
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    public MetricType() : base(0, 0, String.Empty, String.Empty) { }

    public MetricType(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
