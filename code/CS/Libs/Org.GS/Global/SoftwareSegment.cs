using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class SoftwareSegment
  {
    public int SegmentNumber { get; set; }
    public int TotalSegments { get; set; }
    public int SegmentSize { get; set; }
    public int TotalFileSize { get; set; }
    public string SegmentData { get; set; }
    public int ErrorCode { get; set; }

    public SoftwareSegment()
    {
      this.SegmentNumber = 0;
      this.TotalSegments = 0;
      this.SegmentSize = 0;
      this.TotalFileSize = 0;
      this.SegmentData = String.Empty;
      this.ErrorCode = 0;
    }
  }
}
