using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Org.GS;
using Org.GS.Security;

namespace Org.WebApi.Security
{
  public class SecurityManager
  {
    public static object Login_LockObject = new object();
    public static TaskResult Login(LoginModel loginModel)
    {
      if (Monitor.TryEnter(Login_LockObject, 5000))
      {
        var taskResult = new TaskResult("Login");
        try
        {
          //DB.User user = null;
          //using (var db = new DB.GPMasterEntities(g.ConnectionStringName))
          //{
          //  //user = db.Users.Where(u => u.ID == loginModel.ID).FirstOrDefault();
          //}

          //if (user == null)
          //{
          //  taskResult.TaskResultStatus = TaskResultStatus.Failed;
          //  taskResult.Message = "User '" + loginModel.UserName + "' does not exist.";
          //  taskResult.Code = "40221";
          //  taskResult.EndDateTime = DateTime.Now;
          //  return taskResult;
          //}

          //if (user.StatusId != 1)
          //{
          //  taskResult.TaskResultStatus = TaskResultStatus.Failed;
          //  taskResult.Message = "User '" + loginModel.UserName + "' is not in Active status.";
          //  taskResult.Code = "4023";
          //  taskResult.EndDateTime = DateTime.Now;
          //  return taskResult;
          //}

          //if (user.Password != loginModel.Password)
          //{
          //  taskResult.TaskResultStatus = TaskResultStatus.Failed;
          //  taskResult.Message = "User '" + loginModel.UserName + "' login failed - password is incorrect.";
          //  taskResult.Code = "4027";
          //  taskResult.EndDateTime = DateTime.Now;
          //  return taskResult;
          //}

          var securityToken = new SecurityToken();
          securityToken.AccountId = 1; // user.ID;
          securityToken.AuthenticationDateTime = DateTime.Now;
          securityToken.TokenExpirationDateTime = DateTime.Now.AddMinutes(g.TimeoutMinutes);
          string securityTokenString = securityToken.SerializeToken();

          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Object = securityTokenString;
          taskResult.EndDateTime = DateTime.Now;
          //taskResult.UserId = 1; // user.ID;
          return taskResult;
        }
        catch (Exception ex)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Login failed due to exception.";
          taskResult.Code = 4024;
          taskResult.FullErrorDetail = ex.ToReport();
          taskResult.Exception = ex;
          taskResult.EndDateTime = DateTime.Now;
          return taskResult;
        }
        finally
        {
          Monitor.Exit(Login_LockObject);
        }
      }
      else
      {
        var taskResult = new TaskResult("Login", "Failed to obtain lock for user login.", TaskResultStatus.Failed, 4025);
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    // Create SecurityToken for AD Authentication
    // With AD, the security token does not do anything, but the token-based nature is preserved for
    // potential future modification.
    public static object AdAuthenticate_LockObject = new object();
    public static TaskResult AdAuthenticate()
    {
      if (Monitor.TryEnter(AdAuthenticate_LockObject, 5000))
      {
        var taskResult = new TaskResult("AdAuthenticate");
        try
        {
          var securityToken = new SecurityToken();
          securityToken.AccountId = 0;
          securityToken.AuthenticationDateTime = DateTime.Now;
          securityToken.TokenExpirationDateTime = DateTime.MaxValue;
          string securityTokenString = securityToken.SerializeToken();

          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Object = securityTokenString;
          taskResult.EndDateTime = DateTime.Now;
          //taskResult.UserId = 0;
          return taskResult;
        }
        catch (Exception ex)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "AdAuthenticate failed due to exception.";
          taskResult.Code = 4999;
          taskResult.FullErrorDetail = ex.ToReport();
          taskResult.Exception = ex;
          taskResult.EndDateTime = DateTime.Now;
          return taskResult;
        }
        finally
        {
          Monitor.Exit(AdAuthenticate_LockObject);
        }
      }
      else
      {
        var taskResult = new TaskResult("AdAuthenticate", "Failed to obtain lock for AdAuthenticate.", TaskResultStatus.Failed, 4999);
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

  }
}