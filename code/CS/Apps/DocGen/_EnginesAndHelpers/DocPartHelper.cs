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

using Ds = Org.DocGen.DocSpec;
using Org.GS;

namespace Org.DocGen
{
    public static class DocPartHelper
    {
    //    private static object GenerateStyleDefinitionsPart1Content_LockObject = new object();
    //    public static void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
    //    {
    //        lock (GenerateStyleDefinitionsPart1Content_LockObject)
    //        {
    //            Styles styles2 = new Styles() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14" } };
    //            styles2.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            styles2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            styles2.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            styles2.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");

    //            DocDefaults docDefaults2 = new DocDefaults();

    //            RunPropertiesDefault runPropertiesDefault2 = new RunPropertiesDefault();

    //            RunPropertiesBaseStyle runPropertiesBaseStyle2 = new RunPropertiesBaseStyle();
    //            RunFonts runFonts4 = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
    //            Languages languages2 = new Languages() { Val = "en-US", EastAsia = "en-US", Bidi = "ar-SA" };

    //            runPropertiesBaseStyle2.Append(runFonts4);
    //            runPropertiesBaseStyle2.Append(languages2);

    //            runPropertiesDefault2.Append(runPropertiesBaseStyle2);
    //            ParagraphPropertiesDefault paragraphPropertiesDefault2 = new ParagraphPropertiesDefault();

    //            docDefaults2.Append(runPropertiesDefault2);
    //            docDefaults2.Append(paragraphPropertiesDefault2);

    //            LatentStyles latentStyles2 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = true, DefaultUnhideWhenUsed = true, DefaultPrimaryStyle = false, Count = 267 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo138 = new LatentStyleExceptionInfo() { Name = "Normal", UiPriority = 0, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo139 = new LatentStyleExceptionInfo() { Name = "heading 1", UiPriority = 9, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo140 = new LatentStyleExceptionInfo() { Name = "heading 2", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo141 = new LatentStyleExceptionInfo() { Name = "heading 3", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo142 = new LatentStyleExceptionInfo() { Name = "heading 4", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo143 = new LatentStyleExceptionInfo() { Name = "heading 5", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo144 = new LatentStyleExceptionInfo() { Name = "heading 6", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo145 = new LatentStyleExceptionInfo() { Name = "heading 7", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo146 = new LatentStyleExceptionInfo() { Name = "heading 8", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo147 = new LatentStyleExceptionInfo() { Name = "heading 9", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo148 = new LatentStyleExceptionInfo() { Name = "toc 1", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo149 = new LatentStyleExceptionInfo() { Name = "toc 2", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo150 = new LatentStyleExceptionInfo() { Name = "toc 3", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo151 = new LatentStyleExceptionInfo() { Name = "toc 4", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo152 = new LatentStyleExceptionInfo() { Name = "toc 5", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo153 = new LatentStyleExceptionInfo() { Name = "toc 6", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo154 = new LatentStyleExceptionInfo() { Name = "toc 7", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo155 = new LatentStyleExceptionInfo() { Name = "toc 8", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo156 = new LatentStyleExceptionInfo() { Name = "toc 9", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo157 = new LatentStyleExceptionInfo() { Name = "caption", UiPriority = 35, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo158 = new LatentStyleExceptionInfo() { Name = "Title", UiPriority = 10, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo159 = new LatentStyleExceptionInfo() { Name = "Default Paragraph Font", UiPriority = 1 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo160 = new LatentStyleExceptionInfo() { Name = "Subtitle", UiPriority = 11, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo161 = new LatentStyleExceptionInfo() { Name = "Strong", UiPriority = 22, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo162 = new LatentStyleExceptionInfo() { Name = "Emphasis", UiPriority = 20, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo163 = new LatentStyleExceptionInfo() { Name = "Table Grid", UiPriority = 59, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo164 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo165 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo166 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo167 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo168 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo169 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo170 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo171 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo172 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo173 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo174 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo175 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo176 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo177 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo178 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo179 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo180 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo181 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo182 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo183 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo184 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo185 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo186 = new LatentStyleExceptionInfo() { Name = "Revision", UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo187 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo188 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo189 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo190 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo191 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo192 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo193 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo194 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo195 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo196 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo197 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo198 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo199 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo200 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo201 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo202 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo203 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo204 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo205 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo206 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo207 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo208 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo209 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo210 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo211 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo212 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo213 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo214 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo215 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo216 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo217 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo218 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo219 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo220 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo221 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo222 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo223 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo224 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo225 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo226 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo227 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo228 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo229 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo230 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo231 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo232 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo233 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo234 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo235 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo236 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo237 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo238 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo239 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo240 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo241 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo242 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo243 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo244 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo245 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo246 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo247 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo248 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo249 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo250 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo251 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo252 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo253 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo254 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo255 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo256 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo257 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo258 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo259 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo260 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo261 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo262 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo263 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo264 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo265 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo266 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo267 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo268 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo269 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo270 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo271 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo272 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo273 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo274 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, PrimaryStyle = true };

    //            latentStyles2.Append(latentStyleExceptionInfo138);
    //            latentStyles2.Append(latentStyleExceptionInfo139);
    //            latentStyles2.Append(latentStyleExceptionInfo140);
    //            latentStyles2.Append(latentStyleExceptionInfo141);
    //            latentStyles2.Append(latentStyleExceptionInfo142);
    //            latentStyles2.Append(latentStyleExceptionInfo143);
    //            latentStyles2.Append(latentStyleExceptionInfo144);
    //            latentStyles2.Append(latentStyleExceptionInfo145);
    //            latentStyles2.Append(latentStyleExceptionInfo146);
    //            latentStyles2.Append(latentStyleExceptionInfo147);
    //            latentStyles2.Append(latentStyleExceptionInfo148);
    //            latentStyles2.Append(latentStyleExceptionInfo149);
    //            latentStyles2.Append(latentStyleExceptionInfo150);
    //            latentStyles2.Append(latentStyleExceptionInfo151);
    //            latentStyles2.Append(latentStyleExceptionInfo152);
    //            latentStyles2.Append(latentStyleExceptionInfo153);
    //            latentStyles2.Append(latentStyleExceptionInfo154);
    //            latentStyles2.Append(latentStyleExceptionInfo155);
    //            latentStyles2.Append(latentStyleExceptionInfo156);
    //            latentStyles2.Append(latentStyleExceptionInfo157);
    //            latentStyles2.Append(latentStyleExceptionInfo158);
    //            latentStyles2.Append(latentStyleExceptionInfo159);
    //            latentStyles2.Append(latentStyleExceptionInfo160);
    //            latentStyles2.Append(latentStyleExceptionInfo161);
    //            latentStyles2.Append(latentStyleExceptionInfo162);
    //            latentStyles2.Append(latentStyleExceptionInfo163);
    //            latentStyles2.Append(latentStyleExceptionInfo164);
    //            latentStyles2.Append(latentStyleExceptionInfo165);
    //            latentStyles2.Append(latentStyleExceptionInfo166);
    //            latentStyles2.Append(latentStyleExceptionInfo167);
    //            latentStyles2.Append(latentStyleExceptionInfo168);
    //            latentStyles2.Append(latentStyleExceptionInfo169);
    //            latentStyles2.Append(latentStyleExceptionInfo170);
    //            latentStyles2.Append(latentStyleExceptionInfo171);
    //            latentStyles2.Append(latentStyleExceptionInfo172);
    //            latentStyles2.Append(latentStyleExceptionInfo173);
    //            latentStyles2.Append(latentStyleExceptionInfo174);
    //            latentStyles2.Append(latentStyleExceptionInfo175);
    //            latentStyles2.Append(latentStyleExceptionInfo176);
    //            latentStyles2.Append(latentStyleExceptionInfo177);
    //            latentStyles2.Append(latentStyleExceptionInfo178);
    //            latentStyles2.Append(latentStyleExceptionInfo179);
    //            latentStyles2.Append(latentStyleExceptionInfo180);
    //            latentStyles2.Append(latentStyleExceptionInfo181);
    //            latentStyles2.Append(latentStyleExceptionInfo182);
    //            latentStyles2.Append(latentStyleExceptionInfo183);
    //            latentStyles2.Append(latentStyleExceptionInfo184);
    //            latentStyles2.Append(latentStyleExceptionInfo185);
    //            latentStyles2.Append(latentStyleExceptionInfo186);
    //            latentStyles2.Append(latentStyleExceptionInfo187);
    //            latentStyles2.Append(latentStyleExceptionInfo188);
    //            latentStyles2.Append(latentStyleExceptionInfo189);
    //            latentStyles2.Append(latentStyleExceptionInfo190);
    //            latentStyles2.Append(latentStyleExceptionInfo191);
    //            latentStyles2.Append(latentStyleExceptionInfo192);
    //            latentStyles2.Append(latentStyleExceptionInfo193);
    //            latentStyles2.Append(latentStyleExceptionInfo194);
    //            latentStyles2.Append(latentStyleExceptionInfo195);
    //            latentStyles2.Append(latentStyleExceptionInfo196);
    //            latentStyles2.Append(latentStyleExceptionInfo197);
    //            latentStyles2.Append(latentStyleExceptionInfo198);
    //            latentStyles2.Append(latentStyleExceptionInfo199);
    //            latentStyles2.Append(latentStyleExceptionInfo200);
    //            latentStyles2.Append(latentStyleExceptionInfo201);
    //            latentStyles2.Append(latentStyleExceptionInfo202);
    //            latentStyles2.Append(latentStyleExceptionInfo203);
    //            latentStyles2.Append(latentStyleExceptionInfo204);
    //            latentStyles2.Append(latentStyleExceptionInfo205);
    //            latentStyles2.Append(latentStyleExceptionInfo206);
    //            latentStyles2.Append(latentStyleExceptionInfo207);
    //            latentStyles2.Append(latentStyleExceptionInfo208);
    //            latentStyles2.Append(latentStyleExceptionInfo209);
    //            latentStyles2.Append(latentStyleExceptionInfo210);
    //            latentStyles2.Append(latentStyleExceptionInfo211);
    //            latentStyles2.Append(latentStyleExceptionInfo212);
    //            latentStyles2.Append(latentStyleExceptionInfo213);
    //            latentStyles2.Append(latentStyleExceptionInfo214);
    //            latentStyles2.Append(latentStyleExceptionInfo215);
    //            latentStyles2.Append(latentStyleExceptionInfo216);
    //            latentStyles2.Append(latentStyleExceptionInfo217);
    //            latentStyles2.Append(latentStyleExceptionInfo218);
    //            latentStyles2.Append(latentStyleExceptionInfo219);
    //            latentStyles2.Append(latentStyleExceptionInfo220);
    //            latentStyles2.Append(latentStyleExceptionInfo221);
    //            latentStyles2.Append(latentStyleExceptionInfo222);
    //            latentStyles2.Append(latentStyleExceptionInfo223);
    //            latentStyles2.Append(latentStyleExceptionInfo224);
    //            latentStyles2.Append(latentStyleExceptionInfo225);
    //            latentStyles2.Append(latentStyleExceptionInfo226);
    //            latentStyles2.Append(latentStyleExceptionInfo227);
    //            latentStyles2.Append(latentStyleExceptionInfo228);
    //            latentStyles2.Append(latentStyleExceptionInfo229);
    //            latentStyles2.Append(latentStyleExceptionInfo230);
    //            latentStyles2.Append(latentStyleExceptionInfo231);
    //            latentStyles2.Append(latentStyleExceptionInfo232);
    //            latentStyles2.Append(latentStyleExceptionInfo233);
    //            latentStyles2.Append(latentStyleExceptionInfo234);
    //            latentStyles2.Append(latentStyleExceptionInfo235);
    //            latentStyles2.Append(latentStyleExceptionInfo236);
    //            latentStyles2.Append(latentStyleExceptionInfo237);
    //            latentStyles2.Append(latentStyleExceptionInfo238);
    //            latentStyles2.Append(latentStyleExceptionInfo239);
    //            latentStyles2.Append(latentStyleExceptionInfo240);
    //            latentStyles2.Append(latentStyleExceptionInfo241);
    //            latentStyles2.Append(latentStyleExceptionInfo242);
    //            latentStyles2.Append(latentStyleExceptionInfo243);
    //            latentStyles2.Append(latentStyleExceptionInfo244);
    //            latentStyles2.Append(latentStyleExceptionInfo245);
    //            latentStyles2.Append(latentStyleExceptionInfo246);
    //            latentStyles2.Append(latentStyleExceptionInfo247);
    //            latentStyles2.Append(latentStyleExceptionInfo248);
    //            latentStyles2.Append(latentStyleExceptionInfo249);
    //            latentStyles2.Append(latentStyleExceptionInfo250);
    //            latentStyles2.Append(latentStyleExceptionInfo251);
    //            latentStyles2.Append(latentStyleExceptionInfo252);
    //            latentStyles2.Append(latentStyleExceptionInfo253);
    //            latentStyles2.Append(latentStyleExceptionInfo254);
    //            latentStyles2.Append(latentStyleExceptionInfo255);
    //            latentStyles2.Append(latentStyleExceptionInfo256);
    //            latentStyles2.Append(latentStyleExceptionInfo257);
    //            latentStyles2.Append(latentStyleExceptionInfo258);
    //            latentStyles2.Append(latentStyleExceptionInfo259);
    //            latentStyles2.Append(latentStyleExceptionInfo260);
    //            latentStyles2.Append(latentStyleExceptionInfo261);
    //            latentStyles2.Append(latentStyleExceptionInfo262);
    //            latentStyles2.Append(latentStyleExceptionInfo263);
    //            latentStyles2.Append(latentStyleExceptionInfo264);
    //            latentStyles2.Append(latentStyleExceptionInfo265);
    //            latentStyles2.Append(latentStyleExceptionInfo266);
    //            latentStyles2.Append(latentStyleExceptionInfo267);
    //            latentStyles2.Append(latentStyleExceptionInfo268);
    //            latentStyles2.Append(latentStyleExceptionInfo269);
    //            latentStyles2.Append(latentStyleExceptionInfo270);
    //            latentStyles2.Append(latentStyleExceptionInfo271);
    //            latentStyles2.Append(latentStyleExceptionInfo272);
    //            latentStyles2.Append(latentStyleExceptionInfo273);
    //            latentStyles2.Append(latentStyleExceptionInfo274);

