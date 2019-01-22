using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class IpdxCommand
  {
    [XMap(IsRequired = true)]
    public string CommandName {
      get;
      set;
    }

    [XMap(DefaultValue = "NotSet")]
    public TaskResultStatus TaskResultStatus {
      get;
      set;
    }

    [XMap(DefaultValue = "")]
    public string Message {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "Parm", WrapperElement = "Parms", KeyName="K")]
    public Dictionary<string, string> Parms {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public int BeforeWaitMilliseconds {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public int AfterWaitMilliseconds {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public float DurationSeconds {
      get {
        return Get_DurationSeconds();
      }
    }

    public DateTime BeginDateTime {
      get;
      set;
    }
    public DateTime EndDateTime {
      get;
      set;
    }

    public IpdxCommand()
    {
      this.CommandName = String.Empty;
      this.TaskResultStatus = TaskResultStatus.NotSet;
      this.Message = String.Empty;
      this.Parms = new Dictionary<string, string>();
      this.BeforeWaitMilliseconds = 0;
      this.AfterWaitMilliseconds = 0;
      this.BeginDateTime = DateTime.MinValue;
      this.EndDateTime = DateTime.MinValue;
    }

    public float Get_DurationSeconds()
    {
      TimeSpan tsDuration = this.EndDateTime - this.BeginDateTime;
      int totalSeconds = Convert.ToInt32(tsDuration.TotalSeconds);
      int milliseconds = tsDuration.Milliseconds;
      return totalSeconds + (float)milliseconds / 1000;
    }
  }
}
