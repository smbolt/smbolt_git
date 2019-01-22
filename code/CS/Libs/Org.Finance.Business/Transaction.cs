using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Finance.Business
{
  public class Transaction
  {
    public int Id {
      get;
      set;
    }
    public DateTime TransactionDate {
      get;
      set;
    }
    public int Year {
      get {
        return this.TransactionDate.Year;
      }
    }
    public int Month {
      get {
        return this.TransactionDate.Month;
      }
    }
    public int Day {
      get {
        return this.TransactionDate.Day;
      }
    }
    public DebitCredit DebitCredit {
      get;
      set;
    }
    public int TransTypeId {
      get;
      set;
    }
    public decimal Amount {
      get;
      set;
    }
    public decimal Balance {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public int CategoryId {
      get;
      set;
    }
    public int PayeeId {
      get;
      set;
    }
    public string Description {
      get;
      set;
    }
    public string OrigDescription {
      get;
      set;
    }
    public string Comment {
      get;
      set;
    }
    public string Report {
      get {
        return Get_Report();
      }
    }

    public Transaction()
    {
      Initialize();
    }

    public Transaction(DateTime transDate, decimal amount, decimal balance, string description)
    {
      Initialize();
      this.TransactionDate = transDate;
      this.Amount = amount;
      this.Balance = balance;
      this.Description = description.Trim();
      this.OrigDescription = this.Description;
      this.Comment = String.Empty;
      this.DebitCredit = amount > 0 ? DebitCredit.Credit : DebitCredit.Debit;
      this.TransTypeId = 99;
      this.CategoryId = 99999;
      this.PayeeId = 0;
    }

    private void Initialize()
    {
      this.Id = 0;
      this.TransactionDate = DateTime.MinValue;
      this.DebitCredit = DebitCredit.NotSet;
      this.TransTypeId = 99;
      this.Amount = 0;
      this.Balance = 0;
      this.IsActive = true;
      this.CategoryId = 99999;
      this.PayeeId = 1;
      this.Description = String.Empty;
      this.OrigDescription = String.Empty;
      this.Comment = String.Empty;
    }

    private string Get_Report()
    {
      return this.TransactionDate.ToString("yyyy-MM-dd") + "  " + this.DebitCredit.ToString() + "  " +
             this.Amount.ToString("###,##0.00") + g.crlf +
             this.Description + g.crlf + this.Comment;
    }

    public Transaction Clone()
    {
      var clone = new Transaction();
      clone.Id = this.Id;
      clone.TransactionDate = this.TransactionDate;
      clone.DebitCredit = this.DebitCredit;
      clone.TransTypeId = this.TransTypeId;
      clone.Amount = this.Amount;
      clone.Balance = this.Balance;
      clone.IsActive = this.IsActive;
      clone.CategoryId = this.CategoryId;
      clone.PayeeId = this.PayeeId;
      clone.Description = this.Description;
      clone.OrigDescription = this.OrigDescription;
      clone.Comment = this.Comment;
      return clone;
    }

  }
}
