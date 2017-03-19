using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public enum FormatSpecType
  {
    Inclusion,
    Exclusion
  }

  public enum MatchType
  {
    Equals,
    StartsWith,
    EndsWith
  }

  public enum OffsetDir
  {
    None,
    Back,
    Forward
  }

  public enum OffsetType
  {
    Match,
    Scalar,
    MatchAndScalar
  }
}
