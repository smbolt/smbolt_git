namespace NameTags
{
  partial class frmOpenProject
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
      this.lvProjects = new System.Windows.Forms.ListView();
      this.lblOpenProject = new System.Windows.Forms.Label();
      this.btnOpenProject = new System.Windows.Forms.Button();
      this.btnNewProject = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // lvProjects
      //
      this.lvProjects.FullRowSelect = true;
      this.lvProjects.Location = new System.Drawing.Point(12, 30);
      this.lvProjects.Name = "lvProjects";
      this.lvProjects.Size = new System.Drawing.Size(564, 326);
      this.lvProjects.TabIndex = 0;
      this.lvProjects.UseCompatibleStateImageBehavior = false;
      this.lvProjects.View = System.Windows.Forms.View.Details;
      this.lvProjects.SelectedIndexChanged += new System.EventHandler(this.lvProjects_SelectedIndexChanged);
      this.lvProjects.DoubleClick += new System.EventHandler(this.lvProjects_DoubleClick);
      //
      // lblOpenProject
      //
      this.lblOpenProject.AutoSize = true;
      this.lblOpenProject.Location = new System.Drawing.Point(9, 9);
      this.lblOpenProject.Name = "lblOpenProject";
      this.lblOpenProject.Size = new System.Drawing.Size(111, 13);
      this.lblOpenProject.TabIndex = 1;
      this.lblOpenProject.Text = "Open Existing Project:";
      //
      // btnOpenProject
      //
      this.btnOpenProject.Location = new System.Drawing.Point(12, 362);
      this.btnOpenProject.Name = "btnOpenProject";
      this.btnOpenProject.Size = new System.Drawing.Size(136, 26);
      this.btnOpenProject.TabIndex = 2;
      this.btnOpenProject.Text = "Open Project";
      this.btnOpenProject.UseVisualStyleBackColor = true;
      this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
      //
      // btnNewProject
      //
      this.btnNewProject.Location = new System.Drawing.Point(154, 362);
      this.btnNewProject.Name = "btnNewProject";
      this.btnNewProject.Size = new System.Drawing.Size(136, 26);
      this.btnNewProject.TabIndex = 2;
      this.btnNewProject.Text = "New Project";
      this.btnNewProject.UseVisualStyleBackColor = true;
      this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(440, 362);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(136, 26);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // frmOpenProject
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(589, 395);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnNewProject);
      this.Controls.Add(this.btnOpenProject);
      this.Controls.Add(this.lblOpenProject);
      this.Controls.Add(this.lvProjects);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmOpenProject";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Open or Create Project";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvProjects;
    private System.Windows.Forms.Label lblOpenProject;
    private System.Windows.Forms.Button btnOpenProject;
    private System.Windows.Forms.Button btnNewProject;
    private System.Windows.Forms.Button btnCancel;
  }
}