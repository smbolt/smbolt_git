using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Terminal.BMS;
using Org.GS;

namespace Org.Terminal.Screen
{
  [XMap(XType = XType.Element)]
  public class FieldSpec
  {
    private string _name;
    [XMap] public string Name
    {
      get {
        return Get_Name();
      }
      set {
        _name = value;
      }
    }

    private int _origLine;
    [XMap] public int OrigLine
    {
      get {
        return Get_OrigLine();
      }
      set {
        _origLine = value;
      }
    }

    public int CurrLine {
      get;
      set;
    }

    private int _origCol;
    [XMap] public int OrigCol
    {
      get {
        return Get_OrigCol();
      }
      set {
        _origCol = value;
      }
    }

    public int CurrCol {
      get;
      set;
    }

    private int _origLth;
    [XMap] public int OrigLth
    {
      get {
        return Get_OrigLth();
      }
      set {
        _origLth = value;
      }
    }

    public int CurrLth {
      get;
      set;
    }

    private int? _maxSize;
    public int? MaxSize
    {
      get {
        return _maxSize;
      }
      set {
        _maxSize = value;
      }
    }

    private string _init;
    [XMap] public string Init
    {
      get {
        return Get_Init();
      }
      set
      {
        _init = value;
        _origValue = value;
        this.CurrValue = value;
      }
    }

    private bool _useInit;
    [XMap] public bool UseInit
    {
      get {
        return Get_UseInit();
      }
      set
      {
        _useInit = value;
      }
    }

    private string _fill;
    [XMap] public string Fill
    {
      get {
        return Get_Fill();
      }
      set
      {
        _fill = value;
        if (_fill.Length > 0 && _origLth > 0)
        {
          _origValue = new String(_fill[0], _origLth);
          this.CurrValue = value;
        }
      }
    }

    private bool _useFill;
    [XMap] public bool UseFill
    {
      get {
        return Get_UseFill();
      }
      set
      {
        _useFill = value;
      }
    }

    private string _origValue;
    public string OrigValue
    {
      get {
        return _origValue;
      }
    }

    private bool _isProtected;
    public bool IsProtected
    {
      get {
        return Get_IsProtected();
      }
    }

    public string CurrValue {
      get;
      set;
    }

    [XMap]
    public string Attrs {
      get;
      set;
    }
    /*  The Attrs property contains a set of positional single-character values
     *  Position 0 (PROT,UNPROT)
     *     "U" - Unprotected (default)
     *     "P" - Protected
     *  Position 1 (NUM)
     *     "A" - Alphanumeric (default)
     *     "N" - Numeric
     *  Position 2 (Color)
     *     "D" - Default (default)
     *     "N" - Normal
     *     "W" - White
     *     "B" - Blue
     *     "R" - Red
     *     "G" - Green
     *     "Y" - Yellow
     *     "P" - Pink
     *     "T" - Turquois
     */

    public bool AttrbNum {
      get {
        return Get_AttrbNum();
      }
    }
    public bool AttrbBright {
      get {
        return Get_AttrbBright();
      }
    }
    public bool AttrbDark {
      get {
        return Get_AttrbDark();
      }
    }
    public bool AttrbDet {
      get {
        return Get_AttrbDet();
      }
    }
    public bool AttrbIC {
      get {
        return Get_AttrbIC();
      }
    }
    public bool AttrbFSET {
      get {
        return Get_AttrbFSET();
      }
    }
    public FieldColor Color {
      get {
        return Get_Color();
      }
    }

    private HFlex _hFlex;
    public HFlex HFlex
    {
      get {
        return Get_HFlex();
      }
      set {
        _hFlex = value;
      }
    }

    private VFlex _vFlex;
    public VFlex VFlex
    {
      get {
        return Get_VFlex();
      }
      set {
        _vFlex = value;
      }
    }

    public bool TabStop {
      get {
        return Get_TabStop();
      }
    }

    public FieldSpecSet FieldSpecSet {
      get;
      set;
    }

    private Bms_DFHMDF _bms_DFHMDF;
    public Bms_DFHMDF Bms_DFHMDF
    {
      get
      {
        return _bms_DFHMDF;
      }
      set
      {
        _bms_DFHMDF = value;
        Init_DFHMDF();
      }
    }

    public FieldSpec()
    {
      this.Name = String.Empty;
      this.OrigLine = 0;
      this.CurrLine = 0;
      this.OrigCol = 0;
      this.CurrCol = 0;
      this.OrigLth = 0;
      this.CurrLth = 0;
      this.MaxSize = null;
      this.Init = String.Empty;
      this.Fill = String.Empty;
      this.Attrs = String.Empty;
      this.HFlex = HFlex.None;
      this.VFlex = VFlex.None;
    }

    private void Init_DFHMDF()
    {
      this.CurrLine = this.OrigLine;
      this.CurrCol = this.OrigCol;
      this.CurrLth = this.OrigLth;
    }

    private string Get_Name()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.Name;
      else
        return _name;
    }

