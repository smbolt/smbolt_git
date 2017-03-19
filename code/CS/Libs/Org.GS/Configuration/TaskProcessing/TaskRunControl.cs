using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, Name = "TaskRunControl")]
  public class TaskRunControl
  {
    [XMap(IsRequired = true)]
    public int MaxRunTimeSeconds { get; set; }

    [XMap(IsRequired = true)]
    public int MaxExecutions { get; set; }

    [XMap(IsRequired = true)]
    public bool AllowConcurrent { get; set; }

    public TaskRunControl()
    {
      this.MaxRunTimeSeconds = 0;
      this.MaxExecutions = 0;
      this.AllowConcurrent = true;
    }
  }
}
