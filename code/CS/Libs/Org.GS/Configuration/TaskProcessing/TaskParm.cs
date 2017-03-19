using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class TaskParm
  {
    [XMap(Name = "K", IsKey = true)]
    public string Key { get; set; }

    [XMap(Name = "V", DefaultValue = "")]
    public string Value { get; set; }

    public TaskParm()
    {
      this.Key = String.Empty;
      this.Value = String.Empty;
    }
  }
}
