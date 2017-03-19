using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

namespace Org.PdfParser
{
  public class PdfEngine : IDisposable
  {
    public TaskResult ParsePdfToText(string pdfIn)
    {
      var taskResult = new TaskResult();
      taskResult.TaskName = "GetTextFromPdf";
     
      PDDocument pdDoc = null;

      try
      {
        pdDoc = PDDocument.load(pdfIn);
        var textStripper = new PDFTextStripper();
        string text = textStripper.getText(pdDoc);

        if (text.IsBlank())
          taskResult.Message = "No text was extracted from PDF file.";
        else
          taskResult.Message = "Text extracted from PDF - total bytes is " + text.Length.ToString() + ".";

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.Data = text;
        taskResult.EndDateTime = DateTime.Now;

        textStripper = null;
        pdDoc = null; 

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An exception occurred attempting to extract text from PDF file.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

    public void Dispose()
    {

    }
  }
}
