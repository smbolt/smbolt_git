using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.Dx.Business;

namespace NodeQueryWorkbench
{
  public partial class frmMain : Form
  {
    private bool _isFirstShowing = true;

    private Dictionary<string, string> _dataFiles;
    private Dictionary<string, string> _queryFiles;
    private SortedList<int, DxCell> _cells;

    private NodeQuery _nodeQuery;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "Step":
          Step();
          break;

        case "Run":
          Run();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Step()
    {
      txtQueryResults.Text = "Step";
    }


    private void Run()
    {
      try
      {
        ParseData();

        var mapItem = new DxMapItem();
        mapItem.Name = "DummyMapItem";
        mapItem.Src = String.Empty;
        mapItem.Dest = String.Empty;

        _nodeQuery = new NodeQuery(mapItem, txtQuery.Text.Replace("nq:", String.Empty), _cells);

        //_nodeQuery.InitializeExecution();

        var resultCell = _nodeQuery.ProcessQuery();

        txtDataStructure.Text = _nodeQuery.Report;

        if (resultCell != null)
          txtQueryResults.Text = resultCell.TextValue;
        else
          txtQueryResults.Text = "DxCell is null.";
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to execute the query." + g.crlf2 + ex.ToReport(),
                        "NodeQueryWorkbench - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ParseData()
    {
      try
      {
        _cells = new SortedList<int, DxCell>();

        string[] lines = txtRawData.Text.Trim().Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

        int r = 0;
        int c = 0;

        foreach (var line in lines)
        {
          string[] tokens = line.Trim().Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
          foreach (var token in tokens)
          {
            var dxCell = new DxCell();
            dxCell.RowIndex = r;
            dxCell.ColumnIndex = c;
            dxCell.RawValue = token;

            if (c == 0)
              dxCell.IsFirstInSequence = true;

            _cells.Add(_cells.Count, dxCell);

            c++;
          }

          if (_cells.Count > 0)
            _cells.Values.Last().IsLastInSequence = true;

          r++;
          c = 0;
        }

        if (_cells.Count > 0)
        {
          _cells.Values.First().IsFirstInSet = true;
          _cells.Values.Last().IsLastInSet = true;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to parse the data." + g.crlf2 + ex.ToReport(),
                        "NodeQueryWorkbench - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
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
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "NodeQueryWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        this.SetInitialSizeAndLocation();

        RefreshFileLists();

        if (cboDataFiles.Items.Count > 0)
          cboDataFiles.SelectedIndex = 0;

        if (cboQueryFiles.Items.Count > 0)
          cboQueryFiles.SelectedIndex = 0;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "NodeQueryWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void RefreshFileLists()
    {
      _dataFiles = new Dictionary<string, string>();
      cboDataFiles.Items.Clear();
      var dataFileList = Directory.GetFiles(g.ImportsPath + @"\DataFiles").ToList();

      foreach (var dataFile in dataFileList)
      {
        string fileName = Path.GetFileName(dataFile);
        cboDataFiles.Items.Add(fileName);
        _dataFiles.Add(fileName, dataFile);
      }
           

      _queryFiles = new Dictionary<string, string>();
      cboQueryFiles.Items.Clear();
      var queryFileList = Directory.GetFiles(g.ImportsPath + @"\QueryFiles").ToList();

      foreach (var queryFile in queryFileList)
      {
        string fileName = Path.GetFileName(queryFile);
        cboQueryFiles.Items.Add(fileName);
        _queryFiles.Add(fileName, queryFile);
      }
    }

    private void cboDataFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboDataFiles.Text.IsBlank())
      {
        txtRawData.Text = String.Empty;
      }
      else
      {
        txtRawData.Text = File.ReadAllText(_dataFiles[cboDataFiles.Text]);
        txtRawData.SelectionStart = 0;
        txtRawData.SelectionLength = 0;
      }
    }

    private void cboQueryFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboQueryFiles.Text.IsBlank())
      {
        txtQuery.Text = String.Empty;
      }
      else
      {
        txtQuery.Text = File.ReadAllText(_queryFiles[cboQueryFiles.Text]);
        txtQuery.SelectionStart = 0;
        txtQuery.SelectionLength = 0;
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      splitterMain.SplitterDistance = (splitterMain.Height / 3) * 2;

      _isFirstShowing = false;
    }
  }
}
