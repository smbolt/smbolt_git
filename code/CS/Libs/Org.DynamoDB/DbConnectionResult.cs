using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DynamoDB
{
  public class DbConnectionResult
  {
    public DbConnectionStatus DbConnectionStatus { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }

    public DbConnectionResult()
    {
      this.DbConnectionStatus = DbConnectionStatus.NotSet;
      this.Message = String.Empty;
      this.Exception = null;
    }
  }
}
