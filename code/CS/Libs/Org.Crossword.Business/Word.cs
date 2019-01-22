using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Crossword.Business
{
  public class Word
  {
    public int WordId {
      get;
      set;
    }
    public Direction Direction {
      get;
      set;
    }
    public int StartX {
      get;
      set;
    }
    public int? EndX {
      get;
      set;
    }
    public int StartY {
      get;
      set;
    }
    public int? EndY {
      get;
      set;
    }

    private string _xRange;
    private string _yRange;

    public Word(int wordId, Direction direction, string xRange, string yRange)
    {
      this.WordId = wordId;
      this.Direction = direction;
      _xRange = xRange;
      _yRange = yRange;

      if (_xRange.IsNumeric())
      {
        this.Direction = Direction.Across;
        this.StartX = _xRange.ToInt32();
        this.EndX = (int?) null;
        var y = _yRange.Split(Constants.DashDelimiter, StringSplitOptions.RemoveEmptyEntries);
        this.StartY = y[0].ToInt32();
        this.EndY = y[1].ToInt32();
      }
      else
      {
        this.Direction = Direction.Down;
        var x = _xRange.Split(Constants.DashDelimiter, StringSplitOptions.RemoveEmptyEntries);
        this.StartX = x[0].ToInt32();
        this.EndX = x[1].ToInt32();
        this.StartY = _yRange.ToInt32();
        this.EndY = (int?)null;
      }
    }
  }
}
