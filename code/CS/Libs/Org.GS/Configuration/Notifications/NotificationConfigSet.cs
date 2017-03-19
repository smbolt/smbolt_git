using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "NotificationConfig", SequenceDuplicates = true)]
  public class NotificationConfigSet : Dictionary<string, NotificationConfig>
  {
    public ProgramConfig ProgramConfig { get; set; }

    public NotificationConfigSet()
    {
    }

    public NotificationConfigSet(ProgramConfig programConfig)
    {
      this.ProgramConfig = programConfig;
    }
  }
}
