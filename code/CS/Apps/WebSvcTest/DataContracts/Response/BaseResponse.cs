using System;
using System.Collections.Generic;

namespace FieldingSystems.FieldVisor.API.DataContracts.Response
{
  public abstract class BaseResponse
  {
    public ResponseStatus Status {
      get;
      set;
    }
  }
}