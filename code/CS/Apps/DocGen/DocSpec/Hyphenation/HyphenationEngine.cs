using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class HyphenationEngine : SortedList<string, List<string>>
  {
    public TriNode Tree {
      get;
      set;
    }
    public bool InDiagnosticsMode {
      get;
      set;
    }
    public string DiagnosticsReport {
      get;
      set;
    }
    public bool ErrorOnLoad {
      get;
      set;
    }
    public bool IsLoaded {
      get;
      set;
    }

    private StringBuilder sb = new StringBuilder();
    private string loadReport = String.Empty;
    private string verifyReport = String.Empty;
    private string nodeBeingAdded = String.Empty;
    private DateTime BeginDT;
    private DateTime EndDT;
    private float LoadDurationSeconds {
      get;
      set;
    }
    public float HyphenateDurationSeconds {
      get;
      set;
    }

    public HyphenationEngine(bool inDiagnosticsMode)
    {
      this.ErrorOnLoad = false;
      this.DiagnosticsReport = String.Empty;
      this.InDiagnosticsMode = inDiagnosticsMode;
      this.IsLoaded = false;
      this.LoadDurationSeconds = 0F;
      this.HyphenateDurationSeconds = 0F;
    }

    public Hyphenation Hyphenate(string wordToHyphenate)
    {
      this.BeginDT = DateTime.Now;
      List<WordPart> wordParts = wordToHyphenate.GetWordParts();
      List<WordPart> wordPartsWithPatterns = new List<WordPart>();

      foreach (WordPart wordPart in wordParts)
      {
        TriNode t = this.Tree.SearchForNode(wordPart.Text);
        if (t != null)
        {
          wordPart.Patterns.AddRange(t.Patterns);
          wordPartsWithPatterns.Add(wordPart);
        }
      }

      //           Example using the word "hyphenation"
      //
      //           0----+----1----+----2
      //           h y p h e n a t i o n      (when grid first established)
      //           h2y3p h4e4n1a1t2i4o4n      (when grid populated with patterns)
      //           h y-p h e n-a-t i o n      (hyphens are allowed for odd numbers)


      // build the grid
      int gridLth = (wordToHyphenate.Length) * 2 - 1;
      char[] grid = new char[gridLth];
      int ptr = 0;
      foreach (char c in wordToHyphenate)
      {
        grid[ptr] = c;
        ptr += 2;
      }

      // populate the patterns
      foreach (WordPart wp in wordPartsWithPatterns)
      {
        foreach(string pattern in wp.Patterns)
        {
          if (pattern.StartsWith(".") && !wp.AtStart || pattern.EndsWith(".") && !wp.AtEnd)
            break;

          int lPtr = wp.Index * 2;            // points to a letter in the grid
          int nPtr = 0;                       // points to a number in the grid
          bool firstCharInPattern = true;

          foreach (char c in pattern)
          {
            if (c != '.')               // discard the begin-of-word or end-of-word indicators of the pattern
            {
              if (Char.IsDigit(c))
              {
                if (firstCharInPattern)
                  nPtr = lPtr - 1;
                else
                  nPtr = lPtr + 1;

                if (nPtr > 0 && nPtr < grid.Length - 1)   // a number cannot go to the left of the first character or to the right of the next-to-last place in the grid
                {
                  if (c > grid[nPtr])       // if the number 'c' is greater the the current (blank or numeric) value in this spot
                    grid[nPtr] = c;       // set the value of this spot in the grid to the value of 'c'
                }
              }
              else
              {
                while (c != grid[lPtr])
                {
                  lPtr += 2;
                  if (lPtr > grid.Length - 1)
                    throw new Exception("Hyphenation pattern '" + pattern + "' cannot be applied to the word '" + wordToHyphenate + "' (1). " +
                                        "Attempting to place the letter '" + c + "'.");
                }
              }
            }

            firstCharInPattern = false;
          }
        }
      }

      for (int i = 1; i < grid.Length; i+=2)
      {
        if (grid[i].IsOdd())
          grid[i] = '-';
        else
          grid[i] = (char)0;
      }

      Hyphenation h = new Hyphenation(grid);

      this.EndDT = DateTime.Now;
      TimeSpan durationTS = this.EndDT - this.BeginDT;
      this.HyphenateDurationSeconds = durationTS.Seconds + (float)durationTS.Milliseconds / 1000;

      return h;
    }

    public void Load(string path)
    {
      this.BeginDT = DateTime.Now;
      this.Clear();

      int totalNodes = 0;
      int totalUniqueNodes = 0;
      int totalPatterns = 0;

      string fileContents = File.ReadAllText(path);
      List<string> tokens = fileContents.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
      foreach (string token in tokens)
      {
        totalNodes++;
        string alphaOnly = token.ToAlphaOnly();

        if (this.ContainsKey(alphaOnly))
        {
          if (!this[alphaOnly].Contains(token))
          {
            this[alphaOnly].Add(token);
            totalPatterns++;
          }
        }
        else
        {
          this.Add(alphaOnly, new List<string>() {
            token
          });
          totalUniqueNodes++;
          totalPatterns++;
        }
      }


      if (this.InDiagnosticsMode)
      {
        sb.Append("Hyphenation PatternSet Load Report" + g.crlf2 +
                  "Total Nodes to Load : " + totalNodes.ToString("00000") + g.crlf +
                  "Total Unique Nodes  : " + totalUniqueNodes.ToString("00000") + g.crlf +
                  "Total Patterns      : " + totalPatterns.ToString("00000") + g.crlf2);
      }

      this.Tree = new TriNode(null, null, 'm');
      this.Tree.NodeAdded += Tree_NodeAdded;
      this.Tree.TotalNodeCount = 1;
      this.Tree.TotalLowNodeCount = 0;
      this.Tree.TotalEqualNodeCount = 1;
      this.Tree.TotalHighNodeCount = 0;
      this.Tree.InDiagnosticsMode = this.InDiagnosticsMode;

      LoadTree(this.Tree);
      VerifyTree();

      this.EndDT = DateTime.Now;
      TimeSpan durationTS = this.EndDT - this.BeginDT;
      this.LoadDurationSeconds = durationTS.Seconds + (float)durationTS.Milliseconds / 1000;

      string loadTimeReport = String.Empty;
      sb.Append(g.crlf + "Hyphenation Patterns Load Duration: " + this.LoadDurationSeconds.ToString("00.000") + " seconds." + g.crlf);
      this.DiagnosticsReport = sb.ToString();
      this.IsLoaded = true;
    }


    public void LoadTree(TriNode t)
    {
      int limit = 100000;
      int count = 0;

      SortedList<string, List<string>> randomOrder = new SortedList<string, List<string>>();
      Random r = new Random();

      foreach (KeyValuePair<string, List<string>> kvp in this)
      {
        int rnd = r.Next(100, 999);
        string rndStr = rnd.ToString("000");
        string key = rndStr + "-" + kvp.Key;
        randomOrder.Add(key, kvp.Value);
      }

      foreach (KeyValuePair<string, List<string>> kvp in randomOrder)
      {
        string fullKey = kvp.Key;
        string actualKey = fullKey.Split('-').Last();

        nodeBeingAdded = actualKey;
        t.NodeBeingAdded = nodeBeingAdded;
        TriNode addedNode = t.InsertNode(t, actualKey, kvp.Value, false);
        t.IncludedLevels = String.Empty;
        count++;
        if (count > limit)
          break;
      }

      t.NodeBeingAdded = String.Empty;


      if (this.InDiagnosticsMode)
      {
        string nodeCounts = "Total Nodes : " + t.TotalNodeCount.ToString("00000") + g.crlf +
                            "Total Low   : " + t.TotalLowNodeCount.ToString("00000") + g.crlf +
                            "Total Equal : " + t.TotalEqualNodeCount.ToString("00000") + g.crlf +
                            "Total High  : " + t.TotalHighNodeCount.ToString("00000") + g.crlf2;

        loadReport = nodeCounts + sb.ToString();
        sb.Append(loadReport);
      }

    }

    private void VerifyTree()
    {
      int totalNodes = 0;
      int totalHigh = 0;
      int totalEqual = 0;
      int totalLow = 0;
      int totalPatterns = 0;
      int totalDepth = 0;
      int maxDepth = 0;

      StringBuilder sbVerify = new StringBuilder();
      StringBuilder sbErrors = new StringBuilder();

      foreach (KeyValuePair<string, List<string>> kvp in this)
      {
        TriNode t = this.Tree.SearchForNode(kvp.Key);

        totalDepth += t.Level;
        if (t.Level > maxDepth)
          maxDepth = t.Level;

        totalNodes++;
        switch (t.TriNodeType)
        {
          case TriNodeType.Low:
            totalLow++;
            break;
          case TriNodeType.Equal:
            totalEqual++;
            break;
          case TriNodeType.High:
            totalHigh++;
            break;
        }

        if (t == null)
        {
          this.ErrorOnLoad = true;
          if (this.InDiagnosticsMode)
          {
            sbVerify.Append(kvp.Key.PadTo(25) + " *** NOT FOUND ***" + g.crlf);
            sbErrors.Append("*** ERROR *** " + kvp.Key.PadTo(25) + "  Node not found." + g.crlf);
          }
          else
          {
            throw new Exception(kvp.Key.PadTo(25) + "  Node not found.");
          }
        }
        else
        {
          sbVerify.Append(kvp.Key.PadTo(25));
          string valueError = String.Empty;
          if (t.ComputedValue != kvp.Key)
          {
            this.ErrorOnLoad = true;
            if (this.InDiagnosticsMode)
            {
              valueError = " *** ERROR - VALUE OF NODE='" + t.ComputedValue + "' ***";
              sbErrors.Append("*** ERROR *** " + kvp.Key.PadTo(25) + "  VALUE OF NODE='" + t.ComputedValue + "'.");
            }
            else
            {
              throw new Exception(kvp.Key.PadTo(25) + "  VALUE OF NODE='" + t.ComputedValue + "'.");
            }
          }

          string patterns = kvp.Value.ToListOfStrings();
          string loadedPatterns = "No patterns loaded.";
          if (t.Patterns != null)
            if (t.Patterns.Count > 0)
            {
              totalPatterns += t.Patterns.Count;
              loadedPatterns = t.Patterns.ToListOfStrings();
            }

          if (patterns == loadedPatterns)
          {
            if (this.InDiagnosticsMode)
              sbVerify.Append("  OK  Patterns:" + loadedPatterns);
          }
          else
          {
            this.ErrorOnLoad = true;
            if (this.InDiagnosticsMode)
            {
              sbVerify.Append("  *** ERROR *** Patterns:" + patterns + "   LoadedPatterns:" + loadedPatterns);
              sbErrors.Append("*** ERROR *** " + kvp.Key.PadTo(25) + "  Patterns:" + patterns + "   LoadedPatterns:" + loadedPatterns + "." + g.crlf);
            }
            else
            {
              throw new Exception(kvp.Key.PadTo(25) + "  Patterns:" + patterns + "   LoadedPatterns:" + loadedPatterns + ".");
            }
          }

          if (this.InDiagnosticsMode)
            sbVerify.Append("  FullPath=" + t.FullPath + g.crlf);
        }
      }


      if (this.InDiagnosticsMode)
      {
        float averageDepth = (float) totalDepth / totalNodes;
        string nodeCounts = "Total Nodes    : " + totalNodes.ToString("00000") + g.crlf +
                            "Total Low      : " + totalLow.ToString("00000") + g.crlf +
                            "Total Equal    : " + totalEqual.ToString("00000") + g.crlf +
                            "Total High     : " + totalHigh.ToString("00000") + g.crlf +
                            "Total Patterns : " + totalPatterns.ToString("00000") + g.crlf +
                            "Average Depth  : " + averageDepth.ToString("00.0000") + g.crlf +
                            "Max Depth      : " + maxDepth.ToString("000") + g.crlf2;


        sbVerify.Append(g.crlf + sbErrors.ToString() + g.crlf2);
        verifyReport = nodeCounts + sbVerify.ToString();

        sb.Append(verifyReport);
      }
    }

    [DebuggerStepThrough]
    private void Tree_NodeAdded(TriNode t)
    {
      if (!this.InDiagnosticsMode)
        return;

      string value = t.Value;
      bool equal = value == nodeBeingAdded;

      string equalTest = String.Empty;
      if (!equal)
        equalTest = "  <> " + nodeBeingAdded;

      sb.Append(t.Level.ToString("000") + " " +
                t.TriNodeType.ToString().Substring(0, 1) + " " +
                t.Value + "  " + equalTest + "   " + t.FullPath + g.crlf);
    }
  }
}
