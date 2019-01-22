using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class WordPart
  {
    public int Index {
      get;
      set;
    }
    public string Text {
      get;
      set;
    }
    public int TotalWordLength {
      get;
      set;
    }
    public bool AtStart {
      get;
      set;
    }
    public bool AtEnd {
      get;
      set;
    }
    public List<string> Patterns {
      get;
      set;
    }

    public WordPart(int index, string text, int totalWordLength)
    {
      this.Index = index;
      this.Text = text;
      this.TotalWordLength = totalWordLength;
      this.AtStart = index == 0;
      this.AtEnd = index + text.Length == totalWordLength;
      this.Patterns = new List<string>();
    }
  }
}
