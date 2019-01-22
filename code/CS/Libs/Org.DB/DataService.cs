using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Org.GS;
using Org.GS.Configuration;

namespace Org.DB
{
  public class DataService
  {
    public static void TestConnection(ConfigDbSpec configDbSpec)
    {
      try
      {
        string connString = configDbSpec.ConnectionString;
        using (SqlConnection conn = new SqlConnection(connString))
        {
          string sql = "SELECT database_id FROM sys.databases WHERE [name]='" + configDbSpec.DbName + "'";
          using (SqlCommand cmd = new SqlCommand(sql, conn))
          {
            conn.Open();
            SqlDataReader rsMed = cmd.ExecuteReader();

            if (!rsMed.HasRows)
              throw new Exception("Failed to connect to the '" + configDbSpec.DbName + "' database.");

            conn.Close();
            conn.Dispose();
          }
        }

      }
      catch (Exception ex)
      {
        throw new Exception("Failed to connect - Inner exception message is:" +
                            g.crlf2 + ex.Message, ex);
      }
    }
  }
}



