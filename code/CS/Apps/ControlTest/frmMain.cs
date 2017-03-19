using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using BulkTrans.Trucking.BusinessObjects;
using BulkTrans.Trucking.BusinessObjects.Models;
using Org.UI;
using Org.DB;
using Org.GS;
using Org.GS.Configuration;

namespace Org.ControlTest
{
  public partial class frmMain : Form
  {
    private a a;
    private string _selectedPanel = String.Empty;
    private bool _suppressCboChangeEvent = false;
    private frmConfig fConfig;
    private bool _fConfigIsClosed = false;
    private bool _firstShowing = true; 
    private string _mode = String.Empty;
    private string _dataStoreName = "Org_Software";

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
        case "ShowPanel":
          ShowPanel();
          break;

        case "ShowControlConfig":
          ShowControlConfig();
          break;

        case "TestEF":
          this.TestEF();
          break;

        case "LoadTables":
          this.LoadTables();
          break;

        case "BuildClass":
          this.BuildClass();
          break;

        //case "DBConnection":
        //  this.DBConnection();
        //  break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void ShowPanel()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        string panelToShow = cboPanel.Text;

        if (panelToShow.IsBlank())
        {
          MessageBox.Show("Choose a panel from the drop down.", "ControlTest - Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error); 
          this.Cursor = Cursors.Default;
          return;
        }
        
        _selectedPanel = panelToShow;

        if (ckReloadControlSpec.Checked)
        {
          _suppressCboChangeEvent = true;
          ReloadControlSpecFile();
          SetPanelSelection(_selectedPanel);
          _suppressCboChangeEvent = false; 
        }

        pnlDisplay.Controls.Clear();

        var pnlGrid = new UIPanel();
        pnlGrid.Initialize(panelToShow, new Point(20, 20), GetPanelSize(panelToShow), ckReloadControlSpec.Checked, TruckingRespository.DataAssembly, TruckingRespository.BusinessObjectAssembly);
        pnlGrid.ControlEvent += pnlGrid_ControlEvent;

        pnlDisplay.Controls.Add(pnlGrid);
        pnlGrid.SetFocus();

        fctxtMap.Text = pnlGrid.GetControlMap(pnlGrid.Name); 
        //tabMain.SelectedTab = tabPageDisplay;

        this.Cursor = Cursors.Default; 
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to create the StatePanel." + g.crlf2 + 
                        "Exception:" + g.crlf2 + ex.ToReport(), "ControlTest - Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error); 
        this.Cursor = Cursors.Default;
        return;
      }
    }

    private void ShowControlConfig()
    {
      if (fConfig.Visible)
        fConfig.Hide();
      else
        fConfig.Show();
    }

    private void pnlGrid_ControlEvent(UIEventArgs args)
    {



    }

    private Size GetPanelSize(string panelName)
    {
      switch(panelName)
      {
        case "StatePanel": return new Size(700, 500); 
        case "CityPanel": return new Size(645, 500);
        case "DriverPanel": return new Size(724, 600);
        case "CompanyPanel": return new Size(724, 600);
        case "ProductPanel": return new Size(780, 600);
        case "NavMain": return new Size(180, 600);
      }

      return new Size(700, 500);
    }


