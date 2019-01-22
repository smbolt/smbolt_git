using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ProgramFunction
  {
    [XMap(IsRequired = true, IsExplicit = true, IsKey = true)]
    public int FunctionNumber {
      get;
      set;
    }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string FunctionName {
      get;
      set;
    }

    public ProgramFunction()
    {
      this.FunctionNumber = -1;
      this.FunctionName = String.Empty;
    }

    public ProgramFunction(int functionNumber, string functionName)
    {
      this.FunctionNumber = functionNumber;
      this.FunctionName = functionName;
    }
  }
}
