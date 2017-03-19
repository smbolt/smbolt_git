using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Pdfx;
using Org.TP.Concrete;
using Org.TP;
using Org.GS.Configuration;
using Org.GS;

namespace Org.PDF.Tasks
{
  public class PdfSearch : TaskProcessorBase
  {
    private Func<bool> _checkContinue;
    private string _pdfSearchPath;
    private bool _recursiveSearch = false;

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      _checkContinue = checkContinue;
      TaskResult taskResult = base.InitializeTaskResult();
      Initialize();

      try
      {
        return await Task.Run<TaskResult>(() =>
        {
          var fileSystem = new OSFolder();
          fileSystem.FullPath = _pdfSearchPath;
          fileSystem.ProcessChildFolders = true;
          fileSystem.SearchParms = new SearchParms();
          fileSystem.BuildFolderAndFileList();
          var fileList = fileSystem.GetFileList();

          StringBuilder sb = new StringBuilder();

          using (var tx = new TextExtractor())
          {
            foreach (var file in fileList)
            {
              string pdfText = tx.ExtractTextFromPdf(file.FullPath, true);
              sb.Append("********************************************************************************************************" + g.crlf +
                        "********************************************************************************************************" + g.crlf2); 
              sb.Append("FILE: " + file.FullPath + g.crlf + pdfText + g.crlf2);
            }
          }

          taskResult.Data = sb.ToString();


          //sb.Append(g.crlf2 + "FILE LIST" + g.crlf); 
          //foreach (var file in fileList)
          //  sb.Append(file.FullPath + g.crlf);

          //sb.Append(g.crlf + "FILE MAP" + g.crlf);
          //sb.Append(fileSystem.GetFileListReport());
          //taskResult.Data = sb.ToString();

          return taskResult.Success();
        });
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during PdfSearch processing." + g.crlf + ex.ToReport(), ex);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();

      base.AssertParmExistence("PdfSearchPath");

      _pdfSearchPath = base.GetParmValue("PdfSearchPath").ToString();
      if (base.ParmExists("RecursiveSearch"))
        _recursiveSearch = base.GetParmValue("RecursiveSearch").ToBoolean();
    }
  }
}
