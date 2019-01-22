using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MetricApp : MetricObject
  {
    [EntityMap]
    public int AppID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int AppCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string AppName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    public AppType AppType { get; set; }

    public  MetricApp() : base(0, 0, String.Empty, String.Empty) { }

    public MetricApp(int id, int code, string name, string description = "")
      : base(id, code, name, description) { }
  }
}
