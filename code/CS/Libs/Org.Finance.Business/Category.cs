using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Finance.Business
{
  public class Category
  {
    public int Id {
      get;
      set;
    }
    public string IdFormat {
      get {
        return Get_IdFormat();
      }
    }
    public string CategoryDisplay {
      get {
        return Get_CategoryDisplay();
      }
    }
    public string Name {
      get;
      set;
    }
    public bool IsTaxRelated {
      get;
      set;
    }

    public Category()
    {
      this.Id = 0;
      this.Name = String.Empty;
      this.IsTaxRelated = false;
    }

    public Category(int id, string name, bool isTaxRelated)
    {
      this.Id = id;
      this.Name = name.Trim();
      this.IsTaxRelated = isTaxRelated;
    }

    private string Get_IdFormat()
    {
      string idString = this.Id.ToString();
      return idString.Substring(0, 3) + "." + idString.Substring(3);
    }

    private string Get_CategoryDisplay()
    {
      return Get_IdFormat() + " - " + this.Name;
    }
  }
}
