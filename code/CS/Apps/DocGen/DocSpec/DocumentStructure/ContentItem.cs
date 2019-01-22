using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class ContentItem
  {
    public string DeName {
      get;
      set;
    }
    public string ContentQuery {
      get;
      set;
    }
    public string ContentValue {
      get;
      set;
    }

    public ContentItem() { }
    public ContentItem(string deName, string contentQuery, string contentValue)
    {
      this.DeName = deName;
      this.ContentQuery = contentQuery;
      this.ContentValue = contentValue;
    }
  }
}
