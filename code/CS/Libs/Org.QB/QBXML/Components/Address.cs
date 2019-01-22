using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class Address
  {
    [XMap (XType = XType.Element)]
    public string Addr1 { get; set; }

    [XMap(XType = XType.Element)]
    public string Addr2 { get; set; }

    [XMap(XType = XType.Element)]
    public string Addr3 { get; set; }

    [XMap(XType = XType.Element)]
    public string Addr4 { get; set; }

    [XMap(XType = XType.Element)]
    public string Addr5 { get; set; }

    [XMap(XType = XType.Element)]
    public string City { get; set; }

    [XMap(XType = XType.Element)]
    public string State { get; set; }

    [XMap(XType = XType.Element)]
    public string PostalCode { get; set; }

    [XMap(XType = XType.Element)]
    public string Country { get; set; }

    [XMap(XType = XType.Element)]
    public string Note { get; set; }
  }
}
