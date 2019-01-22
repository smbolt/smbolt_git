using System;

namespace FieldingSystems.FieldVisor.API.DataContracts.Response
{
  public class ResponseStatus
  {
    public ResponseStatus()
    {
    }

    public ResponseStatus(bool success, string message)
    {
      Success = success;
      Message = message;
    }

    public bool Success {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }
  }
}
