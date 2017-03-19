using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS
{
  public class ProxSet : Dictionary<string, Prox>
  {
    public ProxSet(object o)
    {
      Populate(o);
    }

    public void Populate(object o)
    {
      this.Clear();

      if (o == null)
        return;

      var piSet = o.GetType().GetProperties().ToList();

      foreach (var pi in piSet)
      {
        var prox = new Prox();
        prox.Name = pi.Name;
        prox.Type = pi.PropertyType;
        prox.Value = pi.GetValue(o);
        this.Add(prox.Name, prox); 
      }
    }
  }
}
