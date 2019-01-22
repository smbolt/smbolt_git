namespace Org.OpsManager
{
  partial class frmTaskParameters
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskParameters));
      this.gvTaskParameters = new System.Windows.Forms.DataGridView();
      this.ctxMenuTaskParameters = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuTaskParametersDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuTaskParametersMigrate = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.gvParameterSets = new System.Windows.Forms.DataGridView();
      this.ctxMenuParameterSets = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuParameterSetsAddToTask = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuParameterSetsMigrate = new System.Windows.Forms.ToolStripMenuItem();
      this.lblTaskParameters = new System.Windows.Forms.Label();
      this.lblAvialableParameterSets = new System.Windows.Forms.Label();
      this.btnNewTaskParameter = new System.Windows.Forms.Button();
      this.gvParametersInSet = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnNewParameterSet = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskParameters)).BeginInit();
      this.ctxMenuTaskParameters.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvParameterSets)).BeginInit();
      this.ctxMenuParameterSets.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvParametersInSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      //
      // gvTaskParameters
      //
      this.gvTaskParameters.AllowUserToAddRows = false;
      this.gvTaskParameters.AllowUserToDeleteRows = false;
      this.gvTaskParameters.AllowUserToResizeRows = false;
      this.gvTaskParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTaskParameters.ContextMenuStrip = this.ctxMenuTaskParameters;
      this.gvTaskParameters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTaskParameters.Location = new System.Drawing.Point(3, 31);
      this.gvTaskParameters.MultiSelect = false;
      this.gvTaskParameters.Name = "gvTaskParameters";
      this.gvTaskParameters.RowHeadersVisible = false;
      this.gvTaskParameters.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTaskParameters.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvTaskParameters.RowTemplate.Height = 19;
      this.gvTaskParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTaskParameters.Size = new System.Drawing.Size(778, 200);
      this.gvTaskParameters.TabIndex = 6;
      this.gvTaskParameters.Tag = "EditTaskParameter";
      this.gvTaskParameters.DoubleClick += new System.EventHandler(this.Action);
      //
      // ctxMenuTaskParameters
      //
      this.ctxMenuTaskParameters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuTaskParametersDelete,
        this.ctxMenuTaskParametersMigrate
      });
      this.ctxMenuTaskParameters.Name = "ctxMenuTaskParameters";
      this.ctxMenuTaskParameters.Size = new System.Drawing.Size(230, 48);
      this.ctxMenuTaskParameters.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenu_Opening);
      //
      // ctxMenuTaskParametersDelete
      //
      this.ctxMenuTaskParametersDelete.Name = "ctxMenuTaskParametersDelete";
      this.ctxMenuTaskParametersDelete.Size = new System.Drawing.Size(229, 22);
      this.ctxMenuTaskParametersDelete.Tag = "DeleteTaskParameter";
      this.ctxMenuTaskParametersDelete.Text = "Delete";
      this.ctxMenuTaskParametersDelete.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuTaskParametersMigrate
      //
      this.ctxMenuTaskParametersMigrate.Name = "ctxMenuTaskParametersMigrate";
      this.ctxMenuTaskParametersMigrate.Size = new System.Drawing.Size(229, 22);
      this.ctxMenuTaskParametersMigrate.Tag = "MigrateVariable";
      this.ctxMenuTaskParametersMigrate.Text = "Migrate Variable to [new env]";
      this.ctxMenuTaskParametersMigrate.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 520);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(783, 18);
      this.lblStatus.TabIndex = 7;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gvParameterSets
      //
      this.gvParameterSets.AllowUserToAddRows = false;
      this.gvParameterSets.AllowUserToDeleteRows = false;
      this.gvParameterSets.AllowUserToResizeRows = false;
      this.gvParameterSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvParameterSets.ContextMenuStrip = this.ctxMenuParameterSets;
      this.gvParameterSets.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvParameterSets.Location = new System.Drawing.Point(3, 281);
      this.gvParameterSets.MultiSelect = false;
      this.gvParameterSets.Name = "gvParameterSets";
      this.gvParameterSets.RowHeadersVisible = false;
      this.gvParameterSets.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvParameterSets.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvParameterSets.RowTemplate.Height = 19;
      this.gvParameterSets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvParameterSets.Size = new System.Drawing.Size(242, 195);
      this.gvParameterSets.TabIndex = 8;
      this.gvParameterSets.Tag = "EditParameterSet";
      this.gvParameterSets.SelectionChanged += new System.EventHandler(this.gvParameterSets_SelectionChange);
      this.gvParameterSets.DoubleClick += new System.EventHandler(this.Action);
      //
      // ctxMenuParameterSets
      //
      this.ctxMenuParameterSets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuParameterSetsAddToTask,
        this.ctxMenuParameterSetsMigrate
      });
      this.ctxMenuParameterSets.Name = "ctxMenuParameterSets";
      this.ctxMenuParameterSets.Size = new System.Drawing.Size(204, 48);
      this.ctxMenuParameterSets.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenu_Opening);
      //
      // ctxMenuParameterSetsAddToTask
      //
      this.ctxMenuParameterSetsAddToTask.Name = "ctxMenuParameterSetsAddToTask";
      this.ctxMenuParameterSetsAddToTask.Size = new System.Drawing.Size(203, 22);
      this.ctxMenuParameterSetsAddToTask.Tag = "AddSetToTaskParameters";
      this.ctxMenuParameterSetsAddToTask.Text = "Add to Task Parameters";
      this.ctxMenuParameterSetsAddToTask.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuParameterSetsMigrate
      //
      this.ctxMenuParameterSetsMigrate.Name = "ctxMenuParameterSetsMigrate";
      this.ctxMenuParameterSetsMigrate.Size = new System.Drawing.Size(203, 22);
      this.ctxMenuParameterSetsMigrate.Tag = "MigrateParameterSet";
      this.ctxMenuParameterSetsMigrate.Text = "Migrate Set to [new env]";
      this.ctxMenuParameterSetsMigrate.Click += new System.EventHandler(this.Action);
      //
      // lblTaskParameters
      //
      this.lblTaskParameters.AutoSize = true;
      this.lblTaskParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTaskParameters.Location = new System.Drawing.Point(320, 9);
      this.lblTaskParameters.Name = "lblTaskParameters";
      this.lblTaskParameters.Size = new System.Drawing.Size(122, 18);
      this.lblTaskParameters.TabIndex = 9;
      this.lblTaskParameters.Text = "Task Parameters";
      //
      // lblAvialableParameterSets
      //
      this.lblAvialableParameterSets.AutoSize = true;
      this.lblAvialableParameterSets.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAvialableParameterSets.Location = new System.Drawing.Point(38, 260);
      this.lblAvialableParameterSets.Name = "lblAvialableParameterSets";
      this.lblAvialableParameterSets.Size = new System.Drawing.Size(172, 18);
      this.lblAvialableParameterSets.TabIndex = 10;
      this.lblAvialableParameterSets.Text = "Available Parameter Sets";
      //
      // btnNewTaskParameter
      //
      this.btnNewTaskParameter.Location = new System.Drawing.Point(620, 237);
      this.btnNewTaskParameter.Name = "btnNewTaskParameter";
      this.btnNewTaskParameter.Size = new System.Drawing.Size(156, 23);
      this.btnNewTaskParameter.TabIndex = 1;
      this.btnNewTaskParameter.Tag = "NewTaskParameter";
      this.btnNewTaskParameter.Text = "New Task Parameter";
      this.btnNewTaskParameter.UseVisualStyleBackColor = true;
      this.btnNewTaskParameter.Click += new System.EventHandler(this.Action);
      //
      // gvParametersInSet
      //
      this.gvParametersInSet.AllowUserToAddRows = false;
      this.gvParametersInSet.AllowUserToDeleteRows = false;
      this.gvParametersInSet.AllowUserToResizeRows = false;
      this.gvParametersInSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvParametersInSet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvParametersInSet.Location = new System.Drawing.Point(280, 281);
      this.gvParametersInSet.MultiSelect = false;
      this.gvParametersInSet.Name = "gvParametersInSet";
      this.gvParametersInSet.ReadOnly = true;
      this.gvParametersInSet.RowHeadersVisible = false;
      this.gvParametersInSet.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvParametersInSet.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvParametersInSet.RowTemplate.Height = 19;
      this.gvParametersInSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvParametersInSet.Size = new System.Drawing.Size(501, 195);
      this.gvParametersInSet.TabIndex = 12;
      this.gvParametersInSet.Tag = "EditParameterInSet";
      //
      // label1
      //
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(459, 260);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(126, 18);
      this.label1.TabIndex = 13;
      this.label1.Text = "Parameters In Set";
      //
      // pictureBox1
      //
      this.pictureBox1.Image = global::Org.OpsManager.Properties.Resources.Right_Arrow;
      this.pictureBox1.Location = new System.Drawing.Point(244, 344);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(37, 50);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 15;
      this.pictureBox1.TabStop = false;
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(323, 482);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // btnNewParameterSet
      //
      this.btnNewParameterSet.Location = new System.Drawing.Point(59, 482);
      this.btnNewParameterSet.Name = "btnNewParameterSet";
      this.btnNewParameterSet.Size = new System.Drawing.Size(125, 25);
      this.btnNewParameterSet.TabIndex = 16;
      this.btnNewParameterSet.Tag = "NewParameterSet";
      this.btnNewParameterSet.Text = "New Parameter Set";
      this.btnNewParameterSet.UseVisualStyleBackColor = true;
      this.btnNewParameterSet.Click += new System.EventHandler(this.Action);
      //
      // frmTaskParameters
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(783, 538);
      this.ControlBox = false;
      this.Controls.Add(this.btnNewParameterSet);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.gvParametersInSet);
      this.Controls.Add(this.btnNewTaskParameter);
      this.Controls.Add(this.lblAvialableParameterSets);
      this.Controls.Add(this.lblTaskParameters);
      this.Controls.Add(this.gvParameterSets);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.gvTaskParameters);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmTaskParameters";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Task Parameters";
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskParameters)).EndInit();
      this.ctxMenuTaskParameters.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvParameterSets)).EndInit();
      this.ctxMenuParameterSets.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvParametersInSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView gvTaskParameters;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.DataGridView gvParameterSets;
    private System.Windows.Forms.Label lblTaskParameters;
    private System.Windows.Forms.Label lblAvialableParameterSets;
    private System.Windows.Forms.Button btnNewTaskParameter;
    private System.Windows.Forms.ContextMenuStrip ctxMenuParameterSets;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuParameterSetsAddToTask;
    private System.Windows.Forms.ContextMenuStrip ctxMenuTaskParameters;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTaskParametersDelete;
    private System.Windows.Forms.DataGridView gvParametersInSet;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTaskParametersMigrate;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuParameterSetsMigrate;
    private System.Windows.Forms.Button btnNewParameterSet;
  }
}