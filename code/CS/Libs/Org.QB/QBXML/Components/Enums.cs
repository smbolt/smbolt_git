using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  public enum TransactionType
  {
    AccountQueryRq,
    CustomerAddRq,
    CustomerQueryRq,
    EmployeeQueryRq
  }

  public enum MetaData
  {
    NoMetaData,
    MetaDataOnly,
    MetaDataAndResponseData
  }

  public enum Iterator
  {
    Continue,
    Start,
    Stop
  }

  public enum ActiveStatus
  {
    All,
    ActiveOnly,
    InactiveOnly
  }

  public enum MatchCriterion
  {
    Contains,
    StartsWith,
    EndsWith
  }

  public enum Operator
  {
    Equal,
    GreaterThan,
    GreaterThanEqual,
    LessThan,
    LessThanEqual
  }

  public enum JobStatus
  {
    NoValue = 0,
    Awarded,
    Closed,
    InProgress,
    None,
    NotAwarded,
    Pending
  }

  public enum DeliveryMethod
  {
    Email,
    Fax,
    Print
  }
}
