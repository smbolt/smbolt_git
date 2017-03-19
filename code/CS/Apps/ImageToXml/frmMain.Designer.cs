namespace Adsdi.ImageToXml
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnShowData = new System.Windows.Forms.Button();
            this.btnSerializeImage = new System.Windows.Forms.Button();
            this.btnSwitchToImageTab = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.tabPageData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnShowData);
            this.pnlTop.Controls.Add(this.btnSerializeImage);
            this.pnlTop.Controls.Add(this.btnSwitchToImageTab);
            this.pnlTop.Controls.Add(this.btnLoadImage);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(948, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // btnShowData
            // 
            this.btnShowData.Location = new System.Drawing.Point(705, 12);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(164, 36);
            this.btnShowData.TabIndex = 0;
            this.btnShowData.Text = "Show Data";
            this.btnShowData.UseVisualStyleBackColor = true;
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // btnSerializeImage
            // 
            this.btnSerializeImage.Location = new System.Drawing.Point(535, 12);
            this.btnSerializeImage.Name = "btnSerializeImage";
            this.btnSerializeImage.Size = new System.Drawing.Size(164, 36);
            this.btnSerializeImage.TabIndex = 0;
            this.btnSerializeImage.Text = "Serialize Image";
            this.btnSerializeImage.UseVisualStyleBackColor = true;
            this.btnSerializeImage.Click += new System.EventHandler(this.btnSerializeImage_Click);
            // 
            // btnSwitchToImageTab
            // 
            this.btnSwitchToImageTab.Location = new System.Drawing.Point(190, 12);
            this.btnSwitchToImageTab.Name = "btnSwitchToImageTab";
            this.btnSwitchToImageTab.Size = new System.Drawing.Size(164, 36);
            this.btnSwitchToImageTab.TabIndex = 0;
            this.btnSwitchToImageTab.Text = "Show Image";
            this.btnSwitchToImageTab.UseVisualStyleBackColor = true;
            this.btnSwitchToImageTab.Click += new System.EventHandler(this.btnSwitchToImageTab_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(20, 12);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(164, 36);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "LoadImage";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(948, 617);
            this.pnlMain.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabPageImage);
            this.tabMain.Controls.Add(this.tabPageData);
            this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
            this.tabMain.Location = new System.Drawing.Point(-5, -5);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(953, 630);
            this.tabMain.TabIndex = 0;
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.pnlImage);
            this.tabPageImage.Location = new System.Drawing.Point(4, 5);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Padding = new System.Windows.Forms.Padding(20, 20, 16, 20);
            this.tabPageImage.Size = new System.Drawing.Size(945, 621);
            this.tabPageImage.TabIndex = 0;
            this.tabPageImage.Tag = "TABPAGE_IMAGE";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // pnlImage
            // 
            this.pnlImage.AutoScroll = true;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.pbMain);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(20, 20);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(909, 581);
            this.pnlImage.TabIndex = 0;
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(853, 512);
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.txtOut);
            this.tabPageData.Location = new System.Drawing.Point(4, 5);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(20, 20, 16, 20);
            this.tabPageData.Size = new System.Drawing.Size(945, 621);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Tag = "TABPAGE_DATA";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // txtOut
            // 
            this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOut.Location = new System.Drawing.Point(20, 20);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOut.Size = new System.Drawing.Size(909, 581);
            this.txtOut.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 677);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageToXml";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlTop.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.tabPageData.ResumeLayout(false);
            this.tabPageData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnShowData;
        private System.Windows.Forms.Button btnSerializeImage;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.Button btnSwitchToImageTab;
        private System.Windows.Forms.TextBox txtOut;
    }
}

