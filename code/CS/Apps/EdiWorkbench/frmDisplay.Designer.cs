namespace Org.EdiWorkbench
{
    partial class frmDisplay
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
            this.rtxtDisplay = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtxtDisplay
            // 
            this.rtxtDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtDisplay.Font = new System.Drawing.Font("Lucida Console", 8F);
            this.rtxtDisplay.Location = new System.Drawing.Point(5, 5);
            this.rtxtDisplay.Name = "rtxtDisplay";
            this.rtxtDisplay.Size = new System.Drawing.Size(1079, 628);
            this.rtxtDisplay.TabIndex = 0;
            this.rtxtDisplay.Text = "";
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1089, 638);
            this.Controls.Add(this.rtxtDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmDisplay";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Results Display";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtDisplay;
    }
}