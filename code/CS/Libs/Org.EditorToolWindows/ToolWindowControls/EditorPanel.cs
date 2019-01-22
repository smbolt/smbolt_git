using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Terminal.Controls;
using Org.Terminal.Screen;
using Org.Terminal.BMS;
using Org.FTW.ToolPanels;
using Org.MF;
using Org.GS;

namespace Org.EditorToolWindows
{
  public partial class EditorPanel : ToolPanelBase
  {
    public FileBase FileBase { get; private set; }
    public bool IsFileLoaded { get { return this.FileBase != null; } }
    public int? FirstFileLineShown { get; private set; }
    public InsertMode InsertMode { get; private set; }

    public event Action<MFEventArgs> EventToHost;

    private TextBlock _focusedTb;

    private static SolidBrush RedBrush;
    private static SolidBrush GreenBrush;
    private static SolidBrush BlueBrush;
    private static SolidBrush YellowBrush;
    private static SolidBrush PinkBrush;
    private static SolidBrush TurquoisBrush;
    private static SolidBrush WhiteBrush;
    private static SolidBrush BackgroundBrush;

    private ScreenSpec _screenSpec;

    public EditorPanel()
    {
      InitializeComponent();
      InitializeControl();
    }

    private void ProcessCommand(string command)
    {
      try
      {
        string cmd = command.Trim().ToUpper();

        switch (cmd)
        {
          case "OPEN":
            this.FirstFileLineShown = 0; 
            OpenFile(g.ImportsPath + @"\COBOL\ORGUT100.CBL");
            break;

          case "CLOSE":
            CloseFile();
            this.FirstFileLineShown = null;
            break;
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void OpenFile(string fullPath)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        this.FileBase = new FileBase();
        this.FileBase.Open(fullPath);

        LoadFileToScreen();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to open the file named '" + fullPath + "'." +
                        ex.ToReport(), "TerminalTest - File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private void CloseFile()
    {

    }

    private void LoadFileToScreen(int linesOffset = 0)
    {
      try
      {
        List<TextBlock> tbTextLine = mfContainer.TextLines;

        this.FirstFileLineShown += linesOffset;
        if (this.FirstFileLineShown < 0)
          this.FirstFileLineShown = 0;

        int recordIndex = this.FirstFileLineShown.Value;

        for (int i = 0; i < tbTextLine.Count - 1; i++)
        {
          if (recordIndex > this.FileBase.RecordSet.Count - 1)
            break;

          tbTextLine[i].Record = this.FileBase.RecordSet[recordIndex++];
          tbTextLine[i].Draw();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the file to the screen.", ex);
      }
    }



    private void InitializeControl()
    {
      this.InsertMode = InsertMode.IntelligentInsert;
      this.FileBase = null;
      this.FirstFileLineShown = null;

      this.mfContainer.EventToHost += mfContainer_EventToHost;
      this.mfContainer.TextBlock_PreviewKeyDown = tb_PreviewKeyDown;
      this.mfContainer.TextBlock_GotFocus = tb_GotFocus;
      this.mfContainer.TextBlock_LostFocus = tb_LostFocus;
      this.mfContainer.MFControlEvent = MFControlEvent;
      this.mfContainer.TextBlock_GetTextBrush = GetTextBrush;

      base.TopPanel.Visible = false;
      mfContainer.BackColor = Color.Black;

      BlueBrush = new SolidBrush(Color.FromArgb(255, 51, 153, 255));
      RedBrush = new SolidBrush(Color.FromArgb(255, 252, 2, 4)); 
      GreenBrush= new SolidBrush(Color.FromArgb(255, 14, 235, 16));
      YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
      PinkBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 255));
      TurquoisBrush = new SolidBrush(Color.FromArgb(255, 0, 255, 160));
      WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
      BackgroundBrush = new SolidBrush(Color.FromArgb(255, 33, 33, 33));
    }

    private void mfContainer_EventToHost(MFEventArgs e)
    {
      if (this.EventToHost != null)
      {
        e.EventCommand = EventCommand.UpdateInfoPanel;
        this.EventToHost(e); 
      }
    }

    public void SetScreenSpec(ScreenSpec screenSpec)
    {
      _screenSpec = screenSpec; 
    }

    public void SetFontSpec(FontSpec fontSpec)
    {
      mfContainer.SetFontSpec(fontSpec); 
    }

    public void ShowFields(bool show)
    {
      mfContainer.ShowFields(show); 
    }

    public void RefreshUI()
    {
      mfContainer.RefreshUI();
      LoadFileToScreen(0); 
    }

    public void ShowScreen()
    {
      try
      {
        if (_screenSpec == null)
          return;

        mfContainer.Controls.Clear();

        mfContainer.DrawMetrics = false;

        int fieldNumber = 0;

        int screenWidth = mfContainer.Size.Width;
        int screenHeight = mfContainer.Size.Height;

        foreach (var fieldSpec in _screenSpec.FieldSpecSet)
        {
          fieldSpec.FieldSpecSet = _screenSpec.FieldSpecSet;

          if (fieldSpec.Name.IsBlank())
            fieldSpec.Bms_DFHMDF.Name = "Field" + fieldNumber.ToString();

          var tb = new TextBlock(mfContainer, fieldSpec);
          tb.Name = fieldSpec.Name;
          tb.TabIndex = fieldNumber++;
          tb.TabStop = fieldSpec.TabStop;
          tb.TextBrush = GetTextBrush(fieldSpec.Color);

          tb.GotFocus += tb_GotFocus;
          tb.LostFocus += tb_LostFocus;
          tb.PreviewKeyDown += tb_PreviewKeyDown;

          if (mfContainer.Controls.ContainsKey(tb.Name))
            continue;

          mfContainer.Controls.Add(tb);
          tb.EventToHost += MFControlEvent;
        }

        mfContainer.LayoutControls();
        mfContainer.DrawAll();

        mfContainer.DrawMetrics = true;
        mfContainer.Invalidate();

        mfContainer.IsReadyForSizeManagement = true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to show the screen.", ex); 
      }
    }

    private void tb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      if (_focusedTb == null)
        return;

      TextBlock tb = sender as TextBlock;

      switch (e.KeyCode)
      {
        case Keys.Enter:
          if (e.Control)
          {
            // simulate MF enter key?
            e.IsInputKey = true;
          }
          else
          {
            if (tb.Name == "COMMAND")
            {
              string commandText = tb.Text;
              if (commandText.IsNotBlank())
              {
                tb.Text = String.Empty;
                tb.CurrPos = 0; 
                tb.Draw();
                ProcessCommand(commandText);
                return;
              }
            }

            mfContainer.ProcessEnterKey(_focusedTb, e.Shift);
            e.IsInputKey = true;
          }
          break;

        case Keys.Tab:
          mfContainer.ProcessTabKey(_focusedTb, e.Shift);
          e.IsInputKey = true;
          break;

        case Keys.Left:
          _focusedTb.LeftArrow();
          e.IsInputKey = true;
          break;

        case Keys.Right:
          _focusedTb.RightArrow();
          e.IsInputKey = true;
          break;

        case Keys.Up:
          e.IsInputKey = true;
          break;

        case Keys.Down:
          e.IsInputKey = true;
          break;
      }
    }



    private Brush GetTextBrush(FieldColor color)
    {
      switch (color)
      {
        case FieldColor.DEFAULT:
        case FieldColor.NEUTRAL:
          return WhiteBrush;

        case FieldColor.BLUE:
          return BlueBrush;

        case FieldColor.RED:
          return RedBrush;

        case FieldColor.GREEN:
          return GreenBrush;

        case FieldColor.TURQUOISE:
          return TurquoisBrush;

        case FieldColor.YELLOW:
          return YellowBrush;

        case FieldColor.PINK:
          return PinkBrush;

        default:
          return WhiteBrush;
      }
    }

    public void SetInitialFocus()
    {
      // THIS NEEDS TO BE FORM-DEPENDENT, NOT HARD-CODED.
      // Eventually it may be that the state should be preserved (or preservable) across showings of the form
      // stored in some kind of user-session profile, some elements of which should work across sessions


      for (int i = 0; i < mfContainer.Controls.Count; i++)
      {
        var control = mfContainer.Controls[i];
        if (control.GetType().Name == "TextBlock")
        {
          var tb = (TextBlock)control;

          if (tb.Name == "COMMAND")
          {
            tb.Focus();
            return;
          }
        }
      }
    }

    private void tb_GotFocus(object sender, EventArgs e)
    {
      if (sender.GetType().Name != "TextBlock")
        return;

      TextBlock tb = sender as TextBlock;

      MFEventArgs args = null;
      
      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(tb, EventType.MouseEnter, e);
        args.EventType = EventType.NotSet;
        args.EventCommand = EventCommand.UpdateInfoPanel;
        args.EventMessage = tb.Name + " got focus"; 
        this.EventToHost(args);
      }

      tb.ActivateCursor();
      string name = tb.Name;
      _focusedTb = tb;
    }

