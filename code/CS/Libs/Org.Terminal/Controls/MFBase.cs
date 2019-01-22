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
  public partial class MFBase : UserControl
  {
    public new string Text 
    { 
      get { return base.Text; }
      set { base.Text = value; }
    }

    protected FieldSpec _fieldSpec;
    public FieldSpec FieldSpec { get { return _fieldSpec; } }
    public int CurrLine { get { return _fieldSpec.CurrLine; } }
    public int OrigLine { get { return _fieldSpec.OrigLine; } }
    public int CurrCol { get { return _fieldSpec.CurrCol; } }
    public int OrigCol { get { return _fieldSpec.OrigCol; } }
    public int CurrLth { get { return _fieldSpec.CurrLth; } }
    public int OrigLth { get { return _fieldSpec.OrigLth; } }
    
    protected event Action<MFEventArgs> EventFromBase;
    public event Action<MFEventArgs> EventToHost;
    protected Bitmap BaseBitmap { get; set; }
    protected PictureBox pb { get { return pbMain; } }
    protected bool IsFocused { get; set; }
    protected bool IsFocusable { get; set; }
    protected Timer CursorTimer { get; set; }
    protected bool IsCursorVisible { get; set; }
    protected bool IsCursorActive { get; set; }
    public bool IsProtected { get { return Get_IsProtected(); } } 
    protected static int CursorBlinkMilliseconds { get; set; }
    protected int CharLeftPadding { get { return Get_CharLeftPadding(); } }
    protected int CharTopPadding { get { return Get_CharTopPadding(); } }
    protected Size CharSize { get { return Get_CharSize(); } }
    protected Font MainTextFont { get { return Get_MainTextFont(); } }
    protected Brush MainTextDefaultBrush { get { return Get_MainTextDefaultBrush(); } }
    public Brush TextBrush { get; set; }
    public Brush BackgroundBrush { get; set; }
    public int CurrPos { get; set; }
    public bool IsPlaced { get; set; }
    public bool IsHFlexControl { get { return Get_IsHFlexControl(); } }
    public bool IsVFlexCreated { get; set; }
    public bool IsVFlexMoved { get; set; }
    public bool IsFloatRight { get { return Get_IsFloatRight(); } }
    public bool IsVFlexControl { get { return Get_IsVFlexControl(); } }
    public bool IsStretch { get { return Get_IsStretch(); } }
    public int LineItem { get; set; }

    public MFContainer MFContainer { get; private set; }

    public MFBase() { }

    public MFBase(MFContainer mfContainer, FieldSpec fieldSpec)
    {
      InitializeComponent();

      _fieldSpec = fieldSpec;
      this.IsFocusable = true;
      this.MFContainer = mfContainer;
      this.TextBrush = Brushes.White;
      this.CurrPos = 0;
      this.IsPlaced = false;
      this.LineItem = 0;
      this.IsVFlexCreated = false;
      this.IsVFlexMoved = false;

      CursorBlinkMilliseconds = 500;

      this.CursorTimer = new Timer();
      this.CursorTimer.Tick += CursorTimer_Tick;
      this.BackgroundBrush = new SolidBrush(Color.Black);
    }

    public new void Click(MFEventArgs e)
    {
      int charPos = e.MouseEventArgs.X / this.CharSize.Width;
      if (charPos > this.Text.Length)
        this.CurrPos = this.Text.Length;
      else
        this.CurrPos = charPos; 
    }

    public void AddCharacter(char c)
    {
      if (Char.IsControl(c))
        return;

      this.Text += c.ToString();
      this.CurrPos++;
    }

    public void BackSpace()
    {
      if (this.Text.Length > 0)
      {
        // need to account for cursor position.

        this.Text = this.Text.Substring(0, this.Text.Length - 1);
        if (this.CurrPos > 0)
          this.CurrPos--;
      }
    }

    public void LeftArrow()
    {
      if (this.CurrPos > 0)
        this.CurrPos--;
    }

    public void RightArrow()
    {
      if (this.CurrPos < this.Text.Length)
        this.CurrPos++;
    }

    public void DeleteChar()
    {

    }

    public new void KeyDown(object sender, KeyEventArgs e)
    {
      if (this.EventFromBase == null)
        return;

      this.EventFromBase(new MFEventArgs(this, EventType.KeyDown, e));
    }

    public new void KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.EventFromBase == null)
        return; 

      this.EventFromBase(new MFEventArgs(this, EventType.KeyPress, e)); 
    }

    public new void KeyUp(object sender, KeyEventArgs e)
    {
      if (this.EventFromBase == null)
        return;

      this.EventFromBase(new MFEventArgs(this, EventType.KeyUp, e)); 
    }

    private void pbMain_SizeChanged(object sender, EventArgs e)
    {

    }

    private void pbMain_Paint(object sender, PaintEventArgs e)
    {
      if (this.EventFromBase == null)
        return;

      this.EventFromBase(new MFEventArgs(this, EventType.Paint, e)); 
    }

    private void pbMain_Click(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.Click, e);
        this.EventFromBase(args); 
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.Click, e);
        this.EventToHost(args); 
      }
    }

    private void pbMain_MouseEnter(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseEnter, e);
        this.EventFromBase(args); 
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseEnter, e);
        this.EventToHost(args);
      }
    }

    private void pbMain_MouseLeave(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseLeave, e);
        this.EventFromBase(args); 
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseLeave, e);
        this.EventToHost(args);
      }
    }

    private void pbMain_MouseMove(object sender, MouseEventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseMove, e);
        this.EventFromBase(args); 
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseMove, e);
        this.EventToHost(args);
      }
    }

    private void MFBase_Enter(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.Enter, e);
        this.EventFromBase(args); 
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.Enter, e);
        this.EventToHost(args);
      }
    }

    private void MFBase_Leave(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.Leave, e);
        this.EventFromBase(args);
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.Leave, e);
        this.EventToHost(args);
      }
    }

    private void MFBase_MouseEnter(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseEnter, e);
        this.EventFromBase(args);
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseEnter, e);
        this.EventToHost(args);
      }
    }

    private void MFBase_MouseLeave(object sender, EventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseLeave, e);
        this.EventFromBase(args);
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseLeave, e);
        this.EventToHost(args);
      }
    }

    private void MFBase_MouseMove(object sender, MouseEventArgs e)
    {
      MFEventArgs args = null;

      if (this.EventFromBase != null)
      {
        args = new MFEventArgs(this, EventType.MouseMove, e);
        this.EventFromBase(args);
      }

      if (this.EventToHost != null)
      {
        if (args == null)
          args = new MFEventArgs(this, EventType.MouseMove, e);
        this.EventToHost(args);
      }
    }

    private void CursorTimer_Tick(object sender, EventArgs e)
    {
      if (this.EventFromBase == null)
        return;

      this.EventFromBase(new MFEventArgs(this, EventType.CursorBlink, e));
    }

    private bool Get_IsProtected()
    {
      if (_fieldSpec == null)
        return true;

      return _fieldSpec.IsProtected;
    }

    private int Get_CharLeftPadding()
    {
      if (this.MFContainer == null)
        return 0;

      return this.MFContainer.CharLeftPadding;
    }

    private int Get_CharTopPadding()
    {
      if (this.MFContainer == null)
        return 0;

      return this.MFContainer.CharTopPadding;
    }

    private Size Get_CharSize()
    {
      if (this.MFContainer == null)
        return new Size(0, 0);

      return this.MFContainer.CharSize;
    }

    private Font Get_MainTextFont()
    {
      if (this.MFContainer == null)
        return new Font("Consolas", 15.0F);

      return this.MFContainer.MainTextFont;
    }

    private Brush Get_MainTextDefaultBrush()
    {
      if (this.MFContainer == null)
        return Brushes.Blue;

      return this.MFContainer.MainTextDefaultBrush;
    }

    private bool Get_IsHFlexControl()
    {
      if (_fieldSpec == null)
        return false;

      return _fieldSpec.HFlex != BMS.HFlex.None;
    }

    private bool Get_IsVFlexControl()
    {
      if (this.IsVFlexCreated || this.IsVFlexMoved)
        return false;

      if (_fieldSpec == null)
        return false;

      return _fieldSpec.VFlex != BMS.VFlex.None;
    }

    private bool Get_IsFloatRight()
    {
      if (_fieldSpec == null)
        return false;

      return _fieldSpec.HFlex == BMS.HFlex.FloatRight;
    }

    private bool Get_IsStretch()
    {
      if (_fieldSpec == null)
        return false;

      return _fieldSpec.HFlex == BMS.HFlex.Stretch;
    }

    public override string ToString()
    {
      string typeName = this.GetType().Name;
      return typeName + " (" + this.Name + ", " + this.Text + ")";
    }
  }
}
