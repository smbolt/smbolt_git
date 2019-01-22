using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Analysis
{
  public class Analytic
  {
    private static bool _isInitialized = Initialize();
    public static Dictionary<int, string> IdentifierDictionary { get; set; }
    public static Dictionary<int, string> ColumnNames { get; set; }
    public Dictionary<int, int> Identifiers { get; set; }
    public int[] DataArray { get; set; }
    public string ArrayString { get { return Get_ArrayString(); } }

    public Analytic(int length)
    {
      this.Identifiers = new Dictionary<int, int>();
      this.DataArray = new int[length];
    }

    private string Get_ArrayString()
    {
      if (this.DataArray == null || this.DataArray.Length == 0)
        return String.Empty;

      var sb = new StringBuilder();
      foreach (int item in this.DataArray)
      {
        sb.Append(item.ToString());
      }

      return sb.ToString();
    }

    private static bool Initialize()
    {
      if (_isInitialized)
        return true;

      IdentifierDictionary = new Dictionary<int, string>();
      ColumnNames = new Dictionary<int, string>();

      return true;
    }
  }
}
