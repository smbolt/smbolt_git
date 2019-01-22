using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Finance.Business
{
  public static class ExtensionsMethods
  {
    public static string ToAbbrev(this DebitCredit debitCredit)
    {
      if (debitCredit == DebitCredit.Credit)
        return "CR";

      return "DR";
    }
  }
}
