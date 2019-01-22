using System;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class Server : MetricObject
  {
    [EntityMap]
    public int ServerID
    {
      get { return base.ID; }
      set { base.ID = value; }
    }

    [EntityMap(Sequencer = true)]
    public int ServerCode
    {
      get { return base.Code; }
      set { base.Code = value; }
    }

    [EntityMap]
    public string ServerName
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    [EntityMap]
    public string ServerDesc
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    [EntityMap]
    public int ServerTypeID { get; set; }

    [EntityMap]
    public int LocationID { get; set; }

    public Server() : base(0, 0, String.Empty, String.Empty)
    {
      this.ServerTypeID = 0;
      this.LocationID = 0;
    }

    public Server(int id, int code, string name, string description = "")
      : base(id, code, name, description)
    {
      this.ServerTypeID = 0;
      this.LocationID = 0;
    }
  }
}
