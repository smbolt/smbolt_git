using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.MOD
{
  public class ModuleEventManager : MarshalByRefObject
  {
    public event EventHandler ModuleEvent;
    public void FireEvent() { ModuleEvent(this, EventArgs.Empty); }
  }
}
