using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NameTags
{
  public static class LogHelper
  {
    public static void Log(string logEntry)
    {
      if (!Directory.Exists(ConfigHelper.DefaultLogPath))
        Directory.CreateDirectory(ConfigHelper.DefaultLogPath);

      if (!File.Exists(ConfigHelper.DefaultLogPath + ConfigHelper.DefaultLogFileName))
      {
        StreamWriter sw = File.CreateText(ConfigHelper.DefaultLogPath + ConfigHelper.DefaultLogFileName);
        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + logEntry);
        sw.Flush();
        sw.Close();
      }
      else
      {
        StreamWriter sw = File.AppendText(ConfigHelper.DefaultLogPath + ConfigHelper.DefaultLogFileName);
        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + logEntry);
        sw.Flush();
        sw.Close();
      }
    }
  }
}
