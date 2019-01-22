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
    private List<Item> _successItems;
    private List<Item> _errorItems;
    private string _outputFolderPath;
    private List<string> _validAttachmentExtensions;
    private decimal _maxFileSizeMB = 50;

    public override async Threading.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _logger = new Logger();
      _message = new StringBuilder();
      _successItems = new List<Item>();
      _errorItems = new List<Item>();

      try
      {
        taskResult = await Threading.Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          if (ParmExists("MaxFileSizeMB"))
            _maxFileSizeMB = GetParmValue("MaxFileSizeMB").ToDecimal();

          string delimitedExtensions = GetParmValue("ValidAttachmentExtensions").ToString();
          _validAttachmentExtensions = delimitedExtensions.ToListOfStrings(Constants.CommaDelimiter);
          _validAttachmentExtensions.ForEach(e => e = e.Trim());

          string processedEmailFolder = GetParmValue("ProcessedEmailFolder").ToString();
          string errorEmailFolder = GetParmValue("ErrorEmailFolder").ToString();

          _outputFolderPath = GetParmValue("OutputFolderPath").ToString();
          _outputFolderPath = _outputFolderPath.Replace("$FSSTEM$", GetParmValue("$FSSTEM$").ToString());
          _outputFolderPath = _outputFolderPath.Replace("$ENV$", GetParmValue("$ENV$").ToString());

          if (!Directory.Exists(_outputFolderPath))
            throw new Exception("OutputFolderPath '" + _outputFolderPath + "' does not exist.");

          var smtpSpec = GetParmValue("ExchangeSmtpSpec") as ConfigSmtpSpec;
          smtpSpec.SmtpPassword = TokenMaker.DecodeToken2(smtpSpec.SmtpPassword);

          ExchangeServiceManager exServMgr = new ExchangeServiceManager(smtpSpec);

          List<Item> items = exServMgr.GetItems("Stmt Inbox");

          if (items.Count == 0)
          {
            taskResult.NoWorkDone = true;
            return taskResult.Success("No emails in the inbox of '" + smtpSpec.EmailFromAddress + "'.");
          }

          _message.Append("Task Results for EmailMonitor of '" + smtpSpec.EmailFromAddress + "':" + g.crlf);
          foreach (var item in items)
            ProcessItem(item);

          _message.Append("All successfully processed emails were moved to the Email Folder '" + processedEmailFolder + "' (" + _successItems.Count + ")" + g.crlf);
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
        DateTime timestamp = DateTime.Now;

        _message.Append("Email: '" + item.Subject + "' | Received On: " + item.DateTimeReceived.ToString("yyyy-MM-dd HH:mm:ss.fff") + g.crlf);
        if (item.GetType() != typeof(EmailMessage))
        {
          _message.Append("  ERROR: Not of type 'EmailMessage'" +  g.crlf2);
          _errorItems.Add(item);
          return;
        }

        var email = item as EmailMessage;
        if (!email.HasAttachments)
        {
          email.IsRead = true;
          email.Update(ConflictResolutionMode.AutoResolve);
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

          string extension = attachment.Name.Split(Constants.PeriodDelimiter).Last();
          if (!ValidExtension(attachment.Name))
          {
            _message.Append(attachmentCount.ToString("000") + ": SKIP - Attachment '" + attachment.Name + "' has an unexpected extension: '" + extension + "'" + g.crlf);
            continue;
          }

          if (attachment.Size > _maxFileSizeMB)
          {
            _message.Append(attachmentCount.ToString("000") + ": SKIP - Attachment '" + attachment.Name + "' has a file size (" + )
          }

          var fileAttachment = attachment as FileAttachment;
          fileAttachment.Load();

          int seq = 0;
          string outputFilePath = _outputFolderPath + @"\" + timestamp.ToString("yyyyMMdd-HHmmss.fff-") + seq.ToString("000") + " " + fileAttachment.Name;

          while(File.Exists(outputFilePath))
          {
            seq++;
            outputFilePath = _outputFolderPath + @"\" + timestamp.ToString("yyyyMMdd-HHmmss.fff-") + seq.ToString("000") + " " + fileAttachment.Name;
          }

          if (!IsDryRun)
            fileAttachment.Load(outputFilePath);

          _message.Append(attachmentCount.ToString("000") + ": Attachment '" + fileAttachment.Name + "' was successfully saved in '" + _outputFolderPath + "'." + g.crlf);

          StringBuilder manifest = new StringBuilder();
          string ciString = g.crlf + "  <CI K=\"{0}\" V=\"{1}\" />";
          manifest.Append("<Manifest>");

          manifest.Append(String.Format(ciString, "FileName", fileAttachment.Name));
          manifest.Append(String.Format(ciString, "RetrievalMethod", "Email"));
          manifest.Append(String.Format(ciString, "From", email.From.Address));
          manifest.Append(String.Format(ciString, "ReceivedBy", email.ReceivedBy.Address));
          manifest.Append(String.Format(ciString, "ToRecipients", String.Join(", ", email.ToRecipients.Select(r => r.Address))));
          manifest.Append(String.Format(ciString, "CcRecipients", String.Join(", ", email.CcRecipients.Select(r => r.Address))));
          manifest.Append(String.Format(ciString, "BccRecipients", String.Join(", ", email.BccRecipients.Select(r => r.Address))));
          manifest.Append(String.Format(ciString, "DateTimeSent", email.DateTimeSent.ToString("yyyy-MM-dd HH:mm:ss")));
          manifest.Append(String.Format(ciString, "DateTimeReceived", email.DateTimeReceived.ToString("yyyy-MM-dd HH:mm:ss")));

          manifest.Append(g.crlf + "</Manifest>");

          File.WriteAllText(outputFilePath + ".manifest.dat", manifest.ToString());

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

        if (extension.StartsWith("xl") || extension.Equals("pdf"))
          return true;

        foreach (string validExt in _validAttachmentExtensions)
        {
          string searchStr = validExt.Replace("*", String.Empty);

          if (validExt == "*")
            return true;
          if (extension == searchStr)
            return true;
          if (validExt.StartsWith("*") && validExt.EndsWith("*") && extension.Contains(searchStr))
            return true;
          if (validExt.StartsWith("*") && extension.EndsWith(searchStr))
            return true;
          if (validExt.EndsWith("*") && extension.StartsWith(searchStr))
            return true;
        }

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
      AssertParmExistence("ProcessedEmailFolder");
      AssertParmExistence("ErrorEmailFolder");
      AssertParmExistence("OutputFolderPath");
      AssertParmExistence("ExchangeSmtpSpec");
      AssertParmExistence("ValidAttachmentExtensions");
    }
  }
}