    //            Style style14 = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true };
    //            StyleName styleName14 = new StyleName() { Val = "Normal" };
    //            PrimaryStyle primaryStyle3 = new PrimaryStyle();
    //            Rsid rsid11 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties6 = new StyleParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines32 = new SpacingBetweenLines() { After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties6.Append(spacingBetweenLines32);

    //            StyleRunProperties styleRunProperties7 = new StyleRunProperties();
    //            FontSize fontSize63 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript63 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties7.Append(fontSize63);
    //            styleRunProperties7.Append(fontSizeComplexScript63);

    //            style14.Append(styleName14);
    //            style14.Append(primaryStyle3);
    //            style14.Append(rsid11);
    //            style14.Append(styleParagraphProperties6);
    //            style14.Append(styleRunProperties7);

    //            Style style15 = new Style() { Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
    //            StyleName styleName15 = new StyleName() { Val = "Default Paragraph Font" };
    //            UIPriority uIPriority13 = new UIPriority() { Val = 1 };
    //            SemiHidden semiHidden6 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed8 = new UnhideWhenUsed();

    //            style15.Append(styleName15);
    //            style15.Append(uIPriority13);
    //            style15.Append(semiHidden6);
    //            style15.Append(unhideWhenUsed8);

    //            Style style16 = new Style() { Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
    //            StyleName styleName16 = new StyleName() { Val = "Normal Table" };
    //            UIPriority uIPriority14 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden7 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed9 = new UnhideWhenUsed();

    //            StyleTableProperties styleTableProperties3 = new StyleTableProperties();
    //            TableIndentation tableIndentation3 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

    //            TableCellMarginDefault tableCellMarginDefault4 = new TableCellMarginDefault();
    //            TopMargin topMargin12 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellLeftMargin tableCellLeftMargin4 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
    //            BottomMargin bottomMargin8 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellRightMargin tableCellRightMargin4 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

    //            tableCellMarginDefault4.Append(topMargin12);
    //            tableCellMarginDefault4.Append(tableCellLeftMargin4);
    //            tableCellMarginDefault4.Append(bottomMargin8);
    //            tableCellMarginDefault4.Append(tableCellRightMargin4);

    //            styleTableProperties3.Append(tableIndentation3);
    //            styleTableProperties3.Append(tableCellMarginDefault4);

    //            style16.Append(styleName16);
    //            style16.Append(uIPriority14);
    //            style16.Append(semiHidden7);
    //            style16.Append(unhideWhenUsed9);
    //            style16.Append(styleTableProperties3);

    //            Style style17 = new Style() { Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
    //            StyleName styleName17 = new StyleName() { Val = "No List" };
    //            UIPriority uIPriority15 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden8 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed10 = new UnhideWhenUsed();

    //            style17.Append(styleName17);
    //            style17.Append(uIPriority15);
    //            style17.Append(semiHidden8);
    //            style17.Append(unhideWhenUsed10);

    //            Style style18 = new Style() { Type = StyleValues.Character, StyleId = "Hyperlink" };
    //            StyleName styleName18 = new StyleName() { Val = "Hyperlink" };
    //            UIPriority uIPriority16 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed11 = new UnhideWhenUsed();
    //            Rsid rsid12 = new Rsid() { Val = "00E778F2" };

    //            StyleRunProperties styleRunProperties8 = new StyleRunProperties();
    //            Color color2 = new Color() { Val = "0000FF" };
    //            Underline underline2 = new Underline() { Val = UnderlineValues.Single };

    //            styleRunProperties8.Append(color2);
    //            styleRunProperties8.Append(underline2);

    //            style18.Append(styleName18);
    //            style18.Append(uIPriority16);
    //            style18.Append(unhideWhenUsed11);
    //            style18.Append(rsid12);
    //            style18.Append(styleRunProperties8);

    //            Style style19 = new Style() { Type = StyleValues.Table, StyleId = "TableGrid" };
    //            StyleName styleName19 = new StyleName() { Val = "Table Grid" };
    //            BasedOn basedOn8 = new BasedOn() { Val = "TableNormal" };
    //            UIPriority uIPriority17 = new UIPriority() { Val = 59 };
    //            Rsid rsid13 = new Rsid() { Val = "00E778F2" };

    //            StyleTableProperties styleTableProperties4 = new StyleTableProperties();
    //            TableIndentation tableIndentation4 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

    //            TableBorders tableBorders3 = new TableBorders();
    //            TopBorder topBorder23 = new TopBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            LeftBorder leftBorder23 = new LeftBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            BottomBorder bottomBorder23 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            RightBorder rightBorder23 = new RightBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            InsideHorizontalBorder insideHorizontalBorder3 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            InsideVerticalBorder insideVerticalBorder3 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

    //            tableBorders3.Append(topBorder23);
    //            tableBorders3.Append(leftBorder23);
    //            tableBorders3.Append(bottomBorder23);
    //            tableBorders3.Append(rightBorder23);
    //            tableBorders3.Append(insideHorizontalBorder3);
    //            tableBorders3.Append(insideVerticalBorder3);

    //            TableCellMarginDefault tableCellMarginDefault5 = new TableCellMarginDefault();
    //            TopMargin topMargin13 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellLeftMargin tableCellLeftMargin5 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
    //            BottomMargin bottomMargin9 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellRightMargin tableCellRightMargin5 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

    //            tableCellMarginDefault5.Append(topMargin13);
    //            tableCellMarginDefault5.Append(tableCellLeftMargin5);
    //            tableCellMarginDefault5.Append(bottomMargin9);
    //            tableCellMarginDefault5.Append(tableCellRightMargin5);

    //            styleTableProperties4.Append(tableIndentation4);
    //            styleTableProperties4.Append(tableBorders3);
    //            styleTableProperties4.Append(tableCellMarginDefault5);

    //            style19.Append(styleName19);
    //            style19.Append(basedOn8);
    //            style19.Append(uIPriority17);
    //            style19.Append(rsid13);
    //            style19.Append(styleTableProperties4);

    //            Style style20 = new Style() { Type = StyleValues.Paragraph, StyleId = "ListParagraph" };
    //            StyleName styleName20 = new StyleName() { Val = "List Paragraph" };
    //            BasedOn basedOn9 = new BasedOn() { Val = "Normal" };
    //            UIPriority uIPriority18 = new UIPriority() { Val = 34 };
    //            PrimaryStyle primaryStyle4 = new PrimaryStyle();
    //            Rsid rsid14 = new Rsid() { Val = "00EB1E85" };

    //            StyleParagraphProperties styleParagraphProperties7 = new StyleParagraphProperties();
    //            Indentation indentation17 = new Indentation() { Left = "720" };
    //            ContextualSpacing contextualSpacing2 = new ContextualSpacing();

    //            styleParagraphProperties7.Append(indentation17);
    //            styleParagraphProperties7.Append(contextualSpacing2);

    //            style20.Append(styleName20);
    //            style20.Append(basedOn9);
    //            style20.Append(uIPriority18);
    //            style20.Append(primaryStyle4);
    //            style20.Append(rsid14);
    //            style20.Append(styleParagraphProperties7);

    //            Style style21 = new Style() { Type = StyleValues.Paragraph, StyleId = "BalloonText" };
    //            StyleName styleName21 = new StyleName() { Val = "Balloon Text" };
    //            BasedOn basedOn10 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle7 = new LinkedStyle() { Val = "BalloonTextChar" };
    //            UIPriority uIPriority19 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden9 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed12 = new UnhideWhenUsed();
    //            Rsid rsid15 = new Rsid() { Val = "00FF0A24" };

    //            StyleParagraphProperties styleParagraphProperties8 = new StyleParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines33 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties8.Append(spacingBetweenLines33);

    //            StyleRunProperties styleRunProperties9 = new StyleRunProperties();
    //            RunFonts runFonts5 = new RunFonts() { Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
    //            FontSize fontSize64 = new FontSize() { Val = "16" };
    //            FontSizeComplexScript fontSizeComplexScript64 = new FontSizeComplexScript() { Val = "16" };

    //            styleRunProperties9.Append(runFonts5);
    //            styleRunProperties9.Append(fontSize64);
    //            styleRunProperties9.Append(fontSizeComplexScript64);

    //            style21.Append(styleName21);
    //            style21.Append(basedOn10);
    //            style21.Append(linkedStyle7);
    //            style21.Append(uIPriority19);
    //            style21.Append(semiHidden9);
    //            style21.Append(unhideWhenUsed12);
    //            style21.Append(rsid15);
    //            style21.Append(styleParagraphProperties8);
    //            style21.Append(styleRunProperties9);

    //            Style style22 = new Style() { Type = StyleValues.Character, StyleId = "BalloonTextChar", CustomStyle = true };
    //            StyleName styleName22 = new StyleName() { Val = "Balloon Text Char" };
    //            LinkedStyle linkedStyle8 = new LinkedStyle() { Val = "BalloonText" };
    //            UIPriority uIPriority20 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden10 = new SemiHidden();
    //            Rsid rsid16 = new Rsid() { Val = "00FF0A24" };

    //            StyleRunProperties styleRunProperties10 = new StyleRunProperties();
    //            RunFonts runFonts6 = new RunFonts() { Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
    //            FontSize fontSize65 = new FontSize() { Val = "16" };
    //            FontSizeComplexScript fontSizeComplexScript65 = new FontSizeComplexScript() { Val = "16" };

    //            styleRunProperties10.Append(runFonts6);
    //            styleRunProperties10.Append(fontSize65);
    //            styleRunProperties10.Append(fontSizeComplexScript65);

    //            style22.Append(styleName22);
    //            style22.Append(linkedStyle8);
    //            style22.Append(uIPriority20);
    //            style22.Append(semiHidden10);
    //            style22.Append(rsid16);
    //            style22.Append(styleRunProperties10);

    //            Style style23 = new Style() { Type = StyleValues.Paragraph, StyleId = "Header" };
    //            StyleName styleName23 = new StyleName() { Val = "header" };
    //            BasedOn basedOn11 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle9 = new LinkedStyle() { Val = "HeaderChar" };
    //            UIPriority uIPriority21 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed13 = new UnhideWhenUsed();
    //            Rsid rsid17 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties9 = new StyleParagraphProperties();

    //            Tabs tabs3 = new Tabs();
    //            TabStop tabStop5 = new TabStop() { Val = TabStopValues.Center, Position = 4680 };
    //            TabStop tabStop6 = new TabStop() { Val = TabStopValues.Right, Position = 9360 };

    //            tabs3.Append(tabStop5);
    //            tabs3.Append(tabStop6);
    //            SpacingBetweenLines spacingBetweenLines34 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties9.Append(tabs3);
    //            styleParagraphProperties9.Append(spacingBetweenLines34);

    //            style23.Append(styleName23);
    //            style23.Append(basedOn11);
    //            style23.Append(linkedStyle9);
    //            style23.Append(uIPriority21);
    //            style23.Append(unhideWhenUsed13);
    //            style23.Append(rsid17);
    //            style23.Append(styleParagraphProperties9);

    //            Style style24 = new Style() { Type = StyleValues.Character, StyleId = "HeaderChar", CustomStyle = true };
    //            StyleName styleName24 = new StyleName() { Val = "Header Char" };
    //            BasedOn basedOn12 = new BasedOn() { Val = "DefaultParagraphFont" };
    //            LinkedStyle linkedStyle10 = new LinkedStyle() { Val = "Header" };
    //            UIPriority uIPriority22 = new UIPriority() { Val = 99 };
    //            Rsid rsid18 = new Rsid() { Val = "00EF483A" };

    //            StyleRunProperties styleRunProperties11 = new StyleRunProperties();
    //            FontSize fontSize66 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript66 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties11.Append(fontSize66);
    //            styleRunProperties11.Append(fontSizeComplexScript66);

    //            style24.Append(styleName24);
    //            style24.Append(basedOn12);
    //            style24.Append(linkedStyle10);
    //            style24.Append(uIPriority22);
    //            style24.Append(rsid18);
    //            style24.Append(styleRunProperties11);

    //            Style style25 = new Style() { Type = StyleValues.Paragraph, StyleId = "Footer" };
    //            StyleName styleName25 = new StyleName() { Val = "footer" };
    //            BasedOn basedOn13 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle11 = new LinkedStyle() { Val = "FooterChar" };
    //            UIPriority uIPriority23 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed14 = new UnhideWhenUsed();
    //            Rsid rsid19 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties10 = new StyleParagraphProperties();

    //            Tabs tabs4 = new Tabs();
    //            TabStop tabStop7 = new TabStop() { Val = TabStopValues.Center, Position = 4680 };
    //            TabStop tabStop8 = new TabStop() { Val = TabStopValues.Right, Position = 9360 };

