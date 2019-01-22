using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.Controls
{
  public class ControlLineSet : SortedList<int, ControlLine>
  {
    public int LastLine { get { return Get_LastLine(); } }

    private int Get_LastLine()
    {
      if (this.Count == 0)
        return -1;

      var controlLine = this.Last();
      return controlLine.Key;
    }

  }
}
