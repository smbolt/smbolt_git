using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Org.WebApi.ApiManagement
{
  public enum ApiStatus
  {
    Success,
    NotFound,
    UnexpectedResult,
    Warning,
    Error,
    NotSet
  }

  public class ApiResult
  {
    public string ApiStatus { get; set; }
    public string Code { get; set; }
    public string Data { get; set; }
    public string Message { get; set; }
    public string LongMessage { get; set; }
    public string Token { get; set; }
    public PagingControl PagingControl { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public object ResponseData { get; set; }

    public ApiResult()
    {
      Initialize();
    }

    public ApiResult(ApiStatus apiStatus)
    {
      Initialize();
      this.ApiStatus = SetStatus(apiStatus);
    }

    public ApiResult(ApiStatus apiStatus, string message)
    {
      Initialize();
      this.ApiStatus = SetStatus(apiStatus);
      this.Message = message;
    }

    public ApiResult(ApiStatus apiStatus, int code, string message)
    {
      Initialize();
      this.ApiStatus = SetStatus(apiStatus);
      this.Code = code.ToString("0000");
      this.Message = message;
    }

    private void Initialize()
    {
      this.ApiStatus = "NotSet";
      this.Code = "0000";
      this.Data = String.Empty;
      this.Message = String.Empty;
      this.LongMessage = String.Empty;
      this.Token = String.Empty;
      this.PagingControl = new PagingControl();
      this.UserId = -1;
      this.UserName = String.Empty;
      this.ResponseData = null;
    }

    private string SetStatus(ApiStatus apiStatus)
    {
      return apiStatus.ToString("G").ToLower();
    }
  }
}