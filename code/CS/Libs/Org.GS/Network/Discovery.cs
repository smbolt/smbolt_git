using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.IO;

namespace Org.GS.Network
{   
    
  public enum ResourceScope
  {
    RESOURCE_CONNECTED = 1,
    RESOURCE_GLOBALNET,
    RESOURCE_REMEMBERED,
    RESOURCE_RECENT,
    RESOURCE_CONTEXT
  };

  public enum ResourceType
  {
    RESOURCETYPE_ANY,
    RESOURCETYPE_DISK,
    RESOURCETYPE_PRINT,
    RESOURCETYPE_RESERVED
  };

  public enum ResourceUsage
  {
    RESOURCEUSAGE_CONNECTABLE = 0x00000001,
    RESOURCEUSAGE_CONTAINER = 0x00000002,
    RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
    RESOURCEUSAGE_SIBLING = 0x00000008,
    RESOURCEUSAGE_ATTACHED = 0x00000010,
    RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
  };

  public enum ResourceDisplayType
  {
    RESOURCEDISPLAYTYPE_GENERIC,
    RESOURCEDISPLAYTYPE_DOMAIN,
    RESOURCEDISPLAYTYPE_SERVER,
    RESOURCEDISPLAYTYPE_SHARE,
    RESOURCEDISPLAYTYPE_FILE,
    RESOURCEDISPLAYTYPE_GROUP,
    RESOURCEDISPLAYTYPE_NETWORK,
    RESOURCEDISPLAYTYPE_ROOT,
    RESOURCEDISPLAYTYPE_SHAREADMIN,
    RESOURCEDISPLAYTYPE_DIRECTORY,
    RESOURCEDISPLAYTYPE_TREE,
    RESOURCEDISPLAYTYPE_NDSCONTAINER
  };

  public class Discovery
  {
    [DllImport("Mpr.dll", EntryPoint = "WNetOpenEnumA", CallingConvention = CallingConvention.Winapi)]
    private static extern ErrorCodes WNetOpenEnum(ResourceScope dwScope, ResourceType dwType, ResourceUsage dwUsage, NETRESOURCE p, out IntPtr lphEnum);

    [DllImport("Mpr.dll", EntryPoint = "WNetCloseEnum", CallingConvention = CallingConvention.Winapi)]
    private static extern ErrorCodes WNetCloseEnum(IntPtr hEnum);

    [DllImport("Mpr.dll", EntryPoint = "WNetEnumResourceA", CallingConvention = CallingConvention.Winapi)]
    private static extern ErrorCodes WNetEnumResource(IntPtr hEnum, ref uint lpcCount, IntPtr buffer, ref uint lpBufferSize);

    public event Action<string> NotifyServerFound;

    enum ErrorCodes
    {
      NO_ERROR = 0,
      ERROR_NO_MORE_ITEMS = 259
    };

    [StructLayout(LayoutKind.Sequential)]
    private class NETRESOURCE
    {
      public ResourceScope dwScope = 0;
      public ResourceType dwType = 0;
      public ResourceDisplayType dwDisplayType = 0;
      public ResourceUsage dwUsage = 0;
      public string lpLocalName = null;
      public string lpRemoteName = null;
      public string lpComment = null;
      public string lpProvider = null;
    };

    private bool _cancel = false;
    public bool Cancel
    {
      get { return _cancel; }
      set { _cancel = value; }
    }

    private ArrayList _aData = new ArrayList();

    public int Count
    {
      get { return _aData.Count; }
    }

    private int depth = 0;

    public Discovery()
    {
    }

    public ArrayList DiscoverNetwork(ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType)
	  {
		  NETRESOURCE pRsrc = new NETRESOURCE();
		  EnumerateServers(pRsrc, scope, type, usage, displayType);
        return _aData;
	  }

    private void EnumerateServers(NETRESOURCE pRsrc, ResourceScope scope, ResourceType type, ResourceUsage usage, ResourceDisplayType displayType)
    {
      uint bufferSize = 16384;
      IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
      IntPtr handle = IntPtr.Zero;
      ErrorCodes result;
      uint cEntries = 1;

      depth++;

      if (_cancel)
        return;

      result = WNetOpenEnum(scope, type, usage, pRsrc, out handle);

      if (result == ErrorCodes.NO_ERROR)
      {
        do
        {
          if (_cancel)
            return;

          result = WNetEnumResource(handle, ref cEntries, buffer, ref	bufferSize);

          if (result == ErrorCodes.NO_ERROR)
          {
            Marshal.PtrToStructure(buffer, pRsrc);

            string dispType = String.Empty;
            switch (pRsrc.dwDisplayType)
            {
              case ResourceDisplayType.RESOURCEDISPLAYTYPE_DOMAIN:
                dispType = "Domain";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_DIRECTORY:
                dispType = "Directory";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_NDSCONTAINER:
                dispType = "NDSContainer";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_GROUP:
                dispType = "Group";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK:
                dispType = "Network";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_ROOT:
                dispType = "Root";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE:
                dispType = "Share";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_TREE:
                dispType = "Tree";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_SHAREADMIN:
                dispType = "ShareAdmin";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_GENERIC:
                dispType = "Generic";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER:
                dispType = "Server";
                break;

              case ResourceDisplayType.RESOURCEDISPLAYTYPE_FILE:
                dispType = "File";
                break;
            }

            string name = pRsrc.lpRemoteName.Trim();
            if (name.Length > 2)
            {
              if (name.Substring(0, 2) == @"\\")
                name = name.Substring(2);
            }

            if (NotifyServerFound != null)
              NotifyServerFound("Discovering: Type=" + dispType + "  Name=" + name);

            if (pRsrc.dwDisplayType == ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER)
            {
              _aData.Add(name);
            }

            if ((pRsrc.dwUsage & ResourceUsage.RESOURCEUSAGE_CONTAINER) == ResourceUsage.RESOURCEUSAGE_CONTAINER)
            {
              EnumerateServers(pRsrc, scope, type, usage, displayType);
              if (_cancel)
                return;
              depth--;
            }
          }
          else if (result != ErrorCodes.ERROR_NO_MORE_ITEMS)
            break;

        } while (result != ErrorCodes.ERROR_NO_MORE_ITEMS);

        WNetCloseEnum(handle);
      }

      Marshal.FreeHGlobal((IntPtr)buffer);
    }

    public IEnumerator GetEnumerator()
    {
      return _aData.GetEnumerator();
    }
  }
}