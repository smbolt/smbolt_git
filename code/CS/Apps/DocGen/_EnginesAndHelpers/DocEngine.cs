using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Packaging;

//using Ap = DocumentFormat.OpenXml.ExtendedProperties;
//using Vt = DocumentFormat.OpenXml.VariantTypes;
//using A = DocumentFormat.OpenXml.Drawing;
//using M = DocumentFormat.OpenXml.Math;
//using Ovml = DocumentFormat.OpenXml.Vml.Office;
//using V = DocumentFormat.OpenXml.Vml;

//using Word = Microsoft.Office.Interop.Word;

//using Rs = Org.BuildMyResume.Domain.Models.Resume;
using Ds = Org.DocGen.DocSpec;
using Org.GS;

namespace Org.DocGen
{
  public class DocEngine : IDisposable
  {
    public Ds.Doc Doc {
      get;
      set;
    }
    public Ds.DocPackage DocPackage {
      get;
      set;
    }
    private bool showProperties = false;
    private bool inDiagnosticsMode = false;

    public string ValidationReport = String.Empty;
    public XElement DocumentXml = new XElement("Document");
    public XElement StylesXml = new XElement("Styles");
    public string DocMap {
      get;
      set;
    }

    public string GenerateDocument(Ds.DocPackage docPackage, float widthFactor, float spaceWidthFactor, float lineFactor, float scale)
    {
      try
      {
        //DocHelper.ResetTagId();
        //this.DocPackage = docPackage;
        //showProperties = this.DocPackage.DocControl.DebugControl.IncludeProperties;
        //inDiagnosticsMode = this.DocPackage.DocControl.DebugControl.InDiagnosticsMode;

        //this.Doc = new Ds.Doc(this.DocPackage, null, null);

        //this.DocMap = this.Doc.GetXmlMap(showProperties).ToString();

        //if (docPackage.DocControl.PrintControl.CreateDocument)
        //{
        //    using (WordprocessingDocument document = WordprocessingDocument.Create(this.DocPackage.DocControl.DocInfo.FullDocxPath, WordprocessingDocumentType.Document))
        //    {
        //        //XQ : Find out what is needed or not needed here...
        //        ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
        //        DocPartHelper.GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1, this.DocPackage.DocControl.DocInfo);

        //        this.AddMainDocumentPart(document);

        //        StylesWithEffectsPart stylesWithEffectsPart1 = document.MainDocumentPart.AddNewPart<StylesWithEffectsPart>("rId3");
        //        DocPartHelper.GenerateStylesWithEffectsPart1Content(stylesWithEffectsPart1);

        //        EndnotesPart endnotesPart1 = document.MainDocumentPart.AddNewPart<EndnotesPart>("rId7");
        //        DocPartHelper.GenerateEndnotesPart1Content(endnotesPart1);

        //        NumberingDefinitionsPart numberingDefinitionsPart1 = document.MainDocumentPart.AddNewPart<NumberingDefinitionsPart>("rId1");
        //        DocPartHelper.GenerateNumberingDefinitionsPart1Content(numberingDefinitionsPart1);

        //        FootnotesPart footnotesPart1 = document.MainDocumentPart.AddNewPart<FootnotesPart>("rId6");
        //        DocPartHelper.GenerateFootnotesPart1Content(footnotesPart1);

        //        WebSettingsPart webSettingsPart1 = document.MainDocumentPart.AddNewPart<WebSettingsPart>("rId5");
        //        DocPartHelper.GenerateWebSettingsPart1Content(webSettingsPart1);

        //        ThemePart themePart1 = document.MainDocumentPart.AddNewPart<ThemePart>("rId10");
        //        DocPartHelper.GenerateThemePart1Content(themePart1);

        //        DocumentSettingsPart documentSettingsPart1 = document.MainDocumentPart.AddNewPart<DocumentSettingsPart>("rId4");
        //        DocPartHelper.GenerateDocumentSettingsPart1Content(documentSettingsPart1);

        //        FontTablePart fontTablePart1 = document.MainDocumentPart.AddNewPart<FontTablePart>("rId9");
        //        DocPartHelper.GenerateFontTablePart1Content(fontTablePart1);

        //        document.MainDocumentPart.AddHyperlinkRelationship(new System.Uri("mailto:smbolt@sbcglobal.net", System.UriKind.Absolute), true, "rId8");

        //        document.MainDocumentPart.Document.Save();

        //        this.ValidationReport = DocHelper.ValidateDocument(document.MainDocumentPart.Document);
        //        this.DocumentXml = DocHelper.GetDocumentXml(document.MainDocumentPart.Document);
        //        this.StylesXml = DocHelper.GetStylesXml(document.MainDocumentPart.Document);

        //        document.Close();

        //        docPackage.DocIn = this.Doc;

        //        return this.ValidationReport;
        //    }
        //}

        return String.Empty;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to generate document '" + this.DocPackage.DocControl.DocInfo.PackageName + "'.  See inner exception for details.", ex);
      }
    }