    private void InitializeForm()
    {
      try
      {
        a = new a();    
        
        int formVerticalSize = 90;
        int formHorizontalSize = 90;

        if (g.AppConfig.ContainsKey("FormVerticalSize"))
          formVerticalSize = g.GetCI("FormVerticalSize").ToInt32();

        if (g.AppConfig.ContainsKey("FormHorizontalSize"))
          formHorizontalSize = g.GetCI("FormHorizontalSize").ToInt32();

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
  
        _mode = g.CI("Mode");
        
        string controlSpecPath = g.ResourcePath + @"\Controls.xml"; 
        if (!File.Exists(controlSpecPath))
          throw new Exception("Controls.xml does not exist at '" + controlSpecPath + "' - cannot load control definitions.");


        string configText = File.ReadAllText(controlSpecPath); 
        XElement configTextXml = XElement.Parse(configText); 
        string configTextFormattedXml = configTextXml.ToString();
        File.WriteAllText(controlSpecPath, configTextFormattedXml);
        configText = File.ReadAllText(controlSpecPath); 
        
        fConfig = new frmConfig(controlSpecPath);
        fConfig.FormClosed += fConfig_FormClosed;
        fConfig.ConfigFormAction += fConfig_ConfigFormAction;

        _suppressCboChangeEvent = true;

        var controls = GetControlsFromSpec(configText);
        Load_cboPanel(controls);

        string defaultPanel = g.GetCI("DefaultPanel");
        SetPanelSelection(defaultPanel);

        Load_cboModels();

        if (_mode == "Controls")
          ShowPanel();

        _suppressCboChangeEvent = false;
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred while initializing the application." + g.crlf2 + "Exception:" + g.crlf2 + ex.ToReport(), 
                        "Control Test - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void Load_cboModels()
    {
      cboModels.Items.Clear();

      var modelList = g.GetList("Models");
      foreach (var model in modelList)
        cboModels.Items.Add(model); 

    }

    private void fConfig_ConfigFormAction(string action)
    {
      switch(action)
      {
        case "SaveAndDisplay":
          ShowPanel();
          break;
      }
    }

    private void fConfig_FormClosed(object sender, FormClosedEventArgs e)
    {
      _fConfigIsClosed = true;
    }

    private void ReloadControlSpecFile()
    {      
      string controlSpec = fConfig.GetText();

      try
      {
        XElement controlSpecXml = XElement.Parse(controlSpec); 

        string controlSpecPath = g.ResourcePath + @"\Controls.xml"; 
        File.WriteAllText(controlSpecPath, controlSpec); 
        var controls = GetControlsFromSpec(controlSpec);
        Load_cboPanel(controls);
      }
      catch(Exception ex)
      {
        MessageBox.Show("The control specification on the Configuration tab is not valid xml."  + g.crlf2 + "Exception:" + ex.ToReport(), 
            "ControlTest - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return; 
      }
    }

    private List<string> GetControlsFromSpec(string controlSpec)
    {
      List<string> controls = new List<string>();

      XElement controlSpecXml = XElement.Parse(controlSpec);
      IEnumerable<XElement> controlsXml = controlSpecXml.Elements("UIControl"); 
      foreach(var control in controlsXml)
      {
        controls.Add(control.Attribute("Name").Value);
      }

      return controls; 
    }

    private void Load_cboPanel(List<string> controls)
    {
      cboPanel.Items.Clear();
      foreach(var control in controls)
        cboPanel.Items.Add(control); 
    }

    private void SetPanelSelection(string panelSelection)
    {        
      if (panelSelection.IsNotBlank())
      {
        for(int i = 0; i < cboPanel.Items.Count; i++)
        {
          if (cboPanel.Items[i].ToString() == panelSelection)
          {
            cboPanel.SelectedIndex = i;
            break;
          }
        }
      }
    }

    private void cboPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_suppressCboChangeEvent)
        return;

      ShowPanel();
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_firstShowing)
        return;

      _firstShowing = true;

      if (_mode == "Controls")
        fConfig.Show();
    }

    private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (fConfig != null)
      {
        if (!fConfig.IsDisposed)
        {
          if (!_fConfigIsClosed)
            fConfig.Close();
          fConfig.Dispose();
          fConfig = null; 
        }
      }
    }

    private void TestEF()
    {
      this.Cursor = Cursors.WaitCursor;

      switch (cboModels.Text)
      {
        case "City":
          txtReport.Text = GetCities();
          break;

        case "State":
          txtReport.Text = GetStates();
          break;
      }

      tabMain.SelectedTab = tabPageReport;
      this.Cursor = Cursors.Default;
    }

    private string GetCities()
    {
      StringBuilder sb = new StringBuilder();

      var repository = new TruckingRespository("TruckingEntities" + Environment.MachineName, _dataStoreName);
      var cities = repository.Cities;
      
      foreach (var city in cities)
      {
        sb.Append("City Name: " + city.CityName + g.crlf);
      }

      return sb.ToString();
    }

    private string GetStates()
    {
      StringBuilder sb = new StringBuilder();

      var repository = new TruckingRespository("TruckingEntities" + Environment.MachineName, _dataStoreName);
      var states = repository.States;
      foreach (var state in states)
      {
        sb.Append("State Name: " + state.StateName + g.crlf);
      }

      return sb.ToString();
    }

    private void LoadTables()
    {
      ConfigDbSpec dbSpec = g.GetDbSpec("LoeForecasting" + Environment.MachineName); 

      DbHelper.Initialize();
      SqlConnection conn = new SqlConnection(dbSpec.ConnectionString);
      conn.Open();

      DbContext db = new DbContext(dbSpec.ConnectionString);
      
      List<string> tables = new List<string>();
      foreach (DbTable t in DbHelper.DbTableSet.Values)
      {
        if (!tables.Contains(t.TableName))
          tables.Add(t.TableName);
      }

      cboTables.Items.Clear();
      cboTables.Items.Add("All Tables"); 

      cboTables.Items.AddRange(tables.ToArray());

      conn.Close();
      conn = null; 
    }

    private void BuildClass()
    {
      if (cboTables.Text.IsBlank())
      {
        MessageBox.Show("Please select a table.", "ControlTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string code = DbHelper.GenerateBasicClassForTable(DbHelper.DbTableSet, cboTables.Text);
      txtReport.Text = code;
      tabMain.SelectedTab = tabPageReport;
    }

    private void gvTest_SelectionChanged(object sender, EventArgs e)
    {

    }

    private void gvTest_RowLeave(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void txtTest_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
