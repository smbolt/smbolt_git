using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class Contact
  {
    [XMap(XType = XType.Element)]
    public string Salutation { get; set; }

    [XMap(XType = XType.Element)]
    public string FirstName { get; set; }

    [XMap(XType = XType.Element)]
    public string MiddleName { get; set; }

    [XMap(XType = XType.Element)]
    public string LastName { get; set; }

    [XMap(XType = XType.Element)]
    public string JobTitle { get; set; }

    [XMap(XType = XType.Element)]
    public AdditionalContactRefList AdditionalContactRefList { get; set; }
  }
}
