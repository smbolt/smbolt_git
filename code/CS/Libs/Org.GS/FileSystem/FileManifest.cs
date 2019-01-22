using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [XMap (XType = XType.Element)]
  public class FileManifest
  {
    [XMap] public string FileName { get; set; }
    [XMap] public ProcessingStatus ProcessingStatus { get; set; }
    [XMap] public string OperatorName { get; set; }
    [XMap] public string MapName { get; set; }
    [XMap] public DateTime? DateTimeProcessed { get; set; }

    public int? OperatorId { get; set; }
    public int? StatementTypeBusEntityId { get; set; }
    public string FullFilePath { get; set; }

    public FileManifest()
    {
      this.FileName = String.Empty;
      this.ProcessingStatus = ProcessingStatus.Hold;
      this.OperatorName = String.Empty;
      this.MapName = String.Empty;
      this.DateTimeProcessed = null;
      this.FullFilePath = null;
    }
  }
}
