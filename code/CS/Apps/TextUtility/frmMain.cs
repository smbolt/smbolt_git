using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FastColoredTextBoxNS;
using System.Windows.Forms;
using Org.GS.Configuration;
using Org.GS;

namespace Org.TextUtility
{
  public enum ShowOptions
  {
    ShowAllMismatches,
    ShowLeftMismatches,
    ShowRightMismatches,
    ShowMatches
  }

  public partial class frmMain : Form
  {
    private ConfigDbSpec _testDbSpec;
    private ConfigDbSpec _prodDbSpec;
    private ConfigDbSpec _selectedDbSpec;

    private Color _leftColor;
    private Color _rightColor;
    private Style _leftStyle;
    private Style _rightStyle;

    private WellSet _wellSet;

    protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();


    TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
    TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
    TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
    TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
    TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
    TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
    TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
    MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

    private Style StringStyle { get; set; }
    private Style CommentStyle { get; set; }
    private Style NumberStyle { get; set; }
    private Style KeywordStyle { get; set; }


    protected Regex JScriptCommentRegex1,
                  JScriptCommentRegex2,
                  JScriptCommentRegex3;

    protected Regex JScriptKeywordRegex;
    protected Regex JScriptNumberRegex;
    protected Regex JScriptStringRegex;


    public frmMain()
    {
      InitializeComponent();

      InitializeForm();
    }



    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "LoadBaseText":
          LoadBaseText();
          break;

        case "SaveBaseText":
          SaveBaseText();
          break;

        case "SaveBaseTextAs":
          SaveBaseTextAs();
          break;        

        case "ClearBaseText":
          txtBaseText.Text = String.Empty;
          lblBaseTextLines.Text = "0 lines";
          break;

        case "CompareText":
          CompareText();
          break;

        case "ClearCompareText":
          txtCompareText.Text = String.Empty;
          lblCompareTextLines.Text = "0 lines";
          break;

        case "LoadQuery":
          LoadQuery();
          break;

        case "SaveQuery":
          SaveQuery();
          break;

        case "SaveQueryAs":
          SaveQueryAs();
          break;  

