using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; 
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS
{
  public static class ExtensionMethods
  {
    public static string ExceptionReport(this Exception e)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("ExceptionType: " + e.GetType().ToString() + g.crlf + 
                "Message: " + e.Message + g.crlf +
                "StackTrace:" + e.StackTrace + g.crlf);

      if (e.InnerException != null)
      {
        sb.Append("InnerExceptionType: " + e.InnerException.GetType().ToString() + g.crlf + 
              "InnerExceptionMessage: " + e.InnerException.Message + g.crlf +
              "InnerExceptionStackTrace:" + e.InnerException.StackTrace + g.crlf);
      }

      return sb.ToString();
    }

		public static string ObjectArrayToString(this object[] array)
		{
			if (array == null)
				return String.Empty;

			if (array.Length == 0)
				return String.Empty;

			string stringValue = String.Empty;
			for(int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					if (stringValue.Length > 0)
						stringValue += "," + array[i].ToString();
					else
						stringValue = array[i].ToString();
				}
			}

			return stringValue;
		}

    [DebuggerStepThrough]
		public static string StringArrayToString(this string[] array)
		{
			if (array == null)
				return String.Empty;

			if (array.Length == 0)
				return String.Empty;

			string stringValue = String.Empty;
			for(int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					if (stringValue.Length > 0)
						stringValue += "," + array[i];
					else
						stringValue = array[i];
				}
			}

			return stringValue;
		}

    //[DebuggerStepThrough]
		public static string[] NumericTokensOnly(this string[] array)
		{
      if (array == null || array.Length == 0)
        return new string[0];

      var numericTokenList = new List<string>();
			for(int i = 0; i < array.Length; i++)
			{
				if (array[i].IsValidIntOrDecimal())
				{
					numericTokenList.Add(array[i].Trim());
				}
			}

			return numericTokenList.ToArray();
		}

    [DebuggerStepThrough]
		public static string[] ObjectArrayToStringArray(this object[] array)
		{
			if (array == null)
				return new string[0];

			if (array.Length == 0)
				return new string[0];

			string stringValue = String.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					if (stringValue.Length > 0)
						stringValue += "," + array[i].ToString();
					else
						stringValue = array[i].ToString();
				}
			}

			return stringValue.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries); 
		}

		public static string GetNextToken(this string value, int beginPos = 0)
		{
			if (value.IsBlank())
				return String.Empty;

			if (beginPos < 0)
				beginPos = 0;

			if (beginPos > value.Length - 1)
				return String.Empty;

			string s = beginPos > 0 ? value.Substring(beginPos).Trim() : value.Trim();
			
			int firstBlank = s.IndexOf(' '); 
			if (firstBlank == -1)
				return s;

			string token = s.Substring(0, firstBlank).Trim();
			return token;
		}

		public static bool IsValidShortDate(this string value)
    {
      if (value == null)
        return false;

      DateTime testDt = DateTime.MinValue;
      if (DateTime.TryParse(value, out testDt))
        return true;

      return false;
    }
    
    [DebuggerStepThrough]
    public static string RemoveTrailingSlash(this string path)
    {
      if (path == null)
        return String.Empty;

      path = path.Trim();

      if (path.Length == 0)
        return String.Empty;

      int lastCharPos = path.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = path[lastCharPos].ToString();

      if (lastChar == @"\" || lastChar == @"/")
        return path.Substring(0, path.Length - 1);

      return path;
    }

    [DebuggerStepThrough]
    public static string EnsureSingleTrailingSlash(this string path)
    {
      if (path.IsBlank())
        return String.Empty;

      path = path.Replace("/", @"\");

      while (path.Length > 1 && path.EndsWith(@"\"))
      {
        int newLth = path.Length - 1;
        path = path.Substring(0, newLth);
      }

      return path + @"\"; 
    }

    [DebuggerStepThrough]
    public static string RemoveTrailingComma(this string value)
    {
      if (value == null)
        return String.Empty;

      value = value.Trim();

      if (value.Length == 0)
        return String.Empty;

      int lastCharPos = value.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = value[lastCharPos].ToString();

      if (lastChar == @",")
        return value.Substring(0, value.Length - 1);

      return value;
    }

    [DebuggerStepThrough]
    public static bool IsValidCCYYMMDD(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (!value.IsNumeric())
        return false;

      if (value.Length != 8)
        return false;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);

      DateTime dateCheck;

      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dateCheck))
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static Color ToColor(this string value)
    {
      if (value == null)
        return Color.White;

      if (value.IsBlank())
        return Color.White;

      if (value.CharCount('.') != 2)
        return Color.White;

      int[] tokens = value.ToTokenArrayInt32(Constants.PeriodDelimiter);

      if (tokens.Length != 3)
        return Color.White;

      foreach (int token in tokens)
      {
        if (token < 0 || token > 255)
          return Color.White;
      }

      return Color.FromArgb(tokens[0], tokens[1], tokens[2]); 
    }

    [DebuggerStepThrough]
    public static string GetStringValueOrNull(this string value)
    {
      if (value == null)
        return null;

      if (value.IsBlank())
        return null;

      return value.Trim();
    }

    [DebuggerStepThrough]
    public static DateTime? GetDateValueOrNull(this string value)
    {
      if (value == null)
        return null;

      if (value.IsValidShortDate())
        return value.ShortDateToDateTime();

      return null;
    }

    public static bool IsValidMMSlashYYYY(this string s)
    {
      if (s.IsBlank())
        return false;

      string[] tokens = s.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 2)
        return false;

      if (tokens[0].IsNotNumeric() || tokens[1].IsNotNumeric())
        return false;

      int mm = tokens[0].ToInt32();
      int yyyy = tokens[1].ToInt32();

      if (mm < 1 || mm > 12)
        return false;

      if (yyyy < 1900 || yyyy > 2199)
        return false;

      return true;
    }

    //[DebuggerStepThrough]
    public static bool IsValidTime(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim().ToUpper();

      bool amFound = value.Contains("AM");
      bool pmFound = value.Contains("PM");

      value = value.Replace("AM", String.Empty).Replace("PM", String.Empty); 

      int colonCount = value.CountOfChar(':');
      if (colonCount < 1 || colonCount > 2)
        return false;

      string[] tokens = value.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length < 2 || tokens.Length > 3)
        return false;

      int hour = 0;
      int minute = 0;
      int second = 0;

      if (tokens[0].IsNotNumeric())
        return false;

      hour = tokens[0].ToInt32();

      if (tokens[1].IsNotNumeric())
        return false;

      minute = tokens[1].ToInt32();

      if (tokens.Length == 3)
      {
        if (tokens[2].IsNotNumeric())
          return false;

        second = tokens[2].ToInt32();
      }

      if (hour > 23)
        return false;

      if (minute > 59)
        return false;

      if (second > 59)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static TimeSpan ToTimeSpan(this string value)
    {
      TimeSpan zeroTimeSpan = new TimeSpan(0);

      if (value == null)
        return zeroTimeSpan;

      value = value.Trim();

      if (value.CountOfChar(':') != 1)
        return zeroTimeSpan;

      string[] tokens = value.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 2)
        return zeroTimeSpan;

      int hour = 0;
      int minute = 0;

      if (tokens[0].IsNotNumeric())
        return zeroTimeSpan;
      hour = tokens[0].ToInt32();

      if (tokens[1].IsNotNumeric())
        return zeroTimeSpan;
      minute = tokens[1].ToInt32();

      if (hour > 23)
        return zeroTimeSpan;

      if (minute > 59)
        return zeroTimeSpan;

      return new TimeSpan(hour, minute, 0);
    }

    [DebuggerStepThrough]
    public static TimeSpan? ToNullableTimeSpan(this string value)
    {
      if (value == null)
        return (TimeSpan?) null;

      return value.ToTimeSpan();
    }

    [DebuggerStepThrough]
    public static bool IsFormattedNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      if (value.CountOfChar('.') > 1)
        return false;

      value = value.Trim().Replace(".", String.Empty).Replace(",", String.Empty);

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }
            

    [DebuggerStepThrough]
    public static int CountOfToken(this string value, string token)
    {
      if (value == null || token == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      int count = 0;
      int pos = 0;
      while (pos != -1)
      {
        pos = value.IndexOf(token, pos);
        if (pos != -1)
        {
          count++;
          pos++; 
        }                
      }

      return count;
    }

    public static DateTime? ShortDateToDateTime(this string value)
    {
      if (value == null)
        return null;

      DateTime testDt = DateTime.MinValue;
      if (DateTime.TryParse(value, out testDt))
        return testDt;

      return null;
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMDDToDateTime(this string value)
    {
      if (value == null)
        return DateTime.MinValue;

      if (value.Trim().Length == 0)
        return DateTime.MinValue;

      DateTime dtReturnValue = DateTime.MinValue;

      value = value.Trim();
            
      if (value.IsNotNumeric())
        return DateTime.MinValue;

      if (value.Length != 8)
        return DateTime.MinValue;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);


      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dtReturnValue))
        return DateTime.MinValue;

      return dtReturnValue;
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMDDHHMMSSToDateTime(this string value)
    {
      if (value == null)
        return DateTime.MinValue;

      if (value.Trim().Length == 0)
        return DateTime.MinValue;

      DateTime dtReturnValue = DateTime.MinValue;

      value = value.Trim();
            
      if (value.IsNotNumeric())
        return DateTime.MinValue;

      if (value.Length != 14)
        return DateTime.MinValue;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);
      string hour = value.Substring(8, 2);
      string minute = value.Substring(10, 2);
      string second = value.Substring(12, 2); 

      if (!DateTime.TryParse(month + "/" + day + "/" + year + " " + hour + ":" + minute + ":" + second, out dtReturnValue))
        return DateTime.MinValue;

      return dtReturnValue;
    }

    [DebuggerStepThrough]
    public static DateTime EndOfDay(this DateTime value)
    {
      if (value == null)
        return DateTime.MaxValue;

      return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59); 
    }

    private static string GetExceptionData(int level, Exception e, bool includeStackTrace)
    {
      StringBuilder sb = new StringBuilder();

      string stackTrace = includeStackTrace ? "StackTrace:" + e.StackTrace + g.crlf : String.Empty;

      sb.Append("(" + level.ToString() + ") ExceptionType: " + e.GetType().ToString() + g.crlf + 
                "Message: " + e.Message + g.crlf + stackTrace);

      if (e.InnerException != null)
        sb.Append(g.crlf + GetExceptionData(++level, e.InnerException, includeStackTrace));

      return sb.ToString();
    }

    public static string GetBits(this byte b)
    {
      BitArray bitArray = new BitArray(new byte[] { b });

      int[] bits = new int[8];
      for (int i = 0; i < 8; i++)
          bits[i] = bitArray[i] ? 1 : 0;

      string returnString = 
          bits[7].ToString() + 
          bits[6].ToString() + 
          bits[5].ToString() + 
          bits[4].ToString() + 
          bits[3].ToString() + 
          bits[2].ToString() + 
          bits[1].ToString() + 
          bits[0].ToString();

      return returnString;
    }

    public static byte SetBitOn(this byte b, int bitNumber)
    {
      if (bitNumber < 0 || bitNumber > 7)
        return b;

      byte mask = (byte)(1 << bitNumber);
      b = (byte)(b | mask);

      return b;
    }

    public static byte SetBitOff(this byte b, int bitNumber)
    {
      if (bitNumber < 0 || bitNumber > 7)
        return b;

      byte mask = (byte)(255 -(1 << bitNumber));
      b = (byte)(b & mask);

      return b;
    }

    public static string ToHex(this byte b)
    {
      string returnValue = BitConverter.ToString(new byte[] { b });
      return returnValue;
    }

    public static byte ToByte(this string s, int pos)
    {
      byte returnValue = Convert.ToByte(s.Substring(pos, 2), 16);
      return returnValue;
    }

    [DebuggerStepThrough]
    public static bool IsValidOrdinalNumber(this string value)
    {
      if (value == null)
        return false;

      if (value.Length < 3)
        return false;

      if (value.Substring(0, 1).IsNotNumeric())
        return false;

      string last2 = value.Substring(value.Length - 2, 2).ToLower();

      if (last2 != "st" && last2 != "nd" && last2 != "rd" && last2 != "th")
        return false;

      string numericPart = value.Substring(0, value.Length - 2);

      if (numericPart.IsNotNumeric())
        return false;

      if (numericPart.Length > 1)
      {
        string last2Numbers = numericPart.Substring(numericPart.Length - 2, 2);

        if (last2Numbers == "11" || last2Numbers == "12" || last2Numbers == "13")
        {
          if (last2 == "th")
            return true;
          else
            return false;
        }
      }

      string lastNumber = numericPart.Substring(numericPart.Length - 1, 1);

      switch (lastNumber)
      {
        case "1":
          return last2 == "st";
        case "2":
          return last2 == "nd";
        case "3":
          return last2 == "rd";
      }

      return last2 == "th";
    }

    [DebuggerStepThrough]
    public static bool HasSentenceEndingPunctuation(this string value)
    {
      if (value == null)
        return false;

      string testValue = value.Trim();

      if (testValue.EndsWith("."))
        return true;

      if (testValue.EndsWith("!"))
        return true;

      if (testValue.EndsWith("?"))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static string PrepForSpellCheck(this string value)
    {
      if (value == null)
        return String.Empty;

      string noPunctValue = value.Replace(",", String.Empty)
                                  .Replace(".", String.Empty)
                                  .Replace(";", String.Empty)
                                  .Replace(":", String.Empty)
                                  .Replace("—", String.Empty)
                                  .Replace("\"", String.Empty)
                                  .Replace("“", String.Empty)
                                  .Replace("”", String.Empty)
                                  .Replace("‑", "-");

      if (noPunctValue.IsNumeric())
        return String.Empty;

      return noPunctValue;
    }

    [DebuggerStepThrough]
    public static int IndexOfNoRepeatChar(this string value, char c, int startIndex)
    {
      if (value == null)
        return -1;

      int pos = value.IndexOf(c, startIndex);
      if (pos == -1)
        return -1;

      if (pos > value.Length - 2)
        return -1;

      int next = pos + 1;
      if (value[next] == c)
        return -1;

      return pos;
    }

    [DebuggerStepThrough]
    public static string DoubleCharAtPos(this string value, int pos)
    {
      if (value == null)
        return String.Empty;

      if (pos > value.Length - 1)
        return String.Empty;

      string charToDouble = value.Substring(pos, 1);
      string newValue = value.Substring(0, pos) + charToDouble + value.Substring(pos, value.Length - pos);

      return newValue;
    }

    [DebuggerStepThrough]
    public static string ToDelimitedList(this List<string> value, string delimiter)
    {
      if (value == null || delimiter == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (string s in value)
      {
        if (sb.Length > 0)
          sb.Append("," + s.Trim());
        else
          sb.Append(s.Trim());
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static Size Inflate(this Size value, int width, int height)
    {
      return new Size(value.Width + width, value.Height + height); 
    }

    [DebuggerStepThrough]
    public static Size Deflate(this Size value, int width, int height)
    {
      int widthToSubtract = width;
      int heightToSubtract = height;

      if (widthToSubtract > value.Width)
        widthToSubtract = value.Width;

      if (heightToSubtract > value.Height)
        heightToSubtract = value.Height;

      return new Size(value.Width - widthToSubtract, value.Height - heightToSubtract); 
    }

    [DebuggerStepThrough]
    public static PointF ShiftRight(this PointF value, float offset)
    {
      value.X += offset;
      return value;
    }

    [DebuggerStepThrough]
    public static PointF ShiftDown(this PointF value, float offset)
    {
      value.Y += offset;
      return value;
    }

    [DebuggerStepThrough]
    public static float ToFloat(this string value)
    {
      if (value == null)
        return 0F;

      if (value.Trim().Length == 0)
        return 0F;

      string valueNoDecimals = value.Replace(".", String.Empty).Trim();

      int signMultiplier = 1;
      int minusSigns = valueNoDecimals.CountOfChar('-'); 
      if (minusSigns == 1)
      {
        if (valueNoDecimals.StartsWith("-") || valueNoDecimals.EndsWith("-"))
        {
          valueNoDecimals = valueNoDecimals.Replace("-", String.Empty);
          signMultiplier = -1;
        }
      }

      if (valueNoDecimals.IsNotNumeric())
        return 0F;

      if (value.CountOfChar('.') > 1)
        return 0F;            

      string noSignValue = value.Replace("-", String.Empty); 

      return (float)decimal.Parse(noSignValue) * signMultiplier;
    }

    [DebuggerStepThrough]
    public static decimal ToDecimal(this string value)
    {
      if (value == null)
        return 0M;

      if (value.Trim().Length == 0)
        return 0M;

      string valueNoDecimals = value.Replace(".", String.Empty).Trim().Replace(",", String.Empty); 

      int signMultiplier = 1;
      int minusSigns = valueNoDecimals.CountOfChar('-'); 
      if (minusSigns == 1)
      {
        if (valueNoDecimals.StartsWith("-") || valueNoDecimals.EndsWith("-"))
        {
          valueNoDecimals = valueNoDecimals.Replace("-", String.Empty);
          signMultiplier = -1;
        }
      }

      if (valueNoDecimals.IsNotNumeric())
        return 0M;

      if (value.CountOfChar('.') > 1)
        return 0M;            

      string noSignValue = value.Replace("-", String.Empty); 

      return decimal.Parse(noSignValue) * signMultiplier;
    }

    [DebuggerStepThrough]
    public static decimal ToDecimal(this object value)
    {
      if (value == null)
        return 0M;

      try
      {
        return Convert.ToDecimal(value); 
      }
      catch
      {
        return 0M;
      }
    }

    [DebuggerStepThrough]
    public static float ToFloat(this object value)
    {
      if (value == null)
        return 0F;

      try
      {
        return Convert.ToSingle(value); 
      }
      catch
      {
        return 0F;
      }
    }

    [DebuggerStepThrough]
    public static DateTime ToDateTime(this object value)
    {
      if (value == null)
        return DateTime.MinValue;

      try
      {
        return Convert.ToDateTime(value); 
      }
      catch
      {
        return DateTime.MinValue;
      }
    }

		public static bool InIntegerRange(this string s, int lowValue, int highValue)
		{
			if (s.IsBlank() || s.IsNotNumeric())
				return false;

			int n = s.ToInt32();

			if (n < lowValue || n > highValue)
				return false;

			return true;
		}

		public static bool InIntegerRange(this int n, int lowValue, int highValue)
		{
			if (n < lowValue || n > highValue)
				return false;

			return true;
		}

		public static string[] RemoveItemAt(this string[] s, int index)
		{
			if (s == null)
				return new string[0];

			if (index > s.Length - 1)
				return s;

			int newLength = s.Length - 1;
			string[] newArray = new string[newLength];

			int j = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (i != index)
					newArray[j++] = s[i];
			}

			return newArray;
		}

		public static string JoinRemainingTokens(this string[] s, string separator = "")
		{
			if (s == null || s.Length == 0)
				return String.Empty;

			string joinedTokens = String.Empty;
			for (int i = 0; i < s.Length; i++)
			{
				if (joinedTokens.Length > 0)
					joinedTokens += separator + s[i].Trim();
				else
					joinedTokens = s[i].Trim();
			}

			return joinedTokens;
		}

		[DebuggerStepThrough]
    public static DateTime? ToNullableDateTime(this object value)
    {
      if (value == null)
        return (DateTime?)null;

      try
      {
        return Convert.ToDateTime(value); 
      }
      catch
      {
        return (DateTime?)null;
      }
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMToDateTime(this int value)
    {
      string ccyymm = value.ToString();

      if (ccyymm.Length != 6)
        return DateTime.MinValue;
      
      try
      {
        int year = ccyymm.Substring(0, 4).ToInt32();
        int month = ccyymm.Substring(4, 2).ToInt32();
        DateTime dt = new DateTime(year, month, 1);
        return dt;
      }
      catch
      {
        return DateTime.MinValue;
      }      
    }

    ////[DebuggerStepThrough]
    //public static int CCYYMMToNextMonth(this int value)
    //{
    //  string ccyymm = value.ToString();

    //  if (ccyymm.Length != 6)
    //    return DateTime.MinValue;
      
    //  try
    //  {
    //    int year = ccyymm.Substring(0, 4).ToInt32();
    //    int month = ccyymm.Substring(4, 2).ToInt32();
    //    DateTime dt = new DateTime(year, month, 1);
    //    return dt;
    //  }
    //  catch
    //  {
    //    return DateTime.MinValue;
    //  }      
    //}

    public static TimeSpan? ToTimeSpan(this object value)
    {
      if (value == null)
        return (TimeSpan?)null;

      return (TimeSpan?)value;
    }

    [DebuggerStepThrough]
    public static long ToTimeStampInt64(this DateTime value)
    {
      return Convert.ToInt64(value.ToString("yyyyMMddHHmmss")); 
    }

    [DebuggerStepThrough]
    public static int ToInt32(this object value)
    {
      if (value == null)
        return 0;

      try
      {
        return Convert.ToInt32(value); 
      }
      catch
      {
        return 0;
      }
    }

    [DebuggerStepThrough]
    public static int CCYYMMToNextMonth(this int value)
    {
      if (value < 195000 || value > 210000)
        return -1;
      int month = value % 100;
      int c = value / 100;
      if (month > 12)
        return -1;
      if (month == 12)
      {
        month = 1;
        int ccyy = (int)(value / 100) + 1;
        value = (ccyy * 100) + 1;
        int nvalue = int.Parse(ccyy.ToString() + month.ToString("00"));
        return nvalue;
      }
      month = month + 1;
      int newvalue = int.Parse(c.ToString() + month.ToString("00"));
      
      return newvalue;
    }

    [DebuggerStepThrough]
    public static int CCYYMMToPreviousMonth(this int value)
    {
      if (value < 195000 || value > 210000)
        return -1;
      int month = value % 100;
      int c = value / 100;
      if (month > 12)
        return -1;
      if (month == 1)
      {
        month = 12;
        int ccyy = (int)(value / 100) - 1;
        int nvalue = int.Parse(ccyy.ToString() + month.ToString("00"));
        return nvalue;
      }
      month = month - 1;
      int newvalue = int.Parse(c.ToString() + month.ToString("00"));
      
      return newvalue;
    }


    [DebuggerStepThrough]
    public static Int64 ToInt64(this object value)
    {
      if (value == null)
        return 0;

      try
      {
        return Convert.ToInt64(value); 
      }
      catch
      {
        return 0;
      }
    }

    [DebuggerStepThrough]
    public static int CountOfChar(this string value, char ch)
    {
      if (value == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      int count = 0;

      foreach (Char c in value)
      {
        if (c == ch)
          count++;
      }

      return count;
    }

    [DebuggerStepThrough]
    public static string ToAlphaOnly(this string value)
    {
      if (value == null)
        return String.Empty;

      if (value.Trim().Length == 0)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (Char c in value)
      {
        if (Char.IsLetter(c))
          sb.Append(c);
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static float ToPointSize(this float value)
    {
        return value / 2;
    }

    [DebuggerStepThrough]
    public static Rectangle ToRectangle(this RectangleF value)
    {
      if (value == null)
        return new Rectangle();

      return new Rectangle(value.X.RoundToInt(), value.Y.RoundToInt(), value.Width.RoundToInt(), value.Height.RoundToInt());
    }

    [DebuggerStepThrough]
    public static Point ToPoint(this PointF value)
    {
      if (value == null)
        return new Point();

      return new Point(value.X.RoundToInt(), value.Y.RoundToInt());
    }

    [DebuggerStepThrough]
    public static int RoundToInt(this float value)
    {
      return System.Math.Round(value, 0).ToInt32();
    }

    [DebuggerStepThrough]
    public static int ToInt32(this float value)
    {
      return (int)value;
    }

    [DebuggerStepThrough]
    public static bool ToBoolean(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return false;

      if (value.In("t, true, 1"))
        return true;
            
      return false;
    }

    [DebuggerStepThrough]
    public static bool ToBoolean(this string value, bool defaultValue)
    {
      if (value == null)
        return defaultValue;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return defaultValue;

      if (value.In("t, true, 1"))
        return true;

      if (value.In("f, false, 0"))
        return false;
            
      return defaultValue;
    }

    [DebuggerStepThrough]
    public static UInt32 ToUInt32(this string value)
    {
        if (value == null)
            return 0;

        value = value.Trim().ToLower();

        if (value.Trim().Length == 0)
            return 0;

        if (!value.IsNumeric())
            return 0;

        return UInt32.Parse(value); 
    }

    [DebuggerStepThrough]
    public static UInt16 ToUInt16(this string value)
    {
      if (value == null)
        return 0;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return 0;

      if (!value.IsNumeric())
        return 0;

      return UInt16.Parse(value); 
    }

    [DebuggerStepThrough]
    public static UInt64 ToUInt64(this string value)
    {
      if (value == null)
        return 0;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return 0;

      if (!value.IsNumeric())
        return 0;

      return UInt64.Parse(value); 
    }

    [DebuggerStepThrough]
    public static bool StringStartsWith(this string value, string set)
    {
      if (value == null || set == null)
        return false;

      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries); 

      foreach(string token in tokens)
      {
        if (value.StartsWith(token.Trim()))
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool WrappedWith(this string value, string start, string end)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value.Trim().Length == 0)
        return false;

      return value.StartsWith(start) && value.EndsWith(end); 
    }

    [DebuggerStepThrough]
    public static string RemoveChars(this string value, string chars)
    {
      if (value == null)
        return String.Empty;

      chars = chars.Trim();

      if (chars.Trim().Length == 0)
        return value;

      foreach(char c in chars)
      {
        value = value.Replace(c.ToString(), String.Empty);
        if (value.Length == 0)
          return String.Empty;
      }

      return value;
    }

    [DebuggerStepThrough]
    public static bool In(this string value, string set)
    {
      if (value == null || set == null)
        return false;

      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries); 

      foreach(string token in tokens)
      {
        if (value == token.Trim())
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool ToListContains(this string value, char[] delim, string containsValue, bool caseSensitive = false)
    {
      if (value.IsBlank() || containsValue.IsBlank() || delim == null)
        return false;

      string thisValue = value;

      if (!caseSensitive)
      {
        thisValue = value.ToLower();
        containsValue = containsValue.ToLower();
      }

      var tokens = thisValue.Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      if (tokens.Count == 0)
        return false;

      return tokens.Contains(containsValue); 
    }
        
    [DebuggerStepThrough]
    public static bool In(this int value, string set)
    {
      if (set == null)
        return false;

      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries); 

      foreach(string token in tokens)
      {
        if (token.Trim().IsNumeric())
        {
          if (value == token.Trim().ToInt32())
              return true;
        }
      }

      return false;
    }  

    [DebuggerStepThrough]
    public static bool In(this int value, List<int> set)
    {
      if (set == null)
        return false;

      return set.Contains(value);
    }

    [DebuggerStepThrough]
    public static bool IsNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }

    //[DebuggerStepThrough]
    public static bool IsValidIntOrDecimal(this string value)
    {
      if (value.IsValidInteger())
        return true;

      if (value.IsFloat(false))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsAlphabetic(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsLetter(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsAlphaNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsLetter(c) && !Char.IsDigit(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsValidNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      int periodCount = value.CharCount('.');
      if (periodCount > 1)
        return false;

      int minusCount = value.CharCount('-');
      if (minusCount > 1)
        return false;

      if (minusCount == 1)
      {
        if (!value.StartsWith("-") && !value.EndsWith("-"))
          return false;
        value = value.Replace("-", String.Empty);
      }

      foreach (Char c in value)
      {
        if (!Char.IsNumber(c) && c != ',' && c != '.')
          return false;
      }

      return true;
    }

    [DebuggerStepThrough]
    public static DateTime FirstDayOfMonth(this DateTime value)
    {
      if (value.Day == 1)
        return value;

      return new DateTime(value.Year, value.Month, 1);
    }

    [DebuggerStepThrough]
    public static int LastDayOfMonth(this DateTime value)
    {
      DateTime firstOfCurrentMonth = new DateTime(value.Year, value.Month, 1);
      DateTime lastDateOfCurrentMonth = firstOfCurrentMonth.AddMonths(1).AddDays(-1);
      return lastDateOfCurrentMonth.Day;
    }

    [DebuggerStepThrough]
    public static DateTime EndOfLastDateOfMonth(this DateTime value)
    {
      DateTime firstOfCurrentMonth = new DateTime(value.Year, value.Month, 1);
      DateTime lastDateOfCurrentMonth = firstOfCurrentMonth.AddMonths(1).AddDays(-1);
      DateTime endOfDay = lastDateOfCurrentMonth.AddMilliseconds(86399999);
      return endOfDay;
    }

    [DebuggerStepThrough]
    public static DateTime FirstDayOfNextMonth(this DateTime value)
    {
      value = value.AddMonths(1); 

      if (value.Day == 1)
        return value;

      return new DateTime(value.Year, value.Month, 1);
    }

    [DebuggerStepThrough]
    public static string ToDateFmt(this DateTime? value, string formatSpecifier)
    {
      if (value == null || !value.HasValue)
        return string.Empty;

      DateTime d = value.Value;

      return d.ToString(formatSpecifier); 
    }

    //[DebuggerStepThrough]
    public static string ToCCYYMMDD(this DateTime value)
    {
      return value.ToString("yyyyMMdd"); 
    }

    [DebuggerStepThrough]
    public static int ToCCYYMM(this DateTime value)
    {
      return (value.Year * 100) + value.Month;
    }

    [DebuggerStepThrough]
    public static DateTime Date(this DateTime value)
    {
      return new DateTime(value.Year, value.Month, value.Day); 
    }

    [DebuggerStepThrough]
    public static string ToMMDDYYHHMM(this DateTime value)
    {
      return value.ToString("MMddyy HHmm"); 
    }

    [DebuggerStepThrough]
    public static string ToCCYYMMDDHHMMSS(this DateTime value)
    {
      return value.ToString("yyyyMMddHHmmss"); 
    }

    [DebuggerStepThrough]
    public static bool IsMinValue(this DateTime value)
    {
      return value == DateTime.MinValue;
    }

    [DebuggerStepThrough]
    public static bool IsNotMinValue(this DateTime value)
    {
      return value != DateTime.MinValue;
    }

    [DebuggerStepThrough]
    public static bool IsIPV4Address(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value.Length == 0)
        return false;
            
      if (value.CountOfChar('.') != 3)
        return false;

      string[] tokens = value.Split('.');

      if (tokens.Length != 4)
        return false;

      foreach (string token in tokens)
      {
          if (token.IsNotNumeric())
            return false;

          int octet = token.ToInt32();

          if (octet < 0 || octet > 255)
            return false;
      }
            
      return true;
    }

    [DebuggerStepThrough]
    public static bool IsInteger(this string value)
    {
			value = value.Trim();

			if (value.IsBlank())
				return false;

			foreach (char c in value)
				if (!Char.IsNumber(c))
					return false;

      return true;
    }

    //[DebuggerStepThrough]
    public static bool IsValidInteger(this string value)
    {
			string trimmedValue = value.Trim();

      if (trimmedValue.IsBlank())
				return false;

      if (trimmedValue.Contains("."))
        return false;

      bool isNegative = false;
      if (trimmedValue.StartsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(1);
        isNegative = true;
      }

      if (trimmedValue.EndsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);
        isNegative = true;
      }

      int commaCount = trimmedValue.CountOfChar(',');

      if (commaCount > 0)
      {
        if (trimmedValue.StartsWith(",") || trimmedValue.EndsWith(","))
          return false;

        string workValue = trimmedValue;
        int commaSearchPosition = workValue.Length - 1;

        int rightPos = workValue.Length;

        while (commaSearchPosition > 0)
        {
          int commaPosition = workValue.LastIndexOf(',', commaSearchPosition);
          if (commaPosition > -1)
            commaSearchPosition = commaPosition - 1;

          if (commaPosition == -1)
            break;

          int offset = rightPos - commaPosition;

          if (offset % 4 != 0)
            return false;
        }

        trimmedValue = trimmedValue.Replace(",", String.Empty);
      }

      foreach (char c in trimmedValue)
				if (!Char.IsNumber(c))
					return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsDecimal(this string value, bool requirePeriod = false)
    {
      return value.IsFloat(requirePeriod);
    }

    //[DebuggerStepThrough]
    public static bool IsFloat(this string value, bool requirePeriod = false)
    {
      if (value == null)
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
        return false;

      int dollarSignCount = trimmedValue.CountOfChar('$');
      if (dollarSignCount > 1)
        return false;

      if (dollarSignCount == 1)
      {
        if (trimmedValue.StartsWith("$"))
          trimmedValue = trimmedValue.Substring(1); 
      }

      int percentCount = trimmedValue.CountOfChar('%');
      if (percentCount > 1)
        return false;

      if (percentCount == 1 && trimmedValue.EndsWith("%"))
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);

      int periodCount = trimmedValue.CountOfChar('.');

			if (requirePeriod && periodCount == 0)
				return false;

      if (periodCount > 1)
        return false;

      int periodPos = trimmedValue.IndexOf('.'); 

      int dashCount = trimmedValue.CountOfChar('-');
      if (dashCount > 1)
        return false;

      bool isNegative = false;
      if (trimmedValue.StartsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(1);
        isNegative = true;
      }

      if (trimmedValue.EndsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);
        isNegative = true; 
      }

      if (trimmedValue.EndsWith("."))
        return false;

      int commaCount = trimmedValue.CountOfChar(',');

      if (commaCount > 0)
      {
        if (trimmedValue.StartsWith(",") || trimmedValue.EndsWith(","))
          return false; 

        string workValue = trimmedValue;
        int commaSearchPosition = workValue.Length - 1; 

        int rightPos = workValue.LastIndexOf('.');
        if (rightPos == -1)
          rightPos = workValue.Length;

        while (commaSearchPosition > 0)
        {
          int commaPosition = workValue.LastIndexOf(',', commaSearchPosition);
          if (commaPosition > -1)
            commaSearchPosition = commaPosition - 1;

          if (commaPosition == -1)
            break;

          int offset = rightPos - commaPosition; 

          if (offset % 4 != 0)
            return false;
        }

        trimmedValue = trimmedValue.Replace(",", String.Empty); 
      }

      bool result = trimmedValue.Replace(".", String.Empty).IsNumeric();

      return result;
    }

    //DebuggerStepThrough]
    public static bool ContainsEntry(this string[] array, string valueToCheck, bool caseInsensitive = true)
    {
      if (array == null || array.Length == 0 || valueToCheck.IsBlank())
        return false;

      foreach (string value in array)
      {
        if (caseInsensitive)
        {
          if (value.ToLower().Trim() == valueToCheck.ToLower().Trim())
            return true;
        }
        else
        {
          if (value.Trim() == valueToCheck.Trim())
            return true;
        }
      }

      return false;
    }

    //DebuggerStepThrough
    public static string[] RemoveEntry(this string[] array, string valueToRemove, bool caseInsensitive = true)
    {
      if (array == null || array.Length == 0 || valueToRemove.IsBlank())
        return array;

      int entryIndexToRemove = -1;

      for (int i = 0; i < array.Length; i++)
      {
        string value = array[i];
        if (caseInsensitive)
        {
          if (value.ToLower().Trim() == valueToRemove.ToLower().Trim())
          {
            entryIndexToRemove = i;
            break;
          }
        }
        else
        {
          entryIndexToRemove = i;
          break;
        }
      }

      if (entryIndexToRemove == -1)
        return array;

      var newArray = new string[array.Length - 1];
      int j =  0;

      for (int i = 0; i < array.Length; i++)
      {
        if (i != entryIndexToRemove)
          newArray[j++] = array[i];
      }

      return newArray;
    }

    [DebuggerStepThrough]
    public static void Clear(this string value)
    {
      value = String.Empty;
    }

    [DebuggerStepThrough]
    public static bool IsNotNumeric(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      foreach (Char c in value)
          if (!Char.IsNumber(c))
            return true;

      return false;
    }

    [DebuggerStepThrough]
    public static int ToInt32(this string value)
    {
      if (value == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      value = value.Trim();
      int negationValue = 1; 

      if (value.StartsWith("-"))
      {
        value = value.Substring(1);
        negationValue = -1;
      }
      else
      {
        if (value.EndsWith("-"))
        {
          value = value.Substring(0, value.Length - 1);
          negationValue = -1;
        }
      }

      if (value.IsNotNumeric())
          return 0;

      return Int32.Parse(value)* negationValue;
    }

    public static int TrimToInt32(this string value)
    {
        var num = "";
        foreach (Char c in value)
            if (Char.IsNumber(c))
                num += c;

        return num.ToInt32();    
    }

    [DebuggerStepThrough]
    public static string TrimLastChar(this string value)
    {
      if (value == null)
        return String.Empty;

      value = value.Trim();
      if (value.Length < 2)
        return String.Empty;

      return value.Substring(0, value.Length - 1); 
    }
    
    [DebuggerStepThrough]
    public static int? ToNullableInt32(this object value)
    {
      if (value == null)
        return (int?) null;

      if (value.GetType() == typeof(System.DBNull))
        return (int?)null; 

      string stringValue = value.ToString().Trim();

      int negationValue = 1;

      if (stringValue.StartsWith("-"))
      {
        stringValue = stringValue.Substring(1);
        negationValue = -1;
      }
      else
      {
        if (stringValue.EndsWith("-"))
        {
          stringValue = stringValue.Substring(0, stringValue.Length - 1);
          negationValue = -1;
        }
      }

      if (stringValue.IsNotNumeric())
        return 0;

      return (int?) Int32.Parse(stringValue) * negationValue;
    }

    [DebuggerStepThrough]
    public static bool ToBoolean(this object value)
    {
      if (value == null)
        return false;

      string stringValue = value.ToString().Trim().ToLower();

      switch (stringValue)
      {
        case "1": return true;
        case "y": return true;
        case "true": return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsBoolean(this string value)
    {
      if (value == null)
        return false;

      switch (value.ToLower().Trim())
      {
        case "0": return true;
        case "1": return true;
        case "y": return true;
        case "n": return true;
        case "true": return true;
        case "false": return true;
        case "t": return true;
        case "f": return true;
        case "yes": return true;
        case "no": return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static int ToInt32OrDefault(this string value, int defaultValue)
    {
      if (value == null)
        return defaultValue;

      if (value.Trim().Length == 0)
        return defaultValue;

      if (value.IsNotNumeric())
        return defaultValue;

      return Int32.Parse(value);
    }

    [DebuggerStepThrough]
    public static string OrDefault(this string value, string defaultValue)
    {
      if (value.IsBlank())
        return defaultValue;

			return value;
    }

    [DebuggerStepThrough]
    public static Size Shrink(this Size value, int width, int height)
    {
			if (width > value.Width)
				width = value.Width;

			if (height > value.Height)
				height = value.Height;

			return new Size(value.Width - width, value.Height - height); 
    }

    [DebuggerStepThrough]
    public static Size Grow(this Size value, int width, int height)
    {
			if (width < 0)
			{
				if (System.Math.Abs(width) > value.Width)
					width = value.Width * -1;
			}

			if (height < 0)
			{
				if (System.Math.Abs(height) > value.Height)
					height = value.Height * -1;
			}

			return new Size(value.Width + width, value.Height + height); 
    }

    [DebuggerStepThrough]
    public static Point Move(this Point value, int x, int y)
    {
			return new Point(value.X + x, value.Y + y); 
    }

    [DebuggerStepThrough]
    public static int ToInt32OrDefault(this object value, int defaultValue)
    {
      if (value == null)
        return defaultValue;

      return value.ToInt32();
    }

    [DebuggerStepThrough]
    public static string ObjectToTrimmedStringOrNull(this object value)
    {
      if (value == null)
        return null;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static string ObjectToTrimmedString(this object value)
    {
      if (value == null)
        return String.Empty;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static bool IsBlank(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool ContainsAlphaChar(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (char c in value)
        if (Char.IsLetter(c))
          return true;

      return false;
    }

    [DebuggerStepThrough]
    public static string CondenseText(this string value)
    {
      if (value == null)
        return String.Empty;
      if (value.Length == 0)
        return String.Empty;

      var sb = new StringBuilder();

      foreach (Char c in value)
      {
        if (!Char.IsWhiteSpace(c))
          sb.Append(c);
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static List<String> ToListOfStrings(this string value, char[] delimiter)
    {
      if (value == null)
        return new List<String>();

      if (value.Trim().Length == 0)
        return new List<String>();

      return value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    [DebuggerStepThrough]
    public static int DaysInMonth(this DateTime value)
    {
      int month = value.Month;

      switch (month)
      {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
          return 31;

        case 2:
          if (value.IsLeapYear())
            return 29;
          return 28;

        default:
          return 30;
      }
    }

    [DebuggerStepThrough]
    public static UInt64 ToInverseLongSortKey(this DateTime value)
    {
      string dateString = value.ToString("yyMMddHHmmss") + "000";
      UInt64 sortKey = UInt64.Parse(dateString);
      UInt64 inverseSortKey = 999999999999999 - sortKey;
      return inverseSortKey;
    }

    [DebuggerStepThrough]
    public static UInt64 ToLongSortKey(this DateTime value)
    {
      string dateString = value.ToString("yyMMddHHmmss") + "000";
      UInt64 sortKey = UInt64.Parse(dateString);
      return sortKey;
    }

    [DebuggerStepThrough]
    public static bool IsNotBlank(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length > 0)
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static string ToAlphaTokens(this string value, int minLth)
    {
      value = value.Replace("\r\n", " ");
      value = value.Replace("\r", " ");
      value = value.Replace("\n", " "); 
      value = value.Trim().ToUpper();

      if (value.IsBlank())
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < value.Length; i++)
      {
        int c = value[i];
        if (c > 47 && c < 58)  // 0-9
        {
          sb.Append(value[i]);
          continue;
        }
        if (c > 64 && c < 91)  // A-Z
        {
          sb.Append(value[i]);
          continue;
        }
        if (c > 96 && c < 123)  // a-z
        {
          sb.Append(value[i]);
          continue;
        }
        if ((c > 191 && c < 199) || (c > 223 && c < 231))  // A-like 
        {
          sb.Append("A");
          continue;
        }
        if (c == 199 || c == 231)  // C-like  
        {
          sb.Append("C");
          continue;
        }
        if ((c > 199 && c < 204) || (c > 231 && c < 236))  // E-like 
        {
          sb.Append("E");
          continue;
        }
        if ((c > 203 && c < 208) || (c > 235 && c < 240))  // I-like 
        {
          sb.Append("I");
          continue;
        }
        if (c == 208 || c == 240)  // D-like 
        {
          sb.Append("D");
          continue;
        }
        if (c == 209 || c == 241)  // N-like 
        {
          sb.Append("N");
          continue;
        }
        if ((c > 209 && c < 215) || (c > 241 && c < 247))  // O-like 
        {
          sb.Append("O");
          continue;
        }
        if ((c > 216 && c < 221) || (c > 248 && c < 253))  // U-like 
        {
          sb.Append("U");
          continue;
        }
        if (c == 216 || c == 248)  // 0-like 
        {
          sb.Append("0");
          continue;
        }
        if (c == 221 || c == 253 || c == 255)  // Y-like 
        {
          sb.Append("Y");
          continue;
        }
        if (c == 223 || c == 254)  // B-like 
        {
          sb.Append("B");
          continue;
        }
        if (c == 215)  // X-like 
        {
          sb.Append("X");
          continue;
        }

        sb.Append(" ");
      }

      value = sb.ToString().Trim();

      sb.Clear();

      int zeroAmidstLetters = value.LocateZeroAmistLetters();
      while(zeroAmidstLetters != -1)
      {
        value = value.ReplaceAtPosition("O", zeroAmidstLetters); 
        zeroAmidstLetters = value.LocateZeroAmistLetters();
      }

      string[] tokens = value.Trim().Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

      int usedTokenCount = 0;

      for (int i = 0; i < tokens.Length; i++)
      {
        string token = tokens[i];

        if (token == "0K")
          token = "OK";
        if (token == "0R")
          token = "OR";
          

        if (token.Length > 1)
        {
          if (!token.In("DR,OK,ST,OKLAHOMA,RD,PL,AVE,DATE,CITY,AND,OR"))
          {
            usedTokenCount++;
            if (usedTokenCount > 10)
              break;

            if ((token.In("PAY,DOLLAR,DOLLARS") && usedTokenCount > 5))
              break;

            if (sb.Length == 0)
              sb.Append(token);
            else
              sb.Append(" " + token);
          }
        }
      }

      return sb.ToString();
    }

    //[DebuggerStepThrough]
    public static int LocateZeroAmistLetters(this string value)
    {
      if (value == null)
        return -1;

      if (value.IsBlank())
        return -1;

      value = value.Trim();

      if (value.Length < 2)
        return -1;

      for (int i = 0; i < value.Length; i++)
      {
        char c = value[i];
        if (c == '0')
        {
          if (i == 0 && i + 1 < value.Length - 1 && (Char.IsLetter(value[i + 1]) || value[i + 1] == ' '))
            return i;

          if (i == value.Length - 1)
          {
            if (Char.IsLetter(value[i - 1]) || value[i - 1] == ' ')
              return i;
          }

          if (i > 0 && i < value.Length - 1)
          {
            if ((Char.IsLetter(value[i - 1])|| value[i - 1] == ' ') && (Char.IsLetter(value[i + 1]) || value[i + 1] == ' '))
              return i;
          }
        }
      }

      return -1;
    }
    
    [DebuggerStepThrough]
    public static Type ToType(this string value)
    {
      try
      {
        if (value == null || value.IsBlank())
          return null; 

        value = value.Replace(" ", String.Empty);
        
        if (value == "Dictionary<string,string>")
          return typeof(Dictionary<,>).MakeGenericType(typeof(System.String), typeof(System.String));

        if (value == "List<string>")
          return typeof(List<>).MakeGenericType(typeof(System.String));

        return Type.GetType(value);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to convert the string value '" + value + "' to a Type.", ex); 
      }
    }

    [DebuggerStepThrough]
    public static int CharCount(this string value, char c)
    {
      if (value == null)
        return 0; 

      return value.Count(f => f == c);
    }

    [DebuggerStepThrough]
    public static string SetIfBlank(this string value, string newValue)
    {
      if (value == null && newValue == null)
        return String.Empty;

      if (value == null && newValue != null)
        return newValue;

      if (value.IsNotBlank())
        return value;

      return newValue;
    }


    [DebuggerStepThrough]
    public static bool IsLeapYear(this DateTime value)
    {
      if (value.Year % 4 == 0 && value.Year % 100 != 0 || value.Year % 400 == 0)
        return true;  

      return false; 
    }


    [DebuggerStepThrough]
    public static DateTime ToCCYYMMDD(this string value)
    {
      return new DateTime(Convert.ToInt32(value.Substring(0,4)),Convert.ToInt32(value.Substring(4,2)),Convert.ToInt32(value.Substring(6,2)));
    }

    [DebuggerStepThrough]
    public static XElement GetElement(this XElement value, string elementName)
    {
      if (value == null || elementName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return null;

      return value.Element(ns + elementName);
    }

    [DebuggerStepThrough]
    public static XElement GetRequiredElement(this XElement value, string elementName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredElement.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElement.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is not found in the xml '" + value.ToString() + "'.");

      return value.Element(ns + elementName);
    }

    [DebuggerStepThrough]
    public static string GetAttributeValue(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return String.Empty;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        if (value.Attribute(attributeName) != null)
          return value.Attribute(attributeName).Value.Trim();
        else
          return String.Empty;

      return value.Attribute(ns + attributeName).Value.Trim();
    }

    [DebuggerStepThrough]
    public static string GetAttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      return value.Attribute(ns + attributeName).Value.Trim();
    }

    [DebuggerStepThrough]
    public static string GetRequiredElementAttributeValue(this XElement value, string elementName, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredElementAttributeValue.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElementAttributeValue.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredElementAttributeValue.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is missing from element '" + value.ToString() + "'.");

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + elementName + "' in xml '" + value.ToString() + "'.");

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetElementAttributeValueOrBlank(this XElement value, string elementName, string attributeName)
    {
      if (value == null || elementName == null || attributeName == null)
        return String.Empty;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return String.Empty;

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        return String.Empty;

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetElementAttributeValueOrNull(this XElement value, string elementName, string attributeName)
    {
      if (value == null || elementName == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return null;

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetRequiredAttributeValue(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeValue.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static UInt32 GetRequiredAttributeUInt32(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeUInt32.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsNotNumeric())
        throw new Exception("Required numeric xml attribute '" + attributeName + "' is not numeric in the xml '" + value.ToString() + "'.");

      return UInt32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static Int32 GetRequiredAttributeInt32(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeInt32.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsNotNumeric())
        throw new Exception("Required numeric xml attribute '" + attributeName + "' is not numeric in the xml '" + value.ToString() + "'.");

      return Int32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static int GetIntegerAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        return 0;

      if (attributeName == null)
        return 0; 

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return Int32.Parse(attributeValue);

      throw new Exception("Required xml integer attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt16 GetUInt16Attribute(this XElement value, string attributeName)
    {
      if (value == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt16.Parse(attributeValue);

      throw new Exception("Required xml UInt16 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt16? GetUInt16AttributeOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt16.Parse(attributeValue);

      throw new Exception("Required xml UInt16 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt32? GetUInt32AttributeOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt32.Parse(attributeValue);

      throw new Exception("Required xml UInt32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt32 GetUInt32Attribute(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt32.Parse(attributeValue);

      throw new Exception("Required xml UInt32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static Int32 GetInt32Attribute(this XElement value, string attributeName)
    {
        if (value == null || attributeName == null)
            return 0;

        XNamespace ns = value.Name.NamespaceName;

        if (value.Attribute(ns + attributeName) == null)
            return 0;

        string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
        if (attributeValue.IsNumeric())
            return Int32.Parse(attributeValue);

        throw new Exception("Required xml Int32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static int GetRequiredIntegerAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredIntegerAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredIntegerAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml integer attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return Int32.Parse(attributeValue);

      throw new Exception("Required xml integer attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static bool GetBooleanAttribute(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return false;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return false;

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static bool GetBooleanAttributeWithDefault(this XElement value, string attributeName, bool defaultValue)
    {
      if (value == null || attributeName == null)
        return defaultValue;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        return defaultValue;
      }

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static bool? GetBooleanAttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.ToLower().Trim();

      switch (attributeValue)
      {
        case "0":
        case "false":
        case "off":
        case "no":
          return false;

        case "1":
        case "true":
        case "on":
        case "yes":
          return true;
      }

      return Boolean.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static Int32? GetInt32AttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.ToLower().Trim();

      return Int32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static bool GetRequiredBooleanAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetRequiredBooleanAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredBooleanAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static object GetEnumAttribute(this XElement value, string attributeName, Type type)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetEnumAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetEnumAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      string attributeValue = String.Empty;

      if (value.Attribute(ns + attributeName) != null)
        attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
          if (enumValue.ToString().ToLower() == attributeValue.ToLower())
              return Enum.Parse(type, enumValue.ToString());
      }

      if (defaultValue.IsBlank())
          throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'  and no default value is defined.");

      return Enum.Parse(type, defaultValue);
    }

    [DebuggerStepThrough]
    public static object GetRequiredElementAttributeEnum(this XElement value, string elementName, string attributeName, Type type)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetRequiredElementAttributeEnum.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElementAttributeEnum.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredElementAttributeEnum.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is missing from element '" + value.ToString() + "'.");

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + elementName + "' in xml '" + value.ToString() + "'.");

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrDefault(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        string defaultValue = String.Empty;
        Array enumValues = Enum.GetValues(type);
        foreach (object enumValue in enumValues)
        {
          int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
          if (intValue == 0)
          {
            defaultValue = enumValue.ToString();
            return Enum.Parse(type, enumValue.ToString());
          }                    
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' does not exist and there is no default value for the enumeration." + type.Name + "'.");
      }
      else
      {
        string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

        string defaultValue = String.Empty;
        Array enumValues = Enum.GetValues(type);
        foreach (object enumValue in enumValues)
        {
          int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
          if (intValue == 0)
          {
            defaultValue = enumValue.ToString();
            break;
          }
        }

        foreach (object enumValue in enumValues)
        {
          if (enumValue.ToString().ToLower() == attributeValue.ToLower())
            return Enum.Parse(type, enumValue.ToString());
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
      }
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrSpecific(this XElement value, string attributeName, Type type, object specific)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        return specific; 
      }
      else
      {
        string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
        Array enumValues = Enum.GetValues(type);

        foreach (object enumValue in enumValues)
        {
          if (enumValue.ToString().ToLower() == attributeValue.ToLower())
            return Enum.Parse(type, enumValue.ToString());
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
      }
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrNull(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static object GetRequiredEnumAttribute(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static string GetRequiredElementValue(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml child element '" + elementName + "' is missing from parent element '" + value.ToString() + "'.");

      string elementValue = value.Element(ns + elementName).Value.Trim();

      if (elementValue.IsBlank())
        throw new Exception("Required xml child element '" + elementName + "' exists but is blank in the xml '" + value.ToString() + "'.");

      return elementValue.Trim();
    }

    [DebuggerStepThrough]
    public static string GetElementValue(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return String.Empty;

      string elementValue = value.Element(ns + elementName).Value.Trim();

      return elementValue.Trim();
    }

    [DebuggerStepThrough]
    public static string ToReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        if (ex.Message.StartsWith("!"))
          return ex.Message.Substring(1);

        sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + Environment.NewLine +
                  "Message: " + ex.Message + Environment.NewLine +
                  "StackTrace:" + ex.StackTrace + Environment.NewLine);
        
        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(Environment.NewLine);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    [DebuggerStepThrough]
    public static bool ContainsText(this Exception value, string text)
    {
      string compareText = text.ToUpper();
      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        if (ex.Message.ToUpper().Contains(compareText))
          return true;

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          ex = ex.InnerException;
          level++;
        }
      }

      return false;
    }

    [DebuggerStepThrough]
    public static string MessageReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        sb.Append("[" + level.ToString() + "] " + ex.Message);

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(g.crlf);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    [DebuggerStepThrough]
    public static void AssertElement(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml child element '" + elementName + "' is missing from element '" + value.ToString() + "'.");
    }

    [DebuggerStepThrough]
    public static void AssertAttribute(this XElement value, string attributeName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");
    }

    [DebuggerStepThrough]
    public static string InQuotes(this string value)
    {
      if (value == null)
        return "\"\"";

      return "\"" + value.Trim() + "\"";
    }

    [DebuggerStepThrough]
    public static string CamelCase(this string value)
    {
      if (value == null)
        return String.Empty;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
        return String.Empty;

      if (trimmedValue.Length == 1)
        return trimmedValue.ToLower();

      return trimmedValue.Substring(0, 1).ToLower() + trimmedValue.Substring(1);
    }

    [DebuggerStepThrough]
    public static string CaptializeFirstLetter(this string value)
    {
      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
          return String.Empty;

      if (trimmedValue.Length == 1)
          return trimmedValue.ToUpper();

      return trimmedValue.Substring(0, 1).ToUpper() + trimmedValue.Substring(1);
    }

		
    public static string ToHex(this byte[] b)
    {
      // need to finish this sometime
      return String.Empty;
    }

		public static string ToTextDump(this string value)
		{
			string header = "        Special characters:  '" + ("\xA4").ToString() + "' is new line, '" + ("\xB7").ToString() + "' is a blank character." + g.crlf + 
				              "        Total length = " + (value.IsBlank() ? "0" : value.Length.ToString("###,##0")) + g.crlf + 
				              "        ----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----0";

			if (value.IsBlank())
				return header + g.crlf + "        *** TEXT VALUE IS BLANK OR NULL ***" + g.crlf;

		  var sb = new StringBuilder();
			sb.Append(header + g.crlf);

			var lines = new List<string>();

			int lth = value.Length;
			int remainingLength = lth;
			int begPos = 0;
			int lineNbr = 0;

			while (remainingLength > 0)
			{
				lineNbr++;
				if (remainingLength > 99)
				{
					string fullLine = value.Substring(begPos, 100).Replace("\n", "\xA4").Replace(" ", "\xB7"); 
					lines.Add(lineNbr.ToString("000000") + "  " + fullLine);
					remainingLength -= 100;
					begPos += 100;
				}
				else
				{
					string partialLine = value.Substring(begPos, remainingLength).Replace("\n", "\xA4").Replace(" ", "\xB7"); 
					lines.Add(lineNbr.ToString("000000") + "  " + partialLine);
					begPos += remainingLength;
					remainingLength = 0; 
				}
			}

			foreach (var line in lines)
				sb.Append(line + g.crlf);

			sb.Append(g.crlf); 

			string report = sb.ToString();
			return report;
		}
		
		public static string To100CharLines(this string value, string indent)
		{
			if (value.IsBlank())
				return String.Empty;

			var sb = new StringBuilder();

			var lines = new List<string>();

			int lth = value.Length;
			int remainingLength = lth;
			int begPos = 0;
			int lineNbr = 0;

			while (remainingLength > 0)
			{
				lineNbr++;
				if (remainingLength > 99)
				{
					string fullLine = value.Substring(begPos, 100).Replace("\n", "\xA4").Replace(" ", "\xB7");
					lines.Add(fullLine);
					remainingLength -= 100;
					begPos += 100;
				}
				else
				{
					string partialLine = value.Substring(begPos, remainingLength).Replace("\n", "\xA4").Replace(" ", "\xB7");
					lines.Add(partialLine);
					begPos += remainingLength;
					remainingLength = 0;
				}
			}

			foreach (var line in lines)
				sb.Append(indent + line + g.crlf);

			sb.Append(g.crlf);

			string report = sb.ToString();
			return report;
		}

    public static string ToBinHexDump(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(value);
      return ToBinHexDump(byteArray); 
    }

    public static string ToBinHexDump(this byte[] a)
    {
      string aStr = System.Text.Encoding.Default.GetString(a); 
      int remainder = a.Length % 8 > 0 ? 1 : 0;
      string[] chr = new string[Convert.ToInt32(a.Length / 8) + remainder];

      int charsRemaining = aStr.Length;
      int pos = 0;
      int address = 0; 
            
      for (int i = 0; i < chr.Length; i++)
      {
        int length = charsRemaining; 
        if (length > 8)
          length = 8;
        chr[i] = aStr.Substring(pos, length).PadTo(8);
        pos += length;
        charsRemaining -= length; 
      }

      string reportHeader = "ADDR-X | ----1--- ----2--- ----3--- ----4---  ----5--- ----6--- ----7--- ----8--- | ----------HEX----------- | ADDR-D | --CHAR-- | --------------DEC---------------" + g.crlf;
      StringBuilder sbReport = new StringBuilder(reportHeader); 

      string[] bin = new string[8];
      string[] hex = new string[8];
      string[] dec = new string[8];
      int bytesProcessed = 0;
      pos = 0;
            
      for (int i = 0; i < a.Length - 1; i++)   
      {
        bin[bytesProcessed] = a[i].GetBits();
        hex[bytesProcessed] = a[i].ToHex().ToLower();
        dec[bytesProcessed] = ((int)a[i]).ToString("000"); 

        bytesProcessed++;

        if (bytesProcessed == 8)
        {
          if (address > 0 && address % 256 == 0)
          {
            sbReport.Append(g.crlf + reportHeader);
          }

          string addressHex = address.ToHex().PadWithLeadingZeros(6);
          string addressDec = address.ToString("000000");

          sbReport.Append(addressHex + " | " + bin[0] + " " + bin[1] + " " + bin[2] + " " + bin[3] + "  " +
                                        bin[4] + " " + bin[5] + " " + bin[6] + " " + bin[7] + " | " +
                                        hex[0] + " " + hex[1] + " " + hex[2] + " " + hex[3] + "  " +
                                        hex[4] + " " + hex[5] + " " + hex[6] + " " + hex[7] + " | " +
                          addressDec + " | " + chr[Convert.ToInt32(i / 8)] + " | " +
                                        dec[0] + " " + dec[1] + " " + dec[2] + " " + dec[3] + "  " +
                                        dec[4] + " " + dec[5] + " " + dec[6] + " " + dec[7] + g.crlf);

          bytesProcessed = 0;

          address += 8;
          pos += 8;
        }
      }

      bytesProcessed = 0; 

      if (pos < a.Length)
      {
          bin = new string[8];
          hex = new string[8];
          dec = new string[8];

          for (int i = 0; i < 8; i++)
          {
              bin[i] = "        ";
              hex[i] = "  "; 
              dec[i] = "   "; 
          }

          for (int i = pos; i < a.Length; i++)
          {
              bin[bytesProcessed] = a[i].GetBits();
              hex[bytesProcessed] = a[i].ToHex().ToLower();
              dec[bytesProcessed] = ((int)a[i]).ToString("000"); 
              bytesProcessed++; 
          }

          string addressHex = address.ToHex().PadWithLeadingZeros(6);
          string addressDec = address.ToString("000000");

          sbReport.Append(addressHex + " | " + bin[0] + " " + bin[1] + " " + bin[2] + " " + bin[3] + "  " +
                                            bin[4] + " " + bin[5] + " " + bin[6] + " " + bin[7] + " | " +
                                            hex[0] + " " + hex[1] + " " + hex[2] + " " + hex[3] + "  " +
                                            hex[4] + " " + hex[5] + " " + hex[6] + " " + hex[7] + " | " +
                              addressDec + " | " + chr[chr.Length - 1] + " | " +
                                            dec[0] + " " + dec[1] + " " + dec[2] + " " + dec[3] + "  " +
                                            dec[4] + " " + dec[5] + " " + dec[6] + " " + dec[7] + g.crlf);

      }

      sbReport.Append(g.crlf + "TOTAL LENGTH IN BYTES: " + a.Length.ToString("###,###,###,##0") + g.crlf); 

      string report = sbReport.ToString();

      return report;
    }

    public static string ToHex(this int n)
    {
      return n.ToString("X"); 
    }

    public static string First50(this string s)
    {
      if (s == null || s.IsBlank())
        return String.Empty;

      if (s.Length > 49)
        return s.Substring(0, 50);

      return s.Trim();
    }

    public static string First50(this XElement x)
    {
      if (x == null)
        return String.Empty;

      return x.ToString().First50();
    }

    public static bool AttributeExists(this XElement e, string attributeName)
    {
      if (e == null)
        return false;

      if (e.Attributes(attributeName).Count() > 0)
        return true;

      return false;
    }

    public static void AddAttribute(this XElement e, string attributeName, string attributeValue)
    {
      if (attributeName.IsBlank())
      {
        if (e != null)
          throw new Exception("Cannot add an attribute with a blank name to the xml element '" + e.First50() + "'.");
      }

      if (attributeValue == null)
        attributeValue = String.Empty;

      if (e == null)
        throw new Exception("The xml element is null - the attribute named '" + attributeName + "' with value '" +
	                        attributeValue + "' cannot be added to a null xml element.");

      if (e.Attributes(attributeName).Count() > 0)
        throw new Exception("The attribute named '" + attributeName + "' already exists in the xml element '" + e.First50() + "'.");

      e.Add(new XAttribute(attributeName.Trim(), attributeValue.Trim())); 
    }

    private static int HexToInteger(this string s)
    {
      if (!s.IsValidHexNumber())
        return -1; 

      s = s.Trim().ToLower();

      return int.Parse(s, System.Globalization.NumberStyles.HexNumber);
    }

    public static bool IsValidHexNumber(this string s)
    {
      s = s.Trim().ToLower();

      if (s.Length % 2 > 0)
        return false;

      foreach (char c in s)
      {
        if (!Char.IsNumber(c))
        {
          if (c < 'a' || c > 'f')
            return false;
        }
      }

      return true;
    }

    public static byte[] HexNumberToByteArray(this string s)
    {
      byte[] emptyByteArray = new byte[0];

      s = s.Trim();

      if (!s.IsValidHexNumber())
          return emptyByteArray;

      int lth = s.Length;
      int byteArrayLength = lth / 2;

      byte[] b = new byte[byteArrayLength];

      for (int i = 0; i < lth; i += 2)
      {
        string hexCode = s.Substring(i, 2);
        int intValue = int.Parse(hexCode, System.Globalization.NumberStyles.HexNumber);
        b[i/2] = (byte)intValue; 
      }

      return b;
    }

    public static string ToEfConnectionString(this Org.GS.Configuration.ConfigDbSpec spec)
    {
      return "metadata=res://*/EntityFramework." + spec.DbName + ".csdl|" + 
              "res://*/EntityFramework." + spec.DbName + ".ssdl|" + 
              "res://*/EntityFramework." + spec.DbName + ".msl;" + 
              "provider=System.Data.SqlClient;" + 
              "provider connection string=\"Data Source=" + spec.DbServer + ";" +  
              "Initial Catalog=" + spec.DbName + ";" + 
              "Integrated Security=False;" + 
              "User ID=" + spec.DbUserId + ";" + 
              "Password=" + spec.DbPassword + "\"";
    }

    public static string ToFullTypeName(this Type type)
    {
      if (type == null)
        return "NULL";
      
      Type theType = type;
      bool isGenericCollection = false;
      bool isNullableType = false;

      Type collectionType = null;
      Type nullableType = null;

      while (theType != null)
      {
        if (!isGenericCollection)
        {
          if (theType.FullName.Contains("System.Collections.Generic."))
          {
            isGenericCollection = true;
            collectionType = theType;
          }
        }

        if (!isNullableType)
        {
          if (theType.FullName.Contains("System.Nullable"))
          {
            isNullableType = true;
            nullableType = theType;
          }
        }

        if (isNullableType)
        {
          Type[] nullableArgTypes = nullableType.GetGenericArguments();
          string nullableArgs = String.Empty;
          foreach (Type nullableArgType in nullableArgTypes)
          {
            if (nullableArgs.IsBlank())
              nullableArgs = nullableArgType.Name;
            else
              nullableArgs += "," + nullableArgType.Name;
          }

          return nullableType.Name + "(" + nullableArgs + ")"; 
        }

        if (isGenericCollection)
        {
          Type[] collectionArgTypes = collectionType.GetGenericArguments();
          string collectionArgs = String.Empty;
          foreach (Type collectionArgType in collectionArgTypes)
          {
            if (collectionArgs.IsBlank())
              collectionArgs = collectionArgType.Name;
            else
              collectionArgs += "," + collectionArgType.Name;
          }

          return collectionType.Name + "(" + collectionArgs + ")"; 
        }

        theType = theType.BaseType;
      }



      return type.Name;
    }
    
		public static string ToMMSSFFF(this TimeSpan ts)
		{
			if (ts == null)
				return String.Empty;

			int min = ts.Minutes;
			int sec = ts.Seconds;
			int ms = ts.Milliseconds;

			string formatted = min.ToString("00") + ":" + sec.ToString("00") + "." + ms.ToString("000");

			return formatted;
		}

		public static string ToApiStartOfDateString(this DateTime dt)
		{
			string apiStartDtString = dt.ToString("yyyy-MM-dd") + "T00:00:00.0000000Z";
			return apiStartDtString;
		}

		public static string ToApiEndOfDateString(this DateTime dt)
		{
			string apiEndDtString = dt.ToString("yyyy-MM-dd") + "T23:59:59.0000000Z";
			return apiEndDtString;
		}

		public static DateTime ToStartOfDate(this DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day);
		}

		public static DateTime ToEndOfDate(this DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
		}

		public static DateTime ToBeginOfWeek(this DateTime dt)
		{
      var dateWork = dt;
      while (dateWork.DayOfWeek != DayOfWeek.Sunday)
      {
        dateWork = dateWork.AddDays(-1); 
      }
      return new DateTime(dateWork.Year, dateWork.Month, dateWork.Day, 0, 0, 0);
		}

		public static DateTime ToEndOfWeek(this DateTime dt)
		{
      var dateWork = dt;
      while (dateWork.DayOfWeek != DayOfWeek.Saturday)
      {
        dateWork = dateWork.AddDays(1); 
      }
      return new DateTime(dateWork.Year, dateWork.Month, dateWork.Day, 23, 59, 59, 999); 
		}

		public static DateTime ToBeginOfMonth(this DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
		}

		public static DateTime ToEndOfMonth(this DateTime dt)
		{
      var dateWork = dt.ToBeginOfMonth();
      return new DateTime(dt.Year, dt.Month, dt.LastDayOfMonth(), 23, 59, 59, 999); 
		}

		public static DateTime ToBeginOfQuarter(this DateTime dt)
		{
      int month = dt.Month;
      while (month != 1 && month != 4 && month != 7 && month != 10)
        month--;

      return new DateTime(dt.Year, month, 1, 0, 0, 0, 0);
		}

		public static DateTime ToEndOfQuarter(this DateTime dt)
    {
      int month = dt.Month;
      while (month != 3 && month != 6 && month != 9 && month != 12)
        month++;

      return new DateTime(dt.Year, month, 1, 1, 0, 0, 0).ToEndOfMonth().ToEndOfDate();
		}

		public static DateTime ToBeginOfYear(this DateTime dt)
		{
			return new DateTime(dt.Year, 1, 1, 0, 0, 0, 0);
		}

		public static DateTime ToEndOfYear(this DateTime dt)
		{
			return new DateTime(dt.Year, 12, 31, 23, 59, 59, 999);
		}

		public static DateTime ToNextDateAtMidnight(this DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1);
		}

		public static bool IsDbNull(this object value)
		{
			if (value == null)
				return false;

			if (value == DBNull.Value)
				return true;

			return false;
		}

		public static decimal? DbToDecimal(this object value)
		{
			if (value == null || value == DBNull.Value)
				return (decimal?)null;

			return Convert.ToDecimal(value);
		}

		public static int? DbToInt32(this object value)
		{
			if (value == null || value == DBNull.Value)
				return (int?)null;

			return Convert.ToInt32(value);
		}

		public static string DbToString(this object value)
		{
			if (value == null || value == DBNull.Value)
				return null;

			return value.ToString();
		}

    public static char? DbToChar(this object value)
    {
      if (value == null || value == DBNull.Value)
        return null;

      string stringValue = value.ToString().Trim();

      if (stringValue.Length > 0)
        return stringValue[0];

      return null;
    }

		public static bool? DbToBoolean(this object value)
		{
			if (value == null || value == DBNull.Value)
				return (bool?)null;

			return Convert.ToBoolean(value);
		}

		public static DateTime? DbToDateTime(this object value)
		{
			if (value == null || value == DBNull.Value)
				return (DateTime?)null;

			return Convert.ToDateTime(value);
		}

		public static TimeSpan? DbToTimeSpan(this object value)
		{
			if (value == null || value == DBNull.Value)
				return (TimeSpan?)null;

			return (TimeSpan?)value;
		}

    public static bool ContainsCustomAttribute(this Assembly value, Type type)
    {
      foreach(var attribute in value.CustomAttributes)
      {
        if(attribute.AttributeType == type)
        {
          return true;
        }
      }
      return false;
    }

    public static bool IsDerivedFromGenericCollection(this Type type)
    {
      if (type == null)
        return false;

      Type theType = type;

      while (theType != null)
      {
        if (theType.IsGenericCollection())
          return true;
        theType = theType.BaseType;
      }

      return false;
    }

    public static bool IsDerivedFromGenericCollection(this PropertyInfo pi)
    {
      if (pi ==  null)
        return false;

      return pi.PropertyType.IsDerivedFromGenericCollection();
    }

    public static bool IsDerivedFromGenericCollection(this object o)
    {
      if (o == null)
        return false;

      return o.GetType().IsDerivedFromGenericCollection();
    }

    public static bool IsGenericCollection(this Type type)
    {
      if (type == null)
        return false;
      
      if (type.FullName.Contains("System.Collections.Generic."))
        return true;

      return false;
    }

    public static bool IsGenericCollection(this object o)
    {
      if (o == null)
        return false;

      return o.GetType().IsGenericCollection();
    }

    public static Type[] GetGenericArguments(this object o)
    {
      if (o == null)
        return null;

      return o.GetType().GetGenericArguments();
    }

    public static bool IsGenericCollection(this PropertyInfo pi)
    {
      if (pi ==  null)
        return false;

      return pi.GetType().IsGenericCollection();
    }

    public static void InvokeAutoInit(this object o)
    {
      if (o == null)
        return;

      MethodInfo mi = o.GetType().GetMethod("AutoInit");
      if (mi != null)
        mi.Invoke(o, null); 
    }

    public static XMap GetXMap(this Type type)
    {
      if (type == null)
        return null;

      return type.GetCustomAttribute(typeof(XMap)) as XMap;
    }

    public static XMap GetXMap(this PropertyInfo pi)
    {
      if (pi == null)
        return null;

      var xMap = pi.GetCustomAttribute<XMap>();

      if (xMap == null)
        return null;

      return xMap;
    }

    public static bool HasXMap(this object o)
    {
      if (o == null)
        return false;

      var xMap = o.GetType().GetXMap();

      return xMap != null;
    }

    public static bool HasXMap(this PropertyInfo pi)
    {
      if (pi == null)
        return false;

      return pi.GetCustomAttribute<XMap>() != null;
    }

    public static XObjectType GetXObjectType(this Type type)
    {
      if (type == null)
        return XObjectType.Null;

      if (type.Name == "XElement")
        return XObjectType.XElement;

      if (type.IsDerivedFromGenericCollection())
        return XObjectType.GenericCollectionBased;

      XMap xMap = type.GetXMap();  
      if (xMap != null)
        return XObjectType.Complex;
      
      return XObjectType.Simple;
    }

    public static XObjectType GetXObjectType(this PropertyInfo pi)
    {
      if (pi == null)
        return XObjectType.Null;

      return pi.PropertyType.GetXObjectType();
    }

    public static List<PropertyInfo> GetXMapProperties(this Type type)
    {
      List<PropertyInfo> xmapPiList = new List<PropertyInfo>();
      var piSet = type.GetProperties().ToList();

      foreach (PropertyInfo pi in piSet)
      {
        if (pi.HasXMap())
          xmapPiList.Add(pi);
      }

      return xmapPiList;
    }

    public static XObject GetPropertyValue(this XElement x, PropertyInfo pi, Type objectType)
    {
      if (pi == null || x == null)
        return null;

      var xMap = pi.GetXMap();

      if (xMap == null)
        throw new Exception("The property '" + pi.Name + "' does not have an XMap attribute - xml being processed is '" + x.ToString().PadToLength(35).Trim() + "'.");

      string propertyName = pi.GetPropertyName();

      XObject propertyValue = null;

      if (xMap.XType == XType.Attribute)
      {
        propertyValue = x.Attribute(propertyName);
      }
      else
      {
        if (xMap.IsObject)
        {
          if (x.Elements().Count() > 1)
            throw new Exception("When XMap.IsObject is true, there cannot be more than one xml element present. Found " + x.Elements().Count().ToString() +
                                " elements when processing object of type '" + objectType.Name + "'.");
          propertyValue = x.Elements().FirstOrDefault();
        }
        else
        {
          propertyValue = x.Element(propertyName);
        }
      }

      return propertyValue;
    }

    public static string GetPropertyName(this PropertyInfo pi)
    {
      if (pi == null)
        return String.Empty;

      string propertyName = pi.Name;
      var xMap = pi.GetXMap();
      if (xMap != null && xMap.Name.IsNotBlank())
        propertyName = xMap.Name;

      return propertyName;
    }
  }
}
