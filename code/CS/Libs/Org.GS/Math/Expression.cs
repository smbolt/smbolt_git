using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.Math
{
  public class Expression
  {
    private char[] opDelim = new char[] { '(', ')', '+', '-', '/', '*', '^' };

    private string e;
    public string ParentExpression { get; set; }
    private Dictionary<string, string> _variables;
    private EquationResultType _resultType;
    private int ID { get; set; }

    public decimal Value { get; set; }
    public SubExpressionType SubExpressionType { get; set; }
    public string SubExpTypeAbbr 
    {
        get { return this.GetSubExpTypeAbbr(); }
    }

    public bool IsComputationComplete { get; set; }

    public int BeginPosition { get; set; }
    public int EndPosition { get; set; }

    public string LeftOperand { get; set; }
    public int LeftOperandBegin { get; set; }
    public int LeftOperandEnd { get; set; }
    public string RightOperand { get; set; }
    public int RightOperandBegin { get; set; }
    public int RightOperandEnd { get; set; }

    public MathOperation MathOperation { get; set; }
    public int MathOperatorPosition { get; set; }

    public Expression(string expressionString, int beginPosition, int endPosition, int id, string parentExpression, Dictionary<string, string> variables)
    {
      this.ID = ++id;
      e = expressionString.Trim();
      this.ParentExpression = parentExpression;

      this.SubExpressionType = SubExpressionType.NotSet;
      this.BeginPosition = beginPosition;
      this.EndPosition = endPosition;
      this.LeftOperand = String.Empty;
      this.RightOperand = String.Empty;
      this.MathOperation = MathOperation.NotSet;

      this.LeftOperandBegin = -1;
      this.LeftOperandEnd = -1;
      this.RightOperandBegin = -1;
      this.RightOperandEnd = -1;
      this.MathOperatorPosition = -1;

      _variables = variables;

      _resultType = EquationResultType.Integer;
      this.IsComputationComplete = false;

      this.Value = 0;
    }

    public string ComputeValue(EquationResultType equationResultType)
    {
      if (e.IsBlank())
        return String.Empty;

      _resultType = equationResultType;
      this.Value = 0;
      string rv = String.Empty;
      g.LogToMemory(String.Empty);

      Expression subExpression = GetNextSubExpression();

      if (subExpression == null)
        return String.Empty;

      if (!subExpression.IsComputationComplete)
      {
        WriteTrace(subExpression, String.Empty, 1);
        rv = subExpression.ComputeValue(equationResultType);
      }
      else
      {
        rv = subExpression.GetStringValue();
      }

      WriteTrace(subExpression, rv, 2);

      if (SubExpressionSpansEntireExpression(subExpression))
          return rv;

      Expression recomposedExpression = RecomposeExpression(subExpression, rv);
      string returnValue = recomposedExpression.ComputeValue(equationResultType);
      WriteTrace(subExpression, returnValue, 3);
      return returnValue;
    }

    private Expression GetNextSubExpression()
    {
      if (e.Contains("("))
        return GetParentheticalExpression();
      else
        if (e.ContainsHighLevelMathOperator())
          return GetHighOrderOperationExpression();
        else
          if (e.ContainsLowLevelMathOperator())
            return GetLowOrderOperationExpression();

      // is this needed?
      // maybe just return (or set this one as) completed...?
      return null;
    }

    public Expression GetParentheticalExpression()
    {
      int op = -1; // open parenthesis position
      int cp = -1; // close parenthesis position
            
      op = e.IndexOf("(");

      if (op == -1)
        throw new Exception("Open parenthesis not found in expression '" + e + "'.");

      int parDepth = 1;

      for(int i = op + 1; i < e.Length; i++)
      {
        if (e[i] == '(')
          parDepth++;
        if (e[i] == ')')
          parDepth--;

        if (parDepth == 0)
        {
          cp = i;
          break;
        }                
      }

      if (cp == -1)
          throw new Exception("Close parenthesis not found in expression '" + e + "' should be paired with open parenthesis at position '" + op.ToString() + "'.");
            
      string subExpression = e.Substring(op + 1, (cp - op) - 1);
      Expression subEx = new Expression(subExpression, op, cp, this.ID, this.e, this._variables);
      subEx.SubExpressionType = SubExpressionType.Parenthetical;
      return subEx;
    }

    public Expression GetHighOrderOperationExpression()
    {
      this.MathOperatorPosition = LocateHighOrderMathOperator();
      this.LocateLeftOperand(true);
      this.LocateRightOperand(true);
      Expression subEx = null;

      if (OperandsSpanEntireExpression())
      {
          subEx = ComputeResultantExpression(SubExpressionType.HighOrder);
          WriteTrace(subEx, subEx.GetStringValue(), 5);
          return subEx;
      }

      subEx = ComputeAndRecompose(SubExpressionType.HighOrder); 
      WriteTrace(subEx, String.Empty, 6);
      return subEx;
    }

    public Expression GetLowOrderOperationExpression()
    {
      this.MathOperatorPosition = LocateLowOrderMathOperator();
      this.LocateLeftOperand(false);
      this.LocateRightOperand(false);
            
      if (OperandsSpanEntireExpression())
          return ComputeResultantExpression(SubExpressionType.LowOrder);

      // can we ever get here?
      return null;
    }

    private void WriteTrace(Expression subExpression, string returnValue, int point)
    {
      g.LogToMemory(this.ID.ToString("000") + " " +
              this.BeginPosition.ToString("000") + " " +
              this.EndPosition.ToString("000") + " " +
              subExpression.GetSubExpTypeAbbr() + " " +
              subExpression.IsComputationComplete.ToString().Substring(0, 1) + " " +
              point.ToString().Trim() + "   " + 
              this.e.PadTo(29) +
              subExpression.e.PadTo(29) + 
              returnValue);
    }

    private decimal GetValueOfOperand(string operand)
    {
      decimal value = 0;

      if (this._variables.ContainsKey(operand))
        value = Decimal.Parse(this._variables[operand]);
      else
        if (operand.IsNumeric() || operand.IsDecimal())
          value = Decimal.Parse(operand);
        else
          throw new Exception("Invalid operand '" + operand + "' in expression + '" + e + "'.");

      return value;
    }

    private MathOperation GetMathOperation()
    {
      switch (e)
      {
        case "+":
          return MathOperation.Add;
        case "-":
          return MathOperation.Subtract;
        case "*":
          return MathOperation.Multiply;
        case @"/":
          return MathOperation.Divide;
        case "^":
          return MathOperation.Exponent;
      }

      return MathOperation.NotSet;
    }

    public string GetStringValue()
    {
      string v = Convert.ToString(this.Value);

      if (v.Contains("."))
      {
        int dp = v.IndexOf(".");
        int maxEndPos = dp + 6;
        if (v.Length > maxEndPos + 1)
          v = v.Substring(0, maxEndPos + 1);
      }

      return v;
    }

    private string GetSubExpTypeAbbr()
    {
      switch (this.SubExpressionType)
      {
        case SubExpressionType.FullEquation:
          return "F";

        case SubExpressionType.Parenthetical:
          return "P";

        case SubExpressionType.HighOrder:
          return "H";

        case SubExpressionType.LowOrder:
          return "L";
      }

      return "N";
    }

    public void SetMathOperator(string op)
    {
      switch (op)
      {
        case "+":
          this.MathOperation = MathOperation.Add;
          break;

        case "-":
          this.MathOperation = MathOperation.Subtract;
          break;

        case @"/":
          this.MathOperation = MathOperation.Divide;
          break;

        case "*":
          this.MathOperation = MathOperation.Multiply;
          break;

        case "^":
          this.MathOperation = MathOperation.Exponent;
          break;
      }

      this.MathOperation = MathOperation.NotSet;
    }

    private bool OperandsSpanEntireExpression()
    {
      return this.LeftOperandBegin == 0 && this.RightOperandEnd == e.Length - 1;
    }

    private bool SubExpressionSpansEntireExpression(Expression subExpression)
    {
      return subExpression.BeginPosition == 0 && subExpression.EndPosition == e.Length - 1;
    }

    public Expression ComputeResultantExpression(SubExpressionType subExpressionType)
    {
      Expression resultantExpression = new Expression(String.Empty, this.LeftOperandBegin, this.RightOperandEnd, this.ID, this.e, this._variables);
      resultantExpression.SubExpressionType = subExpressionType;

      decimal leftValue = this.GetValueOfOperand(this.LeftOperand);
      decimal rightValue = this.GetValueOfOperand(this.RightOperand);

      string op = e[this.MathOperatorPosition].ToString();

      switch (op)
      {
        case "+":
          resultantExpression.Value = leftValue + rightValue;
          break;

        case "-":
          resultantExpression.Value = leftValue - rightValue;
          break;

        case "*":
          resultantExpression.Value = leftValue * rightValue;
          break;

        case @"/":
          resultantExpression.Value = leftValue / rightValue;
          break;

        case "^":
          throw new Exception("Exponent operations are not yet implemented.");

        default:
          throw new Exception("Math operator '" + op + "' is not yet implemented.");
      }

      resultantExpression.IsComputationComplete = true;
      return resultantExpression;
    }

    private Expression ComputeAndRecompose(SubExpressionType subExpressionType)
    {
      string subExpression = e.Substring(this.LeftOperandBegin, (this.RightOperandEnd - this.LeftOperandBegin) + 1);
      Expression subEx = new Expression(subExpression, this.LeftOperandBegin, this.RightOperandEnd, this.ID, this.e, this._variables);
      subEx.SubExpressionType = SubExpressionType.HighOrder;

      WriteTrace(subEx, String.Empty, 7);

      string rv = subEx.ComputeValue(this._resultType);

      string frontPart = String.Empty;
      string backPart = String.Empty;

      if (this.LeftOperandBegin > 0)
          frontPart = e.Substring(0, this.LeftOperandBegin);

      if (this.RightOperandEnd < e.Length - 1)
          backPart = e.Substring(this.RightOperandEnd + 1, (e.Length - this.RightOperandEnd) - 1);

      string interimExpression = frontPart + rv + backPart;

      Expression recomposedExpression = new Expression(interimExpression, 0, this.e.Length - 1, this.ID, this.e, this._variables);

      g.LogToMemory(this.ID.ToString("000") + " " +
            this.BeginPosition.ToString("000") + " " +
            this.EndPosition.ToString("000") + " " +
            this.GetSubExpTypeAbbr() +  " " +
            this.IsComputationComplete.ToString().Substring(0, 1) + " 9   " +
            String.Empty.PadTo(58) + interimExpression);

      return recomposedExpression;
    }

    private Expression RecomposeExpression(Expression subExpression, string rv)
    {
        string frontPart = String.Empty;
        string backPart = String.Empty;

        if (subExpression.BeginPosition > 0)
          frontPart = this.e.Substring(0, subExpression.BeginPosition).Trim();

        if (subExpression.EndPosition < e.Length)
          backPart = this.e.Substring(subExpression.EndPosition + 1, (e.Length - subExpression.EndPosition) - 1).Trim();

        string interimResult = frontPart + rv + backPart;
        WriteTrace(subExpression, interimResult, 4);

        Expression reducedExpression = new Expression(interimResult, 0, interimResult.Length - 1, this.ID, this.e, this._variables);

        return reducedExpression;
    }

    private int LocateHighOrderMathOperator()
    {
      for (int i = 0; i < e.Length; i++)
      {
        if (e[i].IsHighOrderMathOperator())
        {
          return i;
        }
      }

      throw new Exception("High order math operator ('*', '/' or '^') not found in expression '" + e + "'.");
    }

    private int LocateLowOrderMathOperator()
    {
      for (int i = 0; i < e.Length; i++)
      {
        if (e[i].IsLowOrderMathOperator())
        {
          return i;
        }
      }

      throw new Exception("Low order math operator ('+' or '-') not found in expression '" + e + "'.");
    }

    private void LocateLeftOperand(bool isHighOrder)
    {
      if (isHighOrder)
      {
        this.LeftOperand = String.Empty;
        this.LeftOperandBegin = -1;
        this.LeftOperandEnd = this.MathOperatorPosition - 1;

        //find the beginning of the left operand
        for (int i = this.LeftOperandEnd; i > -1; i--)
        {
          // if at the beginning of the expression, that's where the left operand starts
          if (i == 0)
          {
            this.LeftOperandBegin = 0;
            break;
          }
          else
          {   // if we run into a low level operator, the operand starts 1 position to the right of it
              if (e[i].IsLowOrderMathOperator())
              {
                this.LeftOperandBegin = i + 1;
                break;
              }
          }
        }

        this.LeftOperand = e.Substring(this.LeftOperandBegin, (this.LeftOperandEnd - this.LeftOperandBegin) + 1);
      }
      else
      {
          this.LeftOperand = String.Empty;
          this.LeftOperandBegin = 0;
          this.LeftOperandEnd = this.MathOperatorPosition - 1;
          this.LeftOperand = e.Substring(this.LeftOperandBegin, (this.LeftOperandEnd - this.LeftOperandBegin) + 1);
      }
    }

    private void LocateRightOperand(bool isHighOrder)
    {
      if (isHighOrder)
      {
        this.RightOperand = String.Empty;
        this.RightOperandBegin = this.MathOperatorPosition + 1;
        this.RightOperandEnd = -1;

        //find the end of the right operand
        for (int i = this.RightOperandBegin; i > -1; i++)
        {
          // if we're at the end of the expression, that's where the right operand ends
          if (i == e.Length - 1)
          {
            this.RightOperandEnd = i;
            break;
          }
          else
          {   // if we run into a low level operator, the operand starts 1 position to the right of it
            if (e[i].IsMathOperator())
            {
              this.RightOperandEnd = i - 1;
              break;
            }
          }
        }

        this.RightOperand = e.Substring(this.RightOperandBegin, (this.RightOperandEnd - this.RightOperandBegin) + 1);
      }
      else
      {
        this.RightOperand = String.Empty;
        this.RightOperandBegin = this.MathOperatorPosition + 1;
        this.RightOperandEnd = e.Length - 1;
        this.RightOperand = e.Substring(this.RightOperandBegin, (this.RightOperandEnd - this.RightOperandBegin) + 1);
      }
    }
  }
}
