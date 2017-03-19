using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Org.GS;

namespace MFToolkit.Net.Ntp
{
  public class NtpClient
  {
    public static DateTime GetNetworkTime()
    {
      if (g.IsInVisualStudioDesigner)
        return DateTime.Now;

      return GetNetworkTime("time-a.nist.gov");
    }


    public static DateTime GetNetworkTime(string ntpServer)
    {
      IPAddress[] address;

      try
      {
        address = Dns.GetHostEntry(ntpServer).AddressList;
      }
      catch
      {
        return DateTime.Now;
      }


      if (address == null || address.Length == 0)
        throw new ArgumentException("Could not resolve ip address from '" + ntpServer + "'.", "ntpServer");

      IPEndPoint ep = new IPEndPoint(address[0], 123);

      return GetNetworkTime(ep);
    }

    public static DateTime GetNetworkTime(IPEndPoint ep)
    {
      try
      {
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.SendTimeout = 3000;
        s.ReceiveTimeout = 3000;

        s.Connect(ep);

        byte[] ntpData = new byte[48]; // RFC 2030
        ntpData[0] = 0x1B;
        for (int i = 1; i < 48; i++)
          ntpData[i] = 0;

        s.Send(ntpData);
        s.Receive(ntpData);

        byte offsetTransmitTime = 40;
        ulong intpart = 0;
        ulong fractpart = 0;

        for (int i = 0; i <= 3; i++)
          intpart = 256 * intpart + ntpData[offsetTransmitTime + i];

        for (int i = 4; i <= 7; i++)
          fractpart = 256 * fractpart + ntpData[offsetTransmitTime + i];

        ulong milliseconds = (intpart * 1000 + (fractpart * 1000) / 0x100000000L);
        s.Close();

        TimeSpan timeSpan = TimeSpan.FromTicks((long)milliseconds * TimeSpan.TicksPerMillisecond);

        DateTime dateTime = new DateTime(1900, 1, 1);
        dateTime += timeSpan;

        TimeSpan offsetAmount = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
        DateTime networkDateTime = (dateTime + offsetAmount);
        return networkDateTime;
      }
      catch(Exception ex)
      {
        return DateTime.Now;
      }
    }

  }
}
