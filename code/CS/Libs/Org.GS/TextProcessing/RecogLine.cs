using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)] 
  public class RecogLine
  {
    [XMap]
    public string ID { get; set; }

    [XMap]
    public string Use { get; set; }

    [XMap]
    public string Text { get; set; }

    public RecogLine()
    {
      this.ID = String.Empty;
      this.Use = String.Empty;
      this.Text = String.Empty;
    }
  }
}
