using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.WSO
{
  [XMap(XType = XType.Element)]
  public class WsTargetSite
  {
    [XMap]
    public string DeclaringTypeAssemblyName { get; set; }
    [XMap]
    public string DeclaringTypeFullName { get; set; }
    [XMap]
    public string DeclaringTypeModuleName { get; set; }
    [XMap]
    public string DeclaringTypeNamespace { get; set; }
    [XMap]
    public string MemberType { get; set; }
    [XMap]
    public string Name { get; set; }

    public WsTargetSite()
    {
      this.DeclaringTypeAssemblyName = String.Empty;
      this.DeclaringTypeFullName = String.Empty;
      this.DeclaringTypeModuleName = String.Empty;
      this.DeclaringTypeNamespace = String.Empty;
      this.MemberType = String.Empty;
      this.Name = String.Empty;
    }

    public WsTargetSite(Exception ex)
    {
      if (ex == null || ex.TargetSite == null)
        return;

      if (ex.TargetSite.DeclaringType != null && ex.TargetSite.DeclaringType.Assembly != null)
      {
        this.DeclaringTypeAssemblyName = ex.TargetSite.DeclaringType.Assembly.FullName;
        this.DeclaringTypeFullName = ex.TargetSite.DeclaringType.FullName;
        this.DeclaringTypeNamespace = ex.TargetSite.DeclaringType.Namespace;
      }

      if (ex.TargetSite.Module != null)
        this.DeclaringTypeModuleName = ex.TargetSite.Module.Name;

      this.MemberType = ex.TargetSite.MemberType.ToString();

      this.Name = ex.TargetSite.Name; 
    }

    public string ToReport(int level)
    {
      string indent = g.BlankString(level * 2);
      return indent + "Declaring Assembly Name:" + this.DeclaringTypeAssemblyName + g.crlf +
             indent + "Declaring Type Full Name:" + this.DeclaringTypeFullName + g.crlf +
             indent + "Delcaring Type Module Name:" + this.DeclaringTypeModuleName + g.crlf +
             indent + "Declaring Type Namespace:" + this.DeclaringTypeNamespace + g.crlf + 
             indent + "MemberType:" + this.MemberType.ToString() + 
             indent + "Name:" + this.Name + g.crlf; 
    }
  }
}
