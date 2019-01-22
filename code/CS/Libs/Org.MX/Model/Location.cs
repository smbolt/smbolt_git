using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class Location : MetricObject
  {
    [EntityMap]
    public int LocationID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int LocationCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string LocationName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    [EntityMap]
    public string LocationDesc
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    [EntityMap]
    public new bool IsActive
    {
      get { return base.IsActive; }
      set { base.IsActive = value; }
    }

    public Location() : base(0, 0, String.Empty, String.Empty) { }

    public Location(int id, int code, string name = "", string description = "")
      : base(id, code, name, description) { }
  }
}
