using System;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
  public class HCFA1500ServiceLine
  {
    public string CommentLine {
      get;  // 61 characters (in grey area from boxes 24A through 24G
      set;
    }
    public FormDate DateFrom {
      get;  // MMDDCCYY
      set;
    }
    public FormDate DateTo {
      get;  // MMDDCCYY
      set;
    }
    [XmlAttribute]
    public string PlaceOfService {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string EmergencyIndicator {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string ProcedureCode {
      get;  // 6 digits
      set;
    }
    [XmlAttribute]
    public string Mod1 {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string Mod2 {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string Mod3 {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string Mod4 {
      get;  // 2 digits
      set;
    }
    [XmlAttribute]
    public string DiagnosisPointer1 {
      get;
      set;
    }
    [XmlAttribute]
    public string DiagnosisPointer2 {
      get;
      set;
    }
    [XmlAttribute]
    public string DiagnosisPointer3 {
      get;
      set;
    }
    [XmlAttribute]
    public string DiagnosisPointer4 {
      get;
      set;
    }
    public decimal? Charges {
      get;
      set;
    }
    public decimal? DaysOrUnits {
      get;  // 3 characters
      set;
    }
    [XmlAttribute]
    public string EarlyPeriodicScreeningDiagnosisAndTreatment {
      get;  // 2 characters
      set;
    }
    [XmlAttribute]
    public string RenderingProviderIdQualifier {
      get;
      set;
    }
    [XmlAttribute]
    public string RenderingProviderId {
      get;  // 11 characters
      set;
    }
    [XmlAttribute]
    public string RenderingProviderNpi {
      get;  // 10 characters
      set;
    }
  }

}
