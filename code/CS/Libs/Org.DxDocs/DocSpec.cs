using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DxDocs
{
  public enum DxDocType
  {
    NotSet,
    Excel,
    Word,
    Pdf
  }

  public class DocSpec
  {
    public string DocName {
      get;
      set;
    }
    public DxDocType DxDocType {
      get;
      set;
    }
    public string UserName {
      get;
      set;
    }
    public DocParmSet DocParmSet {
      get;
      set;
    }
    public string DocRelativePath {
      get;
      set;
    }
    public string DocAbsolutePath {
      get;
      set;
    }
    public string LogoImageRelativePath {
      get;
      set;
    }
    public string LogoImageAbsolutePath {
      get;
      set;
    }

    public DocSpec()
    {
      this.DocName = String.Empty;
      this.DxDocType = DxDocType.NotSet;
      this.UserName = String.Empty;
      this.DocParmSet = new DocParmSet();
      this.DocRelativePath = String.Empty;
      this.DocAbsolutePath = String.Empty;
      this.LogoImageRelativePath = String.Empty;
      this.LogoImageAbsolutePath = String.Empty;
    }
  }
}
