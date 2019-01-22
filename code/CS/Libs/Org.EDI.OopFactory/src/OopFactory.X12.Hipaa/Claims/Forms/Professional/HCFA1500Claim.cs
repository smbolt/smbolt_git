using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
  public class HCFA1500Claim
  {
    /*
     * 2011/8/16, jhalliday - New Data Model for 837P (Professional) claim.
     *
     * Team: dstrubhar, jhalliday and epkrause
     *
     * Purpose:
     * To create a C# object model that will serve as a container for the X12 837P data
     * AS ENTERED from a HCFA 1500 Professional claim form.
     *
     * Goal:
     * The team has the overall goal of creating tools that can be used to consume and
     * manipulate X12 messages (AKA files/documents) without the need to have a big project
     * budget.  For that reason, this and the related X12 Parser project tools are all open
     * source and freely usable.
     */
    public HCFA1500Claim()
    {
      if (Field03_PatientsDateOfBirth == null) Field03_PatientsDateOfBirth = new FormDate();
      if (Field09b_OtherInsuredsDateOfBirth == null) Field09b_OtherInsuredsDateOfBirth = new FormDate();
      if (Field11a_InsuredsDateOfBirth == null) Field11a_InsuredsDateOfBirth = new FormDate();
      if (Field12_PatientsOrAuthorizedSignatureDate == null) Field12_PatientsOrAuthorizedSignatureDate = new FormDate();
      if (Field14_DateOfCurrentIllnessInjuryOrPregnancy == null) Field14_DateOfCurrentIllnessInjuryOrPregnancy = new FormDate();
      if (Field15_DatePatientHadSameOrSimilarIllness == null) Field15_DatePatientHadSameOrSimilarIllness = new FormDate();
      if (Field16_DatePatientUnableToWork_Start == null) Field16_DatePatientUnableToWork_Start = new FormDate();
      if (Field16_DatePatientUnableToWork_End == null) Field16_DatePatientUnableToWork_End = new FormDate();
      if (Field18_HospitalizationDateFrom == null) Field18_HospitalizationDateFrom = new FormDate();
      if (Field18_HospitalizationDateTo == null) Field18_HospitalizationDateTo = new FormDate();
      if (Field24_ServiceLines == null) Field24_ServiceLines = new List<HCFA1500ServiceLine>();
    }

    // Fields in the HCFA1500 object model are defined in the order they appear on the HCFA1500 form.

    public bool Field01_TypeOfCoverageIsMedicare {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsMedicaid {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsTricareChampus {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsChampVa {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsGroupHealthPlan {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsFECABlkLung {
      get;
      set;
    }
    public bool Field01_TypeOfCoverageIsOther {
      get;
      set;
    }
    public string Field01a_InsuredsIDNumber  {
      get;
      set;
    }
    public string Field02_PatientsName   {
      get;  // HCFA 1500 standard allows 29 total characters
      set;
    }
    public FormDate Field03_PatientsDateOfBirth   {
      get;  // MMDDCCYY - 8 characters
      set;
    }
    public bool Field03_PatientsSexMale  {
      get;
      set;
    }
    public bool Field03_PatientsSexFemale {
      get;
      set;
    }
    public string Field04_InsuredsName  {
      get;  // HCFA 1500 standard allows 29 total characters for these (3) fields
      set;
    }
    public string Field05_PatientsAddress_Street  {
      get;  // 28 characters
      set;
    }
    public string Field05_PatientsAddress_City  {
      get;  // 24 characters
      set;
    }
    public string Field05_PatientsAddress_State {
      get;  // 3 characters
      set;
    }
    public string Field05_PatientsAddress_Zip {
      get;  // 12 characters
      set;
    }
    public string Field05_PatientsTelephone {
      get;  // 10 digits
      set;
    }
    public bool Field06_PatientRelationshipToInsuredIsSelf {
      get;
      set;
    }
    public bool Field06_PatientRelationshipToInsuredIsSpouseOf {
      get;
      set;
    }
    public bool Field06_PatientRelationshipToInsuredIsChildOf {
      get;
      set;
    }
    public bool Field06_PatientRelationshipToInsuredIsOther {
      get;
      set;
    }
    public string Field07_InsuredsAddress_Street {
      get;  // 29 characters (yes, '29' not 28)
      set;
    }
    public string Field07_InsuredsAddress_City {
      get;  // 23 characters (yes, '23' not 24)
      set;
    }
    public string Field07_InsuredsAddress_State {
      get;  // 4 characters (yes, '4' not 3)
      set;
    }
    public string Field07_InsuredsAddress_Zip {
      get;  // 12 characters
      set;
    }
    public string Field07_InsuredsAreaCode {
      get;  // 3 digits
      set;
    }
    public string Field07_InsuredsPhoneNumber {
      get;  // 10 digits
      set;
    }
    public bool Field08_PatientStatusIsSingle {
      get;
      set;
    }
    public bool Field08_PatientStatusIsMarried {
      get;
      set;
    }
    public bool Field08_PatientStatusIsOther {
      get;
      set;
    }
    public bool Field08_PatientStatusIsEmployed {
      get;
      set;
    }
    public bool Field08_PatientStatusIsFullTimeStudent {
      get;
      set;
    }
    public bool Field08_PatientStatusIsPartTimeStudent {
      get;
      set;
    }
    public string Field09_OtherInsuredsName {
      get;  // HCFA 1500 standard allows 28 total characters
      set;
    }
    public string Field09a_OtherInsuredsPolicyOrGroup {
      get;  // 28 characters
      set;
    }
    public FormDate Field09b_OtherInsuredsDateOfBirth {
      get;  // MMDDCCYY - 8 characters, goes to DMG02 (page 151) from X12 spec.
      set;
    }
    public bool Field09b_OtherInsuredIsMale {
      get;  // 1 = Male, 2 = Female;  1 character.
      set;
    }
    public bool Field09b_OtherInsuredIsFemale {
      get;
      set;
    }
    public string Field09c_OtherInsuredsEmployerNameOrSchoolName {
      get;  // 28 characters
      set;
    }
    public string Field09d_OtherInsuredsInsurancePlanNameOrProgramName  {
      get;  // 28 characters
      set;
    }
    public bool Field10a_PatientConditionRelatedToEmployment {
      get;  // 1 = Yes, 2 = No
      set;
    }
    public bool Field10b_PatientConditionRelatedToAutoAccident {
      get;
      set;
    }
    public string Field10b_PatientConditionRelToAutoAccidentState {
      get;  // 2 characters// 1 = Yes, 2 = No
      set;
    }
    public bool Field10c_PatientConditionRelatedToOtherAccident {
      get;  // 1 = Yes, 2 = No
      set;
    }
    public string Field10d_ReservedForLocalUse {
      get;  // 19 characters
      set;
    }
    public string Field11_InsuredsPolicyGroupOfFECANumber {
      get;  // 29 characters
      set;
    }
    public FormDate Field11a_InsuredsDateOfBirth {
      get;  // MMDDCCYY - 8 characters
      set;
    }
    public bool Field11a_InsuredsSexIsMale {
      get;  // 1 = Male, 2 = Female;  1 character.
      set;
    }
    public bool Field11a_InsuredsSexIsFemale {
      get;
      set;
    }
    public string Field11b_InsuredsEmployerOrSchool {
      get;  // 29 characters
      set;
    }
    public string Field11c_InsuredsPlanOrProgramName {
      get;  // 29 characters
      set;
    }
    public bool Field11d_IsThereOtherHealthBenefitPlan {
      get;  // 1 = Yes, 2 = No
      set;
    }
    public string Field12_PatientsOrAuthorizedSignature {
      get;  // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
      set;
    }
    public FormDate Field12_PatientsOrAuthorizedSignatureDate {
      get;  // MMDDCCYY
      set;
    }
    public string Field13_InsuredsOrAuthorizedSignature {
      get;  // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
      set;
    }
    public FormDate Field14_DateOfCurrentIllnessInjuryOrPregnancy {
      get;  // MMDDCCYY
      set;
    }
    public FormDate Field15_DatePatientHadSameOrSimilarIllness {
      get;  // MMDDCCYY
      set;
    }
    public FormDate Field16_DatePatientUnableToWork_Start {
      get;  // MMDDCCYY
      set;
    }
    public FormDate Field16_DatePatientUnableToWork_End {
      get;  // MMDDCCYY
      set;
    }
    public string Field17_ReferringProviderOrOtherSource_Name {
      get;  // HCFA 1500 standard allows 28 total characters for this field
      set;
    }
    public string Field17a_OtherID_Qualifier {
      get;  // 2 digit alpha-numeric value
      set;
    }
    public string Field17a_OtherID_Number {
      get;  // 17 characters
      set;
    }
    public string Field17b_NationalProviderIdentifier {
      get;  // 10 digit numeric
      set;
    }
    public FormDate Field18_HospitalizationDateFrom {
      get;  // MMDDCCYY
      set;
    }
    public FormDate Field18_HospitalizationDateTo {
      get;  // MMDDCCYY
      set;
    }
    public string Field19_ReservedForLocalUse {
      get;  // 83 characters
      set;
    }
    public bool Field20_OutsideLab {
      get;  // 1 = Yes, 2 = No
      set;
    }
    public decimal? Field20_OutsideLabCharges {
      get;  // 8 digit numeric with implied decimal.  ie '20300' is $203.00.                        // 3-1-4 part diagnosis code.
      set;
    }
    public string Field21_Diagnosis1 {
      get;
      set;
    }
    public string Field21_Diagnosis2 {
      get;
      set;
    }
    public string Field21_Diagnosis3 {
      get;
      set;
    }
    public string Field21_Diagnosis4 {
      get;
      set;
    }
    public string Field22_MedicaidSubmissionCode {
      get;  // 11 characters
      set;
    }
    public string Field22_OriginalReferenceNumber {
      get;  // 18 characters
      set;
    }
    public string Field23_PriorAuthorizationNumber {
      get;  // 29 characters                         // Service line details
      set;
    }
    [XmlElement(ElementName="Field24_ServiceLine")]
    public List<HCFA1500ServiceLine> Field24_ServiceLines {
      get;
      set;
    }
    public string Field25_FederalTaxIDNumber {
      get;  // 15 characters
      set;
    }
    public bool Field25_IsEIN {
      get;  // 1 = SSN, 2 = EIN
      set;
    }
    public bool Field25_IsSSN {
      get;
      set;
    }
    public string Field26_PatientAccountNumber {
      get;  // 14 characters
      set;
    }
    public bool? Field27_AcceptAssignment {
      get;  // 1 = Yes, 2 = No.  Refers to acceptance of terms of payor's program.
      set;
    }
    public decimal Field28_TotalCharge {
      get;  // 7 digits
      set;
    }
    public decimal? Field29_AmountPaid {
      get;  // 6 digits                               // 2 digits
      set;
    }
    public decimal? Field30_BalanceDue {
      get;  // 6 digits                               // 2 digits
      set;
    }
    public bool? Field31_PhysicianOrSupplierSignatureIsOnFile {
      get;  // Signed field.  Store true = Signature On File, false = Signature NOT On File.  If SOF, enter date in next field
      set;
    }
    public string Field32_ServiceFacilityLocation_Name {
      get;  // 26 characters
      set;
    }
    public string Field32_ServiceFacilityLocation_Street {
      get;  // 26 characters
      set;
    }
    public string Field32_ServiceFacilityLocation_City {
      get;  // 26 characters for this and next two fields combined
      set;
    }
    public string Field32_ServiceFacilityLocation_State {
      get;  //
      set;
    }
    public string Field32_ServiceFacilityLocation_Zip {
      get;  //
      set;
    }
    public string Field32a_ServiceFacilityLocation_Npi {
      get;  // 10 characters
      set;
    }
    public string Field32b_ServiceFacilityLocation_OtherID {
      get;  // 14 characters
      set;
    }
    public string Field33_BillingProvider_TelephoneNumber {
      get;  // 9 characters
      set;
    }
    public string Field33_BillingProvider_Name {
      get;  // 29 characters
      set;
    }
    public string Field33_BillingProvider_Street {
      get;  // 29 characters
      set;
    }
    public string Field33_BillingProvider_City {
      get;  // 29 characters for this and next two fields combined
      set;
    }
    public string Field33_BillingProvider_State {
      get;  //
      set;
    }
    public string Field33_BillingProvider_Zip {
      get;  //
      set;
    }
    public string Field33a_BillingProvider_Npi {
      get;  // 10 characters
      set;
    }
    public string Field33b_BillingProvider_OtherID {
      get;  // 17 characters
      set;
    }
    #region Serialization Methods

    public string Serialize()
    {
      var writer = new StringWriter();
      new XmlSerializer(typeof(HCFA1500Claim)).Serialize(writer, this);
      return writer.ToString();
    }

    public static HCFA1500Claim Deserialize(string xml)
    {
      var serializer = new XmlSerializer(typeof(HCFA1500Claim));
      return (HCFA1500Claim)serializer.Deserialize(new StringReader(xml));
    }
    #endregion
  }
}
