using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Org.TW;
using Org.TW.Forms;
using Org.GS;

namespace Org.TW.Forms
{
  public partial class frmToolWindowParent : Form
  {
    public bool active {
      get;
      set;
    }
    private ToolWindowManager _twMgr;

    #region ToolWindow Management Items
    [DllImport("user32", CharSet = CharSet.Auto)]
    private extern static int SendMessage(
      IntPtr handle, int msg, int wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    private const int WM_ACTIVATE = 0x006;
    private const int WM_ACTIVATEAPP = 0x01C;
    private const int WM_NCACTIVATE = 0x086;
    #endregion

    public frmToolWindowParent()
    {
      InitializeComponent();
    }

    [DebuggerStepThrough]
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);

      if (m.Msg != WM_NCACTIVATE)
        return;

      if (_twMgr == null || _twMgr.ToolWindows == null || _twMgr.ToolWindows.Count == 0)
        return;

      IntPtr handle = GetForegroundWindow();

      bool anyToolWindowHandle = false;
      foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
      {
        if (f.Handle == handle && !f.IsDisposed)
        {
          anyToolWindowHandle = true;
          break;
        }
      }

      //Console.WriteLine("FromAny " + anyToolWindowHandle.ToString());

      if (((int)m.WParam) == 0)
      {
        if ((handle == this.Handle) || anyToolWindowHandle)
        {
          if (!this.active)
          {
            this.active = true;
            SendMessage(this.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
            //Console.WriteLine("Activate I " + this.Tag.ToString());
          }

          foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
          {
            if (!f.IsDisposed)
            {
              if (f.active == false)
              {
                f.active = true;
                SendMessage(f.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
                //Console.WriteLine("Activate H " + f.Tag.ToString());
              }
            }
          }
        }
        else
        {
          if (this.active)
          {
            this.active = false;
            SendMessage(this.Handle, WM_NCACTIVATE, 0, IntPtr.Zero);
            //Console.WriteLine("Activate G " + this.Tag.ToString());
          }

          foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
          {
            if (!f.IsDisposed)
            {
              if (f.active)
              {
                f.active = false;
                SendMessage(f.Handle, WM_NCACTIVATE, 0, IntPtr.Zero);
                //Console.WriteLine("Deactivate G " + f.Tag.ToString());
              }
            }
          }
        }
      }

      if (((int)m.WParam) == 1)
      {
        if (!this.active)
        {
          this.active = true;
          SendMessage(this.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
        }


        foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
        {
          if (!f.IsDisposed)
          {
            if (!f.active)
            {
              f.active = true;
              SendMessage(f.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
              //Console.WriteLine("Activate F " + f.Tag.ToString());
            }
          }
        }
      }
    }

    public void SetToolWindows(ToolWindowManager twMgr)
    {
      _twMgr = twMgr;
    }

    public bool FromParentOrPeer(IntPtr handle)
    {
      if (this.Handle == handle)
        return true;

      if (_twMgr == null || _twMgr.ToolWindows == null || _twMgr.ToolWindows.Count == 0)
        return false;

      foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
      {
        if (f.Handle == handle && !f.IsDisposed)
        {
          return true;
        }
      }

      return false;
    }

    public void ActivateAllChildren()
    {
      if (_twMgr == null || _twMgr.ToolWindows == null || _twMgr.ToolWindows.Count == 0)
        return;

      foreach (frmToolWindowBase f in _twMgr.ToolWindows.Values)
      {
        if (!f.IsDisposed)
        {
          f.active = true;
          SendMessage(f.Handle, WM_NCACTIVATE, 1, IntPtr.Zero);
          //Console.WriteLine("ParentActivating " + f.Tag.ToString());
        }
      }
    }

    protected void base_Activated(object sender, EventArgs e)
    {
      active = true;
      //Console.WriteLine("ActivatedEvent E " + this.Tag.ToString());
    }

    protected void base_Deactivate(object sender, EventArgs e)
    {
      active = false;
      // Console.WriteLine("DeactivatedEvent E " + this.Tag.ToString());
    }


    protected void base_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_twMgr == null || _twMgr.ToolWindows == null || _twMgr.ToolWindows.Count == 0)
        return;

      _twMgr.MarkTimeToClose();
    }

  }
}
