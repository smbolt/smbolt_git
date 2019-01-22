using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Org.GS;

namespace Org.GS.Database
{
  public class DbType
  {
    public SqlDbType SqlDbType {
      get;
      set;
    }
    public string TypeName {
      get;
      set;
    }
    public int SystemTypeId {
      get;
      set;
    }
    public int UserTypeId {
      get;
      set;
    }
    public int MaxLength {
      get;
      set;
    }
    public int Precision {
      get;
      set;
    }
    public int Scale {
      get;
      set;
    }
    public bool IsNullable {
      get;
      set;
    }

    public DbType()
    {
      SqlDbType = SqlDbType.Text;
      TypeName = String.Empty;
      SystemTypeId = 0;
      UserTypeId = 0;
      MaxLength = 0;
      Precision = 0;
      Scale = 0;
      IsNullable = true;
    }
  }
}
