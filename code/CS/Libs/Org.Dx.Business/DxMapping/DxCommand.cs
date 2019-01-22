using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class DxCommand
  {
    [XMap(IsRequired = true, IsKey = true)]
    public string Name { get; set; }

    [XMap(IsRequired = true)]
    public DxActionType DxActionType { get; set; }

    public string DxProcessingRoutineName { get { return Get_DxProcessingRoutineName(); } }

    [XMap]
    public string DxActionParms { get; set; }

    [XMap(DefaultValue ="True")]
    public bool IsActive { get; set; }

    public DxCommand()
    {
      this.Name = String.Empty;
      this.DxActionType = DxActionType.NotSet;
      this.DxActionParms = String.Empty;
      this.IsActive = true;
    }

    private string Get_DxProcessingRoutineName()
    {
      if (this.DxActionParms.IsBlank())
        return String.Empty;

      return this.DxActionParms.Split(Constants.CommaDelimiter).First();
    }
  }
}
