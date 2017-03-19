using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ParmSet : List<Parm>
  {
    public string ParmsReport { get { return Get_ParmsReport(); } }

    public bool ParmExists(string parmName)
    {
      foreach (var parm in this)
      {
        if (parm.ParameterType == typeof(System.String) && parm.ParameterValue != null && parm.ParameterValue.ToString().StartsWith("ParmSet="))
          continue;

        if (parm.ParameterName == parmName)
          return true;
      }

      return false;
    }

    public object GetParmValue(string parmName)
    {
      // if we find a match that is not pointing to a "ParmSet", then we return it.
      foreach (var parm in this)
      {        
        if (parm.ParameterType == typeof(System.String) && parm.ParameterValue != null && parm.ParameterValue.ToString().StartsWith("ParmSet="))
          continue;

        if (parm.ParameterName == parmName)
          return parm.ParameterValue;
      }

      // otherwise we use any match we can find
      foreach (var parm in this)
      {        
        if (parm.ParameterName == parmName)
          return parm.ParameterValue;
      }

      return String.Empty;
    }

    public void AssertParmExistence(string parmName)
    {
      if (!this.ParmExists(parmName))
        throw new Exception("Required task parameter '" + parmName + "' not found.");
    }

    public string Get_ParmsReport()
    {
      StringBuilder sb = new StringBuilder();
      int totalParms = this.Count;
      sb.Append(" ID    TaskId  " +  ("ParameterSetName").PadTo(30) +
                ("ParameterName").PadTo(40) +
                "ParameterValue" + g.crlf);

      foreach (var parm in this)
      {
        sb.Append(parm.ParameterId.ToString("00000") + "   " +
                  (parm.ScheduledTaskId.HasValue ? parm.ScheduledTaskId.Value.ToString("0000") : "NULL") + "   " +
                  parm.ParameterSetName.PadTo(30) +
                  parm.ParameterName.PadTo(40) +
                  parm.ParameterValue.ToString() + g.crlf); 
      }

      sb.Append("TOTAL PARM CONT: " + totalParms.ToString() + g.crlf);

      string report = sb.ToString();
      return report;
    }
  }
}
