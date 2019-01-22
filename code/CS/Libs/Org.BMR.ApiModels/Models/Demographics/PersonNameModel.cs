using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class PersonNameModel : ApiModelBase
  {
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Suffix { get; set; }
    public int NameFormatId { get; set; }
    public string CustomName { get; set; }
    public bool UseCustomName { get; set; }

    public PersonNameModel()
    {
      this.Salutation = String.Empty;
      this.FirstName = String.Empty;
      this.MiddleName = String.Empty;
      this.LastName = String.Empty;
      this.Suffix = String.Empty;
      this.NameFormatId = 0;
      this.CustomName = null;
      this.UseCustomName = false;
    }
  }
}
