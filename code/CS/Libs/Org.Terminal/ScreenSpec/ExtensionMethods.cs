using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.Terminal.BMS;

namespace Org.Terminal.Screen
{
  public static class ExtensionMethods
  {
    //[DebuggerStepThrough]
    public static int FindEndOfStretch(this int[] line, int start)
    {
      int lastCol = line.Length - 1;

      for (int c = start; c < lastCol; c++)
      {
        if (c < lastCol)
        {
          if (line[c + 1] != -1)
          {
            return c;
          }
        }
      }

      return lastCol;
    }

    //[DebuggerStepThrough]
    public static int GetRightMostPlacement(this int[] line, int length)
    {
      int lastCol = line.Length - 1;
      int ptr = -1;

      for (int c = 0; c < lastCol + 1; c++)
      {
        if (line[c] == -1)
        {
          ptr = c;
        }
        else
        {
          break;
        }
      }

      // If the line already contains controls back up one character to allow space between controls.
      // If more space is desired between floated-right controls, use the length property to control spacing.
      if (ptr < lastCol)
        ptr--;

      ptr -= length - 1;

      if (ptr > -1)
        return ptr;

      return -1;
    }

    //[DebuggerStepThrough]
    public static void MarkAsOccupied(this int[] line, int col, int length, int controlId)
    {
      int lastCol = line.Length - 1;

      if (col + length - 1 > lastCol)
        throw new Exception("The begin column position " + col.ToString() + " plus the specified length " + length.ToString() +
                            "extends beyond the end of the array which is " + lastCol.ToString() + ".");

      int remainingLength = length;
      for (int c = col; c < lastCol + 1 && remainingLength > 0; c++)
      {
        line[c] = controlId;
        remainingLength--;
      }
    }

    [DebuggerStepThrough]
    public static bool IsBmsMacro(this string s)
    {
      if (s == null)
        return false;

      string upper = s.ToUpper().Trim();

      switch (upper)
      {
        case "DFHMSD":
        case "DFHMDI":
        case "DFHMDF":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static BmsStatementType ToBmsStatementType(this string s)
    {
      if (s == null)
        return BmsStatementType.Unidentified;

      string upper = s.ToUpper().Trim();

      switch (upper)
      {
        case "DFHMSD":
          return BmsStatementType.DFHMSD;
        case "DFHMDI":
          return BmsStatementType.DFHMDI;
        case "DFHMDF":
          return BmsStatementType.DFHMDF;
      }

      return BmsStatementType.Unidentified;
    }

  }
}

