using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class Bms_DFHMSD : Bms_BASE
  {
    private List<string> _ctrl {
      get;
      set;
    }

    public DFH_COLOR DFH_COLOR {
      get;
      private set;
    }
    public DFH_CURSLOC DFH_CURSLOC {
      get;
      private set;
    }
    public DFHMSD_DSECT DFHMSD_DSECT {
      get;
      private set;
    }
    public DFH_EXTATT DFH_EXTATT {
      get;
      private set;
    }
    public DFHMSD_FOLD DFHMSD_FOLD {
      get;
      private set;
    }
    public DFH_HILIGHT DFH_HILIGHT {
      get;
      private set;
    }
    public DFHMSD_LANG DFHMSD_LANG {
      get;
      private set;
    }
    public DFHMSD_MODE DFHMSD_MODE {
      get;
      private set;
    }
    public DFH_PS DFH_PS {
      get;
      private set;
    }
    public DFHMSD_OUTLINE DFHMSD_OUTLINE {
      get;
      private set;
    }
    public DFH_SOSI DFH_SOSI {
      get;
      private set;
    }
    public DFHMSD_STORAGE DFHMSD_STORAGE {
      get;
      private set;
    }
    public DFH_TERM DFH_TERM {
      get;
      private set;
    }
    public DFH_TIOAPFX DFH_TIOAPFX {
      get;
      private set;
    }
    public DFH_TRANSP DFH_TRANSP {
      get;
      private set;
    }
    public DFHMSD_TRIGRAPH DFHMSD_TRIGRAPH {
      get;
      private set;
    }
    public DFHMSD_TYPE DFHMSD_TYPE {
      get;
      private set;
    }
    public DFH_VALIDN DFH_VALIDN {
      get;
      private set;
    }

    public Bms_DFHMSD(BmsStatement bmsStatement)
      : base(bmsStatement)
    {
      _ctrl = new List<string>();

      Populate();
    }

    private void Populate()
    {
      try
      {
        foreach (var p in base.BmsStatement.Parameters)
        {
          switch (p.Key)
          {
            case "COLOR":
              break;

            case "CTRL":
              this._ctrl = new List<string>();
              this._ctrl.Add(p.Value.Trim());
              break;

            case "CURSLOC":
              break;

            case "DSECT":


              break;


            case "EXTATT":
              break;

            case "FOLD":
              break;

            case "HILIGHT":
              break;

            case "LANG":
              switch (p.Value.Trim().ToUpper())
              {
                case "COBOL":
                  this.DFHMSD_LANG = DFHMSD_LANG.COBOL;
                  break;

                case "COBOL2":
                  this.DFHMSD_LANG = DFHMSD_LANG.COBOL2;
                  break;

                case "ASM":
                  this.DFHMSD_LANG = DFHMSD_LANG.ASM;
                  break;

                case "PLI":
                  this.DFHMSD_LANG = DFHMSD_LANG.PLI;
                  break;

                case "C":
                  this.DFHMSD_LANG = DFHMSD_LANG.C;
                  break;

                default:
                  // add error and continue
                  break;
              }
              break;

            case "MODE":
              switch (p.Value.Trim().ToUpper())
              {
                case "OUT":
                  this.DFHMSD_MODE = DFHMSD_MODE.OUT;
                  break;

                case "IN":
                  this.DFHMSD_MODE = DFHMSD_MODE.IN;
                  break;

                case "INOUT":
                  this.DFHMSD_MODE = DFHMSD_MODE.INOUT;
                  break;

                case "LOWER":
                  this.DFHMSD_MODE = DFHMSD_MODE.LOWER;
                  break;

                default:
                  // add error and continue
                  break;
              }
              break;

            case "OUTLINE":
              break;

            case "PS":
              break;

            case "SOSI":
              break;

            case "STORAGE":
              switch (p.Value.Trim().ToUpper())
              {
                case "AUTO":
                  this.DFHMSD_STORAGE = DFHMSD_STORAGE.AUTO;
                  break;

                default:
                  // record error and continue;
                  break;
              }
              break;

            case "TERM":
              switch (p.Value.Trim().ToUpper())
              {
                case "ALL":
                  this.DFH_TERM = DFH_TERM.ALL;
                  break;

                case "3270":
                  this.DFH_TERM = DFH_TERM.TYPE_3270;
                  break;

                case "3270-1":
                  this.DFH_TERM = DFH_TERM.TYPE_3270_1;
                  break;

                case "3270-2":
                  this.DFH_TERM = DFH_TERM.TYPE_3270_2;
                  break;

                default:
                  // add error and continue
                  break;
              }
              break;

            case "TIOAPFX":
              switch (p.Value.Trim().ToUpper())
              {
                case "NO":
                  this.DFH_TIOAPFX = DFH_TIOAPFX.NO;
                  break;

                case "YES":
                  this.DFH_TIOAPFX = DFH_TIOAPFX.YES;
                  break;

                default:
                  // add error and continue
                  break;
              }
              break;

            case "TRANSP":
              break;

            case "TRIGRAPH":
              break;

            case "TYPE":
              switch (p.Value.Trim().ToUpper())
              {
                case "&SYSPARM":
                  this.DFHMSD_TYPE = DFHMSD_TYPE.SYSPARM;
                  break;

                case "DSECT":
                  this.DFHMSD_TYPE = DFHMSD_TYPE.DSECT;
                  break;

                case "MAP":
                  this.DFHMSD_TYPE = DFHMSD_TYPE.MAP;
                  break;

                case "FINAL":
                  this.DFHMSD_TYPE = DFHMSD_TYPE.FINAL;
                  break;

                default:
                  // record error
                  break;
              }
              break;

            case "VALIDN":
              break;

            default:
              // write unrecognized parm error
              break;
          }
        }

        foreach (var p in base.BmsStatement.Parentheticals)
        {
          switch (p.Name)
          {
            case "ATTRB":
              break;

            case "CTRL":
              break;

            case "DSATTS":
              break;

            case "JUSTIFY":
              break;

            case "MAPATTS":
              break;

            case "OUTLINE":
              break;

            case "POS":
              break;

            case "SIZE":
              break;

            default:
              // write unrecognized parm error
              break;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the Bms_DFHMSD object from the macro value '" + base.BmsStatement.BmsStatementText + "'.", ex);
      }
    }
  }
}
