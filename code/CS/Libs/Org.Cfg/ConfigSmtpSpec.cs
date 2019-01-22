using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.GS;

namespace Org.Cfg
{
  public class ConfigSmtpSpec : CO
  {
    public override string CIType {
      get {
        return this.GetType().Name;
      }
    }

    [DefaultValue("")]
    public string SmtpServer {
      get;
      set;
    }

    [DefaultValue("")]
    public string SmtpPort {
      get;
      set;
    }

    [DefaultValue("")]
    public string SmtpUserId {
      get;
      set;
    }

    [DefaultValue("")]
    public string SmtpPassword {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool EnableSSL {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool PickUpFromIIS {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool AllowAnonymous {
      get;
      set;
    }

    [DefaultValue("")]
    public string EmailFromAddress {
      get;
      set;
    }
  }
}
