using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  public class TFApplication
  {
    private int _applicationID;
    public int ApplicationID
    {
      get {
        return _applicationID;
      }
      set {
        _applicationID = value;
      }
    }

    private string _applicationName;
    public string ApplicationName
    {
      get {
        return _applicationName;
      }
      set {
        _applicationName = value;
      }
    }

    private int _applicationTypeID;
    public int ApplicationTypeID
    {
      get {
        return _applicationTypeID;
      }
      set {
        _applicationTypeID = value;
      }
    }

    public TFApplication()
    {
      _applicationID = 0;
      _applicationName = String.Empty;
      _applicationTypeID = 0;
    }

  }
}
