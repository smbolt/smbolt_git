using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.VideoSeating.Business
{
  [XMap(XType = XType.Element)]
  public class Trainee
  {
    [XMap]
    public int Number {
      get;
      set;
    }
    [XMap]
    public string FirstName {
      get;
      set;
    }
    [XMap]
    public string LastName {
      get;
      set;
    }
    [XMap]
    public RegistrationMode RegistrationMode {
      get;
      set;
    }

    public string FullName {
      get {
        return (this.FirstName + " " + this.LastName).Trim();
      }
    }
    public string ShortName {
      get {
        return (this.FirstName.Length > 0 ? this.FirstName[0] + ". " + this.LastName : this.LastName).Trim();
      }
    }

    public Trainee()
    {
    }

    public Trainee(int number)
    {
      this.Number = number;
      this.FirstName = String.Empty;
      this.LastName = String.Empty;
      this.RegistrationMode = RegistrationMode.PartTime;
    }

    public Trainee Clone()
    {
      var clone = new Trainee(this.Number);
      clone.FirstName = this.FirstName;
      clone.LastName = this.LastName;
      clone.RegistrationMode = this.RegistrationMode;
      return clone;
    }

    public bool IsUpdated(Trainee compare)
    {
      if (compare.FullName == this.FirstName &&
          compare.LastName == this.LastName &&
          compare.RegistrationMode == this.RegistrationMode)
        return false;

      return true;
    }
  }
}
