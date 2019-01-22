using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Drawing;
using System.Reflection;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class DocumentElement
  {
    private string _name;
    public string Name
    {
      get {
        return this._name;
      }
      set
      {
        if (value.IsBlank())
        {
          if (_name == null)
            _name = String.Empty;
          return;
        }
        this._name = value;
      }
    }

    public string Tag {
      get;
      set;
    }
    public string ContentQuery {
      get;
      set;
    }
    public string ContentValue {
      get;
      set;
    }
    public string StyleId {
      get;
      set;
    }
    public string Class {
      get;
      set;
    }
    public DeType DeType {
      get;
      set;
    }
    public bool IsAdsdi {
      get;
      set;
    }
    public bool IsUsed {
      get;
      set;
    }
    public Doc Doc {
      get;
      set;
    }
    public DocumentElement Parent {
      get;
      set;
    }

    public int Depth {
      get;
      set;
    }
    public string AbsPath {
      get;
      set;
    }
    public string RelPath {
      get;
      set;
    }

    public DocumentElement Properties {
      get;
      set;
    }
    public List<DocumentElement> ChildElements {
      get;
      set;
    }

    public AggregatedProperties AggregatedProperties {
      get;
      set;
    }
    public bool PropertiesAreAggregated {
      get;
      set;
    }
    public string Source {
      get;
      set;
    }
    public Metrics RawMetrics {
      get;
      set;
    }
    public Metrics MappedMetrics {
      get;
      set;
    }

    private bool _isProperties;
    public bool IsProperties {
      get {
        return this._isProperties;
      }
    }
    public string OpenXmlClassName {
      get;
      set;
    }
    public bool IsFirst {
      get {
        return this.Get_IsFirst();
      }
    }
    public bool IsLast {
      get {
        return this.Get_IsLast();
      }
    }
    public bool IsUnique {
      get {
        return this.Get_IsUnique();
      }
    }
    public bool IsLeaf {
      get {
        return this.Get_IsLeaf();
      }
    }
    public string OxName {
      get {
        return Get_OxName();
      }
    }
    public string ParentSet {
      get {
        return Get_ParentSet();
      }
    }
    public string ChildOfSet {
      get {
        return Get_ChildOfSet();
      }
    }
    public bool IsAutoMappable {
      get {
        return Get_AutoMap();
      }
    }
    public string Abbr {
      get {
        return Get_Abbr();
      }
    }
    public bool MapAsAttribute {
      get {
        return Get_IsAttribute();
      }
    }
    public int Level {
      get {
        return Get_Level();
      }
    }
    public virtual string Text {
      get {
        return Get_Text();
      }
    }
    public OccupiedRegionSet RegionSet {
      get;
      set;
    }

    private bool _inDiagnosticsMode;
    public bool InDiagnosticsMode
    {
      get {
        return _inDiagnosticsMode;
      }
      set {
        _inDiagnosticsMode = value;
      }
    }

    public int _diagnosticsLevel;
    public int DiagnosticsLevel
    {
      get {
        return _diagnosticsLevel;
      }
      set {
        _diagnosticsLevel = value;
      }
    }

    public List<Match> PropertyMatchList {
      get;
      set;
    }

    public DocumentElement()
    {
      this.RawMetrics = new Metrics(null);
      this.MappedMetrics = new Metrics(null);
      this.OpenXmlClassName = String.Empty;
      this.AggregatedProperties = new AggregatedProperties(null, null);
      this.PropertiesAreAggregated = false;
      this.Source = String.Empty;
      this.BuildPropertyMatchList();
      this.Set_IsProperties();
      this.ContentQuery = String.Empty;
      this.ContentValue = String.Empty;
      this.StyleId = String.Empty;
      this.InDiagnosticsMode = false;
      this.DiagnosticsLevel = 0;
      this.RegionSet = new OccupiedRegionSet();
    }

    protected virtual void Initialize(XElement xml, Doc doc, DocumentElement parent)
    {
      try
      {
        this.RawMetrics.Doc = doc;
        this.MappedMetrics.Doc = doc;

        this.DeType = (DeType)g.ToEnum<DeType>(this.GetType().Name, DocSpec.DeType.NotSet);
        if (this.DeType == DocSpec.DeType.NotSet)
          throw new Exception("Type '" + this.GetType().Name + "' is not defined in the DeType enumeration.");

        this.Doc = doc;
        if (this.DeType == DeType.Doc)
          this.Doc = (Doc)this;

        this.InDiagnosticsMode = this.Doc.InDiagnosticsMode;

        this.Parent = parent;

        if (parent == null)
          this.Depth = 0;
        else
          this.Depth = parent.Depth + 1;

        string parentAbsPath = String.Empty;
        if (parent == null)
          parentAbsPath = "null";
        else
          parentAbsPath = parent.AbsPath;

        this.ChildElements = new List<DocumentElement>();

        if (xml == null)
        {
          this.Name = this.Doc.GetName(this);
          this.AbsPath = parentAbsPath + @"/" + this.Name;
          this.RelPath = this.Doc.GetRelPath(this.AbsPath);
          this.Tag = String.Empty;
          this.IsAdsdi = false;
          this.IsUsed = false;
          return;
        }

        this.IsUsed = true;
        this.IsAdsdi = xml.Name.NamespaceName.Contains("adsdi.com");

        this.ContentQuery = xml.GetAttributeValue("q");
        if (this.ContentQuery == "*")
          this.ContentQuery = xml.Value.Trim();

        this.StyleId = xml.GetAttributeValue("styleId");

        string wTag = String.Empty;
        if (xml.Attribute("tag") != null)
          wTag = xml.Attribute("tag").Value.Trim();

        string aTag = xml.GetAttributeValue("tag");

        this.Tag = wTag;
        if (aTag.IsNotBlank())
          this.Tag = aTag;

        string wClass = String.Empty;
        if (xml.Attribute("class") != null)
          wClass = xml.Attribute("class").Value.Trim();

        string aClass = xml.GetAttributeValue("class");

        this.Class = wClass;
        if (aClass.IsNotBlank())
          this.Class = aClass;

        string wName = String.Empty;
        if (xml.Attribute("name") != null)
          wName = xml.Attribute("name").Value.Trim();

        string aName = xml.GetAttributeValue("name");

        this.Name = wName;
        if (aName.IsNotBlank())
          this.Name = aName;

        if (this.Name.IsBlank())
          this.Name = this.Doc.GetName(this);

        this.AbsPath = parentAbsPath + @"/" + this.Name;
        if (this.Tag == "tgt")
          this.Doc.TgtPath = this.AbsPath;
        this.RelPath = this.Doc.GetRelPath(this.AbsPath);

        this.Properties = null;

        if (!this.IsAdsdi && this.Tag.IsNotBlank())
        {
          if (this.Doc.Tags.ContainsKey(this.Tag))
            throw new Exception("Non-Adsdi document (generated from Word xml) has duplicate tag '" + this.Tag + "'.");

          doc.Tags.Add(this.Tag, this);
        }

      }
      catch (Exception ex)
      {
        throw (ex);
      }
    }

    protected virtual void LoadChildren(XElement xml, DocumentElement doc, DocumentElement parent)
    {
      if (this.ChildElements == null)
        this.ChildElements = new List<DocumentElement>();

      Section currentSection = null;

      IEnumerable<XElement> set = xml.Elements();
      foreach (XElement e in set)
      {
        string elementName = e.Name.LocalName;
        DocumentElement d = null;

        switch (elementName)
        {
          case "docDefaults":
            break;

          case "repeat":
            int repeatCount = e.GetRequiredIntegerAttribute("count");
            for (int i = 0; i < repeatCount; i++)
            {
              DocHelper.ManageRepeatTags(e, i);
              parent.LoadChildren(e, this.Doc, parent);
            }
            break;

          case "body":
            d = new Body(e, this.Doc, this);
            break;

          case "sectPr":
            d = new Section();
            d.Parent = this;
            d.Doc = this.Doc;
            d.Depth = d.Parent.Depth + 1;
            d.DeType = DocSpec.DeType.Section;
            d.IsAdsdi = true;
            d.IsUsed = true;

            d.Name = e.GetAttributeValue("name");
            if (d.Name.IsBlank())
              d.Name = this.Doc.GetName(d);

            d.AbsPath = d.Parent.AbsPath + @"/" + d.Name;
            d.RelPath = d.Doc.GetRelPath(d.AbsPath);

            d.Tag = e.GetAttributeValue("tag");
            SectionProperties sp = new SectionProperties(e, this.Doc, d);
            sp.Depth = d.Depth + 1;
            d.Properties = sp;
            currentSection = (Section)d;
            break;

          case "tbl":
            if (currentSection != null)
              d = new Table(e, this.Doc, currentSection);
            else
              d = new Table(e, this.Doc, parent);
            break;

          case "tr":
            d = new TableRow(e, this.Doc, parent);
            break;

          case "tc":
            d = new TableCell(e, this.Doc, parent);
            break;

          case "p":
            d = new Paragraph(e, this.Doc, parent);
            break;

          case "r":
            d = new Run(e, this.Doc, parent);
            break;

          case "t":
            d = new Text(e, this.Doc, parent);
            break;

          case "docInfo":
            break;

          case "mapEntry":
            d = new MapEntry(e, this.Doc, parent);
            break;
        }

        if (d != null)
        {
          if (currentSection == null || elementName == "sectPr")
          {
            this.ChildElements.Add(d);
          }
          else
          {
            if (currentSection.ChildElements == null)
              currentSection.ChildElements = new List<DocumentElement>();
            d.Parent = currentSection;
            currentSection.ChildElements.Add(d);
          }
        }
      }
    }

    public void Overlay(DocumentElement de)
    {


    }

    public XElement BuildXmlMap(DocumentElement d, bool showProperties)
    {
      XElement mapElement = null;

      try
      {
        string tag = "[null]";
        if (d.Tag != null)
          tag = d.Tag;

        if (tag.IsBlank())
          tag = "[blank]";

        string parent = "[null]";
        if (d.Parent != null)
          parent = d.Parent.DeType.ToString();

        string name = "[null]";
        if (d.Name != null)
          name = d.Name;

        if (name.IsBlank())
          name = "[blank]";

        string docPath = d.AbsPath;
        if (d.RelPath.IsNotBlank())
          docPath = d.RelPath;

        mapElement = new XElement(d.DeType.ToString());

        if (d.DeType == DocSpec.DeType.Text)
        {
          Text t = (Text)d;
          if (t.Val != null)
          {
            if (t.Val.IsNotBlank())
            {
              mapElement.Add(new XElement("TextValue", t.Val));
            }
          }
        }

        mapElement.Add(new XAttribute("Name", name));
        mapElement.Add(new XAttribute("Tag", tag));
        mapElement.Add(new XAttribute("Depth", d.Depth.ToString()));
        mapElement.Add(new XAttribute("Parent", parent));
        mapElement.Add(new XAttribute("Path", docPath));

        if (d.Properties != null && showProperties)
          mapElement.Add(d.Properties.GetXmlMap(showProperties));

        if (d.AggregatedProperties != null && showProperties)
          mapElement.Add(d.AggregatedProperties.GetXmlMap());

        if (d.ChildElements != null)
          foreach (DocumentElement de in d.ChildElements)
            mapElement.Add(BuildXmlMap(de, showProperties));

        return mapElement;
      }
      catch (Exception ex)
      {
        string exceptionReport = ex.ToReport();
        return new XElement("ErrorOccurred", new XElement("ExceptionReport", exceptionReport));
      }
    }

    public string BuildMap(DocumentElement d, bool showProperties)
    {
      StringBuilder sb = new StringBuilder();

      try
      {
        string name = "[null]";
        if (d.Name != null)
          name = d.Name;

        if (name.IsBlank())
          name = "[blank]";

        string depth = "(" + d.Depth.ToString("00") + ")";

        string parent = "[null]";
        if (d.Parent != null)
          parent = d.Parent.Name;

        string tag = "[null]";
        if (d.Tag != null)
          tag = d.Tag;

        if (tag.IsBlank())
          tag = "[blank]";

        string deType = d.DeType.ToString();

        string text = String.Empty;
        if (d.DeType == DocSpec.DeType.Text)
          text = ((Text)d).Val;

        sb.Append(deType.PadTo(12) + name.PadTo(10) +
                  depth.PadTo(5) +
                  parent.PadTo(8) + " " +
                  "XY:" +
                  d.RawMetrics.Offset.X.ToString("####0").PadTo(6).JustifyRight() +
                  d.RawMetrics.Offset.Y.ToString("####0").PadTo(6).JustifyRight() +
                  "  SSz:" +
                  d.RawMetrics.SizeSpec.Width.Val.ToString("####0.00").PadTo(9).JustifyRight() + " " +
                  d.RawMetrics.SizeSpec.Width.Units.ToString().PadTo(2).PadTo(3) +
                  d.RawMetrics.SizeSpec.Width.SizeControl.ToString().PadTo(2).PadTo(3) +
                  d.RawMetrics.SizeSpec.Width.IsSpecified.ToString().TrimToMax(1).PadTo(2) +
                  d.RawMetrics.SizeSpec.Height.Val.ToString("####0.00").PadTo(9).JustifyRight() + " " +
                  d.RawMetrics.SizeSpec.Height.Units.ToString().PadTo(2).PadTo(3) +
                  d.RawMetrics.SizeSpec.Height.SizeControl.ToString().PadTo(2).PadTo(3) +
                  d.RawMetrics.SizeSpec.Height.IsSpecified.ToString().TrimToMax(1).PadTo(2) +
                  " TSz:" +
                  d.RawMetrics.TotalSize.Width.ToString("####0").PadTo(6).JustifyRight() + " " +
                  d.RawMetrics.TotalSize.Height.ToString("####0").PadTo(6).JustifyRight() + " " +
                  " Tag:" + tag.PadTo(10) + " " +
                  text.TrimToMax(7).PadTo(7) + " " +
                  d.AbsPath + g.crlf);

        if (d.Properties != null && showProperties)
          sb.Append(d.Properties.GetMap(showProperties));

        if (d.ChildElements != null)
          foreach (DocumentElement de in d.ChildElements)
            sb.Append(BuildMap(de, showProperties));

        string map = sb.ToString();

        return map;
      }
      catch (Exception ex)
      {
        string exceptionReport = ex.ToReport();
        return "ErrorOccurred" + g.crlf + "ExceptionReport" + g.crlf +  exceptionReport;
      }
    }

    private string FormatDepthAndType(int depth, string type)
    {
      return (g.BlankString(depth) + depth.ToString("00") + "    "  + type + g.BlankString(35)).Substring(0, 35);
    }

    public XNode GetXmlMap(bool showProperties)
    {
      XNode mapNode = null;

      Type t = this.GetType();
      string name = t.Name;
      if (this.OpenXmlClassName.IsNotBlank())
        name = this.OpenXmlClassName;

      mapNode = new XElement(name);

      List<string> elementNames = new List<string>() {
        "Text", "ContentQuery", "ContentValue"
      };
      if (name == "Text")
      {
        elementNames.Remove("Text");
        elementNames.Insert(0, "Val");
      }
      foreach (string elementName in elementNames)
      {
        PropertyInfo pi = t.GetProperty(elementName);
        if (pi != null)
        {
          object oValue = pi.GetValue(this, null);
          string value = String.Empty;
          if (oValue != null)
            value = oValue.ToString();
          if (value.IsNotBlank())
            ((XElement)mapNode).Add(new XElement(elementName, value));
        }
      }

      foreach (Match m in this.PropertyMatchList)
      {
        PropertyInfo pi = t.GetProperty(m.PropertyName);
        if (pi == null)
        {
          ((XElement)mapNode).Add(new XElement("MISSING-" + m.PropertyName, "null"));
          break;
        }

        string propertyName = pi.Name;
        if (m.Abbr.IsNotBlank())
          propertyName = m.Abbr;
        string propertyValue = "null";
        object oValue = pi.GetValue(this, null);
        if (oValue != null)
          propertyValue = oValue.ToString();

        if (pi.PropertyType.IsSubclassOf(typeof(DocumentElement)))
        {
          if (oValue == null)
          {
            if (this.MapAsAttribute)
              ((XElement)mapNode).Add(new XAttribute(propertyName, "null"));
            else
              ((XElement)mapNode).Add(new XElement(propertyName, "null"));
          }
          else
          {
            DocumentElement de = (DocumentElement)oValue;
            ((XElement)mapNode).Add(de.GetXmlMap(showProperties));
          }
        }
        else
        {
          if (m.MapAsAttribute)
          {
            ((XElement)mapNode).Add(new XAttribute(propertyName, propertyValue));
          }
          else
          {
            ((XElement)mapNode).Add(new XElement(propertyName, propertyValue));
          }
        }
      }

      List<string> propNames = new List<string>() {
        "Name", "Tag", "Parent", "Level"
      };
      foreach (string propName in propNames)
      {
        PropertyInfo piParent = t.GetProperty(propName);
        if (piParent != null)
        {
          object value = piParent.GetValue(this, null);
          string parentName = "[null]";
          if (propName == "Parent")
          {
            if (value != null)
            {
              DocumentElement parent = (DocumentElement)value;
              if (parent.Name != null)
                parentName = parent.Name;
              ((XElement)mapNode).Add(new XAttribute(propName, parentName));
            }
          }
          else
          {
            ((XElement)mapNode).Add(new XAttribute(propName, value.ToString()));
          }
        }
      }

      foreach(DocumentElement de in this.ChildElements)
        ((XElement)mapNode).Add(de.GetXmlMap(showProperties));

      return mapNode;
    }

    public string GetMap(bool showProperties)
    {
      StringBuilder sb = new StringBuilder();

      Type t = this.GetType();
      string name = t.Name;
      if (this.OpenXmlClassName.IsNotBlank())
        name = this.OpenXmlClassName;

      int propertiesMapped = 0;

      foreach (Match m in this.PropertyMatchList)
      {
        PropertyInfo pi = t.GetProperty(m.PropertyName);
        if (pi == null)
        {
          if (propertiesMapped == 0)
            sb.Append(g.BlankString(27) + name + " ");
          sb.Append("MISSING-" + m.PropertyName + " is null" + g.crlf);
          break;
        }

        string propertyName = pi.Name;
        if (m.Abbr.IsNotBlank())
          propertyName = m.Abbr;
        string propertyValue = "null";
        object oValue = pi.GetValue(this, null);
        if (oValue != null)
          propertyValue = oValue.ToString();

        if (pi.PropertyType.IsSubclassOf(typeof(DocumentElement)))
        {
          if (oValue == null)
          {
            if (propertiesMapped == 0)
              sb.Append(g.BlankString(27) + name + " ");
            sb.Append(propertyName + "is null" + g.crlf);
          }
          else
          {
            DocumentElement de = (DocumentElement)oValue;
            sb.Append(de.GetMap(showProperties));
          }
        }
        else
        {
          if (propertiesMapped == 0)
            sb.Append(g.BlankString(27) + name + " ");
          sb.Append(propertyName + ":" + propertyValue + "  ");
          propertiesMapped++;
        }
      }

      if (propertiesMapped > 0)
        sb.Append(g.crlf);

      foreach(DocumentElement de in this.ChildElements)
        sb.Append(de.GetMap(showProperties));

      string map = sb.ToString();

      return map;
    }

    public virtual void Draw(Graphics g)
    {
    }

    public virtual void MapElement(Graphics g)
    {
      this.RawMetrics.SizeSpec = this.GetSizeSpec();

      foreach (DocumentElement de in this.ChildElements)
      {
        de.MapElement(g);
        this.MergeMetrics(de.RawMetrics);
      }
    }

    public virtual void DrawElement(Graphics g)
    {
      this.TraceMetrics("DOCE_001");
      this.RawMetrics.SizeSpec = this.GetSizeSpec();

      foreach (DocumentElement de in this.ChildElements)
      {
        de.DrawElement(g);
        this.MergeMetrics(de.RawMetrics);
        this.TraceMetrics("DOCE_002");
      }
    }

    public virtual SizeSpec GetSizeSpec()
    {
      if (this.Parent != null)
        return this.Parent.RawMetrics.SizeSpec.Clone();

      return new SizeSpec();
    }

    public virtual void SetSizeSpec()
    {
      if (this.Parent != null)
      {
        this.RawMetrics.SizeSpec = this.Parent.RawMetrics.SizeSpec.Clone();
        return;
      }

      this.RawMetrics.SizeSpec = new SizeSpec();
    }

    public virtual void MergeMetrics(Metrics m)
    {

    }

    private string Get_OxName()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return String.Empty;

      if (metaAttribute.OxName == null)
        return String.Empty;

      return metaAttribute.OxName;
    }

    private string Get_ParentSet()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return String.Empty;

      if (metaAttribute.ParentSet == null)
        return String.Empty;

      return metaAttribute.ParentSet;
    }

    private string Get_ChildOfSet()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return String.Empty;

      if (metaAttribute.ChildOfSet == null)
        return String.Empty;

      return metaAttribute.ChildOfSet;
    }

    private bool Get_AutoMap()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return false;

      return metaAttribute.AutoMap;
    }

    private string Get_Abbr()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return String.Empty;

      if (metaAttribute.Abbr == null)
        return String.Empty;

      return metaAttribute.Abbr;
    }

    private bool Get_IsAttribute()
    {
      Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(this.GetType(), typeof(Meta));
      if (metaAttribute == null)
        return false;

      return metaAttribute.IsAttribute;
    }

    private bool Get_IsFirst()
    {
      if (this.DeType == DocSpec.DeType.NotSet)
        return false;

      if (this.AbsPath != null)
        if (this.AbsPath == "[loner]")
          return false;

      if (this.IsProperties)
        return true;

      int thisIndex = -1;
      int total = 0;

      if (this.Parent == null  && this.DeType != DocSpec.DeType.Doc)
        throw new Exception("Object of type '" + this.DeType.ToString() + "' named '" + this.Name + "' has a null Parent.");

      for (int i = 0; i < this.Parent.ChildElements.Count; i++)
      {
        DocumentElement de = this.Parent.ChildElements[i];
        if (de.DeType == this.DeType)
        {
          total++;
          if (de.Name == this.Name)
          {
            if (i == 0)
              return true;

            thisIndex = i;
          }
        }
      }

      if (total == 0)
        throw new Exception("Illogical condition: type '" + this.DeType.ToString() + "' named '" + this.Name + "' cannot be found within its parent of type '" +
                            this.Parent.DeType.ToString() + "' named '" + this.Parent.Name + "'.");

      if (thisIndex == 0)
        return true;

      return false;
    }

    private bool Get_IsLast()
    {
      if (this.DeType == DocSpec.DeType.NotSet)
        return false;

      if (this.AbsPath != null)
        if (this.AbsPath == "[loner]")
          return false;

      if (this.IsProperties)
        return true;

      int thisIndex = -1;
      int total = 0;

      if (this.Parent == null && this.DeType != DocSpec.DeType.Doc)
        throw new Exception("Object of type '" + this.DeType.ToString() + "' named '" + this.Name + "' has a null Parent.");

      for (int i = 0; i < this.Parent.ChildElements.Count; i++)
      {
        DocumentElement de = this.Parent.ChildElements[i];
        if (de.DeType == this.DeType)
        {
          total++;
          if (de.Name == this.Name)
          {
            if (i == this.Parent.ChildElements.Count - 1)
              return true;

            thisIndex = i;
          }
        }
      }

      if (total == 0)
        throw new Exception("Illogical condition: type '" + this.DeType.ToString() + "' named '" + this.Name + "' cannot be found within its parent of type '" +
                            this.Parent.DeType.ToString() + "' named '" + this.Parent.Name + "'.");

      if (thisIndex == total - 1)
        return true;

      return false;
    }

    private bool Get_IsUnique()
    {
      if (this.DeType == DocSpec.DeType.NotSet)
        return false;

      if (this.AbsPath != null)
        if (this.AbsPath == "[loner]")
          return false;

      if (this.IsProperties)
        return true;

      int thisIndex = -1;
      int total = 0;

      if (this.Parent == null  && this.DeType != DocSpec.DeType.Doc)
        throw new Exception("Object of type '" + this.DeType.ToString() + "' named '" + this.Name + "' has a null Parent.");

      for (int i = 0; i < this.Parent.ChildElements.Count; i++)
      {
        DocumentElement de = this.Parent.ChildElements[i];
        if (de.DeType == this.DeType)
        {
          total++;
          if (de.Name == this.Name)
          {
            thisIndex = i;
          }
        }
      }

      if (total == 0)
        throw new Exception("Illogical condition: type '" + this.DeType.ToString() + "' named '" + this.Name + "' cannot be found within its parent of type '" +
                            this.Parent.DeType.ToString() + "' named '" + this.Parent.Name + "'.");

      if (thisIndex == 0 && total == 1)
        return true;

      return false;
    }

    private bool Get_IsLeaf()
    {
      if (this.DeType == DocSpec.DeType.NotSet)
        return false;

      if (this.AbsPath != null)
        if (this.AbsPath == "[loner]")
          return false;

      if (this.IsProperties)
        return true;

      int total = 0;

      if (this.Parent == null  && this.DeType != DocSpec.DeType.Doc)
        throw new Exception("Object of type '" + this.DeType.ToString() + "' named '" + this.Name + "' has a null Parent.");

      for (int i = 0; i < this.Parent.ChildElements.Count; i++)
      {
        DocumentElement de = this.Parent.ChildElements[i];
        if (de.DeType == this.DeType)
        {
          total++;
        }
      }

      if (total == 0)
        throw new Exception("Illogical condition: type '" + this.DeType.ToString() + "' named '" + this.Name + "' cannot be found within its parent of type '" +
                            this.Parent.DeType.ToString() + "' named '" + this.Parent.Name + "'.");

      if (total == 1)
        return true;

      return false;
    }

    private int Get_Level()
    {
      int level = 0;

      DocumentElement de = this;

      while (de.Parent != null)
      {
        level++;
        de = de.Parent;
      }

      return level;
    }

    public bool IsChildOfTag(string tag)
    {
      DocumentElement de = this;

      while (de.Parent != null)
      {
        if (de.Tag == tag)
          return true;
        de = de.Parent;
      }

      return false;
    }

    public bool IsChildOfName(string name)
    {
      DocumentElement de = this;

      while (de.Parent != null)
      {
        if (de.Name == Name)
          return true;
        de = de.Parent;
      }

      return false;
    }

    public SortedList<string, DocumentElement> GetTagList()
    {
      SortedList<string, DocumentElement> tagList = new SortedList<string, DocumentElement>();

      return this.GetTagList(this, tagList);
    }

    public SortedList<string, DocumentElement> GetTagList(DocumentElement de, SortedList<string, DocumentElement> tagList)
    {
      if (!tagList.ContainsKey(de.Tag))
        tagList.Add(de.Tag, de);

      foreach (DocumentElement childDe in de.ChildElements)
        GetTagList(childDe, tagList);

      return tagList;
    }

    public SortedList<string, DocumentElement> GetNameList()
    {
      SortedList<string, DocumentElement> nameList = new SortedList<string, DocumentElement>();

      return this.GetNameList(this, nameList);
    }

    public SortedList<string, DocumentElement> GetNameList(DocumentElement de, SortedList<string, DocumentElement> nameList)
    {
      if (!nameList.ContainsKey(de.Name))
        nameList.Add(de.Name, de);

      foreach (DocumentElement childDe in de.ChildElements)
        GetNameList(childDe, nameList);

      return nameList;
    }

    public DocumentElement GetRoot(DocumentElement de, string rootName)
    {
      if (de == null)
        return null;

      if (de.Name == rootName)
        return de;

      foreach (DocumentElement deChild in de.ChildElements)
      {
        DocumentElement deChildElement = GetRoot(deChild, rootName);
        if (deChildElement.Name == rootName)
          return deChildElement;
      }

      return null;
    }

    private void Set_IsProperties()
    {
      string typeName = this.GetType().Name;

      if (typeName.Contains("Properties"))
        this._isProperties = true;
      else
        this._isProperties = false;
    }

    private void BuildPropertyMatchList()
    {
      this.PropertyMatchList = new List<Match>();
      List<PropertyInfo> piList = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

      foreach (PropertyInfo pi in piList)
      {
        Meta metaAttribute = (Meta)pi.GetCustomAttributes(typeof(Meta), false).FirstOrDefault();
        if (metaAttribute != null)
          this.PropertyMatchList.Add(new Match(pi.Name, metaAttribute.AltName, metaAttribute.Abbr, metaAttribute.IsAttribute));
      }
    }

    public void AggregateProperties()
    {
      if (this.PropertiesAreAggregated)
        return;

      this.AggregatedProperties.RawProperties.Clear();

      if (this.DeType == DeType.Doc)
      {
        if (((DocSpec.Doc)this).DocDefaults != null)
        {
          foreach (DocumentElement de in ((DocSpec.Doc)this).DocDefaults)
          {
            this.AggregatedProperties.RawProperties.AddRange(de.ChildElements);
          }
        }
      }
      else
      {
        if (this.Properties != null)
        {
          this.AggregatedProperties.RawProperties.AddRange(this.Properties.ChildElements);
        }
      }

      this.Doc.RegisterProperties(this.AggregatedProperties.RawProperties);

      AggregatedProperties parentAp = new AggregatedProperties(this, this.Doc);
      if (this.Parent != null)
        parentAp = this.Parent.AggregatedProperties;

      List<PropertyInfo> parentProps = parentAp.GetType().GetProperties().ToList();
      foreach (PropertyInfo parentPi in parentProps)
      {
        if (parentPi.PropertyType.IsSubclassOf(typeof(DocSpec.DocumentElement)))
        {
          string propertyName = parentPi.PropertyType.Name;

          PropertyInfo pi = this.AggregatedProperties.GetType().GetProperty(propertyName);
          if (pi == null)
            throw new Exception("Property name '" + propertyName + "' is not defined as a property in AggregatedProperties object (1).");

          object propertyValue = pi.GetValue(parentAp, null);
          if (propertyValue != null)
            pi.SetValue(this.AggregatedProperties, propertyValue, null);
        }
      }

      foreach (DocumentElement de in this.AggregatedProperties.RawProperties)
      {
        string propertyName = de.GetType().Name;
        PropertyInfo pi = this.AggregatedProperties.GetType().GetProperty(propertyName);
        if (pi == null)
          throw new Exception("Property name '" + propertyName + "' is not defined as a property in AggregatedProperties object (2).");
        pi.SetValue(this.AggregatedProperties, de, null);
      }

      this.PropertiesAreAggregated = true;
    }


    public void InitializeMetrics(DocumentElement de)
    {
      de.RawMetrics.Initialize();
      foreach (DocumentElement childDe in de.ChildElements)
        InitializeMetrics(childDe);
    }

    private string Get_Text()
    {


      return String.Empty;
    }

    public string GetRegionName(int level, string name)
    {
      int seq = 0;
      name = name.Trim();
      string regionName = level.ToString("000") + "-" + name;

      while (this.RegionSet.ContainsKey(regionName))
      {
        seq++;
        regionName = level.ToString("000") + "-" + name +"[" + seq.ToString() + "]";
      }

      return regionName;
    }

    public void TraceMetrics(string label)
    {
      this.Doc.TraceMetrics(label, this);
    }
  }
}
