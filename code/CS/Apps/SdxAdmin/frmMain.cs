using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Sdx.Business;
using Org.GS.Configuration;
using Org.GS;

namespace Org.SdxAdmin
{
  public partial class frmMain : Form
  {
    private Mode _mode = Mode.Design;
    private string _env = "Test";
    private Dictionary<string, string> _dbServers;
    private ConfigDbSpec _configDbSpec;

    private bool addColumnInProgress = false;
    private bool cellEditInProgress = false;



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
        case "Refresh":
          LoadTreeView();
          break;

        case "AddSolution":
          AddSolution();
          break;

        case "UpdateSolution":
          UpdateSolution();
          break;

        case "DeleteSolution":
          DeleteSolution();
          break;

        case "AddLogicalTable":
          AddLogicalTable();
          break;

        case "UpdateLogicalTable":
          UpdateLogicalTable();
          break;

        case "DeleteLogicalTable":
          DeleteLogicalTable();
          break;

        case "AddColumn":
          AddColumn();
          break;

        case "UpdateColumn":
          UpdateColumn();
          break;

        case "DeleteColumn":
          DeleteColumn();
          break;

        case "ReorderColumns":
          ReorderColumns();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void ChangeMode(Mode mode)
    {
      _mode = mode;

      LoadTreeView();

      tabMain.SelectedTab = tabMain.TabPages[_mode.ToInt32()];      
    }

