using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XmlLineError
  {
    public int Level { get; set; }
    public string ErrorMessage { get; set; }
    public Direction Direction { get; set; }

    public XmlLineError(int level, string errorMessage, Direction direction = Direction.Forward)
    {
      this.Level = level;
      this.ErrorMessage = errorMessage;
      this.Direction = Direction;
    }
  }
}
