using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.FTP
{
  public static class FtpExtensionMethods
  {
    public static FileSystemItemSet ToFileSystemItemSet(this string s)
    {
      var set = new FileSystemItemSet();

      if (s.IsBlank())
        return set;

      if (s.Contains("\r\n"))
        s = s.Replace("\r", String.Empty);

      if (s.Contains("\t"))
        s = s.Replace("\t", " ");

      string[] lines = s.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (lines.Length == 0)
        return set;

      foreach (var line in lines)
      {
        string[] tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
        {
          if (tokens.Length == 9)
          {
            set.Add(BuildFileFromNineTokenDirectoryListEntry(tokens));
          }
          else
          {
            throw new Exception("FTP directory list output with lines contains containing a number of tokens different than 9 is not yet implemented. " + g.crlf +
                                "The directory list output line is '" + line + "'.");
          }
        }
      }

      return set;
    }

    public static FileSystemItem BuildFileFromNineTokenDirectoryListEntry(this string[] array)
    {
      try
      {
        // This format expects nine string tokens and is strictly looking for particular data values in specific array entries.
        // If another formats is encountered an exception is thrown providing the detail of the encountered values to provide
        // information for the adaptation to new format.

        if (array == null)
          throw new Exception("The array of tokens is null.");

        if (array.Length != 9)
          throw new Exception("The array of tokens contains " + array.Length.ToString() + " entries but should contain 9 entries.");

        if (!array[4].IsInteger())
          throw new Exception("The array entry at index 4 is expected to be an integer (file size), but a non-integer value was found '" + array[4] + ".");

        long fileSize = array[4].ToInt64();

        if (!array[5].IsMonthString())
          throw new Exception("The array entry at index 5 is expected to be a valid month string, but an invalid value was found '" + array[5] + ".");

        if (!array[6].IsInteger())
          throw new Exception("The array entry at index 6 is expected to be an integer (day of month), but a non-integer value was found '" + array[6] + ".");

        int month = array[5].MonthToMonthNumber();
        int day = array[6].ToInt32();
        int year = DateTime.Now.Year; //no year is provided in this format - may need to adapt later
        DateTime? fileModifiedDate = null;

        try
        {
          fileModifiedDate = new DateTime(year, month, day);
        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred while trying to create a valid date from the values: month=" + month.ToString() + ", day=" + day.ToString() +
                              ", year=" + year.ToString() + ".", ex);
        }

        if (!array[7].IsValidTime())
          throw new Exception("The array entry at index 7 is expected to be a valid time of day value (i.e 10:15), but an invalid value was found '" + array[7] + ". " +
                              "The array values are '" + array.StringArrayToString() + "'.");

        TimeSpan? timeOfDay = null;

        try
        {
          timeOfDay = TimeSpan.Parse(array[7]);
        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred while trying to create a valid time of day value from the token '" + array[7] + ".", ex);
        }

        string fileName = array[8];

        var lastModifiedDate = new DateTime(fileModifiedDate.Value.Year, fileModifiedDate.Value.Month, fileModifiedDate.Value.Day,
                                            timeOfDay.Value.Hours, timeOfDay.Value.Minutes, 0);

        var file = new FileSystemItem();
        file.FileSystemItemType = FileSystemItemType.File;
        file.Name = fileName;
        file.Size = fileSize;
        file.LastChangedDateTime = lastModifiedDate;

        return file;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a FileSystemItem (type=file) from an array of tokens which contains " +
                            "the values '" + array.StringArrayToString() + "'.", ex);
      }
    }
  }
}
