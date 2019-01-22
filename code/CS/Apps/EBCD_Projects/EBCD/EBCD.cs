using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;


namespace Adsdi.EBCD
{
  [Serializable]
  public class EBCD : MarshalByRefObject
  {
    public string Name {
      get;
      set;
    }
    public string Version {
      get;
      set;
    }
    public DateTime BuildDate {
      get;
      set;
    }
    public SortedList<string, string> Config {
      get;
      set;
    }

    public EBCD()
    {
      Name = String.Empty;
      Version = String.Empty;
      BuildDate = DateTime.Now;
      Config = new SortedList<string, string>();
    }
  }
}
