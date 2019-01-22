using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Org.GS
{
  public static class IPDX
  {
    public static event IpdxEventHandler FireIpdxEvent;

    private static bool _isInitialized = IsInitialized();

    private static object TestProperty_LockObject = new object();
    private static string _testProperty;
    public static string TestProperty
    {
      get {
        return _testProperty;
      }
      set
      {
        if (Monitor.TryEnter(TestProperty_LockObject, 100))
        {
          try
          {
            _testProperty = value;
          }
          finally
          {
            Monitor.Exit(TestProperty_LockObject);
          }
        }

        _testProperty = value;
      }
    }

    public static void SendIpdxData(IpdxMessage message)
    {
      if (FireIpdxEvent == null)
        return;

      FireIpdxEvent(message);
    }

    private static bool IsInitialized()
    {
      _testProperty = String.Empty;

      return true;
    }
  }
}
