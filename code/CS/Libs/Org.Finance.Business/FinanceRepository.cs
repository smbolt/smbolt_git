using Org.GS.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using System.Data;

namespace Org.Finance.Business
{
  public class FinanceRepository : IDisposable
  {
    private SqlConnection _conn;
    private string _connectionString;
    private ConfigDbSpec _configDbSpec;


    public FinanceRepository(ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;

      if (!_configDbSpec.IsReadyToConnect())
        throw new Exception(configDbSpec + "' is not ready to connect.");

      _connectionString = _configDbSpec.ConnectionString;
    }

    public void InsertTransSet(TransactionSet transSet)
    {
      SqlTransaction sqlTrans = null;
      Transaction transaction = null;


      try
      {
        EnsureConnection();

        sqlTrans = _conn.BeginTransaction();

        foreach (var trans in transSet.Values)
        {

          string sql =
            "SELECT Id FROM [Finance].[dbo].[Transaction] " + g.crlf +
            "WHERE Id = " + trans.Id.ToString() + " ";

          using (var cmd = new SqlCommand(sql, _conn, sqlTrans))
          {
            var o = cmd.ExecuteScalar();

            if (o != null)
            {
              sql =
                "UPDATE [Finance].[dbo].[Transaction] " + g.crlf +
                "  SET OrigDescription = '" + trans.Description.Replace("'", " ") + "' " + g.crlf +
                "  WHERE Id = " + trans.Id.ToString() + " ";

              using (var cmdUpdate = new SqlCommand(sql, _conn, sqlTrans))
              {
                int rowsUpdated = cmdUpdate.ExecuteNonQuery();
                if (rowsUpdated != 1)
                {
                  throw new Exception("The number of rows updated should have been 1, but was " + rowsUpdated.ToString() +
                                      " when updating the description with sql statement '" + sql + "'.");
                }
                continue;
              }
            }
          }


          transaction = trans;

          sql =
            "INSERT INTO [Finance].[dbo].[Transaction] " + g.crlf +
            "( " + g.crlf +
            "  Id, " + g.crlf +
            "  TransactionDate, " + g.crlf +
            "  Year, " + g.crlf +
            "  Month, " + g.crlf +
            "  Day, " + g.crlf +
            "  DebitCredit, " + g.crlf +
            "  TransTypeId, " + g.crlf +
            "  Amount, " + g.crlf +
            "  Balance, " + g.crlf +
            "  IsActive, " + g.crlf +
            "  CategoryId, " + g.crlf +
            "  PayeeId, " + g.crlf +
            "  Description, " + g.crlf +
            "  OrigDescription, " + g.crlf +
            "  Comment " + g.crlf +
            ") " + g.crlf +
            "VALUES " + g.crlf +
            "( " + g.crlf +
            "  " + trans.Id.ToString() + ", " + g.crlf +
            "  '" + trans.TransactionDate.ToString("MM/dd/yyyy") + "', " + g.crlf +
            "  " + trans.TransactionDate.Year.ToString() + ", " + g.crlf +
            "  " + trans.TransactionDate.Month.ToString() + ", " + g.crlf +
            "  " + trans.TransactionDate.Day.ToString() + ", " + g.crlf +
            "  '" + trans.DebitCredit.ToString() + "', " + g.crlf +
            "  " + trans.TransTypeId.ToString() + ", " + g.crlf +
            "  " + trans.Amount.ToString() + ", " + g.crlf +
            "  " + trans.Balance.ToString() + ", " + g.crlf +
            "  " + (trans.IsActive ? "1" : "0") + ", " + g.crlf +
            "  " + trans.CategoryId.ToString() + ", " + g.crlf +
            "  " + trans.PayeeId.ToString() + ", " + g.crlf +
            "  '" + trans.Description.Trim() + "', " + g.crlf +
            "  '" + trans.Description.Trim() + "', " + g.crlf +
            "  '" + trans.Comment.Trim() + "' " + g.crlf +
            ") ";

          using (var cmd = new SqlCommand(sql, _conn, sqlTrans))
          {
            cmd.ExecuteNonQuery();
          }
        }

        sqlTrans.Commit();
      }
      catch (Exception ex)
      {
        sqlTrans.Rollback();
        throw new Exception("An exception occurred while attempting to insert a transaction set.", ex);
      }
    }

    public void DeleteTransactions()
    {
      try
      {
        EnsureConnection();

        var transSet = new TransactionSet();

        string sql =
          "DELETE FROM dbo.[Transaction] ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to delete all transactions.", ex);
      }
    }

