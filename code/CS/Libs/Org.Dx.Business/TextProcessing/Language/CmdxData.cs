using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class CmdxData
  {
    public string RawText { get; set; }
    public string TextToFind { get; set; }
    public int StartPos { get; set; }
    public bool ExcludeLastToken { get; set; }
    public bool PositionAtEnd { get; set; }
    public bool IsRequired { get; set; }
    public bool IsReportUnit { get; set; }
    public bool NumericOnly { get; set; }
    public Cmdx OriginalCmdx { get; set; }

    public CmdxData()
    {
      this.RawText = String.Empty;
      this.TextToFind = String.Empty;
      this.StartPos = 0;
      this.ExcludeLastToken = false;
      this.PositionAtEnd = false;
      this.IsRequired = true;
      this.IsReportUnit = false;
      this.NumericOnly = false;
      this.OriginalCmdx = null;
    }
  }
}
