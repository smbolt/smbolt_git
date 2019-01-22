using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using Org.GR;
using Org.GS;

namespace Org.VideoSeating.Business
{
  [XMap(XType = XType.Element)]
  public class SeatSpot : DrawingObject
  {
    [XMap]
    public int SeatSpotId {
      get;
      set;
    }

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
    public bool IsActive
    {
      get {
        return _isActive;
      }
      set
      {
        _isActive = value;
        Activate(_isActive);
      }
    }
    private bool _isActive;

    [XMap]
    public string Section {
      get;
      set;
    }
    [XMap(DefaultValue="0")]
    public int SeatNbr {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public Seat Seat
    {
      get {
        return _seat;
      }
      set
      {
        _seat = value;
        if (_seat != null)
        {
          _seat.SeatSpot = this;
          _seat.Row = this.Row;
          _seat.Column = this.Column;
          if (_seat.Label != null)
          {
            _seat.Label.Tag = this;
            _seat.Label.Text = _seat.Text;
          }
        }
      }
    }
    private Seat _seat;

    public string Text {
      get;
      set;
    }
    public string SectionAndSeatNbr {
      get {
        return Get_SectionAndSeatNbr();
      }
    }
    public Size SeatSize {
      get {
        return new Size(this.Size.Width - 2, this.Size.Height - 2);
      }
    }
    public Point SeatLocation {
      get {
        return this.Location.Move(2, 2);
      }
    }
    public bool IsOccupied {
      get {
        return this.Seat != null;
      }
    }
    public bool IsHighlighted {
      get;
      set;
    }
    public RegistrationMode RegistrationMode {
      get;
      set;
    }
    public Panel Panel {
      get;
      set;
    }
    public Label SeatSpotLabel {
      get;
      set;
    }
    public SeatSpotSet SeatSpotSet {
      get;
      set;
    }
    public Training Training {
      get;
      set;
    }

    public Rectangle Rectangle
    {
      get
      {
        var rectSize = new Size(this.Size.Width + 2, this.Size.Height + 2);
        return new Rectangle(this.Location.X - 1, this.Location.Y - 1, rectSize.Width, rectSize.Height);
      }
    }

    public SeatSpot()
    {
    }

    public SeatSpot(int seatSpotId, string text, Size size, Point location, string section, int seatNbr, int column, int row)
    {
      this.SeatSpotId = seatSpotId;
      this.IsActive = true;
      this.Text = text;
      this.Size = size;
      this.Location = location;
      this.Section = section;
      this.SeatNbr = seatNbr;
      this.Column = column;
      this.Row = row;
      this.RegistrationMode = RegistrationMode.PartTime;
      this.IsHighlighted = false;

      this.Panel = new Panel();
      this.Panel.Name = "SeatSpot" + this.SeatSpotId.ToString();
      this.Panel.BackColor = Color.White;
      this.Panel.Size = this.Size;
      this.Panel.Location = this.Location;
      this.Panel.BorderStyle = BorderStyle.FixedSingle;
      this.Panel.Tag = this;

      this.SeatSpotLabel = new Label();
      this.SeatSpotLabel.AutoSize = false;
      this.SeatSpotLabel.Size = this.Size;
      this.SeatSpotLabel.Text = this.Text;
      this.SeatSpotLabel.TextAlign = ContentAlignment.TopCenter;
      this.SeatSpotLabel.Dock = DockStyle.Fill;
      this.SeatSpotLabel.Tag = this;
      this.Panel.Controls.Add(this.SeatSpotLabel);
    }

    public void SetLocation(Point location)
    {
      this.Location = location;
      this.Panel.Location = location;
    }

    public void Highlight()
    {
      if (!this.IsActive)
        return;

      this.Panel.BackColor = Color.LightBlue;
      this.IsHighlighted = true;
    }

    public void Unhighlight()
    {
      if (!this.IsActive)
        return;

      this.Panel.BackColor = Color.White;
      this.IsHighlighted = false;
    }

    public void Activate(bool active)
    {
      if (this.Panel == null)
        return;

      if (active)
      {
        this.Panel.BackColor = Color.White;
      }
      else
      {
        this.Panel.BackColor = SystemColors.AppWorkspace;
      }

      this.Training.RenumberSeats();
    }

    private string Get_RowCol()
    {
      return "R" + this.Row.ToString("00") + "C" + this.Column.ToString("00");
    }

    private string Get_SectionAndSeatNbr()
    {
      if (this.IsActive)
        return this.Section + "-" + this.SeatNbr.ToString();
      else
        return String.Empty;
    }

    public override void DrawObject(Graphics gr, float scale, PointF origin)
    {

      var seatSpotRect = new Rectangle(this.Location, this.Size);

      gr.DrawRectangle(Pens.Black, seatSpotRect);

      //var rectTop1 = new Rectangle(x, y, firstSecondWellRowWidth, wellNumberHeight);
      //string topText1 = "CellTop1";
      //var ss1 = new StringSizer(gx, new Point(rectTop1.X, rectTop1.Y), rectTop1, _smallFont, topText1.Trim());
      //gx.FillRectangle(Brushes.White, rectTop1);
      //gx.DrawRectangle(Pens.Black, rectTop1);
      //gx.DrawString(ss1.StringToDraw, _smallFont, _brushGreen, ss1.DrawingPoint);

    }
  }
}
