using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class NotifyPerson
  {
    public int NotifyPersonId { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string Name { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public bool IsActive { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string EmailAddress { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public bool IsEmailActive { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string SmsNumber { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public bool IsSmsActive { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? NotifyPersonGroupId { get; set; }

    public NotifyPerson()
    {
      this.NotifyPersonId = 0;
      this.Name = String.Empty;
      this.IsActive = false;
      this.EmailAddress = String.Empty;
      this.IsEmailActive = false;
      this.SmsNumber = String.Empty;
      this.IsSmsActive = false;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
    }
  }
}