    //private void AddMainDocumentPart(WordprocessingDocument document)
    //{
    //    document.AddMainDocumentPart();

    //    StyleDefinitionsPart styleDefinitionsPart1 = document.MainDocumentPart.AddNewPart<StyleDefinitionsPart>("rId2");
    //    DocPartHelper.GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);
    //    AddStylesToDocument(styleDefinitionsPart1);

    //    Body body = (Body)BuildBody(this.Doc);
    //    body.AddTag(this.Doc, String.Empty);

    //    document.MainDocumentPart.Document = new Document(body);
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
    //    document.MainDocumentPart.Document.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

    //    AddContent(body, this.Doc);
    //}

    //private OpenXmlCompositeElement BuildBody(DocSpec.DocumentElement doc)
    //{
    //    DocSpec.DocumentElement bodySpec = doc.ChildElements.First();
    //    OpenXmlCompositeElement e = BuildElement(bodySpec);

    //    foreach (DocSpec.DocumentElement de in bodySpec.ChildElements)
    //        e.Append(BuildElement(de));

    //    return e;
    //}

    //private OpenXmlCompositeElement BuildElement(DocSpec.DocumentElement d)
    //{
    //    OpenXmlCompositeElement o = null;

    //    switch (d.DeType)
    //    {
    //        case Ds.DeType.Body:
    //            o = new Body();
    //            o.AddTag(d.Doc, d.Tag);
    //            o.AddAttr("class", d.Class);
    //            o.AddAttr("name", d.Name);
    //            DocSpec.Section section = (DocSpec.Section) d.ChildElements.First();
    //            if (section.Properties != null)
    //            {
    //                SectionProperties sp = DocHelper.CreateSectionProperties((Ds.SectionProperties)section.Properties);
    //                sp.AddTag(d.Doc, d.Tag);
    //                o.Append(sp);
    //            }
    //            break;

    //        case Ds.DeType.Table:
    //            o = new Table();
    //            o.AddTag(d.Doc, d.Tag);
    //            o.AddAttr("class", d.Class);
    //            o.AddAttr("name", d.Name);
    //            if (d.Properties != null)
    //            {
    //                TableProperties tp = DocHelper.GetTableProperties((Ds.TableProperties)d.Properties);
    //                o.Append(tp);
    //            }
    //            break;

    //        case Ds.DeType.TableRow:
    //            o = new TableRow();
    //            o.AddTag(d.Doc, d.Tag);
    //            o.AddAttr("class", d.Class);
    //            o.AddAttr("name", d.Name);
    //            break;

    //        case Ds.DeType.TableCell:
    //            o = new TableCell();
    //            o.AddTag(d.Doc, d.Tag);
    //            o.AddAttr("class", d.Class);
    //            o.AddAttr("name", d.Name);
    //            if (d.Properties != null)
    //            {
    //                TableCellProperties cp = DocHelper.CreateTableCellProperties((Ds.TableCellProperties)d.Properties);
    //                o.Append(cp);
    //            }
    //            break;

