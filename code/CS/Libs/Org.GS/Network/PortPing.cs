using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Org.GS;

namespace Org.GS.Network
{
  public class PortPing
  {
    public static TaskResult PingPort(string ipAddress, int port)
    {
      try
      {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(ipAddress, port);

        if (tcpClient.Connected)
        {
          NetworkStream nstream = tcpClient.GetStream();
          Byte[] data = Encoding.UTF8.GetBytes("TestMessage");
          Byte[] lbuffer = BitConverter.GetBytes(data.Length);
          nstream.Write(lbuffer, 0, lbuffer.Length);
          nstream.Write(data, 0, data.Length);
        }

        string bytesAvailable = tcpClient.Available.ToString();

        tcpClient.Close();
        tcpClient = null;
        return new TaskResult("PortPing", "Connection to " + ipAddress + ", port " + port.ToString() + " was successful.", TaskResultStatus.Success); 
      }
      catch (Exception ex)
      {
        return new TaskResult("PortPing", 
            "Connection to " + ipAddress + ", port " + port.ToString() + " failed. " + g.crlf2 + "Exception Message: " 
            + g.crlf + ex.Message, TaskResultStatus.Failed); 
      }
    }
  }
}