    //            tabs4.Append(tabStop7);
    //            tabs4.Append(tabStop8);
    //            SpacingBetweenLines spacingBetweenLines35 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties10.Append(tabs4);
    //            styleParagraphProperties10.Append(spacingBetweenLines35);

    //            style25.Append(styleName25);
    //            style25.Append(basedOn13);
    //            style25.Append(linkedStyle11);
    //            style25.Append(uIPriority23);
    //            style25.Append(unhideWhenUsed14);
    //            style25.Append(rsid19);
    //            style25.Append(styleParagraphProperties10);

    //            Style style26 = new Style() { Type = StyleValues.Character, StyleId = "FooterChar", CustomStyle = true };
    //            StyleName styleName26 = new StyleName() { Val = "Footer Char" };
    //            BasedOn basedOn14 = new BasedOn() { Val = "DefaultParagraphFont" };
    //            LinkedStyle linkedStyle12 = new LinkedStyle() { Val = "Footer" };
    //            UIPriority uIPriority24 = new UIPriority() { Val = 99 };
    //            Rsid rsid20 = new Rsid() { Val = "00EF483A" };

    //            StyleRunProperties styleRunProperties12 = new StyleRunProperties();
    //            FontSize fontSize67 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript67 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties12.Append(fontSize67);
    //            styleRunProperties12.Append(fontSizeComplexScript67);

    //            style26.Append(styleName26);
    //            style26.Append(basedOn14);
    //            style26.Append(linkedStyle12);
    //            style26.Append(uIPriority24);
    //            style26.Append(rsid20);
    //            style26.Append(styleRunProperties12);

    //            styles2.Append(docDefaults2);
    //            styles2.Append(latentStyles2);
    //            styles2.Append(style14);
    //            styles2.Append(style15);
    //            styles2.Append(style16);
    //            styles2.Append(style17);
    //            styles2.Append(style18);
    //            styles2.Append(style19);
    //            styles2.Append(style20);
    //            styles2.Append(style21);
    //            styles2.Append(style22);
    //            styles2.Append(style23);
    //            styles2.Append(style24);
    //            styles2.Append(style25);
    //            styles2.Append(style26);

    //            styleDefinitionsPart1.Styles = styles2;
    //        }
    //    }

    //    private static object GenerateExtendedFilePropertiesPart1Content_LockObject = new object();
    //    public static void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1, Ds.DocInfo docInfo)
    //    {
    //        lock (GenerateExtendedFilePropertiesPart1Content_LockObject)
    //        {
    //            Ap.Properties properties1 = new Ap.Properties();
    //            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
    //            Ap.Template template1 = new Ap.Template();
    //            template1.Text = "Normal.dotm";
    //            Ap.TotalTime totalTime1 = new Ap.TotalTime();
    //            totalTime1.Text = "6";
    //            Ap.Pages pages1 = new Ap.Pages();
    //            pages1.Text = "1";
    //            Ap.Words words1 = new Ap.Words();
    //            words1.Text = "130";
    //            Ap.Characters characters1 = new Ap.Characters();
    //            characters1.Text = "742";
    //            Ap.Application application1 = new Ap.Application();
    //            application1.Text = "Microsoft Office Word";
    //            Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
    //            documentSecurity1.Text = "0";
    //            Ap.Lines lines1 = new Ap.Lines();
    //            lines1.Text = "6";
    //            Ap.Paragraphs paragraphs1 = new Ap.Paragraphs();

    //            paragraphs1.Text = "1";
    //            Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
    //            scaleCrop1.Text = "false";

    //            Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

    //            Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

    //            Vt.Variant variant1 = new Vt.Variant();
    //            Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
    //            vTLPSTR1.Text = "Title";

    //            variant1.Append(vTLPSTR1);

    //            Vt.Variant variant2 = new Vt.Variant();
    //            Vt.VTInt32 vTInt321 = new Vt.VTInt32();
    //            vTInt321.Text = "1";

    //            variant2.Append(vTInt321);

    //            vTVector1.Append(variant1);
    //            vTVector1.Append(variant2);

    //            headingPairs1.Append(vTVector1);

    //            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

    //            Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)1U };
    //            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
    //            vTLPSTR2.Text = "";

    //            vTVector2.Append(vTLPSTR2);

    //            titlesOfParts1.Append(vTVector2);
    //            Ap.Company company1 = new Ap.Company();
    //            company1.Text = docInfo.CompanyName;
    //            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
    //            linksUpToDate1.Text = "false";
    //            Ap.CharactersWithSpaces charactersWithSpaces1 = new Ap.CharactersWithSpaces();
    //            charactersWithSpaces1.Text = "871";
    //            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
    //            sharedDocument1.Text = "false";

    //            Ap.HyperlinkList hyperlinkList1 = new Ap.HyperlinkList();

    //            Vt.VTVector vTVector3 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)6U };

    //            Vt.Variant variant3 = new Vt.Variant();
    //            Vt.VTInt32 vTInt322 = new Vt.VTInt32();
    //            vTInt322.Text = "2031648";

    //            variant3.Append(vTInt322);

    //            Vt.Variant variant4 = new Vt.Variant();
    //            Vt.VTInt32 vTInt323 = new Vt.VTInt32();
    //            vTInt323.Text = "0";

    //            variant4.Append(vTInt323);

    //            Vt.Variant variant5 = new Vt.Variant();
    //            Vt.VTInt32 vTInt324 = new Vt.VTInt32();
    //            vTInt324.Text = "0";

    //            variant5.Append(vTInt324);

    //            Vt.Variant variant6 = new Vt.Variant();
    //            Vt.VTInt32 vTInt325 = new Vt.VTInt32();
    //            vTInt325.Text = "5";

    //            variant6.Append(vTInt325);

    //            Vt.Variant variant7 = new Vt.Variant();
    //            Vt.VTLPWSTR vTLPWSTR1 = new Vt.VTLPWSTR();
    //            vTLPWSTR1.Text = "mailto:smbolt@sbcglobal.net";     //xq <-does this need to change?

    //            variant7.Append(vTLPWSTR1);

    //            Vt.Variant variant8 = new Vt.Variant();
    //            Vt.VTLPWSTR vTLPWSTR2 = new Vt.VTLPWSTR();
    //            vTLPWSTR2.Text = "";

    //            variant8.Append(vTLPWSTR2);

    //            vTVector3.Append(variant3);
    //            vTVector3.Append(variant4);
    //            vTVector3.Append(variant5);
    //            vTVector3.Append(variant6);
    //            vTVector3.Append(variant7);
    //            vTVector3.Append(variant8);

    //            hyperlinkList1.Append(vTVector3);
    //            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
    //            hyperlinksChanged1.Text = "false";
    //            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
    //            applicationVersion1.Text = "14.0000";

    //            properties1.Append(template1);
    //            properties1.Append(totalTime1);
    //            properties1.Append(pages1);
    //            properties1.Append(words1);
    //            properties1.Append(characters1);
    //            properties1.Append(application1);
    //            properties1.Append(documentSecurity1);
    //            properties1.Append(lines1);
    //            properties1.Append(paragraphs1);
    //            properties1.Append(scaleCrop1);
    //            properties1.Append(headingPairs1);
    //            properties1.Append(titlesOfParts1);
    //            properties1.Append(company1);
    //            properties1.Append(linksUpToDate1);
    //            properties1.Append(charactersWithSpaces1);
    //            properties1.Append(sharedDocument1);
    //            properties1.Append(hyperlinkList1);
    //            properties1.Append(hyperlinksChanged1);
    //            properties1.Append(applicationVersion1);

    //            extendedFilePropertiesPart1.Properties = properties1;
    //        }
    //    }

    //    private static object GenerateStylesWithEffectsPart1Content_LockObject = new object();
    //    public static void GenerateStylesWithEffectsPart1Content(StylesWithEffectsPart stylesWithEffectsPart1)
    //    {
    //        lock (GenerateStylesWithEffectsPart1Content_LockObject)
    //        {
    //            Styles styles1 = new Styles() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 wp14" } };
    //            styles1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
    //            styles1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            styles1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //            styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            styles1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //            styles1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //            styles1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
    //            styles1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    //            styles1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            styles1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            styles1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
    //            styles1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
    //            styles1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
    //            styles1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");


    //            LatentStyles latentStyles1 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = true, DefaultUnhideWhenUsed = true, DefaultPrimaryStyle = false, Count = 267 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo() { Name = "Normal", UiPriority = 0, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo() { Name = "heading 1", UiPriority = 9, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo() { Name = "heading 2", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo() { Name = "heading 3", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo() { Name = "heading 4", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo() { Name = "heading 5", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo() { Name = "heading 6", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo() { Name = "heading 7", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo() { Name = "heading 8", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo() { Name = "heading 9", UiPriority = 9, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo() { Name = "toc 1", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo() { Name = "toc 2", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo() { Name = "toc 3", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo() { Name = "toc 4", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo() { Name = "toc 5", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo() { Name = "toc 6", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo() { Name = "toc 7", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo() { Name = "toc 8", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo() { Name = "toc 9", UiPriority = 39 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo() { Name = "caption", UiPriority = 35, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo() { Name = "Title", UiPriority = 10, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo() { Name = "Default Paragraph Font", UiPriority = 1 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo() { Name = "Subtitle", UiPriority = 11, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo() { Name = "Strong", UiPriority = 22, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo() { Name = "Emphasis", UiPriority = 20, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo() { Name = "Table Grid", UiPriority = 59, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo() { Name = "Revision", UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37 };
    //            LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, PrimaryStyle = true };

