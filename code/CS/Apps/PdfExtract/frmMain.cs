using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Pdfx;
using Org.GS;
using Org.GS.Configuration;

namespace Org.PdfExtract
{
  public partial class frmMain : Form
  {
    private a a;
    private string _filePath = String.Empty;
    private string _outputPath = String.Empty;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Run":
          Run();
          break;

        case "ExtractImages":
          ExtractImages();
          break;

        case "ShowXml":
          DisplayXml();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Run()
    {
      this.Cursor = Cursors.WaitCursor;
      StringBuilder sb = new StringBuilder();

      bool useLocationTextExtractionStrategy = cboTextExtractionStrategy.Text == "Location";

      List<string> filesToProcess = Directory.GetFiles(this._filePath, "*.pdf").ToList();

      try
      {
        foreach (var fileToProcess in filesToProcess)
        {
          string text;
          sb.Append("Processing file: " + fileToProcess + g.crlf);

          using (var extractor = new TextExtractor())
          {
            text = extractor.ExtractTextFromPdf(fileToProcess, false, txtPageBreak.Text, useLocationTextExtractionStrategy);
          }

          if (ckReplaceCrlfWithNewLine.Checked)
            text = text.Replace("\n", "\r\n");

          //sb.Append(text);
          var lines = text.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
          foreach (var line in lines)
            sb.Append(line + g.crlf);
        }

        string outText = sb.ToString();
        txtOut.Text = ProcessText(outText);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred attempting to process a PDF file." + g.crlf +
                        "Exception Message: " + ex.Message + g.crlf +
                        "Stack Trace: " + ex.StackTrace,
                        "PdfExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private string ProcessText(string textIn)
    {
      var sb = new StringBuilder();

      string[] lines = textIn.Split(Constants.NewLineDelimiter);

      for (int i = 0; i < lines.Length; i++)
      {
        string line = lines[i];
        var dateTokenPos = LocateDateTokens(line);

        if (dateTokenPos.Count == 0)
          continue;

        if (dateTokenPos.Count == 1)
        {
          sb.Append(line + g.crlf);
          continue;
        }


        int startPos = 0;

        for (int j = 0; j < dateTokenPos.Count; j++)
        {
          int beg = dateTokenPos.ElementAt(j);
          if (j == 0 && beg > 0)
            beg = 0;

          if (j == dateTokenPos.Count - 1)
          {
            sb.Append(line.Substring(beg, line.Length - beg).Trim() + g.crlf);
            break;
          }

          int end = dateTokenPos.ElementAt(j + 1) - 1;
          string subLine = line.Substring(beg, end - beg).Trim();
          sb.Append(subLine + g.crlf);
        }
      }

      string textOut = sb.ToString();


      return textOut;
    }

    private List<int> LocateDateTokens(string s)
    {
      List<string> dateTokens = new List<string>();
      List<int> dateTokenPos = new List<int>();

      string[] tokens = s.Split(Constants.SpaceDelimiter);

      foreach (var token in tokens)
      {
        if (token.Length == 5)
        {
          if (Char.IsDigit(token[0]) && Char.IsDigit(token[1]) && token[2] == '/' &&
              Char.IsDigit(token[3]) && Char.IsDigit(token[4]))
          {
            dateTokens.Add(token);
          }
        }
      }

      if (dateTokens.Count > 0)
      {
        int pos = 0;

        foreach (var dateToken in dateTokens)
        {
          int tokenPos = s.IndexOf(dateToken, pos);
          dateTokenPos.Add(tokenPos);
          pos = tokenPos + dateToken.Length;
        }
      }

      return dateTokenPos;
    }


    private void ExtractImages()
    {
      this.Cursor = Cursors.WaitCursor;
      StringBuilder sb = new StringBuilder();

      bool useLocationTextExtractionStrategy = cboTextExtractionStrategy.Text == "Location";

      List<string> filesToProcess = Directory.GetFiles(this._filePath, "*.pdf").ToList();

      try
      {
        foreach (var fileToProcess in filesToProcess)
        {
          sb.Append("Processing file: " + fileToProcess + g.crlf);

          using (var extractor = new TextExtractor())
          {
            extractor.ExtractImagesFromPDF(fileToProcess, "");
          }
        }

        string outText = sb.ToString();
        txtOut.Text = outText;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred attempting to process a PDF file." + g.crlf +
                        "Exception Message: " + ex.Message + g.crlf +
                        "Stack Trace: " + ex.StackTrace,
                        "PdfExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void DisplayXml()
    {
      this.Cursor = Cursors.WaitCursor;
      StringBuilder sb = new StringBuilder();


      List<string> filesToProcess = Directory.GetFiles(this._filePath, "*.xml").ToList();

      try
      {
        foreach (var fileToProcess in filesToProcess)
        {
          sb.Append("Processing file: " + fileToProcess + g.crlf);
          string xmlText = File.ReadAllText(fileToProcess);
          var rootElement = XElement.Parse(xmlText);
          sb.Append(rootElement.ToString() + g.crlf2 + "<PAGE BREAK>" + g.crlf2);
        }

        string outText = sb.ToString();
        txtOut.Text = outText;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred attempting to process a PDF file." + g.crlf +
                        "Exception Message: " + ex.Message + g.crlf +
                        "Stack Trace: " + ex.StackTrace,
                        "PdfExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private int Get6thSpaceIndex(string str)
    {
      int count = 0;
      for (int i = 0; i < str.Length; i++)
      {
        if (str[i] == ' ')
        {
          count++;
          if (count == 6)
            return i;
        }
      }
      return -1;
    }



    private void InitializeForm()
    {
      try
      {
        a = new a();

        _filePath = g.ImportsPath;
        _outputPath = g.ExportsPath;

        cboTextExtractionStrategy.SelectedIndex = 0;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "PdfExtract - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

  }
}
