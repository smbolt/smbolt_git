using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldingSystems.FieldVisor.API.DataContracts.Request
{
  public class Authentication
  {
    public string CompanyCode {
      get;
      set;
    }
    public string Username {
      get;
      set;
    }
    public string Password {
      get;
      set;
    }

    public Authentication(string companyCode, string userName, string password)
    {
      CompanyCode = companyCode;
      Username = userName;
      Password = password;
    }

  }

  public class Parameters
  {
    public Int16 GroupId {
      get;
      set;
    }
    public DateTime? StartDT {
      get;
      set;
    }
    public DateTime? EndDT {
      get;
      set;
    }
    public Int16 UserId {
      get;
      set;
    }
    public Parameters(Int16 groupid, DateTime? startdt, DateTime? enddt, Int16 userid)
    {
      GroupId = groupid;
      StartDT = startdt;
      EndDT = enddt;
      UserId = userid;
    }
  }
}
