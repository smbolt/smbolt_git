using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Org.GS.Database
{
  public class DbTypeSet : SortedList<int, DbType>
  {
    public void Load(SqlConnection Connection)
    {
      this.Clear();

      string sql = "SELECT " +
                    "  [name] AS [Name], " +
                    "  [system_type_id] AS [SystemTypeId], " +
                    "  [user_type_id] AS [UserTypeId], " +
                    "  [max_length] AS [MaxLength], " +
                    "  [precision] AS [Precision], " +
                    "  [scale] AS [Scale], " +
                    "  [is_nullable] AS [IsNullable] " +
                    "FROM sys.types " +
                    "ORDER BY system_type_id ";

      SqlDataAdapter da = new SqlDataAdapter(sql, Connection);
      DataSet ds = new DataSet();
      da.Fill(ds);

      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow r = ds.Tables[0].Rows[i];

        DbType t = new DbType();

        if (!r.IsNull("Name"))
          t.TypeName = Convert.ToString(r["Name"]);

        if (!r.IsNull("SystemTypeId"))
          t.SystemTypeId = Convert.ToInt32(r["SystemTypeId"]);

        if (!r.IsNull("UserTypeId"))
          t.UserTypeId = Convert.ToInt32(r["UserTypeId"]);

        if (!r.IsNull("MaxLength"))
          t.MaxLength = Convert.ToInt32(r["MaxLength"]);

        if (!r.IsNull("Precision"))
          t.Precision = Convert.ToInt32(r["Precision"]);

        if (!r.IsNull("Scale"))
          t.Scale = Convert.ToInt32(r["Scale"]);

        if (!r.IsNull("IsNullable"))
          t.IsNullable = Convert.ToBoolean(r["IsNullable"]);

        t.SqlDbType = GetSqlDbType(t.TypeName);

        if (!this.ContainsKey(t.SystemTypeId))
          this.Add(t.SystemTypeId, t);
      }

      DbType unknown = new DbType();
      unknown.TypeName = "Unknown";
      unknown.SystemTypeId = 9999;
      unknown.UserTypeId = 9999;
      unknown.SqlDbType = SqlDbType.Variant;

      if (!this.ContainsKey(unknown.SystemTypeId))
        this.Add(unknown.SystemTypeId, unknown);
    }

    public SqlDbType GetSqlDbType(string typeName)
    {
      switch (typeName)
      {
        case "image": return SqlDbType.Image;
        case "text": return SqlDbType.Text;
        case "uniqueidentifier": return SqlDbType.UniqueIdentifier;
        case "date": return SqlDbType.Date;
        case "time": return SqlDbType.Time;
        case "datetime2": return SqlDbType.DateTime2;
        case "datetimeoffset": return SqlDbType.DateTimeOffset;
        case "tinyint": return SqlDbType.TinyInt;
        case "smallint": return SqlDbType.SmallInt;
        case "int": return SqlDbType.Int;
        case "smalldatetime": return SqlDbType.SmallDateTime;
        case "real": return SqlDbType.Real;
        case "money": return SqlDbType.Money;
        case "datetime": return SqlDbType.DateTime;
        case "float": return SqlDbType.Float;
        case "sql_variant": return SqlDbType.Variant;
        case "ntext": return SqlDbType.NText;
        case "bit": return SqlDbType.Bit;
        case "decimal": return SqlDbType.Decimal;
        case "numeric": return SqlDbType.Decimal;
        case "smallmoney": return SqlDbType.SmallMoney;
        case "bigint": return SqlDbType.BigInt;
        case "varbinary": return SqlDbType.VarBinary;
        case "varchar": return SqlDbType.VarChar;
        case "binary": return SqlDbType.Binary;
        case "char": return SqlDbType.Char;
        case "timestamp": return SqlDbType.Timestamp;
        case "nvarchar": return SqlDbType.NVarChar;
        case "nchar": return SqlDbType.NChar;
        case "xml": return SqlDbType.Xml;


        case "sysname": return SqlDbType.Text;
        case "hierarchyid": return SqlDbType.Text;
        case "geometry": return SqlDbType.Text;
        case "geography": return SqlDbType.Text;
      }

      // default unknown types to text - hopefully won't occur... 
      return SqlDbType.Text;
    }
  }
}
