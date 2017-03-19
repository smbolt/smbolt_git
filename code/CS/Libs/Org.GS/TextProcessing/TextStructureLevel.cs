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
  public class TextStructureLevel
  {
    [XMap(IsKey = true, IsRequired = true)]
    public int Level { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "LevelLimits", WrapperElement = "LevelLimitsSet")]
    public LevelLimitsSet LevelLimitsSet { get; set; }

    public void SetTextLevel(Text text, RecogSpecSet recogSpecSet)
    {
      List<Text> lines = text.Lines;
      List<Text> levelLines;
      string levelText;

      //foreach (var limits in this.LevelLimitsSet)
      //{
      //  int levelStartIndex = limits.GetLevelStartIndex(lines, recogSpecSet);
      //  int levelEndIndex = limits.GetLevelEndIndex(lines, recogSpecSet);
        
      //  while (levelStartIndex != -1)
      //  {
      //    if (levelEndIndex == -1)
      //    {
      //      levelLines = lines.Where((value, index) => index >= levelStartIndex).ToList();
      //      levelText = levelLines.TextListToString();
      //      text.TextList.Add(new Text(levelText, text));
      //      break;
      //    }         

      //    levelLines = lines.Where((value, index) => index >= levelStartIndex && index <= levelEndIndex).ToList();
      //    levelText = levelLines.TextListToString();
      //    text.TextList.Add(new Text(levelText, text));

      //    lines = lines.Where((value, index) => index < levelStartIndex || index > levelEndIndex).ToList();

      //    levelStartIndex = limits.GetLevelStartIndex(lines, recogSpecSet);
      //    levelEndIndex = limits.GetLevelEndIndex(lines, recogSpecSet);
      //  }
      //}
    }
  }
}