    public TransactionSet GetTransactionSet()
    {
      try
      {
        EnsureConnection();

        var transSet = new TransactionSet();

        string sql =
          "SELECT Id AS Id, " + g.crlf +
          "  TransactionDate AS TransactionDate, " + g.crlf +
          "  Year AS Year, " + g.crlf +
          "  Month AS Month, " + g.crlf +
          "  Day AS Day, " + g.crlf +
          "  DebitCredit AS DebitCredit, " + g.crlf +
          "  TransTypeId AS TransTypeId, " + g.crlf +
          "  Amount AS Amount, " + g.crlf +
          "  Balance AS Balance, " + g.crlf +
          "  IsActive AS IsActive, " + g.crlf +
          "  CategoryId AS CategoryId, " + g.crlf +
          "  PayeeId AS PayeeId, " + g.crlf +
          "  Description AS Description, " + g.crlf +
          "  OrigDescription AS OrigDescription, " + g.crlf +
          "  Comment AS Comment " + g.crlf +
          "FROM dbo.[Transaction] " + g.crlf +
          "ORDER BY Id ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          var dt = ds.Tables[0];

          foreach (DataRow r in dt.Rows)
          {
            var entity = new Transaction();
            entity.Id = r["Id"].DbToInt32().Value;
            entity.TransactionDate = r["TransactionDate"].DbToDateTime().Value;
            entity.DebitCredit = g.ToEnum<DebitCredit>(r["DebitCredit"].DbToString());
            entity.TransTypeId = r["TransTypeId"].DbToInt32().Value;
            entity.Amount = r["Amount"].DbToDecimal().Value;
            entity.Balance = r["Balance"].DbToDecimal().Value;
            entity.IsActive = r["IsActive"].DbToBoolean().Value;
            entity.CategoryId = r["CategoryId"].DbToInt32().Value;
            entity.PayeeId = r["PayeeId"].DbToInt32().Value;
            entity.Description = r["Description"].DbToString();
            entity.OrigDescription = r["OrigDescription"].DbToString();
            entity.Comment = r["Comment"].DbToString();

            if (entity.CategoryId == 89001)
              continue;

            transSet.Add(entity.Id, entity);
          }
        }

        return transSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve the TransactionSet.", ex);
      }
    }

    public void UpdateTransactionDescription(int transId, string description)
    {
      try
      {
        EnsureConnection();

        string sql =
          "UPDATE dbo.[Transaction] " + g.crlf +
          "  SET Description = '" + description.Trim() + "' " + g.crlf +
          "  WHERE Id = " + transId.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = CommandType.Text;
          int rowsUpdated = cmd.ExecuteNonQuery();
          if (rowsUpdated != 1)
            throw new Exception("The number of rows updated should be 1 but is " + rowsUpdated.ToString() + ".");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update the description on transaction id " + transId.ToString() + ".", ex);
      }
    }

    public void UpdateTransaction(Transaction trans)
    {
      try
      {
        EnsureConnection();

        string sql =
          "UPDATE dbo.[Transaction] " + g.crlf +
          "  SET Description = '" + trans.Description.Trim() + "', " + g.crlf +
          "      CategoryId = " + trans.CategoryId.ToString() + " " + g.crlf +
          "  WHERE Id = " + trans.Id.ToString() + " ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = CommandType.Text;
          int rowsUpdated = cmd.ExecuteNonQuery();
          if (rowsUpdated != 1)
            throw new Exception("The number of rows updated should be 1 but is " + rowsUpdated.ToString() + ".");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update the transaction with transaction id " + trans.Id.ToString() + ".", ex);
      }
    }

    public void UpdateTransCategory(List<int> transIds, int categoryId)
    {
      try
      {
        EnsureConnection();

        string sql =
          "UPDATE dbo.[Transaction] " + g.crlf +
          "  SET CategoryId = " + categoryId.ToString() + " " + g.crlf +
          "  WHERE Id IN (" + transIds.ToDelimitedList(",") + ")";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = CommandType.Text;
          int rowsUpdated = cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to update the category id on " + transIds.Count.ToString() +
                            "to " + categoryId.ToString() + ".", ex);
      }
    }

    public CategorySet GetCategorySet()
    {
      try
      {
        EnsureConnection();

        var categorySet = new CategorySet();

        string sql =
          "SELECT Id AS Id, " + g.crlf +
          "  Name AS Name, " + g.crlf +
          "  IsTaxrelated AS IsTaxRelated " + g.crlf +
          "FROM dbo.Category " + g.crlf +
          "ORDER BY Id ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          var dt = ds.Tables[0];

          foreach (DataRow r in dt.Rows)
          {
            var entity = new Category();
            entity.Id = r["Id"].DbToInt32().Value;
            entity.Name = r["Name"].DbToString();
            entity.IsTaxRelated = r["IsTaxRelated"].DbToBoolean().Value;
            categorySet.Add(entity.Id, entity);
          }
        }

        return categorySet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve the CategorySet.", ex);
      }
    }

    public void InsertCategory(Category category)
    {
      try
      {
        EnsureConnection();

        string sql =
          "INSERT INTO [Finance].[dbo].[Category] " + g.crlf +
          "( " + g.crlf +
          "  Id, " + g.crlf +
          "  Name, " + g.crlf +
          "  IsTaxRelated " + g.crlf +
          ") " + g.crlf +
          "VALUES " + g.crlf +
          "( " + g.crlf +
          "  " + category.Id.ToString() + ", " + g.crlf +
          "  '" + category.Name.Trim() + "', " + g.crlf +
          "  " + (category.IsTaxRelated ? "1" : "0") + " " + g.crlf +
          ") ";

        using (var cmd = new SqlCommand(sql, _conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert the category.", ex);
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
