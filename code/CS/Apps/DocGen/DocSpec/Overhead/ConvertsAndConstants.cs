using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class Constants
  {
    public const int HeightMaxValue = 2000000000;
    public static char[] SpaceDelim = new char[] { ' ' };
  }

  public static class Conv
  {
    public static float FromPctToPixels(string value)
    {


      return 0F;
    }

    public static float FromDxaToPixels(string value)
    {
      if (value.IsNotNumeric())
        throw new Exception("Pct value '" + value + "' is not numeric.");

      float f = (float)Convert.ToInt32(value) / 1440 * 100;
      return f;
    }

    public static float FromDxaToPixels(UInt32 value)
    {
      float f = (float)Convert.ToInt32(value) / 1440 * 100;
      return f;
    }

    public static float FromPctToPixels(float value, string pct)
    {
      if (pct.IsNotNumeric())
        throw new Exception("Pct value '" + pct + "' is not numeric.");

      float pctValue = (float)Convert.ToInt32(pct) / 5000;
      float returnValue = value * pctValue;

      return returnValue;
    }

  }
}
