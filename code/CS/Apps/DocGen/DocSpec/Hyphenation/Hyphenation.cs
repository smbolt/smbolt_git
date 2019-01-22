using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class Hyphenation
  {
    public bool CanBeHyphenated {
      get;
      set;
    }
    public string OriginalWord {
      get;
      set;
    }
    public string WordWithHyphens {
      get;
      set;
    }
    public List<int> HyphenationPositions {
      get;
      set;
    }

    public Hyphenation(char[] grid)
    {
      int wordLength = (grid.Length / 2) + 1;
      char[] word = new char[wordLength];

      for (int i = 0; i < wordLength; i++)
        word[i] = grid[i * 2];

      this.OriginalWord = new string(word);

      this.HyphenationPositions = new List<int>();
      for (int i = 1; i < grid.Length; i += 2)
      {
        if (grid[i] == '-')
        {
          int hPos = (i - 1) / 2;
          this.HyphenationPositions.Add(hPos);
        }
      }

      if (this.HyphenationPositions.Count == 0)
        this.WordWithHyphens = this.OriginalWord;
      else
      {
        int totalPositions = this.OriginalWord.Length + this.HyphenationPositions.Count;
        char[] wordWithHyphensChars = new char[totalPositions];
        int hyphensPlaced = 0;
        foreach (int i in this.HyphenationPositions)
          wordWithHyphensChars[i + 1 + hyphensPlaced++] = '-';

        int pos = 0;
        foreach (char c in this.OriginalWord)
        {
          while (wordWithHyphensChars[pos] == '-' && pos < wordWithHyphensChars.Length)
            pos++;

          if (pos > wordWithHyphensChars.Length - 1)
          {
            string hyphenPositions = String.Empty;
            foreach (int j in this.HyphenationPositions)
            {
              if (hyphenPositions.IsBlank())
                hyphenPositions += j.ToString();
              else
                hyphenPositions += "," + j.ToString();
            }
            throw new Exception("Error occurred attempting to place hyphens in the word '" + this.OriginalWord + "' after characters in positions " + hyphenPositions + ".");
          }

          wordWithHyphensChars[pos] = c;
          pos++;
        }

        this.WordWithHyphens = new string(wordWithHyphensChars);

        if (pos != wordWithHyphensChars.Length)
        {
          string hyphenPositions = String.Empty;
          foreach (int j in this.HyphenationPositions)
          {
            if (hyphenPositions.IsBlank())
              hyphenPositions += j.ToString();
            else
              hyphenPositions += "," + j.ToString();
          }
          throw new Exception("Error occurred attempting to place hyphens in the word '" + this.OriginalWord + "' after characters in positions " + hyphenPositions + ".");
        }

        if (wordWithHyphensChars[0] == '-')
          throw new Exception("Error occurred attempting to place hyphens in the word '" + this.OriginalWord + "' hyphen occurs in first position '" + this.WordWithHyphens + "'.");

        if (wordWithHyphensChars[wordWithHyphensChars.Length - 1] == '-')
          throw new Exception("Error occurred attempting to place hyphens in the word '" + this.OriginalWord + "' hyphen occurs in last position '" + this.WordWithHyphens + "'.");
      }

      this.CanBeHyphenated = this.HyphenationPositions.Count > 0;
    }
  }
}
