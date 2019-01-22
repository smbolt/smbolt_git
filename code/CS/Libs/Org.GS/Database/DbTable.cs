using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Database
{
  public class DbTable
  {
    public string DatabaseName {
      get;
      set;
    }
    public string SchemaName {
      get;
      set;
    }
    public string TableName {
      get;
      set;
    }
    public string TableNameSansSchema {
      get {
        return Get_TableNameSansSchema();
      }
    }
    public string TableType {
      get;
      set;
    }
    public DateTime CreateDT {
      get;
      set;
    }
    public DateTime ModifyDT {
      get;
      set;
    }

    public DbColumnSet DbColumnSet {
      get;
      set;
    }
    public DbConstraintSet DbConstraintSet {
      get;
      set;
    }
    public DbColumn PrimaryKeyColumn {
      get;
      set;
    }
    public DbColumn IdentityColumn {
      get;
      set;
    }

    public bool HasPrimaryKeyColumn
    {
      get {
        return this.PrimaryKeyColumn == null ? false : true;
      }
    }

    public bool HasIdentityColumn
    {
      get {
        return this.IdentityColumn == null ? false : true;
      }
    }

    public DbTable()
    {
      this.DatabaseName = String.Empty;
      this.SchemaName = String.Empty;
      this.TableName = String.Empty;
      this.TableType = String.Empty;
      this.CreateDT = DateTime.MinValue;
      this.ModifyDT = DateTime.MinValue;

      this.DbColumnSet = new DbColumnSet();
      this.DbConstraintSet = new DbConstraintSet();

      this.PrimaryKeyColumn = null;
      this.IdentityColumn = null;
    }

    public void Load(SqlConnection Connection, DbTypeSet dbTypeSet)
    {
      this.DbColumnSet = new DbColumnSet();

      string sql = "SELECT " +
                   "  c.[name] AS [Name], " +
                   "  c.[column_id] AS [ColumnId], " +
                   "  c.[system_type_id] AS [SystemTypeId], " +
                   "  c.[user_type_id] AS [UserTypeId], " +
                   "  c.[max_length] AS [MaxLength], " +
                   "  c.[precision] AS [Precision], " +
                   "  c.[scale] AS [Scale], " +
                   "  c.[is_nullable] AS [IsNullable], " +
                   "  c.[is_identity] AS [IsIdentity], " +
                   "  i.[is_primary_key] AS [IsPrimaryKey], " +
                   "  c.[default_object_id] as [DefaultObjectId] " +
                   "FROM sys.columns c " +
                   "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " +
                   "LEFT OUTER JOIN sys.indexes i on ic.object_id = i.object_id AND ic.index_id = i.index_id " +
                   "WHERE c.object_id = OBJECT_ID('" + this.TableName + "') " +
                   "ORDER BY c.column_id ";


      SqlDataAdapter da = new SqlDataAdapter(sql, Connection);
      DataSet ds = new DataSet();
      da.Fill(ds);

      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow r = ds.Tables[0].Rows[i];

        DbColumn c = new DbColumn();
        c.Table = this;

        if (!r.IsNull("Name"))
          c.Name = Convert.ToString(r["Name"]);

        if (!r.IsNull("ColumnId"))
          c.ColumnId = Convert.ToInt32(r["ColumnId"]);

        if (!r.IsNull("SystemTypeId"))
          c.SystemTypeId = Convert.ToInt32(r["SystemTypeId"]);

        if (!r.IsNull("UserTypeId"))
          c.UserTypeId = Convert.ToInt32(r["UserTypeId"]);

        if (!r.IsNull("MaxLength"))
          c.MaxLength = Convert.ToInt32(r["MaxLength"]);

        if (!r.IsNull("Precision"))
          c.Precision = Convert.ToInt32(r["Precision"]);

        if (!r.IsNull("Scale"))
          c.Scale = Convert.ToInt32(r["Scale"]);

        if (!r.IsNull("IsNullable"))
          c.IsNullable = Convert.ToBoolean(r["IsNullable"]);

        if (!r.IsNull("IsIdentity"))
          c.IsIdentity = Convert.ToBoolean(r["IsIdentity"]);

        if (!r.IsNull("IsPrimaryKey"))
        {
          int pk = Convert.ToInt32(r["IsPrimaryKey"]);
          c.IsPrimaryKey = pk == 1;
        }

        int defaultObjectId = 0;
        if (!r.IsNull("DefaultObjectId"))
          defaultObjectId = Convert.ToInt32(r["DefaultObjectId"]);

        if (defaultObjectId > 0)
          c.HasDefaultValue = true;

        if (dbTypeSet.ContainsKey(c.SystemTypeId))
          c.DbType = dbTypeSet[c.SystemTypeId];
        else
          c.DbType = dbTypeSet[9999];


        if (this.DbColumnSet.ContainsKey(c.Name))
          continue;

        this.DbColumnSet.Add(c.Name, c);
      }


      foreach (DbColumn c in this.DbColumnSet.Values)
      {
        if (c.IsIdentity)
        {
          this.IdentityColumn = c;
          break;
        }
      }
    }

    private string Get_TableNameSansSchema()
    {
      if (this.SchemaName.IsBlank())
        return this.TableName;

      return this.TableName.Replace(this.SchemaName + ".", String.Empty);
    }
  }
}
