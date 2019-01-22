using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class TriNode
  {
    public event Action<TriNode> NodeAdded;
    public TriNode Root {
      get;
      set;
    }
    public TriNode Parent {
      get;
      set;
    }
    public TriNode N1_Low {
      get;
      set;
    }
    public TriNode N2_Equal {
      get;
      set;
    }
    public TriNode N3_High {
      get;
      set;
    }
    public char Char {
      get;
      set;
    }
    public int Level {
      get {
        return Get_Level();
      }
    }
    public string Value {
      get {
        return Get_Value();
      }
    }
    public List<string> Patterns {
      get;
      set;
    }
    public bool HasData {
      get;
      set;
    }
    public bool IsTerminal {
      get {
        return Get_IsTerminal();
      }
    }
    public TriNodeType TriNodeType {
      get {
        return Get_TriNodeType();
      }
    }
    public string NodeBeingAdded {
      get;
      set;
    }
    public string IncludedLevels {
      get;
      set;
    }
    public string LevelsIncluded {
      get;
      set;
    }
    public string ComputedValue {
      get;
      set;
    }
    public string FullPath {
      get {
        return Get_FullPath();
      }
    }
    public int TotalNodeCount {
      get;
      set;
    }
    public int TotalLowNodeCount {
      get;
      set;
    }
    public int TotalEqualNodeCount {
      get;
      set;
    }
    public int TotalHighNodeCount {
      get;
      set;
    }
    public bool InDiagnosticsMode {
      get;
      set;
    }

    [DebuggerStepThrough]
    public TriNode(TriNode parent, TriNode root, char c)
    {
      this.Root = root;
      this.Parent = parent;
      if (this.Parent == null)
        this.Root = this;
      this.Patterns = new List<string>();
      this.HasData = false;
      this.NodeBeingAdded = String.Empty;
      this.IncludedLevels = String.Empty;
      this.LevelsIncluded = String.Empty;
      this.ComputedValue = String.Empty;
      this.Char = c;
      this.InDiagnosticsMode = false;
    }

    public TriNode InsertNode(TriNode t, string strWord, List<string> patterns, bool isNew)
    {
      TriNodeType triNodeType = strWord[0].Compare(t.Char);

      switch(triNodeType)
      {
        case TriNodeType.Equal:
          t.Root.IncludeLevel(t.Level);
          if (strWord.Length > 1)
          {
            if (t.N2_Equal != null)
            {
              return InsertNode(t.N2_Equal, strWord.Substring(1), patterns, false);
            }
            else
            {
              TriNode equalNode = new TriNode(t, t.Root, strWord[1]);
              t.N2_Equal = equalNode;
              t.N2_Equal.Root.TotalNodeCount++;
              t.N2_Equal.Root.TotalEqualNodeCount++;
              return InsertNode(equalNode, strWord.Substring(1), patterns, true);
            }
          }
          else
          {
            t.Patterns = patterns;
            t.HasData = true;
            t.LevelsIncluded = t.Root.IncludedLevels;
            t.ComputedValue = t.Value;
            t.Root.NodeAdded(t);
            return t;
          }

        case TriNodeType.High:
          if (t.N3_High != null)
          {
            return InsertNode(t.N3_High, strWord, patterns, false);
          }
          else
          {
            TriNode highNode = new TriNode(t, t.Root, strWord[0]);
            t.N3_High = highNode;
            t.N3_High.Root.TotalNodeCount++;
            t.N3_High.Root.TotalHighNodeCount++;
            t.N3_High.Root.IncludeLevel(t.N3_High.Level);
            if (strWord.Length == 1)
            {
              t.N3_High.Patterns = patterns;
              t.N3_High.HasData = true;
              t.N3_High.LevelsIncluded = t.N3_High.Root.IncludedLevels;
              t.N3_High.ComputedValue = t.N3_High.Value;
              t.Root.NodeAdded(t.N3_High);
              return t.N3_High;
            }
            else
            {
              t.Root.IncludeLevel(t.N3_High.Level);
              return InsertNode(t.N3_High, strWord, patterns, true);
            }
          }

        default:
          if (t.N1_Low != null)
          {
            return InsertNode(t.N1_Low, strWord, patterns, false);
          }
          else
          {
            TriNode lowNode = new TriNode(t, t.Root, strWord[0]);
            t.N1_Low = lowNode;
            t.N1_Low.Root.TotalNodeCount++;
            t.N1_Low.Root.TotalLowNodeCount++;
            t.N1_Low.Root.IncludeLevel(t.N1_Low.Level);
            if (strWord.Length == 1)
            {
              t.N1_Low.Patterns = patterns;
              t.N1_Low.HasData = true;
              t.N1_Low.LevelsIncluded = t.N1_Low.Root.IncludedLevels;
              t.N1_Low.ComputedValue = t.N1_Low.Value;
              t.Root.NodeAdded(t.N1_Low);
              return t.N1_Low;
            }
            else
            {
              t.Root.IncludeLevel(t.N1_Low.Level);
              return InsertNode(t.N1_Low, strWord, patterns, true);
            }
          }
      }
    }


    public string Get_Value()
    {
      int level = this.Level;

      string[] levelStrings = this.Root.IncludedLevels.Split(',');
      int levelCount = levelStrings.Length;
      char[] value = new char[levelCount];

      TriNode t = this;
      while (t != null)
      {
        string levelString = t.Level.ToString();
        if (levelStrings.Contains(levelString))
        {
          int idx = -1;
          for (int i = 0; i < levelStrings.Length; i++)
          {
            if (levelStrings[i] == levelString)
            {
              idx = i;
              break;
            }
          }

          value[idx] = t.Char;
        }

        t = t.Parent;
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(value);
      string stringValue = sb.ToString().Replace("*", String.Empty);
      return stringValue;
    }

    public int Get_Level()
    {
      int level = 0;

      TriNode p = this.Parent;
      while (p != null)
      {
        level++;
        p = p.Parent;
      }

      return level;
    }

    public TriNodeType Get_TriNodeType()
    {
      if (this.Parent == null)
        return TriNodeType.Root;

      TriNode parent = this.Parent;

      if (parent.N1_Low != null)
        if (this == parent.N1_Low)
          return TriNodeType.Low;

      if (parent.N2_Equal != null)
        if (this == parent.N2_Equal)
          return TriNodeType.Equal;

      if (parent.N3_High != null)
        if (this == parent.N3_High)
          return TriNodeType.High;

      throw new Exception("Cannot determine TriNodeType of this object: Level='" + this.Level.ToString() + "', Char='" + this.Char + "'.");
    }

    private bool Get_IsTerminal()
    {
      if (this.N1_Low != null || this.N2_Equal != null || this.N3_High != null)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public void IncludeLevel(int level)
    {
      string levelStr = level.ToString();
      List<string> includedLevels = this.IncludedLevels.Split(',').ToList();

      if (includedLevels.Contains(levelStr))
        return;

      if (this.IncludedLevels.IsBlank())
        this.IncludedLevels = levelStr;
      else
        this.IncludedLevels += "," + levelStr;
    }

    public TriNode SearchForNode(string key)
    {
      TriNode currentNode = this;
      int index = 0;

      while (true)
      {
        if (currentNode == null)
          return null;

        char c = key[index];
        TriNodeType triNodeType = c.Compare(currentNode.Char);

        switch (triNodeType)
        {
          case TriNodeType.Equal:
            index++;
            if (index == key.Length)
              return currentNode;
            currentNode = currentNode.N2_Equal;
            break;

          case TriNodeType.Low:
            currentNode = currentNode.N1_Low;
            break;

          case TriNodeType.High:
            currentNode = currentNode.N3_High;
            break;
        }
      }
    }

    public string Get_FullPath()
    {
      int level = this.Level;

      char[] values = new char[level + 1];

      TriNode t = this;
      values[t.Level] = t.Char;
      while (t != null)
      {
        values[t.Level] = t.Char;
        t = t.Parent;
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(values);
      string stringValue = sb.ToString();
      return stringValue;
    }
  }
}