    private void tb_LostFocus(object sender, EventArgs e)
    {
      if (sender.GetType().Name != "TextBlock")
        return;

      TextBlock tb = sender as TextBlock;
      tb.DeactivateCursor();
      string name = tb.Name;


    }

    public new void KeyDown(object sender, KeyEventArgs e)
    {
      if (_focusedTb != null)
        _focusedTb.KeyDown(sender, e); 
    }

    public new void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (_focusedTb != null)
        _focusedTb.KeyPress(sender, e);
    }

    public new void KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Insert:
          switch (this.InsertMode)
          {
            case InsertMode.IntelligentInsert:
              this.InsertMode = InsertMode.StrictInsert;
              break;

            case InsertMode.StrictInsert:
              this.InsertMode = InsertMode.TypeOver;
              break;

            case InsertMode.TypeOver:
              this.InsertMode = InsertMode.IntelligentInsert;
              break;
          }
          UpdateInsertModeUI();
          return;

        case Keys.F7:
          ScrollUp();
          return;

        case Keys.F8:
          ScrollDown();
          return;
      }

      if (_focusedTb != null)
        _focusedTb.KeyUp(sender, e); 
    }

    private void UpdateInsertModeUI()
    {
      var tbMode = mfContainer.Controls.OfType<TextBlock>().Where(c => c.Name == "#MODE").FirstOrDefault();
      if (tbMode == null)
        return;

      switch (this.InsertMode)
      {
        case InsertMode.IntelligentInsert:
          tbMode.Text = "                   i";
          break;

        case InsertMode.StrictInsert:
          tbMode.Text = "                   ^";
          break;

        case InsertMode.TypeOver:
          tbMode.Text = "                   o";
          break;
      }

      tbMode.Draw();
    }

    private void ScrollDown()
    {
      try
      {
        if (!IsFileLoaded)
          return;

        int linesToScroll = GetLinesToScroll();

        LoadFileToScreen(linesToScroll); 

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to scroll down." + ex.ToReport(),
                        "TerminalTest - Scroll Down Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private void ScrollUp()
    {
      try
      {
        if (!IsFileLoaded)
          return;

        int linesToScroll = GetLinesToScroll() * -1;

        LoadFileToScreen(linesToScroll); 

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to scroll up." + ex.ToReport(),
                        "TerminalTest - Scroll Down Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private int GetLinesToScroll()
    {
      return 3;
    }

    private void MFControlEvent(MFEventArgs e)
    {
      switch (e.EventType)
      {
        case EventType.MouseEnter:
          if (this.EventToHost != null)
          {
            var sender = e.Sender as MFBase;
            if (sender == null)
              return;
            e.EventMessage = sender.Name + " Mouse Enter";
            e.EventCommand = EventCommand.UpdateInfoPanel;
            this.EventToHost(e);
          }
          break;

        case EventType.MouseMove:
          if (this.EventToHost != null)
          {
            var sender = e.Sender as MFBase;
            var ma = e.MouseEventArgs; 
            if (sender == null || ma == null)
              return;
            e.EventMessage = sender.Name + " X: " + ma.X.ToString("0000") + "  Y: " + ma.Y.ToString("0000");
            e.EventCommand = EventCommand.UpdateInfoPanel;
            this.EventToHost(e);
          }
          break;

        case EventType.MouseLeave:
          if (this.EventToHost != null)
          {
            var sender = e.Sender as MFBase;
            if (sender == null)
              return;
            e.EventMessage = String.Empty;
            e.EventCommand = EventCommand.UpdateInfoPanel;
            this.EventToHost(e);
          }
          break;

        case EventType.Click:
          if (this.EventToHost != null)
          {
            var sender = e.Sender as MFBase;
            var ma = e.MouseEventArgs;
            if (sender == null || ma == null)
              return;
            e.EventMessage = sender.Name + " X: " + ma.X.ToString("0000") + "  Y: " + ma.Y.ToString("0000") + " clicked";
            e.EventCommand = EventCommand.UpdateInfoPanel;
            this.EventToHost(e);
          }
          break;

        case EventType.Enter:
        case EventType.Leave:
          break;

        default:
          break; 
      }
    }

    public void MainFormResizeEnd()
    {
      mfContainer.MainFormResizeEnd();
    }

  }
}
