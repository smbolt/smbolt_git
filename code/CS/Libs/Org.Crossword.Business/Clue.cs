using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Crossword.Business
{
  public class Clue
  {
    public int WordId { get; set; }
    public int Number { get; set; }
    public int Format { get; set; }
    public string Phrase { get; set; }
    public Direction Direction { get; set; }

    public Clue(int wordId, int number, int format, string phrase, Direction direction)
    {
      this.WordId = wordId;
      this.Number = number;
      this.Format = format;
      this.Phrase = phrase;
      this.Direction = direction;
    }
  }
}
