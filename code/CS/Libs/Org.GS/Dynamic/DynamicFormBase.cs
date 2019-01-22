using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Dyn = Org.GS.Dynamic;

namespace Org.GS.Dynamic
{
  public partial class DynamicFormBase : UserControl
  {
    protected bool IsInitialShowing = true;
    protected bool HasMissingCustomAttributes = false;

    protected Assembly DerivedTypeAssembly {
      get;
      set;
    }
    protected Dyn.DynamicControl DynamicControl {
      get;
      set;
    }

    protected string ModuleName {
      get;
      set;
    }

    public DynamicFormBase()
    {
      InitializeComponent();
    }

    public DynamicFormBase(Assembly derivedTypeAssembly)
    {
      InitializeComponent();
      this.DerivedTypeAssembly = derivedTypeAssembly;

      SetModuleName();

      this.DynamicControl = new Dyn.DynamicControl();
      InitializeBase();
    }

    private void InitializeBase()
    {
      if (!g.AppConfig.ProgramConfigSet.ContainsKey(g.AppInfo.ConfigName))
      {
        MessageBox.Show("No ProgramConfig for '" + g.AppInfo.ConfigName + "'.", g.AppInfo.ConfigName + " - Startup Error");
        throw new Exception("No ProgramConfig for '" + g.AppInfo.ConfigName + "'.");
      }

      LogContext.EstablishContext();

      this.DynamicControl = g.AppConfig.ProgramConfigSet[g.AppInfo.ConfigName].DynamicControl;

      LoadDynamicControl(this.DynamicControl);

      if (g.AppConfig.GetBoolean("UseNotifyIcon"))
      {
        notifyIconMain.Icon = SystemIcons.Application;
        notifyIconMain.BalloonTipText = "Notification from DynamicFormBase.";
        notifyIconMain.ShowBalloonTip(1000);
      }

      if (g.IsInVisualStudioDesigner)
      {
      }


      this.mnuMain.BringToFront();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      lblStatus.Text = action;

      switch (action)
      {
        case "NOTIFY_ICON":
          MessageBox.Show("Notify icon clicked.", "Dynamic Form Base");
          break;

        default:
          this.DoAction(action);
          break;
      }
    }

    public virtual void DoAction(string parm)
    {
    }

    private void SetModuleName()
    {
      if (g.ModuleAssembly == null)
        g.ModuleAssembly = AssemblyHelper.GetModuleAssembly();

      if (g.ModuleAssembly == null)
        throw new Exception("The module must use the custom attribute 'OrgModuleAssembly' in AssemblyInfo.cs " +
                            "to mark the top level assembly of the module so it can be located in the AppDomain. " +
                            "No assembly has been located in the AppDomain with the 'OrgModuleAssembly' attribute.");

      OrgConfigName configNameAttribute = (OrgConfigName)g.ModuleAssembly.GetCustomAttributes(typeof(OrgConfigName), false).ToList().FirstOrDefault();
      if (configNameAttribute == null)
        this.HasMissingCustomAttributes = true;
      else
      {
        if (configNameAttribute.Value.IsNotBlank())
          g.AppInfo.ConfigName = configNameAttribute.Value.Trim();
      }

      OrgModuleName moduleNameAttribute = (OrgModuleName)g.ModuleAssembly.GetCustomAttributes(typeof(OrgModuleName), false).ToList().FirstOrDefault();
      if (moduleNameAttribute == null)
        this.HasMissingCustomAttributes = true;
      else
      {
        if (moduleNameAttribute.Value.IsNotBlank())
        {
          this.ModuleName = moduleNameAttribute.Value.Trim();
          g.AppInfo.ModuleName = this.ModuleName;
        }
      }

      OrgModuleCode moduleCodeAttribute = (OrgModuleCode)g.ModuleAssembly.GetCustomAttributes(typeof(OrgModuleCode), false).ToList().FirstOrDefault();
      if (moduleCodeAttribute == null)
        this.HasMissingCustomAttributes = true;
      else
      {
        g.AppInfo.ModuleCode = moduleCodeAttribute.Value;
      }
    }

    private void LoadDynamicControl(Dyn.DynamicControl dynamicControl)
    {
      Dyn.ControlSet controlSet = dynamicControl.ControlSet;
      Dyn.Control mainMenuControl = controlSet.GetMainMenuControl();
      if (mainMenuControl != null)
      {
        this.LoadMainMenuControls(mainMenuControl, mnuMain);
      }
    }

    private void LoadMainMenuControls(Dyn.Control dc, object menuItem)
    {
      string controlType = menuItem.GetType().Name;

      switch (controlType)
      {
        case "MenuStrip":
          MenuStrip menuStrip = (MenuStrip)menuItem;
          menuStrip.Items.Clear();

          foreach (Dyn.Control dcChild in dc.ControlSet)
          {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Name = dcChild.GetControlName();
            tsmi.Text = dcChild.Text;
            if (dcChild.MapEvent)
            {
              tsmi.Tag = dcChild.GetTag();
              tsmi.Click += this.Action;
            }
            menuStrip.Items.Add(tsmi);
            foreach (Dyn.Control dcGrandChild in dcChild.ControlSet)
            {
              LoadMainMenuControls(dcGrandChild, tsmi);
            }
          }
          break;

        default:
          ToolStripMenuItem tsmi2 = (ToolStripMenuItem)menuItem;
          ToolStripMenuItem tsmiChild = new ToolStripMenuItem();
          tsmiChild.Name = dc.GetControlName();
          tsmiChild.Text = dc.Text;
          if (dc.MapEvent)
          {
            tsmiChild.Tag = dc.GetTag();
            tsmiChild.Click += this.Action;
          }
          tsmi2.DropDownItems.Add(tsmiChild);

          if (dc.ControlSet != null)
          {
            foreach (Dyn.Control dcGrandChild in dc.ControlSet)
            {
              LoadMainMenuControls(dcGrandChild, tsmiChild);
            }
          }
          break;
      }
    }

    public void ArrangeControls()
    {
      if (this.Controls.ContainsKey("pnlBackmost"))
      {
        ((Panel)this.Controls["pnlBackmost"]).BringToFront();
        ((Panel)this.Controls["pnlBackmost"]).BorderStyle = System.Windows.Forms.BorderStyle.None;
      }

      if (this.Controls.ContainsKey("mnuMain"))
      {
        //if (g.IsInVisualStudioDesigner)
        //    this.Controls["mnuMain"].Visible = false;
      }
    }


    public string GetAppPath()
    {
      string exePath = Process.GetCurrentProcess().MainModule.FileName;
      string startupDirectory = Path.GetDirectoryName(exePath);

      List<string> pathParts = new List<string>();

      if (startupDirectory.EndsWith(@"bin\Debug") || startupDirectory.EndsWith(@"bin\Release"))
      {
        pathParts = startupDirectory.Split(@"\".ToCharArray()).ToList();
        pathParts.RemoveRange(pathParts.Count - 2, 2);
        return string.Join(@"\", pathParts.ToArray());
      }

      return startupDirectory;
    }
  }
}
