using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class LevelLimits
  {
    [XMap(XType = XType.Element, ClassName = "LevelIndex")]
    public LevelIndex LevelStart { private get; set; }

    [XMap(XType = XType.Element, ClassName = "LevelIndex")]
    public LevelIndex LevelEnd { private get; set; }

    private int _levelStartIndex;
    private int _lineFindStartIndex;

    private int _levelEndIndex;

    public int GetLevelStartIndex(List<Text> lines, RecogSpecSet recogSpecSet)
    {
      if (this.LevelStart == null)
        return _levelStartIndex = -1;

      _levelStartIndex = this.LevelStart.GetLevelIndex(lines, recogSpecSet);
      return _levelStartIndex;
    }

    public int GetLevelEndIndex(List<Text> lines, RecogSpecSet recogSpecSet)
    {
      if (this.LevelEnd == null || _levelStartIndex == null || _levelStartIndex == -1)
        return _levelEndIndex = -1;

      _levelEndIndex = this.LevelEnd.GetLevelIndex(lines, recogSpecSet, this.LevelStart.LineFindIndex + 1);
      return _levelEndIndex;
    }
  }
}
