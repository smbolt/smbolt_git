using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Crossword.Business
{
  public class Cell
  {
    public int X { get; set; }
    public int Y { get; set; }
    public string Solution { get; set; }
    public int? Number { get; set; }
    public bool HasNumber { get { return this.Number.HasValue; } }

    public Cell()
    {
    }

    public Cell(int x, int y, string solution, int? number = null)
    {
      this.X = x;
      this.Y = y;
      this.Solution = solution;
      this.Number = number;
    }

  }
}
