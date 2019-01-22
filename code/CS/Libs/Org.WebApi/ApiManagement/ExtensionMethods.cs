using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Org.GS;

namespace Org.WebApi.ApiManagement
{
  public static class ExtensionMethods
  {
    public static IHttpActionResult ToApiResult(this TaskResult taskResult, ControllerBase controller)
    {
      var apiResult = new ApiResult();
      apiResult.ApiStatus = taskResult.TaskResultStatus.ToString();
      apiResult.Code = taskResult.Code.ToString();
      apiResult.Token = null;
      apiResult.UserName = controller.UserName;

      switch (taskResult.TaskResultStatus)
      {
        case TaskResultStatus.Success:
          apiResult.ResponseData = taskResult.Object;
          apiResult.Message = taskResult.Message;
          apiResult.Data = taskResult.Data;
          apiResult.PagingControl.IsPaging = taskResult.IsPaging;
          apiResult.PagingControl.TotalEntityCount = taskResult.TotalEntityCount;
          apiResult.PagingControl.SubsetEntityCount = taskResult.SubsetEntityCount;
          apiResult.UserId = 2; //taskResult.UserId;
          return new OkNegotiatedContentResult<ApiResult>(apiResult, controller);

        case TaskResultStatus.NotFound:
          apiResult.Message = taskResult.Message;
          apiResult.LongMessage = taskResult.FullErrorDetail;
          return new OkNegotiatedContentResult<ApiResult>(apiResult, controller);

        case TaskResultStatus.AlreadyExists:
          apiResult.Message = taskResult.Message;
          apiResult.LongMessage = taskResult.FullErrorDetail;
          apiResult.ResponseData = taskResult.Field;
          return new OkNegotiatedContentResult<ApiResult>(apiResult, controller);

        case TaskResultStatus.Failed:
          apiResult.Message = taskResult.Message;
          apiResult.LongMessage = taskResult.FullErrorDetail;
          return new OkNegotiatedContentResult<ApiResult>(apiResult, controller);

        default:
          apiResult.Message = taskResult.Message;;
          if (taskResult.Exception != null)
          {
            apiResult.LongMessage = taskResult.Exception.ToReport();
          }
          else
          {
            if (taskResult.FullErrorDetail.IsNotBlank())
              apiResult.LongMessage = taskResult.FullErrorDetail;
          }
          apiResult.ResponseData = null;
          HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
          responseMessage.Content = new ObjectContent<ApiResult>(apiResult, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
          var response = GetResponseMessageFromController(controller, responseMessage);
          if (response != null)
            return response;
          throw new Exception("GetResponseMessageFromController using controller type '" + controller.GetType().Name + "' returned null. It is possible that " +
                              "a new controller needs to be implemented in the method.");
      }
    }

    public static IHttpActionResult ToExceptionApiResult(this ControllerBase controller, string message, string code, Exception ex)
    {
      var apiResult = new ApiResult();
      apiResult.ApiStatus = ApiStatus.Error.ToString();
      apiResult.Message = message;
      apiResult.Code = code;
      apiResult.ResponseData = ex.ToApiExceptionReport();
      HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
      responseMessage.Content = new ObjectContent<ApiResult>(apiResult, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
      var response = GetResponseMessageFromController(controller, responseMessage);
      if (response != null)
        return response;
      throw new Exception("GetResponseMessageFromController using controller type '" + controller.GetType().Name + "' returned null. It is possible that " +
                          "a new controller needs to be implemented in the method.");
    }

    public static string GetValue(this IEnumerable<KeyValuePair<string, string>> qs, string key)
    {
      return qs.SingleOrDefault(x => x.Key == key).Value;
    }

    public static string ToApiExceptionReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + g.crlf +
                  "Message: " + ex.Message + g.crlf +
                  "StackTrace:" + ex.StackTrace + g.crlf);

        if (ex.GetType().ToString() == "System.Web.Http.HttpResponseException")
        {
          sb.Append("HTTP_ERROR: " + ((System.Web.Http.HttpResponseException)ex).Response.ToString() + g.crlf);
        }

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(g.crlf);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    public static IHttpActionResult GetResponseMessageFromController(ControllerBase controller, HttpResponseMessage responseMessage)
    {
      var mi = controller.GetType().GetMethod("GetResponseMethod");
      if (mi == null)
        return null;

      var parms = new object[] { responseMessage };
      return mi.Invoke(controller, parms) as IHttpActionResult;
    }
  }
}