using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public enum OperandType
  {
    Literal,
    Variable,
    Integer,
    Decimal
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
    public TokenSearchCriteria TokenSearchCriteria { get { return Get_TokenSearchCriteria(); } }

    public string TextToFind { get { return Get_TextToFind(); } }
    public bool PositionAtEnd { get { return Get_PositionAtEnd(); } }
    public bool IsRequired { get { return Get_IsRequired(); } }
    public bool Trim { get { return Get_Trim(); } }
    public string DataName { get { return Get_DataName(); } }
    public string VariableName { get { return Get_VariableName(); } }
    public int StartPosition { get { return Get_StartPosition(); } }
    public string Zap { get { return Get_Zap(); } }
    public int UnitCount { get { return Get_UnitCount(); } }
    public TextUnit TextUnit { get { return Get_TextUnit(); } }
    public PositionAt PositionAt { get { return Get_PositionAt(); } }
    public TruncateDirection TruncateDirection { get { return Get_TruncateDirection(); } }
    public string Pattern { get { return Get_Pattern(); } }
    public string Range { get { return Get_Range(); } }
    public Direction Direction { get { return Get_Direction(); } }
    public string HelperFunction { get { return Get_HelperFunction(); } }
    public int MinimumTokens { get { return Get_MinimumTokens(); } }
    public string BeforeToken { get { return Get_BeforeToken(); } }
    public string AfterToken { get { return Get_AfterToken(); } }
    public bool Join { get { return Get_Join(); } }
    public string TextToReplace { get { return Get_TextToReplace(); } }
    public string ReplacementText { get { return Get_ReplacementText(); } }
    public bool IsCaseSensitive { get { return Get_IsCaseSensitive(); } }
    public bool IsTsdElement { get { return Get_IsTsdElement(); } }
    public int TokenOffset { get { return Get_TokenOffset(); } }
    public int RunLimit { get { return Get_RunLimit(); } }
    public bool RunAsStructureCommand { get { return Get_RunAsStructureCommand(); } }
    public string SubCommandVerb { get { return Get_SubCommandVerb(); } }
    public string FullSubCommand { get { return Get_FullSubCommand(); } }
    public string Regex { get; set; }
    public bool RegexExists { get { return this.Regex.IsNotBlank(); } }


    public string DefaultValue { get { return Get_DefaultValue(); } }
    public bool IsUnique { get { return Get_IsUnique(); } }
    public bool OrEnd { get { return Get_OrEnd(); } }
    public string SpecialRoutine { get { return Get_SpecialRoutine(); } }
    public bool NumericOnly { get { return Get_NumericOnly(); } }
    public bool AdvanceToEol { get { return Get_AdvanceToEol(); } }
    public string DataType { get { return Get_DataType(); } }
    public string DataFormat { get { return Get_DataFormat(); } }
    public string Math { get { return Get_Math(); } }
    public bool MathIsDone { get; set; }
    public string TokenType { get { return Get_TokenType(); } }
    public int NumberOfTokens { get { return Get_NumberOfTokens(); } }
    public string[] TokenTypes { get { return Get_TokenTypes(); } }
    public bool RemoveStoredToken { get { return Get_RemoveStoredToken(); } }
    public int StoredTokenIndex { get { return Get_StoredTokenIndex(); } }
    public int StoredTokenCount { get { return Get_StoredTokenCount(); } }
    public int TokensRequired { get { return Get_TokensRequired(); } }
    public List<Token> TokensToRemove { get { return Get_TokensToRemove(); } }
    public int RowIndex { get { return Get_RowIndex(); } }
    public string LiteralToken { get { return Get_LiteralToken(); } }
    public string Condition { get { return Get_Condition(); } }
    public bool ActiveToRun { get; set; }
    public bool Execute { get { return Get_Execute(); } }
    public string PeerCellName { get { return Get_PeerCellName(); } }
    public string ReportUnit { get { return Get_ReportUnit(); } }
    public string Command { get { return Get_Command(); } }
    public int PositionAdjust { get { return Get_PositionAdjust(); } }
    public bool IsProcessingReportUnit { get; set; }

    public string Code { get; private set; }
    public Verb _verb;
    public Verb Verb { get { return _verb; } }
    public bool IsCommentedOut { get; set; }
    public bool IsBreakPoint { get; set; }

    public bool Break { get; set; }
    public Text Text { get; set; }
    public int LineNumber { get; set; }
    public Tsd Parent { get; set; }
    public ExtractSpec ExtractSpec { get { return Get_ExtractSpec(); } }

    public Cmdx(Cmd cmd)
    {
      _cmd = cmd;
      this.MathIsDone = false;
      Compile();
    }
    
    private void Compile()
    {
      this.Break = _cmd.Break;
      this.Code = _cmd.Code;
      this.Parent = _cmd.Parent;
      this.ActiveToRun = _cmd.ActiveToRun;
      this.LineNumber = _cmd.LineNumber;
      this.IsCommentedOut = _cmd.IsCommentedOut;
      this.IsBreakPoint = _cmd.IsBreakPoint;

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

        // peel off regular expression if included
        if (this.Code.Contains("/rx:"))
        {
          string regex = String.Empty;
          int rxPos = this.Code.IndexOf("/rx:");
          int ptr = rxPos;
          while (ptr > -1 && this.Code[ptr] != '[')
            ptr--;
          if (ptr < 0)
            throw new Exception("Could not find the opening bracket enclosing the regular expression '/rx:' in the Cmdx with code '" + this.Code + "'.");
          int endPos = this.Code.IndexOf("$]", rxPos);
          if (endPos == -1)
            throw new Exception("Could not find the ending of the regular expression '$]' in the Cmdx with code '" + this.Code + "'.");
          this.Regex = this.Code.Substring(ptr + 1, endPos - ptr);
          this.Code = this.Code.Replace(this.Regex, "#REGEX#"); 
        }


        string parms = this.Code.GetTextBetween(Constants.OpenParen, Constants.CloseParen);

        _parms = parms.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < _parms.Length; i++)
        {
          if (this.Verb == Verb.ReplaceText)
          {
            if (i == 1)
            {
              string parm = _parms[i];
              if (parm.StartsWith("'") && parm.EndsWith("'"))
              {
                _parms[i] = parm.Substring(1, parm.Length - 2); 
              }
              else
              {
                _parms[i] = _parms[i].Trim(); 
              }
            }
            else
              _parms[i] = _parms[i].Trim(); 
          }
          else
          {
            _parms[i] = _parms[i].Trim();
          }
        }
      }
      catch(Exception ex)
      {
        throw new CxException(5, new object[] { this, ex }); 
      } 
    }

    private bool Get_IsTsdElement()
    {
      if (this.Verb != Verb.SetXml)
        return false;

      if (this.ParmCount < 2)
        return false;

      return this.Parms[1].Trim().ToLower() == "tsd"; 
    }

    private int Get_TokenOffset()
    {
      if (this.HasNoParms)
        return 0;

      foreach (var parm in this.Parms)
      {
        if (parm.ToLower().StartsWith("tokenoffset["))
        {
          string tokenOffset = parm.ToLower();
          tokenOffset = tokenOffset.Replace("tokenoffset[", String.Empty).Replace("]", String.Empty).Replace("+", String.Empty);
          int negator = 1;
          if (tokenOffset.StartsWith("-"))
          {
            negator = -1;
            tokenOffset = tokenOffset.Substring(1);
          }
          
          if (tokenOffset.IsNumeric())
            return tokenOffset.ToInt32() * negator;
        }
      }

      return 0;
    }

    private int Get_RunLimit()
    {
      string limitParm = this.Parms.GetEntry("runlimit*");
      if (limitParm.IsBlank())
        return -1;

      return limitParm.GetIntegerFromString();
    }

    private bool Get_RunAsStructureCommand()
    {
      if (this.HasNoParms)
        return false;

      if (this.Parms.GetEntry("structure").IsNotBlank())
        return true;

      return false;
    }

    private bool Get_UsePriorEnd()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("$priorend");
    }

    private bool Get_RemoveStoredToken()
    {
      return !this.Parms.ContainsEntry("retain"); 
    }

    private bool Get_PositionAtEnd()
    {
      if (this.HasNoParms)
        return true;

      return !this.Parms.ContainsEntry("beg");
    }

    private bool Get_IsRequired()
    {
      if (this.Verb == Verb.ReplaceText)
      {
        if (this.HasNoParms)
          return false;

        return this.Parms.ContainsEntry("req");
      }

      if (this.HasNoParms)
        return true;

      return !this.Parms.ContainsEntry("opt");
    }

    private string Get_DefaultValue()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms)
      {
        if (parm.ToLower().StartsWith("default="))
          return parm.Substring(8).Trim();
      }

      return String.Empty;
    }

    private string Get_ReportUnit()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms)
      {
        if (parm.ToLower().StartsWith("reportunit="))
          return parm.Substring(11).Trim();
      }

      return String.Empty;
    }

    private bool Get_Trim()
    {
      if (this.Verb == Verb.ReplaceText)
      {
        if (this.ParmCount < 3)
          return true;

        for (int i = 2; i < this.ParmCount; i++)
        {
          if (this.Parms[i] == "notrim")
            return false;
        }

        return true;
      }
      
      if (this.HasNoParms)
        return false;

      foreach (string parm in this.Parms)
      {
        string parm2 = parm.ToLower().Replace("trimright", String.Empty).Replace("trimleft", String.Empty);
        if (parm2.Contains("trim"))
          return true;
      }

      return false;
    }

    private bool Get_TrimRight()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("trimright");
    }

    private bool Get_TrimLeft()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("trimleft");
    }

    private bool Get_IsUnique()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("unique");
    }

    private bool Get_OrEnd()
    {
      if (_verb != Verb.SetTextEnd)
        return false;

      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("orEnd");
    }

    private string Get_Command()
    {
      if (_verb != Verb.ProcessingCommand)
        return String.Empty;

      if (this.ParmCount == 0)
        return String.Empty;

      string command = this.Parms[0];

      return command.GetTextBefore(Constants.OpenBracket).ToLower().Trim();
    }

    private int Get_PositionAdjust()
    {
      if (_verb != Verb.ProcessingCommand)
        return 0;

      if (this.ParmCount == 0)
        return 0;

      string adjust = this.Parms[0].GetTextBetween(Constants.OpenBracket, Constants.CloseBracket).ToLower().Trim();

      if (adjust.IsBlank())
        return 0;

      if (adjust == "next-line")
      {
        if (this.Text != null)
        {
          if (this.Text.BegPos < this.Text.RawText.Length)
          {
            int newLinePos = this.Text.RawText.IndexOf('\n', this.Text.BegPos + 1);
            int adjustment = newLinePos - this.Text.BegPos;
            if (adjustment < 1)
              return 0;
            return adjustment; 
          }
        }
      }

      int negativeFactor = 1;

      if (adjust.StartsWith("-"))
        negativeFactor = -1;

      adjust = adjust.Replace("-", String.Empty).Replace("+", String.Empty);
      if (adjust.IsBlank())
        return 0;

      if (!adjust.IsInteger())
        return 0;

      return adjust.ToInt32() * negativeFactor;
    }

    private string Get_SpecialRoutine()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms.ToLower())
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
    
    private int Get_NumberOfTokens()
    {
      switch (this.Verb)
      {
        case Verb.ExtractTextBefore:
          if (this.ParmCount < 3)
            return -1;
          string parm = this.Parms[2];
          if (parm.IsInteger())
            return parm.ToInt32();
          return -1;

        case Verb.ExtractNextTokens:
        case Verb.ExtractPriorTokensOfType:
          this.AssertParmCount(2);
          string numberOfTokensParm = this.Parms[1];
          if (!numberOfTokensParm.IsNumeric())
            throw new CxException(72, new object[] { this });
          int numberOfTokens = numberOfTokensParm.ToInt32();
          if (numberOfTokens < 1)
            throw new CxException(73, new object[] { this });
          return numberOfTokens;

        case Verb.SetVariable:
        case Verb.SetGlobalVariable:
          if (this.ParmCount < 3)
            throw new CxException(999, this);
          string subCommand = this.Parms[1].ToLower().Trim();
          if (subCommand == "extracttokens")
          {
            string nbrTokens = this.Parms[2];
            if (nbrTokens.IsNotNumeric())
              throw new CxException(999, this);
            return nbrTokens.ToInt32();
          }
          break;
          
      }

      return -1;
    }
    
    private string[] Get_TokenTypes()
    {
      switch (this.Verb)
      {        case Verb.ExtractNextTokens:
        case Verb.ExtractPriorTokensOfType:
          this.AssertParmCount(3);
          string tokenTypes = this.Parms[2];
          return tokenTypes.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries); 
      }

      return new string[0];
    }

    private bool Get_NumericOnly()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("numericonly");
    }

    private bool Get_AdvanceToEol()
    {
      if (this.HasNoParms)
        return false;

      return this.Parms.ContainsEntry("advancetoeol");
    }

    private string Get_Condition()
    {
      if (this.HasNoParms)
        return String.Empty;

      foreach (string parm in this.Parms)
      {
        if (parm.ToLower().StartsWith("cond="))
          return parm;
      }

      return String.Empty;
    }

    private bool Get_Execute()
    {
      if (this.RunLimit > -1 && _cmd.RunCount >= this.RunLimit)
        return false;  

      if (this.Verb == TextProcessing.Verb.SetTsdCondition)
        return true;

      string condition = String.Empty;
      if (this.Parent != null && this.Parent.Condition.IsNotBlank())
        condition = "cond=" + this.Parent.Condition;

      if (this.Condition.IsNotBlank())
        condition = this.Condition;

      if (condition.IsBlank())
        return true;

      if (this.Text == null)
        return true;

      Text t = this.Text;

      condition = condition.CondenseText(true).Trim();

      if (!condition.ToLower().StartsWith("cond="))
        throw new CxException(156, this);

      condition = condition.Substring(5);

      if (condition.IsBlank())
        return false;

      var tokens = condition.SplitByStringToken(Constants.ConditionRelOps, true);

      if (tokens.Length == 3)
        return EvaluateRelationalExpression(tokens);

      return EvaluateVariableValue(condition);

      //if ((t.LocalVariables == null || t.LocalVariables.Count == 0) && (Text.GlobalVariables == null || Text.GlobalVariables.Count == 0))
      //  return true;
      
      //string[] condTokens = condition.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
      //if (condTokens.Length != 2)
      //  return true;

      //string variableName = condTokens[1].Trim();

      //bool isNegated = false;
      //if (variableName.StartsWith("!"))
      //{
      //  isNegated = true;
      //  variableName = variableName.Substring(1); 
      //}

      //string variableValue = String.Empty;

      //if (t.LocalVariables != null && t.LocalVariables.ContainsKey(variableName))
      //  variableValue = t.LocalVariables[variableName].Trim();

      //if (variableValue.IsBlank())
      //{
      //  if (Text.GlobalVariables != null && Text.GlobalVariables.ContainsKey(variableName))
      //    variableValue = Text.GlobalVariables[variableName].Trim();
      //}

      //if (variableValue.IsBlank())
      //{
      //  if (isNegated)
      //    return false;
      //  else
      //    return true;
      //}

      //if (!variableValue.IsBoolean())
      //{
      //  if (isNegated)
      //    return false;
      //  else
      //    return true;
      //}

      //if (isNegated)
      //  return !variableValue.ToBoolean();
      //else
      //  return variableValue.ToBoolean();
    }

    private bool EvaluateRelationalExpression(string[] tokens)
    {
      Text t = this.Text;

      if (tokens == null || tokens.Length != 3)
        return false;

      string leftOperand = tokens[0];
      string relOp = tokens[1];
      string rightOperand = tokens[2];

      if (leftOperand.IsBlank() || rightOperand.IsBlank())
        return false;

      if (!relOp.In("!=,>=,<=,=,>,<"))
        return false;


      // The negation is handled here but no processing is implemented yet that uses the negation
      bool leftNegated = false;
      bool rightNegated = false;

      if (leftOperand.StartsWith("!"))
      {
        leftNegated = true;
        leftOperand = leftOperand.Substring(1);
      }

      if (rightOperand.StartsWith("!"))
      {
        rightNegated = true;
        rightOperand = rightOperand.Substring(1);
      }

      if (leftOperand.IsBlank() || rightOperand.IsBlank())
        return false;

      var leftOperandType = GetOperandType(leftOperand);
      var rightOperandType = GetOperandType(rightOperand);

      if (leftOperandType == OperandType.Literal)
        leftOperand = leftOperand.Substring(1, leftOperand.Length - 2); 

      if (rightOperandType == OperandType.Literal)
        rightOperand = rightOperand.Substring(1, rightOperand.Length - 2);

      object leftOperandValue = GetOperandValue(leftOperand, leftOperandType);
      object rightOperandValue = GetOperandValue(rightOperand, rightOperandType);

      // null operand values can only result from non-existent variables.
      if (leftOperandValue == null || rightOperandValue == null)
        return false;

      // avoid cross-type compares (numeric vs string), allowing the right side to be the determinant
      // (left can't coerce right, but right can coerce left)
      if (rightOperandType == OperandType.Decimal || rightOperandType == OperandType.Integer)
      {
        if (leftOperandType == OperandType.Variable || leftOperandType == OperandType.Literal)
        {
          if (leftOperandValue.ToString().IsInteger())
          {
            leftOperandValue = leftOperandValue.ToString().ToInt32();
            leftOperandType = OperandType.Integer;
          }
          else
          {
            if (leftOperandValue.ToString().IsDecimal(true))
            {
              leftOperandValue = leftOperandValue.ToString().ToDecimal(); 
              leftOperandType = OperandType.Decimal;
            }
          }
        }
        
        if (leftOperandValue.ToString().IsDecimal())
        {
          decimal leftDec = leftOperandValue.ToDecimal();
          decimal rightDec = rightOperandValue.ToDecimal();

          switch (relOp)
          {
            case "=": return leftDec == rightDec;
            case "!=": return leftDec != rightDec;
            case ">": return leftDec > rightDec;
            case "<": return leftDec < rightDec;
            case ">=": return leftDec >= rightDec;
            case "<=": return leftDec <= rightDec;
          }
        }

        throw new Exception ("Left operand type is '" + leftOperandType.ToString() + "' and right operand type is '" + rightOperandType.ToString() + "' " +
                              "and the left operand '" + leftOperand + "' cannot be coerced to a numeric type.");
      }

      // all comparisions are based on the value we are comparing (the left operand)
      // all "fall-throughs" return false, only comparisions evaluating to true will return true;

      switch (leftOperandType)
      {
        case OperandType.Literal:
        case OperandType.Variable:
          switch (rightOperandType)
          {
            case OperandType.Literal:
            case OperandType.Variable:
              int result = leftOperandValue.ToString().CompareTo(rightOperandValue.ToString());
              switch (relOp)
              {
                case "=": return result == 0;
                case "!=": return result != 0;
                case ">": return result > 0;
                case ">=": return !(result < 0);
                case "<": return result < 0;
                case "<=": return !(result > 0);
              }
              return false;
          }
          break;

        case OperandType.Decimal:
        case OperandType.Integer:
          decimal leftDecimalValue = leftOperandValue.ToString().ToDecimal();
          decimal rightDecimalValue = 0M;
          switch (rightOperandType)
          {
            case OperandType.Decimal:
            case OperandType.Integer:
              throw new Exception("We should never get to this point - if we do, find out why.");
              //rightDecimalValue = rightOperandValue.ToString().ToDecimal();

              //switch (relOp)
              //{
              //  case "=": return leftDecimalValue == rightDecimalValue;
              //  case "!=": return leftDecimalValue != rightDecimalValue;
              //  case ">": return leftDecimalValue > rightDecimalValue;
              //  case ">=": return leftDecimalValue >= rightDecimalValue;
              //  case "<": return leftDecimalValue < rightDecimalValue;
              //  case "<=": return leftDecimalValue <= rightDecimalValue;
              //}
              //return false;

            default: // ( right is literal or variable)
              if (rightOperandValue.ToString().IsDecimal()) // integer or decimal literals will pass this test
              {
                rightDecimalValue = rightOperandValue.ToString().ToDecimal();
                switch (relOp)
                {
                  case "=": return leftDecimalValue == rightDecimalValue;
                  case "!=": return leftDecimalValue != rightDecimalValue;
                  case ">": return leftDecimalValue > rightDecimalValue;
                  case ">=": return leftDecimalValue >= rightDecimalValue;
                  case "<": return leftDecimalValue < rightDecimalValue;
                  case "<=": return leftDecimalValue <= rightDecimalValue;
                }
              }
                
              return false; // can't compare - return false;
          }
      }

      return false;
    }

    private bool EvaluateVariableValue(string variableExpression)
    {
      Text t = this.Text;

      string variableValue = String.Empty;
      string varEx = variableExpression.Trim();

      if (varEx.IsBlank())
        return false;

      bool negated = false;
      if (varEx.StartsWith("!"))
      {
        negated = true; 
        varEx = varEx.Substring(1);
      }

      if (varEx.IsBlank())
        return false;

      if (t.LocalVariables.ContainsKey(varEx))
      {
        variableValue = t.LocalVariables[varEx];
        if (negated)
          return !variableValue.ToBoolean();
        else
          return variableValue.ToBoolean(); 
      }

      if (Text.GlobalVariables.ContainsKey(varEx))
      {
        variableValue = Text.GlobalVariables[varEx];
        if (negated)
          return !variableValue.ToBoolean();
        else
          return variableValue.ToBoolean();
      }

      if (negated)
        return true; 

      return false;
    }

    private OperandType GetOperandType(string operand)
    {
      if (operand.IsBlank())
        return OperandType.Variable;

      operand = operand.Trim();

      if (operand.StartsWith("'") && operand.EndsWith("'"))
        return OperandType.Literal;

      if (operand.IsDecimal(true))
        return OperandType.Decimal;

      if (operand.IsInteger())
        return OperandType.Integer;

      return OperandType.Variable;
    }

    private object GetOperandValue(string operand, OperandType operandType)
    {
      Text t = this.Text;

      try
      {
        switch (operandType)
        {
          case OperandType.Literal: return operand;
          case OperandType.Integer: return operand.ToInt32();
          case OperandType.Decimal: return operand.ToDecimal();
        }

        // default is OperandType.Variable

        if (t.LocalVariables != null && t.LocalVariables.ContainsKey(operand))
          return t.LocalVariables[operand];

        if (Text.GlobalVariables != null && Text.GlobalVariables.ContainsKey(operand))
          return Text.GlobalVariables[operand];

        return null;
      }
      catch 
      { 
        return String.Empty; 
      }
    }

    private string Get_ParmString()
    {
      if (this.HasNoParms)
        return String.Empty;

      return this.Parms.StringArrayToString();
    }

    public TokenSearchCriteria Get_TokenSearchCriteria()
    {
      try
      {
        var tsc = new TokenSearchCriteria();
        tsc.Direction = this.Direction;
        tsc.DataType = this.ConvertDataType(this.DataType);
        tsc.Pattern = this.Pattern;
        tsc.Trim = this.Trim;
        tsc.TrimRight = this.Get_TrimRight();
        tsc.TrimLeft = this.Get_TrimLeft();
        tsc.TextToFind = this.TextToFind;
        tsc.MatchCase = this.Parms.ContainsEntry("matchcase");
        tsc.BeforeToken = this.Get_BeforeToken();
        tsc.AfterToken = this.Get_AfterToken();
        tsc.Join = this.Get_Join();
        tsc.IsRequired = this.IsRequired;
        tsc.DataName = this.DataName;
        tsc.DataFormat = this.DataFormat;
        tsc.Math = this.Math;
        tsc.RemoveStoredToken = this.RemoveStoredToken;
        tsc.StoredTokenIndex = this.StoredTokenIndex;

        if (this.Verb == Verb.ExtractToken)
        {
          if (this.ParmCount > 1)
          {
            string parm2 = this.Parms[1].Trim();
            if (parm2.ToLower().StartsWith("lit["))
            {
              tsc.LiteralValue = parm2.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket).Trim(); 
            }
          }
        }
        
        return tsc;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(119, this, ex); 
      }
    }

    public DataType ConvertDataType(string dataType)
    {
      string type = dataType.ToLower().Trim();

      switch (dataType)
      {
        case "dec": return TextProcessing.DataType.Decimal;
        case "decn": return TextProcessing.DataType.DecimalPeriodOptional;
        case "int": return TextProcessing.DataType.Integer;
        case "pct": return TextProcessing.DataType.Percentage;
        case "pctn": return TextProcessing.DataType.PercentagePeriodOptional;
        case "date": return TextProcessing.DataType.Date;
        case "mm/yyyy": return TextProcessing.DataType.MMYYYY;
        case "time": return TextProcessing.DataType.Time;
      }

      return TextProcessing.DataType.String;
    }

    public string Get_TextToFind()
    {
      try
      {
        if (this.HasNoParms && _verb != Verb.Truncate)
          return String.Empty;

        switch (_verb)
        {
          case Verb.Truncate:
            if (this.ParmCount < 3)
              throw new CxException(182, this);
            return this.Parms[1];

          case Verb.SetTextStart:
          case Verb.LocateToken:
            return this.Parms[0];

          case Verb.SetTextEnd:
            if (this.Parms[0].Trim().ToLower() == "trim")
              return String.Empty;
            else
              return this.Parms[0].Trim();

          case Verb.ExtractTextBefore:
            this.AssertParmCount(2);
            if (this.Parms[1].IsBlank())
              throw new CxException(67, new object[] { this });
            return this.Parms[1].Trim();

          case Verb.SetVariable:
          case Verb.SetGlobalVariable:
            if (this.ParmCount < 2)
              return String.Empty;
            string subCommandVerb = this.SubCommandVerb;
            if (!subCommandVerb.In("find,extractValue",false))
              return String.Empty;
            for (int i = 1; i < this.Parms.Length; i++)
            {
              string parm = this.Parms[i].Trim();
              // strip off anything after an open bracket if there is text before the open bracket
              if (parm.Contains("[") && !parm.StartsWith("["))
                parm = parm.Substring(0, parm.IndexOf("[")); 
              if (parm.ToLower().In("stored,opt,join,extractvalue,extracttoeol,lit,trim,find,expression,var"))
                continue;
              if (parm.ToLower().StartsWithIn("cond=,before="))
                continue;
              return parm;
            }

            if (subCommandVerb.ToLower() == "find")
              throw new CxException(60, new object[] { this });
            return String.Empty;

          case Verb.ExtractStoredTokenBefore:
            this.AssertParmCount(2);
            string tokenToFind = this.Parms[1].Trim();
            return tokenToFind; 

          default:
            return String.Empty;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(140, new object[] { this, ex });
      }
    }

    public int Get_StartPosition()
    {
      if (this.Verb == Verb.Truncate)
      {
        if (this.ParmCount != 3)
          throw new CxException(182, this);

        string pos = this.Parms[0];
        if (pos.IsNumeric())
          return pos.ToInt32();

        // use current position
        if (pos == "*") 
          return -1;  

        throw new CxException(183, this);
      }


      if (this.Verb != Verb.LocateToken)
        return -1;

      if (this.ParmCount < 2)
        return -1;

      if (this.Parms[1].IsInteger())
        return this.Parms[1].ToInt32();

      return -1;
    }

    public string Get_Zap()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          string p = parm.CondenseText();
          if (p.ToLower().StartsWith("zap="))
          {
            return parm.Substring(4).Trim();
          }
        }
        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(141, this, ex);
      }
    }

    public string Get_VariableName()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          string p = parm.CondenseText();
          if (p.ToLower().StartsWith("varname="))
          {
            string varName = parm.Substring(8);
            if (varName.IsBlank())
              throw new CxException(142, this);
            return varName;
          }
        }
        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(143, this, ex);
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
          case Verb.SetGlobalVariable:
          case Verb.ExtractToken:
          case Verb.ExtractLiteralToken:
          case Verb.ExtractNextTokenOfType:
          case Verb.ExtractPriorTokenOfType:
          case Verb.ExtractPriorTokensOfType:
          case Verb.ExtractNextToken:
          case Verb.ExtractNextTokens:
          case Verb.ExtractNextLine:
          case Verb.ExtractStoredToken:
          case Verb.ExtractStoredTokens:
          case Verb.ExtractStoredTokenBefore:
          case Verb.ExtractTextBefore:
          case Verb.ExtractFromPeerCell:
          case Verb.ExtractXmlElementValue:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            if (parm.Contains("["))
              dataName = parm.GetTextAfter(Constants.OpenAndCloseBrackets);
            else
              dataName = parm;
            int periodCount = dataName.CountOfChar('.');
            if (_verb == TextProcessing.Verb.SetVariable || _verb == TextProcessing.Verb.SetGlobalVariable)
            {
              if (periodCount == 1)
              {
                int lastPeriod = dataName.LastIndexOf('.');
                if (lastPeriod > -1)
                {
                  dataName = dataName.Substring(0, lastPeriod);
                }
              }
            }
            else
            {
              if (periodCount > 1)
              {
                int lastPeriod = dataName.LastIndexOf('.');
                if (lastPeriod > -1)
                {
                  dataName = dataName.Substring(0, lastPeriod);
                }
              }
            }

            if (dataName.IsBlank())
              throw new CxException(8, new object[] { this });
            return this.Text.ResolveVariables(dataName);

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

    public int Get_RowIndex()
    {
      try
      {
        switch (_verb)
        {
          case Verb.SetRowIndex:
            this.AssertParmCount(1);
            string parm = this.Parms[0];

            if (parm == "+")
              return 99999;

            if (parm.IsNotNumeric())
              throw new CxException(144, new object[] { this });
            return parm.ToInt32();
        }

        return -1;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(9, new object[] { this, ex });
      }
    }

    public string Get_LiteralToken()
    {
      try
      {
        switch (_verb)
        {
          case Verb.ExtractLiteralToken:
            this.AssertParmCount(2);
            string literalToken = this.Parms[1];
            if (literalToken.IsBlank())
              throw new CxException(145, new object[] { this });
            return this.Text.ResolveVariables(literalToken);
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(146, new object[] { this, ex });
      }

    }

    public string Get_PeerCellName()
    {
      try
      {
        switch (_verb)
        {
          case Verb.ExtractFromPeerCell:
            this.AssertParmCount(2);
            return this.Parms[1];
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(147, new object[] { this, ex });
      }
    }

    public string Get_Math()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        for (int i = 0; i < this.ParmCount; i++)
        {
          if (this.Parms[i].ToLower().StartsWith("math="))
            return this.Parms[i].Substring(5);
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(148, new object[] { this, ex });
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
          case Verb.RemoveStoredTokens:
          case Verb.SetTextEnd:
          case Verb.LocateToken:
            return String.Empty;

          default:
            if (this.Parms.Length == 0)
              return String.Empty;
            parm = this.Parms[0];
            dataType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
            if (dataType.IsBlank() || dataType.Trim().ToLower() == "str")
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
          case Verb.ExtractToken:
          case Verb.ExtractNextToken:
          case Verb.ExtractNextTokens:
          case Verb.ExtractStoredToken:
          case Verb.ExtractStoredTokens:
          case Verb.ExtractStoredTokenBefore:
          case Verb.ExtractFromPeerCell:
          case Verb.ExtractNextTokenOfType:
          case Verb.ExtractPriorTokenOfType:
          case Verb.ExtractPriorTokensOfType:
          case Verb.ExtractTextBefore:
          case Verb.SetVariable:
          case Verb.SetGlobalVariable:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            int periodCount = parm.CountOfChar('.');
            // variable names will not contain a dot between table and field
            if (_verb == Verb.SetVariable || _verb == Verb.SetGlobalVariable)
            {
              if (periodCount > 0)
              {
                int lastPeriod = parm.LastIndexOf('.');
                dataFormat = parm.Substring(lastPeriod + 1);
                dataFormat.AssertValidDataFormat(this);
              }
            }
            else
            {
              if (periodCount > 1)
              {
                int lastPeriod = parm.LastIndexOf('.');
                dataFormat = parm.Substring(lastPeriod + 1);
                dataFormat.AssertValidDataFormat(this);
              }
            }
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

    public string Get_Pattern()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          string parmValue = parm.CondenseText();

          if (parmValue.ToLower().Trim().StartsWith("[p'"))
          {
            int patternBeg = parmValue.ToLower().IndexOf("[p'");
            if (patternBeg == -1)
              return String.Empty;

            int patternEnd = parmValue.IndexOf("']", patternBeg);

            if (patternEnd == -1)
              return String.Empty;

            string pattern = parmValue.Substring(patternBeg + 3, patternEnd - (patternBeg + 3));

            return pattern;
          }
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(122, this, ex);
      }
    }

    public string Get_Range()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          string parmValue = parm.CondenseText();

          if (parmValue.ToLower().Trim().StartsWith("[r'"))
          {
            int rangeBeg = parmValue.ToLower().IndexOf("[r'");
            if (rangeBeg == -1)
              return String.Empty;

            int rangeEnd = parmValue.IndexOf("']", rangeBeg);

            if (rangeEnd == -1)
              return String.Empty;

            string range = parmValue.Substring(rangeBeg + 3, rangeEnd - (rangeBeg + 3));

            return range;
          }
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(138, this, ex);
      }
    }

    public Direction Get_Direction()
    {
      try
      {
        if (this.ParmCount == 1)
        {
          if (this.Parms[0].In("0,end"))
            return Direction.Next;
        }

        if (this.Verb == Verb.SetTextPosition)
        {
          if (this.ParmCount < 4)
            throw new CxException(111, this); 

          switch(this.Parms[0].ToLower().Trim())
          {
            case "back": return Direction.Prev;
            case "next": return Direction.Next;
            default:
              throw new CxException(112, this);
          }
        }

        if (this.ParmCount < 2)
          return Direction.Next;

        string direction = this.Parms[1].ToLower().Trim();
        if (direction.StartsWith("prev"))
          return Direction.Prev;
        if (direction.StartsWith("stored"))
          return Direction.Stored;
        if (direction.StartsWith("lit"))
          return Direction.Literal;
        if (direction.StartsWith("var"))
          return Direction.Variable;
        // default is next (forward)
        return Direction.Next;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(86, new object[] { this, ex });
      }
    }

    public string Get_HelperFunction()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          if (parm.ToLower().StartsWith("hf="))
            return parm.ToLower().Trim();
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(127, this, ex); 
      }
    }

    public string Get_BeforeToken()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          if (parm.ToLower().StartsWith("before="))
            return parm.Substring(7);
          if (parm.ToLower().StartsWith("before["))
          {
            int pos = parm.IndexOf(']', 6);
            if (pos == -1)
              throw new CxException(299, this);
            string token = parm.Substring(7, pos - 7);
            if (token.StartsWith("`"))
              return token.Substring(1);
            else
              return "[" + token + "]";
          }
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(141, this, ex); 
      }
    }

    public string Get_AfterToken()
    {
      try
      {
        if (this.ParmCount == 0)
          return String.Empty;

        foreach (string parm in this.Parms)
        {
          if (parm.ToLower().StartsWith("after="))
            return parm.Substring(7);
          if (parm.ToLower().StartsWith("after["))
          {
            int pos = parm.IndexOf(']', 5);
            if (pos == -1)
              throw new CxException(299, this);
            string token = parm.Substring(6, pos - 6);
            if (token.StartsWith("`"))
              return token.Substring(1);
            else
              return "[" + token + "]";
          }
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(141, this, ex); 
      }
    }

    public bool Get_Join()
    {
      try
      {
        if (this.ParmCount == 0)
          return false;

        foreach (string parm in this.Parms)
        {
          if (parm.ToLower() == "join")
            return true;
        }

        return false;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(140, this, ex); 
      }
    }

    public int Get_MinimumTokens()
    {
      try
      {
        if (this.ParmCount == 0)
          return -1;

        foreach (string parm in this.Parms)
        {
          if (parm.ToLower().StartsWith("mintokens="))
          {
            string minTokens = parm.ToLower().Trim().CondenseText().Replace("mintokens=", String.Empty);
            if (!minTokens.IsInteger())
              throw new CxException(130, this, minTokens);
            return minTokens.ToInt32();
          }
        }

        return -1;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(129, this, ex); 
      }
    }

    public int Get_UnitCount()
    {
      try
      {
        if (this.Verb == Verb.SetTextPosition)
        {
          if (this.ParmCount == 1)
          {
            if (this.Parms[0].In("0,end"))
              return 0;
          }

          if (this.ParmCount < 4)
            throw new CxException(111, this);

          if (!this.Parms[1].IsInteger())
            throw new CxException(117, this);

          return this.Parms[1].ToInt32(); 
        }

        throw new CxException(115, this);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(113, this, ex); 
      }
    }

    public TextUnit Get_TextUnit()
    {
      try
      {
        if (this.Verb == Verb.SetTextPosition)
        {
          if (this.ParmCount == 1)
          {
            if (this.Parms[0].In("0,end"))
              return TextUnit.Character;
          }

          if (this.ParmCount < 4)
            throw new CxException(111, this);

          switch (this.Parms[2].ToLower().Trim())
          {
            case "character": return TextUnit.Character;
            case "token": return TextUnit.Token;
            case "line": return TextUnit.Line;
            default:
              throw new CxException(118, this); 
          }
        }

        throw new CxException(116, this);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(114, this, ex); 
      }
    }

    public PositionAt Get_PositionAt()
    {
      try
      {
        if (this.Verb == Verb.SetTextPosition)
        {
          if (this.ParmCount == 1)
          {
            if (this.Parms[0] == "0")
              return PositionAt.Start;
            if (this.Parms[0] == "end")
              return PositionAt.End;
          }

          if (this.ParmCount < 4)
            throw new CxException(111, this);

          switch (this.Parms[3].ToLower().Trim())
          {
            case "start": return PositionAt.Start;
            case "end": return PositionAt.End;
            case "before": return PositionAt.Before;
            case "after": return PositionAt.After;
            default:
              throw new CxException(120, this); 
          }
        }

        throw new CxException(116, this);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(114, this, ex); 
      }
    }

    public TruncateDirection Get_TruncateDirection()
    {
      try
      {
        if (this.Verb != Verb.Truncate)
          throw new CxException(184, this);

        if (this.ParmCount != 3)
          throw new CxException(182, this);

        switch (this.Parms[2].ToLower().Trim())
        {
          case "before": return TruncateDirection.Before;
          case "after": return TruncateDirection.After;
        }

        throw new CxException(185, this);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(186, this, ex); 
      }
    }

    public int Get_StoredTokenIndex()
    {
      string parm = String.Empty;

      try
      {
        switch(_verb)
        {
          case Verb.ExtractToken:
            if (this.ParmCount < 2)
              return 0;
            parm = this.Parms[1].ToLower().Trim();
            if (parm.StartsWith("stored"))
            {
              if (this.Parms.ContainsEntry("last"))
                return 99999;

              if (this.Parms.ContainsEntry("join"))
                return 99998;

              string indexValue = parm.GetBracketedText();
              if (indexValue.IsNumeric())
                return indexValue.ToInt32();
              return 0;
            }
            return 0;

          case Verb.ExtractStoredToken:
          case Verb.ExtractStoredTokens:
            if (this.ParmCount < 2)
              return 0;

            parm = this.Parms[1].ToLower().Trim();

            if (parm.StartsWith("cond="))
              return 0; 

            if (parm == "last")
              return 99999;

            if (parm == "join")
              return 99998;

            if (parm.IsNumeric())
              return parm.ToInt32();

            throw new CxException(21, new object[] { this });

          case Verb.SetGlobalVariable:
          case Verb.SetVariable:
            if (this.ParmCount == 0)
              return 0;
            int parmCount = -1;
            foreach (string parmValue in this.Parms)
            {
              parmCount++;
              if (parmCount == 0)
                continue;
              string parmValueLc = parmValue.Trim().ToLower();
              if (parmValueLc == "join")
                return 99998;
              if (parmValueLc == "last")
                return 99999;
              if (parmValueLc.IsInteger())
                return parmValueLc.ToInt32();
            }
            return 0;

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

    public int Get_TokensRequired()
    {
      try
      {
        switch(_verb)
        {
          case Verb.TokenizeNextLine:
            if (this.ParmCount == 0)
              return -1;

            foreach (string parm in this.Parms)
            {
              if (parm.IsInteger())
                return parm.ToInt32();
            }
            return -1;
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
          case Verb.SetGlobalVariable:
            this.AssertParmCount(1);
            parm = this.Parms[0];
            if (parm.Contains("["))
              tokenType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);

            if (tokenType.IsBlank())
              throw new CxException(16, new object[] { this });
            return tokenType;

          case Verb.ExtractStoredTokenBefore:
            this.AssertParmCount(2);
            parm = this.Parms[1].ToLower().Trim();
            if (parm.StartsWith("[") && parm.EndsWith("]"))
              return parm.Replace("[", String.Empty).Replace("]", String.Empty);
            return String.Empty;

          default:
            if (this.Parms.Length == 0)
              return String.Empty;

            parm = this.Parms[0];
            if (!parm.Contains("["))
              return String.Empty;

            tokenType = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket).ToLower();

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

    public string Get_FullSubCommand()
    {
      try
      {
        switch (_verb)
        {
          case Verb.SetVariable:
          case Verb.SetGlobalVariable:
            this.AssertParmCount(2);
            for (int i = 1; i < this.ParmCount; i++)
            {
              string subCommandVerb = this.Parms[i].GetTextBefore(Constants.OpenBracket);
              if (subCommandVerb.IsValidSubCommandVerb())
                return this.Parms[i].Trim();
            }
            throw new CxException(17, new object[] { this });

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

    public string Get_SubCommandVerb()
    {
      try
      {
        switch (_verb)
        {
          case Verb.SetVariable:
          case Verb.SetGlobalVariable:
            this.AssertParmCount(2);
            for (int i = 1; i < this.ParmCount; i++)
            {
              string subCommandVerb = this.Parms[i].GetTextBefore(Constants.OpenBracket);
              if (subCommandVerb.IsValidSubCommandVerb())
                return subCommandVerb;
            }
            throw new CxException(17, new object[] { this });

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
          case Verb.RemoveStoredTokens:
            if (this.Parms == null || this.Parms.Length == 0)
              throw new CxException(80, new object[] { this });

            foreach (var parm in this.Parms)
            {
              string tokenText = parm.GetTextBefore(Constants.OpenBracket);
              if (tokenText.StartsWith("cond="))
                continue;
              string options = parm.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket).ToLower();
              if (tokenText.IsBlank())
                throw new CxException(82, new object[] { this });
              if (parm.Contains('[') && options.IsBlank())
                throw new CxException(81, new object[] { this });

              var tokenToRemove = new Token();
              tokenToRemove.Text = tokenText;
              tokenToRemove.IsRequired = !options.Contains("opt");
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

    public void SetTsdCondition()
    {
      try
      {
        if (this.Parent == null)
          throw new CxException(163, this); 

        if (this.ParmCount < 1)
          throw new CxException(161, this); 

        string condition = this.Parms[0].Trim();

        if (condition.ToLower() == "off")
        {
          this.Parent.Condition = String.Empty;
          return; 
        }

        string onOff = "on";
        if (this.ParmCount > 1)
          onOff = this.Parms[1].Trim().ToLower();

        if (!onOff.In("on,off"))
          throw new CxException(162, this);

        switch (onOff)
        {
          case "on":
            this.Parent.Condition = condition;
            break;

          case "off":
            this.Parent.Condition = String.Empty;
            break;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(160, this, ex);
      }
    }

    private string Get_TextToReplace()
    {
      if (this.ParmCount < 1)
        throw new CxException(165, this);

      string textToReplace = this.Parms[0].Trim();

      if (textToReplace.IsBlank())
        throw new CxException(166, this);

      return textToReplace;
    }

    private string Get_ReplacementText()
    {
      if (this.ParmCount < 2)
        return String.Empty;

      string replacementText = this.Parms[1];

      return replacementText;
    }

    private bool Get_IsCaseSensitive()
    {
      if (this.Verb != Verb.ReplaceText)
        return false;

      if (this.ParmCount < 3)
        return false;

      for (int i = 2; i < this.ParmCount; i++)
      {
        if (this.Parms[i].ToLower() == "cs")
          return true;
      }

      return false;
    }

    private ExtractSpec Get_ExtractSpec()
    {
      if (this.Parent == null)
        return null;

      Tsd tsdParent = this.Parent;

      if (tsdParent.ExtractSpec != null)
        return tsdParent.ExtractSpec; 

      while (tsdParent != null)
      { 
        if (tsdParent.Parent != null)
          tsdParent = tsdParent.Parent;

        if (tsdParent.ExtractSpec != null)
          return tsdParent.ExtractSpec;
      }

      return null; 
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

    public static bool IsValidSubCommandVerb(this string value)
    {
      if (value.IsBlank())
        return false;
      
      string subCommandVerb = value.GetTextBefore(Constants.OpenBracket).ToLower();

      switch (subCommandVerb)
      {
        case "extractvalue":
        case "extracttokens":
        case "extracttoeol":
        case "getvariable":
        case "expression":
        case "find":
        case "stored":
        case "var":
        case "lit": return true;
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

      switch (dataFormat.ToLower().Trim())
      {
        case "h24:mm:ss":
        case "h12:mm:ss":
        case "ccyymmdd":
        case "mm/dd/yyyy":
        case "stripcommas":
        case "strippunctuation":
          return;
      }

      throw new CxException(22, new object[] { cmdx, dataFormat, allowBlank }); 
    }

  }
}
