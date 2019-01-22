namespace DesktopClock
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.lblClock = new System.Windows.Forms.Label();
      this.lblCalendar = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      //
      // lblClock
      //
      this.lblClock.BackColor = System.Drawing.Color.Black;
      this.lblClock.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblClock.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClock.ForeColor = System.Drawing.Color.Lime;
      this.lblClock.Location = new System.Drawing.Point(0, 24);
      this.lblClock.Name = "lblClock";
      this.lblClock.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
      this.lblClock.Size = new System.Drawing.Size(653, 130);
      this.lblClock.TabIndex = 0;
      this.lblClock.Text = "12:00:00 PM";
      this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblClock.Click += new System.EventHandler(this.lblClock_Click);
      //
      // lblCalendar
      //
      this.lblCalendar.BackColor = System.Drawing.Color.Black;
      this.lblCalendar.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCalendar.ForeColor = System.Drawing.Color.Lime;
      this.lblCalendar.Location = new System.Drawing.Point(0, 0);
      this.lblCalendar.Name = "lblCalendar";
      this.lblCalendar.Size = new System.Drawing.Size(653, 24);
      this.lblCalendar.TabIndex = 0;
      this.lblCalendar.Text = "December 12, 2016";
      this.lblCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      //
      // timer1
      //
      this.timer1.Interval = 1000;
      //
      // Form1
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(653, 154);
      this.Controls.Add(this.lblClock);
      this.Controls.Add(this.lblCalendar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "Form1";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Desktop Clock";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblClock;
    private System.Windows.Forms.Label lblCalendar;
    private System.Windows.Forms.Timer timer1;
  }
}