    private void AddSolution()
    {
      try
      {
        lblStatus.Text = "Add Solution";

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Add a Solution." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Adding Solution", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void UpdateSolution()
    {
      try
      {
        lblStatus.Text = "Update Solution";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Update a Solution." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Updating Solution", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DeleteSolution()
    {
      try
      {
        lblStatus.Text = "Delete Solution";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Delete a Solution." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Deleting Solution", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AddLogicalTable()
    {
      try
      {
        lblStatus.Text = "Add LogicalTable";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Add a LogicalTable." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Adding LogicalTable", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void UpdateLogicalTable()
    {
      try
      {
        lblStatus.Text = "Update LogicalTable";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Update a LogicalTable." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Updating LogicalTable", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DeleteLogicalTable()
    {
      try
      {
        lblStatus.Text = "Delete LogicalTable";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Delete a LogicalTable." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Deleting LogicalTable", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AddColumn()
    {
      try
      {
        gvColumns.Rows.Add();
        gvColumns.CurrentCell = gvColumns.Rows[gvColumns.Rows.Count - 1].Cells[0];
        gvColumns.BeginEdit(true);


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Add a Column." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Adding Column", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void UpdateColumn()
    {
      try
      {
        lblStatus.Text = "Update Column";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Update a Column." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Updating Column", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DeleteColumn()
    {
      try
      {
        lblStatus.Text = "Delete Column";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Delete a Column." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Deleting Column", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ReorderColumns()
    {
      try
      {
        lblStatus.Text = "Reorder Columns";


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to Reorder the columns." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Reordering Columns", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView()
    {
      try
      {
        var solutionSet = GetSolutionSet();

        tvSolutions.Nodes.Clear();

        var rootNode = new TreeNode(_env, 0, 0);
        rootNode.Tag = new SdxEnvironment() { Name = _env };
        tvSolutions.Nodes.Add(rootNode);

        foreach (var solution in solutionSet.Values)
        {
          var solutionNode = new TreeNode(solution.Name, 1, 1);
          solutionNode.Tag = solution;
          rootNode.Nodes.Add(solutionNode);

          if (_mode == Mode.Design)
          {
            foreach (var logicalTable in solution.LogicalTableSet.Values)
            {
              var logicalTableNode = new TreeNode(logicalTable.Name, 2, 2);
              logicalTableNode.Tag = logicalTable;
              solutionNode.Nodes.Add(logicalTableNode);

              foreach (var column in logicalTable.ColumnSet.Values)
              {
                var columnNode = new TreeNode(column.Name, 3, 3);
                columnNode.Tag = column;
                logicalTableNode.Nodes.Add(columnNode);
              }
            }
          }
        }

        tvSolutions.ExpandAll();
        tvSolutions.SelectedNode = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to load the Solution Tree View." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Loading Solution Tree View", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ProcessSelection(SdxBase sdxObject)
    {
      try
      {
        switch (_mode)
        {
          case Mode.Design:
            switch (sdxObject.SdxObjectType)
            {
              case SdxObjectType.LogicalTable:
                LoadColumnsToGrid((LogicalTable)sdxObject);
                break;

            }
            break;
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to respond to a Solution Tree View selection change." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Processing Selection Change", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadColumnsToGrid(LogicalTable logicalTable)
    {
      gvColumns.Rows.Clear();

      foreach (var column in logicalTable.ColumnSet.Values)
      {
        gvColumns.Rows.Add(new object[] {
          column.Name,
          column.TargetColumnName,
          column.Ordinal.ToString(),
          column.SqlDataType,
          column.IsNullable,
          column.DefaultValue,
          column.ColumnId,
        });
      }

      gvColumns.ClearSelection();
    }

    private void NothingSelected()
    {

    }

    private SolutionSet GetSolutionSet()
    {
      using (var repo = new SdxRepository(_configDbSpec))
      {
        return repo.GetSolutionSet();
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object (a)." + ex.ToReport(),
                        "SDX Admin - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

        splitterMain.SplitterDistance = 250;

        InitializeTreeViewImageList();
        InitializeColumnGrid();


        _dbServers = g.GetDictionary("DbServers");
        _configDbSpec = g.GetDbSpec("Sdx");

        cboMode.LoadItems(g.GetList("Modes"));
        cboMode.SelectItem(g.CI("SelectedMode"));

        cboEnvironment.LoadItems(g.GetList("Environments"));
        cboEnvironment.SelectItem(g.CI("SelectedEnvironment"));
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + ex.ToReport(),
                        g.AppInfo.AppName + " - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeTreeViewImageList()
    {
      imgListTreeView.Images.Clear();
      imgListTreeView.ImageSize = new Size(16, 16);

      var resourceManager = new ResourceManager("Org.SdxAdmin.Properties.Resources", Assembly.GetExecutingAssembly());
      var environment = (Image)resourceManager.GetObject("environment");
      imgListTreeView.Images.Add("environment", environment);
      var solution = (Image)resourceManager.GetObject("solution");
      imgListTreeView.Images.Add("solution", solution);
      var table = (Image)resourceManager.GetObject("table");
      imgListTreeView.Images.Add("table", table);
      var column = (Image)resourceManager.GetObject("column");
      imgListTreeView.Images.Add("column", column);
    }

    private void InitializeColumnGrid()
    {
      gvColumns.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "ColumnName";
      col.HeaderText = "Column Name";
      col.Width = 180;
      gvColumns.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "TargetColumnName";
      col.HeaderText = "Target Column Name";
      col.Width = 180;
      gvColumns.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "Ordinal";
      col.HeaderText = "Ordinal";
      col.Width = 60;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvColumns.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "SqlDataType";
      col.HeaderText = "SQL Data Type";
      col.Width = 150;
      gvColumns.Columns.Add(col);

      col = new DataGridViewCheckBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "IsNullable";
      col.HeaderText = "Nullable";
      col.Width = 60;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvColumns.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "DefaultValue";
      col.HeaderText = "DefaultValue";
      col.Width = 150;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvColumns.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      col.Name = "ColumnID";
      col.HeaderText = String.Empty;
      col.Width = 0;
      col.Visible = false;
      gvColumns.Columns.Add(col);

    }

    private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      _env = cboEnvironment.Text;

      if (_env == "Prod")
      {
        MessageBox.Show("The database is not yet implemented in production - resetting selection back to 'Test'.",
                        g.AppInfo.AppName + " - No Production Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        cboEnvironment.SelectItem("Prod");
      }

      _configDbSpec.DbServer = _dbServers[_env];
      LoadTreeView();
    }

    private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      var mode = g.ToEnum<Mode>(cboMode.Text, Mode.Design);

      if (_mode != mode)
      {
        ChangeMode(mode);
      }
    }

    private void ctxMenuSolution_Opening(object sender, CancelEventArgs e)
    {
      if (tvSolutions.SelectedNode == null)
      {
        e.Cancel = true;
        return;
      }

      var sdxObject = tvSolutions.SelectedNode.Tag as SdxBase;

      if (sdxObject == null)
      {
        e.Cancel = true;
        return;
      }

      foreach (ToolStripMenuItem menuItem in ctxMenuSolution.Items)
      {
        string tag = menuItem.Tag.ObjectToTrimmedString();
        
        switch (sdxObject.SdxObjectType)
        {
          case SdxObjectType.Environment:
            menuItem.Visible = tag.In("Refresh,AddSolution");
            break;

          case SdxObjectType.Solution:
            menuItem.Visible = tag != "AddSolution" && (
                                tag.Contains("Solution") || tag == "AddLogicalTable"
                                );

            break;

          case SdxObjectType.LogicalTable:
            menuItem.Visible = tag != "AddLogicalTable" && (
                                tag.Contains("LogicalTable") || tag == "AddColumn"
                                );
            break;

          case SdxObjectType.Column:
            menuItem.Visible = tag != "AddColumn" && (
                                tag.Contains("Column")
                                );
            break;
        }
      }

    }

    private void tvSolutions_MouseDown(object sender, MouseEventArgs e)
    {
      var hitTestInfo = tvSolutions.HitTest(e.X, e.Y);
      if (hitTestInfo.Node != null)
        tvSolutions.SelectedNode = hitTestInfo.Node;
    }

    private void gvColumns_SelectionChanged(object sender, EventArgs e)
    {
      if (gvColumns.SelectedRows.Count == 0)
      {

        return;
      }

    }

    private void tvSolutions_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (tvSolutions.SelectedNode == null)
      {
        NothingSelected();
        return;
      }

      var sdxObject = tvSolutions.SelectedNode.Tag as SdxBase;

      if (sdxObject == null)
        return;

      ProcessSelection(sdxObject);
    }

    private void ctxMenuGrid_Opening(object sender, CancelEventArgs e)
    {
      if (gvColumns.SelectedRows.Count == 0)
      {
        ctxMenuGridAddColumn.Visible = true;
        ctxMenuGridReorderColumns.Visible = true;
        ctxMenuGridDeleteColumn.Visible = false;
        ctxMenuGridUpdateColumn.Visible = false;
      }
      else
      {
        ctxMenuGridAddColumn.Visible = true;
        ctxMenuGridReorderColumns.Visible = true;
        ctxMenuGridDeleteColumn.Visible = true;
        ctxMenuGridUpdateColumn.Visible = true;
      }
    }

    private void gvColumns_CellClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void gvColumns_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex == -1)
      {
        gvColumns.ClearSelection();
      }
      else
      {
        gvColumns.Rows[e.RowIndex].Selected = true;
      }
    }

    private void gvColumns_MouseDown(object sender, MouseEventArgs e)
    {
      var hitTest = gvColumns.HitTest(e.X, e.Y);
      if (hitTest.RowIndex == -1)
        gvColumns.ClearSelection();
    }

    private void gvColumns_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {

    }

    private void gvColumns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
