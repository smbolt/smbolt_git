using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;
using Org.GS.Security;
using Org.GS.Logging;
using Org.GS;

namespace Org.WebApi
{
  public class ControllerBase : ApiController
  {
    private Logger _logger;
    public Logger Logger {
      get {
        return _logger;
      }
    }

    protected WebContext WebContext {
      get;
      private set;
    }
    protected UserSession UserSession {
      get;
      private set;
    }
    public string UserName {
      get {
        return Get_UserName();
      }
    }
    public string UserNameShort {
      get {
        return Get_UserNameShort();
      }
    }

    public IEnumerable<KeyValuePair<string, string>> QS {
      get;
      set;
    }

    public int Page {
      get;
      set;
    }
    public int PerPage {
      get;
      set;
    }
    public string Sort {
      get;
      set;
    }

    private string _sessionId;
    public string SessionId
    {
      get {
        return _sessionId;
      }
      set {
        _sessionId = value;
      }
    }

    public int OrgId {
      get;
      set;
    }
    public int AccountId {
      get;
      set;
    }

    public ControllerBase()
    {
      this.Page = 0;
      this.PerPage = 0;
      this.Sort = String.Empty;
      _logger = new Logger();

      this.UserSession = null;
    }

    protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
    {
      base.Initialize(controllerContext);
      this.WebContext = new WebContext(controllerContext, this.Request);

      bool newUserSession = false;
      string userName = this.WebContext.WindowsPrincipal.Identity.Name;
      string emulateUser = g.GetCI("EmulateUser");
      if (emulateUser.IsNotBlank())
        userName = emulateUser;

      var functionControl = g.AppConfig.ProgramConfigSet[g.AppInfo.ConfigName].ProgramFunctionControl;
      var securityModel = g.GetCI("SecurityModel").ToEnum<SecurityModel>(SecurityModel.ActiveDirectory);

      if (this.UserSession == null)
      {
        newUserSession = true;
        this.UserSession = new UserSession();
        this.UserSession.WindowsPrincipal = this.WebContext.WindowsPrincipal;
        string identityName = this.WebContext.WindowsPrincipal.Identity.Name;
        if (emulateUser.IsNotBlank())
          identityName = emulateUser;
        this.UserSession.ConfigSecurity = g.AppConfig.GetConfigSecurityForUser(identityName);
        this.UserSession.ConfigSecurity.Initialize(functionControl, userName, securityModel);
      }

      this.WebContext.WebClientSecurity.LoggedInUserGroups = this.UserSession.ConfigSecurity.LoggedInUserGroups.GroupNameList;
      this.WebContext.WebClientSecurity.LoggedInUserFunctions = this.UserSession.ConfigSecurity.LoggedInUserAllowedFunctions;
      this.WebContext.WebClientSecurity.DomainUserName = this.UserSession.DomainUserName;
      if (emulateUser.IsNotBlank())
        this.WebContext.WebClientSecurity.DomainUserName = emulateUser;

      g.LogToMemory(this.WebContext.Method + " " + this.WebContext.Uri.OriginalString + " (" + this.WebContext.WebClientSecurity.DomainUserName + ")" +
                    " NewUserSession=" + newUserSession.ToString());


      // use this code to run on different servers...

      int requestPort = this.WebContext.Uri.Port;
      string serverName = this.WebContext.ServerName;

      string connectionStringName = System.Web.Configuration.WebConfigurationManager.AppSettings["connectionStringName"];
      //connectionStringName = Org.DB.ConnStringManager.Translate(connectionStringName);
      g.ConnectionStringName = connectionStringName.IsNotBlank() ? connectionStringName : "GPMasterEntities";

    }

    private bool AllowFunctionForUser(string programFunction)
    {
      switch (this.UserSession.ConfigSecurity.SecurityModel)
      {
        case SecurityModel.Config:
          return this.UserSession.ConfigSecurity.AllowFunctionForUser(programFunction);

        case SecurityModel.ActiveDirectory:
          var groupsAllowedFunction = this.UserSession.ConfigSecurity.GetGroupsAllowedFunction(programFunction);
          foreach (var groupAllowedFunction in groupsAllowedFunction)
          {
            if (this.UserSession.WindowsPrincipal.IsInRole(groupAllowedFunction))
              return true;
          }
          return false;

        default:
          return true;
      }
    }

    private string Get_UserName()
    {
      if (this.UserSession == null)
        return String.Empty;
      return this.UserSession.DomainUserName;
    }

    private string Get_UserNameShort()
    {
      if (this.UserSession == null)
        return String.Empty;

      string userName = this.UserSession.DomainUserName;
      List<string> nameTokens = userName.ToTokenArray(Constants.BSlashDelimiter).ToList();

      if (nameTokens.Count == 0)
        return String.Empty;

      return nameTokens.Last();
    }
  }
}