    //        case Ds.DeType.Paragraph:
    //            o = new Paragraph();
    //            o.AddTag(d.Doc, d.Tag);
    //            o.AddAttr("class", d.Class);
    //            o.AddAttr("name", d.Name);
    //            if (d.Properties != null)
    //            {
    //                ParagraphProperties pp = DocHelper.CreateParagraphProperties((Ds.ParagraphProperties)d.Properties);
    //                o.Append(pp);
    //            }

    //            if (d.ContentValue.IsNotBlank())
    //            {
    //                Ds.Style style = d.Doc.DocResource.StyleSet.Styles[d.StyleId];
    //                if (style.ParagraphProperties.ChildElements.Count > 0)
    //                    o.Append(DocHelper.CreateParagraphProperties(style.ParagraphProperties));

    //                Run defaultRun = new Run();
    //                defaultRun.AddTag(d.Doc, d.Tag);

    //                if (d.StyleId.IsNotBlank())
    //                {
    //                    if (style.RunProperties.ChildElements.Count > 0)
    //                    {
    //                        RunProperties rp = DocHelper.CreateRunProperties(style.RunProperties);
    //                        defaultRun.Append(rp);
    //                    }
    //                }

    //                Text defaultText = new Text(d.ContentValue);
    //                defaultText.AddTag(d.Doc, d.Tag);

    //                defaultRun.Append(defaultText);
    //                o.Append(defaultRun);
    //            }

    //            break;
    //    }

    //    if (d.DeType == Ds.DeType.Body)
    //        d = d.ChildElements.First();

    //    if (o != null)
    //    {
    //        foreach (DocSpec.DocumentElement de in d.ChildElements)
    //        {
    //            o.Append(BuildElement(de));
    //        }

    //        if (o.GetType().Name == "TableCell")
    //        {
    //            if (o.ChildElements.OfType<Paragraph>().Count() == 0)
    //                o.Append(new Paragraph());
    //        }
    //    }

    //    return (OpenXmlCompositeElement)o;
    //}

    //private void AddContent(Body body, Ds.Doc doc)
    //{
    //    DocPackage.ContentItemSet.Clear();

    //    Rs.ResumeEngine resumeEngine = new Rs.ResumeEngine();
    //    Rs.ResumeSet rs = resumeEngine.BuildResumeSetFromCode();
    //    Rs.Resume resume = rs.Resumes.Values.FirstOrDefault();

    //    IEnumerable<Ds.MapEntry> mapEntries = doc.DocMap.MapEntrySet.ChildElements.OfType<Ds.MapEntry>();
    //    foreach (Ds.MapEntry mapEntry in mapEntries)
    //    {
    //        foreach (Ds.DocumentElement de in mapEntry.ChildElements)
    //            ResolveContent(de, resume);

    //        string tag = mapEntry.Tag;

    //        if (doc.Tags.ContainsKey(tag))
    //        {
    //            OpenXmlCompositeElement e = (OpenXmlCompositeElement) doc.Tags[tag];

    //            if (mapEntry.ChildElements.Count > 0)
    //                e.RemoveLoneEmptyParagraph();

    //            foreach (Ds.DocumentElement de in mapEntry.ChildElements)
    //            {
    //                OpenXmlElement oxElement = this.BuildElement(de);
    //                e.Append(oxElement);
    //            }
    //        }
    //    }


    //    //string targetTag = "cTag1";
    //    //if (doc.Tags.ContainsKey(targetTag))
    //    //{
    //    //    OpenXmlCompositeElement e = (OpenXmlCompositeElement) doc.Tags[targetTag];
    //    //    e.LastChild.Remove();
    //    //    Ds.OxProfile profile = new Ds.OxProfile(doc, resume.Owner, "format");
    //    //    ((OpenXmlCompositeElement)e).Append(profile);
    //    //}

    //    //targetTag = "cTag2";
    //    //if (doc.Tags.ContainsKey(targetTag))
    //    //{
    //    //    OpenXmlCompositeElement e = (OpenXmlCompositeElement) doc.Tags[targetTag];
    //    //    e.LastChild.Remove();

