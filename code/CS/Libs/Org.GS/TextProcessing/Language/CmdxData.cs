using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public class CmdxData
  {
    public string RawText { get; set; }
    public string TextToFind { get; set; }
    public int StartPos { get; set; }
    public bool ExcludeLastToken { get; set; }
    public bool PositionAtEndOfToken { get; set; }
    public bool IsRequired { get; set; }
    public bool NumericOnly { get; set; }
    public Cmdx OriginalCmdx { get; set; }

    public CmdxData()
    {
      this.RawText = String.Empty;
      this.TextToFind = String.Empty;
      this.StartPos = 0;
      this.ExcludeLastToken = false;
      this.PositionAtEndOfToken = false;
      this.IsRequired = true;
      this.NumericOnly = false;
      this.OriginalCmdx = null;
    }
  }
}
