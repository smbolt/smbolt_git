using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public enum BmsStatementType
  {
    Uncompiled,
    Unidentified,
    Comment,
    PRINT,
    BASE,
    DFHMSD,
    DFHMDI,
    DFHMDF,
    END
  }

  public enum BmsMapErrorLevel
  {
    NoError = 0,
    Warning = 4,
    Error = 8,
    Severe = 16
  }

  public enum FieldColor
  {
    DEFAULT = 0,
    NEUTRAL,
    BLUE,
    RED,
    PINK,
    GREEN,
    TURQUOISE,
    YELLOW
  }

  // COMMON DFH ENUMS

  public enum DFH_COLOR
  {
    DEFAULT = 0,
    NEUTRAL,
    BLUE,
    RED,
    PINK,
    GREEN,
    TURQUOISE,
    YELLOW
  }

  public enum DFH_CURSLOC
  {
    NO = 0,
    YES,
    OFF
  }

  public enum DFH_EXTATT
  {
    NO = 0,
    YES,
    MAPONLY
  }

  public enum DFH_HILIGHT
  {
    OFF = 0,
    BLINK,
    REVERSE,
    UNDERLINE
  }

  public enum DFH_PS
  {
    BASE = 0,
    PsId
  }

  public enum DFH_SOSI
  {
    NO = 0,
    YES
  }

  public enum DFH_TERM
  {
    ALL = 0,
    TYPE_3270,
    TYPE_3270_1,
    TYPE_3270_2
  }

  public enum DFH_TIOAPFX
  {
    NO = 0,
    YES
  }

  public enum DFH_TRANSP
  {
    YES = 0,
    NO
  }

  public enum DFH_VALIDN
  {
    NONE = 0,
    MUSTFILL,
    MUSTENTER,
    TRIGGER,
    USEREXIT
  }

  // DFHMSD (MAP SET) ENUMS

  public enum DFHMSD_DSECT
  {
    ADS = 0,
    ADSL
  }

  public enum DFHMSD_FOLD
  {
    NONE = 0,
    LOWER,
    UPPER
  }

  public enum DFHMSD_LANG
  {
    ASM = 0,
    COBOL,
    COBOL2,
    PLI,
    C
  }

  public enum DFHMSD_MODE
  {
    OUT = 0,
    IN,
    INOUT,
    LOWER
  }

  public enum DFHMSD_OUTLINE
  {
    NONE = 0,
    BOX,
    LEFT,
    RIGHT,
    OVER,
    UNDER
  }

  public enum DFHMSD_STORAGE
  {
    NotSet = 0,
    AUTO,
  }

  public enum DFHMSD_TRIGRAPH
  {
    NO = 0,
    YES,
  }

  public enum DFHMSD_TYPE
  {
    SYSPARM = 0,
    DSECT,
    MAP,
    FINAL
  }

  // DFHMDI (MAP) ENUMS

  public enum DFHMDI_DATA
  {
    FIELD = 0,
    BLOCK
  }

  public enum DFHMDI_COLUMN
  {
    NUMBER = 0,
    NEXT,
    SAME
  }

  public enum DFHMDI_HEADER
  {
    NO = 0,
    YES
  }

  public enum DFHMDI_JUSTIFY
  {
    LEFT = 0,
    BOTTOM,
    RIGHT,
    FIRST,
    LAST
  }

  public enum DFHMDI_LINE
  {
    NUMBER = 0,
    NEXT,
    SAME
  }

  public enum DFHMDI_OBFMT
  {
    NotSet,
    YES,
    NO
  }

  public enum DFHMDI_TRAILER
  {
    NO = 0,
    YES
  }

  // DFHMDF (FIELD) ENUMS

  public enum DFHMDF_CASE
  {
    NotSet,
    MIXED
  }

  public enum DFHMDF_JUSTIFY
  {
    LEFT_NONE = 0,
    LEFT_BLANK,
    RIGHT_ZERO,
    RIGHT_NONE,
  }

  public enum DFHMDF_HFLEX
  {
    NONE=0,
    STRETCH,
    FLOATRIGHT
  }

  public enum DFHMDF_VFLEX
  {
    NONE=0,
    OCCURS
  }

  public enum HFlex
  {
    None,
    Stretch,
    FloatRight
  }

  public enum VFlex
  {
    None,
    Occurs
  }
}
