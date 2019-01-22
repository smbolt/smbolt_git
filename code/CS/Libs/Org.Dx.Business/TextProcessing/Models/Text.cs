﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.Dx.Business;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class Text
  {
    public static bool BreakpointEnabled = false;
    public static bool KeepBreakpointEnabled = false;
    public static bool InDiagnosticsMode {
      get;
      set;
    }
    public FileType FileType {
      get;
      set;
    }
    public Dictionary<string, string> MetaData {
      get;
      set;
    }
    public string RawText {
      get;
      set;
    }
    public XElement XElement {
      get;
      set;
    }
    public string Name {
      get;
      set;
    }
    public bool DiscardThisText {
      get;
      set;
    }
    private TextUtility _u;
    private OptionsList _optionsList;
    protected OptionsList LevelExtractOptions
    {
      get
      {
        if (_optionsList == null)
          _optionsList = new OptionsList();
        return _optionsList;
      }
      set
      {
        if (value == null)
          _optionsList = new OptionsList();
        else
          _optionsList = value;
      }
    }

    private OptionsList _aggregateOptions;
    public OptionsList ExtractOptions {
      get {
        return Get_ExtractOptions();
      }
    }

    private bool _allowDebugBreak = false;
    public bool AllowDebugBreak {
      get {
        return _allowDebugBreak;
      }
    }

    private DxWorkbook _dxWorkbook;
    public DxWorkbook DxWorkbook
    {
      get {
        return _dxWorkbook;
      }
    }

    private ExtractSpec _extractSpec;
    public ExtractSpec ExtractSpec
    {
      get {
        return _extractSpec;
      }
      set {
        _extractSpec = value;
      }
    }

    public Text Parent {
      get;
      private set;
    }
    public TextSet TextSet {
      get;
      set;
    }
    public string FullPath {
      get {
        return Get_FullPath();
      }
    }

    private string _fullFilePath;
    public string FullFilePath {
      get {
        return _fullFilePath;
      }
    }

    public List<Text> Lines {
      get {
        return this.Decompose(Constants.NewLineDelimiter);
      }
    }
    public string[] Tokens {
      get;
      set;
    }
    public Dictionary<string, string> LocalVariables;
    public static Dictionary<string, string> GlobalVariables;

    public int LineNumber {
      get;
      set;
    }
    public int TextLength {
      get {
        return this.RawText.IsNotBlank() ? this.RawText.Length : 0;
      }
    }
    public bool MoreText {
      get {
        return Get_MoreText();
      }
    }
    public bool HasMoreNonBlankText {
      get {
        return Get_HasMoreNonBlankText();
      }
    }
    public bool PriorText {
      get {
        return Get_PriorText();
      }
    }
    public string First50 {
      get {
        return this.RawText.First50();
      }
    }
    public int PriorEndPosition {
      get {
        return Get_PriorEndPosition();
      }
    }
    public int LastIndex {
      get {
        return this.RawText == null ? -1 : this.RawText.LastIndex();
      }
    }

    public string Description {
      get {
        return Get_Description();
      }
    }
    public string Report {
      get {
        return Get_Report();
      }
    }
    public string FormatName {
      get;
      set;
    }
    public Tsd Tsd {
      get;
      set;
    }
    public string TsdCode {
      get {
        return this.Tsd == null ? String.Empty : this.Tsd.Code;
      }
    }

    public int BegPosInParent {
      get;
      set;
    }
    public int EndPosInParent {
      get;
      set;
    }

    public int StartPos {
      get;
      set;
    }
    public int CurrPos {
      get;
      set;
    }
    public int BegPos {
      get;
      set;
    }
    public int EndPos {
      get;
      set;
    }
    public int TextLth {
      get {
        return this.RawText.IsBlank() ? 0 : this.RawText.Length;
      }
    }
    public string AreaOfCurrPos {
      get {
        return Get_AreaOfCurrPos();
      }
    }

    public bool IsReportUnit {
      get {
        return this.Level == 0;
      }
    }
    public bool IsRoot {
      get {
        return Get_IsRoot();
      }
    }
    public Text Root {
      get {
        return Get_Root();
      }
    }

    public string FullReport {
      get {
        return Get_FullReport();
      }
    }
    public XElement ExtractXml {
      get;
      set;
    }

    public Text PriorSibling {
      get {
        return Get_PriorSibling();
      }
    }

    public string HexDump {
      get {
        return Get_HexDump();
      }
    }
    public string TextDump {
      get {
        return Get_TextDump();
      }
    }
    public int Level {
      get {
        return Get_Level();
      }
    }
    public Cmdx Cmdx {
      get;
      set;
    }
    public CmdxData CmdxData {
      get;
      set;
    }
    public string ExtractionErrorReport {
      get;
      set;
    }

    private Dictionary<string, XElement> _extractTemplates;
    public List<CxException> CxExceptionList = new List<CxException>();

    private StringBuilder _sb;
    public string Log {
      get {
        return this.Root == null ? "" : this.Root.Get_Log();
      }
    }

    public Text(bool allowDebugBreak, FileType fileType, Text parent = null, string fullFilePath = "")
    {
      _u = new TextUtility();
      _allowDebugBreak = allowDebugBreak;
      this.FileType = fileType;
      this.LevelExtractOptions = new OptionsList();

      if (fullFilePath.IsNotBlank())
        _fullFilePath = fullFilePath;

      Initialize(parent);
    }

    public Text(bool allowDebugBreak, string rawText, Text parent = null)
    {
      _u = new TextUtility();
      this.LevelExtractOptions = new OptionsList();
      Initialize(parent);
      this.RawText = rawText;
    }

    public DxWorkbook GetWorkbook(ExtractSpec extractSpec, int exceptionLimit = 0)
    {
      try
      {
        this.ProcessStructureDefinition(extractSpec);
        this.ExtractData(exceptionLimit);

        return _dxWorkbook;
      }
      catch(CxException cx)
      {
        throw new Exception("An exception occurred while attempting to extract data from the Text object." + g.crlf +
                            "The CxException report is as follows:" + cx.ToReport(), cx.GetBaseException());
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a DxWorkbook from an ExtractSpec.", ex);
      }
    }

    public void ProcessStructureDefinition(ExtractSpec extractSpec)
    {
      try
      {
        _extractSpec = extractSpec;
        _extractSpec.Reset();

        if (extractSpec == null)
          return;

        this.Name = extractSpec.Name;
        this.TextSet.Clear();

        switch (this.FileType)
        {
          case FileType.PDF:
            foreach (var tsd in extractSpec.Values)
            {
              CreateStructurePdf(tsd, this, 0);
            }
            break;

          case FileType.XML:
            foreach (var tsd in extractSpec.Values)
            {
              CreateStructureXml(tsd, this);
            }
            break;

          default:
            throw new Exception("An unsupported file type is specified in the ExtractSpec '" + this.FileType.ToString() + "'.");
        }

        this.PropagateOptions();
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(50, new object[] { this, extractSpec, ex } );
      }
    }

    public void ExtractData(int exceptionLimit = 0)
    {
      if (_extractSpec == null)
        return;

      if (!_extractSpec.RunExtract)
        return;

      this.ExtractionErrorReport = String.Empty;

      try
      {
        _dxWorkbook = new DxWorkbook();
        _dxWorkbook.FilePath = this.FullFilePath;
        _dxWorkbook.MapPath = _extractSpec.FullFilePath;
        _dxWorkbook.IsMapped = true;

        // Important to note that the general processing algorithm is different for XML than for PDF.

        if (this.FileType == FileType.XML)
        {
          ExtractDataXml(_dxWorkbook, null, this, exceptionLimit);
        }
        else
        {
          foreach (var text in this.TextSet.Values)
          {
            switch (this.FileType)
            {
              case FileType.PDF:
                ExtractDataPdf(_dxWorkbook, null, text, exceptionLimit);
                break;
            }
          }
        }

        using (var f = new ObjectFactory2())
        {
          if (_dxWorkbook.Values.Count == 0)
            this.ExtractXml = new XElement("DxWorkbook", new XAttribute("Value", "Empty"));

          var ws = _dxWorkbook.Values.FirstOrDefault();
          if (ws != null)
          {
            var cellArray = ws.DxCellArray;
          }

          this.ExtractXml = f.Serialize(_dxWorkbook);
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(53, new object[] { this, ex });
      }
    }

    private void CreateStructurePdf(Tsd tsd, Text t, int startPos)
    {
      try
      {
        if (tsd == null || t == null || t.RawText.IsBlank())
          return;

        tsd.Condition = String.Empty;
        t.BegPos = startPos;
        t.EndPos = -1;
        t.CurrPos = startPos;
        int textLength = t.RawText.Length;
        int minimumTokens = -1;
        string name = tsd.Name;
        int level = t.Level;
        string code = tsd.Code;
        int cmdNbr = 0;
        bool trimText = false;
        int posAdjust = 0;
        var cmdxFactory = new CmdxFactory();

        do
        {
          try
          {
            bool trimSet = false;
            foreach (var cmd in tsd.StructureCommands)
            {
              var cmdx = cmdxFactory.CreateCmdx(cmd);
              cmdNbr++;

              cmdx.IsProcessingReportUnit = tsd.IsReportUnit;

              if (!tsd.Active)
                continue;

              string textZoom = this.AreaOfCurrPos;

              this.WriteLog(cmdx.Code + g.crlf + textZoom);

              t.Cmdx = cmdx;
              cmdx.Text = t;

              if (!cmdx.Execute)
              {
                this.WriteLog("Cmdx code = '" + cmdx.Code + "' skipped due to a condition or run limit.");
                continue;
              }

              cmd.RunCount++;

              switch (cmdx.Verb)
              {
                case Verb.SetTextStart:
                  t.BegPos = t.FindTextPosition(cmdx, t.CurrPos);
                  posAdjust = 0;
                  break;

                case Verb.ProcessingCommand:
                  switch (cmdx.Command)
                  {
                    case "positionadjust":
                      posAdjust = cmdx.PositionAdjust;
                      break;

                    case "trim":
                      trimSet = true;
                      trimText = true;
                      break;
                  }
                  break;

                case Verb.ReplaceText:
                  t.ReplaceText(cmdx);
                  break;

                case Verb.SetTextEnd:
                  if (!trimSet && cmdx.Trim)
                  {
                    trimSet = true;
                    trimText = true;
                  }

                  if (t.BegPos > -1)
                  {
                    int adjustedPos = t.BegPos + posAdjust;
                    if (adjustedPos < 0)
                      throw new CxException(103, new object[] { t, cmdx });
                    if (adjustedPos > t.RawText.Length)
                      throw new CxException(104, new object[] { t, cmdx });
                    minimumTokens = cmdx.MinimumTokens;
                    t.EndPos = t.FindTextPosition(cmdx, adjustedPos);
                  }
                  else
                  {
                    t.EndPos = -1;
                  }
                  break;
              }
            }
          }
          catch (CxException)
          {
            throw;
          }

          if (t.BegPos == -1 || t.EndPos == -1)
            return;

          if (t.BegPos >= t.EndPos)
            return;

          if (t.RawText.Length < t.EndPos)
            return;

          tsd.BeginPosition = t.BegPos;
          tsd.EndPosition = t.EndPos;
          t.CurrPos = t.EndPos + 1;

          if (t.CurrPos > t.RawText.Length - 1)
            t.CurrPos = t.RawText.Length - 1;

          if (t.CurrPos < t.RawText.Length - 1)
          {
            while (t.RawText[t.CurrPos] != ' ' && t.RawText[t.CurrPos] != '\n')
            {
              char c = t.RawText[t.CurrPos];
              t.CurrPos--;
              if (t.CurrPos <= t.BegPos || t.CurrPos < 1)
                break;
            }
          }

          string subText = t.RawText.Substring(t.BegPos, t.EndPos - t.BegPos);
          bool discardText = false;

          if (minimumTokens > 0)
          {
            int tokenCount = subText.TokenCount();
            if (tokenCount < minimumTokens)
              discardText = true;
          }

          if (trimText)
            subText = subText.Trim();

          var childText = new Text(_allowDebugBreak, subText, t);
          childText.FileType = t.FileType;
          childText.DiscardThisText = discardText;
          childText.BegPosInParent = t.BegPos;
          childText.EndPosInParent = t.EndPos;

          childText.Tsd = tsd;
          childText.LevelExtractOptions = tsd.LevelExtractOptions;

          childText.Name = tsd.Name;

          if (tsd.Iterate)
          {
            int seq = 0;
            childText.Name = tsd.Name + "[" + seq.ToString() + "]";
            while (t.TextSet.ContainsKey(childText.Name))
            {
              seq++;
              childText.Name = tsd.Name + "[" + seq.ToString() + "]";
            }
          }
          else
          {
            if (t.TextSet.ContainsKey(childText.Name))
              throw new CxException(51, new object[] { this, childText.Name });
          }

          t.WriteLog("Creating new Text object named '" + childText.Name + "' as a child of the parent Text object named '" + t.Name + "'." +
                     "Starting at position " + t.BegPos.ToString("###,##0") + " and having a length of " + childText.TextLength.ToString("###,##0") + ".");

          t.TextSet.Add(childText.Name, childText);

          foreach (var kvpChildTsd in tsd.TsdSet)
          {
            CreateStructurePdf(kvpChildTsd.Value, childText, 0);
          }

          if (!t.HasMoreNonBlankText)
          {
            t.CurrPos = t.LastIndex;
          }
        }
        while (tsd.Iterate && t.CurrPos < t.LastIndex);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(52, new object[] { this, tsd, startPos, ex } );
      }
    }

    private void ExtractDataPdf(DxWorkbook wb, DxWorksheet ws, Text t, int exceptionLimit = 0)
    {
      try
      {
        if (t.Tsd == null)
          return;

        if (t.Tsd.SkipExtract || t.DiscardThisText)
          return;

        if (t.Tsd.BreakOnLine.IsNotBlank() && t.Tsd.BreakOnLine.IsNumeric())
          ExtractSpec.BreakOnLine = t.Tsd.BreakOnLine.ToInt32();

        var tsd = t.Tsd;
        string tsdName = tsd.Name;

        tsd.Condition = String.Empty;

        bool runExtraction = true;

        if (tsd.Parent == null || tsd.NewWorksheet || ws == null)
        {
          ws = new DxWorksheet(wb, tsd.Name + wb.Count.ToString());
          wb.Add(ws.WorksheetName, ws);
        }

        string tName = t.Name;

        t.CurrPos = 0;
        int textLength = t.RawText.Length;
        string extractedValue = String.Empty;

        string textZoom = this.AreaOfCurrPos;

        var cmdxFactory = new CmdxFactory();
        int rowIndex = 0;

        foreach (var cmd in tsd.TextExtractCommands)
        {
          try
          {
            if (!runExtraction)
              continue;

            if (!tsd.Active)
              continue;

            string code = cmd.Code;
            extractedValue = String.Empty;

            Cmdx cmdx = cmdxFactory.CreateCmdx(cmd);

            cmdx.IsProcessingReportUnit = tsd.IsReportUnit;

            if (cmdx.IsCommentedOut || !cmdx.ActiveToRun)
              continue;

            this.WriteLog(cmdx.Code + g.crlf + textZoom);

            t.Cmdx = cmdx;
            cmdx.Text = t;

            if (!cmdx.Execute)
            {
              this.WriteLog("Cmdx code = '" + cmdx.Code + "' skipped due to a condition or run limit.");
              continue;
            }

            if (_allowDebugBreak && ExtractSpec.BreakOnLine == cmdx.LineNumber)
            {
              Debugger.Break();
              this.Root.ResetDebugBreak();
            }

            cmd.RunCount++;

            bool trimText = cmdx.Trim;

            switch (cmdx.Verb)
            {
              case Verb.ProcessingCommand:
                switch (cmdx.Command)
                {
                  case "setextractionoff":
                    runExtraction = false;
                    break;
                }
                break;

              case Verb.ReplaceText:
                t.ReplaceText(cmdx);
                break;

              case Verb.Truncate:
                t.Truncate(cmdx);
                break;

              case Verb.SetVariable:
                t.SetVariable(cmdx);
                break;

              case Verb.SetTsdCondition:
                cmdx.SetTsdCondition();
                break;

              case Verb.SetGlobalVariable:
                t.SetVariable(cmdx, VariableType.Global);
                break;

              case Verb.SetRowIndex:
                rowIndex = cmdx.RowIndex;
                break;

              case Verb.SetTextPosition:
                t.SetTextPosition(cmdx);
                break;

              case Verb.LocateToken:
                int startPos = cmdx.StartPosition;
                if (startPos == -1)
                  startPos = t.CurrPos;
                t.CurrPos = t.FindTextPosition(cmdx, startPos);
                break;

              case Verb.ExtractToken:
                extractedValue = t.ExtractToken(cmdx);
                break;

              case Verb.ExtractLiteralToken:
                extractedValue = cmdx.LiteralToken;
                break;

              case Verb.ExtractNextTokenOfType:
                extractedValue = t.ExtractNextTokenOfType(cmdx);
                break;

              case Verb.ExtractPriorTokenOfType:
                extractedValue = t.ExtractPriorTokenOfType(cmdx);
                break;

              case Verb.ExtractPriorTokensOfType:
                extractedValue = t.ExtractPriorTokensOfType(cmdx);
                break;

              case Verb.ExtractNextToken:
                extractedValue = t.ExtractNextToken(cmdx);
                break;

              case Verb.ExtractNextTokens:
                extractedValue = t.ExtractNextTokens(cmdx);
                break;

              case Verb.ExtractTextBefore:
                extractedValue = t.ExtractTextBefore(cmdx);
                break;

              case Verb.ExtractNextLine:
                extractedValue = t.ExtractNextLine(cmdx);
                break;

              case Verb.ExtractFromPeerCell:
                var cell = ws.GetCellByName(cmdx.PeerCellName);
                if ((cell == null || cell.ValueOrDefault == null) && cmdx.IsRequired)
                  throw new CxException(150, new object[] { this, cmdx });
                extractedValue = cell.ValueOrDefault.ToString();
                if (cmdx.HelperFunction.IsNotBlank())
                  extractedValue = cmdx.RunHelperFunction(extractedValue);
                break;

              case Verb.TokenizeNextLine:
                t.TokenizeNextLine(cmdx);
                break;

              case Verb.RemoveStoredTokens:
                t.RemoveStoredTokens(cmdx);
                break;

              case Verb.ExtractStoredToken:
                extractedValue = t.ExtractStoredToken(cmdx);
                break;

              case Verb.ExtractStoredTokens:
                extractedValue = t.ExtractStoredTokens(cmdx);
                break;

              case Verb.ExtractStoredTokenBefore:
                extractedValue = t.ExtractStoredTokenBefore(cmdx);
                break;
            }

            if (extractedValue.IsNotBlank() && !cmdx.DataName.ToUpper().StartsWith("DISCARD"))
            {
              if (trimText)
                extractedValue = extractedValue.Trim();

              extractedValue = RunGlobalRoutines(extractedValue);

              try
              {
                int columnIndex = Tsd.ColumnIndexMap.IndexOf(cmdx.DataName);
                var dxCell = new DxCell(cmdx.DataName, rowIndex, columnIndex, extractedValue);
                ws.AddCell(dxCell);

                // rowIndex of 99999 indicates that the actual row index (known by the worksheet (ws)) is to be
                // incremented.  We then pull the actual row index from the DxCell object, which will have its
                // RowIndex property modified by the Worksheet to the appropriate value.
                if (rowIndex == 99999)
                  rowIndex = dxCell.RowIndex;
              }
              catch (CxException cex)
              {
                cex.AddExParms(new object[] { cmdx });
                throw cex;
              }
            }
          }
          catch (CxException cex)
          {
            var sb = new StringBuilder();
            if (Root.CxExceptionList.Count > exceptionLimit)
            {
              sb = new StringBuilder();
              foreach (CxException cx in Root.CxExceptionList)
              {
                sb.Append(cx.ToReport() + g.crlf2);
              }

              string report = sb.ToString();
              var cxEx = new CxException(167, t);
              throw new CxException(167, report);
            }
            Root.CxExceptionList.Add(cex);
            break;
          }
        }

        if (runExtraction)
        {
          foreach (var childText in t.TextSet.Values)
          {
            ExtractDataPdf(wb, ws, childText, exceptionLimit);
          }
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(54, new object[] { t, ex });
      }
    }

    private void CreateStructureXml(Tsd tsd, Text t, int exceptionLimit = 0)
    {
      try
      {
        if (tsd == null || t == null || t.XElement == null)
          return;

        t.Tsd = tsd;
        t.LevelExtractOptions = tsd.LevelExtractOptions;
        tsd.Condition = String.Empty;
        string name = tsd.Name;
        int level = t.Level;
        string code = tsd.Code;
        int cmdNbr = 0;
        var cmdxFactory = new CmdxFactory();
        XElement tsdElement = null;

        try
        {
          foreach (var cmd in tsd.StructureCommands)
          {
            var cmdx = cmdxFactory.CreateCmdx(cmd);
            cmdNbr++;

            cmdx.IsProcessingReportUnit = tsd.IsReportUnit;

            if (!tsd.Active)
              continue;

            t.Cmdx = cmdx;
            cmdx.Text = t;
            XElement xml = null;

            if (!cmdx.Execute)
            {
              this.WriteLog("Cmdx code = '" + cmdx.Code + "' skipped due to a condition or run limit.");
              continue;
            }

            cmd.RunCount++;

            switch (cmdx.Verb)
            {
              case Verb.SetXml:
                xml = t.SetXml(cmdx);
                if (cmdx.IsTsdElement)
                  tsdElement = xml;
                break;
            }
          }
        }
        catch (CxException cex)
        {
          if (Root.CxExceptionList.Count > exceptionLimit)
          {
            var sb = new StringBuilder();
            foreach (CxException cx in Root.CxExceptionList)
            {
              sb.Append(cx.ToReport() + g.crlf2);
            }

            string report = sb.ToString();
            throw new CxException(167, this);
          }
          Root.CxExceptionList.Add(cex);
        }

        if (tsdElement == null)
          return;

        foreach (var childElement in tsdElement.Elements())
        {
          foreach (var kvpChildTsd in tsd.TsdSet)
          {
            var childText = new Text(_allowDebugBreak, "", t);
            childText.FileType = t.FileType;
            childText.XElement = childElement;
            childText.Name = tsd.Name + "[" + t.TextSet.Count.ToString() + "]";
            childText.Tsd = kvpChildTsd.Value;
            t.WriteLog("Creating new Text object named '" + childText.Name + "' as a child of the parent Text object named '" + t.Name + "'." +
                       "Starting at position " + t.BegPos.ToString("###,##0") + " and having a length of " + childText.TextLength.ToString("###,##0") + ".");
            t.TextSet.Add(childText.Name, childText);
            CreateStructureXml(kvpChildTsd.Value, childText);
          }
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(52, new object[] { this, tsd, ex });
      }
    }

    private void ExtractDataXml(DxWorkbook wb, DxWorksheet ws, Text t, int exceptionLimit = 0)
    {
      try
      {
        if (t.Tsd == null)
          return;

        if (t.Tsd.SkipExtract || t.DiscardThisText)
          return;

        if (t.Tsd.BreakOnLine.IsNotBlank() && t.Tsd.BreakOnLine.IsNumeric())
          ExtractSpec.BreakOnLine = t.Tsd.BreakOnLine.ToInt32();

        var tsd = t.Tsd;

        tsd.Condition = String.Empty;

        bool runExtraction = true;

        if (tsd.Parent == null || tsd.NewWorksheet)
        {
          // make sure that the last worksheet in the workbook is not empty (no rows) before we add another sheet
          if (wb.Count == 0 || wb.ElementAt(wb.Count - 1).Value.Rows.Count > 0)
          {
            ws = new DxWorksheet(wb, tsd.Name + wb.Count.ToString());
            wb.Add(ws.WorksheetName, ws);
          }
        }

        string tName = t.Name;

        t.CurrPos = 0;
        int textLength = t.RawText.Length;
        string extractedValue = String.Empty;

        string textZoom = this.AreaOfCurrPos;

        var cmdxFactory = new CmdxFactory();
        int rowIndex = 0;

        foreach (var cmd in tsd.TextExtractCommands)
        {
          try
          {
            if (!runExtraction)
              continue;

            if (!tsd.Active)
              continue;

            string code = cmd.Code;
            extractedValue = String.Empty;

            Cmdx cmdx = cmdxFactory.CreateCmdx(cmd);

            cmdx.IsProcessingReportUnit = tsd.IsReportUnit;

            if (cmdx.IsCommentedOut || !cmdx.ActiveToRun)
              continue;

            this.WriteLog(cmdx.Code + g.crlf + textZoom);

            t.Cmdx = cmdx;
            cmdx.Text = t;

            if (_allowDebugBreak && ExtractSpec.BreakOnLine == cmdx.LineNumber)
            {
              Debugger.Break();
              this.Root.ResetDebugBreak();
            }

            if (!cmdx.Execute)
            {
              this.WriteLog("Cmdx code = '" + cmdx.Code + "' skipped due to a condition or run limit.");
              continue;
            }

            cmd.RunCount++;

            bool trimText = cmdx.Trim;

            switch (cmdx.Verb)
            {
              case Verb.ProcessingCommand:
                switch (cmdx.Command)
                {
                  case "setextractionoff":
                    runExtraction = false;
                    break;
                }
                break;

              case Verb.ExtractXmlElementValue:
                extractedValue = t.ExtractXmlElementValue(cmdx);
                break;

              case Verb.SetVariableFromXmlElementValue:
                t.SetVariableFromXmlElementValue(cmdx);
                break;

              case Verb.ExtractToken:
                extractedValue = this.ExtractToken(cmdx);
                break;

              case Verb.ReplaceText:
                t.ReplaceText(cmdx);
                break;

              case Verb.SetVariable:
                t.SetVariable(cmdx);
                break;

              case Verb.SetTsdCondition:
                cmdx.SetTsdCondition();
                break;

              case Verb.SetGlobalVariable:
                t.SetVariable(cmdx, VariableType.Global);
                break;

              case Verb.SetRowIndex:
                rowIndex = cmdx.RowIndex;
                break;

              case Verb.ExtractFromPeerCell:
                var cell = ws.GetCellByName(cmdx.PeerCellName);
                if ((cell == null || cell.ValueOrDefault == null) && cmdx.IsRequired)
                  throw new CxException(150, new object[] { this, cmdx });
                extractedValue = cell.ValueOrDefault.ToString();
                if (cmdx.HelperFunction.IsNotBlank())
                  extractedValue = cmdx.RunHelperFunction(extractedValue);
                break;
            }

            if (extractedValue.IsNotBlank() && !cmdx.DataName.ToUpper().StartsWith("DISCARD"))
            {
              if (trimText)
                extractedValue = extractedValue.Trim();

              extractedValue = RunGlobalRoutines(extractedValue);

              try
              {
                int columnIndex = Tsd.ColumnIndexMap.IndexOf(cmdx.DataName);
                var dxCell = new DxCell(cmdx.DataName, rowIndex, columnIndex, extractedValue);
                ws.AddCell(dxCell);

                // rowIndex of 99999 indicates that the actual row index (known by the worksheet (ws)) is to be
                // incremented.  We then pull the actual row index from the DxCell object, which will have its
                // RowIndex property modified by the Worksheet to the appropriate value.
                if (rowIndex == 99999)
                  rowIndex = dxCell.RowIndex;
              }
              catch (CxException cex)
              {
                cex.AddExParms(new object[] { cmdx });
                throw cex;
              }
            }
          }
          catch (CxException cex)
          {
            if (Root.CxExceptionList.Count > exceptionLimit)
            {
              var sb = new StringBuilder();
              foreach (CxException cx in Root.CxExceptionList)
              {
                sb.Append(cx.ToReport() + g.crlf2);
              }

              string report = sb.ToString();
              throw new CxException(167, this);
            }
            Root.CxExceptionList.Add(cex);
            break;
          }
        }

        if (runExtraction)
        {
          foreach (var childText in t.TextSet.Values)
          {
            ExtractDataXml(wb, ws, childText, exceptionLimit);
          }
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(54, new object[] { t, ex });
      }
    }


    private string RunGlobalRoutines(string t)
    {
      if (_extractSpec.GlobalRoutineList == null || _extractSpec.GlobalRoutineList.Count == 0)
        return t;

      string processedValue = t;

      foreach (string globalRoutine in _extractSpec.GlobalRoutineList)
      {
        bool valueUpdated = false;
        switch (globalRoutine)
        {
          case "FloatingPeriodRemoval":
            string trimmedValue = processedValue.Trim();
            if (trimmedValue.EndsWith(" ."))
            {
              valueUpdated = true;
              trimmedValue = trimmedValue.Trim().Substring(0, trimmedValue.Length - 2);
            }
            if (trimmedValue.StartsWith(". "))
            {
              valueUpdated = true;
              trimmedValue = trimmedValue.Substring(2);
            }
            if (valueUpdated)
              processedValue = trimmedValue;
            break;
        }
      }

      return processedValue;
    }

    private void Initialize(Text parent)
    {
      this.Name = String.Empty;
      this.DiscardThisText = false;
      this.LineNumber = 0;
      this.Parent = parent;
      this.RawText = String.Empty;
      this.XElement = null;
      this.LocalVariables = new Dictionary<string, string>();
      this.TextSet = new TextSet();
      this.Cmdx = null;
      this.CmdxData = null;
      if (parent == null)
        this.MetaData = new Dictionary<string, string>();
      else
        this.FormatName = this.Parent.FormatName;
    }

    public void RefreshExtractSpec(ExtractSpec spec)
    {
      this.ExtractSpec = spec;

      foreach (var tsd in spec.Values)
      {
        string name = tsd.Name;

        foreach (var childText in this.TextSet.Values)
        {
          string childTextName = childText.Name;
          if (tsd.Iterate)
            childTextName = childTextName.Split('[').FirstOrDefault();

          if (childTextName == tsd.Name)
          {
            childText.Tsd = tsd;
            PopulateTextWithTsd(childText, tsd);
          }
        }
      }
    }

    private void PopulateTextWithTsd(Text text, Tsd tsd)
    {
      foreach (var childText in text.TextSet.Values)
      {
        string childTextName = childText.Name;
        if (tsd.Iterate)
          childTextName = childTextName.Split('[').FirstOrDefault();

        if (tsd.TsdSet.ContainsKey(childTextName))
        {
          var childTsd = tsd.TsdSet[childTextName];
          if (childTextName == childTsd.Name)
          {
            childText.Tsd = childTsd;
            PopulateTextWithTsd(childText, childTsd);
          }
        }
      }
    }

    public string LevelName(int level)
    {
      Text t = this;

      while (t.Level != level)
      {
        if (t.Parent == null)
          return String.Empty;
        t = t.Parent;
      }

      if (t.Name.IsBlank())
        return String.Empty;

      return t.Name;
    }

    public string GetToken(int beg, int lth, bool allowTruncation = false)
    {
      try
      {
        if (this.TextLength == 0)
          return String.Empty;

        if (this.RawText.Length < beg + lth - 1)
        {
          if (allowTruncation)
          {
            return GetToken(beg, allowTruncation);
          }
          else
          {
            throw new CxException(57, new object[] { this, beg, lth, allowTruncation });
          }
        }

        return this.RawText.Substring(beg, lth).Trim();
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(56, new object[] { this, beg, lth, allowTruncation, ex });
      }
    }

    public string GetToken(int startingAt, bool allowTruncation = false)
    {
      try
      {
        if (this.TextLength == 0)
          return String.Empty;

        if (startingAt > this.TextLength - 1)
        {
          if (allowTruncation)
            return String.Empty;
          else
            throw new CxException(58, new object[] { this, startingAt, allowTruncation });
        }

        return this.RawText.Substring(startingAt).Trim();
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(59, new object[] { this, startingAt, allowTruncation, ex });
      }
    }


    public string ExtractToken(Cmdx cmdx)
    {
      try
      {
        var tsc = cmdx.TokenSearchCriteria;
        LocatedToken locatedToken = null;
        int currPos = this.CurrPos;

        switch (tsc.Direction)
        {
          case Direction.Next:
            locatedToken = this.GetNextToken(currPos, tsc);
            break;

          case Direction.Prev:
            locatedToken = this.GetPriorToken(currPos, tsc);
            break;

          case Direction.Literal:
            if (tsc.LiteralValue.IsBlank())
              throw new CxException(131, this, cmdx);
            return tsc.LiteralValue;

          case Direction.Variable:
            string variableValue = String.Empty;
            string variableName = cmdx.Parms[1].Trim().Replace("var[", String.Empty).Replace("]", String.Empty).Trim();

            // if we are concatenating values
            if (variableName.Contains("|"))
            {
              string[] variableNames = variableName.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
              foreach (var varName in variableNames)
              {
                string name = varName;
                bool isVarRequired = true;
                if (varName.ToLower().EndsWith("-o"))
                {
                  isVarRequired = false;
                  name = varName.Substring(0, varName.Length - 2);
                }

                string varValue = this.GetVariable(name, isVarRequired);
                if (varValue.IsNotBlank())
                  variableValue += varValue;
              }

              string returnValue = variableValue;


              if (cmdx.IsRequired && variableValue.IsBlank())
                throw new CxException(135, this, cmdx);

              return variableValue.FormatValue(cmdx);
            }

            variableValue = this.GetVariable(variableName, true);

            if (tsc.BeforeToken.IsNotBlank())
            {
              int pos = -1;
              if (tsc.MatchCase)
                pos = variableValue.IndexOf(tsc.BeforeToken);
              else
                pos = variableValue.ToLower().IndexOf(tsc.BeforeToken.ToLower());
              if (pos > -1)
              {
                string partialValue = variableValue.Substring(0, pos);
                variableValue = partialValue;
              }
            }

            if (tsc.AfterToken.IsNotBlank())
            {
              int pos = -1;
              if (tsc.MatchCase)
                pos = variableValue.IndexOf(tsc.AfterToken);
              else
                pos = variableValue.ToLower().IndexOf(tsc.AfterToken.ToLower());
              if (pos > -1 && variableValue.Length > pos)
              {
                string partialValue = variableValue.Substring(pos + 1);
                variableValue = partialValue;
              }
            }

            variableValue = variableValue.FormatValue(cmdx);

            if (cmdx.Trim)
              return variableValue.Trim();

            return variableValue;


          case Direction.Stored:
            bool removeStoredToken = tsc.RemoveStoredToken;

            // storedTokenIndex values of 99998 and 99999 have special values as follows:
            // 99998 indicates that the tokens are to be "joined" together to form the extracted value
            // 99999 indicates that the last token in the array is to be extracted
            int storedTokenIndex = tsc.StoredTokenIndex;


            if (storedTokenIndex == 99998 && tsc.BeforeToken.IsBlank())
            {
              if (this.Tokens == null || this.Tokens.Length == 0)
              {
                if (tsc.IsRequired)
                  throw new CxException(148, this, cmdx);
                return String.Empty;
              }

              string joinedReturnValue = String.Empty;
              foreach (string joinToken in this.Tokens)
              {
                if (joinToken.IsNotBlank())
                {
                  if (joinedReturnValue.IsBlank())
                    joinedReturnValue = joinToken;
                  else
                    joinedReturnValue += " " + joinToken;
                }
              }

              if (joinedReturnValue.IsBlank() && tsc.IsRequired)
                throw new CxException(149, this, cmdx);

              return joinedReturnValue;
            }

            // if the stored token index is 99999
            // reset it to the last index of the array
            if (storedTokenIndex == 99999)
            {
              if (this.Tokens != null && this.Tokens.Length > 0)
              {
                storedTokenIndex = this.Tokens.Length - 1;
              }
            }


            ////////////////////////////////
            //  BEGIN OF BEFORE PROCESSING
            ////////////////////////////////

            // This section of code will get the stored token or a set of joined tokens "before" a stored literal or type-described token.
            // If tokens are joined and more than one token is found, then the returned tokens are separated by a single blank.

            if (tsc.BeforeToken.IsNotBlank())
            {
              if (this.Tokens == null || this.Tokens.Length == 0)
                throw new CxException(143, this, cmdx);

              string dataType = String.Empty;
              string typeParms = String.Empty;
              string literalValue = String.Empty;
              string returnValue = String.Empty;
              bool targetBeforeTokenFound = false;
              string candidateToken = String.Empty;
              int candidateIndex = -1;
              List<int> tokenIndicesToRemove = new List<int>();

              string bt = tsc.BeforeToken.Trim();

              // bracketed values are "types" with optional descriptive criteria
              if (bt.IsBracketed())
              {
                bt = bt.Substring(1, bt.Length - 2).ToLower();

                if (bt == "#regex#")
                {
                  dataType = "regex";
                  typeParms = cmdx.Regex;
                }
                else
                {
                  if (bt.Contains("/"))
                  {
                    string[] btTokens = bt.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
                    if (btTokens.Length != 2)
                      throw new CxException(139, this, cmdx);
                    dataType = btTokens[0];
                    typeParms = btTokens[1];
                  }
                  else
                  {
                    dataType = bt.ToLower();
                  }
                }

                dataType.AssertValidDataType();

                switch (dataType)
                {
                  case "int":
                  case "regex":
                    break;

                  default:
                    throw new CxException(142, this, cmdx, dataType);
                }
              }
              else  // non-bracketed values indicate literal values to find
              {
                literalValue = bt.Trim();
              }

              // loop through the tokens until finding the "target before token"
              // at which point
              for (int i = 0; i < this.Tokens.Length; i++)
              {
                if (targetBeforeTokenFound)
                  break;

                // The "candidateToken" is the previous token processed.  If it is not the "before token"
                // then we either "join" it to what we're saving off as our return value or use it as the return value
                // if we are not "joining" the tokens.
                // We accumulate the indices of the token(s) to be removed so they can be removed after being
                // extracted, if we are removing tokens - the default.
                if (candidateToken.IsNotBlank())
                {
                  if (tsc.Join)
                  {
                    tokenIndicesToRemove.Add(candidateIndex);
                    if (returnValue.IsBlank())
                      returnValue = candidateToken;
                    else
                      returnValue += " " + candidateToken;
                  }
                  else
                  {
                    tokenIndicesToRemove.Clear();
                    tokenIndicesToRemove.Add(candidateIndex);
                    returnValue = candidateToken;
                  }
                }

                // Read the next token in the array
                string tk = this.Tokens[i].Trim();

                // Save it also as the "candidateToken" and its index value as the candidateIndex
                // to use in the next iteration of the loop.
                candidateToken = tk;
                candidateIndex = i;

                // Are we looking for "literal values" or "typed" values?
                if (literalValue.IsNotBlank())
                {
                  if (_u.TokenMatchesLiteralValue(tk, literalValue, false))
                  {
                    targetBeforeTokenFound = true;
                    break;
                  }
                }
                else // process 'typed' values...
                {
                  if (_u.TokenMatchesTypedValue(tk, dataType, typeParms))
                  {
                    targetBeforeTokenFound = true;
                    break;
                  }
                }

              }

              // if the "target before token" is not found and we "require a value", throw exception
              if (!targetBeforeTokenFound && tsc.IsRequired)
                throw new CxException(148, this, cmdx);

              if (returnValue.IsBlank() && tsc.IsRequired)
                throw new CxException(145, this, cmdx);

              // remove the token(s) that were extracted
              if (tsc.RemoveStoredToken)
              {
                string[] newTokenArray = new string[this.Tokens.Length - tokenIndicesToRemove.Count];
                int newTokenIndex = 0;
                for (int i = 0; i < this.Tokens.Length; i++)
                {
                  if (!tokenIndicesToRemove.Contains(i))
                  {
                    newTokenArray[newTokenIndex] = this.Tokens[i];
                    newTokenIndex++;
                  }
                }

                this.Tokens = newTokenArray;
              }

              return returnValue;
            }

            //////////////////////////////
            //  END OF BEFORE PROCESSING
            //////////////////////////////

            if (this.Tokens == null || this.Tokens.Length - 1 < storedTokenIndex)
            {
              if (tsc.IsRequired)
                throw new CxException(144, this, cmdx);
              else
                return String.Empty;
            }

            string token = this.Tokens[storedTokenIndex];
            if (removeStoredToken)
              this.Tokens = this.Tokens.RemoveItemAt(storedTokenIndex);

            return token.FormatValue(cmdx);
        }

        if (locatedToken.TokenLocated)
        {
          this.CurrPos = locatedToken.CurrentPosition;
          if (cmdx.PositionAtEnd)
            this.CurrPos = locatedToken.TokenEndPosition;
          string token = locatedToken.TokenText.FormatValue(cmdx);
          if (token.IsBlank() && cmdx.IsRequired)
            throw new CxException(68, this, cmdx);
          return token.FormatValue(cmdx);
        }
        else
        {
          if (tsc.IsRequired)
            throw new CxException(123, this, cmdx);
        }

        return String.Empty;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(107, this, cmdx, ex);
      }
    }

    private string Get_FullPath()
    {
      string path = this.Name;

      var t = this;
      while (t.Parent != null)
      {
        t = t.Parent;
        path = t.Name + @"\" + path;
      }

      return path;
    }

    private int Get_Level()
    {
      int level = 0;

      var t = this;
      while (t.Parent != null)
      {
        t = t.Parent;
        level++;
      }

      return level;
    }

    private string Get_Report()
    {
      return this.RawText;
    }

    private string Get_Description()
    {
      string tsdName = this.Tsd == null ? "Tsd is null" : this.Tsd.Name == null ? "Tsd name is null" : this.Tsd.Name;
      string mapName = this.ExtractSpec == null ? " Map is null" : this.ExtractSpec.Name;

      return this.Name + "         Level: " + this.Level.ToString() + "          Path: " + this.FullPath + "           TsdName: " + tsdName + "         Map: " + mapName;
    }

    private string Get_HexDump()
    {
      if (this.RawText.IsBlank())
        return "Raw text is blank or null";

      return this.RawText.ToBinHexDump();
    }

    private string Get_TextDump()
    {
      if (this.RawText.IsBlank())
        return "Raw text is blank or null";

      return this.RawText.ToTextDump();
    }

    private Text Get_PriorSibling()
    {
      if (this.Parent == null)
        return null;

      if (this.Parent.TextSet == null || this.Parent.TextSet.Count == 0)
        return null;

      string thisName = this.Name;

      Text priorSibling = null;

      for (int i = 0; i < this.Parent.TextSet.Count; i++)
      {
        var kvp = this.Parent.TextSet.ElementAt(i);
        if (kvp.Value.Name == thisName)
          return priorSibling;
        priorSibling = kvp.Value;
      }

      return null;
    }

    private Text Get_Root()
    {
      if (this.Parent == null)
        return this;

      Text text = this.Parent;
      while (text.Parent != null)
      {
        text = text.Parent;
      }

      return text;
    }

    private string Get_FullReport()
    {
      Text root = Get_Root();

      if (root != null)
        return root.GetReport(this);

      return String.Empty;
    }

    public string GetReport(Text t)
    {
      StringBuilder sb = new StringBuilder();

      if (t.IsRoot)
        sb.Append("Full Text Hierarchy Report" + g.crlf2);

      sb.Append(g.BlankString(t.Level * 2) + "Level " + t.Level.ToString() + "  Name: " + t.Name + g.crlf +
                g.BlankString(t.Level * 2) + "----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----0" + g.crlf +
                t.RawText.To100CharLines(g.BlankString(t.Level * 2)) + g.crlf);

      foreach (var childText in t.TextSet.Values)
        sb.Append(childText.GetReport(childText));

      return sb.ToString();
    }

    private bool Get_IsRoot()
    {
      if (this.Parent == null)
        return true;

      return false;
    }

    private bool Get_MoreText()
    {
      if (this.RawText == null || this.CurrPos > this.TextLength - 1)
        return false;

      return true;
    }

    private bool Get_HasMoreNonBlankText()
    {
      if (this.CurrPos < 0)
        return false;

      if (this.CurrPos >= this.RawText.LastIndex())
        return false;

      if (this.CurrPos < this.RawText.LastIndex() - 1)
      {
        int pos = this.CurrPos;
        while (pos < this.RawText.LastIndex())
        {
          if (!this.RawText[pos].IsBlankOrNewLine())
            return true;

          pos++;
        }
      }

      return false;
    }

    private bool Get_PriorText()
    {
      if (this.RawText == null || this.CurrPos < 1)
        return false;

      return this.CurrPos < this.RawText.Length - 1;
    }

    private int Get_PriorEndPosition()
    {
      if (this.TextSet == null)
        return 0;

      if (this.TextSet.Count == 0)
        return 0;

      return this.TextSet.Values.Last().EndPosInParent;
    }

    public void RemoveStoredTokens(List<Token> tokensToRemove)
    {
      string code = this.Cmdx.Code;

      if (tokensToRemove.Count == 1 && tokensToRemove.ElementAt(0).Text.IsNumeric())
      {
        bool isRequired = tokensToRemove.ElementAt(0).IsRequired;
        int tokensToRemoveCount = tokensToRemove.ElementAt(0).Text.ToInt32();
        int tokenCount = this.Tokens.Count();

        if (tokenCount > tokensToRemoveCount)
        {
          while (tokensToRemoveCount > 0)
          {
            this.Tokens = this.Tokens.RemoveItemAt(0);
            tokensToRemoveCount--;
          }
          return;
        }
        else
        {
          if (tokenCount < tokensToRemoveCount && isRequired)
            throw new CxException(151, new object[] { this, tokensToRemove });

          this.Tokens = new string[0];
          return;
        }
      }

      foreach (var token in tokensToRemove)
      {
        if (this.Tokens.ContainsEntry(token.Text))
        {
          this.Tokens = this.Tokens.RemoveEntry(token.Text);
        }
        else
        {
          if (token.IsRequired)
            throw new CxException(84, new object[] { this });
        }
      }
    }

    public XElement GetExtractTemplate(string name)
    {
      var root = this.Root;
      if (root == null || root._extractTemplates == null || !root._extractTemplates.ContainsKey(name))
        return null;

      var template = root._extractTemplates[name];

      XElement e = new XElement(template.Name.LocalName);
      foreach(var attr in template.Attributes())
        e.Add(new XAttribute(attr.Name, attr.Value));

      return e;
    }

    public string Get_AreaOfCurrPos()
    {
      try
      {
        string zoom = String.Empty;

        if (this.RawText == null)
          return "[text is null]";

        if (this.RawText.IsBlank())
          return "[text is blank]";

        int showLength = this.TextLength > 130 ? 130 : this.TextLength;
        int currPos = this.CurrPos;
        if (currPos < 0)
          currPos = 0;

        int lowPos = this.CurrPos - 50;
        if (lowPos < 0)
          lowPos = 0;
        int highPos = lowPos + 130;
        if (highPos > this.TextLength)
          highPos = this.TextLength;

        string textToShow = this.RawText.Substring(lowPos, highPos - lowPos).Replace("\n", "\xA4").Replace(" ", "\xB7");

        int lowRuler = lowPos;
        while (lowRuler > 100)
          lowRuler -= 100;

        int highRuler = lowRuler + showLength;

        string ruler = g.Ruler.Substring(lowRuler, highRuler - lowRuler);

        zoom = ruler + g.crlf +
               textToShow + g.crlf +
               g.BlankString(currPos - lowPos) + "^" + g.BlankString(highPos - currPos) + g.crlf +
               "Text length: " + this.TextLength.ToString("###,##0").PadToJustifyRight(8) + g.crlf +
               "Current Pos: " + this.CurrPos.ToString("###,##0").PadToJustifyRight(8);

        return zoom;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    public string ResolveVariables(string value)
    {
      string originalValue = value;

      if (value.IndexOf("$") < 0)
        return value;

      int variableCount = 0;

      do
      {
        int begPos = value.IndexOf("$");
        int endPos = value.IndexOf("$", begPos + 1);
        if (endPos == -1)
          throw new CxException(152, new object[] { this, originalValue });

        int length = endPos - begPos - 1;
        if (length < 1)
          throw new CxException(153, new object[] { this, originalValue });

        string variableName = value.Substring(begPos + 1, length);
        if (this.LocalVariables.ContainsKey(variableName))
        {
          value = value.Replace("$" + variableName + "$", this.LocalVariables[variableName]);
        }
        else
        {
          if (Text.GlobalVariables.ContainsKey(variableName))
          {
            value = value.Replace("$" + variableName + "$", Text.GlobalVariables[variableName]);
          }
          else
          {
            throw new CxException(180, new object[] { this, originalValue });
          }
        }

        variableCount++;
        if (variableCount > 10)
          throw new CxException(155, new object[] { this, originalValue });

      } while (value.IndexOf("$") > -1);

      return value;
    }

    public void SetVariableValue(string key, string value, VariableType variableType = VariableType.Local)
    {
      switch (variableType)
      {
        case VariableType.Local:
          if (this.LocalVariables == null)
            this.LocalVariables = new Dictionary<string, string>();

          if (this.LocalVariables.ContainsKey(key))
            this.LocalVariables[key] = value;
          else
            this.LocalVariables.Add(key, value);
          break;

        case VariableType.Global:
          if (Text.GlobalVariables == null)
            Text.GlobalVariables = new Dictionary<string, string>();

          if (Text.GlobalVariables.ContainsKey(key))
            Text.GlobalVariables[key] = value;
          else
            Text.GlobalVariables.Add(key, value);
          break;
      }
    }

    public void PropagateOptions()
    {
      _u.OptionsList = this.ExtractOptions;

      if (this.TextSet == null)
        return;

      foreach (var t in this.TextSet.Values)
        t.PropagateOptions();
    }

    public OptionsList Get_ExtractOptions()
    {
      if (_aggregateOptions != null)
        return _aggregateOptions;

      List<string> extractOptions = new List<string>();

      // stack up the text objects from this one to the root
      var optionsLists = new List<OptionsList>();
      Text t = this;

      optionsLists.Add(t.LevelExtractOptions);

      while (t.Parent != null)
      {
        t = t.Parent;
        optionsLists.Add(t.LevelExtractOptions);
      }

      optionsLists.Add(t.ExtractSpec.LevelExtractOptions);

      int index = -1;

      // get the extract options starting with the root and working down
      // collect up the extract options down from the top of the hierarchy
      // override negated or unnegated items as appropriate
      // return a single flat list that is the result of the cascade
      for (int i = optionsLists.Count - 1; i > -1; i--)
      {
        var optionList = optionsLists.ElementAt(i);
        for (int j = 0; j < optionList.OptionCount; j++)
        {
          string levelExtractOption = optionList.ElementAt(j);
          // if the item exists in the list, see if we're negating it
          if (!extractOptions.Contains(levelExtractOption))
          {
            // if its negated, see if it needs to override a higher-level unnegated option of the same name
            if (levelExtractOption.StartsWith("!"))
            {
              string unnegatedOption = levelExtractOption.Substring(1);
              index = extractOptions.IndexOf(unnegatedOption);
              if (index > -1)
                extractOptions[index] = levelExtractOption;
              else
                extractOptions.Add(levelExtractOption);
            }
            else // else if it not negated, see if we need to unnegate a negated item higher up in the list
            {
              string negatedOption = "!" + levelExtractOption;
              index = extractOptions.IndexOf(negatedOption);
              if (index > -1)
                extractOptions[index] = levelExtractOption;
              else
                extractOptions.Add(levelExtractOption);
            }
          }
        }
      }

      _aggregateOptions = new OptionsList(extractOptions);

      return _aggregateOptions;
    }

    public void ResetDebugBreak()
    {
      _allowDebugBreak = false;

      if (this.TextSet == null && this.TextSet.Count == 0)
        return;

      foreach (var childText in this.TextSet.Values)
        childText.ResetDebugBreak();
    }

    private string Get_Log()
    {
      if (_sb == null)
        return String.Empty;

      return _sb.ToString();
    }

    public void WriteLog(string message)
    {
      if (!this.IsRoot && this.Root != null)
      {
        this.Root.WriteLog(message);
        return;
      }

      if (!this.IsRoot)
        return;

      if (_sb == null)
        _sb = new StringBuilder();

      _sb.Append(DateTime.Now.ToString("yyyyMMdd HHmmss.fff") + " - " + message + g.crlf);
    }

    public Text Clone()
    {
      var clone = new Text(_allowDebugBreak, this.FileType);
      clone.RawText = this.RawText;
      clone.Name = this.Name;
      clone.ExtractSpec = this.ExtractSpec;
      clone.Parent = this.Parent;
      clone.TextSet = null;
      clone.Tokens = new string[0];
      clone.LocalVariables = new Dictionary<string, string>();
      clone.LineNumber = 0;
      clone.Tsd = this.Tsd.Clone();
      return clone;
    }
  }
}