    //    //    Paragraph par2 = new Paragraph();
    //    //    par2.AddTag(doc, String.Empty);
    //    //    Run run2 = new Run();
    //    //    run2.AddTag(doc, String.Empty);
    //    //    Text t2 = new Text("Highly accomplished senior technology professional with deep understanding and experience in a wide array of technologies and a long " +
    //    //                       "and successful career developing technology solutions to meet critical business needs.");
    //    //    ParagraphProperties pp2 = new ParagraphProperties();
    //    //    pp2.SpacingBetweenLines = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
    //    //    pp2.Append(new Justification() { Val = JustificationValues.Left });
    //    //    pp2.ParagraphStyleId = new ParagraphStyleId() { Val = "Default" };
    //    //    par2.Append(pp2);
    //    //    run2.Append(t2);
    //    //    par2.Append(run2);
    //    //    ((OpenXmlCompositeElement)e).Append(par2);

    //    //    Paragraph par4 = new Paragraph();
    //    //    par4.AddTag(doc, String.Empty);
    //    //    ((OpenXmlCompositeElement)e).Append(par4);

    //    //    Paragraph par3 = new Paragraph();
    //    //    par3.AddTag(doc, String.Empty);
    //    //    Run run3 = new Run();
    //    //    run3.AddTag(doc, String.Empty);
    //    //    Text t3 = new Text("This is the second paragraph within the same table cell. The paragraph should wrap from line to line and be justified according to the " +
    //    //                       "specificaiton in the paragraph properties or default to a higher level default.  Maybe we should add a default justification or simply have a "+
    //    //                       "hard-coded default set to left justification. We probably need several document defaults like this to have a value when no document default or paragraph propert exists.");
    //    //    ParagraphProperties pp3 = new ParagraphProperties();
    //    //    pp3.SpacingBetweenLines = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
    //    //    pp3.Append(new Justification() { Val = JustificationValues.Center });
    //    //    pp3.ParagraphStyleId = new ParagraphStyleId() { Val = "Default" };
    //    //    par3.Append(pp3);
    //    //    run3.Append(t3);
    //    //    par3.Append(run3);
    //    //    ((OpenXmlCompositeElement)e).Append(par3);

    //    //    Paragraph par5 = new Paragraph();
    //    //    par5.AddTag(doc, String.Empty);
    //    //    ((OpenXmlCompositeElement)e).Append(par5);

    //    //    Paragraph par6 = new Paragraph();
    //    //    par6.AddTag(doc, String.Empty);
    //    //    Run run6 = new Run();
    //    //    run6.AddTag(doc, String.Empty);
    //    //    Text t6 = new Text("This is the third paragraph within the same table cell. The paragraph should wrap from line to line and be justified according to the " +
    //    //                       "specificaiton in the paragraph properties or default to a higher level default. Maybe we should add a default justification or simply have a "+
    //    //                       "hard-coded default set to left justification. We probably need several document defaults like this to have a value when no document default or paragraph propert exists.");
    //    //    ParagraphProperties pp6 = new ParagraphProperties();
    //    //    pp6.SpacingBetweenLines = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
    //    //    pp6.Append(new Justification() { Val = JustificationValues.Right });
    //    //    pp6.ParagraphStyleId = new ParagraphStyleId() { Val = "Default" };
    //    //    par6.Append(pp6);
    //    //    run6.Append(t6);
    //    //    par6.Append(run6);
    //    //    ((OpenXmlCompositeElement)e).Append(par6);

    //    //    Paragraph par7 = new Paragraph();
    //    //    par7.AddTag(doc, String.Empty);
    //    //    ((OpenXmlCompositeElement)e).Append(par7);

