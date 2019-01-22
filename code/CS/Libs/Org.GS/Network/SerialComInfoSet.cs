using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.Network
{
  public class SerialComInfoSet : Dictionary<string, SerialComInfo>
  {
    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();

      if (this.Count == 0)
        return "No COM devices found.";

      foreach (KeyValuePair<string, SerialComInfo> kvp in this)
      {
        SerialComInfo sci = kvp.Value;
        sb.Append("COM PORT            : " + sci.PortName + g.crlf +
                  "  InstanceName      : " + sci.InstanceName + g.crlf +
                  "  Active            : " + sci.Active.ToString() + g.crlf +
                  "  BaudRate          : " + sci.BaudRate.ToString() + g.crlf +
                  "  BitsPerByte       : " + sci.BitsPerByte.ToString() + g.crlf +
                  "  Parity            : " + sci.Parity + g.crlf +
                  "  StopBits          : " + sci.StopBits.ToString() + g.crlf +
                  "  IsBusy            : " + sci.IsBusy.ToString() + g.crlf +
                  "  ReceivedCount     : " + sci.ReceivedCount.ToString() + g.crlf +
                  "  TransmittedCount  : " + sci.TransmittedCount.ToString() + g.crlf);

        if (sci.PnP_InfoFound)
        {
          sb.Append(
                "  PnP Caption       : " + sci.PnP_Caption + g.crlf +
                "  PnP Description   : " + sci.PnP_Description + g.crlf + 
                "  PnP Manufacturer  : " + sci.PnP_Manufacturer + g.crlf + 
                "  PnP Name          : " + sci.PnP_Name + g.crlf + 
                "  PnP DeviceID      : " + sci.PnP_DeviceID + g.crlf + 
                "  PnP PNP DeviceID  : " + sci.PnP_PNPDeviceID + g.crlf);
        }
        else
        {
          sb.Append("  NO PNP info found" + g.crlf); 
        }

        sb.Append(g.crlf); 
      }

      string report = sb.ToString();
      return report;
    }

    public string GetDeviceReport(string comDeviceName)
    {
      if (!this.ContainsKey(comDeviceName))
        return "COM device '" + comDeviceName + "' is not found.";

      SerialComInfo sci = this[comDeviceName];

      return sci.GetDeviceReport();
    }
  }
}
