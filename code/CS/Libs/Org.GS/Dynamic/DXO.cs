using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace Org.GS
{
  public enum DxoCommand
  {
    NotSet,
    SetFormSize,
    CloseForm,
    PingCentralUpdateControl,
    CheckCentralUpdateControl
  }

  public class DXO : XElement
  {
    // pass parameter to base (XElement)
    public DXO(string commandName) : base("DXO")
    {
      this.Add(new XAttribute("CommandName", commandName));
    }

    public void SetFormSize(int width, int height)
    {
      this.Add(
        new XElement("Parameters",
                     new XElement("Size",
                                  new XAttribute("Width", width.ToString()),
                                  new XAttribute("Height", height.ToString())
                                 )
                    )
      );
    }

    public Size GetFormSize()
    {
      return new Size(Int32.Parse(this.Element("Parameters").Element("Size").Attribute("Width").Value),
                      Int32.Parse(this.Element("Parameters").Element("Size").Attribute("Height").Value));
    }


    /*  Pattern for creating Parameters Element

      this.Add(
        new XElement("Parameters",
          new XElement("Size",
            new XAttribute("Width", width.ToString()),
            new XAttribute("Height", height.ToString())
            ),
          new XElement("Size2",
            new XAttribute("Width", width.ToString()),
            new XAttribute("Height", height.ToString())
            )
          )
        );
    */
  }
}
