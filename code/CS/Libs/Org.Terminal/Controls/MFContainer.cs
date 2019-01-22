using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Terminal.Screen;
using Org.GS;

namespace Org.Terminal.Controls
{
  public partial class MFContainer : Panel
  {
    public event Action<MFEventArgs> EventToHost;
    public Action<MFEventArgs> MFControlEvent;

    private ControlManager _controlManager;

    public List<TextBlock> TextLines {
      get {
        return Get_TextLines();
      }
    }

    public Font MainTextFont {
      get;
      private set;
    }
    public Color MainTextDefaultColor {
      get;
      private set;
    }
    public Brush MainTextDefaultBrush {
      get;
      private set;
    }
    public Size CharSize {
      get;
      private set;
    }
    public int CharWidth {
      get {
        return this.CharSize.Width;
      }
    }
    public int LineHeight {
      get {
        return this.CharSize.Height;
      }
    }
    public int CapacityNumberOfLinesHigh {
      get {
        return Get_CapacityNumberOfLinesHigh();
      }
    }
    public int CapacityNumberOfCharsWide {
      get {
        return Get_CapacityNumberOfCharsWide();
      }
    }
    public int TopPadding {
      get;
      private set;
    }
    public int RightPadding {
      get;
      private set;
    }
    public int BottomPadding {
      get;
      private set;
    }
    public int LeftPadding {
      get;
      private set;
    }
    public int CharTopPadding {
      get;
      private set;
    }
    public int CharLeftPadding {
      get;
      private set;
    }

    public bool DrawMetrics {
      get;
      set;
    }
    public bool IsReadyForSizeManagement {
      get;
      set;
    }
    public bool ContainsVFlexControl {
      get {
        return _controlManager == null ? false : _controlManager.ContainsVFlexControl;
      }
    }

    private int _prevLinesHigh;
    private int _prevCharsWide;

    public PreviewKeyDownEventHandler TextBlock_PreviewKeyDown;
    public EventHandler TextBlock_GotFocus;
    public EventHandler TextBlock_LostFocus;
    public Func<Org.Terminal.BMS.FieldColor, Brush> TextBlock_GetTextBrush;


    public MFContainer()
    {
      InitializeComponent();
      InitializeControl();
    }

    private List<TextBlock> Get_TextLines()
    {
      var textLines = new List<TextBlock>();

      foreach (var tb in this.Controls.OfType<TextBlock>().Where(t => t.Name.StartsWith("@TL")))
        textLines.Add(tb);

      return textLines;
    }

    public void SetFontSpec(FontSpec fontSpec)
    {
      if (fontSpec == null)
        return;

      if (fontSpec.FontSize == this.MainTextFont.Size && fontSpec.CharSize == this.CharSize)
        return;

      this.MainTextFont = new Font(this.MainTextFont.FontFamily, fontSpec.FontSize);
      this.CharSize = fontSpec.CharSize;

      TextBlock focusedTb = null;

      foreach (TextBlock tb in this.Controls.OfType<TextBlock>())
      {
        if (focusedTb == null && tb.Focused)
          focusedTb = tb;
        tb.Visible = false;
      }

      LayoutControls();
      DrawAll();

      foreach (TextBlock tb in this.Controls.OfType<TextBlock>())
        tb.Visible = true;

      if (focusedTb != null)
        focusedTb.Focus();

      this.Invalidate();
    }

    public void RefreshUI()
    {
      LayoutControls();
      DrawAll();
      this.Invalidate();
    }

    public void ShowFields(bool show)
    {
      SolidBrush backgroundBrush = show ? new SolidBrush(Color.FromArgb(255, 33, 33, 33)) : (SolidBrush) Brushes.Black;

      foreach (MFBase c in this.Controls)
      {
        c.BackgroundBrush = backgroundBrush;
      }
      DrawAll();
      this.Invalidate();
    }

