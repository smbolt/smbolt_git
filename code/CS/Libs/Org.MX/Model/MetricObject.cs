using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.MX.Model
{
  public class MetricObject
  {
    public int ID { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public string Report { get { return Get_Report(); } }

    public MetricObject(int id, int code, string name, string description = "", bool isActive = true)
    {
      this.ID = id;
      this.Code = code;
      this.Name = name;
      this.Description = description;
      this.IsActive = isActive;
    }

    private string Get_Report()
    {
      return this.ID.ToString().PadToLength(8) +
             this.Code.ToString().PadToLength(8) +
             (this.Name.IsNotBlank() ? this.Name.PadToLength(32) : "NO NAME PROPERTY") +
             (this.Description.IsNotBlank() ? this.Description : "NO DESCRIPTION PROPERTY");
    }
  }
}
