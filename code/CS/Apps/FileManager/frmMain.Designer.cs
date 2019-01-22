namespace Org.FileManager
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
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.gbFileSegmentation = new System.Windows.Forms.GroupBox();
      this.btnDesegmentFile = new System.Windows.Forms.Button();
      this.btnSegmentFile = new System.Windows.Forms.Button();
      this.gbFileLocations = new System.Windows.Forms.GroupBox();
      this.lblDesegmentationTargetValue = new System.Windows.Forms.Label();
      this.lblExtractTargetValue = new System.Windows.Forms.Label();
      this.lblSegmentationTargetValue = new System.Windows.Forms.Label();
      this.lblArchiveTargetValue = new System.Windows.Forms.Label();
      this.lblSegmentationSourceValue = new System.Windows.Forms.Label();
      this.lblArchiveSourceValue = new System.Windows.Forms.Label();
      this.lblDesegmentationTarget = new System.Windows.Forms.Label();
      this.lblExtractTarget = new System.Windows.Forms.Label();
      this.lblSegmentationTarget = new System.Windows.Forms.Label();
      this.lblSegmentationSource = new System.Windows.Forms.Label();
      this.lblArchiveTarget = new System.Windows.Forms.Label();
      this.lblArchiveSource = new System.Windows.Forms.Label();
      this.gbArchiveManagement = new System.Windows.Forms.GroupBox();
      this.btnExtractArchive = new System.Windows.Forms.Button();
      this.btnCreateArchive = new System.Windows.Forms.Button();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnCreateDummySegmentationData = new System.Windows.Forms.Button();
      this.cboFileSize = new System.Windows.Forms.ComboBox();
      this.lblFileSize = new System.Windows.Forms.Label();
      this.cboSegmentSize = new System.Windows.Forms.ComboBox();
      this.lblSegmentSize = new System.Windows.Forms.Label();
      this.pnlTop.SuspendLayout();
      this.gbFileSegmentation.SuspendLayout();
      this.gbFileLocations.SuspendLayout();
      this.gbArchiveManagement.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.SuspendLayout();
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 513);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1052, 23);
      this.lblStatus.TabIndex = 0;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.gbFileSegmentation);
      this.pnlTop.Controls.Add(this.gbFileLocations);
      this.pnlTop.Controls.Add(this.gbArchiveManagement);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1052, 223);
      this.pnlTop.TabIndex = 1;
      //
      // gbFileSegmentation
      //
      this.gbFileSegmentation.Controls.Add(this.lblSegmentSize);
      this.gbFileSegmentation.Controls.Add(this.lblFileSize);
      this.gbFileSegmentation.Controls.Add(this.cboSegmentSize);
      this.gbFileSegmentation.Controls.Add(this.cboFileSize);
      this.gbFileSegmentation.Controls.Add(this.btnCreateDummySegmentationData);
      this.gbFileSegmentation.Controls.Add(this.btnDesegmentFile);
      this.gbFileSegmentation.Controls.Add(this.btnSegmentFile);
      this.gbFileSegmentation.Location = new System.Drawing.Point(179, 6);
      this.gbFileSegmentation.Name = "gbFileSegmentation";
      this.gbFileSegmentation.Size = new System.Drawing.Size(160, 206);
      this.gbFileSegmentation.TabIndex = 0;
      this.gbFileSegmentation.TabStop = false;
      this.gbFileSegmentation.Text = "File Segmentation";
      //
      // btnDesegmentFile
      //
      this.btnDesegmentFile.Location = new System.Drawing.Point(19, 48);
      this.btnDesegmentFile.Name = "btnDesegmentFile";
      this.btnDesegmentFile.Size = new System.Drawing.Size(125, 23);
      this.btnDesegmentFile.TabIndex = 0;
      this.btnDesegmentFile.Tag = "DesegmentFile";
      this.btnDesegmentFile.Text = "Desegment File";
      this.btnDesegmentFile.UseVisualStyleBackColor = true;
      this.btnDesegmentFile.Click += new System.EventHandler(this.Action);
      //
      // btnSegmentFile
      //
      this.btnSegmentFile.Location = new System.Drawing.Point(19, 19);
      this.btnSegmentFile.Name = "btnSegmentFile";
      this.btnSegmentFile.Size = new System.Drawing.Size(125, 23);
      this.btnSegmentFile.TabIndex = 0;
      this.btnSegmentFile.Tag = "SegmentFile";
      this.btnSegmentFile.Text = "Segment File";
      this.btnSegmentFile.UseVisualStyleBackColor = true;
      this.btnSegmentFile.Click += new System.EventHandler(this.Action);
      //
      // gbFileLocations
      //
      this.gbFileLocations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                     | System.Windows.Forms.AnchorStyles.Right)));
      this.gbFileLocations.Controls.Add(this.lblDesegmentationTargetValue);
      this.gbFileLocations.Controls.Add(this.lblExtractTargetValue);
      this.gbFileLocations.Controls.Add(this.lblSegmentationTargetValue);
      this.gbFileLocations.Controls.Add(this.lblArchiveTargetValue);
      this.gbFileLocations.Controls.Add(this.lblSegmentationSourceValue);
      this.gbFileLocations.Controls.Add(this.lblArchiveSourceValue);
      this.gbFileLocations.Controls.Add(this.lblDesegmentationTarget);
      this.gbFileLocations.Controls.Add(this.lblExtractTarget);
      this.gbFileLocations.Controls.Add(this.lblSegmentationTarget);
      this.gbFileLocations.Controls.Add(this.lblSegmentationSource);
      this.gbFileLocations.Controls.Add(this.lblArchiveTarget);
      this.gbFileLocations.Controls.Add(this.lblArchiveSource);
      this.gbFileLocations.Location = new System.Drawing.Point(345, 6);
      this.gbFileLocations.Name = "gbFileLocations";
      this.gbFileLocations.Size = new System.Drawing.Size(645, 206);
      this.gbFileLocations.TabIndex = 0;
      this.gbFileLocations.TabStop = false;
      this.gbFileLocations.Text = "File Locations";
      //
      // lblDesegmentationTargetValue
      //
      this.lblDesegmentationTargetValue.AutoSize = true;
      this.lblDesegmentationTargetValue.Location = new System.Drawing.Point(136, 121);
      this.lblDesegmentationTargetValue.Name = "lblDesegmentationTargetValue";
      this.lblDesegmentationTargetValue.Size = new System.Drawing.Size(118, 13);
      this.lblDesegmentationTargetValue.TabIndex = 0;
      this.lblDesegmentationTargetValue.Text = "[desegmentation target]";
      //
      // lblExtractTargetValue
      //
      this.lblExtractTargetValue.AutoSize = true;
      this.lblExtractTargetValue.Location = new System.Drawing.Point(136, 56);
      this.lblExtractTargetValue.Name = "lblExtractTargetValue";
      this.lblExtractTargetValue.Size = new System.Drawing.Size(75, 13);
      this.lblExtractTargetValue.TabIndex = 0;
      this.lblExtractTargetValue.Text = "[extract target]";
      //
      // lblSegmentationTargetValue
      //
      this.lblSegmentationTargetValue.AutoSize = true;
      this.lblSegmentationTargetValue.Location = new System.Drawing.Point(136, 103);
      this.lblSegmentationTargetValue.Name = "lblSegmentationTargetValue";
      this.lblSegmentationTargetValue.Size = new System.Drawing.Size(106, 13);
      this.lblSegmentationTargetValue.TabIndex = 0;
      this.lblSegmentationTargetValue.Text = "[segmentation target]";
      //
      // lblArchiveTargetValue
      //
      this.lblArchiveTargetValue.AutoSize = true;
      this.lblArchiveTargetValue.Location = new System.Drawing.Point(136, 38);
      this.lblArchiveTargetValue.Name = "lblArchiveTargetValue";
      this.lblArchiveTargetValue.Size = new System.Drawing.Size(78, 13);
      this.lblArchiveTargetValue.TabIndex = 0;
      this.lblArchiveTargetValue.Text = "[archive target]";
      //
      // lblSegmentationSourceValue
      //
      this.lblSegmentationSourceValue.AutoSize = true;
      this.lblSegmentationSourceValue.Location = new System.Drawing.Point(136, 85);
      this.lblSegmentationSourceValue.Name = "lblSegmentationSourceValue";
      this.lblSegmentationSourceValue.Size = new System.Drawing.Size(111, 13);
      this.lblSegmentationSourceValue.TabIndex = 0;
      this.lblSegmentationSourceValue.Text = "[segmentation source]";
      //
      // lblArchiveSourceValue
      //
      this.lblArchiveSourceValue.AutoSize = true;
      this.lblArchiveSourceValue.Location = new System.Drawing.Point(136, 20);
      this.lblArchiveSourceValue.Name = "lblArchiveSourceValue";
      this.lblArchiveSourceValue.Size = new System.Drawing.Size(83, 13);
      this.lblArchiveSourceValue.TabIndex = 0;
      this.lblArchiveSourceValue.Text = "[archive source]";
      //
      // lblDesegmentationTarget
      //
      this.lblDesegmentationTarget.AutoSize = true;
      this.lblDesegmentationTarget.Location = new System.Drawing.Point(7, 121);
      this.lblDesegmentationTarget.Name = "lblDesegmentationTarget";
      this.lblDesegmentationTarget.Size = new System.Drawing.Size(121, 13);
      this.lblDesegmentationTarget.TabIndex = 0;
      this.lblDesegmentationTarget.Text = "Desegmentation Target:";
      //
      // lblExtractTarget
      //
      this.lblExtractTarget.AutoSize = true;
      this.lblExtractTarget.Location = new System.Drawing.Point(7, 56);
      this.lblExtractTarget.Name = "lblExtractTarget";
      this.lblExtractTarget.Size = new System.Drawing.Size(77, 13);
      this.lblExtractTarget.TabIndex = 0;
      this.lblExtractTarget.Text = "Extract Target:";
      //
      // lblSegmentationTarget
      //
      this.lblSegmentationTarget.AutoSize = true;
      this.lblSegmentationTarget.Location = new System.Drawing.Point(7, 103);
      this.lblSegmentationTarget.Name = "lblSegmentationTarget";
      this.lblSegmentationTarget.Size = new System.Drawing.Size(109, 13);
      this.lblSegmentationTarget.TabIndex = 0;
      this.lblSegmentationTarget.Text = "Segmentation Target:";
      //
      // lblSegmentationSource
      //
      this.lblSegmentationSource.AutoSize = true;
      this.lblSegmentationSource.Location = new System.Drawing.Point(7, 85);
      this.lblSegmentationSource.Name = "lblSegmentationSource";
      this.lblSegmentationSource.Size = new System.Drawing.Size(112, 13);
      this.lblSegmentationSource.TabIndex = 0;
      this.lblSegmentationSource.Text = "Segmentation Source:";
      //
      // lblArchiveTarget
      //
      this.lblArchiveTarget.AutoSize = true;
      this.lblArchiveTarget.Location = new System.Drawing.Point(7, 38);
      this.lblArchiveTarget.Name = "lblArchiveTarget";
      this.lblArchiveTarget.Size = new System.Drawing.Size(80, 13);
      this.lblArchiveTarget.TabIndex = 0;
      this.lblArchiveTarget.Text = "Archive Target:";
      //
      // lblArchiveSource
      //
      this.lblArchiveSource.AutoSize = true;
      this.lblArchiveSource.Location = new System.Drawing.Point(7, 20);
      this.lblArchiveSource.Name = "lblArchiveSource";
      this.lblArchiveSource.Size = new System.Drawing.Size(83, 13);
      this.lblArchiveSource.TabIndex = 0;
      this.lblArchiveSource.Text = "Archive Source:";
      //
      // gbArchiveManagement
      //
      this.gbArchiveManagement.Controls.Add(this.btnExtractArchive);
      this.gbArchiveManagement.Controls.Add(this.btnCreateArchive);
      this.gbArchiveManagement.Location = new System.Drawing.Point(13, 6);
      this.gbArchiveManagement.Name = "gbArchiveManagement";
      this.gbArchiveManagement.Size = new System.Drawing.Size(160, 206);
      this.gbArchiveManagement.TabIndex = 0;
      this.gbArchiveManagement.TabStop = false;
      this.gbArchiveManagement.Text = "Archive Management";
      //
      // btnExtractArchive
      //
      this.btnExtractArchive.Location = new System.Drawing.Point(19, 46);
      this.btnExtractArchive.Name = "btnExtractArchive";
      this.btnExtractArchive.Size = new System.Drawing.Size(125, 23);
      this.btnExtractArchive.TabIndex = 0;
      this.btnExtractArchive.Tag = "ExtractArchive";
      this.btnExtractArchive.Text = "Extract Archive";
      this.btnExtractArchive.UseVisualStyleBackColor = true;
      this.btnExtractArchive.Click += new System.EventHandler(this.Action);
      //
      // btnCreateArchive
      //
      this.btnCreateArchive.Location = new System.Drawing.Point(19, 19);
      this.btnCreateArchive.Name = "btnCreateArchive";
      this.btnCreateArchive.Size = new System.Drawing.Size(125, 23);
      this.btnCreateArchive.TabIndex = 0;
      this.btnCreateArchive.Tag = "CreateArchive";
      this.btnCreateArchive.Text = "Create Archive";
      this.btnCreateArchive.UseVisualStyleBackColor = true;
      this.btnCreateArchive.Click += new System.EventHandler(this.Action);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1052, 24);
      this.mnuMain.TabIndex = 2;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 247);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1052, 266);
      this.txtOut.TabIndex = 3;
      this.txtOut.WordWrap = false;
      //
      // btnCreateDummySegmentationData
      //
      this.btnCreateDummySegmentationData.Location = new System.Drawing.Point(19, 77);
      this.btnCreateDummySegmentationData.Name = "btnCreateDummySegmentationData";
      this.btnCreateDummySegmentationData.Size = new System.Drawing.Size(125, 23);
      this.btnCreateDummySegmentationData.TabIndex = 0;
      this.btnCreateDummySegmentationData.Tag = "CreateDummySegmentationData";
      this.btnCreateDummySegmentationData.Text = "Create Dummy Data";
      this.btnCreateDummySegmentationData.UseVisualStyleBackColor = true;
      this.btnCreateDummySegmentationData.Click += new System.EventHandler(this.Action);
      //
      // cboFileSize
      //
      this.cboFileSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboFileSize.FormattingEnabled = true;
      this.cboFileSize.Items.AddRange(new object[] {
        "100 (0.1 MB)",
        "1,000 (1 MB)",
        "5,000 (5 MB)",
        "10,000 (10 MB)",
        "20,000 (20 MB)",
        "50,000 (50 MB)",
        "100,000 (100 MB)"
      });
      this.cboFileSize.Location = new System.Drawing.Point(19, 131);
      this.cboFileSize.Name = "cboFileSize";
      this.cboFileSize.Size = new System.Drawing.Size(125, 21);
      this.cboFileSize.TabIndex = 1;
      //
      // lblFileSize
      //
      this.lblFileSize.AutoSize = true;
      this.lblFileSize.Location = new System.Drawing.Point(19, 114);
      this.lblFileSize.Name = "lblFileSize";
      this.lblFileSize.Size = new System.Drawing.Size(69, 13);
      this.lblFileSize.TabIndex = 2;
      this.lblFileSize.Text = "File Size (KB)";
      //
      // cboSegmentSize
      //
      this.cboSegmentSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSegmentSize.FormattingEnabled = true;
      this.cboSegmentSize.Items.AddRange(new object[] {
        "1",
        "5",
        "10",
        "20",
        "50",
        "100",
        "200"
      });
      this.cboSegmentSize.Location = new System.Drawing.Point(19, 179);
      this.cboSegmentSize.Name = "cboSegmentSize";
      this.cboSegmentSize.Size = new System.Drawing.Size(125, 21);
      this.cboSegmentSize.TabIndex = 1;
      //
      // lblSegmentSize
      //
      this.lblSegmentSize.AutoSize = true;
      this.lblSegmentSize.Location = new System.Drawing.Point(19, 162);
      this.lblSegmentSize.Name = "lblSegmentSize";
      this.lblSegmentSize.Size = new System.Drawing.Size(95, 13);
      this.lblSegmentSize.TabIndex = 2;
      this.lblSegmentSize.Text = "Segment Size (KB)";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1052, 536);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FileManager";
      this.pnlTop.ResumeLayout(false);
      this.gbFileSegmentation.ResumeLayout(false);
      this.gbFileSegmentation.PerformLayout();
      this.gbFileLocations.ResumeLayout(false);
      this.gbFileLocations.PerformLayout();
      this.gbArchiveManagement.ResumeLayout(false);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.GroupBox gbFileSegmentation;
    private System.Windows.Forms.Button btnDesegmentFile;
    private System.Windows.Forms.Button btnSegmentFile;
    private System.Windows.Forms.GroupBox gbFileLocations;
    private System.Windows.Forms.Label lblArchiveSource;
    private System.Windows.Forms.GroupBox gbArchiveManagement;
    private System.Windows.Forms.Label lblArchiveSourceValue;
    private System.Windows.Forms.Label lblDesegmentationTargetValue;
    private System.Windows.Forms.Label lblExtractTargetValue;
    private System.Windows.Forms.Label lblSegmentationTargetValue;
    private System.Windows.Forms.Label lblArchiveTargetValue;
    private System.Windows.Forms.Label lblSegmentationSourceValue;
    private System.Windows.Forms.Label lblDesegmentationTarget;
    private System.Windows.Forms.Label lblExtractTarget;
    private System.Windows.Forms.Label lblSegmentationTarget;
    private System.Windows.Forms.Label lblSegmentationSource;
    private System.Windows.Forms.Label lblArchiveTarget;
    private System.Windows.Forms.Button btnExtractArchive;
    private System.Windows.Forms.Button btnCreateArchive;
    private System.Windows.Forms.Button btnCreateDummySegmentationData;
    private System.Windows.Forms.Label lblSegmentSize;
    private System.Windows.Forms.Label lblFileSize;
    private System.Windows.Forms.ComboBox cboSegmentSize;
    private System.Windows.Forms.ComboBox cboFileSize;
  }
}

