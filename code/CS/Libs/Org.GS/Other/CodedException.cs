using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Org.GS
{
  [Serializable]
  public class CodedException : System.Exception
  {
    private string _errorCode;
    public string ErrorCode
    {
      get { return _errorCode; }
      set { _errorCode = value; }
    }

    private string _exceptionDetail;
    public string ExceptionDetail
    {
      get { return _exceptionDetail; }
      set { _exceptionDetail = value; }
    }

    public CodedException(string message)
      : base(message)
    {
      this._errorCode = String.Empty;
      this._exceptionDetail = String.Empty;
    }

    public CodedException(string message, string errorCode)
      : base(message)
    {
      this._errorCode = errorCode;
      this._exceptionDetail = String.Empty;
    }

    public CodedException(string message, string errorCode, string exceptionDetail)
      : base(message)
    {
      this._errorCode = errorCode;
      this._exceptionDetail = exceptionDetail;
    }

    protected CodedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info != null)
      {
        this._errorCode = info.GetValue("ErrorCode", typeof(System.String)).ToString();
        this._exceptionDetail = info.GetValue("ExceptionDetail", typeof(System.String)).ToString();
      }
    }

    public override void GetObjectData(SerializationInfo info,
      StreamingContext context)
    {
      base.GetObjectData(info, context);

      if (info != null)
      {
        info.AddValue("ErrorCode", this._errorCode);
        info.AddValue("ExceptionDetail", this._exceptionDetail);
      }
    }
  }
}
