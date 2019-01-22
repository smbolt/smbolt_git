using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Org.TSK.Business;
using Org.Ops.ApiModels;
using Org.WebApi;
using Org.WebApi.Models;
using Org.DB;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Network;
using Org.GS;

namespace Org.OpsControlApi.Services
{
  public class ScheduledTaskService : ServiceBase
  {
    public static ConfigDbSpec ConfigDbSpec;

    public ScheduledTaskService(WebContext webContext, ControllerBase controller)
      : base(webContext, controller) {}


    public TaskResult GetScheduledTasks(int skip, int take, string sort)
    {
      var taskResult = new TaskResult("GetScheduledTasks");

      try
      {
        var modelSet = GetScheduledTaskList(skip, take, sort, false);
        taskResult.TotalEntityCount = modelSet.TotalEntityCount;
        taskResult.SubsetEntityCount = modelSet.SubsetEntityCount;
        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.IsPaging = true;
        taskResult.Message = modelSet.ModelList.Count.ToString() + " scheduled tasks of " + taskResult.TotalEntityCount.ToString() + " retrieved.";
        taskResult.Object = modelSet.ModelList;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "Retrieval of scheduled tasks failed.";
        taskResult.Code = 4999;
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    private ApiModelSet GetScheduledTaskList(int skip, int take, string sort, bool includeDeleted)
    {
      var modelSet = new ApiModelSet();
      int statusIdLimit = includeDeleted ? 99 : 3;

      try
      {
        using (var repo = new TaskRepository(ConfigDbSpec))
        {
          var models = repo.GetScheduledTasks();
          modelSet.TotalEntityCount = models.Count();
          var modelList = models.Skip(skip).Take(5);
          foreach (var model in modelList)
          {
            var apiModel = new ScheduledTask();
            apiModel.ScheduledTaskId = model.Value.ScheduledTaskId;
            apiModel.TaskName = model.Value.TaskName;
            apiModel.ProcessorTypeId = model.Value.ProcessorTypeId;
            apiModel.ProcessorName = model.Value.ProcessorName;
            apiModel.ProcessorVersion = model.Value.ProcessorVersion;
            apiModel.AssemblyLocation = model.Value.AssemblyLocation;
            apiModel.StoredProcedureName = model.Value.StoredProcedureName;
            apiModel.IsActive = model.Value.IsActive;
            apiModel.IsLongRunning = model.Value.IsLongRunning;
            apiModel.TrackHistory = model.Value.TrackHistory;
            apiModel.ActiveScheduleId = model.Value.ActiveScheduleId;
            apiModel.CreatedBy = model.Value.CreatedBy;
            apiModel.CreatedDate = model.Value.CreatedDate;
            apiModel.ModifiedBy = model.Value.ModifiedBy;
            apiModel.ModifiedDate = model.Value.ModifiedDate;
            modelSet.ModelList.Add(apiModel);
          }

          return modelSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to retrieve scheduled task list.", ex);
      }

    }
  }
}