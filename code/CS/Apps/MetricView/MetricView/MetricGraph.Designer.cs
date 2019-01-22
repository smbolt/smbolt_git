namespace Teleflora.Operations.MetricView
{
  partial class MetricGraph
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
      this.mnuControlContext = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuContextProperties = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextRefreshGraph = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextActivateGraph = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextDeactivateGraph = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextCaptureImage = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextGetGraphData = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextRemoveGraph = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextMaximize = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextFrontAndCenter = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuContextRestorePosition = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuCopyGraphConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveGraphToFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveImageToFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuControlContext.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuControlContext
      //
      this.mnuControlContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuContextProperties,
        this.mnuContextRefreshGraph,
        this.mnuContextActivateGraph,
        this.mnuContextDeactivateGraph,
        this.mnuContextCaptureImage,
        this.mnuContextGetGraphData,
        this.mnuContextRemoveGraph,
        this.mnuContextMaximize,
        this.mnuContextFrontAndCenter,
        this.mnuContextRestorePosition,
        this.mnuCopyGraphConfig,
        this.mnuSaveGraphToFile,
        this.mnuSaveImageToFile
      });
      this.mnuControlContext.Name = "mnuRemoveGraph";
      this.mnuControlContext.Size = new System.Drawing.Size(208, 312);
      //
      // mnuContextProperties
      //
      this.mnuContextProperties.Name = "mnuContextProperties";
      this.mnuContextProperties.Size = new System.Drawing.Size(207, 22);
      this.mnuContextProperties.Tag = "PROPERTIES";
      this.mnuContextProperties.Text = "&Properties";
      this.mnuContextProperties.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextRefreshGraph
      //
      this.mnuContextRefreshGraph.Name = "mnuContextRefreshGraph";
      this.mnuContextRefreshGraph.Size = new System.Drawing.Size(207, 22);
      this.mnuContextRefreshGraph.Tag = "REFRESH_GRAPH";
      this.mnuContextRefreshGraph.Text = "&Refresh";
      this.mnuContextRefreshGraph.Click += new System.EventHandler(this.mnuContextRefreshGraph_Click);
      //
      // mnuContextActivateGraph
      //
      this.mnuContextActivateGraph.Name = "mnuContextActivateGraph";
      this.mnuContextActivateGraph.Size = new System.Drawing.Size(207, 22);
      this.mnuContextActivateGraph.Tag = "ACTIVATE_GRAPH";
      this.mnuContextActivateGraph.Text = "&Activate";
      this.mnuContextActivateGraph.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextDeactivateGraph
      //
      this.mnuContextDeactivateGraph.Name = "mnuContextDeactivateGraph";
      this.mnuContextDeactivateGraph.Size = new System.Drawing.Size(207, 22);
      this.mnuContextDeactivateGraph.Tag = "DEACTIVATE_GRAPH";
      this.mnuContextDeactivateGraph.Text = "&Deactivate";
      this.mnuContextDeactivateGraph.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextCaptureImage
      //
      this.mnuContextCaptureImage.Name = "mnuContextCaptureImage";
      this.mnuContextCaptureImage.Size = new System.Drawing.Size(207, 22);
      this.mnuContextCaptureImage.Tag = "CAPTURE_IMAGE";
      this.mnuContextCaptureImage.Text = "&Capture to Clipboard";
      this.mnuContextCaptureImage.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextGetGraphData
      //
      this.mnuContextGetGraphData.Name = "mnuContextGetGraphData";
      this.mnuContextGetGraphData.Size = new System.Drawing.Size(207, 22);
      this.mnuContextGetGraphData.Tag = "GET_DATA";
      this.mnuContextGetGraphData.Text = "&Get Graph Data";
      this.mnuContextGetGraphData.Click += new System.EventHandler(this.mnuContextGetGraphData_Click);
      //
      // mnuContextRemoveGraph
      //
      this.mnuContextRemoveGraph.Name = "mnuContextRemoveGraph";
      this.mnuContextRemoveGraph.Size = new System.Drawing.Size(207, 22);
      this.mnuContextRemoveGraph.Tag = "REMOVE_GRAPH";
      this.mnuContextRemoveGraph.Text = "Remove &Graph";
      this.mnuContextRemoveGraph.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextMaximize
      //
      this.mnuContextMaximize.Name = "mnuContextMaximize";
      this.mnuContextMaximize.Size = new System.Drawing.Size(207, 22);
      this.mnuContextMaximize.Tag = "MAXIMIZE";
      this.mnuContextMaximize.Text = "&Maximize";
      this.mnuContextMaximize.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextFrontAndCenter
      //
      this.mnuContextFrontAndCenter.Name = "mnuContextFrontAndCenter";
      this.mnuContextFrontAndCenter.Size = new System.Drawing.Size(207, 22);
      this.mnuContextFrontAndCenter.Tag = "FRONT_AND_CENTER";
      this.mnuContextFrontAndCenter.Text = "&Front and Center";
      this.mnuContextFrontAndCenter.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuContextRestorePosition
      //
      this.mnuContextRestorePosition.Name = "mnuContextRestorePosition";
      this.mnuContextRestorePosition.Size = new System.Drawing.Size(207, 22);
      this.mnuContextRestorePosition.Tag = "RESTORE_POSITION";
      this.mnuContextRestorePosition.Text = "Restore &Position";
      this.mnuContextRestorePosition.Click += new System.EventHandler(this.mnuContextMenuAction_Click);
      //
      // mnuCopyGraphConfig
      //
      this.mnuCopyGraphConfig.Name = "mnuCopyGraphConfig";
      this.mnuCopyGraphConfig.Size = new System.Drawing.Size(207, 22);
      this.mnuCopyGraphConfig.Text = "&Copy Graph Config";
      this.mnuCopyGraphConfig.Click += new System.EventHandler(this.mnuCopyGraphConfig_Click);
      //
      // mnuSaveGraphToFile
      //
      this.mnuSaveGraphToFile.Name = "mnuSaveGraphToFile";
      this.mnuSaveGraphToFile.Size = new System.Drawing.Size(207, 22);
      this.mnuSaveGraphToFile.Text = "&Save Graph Config to File";
      this.mnuSaveGraphToFile.Click += new System.EventHandler(this.mnuSaveGraphToFile_Click);
      //
      // mnuSaveImageToFile
      //
      this.mnuSaveImageToFile.Name = "mnuSaveImageToFile";
      this.mnuSaveImageToFile.Size = new System.Drawing.Size(207, 22);
      this.mnuSaveImageToFile.Text = "Save Graph &Image to File";
      this.mnuSaveImageToFile.Click += new System.EventHandler(this.mnuSaveImageToFile_Click);
      //
      // MetricGraph
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ContextMenuStrip = this.mnuControlContext;
      this.Name = "MetricGraph";
      this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MetricGraph_MouseMove);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MetricGraph_MouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MetricGraph_MouseUp);
      this.mnuControlContext.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip mnuControlContext;
    private System.Windows.Forms.ToolStripMenuItem mnuContextRemoveGraph;
    private System.Windows.Forms.ToolStripMenuItem mnuContextActivateGraph;
    private System.Windows.Forms.ToolStripMenuItem mnuContextDeactivateGraph;
    private System.Windows.Forms.ToolStripMenuItem mnuContextProperties;
    private System.Windows.Forms.ToolStripMenuItem mnuContextCaptureImage;
    private System.Windows.Forms.ToolStripMenuItem mnuContextRefreshGraph;
    private System.Windows.Forms.ToolStripMenuItem mnuContextMaximize;
    private System.Windows.Forms.ToolStripMenuItem mnuContextFrontAndCenter;
    private System.Windows.Forms.ToolStripMenuItem mnuContextRestorePosition;
    private System.Windows.Forms.ToolStripMenuItem mnuContextGetGraphData;
    private System.Windows.Forms.ToolStripMenuItem mnuCopyGraphConfig;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveGraphToFile;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveImageToFile;
  }
}
