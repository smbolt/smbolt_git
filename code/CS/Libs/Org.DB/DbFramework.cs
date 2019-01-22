using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.GS;

namespace Org.DB
{
  public enum DbEntity
  {
    NotSet,
    Computer,
    ComputerProduct,
    ComputerProductHistory,
    OrgControl,
    OrgControlHistory,
    Product,
    tblPatientLink,
    tblPatientLinkProcessed
  }

  public static class DbHelper
  {
    private static bool _isInitialized = false;
    public static bool IsInitialized
    {
      get { return _isInitialized; }
    }

    private static bool _isLoaded = false;
    public static bool IsLoaded
    {
      get { return _isLoaded; }
    }

    private static string _dbReport = String.Empty;
    public static string DbReport
    {
      get { return _dbReport; }
    }

    public static string DbCodeGenNamespace { get; set; }
    public static string SqlInstanceName { get; set; }
    public static string DatabaseName { get; set; }
    public static DbTypeSet DbTypeSet { get; set; }
    public static DbTableSet DbTableSet { get; set; }
    public static DbConstraintSet DbConstraintSet { get; set; }
    public static Type[] Types { get; set; }
    public static string CurrentQuery { get; set; }

    public static bool Initialize()
    {
      if (_isInitialized)
        return _isInitialized;

      Types = GetTypes();

      DbCodeGenNamespace = "DatabaseName";
      DbTypeSet = new DbTypeSet();
      DbTableSet = new DbTableSet();
      DbConstraintSet = new DbConstraintSet();

      _isInitialized = true;

      return _isInitialized;
    }

    public static void UnInitialize()
    {
      _isInitialized = false;
      _isLoaded = false;
    }

    public static void GetDbCodeGenNamespace(SqlConnection conn)
    {
      string prefix = String.Empty;

      DbTableSet ts = new DbTableSet();

      string sql = "SELECT " +
                    "  [value] AS [Value] " +
                    "FROM sys.extended_properties " +
                    "WHERE [name] = 'DbCodeGenNamespace' ";

      SqlInstanceName = conn.DataSource;
      DatabaseName = conn.Database;

      SqlDataAdapter da = new SqlDataAdapter(sql, conn);
      DataSet ds = new DataSet();
      da.Fill(ds);

      if (ds.Tables[0].Rows.Count > 0)
      {
        DataRow r = ds.Tables[0].Rows[0];

        if (!r.IsNull("Value"))
          prefix = Convert.ToString(r["Value"]);
      }

      if (prefix.IsBlank())
        DbCodeGenNamespace = conn.Database;
      else
        DbCodeGenNamespace = prefix;
    }

    private static Type[] GetTypes()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      Type[] types = assembly.GetTypes();
      List<Type> typesToReturn = new List<Type>();

      foreach (Type t in types)
      {
        List<System.Attribute> attrList = t.GetCustomAttributes(false).OfType<System.Attribute>().ToList();
        foreach (System.Attribute attr in attrList)
        {
          if (attr.GetType().Name == "IsDbTable")
          {
            typesToReturn.Add(t);
            break;
          }
        }
      }

      return typesToReturn.ToArray();
    }

    public static List<string> GetTables(DbTableSet ts)
    {
      List<string> tables = new List<string>();

      foreach (DbTable t in ts.Values)
      {
        tables.Add(t.TableName);
      }

      tables.Sort();
      return tables;
    }

    public static DbTableSet LoadTables(SqlConnection conn)
    {
      DbTableSet ts = new DbTableSet();

      string sql = "SELECT " +
                    "  s.[name] AS [Schema], " +
                    "  t.[name] AS [Name], " +
                    "  [type] AS [Type], " +
                    "  [create_date] AS [CreateDate], " +
                    "  [modify_date] AS [ModifyDate] " +
                    "FROM sys.tables t " +
                    "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                    "ORDER BY name ";

      SqlDataAdapter da = new SqlDataAdapter(sql, conn);
      DataSet ds = new DataSet();
      da.Fill(ds);

      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow r = ds.Tables[0].Rows[i];

        DbTable t = new DbTable();

        t.DatabaseName = conn.Database;

        if (!r.IsNull("Schema"))
          t.SchemaName = Convert.ToString(r["Schema"]);

        if (!r.IsNull("Name"))
          t.TableName = t.SchemaName + "." + Convert.ToString(r["Name"]);

        if (!r.IsNull("Type"))
          t.TableType = Convert.ToString(r["Type"]);

        if (!r.IsNull("CreateDate"))
          t.CreateDT = Convert.ToDateTime(r["CreateDate"]);

        if (!r.IsNull("ModifyDate"))
          t.ModifyDT = Convert.ToDateTime(r["ModifyDate"]);

        if (t.TableType.Trim().ToUpper() == "U")
          ts.Add(t.TableName, t);
      }

