namespace Teleflora.Operations.MetricView
{
    partial class frmSplash
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlOutline = new System.Windows.Forms.Panel();
            this.lblBuild = new System.Windows.Forms.Label();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.pnlOutline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(90, 90);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(347, 37);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Splash messages";
            // 
            // pnlOutline
            // 
            this.pnlOutline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOutline.Controls.Add(this.lblBuild);
            this.pnlOutline.Controls.Add(this.pbTitle);
            this.pnlOutline.Controls.Add(this.lblMessage);
            this.pnlOutline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutline.Location = new System.Drawing.Point(3, 3);
            this.pnlOutline.Name = "pnlOutline";
            this.pnlOutline.Size = new System.Drawing.Size(487, 162);
            this.pnlOutline.TabIndex = 2;
            // 
            // lblBuild
            // 
            this.lblBuild.AutoSize = true;
            this.lblBuild.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuild.ForeColor = System.Drawing.Color.Black;
            this.lblBuild.Location = new System.Drawing.Point(379, 137);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(58, 11);
            this.lblBuild.TabIndex = 2;
            this.lblBuild.Text = "build number";
            this.lblBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbTitle
            // 
            this.pbTitle.Image = global::Teleflora.Operations.MetricView.Properties.Resources.MetricViewSplashTitle;
            this.pbTitle.Location = new System.Drawing.Point(90, 27);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(300, 60);
            this.pbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTitle.TabIndex = 0;
            this.pbTitle.TabStop = false;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(493, 168);
            this.ControlBox = false;
            this.Controls.Add(this.pnlOutline);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSplash";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSplash_FormClosing);
            this.pnlOutline.ResumeLayout(false);
            this.pnlOutline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlOutline;
        private System.Windows.Forms.Label lblBuild;
    }
}