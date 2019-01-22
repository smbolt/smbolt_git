using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.Dx.Business.TextProcessing;

namespace Org.Dx.Business
{
  [XMap(XType = XType.Element, CollectionElements="EntityMap")]
  public class ColumnIndexMap : Dictionary<string, EntityMap>
  {
    public int IndexOf(string key)
    {
      if (key == null || key.IsBlank())
        throw new CxException(98, new object[] { key } );

      string[] tokens = key.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 2)
        throw new CxException(99, new object[] { key });

      string entityName = tokens[0];
      string columnName = tokens[1];

      if (!this.ContainsKey(entityName))
        throw new CxException(100, new object[] { key });

      var entity = this[entityName];

      if (!entity.ContainsKey(columnName))
        throw new CxException(101, new object[] { key, columnName });

      var column = entity[columnName];

      return column.Index;
    }

    public bool EntryExists(string key)
    {
      if (key == null || key.IsBlank())
        return false;

      string[] tokens = key.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 2)
        return false;

      string entityName = tokens[0];
      string columnName = tokens[1];

      if (!this.ContainsKey(entityName))
        return false;

      var entity = this[entityName];

      if (!entity.ContainsKey(columnName))
        return false;

      return true;
    }

    public ColumnMap GetColumnMap(string key)
    {
      if (key == null || key.IsBlank())
        throw new CxException(98, new object[] { key });

      string[] tokens = key.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 2)
        throw new CxException(99, new object[] { key });

      string entityName = tokens[0];
      string columnName = tokens[1];

      if (!this.ContainsKey(entityName))
        throw new CxException(100, new object[] { key });

      var entity = this[entityName];

      if (!entity.ContainsKey(columnName))
        throw new CxException(101, new object[] { key });

      var column = entity[columnName];

      return column;
    }

  }
}
