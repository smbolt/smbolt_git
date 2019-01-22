using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Org.GS;

namespace Org.VideoSeating.Business
{
  public enum RegistrationMode
  {
    FullTime = 0,
    PartTime = 1,
    SharedSeat = 2,
    NotUsed = 9
  }

  [XMap(XType = XType.Element)]
  public class Seat
  {
    [XMap(IsKey = true)]
    public string RowCol {
      get;
      set;
    }

    [XMap]
    public int Row
    {
      get {
        return _row;
      }
      set
      {
        _row = value;
        this.RowCol = Get_RowCol();
      }
    }
    private int _row;

    [XMap]
    public int Column
    {
      get {
        return _column;
      }
      set
      {
        _column = value;
        this.RowCol = Get_RowCol();
      }
    }
    private int _column;

    [XMap]
    public string Section {
      get;
      set;
    }
    [XMap]
    public int SeatNbr {
      get;
      set;
    }
    [XMap]
    public string ChartName {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "Trainee")]
    public Trainee Trainee1 {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "Trainee")]
    public Trainee Trainee2 {
      get;
      set;
    }

    public string Text {
      get {
        return Get_Text();
      }
    }
    public Point Location {
      get;
      set;
    }
    public Size Size {
      get;
      set;
    }
    public RegistrationMode RegistrationMode {
      get;
      set;
    }
    public SeatSpot SeatSpot {
      get;
      set;
    }
    public Label Label {
      get;
      set;
    }

    public Seat()
    {
      Initialize();
    }

    public Seat(bool initialize)
    {
      if (initialize)
        this.Initialize();
    }

    private void Initialize()
    {
      this.Row = 0;
      this.Column = 0;
      this.RowCol = Get_RowCol();
      this.ChartName = String.Empty;
      this.RegistrationMode = RegistrationMode.PartTime;
      this.Trainee1 = new Trainee(1);
      this.Trainee2 = new Trainee(2);
      this.Label = null;
    }

    public Seat Clone()
    {
      var clone = new Seat(false);
      clone.Size = this.Size;
      clone.Section = this.Section;
      clone.SeatNbr = this.SeatNbr;
      clone.Column = this.Column;
      clone.Row = this.Row;
      clone.ChartName = this.ChartName;
      clone.RegistrationMode = this.RegistrationMode;
      clone.SeatSpot = this.SeatSpot;
      clone.Trainee1 = this.Trainee1.Clone();
      clone.Trainee2 = this.Trainee2.Clone();
      return clone;
    }

    public bool IsUpdated(Seat compare)
    {
      if (compare.ChartName == this.ChartName &&
          compare.RegistrationMode == this.RegistrationMode &&
          !compare.Trainee1.IsUpdated(this.Trainee1) &&
          !compare.Trainee2.IsUpdated(this.Trainee2))
        return false;

      return true;
    }

    private string Get_RowCol()
    {
      return "R" + this.Row.ToString("00") + "C" + this.Column.ToString("00");
    }

    private string Get_Text()
    {
      string regMode = "PT";
      if (this.RegistrationMode == RegistrationMode.FullTime)
        regMode = "FT";
      if (this.RegistrationMode == RegistrationMode.SharedSeat)
        regMode = "SS";

      string text = this.SeatSpot.SectionAndSeatNbr + g.crlf2 +
                    this.ChartName + g.crlf2 +
                    "(" + regMode + ")";

      return text;
    }
  }
}
