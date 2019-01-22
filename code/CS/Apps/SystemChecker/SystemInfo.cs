using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;
using System.Security.Principal;

namespace SystemChecker
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

    [DllImport("advapi32.dll", SetLastError = true)]
    static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);

    private int _processId = -1;
    public int ProcessId { get { return _processId; } }

    private string _domainName = String.Empty;
    public string DomainName { get { return _domainName; } }

    private string _computerName = String.Empty;
    public string ComputerName { get { return _computerName; } }

    private string _userName = String.Empty;
    public string UserName { get { return _userName; } }

    private string _windowsVersion = String.Empty;
    public string WindowsVersion { get { return _windowsVersion; } }

    private string _windowsBuild = String.Empty;
    public string WindowsBuild { get { return _windowsBuild; } }

    private string _systemRoot = String.Empty;
    public string SystemRoot { get { return _systemRoot; } }

    private bool _isUserLocalAdmin = false;
    public bool IsUserLocalAdmin { get { return _isUserLocalAdmin; } }

    private bool _isNT = false;
    public bool IsNT { get { return _isNT; } }

    private bool _isCE = false;
    public bool IsCE { get { return _isCE; } }

    private bool _isWin9X = false;
    public bool IsWin9X { get { return _isWin9X; } }

    private bool _is64Bit = false;
    public bool Is64Bit { get { return _is64Bit; } }
    public bool Is32Bit { get { return !_is64Bit; } }

    private string _highestFramework = String.Empty;
    public string HighestFramework { get { return _highestFramework; } }

    private List<string> _installedFrameworks = new List<string>();
    public List<string> InstalledFrameworks { get { return _installedFrameworks; } }

    private bool _exceptionOccurred = false;
    public bool ExceptionOccurred { get { return _exceptionOccurred; } }

    public Exception Exception { get; private set; }

    public string SystemInfoString { get { return Get_SystemInfoString(); } }


    public SystemInfo()
    {
      Initialize();
    }

    private void Initialize()
    {
      try
      {
        _isUserLocalAdmin = IsUserAdministrator();

        var thisProcess = Process.GetCurrentProcess();
        _processId = thisProcess.Id;

        _systemRoot = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString();
        _domainName = Environment.UserDomainName;
        _computerName = Environment.MachineName;
        _userName = Environment.UserName;

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
                  default:
                    _windowsVersion = "Unknown Windows Major Version 6." + Environment.OSVersion.Version.Minor.ToString();
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

        _windowsBuild = Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString() +
                " - Build " + Environment.OSVersion.Version.Build.ToString();

        _is64Bit = Is64BitOperatingSystem();

        var frameworkVersions = GetFrameworkVersions();
        frameworkVersions.Sort();

        this._installedFrameworks = frameworkVersions;
      }
      catch (Exception ex)
      {
        Exception = ex;
        _exceptionOccurred = true;
      }

    }

    private string Get_SystemInfoString()
    {
      if (_exceptionOccurred)
        return "Exception occurred";

      string systemInfo = "SYSTEM INFORMATION" + Common.crlf +
          "  Windows Version : " + _windowsVersion + Common.crlf +
          "  Windows Build   : " + _windowsBuild + Common.crlf +
          "  OS Type         : " + (_is64Bit ? "64 bit" : "32 bit") + Common.crlf +
          "  SystemRoot      : " + _systemRoot + Common.crlf +
          "  Computer Name   : " + _computerName + Common.crlf +
          "  Domain          : " + _domainName + Common.crlf +
          "  User Name       : " + _userName + Common.crlf +
          "  Process ID      : " + _processId.ToString();

      return systemInfo;

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
            else
              if (sp.IsNotBlank() && install == "1")
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
                if (releaseNum >= 393273)
                  release = " (4.6 RC or later)";
                else if (releaseNum >= 379893)
                  release = " (4.5.2)";
                else if (releaseNum >= 378675)
                  release = " (4.5.1)";
                else if (releaseNum >= 378389)
                  release = " (4.5)";
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

    private bool IsUserAdministrator()
    {
      var identity = WindowsIdentity.GetCurrent();
      if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity");
      var principal = new WindowsPrincipal(identity);

      // Check if this user has the Administrator role. If they do, return immediately.
      // If UAC is on, and the process is not elevated, then this will actually return false.
      if (principal.IsInRole(WindowsBuiltInRole.Administrator)) return true;

      // If we're not running in Vista onwards, we don't have to worry about checking for UAC.
      if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6)
      {
        // Operating system does not support UAC; skipping elevation check.
        return false;
      }

      int tokenInfLength = Marshal.SizeOf(typeof(int));
      IntPtr tokenInformation = Marshal.AllocHGlobal(tokenInfLength);

      try
      {
        var token = identity.Token;
        var result = GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);

        if (!result)
        {
          var exception = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
          throw new InvalidOperationException("Couldn't get token information", exception);
        }

        var elevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation);

        switch (elevationType)
        {
          case TokenElevationType.TokenElevationTypeDefault:
            // TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
            return false;
          case TokenElevationType.TokenElevationTypeFull:
            // TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
            return true;
          case TokenElevationType.TokenElevationTypeLimited:
            // TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
            return true;
          default:
            // Unknown token elevation type.
            return false;
        }
      }
      finally
      {
        if (tokenInformation != IntPtr.Zero) Marshal.FreeHGlobal(tokenInformation);
      }
    }

  }
}
