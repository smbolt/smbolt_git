using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.Math
{
  public class Equation
  {
    public string Formula { get; set; }
    public string[] RawDataItems { get; set; }
    public Dictionary<string, string> Variables { get; set; }
    public List<string> Options { get; set; }

    public EquationResultType EquationResultType { get; set; }
    private int decimalPlaces;

    private Expression Expression { get; set; }

    private string _errorMessage;

    private char[] opDelim = new char[] { '(', ')', '+', '-', '/', '*', '^'};


    public Equation()
    {
      this.Formula = String.Empty;
      this.RawDataItems = new string[0];
      this.Variables = new Dictionary<string, string>();
      this.Options = new List<string>();
      this.EquationResultType = EquationResultType.Integer;
      this.decimalPlaces = 0;
      this._errorMessage = String.Empty;            
    }

    public string ComputeValue()
    {
      try
      {
        if (!this.BuildVariables())
          return _errorMessage;

        if (!this.StripOptions())
          return _errorMessage;

        if (!this.InspectFormula())
          return _errorMessage;

        g.ClearMemoryLog();
        g.LogToMemory(g.crlf + "*==== BEGIN MATH TRACE ====*");
        string varTrace = "Variables: ";
        foreach (KeyValuePair<string, string> kvp in this.Variables)
          varTrace += kvp.Key + "=" + kvp.Value + ";";

        g.LogToMemory(varTrace);
        g.LogToMemory(String.Empty);
        g.LogToMemory("                    Expression                   Sub-Expression               Value");
        g.LogToMemory("ID  BP  EP  ET      0----+----1----+----2----+   0----+----1----+----2----+   0----+----1----+");
        g.LogToMemory("                    " + this.Formula);

        Expression expression = new Expression(this.Formula, 0, this.Formula.Length, 0, this.Formula, this.Variables);
        expression.SubExpressionType = SubExpressionType.FullEquation;

        string result = expression.ComputeValue(this.EquationResultType);
        g.LogToMemory(g.crlf + "*==== END MATH TRACE ====*");
        return result;
      }
      catch (Exception ex)
      {
            return ex.Message;
      }
    }

    private bool StripOptions()
    {
      string formula = this.Formula.Replace(" ", String.Empty);

      int bb = formula.IndexOf("[");
      if (bb == -1)
      {
        _errorMessage = "Invalid formula - first character must be '['";
        return false;
      }

      int eb = formula.IndexOf("]");
      if (eb == -1)
      {
        _errorMessage = "Invalid formula - formula is missing its ending bracket ']'";
        return false;
      }

      if (eb <= bb)
      {
        _errorMessage = "Invalid formula - brackets are in wrong order ']['";
        return false;
      }

      this.Formula = formula.Substring(bb + 1, (eb - bb) - 1);

      if (formula.Length < eb + 2)
        return true;

      string optionString = formula.Substring(eb + 1);

      string[] options = optionString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
      this.Options = options.ToList();

      return true;
    }

    private bool InspectFormula()
    {
      // parenthesis must be balanced
      int ocCount = this.Formula.CountOfChar('(');
      int ccCount = this.Formula.CountOfChar(')');

      if (ocCount != ccCount)
        _errorMessage = "Invalid formula - unbalanced parenthesis";

      // all variables referenced must exist in the variables collection
      string[] varNames = this.Formula.Split(opDelim, StringSplitOptions.RemoveEmptyEntries);
      foreach (string varName in varNames)
      {
        if (varName.IsLegalVariableName())
        {
          if (!this.Variables.ContainsKey(varName))
          {
            _errorMessage = "Invalid formula - reference to non-existent variable '" + varName + "'";
            return false;
          }
        }
        else
        {
          if (!varName.IsNumeric() && !varName.IsDecimal())
          {
            _errorMessage = "Invalid formula - illegal token '" + varName + "'";
            return false;
          }
        }
      }

      // use values assigned to variables to set a default resultType
      EquationResultType defaultResultType = EquationResultType.Integer;
      foreach (string value in this.Variables.Values)
      {
        if (value.IsDecimal())
        {
          defaultResultType = EquationResultType.Float;
          break;
        }
      }

      return true;
    }

    private bool BuildVariables()
    {
      this.Variables.Clear();

      // get a collection of variable names and values from the SectionData collection
      // the variable names come from the Tag property of the DataItem
      Dictionary<string, string> variables = new Dictionary<string, string>();
      foreach (string s in this.RawDataItems)
      {
        int bb = s.IndexOf("[");
        if (bb == -1)
        {
          this._errorMessage = "Invalid equation '" + this.Formula + "' no variable name begin bracket '['";
          return false;
        }

        int eb = s.IndexOf("]");
        if (eb == -1)
        {
          this._errorMessage = "Invalid equation '" + this.Formula + "' no variable name end bracket ']'";
          return false;
        }

        string varName = s.Substring(bb + 1, (eb - bb) - 1).Trim();
        if (varName.IsBlank())
        {
          this._errorMessage = "Invalid equation '" + this.Formula + "' variable name is blank";
          return false;
        }

        string varValue = s.Substring(eb + 1, (s.Length - eb) - 1).Trim();
        if (variables.ContainsKey(varName))
        {
          this._errorMessage = "Invalid equation '" + this.Formula + "' duplicate variable in data items '" + varName + "'";
          return false;
        }

        this.Variables.Add(varName, varValue);
      }

      return true;
    }
  }
}
