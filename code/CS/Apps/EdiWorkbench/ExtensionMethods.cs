using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; 
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using Org.GS;

namespace Org.EdiWorkbench
{
    public static class ExtensionMethods
    {
        public static string PadWork = null;
        public static string PadZero = null;

        public static string ExceptionReport(this Exception e, bool includeStackTrace)
        {
            return GetExceptionData(1, e, includeStackTrace);
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


        public static bool IsValidOrdinalNumber(this string value)
        {
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

        public static bool HasSentenceEndingPunctuation(this string value)
        {
            string testValue = value.Trim();

            if (testValue.EndsWith("."))
                return true;

            if (testValue.EndsWith("!"))
                return true;

            if (testValue.EndsWith("?"))
                return true;

            return false;
        }

        public static string PrepForSpellCheck(this string value)
        {
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

        public static int IndexOfNoRepeatChar(this string value, char c, int startIndex)
        {
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

        public static string DoubleCharAtPos(this string value, int pos)
        {
            if (pos > value.Length - 1)
                return String.Empty;

            string charToDouble = value.Substring(pos, 1);
            string newValue = value.Substring(0, pos) + charToDouble + value.Substring(pos, value.Length - pos);

            return newValue;
        }

        public static string ToDelimitedList(this List<string> value, string delimiter)
        {
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
            if (value.Trim().Length == 0)
                return 0F;

            string valueNoDecimals = value.Replace(".", String.Empty).Trim();

            int signMultiplier = 0;
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

        //[DebuggerStepThrough]
        public static decimal ToDecimal(this string value)
        {
            if (value.Trim().Length == 0)
                return 0M;

            string valueNoDecimals = value.Replace(".", String.Empty).Trim();

            int signMultiplier = 0;
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
        public static int CountOfChar(this string value, char ch)
        {
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
        public static bool ToBoolean(this string value)
        {
            value = value.Trim().ToLower();

            if (value.Trim().Length == 0)
                return false;

            if (value.In("t, true, 1"))
                return true;
            
            return false;
        }

        [DebuggerStepThrough]
        public static UInt32 ToUInt32(this string value)
        {
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
            value = value.Trim().ToLower();

            if (value.Trim().Length == 0)
                return 0;

            if (!value.IsNumeric())
                return 0;

            return UInt64.Parse(value); 
        }

        [DebuggerStepThrough]
        public static bool In(this string value, string set)
        {
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
        public static bool IsNumeric(this string value)
        {
            if (value.Trim().Length == 0)
                return false;

            foreach (Char c in value)
                if (!Char.IsNumber(c))
                    return false;

            return true;
        }

        //[DebuggerStepThrough]
        public static bool IsIPV4Address(this string value)
        {
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
        public static bool IsFloat(this string value)
        {
            string trimmedValue = value.Trim();

            if (trimmedValue.Length == 0)
                return false;

            int periodCount = trimmedValue.CountOfChar('.');

            if (periodCount > 1)
                return false;

            if (trimmedValue[trimmedValue.Length - 1] == '.')
                return false;

            return trimmedValue.Replace(".", String.Empty).IsNumeric();
        }

        [DebuggerStepThrough]
        public static void Clear(this string value)
        {
            value = String.Empty;
        }

        [DebuggerStepThrough]
        public static bool IsNotNumeric(this string value)
        {
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
            if (value.Trim().Length == 0)
                return 0;

            if (value.IsNotNumeric())
                return 0;

            return Int32.Parse(value);
        }

        [DebuggerStepThrough]
        public static bool IsBlank(this string value)
        {
            if (value.Trim().Length == 0)
                return true;

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsNotBlank(this string value)
        {
            if (value.Trim().Length > 0)
                return true;

            return false;
        }

        [DebuggerStepThrough]
        public static int CharCount(this string value, char c)
        {
            return value.Count(f => f == c);
        }

        [DebuggerStepThrough]
        public static string SetIfBlank(this string value, string newValue)
        {
            if (value.IsNotBlank())
                return value;

            return newValue;
        }

        [DebuggerStepThrough]
        public static bool IsLeapYear(this DateTime value)
        {
            int year = value.Year;

            if (year % 4 != 0) 
                return false; 

            if (year % 100 == 0) 
                return (year % 400 == 0); 
            
            return true; 
        }

        [DebuggerStepThrough]
        public static XElement GetElement(this XElement value, string elementName)
        {
            XNamespace ns = value.Name.NamespaceName;

            if (value.Element(ns + elementName) == null)
                return null;

            return value.Element(ns + elementName);
        }

        [DebuggerStepThrough]
        public static XElement GetRequiredElement(this XElement value, string elementName)
        {
            XNamespace ns = value.Name.NamespaceName;

            if (value.Element(ns + elementName) == null)
                throw new Exception("Required xml element '" + elementName + "' is not found in the xml '" + value.ToString() + "'.");

            return value.Element(ns + elementName);
        }

        [DebuggerStepThrough]
        public static string GetAttributeValue(this XElement value, string attributeName)
        {
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
            XNamespace ns = value.Name.NamespaceName;

            if (value.Attribute(ns + attributeName) == null)
                return null;

            return value.Attribute(ns + attributeName).Value.Trim();
        }

        [DebuggerStepThrough]
        public static string GetRequiredElementAttributeValue(this XElement value, string elementName, string attributeName)
        {
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
            XNamespace ns = value.Name.NamespaceName;

            if (value.Attribute(ns + attributeName) == null)
                return false;

            return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
        }

        [DebuggerStepThrough]
        public static bool GetBooleanAttributeWithDefault(this XElement value, string attributeName, bool defaultValue)
        {
            XNamespace ns = value.Name.NamespaceName;

            if (value.Attribute(ns + attributeName) == null)
                return defaultValue;

            return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
        }

        [DebuggerStepThrough]
        public static bool? GetBooleanAttributeValueOrNull(this XElement value, string attributeName)
        {
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
            XNamespace ns = value.Name.NamespaceName;

            if (value.Attribute(ns + attributeName) == null)
                return null;

            string attributeValue = value.Attribute(ns + attributeName).Value.ToLower().Trim();

            return Int32.Parse(attributeValue);
        }

        [DebuggerStepThrough]
        public static bool GetRequiredBooleanAttribute(this XElement value, string attributeName)
        {
            XNamespace ns = value.Name.NamespaceName;

            if (value.Attribute(ns + attributeName) == null)
                throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

            return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
        }

        [DebuggerStepThrough]
        public static object GetEnumAttribute(this XElement value, string attributeName, Type type)
        {
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
                sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + g.crlf +
                          "Message: " + ex.Message + g.crlf +
                          "StackTrace:" + ex.StackTrace + g.crlf);

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
        public static string PadTo(this string value, int totalLength)
        {
            if (value == null)
                return "NULL".PadTo(totalLength);

            // establish PadWork if it has not yet been established
            if (PadWork == null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 200; i++)
                {
                    sb.Append(" ");
                }
                PadWork = sb.ToString();
            }

            if (totalLength > 200)
                totalLength = 200;

            string returnValue = (value.Trim() + PadWork).Substring(0, totalLength);

            return returnValue;
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


        public static string ToHexRepresentation(this byte[] b)
        {



            return String.Empty;
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


        // IMPORTANT - this method will truncate digits if the value passed in is greather than the totalLength specification.
        // Example if totalLength = 4, "1" will be come "0001", but "12345" will become "2345".
        [DebuggerStepThrough]
        public static string PadWithLeadingZeros(this string value, int totalLength)
        {
            value = value.Trim();

            if (value.Length == totalLength)
                return value;

            if (value.Length > totalLength)
            {
                string truncated = value.Substring((value.Length - totalLength), totalLength);
                return truncated;
            }

            // establish PadWork if it has not yet been established
            if (PadZero == null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 200; i++)
                {
                    sb.Append("0");
                }
                PadZero = sb.ToString();
            }

            if (totalLength > 200)
                totalLength = 200;

            int padLength = totalLength - value.Length;
            string returnValue = PadZero.Substring(0, padLength) + value;

            return returnValue;
        }

        private static string ToHex(this int n)
        {
            return n.ToString("X"); 
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

    }
}
