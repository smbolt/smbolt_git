using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class ResumeModel : ApiModelBase
  {
    public int ResumeId { get; set; }
    public string ResumeName { get; set; }
    public int ResumeStatusId { get; set; }
    public string ResumeStatus { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string DateCreated { get { return this.CreatedDateTime.ToString("MM/dd/yyyy"); } }
    public int CreatedAccountId;
    public DateTime? ModifiedDateTime { get; set; }
    public string DateUpdated { get { return (this.ModifiedDateTime.HasValue ? this.ModifiedDateTime.Value.ToString("MM/dd/yyyy") : ""); } }
    public int? ModifiedAccountId { get; set; }
    public string DefaultNextAction { get; set; }
    public List<string> NextActionList { get; set; }
    public string LastAction { get; set; }

    public ResumeModel()
    {
      this.ResumeId = -1;
      this.ResumeName = String.Empty;
      this.ResumeStatusId = -1;
      this.ResumeStatus = String.Empty;
      this.CreatedDateTime = DateTime.MinValue;
      this.CreatedAccountId = -1;
      this.ModifiedDateTime = null;
      this.ModifiedAccountId = null;
      this.DefaultNextAction = String.Empty;
      this.NextActionList = new List<string>();
      this.LastAction = String.Empty;
    }
  }
}

