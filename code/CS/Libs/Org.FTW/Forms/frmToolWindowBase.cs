using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Diagnostics;
using Org.FTW.ToolPanels;
using Org.GS;

namespace Org.FTW.Forms
{
  public partial class frmToolWindowBase : Form
  {
    private bool _isFirstShowing = true;
    public event Action<ToolActionEventArgs> ToolAction;

    public Panel DockPanel
    {
      get { return this.pnlMain; }
    }

    public frmToolWindowParent _parent;
    private ToolPanelBase _toolPanel; 
    public bool active = false;
    private bool IsTimeToClose = false;
    private const int WM_ACTIVATE = 0x006;
    private const int WM_ACTIVATEAPP = 0x01C;
    private const int WM_NCACTIVATE = 0x086;
    private const int MF_STRING = 0x0;
    private const int MF_BYPOSITION = 0x400;
    private const int WM_SYSCOMMAND = 0x112;
		private const int WM_NCLBUTTONDBLCLK = 0x0A3;
    private int SYSMENU_DOCK_ID = 0x1;
    private System.Timers.Timer _moveTimer;

    [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
    private extern static int SendMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DeleteMenu(IntPtr hMenu, int uPosition, int uFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetMenuItemCount(IntPtr hMenu);
    
    public frmToolWindowBase()
    {
    }

    public frmToolWindowBase(frmToolWindowParent parent, string title)
    {
      InitializeComponent();
      _parent = parent;
      this.Text = title;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      // Modify the tool windows system menu, removing all but Move, Size and Close, then add Dock
      base.OnHandleCreated(e);
      IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

      DeleteMenu(hSysMenu, 0, MF_BYPOSITION);
      DeleteMenu(hSysMenu, 2, MF_BYPOSITION);
      DeleteMenu(hSysMenu, 2, MF_BYPOSITION);
      DeleteMenu(hSysMenu, 2, MF_BYPOSITION);

      AppendMenu(hSysMenu, MF_STRING, SYSMENU_DOCK_ID, "&Dock");
    }

    //[DebuggerStepThrough]
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);

      if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_DOCK_ID))
      {
        EnsureToolPanel();

				if (_toolPanel != null)
				{
					_toolPanel.NotifyHost(_toolPanel, ToolPanelHostCommand.Dock);
				}

        return;
      }

      if (_parent == null)
        return;

			if (m.Msg == WM_NCLBUTTONDBLCLK)
			{
				EnsureToolPanel();

				if (_toolPanel != null)
				{
					_toolPanel.NotifyHost(_toolPanel, ToolPanelHostCommand.Dock);
				}

				return;
			}

      if (m.Msg != WM_NCACTIVATE)
        return;

      if (((int)m.WParam) == 0)
      {
        IntPtr handle = GetForegroundWindow();
        if (_parent.FromParentOrPeer(handle))
        {
          //Console.WriteLine("FromParentOrPeer True");
          _parent.ActivateAllChildren();

          if (_parent.active == false)
          {
            _parent.active = true;
            SendMessage(_parent.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
            //Console.WriteLine("Activate A " + _parent.Tag.ToString()); 
          }
        }
        else
        {
          //Console.WriteLine("FromParentOrPeer False"); 
          if (this.active == true)
          {
            this.active = false;
            SendMessage(this.Handle, WM_NCACTIVATE, 0, IntPtr.Zero);
            //Console.WriteLine("Deactivate B " + this.Tag.ToString()); 
          }
          if (_parent.active == true)
          {
            _parent.active = false;
            SendMessage(_parent.Handle, WM_NCACTIVATE, 0, IntPtr.Zero);
            //Console.WriteLine("Deactivate B " + _parent.Tag.ToString()); 
          }
        }
      }
      if (((int)m.WParam) == 1)
      {
        if (this.active == false)
        {
          this.active = true;
          SendMessage(this.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
          //Console.WriteLine("Activate C " + this.Tag.ToString()); 
        }
        if (_parent.active == false)
        {
          _parent.active = true;
          SendMessage(_parent.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
          //Console.WriteLine("Activate C " + _parent.Tag.ToString()); 
        }
      }

    }

    protected void base_Activated(object sender, EventArgs e)
    {
      active = true;
      //Console.WriteLine("ActivatedEvent D " + this.Tag.ToString()); 
    }

    protected void base_Deactivate(object sender, EventArgs e)
    {
      active = false;
      //Console.WriteLine("DeactivatedEvent D " + this.Tag.ToString()); 
    }

    public void MarkTimeToClose()
    {
      IsTimeToClose = true;

      if (_moveTimer != null && _moveTimer.Enabled)
        _moveTimer.Enabled = false;

      this.Close();
    }

    protected void base_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.FormOwnerClosing)
        return;

      if (!IsTimeToClose)
      {
        e.Cancel = true;
        this.Hide();
      }
    }

    private void base_LocationChanged(object sender, EventArgs e)
    {
      if (this.ToolAction == null)
        return;

      if (_moveTimer == null)
      {
        _moveTimer = new System.Timers.Timer();
        _moveTimer.Enabled = false;
        _moveTimer.Interval = 500;
        _moveTimer.Elapsed += MoveTimer_Elapsed;
      }

      _moveTimer.Enabled = false;
      _moveTimer.Enabled = true;
    }

    private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      _moveTimer.Enabled = false;
			if (this.ToolAction != null)
				this.ToolAction(new ToolActionEventArgs(this, ToolActionEvent.ToolWindowMoved));
    }

    private void base_ResizeEnd(object sender, EventArgs e)
    {
      if (this.ToolAction == null)
        return;

      this.ToolAction(new ToolActionEventArgs(this, ToolActionEvent.ToolWindowResized));
    }

    private void base_VisibleChanged(object sender, EventArgs e)
    {
      if (this.ToolAction == null)
        return;

      this.ToolAction(new ToolActionEventArgs(this, ToolActionEvent.ToolWindowVisibleChanged));
    }

    public void WireUpNotifyHost()
    {
      if (this.pnlMain.Controls.Count == 0)
        return;

      object o = this.pnlMain.Controls[0];

      if (!o.GetType().IsSubclassOf(typeof(ToolPanelBase)))
        return;

      ToolPanelBase panel = (ToolPanelBase)o;
    }

    private void EnsureToolPanel()
    {
      if (this.pnlMain.Controls.Count == 0)
        return;

      object o = this.pnlMain.Controls[0];

      if (!o.GetType().IsSubclassOf(typeof(ToolPanelBase)))
        return;

      _toolPanel = (ToolPanelBase)o;
    }
		
  }
}
