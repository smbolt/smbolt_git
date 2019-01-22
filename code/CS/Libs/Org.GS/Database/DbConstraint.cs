using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Database
{
  public class DbConstraint
  {
    public string ConstraintCatalog { get; set; }
    public string ConstraintSchema { get; set; }
    public string ConstraintName { get; set; }
    public string TableCatalog { get; set; }
    public string TableSchema { get; set; }
    public string TableName { get; set; }
    public ConstraintType ConstraintType { get; set; }
    public string ColumnName { get; set; }
    public int OrdinalPosition { get; set; }

    public DbConstraint()
    {
      this.ConstraintCatalog = String.Empty;
      this.ConstraintSchema = String.Empty;
      this.ColumnName = String.Empty;
      this.TableCatalog = String.Empty;
      this.TableSchema = String.Empty;
      this.TableName = String.Empty;
      this.ConstraintType = ConstraintType.NotSet;
      this.ColumnName = String.Empty;
      this.OrdinalPosition = 0;
    }
  }
}
