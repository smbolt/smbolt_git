using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class BmsLine
  {
    public int LineNumber {
      get;
      set;
    }
    public string LineText {
      get;
      set;
    }

    public BmsLine()
    {
      this.LineNumber = 0;
      this.LineText = String.Empty;
    }

    public override string ToString()
    {
      return this.LineNumber.ToString("0000") + " " + this.LineText;
    }

    public BmsLine CloneForVFLEX(int lineNumber)
    {
      var clone = new BmsLine();
      clone.LineNumber = this.LineNumber;
      clone.LineText = this.LineText;

      if (clone.LineText.StartsWith("@"))
      {
        string nameToken = clone.LineText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        if (nameToken != null)
        {
          string nameTokenBase = nameToken.Substring(1);
          string nameTokenBaseType = nameTokenBase.Substring(0, 2);
          string nameTokenNumber = nameTokenBase.Substring(2);
          if (nameTokenNumber.IsNotInteger())
            throw new Exception("The VFLEX line number part of the field name is not numeric - name is '" + nameToken + "'. Error creating clone of the BmsLine '" +
                                this.LineNumber.ToString() + "  " + this.LineText + "'.");
          string newNameToken = "@" + nameTokenBaseType + lineNumber.ToString();
          while (nameToken.Length < newNameToken.Length)
          {
            nameToken = nameToken + " ";
          }

          clone.LineText = clone.LineText.Replace(nameToken, newNameToken);
        }
      }

      return clone;
    }
  }
}
