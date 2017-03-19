using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public class CxException : Exception
  {
    public static Dictionary<int, string> _cxMessages;

    public int _code;
		public int Code { get { return _code; } }
    private string _cxMessage;
    private string _message;
    private object[] _exParms;
		public object[] ExParms { get { return _exParms == null ? new object[0] : _exParms; } }
    public string Report { get { return Get_Report();  } }

    private Cmd _cmd;
    private Cmdx _cmdx;

    public override string Message
    {
      get
      {
        { return GetCxMessage(_code); }
      }
    }

    public CxException()
      :base()
    {
      EnsureMessagesLoaded();
    }

    public CxException(int code)
      : base()
    {
      EnsureMessagesLoaded();
      _code = code;
      _cxMessage = GetCxMessage(_code); 
    }

    public CxException(int code, object[] exParms = null)
      : base()
    {
      EnsureMessagesLoaded();
      _code = code;
      _cxMessage = GetCxMessage(_code);
      _exParms = exParms;
    }

    private static string GetCxMessage(int code)
    {
      EnsureMessagesLoaded();

      if (_cxMessages == null || !_cxMessages.ContainsKey(code))
        return "CX9999 - Message is not defined.";

      return _cxMessages[code];
    }

    private static void EnsureMessagesLoaded()
    {
      if (_cxMessages != null)
        return;

      _cxMessages = new Dictionary<int, string>();
      foreach (var kvp in CxCode.CxCodesAndMessages)
      {
        if (!_cxMessages.ContainsKey(kvp.Key))
          _cxMessages.Add(kvp.Key, kvp.Value); 
      }
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();

      sb.Append(this.ToReport());

      string report = sb.ToString();
      return report;
    }
  }
}
