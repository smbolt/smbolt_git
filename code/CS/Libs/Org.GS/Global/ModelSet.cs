using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ModelSet
  {
    public List<ModelBase> ModelList { get; set; }
    public int TotalEntityCount { get; set; }
    public int SubsetEntityCount { get; set; }

    public ModelSet()
    {
      this.ModelList = new List<ModelBase>();
      this.TotalEntityCount = 0;
      this.SubsetEntityCount = 0; 
    }
  }
}
