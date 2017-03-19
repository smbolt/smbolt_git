using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class SmtpParmsSet : List<SmtpParms>
  {
    public void AddParms(SmtpParms smtpParms)
    {
      if (this.Count == 0)
      {
        this.Add(smtpParms);
          return;
      }

      // don't add the same host twice
      foreach (SmtpParms existingParms in this)
      {
        if (existingParms.SmtpServer.Trim() == smtpParms.SmtpServer.Trim())
          return;
      }

      this.Add(smtpParms);
    }
  }
}
