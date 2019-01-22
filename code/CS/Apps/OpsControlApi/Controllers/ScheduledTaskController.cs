using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using Org.OpsControlApi.Services;
using Org.WebApi;
using Org.WebApi.ApiManagement;
using Org.GS;

namespace OpsControlApi.Controllers
{
  public class ScheduledTaskController : ControllerBase
  {
    [HttpGet]
    [Route("api/tasks")]
    public IHttpActionResult Get()
    {
      base.QS = Request.GetQueryNameValuePairs();
      int page = base.QS.GetValue("page").ToInt32();
      int perPage = base.QS.GetValue("perPage").ToInt32();
      string nameSearch = base.QS.GetValue("nameSearch");
      string legalNameSearch = base.QS.GetValue("legalNameSearch");
      string sort = base.QS.GetValue("sort");

      try
      {
        using (var svc = new ScheduledTaskService(base.WebContext, this))
        {
          return svc.GetScheduledTasks(--base.Page * base.PerPage, base.PerPage, base.Sort).ToApiResult(this); 
        }
      }
      catch (Exception ex)
      {
        return this.ToExceptionApiResult("An exception occurred attempting to retrieve operators (page=" + page.ToString() + ", perPage=" + perPage.ToString() +
          " in OperatorController.Get", "4999", ex);
      }
    }

    public IHttpActionResult GetResponseMessage(HttpResponseMessage responseMessage)
    {
      return ResponseMessage(responseMessage);
    }
  }
}