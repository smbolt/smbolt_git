using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "FSItem")]
  public class FSAction : Dictionary<string, FSItem>
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap]
    public FileSystemCommand FileSystemCommand {
      get;
      set;
    }

    [XMap]
    public string Src {
      get;
      set;
    }
    public string FullSourcePath {
      get {
        return Get_FullSourcePath();
      }
    }

    [XMap]
    public string Dst {
      get;
      set;
    }
    public string FullDestPath {
      get {
        return Get_FullDestPath();
      }
    }

    public string FullActionName {
      get {
        return Get_FullActionName();
      }
    }

    [XMap(DefaultValue = "")]
    public string Options {
      get;
      set;
    }
    public bool ClearDirectory {
      get {
        return OptionsContains("clear");
      }
    }
    public bool Overwrite {
      get {
        return OptionsContains("overwrite");
      }
    }

    [XMap(DefaultValue = "")]
    public string ClearPattern {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap (DefaultValue="Ignore")]
    public FailureAction FailureAction {
      get;
      set;
    }

    public FSActionGroup FSActionGroup {
      get;
      set;
    }
    public FSActionSet FSActionSet {
      get;
      set;
    }

    public bool DestFolderRequired {
      get {
        return Get_DestFolderRequired();
      }
    }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public FSAction(FSActionGroup parent)
    {
      this.Name = String.Empty;
      this.FileSystemCommand = FileSystemCommand.NotSet;
      this.Src = String.Empty;
      this.Dst = String.Empty;
      this.Options = String.Empty;
      this.ClearPattern = String.Empty;
      this.IsActive = true;
      this.FailureAction = FailureAction.Ignore;
      this.FSActionGroup = parent;
    }

    private string Get_FullSourcePath()
    {
      return ComposePath(this.FSActionSet.Src, this.FSActionGroup.Src, this.Src);
    }

    private string Get_FullDestPath()
    {
      return ComposePath(this.FSActionSet.Dst, this.FSActionGroup.Dst, this.Dst);
    }

    private string ComposePath(string setSrc, string groupSrc, string actionSrc)
    {
      if (this.FSActionSet.UseActionSetVariables)
      {
        setSrc = this.FSActionSet.ResolveVariables(setSrc).Trim();
        groupSrc = this.FSActionSet.ResolveVariables(groupSrc).Trim();
        actionSrc = this.FSActionSet.ResolveVariables(actionSrc).Trim();
      }
      else
      { // resolve variables from AppConfig
        setSrc = g.AppConfig.ResolveVariables(setSrc).Trim();
        groupSrc = g.AppConfig.ResolveVariables(groupSrc).Trim();
        actionSrc = g.AppConfig.ResolveVariables(actionSrc).Trim();
      }

      setSrc = setSrc.Replace("/", @"\");
      groupSrc = groupSrc.Replace("/", @"\");
      actionSrc = actionSrc.Replace("/", @"\");

      string path = setSrc;
      if (path.IsNotBlank())
        path.EnsureSingleTrailingSlash();

      if (!groupSrc.StartsWith(@"\\"))
      {
        while (groupSrc.Length > 0 && (groupSrc.StartsWith("~") || groupSrc.StartsWith(@"\")))
          groupSrc = groupSrc.Substring(1);
      }

      if (groupSrc.IsNotBlank())
        groupSrc = groupSrc.EnsureSingleTrailingSlash();

      path = (path += groupSrc).EnsureSingleTrailingSlash();

      if (!actionSrc.StartsWith(@"\\"))
      {
        while (actionSrc.Length > 0 && (actionSrc.StartsWith("~") || actionSrc.StartsWith(@"\")))
          actionSrc = actionSrc.Substring(1);
      }

      if (actionSrc.IsNotBlank())
        actionSrc = actionSrc.EnsureSingleTrailingSlash();

      path = (path += actionSrc).EnsureSingleTrailingSlash();

      return path;
    }

    private string Get_FullActionName()
    {
      return this.FSActionGroup.Name.Trim() + "." + this.Name.Trim();
    }

    private bool OptionsContains(string option)
    {
      if (this.Options == null)
        this.Options = String.Empty;

      return this.Options.ToLower().Contains(option.ToLower());
    }

    private bool Get_DestFolderRequired()
    {
      switch (this.FileSystemCommand)
      {
        case FileSystemCommand.Copy:
        case FileSystemCommand.Move:
          return true;
      }

      return false;
    }
  }
}
