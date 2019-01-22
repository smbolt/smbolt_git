namespace Org.RPT
{
    partial class frmReport
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pbSpacer = new System.Windows.Forms.PictureBox();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbSpacer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlMain
      // 
      this.pnlMain.AutoScroll = true;
      this.pnlMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.pnlMain.Controls.Add(this.pbSpacer);
      this.pnlMain.Controls.Add(this.pbMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 37);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1073, 725);
      this.pnlMain.TabIndex = 0;
      // 
      // pbSpacer
      // 
      this.pbSpacer.Location = new System.Drawing.Point(36, 273);
      this.pbSpacer.Name = "pbSpacer";
      this.pbSpacer.Size = new System.Drawing.Size(50, 50);
      this.pbSpacer.TabIndex = 1;
      this.pbSpacer.TabStop = false;
      // 
      // pbMain
      // 
      this.pbMain.BackColor = System.Drawing.Color.White;
      this.pbMain.Location = new System.Drawing.Point(36, 17);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(359, 240);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnRefresh);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1073, 37);
      this.pnlTop.TabIndex = 1;
      // 
      // btnRefresh
      // 
      this.btnRefresh.Location = new System.Drawing.Point(13, 7);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(123, 23);
      this.btnRefresh.TabIndex = 0;
      this.btnRefresh.Tag = "Refresh";
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.btnRefresh.Click += new System.EventHandler(this.Action);
      // 
      // frmReport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1073, 762);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmReport";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Report Form";
      this.pnlMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbSpacer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.pnlTop.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.PictureBox pbSpacer;
    }
}