using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business.TextProcessing
{
  public class StatementData
  {
    public int StatementDataId { get; set; }
    public int StatementFileId { get; set; }
    public string DataName { get; set; }
    public string DataValue { get; set; }
    public string DBDataType { get; set; }

    public StatementData(string dataName, string dataValue, string dbDataType)
    {
      this.DataName = dataName;
      this.DataValue = dataValue;
      this.DBDataType = dbDataType;
    }
  }
}
