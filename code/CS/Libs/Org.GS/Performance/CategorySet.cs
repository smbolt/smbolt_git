using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element, CollectionElements="Category")]
  public class CategorySet : SortedList<string, Category>
  {
  }
}
