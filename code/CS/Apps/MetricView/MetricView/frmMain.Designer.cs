namespace Teleflora.Operations.MetricView
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
      this.components = new System.ComponentModel.Container();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileNewProfile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileOpenProfile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSaveProfileAs = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileCloseProfile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuGraphs = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsAddGraph = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsActivateAll = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsDeactivateAll = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsPasteGraphFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsAutoArrange = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsAutoArrangeOption1 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsAutoArrangeOption2 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsAutoArrangeOption3 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsReOrder = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsImportGraphFromFile = new System.Windows.Forms.ToolStripMenuItem();
      this.sbMain = new System.Windows.Forms.StatusStrip();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblGraphLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblGraphData = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblMetricData = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblTimeData = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.sbLblValueData = new System.Windows.Forms.ToolStripStatusLabel();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlContent = new System.Windows.Forms.Panel();
      this.pnlControl = new System.Windows.Forms.Panel();
      this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
      this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
      this.timerControl = new System.Windows.Forms.Timer(this.components);
      this.mnuMain.SuspendLayout();
      this.sbMain.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuGraphs
      });
      this.mnuMain.Location = new System.Drawing.Point(2, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1137, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileNewProfile,
        this.mnuFileOpenProfile,
        this.mnuFileSave,
        this.mnuFileSaveProfileAs,
        this.mnuFileCloseProfile,
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(35, 20);
      this.mnuFile.Text = "&File";
      //
      // mnuFileNewProfile
      //
      this.mnuFileNewProfile.Name = "mnuFileNewProfile";
      this.mnuFileNewProfile.Size = new System.Drawing.Size(168, 22);
      this.mnuFileNewProfile.Text = "&New Profile";
      this.mnuFileNewProfile.Click += new System.EventHandler(this.mnuFileNewProfile_Click);
      //
      // mnuFileOpenProfile
      //
      this.mnuFileOpenProfile.Name = "mnuFileOpenProfile";
      this.mnuFileOpenProfile.Size = new System.Drawing.Size(168, 22);
      this.mnuFileOpenProfile.Text = "&Open Profile...";
      this.mnuFileOpenProfile.Click += new System.EventHandler(this.mnuFileOpenProfile_Click);
      //
      // mnuFileSave
      //
      this.mnuFileSave.Name = "mnuFileSave";
      this.mnuFileSave.Size = new System.Drawing.Size(168, 22);
      this.mnuFileSave.Text = "&Save Profile";
      this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
      //
      // mnuFileSaveProfileAs
      //
      this.mnuFileSaveProfileAs.Name = "mnuFileSaveProfileAs";
      this.mnuFileSaveProfileAs.Size = new System.Drawing.Size(168, 22);
      this.mnuFileSaveProfileAs.Text = "Save Profile &as...";
      this.mnuFileSaveProfileAs.Click += new System.EventHandler(this.mnuFileSaveProfileAs_Click);
      //
      // mnuFileCloseProfile
      //
      this.mnuFileCloseProfile.Name = "mnuFileCloseProfile";
      this.mnuFileCloseProfile.Size = new System.Drawing.Size(168, 22);
      this.mnuFileCloseProfile.Text = "&Close Profile";
      this.mnuFileCloseProfile.Click += new System.EventHandler(this.mnuFileCloseProfile_Click);
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(168, 22);
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
      //
      // mnuGraphs
      //
      this.mnuGraphs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuOptionsAddGraph,
        this.mnuOptionsActivateAll,
        this.mnuOptionsDeactivateAll,
        this.mnuOptionsPasteGraphFromClipboard,
        this.mnuOptionsAutoArrange,
        this.mnuOptionsReOrder,
        this.mnuOptionsImportGraphFromFile
      });
      this.mnuGraphs.Name = "mnuGraphs";
      this.mnuGraphs.Size = new System.Drawing.Size(53, 20);
      this.mnuGraphs.Text = "&Graphs";
      this.mnuGraphs.DropDownOpening += new System.EventHandler(this.mnuGraphs_DropDownOpening);
      //
      // mnuOptionsAddGraph
      //
      this.mnuOptionsAddGraph.Name = "mnuOptionsAddGraph";
      this.mnuOptionsAddGraph.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsAddGraph.Text = "Add &Graph";
      this.mnuOptionsAddGraph.Click += new System.EventHandler(this.mnuOptionsAddGraph_Click);
      //
      // mnuOptionsActivateAll
      //
      this.mnuOptionsActivateAll.Name = "mnuOptionsActivateAll";
      this.mnuOptionsActivateAll.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsActivateAll.Text = "&Activate All Graphs";
      this.mnuOptionsActivateAll.Click += new System.EventHandler(this.mnuOptionsActivateAll_Click);
      //
      // mnuOptionsDeactivateAll
      //
      this.mnuOptionsDeactivateAll.Name = "mnuOptionsDeactivateAll";
      this.mnuOptionsDeactivateAll.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsDeactivateAll.Text = "&Deactivate All Graphs";
      this.mnuOptionsDeactivateAll.Click += new System.EventHandler(this.mnuOptionsDeactivateAll_Click);
      //
      // mnuOptionsPasteGraphFromClipboard
      //
      this.mnuOptionsPasteGraphFromClipboard.Name = "mnuOptionsPasteGraphFromClipboard";
      this.mnuOptionsPasteGraphFromClipboard.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsPasteGraphFromClipboard.Text = "&Paste Graph from Clipboard";
      this.mnuOptionsPasteGraphFromClipboard.Click += new System.EventHandler(this.mnuOptionsPasteGraphFromClipboard_Click);
      //
      // mnuOptionsAutoArrange
      //
      this.mnuOptionsAutoArrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuOptionsAutoArrangeOption1,
        this.mnuOptionsAutoArrangeOption2,
        this.mnuOptionsAutoArrangeOption3
      });
      this.mnuOptionsAutoArrange.Name = "mnuOptionsAutoArrange";
      this.mnuOptionsAutoArrange.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsAutoArrange.Text = "Auto Arra&nge";
      this.mnuOptionsAutoArrange.DropDownOpening += new System.EventHandler(this.mnuOptionsAutoArrange_DropDownOpening);
      //
      // mnuOptionsAutoArrangeOption1
      //
      this.mnuOptionsAutoArrangeOption1.Name = "mnuOptionsAutoArrangeOption1";
      this.mnuOptionsAutoArrangeOption1.Size = new System.Drawing.Size(123, 22);
      this.mnuOptionsAutoArrangeOption1.Tag = "1";
      this.mnuOptionsAutoArrangeOption1.Text = "Option1";
      this.mnuOptionsAutoArrangeOption1.Click += new System.EventHandler(this.mnuOptionsAutoArrange_Click);
      //
      // mnuOptionsAutoArrangeOption2
      //
      this.mnuOptionsAutoArrangeOption2.Name = "mnuOptionsAutoArrangeOption2";
      this.mnuOptionsAutoArrangeOption2.Size = new System.Drawing.Size(123, 22);
      this.mnuOptionsAutoArrangeOption2.Tag = "2";
      this.mnuOptionsAutoArrangeOption2.Text = "Option2";
      this.mnuOptionsAutoArrangeOption2.Click += new System.EventHandler(this.mnuOptionsAutoArrange_Click);
      //
      // mnuOptionsAutoArrangeOption3
      //
      this.mnuOptionsAutoArrangeOption3.Name = "mnuOptionsAutoArrangeOption3";
      this.mnuOptionsAutoArrangeOption3.Size = new System.Drawing.Size(123, 22);
      this.mnuOptionsAutoArrangeOption3.Tag = "3";
      this.mnuOptionsAutoArrangeOption3.Text = "Option3";
      this.mnuOptionsAutoArrangeOption3.Click += new System.EventHandler(this.mnuOptionsAutoArrange_Click);
      //
      // mnuOptionsReOrder
      //
      this.mnuOptionsReOrder.Name = "mnuOptionsReOrder";
      this.mnuOptionsReOrder.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsReOrder.Text = "&Re-Order Graphs";
      this.mnuOptionsReOrder.Click += new System.EventHandler(this.mnuOptionsReOrder_Click);
      //
      // mnuOptionsImportGraphFromFile
      //
      this.mnuOptionsImportGraphFromFile.Name = "mnuOptionsImportGraphFromFile";
      this.mnuOptionsImportGraphFromFile.Size = new System.Drawing.Size(217, 22);
      this.mnuOptionsImportGraphFromFile.Text = "&Import Graph from File";
      this.mnuOptionsImportGraphFromFile.Click += new System.EventHandler(this.mnuOptionsImportGraphFromFile_Click);
      //
      // sbMain
      //
      this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.lblStatus,
        this.sbLblGraphLabel,
        this.toolStripStatusLabel2,
        this.sbLblGraphData,
        this.toolStripStatusLabel1,
        this.sbLblMetricData,
        this.sbLblTimeLabel,
        this.sbLblTimeData,
        this.sbLblValueLabel,
        this.sbLblValueData
      });
      this.sbMain.Location = new System.Drawing.Point(2, 713);
      this.sbMain.Name = "sbMain";
      this.sbMain.Size = new System.Drawing.Size(1137, 22);
      this.sbMain.TabIndex = 1;
      this.sbMain.Text = "statusStrip1";
      //
      // lblStatus
      //
      this.lblStatus.AutoSize = false;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(360, 17);
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // sbLblGraphLabel
      //
      this.sbLblGraphLabel.AutoSize = false;
      this.sbLblGraphLabel.Name = "sbLblGraphLabel";
      this.sbLblGraphLabel.Size = new System.Drawing.Size(50, 17);
      this.sbLblGraphLabel.Text = "Graph:";
      this.sbLblGraphLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // toolStripStatusLabel2
      //
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
      //
      // sbLblGraphData
      //
      this.sbLblGraphData.AutoSize = false;
      this.sbLblGraphData.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                                         | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                                         | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.sbLblGraphData.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.sbLblGraphData.Name = "sbLblGraphData";
      this.sbLblGraphData.Size = new System.Drawing.Size(200, 17);
      this.sbLblGraphData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // toolStripStatusLabel1
      //
      this.toolStripStatusLabel1.AutoSize = false;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 17);
      this.toolStripStatusLabel1.Text = "Metric:";
      this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // sbLblMetricData
      //
      this.sbLblMetricData.AutoSize = false;
      this.sbLblMetricData.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                                          | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                                          | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.sbLblMetricData.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.sbLblMetricData.Name = "sbLblMetricData";
      this.sbLblMetricData.Size = new System.Drawing.Size(220, 17);
      this.sbLblMetricData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // sbLblTimeLabel
      //
      this.sbLblTimeLabel.AutoSize = false;
      this.sbLblTimeLabel.Name = "sbLblTimeLabel";
      this.sbLblTimeLabel.Size = new System.Drawing.Size(45, 17);
      this.sbLblTimeLabel.Text = "Time:";
      this.sbLblTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // sbLblTimeData
      //
      this.sbLblTimeData.AutoSize = false;
      this.sbLblTimeData.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.sbLblTimeData.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.sbLblTimeData.Name = "sbLblTimeData";
      this.sbLblTimeData.Size = new System.Drawing.Size(70, 17);
      this.sbLblTimeData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // sbLblValueLabel
      //
      this.sbLblValueLabel.AutoSize = false;
      this.sbLblValueLabel.Name = "sbLblValueLabel";
      this.sbLblValueLabel.Size = new System.Drawing.Size(45, 17);
      this.sbLblValueLabel.Text = "Value:";
      this.sbLblValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // sbLblValueData
      //
      this.sbLblValueData.AutoSize = false;
      this.sbLblValueData.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                                         | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                                         | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.sbLblValueData.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.sbLblValueData.Name = "sbLblValueData";
      this.sbLblValueData.Size = new System.Drawing.Size(109, 17);
      this.sbLblValueData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.pnlContent);
      this.pnlMain.Controls.Add(this.pnlControl);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(2, 24);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1137, 689);
      this.pnlMain.TabIndex = 2;
      //
      // pnlContent
      //
      this.pnlContent.BackColor = System.Drawing.Color.LightSteelBlue;
      this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlContent.Location = new System.Drawing.Point(181, 0);
      this.pnlContent.Name = "pnlContent";
      this.pnlContent.Size = new System.Drawing.Size(956, 689);
      this.pnlContent.TabIndex = 1;
      this.pnlContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlContent_MouseDoubleClick);
      this.pnlContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlContent_MouseDown);
      //
      // pnlControl
      //
      this.pnlControl.BackColor = System.Drawing.Color.SteelBlue;
      this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlControl.Dock = System.Windows.Forms.DockStyle.Left;
      this.pnlControl.Location = new System.Drawing.Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Size = new System.Drawing.Size(181, 689);
      this.pnlControl.TabIndex = 0;
      //
      // dlgOpenFile
      //
      this.dlgOpenFile.Filter = "MetricView Profiles (*.mvp)|*.mvp)";
      this.dlgOpenFile.InitialDirectory = "c:\\Program Files\\MetricView1.0";
      //
      // dlgSaveFile
      //
      this.dlgSaveFile.Filter = "MetricView Profiles (*.mvp)|*.mvp";
      this.dlgSaveFile.InitialDirectory = "c:\\Program Files\\MetricView1.0";
      //
      // timerControl
      //
      this.timerControl.Interval = 1000;
      this.timerControl.Tick += new System.EventHandler(this.timerControl_Tick);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1141, 735);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.sbMain);
      this.Controls.Add(this.mnuMain);
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MetricView";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.sbMain.ResumeLayout(false);
      this.sbMain.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.StatusStrip sbMain;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Panel pnlControl;
    private System.Windows.Forms.ToolStripMenuItem mnuGraphs;
    private System.Windows.Forms.Panel pnlContent;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsAddGraph;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
    private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileOpenProfile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSaveProfileAs;
    private System.Windows.Forms.SaveFileDialog dlgSaveFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileCloseProfile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileNewProfile;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsActivateAll;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsDeactivateAll;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsPasteGraphFromClipboard;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsAutoArrange;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsAutoArrangeOption1;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsAutoArrangeOption2;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsAutoArrangeOption3;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsReOrder;
    private System.Windows.Forms.ToolStripStatusLabel sbLblGraphLabel;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel sbLblGraphData;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripStatusLabel sbLblMetricData;
    private System.Windows.Forms.ToolStripStatusLabel sbLblTimeLabel;
    private System.Windows.Forms.ToolStripStatusLabel sbLblTimeData;
    private System.Windows.Forms.ToolStripStatusLabel sbLblValueLabel;
    private System.Windows.Forms.ToolStripStatusLabel sbLblValueData;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsImportGraphFromFile;
    private System.Windows.Forms.Timer timerControl;
  }
}

