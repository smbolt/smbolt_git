using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
    public class DateUtility
    {

        public static DateTime GetDateFromLongNumeric(string date)
        {
            bool includesTime = false;
            bool includesMilliseconds = false;

            string d = date.Trim();

            if (!d.IsNumeric())
                return DateTime.MinValue;

            if (d.Length == 17)
            {
                includesTime = true;
                includesMilliseconds = true;
            }
            else
                if (d.Length == 14)
                    includesTime = true;
                else
                    if (d.Length != 8)
                        return DateTime.MinValue;

            int year = Int32.Parse(d.Substring(0, 4));
            int month = Int32.Parse(d.Substring(4, 2));
            int day = Int32.Parse(d.Substring(6, 2));
            int hour = 0;
            int minute = 0;
            int second = 0;
            int milliseconds = 0;

            if (includesTime)
            {
                hour = Int32.Parse(d.Substring(8, 2));
                minute = Int32.Parse(d.Substring(10, 2));
                second = Int32.Parse(d.Substring(12, 2));
            }

            if (includesMilliseconds)
                milliseconds = Int32.Parse(d.Substring(14, 3));

            if (year < 1850 || year > 2100)
                return DateTime.MinValue;

            if (month < 1 || month > 12)
                return DateTime.MinValue;

            if (day < 1 || day > 31)
                return DateTime.MinValue;

            if (hour > 24)
                return DateTime.MinValue;

            if (minute > 59)
                return DateTime.MinValue;

            if (second > 59)
                return DateTime.MinValue;

            return new DateTime(year, month, day, hour, minute, second, milliseconds);
        }
    }
}
