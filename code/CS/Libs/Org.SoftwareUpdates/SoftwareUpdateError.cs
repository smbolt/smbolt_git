using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Org.SoftwareUpdates
{
  public enum SoftwareUpdateErrorAction
  {
    NotSet,
    Ignore,
    DisableCheckingToday,
    DisableChecking2Days,
    DisableChecking1Week,
    DisableCheckingPermanently
  }

  public class SoftwareUpdateError
  {
    public string Title {
      get;
      set;
    }
    public string ErrorDetail {
      get;
      set;
    }
    public List<string> ErrorActions {
      get;
      set;
    }
    public Color BackgroundColor {
      get;
      set;
    }

    public SoftwareUpdateError()
    {
      this.Title = String.Empty;
      this.ErrorDetail = String.Empty;
      this.ErrorActions = new List<string>();
      this.BackgroundColor = Color.White;
    }
  }
}