    //    //    Paragraph par8 = new Paragraph();
    //    //    par8.AddTag(doc, String.Empty);
    //    //    Run run8 = new Run();
    //    //    run8.AddTag(doc, String.Empty);
    //    //    Text t8 = new Text("This is the third paragraph within the same table cell. The paragraph should wrap from line to line and be justified according to the " +
    //    //                       "specificaiton in the paragraph properties or default to a higher level default.  Maybe we should add a default justification or simply have a " +
    //    //                       "hard-coded default set to left justification. We probably need several document defaults like this to have a value when no document default or paragraph propert exists.");
    //    //    ParagraphProperties pp8 = new ParagraphProperties();
    //    //    pp8.SpacingBetweenLines = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
    //    //    pp8.Append(new Justification() { Val = JustificationValues.Both });
    //    //    pp8.ParagraphStyleId = new ParagraphStyleId() { Val = "Default" };
    //    //    par8.Append(pp8);
    //    //    run8.Append(t8);
    //    //    par8.Append(run8);
    //    //    ((OpenXmlCompositeElement)e).Append(par8);
    //    //}
    //}

    //public void ResolveContent(Ds.DocumentElement de, Rs.Resume resume)
    //{
    //    de.ContentValue = resume.GetContent(de.ContentQuery);
    //    if (!DocPackage.ContentItemSet.ContainsKey(de.Name))
    //        DocPackage.ContentItemSet.Add(de.Name, new Ds.ContentItem(de.Name, de.ContentQuery, de.ContentValue));
    //    foreach (Ds.DocumentElement e in de.ChildElements)
    //        this.ResolveContent(e, resume);
    //}

    //private void AddStylesToDocument(StyleDefinitionsPart stylePart)
    //{
    //    try
    //    {
    //        Style s = new Style();

    //        string valueAtError = String.Empty;
    //        foreach (Ds.Style styleSpec in this.Doc.DocResource.StyleSet.Styles.Values)
    //        {
    //            s = new Style();
    //            s.StyleId = styleSpec.StyleId;
    //            string styleValue = styleSpec.Type.ToString();
    //            if (styleValue == "DocDefaults")
    //                s.Type = new EnumValue<StyleValues>(StyleValues.Paragraph);
    //            else
    //                s.Type = new EnumValue<StyleValues>((StyleValues)Enum.Parse(typeof(StyleValues), styleValue));

    //            s.Append(new Name() { Val = styleSpec.Name });
    //            s.Append(new BasedOn() { Val = styleSpec.BasedOn });
    //            s.Append(new NextParagraphStyle() { Val = styleSpec.NextParagraphStyle });

    //            s.Default = OnOffValue.FromBoolean(styleSpec.Default);

    //            if (styleSpec.UIPriority.IsNumeric())
    //                s.UIPriority = new UIPriority() { Val = Convert.ToInt32(styleSpec.UIPriority) };

    //            s.SemiHidden = new SemiHidden();
    //            if (styleSpec.SemiHidden)
    //                s.SemiHidden.Val = OnOffOnlyValues.On;
    //            else
    //                s.SemiHidden.Val = OnOffOnlyValues.Off;

    //            s.UnhideWhenUsed = new UnhideWhenUsed();
    //            if (styleSpec.UnhideWhenUsed)
    //                s.UnhideWhenUsed.Val = OnOffOnlyValues.On;
    //            else
    //                s.UnhideWhenUsed.Val = OnOffOnlyValues.Off;

    //            s.PrimaryStyle = new PrimaryStyle();
    //            if (styleSpec.PrimaryStyle)
    //                s.PrimaryStyle.Val = OnOffOnlyValues.On;
    //            else
    //                s.PrimaryStyle.Val = OnOffOnlyValues.Off;

    //            if (styleSpec.ParagraphProperties != null)
    //            {
    //                if (styleSpec.ParagraphProperties.ChildElements.Count > 0)
    //                {
    //                    ParagraphProperties pp = DocHelper.CreateParagraphProperties(styleSpec.ParagraphProperties);
    //                    s.Append(pp);
    //                }
    //            }

    //            if (styleSpec.RunProperties != null)
    //            {
    //                if (styleSpec.RunProperties.ChildElements.Count > 0)
    //                {
    //                    RunProperties rp = DocHelper.CreateRunProperties(styleSpec.RunProperties);
    //                    s.Append(rp);
    //                }
    //            }