    //            latentStyles1.Append(latentStyleExceptionInfo1);
    //            latentStyles1.Append(latentStyleExceptionInfo2);
    //            latentStyles1.Append(latentStyleExceptionInfo3);
    //            latentStyles1.Append(latentStyleExceptionInfo4);
    //            latentStyles1.Append(latentStyleExceptionInfo5);
    //            latentStyles1.Append(latentStyleExceptionInfo6);
    //            latentStyles1.Append(latentStyleExceptionInfo7);
    //            latentStyles1.Append(latentStyleExceptionInfo8);
    //            latentStyles1.Append(latentStyleExceptionInfo9);
    //            latentStyles1.Append(latentStyleExceptionInfo10);
    //            latentStyles1.Append(latentStyleExceptionInfo11);
    //            latentStyles1.Append(latentStyleExceptionInfo12);
    //            latentStyles1.Append(latentStyleExceptionInfo13);
    //            latentStyles1.Append(latentStyleExceptionInfo14);
    //            latentStyles1.Append(latentStyleExceptionInfo15);
    //            latentStyles1.Append(latentStyleExceptionInfo16);
    //            latentStyles1.Append(latentStyleExceptionInfo17);
    //            latentStyles1.Append(latentStyleExceptionInfo18);
    //            latentStyles1.Append(latentStyleExceptionInfo19);
    //            latentStyles1.Append(latentStyleExceptionInfo20);
    //            latentStyles1.Append(latentStyleExceptionInfo21);
    //            latentStyles1.Append(latentStyleExceptionInfo22);
    //            latentStyles1.Append(latentStyleExceptionInfo23);
    //            latentStyles1.Append(latentStyleExceptionInfo24);
    //            latentStyles1.Append(latentStyleExceptionInfo25);
    //            latentStyles1.Append(latentStyleExceptionInfo26);
    //            latentStyles1.Append(latentStyleExceptionInfo27);
    //            latentStyles1.Append(latentStyleExceptionInfo28);
    //            latentStyles1.Append(latentStyleExceptionInfo29);
    //            latentStyles1.Append(latentStyleExceptionInfo30);
    //            latentStyles1.Append(latentStyleExceptionInfo31);
    //            latentStyles1.Append(latentStyleExceptionInfo32);
    //            latentStyles1.Append(latentStyleExceptionInfo33);
    //            latentStyles1.Append(latentStyleExceptionInfo34);
    //            latentStyles1.Append(latentStyleExceptionInfo35);
    //            latentStyles1.Append(latentStyleExceptionInfo36);
    //            latentStyles1.Append(latentStyleExceptionInfo37);
    //            latentStyles1.Append(latentStyleExceptionInfo38);
    //            latentStyles1.Append(latentStyleExceptionInfo39);
    //            latentStyles1.Append(latentStyleExceptionInfo40);
    //            latentStyles1.Append(latentStyleExceptionInfo41);
    //            latentStyles1.Append(latentStyleExceptionInfo42);
    //            latentStyles1.Append(latentStyleExceptionInfo43);
    //            latentStyles1.Append(latentStyleExceptionInfo44);
    //            latentStyles1.Append(latentStyleExceptionInfo45);
    //            latentStyles1.Append(latentStyleExceptionInfo46);
    //            latentStyles1.Append(latentStyleExceptionInfo47);
    //            latentStyles1.Append(latentStyleExceptionInfo48);
    //            latentStyles1.Append(latentStyleExceptionInfo49);
    //            latentStyles1.Append(latentStyleExceptionInfo50);
    //            latentStyles1.Append(latentStyleExceptionInfo51);
    //            latentStyles1.Append(latentStyleExceptionInfo52);
    //            latentStyles1.Append(latentStyleExceptionInfo53);
    //            latentStyles1.Append(latentStyleExceptionInfo54);
    //            latentStyles1.Append(latentStyleExceptionInfo55);
    //            latentStyles1.Append(latentStyleExceptionInfo56);
    //            latentStyles1.Append(latentStyleExceptionInfo57);
    //            latentStyles1.Append(latentStyleExceptionInfo58);
    //            latentStyles1.Append(latentStyleExceptionInfo59);
    //            latentStyles1.Append(latentStyleExceptionInfo60);
    //            latentStyles1.Append(latentStyleExceptionInfo61);
    //            latentStyles1.Append(latentStyleExceptionInfo62);
    //            latentStyles1.Append(latentStyleExceptionInfo63);
    //            latentStyles1.Append(latentStyleExceptionInfo64);
    //            latentStyles1.Append(latentStyleExceptionInfo65);
    //            latentStyles1.Append(latentStyleExceptionInfo66);
    //            latentStyles1.Append(latentStyleExceptionInfo67);
    //            latentStyles1.Append(latentStyleExceptionInfo68);
    //            latentStyles1.Append(latentStyleExceptionInfo69);
    //            latentStyles1.Append(latentStyleExceptionInfo70);
    //            latentStyles1.Append(latentStyleExceptionInfo71);
    //            latentStyles1.Append(latentStyleExceptionInfo72);
    //            latentStyles1.Append(latentStyleExceptionInfo73);
    //            latentStyles1.Append(latentStyleExceptionInfo74);
    //            latentStyles1.Append(latentStyleExceptionInfo75);
    //            latentStyles1.Append(latentStyleExceptionInfo76);
    //            latentStyles1.Append(latentStyleExceptionInfo77);
    //            latentStyles1.Append(latentStyleExceptionInfo78);
    //            latentStyles1.Append(latentStyleExceptionInfo79);
    //            latentStyles1.Append(latentStyleExceptionInfo80);
    //            latentStyles1.Append(latentStyleExceptionInfo81);
    //            latentStyles1.Append(latentStyleExceptionInfo82);
    //            latentStyles1.Append(latentStyleExceptionInfo83);
    //            latentStyles1.Append(latentStyleExceptionInfo84);
    //            latentStyles1.Append(latentStyleExceptionInfo85);
    //            latentStyles1.Append(latentStyleExceptionInfo86);
    //            latentStyles1.Append(latentStyleExceptionInfo87);
    //            latentStyles1.Append(latentStyleExceptionInfo88);
    //            latentStyles1.Append(latentStyleExceptionInfo89);
    //            latentStyles1.Append(latentStyleExceptionInfo90);
    //            latentStyles1.Append(latentStyleExceptionInfo91);
    //            latentStyles1.Append(latentStyleExceptionInfo92);
    //            latentStyles1.Append(latentStyleExceptionInfo93);
    //            latentStyles1.Append(latentStyleExceptionInfo94);
    //            latentStyles1.Append(latentStyleExceptionInfo95);
    //            latentStyles1.Append(latentStyleExceptionInfo96);
    //            latentStyles1.Append(latentStyleExceptionInfo97);
    //            latentStyles1.Append(latentStyleExceptionInfo98);
    //            latentStyles1.Append(latentStyleExceptionInfo99);
    //            latentStyles1.Append(latentStyleExceptionInfo100);
    //            latentStyles1.Append(latentStyleExceptionInfo101);
    //            latentStyles1.Append(latentStyleExceptionInfo102);
    //            latentStyles1.Append(latentStyleExceptionInfo103);
    //            latentStyles1.Append(latentStyleExceptionInfo104);
    //            latentStyles1.Append(latentStyleExceptionInfo105);
    //            latentStyles1.Append(latentStyleExceptionInfo106);
    //            latentStyles1.Append(latentStyleExceptionInfo107);
    //            latentStyles1.Append(latentStyleExceptionInfo108);
    //            latentStyles1.Append(latentStyleExceptionInfo109);
    //            latentStyles1.Append(latentStyleExceptionInfo110);
    //            latentStyles1.Append(latentStyleExceptionInfo111);
    //            latentStyles1.Append(latentStyleExceptionInfo112);
    //            latentStyles1.Append(latentStyleExceptionInfo113);
    //            latentStyles1.Append(latentStyleExceptionInfo114);
    //            latentStyles1.Append(latentStyleExceptionInfo115);
    //            latentStyles1.Append(latentStyleExceptionInfo116);
    //            latentStyles1.Append(latentStyleExceptionInfo117);
    //            latentStyles1.Append(latentStyleExceptionInfo118);
    //            latentStyles1.Append(latentStyleExceptionInfo119);
    //            latentStyles1.Append(latentStyleExceptionInfo120);
    //            latentStyles1.Append(latentStyleExceptionInfo121);
    //            latentStyles1.Append(latentStyleExceptionInfo122);
    //            latentStyles1.Append(latentStyleExceptionInfo123);
    //            latentStyles1.Append(latentStyleExceptionInfo124);
    //            latentStyles1.Append(latentStyleExceptionInfo125);
    //            latentStyles1.Append(latentStyleExceptionInfo126);
    //            latentStyles1.Append(latentStyleExceptionInfo127);
    //            latentStyles1.Append(latentStyleExceptionInfo128);
    //            latentStyles1.Append(latentStyleExceptionInfo129);
    //            latentStyles1.Append(latentStyleExceptionInfo130);
    //            latentStyles1.Append(latentStyleExceptionInfo131);
    //            latentStyles1.Append(latentStyleExceptionInfo132);
    //            latentStyles1.Append(latentStyleExceptionInfo133);
    //            latentStyles1.Append(latentStyleExceptionInfo134);
    //            latentStyles1.Append(latentStyleExceptionInfo135);
    //            latentStyles1.Append(latentStyleExceptionInfo136);
    //            latentStyles1.Append(latentStyleExceptionInfo137);

    //            Style style1 = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true };
    //            StyleName styleName1 = new StyleName() { Val = "Normal" };
    //            PrimaryStyle primaryStyle1 = new PrimaryStyle();
    //            Rsid rsid1 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines26 = new SpacingBetweenLines() { After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties1.Append(spacingBetweenLines26);

    //            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
    //            FontSize fontSize58 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript58 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties1.Append(fontSize58);
    //            styleRunProperties1.Append(fontSizeComplexScript58);

    //            style1.Append(styleName1);
    //            style1.Append(primaryStyle1);
    //            style1.Append(rsid1);
    //            style1.Append(styleParagraphProperties1);
    //            style1.Append(styleRunProperties1);

    //            Style style2 = new Style() { Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
    //            StyleName styleName2 = new StyleName() { Val = "Default Paragraph Font" };
    //            UIPriority uIPriority1 = new UIPriority() { Val = 1 };
    //            SemiHidden semiHidden1 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();

    //            style2.Append(styleName2);
    //            style2.Append(uIPriority1);
    //            style2.Append(semiHidden1);
    //            style2.Append(unhideWhenUsed1);

    //            Style style3 = new Style() { Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
    //            StyleName styleName3 = new StyleName() { Val = "Normal Table" };
    //            UIPriority uIPriority2 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden2 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();

    //            StyleTableProperties styleTableProperties1 = new StyleTableProperties();
    //            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

    //            TableCellMarginDefault tableCellMarginDefault2 = new TableCellMarginDefault();
    //            TopMargin topMargin10 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellLeftMargin tableCellLeftMargin2 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
    //            BottomMargin bottomMargin6 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellRightMargin tableCellRightMargin2 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

    //            tableCellMarginDefault2.Append(topMargin10);
    //            tableCellMarginDefault2.Append(tableCellLeftMargin2);
    //            tableCellMarginDefault2.Append(bottomMargin6);
    //            tableCellMarginDefault2.Append(tableCellRightMargin2);

    //            styleTableProperties1.Append(tableIndentation1);
    //            styleTableProperties1.Append(tableCellMarginDefault2);

    //            style3.Append(styleName3);
    //            style3.Append(uIPriority2);
    //            style3.Append(semiHidden2);
    //            style3.Append(unhideWhenUsed2);
    //            style3.Append(styleTableProperties1);

    //            Style style4 = new Style() { Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
    //            StyleName styleName4 = new StyleName() { Val = "No List" };
    //            UIPriority uIPriority3 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden3 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();

    //            style4.Append(styleName4);
    //            style4.Append(uIPriority3);
    //            style4.Append(semiHidden3);
    //            style4.Append(unhideWhenUsed3);

    //            Style style5 = new Style() { Type = StyleValues.Character, StyleId = "Hyperlink" };
    //            StyleName styleName5 = new StyleName() { Val = "Hyperlink" };
    //            UIPriority uIPriority4 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
    //            Rsid rsid2 = new Rsid() { Val = "00E778F2" };

    //            StyleRunProperties styleRunProperties2 = new StyleRunProperties();
    //            Color color1 = new Color() { Val = "0000FF" };
    //            Underline underline1 = new Underline() { Val = UnderlineValues.Single };

    //            styleRunProperties2.Append(color1);
    //            styleRunProperties2.Append(underline1);

    //            style5.Append(styleName5);
    //            style5.Append(uIPriority4);
    //            style5.Append(unhideWhenUsed4);
    //            style5.Append(rsid2);
    //            style5.Append(styleRunProperties2);

    //            Style style6 = new Style() { Type = StyleValues.Table, StyleId = "TableGrid" };
    //            StyleName styleName6 = new StyleName() { Val = "Table Grid" };
    //            BasedOn basedOn1 = new BasedOn() { Val = "TableNormal" };
    //            UIPriority uIPriority5 = new UIPriority() { Val = 59 };
    //            Rsid rsid3 = new Rsid() { Val = "00E778F2" };

    //            StyleTableProperties styleTableProperties2 = new StyleTableProperties();
    //            TableIndentation tableIndentation2 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

    //            TableBorders tableBorders2 = new TableBorders();
    //            TopBorder topBorder22 = new TopBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            LeftBorder leftBorder22 = new LeftBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            BottomBorder bottomBorder22 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            RightBorder rightBorder22 = new RightBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            InsideHorizontalBorder insideHorizontalBorder2 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
    //            InsideVerticalBorder insideVerticalBorder2 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

    //            tableBorders2.Append(topBorder22);
    //            tableBorders2.Append(leftBorder22);
    //            tableBorders2.Append(bottomBorder22);
    //            tableBorders2.Append(rightBorder22);
    //            tableBorders2.Append(insideHorizontalBorder2);
    //            tableBorders2.Append(insideVerticalBorder2);

    //            TableCellMarginDefault tableCellMarginDefault3 = new TableCellMarginDefault();
    //            TopMargin topMargin11 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellLeftMargin tableCellLeftMargin3 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
    //            BottomMargin bottomMargin7 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
    //            TableCellRightMargin tableCellRightMargin3 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

    //            tableCellMarginDefault3.Append(topMargin11);
    //            tableCellMarginDefault3.Append(tableCellLeftMargin3);
    //            tableCellMarginDefault3.Append(bottomMargin7);
    //            tableCellMarginDefault3.Append(tableCellRightMargin3);

    //            styleTableProperties2.Append(tableIndentation2);
    //            styleTableProperties2.Append(tableBorders2);
    //            styleTableProperties2.Append(tableCellMarginDefault3);

    //            style6.Append(styleName6);
    //            style6.Append(basedOn1);
    //            style6.Append(uIPriority5);
    //            style6.Append(rsid3);
    //            style6.Append(styleTableProperties2);

    //            Style style7 = new Style() { Type = StyleValues.Paragraph, StyleId = "ListParagraph" };
    //            StyleName styleName7 = new StyleName() { Val = "List Paragraph" };
    //            BasedOn basedOn2 = new BasedOn() { Val = "Normal" };
    //            UIPriority uIPriority6 = new UIPriority() { Val = 34 };
    //            PrimaryStyle primaryStyle2 = new PrimaryStyle();
    //            Rsid rsid4 = new Rsid() { Val = "00EB1E85" };

    //            StyleParagraphProperties styleParagraphProperties2 = new StyleParagraphProperties();
    //            Indentation indentation16 = new Indentation() { Left = "720" };
    //            ContextualSpacing contextualSpacing1 = new ContextualSpacing();

    //            styleParagraphProperties2.Append(indentation16);
    //            styleParagraphProperties2.Append(contextualSpacing1);

    //            style7.Append(styleName7);
    //            style7.Append(basedOn2);
    //            style7.Append(uIPriority6);
    //            style7.Append(primaryStyle2);
    //            style7.Append(rsid4);
    //            style7.Append(styleParagraphProperties2);

    //            Style style8 = new Style() { Type = StyleValues.Paragraph, StyleId = "BalloonText" };
    //            StyleName styleName8 = new StyleName() { Val = "Balloon Text" };
    //            BasedOn basedOn3 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "BalloonTextChar" };
    //            UIPriority uIPriority7 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden4 = new SemiHidden();
    //            UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();
    //            Rsid rsid5 = new Rsid() { Val = "00FF0A24" };

    //            StyleParagraphProperties styleParagraphProperties3 = new StyleParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines27 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties3.Append(spacingBetweenLines27);

    //            StyleRunProperties styleRunProperties3 = new StyleRunProperties();
    //            RunFonts runFonts2 = new RunFonts() { Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
    //            FontSize fontSize59 = new FontSize() { Val = "16" };
    //            FontSizeComplexScript fontSizeComplexScript59 = new FontSizeComplexScript() { Val = "16" };

    //            styleRunProperties3.Append(runFonts2);
    //            styleRunProperties3.Append(fontSize59);
    //            styleRunProperties3.Append(fontSizeComplexScript59);

    //            style8.Append(styleName8);
    //            style8.Append(basedOn3);
    //            style8.Append(linkedStyle1);
    //            style8.Append(uIPriority7);
    //            style8.Append(semiHidden4);
    //            style8.Append(unhideWhenUsed5);
    //            style8.Append(rsid5);
    //            style8.Append(styleParagraphProperties3);
    //            style8.Append(styleRunProperties3);

