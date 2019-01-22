using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XWrapperSet : SortedList<int, XWrapper>
  {
    public string ElementList {
      get {
        return Get_ElementList();
      }
    }

    public XWrapperSet(XElement e)
    {
      this.Load(e);
    }

    private void Load(XElement e)
    {
      this.Clear();

      foreach (var childElement in e.Elements())
      {
        this.Add(this.Count, new XWrapper(this.Count, childElement));
      }
    }

    private string Get_ElementList()
    {
      var sb = new StringBuilder();

      if (this.Count == 0)
      {
        sb.Append("No child elements");
      }
      else
      {
        foreach (var kvp in this)
        {
          if (sb.Length > 0)
            sb.Append(g.crlf);
          string firstLine = kvp.Value.XElement.ToString().FirstLine(50);
          sb.Append(kvp.Key.ToString("0000") + "  " + firstLine);
        }
      }

      return sb.ToString();
    }
  }
}
