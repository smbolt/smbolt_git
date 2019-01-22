using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class TextPatternSet : Dictionary<int, TextPattern>
  {
    private string _text;

    public TextPatternSet(string text)
    {
      try
      {
        _text = text;
        this.FindPatterns();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to find patterns in text.", ex);
      }
    }

    private void FindPatterns()
    {
      var sortedTextLines = new SortedList<string, string>();
      string txt1 = _text.Replace("\r\n", "\n");
      string[] lines = txt1.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      for (int i = 0; i < lines.Length; i++)
      {
        string condensedText = lines[i].CondenseText();

        // should be optional to omit numbers or other patterns
        // drop lines that begin with numbers
        if (condensedText.IsNotBlank())
        {
          if (!Char.IsLetter(condensedText[0]))
            continue;
        }

        // should be optional # of characters
        // drop lines that don't have at least 25 non-blank characters
        if (condensedText.Length < 25)
          continue;

        int seq = 0;
        string keyText = condensedText.PadToLength(64);
        string key = keyText + "-" + seq.ToString("00000");
        while (sortedTextLines.ContainsKey(key))
        {
          seq++;
          key = keyText + "-" + seq.ToString("00000");
        }
        sortedTextLines.Add(key, i.ToString("00000") + "-" + lines[i].Trim());
      }

      // occurrenceCounter stores the number of times that a particular string occurs in the data
      var occurrenceCounter = new Dictionary<string, int>();
      foreach (var kvp in sortedTextLines)
      {
        string key = kvp.Key.Substring(0, 64);
        if (!occurrenceCounter.ContainsKey(key))
          occurrenceCounter.Add(key, 0);
        occurrenceCounter[key]++;
      }

      // multipleOccurrenceCounter is just the occurrence counter with all the single occurrences filtered out
      // when looking at it, it is interpreted as "this key" occurs "this many times" in the data
      var multipleOccurrenceCounter = new Dictionary<string, int>();
      foreach (var kvp in occurrenceCounter)
      {
        if (kvp.Value > 1)
        {
          multipleOccurrenceCounter.Add(kvp.Key, kvp.Value);
        }
      }

      var highestOccurrrences = new SortedList<int, List<string>>();
      foreach (var kvp in multipleOccurrenceCounter)
      {
        if (!highestOccurrrences.ContainsKey(kvp.Value))
          highestOccurrrences.Add(kvp.Value, new List<string>() {
          kvp.Key
        });
        else
          highestOccurrrences[kvp.Value].Add(kvp.Key);
      }


      var sortedCounter = new SortedList<int, int>();
      foreach (var kvp in occurrenceCounter)
      {
        if (kvp.Value > 1)
        {
          int keyCount = kvp.Value;
          if (!sortedCounter.ContainsKey(keyCount))
            sortedCounter.Add(keyCount, 0);
          sortedCounter[keyCount]++;
        }
      }

      // not sure how to optimize this.
      // when reading the meaning of sortedCounter (quick watch)

      // there are x items that occur y times

      // may want to just bring them all back and then select them from a grid.


      // not sure if we need this next block of code??

      int indexOfMax = -1;
      int maxOccurs = 0;
      for (int i = 0; i < sortedCounter.Count; i++)
      {
        if (sortedCounter.Values[i] > maxOccurs)
        {
          indexOfMax = i;
          maxOccurs = sortedCounter.Values[i];
        }
      }

      StringBuilder sb = new StringBuilder();

      var orderedLines = new Dictionary<int, SortedList<int, string>>();

      for (int i = sortedCounter.Count - 1; i > -1; i--)
      {
        int occurrences = sortedCounter.Keys[i];
        orderedLines.Add(occurrences, new SortedList<int, string>());
        foreach (var kvp in occurrenceCounter)
        {
          if (kvp.Value == occurrences)
          {
            string recurringLine = kvp.Key;
            foreach (var kvpTextLines in sortedTextLines)
            {
              string key = kvpTextLines.Key.Substring(0, 64);
              if (key == recurringLine)
              {
                string textLine = kvpTextLines.Value.ToLower();
                int lineNumber = textLine.Substring(0, 5).ToInt32();
                string line = textLine.Substring(6);
                if (!orderedLines[occurrences].ContainsKey(lineNumber) && !orderedLines[occurrences].ContainsValue(line))
                  orderedLines[occurrences].Add(lineNumber, line);
              }
            }
          }
        }
      }

      foreach (var kvpOccurrence in orderedLines)
      {
        var textPattern = new TextPattern();
        textPattern.OccurrenceCount = kvpOccurrence.Key;

        foreach (var kvp in kvpOccurrence.Value)
        {
          var text = new Text(false, kvp.Value);
          text.LineNumber = kvp.Key;
          textPattern.TextPatternItems.Add(textPattern.TextPatternItems.Count, text);
        }

        this.Add(this.Count, textPattern);
      }
    }

    public string ToReport()
    {
      if (this.Count == 0)
        return "TextPatternSet is empty";

      StringBuilder sb = new StringBuilder();
      sb.Append("TextPatternSet Report" + g.crlf2);

      foreach (var kvp in this)
      {
        sb.Append(kvp.Value.ToReport() + g.crlf);
      }

      return sb.ToString();
    }
  }
}
