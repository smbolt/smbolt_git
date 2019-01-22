using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class System : MetricObject
  {
    [EntityMap]
    public int SystemID
    {
      get {
        return base.ID;
      }
      set {
        base.ID = value;
      }
    }

    [EntityMap(Sequencer = true)]
    public int SystemCode
    {
      get {
        return base.Code;
      }
      set {
        base.Code = value;
      }
    }

    [EntityMap]
    public string SystemName
    {
      get {
        return base.Name;
      }
      set {
        base.Name = value;
      }
    }

    [EntityMap]
    public string SystemDesc
    {
      get {
        return base.Description;
      }
      set {
        base.Description = value;
      }
    }

    [EntityMap]
    public new bool IsActive
    {
      get {
        return base.IsActive;
      }
      set {
        base.IsActive = value;
      }
    }


    public System() : base(0, 0, String.Empty, String.Empty) { }

    public System(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
