using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  public enum OpsControlFunction
  {
    NotSet,
    StartWinService,
    StopWinService
  }

  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class OpsControl
  {
    [XMap(IsKey=true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue="True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap(DefaultValue="NotSet")]
    public OpsControlFunction OpsControlFunction {
      get;
      set;
    }

    [XMap]
    public string Host {
      get;
      set;
    }

    [XMap]
    public string ServiceName {
      get;
      set;
    }

    [XMap]
    public string RunBefore {
      get;
      set;
    }

    [XMap]
    public string RunAfter {
      get;
      set;
    }

    public OpsControl()
    {
      this.Name = String.Empty;
      this.IsActive = true;
      this.OpsControlFunction = OpsControlFunction.NotSet;
      this.Host = String.Empty;
      this.ServiceName = String.Empty;
      this.RunBefore = String.Empty;
      this.RunAfter = String.Empty;
    }
  }
}
