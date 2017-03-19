using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.ComponentModel;

namespace Org.GS
{
  public class NetworkHelper
  {
    private static bool isTimeInitialized = false;
    private static DateTime networkTimeAtCheck;
    private static DateTime computerTimeAtCheck;
    private static TimeSpan computerOffsetAtCheck;

    private static int _timeCheckIntervalSeconds = 3600;
    public static int TimeCheckIntervalSeconds
    {
      get { return _timeCheckIntervalSeconds; }
      set { _timeCheckIntervalSeconds = value; }
    }

    public static DateTime GetTime()
    {
      return GetTime(_timeCheckIntervalSeconds);
    }

    public static DateTime GetTime(int controlIntervalSeconds)
    {
      if (!isTimeInitialized)
        CheckNetworkTime();

      DateTime currentComputerTime = DateTime.Now;
      TimeSpan timeSinceCheck = currentComputerTime - computerTimeAtCheck;
      if (timeSinceCheck.TotalSeconds > controlIntervalSeconds)
        CheckNetworkTime();

      return DateTime.Now - computerOffsetAtCheck;
    }

    private static void CheckNetworkTime()
    {
      networkTimeAtCheck = MFToolkit.Net.Ntp.NtpClient.GetNetworkTime();
      computerTimeAtCheck = DateTime.Now;
      computerOffsetAtCheck = computerTimeAtCheck - networkTimeAtCheck;
      isTimeInitialized = true;
    }

    public static string GetIPAddressFromHostName(string hostName)
    {
      IPAddress ipAddress = DnsResolveAddress(hostName);
      return ipAddress.ToString();
    }

    public static IPAddress DnsResolveAddress(string address)
    {
      IPAddress[] ipAddresses = Dns.GetHostEntry(address).AddressList;

      if (address == null || address.Length == 0)
        throw new ArgumentException("Could not resolve ip address from '" + address + "'.", "address");

      return ipAddresses[0];
    }
        
    public static void PingHost(string address)
    {
      IPAddress ipAddress = DnsResolveAddress(address);

      Ping pingSender = new Ping();
      PingOptions options = new PingOptions();

      options.DontFragment = true;

      // Create a buffer of 32 bytes of data to be transmitted.
      string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      byte[] buffer = Encoding.ASCII.GetBytes(data);
      int timeout = 5000;
      PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

      switch (reply.Status)
      {
        case IPStatus.Success:
          break;

        default:
          break;

      }
    }
        
    public static TaskResult PingHost(TaskResult taskResult, string address, int timeout)
    {
      IPAddress ipAddress = DnsResolveAddress(address);

      Ping pingSender = new Ping();
      PingOptions options = new PingOptions();

      options.DontFragment = true;

      // Create a buffer of 32 bytes of data to be transmitted.
      string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      byte[] buffer = Encoding.ASCII.GetBytes(data);

      PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

      switch (reply.Status)
      {
        case IPStatus.Success:
              taskResult.TaskResultStatus = TaskResultStatus.Success;
              taskResult.Data = reply.RoundtripTime.ToString("######0");
              break;

        case IPStatus.TimedOut:
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Network ping of host '" + address + "' at IP address '" + ipAddress.ToString() + "' failed due to a timeout - no response in the allowed " + timeout.ToString() + " milliseconds.";
          break;

        default:
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Network ping of host '" + address + "' at IP address '" + ipAddress.ToString() + "' failed due for reason '" + reply.Status.ToString() + "'.";
          break;
      }

      return taskResult;
    }
        
    public static string GetCurrentIpAddress()
    {
      string hostname = Dns.GetHostName();
      IPAddress[] ips = Dns.GetHostAddresses(hostname);
      string ip = String.Empty;
      string hold254Address = String.Empty;

      foreach (IPAddress ipa in ips)
      {
        if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        {
          string tempIP = ipa.ToString().Trim();
          if (tempIP.IndexOf("169.254.") > -1)
            hold254Address = tempIP;
          else
          {
            ip = tempIP;
            break;
          }
        }
      }

      if (ip.Trim().Length == 0)
      {
        if (hold254Address.Trim().Length > 0)
        {
          ip = hold254Address;
        }
        else
        {
          if (ips.Length > 0)
            ip = ips[0].ToString();
        }
      }

      return ip;
    }

    public static string GetIP4Address(string hostAddress)
    {
      string IP4Address = String.Empty;
      foreach (IPAddress IPA in Dns.GetHostAddresses(hostAddress))
      {
        if (IPA.AddressFamily.ToString() == "InterNetwork")
        {
          IP4Address = IPA.ToString();
          break;
        }
      }

      if (IP4Address != String.Empty)
      {
        return IP4Address;
      }

      foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
      {
        if (IPA.AddressFamily.ToString() == "InterNetwork")
        {
          IP4Address = IPA.ToString();
          break;
        }
      }

      return IP4Address;
    }

    public static string GetHostNameFromWebServiceEndpoint(string endpoint)
    {
      string hostName = endpoint.Trim().Replace("https://", String.Empty);
      hostName = hostName.Replace("http://", String.Empty);
      hostName = hostName.Replace(@"\", "/");
      int slashPos = hostName.IndexOf("/");
      if (slashPos > 0)
        hostName = hostName.Substring(0, slashPos);

      int colonPos = hostName.IndexOf(":");
      if (colonPos > 0)
        hostName = hostName.Substring(0, colonPos);

      return hostName.Trim();
    }


    public static TaskResult CheckNetworkConnectivity(TaskResult taskResult)
    {
      try
      {
        if (NetworkInterface.GetIsNetworkAvailable())
        {
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Message = "Network is available.";
        }
        else
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Network is not available.";
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "Network availability check failed with an exception of type '" + ex.GetType().ToString() + "'";
        taskResult.Exception = ex;
        taskResult.FullErrorDetail = taskResult.Message + g.crlf + ex.Message;
        return taskResult;
      }
    }

    public static TaskResult PingServer(string serverName)
    {
      string ipv4Address = String.Empty;
      int timeout = 15000;

      TaskResult taskResult = new TaskResult();
      taskResult.TaskName = "PingServer";

      IPAddress ipAddress = DnsResolveAddress(serverName);

      Ping pingSender = new Ping();
      PingOptions options = new PingOptions();

      options.DontFragment = true;

      // Create a buffer of 32 bytes of data to be transmitted.
      string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      byte[] buffer = Encoding.ASCII.GetBytes(data);

      PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

      switch (reply.Status)
      {
        case IPStatus.Success:
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Data = reply.RoundtripTime.ToString("######0");
          break;

        case IPStatus.TimedOut:
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Network ping of host '" + ipAddress + "' at IP address '" + ipAddress.ToString() + "' failed due to a timeout - no response in the allowed " + timeout.ToString() + " milliseconds.";
          break;

        default:
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Network ping of host '" + ipAddress + "' at IP address '" + ipAddress.ToString() + "' failed due for reason '" + reply.Status.ToString() + "'.";
          break;
      }
 
      return taskResult;
    }
  }
}
