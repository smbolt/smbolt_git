using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Org.Terminal.Controls
{
  public class FontSpec
  {
    public float FontSize { get; set; }
    public Size CharSize { get; set; }

    public FontSpec(float fontSize, Size charSize)
    {
      this.FontSize = fontSize;
      this.CharSize = charSize;
    }
  }
}
