using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Threading = System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.Ops.Business;
using Org.TP.Concrete;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Microsoft.Exchange.WebServices.Data;

namespace Org.Ops.Tasks
{
  public class StatementEmailMonitor : TaskProcessorBase
  {
    private Logger _logger;
    private StringBuilder _message;
    private bool _restrictToOneFile;
    private List<Item> _successItems;
    private List<Item> _errorItems;
    private Dictionary<string, string> _attachmentKeyWordsToDest;
    private string _expectedAttachmentExtension;

    public override async Threading.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _logger = new Logger();
      _message = new StringBuilder();
      _successItems = new List<Item>();
      _errorItems = new List<Item>();
      _attachmentKeyWordsToDest = new Dictionary<string, string>();
      try
      {
        taskResult = await Threading.Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          _restrictToOneFile = GetParmValue("RestrictToOneFile").ToBoolean();
          _expectedAttachmentExtension = GetParmValue("ExpectedAttachmentExtension").ToString().ToLower();
          string processedEmailFolder = GetParmValue("ProcessedEmailFolder").ToString();
          string errorEmailFolder = GetParmValue("ErrorEmailFolder").ToString();

          string fsStem = GetParmValue("$FSSTEM$").ToString();
          string env = GetParmValue("$ENV$").ToString();

          foreach (var parm in base.ParmSet.Where(parm => parm.ParameterName.StartsWith("KeyWord:")))
          {
            string keyWord = parm.ParameterName.Split(Constants.ColonDelimiter)[1];
            string outputFolderPath = parm.ParameterValue.ToString();

            if (outputFolderPath.Contains("$FSSTEM$"))
              outputFolderPath = outputFolderPath.Replace("$FSSTEM$", fsStem);

            if (outputFolderPath.Contains("$ENV$"))
              outputFolderPath = outputFolderPath.Replace("$ENV$", env);

            _attachmentKeyWordsToDest.Add(keyWord, outputFolderPath);
          }

          if (_attachmentKeyWordsToDest.Count == 0)
            return taskResult.Failed("Could not locate any 'KeyWord' parameters to determine the output folder path.");

          var emailAddress = GetParmValue("EmailAddress").ToString();
          var tokenizedPassword = GetParmValue("EmailPassword").ToString();
          ExchangeServiceManager exServMgr = new ExchangeServiceManager(emailAddress, TokenMaker.GenerateToken2(tokenizedPassword));
          tokenizedPassword = null;

          List<Item> items = exServMgr.GetItems("Stmt Inbox");

          if (items.Count == 0)
          {
            taskResult.NoWorkDone = true;
            return taskResult.Success("No emails in the inbox of '" + emailAddress + "'.");
          }

          _message.Append("Task Results for EmailMonitor of '" + emailAddress + "':" + g.crlf);
          foreach (var item in items)
            ProcessItem(item);

          _message.Append("All items that processed successfully were moved to the Email Folder '" + processedEmailFolder + "' (" + _successItems.Count + ")" + g.crlf);
          _message.Append("All items that encountered an error were moved to the Email Folder '" + errorEmailFolder + "' (" + _errorItems.Count + ")");

          string dryRunIndicator = String.Empty;
          if (IsDryRun)
          {
            dryRunIndicator = "*** DRY RUN ***" + g.crlf2;
            taskResult.NoWorkDone = true;
          }
          else
          {
            exServMgr.MoveItems(_successItems, processedEmailFolder);
            exServMgr.MoveItems(_errorItems, errorEmailFolder);
          }

          if (_errorItems.Count == 0)
            return taskResult.Success(dryRunIndicator + _message.ToString());
          else
            return taskResult.Failed(dryRunIndicator + _message.ToString());
        });

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed(_message.ToString() + g.crlf2 + "An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    private void ProcessItem(Item item)
    {
      try
      {
        _message.Append("Email: '" + item.Subject + "' | Received On: " + item.DateTimeReceived.ToString("yyyy-MM-dd HH:mm:ss") + g.crlf);
        if (item.GetType() != typeof(EmailMessage))
        {
          _message.Append("  ERROR: Not of type 'EmailMessage'" +  g.crlf2);
          _errorItems.Add(item);
          return;
        }

        var email = item as EmailMessage;
        if (!email.HasAttachments)
        { 
          _message.Append("  ERROR: No Attachments" + g.crlf2);
          _errorItems.Add(item);
          return;
        }

        int attachmentCount = 0;
        bool attachmentSuccess = false;
        bool attachmentError = false;       
        foreach (var attachment in email.Attachments)
        {
          if (attachment.Name.EndsWith("png") || attachment.Name.EndsWith("jpg"))
            continue;

          attachmentCount++;
          if (attachment.GetType() != typeof(FileAttachment))
          {
            _message.Append(attachmentCount.ToString("000") + ": SKIP - Attachment '" + attachment.Name + "' is not of type 'FileAttachment'" + g.crlf);
            continue;
          }

          if (!ValidExtension(attachment.Name))
          {
            _message.Append(attachmentCount.ToString("000") + ": SKIP - Attachment '" + attachment.Name + "' does not have the expected extension '" + _expectedAttachmentExtension + "'" + g.crlf);
            continue;
          }

          var fileAttachment = attachment as FileAttachment;
          fileAttachment.Load();

          string outputFolderPath = String.Empty;
          foreach (var keyWordToDest in _attachmentKeyWordsToDest)
          {
            if (fileAttachment.Name.ToLower().Contains(keyWordToDest.Key.ToLower()))
            {
              outputFolderPath = keyWordToDest.Value;
              break;
            }
          }

          if (outputFolderPath.IsBlank())
          {
            _message.Append(attachmentCount.ToString("000") + ": SKIP - Could not find any Key Word in Attachment '" + fileAttachment.Name + "'" + g.crlf);
            continue;
          }

          if (!Directory.Exists(outputFolderPath))
          {
            _message.Append(attachmentCount.ToString("000") + ": ERROR - Could not save Attachment '" + fileAttachment.Name + "' because Directory '" + outputFolderPath + "' does not exist." + g.crlf);
            attachmentError = true;
            continue;
          }

          string outputFilePath = outputFolderPath + @"\" + fileAttachment.Name;
          
          if (_restrictToOneFile && Directory.GetFiles(outputFolderPath).Count() > 0)
          {
            _message.Append(attachmentCount.ToString("000") + ": ERROR - Task is configured to restrict to one file and Directory '" + outputFolderPath + "' already contains files" + g.crlf);
            attachmentError = true;
            continue;
          }
          else if (File.Exists(outputFilePath))
          {
            _message.Append(attachmentCount.ToString("000") + ": ERROR - Directory '" + outputFolderPath + "' already contains a file named '" + fileAttachment.Name + g.crlf);
            attachmentError = true;
            continue;
          }

          if (!IsDryRun)
            fileAttachment.Load(outputFilePath);

          _message.Append(attachmentCount.ToString("000") + ": Attachment '" + fileAttachment.Name + "' was successfully saved in '" + outputFolderPath + "'." + g.crlf);
          attachmentSuccess = true;
        }

        _message.Append(g.crlf);

        if (attachmentError || !attachmentSuccess)
        {
          _errorItems.Add(item);
          return;
        }

        if (!IsDryRun)
        {
          email.IsRead = true;
          email.Update(ConflictResolutionMode.AutoResolve);
        }
        _successItems.Add(item);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to process Outlook item with subject '" + item.Subject + "'.", ex);
      }
    }

    private bool ValidExtension(string fileName)
    {
      try
      {
        string extension = fileName.Split(Constants.PeriodDelimiter).Last();

        string searchStr = _expectedAttachmentExtension.Replace("*", String.Empty);

        if (_expectedAttachmentExtension == "*")
          return true;
        if (extension == searchStr)
          return true;
        if (_expectedAttachmentExtension.StartsWith("*") && _expectedAttachmentExtension.EndsWith("*") && extension.Contains(searchStr))
          return true;
        if (_expectedAttachmentExtension.StartsWith("*") && extension.EndsWith(searchStr))
          return true;
        if (_expectedAttachmentExtension.EndsWith("*") && extension.StartsWith(searchStr))
          return true;

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to validate the extension of attached file '" + fileName + "'.", ex);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();

      AssertParmExistence("$FSSTEM$");
      AssertParmExistence("$ENV$");
      AssertParmExistence("RestrictToOneFile");
      AssertParmExistence("EmailAddress");
      AssertParmExistence("EmailPassword");
      AssertParmExistence("ProcessedEmailFolder");
      AssertParmExistence("ErrorEmailFolder");
      AssertParmExistence("ExpectedAttachmentExtension");
    }
  }
}
