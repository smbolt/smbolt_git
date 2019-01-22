using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Org.GS.Logging
{
  public class LogException
  {
    public string Message {
      get;
      set;
    }
    public string Source {
      get;
      set;
    }
    public string StackTrace {
      get;
      set;
    }
    public string TargetSite {
      get;
      set;
    }
    public LogException InnerLogException {
      get;
      set;
    }

    public LogException()
    {
      Message = String.Empty;
      Source = String.Empty;
      StackTrace = String.Empty;
      TargetSite = String.Empty;
      InnerLogException = null;
    }

    public void LoadFromXml(XElement ex)
    {

    }

    public XElement GetAsXElement()
    {
      XElement lex = new XElement("LogException");
      lex.Add(new XElement("Message", Message));
      lex.Add(new XElement("Source", Source));
      lex.Add(new XElement("StackTrace", StackTrace));
      lex.Add(new XElement("TargetSite", TargetSite));
      if (InnerLogException != null)
        lex.Add(new XElement("InnerLogException", InnerLogException.GetAsXElement()));

      return lex;
    }
  }
}