    private int Get_OrigLine()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.PosLine;
      else
        return _origLine;
    }

    private int Get_OrigCol()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.PosColumn;
      else
        return _origCol;
    }

    private int Get_OrigLth()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.Length;
      else
        return _origLth;
    }

    private string Get_Init()
    {
      if (this.Bms_DFHMDF != null)
      {
        string initValue = this.Bms_DFHMDF.UseInit ? this.Bms_DFHMDF.Init : String.Empty;
        _origValue = initValue;
        this.CurrValue = initValue;
        return initValue;
      }
      else
      {
        return _init;
      }
    }

    private bool Get_UseInit()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.UseInit;
      else
        return _useInit;
    }

    private string Get_Fill()
    {
      if (this.Bms_DFHMDF != null)
      {
        string fillValue = this.Bms_DFHMDF.UseFill ? this.Bms_DFHMDF.Fill : String.Empty;
        if (fillValue.Length > 0 && _origLth > 0)
        {
          _origValue = fillValue;
          this.CurrValue = fillValue;
        }
        _fill = fillValue;
        return fillValue;
      }
      else
      {
        return _fill;
      }
    }

    private bool Get_UseFill()
    {
      if (this.Bms_DFHMDF != null)
        return this.Bms_DFHMDF.UseFill;
      else
        return _useFill;
    }

    public HFlex Get_HFlex()
    {
      if (this.Bms_DFHMDF != null)
      {
        switch (this.Bms_DFHMDF.DFHMDF_HFLEX)
        {
          case DFHMDF_HFLEX.STRETCH:
            return HFlex.Stretch;
          case DFHMDF_HFLEX.FLOATRIGHT:
            return HFlex.FloatRight;
          default:
            return HFlex.None;
        }
      }
      else
      {
        return _hFlex;
      }
    }

    public VFlex Get_VFlex()
    {
      if (this.Bms_DFHMDF != null)
      {
        switch (this.Bms_DFHMDF.DFHMDF_VFLEX)
        {
          case DFHMDF_VFLEX.OCCURS:
            return VFlex.Occurs;
          default:
            return VFlex.None;
        }
      }
      else
      {
        return _vFlex;
      }
    }

    private bool Get_IsProtected()
    {
      if (this.Bms_DFHMDF != null)
      {
        return this.Bms_DFHMDF.AttrbProt;
      }
      else
      {
        return _isProtected;
      }

      return false;
    }

    private bool Get_AttrbNum()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private bool Get_AttrbBright()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private bool Get_AttrbDark()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private bool Get_AttrbDet()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private bool Get_AttrbIC()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private bool Get_AttrbFSET()
    {
      if (this.Bms_DFHMDF != null)
      {

      }
      else
      {

      }

      return false;
    }

    private FieldColor Get_Color()
    {
      if (this.Bms_DFHMDF != null)
      {
        switch (this.Bms_DFHMDF.DFH_COLOR)
        {
          case DFH_COLOR.DEFAULT:
            return FieldColor.DEFAULT;
          case DFH_COLOR.NEUTRAL:
            return FieldColor.NEUTRAL;
          case DFH_COLOR.BLUE:
            return FieldColor.BLUE;
          case DFH_COLOR.GREEN:
            return FieldColor.GREEN;
          case DFH_COLOR.RED:
            return FieldColor.RED;
          case DFH_COLOR.PINK:
            return FieldColor.PINK;
          case DFH_COLOR.YELLOW:
            return FieldColor.YELLOW;
          case DFH_COLOR.TURQUOISE:
            return FieldColor.TURQUOISE;
          default:
            return FieldColor.DEFAULT;
        }
      }
      else
      {
        if (this.Attrs.Length < 3)
          return FieldColor.DEFAULT;

        switch (this.Attrs.ToUpper()[2])
        {
          case 'D':
            return FieldColor.DEFAULT;
          case 'N':
            return FieldColor.NEUTRAL;
          case 'B':
            return FieldColor.BLUE;
          case 'G':
            return FieldColor.GREEN;
          case 'R':
            return FieldColor.RED;
          case 'P':
            return FieldColor.PINK;
          case 'Y':
            return FieldColor.YELLOW;
          case 'T':
            return FieldColor.TURQUOISE;
          default:
            return FieldColor.DEFAULT;
        }
      }
    }

    private bool Get_TabStop()
    {
      if (this.Bms_DFHMDF != null)
      {
        return !this.Bms_DFHMDF.AttrbProt;
      }
      else
      {
        return true;
      }
    }

    public override string ToString()
    {
      string returnValue = this.Name + " (" + this.OrigLine.ToString() + "," + this.OrigCol.ToString() + ")";

      if (this.UseInit)
        returnValue += " INIT=" + this.Init.PadToLength(12).Trim();

      if (this.UseFill)
        returnValue += " FILL=" + this.Fill;

      return returnValue;
    }

    public FieldSpec CloneForVFLEX(int lineNumber)
    {
      var clone = new FieldSpec();
      clone.FieldSpecSet = this.FieldSpecSet;
      clone.Bms_DFHMDF = this.Bms_DFHMDF.CloneForVFLEX(lineNumber);
      return clone;
    }

  }
}
