using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public static class ExtensionMethods
  {
    [DebuggerStepThrough]
    public static TriNodeType Compare(this char value, char compValue)
    {
      int result = value.CompareTo(compValue);

      if (result < 0)
        return TriNodeType.Low;

      if (result > 0)
        return TriNodeType.High;

      return TriNodeType.Equal;
    }

    public static string ToListOfStrings(this List<string> value)
    {
      StringBuilder sb = new StringBuilder();

      foreach (string s in value)
      {
        if (sb.Length > 0)
          sb.Append(",");
        sb.Append(s);
      }

      return sb.ToString();
    }

    public static List<WordPart> GetWordParts(this string word)
    {
      word = word.Trim();
      int partCount = word.Length.GetSumOfSequence();
      List<WordPart> wordParts = new List<WordPart>();

      int wordLength = word.Length;
      int lth = 2;
      int pos = 0;

      while (true)
      {
        if (pos + lth < wordLength + 1)
        {
          wordParts.Add(new WordPart(pos, word.Substring(pos++, lth), wordLength));
        }
        else
        {
          if (lth == wordLength)
            break;
          pos = 0;
          lth++;
        }
      }

      return wordParts;
    }

    public static int GetSumOfSequence(this int number)
    {
      int sum = 0;

      int partsOfThisLength = 1;
      for (int i = number; i > 1; i--)
        sum += partsOfThisLength++;

      return sum;
    }

    public static bool IsOdd(this char c)
    {
      if (!Char.IsDigit(c))
        return false;

      int value = Int32.Parse(c.ToString());
      return value % 2 > 0;
    }
  }
}
