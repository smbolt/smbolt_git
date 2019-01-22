using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Security
{
  public class ClientHash
  {
    public bool IsValid {
      get;
      set;
    }
    public string AppCodeName {
      get;
      set;
    }
    public string AppName {
      get;
      set;
    }
    public string Product {
      get;
      set;
    }
    public string CookieEnabled {
      get;
      set;
    }
    public string Language {
      get;
      set;
    }
    public string Vendor {
      get;
      set;
    }
    public string Platform {
      get;
      set;
    }
    public string UserAgent {
      get;
      set;
    }
    public string InputEncoding {
      get;
      set;
    }
    public string DefaultCharSet {
      get;
      set;
    }
    public string HashString {
      get;
      set;
    }
    public bool ExceptionOccurred {
      get;
      set;
    }
    public Exception Exception {
      get;
      set;
    }

    public ClientHash()
    {
      this.IsValid = false;
      this.AppCodeName = String.Empty;
      this.AppName = String.Empty;
      this.Product = String.Empty;
      this.CookieEnabled = String.Empty;
      this.Language = String.Empty;
      this.Vendor = String.Empty;
      this.Platform = String.Empty;
      this.UserAgent = String.Empty;
      this.InputEncoding = String.Empty;
      this.DefaultCharSet = String.Empty;
      this.HashString = String.Empty;
      this.ExceptionOccurred = false;
      this.Exception = null;
    }

    public void EnsureValues()
    {
      if (this.AppCodeName == null)
        this.AppCodeName = String.Empty;

      if (this.AppName == null)
        this.AppName = String.Empty;

      if (this.Product == null)
        this.Product = String.Empty;

      if (this.CookieEnabled == null)
        this.CookieEnabled = String.Empty;

      if (this.Language == null)
        this.Language = String.Empty;

      if (this.Vendor == null)
        this.Vendor = String.Empty;

      if (this.Platform == null)
        this.Platform = String.Empty;

      if (this.UserAgent == null)
        this.UserAgent = String.Empty;

      if (this.InputEncoding == null)
        this.InputEncoding = String.Empty;

      if (this.DefaultCharSet == null)
        this.DefaultCharSet = String.Empty;
    }
  }
}
