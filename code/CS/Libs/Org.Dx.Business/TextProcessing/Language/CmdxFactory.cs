using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class CmdxFactory
  {
    public Cmdx CreateCmdx(Cmd cmd)
    {
      bool breakOnThisCmdx = false;

      if (cmd == null)
        throw new CxException(3);

      string verb = cmd.Verb.ToLower().Trim();

      if (verb.StartsWith("*") || verb.StartsWith("#"))
        verb = verb.Substring(1);

      if (verb.StartsWith("~"))
      {
        breakOnThisCmdx = true;
        verb = verb.Substring(1);
      }

      Cmdx cmdx = null;

      switch(verb)
      {
        case "setxml":
          cmdx = new SetXml(cmd);
          break;
        case "setvariablefromxmlelementvalue":
          cmdx = new SetXml(cmd);
          break;
        case "extractxmlelementvalue":
          cmdx = new ExtractXmlElementValue(cmd);
          break;
        case "setrowindex":
          cmdx = new SetRowIndex(cmd);
          break;
        case "settextposition":
          cmdx = new SetTextPosition(cmd);
          break;
        case "settextstart":
          cmdx = new SetTextPosition(cmd);
          break;
        case "settextend":
          cmdx = new SetTextPosition(cmd);
          break;
        case "settsdcondition":
          cmdx = new SetTsdCondition(cmd);
          break;
        case "locatetoken":
          cmdx = new LocateToken(cmd);
          break;
        case "extracttoken":
          cmdx = new ExtractToken(cmd);
          break;
        case "extractliteraltoken":
          cmdx = new ExtractLiteralToken(cmd);
          break;
        case "extractnexttokenoftype":
          cmdx = new ExtractNextTokenOfType(cmd);
          break;
        case "extractpriortokenoftype":
          cmdx = new ExtractPriorTokenOfType(cmd);
          break;
        case "extractpriortokensoftype":
          cmdx = new ExtractPriorTokensOfType(cmd);
          break;
        case "extractnexttoken":
          cmdx = new ExtractNextToken(cmd);
          break;
        case "extractnexttokens":
          cmdx = new ExtractNextTokens(cmd);
          break;
        case "extracttextbefore":
          cmdx = new ExtractTextBefore(cmd);
          break;
        case "extractfrompeercell":
          cmdx = new ExtractFromPeerCell(cmd);
          break;
        case "tokenizenextline":
          cmdx = new TokenizeNextLine(cmd);
          break;
        case "removestoredtokens":
          cmdx = new RemoveStoredTokens(cmd);
          break;
        case "extractstoredtoken":
          cmdx = new ExtractStoredToken(cmd);
          break;
        case "extractstoredtokens":
          cmdx = new ExtractStoredTokens(cmd);
          break;
        case "extractstoredtokenbefore":
          cmdx = new ExtractStoredTokenBefore(cmd);
          break;
        case "extractnextline":
          cmdx = new ExtractNextLine(cmd);
          break;
        case "truncate":
          cmdx = new Truncate(cmd);
          break;
        case "setvariable":
          cmdx = new SetVariable(cmd);
          break;
        case "setglobalvariable":
          cmdx = new SetGlobalVariable(cmd);
          break;
        case "processingcommand":
          cmdx = new ProcessingCommand(cmd);
          break;
        case "replacetext":
          cmdx = new ReplaceText(cmd);
          break;

        default:
          throw new CxException(4, new object[] { cmd });
      }


      if (cmd.Break)
        cmdx.Break = cmd.Break;

      return cmdx;

    }
  }
}
