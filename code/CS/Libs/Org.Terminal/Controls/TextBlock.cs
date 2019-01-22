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
using Org.MF;
using Org.GS;

namespace Org.Terminal.Controls
{
  public partial class TextBlock : MFBase
  {
    private Rectangle _prevCursorRect;
    public string TextBlockName {
      get {
        return this.Name;
      }
    }

    private Record _record;
    public Record Record
    {
      get
      {
        return _record;
      }
      set
      {
        _record = value;
        if (_record == null)
          this.Text = String.Empty;
        else
          this.Text = _record.Text;
      }
    }

    public TextBlock() : base() { }

    public TextBlock(MFContainer parent, FieldSpec fieldSpec)
      : base(parent, fieldSpec)
    {
      InitializeComponent();

      if (fieldSpec.UseFill)
      {
        if (fieldSpec.Fill.Length > 0 && this.CurrLth > 0)
        {
          this.Text = new String(fieldSpec.Fill[0], this.CurrLth);
        }
        else
        {
          this.Text = String.Empty;
        }
      }
      else
      {
        if (fieldSpec.UseInit)
        {
          this.Text = fieldSpec.Init;
        }
        else
        {
          this.Text = String.Empty;
        }
      }

      base.EventFromBase += EventFromBase_Handler;
      this.BackColor = Color.Black;
    }

    private void EventFromBase_Handler(MFEventArgs e)
    {
      if (pb == null)
        return;

      switch (e.EventType)
      {
        case EventType.KeyDown:
          if (e.KeyEventArgs != null)
          {

          }
          break;

        case EventType.KeyPress:
          if (e.KeyPressEventArgs != null)
          {
            char c = e.KeyPressEventArgs.KeyChar;
            switch (c)
            {
              case '\b':
              case '\r':
              case '\n':
              case '\t':
                break;

              default:
                this.AddCharacter(c);
                Draw();
                break;
            }

          }
          break;

        case EventType.KeyUp:
          if (e.KeyEventArgs != null)
          {
            switch (e.KeyEventArgs.KeyCode)
            {
              case Keys.Back:
                this.BackSpace();
                this.Draw();
                break;
            }
          }
          break;

        case EventType.MouseEnter:
          break;

        case EventType.MouseMove:
          break;

        case EventType.MouseLeave:
          break;

        case EventType.Paint:
          PaintObject(e.PaintEventArgs);
          break;

        case EventType.Click:
          this.Click(e);
          this.Focus();
          ActivateCursor();
          break;

        case EventType.Enter:
          ActivateCursor();
          break;

        case EventType.Leave:
          if (this.Text.IsNotBlank())
          {
            this.Text = this.Text.ToUpper();
            this.Draw();
          }
          DeactivateCursor();
          break;

        case EventType.CursorBlink:
          BlinkCursor();
          break;
      }
    }

    public void SetLineNumber(int newLineNumber)
    {
      if (this.CurrLine == newLineNumber)
        return;

      this.FieldSpec.CurrLine = newLineNumber;
    }

    public void PaintObject(PaintEventArgs e)
    {
      if (this.Name == "@LC0")
        Console.WriteLine("TextBlock.PaintObject");

      if (BaseBitmap == null)
        return;

      e.Graphics.DrawImage(BaseBitmap, new Point(0, 0));
    }

    public void ClearBitmap()
    {
      this.BaseBitmap = null;
    }

    public void Draw()
    {
      if (BaseBitmap == null)
        BaseBitmap = new Bitmap(this.Width, this.Height);

      if (this.Name == "@LC1")
        Console.WriteLine("Draw width=" + this.Width.ToString() + "  height=" + this.Height.ToString());

      using (var gr = Graphics.FromImage(BaseBitmap))
      {
        gr.FillRectangle(BackgroundBrush, new Rectangle(0, 0, this.Width, this.Height));

        for (int i = 0; i < this.Text.Length; i++)
        {
          if (base.CharLeftPadding + i * base.CharSize.Width > this.Width)
            return;

          int extraTopPadding = 0;
          if (this.Text[i] == '*')
            extraTopPadding = 4;

          gr.DrawString(this.Text[i].ToString(), MainTextFont, TextBrush, new Point(base.CharLeftPadding + i * base.CharSize.Width, base.CharTopPadding + extraTopPadding));
        }
      }

      pb.Invalidate();
    }

    public void ActivateCursor()
    {
      if (base.IsCursorActive)
        return;

      base.IsCursorActive = true;
      base.IsCursorVisible = true;
      BlinkCursor();
      base.CursorTimer.Interval = CursorBlinkMilliseconds;
      base.CursorTimer.Enabled = true;
    }

    public void DeactivateCursor()
    {
      if (!base.IsCursorActive)
        return;

      base.IsCursorActive = false;
      base.CursorTimer.Enabled = false;
      BlinkCursor(true);
    }

    private void BlinkCursor(bool turnOff = false)
    {
      if (BaseBitmap == null)
        return;

      int charPos = this.CurrPos;

      var cursorRect = new Rectangle(base.CharLeftPadding + charPos * base.CharSize.Width + 3, base.CharTopPadding + base.CharSize.Height + 0, base.CharSize.Width, 2);
      //var removeCursorRect = new Rectangle(base.CharLeftPadding, base.CharTopPadding + base.CharSize.Height + 0, this.Width - base.CharLeftPadding, 2);

      using (var gr = base.pb.CreateGraphics())
      {
        if (base.IsCursorVisible && !turnOff)
        {
          gr.FillRectangle(Brushes.White, cursorRect);
          _prevCursorRect = cursorRect;
        }
        else
        {
          if (_prevCursorRect != null)
            gr.FillRectangle(Brushes.Black, _prevCursorRect);
        }

        if (turnOff)
        {
          gr.FillRectangle(Brushes.Black, _prevCursorRect);
          base.IsCursorActive = false;
          base.IsCursorVisible = false;
        }
        else
        {
          base.IsCursorVisible = !base.IsCursorVisible;
        }
      }
    }

    public TextBlock CloneForVFlex(int lineNumber)
    {
      string name = this.Name;
      if (!name.StartsWith("@"))
        throw new Exception("The name of VFLEX source controls must start with '@'. The name of the VFLEX source control " +
                            "is '" + name + "'.");

      string nameWork = name.Replace("@", String.Empty);

      if (!nameWork.StartsWith("LC") && !nameWork.StartsWith("TL"))
        throw new Exception("The name of VFLEX source controls must start with '@LC' or '@TL'. The name of the VFLEX source control " +
                            "is '" + name + "'.");

      string nameBase = nameWork.Substring(0, 2);
      nameWork = nameWork.Substring(2);
      if (nameWork.IsNotInteger())
      {
        throw new Exception("The name of VFLEX source controls must include only the line original line number folowing the " +
                            "'@LC' or '@TL' that the name starts with. The name of the VFLEX source control " +
                            "is '" + name + "'.");
      }

      int relativeLineNumber = nameWork.ToInt32();
      int physicalLineNumber = this.CurrLine;
      int newLineNumber = lineNumber - physicalLineNumber;
      var newFieldSpec = this._fieldSpec.CloneForVFLEX(newLineNumber);

      var clone = new TextBlock(this.MFContainer, newFieldSpec);
      clone.Name = newFieldSpec.Name;
      clone.TabStop = newFieldSpec.TabStop;

      bool isVFlexControl = clone.IsVFlexControl;

      return clone;
    }
  }
}
