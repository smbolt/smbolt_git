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
  public class ConfigFtpSpec : CO
  {
    [DefaultValue("")]
    public string FtpServer {
      get;
      set;
    }

    [DefaultValue("")]
    public string FtpUserId {
      get;
      set;
    }

    [DefaultValue("")]
    public string FtpPassword {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool FtpKeepAlive {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool FtpUsePassive {
      get;
      set;
    }

    [DefaultValue(false)]
    public bool FtpUseBinary {
      get;
      set;
    }

    [DefaultValue(8192)]
    public int FtpBufferSize {
      get;
      set;
    }
  }
}
