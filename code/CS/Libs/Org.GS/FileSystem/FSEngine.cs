using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.GS
{
  public class FSEngine : IDisposable
  {
    public TaskResult Run(FSActionSet fsActionSet)
    {
      try
      {
        fsActionSet.Initialize();

        var taskResult = new TaskResult();
        taskResult.TaskName = "RunFSAction";

        foreach (var fsActionGroupKvp in fsActionSet)
        {
          string actionGroupName = fsActionGroupKvp.Key;
          var actionGroup = fsActionGroupKvp.Value;

          if (actionGroup.IsActive)
          {
            var actionGroupTaskResult = this.Run(actionGroup);
            taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, actionGroupTaskResult);

            if (actionGroupTaskResult.TaskResultStatus == TaskResultStatus.Failed)
            {
              switch(actionGroup.FailureAction)
              {
                case FailureAction.AbortGroup:
                  actionGroup.ContinueProcessing = false;
                  break;

                case FailureAction.AbortSet:
                  actionGroup.ContinueProcessing = false;
                  actionGroup.FSActionSet.ContinueProcessing = false;
                  break;
              }

              if (!fsActionSet.ContinueProcessing)
                break;
            }
          }
        }

        taskResult.TaskResultStatus = taskResult.TaskResultSet.GetWorstResult();
        return taskResult;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attemptint to run FSActionSet.", ex); 
      }
    }

    public TaskResult Run(FSActionGroup fsActionGroup)
    {
      var fsu = new FileSystemUtility();

      var taskResult = new TaskResult();
      taskResult.TaskName = "ActionGroup:" + fsActionGroup.Name; 

      try
      {
        foreach (var fsActionKvp in fsActionGroup)
        {
          string key = fsActionKvp.Key;
          var action = fsActionKvp.Value;
          string fullActionName = action.FullActionName;
          string src = action.FullSourcePath;
          string dst = action.FullDestPath;

          if (!Directory.Exists(src))
            throw new Exception("Directory '" + src + "' does not exist and thus cannot be the source for file system action (FSAction) '" + fullActionName + "'.");
          
          if (action.DestFolderRequired && !Directory.Exists(dst))
            Directory.CreateDirectory(dst); 

          if (action.DestFolderRequired && action.ClearDirectory)
            fsu.ClearDirectoryOfFiles(dst, action.ClearPattern.IsNotBlank() ? action.ClearPattern : "*.*"); 

          foreach (var item in action.Values)
          {
            switch (action.FileSystemCommand)
            {
              case FileSystemCommand.Copy:
                fsu.CopyFiles(src, dst, item.Name, action.Overwrite);
                taskResult.TaskResultStatus = TaskResultStatus.Success;
                break;

              case FileSystemCommand.ReadWriteTest:
                if (fsu.AssertAccess(src, FileSystemAccess.ReadWrite))
                  taskResult.TaskResultStatus = TaskResultStatus.Success;
                else
                  taskResult.TaskResultStatus = TaskResultStatus.Failed;
                break;

              default:
                throw new Exception("FileSystemCommand '" + action.FileSystemCommand.ToString() + "' is not implemented yet."); 
            }
          }
        }

        return taskResult;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attemptint to run FSActionGroup named '" + fsActionGroup.Name + "'.", ex); 
      }
    }

    public void Dispose()
    {

    }
  }
}
