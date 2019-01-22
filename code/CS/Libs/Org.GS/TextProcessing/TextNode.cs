using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TextNode 
  {
    public string Key { get; set; }
    public string Text { get; set; }
    public bool ExpectedOutcome { get; set; }

    public TextNode()
    {
    }

    public TextNodeSet GetTextNodeSet(string[] textNodes)
    {
      var textNodeSet = new TextNodeSet();
      int keyCounter = 0;
      foreach (var line in textNodes)
      {
        if (line.Length == 0)
          continue;
        string[] tokens = line.Split(Constants.WhiteSpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
        var textNode = new TextNode();
        textNode.Key = tokens[0] + keyCounter;
        textNode.Text = tokens[1];
        textNode.ExpectedOutcome = tokens[2].ToBoolean();
        textNodeSet.Add(keyCounter, textNode);
        keyCounter ++;
      }
      return textNodeSet;
    }

    // Don't know if I need another method for getting TextNodes yet..Still in progress;

    //public TextNodeSet GetSpecificTextNodeSet(string[] textNodes, List<string> testSets, List<string> tests, Dictionary<string, string> testItems)
    //{
      
    //  foreach (var node in textNodes)
    //  {
    //    string[] tokens = node.Split(Constants.WhiteSpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
    //    string keyPart1 = tokens[0];
    //    string keyPart2 = tokens[1];
    //    string keyPart3 = tokens[2];
    //    string key = keyPart1 + "." + keyPart2 + "." + keyPart3;

    //    //if (node.Key.Contains(key))
    //    //  cboTestData.Items.Add(node.Text.Replace("\"", ""));
    //  }


    //  var textNodeSet = new TextNodeSet();
    //  int keyCounter = 0;
    //  foreach (var line in textNodes)
    //  {
    //    if (line.Length == 0)
    //      continue;
    //    string[] tokens = line.Split(Constants.WhiteSpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
    //    var textNode = new TextNode();
    //    textNode.Key = tokens[0] + keyCounter;
    //    textNode.Text = tokens[1];
    //    textNode.ExpectedOutcome = tokens[2].ToBoolean();
    //    textNodeSet.Add(keyCounter, textNode);
    //    keyCounter ++;
    //  }
    //  return textNodeSet;
    //}
  }
}
