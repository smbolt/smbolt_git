using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class Token
  {
    public string RawText {
      get;
      private set;
    }
    public TokenType TokenType {
      get;
      set;
    }


    public Token(string rawText)
    {
      this.RawText = rawText;
      this.TokenType = TokenType.Unprocessed;
    }
  }
}
