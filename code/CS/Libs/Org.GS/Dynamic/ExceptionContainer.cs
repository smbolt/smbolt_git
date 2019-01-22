using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class ExceptionContainer
  {
    public ExceptionContainer Parent {
      get;
      set;
    }
    public int ExceptionLevel {
      get;
      set;
    }
    public int ExceptionLevels {
      get;
      set;
    }
    public Dictionary<string, object> Data {
      get;
      set;
    }
    public ExceptionContainer InnerException {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }
    public string StackTrace {
      get;
      set;
    }

    public ExceptionContainer(Exception ex)
    {
      ExceptionLevel = 1;
      Parent = null;
      LoadException(ex, ExceptionLevel, this);

      if (ExceptionLevel == 1)
        SetExceptionLevels();
    }

    public ExceptionContainer(Exception ex, int level, ExceptionContainer parent)
    {
      Parent = parent;
      ExceptionLevel = level;
      LoadException(ex, ++level, this);
    }

    private void LoadException(Exception ex, int level, ExceptionContainer parent)
    {
      this.Data = PopulateDataCollection(ex.Data);
      this.Message = ex.Message;
      this.StackTrace = ex.StackTrace;
      if (ex.InnerException != null)
        this.InnerException = new ExceptionContainer(ex.InnerException, ++level, parent);
      else
        this.InnerException = null;
    }

    private Dictionary<string, object> PopulateDataCollection(System.Collections.IDictionary data)
    {
      Dictionary<string, object> dict = new Dictionary<string, object>();

      foreach (System.Collections.DictionaryEntry de in data)
      {
        string key = de.Key.ToString();
        if (!dict.ContainsKey(key))
          dict.Add(key, de.Value);
      }

      return dict;
    }

    private void SetExceptionLevels()
    {
      int totalLevels = 1;

      ExceptionContainer ec = this;
      ExceptionContainer eci = this.InnerException;

      while (eci != null)
      {
        totalLevels++;
        ec = eci;
        eci = ec.InnerException;
      }

      ec = this;
      eci = this.InnerException;
      ec.ExceptionLevels = totalLevels;

      while (eci != null)
      {
        ec = eci;
        ec.ExceptionLevels = totalLevels;
        eci = ec.InnerException;
      }
    }

    public string GetMessages()
    {
      string messages = String.Empty;

      ExceptionContainer ec = this;
      ExceptionContainer eci = this.InnerException;

      while (eci != null)
      {
        ec = eci;
        eci = ec.InnerException;
      }

      messages = messages + "(Exception " + ec.ExceptionLevel.ToString() + " of " + ec.ExceptionLevels.ToString() + ") " + ec.Message + Environment.NewLine + Environment.NewLine;

      ExceptionContainer parent = ec.Parent;

      while (parent != null)
      {
        messages = messages + "(Exception " + parent.ExceptionLevel.ToString() + " of " + parent.ExceptionLevels.ToString() + ") " + parent.Message + Environment.NewLine + Environment.NewLine;
        parent = parent.Parent;
      }

      return messages.Trim();
    }

    public string GetInnermostStackTrace()
    {
      string stackTrace = String.Empty;

      ExceptionContainer ec = this;
      ExceptionContainer eci = this.InnerException;

      while (eci != null)
      {
        ec = eci;
        eci = ec.InnerException;
      }

      stackTrace = "(Deepest Stack Trace - Level " + ec.ExceptionLevel.ToString() + " of " + ec.ExceptionLevels.ToString() + ") " + ec.StackTrace;

      return stackTrace;
    }

    public string GetFullStackTrace()
    {
      string stackTrace = "Level 1: " + this.StackTrace;

      ExceptionContainer ec = this;
      ExceptionContainer eci = this.InnerException;

      while (eci != null)
      {
        ec = eci;
        stackTrace += Environment.NewLine + "Level " + ec.ExceptionLevel + ": " + ec.StackTrace;
        eci = ec.InnerException;
      }

      stackTrace = Environment.NewLine + stackTrace;

      return stackTrace;
    }
  }
}
