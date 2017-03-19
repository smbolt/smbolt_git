using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public class CmdxFactory
  {
    public Cmdx CreateCmdx(Cmd cmd)
    {
      if (cmd == null)
        throw new CxException(3);

      switch(cmd.Verb.ToLower())
      {
        case "addexporttemplate": return new AddExportTemplate(cmd);
        case "settextstart": return new SetTextPosition(cmd);
        case "settextend": return new SetTextPosition(cmd);
        case "locatetoken": return new LocateToken(cmd);
        case "extractnexttoken": return new ExtractNextToken(cmd);
        case "extractnexttokens": return new ExtractNextTokens(cmd);
        case "extracttextbefore": return new ExtractTextBefore(cmd);
        case "tokenizenextline": return new TokenizeNextLine(cmd);
        case "removetokens": return new RemoveTokens(cmd);
        case "extractstoredtoken": return new ExtractStoredToken(cmd);
        case "extractstoredtokens": return new ExtractStoredTokens(cmd);
        case "extractnextline": return new ExtractNextLine(cmd);
        case "truncate": return new Truncate(cmd);
        case "setvariable": return new SetVariable(cmd); 
        case "createelement": return new CreateElement(cmd); 

      }

      throw new CxException(4, new object[] { cmd });
    }
  }
}
