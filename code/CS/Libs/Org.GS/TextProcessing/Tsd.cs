using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements="Cmd")]
  public class Tsd : List<Cmd>
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(DefaultValue="False")]
    public bool Iterate { get; set; }

    [XMap]
    public string ExportInfo { get; set; }

    [XMap(DefaultValue="False")]
    public bool Debug { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "Tsd", WrapperElement="TsdSet")]
    public TsdSet TsdSet { get; set; }

    public string Code { get { return Get_Code(); } }
    public int BeginPosition { get; set; }
    public int EndPosition { get; set; }
    public Tsd PriorSibling { get { return Get_PriorSibling(); } }
    public string FullXmlPath { get { return Get_FullXmlPath(); } }
    public string FullFilePath { get { return (this.ExtractSpec != null ? this.ExtractSpec.FullFilePath : String.Empty); } }

    private bool? _isExportAttribute;
    private bool? _isExportElement;
    private string _exportName;
    private bool? _isExportUnique;
    public bool IsExportAttribute { get { return Get_IsExportAttribute(); } }
    public bool IsExportElement { get { return Get_IsExportElement(); } }
    public bool IsExportXml { get { return this.IsExportAttribute || this.IsExportElement; } }
    public string ExportName { get { return Get_ExportName(); } }
    public bool IsExportUnique { get { return Get_IsExportUnique(); } }
    public XElement ExportElement { get { return Get_ExportElement(); } }
    private Dictionary<string, string> _exportAttributes;
    public XAttribute ExportAttribute { get { return Get_ExportAttribute(); } }

    public List<Cmd> StructureCommands { get { return Get_StructureCommands(); } }
    public List<Cmd> TextExtractCommands { get { return Get_TextExtractCommands(); } }

    public ExtractSpec ExtractSpec; 
    public Tsd Parent { get; set; }

    public Tsd()
    {
      this.TsdSet = new TsdSet();
      this.Iterate = false;
      this.Debug = false;
      this.ExportInfo = String.Empty;
    }

    public string Get_Code()
    {
      var sb = new StringBuilder();

      foreach (var cmd in this)
      {
        if (sb.Length > 0)
          sb.Append(g.crlf); 
        sb.Append(cmd.Code); 
      }

      return sb.ToString();
    }

    private Tsd Get_PriorSibling()
    {
      if (this.Parent == null)
        return null;

      if (this.Parent.TsdSet == null || this.TsdSet.Count == 0)
        return null; 

      string thisName = this.Name;

      Tsd priorSibling = null;

      for (int i = 0; i < this.Parent.TsdSet.Count; i++)
      {
        var kvp = this.Parent.TsdSet.ElementAt(i); 
        if (kvp.Value.Name == thisName)
          return priorSibling;
        priorSibling = kvp.Value;
      }

      return null;
    }

    private string Get_FullXmlPath()
    {
      if (this.ExtractSpec == null)
        return String.Empty;

      string fullPath = this.Name;

      var  parent = this.Parent;
      while (parent != null)
      {
        fullPath = parent.Name + @"\" + fullPath;
        parent = parent.Parent;
      }

      return this.ExtractSpec.Name + @"\"  + fullPath;
    }

    public List<Cmd> Get_StructureCommands()
    {
      var list = new List<Cmd>();

      foreach (var cmd in this)
      {
        switch (cmd.Verb.ToLower())
        {
          case "settextstart":
          case "settextend":
            list.Add(cmd);
            break;
        }
      }

      return list;
    }

    public List<Cmd> Get_TextExtractCommands()
    {
      var list = new List<Cmd>();

      foreach (var cmd in this)
      {
        switch (cmd.Verb.ToLower())
        {
          case "settextstart":
          case "settextend":
            break;

          default:
						if (!cmd.Verb.ToLower().StartsWith("*"))
							list.Add(cmd);
            break;
        }
      }

      return list;
    }

    private bool Get_IsExportAttribute()
    {
      if (_isExportAttribute.HasValue)
        return _isExportAttribute.Value;

      CompileExportInfo();

      return _isExportAttribute.Value;
    }

    private bool Get_IsExportElement()
    {
      if (_isExportElement.HasValue)
        return _isExportElement.Value;

      CompileExportInfo();

      return _isExportElement.Value;
    }

    private string Get_ExportName()
    {
      if (_exportName.IsNotBlank())
        return _exportName;


      return _exportName;
    }

    private bool Get_IsExportUnique()
    {
      if (_isExportUnique.HasValue)
        return _isExportUnique.Value;

      CompileExportInfo();

      return _isExportUnique.Value;
    }

    private XElement Get_ExportElement()
    {
      try
      {
        if (!_isExportElement.HasValue)
          CompileExportInfo();

        if (!_isExportElement.Value)
          return null;

        XElement newElement = new XElement(_exportName);
        foreach (var kvpAttr in _exportAttributes)
          newElement.Add(kvpAttr.Key, kvpAttr.Value);

        return newElement;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(119, new object[] { this, ex }); 
      }
    }

    private XAttribute Get_ExportAttribute()
    {
      if (!_isExportAttribute.HasValue)
        CompileExportInfo();

      if (!_isExportAttribute.Value)
        return null;

      // build the attribute (do we have a value yet? - only if the tsd xml supplies, tsds do not have extracted values.

      Debugger.Break(); // NEED TO CODE THIS
      return new XAttribute("TestAttributeName", "TestAttributeValue"); 
    }

    public void CompileExportInfo()
    {
      try
      {
        _exportAttributes = new Dictionary<string, string>();

        if (this.ExportInfo.IsBlank())
        {
          _isExportAttribute = false;
          _isExportElement = false;
          _isExportUnique = false;
          _exportName = String.Empty;
          return;
        }

        string exportInfo = this.ExportInfo;
        string[] parms = exportInfo.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

        if (parms.Length == 0)
          throw new CxException(104, new object[] { this });

        // first parameter is the Xml name and optional pipe separated attributes in parenthesis: ElementName(Attr1=Value1|Attr2=Value2)
        string xmlSpec = parms[0].Trim();

        bool xmlSpecIndicatesElement = false;

        if (!xmlSpec.Contains('(') && !xmlSpec.Contains(')'))
        {
          string xmlName = xmlSpec;
          if (xmlName.IsBlank())
            throw new CxException(118, new object[] { this });
          if (xmlName.Contains(' '))
            throw new CxException(105, new object[] { this });
          _exportName = xmlName;
        }
        else
        {
          string xmlName = xmlSpec.GetTextBefore(Constants.OpenParen);

          if (xmlName.IsBlank())
            throw new CxException(106, new object[] { this });
          if (xmlName.Contains(' '))
            throw new CxException(107, new object[] { this });          

          xmlSpecIndicatesElement = true; // has to be an element if it has attributes
          _exportName = xmlName;

          string attrString = xmlSpec.GetTextBetween(Constants.OpenParen, Constants.CloseParen);

          if (attrString.IsBlank())
            throw new CxException(108, new object[] { this });

          string[] attrs = attrString.Split(Constants.CommaDelimiter);
          foreach (string attr in attrs)
          {
            string attrName = String.Empty;
            string attrValue = String.Empty;

            if (attr.Contains("="))
            {
              string[] tokens = attr.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
              if (tokens.Length != 2)
                throw new CxException(109, new object[] { this });
              if (tokens[0].Contains(' '))
                throw new CxException(110, new object[] { this });
              attrName = tokens[0];
              if (_exportAttributes.ContainsKey(attrName))
                throw new CxException(111, new object[] { this });
              attrValue = tokens[1];
              _exportAttributes.Add(attrName, attrValue);
            }
            else
            {
              attrName = attr;
              if (_exportAttributes.ContainsKey(attrName))
                throw new CxException(111, new object[] { this });
              attrValue = String.Empty;
              _exportAttributes.Add(attrName, attrValue);
            }
          }
        }

        // if no more parms, default the rest
        if (parms.Length == 1)
        {
          _isExportUnique = false;
          _isExportElement = true;
          _isExportAttribute = false;
          return;
        }

        // more parameters; set defaults for Tsd

        _isExportUnique = _isExportUnique.HasValue ? _isExportUnique.Value : false;
        _isExportElement = _isExportElement.HasValue ? _isExportElement.Value : true;
        _isExportAttribute = _isExportAttribute.HasValue ? _isExportAttribute.Value : false;

        bool attributeParmFound = false;
        bool elementParmFound = false;
        bool uniqueParmFound = false;
        bool newParmFound = false;


        for (int i = 1; i < parms.Length; i++)
        {
          string parm = parms[i].CompressBlanksTo(0);

          // if this is a template spec
          if (parm.ToLower().StartsWith("template="))
          {
            Debugger.Break(); // need to code here

          }
          else
          {
            parm = parm.ToLower();
            switch (parm)
            {
              case "unique": 
                uniqueParmFound = true;
                if (newParmFound)
                  throw new CxException(113, new object[] { this });
                _isExportUnique = true; 
                break;

              case "new": 
                newParmFound = true;
                if (uniqueParmFound)
                  throw new CxException(114, new object[] { this });
                _isExportUnique = false; 
                break;

              case "attribute":
                attributeParmFound = true;
                if (xmlSpecIndicatesElement)
                  throw new CxException(112, new object[] { this });
                if (elementParmFound)
                  throw new CxException(115, new object[] { this });
                _isExportAttribute = true;
                _isExportElement = false;
                break;

              case "element":
                elementParmFound = true;
                if (attributeParmFound)
                  throw new CxException(116, new object[] { this });
                _isExportAttribute = false;
                _isExportElement = true;
                break;
            }
          }
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(103, new object[] { this, ex }); 
      }
    }

    public Tsd Clone()
    {
      var clone = new Tsd();
      clone.Name = this.Name;
      clone.Iterate = this.Iterate;
      clone.Debug = this.Debug;
      clone.ExtractSpec = this.ExtractSpec;

      foreach (var cmd in this)
      {
        clone.Add(cmd.Clone());
      }

      clone.Parent = this.Parent;
      return clone;
    }

  }
}
