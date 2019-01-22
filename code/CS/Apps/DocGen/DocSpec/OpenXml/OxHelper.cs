using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DocumentFormat.OpenXml;
//using Wd = DocumentFormat.OpenXml.Wordprocessing;
//using Rs = Org.BuildMyResume.Domain.Models.Resume;
using Org.DocGen;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public static class OxHelper
  {
    //private static object BuildParagraph_LockObject = new object();
    //public static Wd.Paragraph BuildParagraph(Doc doc, string styleId, string text)
    //{
    //    lock (BuildParagraph_LockObject)
    //    {
    //        Wd.Paragraph p = new Wd.Paragraph();
    //        p.AddTag(doc, String.Empty);
    //        Wd.Run run = new Wd.Run();
    //        run.AddTag(doc, String.Empty);
    //        Wd.Text t = new Wd.Text(text);
    //        Wd.ParagraphProperties pp = new Wd.ParagraphProperties();
    //        pp.SpacingBetweenLines = new Wd.SpacingBetweenLines() { After = "0", Line = "240", LineRule = Wd.LineSpacingRuleValues.Auto };
    //        pp.Append(new Wd.Justification() { Val = Wd.JustificationValues.Center });
    //        pp.ParagraphStyleId = new Wd.ParagraphStyleId() { Val = styleId };
    //        p.Append(pp);
    //        run.Append(t);
    //        p.Append(run);
    //        return p;
    //    }
    //}
  }
}
