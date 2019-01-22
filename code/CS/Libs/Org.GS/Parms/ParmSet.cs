using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.GS
{
  [Serializable]
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

    public void AddParm(string parmName, string parmValue)
    {
      if (parmName.IsBlank())
        throw new Exception("The parmName parameter value cannot be blank or null.");

      this.AddParm(parmName, (object)parmValue); 
    }

    public void AddParm(string parmName, object parmValue)
    {
      if (parmName.IsBlank())
        throw new Exception("The parmName parameter value cannot be blank or null.");

      if (this.Where(p => p.ParameterName == parmName).Count() > 0)
        throw new Exception("The parmName '" + parmName + "' already exists in the collection.");

      if (parmValue.GetType().Name == "String")
        this.Add(new Parm(parmName, parmValue.ToString()));
      else
        this.Add(new Parm(parmName, parmValue));
    }

    public void ProcessOverrides(Dictionary<string, string> overrideParms)
    {
      if (overrideParms == null || overrideParms.Count == 0)
        return;

      foreach (var kvpParm in overrideParms)
      {
        string parmName = kvpParm.Key;
        string parmValue = kvpParm.Value;

        if (this.ParmExists(parmName))
        {
          var parm = this.Where(p => p.ParameterName == parmName).FirstOrDefault();
          if (parm != null)
          {
            parm.ParameterValue = parmValue;
          }
        }
        else
        {
          this.AddParm(parmName, parmValue);
        }
      }
    }

    public void SetParmValue(string parmName, object parmValue)
    {
      if (parmName.IsBlank())
        return; 

      var parm = this.Where(p => p.ParameterName == parmName).FirstOrDefault();

      if (parm != null)
      {
        parm.ParameterValue = parmValue;
      }
      else
      {
        this.AddParm(parmName, parmValue); 
      }
    }

    public bool ContainsParmName(string parmName)
    {
      return this.Where(p => p.ParameterName == parmName).Count() > 0;
    }

    public object GetParmValue(string parmName, string defaultValue = "")
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

      if (defaultValue.IsNotBlank())
        return defaultValue;

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
                  ParmValueReport(parm.ParameterValue) + g.crlf); 
      }

      sb.Append("TOTAL PARM CONT: " + totalParms.ToString() + g.crlf);

      string report = sb.ToString();
      return report;
    }

    private string ParmValueReport(object parmValue)
    {
      try
      {
        if (parmValue == null)
          return "NULL";

        if (parmValue.ToString().IsBlank())
          return "[BLANK]";

        string parmType = parmValue.GetType().Name;

        switch (parmType)
        {
          case "ConfigDbSpec":
            var dbSpec = (ConfigDbSpec)parmValue;
            return dbSpec.ConnectionString;

          case "ConfigWsSpec":
            var wsSpec = (ConfigWsSpec)parmValue;
            return wsSpec.WebServiceEndpoint;
      }

        return parmValue.ToString();
      }
      catch (Exception ex)
      {
        return "EXCEPTION THROWN: " + ex.ToReport();
      }
    }
  }
}
