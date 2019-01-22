using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.XmlStreamSplit
{
  public partial class frmMain : Form
  {
    private long _offset = 0;
    private long _totalBytesRead = 0;
    private long _totalFileLength = 0;
    private long _bufferLength = 100000;
    private string _bufferRemainder = String.Empty;

    private string _selectedInputFileName;
    private string _selectedFileFolder;
    private int _segmentSize;
    private int _segmentIndex;
    private int _segmentCount;
    private string _textView;
    private bool _isFileLoaded;

    private FileStream _fsOut;
    private string _drillingCostItemFileName = @"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostItem_DO_NOT_USE.xml";
    private string _drillingCostSubItemFileName = @"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostSubItem_DO_NOT_USE.xml";

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = sender.ActionTag();

      switch (action)
      {
        case "SelectInputFile":
          SelectInputFile();
          break;

        case "AnalyzeFile":
          AnalyzeFile();
          break;

        case "FormatXml":
          FormatXml();
          break;

        case "ToggleWrapText":
          txtOut.WordWrap = ckWrapText.Checked;
          break;

        case "ShowFirstSegment":
          ShowSegment(action);
          break;

        case "Exit":
          Close();
          break;
      }
    }

    private void SelectInputFile()
    {
      if (_selectedFileFolder.IsNotBlank())
        dlgFileOpen.InitialDirectory = _selectedFileFolder;
      else
        dlgFileOpen.InitialDirectory = @"C:\";

      if (dlgFileOpen.ShowDialog() == DialogResult.OK)
      {
        _selectedInputFileName = dlgFileOpen.FileName;
        _selectedFileFolder = Path.GetDirectoryName(dlgFileOpen.FileName);
        txtInputSelectedInputFile.Text = _selectedInputFileName;
        var fileInfo = new FileInfo(_selectedInputFileName);
        InitializeFileScrollBar(fileInfo.Length);
        lblFileInfo.Text = "File length: " + fileInfo.Length.ToString("###,###,###,##0");
        _segmentCount = ((int)fileInfo.Length / _segmentSize) + 1;
        lblSegmentCount.Text = "Approximately " + _segmentCount.ToString("###,###,##0") + " segments";
        g.AppConfig.SetCI("LastSelectedInputFile", _selectedInputFileName);
      }
    }

    private void AnalyzeFile()
    {
      try
      {


      }
      catch (Exception ex)
      {

      }
    }

    private void InitializeFileScrollBar(long fileLength)
    {
      _segmentCount = (int) ((long)fileLength / (long)_segmentSize) + 1;
      scrollFile.SmallChange = 1;
      long largeChange = _segmentCount / 100;
      scrollFile.LargeChange = (int)largeChange;
      scrollFile.Minimum = 0;
      scrollFile.Maximum = (int)_segmentCount;
      scrollFile.Value = _segmentIndex;
    }

    private void FormatXml()
    {
      try
      {
        string text = GetTextSegment();
        var xmlHierarchy = txtXmlHierarchy.Text.Split(Constants.PipeDelimiter);

        for (int i = 0; i < xmlHierarchy.Length; i++)
        {
          string element = xmlHierarchy.ElementAt(i);
          if (element.IsNotBlank())
          {
            if (i > 0)
            {
              if (text.Contains(element))
              {
                text = text.Replace("<" + element, g.crlf + g.BlankString(i * 2) + "<" + element);
              }
            }
          }
        }

        txtOut.Text = text;
      }
      catch (Exception ex)
      {

      }
    }

    private void ShowSegment(string action)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        switch (action)
        {
          case "ShowFirstSegment":
            _segmentIndex = 0;
            break;

          case "ShowLastSegment":
            break;

          case "ShowNextSegement":
            break;

          case "ShowPrevSegment":
            break;
        }

        txtOut.Text = GetTextSegment();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to display file segment requested action is '" + action + "'." + g.crlf2 +
                        ex.ToReport(), "XmlStreamSplit - Error Showing File Segment", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private string GetTextSegment()
    {
      try
      {
        using (var fs = new FileStream(_selectedInputFileName, FileMode.Open))
        {
          long fileLength = fs.Length;
          string fileText = String.Empty;
          long position = (long)_segmentIndex * (long)_segmentSize;


          if (position > fs.Length - _segmentSize)
            position = fs.Length - _segmentSize;

          fs.Position = position;
          var bytes = new byte[_segmentSize];

          fs.Read(bytes, 0, _segmentSize);

          if (_textView == "Normal")
          {
            fileText = System.Text.Encoding.UTF8.GetString(bytes);
          }
          else
          {
            fileText = bytes.ToHexDump();
          }

          return fileText;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a segment of text from the file.", ex);
      }
    }

    private void RefreshTextView()
    {
      if (_segmentCount == 0)
        return;

      lblStatus.Text = "Segment Index = " + _segmentIndex.ToString("###,###,##0") + "  Total Segments = " + _segmentCount.ToString("###,###,##0");

      FormatXml();
    }


    private void btnGo_Click(object sender, EventArgs e)
    {
      try
      {
        using (var fs = new FileStream(@"C:\_work\WellEz\XmlFiles\Drilling.xml", FileMode.Open))
        {
          _totalFileLength = fs.Length;
          ProcessFile(fs);
          fs.Close();
        }
      }
      catch (Exception ex)
      {
        txtOut.Text = ex.ToReport();
      }
    }

    private void ProcessFile(FileStream fs)
    {
      _fsOut = new FileStream(_drillingCostItemFileName, FileMode.Append);
      string opening = "<ROOT>" + g.crlf + "  <DrillingCostItem>" + g.crlf;
      var openingBuffer = System.Text.Encoding.UTF8.GetBytes(opening);
      _fsOut.Write(openingBuffer, 0, openingBuffer.Length);

      while (true)
      {
        long bufferLength = _bufferLength;

        if (fs.Length - fs.Position < _bufferLength)
        {
          bufferLength = fs.Length - fs.Position - 1;
        }

        var bytes = new byte[bufferLength];
        int bytesRead = fs.Read(bytes, 0, (int) bufferLength);
        var str = System.Text.Encoding.UTF8.GetString(bytes);

        ProcessText(str, bytesRead);

        txtOut.Text = "Processing position " + fs.Position.ToString("###,###,###,##0") + "  file length is " + fs.Length.ToString("###,###,###,##0");
        Application.DoEvents();

        if (_fsOut == null)
          return;
      }
    }

    private void ProcessText(string t, int bytesRead)
    {
      t = t.Replace("<ROOT><DrillingCostItem>", String.Empty);

      string rows = String.Empty;
      string chunk = _bufferRemainder + t;

      int pos = chunk.LastIndexOf("<row ");

      if (pos != -1)
      {
        rows = chunk.Substring(0, pos).Replace("<row ", g.crlf + "<row ");
        _bufferRemainder = chunk.Substring(pos);
        //_bufferRemainder += ">";

        if (rows.Contains("<DrillingCostSubItem>"))
        {
          int pos2 = rows.IndexOf("<DrillingCostSubItem>");
          string endOfDrillingCostItems = rows.Substring(0, pos2).Replace("</DrillingCostItem>", String.Empty);
          var outBytes = System.Text.Encoding.UTF8.GetBytes(endOfDrillingCostItems + g.crlf + "  </DrillingCostItem>" + g.crlf + "</ROOT>");
          _fsOut.Write(outBytes, 0, outBytes.Length);
          _fsOut.Flush();
          _fsOut.Close();

          _fsOut = new FileStream(_drillingCostSubItemFileName, FileMode.Append);
          string opening = "<ROOT>" + g.crlf + "  <DrillingCostSubItem>" + g.crlf;
          var openingBuffer = System.Text.Encoding.UTF8.GetBytes(opening);
          _fsOut.Write(openingBuffer, 0, openingBuffer.Length);

          _bufferRemainder = rows.Substring(pos2).Replace("<DrillingCostSubItem>", String.Empty).TrimStart().Replace(g.crlf + "<row ", "<row ");
          return;
        }

        if (_fsOut.Length > 1900000000)
        {
          var outBytes2 = System.Text.Encoding.UTF8.GetBytes(rows + g.crlf + "  </DrillingCostSubItem>" + g.crlf + "</ROOT>");
          _fsOut.Write(outBytes2, 0, outBytes2.Length);
          _fsOut.Flush();
          _fsOut.Close();

          _fsOut = new FileStream(_drillingCostSubItemFileName.Replace(".xml", "(2).xml"), FileMode.Append);
          string opening = "<ROOT>" + g.crlf + "  <DrillingCostSubItem>" + g.crlf;
          var openingBuffer = System.Text.Encoding.UTF8.GetBytes(opening);
          _fsOut.Write(openingBuffer, 0, openingBuffer.Length);
          return;
        }

        if (bytesRead < 100000)
        {
          string lastRow = _bufferRemainder.Replace("</DrillingCostSubItem></ROOT", String.Empty);

          string lastRowString = rows + g.crlf + lastRow + g.crlf + "  </DrillingCostSubItem>" + g.crlf + "</ROOT>";

          var outBytes3 = System.Text.Encoding.UTF8.GetBytes(lastRowString);
          _fsOut.Write(outBytes3, 0, outBytes3.Length);
          _fsOut.Flush();
          _fsOut.Close();
          _fsOut = null;
          return;
        }

        var outBytesMain = System.Text.Encoding.UTF8.GetBytes(rows);
        _fsOut.Write(outBytesMain, 0, outBytesMain.Length);

      }
      else
      {


      }


      //return t;
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        string errorMessage = ex.ToReport();

        if (errorMessage.Length > 10000)
          errorMessage = errorMessage.Substring(0, 10000);

        MessageBox.Show(errorMessage, "XmlStreamSplit - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.SetInitialSizeAndLocation();

        _segmentIndex = 0;
        _segmentSize = 100000;
        splitterMain.Panel1Collapsed = true;

        cboTextView.SelectedIndex = 0;

        cboSegmentSize.LoadItems(g.GetList("SegmentSizeList"));
        cboSegmentSize.SelectItem(g.CI("SelectedSegmentSize"));

        _selectedInputFileName = g.CI("LastSelectedInputFile");
        if (File.Exists(_selectedInputFileName))
        {
          _selectedFileFolder = Path.GetDirectoryName(_selectedInputFileName);
          txtInputSelectedInputFile.Text = _selectedInputFileName;
          var fileInfo = new FileInfo(_selectedInputFileName);
          lblFileInfo.Text = "File length: " + fileInfo.Length.ToString("###,###,###,##0");
          _segmentCount = (int)((long)fileInfo.Length / (long)_segmentSize) + 1;
          lblSegmentCount.Text = "Approximately " + _segmentCount.ToString("###,###,##0") + " segments";
          InitializeFileScrollBar(fileInfo.Length);
        }
        else
        {
          g.AppConfig.SetCI("LastSelectedInputFile", String.Empty);
          lblFileInfo.Text = String.Empty;
          lblSegmentCount.Text = String.Empty;
        }

        txtXmlHierarchy.Text = g.CI("XmlHierarchy");

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 +
                        ex.ToReport(), "XmlStreamSplit - Initialization Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }
    }

    private void btnGoToLine_Click(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        int lineNumber = 0;

        var sb = new StringBuilder();

        long bytesRead = 0;
        int segmentLength = 100000;

        var fso = new FileStream(@"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostSubItem(3).xml", FileMode.Append);
        var fsi = new FileStream(@"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostSubItem(2).xml", FileMode.Open);


        while (fsi.Position < fsi.Length)
        {
          if (fsi.Length - fsi.Position > segmentLength)
          {
            var bytes = new byte[segmentLength];
            fsi.Read(bytes, 0, segmentLength);
            fso.Write(bytes, 0, segmentLength);
          }
          else
          {
            long remain = fsi.Length - fsi.Position - 1;
            var remainingBytes = new byte[remain];
            fsi.Read(remainingBytes, 0, (int) remain);
            string remainingString = System.Text.Encoding.UTF8.GetString(remainingBytes);

            fso.Write(remainingBytes, 0, (int) remain);

            string endElements = "</DrillingCostSubItem>" + g.crlf + "</ROOT>" + g.crlf;
            var endBytes = System.Text.Encoding.UTF8.GetBytes(endElements);
            fso.Write(endBytes, 0, endBytes.Length);

          }
        }


        fsi.Close();



        fso.Close();


        return;


        var sw = new StreamWriter(@"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostSubItem(3).xml");

        using (var sr = new StreamReader(@"\\gulfport.net\data\Data Management\WellEz\Imports\XMLFiles\DrillingCostSubItem(2).xml"))
        {

          while (!sr.EndOfStream)
          {
            string theLine = sr.ReadLine();
            sw.WriteLine(theLine);

            lineNumber++;

            if (sr.EndOfStream)
            {
              string stop = "stop";
            }

            if (lineNumber % 10000 == 0)
            {
              lblStatus.Text = lineNumber.ToString("###,###,##0") + " lines read";
              Application.DoEvents();
            }
            continue;






            if (lineNumber > 1490500)
            {
              string restOfFile = sr.ReadToEnd();


              while (lineNumber < 1490700)
              {
                if (sr.EndOfStream)
                {
                  txtOut.Text = sb.ToString() + g.crlf2 + "END OF STREAM REACHED" + g.crlf;
                  this.Cursor = Cursors.Default;
                  sr.Close();
                  return;
                }
                else
                {
                  string line = sr.ReadLine();
                  sb.Append(lineNumber.ToString("###,###,##0") + "  " + line + g.crlf);
                  lineNumber++;
                }
              }

              txtOut.Text = sb.ToString() + g.crlf2 + "END OF READ SEGMENT REACHED" + g.crlf;
              this.Cursor = Cursors.Default;
              sr.Close();
              return;
            }
          }

          sw.WriteLine("</DrillingCostSubItem>" + g.crlf + "</ROOT>" + g.crlf);
          sw.Close();

          sr.Close();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while processing the stream reader." + g.crlf2 + ex.ToReport(),
                        "XmlStreamSplit - Error Processing Stream", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnShowStart_Click(object sender, EventArgs e)
    {

    }

    private void btnShowEnd_Click(object sender, EventArgs e)
    {

    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        if (g.AppConfig.IsUpdated)
        {
          switch (MessageBox.Show("Do you want to save your configuration changes?" + g.crlf2 + "(Last file processed)",
                                  g.AppInfo.AppName + " - Save Configuration Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
          {
            case DialogResult.Yes:
              g.AppConfig.Save();
              break;

            case DialogResult.No:
              break;

            case DialogResult.Cancel:
              e.Cancel = true;
              break;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while saving the AppConfig.xml file." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Saving AppConfig.xml File", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void cboSegmentSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      _segmentSize = cboSegmentSize.Text.GetIntegerFromString();
      if (File.Exists(_selectedInputFileName))
      {
        var fileInfo = new FileInfo(_selectedInputFileName);
        lblSegmentCount.Text = "Approximately " + (fileInfo.Length / _segmentSize).ToString("###,###,##0") + " segments";
      }
      else
      {
        lblSegmentCount.Text = String.Empty;
      }
    }

    private void cboTextView_SelectedIndexChanged(object sender, EventArgs e)
    {
      _textView = cboTextView.Text;

      if (File.Exists(_selectedInputFileName))
      {
        txtOut.Text = String.Empty;
      }
      else
      {
        RefreshTextView();
      }
    }

    private void scrollFile_Scroll(object sender, ScrollEventArgs e)
    {
      _segmentIndex = e.NewValue;
      RefreshTextView();
    }
  }
}
