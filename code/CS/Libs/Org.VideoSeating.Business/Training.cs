using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using Org.GR;
using Org.GS;

namespace Org.VideoSeating.Business
{
	public enum Season
	{
		NotSet,
		Summer,
		Winter
	}

	[XMap(XType= XType.Element)]
	public class Training
	{
		[XMap]
		public Season Season { get; set; }

		[XMap]
		public int Year { get; set; }

		[XMap]
		public int SeatSpotWidth 
		{
			get { return _seatSpotWidth; }
			set 
			{
				_seatSpotWidth = value;
				_seatSpotSize = new Size(value, value);
				_seatSize = new Size(value - 4, value - 4); 
			}
		}
		private int _seatSpotWidth;

		private Size _seatSpotSize;
		private Size _seatSize;

		[XMap]
		public int TopMargin { get; set; }

		[XMap]
		public int MinRightMargin { get; set; }

		[XMap]
		public int MinBottomMargin { get; set; }

		[XMap]
		public int MinLeftMargin { get; set; }

		[XMap]
		public int Padding { get; set; }

		[XMap]
		public int Columns { get; set; }

		[XMap]
		public int Rows { get; set; }

		[XMap]
		public int Aisle { get; set; }

		public int ChartWidth { get { return (this.Columns * _seatSpotSize.Width) + ((this.Columns - 1) * this.Padding); } }
		public int ChartWidthWithMargins { get { return this.ChartWidth + this.MinLeftMargin + this.MinRightMargin; } }
		public int ChartHeight { get { return (this.Rows * _seatSpotSize.Height) + ((this.Rows - 1) * this.Padding); } }
		public int ChartHeightWithMargins { get { return this.ChartHeight + this.TopMargin + this.MinBottomMargin; } }
		public string YearAndSeason { get { return this.Year.ToString() + "-" + this.Season.ToString(); } }
		public string SeasonAndYearDisplayInParens { get { return "(" + this.Season.ToString() + " " + this.Year.ToString() + ")"; } }
		public string Folder { get; set; }
		public string FileName { get { return this.YearAndSeason + ".xml"; } }
		public string FullFilePath { get { return this.Folder + @"\" + this.FileName; } }

		public DrawingObjectSet DrawingObjectSet { get { return Get_DrawingObjectSet(); } }
 
		[XMap(XType = XType.Element, CollectionElements = "SeatSpot", WrapperElement = "SeatSpotSet")]
		public SeatSpotSet SeatSpotSet { get; set; }

		public Training()
		{
			this.Season = Season.NotSet;
			this.Year = DateTime.Now.Year;
			this.SeatSpotSet = new SeatSpotSet();
			this.SeatSpotSet.Training = this;
		}

		public Training(string legacyXml)
		{
			CreateFromLegacyXml(legacyXml); 
		}

		public void CreateSeatSpots(Size uiSize)
		{
			this.Aisle = (int) this.Columns / 2; 

			int cw = uiSize.Width;
			int ch = uiSize.Height;

			this.SeatSpotSet = new SeatSpotSet();
			int startX = cw / 2 - _seatSpotSize.Width / 2;

			int bottomExtent = 0;
			int rightExtent = 0;
			int leftSideOfAisle = 0;
			int seatNbrA = 0;
			int seatNbrB = 0;

			for (var r = 0; r < this.Rows; r++)
			{
				for (var c = 0; c < this.Columns; c++)
				{
					if (c != this.Aisle)
					{
						var location = new Point(startX + c * (_seatSpotSize.Width + this.Padding), this.TopMargin + r * (_seatSpotSize.Height + this.Padding));

						if (rightExtent == 0 && c == this.Columns - 1)
							rightExtent = location.X + _seatSpotSize.Width;

						if (bottomExtent == 0 && r == this.Rows - 1)
							bottomExtent = location.Y + _seatSpotSize.Height;

						int seatNbr = c < this.Aisle ? ++seatNbrA : ++seatNbrB;
						string section = c < this.Aisle ? "A" : "B";
						string text = section + "-" + seatNbr.ToString();
						var seatSpot = new SeatSpot(this.SeatSpotSet.Count, text, _seatSpotSize, location, section, seatNbr, c, r);
						seatSpot.SeatSpotSet = this.SeatSpotSet;
						seatSpot.Training = this; 
						this.SeatSpotSet.Add(seatSpot.RowCol, seatSpot);
						seatSpot.Panel.BackColor = Color.White;
					}
					else
					{
						if (leftSideOfAisle == 0)
							leftSideOfAisle = startX + c * (_seatSpotSize.Width + this.Padding);
					}
				}
			}

			this.SeatSpotSet.TopArea = new Rectangle(0, 0, cw, this.TopMargin);

			if (cw > rightExtent)
				this.SeatSpotSet.RightArea = new Rectangle(rightExtent, this.TopMargin, cw - rightExtent, bottomExtent < ch ? bottomExtent - this.TopMargin : ch - this.TopMargin);
			else
				this.SeatSpotSet.RightArea = new Rectangle();

			if (ch > bottomExtent)
				this.SeatSpotSet.BottomArea = new Rectangle(0, bottomExtent, rightExtent, ch - bottomExtent);
			else
				this.SeatSpotSet.BottomArea = new Rectangle();

			this.SeatSpotSet.LeftArea = new Rectangle(0, this.TopMargin, startX, bottomExtent - this.TopMargin);

			this.SeatSpotSet.Aisle = new Rectangle(leftSideOfAisle, this.TopMargin, _seatSpotSize.Width + this.Padding, this.SeatSpotSet.LeftArea.Height); 
		}

		public void SetNonSeatAreas(Size uiSize)
		{
			this.Aisle = (int)this.Columns / 2;

			int cw = uiSize.Width;
			int ch = uiSize.Height;

			int startX = cw / 2 - _seatSpotSize.Width / 2;

			int bottomExtent = 0;
			int rightExtent = 0;
			int leftSideOfAisle = 0;

			for (var r = 0; r < this.Rows; r++)
			{
				for (var c = 0; c < this.Columns; c++)
				{
					if (c != this.Aisle)
					{
						var location = new Point(startX + c * (_seatSpotSize.Width + this.Padding), this.TopMargin + r * (_seatSpotSize.Height + this.Padding));

						if (rightExtent == 0 && c == this.Columns - 1)
							rightExtent = location.X + _seatSpotSize.Width;

						if (bottomExtent == 0 && r == this.Rows - 1)
							bottomExtent = location.Y + _seatSpotSize.Height;
					}
					else
					{
						if (leftSideOfAisle == 0)
							leftSideOfAisle = startX + c * (_seatSpotSize.Width + this.Padding);
					}
				}
			}

			this.SeatSpotSet.TopArea = new Rectangle(0, 0, cw, this.TopMargin);

			if (cw > rightExtent)
				this.SeatSpotSet.RightArea = new Rectangle(rightExtent, this.TopMargin, cw - rightExtent, bottomExtent < ch ? bottomExtent - this.TopMargin : ch - this.TopMargin);
			else
				this.SeatSpotSet.RightArea = new Rectangle();

			if (ch > bottomExtent)
				this.SeatSpotSet.BottomArea = new Rectangle(0, bottomExtent, rightExtent, ch - bottomExtent);
			else
				this.SeatSpotSet.BottomArea = new Rectangle();

			this.SeatSpotSet.LeftArea = new Rectangle(0, this.TopMargin, startX, bottomExtent - this.TopMargin);

			this.SeatSpotSet.Aisle = new Rectangle(leftSideOfAisle, this.TopMargin, _seatSpotSize.Width + this.Padding, this.SeatSpotSet.LeftArea.Height); 
		}

		public Size UpdateChartLayout(Size uiSize)
		{
			var updatedUiSize = new Size(uiSize.Width, uiSize.Height); 

			if (updatedUiSize.Width < this.ChartHeightWithMargins)
				updatedUiSize.Width = this.ChartHeightWithMargins;

			int cw = uiSize.Width;
			int ch = uiSize.Height;

			int startX = cw / 2 - this.ChartWidth / 2;

			if (startX < this.MinLeftMargin)
				startX = this.MinLeftMargin;

			int bottomExtent = 0;
			int rightExtent = 0;
			int leftSideOfAisle = 0;

			var locationDictionary = new Dictionary<string, Point>();

			for (var r = 0; r < this.Rows; r++)
			{
				for (var c = 0; c < this.Columns; c++)
				{
					if (c != this.Aisle)
					{
						var location = new Point(startX + c * (_seatSpotSize.Width + this.Padding), this.TopMargin + r * (_seatSpotSize.Height + this.Padding));

						if (rightExtent == 0 && c == this.Columns - 1)
							rightExtent = location.X + _seatSpotSize.Width;

						if (bottomExtent == 0 && r == this.Rows - 1)
							bottomExtent = location.Y + _seatSpotSize.Height;

						string key = "R" + r.ToString("00") + "C" + c.ToString("00");
						locationDictionary.Add(key, location); 
					}
					else
					{
						if (leftSideOfAisle == 0)
							leftSideOfAisle = startX + c * (_seatSpotSize.Width + this.Padding); 
					}
				}
			}

			foreach(var seatSpot in this.SeatSpotSet.Values)
			{
				if (!locationDictionary.ContainsKey(seatSpot.RowCol))
					throw new Exception("Unable to set location for seat spot at row " + seatSpot.Row.ToString() + " and column " + seatSpot.Column.ToString() + "."); 

				seatSpot.SetLocation(locationDictionary[seatSpot.RowCol]); 
				
				if (seatSpot.Seat != null)
				{
					seatSpot.Seat.Label.Location = seatSpot.SeatLocation; 
				}
			}

			this.SeatSpotSet.TopArea = new Rectangle(0, 0, cw, this.TopMargin);

			if (cw > rightExtent)
				this.SeatSpotSet.RightArea = new Rectangle(rightExtent, this.TopMargin, cw - rightExtent, bottomExtent < ch ? bottomExtent - this.TopMargin : ch - this.TopMargin);
			else
				this.SeatSpotSet.RightArea = new Rectangle();

			if (updatedUiSize.Height > bottomExtent)
				this.SeatSpotSet.BottomArea = new Rectangle(0, bottomExtent, rightExtent, updatedUiSize.Height - bottomExtent);
			else
				this.SeatSpotSet.BottomArea = new Rectangle();

			this.SeatSpotSet.LeftArea = new Rectangle(0, this.TopMargin, startX, bottomExtent - this.TopMargin);

			this.SeatSpotSet.Aisle = new Rectangle(leftSideOfAisle, this.TopMargin, _seatSpotSize.Width + this.Padding, this.SeatSpotSet.LeftArea.Height); 

			return updatedUiSize;
		}

		public void Open()
		{
			try
			{
				using (var f = new ObjectFactory2())
				{
					string trainingString = File.ReadAllText(this.FullFilePath);
					var t = f.Deserialize(XElement.Parse(trainingString)) as Training;
					this.Season = t.Season;
					this.Year = t.Year;
					this.SeatSpotWidth = t.SeatSpotWidth;
					this.TopMargin = t.TopMargin;
					this.MinRightMargin = t.MinRightMargin;
					this.MinBottomMargin = t.MinBottomMargin;
					this.MinLeftMargin = t.MinLeftMargin;
					this.Padding = t.Padding;
					this.Columns = t.Columns;
					this.Rows = t.Rows;
					this.Aisle = t.Aisle;
					this.SeatSpotSet = t.SeatSpotSet;
					this.RenumberSeats();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to save the training file.", ex);
			}
		}

		public void Save()
		{
			try
			{
				using (var f = new ObjectFactory2())
				{
					File.WriteAllText(this.FullFilePath, f.Serialize(this).ToString()); 
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to save the training file.", ex);
			}
		}

		public void AutoInit()
		{
			this.SeatSpotSet.Training = this;

			foreach (var seatSpot in this.SeatSpotSet.Values)
			{
				seatSpot.Training = this;
				seatSpot.SeatSpotSet = this.SeatSpotSet;
				seatSpot.Size = this._seatSpotSize;
				seatSpot.Panel = new Panel();
				seatSpot.Panel.Name = "SeatSpot" + seatSpot.SeatSpotId.ToString();
				if (seatSpot.IsActive)
					seatSpot.Panel.BackColor = Color.White;
				else
					seatSpot.Panel.BackColor = SystemColors.AppWorkspace; 
				seatSpot.Panel.Size = _seatSpotSize;
				seatSpot.Panel.Location = seatSpot.Location;
				seatSpot.Panel.BorderStyle = BorderStyle.FixedSingle;
				seatSpot.Panel.Tag = seatSpot;

				seatSpot.SeatSpotLabel = new Label();
				seatSpot.SeatSpotLabel.AutoSize = false;
				seatSpot.SeatSpotLabel.Size = _seatSpotSize;
				seatSpot.SeatSpotLabel.Text = seatSpot.SectionAndSeatNbr;
				seatSpot.SeatSpotLabel.TextAlign = ContentAlignment.TopCenter;
				seatSpot.SeatSpotLabel.Dock = DockStyle.Fill;
				seatSpot.SeatSpotLabel.Tag = seatSpot;
				seatSpot.Panel.Controls.Add(seatSpot.SeatSpotLabel);

				if (seatSpot.Seat != null)
				{
					seatSpot.Seat.Row = seatSpot.Row;
					seatSpot.Seat.Column = seatSpot.Column;
					seatSpot.Seat.Label = new Label();
					seatSpot.Seat.Label.Size = _seatSize;
					seatSpot.Seat.Label.Location = seatSpot.SeatLocation;
					seatSpot.Seat.SeatSpot = seatSpot;

					seatSpot.Seat.Label.Padding = new Padding(0, 4, 0, 2);
					seatSpot.Seat.Label.Name = "Seat-R" + seatSpot.Seat.Row.ToString("00") + "C" + seatSpot.Seat.Column.ToString("00");
					seatSpot.Seat.Label.Tag = seatSpot;
					seatSpot.Seat.Label.BackColor = Color.DodgerBlue;
					seatSpot.Seat.Label.Text = seatSpot.Seat.Text;
					seatSpot.Seat.Label.BorderStyle = BorderStyle.FixedSingle;
					seatSpot.Seat.Label.TextAlign = ContentAlignment.TopCenter;
					seatSpot.Seat.Label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
					seatSpot.Seat.Label.BringToFront();
				}
			}
		}

		public string GetReport()
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("Training Report - " + this.Season.ToString() + " " + this.Year.ToString() + g.crlf2);

				foreach (var seatSpot in this.SeatSpotSet.Values)
				{
					int seatSpotId = seatSpot.SeatSpotId;
					var panelTag = seatSpot.Panel.Tag as SeatSpot;
				  SeatSpot labelSeatSpot = null;

					sb.Append(seatSpot.SeatSpotId.ToString("000") + " " + panelTag.SeatSpotId.ToString("000") + "  " + seatSpot.RowCol + "   ");
					if (seatSpot.Seat == null)
					{
						sb.Append("-");
					}
					else
					{
						var seat = seatSpot.Seat;
						var labelTag = seatSpot.SeatSpotLabel.Tag;
						if (labelTag != null)
							labelSeatSpot = labelTag as SeatSpot;

						sb.Append("Seat-" + seat.RowCol + "  " + seat.ChartName + "  (" + seat.SeatSpot.SectionAndSeatNbr + ")"); 
					}

					sb.Append(g.crlf);

					if (seatSpotId != panelTag.SeatSpotId)
						sb.Append("ERROR => seatSpot.SeatSpotId (" + seatSpotId.ToString("000") + ") != seatSpot.Panel.SeatSpotId (" + panelTag.SeatSpotId.ToString("000") + ")" + g.crlf);

					if (seatSpot.Seat != null && labelSeatSpot == null)
						sb.Append("ERROR => seat label tag is null" + g.crlf);

					if (seatSpot.Seat != null && labelSeatSpot != null && labelSeatSpot.SeatSpotId != seatSpotId)
						sb.Append("ERROR => seatSpot.SeatSpotId (" + seatSpotId.ToString("000") + ") != seat.Panel.SeatSpotId (" + labelSeatSpot.SeatSpotId.ToString("000") + ")" + g.crlf); 

				}

				string report = sb.ToString();
				return report;
			}
			catch (Exception ex)
			{
				return "An exception occurred attempting to create the report." + g.crlf2 + ex.ToReport();
			}
		}

		public void RenumberSeats()
		{
			int seatNbrA = 0;
			int seatNbrB = 0;

			int col = 0;
			int row = 0;



			foreach (var seatSpot in this.SeatSpotSet.Values)
			{
				if (col > this.Columns - 1)
				{
					row++;
					col = 0;

				}

				seatSpot.Row = row;
				seatSpot.Column = col;

				col++;
				if (col == this.Aisle)
					col++;


				if (seatSpot.IsActive)
				{
					if (seatSpot.Column < this.Aisle)
					{
						seatSpot.Section = "A";
						seatSpot.SeatNbr = ++seatNbrA;
						seatSpot.SeatSpotLabel.Text = seatSpot.SectionAndSeatNbr;
						if (seatSpot.Seat != null)
						{
							seatSpot.Seat.Row = seatSpot.Row;
							seatSpot.Seat.Column = seatSpot.Column;
							seatSpot.Seat.Section = seatSpot.Section;
							seatSpot.Seat.SeatNbr = seatSpot.SeatNbr;
							seatSpot.Seat.Label.Text = seatSpot.Seat.Text;
						}
					}
					else
					{
						seatSpot.Section = "B";
						seatSpot.SeatNbr = ++seatNbrB;
						seatSpot.SeatSpotLabel.Text = seatSpot.SectionAndSeatNbr;
						if (seatSpot.Seat != null)
						{
							seatSpot.Seat.Row = seatSpot.Row;
							seatSpot.Seat.Column = seatSpot.Column;
							seatSpot.Seat.Section = seatSpot.Section;
							seatSpot.Seat.SeatNbr = seatSpot.SeatNbr;
							seatSpot.Seat.Label.Text = seatSpot.Seat.Text;
						}
					}
				}
				else
				{
					seatSpot.Section = String.Empty;
					seatSpot.SeatNbr = 0;
					seatSpot.SeatSpotLabel.Text = String.Empty;
					if (seatSpot.Seat != null)
					{
						seatSpot.Seat.Row = seatSpot.Row;
						seatSpot.Seat.Column = seatSpot.Column;
					}
				}
			}
		}

		private void CreateFromLegacyXml(string xml)
		{
			XElement trainingXml = XElement.Parse(xml);
			this.Season = g.ToEnum<Season>(trainingXml.Attribute("season").Value, Season.NotSet);
			this.Year = trainingXml.Attribute("year").Value.ToInt32();

			this.SeatSpotSet = new SeatSpotSet();
			IEnumerable<XElement> seats = trainingXml.Element("seats").Elements();

			foreach (var seat in seats)
			{
				var seatSpot = new SeatSpot();
				seatSpot.SeatSpotId = seat.Attribute("index").Value.ToInt32() - 1;
				seatSpot.IsActive = seat.Attribute("active").Value.ToBoolean();
				string occupant = seat.Attribute("occupant").Value;
				if (occupant != "INACTIVE")
				{
					seatSpot.Seat = new Seat();
					seatSpot.Seat.ChartName = occupant;
					seatSpot.Seat.Trainee1 = new Trainee();
					seatSpot.Seat.Trainee1.FirstName = seat.Attribute("NameF1").Value;
					seatSpot.Seat.Trainee1.LastName = seat.Attribute("NameL1").Value;
					seatSpot.Seat.Trainee1.Number = 1;
					seatSpot.Seat.Trainee2.RegistrationMode = RegistrationMode.NotUsed;
					seatSpot.Seat.Trainee2.Number = 2;

					string schedule = seat.Attribute("schedule").Value;
					switch (schedule)
					{
						case "0":
							seatSpot.Seat.RegistrationMode = RegistrationMode.FullTime;
							break;

						case "1":
							seatSpot.Seat.RegistrationMode = RegistrationMode.PartTime;
							break;

						case "2":
							seatSpot.Seat.RegistrationMode = RegistrationMode.SharedSeat;
							seatSpot.Seat.Trainee2.FirstName = seat.Attribute("NameF2").Value;
							seatSpot.Seat.Trainee2.FirstName = seat.Attribute("NameL2").Value; 
							break;
					}

				}

				this.SeatSpotSet.Add(this.SeatSpotSet.Count.ToString(), seatSpot); 
			}
		}

		private DrawingObjectSet Get_DrawingObjectSet()
		{
			var doSet = new DrawingObjectSet();

			if (this.SeatSpotSet != null && this.SeatSpotSet.Count > 0)
			{
				foreach(var seatSpot in this.SeatSpotSet)
				{
					doSet.Add(doSet.Count, seatSpot.Value);
				}
			}

			return doSet; 
		}

	}
}
