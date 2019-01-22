namespace Teleflora.Operations.MetricView
{
  partial class frmMetricData
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
      this.txtGraphData = new System.Windows.Forms.TextBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.lblData = new System.Windows.Forms.Label();
      this.btnCommaSeparated = new System.Windows.Forms.Button();
      this.btnTextReport = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // txtGraphData
      //
      this.txtGraphData.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtGraphData.Location = new System.Drawing.Point(12, 28);
      this.txtGraphData.MaxLength = 500000;
      this.txtGraphData.Multiline = true;
      this.txtGraphData.Name = "txtGraphData";
      this.txtGraphData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtGraphData.Size = new System.Drawing.Size(770, 606);
      this.txtGraphData.TabIndex = 0;
      this.txtGraphData.WordWrap = false;
      //
      // btnClose
      //
      this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnClose.Location = new System.Drawing.Point(630, 642);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(152, 25);
      this.btnClose.TabIndex = 1;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      //
      // lblData
      //
      this.lblData.AutoSize = true;
      this.lblData.Location = new System.Drawing.Point(12, 9);
      this.lblData.Name = "lblData";
      this.lblData.Size = new System.Drawing.Size(88, 13);
      this.lblData.TabIndex = 2;
      this.lblData.Text = "Data from Graph:";
      //
      // btnCommaSeparated
      //
      this.btnCommaSeparated.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCommaSeparated.Location = new System.Drawing.Point(170, 642);
      this.btnCommaSeparated.Name = "btnCommaSeparated";
      this.btnCommaSeparated.Size = new System.Drawing.Size(152, 25);
      this.btnCommaSeparated.TabIndex = 1;
      this.btnCommaSeparated.Text = "Comma Separated";
      this.btnCommaSeparated.UseVisualStyleBackColor = true;
      this.btnCommaSeparated.Click += new System.EventHandler(this.btnCommaSeparated_Click);
      //
      // btnTextReport
      //
      this.btnTextReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnTextReport.Location = new System.Drawing.Point(12, 642);
      this.btnTextReport.Name = "btnTextReport";
      this.btnTextReport.Size = new System.Drawing.Size(152, 25);
      this.btnTextReport.TabIndex = 1;
      this.btnTextReport.Text = "Text Report";
      this.btnTextReport.UseVisualStyleBackColor = true;
      this.btnTextReport.Click += new System.EventHandler(this.btnTextReport_Click);
      //
      // frmMetricData
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(794, 675);
      this.Controls.Add(this.lblData);
      this.Controls.Add(this.btnTextReport);
      this.Controls.Add(this.btnCommaSeparated);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.txtGraphData);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMetricData";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Graph Data Points";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtGraphData;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Label lblData;
    private System.Windows.Forms.Button btnCommaSeparated;
    private System.Windows.Forms.Button btnTextReport;
  }
}