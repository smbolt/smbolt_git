using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business.TextProcessing
{
  public class StatementFile
  {
    public int StatementFileId { get; set; }
    public int StatementTypeId { get; set; }
    public DateTime ExtractDateTime { get; set; }
    public string FullFilePath { get; set; }

    public List<StatementData> StatementDataSet { get; set; }

    public StatementFile()
    {
      this.StatementDataSet = new List<StatementData>();
    }
  }
}
