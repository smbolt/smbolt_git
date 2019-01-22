using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.Database
{
  public class DbColumn
  {
    public DbTable Table {
      get;
      set;
    }
    public DbEntityBase Row {
      get;
      set;
    }
    public string Name {
      get;
      set;
    }
    public DbType DbType {
      get;
      set;
    }
    public int ColumnId {
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
    public bool IsNullInDB {
      get;
      set;
    }
    public bool IsIdentity {
      get;
      set;
    }
    public bool IsPrimaryKey {
      get;
      set;
    }
    public bool IsSequencer {
      get;
      set;
    }
    public bool HasDefaultValue {
      get;
      set;
    }

    public bool IsUpdated
    {
      get {
        return this.IsColumnUpdated();
      }
    }


    public DbColumn()
    {
      this.Table = null;
      this.Name = String.Empty;
      this.DbType = new DbType();
      this.ColumnId = 0;
      this.SystemTypeId = 0;
      this.UserTypeId = 0;
      this.MaxLength = 0;
      this.Precision = 0;
      this.Scale = 0;
      this.IsNullable = true;
      this.IsNullInDB = false;
      this.IsIdentity = false;
      this.IsPrimaryKey = false;
      this.IsSequencer = false;
      this.HasDefaultValue = false;
    }

    private bool IsColumnUpdated()
    {
      PropertyInfo pi = this.Row.GetType().GetProperty(this.Name);
      if (pi == null)
        throw new Exception("Cannot locate PropertyInfo via Reflection for column name '" + this.Name + "' in table '" + this.Table.TableName + "'.");

      string propertyType = pi.PropertyType.Name;
      string propertyName = pi.Name;
      string origFieldName = "__" + propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
      FieldInfo ofi = this.Row.GetType().GetField(origFieldName, BindingFlags.NonPublic | BindingFlags.Instance);

      object currentValue = pi.GetValue(this.Row, null);
      object originalValue = ofi.GetValue(this.Row);

      if (pi.PropertyType.IsGenericType)
      {
        Type underlyingType = Nullable.GetUnderlyingType(pi.PropertyType);
        propertyType = underlyingType.Name;
      }

      if (DbUtility.ValueIsUpdated(propertyName, propertyType, originalValue, currentValue))
        return true;

      return false;
    }
  }
}
