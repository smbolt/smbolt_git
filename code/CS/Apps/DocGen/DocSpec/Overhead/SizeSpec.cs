using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class SizeSpec
    {
        public SpecUnit Width { get; set; }
        public SpecUnit Height { get; set; }
        public SizeF SizeF { get { return Get_SizeF(); } }
        public Size Size { get { return Get_Size(); } }
        public bool CanGrowW { get { return Get_CanGrowW(); } }
        public bool CanGrowH { get { return Get_CanGrowH(); } }
        public float MinWidth { get { return Get_MinWidth(); } }
        public float MaxWidth { get { return Get_MaxWidth(); } }
        public float MinHeight { get { return Get_MinHeight(); } }
        public float MaxHeight { get { return Get_MaxHeight(); } }

        public SizeSpec()
        {
            this.Width = new SpecUnit();
            this.Width.Dimension = SpecUnitDimension.Width;
            this.Height = new SpecUnit();
            this.Height.Dimension = SpecUnitDimension.Height;
        }

        public SizeSpec(Size sz, CommonUnits units, SizeControl sizeControl)
        {
            this.Width = new SpecUnit(SpecUnitDimension.Width, (float)sz.Width, units, sizeControl);
            this.Height = new SpecUnit(SpecUnitDimension.Height, (float)sz.Height, units, sizeControl);
        }

        public SizeSpec(SizeF sz, CommonUnits units, SizeControl sizeControl)
        {
            this.Width = new SpecUnit(SpecUnitDimension.Width, sz.Width, units, sizeControl);
            this.Height = new SpecUnit(SpecUnitDimension.Height, sz.Height, units, sizeControl);
        }

        public SizeSpec(SpecUnit widthSpec, SpecUnit heightSpec)
        {
            this.Width = widthSpec;
            this.Height = heightSpec;

            Size sz = this.Size;
        }

        public SizeF Get_SizeF()
        {
            float width = 0F;
            float height = 0F;

            if (this.Width.IsSpecified)
                width = this.Width.Val;

            if (this.Height.IsSpecified)
                height = this.Height.Val;

            return new SizeF(width, height);
        }

        public Size Get_Size()
        {
            SizeF sizeF = this.Get_SizeF();
            int width = Convert.ToInt32(sizeF.Width);
            int height = Convert.ToInt32(sizeF.Height);

            return new Size(width, height);
        }

        public SizeSpec Clone()
        {
            SizeSpec clone = new SizeSpec();

            clone.Width = new SpecUnit();
            clone.Width.IsSpecified = this.Width.IsSpecified;
            clone.Width.Val = this.Width.Val;
            clone.Width.Dimension = SpecUnitDimension.Width;
            clone.Width.Units = this.Width.Units;
            clone.Width.SizeControl = this.Width.SizeControl;

            clone.Height = new SpecUnit();
            clone.Height.IsSpecified = this.Height.IsSpecified;
            clone.Height.Val = this.Height.Val;
            clone.Height.Dimension = SpecUnitDimension.Height;
            clone.Height.Units = this.Height.Units;
            clone.Height.SizeControl = this.Height.SizeControl;
            
            return clone;
        }

        public bool Get_CanGrowW()
        {


            return true;
        }

        private bool Get_CanGrowH()
        {


            return true;
        }

        private float Get_MinWidth()
        {

            return 0F;
        }

        private float Get_MaxWidth()
        {

            return 0F;
        }

        private float Get_MinHeight()
        {

            return 0F;
        }

        private float Get_MaxHeight()
        {

            return 0F;
        }



    }
}
