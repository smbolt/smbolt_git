using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.PipesWorkbench
{
  public class ReadFileToStream
  {
    private string fn;
    private StreamString ss;

    public ReadFileToStream(StreamString str, string filename)
    {
      fn = filename;
      ss = str;
    }

    public void Start()
    {
      //string contents = File.ReadAllText(fn);
      ss.WriteString("SIMULATED FILE CONTENTS HERE");
    }
  }
}
