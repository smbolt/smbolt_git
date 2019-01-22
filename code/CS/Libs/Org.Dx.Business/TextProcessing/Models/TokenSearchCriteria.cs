using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business.TextProcessing
{
  public class TokenSearchCriteria
  {
    public Direction Direction {
      get;
      set;
    }
    public DataType DataType {
      get;
      set;
    }
    public string Pattern {
      get;
      set;
    }
    public bool Trim {
      get;
      set;
    }
    public bool TrimRight {
      get;
      set;
    }
    public bool TrimLeft {
      get;
      set;
    }
    public string TextToFind {
      get;
      set;
    }
    public bool MatchCase {
      get;
      set;
    }
    public bool IsRequired {
      get;
      set;
    }
    public string BeforeToken {
      get;
      set;
    }
    public string AfterToken {
      get;
      set;
    }
    public bool Join {
      get;
      set;
    }
    public PositionAt PositionAt {
      get;
      set;
    }
    public string DataName {
      get;
      set;
    }
    public string DefaultValue {
      get;
      set;
    }
    public string DataFormat {
      get;
      set;
    }
    public string Math {
      get;
      set;
    }
    public bool RemoveStoredToken {
      get;
      set;
    }
    public int StoredTokenIndex {
      get;
      set;
    }
    public string LiteralValue {
      get;
      set;
    }

    public TokenSearchCriteria()
    {
      this.Direction = Direction.Next;
      this.DataType = DataType.String;
      this.Pattern = String.Empty;
      this.Trim = true;
      this.TrimRight = false;
      this.TrimLeft = false;
      this.TextToFind = String.Empty;
      this.MatchCase = false;
      this.BeforeToken = String.Empty;
      this.AfterToken = String.Empty;
      this.Join = false;
      this.PositionAt = PositionAt.End;
      this.DataName = String.Empty;
      this.DefaultValue = String.Empty;
      this.DataFormat = String.Empty;
      this.Math = String.Empty;
      this.RemoveStoredToken = true;
      this.StoredTokenIndex = 0;
      this.LiteralValue = String.Empty;
    }
  }
}
