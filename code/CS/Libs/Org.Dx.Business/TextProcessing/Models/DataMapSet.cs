using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "DataMap", WrapperElement = "DataMapSet")]
  public class DataMapSet : List<DataMap>
  {
    [XMap(IsRequired = true)]
    public int UnitLevel { get; set; }

    public List<StatementFile> GetStatementFiles(Text text, RecogSpecSet recogSpecSet, string formatName)
    {
      List<Text> reportUnits = GetReportUnits(new List<Text> { text }, 0, formatName);
      var statementFiles = new List<StatementFile>();

      foreach (var reportUnit in reportUnits)
      {
        var statementFile = new StatementFile();
        foreach (var dataMap in this)
          statementFile.StatementDataSet.Add(dataMap.GetStatementData(reportUnit, recogSpecSet, formatName));
        statementFiles.Add(statementFile);
      }

      return statementFiles;
    }

    private List<Text> GetReportUnits(List<Text> textList, int level, string formatName)
    {
      //if (level == UnitLevel)
      return textList;

      //List<Text> childList = new List<Text>();
      //foreach (var text in textList)
      //  childList.AddRange(text.TextList.Where(u => u.FormatName == formatName));

      //return GetReportUnits(childList, level + 1, formatName);
    }
  }
}
