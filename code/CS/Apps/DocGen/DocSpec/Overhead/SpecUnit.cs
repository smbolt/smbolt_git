using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class SpecUnit
  {
    public bool IsSpecified {
      get;
      set;
    }
    public float Val {
      get;
      set;
    }
    public CommonUnits Units {
      get;
      set;
    }
    public SizeControl SizeControl {
      get;
      set;
    }
    public SpecUnitDimension Dimension {
      get;
      set;
    }


    public SpecUnit()
    {
      this.IsSpecified = false;
      this.Val = 0F;
      this.Units = CommonUnits.Dxa;
      this.SizeControl = SizeControl.Auto;
      this.Dimension = SpecUnitDimension.NotSet;
    }

    public SpecUnit(SpecUnitDimension dimension)
    {
      this.IsSpecified = false;
      this.Val = 0F;
      this.Units = CommonUnits.Inches;
      this.Dimension = dimension;
    }

    public SpecUnit(SpecUnitDimension dimension, float val, CommonUnits units, SizeControl sizeControl)
    {
      this.IsSpecified = true;
      this.Val = val;
      this.Dimension = dimension;
      this.Units = units;
      this.SizeControl = sizeControl;
    }
  }
}
