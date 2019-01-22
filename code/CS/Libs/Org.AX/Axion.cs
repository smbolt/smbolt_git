using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
using Org.GS.Configuration;
using Org.WSO;
using Org.WSO.Transactions;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class Axion : UnitOfWork
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(IsRequired = true)]
    public AxionType AxionType { get; set; }    

    [XMap(DefaultValue = "True")]
    public bool IsActive { get; set; }

    public bool IsDryRun { get; set; }

    public Axion()
    {
      this.Name = String.Empty;
      this.AxionType = AxionType.NotSet;
      this.IsActive = true;
      this.IsDryRun = false;
    }
  }
}
