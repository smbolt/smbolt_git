using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Org.CHCM.Business.Models;
using Org.GS.Configuration;
using Org.GS;

namespace Org.CHCM.Business
{
  public class CHCMRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;


    public CHCMRepository(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");
      _connectionString = _configDbSpec.ConnectionString;
    }

    public List<Person> GetPersons()
    {
      try
      {
        EnsureConnection();

        var persons = new List<Person>();

        string sql = "SELECT PersonId AS PersonId, " + g.crlf +
                     "  Prefix as Prefix, " + g.crlf +
                     "  FirstName AS FirstName, " + g.crlf +
                     "  MiddleName AS MiddleName, " + g.crlf +
                     "  LastName AS LastName, " + g.crlf +
                     "  Suffix AS Suffix, " + g.crlf +
                     "  IsActive AS IsActive, " + g.crlf +
                     "  SchoolGrade AS SchoolGrade, " + g.crlf +
                     "  BirthYear AS BirthYear, " + g.crlf +
                     "  BirthMonth AS BirthMonth, " + g.crlf +
                     "  BirthDay AS BirthDay, " + g.crlf +
                     "  UseBirthday AS UseBirthDay, " + g.crlf +
                     "  Age AS Age " + g.crlf +
                     "FROM dbo.Person " + g.crlf + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return persons;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var person = new Person();
            person.PersonId = r["PersonId"].DbToInt32().Value;
            person.Prefix = r["Prefix"].DbToString();
            person.FirstName = r["FirstName"].DbToString();
            person.MiddleName = r["MiddleName"].DbToString();
            person.LastName = r["LastName"].DbToString();
            person.Suffix = r["Suffix"].DbToString();
            person.FirstName = r["FirstName"].DbToString();
            person.IsActive = r["IsActive"].DbToBoolean().Value;
            person.BirthYear = r["BirthYear"].DbToInt32();
            person.BirthMonth = r["BirthMonth"].DbToInt32();
            person.BirthDay = r["BirthDay"].DbToInt32();
            person.SchoolGrade = r["SchoolGrade"].DbToChar();
            person.UseBirthday = r["UseBirthday"].DbToBoolean().Value;
            person.Age = r["Age"].DbToInt32();
            persons.Add(person);
          }
        }

        return persons;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve Persons from the database.", ex);
      }
    }

    private void EnsureConnection()
    {
      try
      {
        if (_conn == null)
          _conn = new SqlConnection(_connectionString);

        if (_conn.State != ConnectionState.Open)
          _conn.Open();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to ensure (or create) the database connection.", ex);
      }
    }

    public void Dispose()
    {
      if (_conn == null)
        return;

      if (_conn.State == ConnectionState.Open)
        _conn.Close();
      _conn.Dispose();
      _conn = null;
    }

  }
}
