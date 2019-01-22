using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class Bms_DFHMDI : Bms_BASE
  {
    public string Name { get; set; }

    public DFHMDI_LINE DFHMDI_LINE { get; private set; }
    public int Line { get; set; }

    public DFHMDI_COLUMN DFHMDI_COLUMN { get; private set; }
    public int Column { get; set; }

    public int SizeWidth { get; private set; }
    public int SizeHeight { get; private set; }

    private List<string> _ctrl { get; set; } 

    public DFH_COLOR DFH_COLOR { get; private set; }
    public DFH_CURSLOC DFH_CURSLOC { get; private set; }
    public DFHMDI_DATA DFHMDI_DATA { get; private set; }
    public DFH_EXTATT DFH_EXTATT { get; private set; }
    public DFHMDI_HEADER DFHMDI_HEADER { get; private set; }
    public DFH_HILIGHT DFH_HILIGHT { get; private set; }
    public DFHMDI_JUSTIFY DFHMDI_JUSTIFY { get; private set; }
    public DFHMDI_OBFMT DFHMDI_OBFMT { get; private set; }
    public DFH_PS DFH_PS { get; private set; }
    public DFH_TERM DFH_TERM { get; private set; }
    public DFH_TIOAPFX DFH_TIOAPFX { get; private set; }
    public DFHMDI_TRAILER DFHMDI_TRAILER { get; private set; }
    public DFH_SOSI DFH_SOSI { get; private set; }
    public DFH_TRANSP DFH_TRANSP { get; private set; }
    public DFH_VALIDN DFH_VALIDN { get; private set; }

    public Bms_DFHMDI(BmsStatement bmsStatement)
      : base(bmsStatement)
    {
      this.Name = String.Empty;
      this.Line = 0;
      this.Column = 0;
      this.SizeWidth = 0;
      this.SizeHeight = 0;

      this._ctrl = new List<string>();

      Populate();
    }

    private void Populate()
    {
      try
      {
        string[] remainingTokens = base.BmsStatement.RemainingText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (remainingTokens.Length == 2)
        {
          this.Name = remainingTokens[0].ToUpper(); 
        }
        else
        {
          this.Name = "MAP1";
          // only name and macro should exist, if not there's an error 
          // add error
          // let parameter and parentheticals go ahead and compile
          // can report on additional errors
        }

        foreach (var p in base.BmsStatement.Parameters)
        {
          switch (p.Key)
          {
            case "COLOR":
              break;

            case "COLUMN":              
              string colValue = p.Value.Trim().ToUpper();
              switch (colValue)
              {
                case "NEXT":
                  this.DFHMDI_COLUMN = DFHMDI_COLUMN.NEXT;
                  break;

                case "SAME":
                  this.DFHMDI_COLUMN = DFHMDI_COLUMN.SAME;
                  break;

                default:
                  if (p.Value.Trim().IsInteger())
                  {
                    // check for valid range of numbers
                    this.DFHMDI_COLUMN = DFHMDI_COLUMN.NUMBER;
                    this.Column = p.Value.Trim().ToInt32();
                  }
                  else
                  {
                    // add error and continue
                  }
                  break;
              }
              break;

            case "CURSLOC":
              break;

            case "DATA":
              break;

            case "DSECT":
              break;

            case "EXTATT":
              break;

            case "HEADER":
              break;

            case "HILIGHT":
              break;

            case "JUSTIFY":
              break;

            case "LINE":
              string lineValue = p.Value.Trim().ToUpper();
              switch (lineValue)
              {
                case "NEXT":
                  this.DFHMDI_LINE = DFHMDI_LINE.NEXT;
                  break;

                case "SAME":
                  this.DFHMDI_LINE = DFHMDI_LINE.SAME;
                  break;

                default:
                  if (p.Value.Trim().IsInteger())
                  {
                    // check for valid range of numbers
                    this.DFHMDI_LINE = DFHMDI_LINE.NUMBER;
                    this.Line = p.Value.Trim().ToInt32();
                  }
                  else
                  {
                    // add error and continue
                  }
                  break;
              }
              break;

            case "OBFMT":
              break;

            case "PS":
              break;

            case "SOSI":
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

            case "TRAILER":
              break;

            case "VALIDN":
              break;

            default:
              // write unrecognized trans error
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
              if (p.Parms == null || p.Parms.Count == 0)
              {
                // add error message and continue;
             
              }

              if (p.Parms.Count != 2)
              {
                // add error message and continue
              }

              if (p.Parms[0].IsNotInteger())
              {
                // add error message anc continue
              }

              if (p.Parms[1].IsNotInteger())
              {
                // add error message and continue
              }

              this.SizeWidth = p.Parms[0].ToInt32();
              this.SizeHeight = p.Parms[1].ToInt32();

              if (this.SizeWidth < 1 || this.SizeWidth > 512)
              {
                // add error message and continue
              }

              if (this.SizeHeight < 1 || this.SizeHeight > 132)
              {
                // add error message and continue
              }

              break;

            default:
              // write unrecognized trans error
              break;
          }
        }

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the Bms_DFHMDI object from the macro value '" + base.BmsStatement.BmsStatementText + "'.", ex); 
      }
    }
  }
}
