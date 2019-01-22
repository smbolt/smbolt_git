using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Script
  {
    private string _rawText;
    private string _compText;

    public Script(string rawText)
    {
      ParseScript();
    }

    private void ParseScript()
    {
      try
      {
        _compText = _rawText.CondenseText(); 

      }
      catch (Exception ex)
      {


      }
    }

  }
}
