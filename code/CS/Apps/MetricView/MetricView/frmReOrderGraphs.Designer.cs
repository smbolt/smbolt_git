namespace Teleflora.Operations.MetricView
{
  partial class frmReOrderGraphs
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
      this.gbGraphInfo = new System.Windows.Forms.GroupBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.lbGraphs = new System.Windows.Forms.ListBox();
      this.btnMoveUp = new System.Windows.Forms.Button();
      this.btnMoveDown = new System.Windows.Forms.Button();
      this.lblInfo = new System.Windows.Forms.Label();
      this.gbGraphInfo.SuspendLayout();
      this.SuspendLayout();
      //
      // gbGraphInfo
      //
      this.gbGraphInfo.Controls.Add(this.lbGraphs);
      this.gbGraphInfo.Controls.Add(this.lblInfo);
      this.gbGraphInfo.Controls.Add(this.btnMoveDown);
      this.gbGraphInfo.Controls.Add(this.btnMoveUp);
      this.gbGraphInfo.Location = new System.Drawing.Point(12, 12);
      this.gbGraphInfo.Name = "gbGraphInfo";
      this.gbGraphInfo.Size = new System.Drawing.Size(434, 300);
      this.gbGraphInfo.TabIndex = 1;
      this.gbGraphInfo.TabStop = false;
      this.gbGraphInfo.Text = "Update Graph Sequential Order";
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(236, 327);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(105, 25);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // btnUpdate
      //
      this.btnUpdate.Location = new System.Drawing.Point(118, 327);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(105, 25);
      this.btnUpdate.TabIndex = 3;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      //
      // lbGraphs
      //
      this.lbGraphs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbGraphs.FormattingEnabled = true;
      this.lbGraphs.Location = new System.Drawing.Point(175, 20);
      this.lbGraphs.Name = "lbGraphs";
      this.lbGraphs.Size = new System.Drawing.Size(244, 264);
      this.lbGraphs.TabIndex = 0;
      this.lbGraphs.SelectedIndexChanged += new System.EventHandler(this.lbGraphs_SelectedIndexChanged);
      //
      // btnMoveUp
      //
      this.btnMoveUp.Location = new System.Drawing.Point(22, 121);
      this.btnMoveUp.Name = "btnMoveUp";
      this.btnMoveUp.Size = new System.Drawing.Size(126, 25);
      this.btnMoveUp.TabIndex = 1;
      this.btnMoveUp.Text = "Move Up";
      this.btnMoveUp.UseVisualStyleBackColor = true;
      this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
      //
      // btnMoveDown
      //
      this.btnMoveDown.Location = new System.Drawing.Point(22, 153);
      this.btnMoveDown.Name = "btnMoveDown";
      this.btnMoveDown.Size = new System.Drawing.Size(126, 25);
      this.btnMoveDown.TabIndex = 2;
      this.btnMoveDown.Text = "Move Down";
      this.btnMoveDown.UseVisualStyleBackColor = true;
      this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
      //
      // lblInfo
      //
      this.lblInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInfo.Location = new System.Drawing.Point(10, 36);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(171, 56);
      this.lblInfo.TabIndex = 3;
      this.lblInfo.Text = "The sequential order of the graphs controls the order that they will appear in on" +
                          " the screen when auto arranged.";
      //
      // frmReOrderGraphs
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 364);
      this.ControlBox = false;
      this.Controls.Add(this.btnUpdate);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.gbGraphInfo);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmReOrderGraphs";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Re-Order Graphs";
      this.gbGraphInfo.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbGraphInfo;
    private System.Windows.Forms.ListBox lbGraphs;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnMoveDown;
    private System.Windows.Forms.Button btnMoveUp;
    private System.Windows.Forms.Label lblInfo;
  }
}