    //            Style style9 = new Style() { Type = StyleValues.Character, StyleId = "BalloonTextChar", CustomStyle = true };
    //            StyleName styleName9 = new StyleName() { Val = "Balloon Text Char" };
    //            LinkedStyle linkedStyle2 = new LinkedStyle() { Val = "BalloonText" };
    //            UIPriority uIPriority8 = new UIPriority() { Val = 99 };
    //            SemiHidden semiHidden5 = new SemiHidden();
    //            Rsid rsid6 = new Rsid() { Val = "00FF0A24" };

    //            StyleRunProperties styleRunProperties4 = new StyleRunProperties();
    //            RunFonts runFonts3 = new RunFonts() { Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
    //            FontSize fontSize60 = new FontSize() { Val = "16" };
    //            FontSizeComplexScript fontSizeComplexScript60 = new FontSizeComplexScript() { Val = "16" };

    //            styleRunProperties4.Append(runFonts3);
    //            styleRunProperties4.Append(fontSize60);
    //            styleRunProperties4.Append(fontSizeComplexScript60);

    //            style9.Append(styleName9);
    //            style9.Append(linkedStyle2);
    //            style9.Append(uIPriority8);
    //            style9.Append(semiHidden5);
    //            style9.Append(rsid6);
    //            style9.Append(styleRunProperties4);

    //            Style style10 = new Style() { Type = StyleValues.Paragraph, StyleId = "Header" };
    //            StyleName styleName10 = new StyleName() { Val = "header" };
    //            BasedOn basedOn4 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle3 = new LinkedStyle() { Val = "HeaderChar" };
    //            UIPriority uIPriority9 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed6 = new UnhideWhenUsed();
    //            Rsid rsid7 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties4 = new StyleParagraphProperties();

    //            Tabs tabs1 = new Tabs();
    //            TabStop tabStop1 = new TabStop() { Val = TabStopValues.Center, Position = 4680 };
    //            TabStop tabStop2 = new TabStop() { Val = TabStopValues.Right, Position = 9360 };

    //            tabs1.Append(tabStop1);
    //            tabs1.Append(tabStop2);
    //            SpacingBetweenLines spacingBetweenLines28 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties4.Append(tabs1);
    //            styleParagraphProperties4.Append(spacingBetweenLines28);

    //            style10.Append(styleName10);
    //            style10.Append(basedOn4);
    //            style10.Append(linkedStyle3);
    //            style10.Append(uIPriority9);
    //            style10.Append(unhideWhenUsed6);
    //            style10.Append(rsid7);
    //            style10.Append(styleParagraphProperties4);

    //            Style style11 = new Style() { Type = StyleValues.Character, StyleId = "HeaderChar", CustomStyle = true };
    //            StyleName styleName11 = new StyleName() { Val = "Header Char" };
    //            BasedOn basedOn5 = new BasedOn() { Val = "DefaultParagraphFont" };
    //            LinkedStyle linkedStyle4 = new LinkedStyle() { Val = "Header" };
    //            UIPriority uIPriority10 = new UIPriority() { Val = 99 };
    //            Rsid rsid8 = new Rsid() { Val = "00EF483A" };

    //            StyleRunProperties styleRunProperties5 = new StyleRunProperties();
    //            FontSize fontSize61 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript61 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties5.Append(fontSize61);
    //            styleRunProperties5.Append(fontSizeComplexScript61);

    //            style11.Append(styleName11);
    //            style11.Append(basedOn5);
    //            style11.Append(linkedStyle4);
    //            style11.Append(uIPriority10);
    //            style11.Append(rsid8);
    //            style11.Append(styleRunProperties5);

    //            Style style12 = new Style() { Type = StyleValues.Paragraph, StyleId = "Footer" };
    //            StyleName styleName12 = new StyleName() { Val = "footer" };
    //            BasedOn basedOn6 = new BasedOn() { Val = "Normal" };
    //            LinkedStyle linkedStyle5 = new LinkedStyle() { Val = "FooterChar" };
    //            UIPriority uIPriority11 = new UIPriority() { Val = 99 };
    //            UnhideWhenUsed unhideWhenUsed7 = new UnhideWhenUsed();
    //            Rsid rsid9 = new Rsid() { Val = "00EF483A" };

    //            StyleParagraphProperties styleParagraphProperties5 = new StyleParagraphProperties();

    //            Tabs tabs2 = new Tabs();
    //            TabStop tabStop3 = new TabStop() { Val = TabStopValues.Center, Position = 4680 };
    //            TabStop tabStop4 = new TabStop() { Val = TabStopValues.Right, Position = 9360 };

    //            tabs2.Append(tabStop3);
    //            tabs2.Append(tabStop4);
    //            SpacingBetweenLines spacingBetweenLines29 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            styleParagraphProperties5.Append(tabs2);
    //            styleParagraphProperties5.Append(spacingBetweenLines29);

    //            style12.Append(styleName12);
    //            style12.Append(basedOn6);
    //            style12.Append(linkedStyle5);
    //            style12.Append(uIPriority11);
    //            style12.Append(unhideWhenUsed7);
    //            style12.Append(rsid9);
    //            style12.Append(styleParagraphProperties5);

    //            Style style13 = new Style() { Type = StyleValues.Character, StyleId = "FooterChar", CustomStyle = true };
    //            StyleName styleName13 = new StyleName() { Val = "Footer Char" };
    //            BasedOn basedOn7 = new BasedOn() { Val = "DefaultParagraphFont" };
    //            LinkedStyle linkedStyle6 = new LinkedStyle() { Val = "Footer" };
    //            UIPriority uIPriority12 = new UIPriority() { Val = 99 };
    //            Rsid rsid10 = new Rsid() { Val = "00EF483A" };

    //            StyleRunProperties styleRunProperties6 = new StyleRunProperties();
    //            FontSize fontSize62 = new FontSize() { Val = "22" };
    //            FontSizeComplexScript fontSizeComplexScript62 = new FontSizeComplexScript() { Val = "22" };

    //            styleRunProperties6.Append(fontSize62);
    //            styleRunProperties6.Append(fontSizeComplexScript62);

    //            style13.Append(styleName13);
    //            style13.Append(basedOn7);
    //            style13.Append(linkedStyle6);
    //            style13.Append(uIPriority12);
    //            style13.Append(rsid10);
    //            style13.Append(styleRunProperties6);

    //            //styles1.Append(docDefaults1);
    //            styles1.Append(latentStyles1);
    //            styles1.Append(style1);
    //            styles1.Append(style2);
    //            styles1.Append(style3);
    //            styles1.Append(style4);
    //            styles1.Append(style5);
    //            styles1.Append(style6);
    //            styles1.Append(style7);
    //            styles1.Append(style8);
    //            styles1.Append(style9);
    //            styles1.Append(style10);
    //            styles1.Append(style11);
    //            styles1.Append(style12);
    //            styles1.Append(style13);

    //            stylesWithEffectsPart1.Styles = styles1;
    //        }
    //    }

    //    private static object GenerateEndnotesPart1Content_LockObject = new object();
    //    public static void GenerateEndnotesPart1Content(EndnotesPart endnotesPart1)
    //    {
    //        lock (GenerateEndnotesPart1Content_LockObject)
    //        {
    //            Endnotes endnotes1 = new Endnotes() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 wp14" } };
    //            endnotes1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
    //            endnotes1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            endnotes1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //            endnotes1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            endnotes1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //            endnotes1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //            endnotes1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
    //            endnotes1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    //            endnotes1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //            endnotes1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            endnotes1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            endnotes1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
    //            endnotes1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
    //            endnotes1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
    //            endnotes1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

    //            Endnote endnote1 = new Endnote() { Type = FootnoteEndnoteValues.Separator, Id = -1 };

    //            Paragraph paragraph42 = new Paragraph() { RsidParagraphAddition = "00B2636C", RsidParagraphProperties = "00EF483A", RsidRunAdditionDefault = "00B2636C" };

    //            ParagraphProperties paragraphProperties37 = new ParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines30 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            paragraphProperties37.Append(spacingBetweenLines30);

    //            Run run31 = new Run();
    //            SeparatorMark separatorMark1 = new SeparatorMark();

    //            run31.Append(separatorMark1);

    //            paragraph42.Append(paragraphProperties37);
    //            paragraph42.Append(run31);

    //            endnote1.Append(paragraph42);

    //            Endnote endnote2 = new Endnote() { Type = FootnoteEndnoteValues.ContinuationSeparator, Id = 0 };

    //            Paragraph paragraph43 = new Paragraph() { RsidParagraphAddition = "00B2636C", RsidParagraphProperties = "00EF483A", RsidRunAdditionDefault = "00B2636C" };

    //            ParagraphProperties paragraphProperties38 = new ParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines31 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            paragraphProperties38.Append(spacingBetweenLines31);

    //            Run run32 = new Run();
    //            ContinuationSeparatorMark continuationSeparatorMark1 = new ContinuationSeparatorMark();

    //            run32.Append(continuationSeparatorMark1);

    //            paragraph43.Append(paragraphProperties38);
    //            paragraph43.Append(run32);

    //            endnote2.Append(paragraph43);

    //            endnotes1.Append(endnote1);
    //            endnotes1.Append(endnote2);

    //            endnotesPart1.Endnotes = endnotes1;
    //        }
    //    }

    //    private static object GenerateNumberingDefinitionsPart1Content_LockObject = new object();
    //    public static void GenerateNumberingDefinitionsPart1Content(NumberingDefinitionsPart numberingDefinitionsPart1)
    //    {
    //        lock (GenerateNumberingDefinitionsPart1Content_LockObject)
    //        {
    //            Numbering numbering1 = new Numbering() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 wp14" } };
    //            numbering1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
    //            numbering1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            numbering1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //            numbering1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            numbering1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //            numbering1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //            numbering1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
    //            numbering1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    //            numbering1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //            numbering1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            numbering1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            numbering1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
    //            numbering1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
    //            numbering1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
    //            numbering1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

    //            AbstractNum abstractNum1 = new AbstractNum() { AbstractNumberId = 0 };
    //            Nsid nsid1 = new Nsid() { Val = "73E03B5C" };
    //            MultiLevelType multiLevelType1 = new MultiLevelType() { Val = MultiLevelValues.HybridMultilevel };
    //            TemplateCode templateCode1 = new TemplateCode() { Val = "D36EB1F2" };

    //            Level level1 = new Level() { LevelIndex = 0, TemplateCode = "04090001" };
    //            StartNumberingValue startNumberingValue1 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat1 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText1 = new LevelText() { Val = "·" };
    //            LevelJustification levelJustification1 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties1 = new PreviousParagraphProperties();
    //            Indentation indentation18 = new Indentation() { Left = "360", Hanging = "360" };

    //            previousParagraphProperties1.Append(indentation18);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties1 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts7 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

    //            numberingSymbolRunProperties1.Append(runFonts7);

    //            level1.Append(startNumberingValue1);
    //            level1.Append(numberingFormat1);
    //            level1.Append(levelText1);
    //            level1.Append(levelJustification1);
    //            level1.Append(previousParagraphProperties1);
    //            level1.Append(numberingSymbolRunProperties1);

    //            Level level2 = new Level() { LevelIndex = 1, TemplateCode = "04090003", Tentative = true };
    //            StartNumberingValue startNumberingValue2 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat2 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText2 = new LevelText() { Val = "o" };
    //            LevelJustification levelJustification2 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties2 = new PreviousParagraphProperties();
    //            Indentation indentation19 = new Indentation() { Left = "1080", Hanging = "360" };

    //            previousParagraphProperties2.Append(indentation19);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties2 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts8 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

    //            numberingSymbolRunProperties2.Append(runFonts8);

    //            level2.Append(startNumberingValue2);
    //            level2.Append(numberingFormat2);
    //            level2.Append(levelText2);
    //            level2.Append(levelJustification2);
    //            level2.Append(previousParagraphProperties2);
    //            level2.Append(numberingSymbolRunProperties2);

    //            Level level3 = new Level() { LevelIndex = 2, TemplateCode = "04090005", Tentative = true };
    //            StartNumberingValue startNumberingValue3 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat3 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText3 = new LevelText() { Val = "§" };
    //            LevelJustification levelJustification3 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties3 = new PreviousParagraphProperties();
    //            Indentation indentation20 = new Indentation() { Left = "1800", Hanging = "360" };

    //            previousParagraphProperties3.Append(indentation20);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties3 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts9 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

    //            numberingSymbolRunProperties3.Append(runFonts9);

    //            level3.Append(startNumberingValue3);
    //            level3.Append(numberingFormat3);
    //            level3.Append(levelText3);
    //            level3.Append(levelJustification3);
    //            level3.Append(previousParagraphProperties3);
    //            level3.Append(numberingSymbolRunProperties3);

    //            Level level4 = new Level() { LevelIndex = 3, TemplateCode = "04090001", Tentative = true };
    //            StartNumberingValue startNumberingValue4 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat4 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText4 = new LevelText() { Val = "·" };
    //            LevelJustification levelJustification4 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties4 = new PreviousParagraphProperties();
    //            Indentation indentation21 = new Indentation() { Left = "2520", Hanging = "360" };

    //            previousParagraphProperties4.Append(indentation21);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties4 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts10 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

    //            numberingSymbolRunProperties4.Append(runFonts10);

    //            level4.Append(startNumberingValue4);
    //            level4.Append(numberingFormat4);
    //            level4.Append(levelText4);
    //            level4.Append(levelJustification4);
    //            level4.Append(previousParagraphProperties4);
    //            level4.Append(numberingSymbolRunProperties4);

