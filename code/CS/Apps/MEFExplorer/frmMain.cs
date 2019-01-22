using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.ReflectionModel;
using System.ComponentModel.Composition.Diagnostics;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.MEF;
using Org.MOD;
using Org.MOD.Contracts;
using Org.GS;

namespace Org.MEFExplorer
{
  public partial class frmMain : Form
  {
    private a a;
    private FileSystemUtility _fsu;
    private static CompositionContainer _container;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Browse":
          BrowseFolders();
          break;

        case "ExploreMEF":
          ExploreMEF();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void BrowseFolders()
    {
      string defaultPath = g.CI("DefaultFolderPath");
      string lastUsedPath = g.CI("LastUsedFolderPath");
      string path = lastUsedPath;
      if (path.IsBlank())
        path = defaultPath;

      folderDialog.SelectedPath = path;

      if (folderDialog.ShowDialog() == DialogResult.OK)
      {
        string selectedPath = folderDialog.SelectedPath;
        g.AppConfig.UpdateCI("MEFExplorer", "Options", "LastUsedFolderPath", selectedPath);
        g.AppConfig.Save();
        LoadFolders(folderDialog.SelectedPath);
      }
    }

    private void LoadFolders(string selectedPath)
    {
      cboFolders.Items.Clear();
      cboFolders.Items.Add(selectedPath);
      List<string> subDirectories = Directory.GetDirectories(selectedPath).ToList();
      foreach (string subDirectory in subDirectories)
        cboFolders.Items.Add(subDirectory);  
    }

    private void ExploreMEF()
    {
      this.Cursor = Cursors.WaitCursor;

      var moduleSet = new ModuleOnDiskSet(cboFolders.Text, String.Empty);


      AggregateCatalog catalog = new AggregateCatalog();

      foreach (var moduleOnDisk in moduleSet.Values)
      {
        DirectoryCatalog directoryCatalog = new DirectoryCatalog(moduleOnDisk.ModuleFolder);

        catalog.Catalogs.Add(directoryCatalog);
      }

      _container = new CompositionContainer(catalog);

      try
      {
        _container.ComposeParts(this); 
      }
      catch (CompositionException ex)
      {
        MessageBox.Show("An exception occurred attempting to compose MEF catalog." + g.crlf2 + ex.ToReport(), "MEFExplorer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      StringBuilder sb = new StringBuilder();

      sb.Append("MEF Catalog - Total Items = " + _container.Catalog.Count().ToString() + g.crlf);

      int partCount = 0;

      foreach (var part in _container.Catalog)
      {
        partCount++;

        string partName = part.ToString();
        int exportDefCount = part.ExportDefinitions.Count();
        sb.Append(g.crlf + partCount.ToString("00") + "  " + partName + g.crlf);
        int exportDefNbr = 0;

        foreach (var exportDef in part.ExportDefinitions)
        {
          exportDefNbr++;
          var exportingMemberInfo = ReflectionModelServices.GetExportingMember(exportDef);
          string exportDefName = exportDef.ToString();
          sb.Append(g.crlf + "    Export Def " + exportDefNbr.ToString() + "  " + exportDefName + g.crlf);
          sb.Append("    Contract Name : " + exportDef.ContractName + g.crlf); 

          var accessors = exportingMemberInfo.GetAccessors();
          int accessorsCount = accessors.Count();
          sb.Append("    Accessor Count " + accessorsCount.ToString() + g.crlf);
          int accessorNbr = 0;

          foreach (var accessor in accessors)
          {
            accessorNbr++;
            string accessorName = accessor.Name;
            sb.Append("    Accessor " + accessorNbr.ToString() + "  " + accessorName + g.crlf); 

            var module = accessor.Module;
            string fullyQualifiedName = module.FullyQualifiedName;
            var assembly = module.Assembly;
            string assemblyFullName = assembly.FullName;
            string assemblyLocation = assembly.Location;

            sb.Append("    Assembly: " + assemblyFullName + g.crlf);
            sb.Append("    Location: " + assemblyLocation + g.crlf);
          }

          sb.Append("    Meta Data: " + g.crlf);
          foreach (var metaData in exportDef.Metadata)
          {
            sb.Append("        Key: " + metaData.Key.PadTo(25) + "    Value: " + metaData.Value.ToString() + g.crlf);
          }
        }

        sb.Append(g.crlf); 
      }


      string report = sb.ToString();


      txtOut.Text = report;

      this.Cursor = Cursors.Default;
    }


    private void InitializeForm()
    {
      try
      {
        a = new a();
        _fsu = new FileSystemUtility();

        var fsActionSet = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].FSActionSet;

        using (var fsEngine = new FSEngine())
        {
          var taskResult = fsEngine.Run(fsActionSet);
          if (taskResult.TaskResultStatus != TaskResultStatus.Success)
          {
            MessageBox.Show("An error occurred attempting to migrate the MEF Modules." + g.crlf2 + taskResult.Message,
                            "RP Test - Module Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }

        string defaultPath = g.CI("DefaultFolderPath");
        string lastUsedPath = g.CI("LastUsedFolderPath");
        string path = lastUsedPath;
        if (path.IsBlank())
          path = defaultPath;

        LoadFolders(path); 
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(), "MEF Explorer - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


    }

    private void cboFolders_SelectedIndexChanged(object sender, EventArgs e)
    {
      var moduleSetType = _fsu.GetModuleSetType(cboFolders.Text);

      switch (moduleSetType)
      {
        case ModuleOnDiskSetType.NotModuleSet:
          ckUseModuleSet.Checked = false;
          ckTwoLevel.Checked = false;
          break;

        case ModuleOnDiskSetType.OneLevel:
          ckUseModuleSet.Checked = true;
          ckTwoLevel.Checked = false;
          break;

        case ModuleOnDiskSetType.TwoLevel:
          ckUseModuleSet.Checked = true;
          ckTwoLevel.Checked = true;
          break;
      }
    }

  }
}
