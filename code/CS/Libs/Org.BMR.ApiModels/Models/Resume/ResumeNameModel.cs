using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class ResumeNameModel : ApiModelBase
  {
    public int ResumeId { get; set; }
    public string ResumeName { get; set; }
    public bool PersonIsInitial { get; set; }

    public ResumeNameModel()
    {
      this.ResumeId = -1;
      this.ResumeName = String.Empty;
      this.PersonIsInitial = false;
    }
  }
}
