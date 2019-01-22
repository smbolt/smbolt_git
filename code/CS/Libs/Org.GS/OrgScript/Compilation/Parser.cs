using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Org.GS.OrgScript.Compilation
{
  public class Parser
  {
    public string RawCode {
      get;
      private set;
    }
    public CompilerConfig CompilerConfig {
      get;
      private set;
    }
    public string[] CodeParts {
      get;
      private set;
    }
    public string SyntaxNodeReport {
      get;
      private set;
    }
    public List<SyntaxNodeSet> SyntaxNodeSets {
      get;
      private set;
    }
    public ParserNextStep ParserNextStep {
      get;
      private set;
    }

    public Parser(string rawCode, CompilerConfig compilerConfig)
    {
      this.RawCode = rawCode;
      this.SyntaxNodeReport = String.Empty;
      this.CompilerConfig = compilerConfig;
      this.SyntaxNodeSets = new List<SyntaxNodeSet>();
      this.ParserNextStep = ParserNextStep.SplitCodeForThreading;
    }

    public void RunNextStep()
    {
      try
      {



      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run next step in the parser - next step is '" + this.ParserNextStep.ToString() + "'.", ex);
      }
    }


    public SyntaxTree Parse()
    {
      try
      {
        var syntaxTree = new SyntaxTree();

        if (this.RawCode.IsBlank())
          return syntaxTree;

        string[] codeParts = CreateCodeParts(this.RawCode, this.CompilerConfig.TargetRawLength);
        CompareCodePartsToRawText(this.RawCode, codeParts);


        foreach (var codePart in codeParts.Where(cp => cp != null))
        {
          this.SyntaxNodeSets.Add(new SyntaxNodeSet(codePart));
        }

        if (this.CompilerConfig.RunParallel)
        {
          Parallel.ForEach(this.SyntaxNodeSets, (syntaxNodeSet) =>
          {
            syntaxNodeSet.CreateSyntaxNodes();
          });
        }
        else
        {
          foreach (var syntaxNodeSet in this.SyntaxNodeSets)
          {
            syntaxNodeSet.CreateSyntaxNodes();
          }
        }

        if (this.CompilerConfig.InDebugMode)
        {
          this.SyntaxNodeReport = CreateSyntaxNodeReport(this.SyntaxNodeSets);
        }



        return syntaxTree;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during language parsing.", ex);
      }
    }

    private void CompareCodePartsToRawText(string rawText, string[] codeParts)
    {
      if (!this.CompilerConfig.InDebugMode)
        return;

      var sb = new StringBuilder();
      foreach (var codePart in codeParts)
      {
        if (codePart == null)
          break;
        sb.Append(codePart);
      }

      string reassembledCode = sb.ToString();
      if (rawText != reassembledCode)
        throw new Exception("The reassembled code parts does not match the original raw text of the code.");
    }

    private string[] CreateCodeParts(string rawText, int targetRawLength)
    {
      // This method breaks the rawText string into a string array breaking at the closest
      // blank character in the vicinity of the targetRawLength parameter. The string array
      // is returned so subsequent processing can use multiple threads for parsing.

      char[] breakChars = new char[] { ' ', '\n', '\r', '{', '}' };

      try
      {
        string[] codeParts = new string[Convert.ToInt32((rawText.Length / targetRawLength) * 1.25)];

        int lastEnd = -1;
        int charPtr = 0;
        int partPtr = 0;

        while (charPtr < rawText.Length - 1)
        {
          // Take the next bite of text...
          charPtr = lastEnd + targetRawLength;

          // Ensure that we haven't gone past the end of the rawText.
          if (charPtr > rawText.Length - 1)
          {
            charPtr = rawText.Length - 1;
            int remainingLength = charPtr - lastEnd;
            codeParts[partPtr] = rawText.Substring(lastEnd + 1, charPtr - lastEnd);
            break;
          }

          // Ensure that there is at least one blank character in the bite of text taken.
          int startSearch = lastEnd;
          if (startSearch < 0)
            startSearch = 0;

          int lengthToSearch = charPtr - startSearch;

          while (rawText.IndexOfAny(breakChars, startSearch, lengthToSearch) == -1)
          {
            // If we have reached the end of the text, then we take the block of text
            // as the next code part, by setting the charPtr at the end of the text and
            // exiting the while loop.
            if (startSearch + lengthToSearch == rawText.Length - 1)
            {
              charPtr = rawText.Length - 1;
              break;
            }

            // If there's no blank character, take another 100 characters
            lengthToSearch += 100;
            // Be sure we're not past the end
            if (startSearch + lengthToSearch > rawText.Length - 1)
              lengthToSearch = rawText.Length - startSearch - 1;
          }

          if (partPtr == codeParts.Length - 1)
            charPtr = rawText.Length - 1;

          if (charPtr - lastEnd == 0)
            break;

          if (charPtr < rawText.Length - 1)
          {
            while (rawText[charPtr] != ' ')
              charPtr--;
          }

          codeParts[partPtr] = rawText.Substring(lastEnd + 1, charPtr - lastEnd);
          lastEnd = charPtr;
          partPtr++;
        }

        return codeParts;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to split the rawText parameter into a string array with target " +
                            "element length of " + targetRawLength.ToString() + ".", ex);
      }
    }

    private string CreateSyntaxNodeReport(List<SyntaxNodeSet> syntaxNodeSets)
    {
      var sb = new StringBuilder();

      foreach (var syntaxNodeSet in syntaxNodeSets)
      {
        foreach (var syntaxNode in syntaxNodeSet)
        {
          sb.Append(syntaxNode.RawText + "    (" + syntaxNode.SyntaxNodeType.Display() + ")" + g.crlf);
        }
      }

      string report = sb.ToString();
      return report;
    }
  }
}
