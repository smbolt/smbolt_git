namespace Adsdi.MetrixAdmin.Module
{
  partial class MainForm
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

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.pnlBackmost = new System.Windows.Forms.Panel();
      this.waitSpinner1 = new Adsdi.Controls.WaitSpinner();
      this.buttonBar1 = new Adsdi.Controls.ButtonBar();
      this.button5 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.btnTest1 = new System.Windows.Forms.Button();
      this.pnlBackmost.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlBackmost
      //
      this.pnlBackmost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlBackmost.Controls.Add(this.waitSpinner1);
      this.pnlBackmost.Controls.Add(this.buttonBar1);
      this.pnlBackmost.Controls.Add(this.button5);
      this.pnlBackmost.Controls.Add(this.button2);
      this.pnlBackmost.Controls.Add(this.button4);
      this.pnlBackmost.Controls.Add(this.button1);
      this.pnlBackmost.Controls.Add(this.button3);
      this.pnlBackmost.Controls.Add(this.btnTest1);
      this.pnlBackmost.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlBackmost.Location = new System.Drawing.Point(0, 0);
      this.pnlBackmost.Name = "pnlBackmost";
      this.pnlBackmost.Size = new System.Drawing.Size(792, 480);
      this.pnlBackmost.TabIndex = 1;
      //
      // waitSpinner1
      //
      this.waitSpinner1.BackColor = System.Drawing.Color.White;
      this.waitSpinner1.GridRowIndex = -1;
      this.waitSpinner1.Location = new System.Drawing.Point(20, 86);
      this.waitSpinner1.Name = "waitSpinner1";
      this.waitSpinner1.Size = new System.Drawing.Size(48, 48);
      this.waitSpinner1.Status = Adsdi.Controls.WaitSpinnerStatus.Stopped;
      this.waitSpinner1.TabIndex = 2;
      //
      // buttonBar1
      //
      this.buttonBar1.ButtonSize = new System.Drawing.Size(76, 54);
      this.buttonBar1.ConfigString = resources.GetString("buttonBar1.ConfigString");
      this.buttonBar1.ControlOrientation = Adsdi.Controls.ControlOrientation.Horizontal;
      this.buttonBar1.Dock = System.Windows.Forms.DockStyle.Top;
      this.buttonBar1.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(193)))), ((int)(((byte)(247)))));
      this.buttonBar1.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
      this.buttonBar1.Location = new System.Drawing.Point(0, 0);
      this.buttonBar1.Name = "buttonBar1";
      this.buttonBar1.Size = new System.Drawing.Size(790, 55);
      this.buttonBar1.TabIndex = 1;
      this.buttonBar1.ControlEvent += new System.Action<Adsdi.Controls.ControlEventArgs>(this.buttonBar_ControlEvent);
      //
      // button5
      //
      this.button5.Location = new System.Drawing.Point(378, 325);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(127, 45);
      this.button5.TabIndex = 0;
      this.button5.Text = "Test 6";
      this.button5.UseVisualStyleBackColor = true;
      //
      // button2
      //
      this.button2.Location = new System.Drawing.Point(245, 325);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(127, 45);
      this.button2.TabIndex = 0;
      this.button2.Text = "Test 3";
      this.button2.UseVisualStyleBackColor = true;
      //
      // button4
      //
      this.button4.Location = new System.Drawing.Point(378, 274);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(127, 45);
      this.button4.TabIndex = 0;
      this.button4.Text = "Test 5";
      this.button4.UseVisualStyleBackColor = true;
      //
      // button1
      //
      this.button1.Location = new System.Drawing.Point(245, 274);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(127, 45);
      this.button1.TabIndex = 0;
      this.button1.Text = "Test 2";
      this.button1.UseVisualStyleBackColor = true;
      //
      // button3
      //
      this.button3.Location = new System.Drawing.Point(378, 223);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(127, 45);
      this.button3.TabIndex = 0;
      this.button3.Text = "Test 4";
      this.button3.UseVisualStyleBackColor = true;
      //
      // btnTest1
      //
      this.btnTest1.Location = new System.Drawing.Point(245, 223);
      this.btnTest1.Name = "btnTest1";
      this.btnTest1.Size = new System.Drawing.Size(127, 45);
      this.btnTest1.TabIndex = 0;
      this.btnTest1.Tag = "TEST1";
      this.btnTest1.Text = "Test 1";
      this.btnTest1.UseVisualStyleBackColor = true;
      this.btnTest1.Click += new System.EventHandler(this.Action_Click);
      //
      // MainForm
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlBackmost);
      this.Name = "MainForm";
      this.Size = new System.Drawing.Size(792, 480);
      this.pnlBackmost.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlBackmost;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button btnTest1;
    private Controls.ButtonBar buttonBar1;
    private Controls.WaitSpinner waitSpinner1;



  }
}
