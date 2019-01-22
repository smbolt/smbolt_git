using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class BmsStatement
  {
    public BmsStatementType BmsStatementType { get; set; }
    public Bms_BASE Bms_BASE { get; set; }
    public BmsLineSet BmsLineSet { get; set; }
    public string BmsStatementText { get { return Get_BmsStatementText(); } }
    public string BmsStatementOrigText { get { return Get_BmsStatementOrigText(); } }
    public string FirstToken { get; set; }
    public string SecondToken { get; set; }
    public string FieldName { get; set; }
    public List<Parenthetical> Parentheticals { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public string RemainingText { get; private set; }
    public BmsMapErrorSet BmsMapErrorSet { get; set; }
    public int ErrorCode { get { return Get_ErrorCode(); } } 


    public BmsStatement()
    {
      this.BmsStatementType = BmsStatementType.Uncompiled;
      this.Bms_BASE = null; 
      this.BmsLineSet = new BmsLineSet();
      this.FirstToken = String.Empty;
      this.SecondToken = String.Empty;
      this.FieldName = String.Empty;
      this.Parentheticals = new List<Parenthetical>();
      this.Parameters = new Dictionary<string, string>();
      this.BmsMapErrorSet = new BmsMapErrorSet(); 
    }


    /*  
     * See this site for details of the allowed parameters
     * https://www.ibm.com/support/knowledgecenter/en/SSGMCP_5.1.0/com.ibm.cics.ts.applicationprogramming.doc/topics/dfhp4_bmsmacros.html
    */

    public void Compile_PRINT()
    {

    }

    public void Compile_DFHMSD()
    {
      try
      {
        this.Bms_BASE = new Bms_DFHMSD(this);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to compile a DFHMSD macro.", ex);
      }
    }

    public void Compile_DFHMDI()
    {
      try
      {
        this.Bms_BASE = new Bms_DFHMDI(this);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to compile a DFHMDI macro.", ex);
      }
    }

    public void Compile_DFHMDF()
    {
      try
      {
        this.Bms_BASE = new Bms_DFHMDF(this);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to compile a DFHMDF macro.", ex);
      }
    }

    public void Compile_Other()
    {

    }


    public override string ToString()
    {
      return this.BmsLineSet.ToString();
    }

    public void ExtractParentheticals()
    {
      try
      {
        this.Parentheticals = new List<Parenthetical>();

        if (!this.BmsStatementText.Contains("("))
        {
          this.RemainingText = this.BmsStatementText;
          return;
        }

        string t = this.BmsStatementText;

        foreach (var p in BmsConstants.Parentheticals)
        {
          string u = t.ToUpper();
          string name = p + "=(";
          if (u.Contains(name))
          {
            int beg = u.IndexOf(name);
            if (beg > 0)
            {
              int end = u.IndexOf(")", beg);
              if (end > 0)
              {
                string unit = t.Substring(beg, end - beg + 1);
                string parms = unit.Substring(name.Length).Replace(")", String.Empty); 
                name = name.Replace("=(", String.Empty);
                this.Parentheticals.Add(new Parenthetical(name, parms));
                string begText = t.Substring(0, beg);
                if (end < t.Length)
                  end++;
                string endText = t.Substring(end);
                if (endText[0] == ',')
                  endText = endText.Substring(1);
                string remText = begText + endText;
                t = remText;
              }
              else
              {
                // found beginning but not the end
                // record compile error and move on
              }
            }
          }
        }

        this.RemainingText = t;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to extract parenthetical values from the statement '" + this.BmsStatementText + "'.", ex); 
      }
    }

    public void ExtractParameters()
    {
      string stmtText = String.Empty;

      char spaceRepl = '\xA4';
      char equalsRepl = '\xA7';
      char commaRepl = '\xA9';


      try
      {
        this.Parameters = new Dictionary<string, string>();

        stmtText = this.RemainingText;
        string workText = stmtText;

        if (workText.Contains("'"))
        {
          bool inApost = false;
          char[] chars = new char[workText.Length];

          for (int i = 0; i < workText.Length; i++)
          {
            if (workText[i] == '\'')
            {
              inApost = !inApost;
              chars[i] = workText[i];
              continue;
            }

            if (inApost)
            {
              switch (workText[i])
              {
                case ' ':
                  chars[i] = spaceRepl;
                  break;

                case '=':
                  chars[i] = equalsRepl;
                  break;

                case ',':
                  chars[i] = commaRepl;
                  break;

                default:
                  chars[i] = workText[i];
                  break;
              }
            }
            else
            {
              chars[i] = workText[i];
            }
          }

          workText = new String(chars); 
        }

        string[] stmtPhrases = workText.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < stmtPhrases.Length; i++)
        {
          stmtPhrases[i] = stmtPhrases[i].Replace(spaceRepl, ' ').Replace(commaRepl, ','); 
        }

        var phrasesToRemove = new List<int>();

        for (int i = 0; i < stmtPhrases.Length; i++)
        {
          if (stmtPhrases[i].Contains("="))
          {
            string[] tokens = stmtPhrases[i].Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);

            switch (tokens.Length)
            {
              case 0:
                // found just an equal sign between commas - need error message
                break;

              case 1:
                // found a named parameter but no value - need error message
                break;

              case 2:
                this.Parameters.Add(tokens[0].ToUpper().Trim(), tokens[1].Trim().Replace(equalsRepl, '='));
                phrasesToRemove.Add(i);
                break;

              default:
                // found multiple equal signs - need error message
                break;
            }
          }
        }

        workText = workText.Replace(spaceRepl, ' ')
                           .Replace(equalsRepl, '=') 
                           .Replace(commaRepl, ','); 

        foreach (var idx in phrasesToRemove)
        {
          string phraseToRemove = stmtPhrases[idx].Replace(equalsRepl, '='); 

          if (!workText.Contains(phraseToRemove))
            throw new Exception("Cannot locate phrase '" + phraseToRemove + "' in the statement so that it can be removed. The phase has been extracted as a " +
                                "keyword/value pair.");

          if (workText.Contains("," + phraseToRemove))
            workText = workText.Replace("," + phraseToRemove, String.Empty);
          if (workText.Contains(phraseToRemove + ","))
            workText = workText.Replace(phraseToRemove + ",", String.Empty);
          if (workText.Contains(phraseToRemove))
            workText = workText.Replace(phraseToRemove, String.Empty); 
        }

        while (workText.Contains(",,"))
          workText = workText.Replace(",,", ",");

        while (workText.Contains("  "))
          workText = workText.Replace("  ", " ");

        this.RemainingText = workText.Trim(); 
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to extract parameters from the BMS statement '" + this.BmsStatementText + "'.", ex); 
      }
    }

    private string Get_BmsStatementText()
    {
      if (this.BmsLineSet == null || this.BmsLineSet.Count == 0)
        return String.Empty;
      
      var sb = new StringBuilder();

      foreach(var bmsLine in this.BmsLineSet.Values)
      {
        string lineText = bmsLine.LineText;
        if (lineText.Length > 71)
        {
          lineText = lineText.Substring(0, 72); 
          if (lineText[71].ToString().IsNotBlank())
          {
            lineText = lineText.Substring(0, 71).Trim();
          }
        }

        sb.Append(lineText.Trim());
      }

      string text = sb.ToString();
      char spaceRepl = '\xA4';

      if (text.Contains("  "))
      {
        bool inApost = false;
        char[] chars = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
          if (text[i] == '\'')
          {
            inApost = !inApost;
            chars[i] = text[i];
            continue;
          }

          if (inApost)
          {
            if (text[i] == ' ')
              chars[i] = spaceRepl;
            else
              chars[i] = text[i];
          }
          else
            chars[i] = text[i];

        }

        text = new String(chars); 
      }

      while (text.Contains("  "))
        text = text.Replace("  ", " ");

      text = text.Replace(spaceRepl, ' '); 

      return text;
    }

    private string Get_BmsStatementOrigText()
    {
      if (this.BmsLineSet == null || this.BmsLineSet.Count == 0)
        return String.Empty;

      var sb = new StringBuilder();

      foreach (var bmsLine in this.BmsLineSet.Values)
      {
        if (sb.Length > 0)
          sb.Append(g.crlf); 
        sb.Append(bmsLine.LineText);
      }

      string origText = sb.ToString();

      return origText;
    }

    private int Get_ErrorCode()
    {
      if (this.BmsMapErrorSet == null || this.BmsMapErrorSet.Count == 0)
        return 0;

      return this.BmsMapErrorSet.Values.Max(e => e.Code);
    }

    public BmsStatement CloneForVFLEX(int lineNumber)
    {
      var clone = new BmsStatement();

      foreach (var kvpLine in this.BmsLineSet)
      {
        clone.BmsLineSet.Add(kvpLine.Key, kvpLine.Value.CloneForVFLEX(lineNumber)); 
      }

      clone.ExtractParentheticals();
      clone.ExtractParameters();

      int origLineNumber = this.FieldName.ExtractInteger();
      int increment = lineNumber - origLineNumber;

      for (int i = 0; i < clone.Parentheticals.Count; i++)
      {
        if (clone.Parentheticals[i].Name == "POS")
        {
          string lineParm = clone.Parentheticals[i].Parms[0];
          if (lineParm.IsInteger())
          {
            clone.Parentheticals[i].Parms[0] = (lineParm.ToInt32() + increment).ToString();
          }            
          break;
        }
      }

      clone.Compile_DFHMDF(); 

      return clone;
    }
  }
}
