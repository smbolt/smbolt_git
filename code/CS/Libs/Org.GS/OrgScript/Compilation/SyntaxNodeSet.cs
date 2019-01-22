using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class SyntaxNodeSet : List<SyntaxNode>
  {
    public string RawText { get; private set; }

    public SyntaxNodeSet(string rawText)
    {
      this.RawText = rawText;
    }

    public void CreateSyntaxNodes()
    {
      try
      {
        int begPtr = 0;
        int ptr = 0;

        bool processingBlanks = false;
        bool processingText = false;

        SyntaxNode syntaxNode = null;

        while (ptr < this.RawText.Length - 1)
        {
          char c = this.RawText[ptr];

          // If we're processing a sequence of blanks, we need to check if the blank sequence is continuing
          // or if the sequence of blanks has terminated.
          if (processingBlanks)
          {
            // The blank sequence is continuing, simply increment and continue forward.
            if (c == ' ')
            {
              ptr++;
              continue;
            }
            else
            {
              // The blank sequence has ended, so we write it out and let the character be processed below.
              syntaxNode = new SyntaxNode(this.RawText.Substring(begPtr, ptr - begPtr), SyntaxNodeType.Spaces);
              syntaxNode.BeginPos = begPtr;
              syntaxNode.EndPos = ptr - 1;
              begPtr = ptr;
              this.Add(syntaxNode);
              processingBlanks = false;
            }
          }

          // Process single character syntax nodes
          syntaxNode = GetSingleCharSyntaxNode(c);
          if (syntaxNode != null)
          {
            // If we found a single character node, we need to check if a text token is being processed.
            // If so, we need to terminate the text node.
            if (processingText)
            {
              syntaxNode = new SyntaxNode(this.RawText.Substring(begPtr, ptr - begPtr), SyntaxNodeType.Text);
              syntaxNode.BeginPos = begPtr;
              syntaxNode.EndPos = ptr - 1;
              begPtr = ptr;
              this.Add(syntaxNode);
            }

            syntaxNode.BeginPos = ptr;
            syntaxNode.EndPos = ptr;
            begPtr = ptr + 1;
            this.Add(syntaxNode);
            ptr++;
            continue;
          }

          // The character was not a single character node, so it's either part of a blank sequence
          // or a text sequence.

          if (c == ' ')
          {
            // If a text sequence is being processed and the character is blank, we need to write out the 
            // text sequence and fall through to allow the blank character to be processed.
            if (processingText)
            {
              syntaxNode = new SyntaxNode(this.RawText.Substring(begPtr, ptr - begPtr), SyntaxNodeType.Text);
              syntaxNode.BeginPos = begPtr;
              syntaxNode.EndPos = ptr - 1;
              begPtr = ptr;
              this.Add(syntaxNode);
            }

            processingBlanks = true;
            ptr++;
          }
          else
          {
            processingText = true;
            ptr++;
            continue;
          }
        }

        // After reaching the end of the rawText, we need to check whether a blank sequence
        // or text sequence was in progress.

        if (processingBlanks)
        {
          syntaxNode = new SyntaxNode(this.RawText.Substring(begPtr, ptr - begPtr), SyntaxNodeType.Spaces);
          syntaxNode.BeginPos = begPtr;
          syntaxNode.EndPos = ptr - 1;
          begPtr = ptr;
          this.Add(syntaxNode);
        }

        if (processingText)
        {
          syntaxNode = new SyntaxNode(this.RawText.Substring(begPtr, ptr - begPtr), SyntaxNodeType.Text);
          syntaxNode.BeginPos = begPtr;
          syntaxNode.EndPos = ptr - 1;
          begPtr = ptr;
          this.Add(syntaxNode);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while creating SyntaxNodes from the raw text.", ex);
      }
    }

    private SyntaxNode GetSingleCharSyntaxNode(char c)
    {
      if (c == '{') return new SyntaxNode(c.ToString(), SyntaxNodeType.OpenCurlyBracket);
      if (c == '}') return new SyntaxNode(c.ToString(), SyntaxNodeType.CloseCurlyBracket);

      if (c == '(') return new SyntaxNode(c.ToString(), SyntaxNodeType.OpenParen);
      if (c == ')') return new SyntaxNode(c.ToString(), SyntaxNodeType.CloseParen);

      if (c == '[') return new SyntaxNode(c.ToString(), SyntaxNodeType.OpenSquareBracket);
      if (c == ']') return new SyntaxNode(c.ToString(), SyntaxNodeType.CloseSquareBracket);

      if (c == '\\') return new SyntaxNode(c.ToString(), SyntaxNodeType.BackSlash);
      if (c == '/') return new SyntaxNode(c.ToString(), SyntaxNodeType.ForwardSlash);

      if (c == '>') return new SyntaxNode(c.ToString(), SyntaxNodeType.GreaterThan);
      if (c == '<') return new SyntaxNode(c.ToString(), SyntaxNodeType.LessThan);

      if (c == '"') return new SyntaxNode(c.ToString(), SyntaxNodeType.DoubleQuote);
      if (c == '\'') return new SyntaxNode(c.ToString(), SyntaxNodeType.Apostrophe);


      if (c == '`') return new SyntaxNode(c.ToString(), SyntaxNodeType.BackTick);
      if (c == '~') return new SyntaxNode(c.ToString(), SyntaxNodeType.Tilde);
      if (c == '!') return new SyntaxNode(c.ToString(), SyntaxNodeType.ExclamationPoint);
      if (c == '@') return new SyntaxNode(c.ToString(), SyntaxNodeType.AtSign);
      if (c == '#') return new SyntaxNode(c.ToString(), SyntaxNodeType.PoundSign);
      if (c == '$') return new SyntaxNode(c.ToString(), SyntaxNodeType.DollarSign);
      if (c == '%') return new SyntaxNode(c.ToString(), SyntaxNodeType.PercentSign);
      if (c == '^') return new SyntaxNode(c.ToString(), SyntaxNodeType.Caret);
      if (c == '&') return new SyntaxNode(c.ToString(), SyntaxNodeType.Ampersand);
      if (c == '*') return new SyntaxNode(c.ToString(), SyntaxNodeType.Asterisk);
      if (c == '_') return new SyntaxNode(c.ToString(), SyntaxNodeType.Underscore);
      if (c == '-') return new SyntaxNode(c.ToString(), SyntaxNodeType.Dash);
      if (c == '+') return new SyntaxNode(c.ToString(), SyntaxNodeType.PlusSign);
      if (c == '=') return new SyntaxNode(c.ToString(), SyntaxNodeType.EqualSign);
      if (c == '|') return new SyntaxNode(c.ToString(), SyntaxNodeType.Pipe);
      if (c == '\r') return new SyntaxNode(c.ToString(), SyntaxNodeType.CarriageReturn);
      if (c == '\n') return new SyntaxNode(c.ToString(), SyntaxNodeType.LineFeed);
      if (c == '\t') return new SyntaxNode(c.ToString(), SyntaxNodeType.Tab);
      if (c == '.') return new SyntaxNode(c.ToString(), SyntaxNodeType.Period);
      if (c == ',') return new SyntaxNode(c.ToString(), SyntaxNodeType.Comma);
      if (c == '?') return new SyntaxNode(c.ToString(), SyntaxNodeType.QuestionMark);
      if (c == ':') return new SyntaxNode(c.ToString(), SyntaxNodeType.Colon);
      if (c == ';') return new SyntaxNode(c.ToString(), SyntaxNodeType.Semicolon);

      return null;
    }
  }
}
