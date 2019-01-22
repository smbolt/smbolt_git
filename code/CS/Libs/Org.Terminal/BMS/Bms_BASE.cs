using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public class Bms_BASE
  {
    protected BmsStatement BmsStatement;
    public BmsStatementType BmsStatementType { get { return Get_StatementType(); } }

    public Bms_BASE(BmsStatement bmsStatement)
    {
      this.BmsStatement = bmsStatement;
    }

    public BmsStatementType Get_StatementType()
    {
      if (this.BmsStatement == null)
        return BmsStatementType.Unidentified;

      return this.BmsStatement.BmsStatementType;
    }
  }
}
