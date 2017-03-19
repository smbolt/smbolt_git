using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Org.WebApi.ApiManagement
{
  public class ApiInput
  {
    public string ApiAction { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime SentDateTime { get; set; }
    public int Code { get; set; }
    public string Command { get; set; }
    public object Value { get; set; }

    public ApiInput()
    {
      this.ApiAction = String.Empty;
      this.UserId = -1;
      this.UserName = String.Empty;
      this.SentDateTime = DateTime.Now;
      this.Code = -1;
      this.Command = String.Empty;
      this.Value = null;
    }
  }
}