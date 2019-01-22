namespace Org.PdfExtractToolWindows
{
  partial class TreeViewTextDocStructure
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewTextDocStructure));
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvDocStructure = new System.Windows.Forms.TreeView();
      this.txtDoc = new FastColoredTextBoxNS.FastColoredTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtDoc)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlBottom
      // 
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 499);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(797, 33);
      this.pnlBottom.TabIndex = 1;
      // 
      // splitterMain
      // 
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Name = "splitterMain";
      // 
      // splitterMain.Panel1
      // 
      this.splitterMain.Panel1.Controls.Add(this.tvDocStructure);
      // 
      // splitterMain.Panel2
      // 
      this.splitterMain.Panel2.Controls.Add(this.txtDoc);
      this.splitterMain.Size = new System.Drawing.Size(797, 499);
      this.splitterMain.SplitterDistance = 220;
      this.splitterMain.SplitterWidth = 3;
      this.splitterMain.TabIndex = 2;
      // 
      // tvDocStructure
      // 
      this.tvDocStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvDocStructure.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvDocStructure.Location = new System.Drawing.Point(0, 0);
      this.tvDocStructure.Name = "tvDocStructure";
      this.tvDocStructure.Size = new System.Drawing.Size(220, 499);
      this.tvDocStructure.TabIndex = 0;
      // 
      // txtDoc
      // 
      this.txtDoc.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.txtDoc.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtDoc.BackBrush = null;
      this.txtDoc.CharHeight = 13;
      this.txtDoc.CharWidth = 7;
      this.txtDoc.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtDoc.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtDoc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDoc.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtDoc.IsReplaceMode = false;
      this.txtDoc.Location = new System.Drawing.Point(0, 0);
      this.txtDoc.Name = "txtDoc";
      this.txtDoc.Paddings = new System.Windows.Forms.Padding(0);
      this.txtDoc.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtDoc.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtDoc.ServiceColors")));
      this.txtDoc.Size = new System.Drawing.Size(574, 499);
      this.txtDoc.TabIndex = 0;
      this.txtDoc.Zoom = 100;
      // 
      // TreeViewTextDocStructure
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlBottom);
      this.Name = "TreeViewTextDocStructure";
      this.Size = new System.Drawing.Size(797, 532);
      this.Tag = "ToolPanel_TreeViewTextDocStructure";
      this.Controls.SetChildIndex(this.pnlBottom, 0);
      this.Controls.SetChildIndex(this.splitterMain, 0);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtDoc)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvDocStructure;
    private FastColoredTextBoxNS.FastColoredTextBox txtDoc;
  }
}