    //            Level level5 = new Level() { LevelIndex = 4, TemplateCode = "04090003", Tentative = true };
    //            StartNumberingValue startNumberingValue5 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat5 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText5 = new LevelText() { Val = "o" };
    //            LevelJustification levelJustification5 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties5 = new PreviousParagraphProperties();
    //            Indentation indentation22 = new Indentation() { Left = "3240", Hanging = "360" };

    //            previousParagraphProperties5.Append(indentation22);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties5 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts11 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

    //            numberingSymbolRunProperties5.Append(runFonts11);

    //            level5.Append(startNumberingValue5);
    //            level5.Append(numberingFormat5);
    //            level5.Append(levelText5);
    //            level5.Append(levelJustification5);
    //            level5.Append(previousParagraphProperties5);
    //            level5.Append(numberingSymbolRunProperties5);

    //            Level level6 = new Level() { LevelIndex = 5, TemplateCode = "04090005", Tentative = true };
    //            StartNumberingValue startNumberingValue6 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat6 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText6 = new LevelText() { Val = "§" };
    //            LevelJustification levelJustification6 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties6 = new PreviousParagraphProperties();
    //            Indentation indentation23 = new Indentation() { Left = "3960", Hanging = "360" };

    //            previousParagraphProperties6.Append(indentation23);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties6 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts12 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

    //            numberingSymbolRunProperties6.Append(runFonts12);

    //            level6.Append(startNumberingValue6);
    //            level6.Append(numberingFormat6);
    //            level6.Append(levelText6);
    //            level6.Append(levelJustification6);
    //            level6.Append(previousParagraphProperties6);
    //            level6.Append(numberingSymbolRunProperties6);

    //            Level level7 = new Level() { LevelIndex = 6, TemplateCode = "04090001", Tentative = true };
    //            StartNumberingValue startNumberingValue7 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat7 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText7 = new LevelText() { Val = "·" };
    //            LevelJustification levelJustification7 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties7 = new PreviousParagraphProperties();
    //            Indentation indentation24 = new Indentation() { Left = "4680", Hanging = "360" };

    //            previousParagraphProperties7.Append(indentation24);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties7 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts13 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

    //            numberingSymbolRunProperties7.Append(runFonts13);

    //            level7.Append(startNumberingValue7);
    //            level7.Append(numberingFormat7);
    //            level7.Append(levelText7);
    //            level7.Append(levelJustification7);
    //            level7.Append(previousParagraphProperties7);
    //            level7.Append(numberingSymbolRunProperties7);

    //            Level level8 = new Level() { LevelIndex = 7, TemplateCode = "04090003", Tentative = true };
    //            StartNumberingValue startNumberingValue8 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat8 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText8 = new LevelText() { Val = "o" };
    //            LevelJustification levelJustification8 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties8 = new PreviousParagraphProperties();
    //            Indentation indentation25 = new Indentation() { Left = "5400", Hanging = "360" };

    //            previousParagraphProperties8.Append(indentation25);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties8 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts14 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

    //            numberingSymbolRunProperties8.Append(runFonts14);

    //            level8.Append(startNumberingValue8);
    //            level8.Append(numberingFormat8);
    //            level8.Append(levelText8);
    //            level8.Append(levelJustification8);
    //            level8.Append(previousParagraphProperties8);
    //            level8.Append(numberingSymbolRunProperties8);

    //            Level level9 = new Level() { LevelIndex = 8, TemplateCode = "04090005", Tentative = true };
    //            StartNumberingValue startNumberingValue9 = new StartNumberingValue() { Val = 1 };
    //            NumberingFormat numberingFormat9 = new NumberingFormat() { Val = NumberFormatValues.Bullet };
    //            LevelText levelText9 = new LevelText() { Val = "§" };
    //            LevelJustification levelJustification9 = new LevelJustification() { Val = LevelJustificationValues.Left };

    //            PreviousParagraphProperties previousParagraphProperties9 = new PreviousParagraphProperties();
    //            Indentation indentation26 = new Indentation() { Left = "6120", Hanging = "360" };

    //            previousParagraphProperties9.Append(indentation26);

    //            NumberingSymbolRunProperties numberingSymbolRunProperties9 = new NumberingSymbolRunProperties();
    //            RunFonts runFonts15 = new RunFonts() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

    //            numberingSymbolRunProperties9.Append(runFonts15);

    //            level9.Append(startNumberingValue9);
    //            level9.Append(numberingFormat9);
    //            level9.Append(levelText9);
    //            level9.Append(levelJustification9);
    //            level9.Append(previousParagraphProperties9);
    //            level9.Append(numberingSymbolRunProperties9);

    //            abstractNum1.Append(nsid1);
    //            abstractNum1.Append(multiLevelType1);
    //            abstractNum1.Append(templateCode1);
    //            abstractNum1.Append(level1);
    //            abstractNum1.Append(level2);
    //            abstractNum1.Append(level3);
    //            abstractNum1.Append(level4);
    //            abstractNum1.Append(level5);
    //            abstractNum1.Append(level6);
    //            abstractNum1.Append(level7);
    //            abstractNum1.Append(level8);
    //            abstractNum1.Append(level9);

    //            NumberingInstance numberingInstance1 = new NumberingInstance() { NumberID = 1 };
    //            AbstractNumId abstractNumId1 = new AbstractNumId() { Val = 0 };

    //            numberingInstance1.Append(abstractNumId1);

    //            numbering1.Append(abstractNum1);
    //            numbering1.Append(numberingInstance1);

    //            numberingDefinitionsPart1.Numbering = numbering1;
    //        }
    //    }

    //    private static object GenerateFootnotesPart1Content_LockObject = new object();
    //    public static void GenerateFootnotesPart1Content(FootnotesPart footnotesPart1)
    //    {
    //        lock (GenerateFootnotesPart1Content_LockObject)
    //        {
    //            Footnotes footnotes1 = new Footnotes() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 wp14" } };
    //            footnotes1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
    //            footnotes1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            footnotes1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //            footnotes1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            footnotes1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //            footnotes1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //            footnotes1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
    //            footnotes1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    //            footnotes1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //            footnotes1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            footnotes1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            footnotes1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
    //            footnotes1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
    //            footnotes1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
    //            footnotes1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

    //            Footnote footnote1 = new Footnote() { Type = FootnoteEndnoteValues.Separator, Id = -1 };

    //            Paragraph paragraph44 = new Paragraph() { RsidParagraphAddition = "00B2636C", RsidParagraphProperties = "00EF483A", RsidRunAdditionDefault = "00B2636C" };

    //            ParagraphProperties paragraphProperties39 = new ParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines36 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            paragraphProperties39.Append(spacingBetweenLines36);

    //            Run run33 = new Run();
    //            SeparatorMark separatorMark2 = new SeparatorMark();

    //            run33.Append(separatorMark2);

    //            paragraph44.Append(paragraphProperties39);
    //            paragraph44.Append(run33);

    //            footnote1.Append(paragraph44);

    //            Footnote footnote2 = new Footnote() { Type = FootnoteEndnoteValues.ContinuationSeparator, Id = 0 };

    //            Paragraph paragraph45 = new Paragraph() { RsidParagraphAddition = "00B2636C", RsidParagraphProperties = "00EF483A", RsidRunAdditionDefault = "00B2636C" };

    //            ParagraphProperties paragraphProperties40 = new ParagraphProperties();
    //            SpacingBetweenLines spacingBetweenLines37 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

    //            paragraphProperties40.Append(spacingBetweenLines37);

    //            Run run34 = new Run();
    //            ContinuationSeparatorMark continuationSeparatorMark2 = new ContinuationSeparatorMark();

    //            run34.Append(continuationSeparatorMark2);

    //            paragraph45.Append(paragraphProperties40);
    //            paragraph45.Append(run34);

    //            footnote2.Append(paragraph45);

    //            footnotes1.Append(footnote1);
    //            footnotes1.Append(footnote2);

    //            footnotesPart1.Footnotes = footnotes1;
    //        }
    //    }

    //    private static object GenerateWebSettingsPart1Content_LockObject = new object();
    //    public static void GenerateWebSettingsPart1Content(WebSettingsPart webSettingsPart1)
    //    {
    //        lock (GenerateWebSettingsPart1Content_LockObject)
    //        {
    //            WebSettings webSettings1 = new WebSettings() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14" } };
    //            webSettings1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            webSettings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            webSettings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            webSettings1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            OptimizeForBrowser optimizeForBrowser1 = new OptimizeForBrowser();
    //            RelyOnVML relyOnVML1 = new RelyOnVML();
    //            AllowPNG allowPNG1 = new AllowPNG();

    //            webSettings1.Append(optimizeForBrowser1);
    //            webSettings1.Append(relyOnVML1);
    //            webSettings1.Append(allowPNG1);

    //            webSettingsPart1.WebSettings = webSettings1;
    //        }
    //    }

    //    private static object GenerateThemePart1Content_LockObject = new object();
    //    public static void GenerateThemePart1Content(ThemePart themePart1)
    //    {
    //        lock (GenerateThemePart1Content_LockObject)
    //        {
    //            A.Theme theme1 = new A.Theme() { Name = "Office Theme" };
    //            theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

    //            A.ThemeElements themeElements1 = new A.ThemeElements();

    //            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Office" };

    //            A.Dark1Color dark1Color1 = new A.Dark1Color();
    //            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

    //            dark1Color1.Append(systemColor1);

    //            A.Light1Color light1Color1 = new A.Light1Color();
    //            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

    //            light1Color1.Append(systemColor2);

    //            A.Dark2Color dark2Color1 = new A.Dark2Color();
    //            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "1F497D" };

    //            dark2Color1.Append(rgbColorModelHex1);

    //            A.Light2Color light2Color1 = new A.Light2Color();
    //            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "EEECE1" };

    //            light2Color1.Append(rgbColorModelHex2);

    //            A.Accent1Color accent1Color1 = new A.Accent1Color();
    //            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "4F81BD" };

    //            accent1Color1.Append(rgbColorModelHex3);

    //            A.Accent2Color accent2Color1 = new A.Accent2Color();
    //            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "C0504D" };

    //            accent2Color1.Append(rgbColorModelHex4);

    //            A.Accent3Color accent3Color1 = new A.Accent3Color();
    //            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "9BBB59" };

    //            accent3Color1.Append(rgbColorModelHex5);

    //            A.Accent4Color accent4Color1 = new A.Accent4Color();
    //            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "8064A2" };

    //            accent4Color1.Append(rgbColorModelHex6);

    //            A.Accent5Color accent5Color1 = new A.Accent5Color();
    //            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4BACC6" };

    //            accent5Color1.Append(rgbColorModelHex7);

    //            A.Accent6Color accent6Color1 = new A.Accent6Color();
    //            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "F79646" };

    //            accent6Color1.Append(rgbColorModelHex8);

    //            A.Hyperlink hyperlink2 = new A.Hyperlink();
    //            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0000FF" };

    //            hyperlink2.Append(rgbColorModelHex9);

    //            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
    //            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "800080" };

    //            followedHyperlinkColor1.Append(rgbColorModelHex10);

    //            colorScheme1.Append(dark1Color1);
    //            colorScheme1.Append(light1Color1);
    //            colorScheme1.Append(dark2Color1);
    //            colorScheme1.Append(light2Color1);
    //            colorScheme1.Append(accent1Color1);
    //            colorScheme1.Append(accent2Color1);
    //            colorScheme1.Append(accent3Color1);
    //            colorScheme1.Append(accent4Color1);
    //            colorScheme1.Append(accent5Color1);
    //            colorScheme1.Append(accent6Color1);
    //            colorScheme1.Append(hyperlink2);
    //            colorScheme1.Append(followedHyperlinkColor1);

    //            A.FontScheme fontScheme1 = new A.FontScheme() { Name = "Office" };

    //            A.MajorFont majorFont1 = new A.MajorFont();
    //            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Cambria" };
    //            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
    //            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
    //            A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ ゴシック" };
    //            A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
    //            A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
    //            A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
    //            A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
    //            A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
    //            A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Angsana New" };
    //            A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
    //            A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
    //            A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
    //            A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
    //            A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
    //            A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
    //            A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
    //            A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
    //            A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
    //            A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
    //            A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
    //            A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
    //            A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
    //            A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
    //            A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
    //            A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
    //            A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
    //            A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
    //            A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
    //            A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
    //            A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
    //            A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
    //            A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

    //            majorFont1.Append(latinFont1);
    //            majorFont1.Append(eastAsianFont1);
    //            majorFont1.Append(complexScriptFont1);
    //            majorFont1.Append(supplementalFont1);
    //            majorFont1.Append(supplementalFont2);
    //            majorFont1.Append(supplementalFont3);
    //            majorFont1.Append(supplementalFont4);
    //            majorFont1.Append(supplementalFont5);
    //            majorFont1.Append(supplementalFont6);
    //            majorFont1.Append(supplementalFont7);
    //            majorFont1.Append(supplementalFont8);
    //            majorFont1.Append(supplementalFont9);
    //            majorFont1.Append(supplementalFont10);
    //            majorFont1.Append(supplementalFont11);
    //            majorFont1.Append(supplementalFont12);
    //            majorFont1.Append(supplementalFont13);
    //            majorFont1.Append(supplementalFont14);
    //            majorFont1.Append(supplementalFont15);
    //            majorFont1.Append(supplementalFont16);
    //            majorFont1.Append(supplementalFont17);
    //            majorFont1.Append(supplementalFont18);
    //            majorFont1.Append(supplementalFont19);
    //            majorFont1.Append(supplementalFont20);
    //            majorFont1.Append(supplementalFont21);
    //            majorFont1.Append(supplementalFont22);
    //            majorFont1.Append(supplementalFont23);
    //            majorFont1.Append(supplementalFont24);
    //            majorFont1.Append(supplementalFont25);
    //            majorFont1.Append(supplementalFont26);
    //            majorFont1.Append(supplementalFont27);
    //            majorFont1.Append(supplementalFont28);
    //            majorFont1.Append(supplementalFont29);
    //            majorFont1.Append(supplementalFont30);

