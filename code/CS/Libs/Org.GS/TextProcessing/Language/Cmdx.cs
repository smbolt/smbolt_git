using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.TextProcessing
{
  public enum Verb
  {
    AddExportTemplate,
    SetTextStart,
    SetTextEnd,
    SetVariable,
    SetGlobalVariable,
    LocateToken,
    ExtractNextToken,
    ExtractNextTokens,
    ExtractNextLine,
    ExtractTextBefore,
    TokenizeNextLine,
    RemoveTokens,
    ExtractStoredToken,
    ExtractStoredTokens,
    CreateElement,
    Truncate,
    InvalidVerb
  }

  public enum Direction
  {
    BeforeInclusive,
    BeforeExclusive,
    AfterInclusive,
    AfterExclusive,
    NotApplicable
  }

  public class Cmdx
  {
    private Cmd _cmd;

    private string[] _parms;
    internal string[] Parms { get { return _parms == null ? new string[0] : _parms; } }

    public bool HasNoParms { get { return this.Parms == null || this.Parms.Length == 0; } }
    public string ParmString { get { return Get_ParmString(); } }
    public int ParmCount { get { return this.Parms == null ? 0 : this.Parms.Length; } }
    public bool UsePriorEnd { get { return Get_UsePriorEnd(); } }
    public string TextToFind { get { return Get_TextToFind(); } }
    public string TextParmValue { get { return Get_TextParmValue(); } }
    public bool ExcludeLastToken { get { return Get_ExcludeLastToken(); } }
    public bool PositionAtEnd { get { return Get_PositionAtEnd(); } }
    public bool IsRequired { get { return Get_IsRequired(); } }
    public string IsRequiredIf { get { return Get_IsRequiredIf(); } }
    public string SpecialRoutine { get { return Get_SpecialRoutine(); } }
    public bool NumericOnly { get { return Get_NumericOnly(); } }
    public bool AdvanceToEol { get { return Get_AdvanceToEol(); } }
    public string DataName { get { return Get_DataName(); } }
    public string DataType { get { return Get_DataType(); } }
    public string DataFormat { get { return Get_DataFormat(); } }
    public string TokenType { get { return Get_TokenType(); } }
    public int NumberOfTokens { get { return Get_NumberOfTokens(); } }
    public string SubCommand { get { return Get_SubCommand(); } }
    public bool RemoveStoredToken { get { return Get_RemoveStoredToken(); } }
    public int StoredTokenIndex { get { return Get_StoredTokenIndex(); } }
    public int StoredTokenCount { get { return Get_StoredTokenCount(); } }
    public List<Token> TokensToRemove { get { return Get_TokensToRemove(); } }
    public Direction Direction { get { return Get_Direction(); } }
    public string ElementName { get { return Get_ElementName(); } }
    public Dictionary<string, string> Attributes { get { return Get_Attributes(); } }
    public string ExportTemplateName { get { return Get_ExportTemplateName(); } }
    public XElement ExportTemplate { get { return Get_ExportTemplate(); } }
    public bool ActiveToRun { get; set; }

    public string Code { get; private set; }
    public Verb _verb;
    public Verb Verb { get { return _verb; } }

    public bool Break { get; set; }

    public Cmdx(Cmd cmd)
    {
      _cmd = cmd;
      this.Break = cmd.Break;
      this.Code = cmd.Code; 
      Compile();
    }
    
    private void Compile()
    {
      this.ActiveToRun = _cmd.ActiveToRun;

      if (_cmd == null)
        throw new CxException(1, new object[] {this});

      if (_cmd.Code.IsBlank())
        _cmd.Code = String.Empty;

      try
      {
        _verb = Get_Verb();
        PopulateParms();
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(2, new object[] { this, ex }); 
      }

    }

    private void PopulateParms()
    {
      try
      {
        if (this.Code.IsBlank())
          _parms = new string[0];

        string parms = this.Code.GetTextBetween(Constants.OpenParen, Constants.CloseParen);

        _parms = parms.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < _parms.Length; i++)
        {
          if (this.Verb == Verb.CreateElement && i == 0)
            _parms[i] = _parms[i].Trim();
          else
            _parms[i] = _parms[i].Trim().ToLower();
        }
      }
      catch(Exception ex)
      {
        throw new CxException(5, new object[] { this, ex }); 
      } 
    }

    private bool Get_UsePriorEnd()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("$priorend");
    }
    
    private bool Get_ExcludeLastToken()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("exclude");
    }

    private bool Get_RemoveStoredToken()
    {
      switch (this.Verb)
      {
        case Verb.ExtractStoredToken:
        case Verb.ExtractStoredTokens:
          if (this.HasNoParms)
            return false;
          return this.Parms.Contains("remove");
      }

      return false;
    }

    private bool Get_PositionAtEnd()
    {
      switch (this.Verb)
      {
        case Verb.SetTextStart: return false;
        case Verb.SetTextEnd: return true;
        case Verb.ExtractNextToken: return true;
      }

      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("end");
    }

    private bool Get_IsRequired()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("required");
    }

    private string Get_IsRequiredIf()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms)
      {
        if (parm.StartsWith("requiredif["))
        {
          if (parm.IndexOf("]") == -1)
            throw new CxException(30, new object[] { this });

          string condition = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
          if (condition.IsBlank())
            throw new CxException(31, new object[] { this });

          return parm;
        }
      }

      return String.Empty;
    }

    private string Get_SpecialRoutine()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms)
      {
        if (parm.StartsWith("sr["))
        {
          if (parm.IndexOf("]") == -1)
            throw new CxException(32, new object[] { this });

          string condition = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
          if (condition.IsBlank())
            throw new CxException(33, new object[] { this });

          return parm;
        }
      }

      return String.Empty;
    }

    private string Get_ElementName()
    {
      switch (this.Verb)
      {
        case Verb.CreateElement:
          this.AssertParmCount(1);
          string parm = this.Parms[0];

          if (parm.Contains('('))
            parm = parm.GetTextBefore(Constants.OpenParen); 

          if (parm.IsBlank())
            throw new CxException(87, new object[] { this });

          return parm;
      }

      return String.Empty;
    }

    public Dictionary<string, string> Get_Attributes()
    {
      var attributes = new Dictionary<string, string>();

      switch (this.Verb)
      {
        case Verb.CreateElement:
          this.AssertParmCount(1); 
          string parm = this.Parms[0];

          if (!parm.Contains('('))
            return attributes;

          string attrString = parm.GetTextBetween(Constants.OpenParen, Constants.CloseParen);
          if (attrString.IsBlank())
            throw new CxException(91, new object[] { this });
          string[] attrs = attrString.Split(Constants.PipeDelimiter);
          foreach (string attr in attrs)
          {
            string attrName = String.Empty;
            string attrValue = String.Empty;

            if (attr.Contains("="))
            {
              string[] tokens = attr.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
              if (tokens.Length != 2)
                throw new CxException(92, new object[] { this });
              if (tokens[0].Contains(' '))
                throw new CxException(93, new object[] { this });
              attrName = tokens[0];
              if (attributes.ContainsKey(attrName))
                throw new CxException(94, new object[] { this });
              attrValue = tokens[1];
              attributes.Add(attrName, attrValue);
            }
            else
            {
              attrName = attr;
              if (attributes.ContainsKey(attrName))
                throw new CxException(94, new object[] { this });
              attrValue = String.Empty;
              attributes.Add(attrName, attrValue);
            }
          }
          return attributes;
      }

      return attributes;
    }

    private int Get_NumberOfTokens()
    {
      switch (this.Verb)
      {
        case Verb.ExtractNextTokens:
          this.AssertParmCount(2);
          string numberOfTokensParm = this.Parms[1];
          if (!numberOfTokensParm.IsNumeric())
            throw new CxException(72, new object[] { this });
          int numberOfTokens = numberOfTokensParm.ToInt32();
          if (numberOfTokens < 1)
            throw new CxException(73, new object[] { this });
          return numberOfTokens;
      }

      return -1;
    }

    private bool Get_NumericOnly()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("numericonly");
    }

    private bool Get_AdvanceToEol()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.Contains("advancetoeol");
    }

    private string Get_ParmString()
    {
      if (this.HasNoParms)
        return String.Empty;

      return this.Parms.StringArrayToString();
    }

    public string Get_TextToFind()
    {
      if (this.HasNoParms)
        return String.Empty;

      return this.Parms[0];
    }

    public string Get_TextParmValue()
    {
      string textParmValue = String.Empty;
      string parm = String.Empty;

      try
      {
        if (this.HasNoParms)
          return String.Empty;

        switch (_verb)
        {
          case Verb.SetVariable:
            this.AssertParmCount(3);
            textParmValue = this.Parms[2];
            if (textParmValue.IsBlank())
              throw new CxException(60, new object[] { this });
            return textParmValue;

          case Verb.ExtractTextBefore:
            this.AssertParmCount(2);
            textParmValue = this.Parms[1];
            if (textParmValue.IsBlank())
              throw new CxException(67, new object[] { this });
            return textParmValue;
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(20, new object[] { this, ex }); 
      }
    }

    public string Get_DataName()
    {
      try
      {
        string dataName = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.SetVariable:
          case Verb.ExtractNextToken:
          case Verb.ExtractNextLine:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            if (parm.Contains("["))
              dataName = parm.GetTextAfter(Constants.OpenAndCloseBrackets);
            else
              dataName = parm;
            if (dataName.Contains("."))
              dataName = dataName.GetTextBefore(Constants.PeriodDelimiter); 
            if (dataName.IsBlank())
              throw new CxException(8, new object[] { this });
            return dataName;

          default:
            return String.Empty;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(9, new object[] { this, ex });
      }
    }

    public string Get_DataType()
    {
      try
      {
        string dataType = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.TokenizeNextLine:
          case Verb.RemoveTokens:
          case Verb.AddExportTemplate:
            return String.Empty;

          default:
            if (_verb != Verb.SetTextEnd)
              this.AssertParmCount(1);
            if (this.Parms.Length == 0)
              return String.Empty;
            parm = this.Parms[0];
            dataType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
            if (dataType.IsBlank())
              return String.Empty;
            dataType.AssertValidDataType();
            return dataType;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(10, new object[] { this, ex });
      }
    }

    public string Get_DataFormat()
    {
      try
      {
        string dataFormat = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.ExtractNextToken:
          case Verb.ExtractNextTokens:
          case Verb.ExtractStoredToken:
          case Verb.ExtractStoredTokens:
          case Verb.SetVariable:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            dataFormat = parm.GetTextAfter(Constants.PeriodDelimiter);
            dataFormat.AssertValidDataFormat(this);
            return dataFormat;

          default:
            return String.Empty;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(11, new object[] { this, ex });
      }
    }

    public Direction Get_Direction()
    {
      try
      {
        string dataFormat = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.Truncate:
            this.AssertParmCount(1);
            parm = this.Parms[0];

            if (!parm.ToLower().In("before,after"))
              throw new CxException(85, new object[] { this });

            if (this.Parms.ContainsEntry("inclusive"))
            {
              if (parm.ToLower() == "before")
                return Direction.BeforeInclusive;
              return Direction.AfterInclusive;
            }

            if (parm.ToLower() == "before")
              return Direction.BeforeExclusive;
            return Direction.AfterExclusive;

          default:
            return Direction.NotApplicable;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(86, new object[] { this, ex });
      }
    }

    public int Get_StoredTokenIndex()
    {
      try
      {
        switch(_verb)
        {
          case Verb.ExtractStoredToken:
          case Verb.ExtractStoredTokens:
            this.AssertParmCount(2);
            string parm = this.Parms[1];

			      if (parm.ToLower() == "last")
				      return 99999;

			      if (parm.ToLower() == "join")
				      return 99998;

            if (parm.IsNumeric())
              return parm.ToInt32();

            throw new CxException(21, new object[] { this });
        }

        return -1;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(12, new object[] { this, ex });
      }
    }

    public int Get_StoredTokenCount()
    {
      try
      {
        switch(_verb)
        {
          case Verb.ExtractStoredTokens:
            this.AssertParmCount(3);
            string parm = this.Parms[2];
            if (parm.IsNumeric())
              return parm.ToInt32();

            throw new CxException(77, new object[] { this });
        }

        return -1;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(12, new object[] { this, ex });
      }
    }

    public string Get_TokenType()
    {
      try
      {
        string tokenType = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.SetVariable:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            if (parm.Contains("["))
              tokenType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);

            if (tokenType.IsBlank())
              throw new CxException(16, new object[] { this });
            return tokenType;

          default:
            if (this.Parms.Length == 0)
              return String.Empty;

            parm = this.Parms[0];
            if (!parm.Contains("["))
              return String.Empty;
              
              tokenType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);

            if (tokenType.IsBlank())
              throw new CxException(16, new object[] { this });
            return tokenType;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(13, new object[] { this, ex } );
      }
    }

    public string Get_SubCommand()
    {
      try
      {
        string subCommand = String.Empty;
        string parm = String.Empty;

        switch (_verb)
        {
          case Verb.SetVariable:
            this.AssertParmCount(2);
            parm = this.Parms[1];
            subCommand = parm.ToLower();
            if (!subCommand.IsValidSubCommand())
              throw new CxException(17, new object[] { this });
            return subCommand;

          default:
            return String.Empty;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(14, new object[] { this, ex });
      }
    }

    public List<Token> Get_TokensToRemove()
    {
      try
      {
        var tokensToRemove = new List<Token>();

        switch (this.Verb)
        {
          case Verb.RemoveTokens:
            if (this.Parms == null || this.Parms.Length == 0)
              throw new CxException(80, new object[] { this });
            foreach (var parm in this.Parms)
            {
              string tokenText = parm.GetTextBefore(Constants.OpenBracket); 
              string options = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
              if (tokenText.IsBlank())
                throw new CxException(82, new object[] { this });
              if (parm.Contains('[') && options.IsBlank())
                throw new CxException(81, new object[] { this });

              var tokenToRemove = new Token();
              tokenToRemove.Text = tokenText;
              tokenToRemove.IsRequired = !options.ToLower().Contains("opt");
              tokensToRemove.Add(tokenToRemove);
            }
            break;
        }
        
        return tokensToRemove;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(79, new object[] { this, ex }); 
      }
    }

    private string Get_ExportTemplateName()
    {

      System.Diagnostics.Debugger.Break();
      return String.Empty;
    }

    private XElement Get_ExportTemplate()
    {
      try
      {

        System.Diagnostics.Debugger.Break();



        return new XElement("Template"); 
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(123, new object[] { this }); 
      }
    }

    private Verb Get_Verb()
    {
      try
      {
        string verb = this.Code.GetTextBefore(Constants.OpenParen);

        _verb = g.ToEnum<Verb>(verb, TextProcessing.Verb.InvalidVerb);
        if (_verb == TextProcessing.Verb.InvalidVerb)
          throw new CxException(18, new object[] {_cmd });

        return _verb;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(6, new object[] {_cmd, ex });
      }
    }    
  }

  public static class CmdxExtensionMethods
  {
    public static bool ParmsContains(this string[] parms, string parmValue)
    {
      if (parms == null || parms.Length == 0 || parmValue.IsBlank())
        return false;

      foreach (string parm in parms)
      {
        if (parm.ToLower() == parmValue.ToLower())
          return true;
      }

      return false;
    }

    public static bool IsValidSubCommand(this string value)
    {
      if (value.IsBlank())
        return false;

      switch (value.ToLower())
      {
        case "find": return true;
      }

      return false;
    }

    public static void AssertParmCount(this Cmdx cmdx, int count)
    {
      int parmCount = 0;

      if (!cmdx.HasNoParms)
        parmCount = cmdx.Parms.Length;

      if (cmdx.Parms.Length < count)
        throw new CxException(15, new object[] { cmdx, count }); 
    }

    public static void AssertValidDataFormat(this string dataFormat, Cmdx cmdx, bool allowBlank = true)
    {
      if (dataFormat.IsBlank() && allowBlank)
        return;

      switch (dataFormat.ToLower())
      {
        case "h24:mm:ss":
        case "h12:mm:ss":
        case "ccyymmdd":
        case "mm/dd/yyyy":
          return;
      }

      throw new CxException(22, new object[] { cmdx, dataFormat, allowBlank }); 
    }

  }
}
