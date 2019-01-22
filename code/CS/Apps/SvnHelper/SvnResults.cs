using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.SvnHelper
{
  public class SvnResults
  {
    private Dictionary<string, string> _rawResults;

    public SvnResults()
    {
      this._rawResults = new Dictionary<string, string>();
    }

    public void AddResult(string result)
    {
      string timeStamp = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
      while (this._rawResults.ContainsKey(timeStamp))
      {
        System.Threading.Thread.Sleep(5);
        timeStamp = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
      }

      this._rawResults.Add(timeStamp, result);
    }

    public string GetResults()
    {
      string[] delim = new string[] { g.crlf };

      StringBuilder sb = new StringBuilder();

      foreach (KeyValuePair<string, string> kvp in this._rawResults)
      {
        List<string> lines = kvp.Value.Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (string line in lines)
        {
          if (line.StartsWith("Updating ") || line.StartsWith("Transmitting file data"))
            continue;

          if (line.Contains("svn commit"))
          {
            string work1 = line.Replace("*", String.Empty).Replace("svn commit", String.Empty).Trim();
            int pos = work1.IndexOf("-m");
            if (pos > -1)
              work1 = work1.Substring(0, pos - 1).Trim();

            sb.Append("Commit : " + work1 + g.crlf);
            continue;
          }

          if (line.StartsWith("Sending ") || line.StartsWith("Committed revision"))
          {
            sb.Append("    " + line + g.crlf);
            continue;
          }


          if (line.Contains("svn update"))
          {
            string work1 = line.Replace("*", String.Empty).Replace("svn update", String.Empty).Trim();
            int pos = work1.IndexOf("-m");
            if (pos > -1)
              work1 = work1.Substring(0, pos - 1).Trim();

            sb.Append("Update : " + work1 + g.crlf);
            continue;
          }

          sb.Append(line + g.crlf);
        }
      }


      string results = sb.ToString();
      return results;
    }

  }
}
