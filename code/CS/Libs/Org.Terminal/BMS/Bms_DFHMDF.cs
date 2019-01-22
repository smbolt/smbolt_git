using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class Bms_DFHMDF : Bms_BASE
  {
    public string Name { get; set; }
    public int PosLine { get; private set; }
    public int PosColumn { get; private set; }
    public bool AttrbProt { get; private set; }
    public bool AttrbNum { get; private set; }
    public bool AttrbBright { get; private set; }
    public bool AttrbDark { get; private set; }
    public bool AttrbDet { get; private set; }
    public bool AttrbIC { get; private set; }
    public bool AttrbFSET { get; private set; }
    public int Length { get; private set; }
    public int? MaxSize { get; private set; }
    public int? MaxOccurs { get; private set; }
    public int InitUnderlineChar { get; private set; }
    public string Init { get; private set; }
    public bool UseInit { get; private set; }
    public string Fill { get; private set; }
    public bool UseFill { get; private set; }
    public string HexInit { get; private set; }
    public bool UseHexInit { get; private set; }
    public string GInit { get; private set; }
    public bool UseGInit { get; private set; }
    public string GrpName { get; private set; }
    public int Occurs { get; private set; }
    public string PicIn { get; private set; }
    public string PicOut { get; private set; }
    public string SkipTo { get; private set; }
    

    public bool OutlineBox { get; private set; }
    public bool OutlineTop { get; private set; }
    public bool OutlineRight { get; private set; }
    public bool OutlineBottom { get; private set; }
    public bool OutlineLeft { get; private set; }
    public bool Underline { get; set; }

    private List<string> _attrb { get; set; }

    public DFH_COLOR DFH_COLOR { get; private set; }
    public DFH_HILIGHT DFH_HILIGHT { get; private set; }
    public DFH_PS DFH_PS { get; private set; }
    public DFH_TRANSP DFH_TRANSP { get; private set; }
    public DFHMDF_CASE DFHMDF_CASE { get; private set; }
    public DFHMDF_HFLEX DFHMDF_HFLEX { get; private set; }
    public DFHMDF_VFLEX DFHMDF_VFLEX { get; private set; }

    public Bms_DFHMDF(BmsStatement bmsStatement)
      : base(bmsStatement)
    {
      this._attrb = new List<string>();

      SetDefaults();
      Populate();
    }

    private void Populate()
    {
      try
      {
        int apostCount = 0; 

        string[] remainingTokens = base.BmsStatement.RemainingText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

        if (remainingTokens.Length == 2 && remainingTokens[0] != "DFHMDF")
          this.Name = remainingTokens[0].ToUpper();

        foreach (var p in base.BmsStatement.Parameters)
        {
          string parmValue = p.Value.Trim();

          switch (p.Key)
          {
            case "COLOR":
              switch (parmValue.ToUpper())
              {
                case "NEUTRAL":
                  this.DFH_COLOR = DFH_COLOR.NEUTRAL;
                  break;

                case "DEFAULT":
                  this.DFH_COLOR = DFH_COLOR.DEFAULT;
                  break;

                case "BLUE":
                  this.DFH_COLOR = DFH_COLOR.BLUE;
                  break;

                case "RED":
                  this.DFH_COLOR = DFH_COLOR.RED;
                  break;

                case "PINK":
                  this.DFH_COLOR = DFH_COLOR.PINK;
                  break;

                case "GREEN":
                  this.DFH_COLOR = DFH_COLOR.GREEN;
                  break;

                case "TURQUOISE":
                  this.DFH_COLOR = DFH_COLOR.TURQUOISE;
                  break;

                case "YELLOW":
                  this.DFH_COLOR = DFH_COLOR.YELLOW;
                  break;

                default:
                  // add error and continue;
                  break;
              }
              break;

            case "INITIAL":
              apostCount = parmValue.CountOfChar('\'');
              // figure out how embedded single quotes are coded
              // get them "translated" first, then check for the required
              // beginning and ending quotes

              if (apostCount != 2)
              {
                // add error and continue
              }

              if (!parmValue.StartsWith("'"))
              {
                // add error and continue
              }

              if (!parmValue.EndsWith("'"))
              {
                // add error and continue
              }

              string initValue = parmValue.Substring(1, parmValue.Length  - 2);
              this.InitUnderlineChar = -1;
              if (initValue.Contains("~"))
              {
                this.InitUnderlineChar = initValue.IndexOf("~");
                initValue = initValue.Replace("~", String.Empty);
              }

              this.Init = initValue;
              this.UseInit = true;

              if (this.Init.Length > 4096)
              {
                // too long
              }
              break;

            case "FILL":
              apostCount = parmValue.CountOfChar('\'');
              // figure out how embedded single quotes are coded
              // get them "translated" first, then check for the required
              // beginning and ending quotes

              if (apostCount != 2)
              {
                // add error and continue
              }

              if (!parmValue.StartsWith("'"))
              {
                // add error and continue
              }

              if (!parmValue.EndsWith("'"))
              {
                // add error and continue
              }

              string fillValue = parmValue.Substring(1, parmValue.Length  - 2);

              this.Fill = fillValue;
              this.UseFill = true;

              if (this.Fill.Length > 1)
              {
                // too long
              }
              break;

            case "LENGTH":
              if (parmValue.IsInteger())
              {
                this.Length = parmValue.ToInt32();
                if (this.Length < 1 || this.Length > 4096)
                {
                  // write error and continue
                }
              }
              else
              {
                // add error and continue
              }
              break;
            case "SKIPTO":
              if (parmValue.IsNotBlank())
              {
                this.SkipTo = parmValue.Trim();
              }
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
              if (ValidateAttributes(p.Parms))
              {
                foreach (var attr in p.Parms)
                {
                  switch (attr)
                  {
                    case "PROT":
                      this.AttrbProt = true;
                      break;

                    case "UNPROT":
                      this.AttrbProt = false;
                      break;

                    case "NUM":
                      this.AttrbNum = true;
                      break;

                    case "BRT":
                      this.AttrbBright = true;
                      this.AttrbDark = false;
                      break;

                    case "NORM":
                      this.AttrbBright = false;
                      this.AttrbDark = false;
                      break;

                    case "DRK":
                      this.AttrbBright = false;
                      this.AttrbDark = true;
                      break;

                    case "DET":
                      this.AttrbDet = true;
                      break;

                    case "IC":
                      this.AttrbIC = true;
                      break;

                    case "FSET":
                      this.AttrbFSET = true;
                      break;

                  }
                }
              }
              break;

            case "POS":
              if (p.Parms == null || p.Parms.Count != 2)
              {
                // add error and continue
              }

              if (p.Parms[0].IsNotInteger())
              {
                // add error and continue
              }

              if (p.Parms[1].IsNotInteger())
              {
                // add error and continue
              }

              this.PosLine = p.Parms[0].ToInt32();
              this.PosColumn = p.Parms[1].ToInt32();

              if (this.PosLine < 1 || this.PosLine > 132)
              {
                // add error and continue
              }

              if (this.PosColumn < 1 || this.PosColumn > 512)
              {
                // add error and continue
              }
              break;

            case "HFLEX":
              if (p.Parms == null || p.Parms.Count < 1)
              {
                // add error and continue
              }

              switch (p.Parms[0])
              {
                case "S":
                  this.DFHMDF_HFLEX = DFHMDF_HFLEX.STRETCH;
                  break;

                case "F":
                  this.DFHMDF_HFLEX = DFHMDF_HFLEX.FLOATRIGHT;
                  break;

                default:
                  // add error and continue
                  break;
              }

              if (p.Parms.Count > 1)
              {
                if (p.Parms[1].IsNotInteger())
                {
                  // add error and continue
                }

                this.MaxSize = p.Parms[1].ToInt32();
              }
              break;

            case "VFLEX":
              if (p.Parms == null || p.Parms.Count < 1)
              {
                // add error and continue
              }

              switch (p.Parms[0])
              {
                case "O":
                  this.DFHMDF_VFLEX = DFHMDF_VFLEX.OCCURS;
                  break;

                default:
                  // add error and continue
                  break;
              }

              if (p.Parms.Count > 1)
              {
                if (p.Parms[1].IsNotInteger())
                {
                  // add error and continue
                }

                this.MaxOccurs = p.Parms[1].ToInt32();
              }
              break;

            default:
              // write unrecognized parm error
              break;
          }
        }

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the Bms_DFHMDF object from the macro value '" + base.BmsStatement.BmsStatementText + "'.", ex); 
      }
    }

    private void SetDefaults()
    {
      this.Name = String.Empty;
      this.PosLine = 0;
      this.PosColumn = 0;
      this.AttrbProt = false;
      this.AttrbNum = false;
      this.AttrbBright = false;
      this.AttrbDark = false;
      this.AttrbDet = false;
      this.AttrbIC = false;
      this.AttrbFSET = false;
      this.Length = 0;
      this.Init = String.Empty;
      this.UseInit = false;
      this.HexInit = String.Empty;
      this.UseHexInit = false;
      this.GInit = String.Empty;
      this.UseGInit = false;
      this.GrpName = String.Empty;
      this.Occurs = -1;
      this.OutlineBox = false;
      this.OutlineTop = false;
      this.OutlineRight = false;
      this.OutlineBottom = false;
      this.OutlineLeft = false;
      this.Underline = false;
      this.SkipTo = String.Empty;
    }

    private bool ValidateAttributes(List<string> attrs)
    {
      if (attrs == null || attrs.Count == 0)
      {
        // write error and continue
        return false;
      }

      // check for invalid attributes
      foreach (var attr in attrs)
      {
        switch (attr)
        {
          case "PROT":
          case "UNPROT":
          case "NUM":
          case "BRT":
          case "NORM":
          case "DRK":
          case "DET":
          case "IC":
          case "FSET":
            continue;
            
          default:
            // add error and continue
            return false;
        }
      }

      if (attrs.Count == 1)
        return true;

      if (attrs.Contains("BRT") && attrs.Contains("NORM"))
      {
        // write error
        return false;
      }

      if (attrs.Contains("BRT") && attrs.Contains("DRK"))
      {
        // write error
        return false;
      }

      if (attrs.Contains("NORM") && attrs.Contains("DRK"))
      {
        // write error
        return false;
      }

      if (attrs.Contains("PROT") && attrs.Contains("UNPROT"))
      {
        // write error
        return false;
      }

      return true;
    }

    public Bms_DFHMDF CloneForVFLEX(int lineNumber)
    {
      var clone = new Bms_DFHMDF(this.BmsStatement.CloneForVFLEX(lineNumber));

      return clone; 
    }
  }
}
