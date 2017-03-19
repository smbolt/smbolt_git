using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class SearchResults
  {
    public SortedList<string, OSFile> OSFileList { get; set; }

    public SearchResults()
    {
      this.OSFileList = new SortedList<string, OSFile>();
    }
  }
}
