using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  // Hierarchical Text-Base Object (Hto)
  public class Hto
  {
    public string Id { get; set; }
    private string _value;
    public string Value
    {
      get { return Get_Value(); }
      set { _value = value; }
    }
    public bool IsElemental { get { return this.HtoSet == null; } }
    public bool IsRoot { get { return this.Parent == null; } }
    public int Count { get { return this.HtoSet?.Count ?? 0; } }
    public HtoSet HtoSet { get; set; }
    public Hto Parent { get; set; }
    public Hto PrevSibling { get { return Get_PrevSibling(); } }
    public Hto NextSibling { get { return Get_NextSibling(); } }
    public int Seq { get; set; }
    public int Level { get { return Get_Level(); } }

    public Hto()
    {
    }

    public Hto(string id, string value, Hto parent = null)
    {
      this.Id = id ?? new Guid().ToString();
      this._value = value ?? String.Empty;
      this.Parent = parent;
      this.HtoSet = null;
    }

    public Hto(string id, HtoSet htoSet, Hto parent = null)
    {
      this.Id = id ?? new Guid().ToString();
      this._value = null;
      this.HtoSet = htoSet;
      this.Parent = parent;
    }

    private string Get_Value()
    {
      if (this.IsElemental)
        return _value?.Trim() ?? String.Empty;

      return GetValueRecursive();
    }

    public string GetValueRecursive()
    {
      var sb = new StringBuilder();
      GetValueRecursive(sb);
      string value = sb.ToString();
      return value;
    }

    public void GetValueRecursive(StringBuilder sb)
    {
      string indent = new string(' ', this.Level * 2);

      sb.Append(indent + this.Level.ToString() + "." + this.Seq.ToString() + "  " + (this.Id ?? "NO-ID") + ": ");

      if (this.IsElemental)
      {
        sb.Append(_value + g.crlf);
      }
      else
      {
        sb.Append("(" + this.HtoSet.HtoSourceObjectType.ToString() + ")" + g.crlf);
        foreach (var hto in this.HtoSet)
        {
          hto.GetValueRecursive(sb);
        }
      }
    }

    private int Get_Level()
    {
      int level = 0;

      Hto parent = this.Parent;

      while (parent != null)
      {
        level++;
        parent = parent.Parent;
      }

      return level;
    }

    private Hto Get_PrevSibling()
    {
      int prevSeq = this.Seq - 1;

      if (this.Parent == null || this.Parent.HtoSet == null)
        return null;

      if (prevSeq < 0 || prevSeq > this.Parent.HtoSet.Count - 1)
        return null;

      return this.Parent.HtoSet.ElementAt(prevSeq);
    }

    private Hto Get_NextSibling()
    {
      int nextSeq = this.Seq + 1;

      if (this.Parent == null || this.Parent.HtoSet == null)
        return null;

      if (nextSeq < 0 || nextSeq > this.Parent.HtoSet.Count - 1)
        return null;

      return this.Parent.HtoSet.ElementAt(nextSeq);
    }
  }
}
