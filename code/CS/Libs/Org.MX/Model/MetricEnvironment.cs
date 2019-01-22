using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.MX.Model
{
  [EntityMap]
  public class MetricEnvironment : MetricObject
  {
    [EntityMap]
    public int MetricEnvironmentID
    {
      get {
        return base.ID;
      }
      set {
        base.ID = value;
      }
    }

    [EntityMap(Sequencer = true)]
    public int MetricEnvironmentCode
    {
      get {
        return base.Code;
      }
      set {
        base.Code = value;
      }
    }

    [EntityMap]
    public string MetricEnvironmentName
    {
      get {
        return base.Name;
      }
      set {
        base.Name = value;
      }
    }

    [EntityMap]
    public string MetricEnvironmentDesc
    {
      get {
        return base.Description;
      }
      set {
        base.Description = value;
      }
    }

    public MetricEnvironment() : base(0, 0, String.Empty, String.Empty) { }

    public MetricEnvironment(int id, int code, string name, string description)
      : base(id, code, name, description) { }
  }
}
