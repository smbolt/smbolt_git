using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.TextProcessing
{
  public class TextPattern
  {
    public int OccurrenceCount { get; set; }
    public SortedList<int, Text> TextPatternItems { get; set; }

    public TextPattern()
    {
      this.TextPatternItems = new SortedList<int, Text>();
    }

    public string ToReport()
    {
      if (this.TextPatternItems == null)
        this.TextPatternItems = new SortedList<int, Text>();

      StringBuilder sb = new StringBuilder();
      sb.Append("TextPattern occurs " + this.OccurrenceCount.ToString() + " times." + g.crlf);
      sb.Append("ITEM    LINE    TEXT PATTERN" + g.crlf);
      sb.Append("----    ----    -----------------------------------------------------------------------------------" + g.crlf);

      foreach (var kvp in this.TextPatternItems)
      {
        sb.Append(kvp.Key.ToString("0000") + "    " + kvp.Value.LineNumber.ToString("0000") + "    " + kvp.Value.RawText + g.crlf);
      }

      return sb.ToString();
    }
  }
}
