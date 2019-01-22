using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Notify
{
  public class NotificationsManagerOptions
  {
    private int optionsLength = 2;

    private bool _throwExceptions;
    public bool ThrowExceptions {
      get {
        return _throwExceptions;
      }
    }

    private bool _logExceptions;
    public bool LogExceptions {
      get {
        return _logExceptions;
      }
    }

    public string OptionsAsString {
      get {
        return Get_OptionsAsString();
      }
    }

    public NotificationsManagerOptions()
    {
      _throwExceptions = false;
      _logExceptions = false;
    }

    public NotificationsManagerOptions(string options)
    {
      if (options == null)
        options = String.Empty.PadToLength(optionsLength, '0');

      if (options.Length < optionsLength)
        options = options.PadToLength(optionsLength, '0');

      _throwExceptions = options[0] != '0';
      _logExceptions = options[1] != '0';
    }

    private string Get_OptionsAsString()
    {
      return (_throwExceptions ? "1" : "0") +
             (_logExceptions ? "1" : "0");
    }

  }
}
