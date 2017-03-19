using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Org.GS
{
  [Serializable]
  public class WebApiException : Exception
  {
    public int Code { get; set; }

    public WebApiException(int code, string message)
      : base(message)
    {
      this.Code = code;
    }

    protected WebApiException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info != null)
      {
        this.Code = Convert.ToInt32(info.GetValue("ErrorType", typeof(int)));
      }
    }

    public override void GetObjectData(SerializationInfo info,
      StreamingContext context)
    {
      base.GetObjectData(info, context);

      if (info != null)
      {
        info.AddValue("Code", this.Code);
      }
    }
  }
}
