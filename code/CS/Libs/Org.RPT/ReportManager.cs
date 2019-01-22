using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.RPT
{
  public class ReportManager
  {
    public static void Initialize()
    {
      XmlMapper.AddAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
