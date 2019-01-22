using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ModelBase
  {
    public PropertyInfoPairSet PropertyInfoPairSet {
      get;
      set;
    }

    public ModelBase()
    {
      this.PropertyInfoPairSet = new PropertyInfoPairSet();
    }
  }

  public class EntityBase
  {
  }
}
