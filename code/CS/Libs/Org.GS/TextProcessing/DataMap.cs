using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)] 
  public class DataMap
  {
    [XMap(IsRequired = true)]
    public string TextLocation { get; set; }

    [XMap(XType = XType.Element, IsRequired = true)]
    public LevelIndex LevelIndex { get; set; }

    [XMap(IsRequired = true)]
    public string TokenIndex { get; set; }

    [XMap(Name="ExpectedTokenTypes")]
    public string ExpectedTokenTypesString { get; set; }

    private Dictionary<int, string> ExpectedTokenTypes { get { return LoadExpectedTokenTypes(); } }

    [XMap(IsRequired = true)]
    public string DataName { get; set; }

    [XMap(IsRequired = true)]
    public string DBDataType { get; set; }

    private Text _text;
    private RecogSpecSet _recogSpecSet;
    
    private Dictionary<int, string> LoadExpectedTokenTypes()
    {
      var expectedTokenTypes = new Dictionary<int, string>();

      string[] tokenTypes = this.ExpectedTokenTypesString.Split(',');

      for (int i = 0; i < tokenTypes.Count(); i++ )
      {
        switch (tokenTypes[i].Trim().ToLower())
        {
          case "int":
          case "decimal":
          case "datetime":
          case "currency":
            expectedTokenTypes.Add(i, tokenTypes[i].Trim().ToLower());
            break;

          case "":
            break;

          default:
            throw new Exception("DataMap is not configured to have an expected token type of '" + tokenTypes[i] + "'.");
        }
      }
      return expectedTokenTypes;
    }

    public StatementData GetStatementData(Text text, RecogSpecSet recogSpecSet, string formatName)
    {
      try
      {
        _text = text;
        _recogSpecSet = recogSpecSet;

        Text relevantText = GetTextByLocation();

        int lineIndex = LevelIndex.GetLevelIndex(relevantText.Lines, recogSpecSet);

        if (lineIndex == -1)
          throw new Exception("Could not locate desired line based on configurations.");

        string line = relevantText.Lines.ElementAt(lineIndex).RawText;
        string[] lineTokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

        //Check for Expected Token Types
        foreach (var tokenType in this.ExpectedTokenTypes)
        {
          if (lineTokens.Count() < tokenType.Key)
            throw new Exception(String.Format("Expected token index ({0}) is greater than the line token count ({1}).", tokenType.Key, lineTokens.Count()));

          string token = lineTokens[tokenType.Key];
        
          switch (tokenType.Value)
          {
            case "int":
              if (token.IsNotNumeric())
                throw new Exception(String.Format("Token '{0}' is not of the expected type '{1}'", token, tokenType.Value));
              break;

            case "decimal":
              if (!token.IsDecimal())
                throw new Exception(String.Format("Token '{0}' is not of the expected type '{1}'", token, tokenType.Value));
              break;

            case "datetime":
              DateTime tempDT;
              if (!DateTime.TryParse(token, out tempDT))
                throw new Exception(String.Format("Token '{0}' is not of the expected type '{1}'", token, tokenType.Value));
              break;

            case "currency":
              decimal tempDec;
              if (!Decimal.TryParse(token, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out tempDec))
                throw new Exception(String.Format("Token '{0}' is not of the expected type '{1}'", token, tokenType.Value));
              break;
          }
        }

        string dataValue = String.Empty;
        if (this.TokenIndex.IsNumeric())
        {
          if (this.TokenIndex.ToInt32() >= lineTokens.Count())
            throw new Exception(String.Format("Data Token Index ({0}) is greater than the number of tokens in the line ({1})." + g.crlf + "Line text: '{2}'", this.TokenIndex, lineTokens.Count(), line));
          dataValue = lineTokens[this.TokenIndex.ToInt32()];
        }
        else if (this.TokenIndex == "*")
        {
          dataValue = line;
        }

        return new StatementData(this.DataName, dataValue, this.DBDataType);
      }
      catch (Exception ex)
      {
        throw new Exception(String.Format("Format '{0}' : DataName '{1}'", formatName, this.DataName), ex);
      }
    }

    private Text GetTextByLocation()
    {
      string[] locationMap = this.TextLocation.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);

      Text locationText = _text;
      //foreach (var location in locationMap)
      //{
      //  if (location.IsNotNumeric())
      //    throw new Exception("'" + location + "' is not a valid character for data mapping. Full TextLocation is '" + this.TextLocation + "'.");

      //  int locIndex = location.ToInt32();

      //  if (locIndex >= locationText.TextList.Count)
      //    throw new Exception(String.Format("Location Index '{0}' is out of range for the Text object (TextList Count = '{1}')." + g.crlf +
      //                                      "Full TextLocation is '{2}'.", locIndex, locationText.TextList.Count, this.TextLocation));

      //  locationText = locationText.TextList.ElementAt(locIndex);
      //}
      return locationText;
    }
  }
}