        case "RunQuery":
          LoadWells();
          CompareText();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void LoadBaseText()
    {
      try
      {
        txtBaseText.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(100);

        int lineCount = 0;
        var sb = new StringBuilder();
        var baseLines = File.ReadAllText(g.ImportsPath + @"\BaseData\" + cboBaseData.Text).Trim().ToLineList();

        foreach (var baseLine in baseLines)
        {
          lineCount++;
          sb.Append(baseLine + g.crlf);
        }

        txtBaseText.Text = sb.ToString().Trim();
        txtBaseText.SelectionStart = 0;
        txtBaseText.SelectionLength = 0;
        lblBaseTextLines.Text = lineCount.ToString("###,##0") + " lines";
        txtBaseText.GoHome();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to load the base data file." + g.crlf2 + ex.ToReport(),
                "Text Utility - File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SaveBaseText()
    {
      File.WriteAllText(g.ImportsPath + @"\BaseData\" + cboBaseData.Text, txtBaseText.Text);
    }

    private void SaveBaseTextAs()
    {
      using (var fSaveAs = new frmSaveAs(g.ImportsPath + @"\BaseText\" + cboBaseData.Text, g.ImportsPath + @"\BaseText"))
      {
        if (fSaveAs.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllText(fSaveAs.NewFilePath, txtBaseText.Text);
          LoadBaseTextComboBox(Path.GetFileName(fSaveAs.NewFilePath));
        }
      }
    }

    private void SaveQuery()
    {
      File.WriteAllText(g.ImportsPath + @"\Queries\" + cboQuery.Text, txtQuery.Text);
    }

    private void SaveQueryAs()
    {
      using (var fSaveAs = new frmSaveAs(g.ImportsPath + @"\Queries\" + cboQuery.Text, g.ImportsPath + @"\Queries"))
      {
        if (fSaveAs.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllText(fSaveAs.NewFilePath, txtQuery.Text);
          LoadQueriesComboBox(Path.GetFileName(fSaveAs.NewFilePath));
        }
      }
    }

    private void LoadQuery()
    {
      try
      {
        txtQuery.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(100);

        txtQuery.Text = File.ReadAllText(g.ImportsPath + @"\Queries\" + cboQuery.Text).Trim();
        txtQuery.SelectionStart = 0;
        txtQuery.SelectionLength = 0;
        txtQuery.GoHome();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to load the base data file." + g.crlf2 + ex.ToReport(),
                "Text Utility - File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadWells()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _wellSet = GetWells();
        txtCompareText.Text = _wellSet.WellNameReport;
        lblCompareTextLines.Text = _wellSet.Count.ToString("###,##0") + " lines";
        LoadWellGrid();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to retrieve wells from the database." + g.crlf2 + ex.ToReport(),
                       "Text Utility - Well Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

      }
    }

    private WellSet GetWells()
    {
      try
      {
        var wellSet = new WellSet();
        var piSet = typeof(Well).GetProperties();

        using (var conn = new SqlConnection(_selectedDbSpec.ConnectionString))
        {
          conn.Open();

          string sql = txtQuery.Text;

          using (var cmd = new SqlCommand(sql, conn))
          {
            cmd.CommandType = System.Data.CommandType.Text;
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);

            var dt = ds.Tables[0];

            foreach (DataRow r in dt.Rows)
            {
              var well = new Well();

              foreach (var pi in piSet)
              {
                string columnName = pi.Name;

                if (dt.Columns.Contains(columnName))
                {
                  var col = r[columnName];

                  if (col != DBNull.Value)
                  {
                    switch (pi.PropertyType.Name)
                    {
                      case "String":
                        pi.SetValue(well, col.DbToString());
                        break;

                      case "Int32":
                        pi.SetValue(well, col.DbToInt32().Value);
                        break;

                      default:
                        throw new Exception("The data type '" + pi.PropertyType.Name + "' of well property '" + pi.Name + "' " +
                                            "is not currently supported.");
                    }
                  }
                }
              }

              wellSet.Add(well);
            }
          }

          conn.Close();

          return wellSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retreive wells.", ex);
      }
    }


    private string RunQuery()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        var sb = new StringBuilder();

        using (var conn = new SqlConnection(_selectedDbSpec.ConnectionString))
        {
          int itemCount = 0;

          conn.Open();

          string sql = txtQuery.Text;

          using (var cmd = new SqlCommand(sql, conn))
          {
            cmd.CommandType = System.Data.CommandType.Text;
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);

            var dt = ds.Tables[0];

            foreach (DataRow r in dt.Rows)
            {
              string line = String.Empty;
              for (int i = 0; i < r.ItemArray.Count(); i++)
              {
                string colValue = r[i].GetDbColumnStringValue();
                line += line.IsBlank() ? colValue : "^" + colValue;
              }

              sb.Append(line + g.crlf);

              itemCount++;
            }
          }

          conn.Close();

          this.Cursor = Cursors.Default;

          txtCompareResults.Text = sb.ToString();

          lblCompareTextLines.Text = itemCount.ToString("###,##0") + " lines";
          return String.Empty;
        }     
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during the execution of the query." + g.crlf2 + ex.ToReport(),
                "Text Utility - Error Querying Database", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return String.Empty;
      }
    }

    private void CompareText()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        var showOption = GetShowOption();

        txtCompareResults.Text = String.Empty;
        Application.DoEvents();

        var sb = new StringBuilder();
        
        var leftLines = txtBaseText.Text.ToLineList();
        var rightLines = txtCompareText.Text.ToLineList();

        var matches = new List<string>();

        int leftLineUnmatchedCount = 0;
        int rightLineUnmatchedCount = 0;
        int matchCount = 0;
        
        for (int i = 0; i < leftLines.Count; i++)
        {
          string leftLine = leftLines.ElementAt(i);
          if (!rightLines.Contains(leftLine))
          {
            if (showOption != ShowOptions.ShowAllMismatches && showOption != ShowOptions.ShowLeftMismatches)
              continue;

            sb.Append("L : " + leftLine + g.crlf);
            //var startPlace = new Place(0, txtCompareResults.Lines.Count - 1);
            //txtCompareResults.Text += leftLine + g.crlf;
            //var endPlace = new Place(leftLine.Length, txtCompareResults.Lines.Count - 1);
            //var range = new Range(txtCompareResults, startPlace, endPlace);
            //range.SetStyle(_leftStyle);
            leftLineUnmatchedCount++;
          }
          else
          {
            if (!matches.Contains(leftLine))
              matches.Add(leftLine);
          }
        }

        if (sb.Length > 0)
          sb.Append(g.crlf);


        for (int i = 0; i < rightLines.Count; i++)
        {
          string rightLine = rightLines.ElementAt(i);
          if (!leftLines.Contains(rightLine))
          {
            if (showOption != ShowOptions.ShowAllMismatches && showOption != ShowOptions.ShowRightMismatches)
              continue;

            sb.Append("R : " + rightLine + g.crlf);
            //var startPlace = new Place(0, txtCompareResults.Lines.Count - 1);
            //txtCompareResults.Text += rightLine + g.crlf;
            //rightLineUnmatchedCount++;
            //var endPlace = new Place(rightLine.Length, txtCompareResults.Lines.Count - 1);
            //var range = new Range(txtCompareResults, startPlace, endPlace);
            //range.SetStyle(_rightStyle);
            rightLineUnmatchedCount++;
          }
          else
          {
            if (!matches.Contains(rightLine))
              matches.Add(rightLine);
          }
        }

        if (showOption == ShowOptions.ShowMatches)
        {
          foreach (var match in matches)
            sb.Append(match + g.crlf);
        }


        string result = sb.ToString().Trim();

        if (result.IsBlank())
        {
          txtCompareResults.Text = showOption == ShowOptions.ShowMatches ? "*** NO MATCHES *** " :  "*** ITEM LISTS ARE IDENTICAL ***";
        }
        else
        {
          txtCompareResults.Text = result;
        }

        matchCount = matches.Count;

        lblStatus.Text = "Not matched: Left:" + leftLineUnmatchedCount.ToString("###,##0") +
                         "  Right:" + rightLineUnmatchedCount.ToString("###,##0") +
                         "  Matched:" + matchCount.ToString("###,##0");


        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during the text comparison operation." + g.crlf2 + ex.ToReport(),
                        "Text Utility - Text Comparison Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ShowOptions GetShowOption()
    {
      if (rbShowAll.Checked)
        return ShowOptions.ShowAllMismatches;

      if (rbShowLeft.Checked)
        return ShowOptions.ShowLeftMismatches;

      if (rbShowRight.Checked)
        return ShowOptions.ShowRightMismatches;

      if (rbShowMatches.Checked)
        return ShowOptions.ShowMatches;

      return ShowOptions.ShowAllMismatches;
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
                        "Text Utility - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        this.SetInitialSizeAndLocation();

        InitializeWellGrid();

        _testDbSpec = g.GetDbSpec("Test");
        _prodDbSpec = g.GetDbSpec("Prod");

        _leftColor = Color.Blue;
        _rightColor = Color.Green;

        txtBaseText.ForeColor = _leftColor;
        txtCompareText.ForeColor = _rightColor;

        _leftStyle = new TextStyle(new SolidBrush(_leftColor), null, FontStyle.Regular);
        _rightStyle = new TextStyle(new SolidBrush(_rightColor), null, FontStyle.Regular);

        cboEnvironment.SelectedIndex = 0;

        LoadBaseTextComboBox();
        LoadQueriesComboBox();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "Text Utility - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeWellGrid()
    {
      gvWells.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "WellName";
      col.HeaderText = "Well Name";
      col.Width = 200;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "GPWellNo";
      col.HeaderText = "GPWellNo";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Active";
      col.HeaderText = "Active";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Operated";
      col.HeaderText = "Oper";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "CountyName";
      col.HeaderText = "County";
      col.Width = 120;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "StateName";
      col.HeaderText = "State";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "FieldName";
      col.HeaderText = "Field";
      col.Width = 110;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "PhaseWindow";
      col.HeaderText = "Phase Window";
      col.Width = 110;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "API";
      col.HeaderText = "API";
      col.Width = 110;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvWells.Columns.Add(col);
    }

    private void LoadWellGrid()
    {
      gvWells.Rows.Clear();

      if (_wellSet == null || _wellSet.Count == 0)
        return;

      var piSet = typeof(Well).GetProperties();

      foreach (var well in _wellSet)
      {
        var row = new DataGridViewRow();
        foreach (DataGridViewColumn gvCol in gvWells.Columns)
        {
          var pi = piSet.Where(p => p.Name == gvCol.Name).FirstOrDefault();
          var cell = new DataGridViewTextBoxCell();
          cell.Value = pi != null ? pi.GetValue(well) : null;
          row.Cells.Add(cell);
        }
        gvWells.Rows.Add(row);
      }
    }

    private void LoadBaseTextComboBox(string selected = "")
    {
      cboBaseData.LoadItems(Directory.GetFiles(g.ImportsPath + @"\BaseData", "*.txt").Select(f => Path.GetFileName(f)).ToList());

      if (selected.IsBlank())
      {
        if (cboBaseData.Items.Count > 0)
          cboBaseData.SelectedIndex = 0;
      }
      else
      {
        cboBaseData.SelectItem(selected);
      }    
    }

    private void LoadQueriesComboBox(string selected = "")
    {
      cboQuery.LoadItems(Directory.GetFiles(g.ImportsPath + @"\Queries", "*.sql").Select(f => Path.GetFileName(f)).ToList());

      if (selected.IsBlank())
      {
        if (cboQuery.Items.Count > 0)
          cboQuery.SelectedIndex = 0;
      }
      else
      {
        cboQuery.SelectItem(selected);
      }
    }

    private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboEnvironment.Text == "Prod")
        _selectedDbSpec = _prodDbSpec;
      else
        _selectedDbSpec = _testDbSpec;
    }

    private void cboBaseData_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboBaseData.Text.IsNotBlank())
        LoadBaseText();
    }

    private void cboQuery_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboQuery.Text.IsNotBlank())
        LoadQuery();
    }


    private void fctb_DoubleClick(object sender, EventArgs e)
    {
      //var pi = sender.GetType().GetProperty("Size");
      //if (pi == null)
      //  return;

      //var size = pi.GetValue(sender);

      //if (size == null)
      //  return;

      //Size s = (Size)size;

      //MessageBox.Show("Width: " + s.Width.ToString() + "  Height: " + s.Height.ToString(), ((Control)sender).Name);
    }

    private void rb_CheckedChanged(object sender, EventArgs e)
    {
      CompareText();
    }

    private void txtQuery_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      CSharpSyntaxHighlight(e);
    }


    private void CSharpSyntaxHighlight(TextChangedEventArgs e)
    {
      txtQuery.LeftBracket = '(';
      txtQuery.RightBracket = ')';
      txtQuery.LeftBracket2 = '\x0';
      txtQuery.RightBracket2 = '\x0';
      //clear style of changed range
      e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

      //string highlighting
      e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
      //comment highlighting
      e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
      e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
      e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
      //number highlighting
      e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
      //attribute highlighting
      e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
      //class name highlighting
      e.ChangedRange.SetStyle(BoldStyle, @"\b(SELECT|select|DISTINCT|DISTINCT|FROM|from|WHERE|where)\s+(?<range>\w+?)\b");
      //keyword highlighting
      e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

      //clear folding markers
      e.ChangedRange.ClearFoldingMarkers();

      //set folding markers
      e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
      e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
      e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
    }

  }
}
