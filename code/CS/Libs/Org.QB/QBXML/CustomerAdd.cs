using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  [XElementSequence("Name,IsActive,ClassRef,ParentRef,CompanyName,Salutation,FirstName,MiddleName,LastName,JobTitle,BillAddress," +
                    "ShipAddress,ShipToAddress,Phone,AltPhone,Fax,Email,Cc,Contact,AltContact,AdditionalContactRefList,Contacts," +
                    "CustomerTypeRef,TermsRef,SalesRepRef,OpenBalance,OpenBalanceDate,SalesTaxCodeRef,ItemSalesTaxRef," +
                    "ResaleNumber,AccountNumber,CreditLimit,PreferredPaymentMethodRef,CreditCardInfo,JobStatus,JobStartDate," +
                    "JobProjectedEndDate,JobEndDate,JobDesc,JobTypeRef,Notes,AdditionalNotes,PreferredDeliveryMethod," +
                    "PriceLevelRef,ExternalGUID,CurrencyRef")]
  public class CustomerAdd
  {
    [XMap (XType = XType.Element)]
    public string Name {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public bool IsActive {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Ref ClassRef {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Ref ParentRef {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public string CompanyName {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Salutation {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string FirstName {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string MiddleName {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string LastName {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string JobTitle {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Address BillAddress {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Address ShipAddress {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Address ShipToAddress {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Phone {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string AltPhone {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Fax {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Email {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Cc {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Contact {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string AltContact {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public AdditionalContactRefList AdditionalContactRefList {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Ref CustomerTypeRef {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public Ref TermsRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref SalesRepRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public decimal? OpenBalance {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? OpenBalanceDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref SalesTaxCodeRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref ItemSalesTaxRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string ResaleNumber {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string AccountNumber {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public decimal? CreditLimit {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref PreferredPaymentMethodRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public CreditCardInfo CreditCardInfo {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public JobStatus JobStatus {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? JobStartDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? JobProjectedEndDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? JobEndDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string JobDesc {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref JobTypeRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string Notes {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DeliveryMethod PreferredDeliveryMethod {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref PriceLevelRef {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public string ExternalGUID {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Ref CurrencyRef {
      get;
      set;
    }
  }
}
