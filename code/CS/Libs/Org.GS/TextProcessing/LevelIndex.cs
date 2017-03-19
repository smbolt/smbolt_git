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
  [XMap(XType = XType.Element)]
  public class LevelIndex
  {
    [XMap(IsRequired = true)]
    public int LineFind { get; set; }

    [XMap(DefaultValue = "Equals")]
    public MatchType FindMatchType { get; set; }

    [XMap(DefaultValue = "None")]
    public OffsetDir OffsetDir { get; set; }

    [XMap(DefaultValue = "Match")]
    public OffsetType OffsetType { get; set; }

    [XMap]
    public int? OffsetMatch { get; set; }

    [XMap(DefaultValue = "Equals")]
    public MatchType OffsetMatchType { get; set; }

    [XMap]
    public int? OffsetScalar { get; set; }

    public int LineFindIndex { get; private set; }

    public int GetLevelIndex(List<Text> lines, RecogSpecSet recogSpecSet)
    {
      //if (lines == null || lines.Count == 0)
        return -1;

      //string lineFindString = recogSpecSet[0].RecogLineSet[LineFind].Text;
      //int limitIndex = this.LineFindIndex = lines.IndexOf(lineFindString, this.FindMatchType);
      //limitIndex = this.OffsetIndex(lines, recogSpecSet, limitIndex);

      //return limitIndex;
    }

    public int GetLevelIndex (List<Text> lines, RecogSpecSet recogSpecSet, int startIndex)
    {
      //if (lines == null || lines.Count == 0)
        return -1;

      //string lineFindString = recogSpecSet[0].RecogLineSet[LineFind].Text;
      //int limitIndex = this.LineFindIndex = lines.IndexOf(lineFindString, this.FindMatchType, startIndex);
      //limitIndex = this.OffsetIndex(lines, recogSpecSet, limitIndex);

      //return limitIndex;
    }

    private int OffsetIndex(List<Text> lines, RecogSpecSet recogSpecSet, int limitIndex)
    {
      //if (limitIndex == -1)
      //  return limitIndex;

      //string lineFindString;

      //if (this.OffsetType == OffsetType.Match || this.OffsetType == OffsetType.MatchAndScalar)
      //{
      //  switch (this.OffsetDir)
      //  {
      //    case OffsetDir.Forward:
      //      if (this.OffsetMatch == null)
      //        throw new Exception("OffsetMatch cannot be null when OffsetDir is set to '" + this.OffsetDir.ToString() + "'. Related text starts with '" + lines.First().RawText);
      //      lineFindString = recogSpecSet[0].RecogLineSet[OffsetMatch.Value].Text;
      //      limitIndex = lines.IndexOf(lineFindString, this.OffsetMatchType, limitIndex + 1);
      //      break;

      //    case OffsetDir.Back:
      //      if (this.OffsetMatch == null)
      //        throw new Exception("OffsetMatch cannot be null when OffsetDir is set to '" + this.OffsetDir.ToString() + "'. Related text starts with '" + lines.First().RawText);
      //      lineFindString = recogSpecSet[0].RecogLineSet[OffsetMatch.Value].Text;
      //      limitIndex = lines.LastIndexOf(lineFindString, this.OffsetMatchType, limitIndex - 1, limitIndex);
      //      break;
      //  }
      //}

      //if (limitIndex == -1)
      //  return limitIndex;

      //if (this.OffsetType == OffsetType.Scalar || this.OffsetType == OffsetType.MatchAndScalar)
      //{
      //  if (!this.OffsetScalar.HasValue)
      //    throw new Exception("OffsetScalar cannot be null when OffsetType is set to '" + this.OffsetType.ToString() + "'. Related text starts with '" + lines.First().RawText);
      //  switch (this.OffsetDir)
      //  {
      //    case OffsetDir.Forward:
      //      limitIndex = limitIndex + OffsetScalar.Value;
      //      break;

      //    case OffsetDir.Back:
      //      limitIndex = limitIndex - OffsetScalar.Value;
      //      break;
      //  }
      //}

      return limitIndex;
    }
  }

  
}
