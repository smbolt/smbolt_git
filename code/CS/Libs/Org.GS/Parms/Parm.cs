using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Parm
  {
    public int ParameterId { get; set; }
    public int? ScheduledTaskId { get; set; }
    public string ParameterSetName { get; set; }
    public string ParameterName { get; set; }
    public object ParameterValue { get; set; }
    public Type ParameterType { get; set; }
    public bool RemoveThisParm { get; set; }

    public Parm()
    {
      this.ParameterId = 0;
      this.ScheduledTaskId = null;
      this.ParameterSetName = String.Empty;
      this.ParameterName = String.Empty;
      this.ParameterValue = String.Empty;
      this.ParameterType = typeof(System.String);
      this.RemoveThisParm = false; 
    }

    public Parm(string parmName, string parmValue)
    {
      this.ParameterId = 0;
      this.ScheduledTaskId = null;
      this.ParameterSetName = "Dynamic";
      this.ParameterName = parmName;
      this.ParameterValue = parmValue;
      this.ParameterType = typeof(System.String);
      this.RemoveThisParm = false; 
    }

    public Parm(string parmName, object parmValue)
    {
      this.ParameterId = 0;
      this.ScheduledTaskId = null;
      this.ParameterSetName = "Dynamic";
      this.ParameterName = parmName;
      this.ParameterValue = parmValue;
      this.ParameterType = parmValue.GetType();
      this.RemoveThisParm = false; 
    }
  }
}