    //            if (styleSpec.TableProperties != null)
    //            {
    //                if (styleSpec.TableProperties.ChildElements.Count > 0)
    //                {
    //                    TableProperties tp = DocHelper.CreateTableProperties(styleSpec.TableProperties);
    //                    s.Append(tp);
    //                }
    //            }

    //            if (s.StyleId == "DocDefault")
    //            {
    //                stylePart.Styles.DocDefaults = new DocDefaults();
    //                ParagraphProperties parPr = s.OfType<ParagraphProperties>().FirstOrDefault();
    //                if (parPr != null)
    //                {
    //                    stylePart.Styles.DocDefaults.ParagraphPropertiesDefault = new ParagraphPropertiesDefault();
    //                    foreach (OpenXmlElement e in parPr)
    //                        stylePart.Styles.DocDefaults.ParagraphPropertiesDefault.Append(e.CloneNode(true));
    //                }

    //                RunProperties rPr = s.OfType<RunProperties>().FirstOrDefault();
    //                if (rPr != null)
    //                {
    //                    stylePart.Styles.DocDefaults.RunPropertiesDefault = new RunPropertiesDefault();
    //                    foreach (OpenXmlElement e in rPr)
    //                        stylePart.Styles.DocDefaults.RunPropertiesDefault.Append(e.CloneNode(true));
    //                }
    //            }
    //            else
    //            {
    //                valueAtError = stylePart.Styles.Count().ToString();
    //                stylePart.Styles.Append(s);
    //            }
    //        }

    //        stylePart.Styles.Save();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void SetPackageProperties(OpenXmlPackage document)
    //{
    //    document.PackageProperties.Creator = this.DocPackage.DocControl.DocInfo.CompanyName;
    //    document.PackageProperties.Revision = "3";
    //    document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2012-10-15T11:47:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
    //    document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2012-10-15T11:53:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
    //    document.PackageProperties.LastModifiedBy = this.DocPackage.DocControl.DocInfo.CompanyName;
    //    document.PackageProperties.LastPrinted = System.Xml.XmlConvert.ToDateTime("2012-10-15T11:50:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
    //}

    //public void SendDocumentToPrinter(string path)
    //{
    //    Microsoft.Office.Interop.Word.Application wordApp;
    //    wordApp = new Microsoft.Office.Interop.Word.Application();
    //    wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
    //    object filename = path;
    //    object missingValue = Type.Missing;

    //    // Using OpenOld so as to be compatible with other versions of Word
    //    Word.Document document = wordApp.Documents.OpenOld(ref filename,
    //                                               ref missingValue, ref missingValue, ref missingValue,
    //                                               ref missingValue, ref missingValue, ref missingValue,
    //                                               ref missingValue, ref missingValue, ref missingValue);

    //    // Set the active printer
    //    wordApp.ActivePrinter = "Miraplacid Publisher";

    //    object myTrue = true; // Print in background
    //    object myFalse = false;

    //    // Using PrintOutOld to be version independent
    //    wordApp.ActiveDocument.PrintOutOld(ref myTrue, ref myFalse, ref missingValue, ref missingValue,
    //                                       ref missingValue, ref missingValue, ref missingValue, ref missingValue,
    //                                       ref missingValue, ref missingValue, ref myFalse, ref missingValue,
    //                                       ref missingValue, ref missingValue);

    //    ((Microsoft.Office.Interop.Word._Document)document).Close(ref missingValue, ref missingValue, ref missingValue);

    //    // Make sure all of the documents are gone from the queue
    //    while (wordApp.BackgroundPrintingStatus > 0)
    //    {
    //        System.Threading.Thread.Sleep(250);
    //    }

    //    ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref missingValue, ref missingValue, ref missingValue);
    //}

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        // free managed resources
        //if (managedResource != null)
        //{
        //    managedResource.Dispose();
        //    managedResource = null;
        //}
      }
      // free native resources if there are any.
      //if (nativeResource != IntPtr.Zero)
      //{
      //    Marshal.FreeHGlobal(nativeResource);
      //    nativeResource = IntPtr.Zero;
      //}
    }

  }
}
