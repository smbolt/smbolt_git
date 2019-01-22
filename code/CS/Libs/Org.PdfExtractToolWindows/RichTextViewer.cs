using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW.ToolPanels;
using Org.GS;

namespace Org.PdfExtractToolWindows
{
  public partial class RichTextViewer : ToolPanelBase
  {
    private string _originalText;
    private bool _initialShowing = true;

    public string Text
    {
      get
      {
        return this.txtData.Text;
      }
      set
      {
        _originalText = value;
        this.txtData.Text = value;
        if (_initialShowing)
        {
          _initialShowing = false;
        }
        else
        {
          RunFilters();
        }
      }
    }

    public Panel TopPanel {
      get {
        return base.TopPanel;
      }
    }

    public RichTextViewer()
      : base("RichTextViewer")
    {
      InitializeComponent();
      ckUseDynamicFiltering.Checked = true;
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RunFilters":
          RunFilters();
          break;
      }

    }

    private void RunFilters()
    {
      if (txtFilters.Text.Trim().IsBlank())
      {
        txtData.Text = _originalText;
        return;
      }

      string[] filters = txtFilters.Text.Trim().ToLower().Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      string t = _originalText;

      StringBuilder sb = new StringBuilder();

      string[] lines = _originalText.Replace("\r", String.Empty).Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string line in lines)
      {
        if (line.Contains("DxWorkbook") || line.Contains("DxWorksheet") || line.Contains("DxCellArray"))
          sb.Append(line + g.crlf);
        else
        {
          string lineLc = line.ToLower();
          foreach (var filter in filters)
          {
            if (lineLc.Contains(filter))
            {
              sb.Append(line + g.crlf);
              break;
            }
          }
        }
      }

      txtData.Text = sb.ToString();
    }

    public Control GetTopPanelControl(string controlName)
    {
      return this.TopPanel.Controls.OfType<Control>().Where(c => c.Name == controlName).FirstOrDefault();
    }

    private void txtFilters_TextChanged(object sender, EventArgs e)
    {
      if (ckUseDynamicFiltering.Checked)
        RunFilters();
    }

    private void ckUseDynamicFiltering_CheckedChanged(object sender, EventArgs e)
    {
      btnRunFilter.Enabled = !ckUseDynamicFiltering.Checked;
    }
  }
}
