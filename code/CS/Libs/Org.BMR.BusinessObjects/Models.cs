using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;
using Org.GS;

namespace Org.BMR.BusinessObjects
{

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Account")]
  public class Account : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountId", false, true, true)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountTypeId", false, false, false)]
    public int AccountTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountName", false, false, false)]
    public string AccountName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "OrgId", false, false, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "PasswordHash", false, false, false)]
    public string PasswordHash {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountStatusId", false, false, false)]
    public int AccountStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "ProcessName", false, false, false)]
    public string ProcessName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "SubProcessName", false, false, false)]
    public string SubProcessName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "SubProcessStep", true, false, false)]
    public int? SubProcessStep {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "ProcessEntityId", true, false, false)]
    public int? ProcessEntityId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "SecurityQuestionId", false, false, false)]
    public int SecurityQuestionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "SecurityAnswer", false, false, false)]
    public string SecurityAnswer {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountEmailAddress", false, false, false)]
    public string AccountEmailAddress {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountPIN", false, false, false)]
    public string AccountPIN {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "FirstName", false, false, false)]
    public string FirstName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "LastName", false, false, false)]
    public string LastName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccountPolicyId", false, false, false)]
    public int AccountPolicyId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "LockoutEndDateUtc", true, false, false)]
    public DateTime? LockoutEndDateUtc {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "AccessFailedCount", false, false, false)]
    public int AccessFailedCount {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "CreatedDateTime", false, false, false)]
    public DateTime CreatedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "CreatedAccountId", false, false, false)]
    public int CreatedAccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "ModifiedDateTime", false, false, false)]
    public DateTime ModifiedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Account", "ModifiedAccountId", false, false, false)]
    public int ModifiedAccountId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AccountLoginToken")]
  public class AccountLoginToken : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "AccountLoginTokenId", false, true, true)]
    public long AccountLoginTokenId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "Token", false, false, false)]
    public string Token {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "ClientHash", false, false, false)]
    public string ClientHash {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "TokenCreateDateTime", false, false, false)]
    public DateTime TokenCreateDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "TokenExpireDateTime", false, false, false)]
    public DateTime TokenExpireDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "TokenStatusId", false, false, false)]
    public int TokenStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "IpAddress", false, false, false)]
    public string IpAddress {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "TimesUsed", false, false, false)]
    public int TimesUsed {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountLoginToken", "LogOutDateTime", true, false, false)]
    public DateTime LogOutDateTime {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AccountStatu")]
  public class AccountStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountStatu", "AccountStatusId", false, true, false)]
    public int AccountStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountStatu", "AccountStatus", false, false, false)]
    public string AccountStatus1 {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AccountTokenStatus")]
  public class AccountTokenStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountTokenStatus", "TokenStatusId", false, true, false)]
    public int TokenStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountTokenStatus", "TokenStatusDesc", false, false, false)]
    public string TokenStatusDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AccountType")]
  public class AccountType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountType", "AccountTypeId", false, true, true)]
    public int AccountTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AccountType", "AccountTypeDesc", false, false, false)]
    public string AccountTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Address")]
  public class Address : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "AddressId", false, true, false)]
    public int AddressId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "AddressFormatId", false, false, false)]
    public int AddressFormatId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "StreetAddress1", true, false, false)]
    public string StreetAddress1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "StreetAddress2", true, false, false)]
    public string StreetAddress2 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "StreetAddress3", true, false, false)]
    public string StreetAddress3 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "City", true, false, false)]
    public string City {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "PostalCode", true, false, false)]
    public string PostalCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "PoliticalUnitId1", true, false, false)]
    public int? PoliticalUnitId1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "PoliticalUnit1", true, false, false)]
    public string PoliticalUnit1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "PolilticalUnitId2", true, false, false)]
    public int? PolilticalUnitId2 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "PoliticalUnit2", true, false, false)]
    public string PoliticalUnit2 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Address", "CountryCode", true, false, false)]
    public int? CountryCode {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AddressFormat")]
  public class AddressFormat : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressFormat", "AddressFormatId", false, true, true)]
    public int AddressFormatId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressFormat", "AddressFormatName", false, false, false)]
    public string AddressFormatName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressFormat", "AddressFormatSpec", false, false, false)]
    public string AddressFormatSpec {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AddressType")]
  public class AddressType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressType", "AddressTypeCode", false, true, false)]
    public string AddressTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressType", "AddressTypeValue", false, false, false)]
    public string AddressTypeValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AddressType", "AddressTypeDesc", false, false, false)]
    public string AddressTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AppLog")]
  public class AppLog : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "LogId", false, true, true)]
    public long LogId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "LogDateTime", false, false, false)]
    public DateTime LogDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "SeverityCodeId", false, false, false)]
    public string SeverityCodeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "SeverityCode", false, false, false)]
    public string SeverityCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "Message", false, false, false)]
    public string Message {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "ModuleCode", false, false, false)]
    public int ModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "EventCode", false, false, false)]
    public int EventCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "SessionId", false, false, false)]
    public string SessionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "OrgId", false, false, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "EntityTypeId", false, false, false)]
    public int EntityTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "EntityId", false, false, false)]
    public int EntityId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLog", "UserName", true, false, false)]
    public string UserName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AppLogDetail")]
  public class AppLogDetail : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetail", "LogDetailId", false, true, true)]
    public int LogDetailId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetail", "LogId", false, false, false)]
    public long LogId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetail", "SetId", false, false, false)]
    public long SetId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetail", "AppLogDetailTypeCode", false, false, false)]
    public string AppLogDetailTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetail", "LogDetail", false, false, false)]
    public string LogDetail {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AppLogDetailType")]
  public class AppLogDetailType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetailType", "AppLogDetailTypeCode", false, true, false)]
    public string AppLogDetailTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogDetailType", "AppLogDetailTypeDesc", false, false, false)]
    public string AppLogDetailTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "AppLogSeverity")]
  public class AppLogSeverity : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogSeverity", "SeverityCodeId", false, true, false)]
    public int SeverityCodeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogSeverity", "SeverityCode", false, false, false)]
    public string SeverityCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "AppLogSeverity", "SeverityDesc", false, false, false)]
    public string SeverityDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Calendar")]
  public class Calendar : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Calendar", "CalendarId", false, true, true)]
    public int CalendarId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Calendar", "CalendarYear", false, false, false)]
    public int CalendarYear {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Calendar", "CalendarDate", false, false, false)]
    public DateTime CalendarDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Calendar", "DateTypeID", false, false, false)]
    public int DateTypeID {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Calendar", "DateType", false, false, false)]
    public string DateType {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ConfigItem")]
  public class ConfigItem : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ConfigItemId", false, true, true)]
    public int ConfigItemId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ModuleCode", false, false, false)]
    public int ModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ConfigItemTypeCode", false, false, false)]
    public string ConfigItemTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ConfigItemPriority", false, false, false)]
    public int ConfigItemPriority {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "Category", true, false, false)]
    public string Category {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ConfigItemKey", false, false, false)]
    public string ConfigItemKey {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "ConfigItemValue", true, false, false)]
    public string ConfigItemValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItem", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ConfigItemType")]
  public class ConfigItemType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItemType", "ConfigItemTypeCode", false, true, false)]
    public string ConfigItemTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ConfigItemType", "ConfigItemTypeValue", false, false, false)]
    public string ConfigItemTypeValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Country")]
  public class Country : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Country", "CountryCode", false, true, false)]
    public int CountryCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Country", "CountryCodeA2", false, false, false)]
    public string CountryCodeA2 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Country", "CountryCodeA3", false, false, false)]
    public string CountryCodeA3 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Country", "CountryName", false, false, false)]
    public string CountryName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Coupon")]
  public class Coupon : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Coupon", "CouponId", false, true, true)]
    public int CouponId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Coupon", "CouponTypeCode", false, false, false)]
    public string CouponTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Coupon", "CouponDesc", false, false, false)]
    public string CouponDesc {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Coupon", "Discount", false, false, false)]
    public float Discount {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "CouponType")]
  public class CouponType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "CouponType", "CouponTypeCode", false, true, false)]
    public string CouponTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "CouponType", "CouponTypeDesc", false, false, false)]
    public string CouponTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "EmailAddress")]
  public class EmailAddress : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddress", "EmailAddressId", false, true, true)]
    public int EmailAddressId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddress", "EmailAddressValue", false, false, false)]
    public string EmailAddressValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "EmailAddressStatus")]
  public class EmailAddressStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressStatus", "EmailAddressStatusId", false, true, false)]
    public int EmailAddressStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressStatus", "EmailAddressStatusAbbr", false, false, false)]
    public string EmailAddressStatusAbbr {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressStatus", "EmailAddressStatusDesc", false, false, false)]
    public string EmailAddressStatusDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "EmailAddressType")]
  public class EmailAddressType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressType", "EmailAddressTypeCode", false, true, false)]
    public string EmailAddressTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressType", "EmailAddressTypeValue", false, false, false)]
    public string EmailAddressTypeValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "EmailAddressType", "EmailAddressTypeDesc", false, false, false)]
    public string EmailAddressTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Event")]
  public class Event : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Event", "EventCode", false, true, false)]
    public int EventCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Event", "EventDesc", false, false, false)]
    public string EventDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "FrameworkVersion")]
  public class FrameworkVersion : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "FrameworkVersionId", false, true, true)]
    public int FrameworkVersionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "FrameworkVersionString", false, false, false)]
    public string FrameworkVersionString {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "Version", false, false, false)]
    public string Version {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "VersionNum", false, false, false)]
    public string VersionNum {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "ServicePackString", false, false, false)]
    public string ServicePackString {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "FrameworkVersion", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Group")]
  public class Group : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Group", "GroupId", false, true, true)]
    public int GroupId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Group", "OrgId", false, false, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Group", "GroupName", false, false, false)]
    public string GroupName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Group", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "GroupMembership")]
  public class GroupMembership : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "GroupMembership", "UserGroupMembershipId", false, true, true)]
    public int UserGroupMembershipId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "GroupMembership", "GroupId", false, false, false)]
    public int GroupId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "GroupMembership", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "GroupMembership", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "HolidayAction")]
  public class HolidayAction : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "HolidayActionId", false, true, true)]
    public int HolidayActionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "Description", false, false, false)]
    public string Description {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "HolidayAction", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Module")]
  public class Module : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Module", "ModuleCode", false, true, false)]
    public int ModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Module", "ModuleName", false, false, false)]
    public string ModuleName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Order")]
  public class Order : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "OrderId", false, true, true)]
    public int OrderId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "PersonId", false, false, false)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "OrderDate", false, false, false)]
    public DateTime OrderDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "CreateDateTime", false, false, false)]
    public DateTime CreateDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "IpAddress", false, false, false)]
    public string IpAddress {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Order", "SessionId", false, false, false)]
    public string SessionId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "OrderDetail")]
  public class OrderDetail : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "OrderDetailId", false, true, true)]
    public int OrderDetailId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "OrderId", false, false, false)]
    public int OrderId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "ProductId", false, false, false)]
    public int ProductId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "Quantity", false, false, false)]
    public int Quantity {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "Price", false, false, false)]
    public decimal Price {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "LineItemDiscount", false, false, false)]
    public float LineItemDiscount {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "DiscountedPrice", false, false, false)]
    public decimal DiscountedPrice {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrderDetail", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Organization")]
  public class Organization : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "OrgId", false, true, true)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "ParentOrgId", true, false, false)]
    public int? ParentOrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "OrgStatusId", false, false, false)]
    public int OrgStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "OrgTypeId", false, false, false)]
    public int OrgTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "OrgName", false, false, false)]
    public string OrgName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Organization", "OrgDescription", true, false, false)]
    public string OrgDescription {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "OrgPerson")]
  public class OrgPerson : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPerson", "OrgPersonId", false, true, false)]
    public int OrgPersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPerson", "OrgId", false, false, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPerson", "PersonId", false, false, false)]
    public int PersonId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "OrgPersonType")]
  public class OrgPersonType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPersonType", "OrgPersonTypeId", false, true, true)]
    public int OrgPersonTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPersonType", "OrgId", false, false, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPersonType", "OrgPersonTypeAbbr", false, false, false)]
    public string OrgPersonTypeAbbr {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPersonType", "OrgPersonTypeValue", false, false, false)]
    public string OrgPersonTypeValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgPersonType", "OrgPersonTypeDesc", false, false, false)]
    public string OrgPersonTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "OrgStatus")]
  public class OrgStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgStatus", "OrgStatusId", false, true, false)]
    public int OrgStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgStatus", "OrgStatusAbbr", false, false, false)]
    public string OrgStatusAbbr {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgStatus", "OrgStatusValue", false, false, false)]
    public string OrgStatusValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "OrgType")]
  public class OrgType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgType", "OrgTypeId", false, true, false)]
    public int OrgTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "OrgType", "OrgTypeValue", false, false, false)]
    public string OrgTypeValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PasswordChangeHistory")]
  public class PasswordChangeHistory : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangeId", false, true, true)]
    public int PasswordChangeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "CreateDateTime", false, false, false)]
    public DateTime CreateDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangeRequiredDate", false, false, false)]
    public DateTime PasswordChangeRequiredDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangeReasonCode", false, false, false)]
    public int PasswordChangeReasonCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangeVerificationCode", false, false, false)]
    public string PasswordChangeVerificationCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "NotificationSentDateTime", true, false, false)]
    public DateTime? NotificationSentDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangeAttemptDateTime", true, false, false)]
    public DateTime? PasswordChangeAttemptDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeHistory", "PasswordChangedDateTime", true, false, false)]
    public DateTime? PasswordChangedDateTime {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PasswordChangeReason")]
  public class PasswordChangeReason : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeReason", "PasswordChangeReasonCode", false, true, false)]
    public int PasswordChangeReasonCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PasswordChangeReason", "PasswordChangeReasonValue", false, false, false)]
    public string PasswordChangeReasonValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PeriodContext")]
  public class PeriodContext : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "PeriodContextId", false, true, true)]
    public int PeriodContextId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "Period", false, false, false)]
    public string Period {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PeriodContext", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Person")]
  public class Person : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "PersonId", false, true, true)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "AccountOwner", false, false, false)]
    public bool AccountOwner {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "PersonStatusId", false, false, false)]
    public int PersonStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "Salutation", true, false, false)]
    public string Salutation {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "Title", true, false, false)]
    public string Title {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "FirstName", true, false, false)]
    public string FirstName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "MiddleName", true, false, false)]
    public string MiddleName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "LastName", false, false, false)]
    public string LastName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "Suffix", true, false, false)]
    public string Suffix {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "NameFormatId", false, false, false)]
    public int NameFormatId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "Gender", true, false, false)]
    public string Gender {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "CustomName", true, false, false)]
    public string CustomName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "UseCustomName", false, false, false)]
    public bool UseCustomName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "DateOfBirth", true, false, false)]
    public DateTime? DateOfBirth {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "CreatedDateTime", false, false, false)]
    public DateTime CreatedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "CreatedAccountId", false, false, false)]
    public int CreatedAccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "ModifiedDateTime", false, false, false)]
    public DateTime ModifiedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Person", "ModifiedAccountId", false, false, false)]
    public int ModifiedAccountId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PersonAddress")]
  public class PersonAddress : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "PersonId", false, true, false)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "AddressId", false, true, false)]
    public int AddressId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "AddressTypeCode", false, false, false)]
    public string AddressTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "IsPrimaryAddress", false, false, false)]
    public bool IsPrimaryAddress {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "Seq", false, false, false)]
    public int Seq {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonAddress", "PrivacyStatusCode", false, false, false)]
    public string PrivacyStatusCode {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PersonEmailAddress")]
  public class PersonEmailAddress : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "PersonEmailAddressId", false, true, true)]
    public int PersonEmailAddressId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "PersonId", false, false, false)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "EmailAddressId", false, false, false)]
    public int EmailAddressId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "EmailAddressTypeCode", false, false, false)]
    public string EmailAddressTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "EmailAddressStatusId", false, false, false)]
    public int EmailAddressStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonEmailAddress", "PrivacyStatusCode", false, false, false)]
    public string PrivacyStatusCode {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PersonPhoneNumber")]
  public class PersonPhoneNumber : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonPhoneNumber", "PersonId", false, true, false)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonPhoneNumber", "PhoneNumberId", false, true, false)]
    public int PhoneNumberId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonPhoneNumber", "PhoneNumberTypeCode", false, false, false)]
    public string PhoneNumberTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonPhoneNumber", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonPhoneNumber", "PrivacyStatusCode", false, false, false)]
    public string PrivacyStatusCode {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PersonStatus")]
  public class PersonStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonStatus", "PersonStatusId", false, true, false)]
    public int PersonStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonStatus", "PersonStatus", false, false, false)]
    public string PersonStatus1 {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PersonType")]
  public class PersonType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonType", "PersonTypeId", false, true, false)]
    public int PersonTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PersonType", "PersonTypeValue", false, false, false)]
    public string PersonTypeValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PhoneNumber")]
  public class PhoneNumber : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumber", "PhoneNumberId", false, true, true)]
    public int PhoneNumberId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumber", "PhoneNumberFormatCode", false, false, false)]
    public string PhoneNumberFormatCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumber", "PhoneNumberValue", false, false, false)]
    public string PhoneNumberValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PhoneNumberFormat")]
  public class PhoneNumberFormat : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumberFormat", "PhoneNumberFormatCode", false, true, false)]
    public string PhoneNumberFormatCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumberFormat", "PhoneNumberFormatDesc", false, false, false)]
    public string PhoneNumberFormatDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PhoneNumberType")]
  public class PhoneNumberType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumberType", "PhoneNumberTypeCode", false, true, false)]
    public string PhoneNumberTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumberType", "PhoneNumberTypeValue", false, false, false)]
    public string PhoneNumberTypeValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PhoneNumberType", "PhoneNumberTypeDesc", false, false, false)]
    public string PhoneNumberTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PoliticalUnit")]
  public class PoliticalUnit : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "PoliticalUnitId", false, true, true)]
    public int PoliticalUnitId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "CountryCode", false, false, false)]
    public int CountryCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "PoliticalUnitTypeId", false, false, false)]
    public int PoliticalUnitTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "ParentPoliticalUnitId", true, false, false)]
    public int? ParentPoliticalUnitId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "Name", false, false, false)]
    public string Name {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "Abbreviation", true, false, false)]
    public string Abbreviation {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "Description", false, false, false)]
    public string Description {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "CapitalOrPrimaryCity", true, false, false)]
    public string CapitalOrPrimaryCity {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnit", "SuppressUsage", true, false, false)]
    public bool? SuppressUsage {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PoliticalUnitType")]
  public class PoliticalUnitType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnitType", "PoliticalUnitTypeId", false, true, false)]
    public int PoliticalUnitTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PoliticalUnitType", "PoliticalUnitTypeValue", false, false, false)]
    public string PoliticalUnitTypeValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "PrivacyStatus")]
  public class PrivacyStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PrivacyStatus", "PrivacyStatusCode", false, true, false)]
    public string PrivacyStatusCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "PrivacyStatus", "PrivacyStatusDesc", false, false, false)]
    public string PrivacyStatusDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Product")]
  public class Product : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "ProductId", false, true, true)]
    public int ProductId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "ProductCategoryCode", false, false, false)]
    public string ProductCategoryCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "ProductDescription", false, false, false)]
    public string ProductDescription {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "Price", false, false, false)]
    public decimal Price {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Product", "Cost", false, false, false)]
    public decimal Cost {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ProductCategory")]
  public class ProductCategory : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ProductCategory", "ProductCategoryCode", false, true, false)]
    public string ProductCategoryCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ProductCategory", "ProductCategoryDesc", false, false, false)]
    public string ProductCategoryDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Referral")]
  public class Referral : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "ReferralId", false, true, true)]
    public int ReferralId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "ReferralTypeCode", false, false, false)]
    public string ReferralTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "ReferralCode", false, false, false)]
    public string ReferralCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "CouponId", false, false, false)]
    public int CouponId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Referral", "Commission", false, false, false)]
    public float Commission {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ReferralType")]
  public class ReferralType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ReferralType", "ReferralTypeCode", false, true, false)]
    public string ReferralTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ReferralType", "ReferralTypeDesc", false, false, false)]
    public string ReferralTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "RelatedOrg")]
  public class RelatedOrg : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "OrgId", false, true, false)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "RelatedOrgId", false, true, false)]
    public int RelatedOrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "RelationshipTypeCode", false, false, false)]
    public string RelationshipTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "RelationshipBeginDateTime", true, false, false)]
    public DateTime? RelationshipBeginDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "RelationshipEndDateTime", true, false, false)]
    public DateTime? RelationshipEndDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedOrg", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "RelatedPerson")]
  public class RelatedPerson : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "PersonId", false, true, false)]
    public int PersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "RelatedPersonId", false, true, false)]
    public int RelatedPersonId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "RelationshipTypeCode", false, false, false)]
    public string RelationshipTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "RelationshipBeginDateTime", true, false, false)]
    public DateTime? RelationshipBeginDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "RelationshipEndDateTime", true, false, false)]
    public DateTime? RelationshipEndDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelatedPerson", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "RelationshipType")]
  public class RelationshipType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelationshipType", "RelationshipTypeCode", false, true, false)]
    public string RelationshipTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "RelationshipType", "RelationshipTypeDesc", false, false, false)]
    public string RelationshipTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Resume")]
  public partial class Resume : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "ResumeId", false, true, true)]
    public int ResumeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "AccountId", false, false, false)]
    public int AccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "ResumeName", false, false, false)]
    public string ResumeName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "ResumeStatusId", false, false, false)]
    public int ResumeStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "CreatedDateTime", false, false, false)]
    public DateTime CreatedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "CreatedAccountId", false, false, false)]
    public int CreatedAccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "ModifiedDateTime", false, false, false)]
    public DateTime? ModifiedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Resume", "ModifiedAccountId", false, false, false)]
    public int? ModifiedAccountId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ResumeStatu")]
  public partial class ResumeStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ResumeStatu", "ResumeStatusId", false, true, true)]
    public int ResumeStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ResumeStatu", "ResumeStatusDesc", false, false, false)]
    public string ResumeStatusDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ScheduledTask")]
  public class ScheduledTask : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "ScheduledTaskId", false, true, true)]
    public int ScheduledTaskId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "TaskName", false, false, false)]
    public string TaskName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "IsManaged", false, false, false)]
    public bool IsManaged {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "TaskNumber", true, false, false)]
    public int? TaskNumber {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "AssemblyName", false, false, false)]
    public string AssemblyName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "AssemblyLocation", false, false, false)]
    public string AssemblyLocation {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "ClassName", false, false, false)]
    public string ClassName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "StoredProcedureName", true, false, false)]
    public string StoredProcedureName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "IsActive", false, false, false)]
    public bool IsActive {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "IsLongRunning", false, false, false)]
    public bool IsLongRunning {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "ActiveScheduleId", true, false, false)]
    public int? ActiveScheduleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTask", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "ScheduledTaskParameter")]
  public class ScheduledTaskParameter : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ParameterId", false, true, true)]
    public int ParameterId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ScheduledTaskId", true, false, false)]
    public int? ScheduledTaskId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ParameterSetName", true, false, false)]
    public string ParameterSetName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ParameterName", false, false, false)]
    public string ParameterName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ParameterValue", true, false, false)]
    public string ParameterValue {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "DataType", false, false, false)]
    public string DataType {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "ScheduledTaskParameter", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SecurityQuestion")]
  public class SecurityQuestion : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SecurityQuestion", "SecurityQuestionId", false, true, true)]
    public int SecurityQuestionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SecurityQuestion", "SecurityQuestionText", false, false, false)]
    public string SecurityQuestionText {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SecurityQuestion", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SecurityQuestion", "Sequencer", true, false, false)]
    public int? Sequencer {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Service")]
  public class Service : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ServiceId", false, true, true)]
    public int ServiceId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "OrderId", false, false, false)]
    public int OrderId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ProductId", false, false, false)]
    public int ProductId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ServiceBeginDate", false, false, false)]
    public DateTime ServiceBeginDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ServiceDurationUnit", false, false, false)]
    public string ServiceDurationUnit {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ServiceDurationQuantity", false, false, false)]
    public int ServiceDurationQuantity {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "ServiceEndDate", false, false, false)]
    public DateTime ServiceEndDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Service", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SoftwareModule")]
  public class SoftwareModule : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "SoftwareModuleId", false, true, true)]
    public int SoftwareModuleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "SoftwareModuleCode", false, false, false)]
    public int SoftwareModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "SoftwareModuleName", false, false, false)]
    public string SoftwareModuleName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "SoftwareModuleTypeId", false, false, false)]
    public int SoftwareModuleTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "CreatedDateTime", false, false, false)]
    public DateTime CreatedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "CreatedAccountId", false, false, false)]
    public int CreatedAccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "ModifiedDateTime", true, false, false)]
    public DateTime? ModifiedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModule", "ModifiedAccountId", true, false, false)]
    public int? ModifiedAccountId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SoftwareModuleType")]
  public class SoftwareModuleType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModuleType", "SoftwareModuleTypeId", false, true, false)]
    public int SoftwareModuleTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareModuleType", "SoftwareModuleTypeName", false, false, false)]
    public string SoftwareModuleTypeName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SoftwarePlatform")]
  public class SoftwarePlatform : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwarePlatform", "SoftwarePlatformId", false, true, true)]
    public int SoftwarePlatformId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwarePlatform", "SoftwarePlatformString", false, false, false)]
    public string SoftwarePlatformString {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwarePlatform", "PlatformDescription", false, false, false)]
    public string PlatformDescription {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwarePlatform", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SoftwareRepository")]
  public class SoftwareRepository : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareRepository", "RepositoryId", false, true, false)]
    public int RepositoryId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareRepository", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareRepository", "RepositoryRoot", false, false, false)]
    public string RepositoryRoot {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "SoftwareVersion")]
  public class SoftwareVersion : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "SoftwareVersionId", false, true, true)]
    public int SoftwareVersionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "SoftwareModuleId", false, false, false)]
    public int SoftwareModuleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "SoftwareVersion", false, false, false)]
    public string SoftwareVersion1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "SoftwarePlatformId", false, false, false)]
    public int SoftwarePlatformId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "SoftwareVersion", "RepositoryId", false, false, false)]
    public int RepositoryId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "Status")]
  public class Status : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Status", "StatusId", false, true, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "Status", "StatusValue", false, false, false)]
    public string StatusValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "StatusUsage")]
  public class StatusUsage : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "StatusUsage", "StatusUsageId", false, true, true)]
    public int StatusUsageId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "StatusUsage", "StatusListName", false, false, false)]
    public string StatusListName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "StatusUsage", "StatusId", false, false, false)]
    public int StatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "StatusUsage", "SortId", false, false, false)]
    public int SortId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "StatusUsage", "Omit", true, false, false)]
    public bool? Omit {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "sysdiagrams")]
  public class sysdiagrams : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "sysdiagrams", "name", false, false, false)]
    public string name {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "sysdiagrams", "principal_id", false, false, false)]
    public int principal_id {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "sysdiagrams", "diagram_id", false, true, true)]
    public int diagram_id {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "sysdiagrams", "version", true, false, false)]
    public int? version {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "sysdiagrams", "definition", true, false, false)]
    public byte[] definition {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TaskSchedule")]
  public class TaskSchedule : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "TaskScheduleId", false, true, true)]
    public int TaskScheduleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "ScheduledTaskId", false, false, false)]
    public int ScheduledTaskId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "ScheduleName", false, false, false)]
    public string ScheduleName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "ScheduleNumber", true, false, false)]
    public int? ScheduleNumber {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "IsActive", false, false, false)]
    public bool IsActive {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "IsManaged", false, false, false)]
    public bool IsManaged {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskSchedule", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TaskScheduleElement")]
  public class TaskScheduleElement : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "TaskScheduleElementId", false, true, true)]
    public int TaskScheduleElementId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "TaskScheduleId", false, false, false)]
    public int TaskScheduleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "IsActive", false, false, false)]
    public bool IsActive {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "IsManaged", false, false, false)]
    public bool IsManaged {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ElementNumber", true, false, false)]
    public int? ElementNumber {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "TaskScheduleExecutionTypeId", false, false, false)]
    public int TaskScheduleExecutionTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "FrequencySeconds", true, false, false)]
    public decimal? FrequencySeconds {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "IsClockAligned", false, false, false)]
    public bool IsClockAligned {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ScheduleElementPriority", true, false, false)]
    public int? ScheduleElementPriority {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "StartDate", true, false, false)]
    public DateTime? StartDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "StartTime", true, false, false)]
    public TimeSpan? StartTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "EndDate", true, false, false)]
    public DateTime? EndDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "EndTime", true, false, false)]
    public TimeSpan? EndTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "IntervalTypeId", true, false, false)]
    public int? IntervalTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnSunday", false, false, false)]
    public bool OnSunday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnMonday", false, false, false)]
    public bool OnMonday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnTuesday", false, false, false)]
    public bool OnTuesday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnWednesday", false, false, false)]
    public bool OnWednesday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnThursday", false, false, false)]
    public bool OnThursday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnFriday", false, false, false)]
    public bool OnFriday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnSaturday", false, false, false)]
    public bool OnSaturday {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnWorkDays", false, false, false)]
    public bool OnWorkDays {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnEvenDays", false, false, false)]
    public bool OnEvenDays {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "OnOddDays", false, false, false)]
    public bool OnOddDays {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "SpecificDays", true, false, false)]
    public string SpecificDays {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ExceptSpecificDays", false, false, false)]
    public bool ExceptSpecificDays {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "First", false, false, false)]
    public bool First {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Second", false, false, false)]
    public bool Second {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Third", false, false, false)]
    public bool Third {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Fourth", false, false, false)]
    public bool Fourth {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Fifth", false, false, false)]
    public bool Fifth {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Last", false, false, false)]
    public bool Last {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "Every", false, false, false)]
    public bool Every {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "HolidayActionId", true, false, false)]
    public int? HolidayActionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "PeriodContextId", true, false, false)]
    public int? PeriodContextId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ExecutionLimit", true, false, false)]
    public int? ExecutionLimit {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "MaxRunTimeSeconds", true, false, false)]
    public int? MaxRunTimeSeconds {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleElement", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TaskScheduleExecutionType")]
  public class TaskScheduleExecutionType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "TaskScheduleExecutionTypeId", false, true, true)]
    public int TaskScheduleExecutionTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "ExecutionType", false, false, false)]
    public string ExecutionType {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleExecutionType", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TaskScheduleIntervalType")]
  public class TaskScheduleIntervalType : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "IntervalTypeId", false, true, true)]
    public int IntervalTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "IntervalType", false, false, false)]
    public string IntervalType {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "CreatedBy", false, false, false)]
    public int CreatedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "CreatedDate", false, false, false)]
    public DateTime CreatedDate {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "ModifiedBy", false, false, false)]
    public int ModifiedBy {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TaskScheduleIntervalType", "ModifiedDate", false, false, false)]
    public DateTime ModifiedDate {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TestTable1")]
  public class TestTable1 : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "int_pk", false, true, true)]
    public int int_pk {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "bigint_n", true, false, false)]
    public long? bigint_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "bigint_nn", false, false, false)]
    public long bigint_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "binary_1000_n", true, false, false)]
    public byte[] binary_1000_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "binary_1000_nn", false, false, false)]
    public byte[] binary_1000_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "bit_n", true, false, false)]
    public bool? bit_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "bit_nn", false, false, false)]
    public bool bit_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "char_20_n", true, false, false)]
    public string char_20_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "char_20_nn", false, false, false)]
    public string char_20_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "date_n", true, false, false)]
    public DateTime? date_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "date_nn", false, false, false)]
    public DateTime date_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetime_n", true, false, false)]
    public DateTime? datetime_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetime_nn", false, false, false)]
    public DateTime datetime_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetime2_7_n", true, false, false)]
    public DateTime? datetime2_7_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetime2_7_nn", false, false, false)]
    public DateTime datetime2_7_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetimeoffset_7_n", true, false, false)]
    public TimeSpan? datetimeoffset_7_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "datetimeoffset_7_nn", false, false, false)]
    public TimeSpan datetimeoffset_7_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "decimal18_0_n", true, false, false)]
    public decimal? decimal18_0_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "decimal18_0_nn", false, false, false)]
    public decimal decimal18_0_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "decimal18_2_n", true, false, false)]
    public decimal? decimal18_2_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "decimal18_2_nn", false, false, false)]
    public decimal decimal18_2_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "float_n", true, false, false)]
    public float? float_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "float_nn", false, false, false)]
    public float float_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "image_n", true, false, false)]
    public Image image_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "image_nn", false, false, false)]
    public Image image_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "int_n", true, false, false)]
    public int? int_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "int_nn", false, false, false)]
    public int int_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "money_n", true, false, false)]
    public decimal? money_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "money_nn", false, false, false)]
    public decimal money_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "nchar_20_n", true, false, false)]
    public string nchar_20_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "nchar_20_nn", false, false, false)]
    public string nchar_20_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "ntext_n", true, false, false)]
    public string ntext_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "ntext_nn", false, false, false)]
    public string ntext_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "numeric_18_0_n", true, false, false)]
    public decimal? numeric_18_0_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "numeric_18_0_nn", false, false, false)]
    public decimal numeric_18_0_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "numeric_18_2_n", true, false, false)]
    public decimal? numeric_18_2_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "numeric_18_2_nn", false, false, false)]
    public decimal numeric_18_2_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "nvarchar_50_n", true, false, false)]
    public string nvarchar_50_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "nvarchar_50_nn", false, false, false)]
    public string nvarchar_50_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "real_n", true, false, false)]
    public float? real_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "real_nn", false, false, false)]
    public float real_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smalldatetime_n", true, false, false)]
    public DateTime? smalldatetime_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smalldatetime_nn", false, false, false)]
    public DateTime smalldatetime_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smallint_n", true, false, false)]
    public int? smallint_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smallint_nn", false, false, false)]
    public int smallint_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smallmoney_n", true, false, false)]
    public decimal? smallmoney_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "smallmoney_nn", false, false, false)]
    public decimal smallmoney_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "text_n", true, false, false)]
    public string text_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "text_nn", false, false, false)]
    public string text_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "time_7_n", true, false, false)]
    public TimeSpan? time_7_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "time_7_nn", false, false, false)]
    public TimeSpan time_7_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "tinyint_n", true, false, false)]
    public int? tinyint_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "tinyint_nn", false, false, false)]
    public int tinyint_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "uniqueidentifier_n", true, false, false)]
    public Guid? uniqueidentifier_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "uniqueidentifier_nn", false, false, false)]
    public Guid uniqueidentifier_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "varbinary_1000_n", true, false, false)]
    public byte[] varbinary_1000_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "varbinary_1000_nn", false, false, false)]
    public byte[] varbinary_1000_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "varchar_50_n", true, false, false)]
    public string varchar_50_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "varchar_50_nn", false, false, false)]
    public string varchar_50_nn {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "xml_n", true, false, false)]
    public XElement xml_n {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable1", "xml_nn", false, false, false)]
    public XElement xml_nn {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TestTable2")]
  public class TestTable2 : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable2", "Col1", true, false, false)]
    public string Col1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TestTable2", "Col2", true, false, false)]
    public string Col2 {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TriviaAnswers")]
  public class TriviaAnswers : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaAnswers", "QuestionId", false, false, false)]
    public int QuestionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaAnswers", "OptionId", false, false, false)]
    public int OptionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaAnswers", "Id", false, true, true)]
    public int Id {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaAnswers", "UserId", true, false, false)]
    public string UserId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TriviaOptions")]
  public class TriviaOptions : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaOptions", "QuestionId", false, false, false)]
    public int QuestionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaOptions", "Id", false, false, false)]
    public int Id {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaOptions", "Title", false, false, false)]
    public string Title {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaOptions", "IsCorrect", false, false, false)]
    public bool IsCorrect {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Adsdi_Org", "dbo", "TriviaQuestions")]
  public class TriviaQuestions : ModelBase
  {
    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaQuestions", "Id", false, false, false)]
    public int Id {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Adsdi_Org", "dbo", "TriviaQuestions", "Title", false, false, false)]
    public string Title {
      get;
      set;
    }
  }




}