    //            A.MinorFont minorFont1 = new A.MinorFont();
    //            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri" };
    //            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
    //            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
    //            A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ 明朝" };
    //            A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
    //            A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
    //            A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
    //            A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
    //            A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
    //            A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Thai", Typeface = "Cordia New" };
    //            A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
    //            A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
    //            A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
    //            A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
    //            A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
    //            A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
    //            A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
    //            A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
    //            A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
    //            A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
    //            A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
    //            A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
    //            A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
    //            A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
    //            A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
    //            A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
    //            A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
    //            A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
    //            A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
    //            A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
    //            A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
    //            A.SupplementalFont supplementalFont59 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };
    //            A.SupplementalFont supplementalFont60 = new A.SupplementalFont() { Script = "Geor", Typeface = "Sylfaen" };

    //            minorFont1.Append(latinFont2);
    //            minorFont1.Append(eastAsianFont2);
    //            minorFont1.Append(complexScriptFont2);
    //            minorFont1.Append(supplementalFont31);
    //            minorFont1.Append(supplementalFont32);
    //            minorFont1.Append(supplementalFont33);
    //            minorFont1.Append(supplementalFont34);
    //            minorFont1.Append(supplementalFont35);
    //            minorFont1.Append(supplementalFont36);
    //            minorFont1.Append(supplementalFont37);
    //            minorFont1.Append(supplementalFont38);
    //            minorFont1.Append(supplementalFont39);
    //            minorFont1.Append(supplementalFont40);
    //            minorFont1.Append(supplementalFont41);
    //            minorFont1.Append(supplementalFont42);
    //            minorFont1.Append(supplementalFont43);
    //            minorFont1.Append(supplementalFont44);
    //            minorFont1.Append(supplementalFont45);
    //            minorFont1.Append(supplementalFont46);
    //            minorFont1.Append(supplementalFont47);
    //            minorFont1.Append(supplementalFont48);
    //            minorFont1.Append(supplementalFont49);
    //            minorFont1.Append(supplementalFont50);
    //            minorFont1.Append(supplementalFont51);
    //            minorFont1.Append(supplementalFont52);
    //            minorFont1.Append(supplementalFont53);
    //            minorFont1.Append(supplementalFont54);
    //            minorFont1.Append(supplementalFont55);
    //            minorFont1.Append(supplementalFont56);
    //            minorFont1.Append(supplementalFont57);
    //            minorFont1.Append(supplementalFont58);
    //            minorFont1.Append(supplementalFont59);
    //            minorFont1.Append(supplementalFont60);

    //            fontScheme1.Append(majorFont1);
    //            fontScheme1.Append(minorFont1);

    //            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Office" };

    //            A.FillStyleList fillStyleList1 = new A.FillStyleList();

    //            A.SolidFill solidFill1 = new A.SolidFill();
    //            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

    //            solidFill1.Append(schemeColor1);

    //            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

    //            A.GradientStopList gradientStopList1 = new A.GradientStopList();

    //            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

    //            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint1 = new A.Tint() { Val = 50000 };
    //            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 300000 };

    //            schemeColor2.Append(tint1);
    //            schemeColor2.Append(saturationModulation1);

    //            gradientStop1.Append(schemeColor2);

    //            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 35000 };

    //            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint2 = new A.Tint() { Val = 37000 };
    //            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 300000 };

    //            schemeColor3.Append(tint2);
    //            schemeColor3.Append(saturationModulation2);

    //            gradientStop2.Append(schemeColor3);

    //            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

    //            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint3 = new A.Tint() { Val = 15000 };
    //            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 350000 };

    //            schemeColor4.Append(tint3);
    //            schemeColor4.Append(saturationModulation3);

    //            gradientStop3.Append(schemeColor4);

    //            gradientStopList1.Append(gradientStop1);
    //            gradientStopList1.Append(gradientStop2);
    //            gradientStopList1.Append(gradientStop3);
    //            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 16200000, Scaled = true };

    //            gradientFill1.Append(gradientStopList1);
    //            gradientFill1.Append(linearGradientFill1);

    //            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

    //            A.GradientStopList gradientStopList2 = new A.GradientStopList();

    //            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

    //            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade1 = new A.Shade() { Val = 51000 };
    //            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 130000 };

    //            schemeColor5.Append(shade1);
    //            schemeColor5.Append(saturationModulation4);

    //            gradientStop4.Append(schemeColor5);

    //            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 80000 };

    //            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade2 = new A.Shade() { Val = 93000 };
    //            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 130000 };

    //            schemeColor6.Append(shade2);
    //            schemeColor6.Append(saturationModulation5);

    //            gradientStop5.Append(schemeColor6);

    //            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

    //            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade3 = new A.Shade() { Val = 94000 };
    //            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 135000 };

    //            schemeColor7.Append(shade3);
    //            schemeColor7.Append(saturationModulation6);

    //            gradientStop6.Append(schemeColor7);

    //            gradientStopList2.Append(gradientStop4);
    //            gradientStopList2.Append(gradientStop5);
    //            gradientStopList2.Append(gradientStop6);
    //            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 16200000, Scaled = false };

    //            gradientFill2.Append(gradientStopList2);
    //            gradientFill2.Append(linearGradientFill2);

    //            fillStyleList1.Append(solidFill1);
    //            fillStyleList1.Append(gradientFill1);
    //            fillStyleList1.Append(gradientFill2);

    //            A.LineStyleList lineStyleList1 = new A.LineStyleList();

    //            A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

    //            A.SolidFill solidFill2 = new A.SolidFill();

    //            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade4 = new A.Shade() { Val = 95000 };
    //            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 105000 };

    //            schemeColor8.Append(shade4);
    //            schemeColor8.Append(saturationModulation7);

    //            solidFill2.Append(schemeColor8);
    //            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

    //            outline1.Append(solidFill2);
    //            outline1.Append(presetDash1);

    //            A.Outline outline2 = new A.Outline() { Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

    //            A.SolidFill solidFill3 = new A.SolidFill();
    //            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

    //            solidFill3.Append(schemeColor9);
    //            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

    //            outline2.Append(solidFill3);
    //            outline2.Append(presetDash2);

    //            A.Outline outline3 = new A.Outline() { Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

    //            A.SolidFill solidFill4 = new A.SolidFill();
    //            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

    //            solidFill4.Append(schemeColor10);
    //            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

    //            outline3.Append(solidFill4);
    //            outline3.Append(presetDash3);

    //            lineStyleList1.Append(outline1);
    //            lineStyleList1.Append(outline2);
    //            lineStyleList1.Append(outline3);

    //            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

    //            A.EffectStyle effectStyle1 = new A.EffectStyle();

    //            A.EffectList effectList1 = new A.EffectList();

    //            A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

    //            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
    //            A.Alpha alpha1 = new A.Alpha() { Val = 38000 };

    //            rgbColorModelHex11.Append(alpha1);

    //            outerShadow1.Append(rgbColorModelHex11);

    //            effectList1.Append(outerShadow1);

    //            effectStyle1.Append(effectList1);

    //            A.EffectStyle effectStyle2 = new A.EffectStyle();

    //            A.EffectList effectList2 = new A.EffectList();

    //            A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

    //            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
    //            A.Alpha alpha2 = new A.Alpha() { Val = 35000 };

    //            rgbColorModelHex12.Append(alpha2);

    //            outerShadow2.Append(rgbColorModelHex12);

    //            effectList2.Append(outerShadow2);

    //            effectStyle2.Append(effectList2);

    //            A.EffectStyle effectStyle3 = new A.EffectStyle();

    //            A.EffectList effectList3 = new A.EffectList();

    //            A.OuterShadow outerShadow3 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

    //            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
    //            A.Alpha alpha3 = new A.Alpha() { Val = 35000 };

    //            rgbColorModelHex13.Append(alpha3);

    //            outerShadow3.Append(rgbColorModelHex13);

    //            effectList3.Append(outerShadow3);

    //            A.Scene3DType scene3DType1 = new A.Scene3DType();

    //            A.Camera camera1 = new A.Camera() { Preset = A.PresetCameraValues.OrthographicFront };
    //            A.Rotation rotation1 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 0 };

    //            camera1.Append(rotation1);

    //            A.LightRig lightRig1 = new A.LightRig() { Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
    //            A.Rotation rotation2 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 1200000 };

    //            lightRig1.Append(rotation2);

    //            scene3DType1.Append(camera1);
    //            scene3DType1.Append(lightRig1);

    //            A.Shape3DType shape3DType1 = new A.Shape3DType();
    //            A.BevelTop bevelTop1 = new A.BevelTop() { Width = 63500L, Height = 25400L };

    //            shape3DType1.Append(bevelTop1);

    //            effectStyle3.Append(effectList3);
    //            effectStyle3.Append(scene3DType1);
    //            effectStyle3.Append(shape3DType1);

    //            effectStyleList1.Append(effectStyle1);
    //            effectStyleList1.Append(effectStyle2);
    //            effectStyleList1.Append(effectStyle3);

    //            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

    //            A.SolidFill solidFill5 = new A.SolidFill();
    //            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

    //            solidFill5.Append(schemeColor11);

    //            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

    //            A.GradientStopList gradientStopList3 = new A.GradientStopList();

    //            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

    //            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint4 = new A.Tint() { Val = 40000 };
    //            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 350000 };

    //            schemeColor12.Append(tint4);
    //            schemeColor12.Append(saturationModulation8);

    //            gradientStop7.Append(schemeColor12);

    //            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 40000 };

    //            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint5 = new A.Tint() { Val = 45000 };
    //            A.Shade shade5 = new A.Shade() { Val = 99000 };
    //            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 350000 };

    //            schemeColor13.Append(tint5);
    //            schemeColor13.Append(shade5);
    //            schemeColor13.Append(saturationModulation9);

    //            gradientStop8.Append(schemeColor13);

    //            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

    //            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade6 = new A.Shade() { Val = 20000 };
    //            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 255000 };

    //            schemeColor14.Append(shade6);
    //            schemeColor14.Append(saturationModulation10);

    //            gradientStop9.Append(schemeColor14);

    //            gradientStopList3.Append(gradientStop7);
    //            gradientStopList3.Append(gradientStop8);
    //            gradientStopList3.Append(gradientStop9);

    //            A.PathGradientFill pathGradientFill1 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
    //            A.FillToRectangle fillToRectangle1 = new A.FillToRectangle() { Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

    //            pathGradientFill1.Append(fillToRectangle1);

    //            gradientFill3.Append(gradientStopList3);
    //            gradientFill3.Append(pathGradientFill1);

    //            A.GradientFill gradientFill4 = new A.GradientFill() { RotateWithShape = true };

    //            A.GradientStopList gradientStopList4 = new A.GradientStopList();

    //            A.GradientStop gradientStop10 = new A.GradientStop() { Position = 0 };

    //            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Tint tint6 = new A.Tint() { Val = 80000 };
    //            A.SaturationModulation saturationModulation11 = new A.SaturationModulation() { Val = 300000 };

    //            schemeColor15.Append(tint6);
    //            schemeColor15.Append(saturationModulation11);

    //            gradientStop10.Append(schemeColor15);

    //            A.GradientStop gradientStop11 = new A.GradientStop() { Position = 100000 };

    //            A.SchemeColor schemeColor16 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
    //            A.Shade shade7 = new A.Shade() { Val = 30000 };
    //            A.SaturationModulation saturationModulation12 = new A.SaturationModulation() { Val = 200000 };

    //            schemeColor16.Append(shade7);
    //            schemeColor16.Append(saturationModulation12);

    //            gradientStop11.Append(schemeColor16);

    //            gradientStopList4.Append(gradientStop10);
    //            gradientStopList4.Append(gradientStop11);

    //            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
    //            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle() { Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

    //            pathGradientFill2.Append(fillToRectangle2);

    //            gradientFill4.Append(gradientStopList4);
    //            gradientFill4.Append(pathGradientFill2);

    //            backgroundFillStyleList1.Append(solidFill5);
    //            backgroundFillStyleList1.Append(gradientFill3);
    //            backgroundFillStyleList1.Append(gradientFill4);

    //            formatScheme1.Append(fillStyleList1);
    //            formatScheme1.Append(lineStyleList1);
    //            formatScheme1.Append(effectStyleList1);
    //            formatScheme1.Append(backgroundFillStyleList1);

    //            themeElements1.Append(colorScheme1);
    //            themeElements1.Append(fontScheme1);
    //            themeElements1.Append(formatScheme1);
    //            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
    //            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

    //            theme1.Append(themeElements1);
    //            theme1.Append(objectDefaults1);
    //            theme1.Append(extraColorSchemeList1);

    //            themePart1.Theme = theme1;
    //        }
    //    }


