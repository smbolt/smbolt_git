using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Validation;

//using Word = Microsoft.Office.Interop.Word;

using Org.GS;
using Ds = Org.DocGen.DocSpec;

namespace Org.DocGen
{
    public static class DocHelper
    {
        public static bool IsInitialized = Initialize();
        public static Assembly OpenXmlAssembly { get; set; }
        private static string OpenXmlNamespace = "DocumentFormat.OpenXml.Wordprocessing.";
        private static int tagId;

        //private static object CreateSectionProperties_LockObject = new object();
        //public static SectionProperties CreateSectionProperties(DocSpec.SectionProperties propSpec)
        //{
        //    lock (CreateTableCellProperties_LockObject)
        //    {
        //        SectionProperties sectionProperties = new SectionProperties();

        //        foreach (Ds.DocumentElement e in propSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                sectionProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }
                
        //        return sectionProperties;
        //    }
        //}

        //private static object GetTableProperties_LockObject = new object();
        //public static TableProperties GetTableProperties(DocSpec.TableProperties tablePropertiesSpec)
        //{
        //    lock (GetTableProperties_LockObject)
        //    {
        //        TableProperties tableProperties = new TableProperties();

        //        foreach (Ds.DocumentElement e in tablePropertiesSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                tableProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    case Ds.DeType.TableBorders:
        //                        Ds.TableBorders tb = (Ds.TableBorders)e;
        //                        TableBorders tableBorders = new TableBorders();
        //                        foreach (Ds.Border usedBorder in tb.UsedBorders)
        //                            tableBorders.Append(AutoMapToOpenXml(usedBorder));
        //                        tableProperties.Append(tableBorders);
        //                        break;

        //                    case Ds.DeType.TableCellMarginDefault:
        //                        Ds.TableCellMarginDefault tcmd = (Ds.TableCellMarginDefault)e;
        //                        TableCellMarginDefault tableCellDefaultMargins = new TableCellMarginDefault();
        //                        foreach (Ds.Margin usedMargin in tcmd.UsedMargins)
        //                            tableCellDefaultMargins.Append(AutoMapToOpenXml(usedMargin));
        //                        tableProperties.Append(tableCellDefaultMargins);
        //                        break;

        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }

        //        return tableProperties;
        //    }
        //}

        //private static object CreateTableProperties_LockObject = new object();
        //public static TableProperties CreateTableProperties(DocSpec.TableProperties propSpec)
        //{
        //    lock(CreateTableProperties_LockObject)
        //    {
        //        TableProperties tableProperties = new TableProperties();

        //        foreach (Ds.DocumentElement e in propSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                tableProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    case Ds.DeType.TableCellMargin:
        //                        Ds.TableCellMargin tcm = (Ds.TableCellMargin)e;
        //                        TableCellMargin tableCellMargin = new TableCellMargin();

        //                        foreach (Ds.Margin usedMargin in tcm.UsedMargins)
        //                            tableCellMargin.Append(AutoMapToOpenXml(usedMargin));

        //                        tableProperties.Append(tableCellMargin);
        //                        break;

        //                    case Ds.DeType.TableCellMarginDefault:
        //                        Ds.TableCellMarginDefault tcmd = (Ds.TableCellMarginDefault)e;
        //                        TableCellMarginDefault tableCellMarginDefault = new TableCellMarginDefault();

        //                        foreach (Ds.Margin usedMargin in tcmd.UsedMargins)
        //                            tableCellMarginDefault.Append(AutoMapToOpenXml(usedMargin));

