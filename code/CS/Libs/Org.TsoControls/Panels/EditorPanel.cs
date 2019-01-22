using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TsoControls.Controls;
using Org.TsoControls.PanelDefs;
using Org.GS;

namespace Org.TsoControls.Panels
{
  public partial class EditorPanel : PanelBase
  {
    private PanelDef _panelDef;
    private string _code;
    private string[] _lines;

    private PanelControlBase _focusedControl;

    private Dictionary<int, EditLine> _docLines;

    public EditorPanel()
    {
      InitializeComponent();
    }

    public EditorPanel(PanelDef panelDef = null)
    {
      InitializeComponent();
      _panelDef = panelDef;

      if (_panelDef != null)
      {
        CreatePanel();
        LoadDocument();
      }
    }

    public void ProcessKeyStroke(KeyEventArgs e)
    {
      if (_focusedControl == null)
        return;

      EditText editText = null;
      LabelText labelText = null;

      int lineNumber = -1;

      switch (_focusedControl.GetType().Name)
      {
        case "EditText":
          editText = _focusedControl as EditText;
          lineNumber = editText.ParentLine.LineNumber;
          break;

        case "LabelText":
          labelText = _focusedControl as LabelText;
          lineNumber = labelText.ParentLine.LineNumber;
          break;
      }

      switch (e.KeyCode)
      {
        case Keys.Down:
          if (_docLines.ContainsKey(lineNumber + 1))
          {
            var editLine = _docLines[lineNumber + 1];
            var newEditText = editLine.GetControl(editText.Name) as EditText;
            int selectionStart = editText.SelectionStart;
            newEditText.Focus();
            newEditText.SelectionStart = selectionStart;
          }

          break;

        case Keys.Up:
          if (_docLines.ContainsKey(lineNumber - 1))
          {
            var editLine = _docLines[lineNumber - 1];
            var newEditText = editLine.GetControl(editText.Name) as EditText;
            int selectionStart = editText.SelectionStart;
            newEditText.Focus();
            newEditText.SelectionStart = selectionStart;
          }

          break;

      }
    }

    private void LoadDocument()
    {
      for (int i = 0; i < 30; i++)
      {
        Console.WriteLine("i = " + i.ToString() + DateTime.Now.ToString("hh:mm:ss.fff"));

        if (i < _docLines.Count)
        {
          if (i > _lines.Length - 1)
            break;

          string line = _lines[i];
          var editLine = _docLines.Values.ElementAt(i);
          foreach (Control c in editLine.Controls)
          {
            if (c.GetType().Name == "EditText")
            {
              EditText e = (EditText)c;
              switch (e.Name)
              {
                case "LineNumber":
                  e.Text = (i * 100).ToString("000000") + " ";
                  break;

                case "LineText":
                  e.Text = line;
                  break;
              }
            }
          }
        }
      }
    }


    private void CreatePanel()
    {
      try
      {
        this.Controls.Clear();
        _docLines = new Dictionary<int, EditLine>();

        if (_panelDef == null || _panelDef.Values.Count == 0)
          return;

        foreach (var panelLine in _panelDef.Values)
        {
          bool repeat = panelLine.Repeat > 1;
          int lineNumber = panelLine.LineNumber;

          for (int i = 0; i < panelLine.Repeat; i++)
          {
            var line = new EditLine(lineNumber, panelLine.SetName);
            foreach (var e in panelLine)
            {
              PanelControlBase c = null;
              switch (e.Type)
              {
                case PanelLineElementType.Edit:
                  c = new EditText(e, line);
                  break;

                case PanelLineElementType.Label:
                  c = new LabelText(e, line);
                  break;
              }

              c.Enter += C_Enter;
              line.Controls.Add(c);
              c.BringToFront();
              c.Dock = DockStyle.Left;
            }

            if (line.InDocument)
            {
              _docLines.Add(line.LineNumber, line);
            }

            this.Controls.Add(line);
            line.BringToFront();
            line.Dock = DockStyle.Top;
            lineNumber++;
          }

        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occured while attempting to create the panel from the PanelDef.", ex);
      }
    }

    private void C_Enter(object sender, EventArgs e)
    {
      _focusedControl = sender as PanelControlBase;
    }

    public void SetCode(string code)
    {
      _code = code;
      _lines = _code.Split(Constants.NewLineDelimiter, StringSplitOptions.None);
    }

    public void SetPanelDef(PanelDef panelDef)
    {
      _panelDef = panelDef;
      CreatePanel();
      LoadDocument();
    }

    public void Initialize()
    {

    }
  }
}
