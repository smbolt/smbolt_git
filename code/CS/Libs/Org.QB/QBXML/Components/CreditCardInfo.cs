using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class CreditCardInfo
  {
    [XMap (XType = XType.Element)]
    public string CreditCardNumber {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public int ExpirationMonth {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public int ExpirationYear {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string NameOnCard {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string CreditCardAddress {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string CreditCardPostalCode {
      get;
      set;
    }
  }
}
