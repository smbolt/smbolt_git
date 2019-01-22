using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.PdfExtract
{
  public enum PipeType
  {
    Production = 1,
    Demand = 2,
    Storge = 3,
    Balance = 4,
    HDD = 5,
    CDD = 6,
    Exports = 20,
    Imports = 21,
    NotSet = 99
  }


  public class GenscapeItem
  {
    public string LocationName {
      get;
      set;
    }
    public string LocationID {
      get;
      set;
    }
    public string ColumnIndex {
      get;
      set;
    }
    public string PipeName {
      get;
      set;
    }
    public PipeType PipeType {
      get;
      set;
    }
    public float PipeCapacity {
      get;
      set;
    }
    public DateTime ValueDate {
      get;
      set;
    }
    public float Value {
      get;
      set;
    }
    public float Capacity {
      get;
      set;
    }
    public string Key {
      get {
        return Get_Key();
      }
    }

    public GenscapeItem()
    {
      Initialize();
    }

    public GenscapeItem(string locationName, string locationId, DateTime valueDate, PipeType pipeType)
    {
      Initialize();
      this.LocationName = locationName;
      this.LocationID = locationId;
      this.ValueDate = valueDate;
      this.PipeType = pipeType;
    }

    private void Initialize()
    {
      this.LocationName = String.Empty;
      this.LocationID = String.Empty;
      this.ColumnIndex = "999";
      this.PipeName = String.Empty;
      this.PipeType = PipeType.NotSet;
      this.PipeCapacity = 0;
      this.ValueDate = DateTime.MinValue;
      this.Value = 0;
      this.Capacity = 5.55F;
    }

    private string Get_Key()
    {
      string valueDate = this.ValueDate.ToCCYYMMDD();

      return this.LocationID + "-" +
             this.PipeType.ToInt32().ToString("000") + "-" +
             this.ColumnIndex + "-" +
             this.ValueDate.ToCCYYMMDD();
    }

    public string ToReport()
    {
      return this.Key + " " +
             this.LocationID + " " +
             this.LocationName.PadTo(25) + " " +
             this.PipeType.ToString().PadTo(15) + " " +
             this.PipeName.PadTo(35) + " " +
             this.ValueDate.ToString("yyyyMMdd") + " " +
             this.ColumnIndex + " " +
             this.Value.ToString("###,##0.00").PadToJustifyRight(10) + g.crlf;
    }
  }
}
