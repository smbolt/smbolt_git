using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.CHCM.Business.Models
{
  public class Person
  {
    public int PersonId {
      get;
      set;
    }
    public string Prefix {
      get;
      set;
    }
    public string FirstName {
      get;
      set;
    }
    public string MiddleName {
      get;
      set;
    }
    public string LastName {
      get;
      set;
    }
    public string Suffix {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public char? SchoolGrade {
      get;
      set;
    }
    public int? BirthYear {
      get;
      set;
    }
    public int? BirthMonth {
      get;
      set;
    }
    public int? BirthDay {
      get;
      set;
    }
    public bool UseBirthday {
      get;
      set;
    }
    public int? Age {
      get;
      set;
    }

    public string Birthday {
      get {
        return Get_Birthday();
      }
    }


    private string Get_Birthday()
    {
      if (!this.BirthYear.HasValue && !this.BirthMonth.HasValue && !this.BirthDay.HasValue)
        return String.Empty;

      string year = this.BirthYear.HasValue ? this.BirthYear.Value.ToString() : String.Empty;
      string month = this.BirthMonth.HasValue ? this.BirthMonth.Value.ToString() : String.Empty;
      string day = this.BirthDay.HasValue ? this.BirthDay.Value.ToString() : String.Empty;

      string birthDay = String.Empty;


      if (month.IsNotBlank())
        birthDay += month + "/";

      if (day.IsNotBlank())
        birthDay += day + "/";

      if (year.IsNotBlank())
        birthDay += year;

      return birthDay;
    }
  }
}
