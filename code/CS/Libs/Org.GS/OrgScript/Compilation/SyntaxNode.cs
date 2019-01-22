using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class SyntaxNode
  {
    public string RawText {
      get;
      private set;
    }
    public int Length {
      get {
        return this.RawText.Length;
      }
    }
    public int BeginPos {
      get;
      set;
    }
    public int EndPos {
      get;
      set;
    }
    public int LineNumber {
      get;
      set;
    }
    public SyntaxNodeType SyntaxNodeType {
      get;
      private set;
    }

    public SyntaxNodeSet SyntaxNodeSet {
      get;
      private set;
    }

    public SyntaxNode(string rawText, SyntaxNodeType syntaxNodeType = SyntaxNodeType.NotSet)
    {
      if (rawText == null)
        throw new Exception("A SyntaxNode cannot be created from a null string.");

      this.RawText = rawText;
      this.SyntaxNodeType = syntaxNodeType;

      if (this.SyntaxNodeType == SyntaxNodeType.NotSet)
        this.SyntaxNodeType = GetSyntaxNodeType();
    }

    private SyntaxNodeType GetSyntaxNodeType()
    {



      return SyntaxNodeType.Unidentified;
    }

    public override string ToString()
    {
      return this.RawText + " (Length:" + this.Length.ToString() + " Type:" + this.SyntaxNodeType.Display() + ")";
    }
  }
}