        //                        tableProperties.Append(tableCellMarginDefault);
        //                        break;

        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }                

        //        return tableProperties;
        //    }
        //}

        //private static object CreateTableCellProperties_LockObject = new object();
        //public static TableCellProperties CreateTableCellProperties(DocSpec.TableCellProperties propSpec)
        //{
        //    lock(CreateTableCellProperties_LockObject)
        //    {
        //        TableCellProperties tableCellProperties = new TableCellProperties();

        //        foreach (Ds.DocumentElement e in propSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                tableCellProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    case Ds.DeType.TableCellBorders:
        //                        Ds.TableCellBorders tcb = (Ds.TableCellBorders)e;
        //                        TableCellBorders tableCellBorders = new TableCellBorders();

        //                        foreach (Ds.Border usedBorder in tcb.UsedBorders)
        //                            tableCellBorders.Append(AutoMapToOpenXml(usedBorder));

        //                        tableCellProperties.Append(tableCellBorders);
        //                        break;

        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }                

        //        return tableCellProperties;
        //    }
        //}

        //private static object CreateParagraphProperties_LockObject = new object();
        //public static ParagraphProperties CreateParagraphProperties(DocSpec.ParagraphProperties propSpec)
        //{
        //    lock (CreateParagraphProperties_LockObject)
        //    {
        //        ParagraphProperties paragraphProperties = new ParagraphProperties();

        //        foreach (Ds.DocumentElement e in propSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                paragraphProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }

        //        return paragraphProperties;
        //    }
        //}

        //private static object CreateRunProperties_LockObject = new object();
        //public static RunProperties CreateRunProperties(DocSpec.RunProperties propSpec)
        //{
        //    lock (CreateRunProperties_LockObject)
        //    {
        //        RunProperties runProperties = new RunProperties();

        //        foreach (Ds.DocumentElement e in propSpec.ChildElements)
        //        {
        //            if (e.IsAutoMappable)
        //            {
        //                runProperties.Append(AutoMapToOpenXml(e));
        //            }
        //            else
        //            {
        //                switch (e.DeType)
        //                {
        //                    default:
        //                        throw new Exception("No OpenXml object creation code exists for type '" + e.DeType.ToString() + "'.");
        //                }
        //            }
        //        }

        //        return runProperties;
        //    }
        //}

        //private static object AutoMapToOpenXml_LockObject = new object();
        //public static OpenXmlElement AutoMapToOpenXml(Ds.DocumentElement e)
        //{
        //    lock (AutoMapToOpenXml_LockObject)
        //    {
        //        string className = e.GetType().Name;
        //        if (e.OpenXmlClassName.IsNotBlank())
        //            className = e.OpenXmlClassName;
        //        string fullTypeName = OpenXmlNamespace + className;
        //        Type t = OpenXmlAssembly.GetType(fullTypeName);
        //        if (t == null)
        //            throw new Exception("Unable to retrieve type '" + fullTypeName + "' from Assembly '" + OpenXmlAssembly.FullName + "'.");

        //        OpenXmlElement ox = (OpenXmlElement)Activator.CreateInstance(t);
        //        if (ox == null)
        //            throw new Exception("Unable to create type '" + fullTypeName + "'.");

        //        foreach (Ds.Match m in e.PropertyMatchList)
        //        {
        //            string sourcePropertyName = m.PropertyName;
        //            string destPropertyName = m.AltName.IsBlank() ? m.PropertyName : m.AltName;

        //            PropertyInfo piSource = e.GetType().GetProperty(sourcePropertyName);
        //            if (piSource == null)
        //                throw new Exception("Cannot find source property.");

        //            PropertyInfo piDest = ox.GetType().GetProperty(destPropertyName);
        //            if (piDest == null)
        //                throw new Exception("Cannot find destination property.");

        //            Ds.PropertyTypeInfo sourceType = GetPropertyType(piSource);
        //            Ds.PropertyTypeInfo destType = GetPropertyType(piDest);

        //            object sourceValue = piSource.GetValue(e, null);

        //            if (sourceValue == null)
        //                piDest.SetValue(ox, null, null);
        //            else
        //            {
        //                if (destType.IsSameAs(sourceType))
        //                    piDest.SetValue(ox, sourceValue, null);
        //                else
        //                    piDest.SetValue(ox, TranslateType(sourceType, destType, sourceValue, Ds.TypeConversionDirection.AdsdiToOpenXml), null);
        //            }
        //        }
                

        //        return ox;
        //    }
        //}
        //private static object AutoMapToClone_LockObject = new object();
        //public static Ds.DocumentElement AutoMapToClone(Ds.DocumentElement e)
        //{
        //    lock (AutoMapToClone_LockObject)
        //    {
        //        Type t = e.GetType();

        //        Ds.DocumentElement de = (Ds.DocumentElement)Activator.CreateInstance(t);
        //        if (de == null)
        //            throw new Exception("Unable to create type '" + t.Name + "'.");

        //        foreach (Ds.Match m in e.PropertyMatchList)
        //        {
        //            string sourcePropertyName = m.PropertyName;
        //            string destPropertyName = m.PropertyName;
        //            PropertyInfo piSource = e.GetType().GetProperty(sourcePropertyName);
        //            PropertyInfo piDest = de.GetType().GetProperty(destPropertyName);
        //            object sourceValue = piSource.GetValue(e, null);
        //            piDest.SetValue(de, sourceValue, null);
        //        }

        //        de.Doc = e.Doc;
        //        de.Name = e.Doc.GetName(de);
        //        de.AbsPath = "[loner]";
        //        de.RelPath = "[loner]";

        //        de.DeType = (Ds.DeType)g.ToEnum<Ds.DeType>(de.GetType().Name, DocSpec.DeType.NotSet);
        //        if (de.DeType == DocSpec.DeType.NotSet)
        //            throw new Exception("Type '" + de.GetType().Name + "' is not defined in the DeType enumeration.");

        //        de.IsUsed = true;
        //        de.Tag = String.Empty;

        //        return de;
        //    }
        //}

        //private static object GetPropertyType_LockObject = new object();
        //private static Ds.PropertyTypeInfo GetPropertyType(PropertyInfo pi)
        //{
        //    lock (GetPropertyType_LockObject)
        //    {
        //        bool isNullable = false;
        //        bool isEnumValue = false;

        //        Type type = null;
        //        if (pi.PropertyType.IsGenericType)
        //        {
        //            string underlyingTypeName = pi.PropertyType.UnderlyingSystemType.Name;
        //            switch (underlyingTypeName)
        //            {
        //                case "EnumValue`1":
        //                    type = pi.PropertyType.UnderlyingSystemType.GetGenericArguments().FirstOrDefault();
        //                    isEnumValue = true;
        //                    break;

        //                case "Nullable`1":
        //                    isNullable = true;
        //                    type = Nullable.GetUnderlyingType(pi.PropertyType);
        //                    break;
        //            }
        //        }
        //        else
        //            type = pi.PropertyType;

        //        if (type == null)
        //            throw new Exception("Unable to determine property type for property '" + pi.Name + "' of object of type '" + pi.DeclaringType.Name + "'.");

        //        return new Ds.PropertyTypeInfo(isNullable, isEnumValue, type);
        //    }
        //}

        //private static object TranslateType_LockObject = new object();
        //private static object TranslateType(Ds.PropertyTypeInfo sourceType, Ds.PropertyTypeInfo destType, object sourceValue, Ds.TypeConversionDirection conversionDirection)
        //{
        //    lock (TranslateType_LockObject)
        //    {
        //        if (sourceValue == null)
        //            return null;

        //        string convType = sourceType.NativeType.Name + "-" + destType.NativeType.Name;

        //        switch (convType)
        //        {
        //            case "Int32-Int32Value":
        //                return Int32Value.FromInt32(Convert.ToInt32(sourceValue));

        //            case "UInt32-UInt32Value":
        //                return UInt32Value.FromUInt32(Convert.ToUInt32(sourceValue));

        //            case "Boolean-OnOffValue":
        //                return OnOffValue.FromBoolean(Convert.ToBoolean(sourceValue));

        //            case "PageOrientationValues-PageOrientationValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        PageOrientationValues pov = (PageOrientationValues)Enum.Parse(typeof(PageOrientationValues), sourceValue.ToString().CaptializeFirstLetter());
        //                        EnumValue<PageOrientationValues> epov = new EnumValue<PageOrientationValues>(pov);
        //                        return epov;
        //                    }
        //                }
        //                break;

        //            case "TableWidthUnitValues-TableWidthUnitValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        TableWidthUnitValues twuv = (TableWidthUnitValues)Enum.Parse(typeof(TableWidthUnitValues), sourceValue.ToString());
        //                        EnumValue<TableWidthUnitValues> etwuv = new EnumValue<TableWidthUnitValues>(twuv);
        //                        return etwuv;
        //                    }
        //                }
        //                break;

        //            case "TableWidthUnitValues-TableWidthValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        TableWidthValues twv = (TableWidthValues)Enum.Parse(typeof(TableWidthValues), sourceValue.ToString());
        //                        EnumValue<TableWidthValues> etwv = new EnumValue<TableWidthValues>(twv);
        //                        return etwv;
        //                    }
        //                }
        //                break;

        //            case "HeightRuleValues-HeightRuleValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        HeightRuleValues hrv = (HeightRuleValues)Enum.Parse(typeof(HeightRuleValues), sourceValue.ToString());
        //                        EnumValue<HeightRuleValues> etwuv = new EnumValue<HeightRuleValues>(hrv);
        //                        return etwuv;
        //                    }
        //                }
        //                break;

        //            case "LineSpacingRuleValues-LineSpacingRuleValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        LineSpacingRuleValues lsrv = (LineSpacingRuleValues)Enum.Parse(typeof(LineSpacingRuleValues), sourceValue.ToString());
        //                        EnumValue<LineSpacingRuleValues> etwuv = new EnumValue<LineSpacingRuleValues>(lsrv);
        //                        return etwuv;
        //                    }
        //                }
        //                break;

        //            case "BorderValues-BorderValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        BorderValues bv = (BorderValues)Enum.Parse(typeof(BorderValues), sourceValue.ToString());
        //                        EnumValue<BorderValues> ebv = new EnumValue<BorderValues>(bv);
        //                        return ebv;
        //                    }
        //                }
        //                break;

        //            case "TableStyleValues-StringValue":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    StringValue tsv = new StringValue(sourceValue.ToString());
        //                    return tsv;
        //                }
        //                break;

        //            case "JustificationValues-JustificationValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        JustificationValues jv = (JustificationValues)Enum.Parse(typeof(JustificationValues), sourceValue.ToString());
        //                        EnumValue<JustificationValues> ejv = new EnumValue<JustificationValues>(jv);
        //                        return ejv;
        //                    }
        //                }
        //                break;

        //            case "UnderlineValues-UnderlineValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        UnderlineValues uv = (UnderlineValues)Enum.Parse(typeof(UnderlineValues), sourceValue.ToString());
        //                        EnumValue<UnderlineValues> euv = new EnumValue<UnderlineValues>(uv);
        //                        return euv;
        //                    }
        //                }
        //                break;

        //            case "ThemeFontValues-ThemeFontValues":
        //                if (conversionDirection == Ds.TypeConversionDirection.AdsdiToOpenXml)
        //                {
        //                    if (destType.IsEnumValue)
        //                    {
        //                        ThemeFontValues tfv = (ThemeFontValues)Enum.Parse(typeof(ThemeFontValues), sourceValue.ToString());
        //                        EnumValue<ThemeFontValues> etfv = new EnumValue<ThemeFontValues>(tfv);
        //                        return etfv;
        //                    }
        //                }
        //                break;

        //            case "String-StringValue":
        //                StringValue sv = new StringValue(sourceValue.ToString());
        //                return sv;

        //            case "String-HexBinaryValue":
        //                HexBinaryValue hbv = new HexBinaryValue(sourceValue.ToString());
        //                return hbv;

        //            case "String-Int16Value":
        //                Int16Value i16v = new Int16Value(Convert.ToInt16(sourceValue.ToString()));
        //                return i16v;
        //        }

        //        throw new Exception("No type translation implemented for convType '" + convType + "'.");
        //    }
        //}

        //public static string BuildDocumentMap(Body body)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.OmitXmlDeclaration = true;
        //    settings.ConformanceLevel = ConformanceLevel.Fragment;
        //    settings.CloseOutput = false;
        //    MemoryStream strm = new MemoryStream();
        //    XmlWriter w = XmlWriter.Create(sb, settings);                      

        //    foreach (OpenXmlElement e in body.ChildElements)
        //    {
        //        e.WriteTo(w);
        //    }

        //    w.Flush();
        //    w.Close();

        //    string map = sb.ToString();

        //    XElement xMap = XElement.Parse("<root>" + map + "</root>");
        //    string formattedMap = xMap.ToString();


        //    return formattedMap;
        //}

        //public static string ValidateDocument(Document doc)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        OpenXmlValidator validator = new OpenXmlValidator();
        //        int count = 0;
        //        IEnumerable<ValidationErrorInfo> validationErrors = validator.Validate(doc);

        //        foreach (ValidationErrorInfo error in validationErrors)
        //        {
        //            count++;
        //            if (sb.Length > 0)
        //                sb.Append("-------------------------------------------------------------------------"+ g.nl);

        //            sb.Append("Error " + count.ToString() + g.nl);
        //            sb.Append("Description: " + error.Description + g.nl);
        //            sb.Append("Path: " + error.Path.XPath + g.nl);
        //            sb.Append("Part: " + error.Part.Uri + g.nl2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sb.Append(g.crlf2 + "*** EXCEPTION ***" + g.crlf + ex.Message + g.nl);
        //    }

        //    string validationReport = sb.ToString();
        //    return validationReport;
        //}

        //public static XElement GetDocumentXml(Document doc)
        //{
        //    XElement docDefaultsElement = null;            
        //    if(doc.MainDocumentPart.StyleDefinitionsPart != null)
        //    {
        //        DocumentFormat.OpenXml.Packaging.StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
        //        if (stylePart.Styles.DocDefaults != null)
        //        {
        //            docDefaultsElement = XElement.Parse(stylePart.Styles.DocDefaults.OuterXml);
        //        }
        //    }

        //    string outerXml = doc.OuterXml;
        //    XElement documentXml = XElement.Parse(outerXml);

        //    if (docDefaultsElement != null)
        //        documentXml.AddFirst(docDefaultsElement);

        //    return documentXml;
        //}

        //public static XElement GetStylesXml(Document doc)
        //{
        //    XElement stylesElement = new XElement("Styles");          
        //    if(doc.MainDocumentPart.StyleDefinitionsPart != null)
        //    {
        //        DocumentFormat.OpenXml.Packaging.StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
        //        stylesElement = XElement.Parse(stylePart.Styles.OuterXml);
        //    }

        //    return stylesElement;
        //}

        private static object ManageRepeatTags_LockObject = new object();
        public static void ManageRepeatTags(XElement e, int number)
        {
            lock (ManageRepeatTags_LockObject)
            {
                XNamespace ns = e.Name.NamespaceName;
                IEnumerable<XAttribute> tags = e.Descendants().Attributes(ns + "tag");

                foreach (XAttribute tag in tags)
                {
                    string tagPattern= String.Empty;
                    if (tag.Parent.Attribute(ns + "tag-repeat") != null)
                        tagPattern = tag.Parent.Attribute(ns + "tag-repeat").Value.Trim();
                    else
                        tagPattern = tag.Parent.Attribute(ns + "tag").Value.Trim();

                    if (!tagPattern.Contains("#"))
                        throw new Exception("A tag attribute within a repeat element must include a '#' as a placeholder for the number.");

                    // store the pattern if it does not yet exist
                    if (tag.Parent.Attribute(ns + "tag-repeat") == null)
                        tag.Parent.Add(new XAttribute(ns + "tag-repeat", tagPattern));

                    string tagValue = tagPattern.Replace("#", number.ToString());

                    tag.Value = tagValue;
                }
            }
        }

        public static void ResetTagId()
        {
            tagId = 1000;
        }

        //public static void AddTag(this OpenXmlElement e, Ds.Doc doc, string aTag)
        //{
        //    OpenXmlAttribute attr = new OpenXmlAttribute();
        //    attr.LocalName = "tag";

        //    string tagValue = aTag;

        //    if (tagValue.IsBlank())
        //    {
        //        tagValue = tagId.ToString();
        //        tagId++;
        //    }
            
        //    attr.Value = tagValue;
        //    e.SetAttribute(attr);

        //    if (doc == null)
        //        throw new Exception("Ds.Doc is null.");

        //    if (doc.Tags.ContainsKey(tagValue))
        //        throw new Exception("Tag '" + tagValue + "' already exists in document.");

        //    doc.Tags.Add(tagValue, e);
        //}

        //public static void AddAttr(this OpenXmlElement e, string name, string value)
        //{
        //    if (value.IsBlank())
        //        return;

        //    OpenXmlAttribute attr = new OpenXmlAttribute();
        //    attr.LocalName = name;
        //    attr.Value = value;
        //    e.SetAttribute(attr);
        //}

        //public static string GetTag(this OpenXmlElement e)
        //{
        //    try
        //    {
        //        if (e.InnerXml == null)
        //            return String.Empty;

        //        int endElementPos = e.InnerXml.IndexOf(">");

        //        if (endElementPos == -1)
        //            return String.Empty;

        //        string elementString = e.InnerXml.Substring(0, endElementPos + 1);
        //        if (!elementString.Contains("/>"))
        //            elementString = elementString.Replace(">", "/>");

        //        XElement element = XElement.Parse(elementString);

        //        if (element.Attribute("tag") != null)
        //            return element.Attribute("tag").Value.Trim();

        //        return String.Empty;
        //    }
        //    catch
        //    {
        //        return String.Empty;
        //    }
        //}

        //public static string GetClass(this OpenXmlElement e)
        //{
        //    try
        //    {
        //        if (e.InnerXml == null)
        //            return String.Empty;

        //        int endElementPos = e.InnerXml.IndexOf(">");

        //        if (endElementPos == -1)
        //            return String.Empty;

        //        string elementString = e.InnerXml.Substring(0, endElementPos + 1);
        //        if (!elementString.Contains("/>"))
        //            elementString = elementString.Replace(">", "/>");

        //        XElement element = XElement.Parse(elementString);

        //        if (element.Attribute("class") != null)
        //            return element.Attribute("class").Value.Trim();

        //        return String.Empty;
        //    }
        //    catch
        //    {
        //        return String.Empty;
        //    }
        //}

        private static bool Initialize()
        {
            if (IsInitialized)
                return true;

            //OpenXmlAssembly = Assembly.GetAssembly(typeof(DocumentFormat.OpenXml.Wordprocessing.Document));
            //tagId = 1000;

            return true;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Extension methods related to OpenXml objects
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //public static void RemoveLoneEmptyParagraph(this OpenXmlCompositeElement value)
        //{
        //    OpenXmlElement e = value.LastChild;
        //    if (e == null)
        //        return;

        //    if (e.GetType().ToString() != "DocumentFormat.OpenXml.Wordprocessing.Paragraph")
        //        return;

        //    if (e.ChildElements.Count > 0)
        //        return;           

        //    value.LastChild.Remove();
        //}
    }
}