      return ts;
    }

    public static void LoadTableConstraints()
    {
      foreach (DbConstraint c in DbHelper.DbConstraintSet)
      {
        if (c.ConstraintType == ConstraintType.PRIMARY_KEY)
        {
          DbTable t = DbTableSet[c.TableSchema + "." + c.TableName];
          foreach (DbColumn col in t.DbColumnSet.Values)
          {
            if (col.Name == c.ColumnName)
            {
              t.PrimaryKeyColumn = col;
              break;
            }
          }
        }
      }

      _isLoaded = true;
    }

    public static DbTypeSet LoadTypes(SqlConnection conn)
    {
      DbTypeSet typeSet = new DbTypeSet();
      typeSet.Load(conn);

      return typeSet;
    }

    public static DbConstraintSet LoadConstraints(SqlConnection conn)
    {
      DbConstraintSet cs = new DbConstraintSet();

      string sql = "SELECT " +
                    "  KU.CONSTRAINT_CATALOG as [ConstraintCatalog], " +
                    "  KU.CONSTRAINT_SCHEMA as [ConstraintSchema], " +
                    "  KU.CONSTRAINT_NAME as [ConstraintName], " +
                    "  KU.TABLE_CATALOG as [TableCatalog], " +
                    "  KU.TABLE_SCHEMA as [TableSchema], " +
                    "  KU.TABLE_NAME as [TableName], " +
                    "  TC.CONSTRAINT_TYPE as [ConstraintType], " +
                    "  KU.COLUMN_NAME as [ColumnName], " +
                    "  KU.ORDINAL_POSITION as [OrdinalPosition] " +
                    "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC " +
                    "  INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU " +
                    "    ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME " +
                    "ORDER BY " +
                    "[TableName], " +
                    "[OrdinalPosition] ";


      SqlDataAdapter da = new SqlDataAdapter(sql, conn);
      DataSet ds = new DataSet();
      da.Fill(ds);

      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow r = ds.Tables[0].Rows[i];

        DbConstraint c = new DbConstraint();

        if (!r.IsNull("ConstraintCatalog"))
          c.ConstraintCatalog = Convert.ToString(r["ConstraintCatalog"]);

        if (!r.IsNull("ConstraintSchema"))
          c.ConstraintSchema = Convert.ToString(r["ConstraintSchema"]);

        if (!r.IsNull("ConstraintName"))
          c.ConstraintName = Convert.ToString(r["ConstraintName"]);

        if (!r.IsNull("TableCatalog"))
          c.TableCatalog = Convert.ToString(r["TableCatalog"]);

        if (!r.IsNull("TableSchema"))
          c.TableSchema = Convert.ToString(r["TableSchema"]);

        if (!r.IsNull("TableName"))
          c.TableName = Convert.ToString(r["TableName"]);

        if (!r.IsNull("ConstraintType"))
          c.ConstraintType = GetConstraintType(Convert.ToString(r["ConstraintType"]));

        if (!r.IsNull("ColumnName"))
          c.ColumnName = Convert.ToString(r["ColumnName"]);

        if (!r.IsNull("OrdinalPosition"))
          c.OrdinalPosition = Convert.ToInt32(r["OrdinalPosition"]);

        cs.Add(c);
      }

      return cs;
    }

    private static ConstraintType GetConstraintType(string value)
    {
      value = value.ToUpper();

      if (value.Contains("PRIMARY"))
        return ConstraintType.PRIMARY_KEY;

      if (value.Contains("FOREIGN"))
        return ConstraintType.FOREIGN_KEY;

      if (value.Contains("UNIQUE"))
        return ConstraintType.UNIQUE;

      return ConstraintType.UNKNOWN;
    }


    public static void LoadColumns(SqlConnection conn, DbTableSet ts, DbTypeSet typeSet)
    {
      foreach (DbTable t in ts.Values)
      {
        t.Load(conn, typeSet);
      }
    }

    public static string GenerateDbSchema(string instanceName, string databaseName, DbTableSet ts)
    {
      XElement root = new XElement("DbSchema");
      root.Add(new XAttribute("Instance", instanceName));
      root.Add(new XAttribute("Database", databaseName));
      root.Add(new XAttribute("DateTime", DateTime.Now.ToCCYYMMDDHHMMSS()));
      root.Add(GenerateSchemaFromTables(ts));
      return root.ToString();
    }

    private static XElement GenerateSchemaFromTables(DbTableSet ts)
    {
      XElement tableSetElement = new XElement("TableSet");
      foreach (DbTable t in ts.Values)
      {
        XElement tableElement = new XElement("Table");
        tableElement.Add(new XAttribute("TableName", t.TableName));
        foreach (DbColumn c in t.DbColumnSet.Values)
        {
          XElement columnElement = new XElement("Column");
          columnElement.Add(new XElement("ColumnName", c.Name));
          columnElement.Add(new XElement("DataType", c.DbType.SqlDbType.ToString()));
          columnElement.Add(new XElement("Nullable", c.IsNullable.ToString()));
          columnElement.Add(new XElement("IsIdentity", c.IsIdentity.ToString()));
          columnElement.Add(new XElement("IsPrimaryKey", c.IsPrimaryKey.ToString())); 
          columnElement.Add(new XElement("Precision", c.Precision.ToString()));
          columnElement.Add(new XElement("Scale", c.Scale.ToString()));
          columnElement.Add(new XElement("SystemTypeId", c.SystemTypeId.ToString()));
          columnElement.Add(new XElement("MaxLength", c.MaxLength.ToString()));
          tableElement.Add(columnElement);
        }
        tableSetElement.Add(tableElement);
      }

      return tableSetElement;
    }

    public static string GenerateCode(DbTableSet ts, bool updateCode, string dbClassesPath)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(BuildCodeFileStart());
      sb.Append(BuildCustomAttributes());
      sb.Append(BuildEntityEnum(ts));
      sb.Append(GenerateClasses(ts));
      sb.Append(BuildCodeFileEnd());
      string dbClasses = sb.ToString();

      if (updateCode)
        File.WriteAllText(dbClassesPath, dbClasses);

      return dbClasses;
    }

    private static string BuildCodeFileStart()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("// ************************************************************************************" + _db.nl);
      sb.Append("// *** THIS CODE FILE IS AUTOMATICALLY GENERATED AND SHOULD NOT BE EDITED MANUALLY. *** " + _db.nl);
      sb.Append("// *** TO GENERATE THIS FILE USE THE UTILITY PROJECT NAMED DbGen or DbCodeGen.      ***" + _db.nl);
      sb.Append("// ************************************************************************************" + _db.nl2);

      sb.Append("using System;" + _db.nl);
      sb.Append("using System.Collections.Generic;" + _db.nl);
      sb.Append("using System.Linq;" + _db.nl);
      sb.Append("using System.Text;" + _db.nl);
      sb.Append("using System.Xml.Linq;" + _db.nl);
      sb.Append("using System.Drawing;" + _db.nl);
      sb.Append("using System.Data;" + _db.nl);
      sb.Append("using System.Data.SqlClient;" + _db.nl);
      sb.Append("using Org.GS;" + _db.nl);
      sb.Append(_db.nl + "namespace Org.GS.Database" + "." + DbCodeGenNamespace + _db.nl);
      sb.Append("{" + _db.nl);

      return sb.ToString();
    }


    private static string BuildCustomAttributes()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("    [AttributeUsage(AttributeTargets.Class)]" + _db.nl);
      sb.Append("    public class IsDbTable : Attribute {}" + _db.nl2);

      sb.Append("    [AttributeUsage(AttributeTargets.Property)]" + _db.nl);
      sb.Append("    public class IsDbColumn : Attribute {}" + _db.nl2);

      return sb.ToString();
    }

    private static string BuildEntityEnum(DbTableSet ts)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("    public enum DbEntity" + _db.nl);
      sb.Append("    {" + _db.nl);
      sb.Append("        NotSet");
      if (ts.Values.Count > 0)
        sb.Append(",");
      sb.Append(_db.nl);

      int count = 0;

      foreach (DbTable t in ts.Values)
      {
        count++;
        sb.Append("        " + t.TableName);
        if (count < ts.Values.Count)
          sb.Append(",");
        sb.Append(_db.nl);
      }

      sb.Append("    }" + _db.nl2);

      string code = sb.ToString();

      return code;
    }

    private static string BuildCodeFileEnd()
    {
      return "}" + _db.nl;
    }

    public static string GenerateClasses(DbTableSet ts)
    {
      StringBuilder sb = new StringBuilder();

      foreach (DbTable t in ts.Values)
      {
        sb.Append(BuildClassStart(t));

        sb.Append(GeneratePropertiesFromColumns(t.DbColumnSet));

        sb.Append("        public override bool IsLoadingForUpdate { get; set; }" + _db.nl2);

        sb.Append("        public void Dispose() {}" + _db.nl2);

        sb.Append(BuildConstructors(t));

        sb.Append("    }" + _db.nl2);
      }


      return sb.ToString();
    }

    public static string GenerateBasicClassForTable(DbTableSet ts, string tableName)
    {
      StringBuilder sb = new StringBuilder();

      if (tableName == "All Tables")
      {
        foreach (DbTable t in ts.Values)
        {
          sb.Append(BuildBasicClassStart(t));
          sb.Append(GenerateBasicPropertiesFromColumns(t.DbColumnSet));
          sb.Append("  }" + _db.nl2);
        }
      }
      else
      {
        DbTable t = ts[tableName];

        if (t == null)
          return "Table '" + tableName + "' not found in TableSet."; 
      
        sb.Append(BuildBasicClassStart(t));
        sb.Append(GenerateBasicPropertiesFromColumns(t.DbColumnSet));
        sb.Append("  }" + _db.nl2);
      }

      return sb.ToString();
    }

    private static string BuildClassStart(DbTable t)
    {
      string classStart = String.Empty;

      classStart =
          "    [IsDbTable]" + _db.nl +
          "    [ObjectMapPrefix(\"DB\")]" + _db.nl +
          "    public class " + t.TableNameSansSchema + " : DbEntityBase, IDisposable" + _db.nl + "    {" + _db.nl;

      return classStart;
    }

    private static string BuildBasicClassStart(DbTable t)
    {
      string classStart = String.Empty;

      classStart =
          "  [DbMap(DbElement.Model, \"" + t.DatabaseName + "\", \"" + t.SchemaName + "\", \"" + t.TableNameSansSchema + "\")]" + g.crlf + 
          "  public class " + t.TableNameSansSchema + " : ModelBase" + _db.nl + "  {" + _db.nl;

      return classStart;
    }

    private static string BuildConstructors(DbTable t)
    {
      string method =
             "        public " + t.TableName + "()" + _db.nl +
             "        {" + _db.nl +
             "        }" + _db.nl2 +

             "        public " + t.TableName + "(object source)" + _db.nl +
             "        {" + _db.nl +
             "            base.MapFromObject(source, String.Empty);" + _db.nl +
             "        }" + _db.nl2 +

             "        public " + t.TableName + "(object source, string context)" + _db.nl +
             "        {" + _db.nl +
             "            base.MapFromObject(source, context);" + _db.nl +
             "        }" + _db.nl;

      return method;
    }

    private static string GeneratePropertiesFromColumns(DbColumnSet cs)
    {
      StringBuilder sb = new StringBuilder();

      foreach (DbColumn c in cs.Values)
      {
        string code = String.Empty;
        string type = GetTypeString(c.DbType.SqlDbType);
        string pfn = "_" + c.Name.Substring(0, 1).ToLower() + c.Name.Substring(1);
        string ofn = "_" + pfn;
        bool nullable = false;

        switch (type)
        {
          case "int":
          case "long":
          case "decimal":
          case "float":
          case "bool":
          case "DateTime":
          case "TimeSpan":
          case "Guid":
            nullable = true;
            break;

          case "string":
          case "byte[]":
          case "Image":
          case "object":
          case "XElement":
            break;

          default:
            break;
        }

        string privateField;
        string originalField;

        string attr = "        [IsDbColumn]" + _db.nl;
        string start = "        public ";

        if (nullable)
        {
          privateField = "        private " + type + "? " + pfn + ";" + _db.nl;
          originalField = "        private " + type + "? " + ofn + ";" + _db.nl;

          code = privateField +
                 originalField +
                 attr + start + type + "? " + c.Name + _db.nl +
                "        {" + _db.nl +
                "            get" + _db.nl +
                "            {" + _db.nl +
                "                if (" + pfn + ".HasValue)" + _db.nl +
                "                    return " + pfn + ".Value;" + _db.nl +
                "                else" + _db.nl +
                "                    return null;" + _db.nl +
                "            }" + _db.nl +
                "            set" + _db.nl +
                "            {" + _db.nl +
                "               " + pfn + " = value;" + _db.nl +
                "                if (this.IsLoadingForUpdate)" + _db.nl +
                "                    " + ofn + " = value;" + _db.nl +
                "            }" + _db.nl +
                "        }" + _db.nl2;
        }
        else
        {
          privateField = "        private " + type + " " + pfn + ";" + _db.nl;
          originalField = "        private " + type + " " + ofn + ";" + _db.nl;

          code = privateField +
                 originalField +
                 attr + start + type + " " + c.Name + _db.nl +
                "        {" + _db.nl +
                "            get { return " + pfn + "; }" + _db.nl;

          if (type == "string" && c.MaxLength > 0)
          {
            code +=
              "            set" + _db.nl +
              "            {" + _db.nl +
              "                if (value == null)" + _db.nl +
              "                {" + _db.nl +
              "                    " + pfn + " = value;" + _db.nl +
              "                }" + _db.nl +
              "                else" + _db.nl +
              "                {" + _db.nl +
              "                    if (value.Length > " + c.MaxLength.ToString() + ")" + _db.nl +
              "                        " + pfn + " = value.Substring(0, " + c.MaxLength.ToString() + ");" + _db.nl +
              "                    else" + _db.nl +
              "                        " + pfn + " = value;" + _db.nl +
              "                }" + _db.nl +
              "                if (this.IsLoadingForUpdate)" + _db.nl +
              "                    " + ofn + " = " + pfn + ";" + _db.nl +
              "            }" + _db.nl;
          }
          else
          {
            code +=
              "            set" + _db.nl +
              "            {" + _db.nl +
              "               " + pfn + " = value;" + _db.nl +
              "                if (this.IsLoadingForUpdate)" + _db.nl +
              "                    " + ofn + " = value;" + _db.nl +
              "            }" + _db.nl;
          }

          code += "        }" + _db.nl2;
        }

        sb.Append(code);
      }

      return sb.ToString();
    }

    private static string GenerateBasicPropertiesFromColumns(DbColumnSet cs)
    {
      StringBuilder sb = new StringBuilder();

      int columnCount = 0;

      foreach (DbColumn c in cs.Values)
      {
        string code = String.Empty;
        string type = GetTypeString(c.DbType.SqlDbType);
        string pfn = "_" + c.Name.Substring(0, 1).ToLower() + c.Name.Substring(1);
        string ofn = "_" + pfn;
        bool nullable = c.IsNullable;

        switch (type)
        {
          case "string":
          case "byte[]":
          case "Image":
          case "object":
          case "XElement":
            nullable = false;
            break;
        }

        string columnName = c.Name;
        if (c.Name == c.Table.TableNameSansSchema)
          columnName += "1";

        string attribute = "    [DbMap(DbElement.Column, \"" + c.Table.DatabaseName  + "\", \"" + c.Table.SchemaName + 
          "\", \"" + c.Table.TableNameSansSchema + "\", \"" + c.Name + "\", " + (c.IsNullable ? "true" : "false") + ", " + 
          (c.IsPrimaryKey ? "true" : "false") + ", " + (c.IsIdentity ? "true": "false") + ")]"; 
        if (columnCount > 0)
          attribute = g.crlf + attribute;

        string start = "    public ";

        if (nullable)
        {
          code = attribute + g.crlf + start + type + "? " + columnName + " { get; set; }" + g.crlf;
        }
        else
        {
          code = attribute + g.crlf + start + type + " " + columnName + " { get; set; }" + g.crlf;
        }

        sb.Append(code);
        columnCount++; 
      }

      return sb.ToString();
    }

    private static string GetTypeString(SqlDbType type)
    {
      switch (type)
      {
        case SqlDbType.Image: return "Image";
        case SqlDbType.Text: return "string";
        case SqlDbType.UniqueIdentifier: return "Guid";
        case SqlDbType.Date: return "DateTime";
        case SqlDbType.DateTimeOffset: return "TimeSpan";
        case SqlDbType.TinyInt: return "int";
        case SqlDbType.SmallInt: return "int";
        case SqlDbType.Int: return "int";
        case SqlDbType.SmallDateTime: return "DateTime";
        case SqlDbType.Real: return "float";
        case SqlDbType.Money: return "decimal";
        case SqlDbType.DateTime: return "DateTime";
        case SqlDbType.DateTime2: return "DateTime";
        case SqlDbType.Time: return "TimeSpan";
        case SqlDbType.Float: return "float";
        case SqlDbType.Variant: return "object";
        case SqlDbType.NText: return "string";
        case SqlDbType.Bit: return "bool";
        case SqlDbType.Decimal: return "decimal";
        case SqlDbType.SmallMoney: return "decimal";
        case SqlDbType.BigInt: return "long";
        case SqlDbType.VarBinary: return "byte[]";
        case SqlDbType.VarChar: return "string";
        case SqlDbType.Binary: return "byte[]";
        case SqlDbType.Char: return "string";
        case SqlDbType.Timestamp: return "byte[]";
        case SqlDbType.NVarChar: return "string";
        case SqlDbType.NChar: return "string";
        case SqlDbType.Xml: return "XElement";
      }

      return "object";
    }

    public static string GetPropertyType(Type t)
    {
      string typeName = t.Name;

      if (!typeName.StartsWith("Nullable"))
        return typeName;

      Type nullableType = Nullable.GetUnderlyingType(t);
      return nullableType.Name;
    }

    public static string GetTypeName(Type t)
    {
      if (t.IsGenericType)
        return (Nullable.GetUnderlyingType(t)).Name;

      return t.Name;
    }

    public static void PrepareInsertCommand(object o, DbTable table, SqlCommand cmd, int rowIndex)
    {
      string tableName = table.TableName;

      string columnsClause = String.Empty;
      string valuesClause = String.Empty;
      string getIdentityClause = String.Empty;
      bool getIdentity = false;
      SqlParameter identityParameter = null;

      DbEntityBase entity = (DbEntityBase)o;

      int columnIndex = 0;
      StringBuilder sb = new StringBuilder();
      sb.Append("IX  Name                 D N Type                 Value   " + _db.nl);

      foreach (DbColumn c in table.DbColumnSet.Values)
      {
        if (c.IsIdentity)
        {
          getIdentity = true;
          getIdentityClause = " SET @identity = SCOPE_IDENTITY() ";
          identityParameter = new SqlParameter("@identity", c.DbType.SqlDbType);
          identityParameter.Direction = ParameterDirection.Output;

          continue;
        }

        if (c.DbType.SqlDbType == SqlDbType.Timestamp)
          continue;

        bool hasDefault = c.HasDefaultValue;
        bool isNullable = c.IsNullable;

        PropertyInfo pi = entity.GetType().GetProperty(c.Name);
        if (pi == null)
          throw new Exception("Cannot locate PropertyInfo via Reflection for column name '" + c.Name + "' in table '" + tableName + "'.");

        string parmName = "@" + c.Name;
        object columnValue = GetColumnValue(pi.GetValue(entity, null), c, isNullable);

        if (columnValue.GetType().Name == "DBNull" && !c.IsNullable && !c.HasDefaultValue && c.DbType.SqlDbType != SqlDbType.Timestamp)
          throw new Exception("The value for column '" + c.Name + "' in table '" + tableName + "' is null but the database column is not nullable and does not have a default value .");

        bool useDbDefault = columnValue.GetType().Name == "DBNull" && c.HasDefaultValue;

        if (!useDbDefault)
        {
          cmd.Parameters.Add(parmName, c.DbType.SqlDbType).Value = columnValue;

          if (columnsClause.IsNotBlank())
            columnsClause += ", " + _db.nl;

          columnsClause += "    [" + c.Name + "] ";

          if (valuesClause.IsNotBlank())
            valuesClause += ", " + _db.nl;

          valuesClause += "    " + parmName + " ";
        }

        columnIndex++;
      }

      string sql = "INSERT INTO [" + tableName + "] " + _db.nl +
                   "( " + _db.nl +
                   columnsClause + _db.nl +
                   ") " + _db.nl +
                   "VALUES " + _db.nl +
                   "( " + _db.nl +
                   valuesClause + _db.nl +
                   ") " + _db.nl;

      if (getIdentity)
      {
        sql += " ; " + _db.nl + getIdentityClause;
        cmd.Parameters.Add(identityParameter);
      }

      cmd.CommandText = sql;
      DbHelper.CurrentQuery = sql;
    }

    public static void PrepareUpdateCommand(object o, DbTable table, SqlCommand cmd, int rowIndex)
    {
      string tableName = table.TableName;
      cmd.Parameters.Clear();
      cmd.CommandType = CommandType.Text;

      List<string> columns = new List<string>();
      List<string> values = new List<string>();

      string whereClause = String.Empty;
      SqlParameter identityParameter = null;

      DbEntityBase entity = (DbEntityBase)o;

      int columnIndex = 0;
      StringBuilder sb = new StringBuilder();
      sb.Append("IX  Name                 D N Type                 Value   " + _db.nl);

      foreach (DbColumn c in table.DbColumnSet.Values)
      {
        PropertyInfo pi = entity.GetType().GetProperty(c.Name);
        if (pi == null)
          throw new Exception("Cannot locate PropertyInfo via Reflection for column name '" + c.Name + "' in table '" + tableName + "'.");

        if (c.IsIdentity)
        {
          object identityValue = GetColumnValue(pi.GetValue(entity, null), c, c.IsNullable);
          whereClause = "WHERE  [" + c.Name + "] = @identity ";
          identityParameter = new SqlParameter("@identity", c.DbType.SqlDbType);
          identityParameter.Value = identityValue;
          continue;
        }

        c.Row = entity;
        if (!c.IsUpdated)
          continue;

        bool hasDefault = c.HasDefaultValue;
        bool isNullable = c.IsNullable;

        string parmName = "@" + c.Name;
        object columnValue = GetColumnValue(pi.GetValue(entity, null), c, isNullable);

        cmd.Parameters.Add(parmName, c.DbType.SqlDbType).Value = columnValue;
        if (c.DbType.SqlDbType == SqlDbType.VarChar)
          cmd.Parameters[parmName].Size = c.MaxLength;

        columns.Add(c.Name);
        values.Add(parmName);

        columnIndex++;
      }

      string sql = "UPDATE [" + tableName + "] " + _db.nl + "SET  ";

      string[] columnArray = columns.ToArray();
      string[] valueArray = values.ToArray();

      for (int i = 0; i < columnArray.Length; i++)
      {
        sql += "  [" + columnArray[i] + "] = " + valueArray[i];
        if (i < columnArray.Length - 1)
          sql += ",";
        sql += _db.nl;
      }

      sql += whereClause;
      cmd.Parameters.Add(identityParameter);

      cmd.CommandText = sql;
    }

    private static object GetColumnValue(object o, DbColumn c, bool isNullable)
    {
      object columnValue = o;

      if (c.DbType.SqlDbType == SqlDbType.DateTimeOffset)
      {
        DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
        columnValue = dto.ToString("yyyy-MM-dd HH:mm:ss.ffffff zzz");
      }

      if (c.DbType.SqlDbType == SqlDbType.Xml && columnValue != null)
        columnValue = new System.Data.SqlTypes.SqlXml(((XElement)columnValue).CreateReader());

      if (c.DbType.SqlDbType == SqlDbType.Image)
      {
        if (isNullable)
          columnValue = null;
        else
        {
          Image image = (Bitmap)o;
          ImageConverter converter = new ImageConverter();
          byte[] imgBytes = new byte[0];
          imgBytes = (byte[])converter.ConvertTo(image, imgBytes.GetType());
          columnValue = imgBytes;
        }
      }

      if (c.DbType.SqlDbType == SqlDbType.Binary || c.DbType.SqlDbType == SqlDbType.VarBinary)
      {
        string columnName = c.Name;
        if (isNullable)
          columnValue = null;
      }

      if (columnValue == null)
        columnValue = DBNull.Value;

      return columnValue;
    }

    public static bool ValueIsUpdated(string propertyName, string propertyType, object originalValue, object currentValue)
    {
      if (originalValue == null && currentValue == null)
        return false;

      if (originalValue == null || currentValue == null)
        return true;

      switch (propertyType)
      {
        case "Int32":
        case "int":
          if (Convert.ToInt32(currentValue) != Convert.ToInt32(originalValue))
            return true;
          break;

        case "Int64":
        case "long":
          if (((long)currentValue) != ((long)originalValue))
            return true;
          break;

        case "Decimal":
          if (Convert.ToDecimal(currentValue) != Convert.ToDecimal(originalValue))
            return true;
          break;

        case "Single":
          if (Convert.ToSingle(currentValue) != Convert.ToSingle(originalValue))
            return true;
          break;

        case "DateTime":
          if (Convert.ToDateTime(currentValue) != Convert.ToDateTime(originalValue))
            return true;
          break;

        case "String":
          if (Convert.ToString(currentValue) != Convert.ToString(originalValue))
            return true;
          break;

        case "Byte[]":
        case "Bitmap":
          byte[] currentArray = (byte[])currentValue;
          byte[] originalArray = (byte[])originalValue;
          if (currentArray.Length != originalArray.Length)
            return true;

          for (int i = 0; i < currentArray.Length; i++)
          {
            if (currentArray[i] != originalArray[i])
              return true;
          }
          break;

        case "XElement":
          if (((XElement)currentValue).ToString() != ((XElement)originalValue).ToString())
            return true;
          break;


        case "Boolean":
          if (Convert.ToBoolean(currentValue) != Convert.ToBoolean(originalValue))
            return true;
          break;

        case "TimeSpan":
          if (((TimeSpan)currentValue) != ((TimeSpan)originalValue))
            return true;
          break;

        default:
          throw new Exception("Unable to determine equality of " + propertyName + " with its underlying 'originalValue' because comparisions for " +
                              "the data type '" + propertyType + " are not yet supported.");
      }

      return false;
    }

    public static string GetTableNameFromType(object o)
    {
      string fullTypeName = o.GetType().FullName;

      int begPos = fullTypeName.IndexOf("[[");
      if (begPos > -1)
      {
        int endPos = fullTypeName.IndexOf(",", begPos);
        if (endPos > -1)
        {
          string typeName = fullTypeName.Substring(begPos + 2, (endPos - begPos) - 2);
          int lastDot = typeName.LastIndexOf(".");
          if (lastDot == -1)
            return String.Empty;
          string typeNamespace = typeName.Substring(0, lastDot);

          string tableName = typeName.Replace(typeNamespace + ".", String.Empty);
          tableName = tableName.Replace(DbHelper.DbCodeGenNamespace, String.Empty);

          return tableName;
        }
      }

      return String.Empty;
    }

    public static bool EntityExists(DbContext db, string query, string parms)
    {
      db.CheckConnection();

      string entity = query.Split(':').First();
      entity = DbHelper.DbCodeGenNamespace + entity;

      string columnString = query.Split(':').Last();
      string[] columns = columnString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
      string[] values = parms.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

      DbEntity dbEntity = DbEntity.NotSet;
      if (Enum.IsDefined(typeof(DbEntity), entity))
        dbEntity = (DbEntity)Enum.Parse(typeof(DbEntity), entity);
      else
        throw new Exception("Invalid entity (table) specification in query: '" + query + "'.");

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db.Connection;
      PrepareExistsCommand(cmd, dbEntity, columns, values);
      object item = cmd.ExecuteScalar();

      if (item == null)
        return false;

      return true;
    }

    public static void PrepareExistsCommand(SqlCommand cmd, DbEntity entity, string[] columns, string[] values)
    {
      cmd.Parameters.Clear();

      string tableName = entity.ToString();
      tableName = tableName.Replace(DbHelper.DbCodeGenNamespace, String.Empty);

      if (!DbHelper.DbTableSet.ContainsKey(tableName))
        throw new Exception("Table named '" + tableName + "' does not exist in the database.");

      DbTable t = DbHelper.DbTableSet[tableName];

      StringBuilder sb = new StringBuilder();
      sb.Append("SELECT ");

      string primaryKeyColumnName = String.Empty;
      if (t.PrimaryKeyColumn != null)
        primaryKeyColumnName = t.PrimaryKeyColumn.Name;

      if (primaryKeyColumnName.IsNotBlank())
        sb.Append(" [" + primaryKeyColumnName + "] ");
      else
        sb.Append(" * ");

      sb.Append(_db.nl + "FROM [" + t.TableName + "] " + _db.nl);

      if (columns.Length != values.Length)
        throw new Exception("The number of columns specified (" + columns.Length.ToString() + ") does not match the number of values specified (" + values.Length.ToString() + ").");

      if (columns.Length < 1)
        throw new Exception("At least one column and value must be specified.");

      sb.Append("WHERE ");

      for (int i = 0; i < columns.Length; i++)
      {
        if (i > 0)
          sb.Append("AND ");

        string columnName = columns[i].Trim();
        string parmName = "@" + columns[i].Trim();

        DbColumn c;
        if (!t.DbColumnSet.ContainsKey(columnName))
          throw new Exception("Table '" + t.TableName + "' does not contain a column named '" + columnName + "'.");

        c = t.DbColumnSet[columnName];

        sb.Append("[" + columnName + "] = " + parmName + _db.nl);
        cmd.Parameters.Add(parmName, c.DbType.SqlDbType).Value = values[i];
      }

      cmd.CommandText = sb.ToString();
    }

    public static void LoadEntityField(DbEntityBase entity, object value, DbColumn c)
    {
      string propertyName = c.Name;
      string originalFieldName = "__" + c.Name.Substring(0, 1).ToLower() + c.Name.Substring(1);

      DbColumn dbColumn = entity.DbTable.DbColumnSet[c.Name];
      PropertyInfo pi = entity.GetType().GetProperty(c.Name);
      FieldInfo ofi = entity.GetType().GetField(originalFieldName, BindingFlags.Instance | BindingFlags.NonPublic);

      if (pi != null)
      {
        string valueType = value.GetType().Name;
        if (valueType == "DBNull")
        {
          pi.SetValue(entity, null, null);
        }
        else
        {
          switch (c.Name)
          {
            case "time_7_n":
            case "time_7_nn":
            case "datetimeoffset_7_n":
            case "datetimeoffset_7_nn":
            case "timestamp_n":
            case "real_n":
            case "real_nn":
            case "sql_variant_n":
            case "sql_variant_nn":

              break;

            default:
              string propertyType = DbHelper.GetPropertyType(pi.PropertyType);
              switch (propertyType)
              {
                case "Int32":
                  int int32 = Convert.ToInt32(value);
                  pi.SetValue(entity, int32, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, int32);
                  }
                  break;

                case "String":
                  string stringValue = Convert.ToString(value);
                  pi.SetValue(entity, stringValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, stringValue);
                  }
                  break;

                case "Int64":
                  Int64 int64 = Convert.ToInt64(value);
                  pi.SetValue(entity, Convert.ToInt64(int64), null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, int64);
                  }
                  break;

                case "Byte[]":
                  pi.SetValue(entity, (byte[])value, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, (byte[])value);
                  }
                  break;

                case "Boolean":
                  bool boolValue = Convert.ToBoolean(value);
                  pi.SetValue(entity, boolValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, boolValue);
                  }
                  break;

                case "DateTime":
                  DateTime datetimeValue = Convert.ToDateTime(value);
                  pi.SetValue(entity, datetimeValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, datetimeValue);
                  }
                  break;

                case "TimeSpan":
                  throw new Exception("Cannot set property value for column '" + c.Name + "' with data type '" + propertyType + "'.");

                case "Decimal":
                  decimal decimalValue = Convert.ToDecimal(value);
                  pi.SetValue(entity, decimalValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, decimalValue);
                  }
                  break;

                case "Single":
                  Single singleValue = Convert.ToSingle(value);
                  pi.SetValue(entity, singleValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, singleValue);
                  }
                  break;

                case "Image":
                  Image imageValue = Image.FromStream(new MemoryStream((byte[])value));
                  pi.SetValue(entity, imageValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, imageValue);
                  }
                  break;

                case "Object":
                  pi.SetValue(entity, value, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, value);
                  }
                  break;

                case "XElement":
                  XElement xElementValue = XElement.Parse(Convert.ToString(value));
                  pi.SetValue(entity, xElementValue, null);
                  if (entity.IsLoadingForUpdate)
                  {
                    if (ofi != null)
                      ofi.SetValue(entity, xElementValue);
                  }
                  break;

                default:
                  throw new Exception("Cannot set property value for column '" + c.Name + "' with data type '" + propertyType + "'.");

              }
              break;
          }
        }
      }
    }

    public static void BuildDbReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("Database Report for " + DbCodeGenNamespace + _db.nl2);

      foreach (DbTable t in DbTableSet.Values)
      {
        sb.Append("Table: " + t.TableName + _db.nl);
        foreach (DbColumn c in t.DbColumnSet.Values)
        {
          sb.Append(c.Name.PadTo(40) +
                    c.DbType.TypeName.PadTo(25) +
                    c.MaxLength.ToString() + _db.nl);
        }

        sb.Append(_db.nl);
      }

      _dbReport = sb.ToString();
    }
  }

  public class DbContext : DbContextBase
  {
    public DbContext(string connectionString)
      : base(connectionString)
    {
    }

    public DbSet<T> DbSet<T>()
    {
      return new DbSet<T>(this);
    }
  }

  public class DbContextBase : IDisposable
  {
    private static object DbContextBaseConstructor_LockObject;
    private bool isDbHelperInitialized = DbHelper.Initialize();
    private bool isDisposed = false;
    internal DbFunction DbFunction;

    public SqlConnection Connection { get; set; }
    public bool ConnectionOpen
    {
      get { return IsConnectionOpen(); }
    }

    private bool _updateAttempted;
    public bool UpdateAttempted
    {
      get { return _updateAttempted; }
    }

    private string _connectionString;

    private SqlTransaction Transaction;

    public DbContextBase(string connectionString)
    {
      if (DbContextBaseConstructor_LockObject == null)
        DbContextBaseConstructor_LockObject = new object();

      if (Monitor.TryEnter(DbContextBaseConstructor_LockObject, 5000))
      {
        try
        {
          this._updateAttempted = false;
          this.DbFunction = DbFunction.NotSet;
          this._connectionString = connectionString;
          this.Connection = new SqlConnection(_connectionString);
          this.Connection.Open();

          if (DbHelper.IsLoaded)
            return;

          DbHelper.GetDbCodeGenNamespace(Connection);

          DbHelper.DbTypeSet = DbHelper.LoadTypes(Connection);
          DbHelper.DbTableSet = DbHelper.LoadTables(Connection);
          DbHelper.DbConstraintSet = DbHelper.LoadConstraints(Connection);
          DbHelper.LoadColumns(Connection, DbHelper.DbTableSet, DbHelper.DbTypeSet);
          DbHelper.LoadTableConstraints();
          //DbHelper.BuildDbReport();
        }
        catch
        {
          throw;
        }
        finally
        {
          Monitor.Exit(DbContextBaseConstructor_LockObject);
        }
      }

    }

    ~DbContextBase()
    {
      this.Dispose(false);
    }

    public void CloseConnection()
    {
      if (this.Connection != null)
      {
        if (this.Connection.State != ConnectionState.Closed)
          this.Connection.Close();
      }
    }

    private bool IsConnectionOpen()
    {
      if (this.Connection != null)
      {
        if (this.Connection.State == ConnectionState.Open)
          return true;
      }

      return false;
    }

    public DbEntityBaseSet GetEntitySet(string tableName)
    {
      CheckConnection();
      this.DbFunction = DbFunction.Select;
      DbEntityBaseSet set = new DbEntityBaseSet();
      Type t = GetEntityType(tableName);

      string sql = "select * from dbo." + tableName;
      SqlDataAdapter da = new SqlDataAdapter(sql, this.Connection);
      DataSet ds = new DataSet();
      da.Fill(ds);

      foreach (DataRow r in ds.Tables[0].Rows)
      {
        DbEntityBase entity = (DbEntityBase)Activator.CreateInstance(t);
        for (int i = 0; i < r.ItemArray.Length; i++)
        {
          object value = r.ItemArray[i];
          DataColumn c = r.Table.Columns[i];
          string columnName = c.ColumnName;
          DbColumn dbColumn = entity.DbTable.DbColumnSet[columnName];
          DbHelper.LoadEntityField(entity, value, dbColumn);
        }
        set.Add(entity);
      }

      return set;
    }

    public void Load<T>(DbSet<T> dbSet)
    {
      CheckConnection();
      this.DbFunction = DbFunction.Select;
      Type t = GetEntityType(dbSet.DbQuery.TableName);

      string sql = dbSet.DbQuery.BuildQuery();
      SqlDataAdapter da = new SqlDataAdapter(sql, this.Connection);
      DataSet ds = new DataSet();
      da.Fill(ds);

      foreach (DataRow r in ds.Tables[0].Rows)
      {
        DbEntityBase entity = (DbEntityBase)Activator.CreateInstance(t);
        entity.IsLoadingForUpdate = true;
        for (int i = 0; i < r.ItemArray.Length; i++)
        {
          object value = r.ItemArray[i];
          DataColumn c = r.Table.Columns[i];
          string columnName = c.ColumnName;
          DbColumn dbColumn = entity.DbTable.DbColumnSet[columnName];
          DbHelper.LoadEntityField(entity, value, dbColumn);
        }

        entity.IsLoadingForUpdate = false;
        entity.IsLoadedForUpdate = true;
        dbSet.Add((T)(object)entity);
      }
    }

    public void Load(DbResultSet resultSet)
    {
      CheckConnection();
      this.DbFunction = DbFunction.Select;

      string sql = resultSet.Query;
      SqlDataAdapter da = new SqlDataAdapter(sql, this.Connection);
      DataSet ds = new DataSet();
      da.Fill(ds);

      foreach (DataRow r in ds.Tables[0].Rows)
      {
        DbQueryResult result = new DbQueryResult();

        for (int i = 0; i < r.ItemArray.Length; i++)
        {
          object value = r.ItemArray[i];
          DataColumn c = r.Table.Columns[i];
          string columnName = c.ColumnName;
          int seq = 0;
          while (result.ContainsKey(columnName))
          {
            columnName = columnName + seq.ToString();
            seq++;
          }

          result.Add(columnName, value);
        }

        resultSet.Add(result);
      }
    }

    public void ExecuteNonQuery(DbResult result)
    {
      CheckConnection();

      try
      {
        string sql = result.Query;
        SqlCommand cmd = new SqlCommand(sql, this.Connection);
        cmd.CommandType = CommandType.Text;
        result.RowsAffected = cmd.ExecuteNonQuery();
        result.DbStatusCode = DbStatusCode.Success;
      }
      catch (Exception ex)
      {
        result.DbStatusCode = DbStatusCode.Failed;
        result.Message = ex.Message;
      }
    }

    public void ExecuteScalar(DbResult result)
    {
      CheckConnection();

      try
      {
        string sql = result.Query;
        SqlCommand cmd = new SqlCommand(sql, this.Connection);
        cmd.CommandType = CommandType.Text;
        result.ScalarValue = cmd.ExecuteScalar();
        result.DbStatusCode = DbStatusCode.Success;
      }
      catch (Exception ex)
      {
        result.DbStatusCode = DbStatusCode.Failed;
        result.Message = ex.Message;
      }
    }

    public void BeginTransaction()
    {
      this.Transaction = this.Connection.BeginTransaction();
    }

    public void CommitTransaction()
    {
      if (this.Transaction == null)
        throw new Exception("Failed to commit transaction because the Transaction object is null.");

      if (this.Transaction.Connection == null)
        throw new Exception("Failed to commit transaction because no the Connection property of the Transaction object is null.");

      this.Transaction.Commit();

      this.Transaction = null;
    }

    public void RollbackTransaction()
    {
      if (this.Transaction == null)
        throw new Exception("Failed to rollback transaction because the Transaction object is null.");

      if (this.Transaction.Connection == null)
        throw new Exception("Failed to rollback transaction because no the Connection property of the Transaction object is null.");

      this.Transaction.Rollback();

      this.Transaction = null;
    }

    public object Insert<T>(DbRow<T> dbRow)
    {
      this._updateAttempted = true;
      CheckConnection();
      this.DbFunction = DbFunction.Insert;
      object identityValue = null;
      Type t = GetEntityType(dbRow.DbQuery.TableName);

      using (SqlCommand cmd = new SqlCommand("", this.Connection))
      {
        if (this.Transaction != null)
          cmd.Transaction = this.Transaction;

        cmd.Parameters.Clear();
        DbHelper.PrepareInsertCommand(dbRow.DbEntity, dbRow.DbTable, cmd, -1);
        int rowsAffected = cmd.ExecuteNonQuery();

        if (cmd.Parameters.Contains("@identity"))
          identityValue = cmd.Parameters["@identity"].Value;
      }

      return identityValue;
    }

    public object Insert<T>(DbSet<T> dbSet)
    {
      this._updateAttempted = true;
      CheckConnection();
      this.DbFunction = DbFunction.Insert;
      object identityValue = null;
      Type t = GetEntityType(dbSet.DbQuery.TableName);

      using (SqlCommand cmd = new SqlCommand("", this.Connection))
      {
        if (this.Transaction != null)
          cmd.Transaction = this.Transaction;

        for (int i = 0; i < dbSet.Count; i++)
        {
          cmd.Parameters.Clear();
          DbHelper.PrepareInsertCommand(dbSet[i], dbSet.DbTable, cmd, i);
          int rowsAffected = cmd.ExecuteNonQuery();

          if (cmd.Parameters.Contains("@identity"))
            identityValue = cmd.Parameters["@identity"].Value;
        }
      }

      return identityValue;
    }

    public int Update<T>(DbSet<T> dbSet)
    {
      this._updateAttempted = true;
      CheckConnection();
      this.DbFunction = DbFunction.Update;
      int rowsUpdated = 0;
      Type t = GetEntityType(dbSet.DbQuery.TableName);

      using (SqlCommand cmd = new SqlCommand("", this.Connection))
      {
        if (this.Transaction != null)
          cmd.Transaction = this.Transaction;

        for (int i = 0; i < dbSet.Count; i++)
        {
          cmd.Parameters.Clear();
          DbHelper.PrepareUpdateCommand(dbSet[i], dbSet.DbTable, cmd, i);
          rowsUpdated += cmd.ExecuteNonQuery();
        }
      }

      return rowsUpdated;
    }

    public int Update<T>(DbRow<T> dbRow)
    {
      this._updateAttempted = true;
      CheckConnection();
      this.DbFunction = DbFunction.Update;
      int rowsUpdated = 0;
      Type t = GetEntityType(dbRow.DbQuery.TableName);

      using (SqlCommand cmd = new SqlCommand("", this.Connection))
      {
        if (this.Transaction != null)
          cmd.Transaction = this.Transaction;

        cmd.Parameters.Clear();
        DbHelper.PrepareUpdateCommand(dbRow.DbEntity, dbRow.DbTable, cmd, 0);
        rowsUpdated += cmd.ExecuteNonQuery();
      }

      return rowsUpdated;
    }

    public void CheckConnection()
    {
      if (this.Connection != null)
        if (this.Connection.State == ConnectionState.Open)
          return;

      if (this._connectionString == null)
        throw new Exception("Connection string is null.");

      if (this._connectionString.IsNotBlank())
      {
        this.Connection = new SqlConnection(_connectionString);
        this.Connection.Open();
      }
    }

    //public void LoadEntityField(DbEntityBase entity, object value, DataColumn c)
    //{
    //    string propertyName = c.ColumnName;
    //    string originalFieldName = "__" + c.ColumnName.Substring(0, 1).ToLower() + c.ColumnName.Substring(1);

    //    DbColumn dbColumn = entity.DbColumnSet[c.ColumnName];
    //    PropertyInfo pi = entity.GetType().GetProperty(c.ColumnName);
    //    FieldInfo ofi = entity.GetType().GetField(originalFieldName, BindingFlags.Instance | BindingFlags.NonPublic);

    //    if (pi != null)
    //    {
    //        string valueType = value.GetType().Name;
    //        if (valueType == "DBNull")
    //        {
    //            pi.SetValue(entity, null, null);
    //        }
    //        else
    //        {
    //            switch (c.ColumnName)
    //            {
    //                case "time_7_n":
    //                case "time_7_nn":
    //                case "datetimeoffset_7_n":
    //                case "datetimeoffset_7_nn":
    //                case "timestamp_n":
    //                case "real_n":
    //                case "real_nn":
    //                case "sql_variant_n":
    //                case "sql_variant_nn":

    //                    break;

    //                default:
    //                    string propertyType = DbHelper.GetPropertyType(pi.PropertyType);
    //                    switch (propertyType)
    //                    {
    //                        case "Int32":
    //                            int int32 = Convert.ToInt32(value);
    //                            pi.SetValue(entity, int32, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, int32);
    //                            }
    //                            break;

    //                        case "String":
    //                            string stringValue = Convert.ToString(value);
    //                            pi.SetValue(entity, stringValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, stringValue);
    //                            }
    //                            break;

    //                        case "Int64":
    //                            Int64 int64 = Convert.ToInt64(value);
    //                            pi.SetValue(entity, Convert.ToInt64(int64), null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, int64);
    //                            }
    //                            break;

    //                        case "Byte[]":
    //                            pi.SetValue(entity, (byte[])value, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, (byte[])value);
    //                            }
    //                            break;

    //                        case "Boolean":
    //                            bool boolValue = Convert.ToBoolean(value);
    //                            pi.SetValue(entity, boolValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, boolValue);
    //                            }
    //                            break;

    //                        case "DateTime":
    //                            DateTime datetimeValue = Convert.ToDateTime(value);
    //                            pi.SetValue(entity, datetimeValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, datetimeValue);
    //                            }
    //                            break;

    //                        case "TimeSpan":
    //                            throw new Exception("Cannot set property value for column '" + c.ColumnName + "' with data type '" + propertyType + "'.");

    //                        case "Decimal":
    //                            decimal decimalValue = Convert.ToDecimal(value);
    //                            pi.SetValue(entity, decimalValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, decimalValue);
    //                            }
    //                            break;

    //                        case "Single":
    //                            Single singleValue = Convert.ToSingle(value);
    //                            pi.SetValue(entity, singleValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, singleValue);
    //                            }
    //                            break;

    //                        case "Image":
    //                            Image imageValue = Image.FromStream(new MemoryStream((byte[])value));
    //                            pi.SetValue(entity, imageValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, imageValue);
    //                            }
    //                            break;

    //                        case "Object":
    //                            pi.SetValue(entity, value, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, value);
    //                            }
    //                            break;

    //                        case "XElement":
    //                            XElement xElementValue = XElement.Parse(Convert.ToString(value));
    //                            pi.SetValue(entity, xElementValue, null);
    //                            if (entity.IsLoadingForUpdate)
    //                            {
    //                                if (ofi != null)
    //                                    ofi.SetValue(entity, xElementValue);
    //                            }
    //                            break;

    //                        default:
    //                            throw new Exception("Cannot set property value for column '" + c.ColumnName + "' with data type '" + propertyType + "'.");

    //                    }
    //                    break;
    //            }
    //        }
    //    }
    //}

    public Type GetEntityType(string tableName)
    {
      Type entityType = null;
      string className = tableName;

      foreach (Type typ in DbHelper.Types)
      {
        if (typ.Name == className)
        {
          entityType = typ;
          break;
        }
      }

      if (entityType == null)
        throw new Exception("Could not determine the data type for the table named '" + tableName + "'.");

      return entityType;
    }

    private Type[] GetTypes()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      Type[] types = assembly.GetTypes();

      return types;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposeManagedResources)
    {
      if (!this.isDisposed)
      {
        if (disposeManagedResources)
        {
          if (this.Connection != null)
          {
            if (this.Connection.State != ConnectionState.Closed)
              this.Connection.Close();

            this.Connection.Dispose();
            this.Connection = null;
          }
        }
        this.isDisposed = true;
      }
    }
  }

  public class DbRow<T>
  {
    public DbQuery<T> DbQuery { get; set; }
    public DbTable DbTable { get; set; }
    public DbContext DbContext { get; set; }
    public DbEntityBase DbEntity { get; set; }

    public List<DbSet<T>> Joined { get; set; }

    public DbRow(DbContext db)
    {
      this.DbContext = db;

      this.DbQuery = new DbQuery<T>(this.DbContext, this);
      this.DbQuery.TableName = DbHelper.GetTableNameFromType(this);

      if (DbHelper.DbTableSet.ContainsKey(this.DbQuery.TableName))
        this.DbTable = DbHelper.DbTableSet[this.DbQuery.TableName];

      this.DbEntity = null;
      this.Joined = new List<DbSet<T>>();
    }
  }

  public class DbSet<T> : List<T>
  {
    public DbQuery<T> DbQuery { get; set; }
    public DbTable DbTable { get; set; }
    public DbContext DbContext { get; set; }

    public List<DbSet<T>> Joined { get; set; }

    public DbSet(DbContext db)
    {
      this.DbContext = db;

      this.DbQuery = new DbQuery<T>(this.DbContext, this);
      this.DbQuery.TableName = DbHelper.GetTableNameFromType(this);

      if (DbHelper.DbTableSet.ContainsKey(this.DbQuery.TableName))
        this.DbTable = DbHelper.DbTableSet[this.DbQuery.TableName];

      this.Joined = new List<DbSet<T>>();
    }
  }

  public class DbIntKeyedSet<T> : Dictionary<int, T>
  {
  }

  public class DbEntityBaseSet : List<DbEntityBase>
  {
  }

  public class DbEntityBase
  {
    public string AliasedTypeName { get; set; }
    public string TableName { get; set; }
    public DbTable DbTable { get; set; }
    public DbEntityBase OriginalValue { get; set; }

    public virtual bool IsLoadingForUpdate { get; set; }

    private bool _isLoadedForUpdate;
    public bool IsLoadedForUpdate
    {
      get { return _isLoadedForUpdate; }
      set { _isLoadedForUpdate = value; }
    }

    public bool IsUpdated
    {
      get { return this.IsThisEntityUpdated(); }
    }

    public DbEntityBase()
    {
      string className = this.GetType().Name;
      this.TableName = className;
      this.DbTable = DbHelper.DbTableSet[this.TableName];
    }


    private bool IsThisEntityUpdated()
    {
      PropertyInfo[] piSet = this.GetType().GetProperties();

      foreach (PropertyInfo pi in piSet)
      {
        object[] attrs = pi.GetCustomAttributes(false);

        if (pi.GetCustomAttributes(typeof(IsDbColumn), false).Count() > 0)
        {
          string propertyType = pi.PropertyType.Name;
          string propertyName = pi.Name;
          string origFieldName = "__" + propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
          FieldInfo ofi = this.GetType().GetField(origFieldName, BindingFlags.NonPublic | BindingFlags.Instance);

          object currentValue = pi.GetValue(this, null);
          object originalValue = ofi.GetValue(this);

          if (pi.PropertyType.IsGenericType)
          {
            Type underlyingType = Nullable.GetUnderlyingType(pi.PropertyType);
            propertyType = underlyingType.Name;
          }

          if (DbHelper.ValueIsUpdated(propertyName, propertyType, originalValue, currentValue))
            return true;
        }
      }

      return false;
    }

    public DbEntityBase MapFromObject(object source, string context)
    {
      string aliasedSourceTypeName = g.ObjectMap.GetAliasedTypeName(source);

      if (this.AliasedTypeName == null)
        this.AliasedTypeName = g.ObjectMap.GetAliasedTypeName(this);

      string sourceType = source.GetType().Name;
      string destType = this.GetType().Name;

      PropertyInfo[] sourceProps = source.GetType().GetProperties();
      PropertyInfo[] destProps = this.GetType().GetProperties();
      List<PropertyInfo> unmappedProperties = new List<PropertyInfo>();
      foreach (PropertyInfo pi in destProps)
        unmappedProperties.Add(pi);

      foreach (PropertyInfo dpi in destProps)
      {
        foreach (PropertyInfo spi in sourceProps)
        {
          string sourcePropType = spi.PropertyType.Name;
          if (sourcePropType.Contains("Nullable"))
            sourcePropType = Nullable.GetUnderlyingType(spi.PropertyType).Name;

          string destPropType = dpi.PropertyType.Name;
          if (destPropType.Contains("Nullable"))
            destPropType = Nullable.GetUnderlyingType(dpi.PropertyType).Name;

          if (dpi.Name == spi.Name && sourcePropType == destPropType)
          {
            dpi.SetValue(this, spi.GetValue(source, null), null);
            unmappedProperties.Remove(dpi);
            break;
          }
        }
      }

      ObjectMapContext omc;
      if (unmappedProperties.Count > 0)
      {
        context = context.SetIfBlank("Default");
        if (g.ObjectMap.ContainsKey(context))
        {
          omc = g.ObjectMap.GetContext(context);

          foreach (PropertyInfo unmappedProperty in unmappedProperties)
          {
            string destPropString = this.AliasedTypeName + "." + unmappedProperty.Name;
            //omc.MapProperty(this, unmappedProperty, destPropString, source, aliasedSourceTypeName);
          }
        }
      }

      return this;
    }
  }

  public class DbResultSet : List<DbQueryResult>
  {
    public DbContext DbContext { get; set; }
    public DbColumnSet DbColumnSet { get; set; }
    public string Query { get; set; }

    public DbResultSet(DbContext db)
    {
      this.DbContext = db;
      this.DbColumnSet = new DbColumnSet();
      this.Query = String.Empty;
    }
  }

  public class DbResult
  {
    public DbContext DbContext { get; set; }
    public string Query { get; set; }
    public DbStatusCode DbStatusCode { get; set; }
    public string Message { get; set; }
    public int RowsAffected { get; set; }
    public object ScalarValue { get; set; }

    public DbResult(DbContext db)
    {
      this.DbContext = db;
      this.Query = String.Empty;
      this.DbStatusCode = DbStatusCode.NotSet;
      this.Message = String.Empty;
      this.RowsAffected = 0;
      this.ScalarValue = null;
    }
  }

  public class DbTableSet : SortedList<string, DbTable>
  {
  }

  public class DbTable
  {
    public string DatabaseName { get; set; }
    public string SchemaName { get; set; }
    public string TableName { get; set; }
    public string TableNameSansSchema { get { return Get_TableNameSansSchema(); } }
    public string TableType { get; set; }
    public DateTime CreateDT { get; set; }
    public DateTime ModifyDT { get; set; }

    public DbColumnSet DbColumnSet { get; set; }
    public DbConstraintSet DbConstraintSet { get; set; }
    public DbColumn PrimaryKeyColumn { get; set; }
    public DbColumn IdentityColumn { get; set; }

    public bool HasPrimaryKeyColumn
    {
      get { return this.PrimaryKeyColumn == null ? false : true; }
    }

    public bool HasIdentityColumn
    {
      get { return this.IdentityColumn == null ? false : true; }
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

      string sql = "SELECT " + g.crlf + 
                    "  c.[name] AS [Name], " + g.crlf +
                    "  c.[column_id] AS [ColumnId], " + g.crlf +
                    "  c.[system_type_id] AS [SystemTypeId], " + g.crlf +
                    "  c.[user_type_id] AS [UserTypeId], " + g.crlf +
                    "  c.[max_length] AS [MaxLength], " + g.crlf +
                    "  c.[precision] AS [Precision], " + g.crlf +
                    "  c.[scale] AS [Scale], " + g.crlf +
                    "  c.[is_nullable] AS [IsNullable], " + g.crlf +
                    "  c.[is_identity] AS [IsIdentity], " + g.crlf +
                    "  i.[is_primary_key] AS [IsPrimaryKey], " + g.crlf +
                    "  c.[default_object_id] as [DefaultObjectId] " + g.crlf +
                    "FROM sys.columns c " +
                    "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " + g.crlf +
                    "LEFT OUTER JOIN sys.indexes i on ic.object_id = i.object_id AND ic.index_id = i.index_id " + g.crlf +
                    "WHERE c.object_id = OBJECT_ID('" + this.TableName + "') " + g.crlf +
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

  public class DbColumnSet : Dictionary<string, DbColumn>
  {
  }

  public class DbColumn
  {
    public DbTable Table { get; set; }
    public DbEntityBase Row { get; set; }
    public string Name { get; set; }
    public DbType DbType { get; set; }
    public int ColumnId { get; set; }
    public int SystemTypeId { get; set; }
    public int UserTypeId { get; set; }
    public int MaxLength { get; set; }
    public int Precision { get; set; }
    public int Scale { get; set; }
    public bool IsNullable { get; set; }
    public bool IsNullInDB { get; set; }
    public bool IsIdentity { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool HasDefaultValue { get; set; }

    public bool IsUpdated
    {
      get { return this.IsColumnUpdated(); }
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

      if (DbHelper.ValueIsUpdated(propertyName, propertyType, originalValue, currentValue))
        return true;

      return false;
    }
  }

  public class DbConstraintSet : List<DbConstraint>
  {
  }

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

  public class DbTypeSet : SortedList<int, DbType>
  {
    public void Load(SqlConnection Connection)
    {
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

  public class DbType
  {
    public SqlDbType SqlDbType { get; set; }
    public string TypeName { get; set; }
    public int SystemTypeId { get; set; }
    public int UserTypeId { get; set; }
    public int MaxLength { get; set; }
    public int Precision { get; set; }
    public int Scale { get; set; }
    public bool IsNullable { get; set; }

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

  public class DbQuery<T>
  {
    public DbSet<T> DbSet { get; set; }
    public DbRow<T> DbRow { get; set; }
    public DbContext DbContext;
    public string TableName { get; set; }
    public string WhereClause { get; set; }
    public string OrderByClause { get; set; }
    public string ColumnList { get; set; }
    public bool IsSet { get; set; }

    public DbQuery(DbContext dbContext, DbRow<T> dbRow)
    {
      this.DbRow = dbRow;
      this.DbSet = null;
      this.DbContext = dbContext;
      this.TableName = String.Empty;
      this.WhereClause = String.Empty;
      this.OrderByClause = String.Empty;
      this.ColumnList = String.Empty;
      IsSet = false;
    }

    public DbQuery(DbContext dbContext, DbSet<T> dbSet)
    {
      this.DbRow = null;
      this.DbSet = dbSet;
      this.DbContext = dbContext;
      this.TableName = String.Empty;
      this.WhereClause = String.Empty;
      this.OrderByClause = String.Empty;
      this.ColumnList = String.Empty;
      IsSet = true;
    }

    public string BuildQuery()
    {
      switch (this.DbContext.DbFunction)
      {
        case DbFunction.Select:
          return BuildSelectQuery();
      }

      throw new Exception("Invalid DbFunction found in the BuildQuery method");
    }

    private string BuildSelectQuery()
    {
      return " SELECT " + this.GetColumnList() + " " +
             " FROM [" + this.TableName + "] " +
             this.GetWhereClause() + " " +
             this.GetOrderByClause() + " ";
    }

    private string GetColumnList()
    {
      if (this.ColumnList.Trim().Length > 0)
        return " " + this.ColumnList + " ";
      else
        return " * ";
    }

    private string GetWhereClause()
    {
      if (this.WhereClause.Trim().Length > 0)
        return " " + this.WhereClause + " ";
      else
        return String.Empty;
    }

    private string GetOrderByClause()
    {
      if (this.OrderByClause.Trim().Length > 0)
        return " " + this.OrderByClause + " ";
      else
        return String.Empty;
    }


  }

  public enum DbRelOp
  {
    NotSet,
    Equals,
    GreaterThan,
    LessThan,
    GreaterThanOrEqualTo,
    LessThanOrEqualTo,
    Like,
    OpenParen,
    CloseParen,
    And,
    Or
  }

  public enum DbRelSpecType
  {
    NotSet,
    ColumnSpec,
    Logical
  }

  public enum DbStatusCode
  {
    NotSet,
    Success,
    Failed
  }

  public class DbCriteria
  {
    public DbRelSpecSet DbRelSpecSet { get; set; }

    public DbCriteria()
    {
      this.DbRelSpecSet = new DbRelSpecSet();
    }
  }

  public class DbRelSpecSet : List<DbRelSpec>
  {
  }

  public class DbRelSpec
  {
    public DbRelSpecType DbRelSpecType { get; set; }
    public string ColumnName { get; set; }
    public bool UseNegation { get; set; }
    public DbRelOp DbRelOp { get; set; }
    public object Value { get; set; }

    public DbRelSpec()
    {
      this.DbRelSpecType = DbRelSpecType.NotSet;
      this.ColumnName = String.Empty;
      this.UseNegation = false;
      this.DbRelOp = DbRelOp.NotSet;
      this.Value = null;
    }
  }

  public class DbQueryResult : Dictionary<string, object>
  {

  }

  public static class DbExtensionMethods
  {
    public static DbResultSet RunQuery(this DbResultSet resultSet, string query)
    {
      if (query.Trim().IsBlank())
        return resultSet;

      resultSet.Query = query;

      resultSet.DbContext.Load(resultSet);
      return resultSet;
    }

    public static DbResult RunNonQuery(this DbResult result, string query)
    {
      if (query.Trim().IsBlank())
        return result;

      result.Query = query;

      result.DbContext.ExecuteNonQuery(result);
      return result;
    }

    public static DbResult RunScalar(this DbResult result, string query)
    {
      if (query.Trim().IsBlank())
        return result;

      result.Query = query;

      result.DbContext.ExecuteScalar(result);
      return result;
    }

    public static DbSet<T> Where<T>(this DbSet<T> dbSet, string whereClause)
    {
      if (whereClause.Trim().IsBlank())
        return dbSet;

      dbSet.DbQuery.WhereClause = " WHERE " + whereClause;
      return dbSet;
    }

    public static DbSet<T> And<T>(this DbSet<T> dbSet, string andClause)
    {
      dbSet.DbQuery.WhereClause += " AND " + andClause;
      return dbSet;
    }

    public static DbSet<T> OrderBy<T>(this DbSet<T> dbSet, string orderByClause)
    {
      dbSet.DbQuery.OrderByClause = " ORDER BY " + orderByClause;
      return dbSet;
    }

    public static DbSet<T> Select<T>(this DbSet<T> dbSet, string columnList)
    {
      dbSet.DbQuery.ColumnList = columnList;
      return dbSet.Select();
    }

    public static DbSet<T> Select<T>(this DbSet<T> dbSet)
    {
      dbSet.DbContext.Load(dbSet);
      return dbSet;
    }

    public static DbIntKeyedSet<T> ToIntKeyedSet<T>(this DbSet<T> dbSet)
    {
      DbIntKeyedSet<T> dbIntKeyedSet = new DbIntKeyedSet<T>();

      string primaryKeyName = dbSet.DbTable.PrimaryKeyColumn.Name;
      PropertyInfo pi = typeof(T).GetProperty(primaryKeyName);

      foreach (T t in dbSet)
      {
        object o = pi.GetValue(t, null);
        Int32 key = Convert.ToInt32(o, null);
        dbIntKeyedSet.Add(key, t);
      }

      return dbIntKeyedSet;
    }

    public static T SelectUnique<T>(this DbSet<T> dbSet)
    {
      dbSet.DbContext.Load(dbSet);
      return dbSet.FirstOrDefault();
    }

    public static DbEntityBase SingleOrDefault<T>(this DbSet<T> dbSet)
    {
      if (dbSet.Count == 1)
        return dbSet.First() as DbEntityBase;

      return null;
    }
  }

  public class _db
  {
    public static string nl = g.crlf;
    public static string nl2 = g.crlf + g.crlf;
  }

  public enum DbFunction
  {
    NotSet,
    Select,
    Insert,
    Update,
    Delete
  }

  public enum ConstraintType
  {
    NotSet,
    PRIMARY_KEY,
    FOREIGN_KEY,
    UNIQUE,
    UNKNOWN
  }


}
