﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.GS.TextProcessing
{
  public class Text
  {
    public static bool BreakpointEnabled = false;
    public static bool KeepBreakpointEnabled = false;
    public static bool InDiagnosticsMode { get; set; }
    public Dictionary<string, string> MetaData { get; set; }
    public string RawText { get; set; }
    public string Name { get; set; }

    private ExtractSpec _extractSpec;
    public ExtractSpec ExtractSpec
    {
      get { return _extractSpec; }
      set { _extractSpec = value; }
    }

    public Text Parent { get; private set; }
    public TextSet TextSet { get; set; }
    public string FullPath { get { return Get_FullPath(); } }

    public List<Text> Lines { get { return this.Decompose(Constants.NewLineDelimiter); } }
    public string[] Tokens { get; set; }
    public Dictionary<string, string> LocalVariables;
    public Dictionary<string, string> GlobalVariables;

    public int LineNumber { get; set; }
    public int TextLength { get { return this.RawText.IsNotBlank() ? this.RawText.Length : 0; } }
    public bool MoreText { get { return Get_MoreText(); } }
    public string First50 { get { return this.RawText.First50(); } }
    public int PriorEndPosition { get { return Get_PriorEndPosition(); } }

    public string Description { get { return Get_Description(); } }
    public string Report { get { return Get_Report(); } }
    public string FormatName { get; set; }
    public Tsd Tsd { get; set; }
    public string TsdCode { get { return this.Tsd == null ? String.Empty : this.Tsd.Code; } }

    public int BegPosInParent { get; set; }
    public int EndPosInParent { get; set; }

    public int StartPos { get; set; }
    public int CurrPos { get; set; }
    public int BegPos { get; set; }
    public int EndPos { get; set; }
    public int TextLth { get { return this.RawText.IsBlank() ? 0 : this.RawText.Length; } }
    public string AreaOfCurrPos { get { return Get_AreaOfCurrPos(); } }

    public bool IsRoot { get { return Get_IsRoot(); } }
    public Text Root { get { return Get_Root(); } }

    public string FullReport { get { return Get_FullReport(); } }
    public XElement ExtractXml { get; set; }

    public Text PriorSibling { get { return Get_PriorSibling(); } }

    public string HexDump { get { return Get_HexDump(); } }
    public string TextDump { get { return Get_TextDump(); } }
    public int Level { get { return Get_Level(); } }
    public Cmdx Cmdx { get; set; }
    public CmdxData CmdxData { get; set; }

    private Dictionary<string, XElement> _exportTemplates;

    private StringBuilder _sb;
    public string Log { get { return this.Root == null ? "" : this.Root.Get_Log(); } }

    public Text(Text parent = null)
    {
      Initialize(parent);
    }

    public Text(string rawText, Text parent = null)
    {
      Initialize(parent);
      this.RawText = rawText;
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

        foreach (var tsd in extractSpec.Values)
          CreateStructure(tsd, this, 0);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(50, new object[] { this, extractSpec, ex });
      }
    }

    private void CreateStructure(Tsd tsd, Text t, int startPos)
    {
      try
      {
        if (tsd == null || t == null || t.RawText.IsBlank())
          return;

        t.BegPos = startPos;
        t.EndPos = -1;
        t.CurrPos = startPos;
        int textLength = t.RawText.Length;

        string name = tsd.Name;
        int level = t.Level;
        string code = tsd.Code;

        int cmdNbr = 0;

        if (Text.InDiagnosticsMode && tsd.Debug)
        {
          Debugger.Break();
          string report = t.FullReport;
        }

        var cmdxFactory = new CmdxFactory();

        do
        {
          foreach (var cmd in tsd.StructureCommands)
          {
            var cmdx = cmdxFactory.CreateCmdx(cmd);
            cmdNbr++;

            string textZoom = this.AreaOfCurrPos;

            this.WriteLog(cmdx.Code + g.crlf + textZoom);

            if (BreakpointEnabled)
            {
              if (cmdx.Break)
              {
                if (!KeepBreakpointEnabled)
                  BreakpointEnabled = false;
                Debugger.Break();
              }
            }

            t.Cmdx = cmdx;

            switch (cmdx.Verb)
            {
              case Verb.SetTextStart:
                t.BegPos = t.FindTextPosition(cmdx, t.CurrPos);
                break;

              case Verb.SetTextEnd:
                if (t.BegPos > -1)
                  t.EndPos = t.FindTextPosition(cmdx, t.BegPos);
                else
                  t.EndPos = -1;
                break;
            }
          }

          if (t.BegPos == -1 || t.EndPos == -1)
            return;

          if (t.BegPos >= t.EndPos)
            return;

          if (t.RawText.Length < t.EndPos)
            return;

          //int intervalLength = t.EndPos - t.BegPos;
          //while (t.RawText.Length > t.BegPos + intervalLength)
          //{

          //}

          tsd.BeginPosition = t.BegPos;
          tsd.EndPosition = t.EndPos;
          t.CurrPos = t.EndPos + 1;

          string subText = t.RawText.Substring(t.BegPos, t.EndPos - t.BegPos);

          var childText = new Text(subText, t);
          childText.BegPosInParent = t.BegPos;
          childText.EndPosInParent = t.EndPos;

          //string parentTextDump = t.TextDump;	
          //string childTextDump = childText.TextDump;

          childText.Tsd = tsd;
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
            CreateStructure(kvpChildTsd.Value, childText, 0);
          }
        }
        while (tsd.Iterate && t.CurrPos < t.TextLth);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(52, new object[] { this, tsd, startPos, ex });
      }
    }

    public void ExtractData()
    {
      if (_extractSpec == null)
        return;

      if (!_extractSpec.RunExtract)
        return;

      try
      {
        this.ExtractXml = new XElement(this.ExtractSpec.RootElement);
        this.ExtractXml.Add(new XAttribute("RecogSpecName", _extractSpec.RecogSpecName));
        this.ExtractXml.Add(new XAttribute("Desc", _extractSpec.Desc));
        foreach (var text in this.TextSet.Values)
        {
          ExtractData(this.ExtractXml, text);
        }

      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(53, new object[] { this, ex });
      }
    }

    private void ExtractData(XElement parent, Text t)
    {
      try
      {
        if (t.Tsd == null)
          return;

        if (t.Tsd.Debug)
        {
          Debugger.Break();
        }

        XElement xml = parent;

        var tsd = t.Tsd;

        if (tsd.IsExportXml)
        {
          if (tsd.IsExportElement)
          {
            string elementName = tsd.ExportName;

            // for now - if the name is equal, we assume we are where we need to be
            // this means that we'll add subsequent xml nodes under the element named xml
            if (xml.Name.LocalName != elementName)
            {
              if (tsd.IsExportUnique)
              {
                // element names not equal and the element should be unique 
                // find at this level or create new and add - step down...
                XElement existingElement = xml.Element(tsd.ExportName);

                if (existingElement != null)
                {
                  xml = existingElement;
                }
                else
                {
                  XElement e = tsd.ExportElement;
                  parent.Add(e);
                  xml = e;
                }
              }
              else
              {

              }
            }
          }
          else
          {
            // this is for exporting attributes at the Tsd level - probably would never do
            // but since its currently logically possible (code in Tsd to support it), we'll keep it for now
            Debugger.Break(); // need to code here
          }
        }

        t.CurrPos = 0;
        int textLength = t.RawText.Length;
        string extractedValue = String.Empty;

        string textZoom = this.AreaOfCurrPos;

        var cmdxFactory = new CmdxFactory();

        foreach (var cmd in tsd.TextExtractCommands)
        {
          string code = cmd.Code;
          extractedValue = String.Empty;
          XElement xElement = null;

          Cmdx cmdx = cmdxFactory.CreateCmdx(cmd);

          if (!cmdx.ActiveToRun)
            continue;

          this.WriteLog(cmdx.Code + g.crlf + textZoom);

          if (BreakpointEnabled)
          {
            if (cmdx.Break)
            {
              if (!KeepBreakpointEnabled)
                BreakpointEnabled = false;
              Debugger.Break();
            }
          }

          t.Cmdx = cmdx;

          switch (cmdx.Verb)
          {
            case Verb.AddExportTemplate:
              t.AddExportTemplate();
              cmd.ActiveToRun = false;
              continue;

            case Verb.CreateElement:
              xElement = t.CreateElement(cmdx);
              if (xElement != null)
              {
                xml.Add(xElement);
                xml = xElement;
              }
              break;

            case Verb.SetVariable:
              t.SetVariable(cmdx);
              break;

            case Verb.LocateToken:
              t.CurrPos = t.FindTextPosition(cmdx, t.CurrPos);
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

            case Verb.TokenizeNextLine:
              t.TokenizeNextLine(cmdx);
              break;

            case Verb.RemoveTokens:
              t.RemoveTokens(cmdx);
              break;

            case Verb.ExtractStoredToken:
              extractedValue = t.ExtractStoredToken(cmdx);
              break;

            case Verb.ExtractStoredTokens:
              extractedValue = t.ExtractStoredTokens(cmdx);
              break;

            case Verb.Truncate:
              t.Truncate(cmdx);
              break;
          }

          if (extractedValue.IsNotBlank())
          {
            if (extractedValue.StartsWith("$"))
              extractedValue = extractedValue.Substring(1);

            if (extractedValue.EndsWith("%"))
              extractedValue = extractedValue.Substring(0, extractedValue.Length - 1);

            if (xml.AttributeExists(cmd.DataName))
              throw new CxException(55, new object[] { parent, xml, cmd.DataName, t, cmdx });

            xml.AddAttribute(cmd.DataName, extractedValue);
          }
        }

        foreach (var childText in t.TextSet.Values)
        {
          ExtractData(xml, childText);
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(54, new object[] { parent, t, ex });
      }
    }

    private void Initialize(Text parent)
    {
      this.Name = String.Empty;
      this.LineNumber = 0;
      this.Parent = parent;
      this.RawText = String.Empty;
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
      catch (CxException) { throw; }
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
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(59, new object[] { this, startingAt, allowTruncation, ex });
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

      return this.Name + "         Level: " + this.Level.ToString() + "          Path: " + this.FullPath + "           TsdName: " + tsdName;
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

    private int Get_PriorEndPosition()
    {
      if (this.TextSet == null)
        throw new CxException(48, new object[] { this });

      if (this.TextSet.Count == 0)
        throw new CxException(49, new object[] { this });

      return this.TextSet.Values.Last().EndPosInParent;
    }

    public void RemoveTokens(List<Token> tokensToRemove)
    {
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

    public void AddExportTemplate()
    {
      if (this.Root == null)
        throw new CxException(122, new object[] { this });

      var root = this.Root;
      if (root._exportTemplates == null)
        root._exportTemplates = new Dictionary<string, XElement>();

      if (root._exportTemplates.ContainsKey(this.Cmdx.ExportTemplateName))
        throw new CxException(123, new object[] { this });

      root._exportTemplates.Add(this.Cmdx.ExportTemplateName, this.Cmdx.ExportTemplate);
    }

    public XElement GetExportTemplate(string name)
    {
      var root = this.Root;
      if (root == null || root._exportTemplates == null || !root._exportTemplates.ContainsKey(name))
        return null;

      return root._exportTemplates[name];
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
        if (highPos > this.TextLength - 1)
          highPos = this.TextLength - 1;

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
      var clone = new Text();
      clone.RawText = this.RawText;
      clone.Name = this.Name;
      clone.ExtractSpec = this.ExtractSpec;
      clone.Parent = this.Parent;
      clone.TextSet = null;
      clone.Tokens = new string[0];
      clone.LocalVariables = new Dictionary<string, string>();
      clone.GlobalVariables = new Dictionary<string, string>();
      clone.LineNumber = 0;
      clone.Tsd = this.Tsd.Clone();
      return clone;
    }
  }
}
