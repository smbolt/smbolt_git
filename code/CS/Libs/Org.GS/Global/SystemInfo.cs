using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Org.GS
{
  public class SystemInfo
  {
    [DllImport("kernel32.dll")]
    static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetProcAddress(IntPtr hModule,
                                        [MarshalAs(UnmanagedType.LPStr)]string procName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

    private int _processId = -1;
    public int ProcessId {
      get {
        return _processId;
      }
    }

    private string _domainName = String.Empty;
    public string DomainName {
      get {
        return _domainName;
      }
    }

    private string _computerName = String.Empty;
    public string ComputerName {
      get {
        return _computerName;
      }
    }

    private string _userName = String.Empty;
    public string UserName {
      get {
        return _userName;
      }
    }

    public string DomainAndComputer {
      get {
        return _domainName + @"\" + _computerName;
      }
    }

    public string DomainAndUser {
      get {
        return _domainName + @"\" + _userName;
      }
    }

    private string _windowsVersion = String.Empty;
    public string WindowsVersion {
      get {
        return _windowsVersion;
      }
    }

    private string _windowsBuild = String.Empty;
    public string WindowsBuild {
      get {
        return _windowsBuild;
      }
    }

    private string _windowsReleaseId = String.Empty;
    public string WindowsReleaseId {
      get {
        return _windowsReleaseId;
      }
    }

    private string _windowsOsVer = String.Empty;
    public string WindowsOsVer {
      get {
        return _windowsOsVer;
      }
    }

    private string _servicePack = String.Empty;
    public string ServicePack {
      get {
        return _servicePack;
      }
    }

    private string _systemRoot = String.Empty;
    public string SystemRoot {
      get {
        return _systemRoot;
      }
    }

    private bool _isNT = false;
    public bool IsNT {
      get {
        return _isNT;
      }
    }

    private bool _isCE = false;
    public bool IsCE {
      get {
        return _isCE;
      }
    }

    private bool _isWin9X = false;
    public bool IsWin9X {
      get {
        return _isWin9X;
      }
    }

    private bool _is64Bit = false;
    public bool Is64Bit {
      get {
        return _is64Bit;
      }
    }
    public bool Is32Bit {
      get {
        return !_is64Bit;
      }
    }

    private string _highestFramework = String.Empty;
    public string HighestFramework {
      get {
        return _highestFramework;
      }
    }
    public string HighestFrameworkShort {
      get {
        return Get_HighestFrameworkShort();
      }
    }

    private InstalledFrameworks _installedFrameworks = new InstalledFrameworks();
    public InstalledFrameworks InstalledFrameworks {
      get {
        return _installedFrameworks;
      }
    }

    private bool _exceptionOccurred = false;
    public bool ExceptionOccurred {
      get {
        return _exceptionOccurred;
      }
    }

    public Exception Exception {
      get;
      private set;
    }

    public string SystemInfoString {
      get {
        return Get_SystemInfoString();
      }
    }
    public string PlatformString {
      get {
        return Get_PlatformString();
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSVERSIONINFO
    {
      public int dwOSVersionInfoSize;
      public int dwMajorVersion;
      public int dwMinorVersion;
      public int dwBuildNumber;
      public int dwPlatformId;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
      public string szCSDVersion;
    }


    public SystemInfo()
    {
      Initialize();
    }

    private void Initialize()
    {
      try
      {
        var thisProcess = Process.GetCurrentProcess();
        _processId = thisProcess.Id;

        _systemRoot = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString();
        _domainName = Environment.UserDomainName;
        _computerName = Environment.MachineName;
        _userName = Environment.UserName;
        _servicePack = GetServicePack();

        _windowsReleaseId = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();
        _windowsOsVer = GetOsVer();

        if (_windowsOsVer.StartsWith("10."))
        {
          _windowsVersion = "Windows 10";
          _isNT = true;
        }
        else
        {


          switch (Environment.OSVersion.Platform)
          {
            case PlatformID.Win32NT:
              switch (Environment.OSVersion.Version.Major)
              {
                case 3:
                  _windowsVersion = "Windows NT 3.51";
                  _isNT = true;
                  break;
                case 4:
                  _windowsVersion = "Windows NT 4";
                  _isNT = true;
                  break;
                case 5:
                  switch (Environment.OSVersion.Version.Minor)
                  {
                    case 0:
                      _windowsVersion = "Windows 2000";
                      _isNT = true;
                      break;

                    case 1:
                      _windowsVersion = "Windows XP";
                      _isNT = true;
                      break;
                    case 2:
                      _windowsVersion = "Windows Server 2003";
                      _isNT = true;
                      break;
                    default:
                      _windowsVersion = "Unknown Windows Major Version 5." + Environment.OSVersion.Version.Minor.ToString();
                      _isNT = true;
                      break;

                  }
                  break;
                case 6:
                  switch (Environment.OSVersion.Version.Minor)
                  {
                    case 0:
                      _windowsVersion = "Windows Vista";
                      _isNT = true;
                      break;

                    case 1:
                      _windowsVersion = "Windows 7";
                      _isNT = true;
                      break;
                    case 2:
                      _windowsVersion = "Windows 8";
                      _isNT = true;
                      break;
                    case 3:
                      _windowsVersion = "Windows 8.1";
                      _isNT = true;
                      break;
                    default:
                      _windowsVersion = "Unknown Windows Major Version 6." + Environment.OSVersion.Version.Minor.ToString();
                      _isNT = true;
                      break;

                  }
                  break;
                case 10:
                  switch (Environment.OSVersion.Version.Minor)
                  {
                    case 0:
                      _windowsVersion = "Windows 10";
                      _isNT = true;
                      break;
                    case 1:
                      _windowsVersion = "Windows 10.1";
                      _isNT = true;
                      break;
                    case 2:
                      _windowsVersion = "Windows 10.2";
                      _isNT = true;
                      break;
                    case 3:
                      _windowsVersion = "Windows 10.3";
                      _isNT = true;
                      break;
                    case 4:
                      _windowsVersion = "Windows 10.4";
                      _isNT = true;
                      break;
                    default:
                      _windowsVersion = "Unknown Windows Major Version 10." + Environment.OSVersion.Version.Minor.ToString();
                      _isNT = true;
                      break;
                  }
                  break;
                default:
                  _windowsVersion = "Unknown Windows NT Version";
                  _isNT = true;
                  break;

              }
              break;

            case PlatformID.Win32S:
              _windowsVersion = "Windows 3.x - Version ";
              break;

            case PlatformID.Win32Windows:
              switch (Environment.OSVersion.Version.Minor)
              {
                case 0:
                  _windowsVersion = "Windows 95";
                  _isWin9X = true;
                  break;
                case 10:
                  _windowsVersion = "Windows 98";
                  _isWin9X = true;
                  break;
                case 90:
                  _windowsVersion = "Windows ME";
                  _isWin9X = true;
                  break;
                default:
                  _windowsVersion = "Unknown Windows 9x Version ";
                  _isWin9X = true;
                  break;
              }
              break;
            case PlatformID.WinCE:
              _windowsVersion = "Windows CE / PocketPC";
              _isCE = true;
              break;
            default:
              _windowsVersion = "Unknown Windows CE Version";
              _isCE = true;
              break;
          }
        }

        _windowsBuild = Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString() +
                        " - Build " + Environment.OSVersion.Version.Build.ToString();

        _is64Bit = Is64BitOperatingSystem();

        ProcessFrameworks();
      }
      catch(Exception ex)
      {
        Exception = ex;
        _exceptionOccurred = true;
      }
    }

    private static ManagementObject GetMngObj(string className)
    {
      var wmi = new ManagementClass(className);

      foreach (var o in wmi.GetInstances())
      {
        var mo = (ManagementObject)o;
        if (mo != null) return mo;
      }

      return null;
    }

    public static string GetOsVer()
    {
      try
      {
        ManagementObject mo = GetMngObj("Win32_OperatingSystem");

        if (null == mo)
          return String.Empty;

        return mo["Version"] as string;
      }
      catch (Exception e)
      {
        return string.Empty;
      }
    }

    private void ProcessFrameworks()
    {
      var fxs = GetFrameworkVersions();

      SortedList<string, string> sortedFx = new SortedList<string,string>();

      foreach(string fx in fxs)
      {
        var tokens = fx.ToTokenArray(Constants.SpaceDelimiter).ToList();
        foreach(string token in tokens)
        {
          string tokenNoDots = token.Replace(".", String.Empty);
          if (tokenNoDots.IsNumeric())
          {
            string versionKey = String.Empty;
            List<string> versionTokens = token.ToTokenArray(Constants.DotDelimiter).ToList();
            foreach(string versionToken in versionTokens)
            {
              string versionTokenFmt = Int32.Parse(versionToken).ToString("000000");
              if (versionKey.IsBlank())
                versionKey = versionTokenFmt;
              else
                versionKey += "." + versionTokenFmt;
            }

            string clientFull = String.Empty;

            if (fx.ToLower().Contains("client"))
            {
              versionKey += "(CLIENT)";
              clientFull = "-C";
            }

            if (fx.ToLower().Contains("full"))
            {
              versionKey += "(FULL)";
              clientFull = "-F";
            }

            if (!sortedFx.ContainsKey(versionKey))
              sortedFx.Add(versionKey, token + clientFull + "|" + fx);

            break;
          }
        }
      }

      if (sortedFx.Count > 0)
        _highestFramework = sortedFx.Last().Value;

      foreach(var fwk in sortedFx.Values)
        _installedFrameworks.Insert(0, fwk);
    }

    private string Get_HighestFrameworkShort()
    {
      if (_highestFramework == null)
        return String.Empty;

      if (_highestFramework.Contains("|"))
      {
        string[] tokens = _highestFramework.ToTokenArray(Constants.PipeDelimiter);
        return tokens[0];
      }

      return _highestFramework;
    }

    private string Get_SystemInfoString()
    {
      if (_exceptionOccurred)
        return "Exception occurred";

      string systemInfo = "SYSTEM INFORMATION" + g.crlf +
                          "  Windows Version   : " + _windowsVersion + g.crlf +
                          "  Windows OsVer     : " + _windowsOsVer + g.crlf +
                          "  Windows ReleaseId : " + _windowsReleaseId + g.crlf +
                          "  Windows Build     : " + _windowsBuild + g.crlf +
                          "  OS Type           : " + (_is64Bit ? "64 bit" : "32 bit") + g.crlf +
                          "  Service Pack      : " + _servicePack + g.crlf +
                          "  Highest FX        : " + _highestFramework + g.crlf +
                          "  SystemRoot        : " + _systemRoot + g.crlf +
                          "  Computer Name     : " + _computerName + g.crlf +
                          "  Domain            : " + _domainName + g.crlf +
                          "  User Name         : " + _userName + g.crlf +
                          "  Process ID        : " + _processId.ToString();

      return systemInfo;

    }

    private string Get_PlatformString()
    {
      string winVersion = Get_WinVersion();
      string winBits = _is64Bit ? "X64" : "X86";
      string sp = _servicePack.Replace("Service Pack ", "SP");
      string fx = this.HighestFrameworkShort;
      if (g.FxVersionSet.ContainsKey(fx))
        fx = g.FxVersionSet[fx].Version;

      string extra = "#";
      return winVersion + "^" + winBits + "^" + sp + "^" + fx + "^" + extra;
    }

    private string Get_WinVersion()
    {
      switch(_windowsVersion)
      {
        case "Windows 10.4":
          return "WIN10.4";
        case "Windows 10.3":
          return "WIN10.3";
        case "Windows 10.2":
          return "WIN10.2";
        case "Windows 10.1":
          return "WIN10.1";
        case "Windows 10.0":
          return "WIN10.0";
        case "Windows 8.1":
          return "WIN8.1";
        case "Windows 8":
          return "WIN8";
        case "Windows 7":
          return "WIN7";
        case "Windows Vista":
          return "WINVST";
        case "Windows XP":
          return "WINXP";
        case "Windows 2000":
          return "WIN2K";
        case "Windows NT 4":
          return "NT4";
        case "Windows NT 3.51":
          return "NT351";
        case "Windows ME":
          return "WINME";
        case "Windows 98":
          return "WIN98";
        case "Windows 95":
          return "WIN95";
        case "Windows 3.x - Version ":
          return "WIN3X";

        case "Windows Server 2003":
          return "SVR2K3";
        case "Windows CE / PocketPC":
          return "WINCE";
      }

      return "WIN?";
    }

    private bool Is64BitOperatingSystem()
    {
      if (IntPtr.Size == 8)  // 64-bit programs run only on Win64
      {
        return true;
      }
      else  // 32-bit programs run on both 32-bit and 64-bit Windows
      {
        // Detect whether the current process is a 32-bit process
        // running on a 64-bit system.
        bool flag;
        return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
                 IsWow64Process(GetCurrentProcess(), out flag)) && flag);
      }
    }

    /// <summary>
    /// The function determins whether a method exists in the export
    /// table of a certain module.
    /// </summary>
    /// <param name="moduleName">The name of the module</param>
    /// <param name="methodName">The name of the method</param>
    /// <returns>
    /// The function returns true if the method specified by methodName
    /// exists in the export table of the module specified by moduleName.
    /// </returns>
    private bool DoesWin32MethodExist(string moduleName, string methodName)
    {
      IntPtr moduleHandle = GetModuleHandle(moduleName);
      if (moduleHandle == IntPtr.Zero)
      {
        return false;
      }
      return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
    }


    /// <summary>
    /// The function determines whether the operating system of the
    /// current machine of any remote machine is a 64-bit operating
    /// system through Windows Management Instrumentation (WMI).
    /// </summary>
    /// <param name="machineName">
    /// The full computer name or IP address of the target machine. "."
    /// or null means the local machine.
    /// </param>
    /// <param name="domain">
    /// NTLM domain name. If the parameter is null, NTLM authentication
    /// will be used and the NTLM domain of the current user will be used.
    /// </param>
    /// <param name="userName">
    /// The user name to be used for the connection operation. If the
    /// user name is from a domain other than the current domain, the
    /// string may contain the domain name and user name, separated by a
    /// backslash: string 'username' = "DomainName\\UserName". If the
    /// parameter is null, the connection will use the currently logged-
    /// on user
    /// </param>
    /// <param name="password">
    /// The password for the specified user.
    /// </param>
    /// <returns>
    /// The function returns true if the operating system is 64-bit;
    /// otherwise, it returns false.
    /// </returns>
    /// <exception cref="System.Management.ManagementException">
    /// The ManagementException exception is generally thrown with the
    /// error code: System.Management.ManagementStatus.InvalidParameter.
    /// You need to check whether the parameters for ConnectionOptions
    /// (e.g. user name, password, domain) are set correctly.
    /// </exception>
    /// <exception cref="System.Runtime.InteropServices.COMException">
    /// A common error accompanied with the COMException is "The RPC
    /// server is unavailable. (Exception from HRESULT: 0x800706BA)".
    /// This is usually caused by the firewall on the target machine that
    /// blocks the WMI connection or some network problem.
    /// </exception>
    public bool Is64BitOperatingSystem(string machineName,
                                       string domain, string userName, string password)
    {
      ConnectionOptions options = null;
      if (!string.IsNullOrEmpty(userName))
      {
        // Build a ConnectionOptions object for the remote connection
        // if you plan to connect to the remote with a different user
        // name and password than the one you are currently using.
        options = new ConnectionOptions();
        options.Username = userName;
        options.Password = password;
        options.Authority = "NTLMDOMAIN:" + domain;
      }
      // Else the connection will use the currently logged-on user

      // Make a connection to the target computer.
      ManagementScope scope = new ManagementScope("\\\\" +
          (string.IsNullOrEmpty(machineName) ? "." : machineName) +
          "\\root\\cimv2", options);
      scope.Connect();

      // Query Win32_Processor.AddressWidth which dicates the current
      // operating mode of the processor (on a 32-bit OS, it would be
      // "32"; on a 64-bit OS, it would be "64").
      // Note: Win32_Processor.DataWidth indicates the capability of
      // the processor. On a 64-bit processor, it is "64".
      // Note: Win32_OperatingSystem.OSArchitecture tells the bitness
      // of OS too. On a 32-bit OS, it would be "32-bit". However, it
      // is only available on Windows Vista and newer OS.
      ObjectQuery query = new ObjectQuery(
        "SELECT AddressWidth FROM Win32_Processor");

      // Perform the query and get the result.
      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
      ManagementObjectCollection queryCollection = searcher.Get();
      foreach (ManagementObject queryObj in queryCollection)
      {
        if (queryObj["AddressWidth"].ToString() == "64")
        {
          return true;
        }
      }

      return false;
    }

    [DllImport("kernel32.Dll")] public static extern short GetVersionEx(ref OSVERSIONINFO o);
    static public string GetServicePack()
    {
      OSVERSIONINFO os = new OSVERSIONINFO();
      os.dwOSVersionInfoSize=Marshal.SizeOf(typeof(OSVERSIONINFO));
      GetVersionEx(ref os);
      if (os.szCSDVersion=="")
        return "NOSP";
      else
        return os.szCSDVersion;
    }

    private List<string> GetFrameworkVersions()
    {
      List<string> frameworks = new List<string>();
      string path = @"SOFTWARE\Microsoft\NET Framework Setup\NDP";

      try
      {
        var ndpKey = Registry.LocalMachine.OpenSubKey(path);
        foreach (var versionKeyName in ndpKey.GetSubKeyNames())
        {
          if (versionKeyName.ToLower().StartsWith("v"))
          {
            RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
            string name = (string)versionKey.GetValue("Version", "");
            string sp = versionKey.GetValue("SP", "").ToString();
            string install = versionKey.GetValue("Install", "").ToString();

            if (sp.IsNotBlank())
              sp = "SP" + sp;

            string framework = String.Empty;

            if (install.IsBlank())
              framework = versionKeyName + " " + name;
            else if (sp.IsNotBlank() && install == "1")
              framework = versionKeyName + " " + name + " " + sp;

            if (name.IsNotBlank())
            {
              if (framework.IsNotBlank())
                frameworks.Add(framework);
              continue;
            }

            foreach (string subKeyName in versionKey.GetSubKeyNames())
            {
              RegistryKey subKey = versionKey.OpenSubKey(subKeyName);

              name = (string)subKey.GetValue("Version", "");
              if (name.IsNotBlank())
                sp = subKey.GetValue("SP", "").ToString();

              if (sp.IsNotBlank())
                sp = "SP" + sp;

              install = subKey.GetValue("Install", "").ToString();

              string release = subKey.GetValue("Release", "").ToString();
              if (release.IsNotNumeric())
                release = String.Empty;
              else
              {
                int releaseNum = release.ToInt32();

                switch (releaseNum)
                {
                  case 461814:
                  case 461808:
                    release = " (4.7.2)";
                    break;

                  case 461310:
                  case 461308:
                    release = " (4.7.1)";
                    break;

                  case 460798:
                  case 460805:
                    release = " (4.7.0)";
                    break;

                  case 394806:
                  case 394802:
                    release = " (4.6.2)";
                    break;

                  case 394254:
                  case 394271:
                    release = " (4.6.1)";
                    break;

                  case 393295:
                  case 393297:
                    release = " (4.6)";
                    break;

                  case 379893:
                    release = " (4.5.2)";
                    break;

                  case 378758:
                  case 378675:
                    release = " (4.5.1)";
                    break;

                  case 378389:
                    release = " (4.5)";
                    break;

                  default:
                    release = "UNKNOWN(" + release + ")";
                    break;
                }
              }

              if (install.IsBlank())
                framework = versionKeyName + " " + name + release;
              else
              {
                if (sp.IsNotBlank() && install == "1")
                {
                  framework = versionKeyName + " " + subKeyName + " " + name + " " + sp + release;
                }
                else if (install == "1")
                {
                  framework = versionKeyName + " " + subKeyName + " " + name + release;
                }
              }

              if (framework.IsNotBlank())
              {
                frameworks.Add(framework);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (!_exceptionOccurred)
        {
          _exceptionOccurred = true;
          Exception = ex;
        }
      }

      return frameworks;
    }

    public static AssemblyMapSet GetAssemblyMapSet()
    {
      var set = new AssemblyMapSet();

      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      foreach( var asm in assemblies)
      {
        var orgMapAssemblyAttribute = (OrgAssemblyTag) asm.GetCustomAttributes(typeof(OrgAssemblyTag), false).ToList().FirstOrDefault();
        if (orgMapAssemblyAttribute != null)
        {
          var assemblyMap = new AssemblyMap();
          assemblyMap.AssemblyName = asm.FullName;
          assemblyMap.CodeBase = asm.CodeBase;
          assemblyMap.ImageRuntimeVersion = asm.ImageRuntimeVersion;
          assemblyMap.ManifestModule = asm.ManifestModule;
          assemblyMap.FullName = asm.FullName;
          assemblyMap.CustomAttributes = asm.CustomAttributes;
          set.Add(asm.FullName.ToString(), assemblyMap);
        }
      }

      return set;
    }

    public void Refresh()
    {
      Initialize();
    }
  }
}
