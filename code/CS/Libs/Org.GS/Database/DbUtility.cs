using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Database
{
  public class DbUtility : IDisposable
  {
    private SqlConnection _conn;
    private Assembly _dbClassesAssembly;

    public DbUtility(SqlConnection conn, Assembly dbClassesAssembly)
    {
      if (conn == null)
        throw new Exception("The SqlConnection parameter is null.");

      if (conn.State != ConnectionState.Open)
        throw new Exception("The SqlConnection object parameter is not open.");

      if (dbClassesAssembly == null)
        throw new Exception("The DbClassesAssembly parameter is null.");

      _conn = conn;
      _dbClassesAssembly = dbClassesAssembly;
    }

    public DbTableSet GetTableSet()
    {
      try
      {
        var tableTypes = this.GetTypes();
        var dbTypeSet = this.LoadDbTypes();
        var tableNames = tableTypes.ClassNames();
        var tables = LoadTables(tableNames);
        LoadColumns(tables, dbTypeSet);
        return tables;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build a DbTableSet for the list of tables.", ex);
      }
    }

    private DbTableSet LoadTables(List<string> tableNames)
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

      SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
      DataSet ds = new DataSet();
      da.Fill(ds);

      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow r = ds.Tables[0].Rows[i];

        DbTable t = new DbTable();

        t.DatabaseName = _conn.Database;

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
        {
          string tableName = t.TableName.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries).Last();

          if (tableNames.Contains(tableName))
            ts.Add(tableName, t);
        }
      }

      return ts;
    }

    private void LoadColumns(DbTableSet ts, DbTypeSet typeSet)
    {
      foreach (DbTable t in ts.Values)
      {
        t.Load(_conn, typeSet);
      }
    }

    private DbTypeSet LoadDbTypes()
    {
      DbTypeSet typeSet = new DbTypeSet();
      typeSet.Load(_conn);
      return typeSet;
    }

    private Type[] GetTypes()
    {
      Type[] types = _dbClassesAssembly.GetTypes();
      List<Type> typesToReturn = new List<Type>();

      foreach (Type t in types)
      {
        var attr = t.GetCustomAttributes().OfType<IsDbTable>().FirstOrDefault();  
        if (attr != null)
          typesToReturn.Add(t);
      }

      return typesToReturn.ToArray();
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

    public void Dispose()
    {
    }
  }
}
