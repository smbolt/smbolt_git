using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "document", Abbr = "Doc")]
  public class Doc : DocumentElement
  {
    public DocPackage DocPackage {
      get;
      set;
    }
    public SectionSet SectionSet {
      get;
      set;
    }
    public DocResource DocResource {
      get;
      set;
    }
    public DocMap DocMap {
      get;
      set;
    }
    public Dictionary<string, object> Tags {
      get;
      set;
    }
    public SortedList<string, DocumentElement> DeSet {
      get;
      set;
    }
    public int LastIndicatorId {
      get;
      set;
    }
    public string TgtPath {
      get;
      set;
    }

    public List<DocumentElement> DocDefaults {
      get;
      set;
    }
    public List<string> UniquePropertyTypes {
      get;
      set;
    }

    public float WidthFactor {
      get;
      set;
    }
    public float SpaceWidthFactor {
      get;
      set;
    }
    public float LineFactor {
      get;
      set;
    }
    public float Scale {
      get;
      set;
    }

    private bool showProperties = false;
    private int diagnosticsLevel = 0;

    public Point Point {
      get;
      set;
    }
    public System.Drawing.Font Font13b = new System.Drawing.Font("Calibri", 10, FontStyle.Regular);
    private DocNameManager NameManager;
    public XElement WordXml {
      get;
      set;
    }
    public XElement StyleXml {
      get;
      set;
    }

    public XElement XmlMap {
      get;
      set;
    }
    public string Map {
      get;
      set;
    }

    public FontSet FontSet {
      get;
      set;
    }
    public PenSet PenSet {
      get;
      set;
    }
    public PageSet PageSet {
      get;
      set;
    }
    public DrawingMode DrawingMode {
      get;
      set;
    }
    public PointF ShiftOffset {
      get;
      set;
    }

    private StringBuilder _metricsTrace;
    public string MetricsTrace {
      get {
        return Get_MetricsTrace();
      }
    }


    public Doc(DocPackage package, Doc doc, DocumentElement parent)
    {
      this.DocPackage = package;
      XElement docXml = null;
      XElement mapXml = null;

      this._metricsTrace = new StringBuilder();

      this.showProperties = this.DocPackage.DocControl.DebugControl.IncludeProperties;
      this.InDiagnosticsMode = this.DocPackage.DocControl.DebugControl.InDiagnosticsMode;
      this.DiagnosticsLevel = this.DocPackage.DocControl.DebugControl.DiagnosticsLevel;

      this.WidthFactor = this.DocPackage.DocControl.PrintControl.WidthFactor;
      this.SpaceWidthFactor = this.DocPackage.DocControl.PrintControl.SpaceWidthFactor;
      this.LineFactor = this.DocPackage.DocControl.PrintControl.LineFactor;
      this.Scale = this.DocPackage.DocControl.PrintControl.Scale;

      this.Doc = this;
      this.Parent = null;
      this.InDiagnosticsMode = InDiagnosticsMode;
      this.ShiftOffset = new PointF(0, 0);

      this.Initialize();

      if (package.IsAdsdi)
      {
        docXml = package.DocPartsIn["officeDocument"].DocPartXml;
        if (package.DocPartsIn.ContainsKey("docResource"))
          this.DocResource = new DocResource(package.DocPartsIn["docResource"].DocPartXml, this, this);

        mapXml = package.DocPartsIn["map"].DocPartXml;
        this.DocMap = new DocMap(mapXml, this, this);
      }
      else
      {
        docXml = package.DocPartsOut["document"].DocPartXml;
        if (package.DocPartsOut.ContainsKey("styles"))
        {
          this.StyleXml = package.DocPartsOut["styles"].DocPartXml;
          this.DocResource = new DocResource(package.DocPartsOut["styles"].DocPartXml, this, this);
        }
      }

      XElement docDefaults = docXml.GetElement("docDefaults");
      if (docDefaults != null)
        this.LoadDocDefaults(docDefaults);

      this.DrawingMode = DrawingMode.FullDocument;

      this.LoadFromXml(docXml, showProperties);
    }

    public void LoadFromXml(XElement xml, bool showProperties)
    {
      base.Initialize(xml, this, null);

      this.WordXml = xml;

      this.Doc = this;
      this.Parent = null;
      this.Depth = 0;
      this.Tags = new Dictionary<string, object>();
      this.LastIndicatorId = 0;

      if (this.IsAdsdi)
        this.Name = xml.GetRequiredElement("docInfo").GetRequiredElementAttributeValue("name", "val");
      else
        this.Name = "printDoc";

      this.AbsPath = this.Name;

      XElement docDefaults = xml.GetElement("docDefaults");
      if (docDefaults != null)
        this.LoadDocDefaults(docDefaults);

      this.LoadChildren(xml, this, null);

      if (!this.IsAdsdi)
        this.RunPropertyAggregation(this);

      this.DeSet = this.GetNameList();
    }

    private void Initialize()
    {
      this.NameManager = new DocNameManager();
      this.SectionSet = new SectionSet();
      this.FontSet = new FontSet();
      this.PenSet = new PenSet();
      this.PageSet = new PageSet(1.0F);
      this.DocDefaults = new List<DocumentElement>();
      this.UniquePropertyTypes = new List<string>();
      this.Tags = new Dictionary<string, object>();
      this.AbsPath = String.Empty;
      this.RelPath = String.Empty;
      this.TgtPath = String.Empty;
      this.WordXml = new XElement("WordXml");
      this.StyleXml = new XElement("StyleXml");
      this.XmlMap = new XElement("MapNotBuilt");
      this.Map = "Map not built.";
      this.Tag = "doc";

      this.Doc = this;
    }

    public void CreateMaps(bool showProperties)
    {
      this.XmlMap = this.GetTheXmlMap(showProperties);
      this.Map = this.GetTheMap(showProperties);
    }

    public XElement GetTheXmlMap(bool showProperties)
    {
      XElement map = new XElement("DocMap");
      map.Add(this.BuildXmlMap(this, showProperties));
      return map;
    }

    public string GetTheMap(bool showProperties)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("UniquePropertyTypes" + g.crlf);
      this.UniquePropertyTypes.Sort();
      foreach (string propertyType in this.UniquePropertyTypes)
        sb.Append("  " + propertyType + g.crlf);
      sb.Append(g.crlf2);

      sb.Append(this.BuildMap(this, showProperties));

      string map = sb.ToString();
      return map;
    }

    public string GetName(DocumentElement e)
    {
      return this.NameManager.GetNextName(e);
    }

    public string GetRelPath(string absPath)
    {
      if (this.TgtPath.IsBlank())
        return String.Empty;

      string relPath = absPath.Replace(this.TgtPath, "*");

      return relPath;
    }

    public void PrepareForImageGeneration()
    {
      this.PopulateSectionSet();
      this.InitializeMetrics(this);
    }

    public void LoadDocDefaults(XElement xml)
    {
      IEnumerable<XElement> props = xml.Elements();
      foreach (XElement prop in props)
      {
        string elementName = prop.Name.LocalName;
        switch (elementName)
        {
          case "rPrDefault":
            RunProperties rp = new RunProperties(prop, this, this);
            this.DocDefaults.Add(rp);
            break;

          case "pPrDefault":
            ParagraphProperties pp = new ParagraphProperties(prop, this, this);
            this.DocDefaults.Add(pp);
            break;
        }
      }
    }

    public void PopulateSectionSet()
    {
      this.SectionSet = new DocumentElementSet(this, "get[Section()]").ToSectionSet();

      //DocumentElementSet cellSet = new DocumentElementSet(this, "get[TableCell()]");


    }

    public string GetTagReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach (var tag in this.Tags.OrderBy(kvp => kvp.Key))
      {
        string tagReport = String.Empty;
        if (tag.Value.GetType().IsSubclassOf(typeof(DocumentElement)))
        {
          DocumentElement de = (DocumentElement)tag.Value;
          tagReport = de.DeType.ToString().PadTo(20) +
                      de.Level.ToString("000") + "  " +
                      de.IsFirst.ToString().TrimToMax(1) +
                      de.IsLast.ToString().TrimToMax(1) +
                      de.IsUnique.ToString().TrimToMax(1) +
                      de.IsLeaf.ToString().TrimToMax(1) + "   " +
                      de.Name.PadTo(15) +
                      de.AbsPath;
        }
        else
        {
          tagReport = "object is not a descendent of 'DocumentElement'.";
        }

        sb.Append(tag.Key.PadTo(12) + tagReport + g.crlf);
      }

      string report = sb.ToString();
      return report;
    }

    public string GetRegionsReport()
    {
      StringBuilder sb = new StringBuilder();
      bool headerWritten = false;
      int regionNumber = 0;

      foreach (OccupiedRegion or in this.RegionSet.Values)
      {
        DocumentElement de = or.DocumentElement;

        if (!headerWritten)
        {
          sb.Append("Nbr   Name                              X         Y          W         H      Off-X     Off-Y      Tot-W     Tot-H      Type" + g.crlf);
          headerWritten = true;
        }


        sb.Append(regionNumber.ToString("000") + "   " +
                  or.RegionName.PadTo(30) +
                  or.RectF.X.ToString("0000.0000") + "," + or.RectF.Y.ToString("0000.0000") + "  " +
                  or.RectF.Width.ToString("0000.0000") + "," + or.RectF.Height.ToString("0000.0000") + "  " +
                  de.RawMetrics.Offset.X.ToString("0000.0000") + "," + de.RawMetrics.Offset.Y.ToString("0000.0000") + "  " +
                  de.RawMetrics.TotalSize.Width.ToString("0000.0000") + "," + de.RawMetrics.TotalSize.Height.ToString("0000.0000") + "  " +
                  de.DeType.ToString() + g.crlf);
        regionNumber++;
      }


      string report = sb.ToString();
      return report;
    }

    private void RunPropertyAggregation(DocumentElement e)
    {
      e.AggregateProperties();

      foreach (DocumentElement de in e.ChildElements)
        RunPropertyAggregation(de);
    }

    public void RegisterProperties(List<DocumentElement> list)
    {
      foreach (DocumentElement de in list)
      {
        string typeName = de.DeType.ToString();
        if (!this.UniquePropertyTypes.Contains(typeName))
          this.UniquePropertyTypes.Add(typeName);
      }
    }

    public Font GetFont(AggregatedProperties ap)
    {
      Font f = null;

      float fontSize = (float) Convert.ToInt32(ap.FontSize.Val) / 2;
      string styleId = ap.ParagraphStyleId.Val;
      //Style style = this.DocResource.StyleSet


      return f;
    }

    public void TraceMetrics(string label, DocumentElement de)
    {
      _metricsTrace.Append(label.PadTo(15) + " " + de.Name.PadTo(15) + " " +
                           de.RawMetrics.CurrentHorizontalOffset.X.ToString("0000.000") + " " + de.RawMetrics.CurrentOffset.Y.ToString("0000.00") + g.crlf);
    }

    public string Get_MetricsTrace()
    {
      string metricsTrace = this._metricsTrace.ToString();
      return metricsTrace;
    }

  }
}
