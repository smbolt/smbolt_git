using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  public static class DxMapItemExtensionMethods
  {
    public static bool IncludeBasedOnCondition(this DxMapItem mapItem)
    {
      try
      {
        // if MapItem is commented out, don't process
        if (mapItem.Name.StartsWith("*"))
          return false;

        // if there is no condition specified, the do process
        if (mapItem.Cond.IsBlank())
          return true;

        string cond = mapItem.Cond.Trim();
        bool isNegated = cond.StartsWith("!");
        if (isNegated)
          cond = cond.Substring(1);

        string[] tokens = cond.GetRelationalExpression().TrimArrayTokens();

        for (int i = 0; i < tokens.Length; i++)
        {
          tokens[i] = tokens[i].Trim();
        }

        string leftRawValue = String.Empty;
        string leftValue = String.Empty;
        string rightRawValue = String.Empty;
        string rightValue = String.Empty;
        string relOp = String.Empty;

        switch (tokens.Length)
        {
          case 1:
            // Single tokens starting with $ result in a variable, if it exists, being evaluated to a boolean result.
            // If the variable does not exist, then the result is false by default.
            if (cond.StartsWith("$"))
            {
              string variableName = cond;
              bool conditionValue = MapEngine.GetVariableValue(variableName).ToBoolean();
              return isNegated ? !conditionValue : conditionValue;
            }
            break;


          case 2:
            throw new Exception("Conditions with relational expressions with two tokens are not yet implemented. " +
                                "The DxMapItem is '" + mapItem.Report + "'.");

          case 3:
            leftRawValue = tokens[0];
            relOp = tokens[1];
            rightRawValue = tokens[2];

            leftValue = leftRawValue.StartsWith("$") ? MapEngine.GetVariableValue(leftRawValue).Trim() : leftRawValue;
            rightValue = rightRawValue.StartsWith("$") ? MapEngine.GetVariableValue(rightRawValue).Trim() : rightRawValue;
            bool booleanResult = true;

            switch (relOp)
            {
              case "=":
                booleanResult = leftValue.CompareTo(rightValue) == 0;
                break;
              case "!=":
                booleanResult = leftValue.CompareTo(rightValue) != 0;
                break;
              case ">=":
                booleanResult = leftValue.CompareTo(rightValue) > -1;
                break;
              case "<=":
                booleanResult = leftValue.CompareTo(rightValue) < 1;
                break;
              case ">":
                booleanResult = leftValue.CompareTo(rightValue) > 0;
                break;
              case "<":
                booleanResult = leftValue.CompareTo(rightValue) < 0;
                break;
              default:
                throw new Exception("Unexpected relational operator encountered '" + relOp + "' when processing " +
                                    "DxMapItem '" + mapItem.Report + "'.");
            }

            if (isNegated)
              return !booleanResult;
            else
              return booleanResult;

          default:
            throw new Exception("The DxMapItem.Cond is invalid - it contains " + tokens.Length.ToString() + " tokens. The number of tokens allowed is from 1 to 3.");
        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the ProcessThisMapItemn method.", ex);
      }
    }

    public static bool ProcessThisMapItem(this DxMapItem mapItem, string srcCellValue)
    {
      try
      {
        // if MapItem is commented out, don't process
        if (mapItem.Name.StartsWith("*"))
          return false;

        // if there is no condition specified, the do process
        if (mapItem.Cond.IsBlank())
          return true;

        string cond = mapItem.Cond.Trim();
        bool isNegated = cond.StartsWith("!");
        if (isNegated)
          cond = cond.Substring(1);

        string[] tokens = cond.GetRelationalExpression().TrimArrayTokens();

        for (int i = 0; i < tokens.Length; i++)
        {
          tokens[i] = tokens[i].Trim();
        }

        string leftRawValue = String.Empty;
        string leftValue = String.Empty;
        string rightRawValue = String.Empty;
        string rightValue = String.Empty;
        string relOp = String.Empty;

        switch (tokens.Length)
        {
          case 1:
            // Single tokens starting with $ result in a variable, if it exists, being evaluated to a boolean result.
            // If the variable does not exist, then the result is false by default.
            if (cond.StartsWith("$"))
            {
              string variableName = cond;
              bool conditionValue = MapEngine.GetVariableValue(variableName).ToBoolean();
              return isNegated ? !conditionValue : conditionValue;
            }

            // Just take care of the 'false' result here, when we don't want to process the map item,
            // and let the 'true' result be returned by default at the bottom of the method.
            if (isNegated)
            {
              if (cond == srcCellValue)
                return false;
            }
            else
            {
              if (cond != srcCellValue)
                return false;
            }
            break;


          case 2:
            throw new Exception("Conditions with relational expressions with two tokens are not yet implemented. " +
                                "The DxMapItem is '" + mapItem.Report + "'.");

          case 3:
            leftRawValue = tokens[0];
            relOp = tokens[1];
            rightRawValue = tokens[2];

            leftValue = leftRawValue.StartsWith("$") ? MapEngine.GetVariableValue(leftRawValue).Trim() : leftRawValue;
            rightValue = rightRawValue.StartsWith("$") ? MapEngine.GetVariableValue(rightRawValue).Trim() : rightRawValue;
            bool booleanResult = true;

            switch (relOp)
            {
              case "=":
                booleanResult = leftValue.CompareTo(rightValue) == 0;
                break;
              case "!=":
                booleanResult = leftValue.CompareTo(rightValue) != 0;
                break;
              case ">=":
                booleanResult = leftValue.CompareTo(rightValue) > -1;
                break;
              case "<=":
                booleanResult = leftValue.CompareTo(rightValue) < 1;
                break;
              case ">":
                booleanResult = leftValue.CompareTo(rightValue) > 0;
                break;
              case "<":
                booleanResult = leftValue.CompareTo(rightValue) < 0;
                break;
              default:
                throw new Exception("Unexpected relational operator encountered '" + relOp + "' when processing " +
                                    "DxMapItem '" + mapItem.Report + "'.");
            }

            if (isNegated)
              return !booleanResult;
            else
              return booleanResult;


          default:
            throw new Exception("The DxMapItem.Cond is invalid - it contains " + tokens.Length.ToString() + " tokens. The number of tokens allowed is from 1 to 3.");

        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the ProcessThisMapItemn method.", ex);
      }
    }
  }
}
