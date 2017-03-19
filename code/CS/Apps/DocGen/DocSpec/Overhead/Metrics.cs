using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class Metrics
    {
        private PointF _offset;
        public PointF Offset 
        {
            get { return Get_Offset(); } 
            set { _offset = value; }
        }
        public SizeF MaxSize { get; set; }
        public SizeF TotalSize { get; set; }
        public SizeSpec SizeSpec { get; set; }
        public float WidthSpecUsed { get; set; }
        public float HeightSpecUsed { get; set; }
        public float MaxChildHeightSpec { get; set; }

        public PointF CurrentOffset { get { return Get_CurrentOffset(); }}
        public PointF CurrentHorizontalOffset { get { return Get_CurrentHorizontalOffset(); }}
        public PointF CurrentVerticalOffset { get { return Get_CurrentVerticalOffset(); }}
        public RectangleF UsedRectangle { get { return Get_UsedRectangle(); }}
        public Doc Doc { get; set; }

        public Metrics(Doc doc)
        {
            this.Doc = doc;
            this.Initialize();
        }

        public void Initialize()
        {
            this.Offset = new PointF(0, 0);
            this.MaxSize = new SizeF(0, 0);
            this.TotalSize = new SizeF(0, 0);
            this.SizeSpec = new SizeSpec();
            this.WidthSpecUsed = 0F;
            this.HeightSpecUsed = 0F;
            this.MaxChildHeightSpec = 0F;
        }

        public RectangleF GetRectangleF()
        {
            return new RectangleF(this.Offset, new SizeF(this.TotalSize.Width, this.TotalSize.Height));
        }

        private PointF Get_CurrentOffset()
        {
            return this.Offset.ShiftDown(this.HeightSpecUsed).ShiftRight(this.WidthSpecUsed);
        }

        private PointF Get_CurrentVerticalOffset()
        {
            return this.Offset.ShiftDown(this.HeightSpecUsed);
        }

        private PointF Get_CurrentHorizontalOffset()
        {
            return this.Offset.ShiftRight(this.WidthSpecUsed);
        }

        private RectangleF Get_UsedRectangle()
        {
            return new RectangleF(this.Offset, this.TotalSize);
        }

        private PointF Get_Offset()
        {
            if (this.Doc == null)
                return _offset;

            return new PointF(_offset.X + this.Doc.ShiftOffset.X, _offset.Y + this.Doc.ShiftOffset.Y); 
        }

    }
}
