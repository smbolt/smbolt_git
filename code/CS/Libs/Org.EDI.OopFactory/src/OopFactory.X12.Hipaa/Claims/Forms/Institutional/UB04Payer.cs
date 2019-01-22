using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
  public class UB04Payer
  {
    public string Field50_PayerName {
      get;  // Destination Payer (2010BB/2310)
      set;
    }
    public string Field51_HealthPlanId {
      get;  // Destination payer (2010BB/2310)
      set;
    }
    public string Field52_ReleaseOfInfoCertIndicator {
      get;  // Destination payer (2300)
      set;
    }
    public string Field53_AssignmentOfBenefitsCertIndicator {
      get;  // Destination payer (2300)
      set;
    }
    public decimal? Field54_PriorPayments {
      get;  // Destination payer (2320)
      set;
    }
    public decimal? Field55_EstimatedAmountDue {
      get;
      set;
    }
    public string Field58_InsuredsName {
      get;
      set;
    }
    public string Field59_PatientRelationship {
      get;
      set;
    }
    public string Field60_InsuredsUniqueId {
      get;
      set;
    }
    public string Field61_GroupName {
      get;
      set;
    }
    public string Field62_InsuredsGroupNumber {
      get;
      set;
    }
    public string Field65_EmployerName {
      get;
      set;
    }

  }
}
