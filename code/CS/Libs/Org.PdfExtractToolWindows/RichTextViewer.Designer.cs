namespace Org.PdfExtractToolWindows
{
  partial class RichTextViewer
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextViewer));
      this.txtData = new FastColoredTextBoxNS.FastColoredTextBox();
      this.pnlRichTextViewerTop = new System.Windows.Forms.Panel();
      this.txtFilters = new System.Windows.Forms.TextBox();
      this.btnRunFilter = new System.Windows.Forms.Button();
      this.lblTextFilter = new System.Windows.Forms.Label();
      this.ckUseDynamicFiltering = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.txtData)).BeginInit();
      this.pnlRichTextViewerTop.SuspendLayout();
      this.SuspendLayout();
      //
      // txtData
      //
      this.txtData.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''
      };
      this.txtData.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtData.BackBrush = null;
      this.txtData.CharHeight = 13;
      this.txtData.CharWidth = 7;
      this.txtData.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtData.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtData.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtData.IsReplaceMode = false;
      this.txtData.Location = new System.Drawing.Point(0, 56);
      this.txtData.Margin = new System.Windows.Forms.Padding(2);
      this.txtData.Name = "txtData";
      this.txtData.Paddings = new System.Windows.Forms.Padding(0);
      this.txtData.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtData.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtData.ServiceColors")));
      this.txtData.Size = new System.Drawing.Size(647, 438);
      this.txtData.TabIndex = 4;
      this.txtData.Tag = "";
      this.txtData.Zoom = 100;
      //
      // pnlRichTextViewerTop
      //
      this.pnlRichTextViewerTop.Controls.Add(this.ckUseDynamicFiltering);
      this.pnlRichTextViewerTop.Controls.Add(this.lblTextFilter);
      this.pnlRichTextViewerTop.Controls.Add(this.btnRunFilter);
      this.pnlRichTextViewerTop.Controls.Add(this.txtFilters);
      this.pnlRichTextViewerTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlRichTextViewerTop.Location = new System.Drawing.Point(0, 26);
      this.pnlRichTextViewerTop.Name = "pnlRichTextViewerTop";
      this.pnlRichTextViewerTop.Size = new System.Drawing.Size(647, 30);
      this.pnlRichTextViewerTop.TabIndex = 5;
      //
      // txtFilters
      //
      this.txtFilters.Location = new System.Drawing.Point(117, 2);
      this.txtFilters.Name = "txtFilters";
      this.txtFilters.Size = new System.Drawing.Size(310, 20);
      this.txtFilters.TabIndex = 0;
      this.txtFilters.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
      //
      // btnRunFilter
      //
      this.btnRunFilter.Location = new System.Drawing.Point(433, 1);
      this.btnRunFilter.Name = "btnRunFilter";
      this.btnRunFilter.Size = new System.Drawing.Size(76, 21);
      this.btnRunFilter.TabIndex = 1;
      this.btnRunFilter.Tag = "RunFilters";
      this.btnRunFilter.Text = "Run Filters";
      this.btnRunFilter.UseVisualStyleBackColor = true;
      this.btnRunFilter.Click += new System.EventHandler(this.Action);
      //
      // lblTextFilter
      //
      this.lblTextFilter.AutoSize = true;
      this.lblTextFilter.Location = new System.Drawing.Point(22, 6);
      this.lblTextFilter.Name = "lblTextFilter";
      this.lblTextFilter.Size = new System.Drawing.Size(94, 13);
      this.lblTextFilter.TabIndex = 2;
      this.lblTextFilter.Text = "Text Match Filters:";
      //
      // ckUseDynamicFiltering
      //
      this.ckUseDynamicFiltering.AutoSize = true;
      this.ckUseDynamicFiltering.Location = new System.Drawing.Point(516, 4);
      this.ckUseDynamicFiltering.Name = "ckUseDynamicFiltering";
      this.ckUseDynamicFiltering.Size = new System.Drawing.Size(128, 17);
      this.ckUseDynamicFiltering.TabIndex = 3;
      this.ckUseDynamicFiltering.Text = "Use Dynamic Filtering";
      this.ckUseDynamicFiltering.UseVisualStyleBackColor = true;
      this.ckUseDynamicFiltering.CheckedChanged += new System.EventHandler(this.ckUseDynamicFiltering_CheckedChanged);
      //
      // RichTextViewer
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtData);
      this.Controls.Add(this.pnlRichTextViewerTop);
      this.Name = "RichTextViewer";
      this.Size = new System.Drawing.Size(647, 494);
      this.Tag = "ToolPanel_RichTextViewer";
      this.Controls.SetChildIndex(this.pnlRichTextViewerTop, 0);
      this.Controls.SetChildIndex(this.txtData, 0);
      ((System.ComponentModel.ISupportInitialize)(this.txtData)).EndInit();
      this.pnlRichTextViewerTop.ResumeLayout(false);
      this.pnlRichTextViewerTop.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private FastColoredTextBoxNS.FastColoredTextBox txtData;
    private System.Windows.Forms.Panel pnlRichTextViewerTop;
    private System.Windows.Forms.Label lblTextFilter;
    private System.Windows.Forms.Button btnRunFilter;
    private System.Windows.Forms.TextBox txtFilters;
    private System.Windows.Forms.CheckBox ckUseDynamicFiltering;
  }
}
