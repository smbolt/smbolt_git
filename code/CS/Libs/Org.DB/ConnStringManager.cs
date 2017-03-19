using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DB
{
  public static class ConnStringManager
  {
    private static Dictionary<string, string> _map = new Dictionary<string, string>();

    public static string Translate(string connStringName)
    {
      if (_map.Count == 0)
        return connStringName;

      if (_map.ContainsKey(g.SystemInfo.ComputerName))
        return connStringName + "_" + _map[g.SystemInfo.ComputerName]; 

      return connStringName;
    }

    public static void Load(string config)
    {
      _map = new Dictionary<string,string>();

      if (config == null)
        return;

      string[] tokens = config.ToTokenArray(Constants.CommaDelimiter);
      if (tokens.Length % 2 != 0)
        return;

      for (int i = 0; i < tokens.Length; i++)
      {
        _map.Add(tokens[i].Trim(), tokens[i + 1].Trim());
        i++;
      }
    }
  }
}
