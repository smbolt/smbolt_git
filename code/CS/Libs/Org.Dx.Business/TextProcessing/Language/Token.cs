using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business.TextProcessing
{
  public class Token
  {
    public string Text { get; set; }
    public bool IsRequired { get; set; }

    public Token()
    {
      this.Text = String.Empty;
      this.IsRequired = false;
    }
  }
}
