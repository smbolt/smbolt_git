using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  [XElementSequence("ListID,FullName,MaxReturned,ActiveStatus,FromModifiedDate,ToModifiedDate," +
                    "NameFilter,NameRangeFilter,TotalBalanceFilter,CurrencyFilter,ClassFilter," +
                    "IncludeRetElement,OwnerID")]
  public class CustomerQueryRq : QueryRqBase
  {
    [XMap(XType = XType.Element)]
    public TotalBalanceFilter TotalBalanceFilter {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public CurrencyFilter CurrencyFilter {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public ClassFilter ClassFilter {
      get;
      set;
    }

    public CustomerQueryRq()
      : base()
    {
      this.TotalBalanceFilter = null;
      this.CurrencyFilter = null;
      this.ClassFilter = null;
    }
  }
}
