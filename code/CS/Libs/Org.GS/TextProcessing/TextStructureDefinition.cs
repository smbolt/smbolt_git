using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "TextStructureLevel")]
  public class TextStructureDefinition : Dictionary<int, TextStructureLevel>
  {
    public TextSet GetLowerLevels(string rawText, RecogSpecSet recogSpecSet, string formatName)
    {
      Text text = new Text(rawText);
      text.FormatName = formatName;
      SetTextStructure(text, recogSpecSet);

      return text.TextSet;
    }
    
    public void SetTextStructure(Text text, RecogSpecSet recogSpecSet)
    {
      if (!this.ContainsKey(text.Level + 1))
        return;

      if (this[text.Level + 1].LevelLimitsSet == null)
        return;

      this[text.Level + 1].SetTextLevel(text, recogSpecSet);
      foreach (var child in text.TextSet.Values)
        SetTextStructure(child, recogSpecSet);
    }
  }
}
