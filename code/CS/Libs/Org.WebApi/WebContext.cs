using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Principal;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using Org.GS.Security;
using Org.GS;

namespace Org.WebApi
{
  public class WebContext
  {
    private RequestMessage _requestMessage;
    public RequestMessage RequestMessage { get { return _requestMessage; } }

    private ClientHash _clientHash;
    public ClientHash ClientHash { get { return _clientHash; } }

    private HttpControllerContext _httpControllerContext;
    public HttpControllerContext HttpControllerContext
    {
      get { return _httpControllerContext; }
      private set { _httpControllerContext = value; }
    }

    public HttpContextWrapper HttpContext 
    { 
      get { return Get_HttpContext(); }
    }

    public HttpServerUtilityWrapper Server
    {
      get { return Get_Server(); }
    }

    public HttpRequestMessage Request
    {
      get { return Get_Request(); }
    }

    public HttpRequestContext RequestContext
    {
      get { return Get_RequestContext(); }
    }

    public WindowsPrincipal WindowsPrincipal { get { return Get_WindowsPrincipal(); } }
    public WebClientSecurity WebClientSecurity { get; set; }
    public string ServerName { get { return Get_ServerName(); } }
    public string Method { get { return Get_Method(); } }
    public Uri Uri { get { return Get_Uri(); } }
    
    //constructor
    public WebContext(HttpControllerContext controllerContext, System.Net.Http.HttpRequestMessage httpRequestMessage)
    {
      this.WebClientSecurity = new WebClientSecurity();
      _requestMessage = new RequestMessage(httpRequestMessage); 
      _httpControllerContext = controllerContext;
      _clientHash = GetClientHash();
    }

    private HttpContextWrapper Get_HttpContext()
    {
      var httpContext = this.HttpControllerContext.Request.Properties["MS_HttpContext"];
      if (httpContext == null)
        return null;
      return (HttpContextWrapper)httpContext;
    }

    private HttpServerUtilityWrapper Get_Server()
    {
      if (this.HttpContext == null)
        return null; 
      return (System.Web.HttpServerUtilityWrapper)((System.Web.HttpContextWrapper)this.HttpContext).Server;   
    }

    private HttpRequestMessage Get_Request()
    {
      if (this.HttpControllerContext == null)
        return null;

      if (this.HttpControllerContext.Request == null)
        return null;

      return this.HttpControllerContext.Request;
    }

    private HttpRequestContext Get_RequestContext()
    {
      if (this.Request == null)
        return null;

      var requestContext = this.Request.Properties["MS_RequestContext"];
      if (requestContext == null)
        return null;
      return (HttpRequestContext) requestContext; 
    }

    private WindowsPrincipal Get_WindowsPrincipal()
    {
      if (this.RequestContext == null)
        return null;

      return (WindowsPrincipal)this.RequestContext.Principal;
    }

    private string Get_ServerName()
    {
      if (this.Server == null)
        return String.Empty;
      return this.Server.MachineName; 
    }

    private string Get_Method()
    {
      if (this.Request == null)
        return String.Empty;

      return this.Request.Method.ToString();    
    }

    private Uri Get_Uri()
    {
      if (this.Request == null)
        return null;

      return this.Request.RequestUri;
    }

    private ClientHash GetClientHash()
    {
      var clientHash = new ClientHash();

      try
      {
        if (Request == null || Request.Headers == null)
          return clientHash;

        if (!Request.Headers.Contains("clientHash"))
          return clientHash;

        IEnumerable<string> headerValues;
        if (!Request.Headers.TryGetValues("clientHash", out headerValues))
          return clientHash;

        string clientHashValue = headerValues.FirstOrDefault();

        if (clientHashValue == null)
          return clientHash;

        if (clientHashValue.IsBlank())
          return clientHash;

        byte[] hashBytes = Convert.FromBase64String(clientHashValue);
        string hashValue = System.Text.Encoding.UTF8.GetString(hashBytes);
        Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(hashValue);
        clientHash = jObject.ToObject<ClientHash>();
        clientHash.IsValid = true;
        clientHash.HashString = clientHashValue;

        return clientHash;
      }

      catch (Exception ex)
      {
        clientHash.IsValid = false;
        clientHash.ExceptionOccurred = true;
        clientHash.Exception = ex;
        return clientHash;
      }
    }

  }
}