    public void LayoutControls()
    {
      try
      {
        // WORK AT HIGHER LEVEL...
        // PUSH THE DETAILED PROCESSING INTO THE MFCONTAINER OBJECT (RENAME THE OBJECT)
        // MAKE MFCONTAINER (ABSTRACTING CONTROL MANAGER) ABLE TO DO THINGS LIKE "REMOVE VFLEX-CREATED LINES"
        // MAKE IT SMART ENOUGH TO ONLY REMOVE THOSE THAT ARE GETTING SQUEEZED OUT...  OR MAYBE JUST SET THEM ASIDE
        // LIKE A "SAVED ROW"...
        // QUICKLY RENUMBER / PUSH DOWN/UP THE VFLEX-MOVED CONTROLS
        // WHAT ABOUT TAGGING THOSE (LOGICALLY) THAT NEED TO BE MOVED SO THEY DON'T HAVE TO BE DISCOVERED.


        int nbrLines = this.CapacityNumberOfLinesHigh;
        int nbrCols = this.CapacityNumberOfCharsWide;

        // LATER - do the minimum sizing (below) from screen definition - not hard coding...

        if (nbrLines < 24)
          nbrLines = 24;

        if (nbrCols < 80)
          nbrCols = 80;

        ControlLine vFlexLine = GetVFlexLine();

        if (vFlexLine != null)
        {
          // figure out what to remove and what to add if anything...
          // rather than removing all and replacing what's needed.

          ClearControlLines();
          RemoveVFlexCreatedControls();

          if (_controlManager.ControlLineSet.Count == 0)
          {
            _controlManager.ClearControlLineSet();

            foreach (var tb in this.Controls.OfType<TextBlock>())
            {
              if (!_controlManager.ControlLineSet.ContainsKey(tb.CurrLine))
                _controlManager.ControlLineSet.Add(tb.CurrLine, new ControlLine());
              var controlLine = _controlManager.ControlLineSet[tb.CurrLine];
              tb.IsPlaced = false;
              tb.LineItem = controlLine.Count + 1;
              controlLine.Add(tb);
            }
          }

          int vFlexLineNumber = vFlexLine[0].OrigLine;
          int nextLineNumber = GetNextLineNumber(vFlexLineNumber);

          if (nextLineNumber > -1)
          {
            int lastControlLine = _controlManager.ControlLineSet.LastLine;
            int numberOfControlLinesToMove = nextLineNumber - lastControlLine + 1;
            int numberOfLinesToMoveDown = nbrLines - lastControlLine - numberOfControlLinesToMove + 1;
            _controlManager.MoveLinesDown(nextLineNumber, numberOfLinesToMoveDown);
            nextLineNumber = nbrLines - numberOfControlLinesToMove + 1;

            for (int newLine = vFlexLineNumber + 1; newLine < nextLineNumber; newLine++)
            {
              foreach (TextBlock sourceTb in vFlexLine)
              {
                if (sourceTb.IsVFlexControl)
                {
                  var newTb = sourceTb.CloneForVFlex(newLine);
                  if (this.Controls.ContainsKey(newTb.Name))
                    throw new Exception("The VFLEX cloned control named '" + newTb.Name + "' cannot be added to the " +
                                        "MFContainer because a control with the same name already exists.");

                  newTb.IsVFlexCreated = true;
                  this.Controls.Add(newTb);

                  newTb.TextBrush = TextBlock_GetTextBrush(newTb.FieldSpec.Color);
                  newTb.GotFocus += TextBlock_GotFocus;
                  newTb.LostFocus += TextBlock_LostFocus;
                  newTb.PreviewKeyDown += TextBlock_PreviewKeyDown;
                  newTb.EventToHost += MFControlEvent;

                  if (!_controlManager.ControlLineSet.ContainsKey(newTb.CurrLine))
                    _controlManager.ControlLineSet.Add(newTb.CurrLine, new ControlLine());
                  var controlLine = _controlManager.ControlLineSet[newTb.CurrLine];
                  controlLine.Add(newTb);
                }
              }
            }
          }
        }


        InitializePlacement();

        foreach (var controlLine in _controlManager.ControlLineSet.Values)
        {
          if (!controlLine.IncludesHFlexControls)
          {
            // when no FlexControls on line, use original location and length
            foreach (var tb in controlLine)
            {
              int x = this.LeftPadding + ((tb.FieldSpec.OrigCol - 1) * this.CharSize.Width);
              int y = this.TopPadding + ((tb.FieldSpec.CurrLine - 1) * this.CharSize.Height);
              int width = tb.FieldSpec.OrigLth * this.CharSize.Width;
              tb.Location = new Point(x, y);
              tb.Size = new Size(width, this.CharSize.Height);
              tb.IsPlaced = true;
            }
          }
          else
          {
            // Build array of integers to indicate the characters the controls occupy.

            int[] line = new int[nbrCols];
            for (int c = 0; c < nbrCols; c++)
            {
              line[c] = -1;
            }

            // place any "float right" controls processing the controls from right to left


            // FLOAT RIGHT CONTROLS
            // Place controls from right to left
            for (int i = controlLine.Count - 1; i > -1;  i--)
            {
              var tb = controlLine.ElementAt(i);
              if (tb.IsFloatRight)
              {
                int rightMost = line.GetRightMostPlacement(tb.CurrLth);
                if (rightMost == -1)
                  throw new Exception("Cannot place text block control named " + tb.Name + " on line " + tb.CurrLine.ToString() + ".");
                tb.FieldSpec.CurrCol = rightMost + 1;
                tb.IsPlaced = true;
                line.MarkAsOccupied(tb.CurrCol - 1, tb.CurrLth, tb.LineItem);
                int x = this.LeftPadding + ((tb.FieldSpec.CurrCol - 1) * this.CharSize.Width);
                int y = this.TopPadding + ((tb.FieldSpec.CurrLine - 1) * this.CharSize.Height);
                int width = tb.FieldSpec.CurrLth * this.CharSize.Width;
                tb.Location = new Point(x, y);
                tb.Size = new Size(width, this.CharSize.Height);
              }
              else
              {
                // right-most non-FloatRight control terminates processing for the line
                break;
              }
            }

            // STRETCH CONTROLS
            foreach (var tb in controlLine.Where(t => !t.IsPlaced && t.IsStretch))
            {
              int endPos = line.FindEndOfStretch(tb.CurrCol - 1);
              tb.FieldSpec.CurrLth = endPos - tb.CurrCol + 2;
              string text = tb.Text;

              if (tb.IsProtected)
              {
                if (text.Length < tb.CurrLth && tb.FieldSpec.Fill.IsNotBlank())
                {
                  int lengthToAdd = tb.CurrLth - text.Length;
                  string addedText = String.Empty;
                  if (tb.Text.IsNotBlank())
                    addedText = new String(tb.FieldSpec.Fill[0], lengthToAdd);
                  tb.Text = tb.Text + addedText;
                }

                if (text.Length > tb.CurrLth)
                  tb.Text = tb.Text.Substring(0, tb.CurrLth);
              }

              int x = this.LeftPadding + ((tb.FieldSpec.CurrCol - 1) * this.CharSize.Width);
              int y = this.TopPadding + ((tb.FieldSpec.CurrLine - 1) * this.CharSize.Height);
              int width = tb.FieldSpec.CurrLth * this.CharSize.Width;
              tb.Location = new Point(x, y);
              tb.Size = new Size(width, this.CharSize.Height);
              tb.IsPlaced = true;
            }

            // FIXED CONTROLS
            foreach (var tb in controlLine.Where(t => !t.IsPlaced))
            {
              int x = this.LeftPadding + ((tb.FieldSpec.CurrCol - 1) * this.CharSize.Width);
              int y = this.TopPadding + ((tb.FieldSpec.CurrLine - 1) * this.CharSize.Height);
              int width = tb.FieldSpec.CurrLth * this.CharSize.Width;
              tb.Location = new Point(x, y);
              tb.Size = new Size(width, this.CharSize.Height);
              tb.IsPlaced = true;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while laying out the controls in the MFContainer.", ex);
      }
    }

    private void ClearControlLines()
    {
      if (_controlManager == null)
        return;

      _controlManager.ClearControlLineSet();
    }

    private void RemoveVFlexCreatedControls()
    {
      var controlsToRemove = new List<string>();
      foreach (TextBlock tb in this.Controls.OfType<TextBlock>())
      {
        if (tb.IsVFlexCreated)
          controlsToRemove.Add(tb.Name);
      }

      foreach (var controlName in controlsToRemove)
        this.Controls.RemoveByKey(controlName);
    }

    private ControlLine GetVFlexLine()
    {
      return _controlManager.VFlexLine;
    }

    private int GetNextLineNumber(int vFlexLineNumber)
    {
      if (_controlManager == null || _controlManager.ControlLineSet == null || _controlManager.ControlLineSet.Count == 0)
        return -1;

      int nextLineNumber = -1;
      foreach (int lineNbr in _controlManager.ControlLineSet.Keys)
      {
        if (lineNbr > vFlexLineNumber)
        {
          nextLineNumber = lineNbr;
          break;
        }
      }

      return nextLineNumber;
    }

    private void InitializePlacement()
    {
      if (_controlManager == null || _controlManager.ControlLineSet == null || _controlManager.ControlLineSet.Count == 0)
        return;

      foreach (var controlLine in _controlManager.ControlLineSet.Values)
      {
        foreach(TextBlock tb in controlLine)
          tb.IsPlaced = false;
      }
    }

    public void DrawAll()
    {
      try
      {
        foreach (var tb in this.Controls.OfType<TextBlock>())
        {
          tb.ClearBitmap();
          tb.Draw();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while drawing the controls in the MFContainer.", ex);
      }
    }

    private void InitializeControl()
    {
      this.IsReadyForSizeManagement = false;
      this.BackColor = Color.Black;

      _controlManager = new ControlManager(this);
      this.DrawMetrics = false;

      float initialFontSize = 13.0F;
      float fontSizeToWidth = (float) 11 / 15;
      float fontSizeToHeight = (float) 20 / 15;

      int charWidth = Convert.ToInt32((float)initialFontSize * fontSizeToWidth);
      int charHeight = Convert.ToInt32((float)initialFontSize * fontSizeToHeight);


      this.CharSize = new Size(charWidth, charHeight);

      this.TopPadding = 24;
      this.LeftPadding = 24;
      this.RightPadding = 24;
      this.BottomPadding = 24;

      this.CharTopPadding = -3;
      this.CharLeftPadding = -3;

      this.MainTextFont = new Font("Consolas", initialFontSize);
      this.MainTextDefaultColor = Color.FromArgb(255, 51, 153, 255);
      this.MainTextDefaultBrush = new SolidBrush(this.MainTextDefaultColor);

      _prevLinesHigh = 0;
      _prevCharsWide = 0;

    }

    private void MFContainer_ControlAdded(object sender, ControlEventArgs e)
    {
      if (!e.Control.GetType().IsSubclassOf(typeof(MFBase)))
        return;

      _controlManager.AddControl((MFBase)e.Control);
    }

    private void MFContainer_ControlRemoved(object sender, ControlEventArgs e)
    {
      if (!e.Control.GetType().IsSubclassOf(typeof(MFBase)))
        return;

      _controlManager.RemoveControl((MFBase)e.Control);
    }

    public void ClearControls()
    {
      if (_controlManager != null)
        _controlManager.ClearControls();
    }

    public void ProcessTabKey(TextBlock currTb, bool shiftKey)
    {
      if (currTb == null)
        return;

      TextBlock nextControl = null;

      if (shiftKey)
        nextControl = _controlManager.GetPrevControl(currTb) as TextBlock;
      else
        nextControl = _controlManager.GetNextControl(currTb) as TextBlock;

      if (nextControl == null)
        return;

      nextControl.CurrPos = 0;
      nextControl.Focus();
    }

    public void ProcessEnterKey(TextBlock currTb, bool shiftKey)
    {
      // This is processing of the enter key without the control key held down.

      // When not in a editor line command or text line field, act just like the tab key.
      // When in a editor line command or text line, then move to next "like" control vertically.
      // Like with the Tab key, the shift will determine direction (forward, back, up, down).

      if (currTb == null)
        return;

      TextBlock nextControl = null;
      string controlType = String.Empty;

      bool inLineCommand = currTb.Name.StartsWith("@LC");
      bool inTextLine = currTb.Name.StartsWith("@TL");

      if (inLineCommand)
        controlType = "@LC";

      if (inTextLine)
        controlType = "@TL";

      if (shiftKey)
        nextControl = _controlManager.GetPrevControl(currTb, controlType) as TextBlock;
      else
        nextControl = _controlManager.GetNextControl(currTb, controlType) as TextBlock;

      if (nextControl == null)
        return;

      nextControl.CurrPos = 0;
      nextControl.Focus();
    }

    private void MFContainer_Click(object sender, EventArgs e)
    {
    }

    private void MFContainer_Resize(object sender, EventArgs e)
    {
      CheckScreenLayout();
    }

    public void MainFormResizeEnd()
    {
      CheckScreenLayout(true);
    }

    private void CheckScreenLayout(bool forceLayout = false)
    {
      if (this.IsReadyForSizeManagement)
      {
        int linesHigh = this.CapacityNumberOfLinesHigh;
        int charsWide = this.CapacityNumberOfCharsWide;

        int linesDiff = Math.Abs(_prevLinesHigh - linesHigh);
        int charsDiff = Math.Abs(_prevCharsWide - charsWide);

        if (!forceLayout && linesDiff < 1 && charsDiff < 1)
        {
          return;
        }

        this.LayoutControls();

        _prevLinesHigh = linesHigh;
        _prevCharsWide = charsWide;

        if (this.EventToHost != null)
        {
          MFEventArgs args = null;

          if (this.EventToHost != null)
          {
            if (args == null)
              args = new MFEventArgs(this, EventType.ScreenResize);
            this.EventToHost(args);
          }
        }
      }

      this.DrawAll();
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      float widthPercent = (float) e.ClipRectangle.Width / this.Width;
      float heightPercent = (float)e.ClipRectangle.Height / this.Height;

      if (widthPercent < 0.95F || heightPercent < 0.95F)
      {
        if (!this.DrawMetrics)
          return;
      }

      // Manage this through a floating tool window...
      // Don't need the docking stuff...
      // Might be able to leverage the message loop access...

      bool drawGrid = false;

      if (drawGrid)
      {
        int drawableWidth = this.Width - (this.LeftPadding + this.RightPadding);
        int drawableHeight = this.Height - (this.TopPadding + this.BottomPadding);

        var edge = new Rectangle(this.LeftPadding, this.TopPadding, drawableWidth, drawableHeight);
        e.Graphics.DrawRectangle(Pens.LightSteelBlue, edge);


        for (int c = this.LeftPadding; c < this.LeftPadding + drawableWidth; c += this.CharSize.Width)
        {
          e.Graphics.DrawLine(Pens.LightGray, new Point(c, this.TopPadding), new Point(c, this.TopPadding + drawableHeight));
        }

        for (int r = this.TopPadding; r < this.TopPadding + drawableHeight; r += this.CharSize.Height)
        {
          e.Graphics.DrawLine(Pens.LightGray, new Point(this.LeftPadding, r), new Point(this.LeftPadding + drawableWidth, r));
        }

        this.DrawMetrics = false;
      }
    }

    private int Get_CapacityNumberOfLinesHigh()
    {
      if (this.LineHeight < 0 || this.Size.Height < 0)
        return 0;

      return Convert.ToInt32((this.Size.Height - (this.TopPadding + this.BottomPadding)) / this.LineHeight);
    }

    private int Get_CapacityNumberOfCharsWide()
    {
      if (this.CharWidth < 0 || this.Size.Width < 0)
        return 0;

      return Convert.ToInt32((this.Size.Width - (this.LeftPadding + this.RightPadding)) / this.CharWidth);
    }
  }
}
