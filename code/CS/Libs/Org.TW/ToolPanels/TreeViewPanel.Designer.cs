namespace Org.TW.ToolPanels
{
  partial class TreeViewPanel
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
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Child1");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Child2");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Root", new System.Windows.Forms.TreeNode[] {
        treeNode1,
        treeNode2
      });
      this.pnlTop = new System.Windows.Forms.Panel();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tvDoc = new System.Windows.Forms.TreeView();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(264, 5);
      this.pnlTop.TabIndex = 0;
      //
      // pnlBottom
      //
      this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 347);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(264, 5);
      this.pnlBottom.TabIndex = 1;
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.tvDoc);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 5);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(264, 342);
      this.pnlMain.TabIndex = 2;
      //
      // tvDoc
      //
      this.tvDoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvDoc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvDoc.Location = new System.Drawing.Point(0, 0);
      this.tvDoc.Name = "tvDoc";
      treeNode1.Name = "Node1";
      treeNode1.Text = "Child1";
      treeNode2.Name = "Node2";
      treeNode2.Text = "Child2";
      treeNode3.Name = "Node0";
      treeNode3.Text = "Root";
      this.tvDoc.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
        treeNode3
      });
      this.tvDoc.Size = new System.Drawing.Size(264, 342);
      this.tvDoc.TabIndex = 0;
      this.tvDoc.Tag = "";
      this.tvDoc.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDoc_AfterSelect);
      //
      // TreeViewPanel
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.pnlTop);
      this.Name = "TreeViewPanel";
      this.Size = new System.Drawing.Size(264, 352);
      this.Tag = "ToolPanel_TreeView";
      this.pnlMain.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TreeView tvDoc;
  }
}
