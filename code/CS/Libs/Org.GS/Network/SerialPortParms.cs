using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Org.GS.Network
{
  public enum DataMode
  {
    Text,
    Binary
  }

  public class SerialPortParms
  {
    public DataMode DataMode { get; set; }
    public int BaudRate { get; set; }
    public int DataBits { get; set; }
    public Parity Parity { get; set; }
    public StopBits StopBits { get; set; }
    public string PortName { get; set; }
    public string DeviceIdentifer { get; set; }
    public bool InDiagnosticsMode { get; set; }

    public SerialPortParms()
    {
      this.DataMode = DataMode.Text;
      this.BaudRate = 9600;
      this.DataBits = 8;
      this.Parity = Parity.None;
      this.StopBits = StopBits.One;
      this.PortName = String.Empty;
      this.DeviceIdentifer = String.Empty;
      this.InDiagnosticsMode = false; 
    }
  }
}
