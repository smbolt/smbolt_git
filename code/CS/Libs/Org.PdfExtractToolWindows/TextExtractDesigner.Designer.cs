namespace Org.PdfExtractToolWindows
{
  partial class TextExtractDesigner
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
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvDocStructure = new System.Windows.Forms.TreeView();
      this.ctxMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuTreeViewShowAllNodes = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuTreeViewShowOnlyThisNodeType = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuTreeViewEditConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuTreeViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
      this.pnlTreeViewTop = new System.Windows.Forms.Panel();
      this.splitterRight = new System.Windows.Forms.SplitContainer();
      this.txtTextValue = new FastColoredTextBoxNS.FastColoredTextBox();
      this.splitterRightBottom = new System.Windows.Forms.SplitContainer();
      this.txtConfig = new FastColoredTextBoxNS.FastColoredTextBox();
      this.pnlConfigTop = new System.Windows.Forms.Panel();
      this.txtExtract = new FastColoredTextBoxNS.FastColoredTextBox();
      this.pnlExtractTop = new System.Windows.Forms.Panel();
      this.pnlDesignerMainTop = new System.Windows.Forms.Panel();
      this.lblTextStructure = new System.Windows.Forms.Label();
      this.pnlTextExtractDesignerTop = new System.Windows.Forms.Panel();
      this.ckRunExtract = new System.Windows.Forms.CheckBox();
      this.btnInit = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnStep = new System.Windows.Forms.Button();
      this.btnWorkHere = new System.Windows.Forms.Button();
      this.btnReprocess = new System.Windows.Forms.Button();
      this.pnlBackground = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.ctxMenuTreeView.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterRight)).BeginInit();
      this.splitterRight.Panel1.SuspendLayout();
      this.splitterRight.Panel2.SuspendLayout();
      this.splitterRight.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtTextValue)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitterRightBottom)).BeginInit();
      this.splitterRightBottom.Panel1.SuspendLayout();
      this.splitterRightBottom.Panel2.SuspendLayout();
      this.splitterRightBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtConfig)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtExtract)).BeginInit();
      this.pnlDesignerMainTop.SuspendLayout();
      this.pnlTextExtractDesignerTop.SuspendLayout();
      this.pnlBackground.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlBottom
      //
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 1197);
      this.pnlBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(1866, 15);
      this.pnlBottom.TabIndex = 1;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvDocStructure);
      this.splitterMain.Panel1.Controls.Add(this.pnlTreeViewTop);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.splitterRight);
      this.splitterMain.Panel2.Controls.Add(this.pnlDesignerMainTop);
      this.splitterMain.Size = new System.Drawing.Size(1860, 1111);
      this.splitterMain.SplitterDistance = 251;
      this.splitterMain.TabIndex = 2;
      //
      // tvDocStructure
      //
      this.tvDocStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvDocStructure.ContextMenuStrip = this.ctxMenuTreeView;
      this.tvDocStructure.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvDocStructure.ImageIndex = 0;
      this.tvDocStructure.ImageList = this.imgListTreeView;
      this.tvDocStructure.Location = new System.Drawing.Point(0, 62);
      this.tvDocStructure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tvDocStructure.Name = "tvDocStructure";
      this.tvDocStructure.SelectedImageIndex = 0;
      this.tvDocStructure.Size = new System.Drawing.Size(249, 1047);
      this.tvDocStructure.TabIndex = 0;
      this.tvDocStructure.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvDocStructure_BeforeSelect);
      this.tvDocStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDocStructure_AfterSelect);
      this.tvDocStructure.Click += new System.EventHandler(this.tvDocStructure_Click);
      //
      // ctxMenuTreeView
      //
      this.ctxMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuTreeViewShowAllNodes,
        this.ctxMenuTreeViewShowOnlyThisNodeType,
        this.ctxMenuTreeViewEditConfig,
        this.ctxMenuTreeViewRefresh
      });
      this.ctxMenuTreeView.Name = "ctxMenuTreeView";
      this.ctxMenuTreeView.Size = new System.Drawing.Size(217, 114);
      //
      // ctxMenuTreeViewShowAllNodes
      //
      this.ctxMenuTreeViewShowAllNodes.Name = "ctxMenuTreeViewShowAllNodes";
      this.ctxMenuTreeViewShowAllNodes.Size = new System.Drawing.Size(216, 22);
      this.ctxMenuTreeViewShowAllNodes.Tag = "ShowAllNodes";
      this.ctxMenuTreeViewShowAllNodes.Text = "Show &All Nodes";
      this.ctxMenuTreeViewShowAllNodes.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuTreeViewShowOnlyThisNodeType
      //
      this.ctxMenuTreeViewShowOnlyThisNodeType.Name = "ctxMenuTreeViewShowOnlyThisNodeType";
      this.ctxMenuTreeViewShowOnlyThisNodeType.Size = new System.Drawing.Size(216, 22);
      this.ctxMenuTreeViewShowOnlyThisNodeType.Tag = "ShowOnlyThisNodeType";
      this.ctxMenuTreeViewShowOnlyThisNodeType.Text = "Show Only This Node Type";
      this.ctxMenuTreeViewShowOnlyThisNodeType.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuTreeViewEditConfig
      //
      this.ctxMenuTreeViewEditConfig.Name = "ctxMenuTreeViewEditConfig";
      this.ctxMenuTreeViewEditConfig.Size = new System.Drawing.Size(216, 22);
      this.ctxMenuTreeViewEditConfig.Tag = "EditConfig";
      this.ctxMenuTreeViewEditConfig.Text = "&Edit Config";
      this.ctxMenuTreeViewEditConfig.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuTreeViewRefresh
      //
      this.ctxMenuTreeViewRefresh.Name = "ctxMenuTreeViewRefresh";
      this.ctxMenuTreeViewRefresh.Size = new System.Drawing.Size(216, 22);
      this.ctxMenuTreeViewRefresh.Tag = "Refresh";
      this.ctxMenuTreeViewRefresh.Text = "&Refresh";
      this.ctxMenuTreeViewRefresh.Click += new System.EventHandler(this.Action);
      //
      // imgListTreeView
      //
      this.imgListTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTreeView.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
      //
      // pnlTreeViewTop
      //
      this.pnlTreeViewTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTreeViewTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTreeViewTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTreeViewTop.Name = "pnlTreeViewTop";
      this.pnlTreeViewTop.Size = new System.Drawing.Size(249, 62);
      this.pnlTreeViewTop.TabIndex = 3;
      //
      // splitterRight
      //
      this.splitterRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterRight.Location = new System.Drawing.Point(0, 62);
      this.splitterRight.Name = "splitterRight";
      this.splitterRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterRight.Panel1
      //
      this.splitterRight.Panel1.Controls.Add(this.txtTextValue);
      //
      // splitterRight.Panel2
      //
      this.splitterRight.Panel2.Controls.Add(this.splitterRightBottom);
      this.splitterRight.Size = new System.Drawing.Size(1603, 1047);
      this.splitterRight.SplitterDistance = 523;
      this.splitterRight.TabIndex = 5;
      //
      // txtTextValue
      //
      this.txtTextValue.AutoCompleteBracketsList = new char[] {
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
      this.txtTextValue.AutoScrollMinSize = new System.Drawing.Size(53, 13);
      this.txtTextValue.BackBrush = null;
      this.txtTextValue.CharHeight = 13;
      this.txtTextValue.CharWidth = 7;
      this.txtTextValue.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtTextValue.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtTextValue.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtTextValue.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtTextValue.IsReplaceMode = false;
      this.txtTextValue.LineNumberStartValue = ((uint)(10001u));
      this.txtTextValue.Location = new System.Drawing.Point(0, 0);
      this.txtTextValue.Name = "txtTextValue";
      this.txtTextValue.Paddings = new System.Windows.Forms.Padding(0);
      this.txtTextValue.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtTextValue.ServiceColors = null;
      this.txtTextValue.Size = new System.Drawing.Size(1603, 523);
      this.txtTextValue.TabIndex = 4;
      this.txtTextValue.Zoom = 100;
      //
      // splitterRightBottom
      //
      this.splitterRightBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterRightBottom.Location = new System.Drawing.Point(0, 0);
      this.splitterRightBottom.Name = "splitterRightBottom";
      this.splitterRightBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterRightBottom.Panel1
      //
      this.splitterRightBottom.Panel1.Controls.Add(this.txtConfig);
      this.splitterRightBottom.Panel1.Controls.Add(this.pnlConfigTop);
      //
      // splitterRightBottom.Panel2
      //
      this.splitterRightBottom.Panel2.Controls.Add(this.txtExtract);
      this.splitterRightBottom.Panel2.Controls.Add(this.pnlExtractTop);
      this.splitterRightBottom.Size = new System.Drawing.Size(1603, 520);
      this.splitterRightBottom.SplitterDistance = 292;
      this.splitterRightBottom.TabIndex = 0;
      //
      // txtConfig
      //
      this.txtConfig.AutoCompleteBracketsList = new char[] {
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
      this.txtConfig.AutoScrollMinSize = new System.Drawing.Size(53, 13);
      this.txtConfig.BackBrush = null;
      this.txtConfig.CharHeight = 13;
      this.txtConfig.CharWidth = 7;
      this.txtConfig.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtConfig.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtConfig.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtConfig.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtConfig.IsReplaceMode = false;
      this.txtConfig.LineNumberStartValue = ((uint)(10001u));
      this.txtConfig.Location = new System.Drawing.Point(0, 28);
      this.txtConfig.Name = "txtConfig";
      this.txtConfig.Paddings = new System.Windows.Forms.Padding(0);
      this.txtConfig.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtConfig.ServiceColors = null;
      this.txtConfig.Size = new System.Drawing.Size(1603, 264);
      this.txtConfig.TabIndex = 5;
      this.txtConfig.Zoom = 100;
      //
      // pnlConfigTop
      //
      this.pnlConfigTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlConfigTop.Location = new System.Drawing.Point(0, 0);
      this.pnlConfigTop.Name = "pnlConfigTop";
      this.pnlConfigTop.Size = new System.Drawing.Size(1603, 28);
      this.pnlConfigTop.TabIndex = 0;
      //
      // txtExtract
      //
      this.txtExtract.AutoCompleteBracketsList = new char[] {
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
      this.txtExtract.AutoScrollMinSize = new System.Drawing.Size(2, 13);
      this.txtExtract.BackBrush = null;
      this.txtExtract.CharHeight = 13;
      this.txtExtract.CharWidth = 7;
      this.txtExtract.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtExtract.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtExtract.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtExtract.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtExtract.IsReplaceMode = false;
      this.txtExtract.LineNumberStartValue = ((uint)(10001u));
      this.txtExtract.Location = new System.Drawing.Point(0, 28);
      this.txtExtract.Name = "txtExtract";
      this.txtExtract.Paddings = new System.Windows.Forms.Padding(0);
      this.txtExtract.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtExtract.ServiceColors = null;
      this.txtExtract.Size = new System.Drawing.Size(1603, 196);
      this.txtExtract.TabIndex = 5;
      this.txtExtract.Zoom = 100;
      //
      // pnlExtractTop
      //
      this.pnlExtractTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlExtractTop.Location = new System.Drawing.Point(0, 0);
      this.pnlExtractTop.Name = "pnlExtractTop";
      this.pnlExtractTop.Size = new System.Drawing.Size(1603, 28);
      this.pnlExtractTop.TabIndex = 1;
      //
      // pnlDesignerMainTop
      //
      this.pnlDesignerMainTop.Controls.Add(this.lblTextStructure);
      this.pnlDesignerMainTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlDesignerMainTop.Location = new System.Drawing.Point(0, 0);
      this.pnlDesignerMainTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlDesignerMainTop.Name = "pnlDesignerMainTop";
      this.pnlDesignerMainTop.Size = new System.Drawing.Size(1603, 62);
      this.pnlDesignerMainTop.TabIndex = 3;
      //
      // lblTextStructure
      //
      this.lblTextStructure.AutoSize = true;
      this.lblTextStructure.Location = new System.Drawing.Point(26, 20);
      this.lblTextStructure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblTextStructure.Name = "lblTextStructure";
      this.lblTextStructure.Size = new System.Drawing.Size(109, 20);
      this.lblTextStructure.TabIndex = 0;
      this.lblTextStructure.Text = "Text Structure";
      //
      // pnlTextExtractDesignerTop
      //
      this.pnlTextExtractDesignerTop.Controls.Add(this.ckRunExtract);
      this.pnlTextExtractDesignerTop.Controls.Add(this.btnInit);
      this.pnlTextExtractDesignerTop.Controls.Add(this.btnRun);
      this.pnlTextExtractDesignerTop.Controls.Add(this.btnStep);
      this.pnlTextExtractDesignerTop.Controls.Add(this.btnWorkHere);
      this.pnlTextExtractDesignerTop.Controls.Add(this.btnReprocess);
      this.pnlTextExtractDesignerTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTextExtractDesignerTop.Location = new System.Drawing.Point(0, 40);
      this.pnlTextExtractDesignerTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTextExtractDesignerTop.Name = "pnlTextExtractDesignerTop";
      this.pnlTextExtractDesignerTop.Size = new System.Drawing.Size(1866, 46);
      this.pnlTextExtractDesignerTop.TabIndex = 3;
      //
      // ckRunExtract
      //
      this.ckRunExtract.AutoSize = true;
      this.ckRunExtract.Location = new System.Drawing.Point(873, 11);
      this.ckRunExtract.Name = "ckRunExtract";
      this.ckRunExtract.Size = new System.Drawing.Size(112, 24);
      this.ckRunExtract.TabIndex = 2;
      this.ckRunExtract.Text = "Run Extract";
      this.ckRunExtract.UseVisualStyleBackColor = true;
      //
      // btnInit
      //
      this.btnInit.Location = new System.Drawing.Point(530, 6);
      this.btnInit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnInit.Name = "btnInit";
      this.btnInit.Size = new System.Drawing.Size(99, 35);
      this.btnInit.TabIndex = 1;
      this.btnInit.Tag = "Init";
      this.btnInit.Text = "Init";
      this.btnInit.UseVisualStyleBackColor = true;
      this.btnInit.Click += new System.EventHandler(this.Action);
      //
      // btnRun
      //
      this.btnRun.Location = new System.Drawing.Point(736, 6);
      this.btnRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(99, 35);
      this.btnRun.TabIndex = 1;
      this.btnRun.Tag = "Run";
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      //
      // btnStep
      //
      this.btnStep.Location = new System.Drawing.Point(633, 6);
      this.btnStep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnStep.Name = "btnStep";
      this.btnStep.Size = new System.Drawing.Size(99, 35);
      this.btnStep.TabIndex = 1;
      this.btnStep.Tag = "Step";
      this.btnStep.Text = "Step";
      this.btnStep.UseVisualStyleBackColor = true;
      this.btnStep.Click += new System.EventHandler(this.Action);
      //
      // btnWorkHere
      //
      this.btnWorkHere.Location = new System.Drawing.Point(281, 5);
      this.btnWorkHere.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnWorkHere.Name = "btnWorkHere";
      this.btnWorkHere.Size = new System.Drawing.Size(166, 35);
      this.btnWorkHere.TabIndex = 1;
      this.btnWorkHere.Tag = "WorkHere";
      this.btnWorkHere.Text = "Work Here";
      this.btnWorkHere.UseVisualStyleBackColor = true;
      this.btnWorkHere.Click += new System.EventHandler(this.Action);
      //
      // btnReprocess
      //
      this.btnReprocess.Location = new System.Drawing.Point(14, 5);
      this.btnReprocess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnReprocess.Name = "btnReprocess";
      this.btnReprocess.Size = new System.Drawing.Size(166, 35);
      this.btnReprocess.TabIndex = 0;
      this.btnReprocess.Tag = "Reprocess";
      this.btnReprocess.Text = "Reprocess";
      this.btnReprocess.UseVisualStyleBackColor = true;
      this.btnReprocess.Click += new System.EventHandler(this.Action);
      //
      // pnlBackground
      //
      this.pnlBackground.Controls.Add(this.splitterMain);
      this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlBackground.Location = new System.Drawing.Point(0, 86);
      this.pnlBackground.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlBackground.Name = "pnlBackground";
      this.pnlBackground.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
      this.pnlBackground.Size = new System.Drawing.Size(1866, 1111);
      this.pnlBackground.TabIndex = 4;
      //
      // TextExtractDesigner
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlBackground);
      this.Controls.Add(this.pnlTextExtractDesignerTop);
      this.Controls.Add(this.pnlBottom);
      this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
      this.Name = "TextExtractDesigner";
      this.Size = new System.Drawing.Size(1866, 1212);
      this.Tag = "ToolPanel_TextExtractDesigner";
      this.Controls.SetChildIndex(this.pnlBottom, 0);
      this.Controls.SetChildIndex(this.pnlTextExtractDesignerTop, 0);
      this.Controls.SetChildIndex(this.pnlBackground, 0);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.ctxMenuTreeView.ResumeLayout(false);
      this.splitterRight.Panel1.ResumeLayout(false);
      this.splitterRight.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterRight)).EndInit();
      this.splitterRight.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtTextValue)).EndInit();
      this.splitterRightBottom.Panel1.ResumeLayout(false);
      this.splitterRightBottom.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterRightBottom)).EndInit();
      this.splitterRightBottom.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtConfig)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtExtract)).EndInit();
      this.pnlDesignerMainTop.ResumeLayout(false);
      this.pnlDesignerMainTop.PerformLayout();
      this.pnlTextExtractDesignerTop.ResumeLayout(false);
      this.pnlTextExtractDesignerTop.PerformLayout();
      this.pnlBackground.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvDocStructure;
    private System.Windows.Forms.Panel pnlTreeViewTop;
    private System.Windows.Forms.Panel pnlTextExtractDesignerTop;
    private System.Windows.Forms.Panel pnlBackground;
    private System.Windows.Forms.ImageList imgListTreeView;
    private System.Windows.Forms.Button btnReprocess;
    private System.Windows.Forms.ContextMenuStrip ctxMenuTreeView;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewEditConfig;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewRefresh;
    private System.Windows.Forms.Panel pnlDesignerMainTop;
    private FastColoredTextBoxNS.FastColoredTextBox txtTextValue;
    private System.Windows.Forms.Label lblTextStructure;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewShowAllNodes;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewShowOnlyThisNodeType;
    private System.Windows.Forms.SplitContainer splitterRight;
    private System.Windows.Forms.SplitContainer splitterRightBottom;
    private FastColoredTextBoxNS.FastColoredTextBox txtConfig;
    private System.Windows.Forms.Panel pnlConfigTop;
    private FastColoredTextBoxNS.FastColoredTextBox txtExtract;
    private System.Windows.Forms.Panel pnlExtractTop;
    private System.Windows.Forms.Button btnWorkHere;
    private System.Windows.Forms.Button btnStep;
    private System.Windows.Forms.Button btnInit;
    private System.Windows.Forms.CheckBox ckRunExtract;
    private System.Windows.Forms.Button btnRun;
  }
}
