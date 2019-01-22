using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

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
    public class ImageEngine
    {
        private Ds.DocPackage DocPackage; 
        private float Scale = 1.0F;
        private bool InDiagnosticsMode = false;
        private bool ShowScale = false;
        private bool ShowProperties = false;
        private int TextContrast = 3;
        private float WidthFactor;
        private float SpaceWidthFactor;
        private float LineFactor;

        public Ds.PageSet GenerateImageFromPackage(Ds.DocPackage package, bool loadFromXml)
        {
            this.DocPackage = package;
            this.InDiagnosticsMode = this.DocPackage.DocControl.DebugControl.InDiagnosticsMode;
            this.ShowScale = this.DocPackage.DocControl.DebugControl.ShowScale;
            this.ShowProperties = this.DocPackage.DocControl.DebugControl.IncludeProperties;
            this.TextContrast = this.DocPackage.DocControl.PrintControl.TextContrast;
            this.WidthFactor = this.DocPackage.DocControl.PrintControl.WidthFactor;
            this.SpaceWidthFactor = this.DocPackage.DocControl.PrintControl.SpaceWidthFactor;
            this.LineFactor = this.DocPackage.DocControl.PrintControl.LineFactor;

            this.Scale = this.DocPackage.DocControl.PrintControl.Scale;

            if (loadFromXml)
                package.DocOut = new Ds.Doc(package, null, null);

            package.RefreshContent();

            package.DocOut.PrepareForImageGeneration();

            package.DocOut.PageSet = new Ds.PageSet(this.Scale);
            package.DocOut.PageSet.Scale = this.Scale;
            Ds.Page currentPage  = null;
            int newPageNumber = 0;
            Image img = null;
            Graphics gr = null;
            string fontFace = "Calibri";
            System.Drawing.Font font13b = new System.Drawing.Font(fontFace, 13, FontStyle.Regular | FontStyle.Bold);

            foreach (Ds.Section section in package.DocOut.SectionSet.Values)
            {
                newPageNumber = package.DocOut.PageSet.AddSection(section);
                currentPage = package.DocOut.PageSet[newPageNumber];
                img = currentPage.Image;

                gr = Graphics.FromImage(img);
                gr.ScaleTransform(this.Scale, this.Scale);
                gr.TextContrast = this.TextContrast;
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                Ds.PageSize pgSz = (Ds.PageSize)section.Properties.ChildElements.OfType<Ds.PageSize>().FirstOrDefault();
                if (pgSz == null)
                    throw new Exception("No PageSize property found in section properties for section '" + section.Name + "'.");

                Ds.PageMargin pgMg = (Ds.PageMargin)section.Properties.ChildElements.OfType<Ds.PageMargin>().FirstOrDefault();
                if (pgMg == null)
                    throw new Exception("No PageMargin property found in section properties for section '" + section.Name + "'.");

                Size pageSize = pgSz.ToSizeFromTwips();
                Ds.PageLayout pageLayout = pgMg.PageLayoutFromTwips(pageSize);
                section.RawMetrics.Offset = new Point(pageLayout.Main.Left, pageLayout.Main.Top);
                section.RawMetrics.MaxSize = new Size(pageLayout.Main.Width, Ds.Constants.HeightMaxValue);
                section.RawMetrics.TotalSize = new Size(0, 0);

                this.MapElement(gr, section); 
                this.DrawElement(gr, section);
                package.DocOut.CreateMaps(this.ShowProperties);

                //img.Save(@"C:\__dev\ResumeImage\trunk\ResumeImageTest\ResumeImages\img.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            if (gr != null)
            {
                gr.ResetTransform();
                gr.Dispose();
            }

            font13b.Dispose();

            return package.DocOut.PageSet;
        }

        public void DrawDocumentElement(PictureBox pb, Ds.DocumentElement e)
        {
            e.Doc.DrawingMode = Ds.DrawingMode.DocumentPortion;
            e.Doc.ShiftOffset = new PointF(e.RawMetrics.Offset.X * -1, e.RawMetrics.Offset.Y * -1); 
            pb.Image = new Bitmap(pb.Width, pb.Height);
            Graphics gr = Graphics.FromImage(pb.Image);
            gr.ScaleTransform(this.Scale, this.Scale);
            gr.TextContrast = this.TextContrast;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gr.TextRenderingHint = TextRenderingHint.AntiAlias;

            DrawElement(gr, e);

            e.Doc.DrawingMode = Ds.DrawingMode.FullDocument;
            e.Doc.ShiftOffset = new PointF(0F, 0F); 
        }

        private void MapElement(Graphics gr, Ds.DocumentElement e)
        {
            e.SetSizeSpec();

            foreach (Ds.DocumentElement de in e.ChildElements)
            {
                de.MapElement(gr);
                e.MergeMetrics(de.RawMetrics);
            }
        }

        private void DrawElement(Graphics gr, Ds.DocumentElement e)
        {
            //e.RawMetrics.SizeSpec = e.GetSizeSpec();

            e.TraceMetrics("IENG_001");

            foreach (Ds.DocumentElement de in e.ChildElements)
            {
                de.DrawElement(gr);
                e.MergeMetrics(de.RawMetrics);
                e.TraceMetrics("IENG_002"); 
            }
        }


    }
}