    //    private static object GenerateDocumentSettingsPart1Content_LockObject = new object();
    //    public static void GenerateDocumentSettingsPart1Content(DocumentSettingsPart documentSettingsPart1)
    //    {
    //        lock (GenerateDocumentSettingsPart1Content_LockObject)
    //        {
    //            Settings settings1 = new Settings() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14" } };
    //            settings1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            settings1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
    //            settings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            settings1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    //            settings1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
    //            settings1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
    //            settings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            settings1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
    //            settings1.AddNamespaceDeclaration("sl", "http://schemas.openxmlformats.org/schemaLibrary/2006/main");
    //            Zoom zoom1 = new Zoom() { Percent = "190" };
    //            ProofState proofState1 = new ProofState() { Spelling = ProofingStateValues.Clean, Grammar = ProofingStateValues.Clean };
    //            DefaultTabStop defaultTabStop1 = new DefaultTabStop() { Val = 720 };
    //            DrawingGridHorizontalSpacing drawingGridHorizontalSpacing1 = new DrawingGridHorizontalSpacing() { Val = "110" };
    //            DisplayHorizontalDrawingGrid displayHorizontalDrawingGrid1 = new DisplayHorizontalDrawingGrid() { Val = 2 };
    //            CharacterSpacingControl characterSpacingControl1 = new CharacterSpacingControl() { Val = CharacterSpacingValues.DoNotCompress };

    //            FootnoteDocumentWideProperties footnoteDocumentWideProperties1 = new FootnoteDocumentWideProperties();
    //            FootnoteSpecialReference footnoteSpecialReference1 = new FootnoteSpecialReference() { Id = -1 };
    //            FootnoteSpecialReference footnoteSpecialReference2 = new FootnoteSpecialReference() { Id = 0 };

    //            footnoteDocumentWideProperties1.Append(footnoteSpecialReference1);
    //            footnoteDocumentWideProperties1.Append(footnoteSpecialReference2);

    //            EndnoteDocumentWideProperties endnoteDocumentWideProperties1 = new EndnoteDocumentWideProperties();
    //            EndnoteSpecialReference endnoteSpecialReference1 = new EndnoteSpecialReference() { Id = -1 };
    //            EndnoteSpecialReference endnoteSpecialReference2 = new EndnoteSpecialReference() { Id = 0 };

    //            endnoteDocumentWideProperties1.Append(endnoteSpecialReference1);
    //            endnoteDocumentWideProperties1.Append(endnoteSpecialReference2);

    //            Compatibility compatibility1 = new Compatibility();
    //            CompatibilitySetting compatibilitySetting1 = new CompatibilitySetting() { Name = CompatSettingNameValues.CompatibilityMode, Uri = "http://schemas.microsoft.com/office/word", Val = "14" };
    //            CompatibilitySetting compatibilitySetting2 = new CompatibilitySetting() { Name = CompatSettingNameValues.OverrideTableStyleFontSizeAndJustification, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };
    //            CompatibilitySetting compatibilitySetting3 = new CompatibilitySetting() { Name = CompatSettingNameValues.EnableOpenTypeFeatures, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };
    //            CompatibilitySetting compatibilitySetting4 = new CompatibilitySetting() { Name = CompatSettingNameValues.DoNotFlipMirrorIndents, Uri = "http://schemas.microsoft.com/office/word", Val = "1" };

    //            compatibility1.Append(compatibilitySetting1);
    //            compatibility1.Append(compatibilitySetting2);
    //            compatibility1.Append(compatibilitySetting3);
    //            compatibility1.Append(compatibilitySetting4);

    //            M.MathProperties mathProperties1 = new M.MathProperties();
    //            M.MathFont mathFont1 = new M.MathFont() { Val = "Cambria Math" };
    //            M.BreakBinary breakBinary1 = new M.BreakBinary() { Val = M.BreakBinaryOperatorValues.Before };
    //            M.BreakBinarySubtraction breakBinarySubtraction1 = new M.BreakBinarySubtraction() { Val = M.BreakBinarySubtractionValues.MinusMinus };
    //            M.SmallFraction smallFraction1 = new M.SmallFraction() { Val = M.BooleanValues.Zero };
    //            M.DisplayDefaults displayDefaults1 = new M.DisplayDefaults();
    //            M.LeftMargin leftMargin3 = new M.LeftMargin() { Val = (UInt32Value)0U };
    //            M.RightMargin rightMargin3 = new M.RightMargin() { Val = (UInt32Value)0U };
    //            M.DefaultJustification defaultJustification1 = new M.DefaultJustification() { Val = M.JustificationValues.CenterGroup };
    //            M.WrapIndent wrapIndent1 = new M.WrapIndent() { Val = (UInt32Value)1440U };
    //            M.IntegralLimitLocation integralLimitLocation1 = new M.IntegralLimitLocation() { Val = M.LimitLocationValues.SubscriptSuperscript };
    //            M.NaryLimitLocation naryLimitLocation1 = new M.NaryLimitLocation() { Val = M.LimitLocationValues.UnderOver };

    //            mathProperties1.Append(mathFont1);
    //            mathProperties1.Append(breakBinary1);
    //            mathProperties1.Append(breakBinarySubtraction1);
    //            mathProperties1.Append(smallFraction1);
    //            mathProperties1.Append(displayDefaults1);
    //            mathProperties1.Append(leftMargin3);
    //            mathProperties1.Append(rightMargin3);
    //            mathProperties1.Append(defaultJustification1);
    //            mathProperties1.Append(wrapIndent1);
    //            mathProperties1.Append(integralLimitLocation1);
    //            mathProperties1.Append(naryLimitLocation1);
    //            ThemeFontLanguages themeFontLanguages1 = new ThemeFontLanguages() { Val = "en-US" };
    //            ColorSchemeMapping colorSchemeMapping1 = new ColorSchemeMapping() { Background1 = ColorSchemeIndexValues.Light1, Text1 = ColorSchemeIndexValues.Dark1, Background2 = ColorSchemeIndexValues.Light2, Text2 = ColorSchemeIndexValues.Dark2, Accent1 = ColorSchemeIndexValues.Accent1, Accent2 = ColorSchemeIndexValues.Accent2, Accent3 = ColorSchemeIndexValues.Accent3, Accent4 = ColorSchemeIndexValues.Accent4, Accent5 = ColorSchemeIndexValues.Accent5, Accent6 = ColorSchemeIndexValues.Accent6, Hyperlink = ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = ColorSchemeIndexValues.FollowedHyperlink };

    //            ShapeDefaults shapeDefaults1 = new ShapeDefaults();
    //            Ovml.ShapeDefaults shapeDefaults2 = new Ovml.ShapeDefaults() { Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 1026 };

    //            Ovml.ShapeLayout shapeLayout1 = new Ovml.ShapeLayout() { Extension = V.ExtensionHandlingBehaviorValues.Edit };
    //            Ovml.ShapeIdMap shapeIdMap1 = new Ovml.ShapeIdMap() { Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "1" };

    //            shapeLayout1.Append(shapeIdMap1);

    //            shapeDefaults1.Append(shapeDefaults2);
    //            shapeDefaults1.Append(shapeLayout1);
    //            DecimalSymbol decimalSymbol1 = new DecimalSymbol() { Val = "." };
    //            ListSeparator listSeparator1 = new ListSeparator() { Val = "," };

    //            settings1.Append(zoom1);
    //            settings1.Append(proofState1);
    //            settings1.Append(defaultTabStop1);
    //            settings1.Append(drawingGridHorizontalSpacing1);
    //            settings1.Append(displayHorizontalDrawingGrid1);
    //            settings1.Append(characterSpacingControl1);
    //            settings1.Append(footnoteDocumentWideProperties1);
    //            settings1.Append(endnoteDocumentWideProperties1);
    //            settings1.Append(compatibility1);
    //            settings1.Append(mathProperties1);
    //            settings1.Append(themeFontLanguages1);
    //            settings1.Append(colorSchemeMapping1);
    //            settings1.Append(shapeDefaults1);
    //            settings1.Append(decimalSymbol1);
    //            settings1.Append(listSeparator1);

    //            documentSettingsPart1.Settings = settings1;
    //        }
    //    }

    //    private static object GenerateFontTablePart1Content_LockObject = new object();
    //    public static void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
    //    {
    //        lock (GenerateFontTablePart1Content_LockObject)
    //        {

    //            Fonts fonts1 = new Fonts() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14" } };
    //            fonts1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
    //            fonts1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    //            fonts1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    //            fonts1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");

    //            Font font1 = new Font() { Name = "Symbol" };
    //            Panose1Number panose1Number1 = new Panose1Number() { Val = "05050102010706020507" };
    //            FontCharSet fontCharSet1 = new FontCharSet() { Val = "02" };
    //            FontFamily fontFamily1 = new FontFamily() { Val = FontFamilyValues.Roman };
    //            Pitch pitch1 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature1 = new FontSignature() { UnicodeSignature0 = "00000000", UnicodeSignature1 = "10000000", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "80000000", CodePageSignature1 = "00000000" };

    //            font1.Append(panose1Number1);
    //            font1.Append(fontCharSet1);
    //            font1.Append(fontFamily1);
    //            font1.Append(pitch1);
    //            font1.Append(fontSignature1);

    //            Font font2 = new Font() { Name = "Times New Roman" };
    //            Panose1Number panose1Number2 = new Panose1Number() { Val = "02020603050405020304" };
    //            FontCharSet fontCharSet2 = new FontCharSet() { Val = "00" };
    //            FontFamily fontFamily2 = new FontFamily() { Val = FontFamilyValues.Roman };
    //            Pitch pitch2 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature2 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C0007841", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

    //            font2.Append(panose1Number2);
    //            font2.Append(fontCharSet2);
    //            font2.Append(fontFamily2);
    //            font2.Append(pitch2);
    //            font2.Append(fontSignature2);

    //            Font font3 = new Font() { Name = "Courier New" };
    //            Panose1Number panose1Number3 = new Panose1Number() { Val = "02070309020205020404" };
    //            FontCharSet fontCharSet3 = new FontCharSet() { Val = "00" };
    //            FontFamily fontFamily3 = new FontFamily() { Val = FontFamilyValues.Modern };
    //            Pitch pitch3 = new Pitch() { Val = FontPitchValues.Fixed };
    //            FontSignature fontSignature3 = new FontSignature() { UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C0007843", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

    //            font3.Append(panose1Number3);
    //            font3.Append(fontCharSet3);
    //            font3.Append(fontFamily3);
    //            font3.Append(pitch3);
    //            font3.Append(fontSignature3);

    //            Font font4 = new Font() { Name = "Wingdings" };
    //            Panose1Number panose1Number4 = new Panose1Number() { Val = "05000000000000000000" };
    //            FontCharSet fontCharSet4 = new FontCharSet() { Val = "02" };
    //            FontFamily fontFamily4 = new FontFamily() { Val = FontFamilyValues.Auto };
    //            Pitch pitch4 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature4 = new FontSignature() { UnicodeSignature0 = "00000000", UnicodeSignature1 = "10000000", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "80000000", CodePageSignature1 = "00000000" };

    //            font4.Append(panose1Number4);
    //            font4.Append(fontCharSet4);
    //            font4.Append(fontFamily4);
    //            font4.Append(pitch4);
    //            font4.Append(fontSignature4);

    //            Font font5 = new Font() { Name = "Calibri" };
    //            Panose1Number panose1Number5 = new Panose1Number() { Val = "020F0502020204030204" };
    //            FontCharSet fontCharSet5 = new FontCharSet() { Val = "00" };
    //            FontFamily fontFamily5 = new FontFamily() { Val = FontFamilyValues.Swiss };
    //            Pitch pitch5 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature5 = new FontSignature() { UnicodeSignature0 = "E10002FF", UnicodeSignature1 = "4000ACFF", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

    //            font5.Append(panose1Number5);
    //            font5.Append(fontCharSet5);
    //            font5.Append(fontFamily5);
    //            font5.Append(pitch5);
    //            font5.Append(fontSignature5);

    //            Font font6 = new Font() { Name = "Tahoma" };
    //            Panose1Number panose1Number6 = new Panose1Number() { Val = "020B0604030504040204" };
    //            FontCharSet fontCharSet6 = new FontCharSet() { Val = "00" };
    //            FontFamily fontFamily6 = new FontFamily() { Val = FontFamilyValues.Swiss };
    //            Pitch pitch6 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature6 = new FontSignature() { UnicodeSignature0 = "E1002EFF", UnicodeSignature1 = "C000605B", UnicodeSignature2 = "00000029", UnicodeSignature3 = "00000000", CodePageSignature0 = "000101FF", CodePageSignature1 = "00000000" };

    //            font6.Append(panose1Number6);
    //            font6.Append(fontCharSet6);
    //            font6.Append(fontFamily6);
    //            font6.Append(pitch6);
    //            font6.Append(fontSignature6);

    //            Font font7 = new Font() { Name = "Cambria" };
    //            Panose1Number panose1Number7 = new Panose1Number() { Val = "02040503050406030204" };
    //            FontCharSet fontCharSet7 = new FontCharSet() { Val = "00" };
    //            FontFamily fontFamily7 = new FontFamily() { Val = FontFamilyValues.Roman };
    //            Pitch pitch7 = new Pitch() { Val = FontPitchValues.Variable };
    //            FontSignature fontSignature7 = new FontSignature() { UnicodeSignature0 = "E00002FF", UnicodeSignature1 = "400004FF", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

    //            font7.Append(panose1Number7);
    //            font7.Append(fontCharSet7);
    //            font7.Append(fontFamily7);
    //            font7.Append(pitch7);
    //            font7.Append(fontSignature7);

    //            fonts1.Append(font1);
    //            fonts1.Append(font2);
    //            fonts1.Append(font3);
    //            fonts1.Append(font4);
    //            fonts1.Append(font5);
    //            fonts1.Append(font6);
    //            fonts1.Append(font7);

    //            fontTablePart1.Fonts = fonts1;
    //        }
    //    }

    }
}
