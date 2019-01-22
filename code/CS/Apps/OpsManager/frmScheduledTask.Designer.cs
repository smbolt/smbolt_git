namespace Org.OpsManager
{
  partial class frmScheduledTask
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
      this.lblTaskName = new System.Windows.Forms.Label();
      this.txtTaskName = new System.Windows.Forms.TextBox();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.chkIsLongRunning = new System.Windows.Forms.CheckBox();
      this.lblProcessorName = new System.Windows.Forms.Label();
      this.txtProcessorName = new System.Windows.Forms.TextBox();
      this.lblAssemblyLocation = new System.Windows.Forms.Label();
      this.txtAssemblyLocation = new System.Windows.Forms.TextBox();
      this.txtStoredProcedureName = new System.Windows.Forms.TextBox();
      this.lblStoredProcedureName = new System.Windows.Forms.Label();
      this.lblProcessorType = new System.Windows.Forms.Label();
      this.cboProcessorType = new System.Windows.Forms.ComboBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.gvTaskSchedules = new System.Windows.Forms.DataGridView();
      this.ctxMenuTaskSchedule = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuTaskScheduleDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.cbRunUntilTask = new System.Windows.Forms.CheckBox();
      this.cbRunUntilOverride = new System.Windows.Forms.CheckBox();
      this.lblProcessorVersion = new System.Windows.Forms.Label();
      this.txtProcessorVersion = new System.Windows.Forms.TextBox();
      this.lblPeriod = new System.Windows.Forms.Label();
      this.cboPeriod = new System.Windows.Forms.ComboBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblRunUntilOffsetMinutes = new System.Windows.Forms.Label();
      this.txtRunUntilOffsetMinutes = new System.Windows.Forms.TextBox();
      this.chkTrackHistory = new System.Windows.Forms.CheckBox();
      this.btnNewTaskSchedule = new System.Windows.Forms.Button();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.chkSuppressNotificationsOnSuccess = new System.Windows.Forms.CheckBox();
      this.grpBxRunUntilParameters = new System.Windows.Forms.GroupBox();
      this.btnViewTaskParameters = new System.Windows.Forms.Button();
      this.txtAssemblyName = new System.Windows.Forms.TextBox();
      this.lblAssemblyName = new System.Windows.Forms.Label();
      this.txtCatalogName = new System.Windows.Forms.TextBox();
      this.lblCatalogName = new System.Windows.Forms.Label();
      this.txtCatalogEntry = new System.Windows.Forms.TextBox();
      this.lblCatalogEntry = new System.Windows.Forms.Label();
      this.txtObjectTypeName = new System.Windows.Forms.TextBox();
      this.lblObjectTypeName = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskSchedules)).BeginInit();
      this.ctxMenuTaskSchedule.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.grpBxRunUntilParameters.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblTaskName
      // 
      this.lblTaskName.AutoSize = true;
      this.lblTaskName.Location = new System.Drawing.Point(14, 15);
      this.lblTaskName.Name = "lblTaskName";
      this.lblTaskName.Size = new System.Drawing.Size(62, 13);
      this.lblTaskName.TabIndex = 0;
      this.lblTaskName.Text = "Task Name";
      // 
      // txtTaskName
      // 
      this.txtTaskName.Location = new System.Drawing.Point(17, 30);
      this.txtTaskName.Name = "txtTaskName";
      this.txtTaskName.Size = new System.Drawing.Size(188, 20);
      this.txtTaskName.TabIndex = 1;
      this.txtTaskName.Tag = "PropertyChange";
      this.txtTaskName.TextChanged += new System.EventHandler(this.Action);
      // 
      // chkIsActive
      // 
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(762, 51);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.chkIsActive.Size = new System.Drawing.Size(67, 17);
      this.chkIsActive.TabIndex = 25;
      this.chkIsActive.Tag = "PropertyChange";
      this.chkIsActive.Text = "Is Active";
      this.chkIsActive.UseVisualStyleBackColor = true;
      this.chkIsActive.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // chkIsLongRunning
      // 
      this.chkIsLongRunning.AutoSize = true;
      this.chkIsLongRunning.Location = new System.Drawing.Point(725, 70);
      this.chkIsLongRunning.Name = "chkIsLongRunning";
      this.chkIsLongRunning.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.chkIsLongRunning.Size = new System.Drawing.Size(104, 17);
      this.chkIsLongRunning.TabIndex = 26;
      this.chkIsLongRunning.Tag = "PropertyChange";
      this.chkIsLongRunning.Text = "Is Long Running";
      this.chkIsLongRunning.UseVisualStyleBackColor = true;
      this.chkIsLongRunning.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // lblProcessorName
      // 
      this.lblProcessorName.AutoSize = true;
      this.lblProcessorName.Location = new System.Drawing.Point(17, 59);
      this.lblProcessorName.Name = "lblProcessorName";
      this.lblProcessorName.Size = new System.Drawing.Size(85, 13);
      this.lblProcessorName.TabIndex = 6;
      this.lblProcessorName.Text = "Processor Name";
      // 
      // txtProcessorName
      // 
      this.txtProcessorName.Location = new System.Drawing.Point(18, 75);
      this.txtProcessorName.Name = "txtProcessorName";
      this.txtProcessorName.Size = new System.Drawing.Size(188, 20);
      this.txtProcessorName.TabIndex = 3;
      this.txtProcessorName.Tag = "PropertyChange";
      this.txtProcessorName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblAssemblyLocation
      // 
      this.lblAssemblyLocation.AutoSize = true;
      this.lblAssemblyLocation.Location = new System.Drawing.Point(225, 103);
      this.lblAssemblyLocation.Name = "lblAssemblyLocation";
      this.lblAssemblyLocation.Size = new System.Drawing.Size(95, 13);
      this.lblAssemblyLocation.TabIndex = 14;
      this.lblAssemblyLocation.Text = "Assembly Location";
      // 
      // txtAssemblyLocation
      // 
      this.txtAssemblyLocation.Enabled = false;
      this.txtAssemblyLocation.Location = new System.Drawing.Point(228, 120);
      this.txtAssemblyLocation.Name = "txtAssemblyLocation";
      this.txtAssemblyLocation.Size = new System.Drawing.Size(188, 20);
      this.txtAssemblyLocation.TabIndex = 6;
      this.txtAssemblyLocation.Tag = "PropertyChange";
      this.txtAssemblyLocation.TextChanged += new System.EventHandler(this.Action);
      // 
      // txtStoredProcedureName
      // 
      this.txtStoredProcedureName.Enabled = false;
      this.txtStoredProcedureName.Location = new System.Drawing.Point(17, 120);
      this.txtStoredProcedureName.Name = "txtStoredProcedureName";
      this.txtStoredProcedureName.Size = new System.Drawing.Size(188, 20);
      this.txtStoredProcedureName.TabIndex = 5;
      this.txtStoredProcedureName.Tag = "PropertyChange";
      this.txtStoredProcedureName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblStoredProcedureName
      // 
      this.lblStoredProcedureName.AutoSize = true;
      this.lblStoredProcedureName.Location = new System.Drawing.Point(17, 104);
      this.lblStoredProcedureName.Name = "lblStoredProcedureName";
      this.lblStoredProcedureName.Size = new System.Drawing.Size(121, 13);
      this.lblStoredProcedureName.TabIndex = 12;
      this.lblStoredProcedureName.Text = "Stored Procedure Name";
      // 
      // lblProcessorType
      // 
      this.lblProcessorType.AutoSize = true;
      this.lblProcessorType.Location = new System.Drawing.Point(223, 15);
      this.lblProcessorType.Name = "lblProcessorType";
      this.lblProcessorType.Size = new System.Drawing.Size(81, 13);
      this.lblProcessorType.TabIndex = 14;
      this.lblProcessorType.Text = "Processor Type";
      // 
      // cboProcessorType
      // 
      this.cboProcessorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboProcessorType.FormattingEnabled = true;
      this.cboProcessorType.Location = new System.Drawing.Point(226, 30);
      this.cboProcessorType.Name = "cboProcessorType";
      this.cboProcessorType.Size = new System.Drawing.Size(188, 21);
      this.cboProcessorType.TabIndex = 2;
      this.cboProcessorType.Tag = "PropertyChange";
      this.cboProcessorType.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 522);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(844, 20);
      this.lblStatus.TabIndex = 16;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // gvTaskSchedules
      // 
      this.gvTaskSchedules.AllowUserToAddRows = false;
      this.gvTaskSchedules.AllowUserToDeleteRows = false;
      this.gvTaskSchedules.AllowUserToResizeRows = false;
      this.gvTaskSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTaskSchedules.ContextMenuStrip = this.ctxMenuTaskSchedule;
      this.gvTaskSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvTaskSchedules.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTaskSchedules.Location = new System.Drawing.Point(0, 252);
      this.gvTaskSchedules.MultiSelect = false;
      this.gvTaskSchedules.Name = "gvTaskSchedules";
      this.gvTaskSchedules.RowHeadersVisible = false;
      this.gvTaskSchedules.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTaskSchedules.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvTaskSchedules.RowTemplate.Height = 19;
      this.gvTaskSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTaskSchedules.Size = new System.Drawing.Size(844, 230);
      this.gvTaskSchedules.TabIndex = 40;
      this.gvTaskSchedules.Tag = "EditTaskSchedule";
      this.gvTaskSchedules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTaskSchedules_CellContentClick);
      this.gvTaskSchedules.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvTaskSchedules_CellMouseUp);
      this.gvTaskSchedules.DoubleClick += new System.EventHandler(this.Action);
      // 
      // ctxMenuTaskSchedule
      // 
      this.ctxMenuTaskSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuTaskScheduleDelete});
      this.ctxMenuTaskSchedule.Name = "ctxMenuTaskSchedule";
      this.ctxMenuTaskSchedule.Size = new System.Drawing.Size(108, 26);
      this.ctxMenuTaskSchedule.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuScheduledTask_Opening);
      // 
      // ctxMenuTaskScheduleDelete
      // 
      this.ctxMenuTaskScheduleDelete.Name = "ctxMenuTaskScheduleDelete";
      this.ctxMenuTaskScheduleDelete.Size = new System.Drawing.Size(107, 22);
      this.ctxMenuTaskScheduleDelete.Tag = "DeleteTaskSchedule";
      this.ctxMenuTaskScheduleDelete.Text = "Delete";
      this.ctxMenuTaskScheduleDelete.Click += new System.EventHandler(this.Action);
      // 
      // cbRunUntilTask
      // 
      this.cbRunUntilTask.AutoSize = true;
      this.cbRunUntilTask.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.cbRunUntilTask.Location = new System.Drawing.Point(16, 20);
      this.cbRunUntilTask.Name = "cbRunUntilTask";
      this.cbRunUntilTask.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.cbRunUntilTask.Size = new System.Drawing.Size(97, 17);
      this.cbRunUntilTask.TabIndex = 21;
      this.cbRunUntilTask.Tag = "RunUntilTaskChange";
      this.cbRunUntilTask.Text = "Run Until Task";
      this.cbRunUntilTask.UseVisualStyleBackColor = true;
      this.cbRunUntilTask.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // cbRunUntilOverride
      // 
      this.cbRunUntilOverride.AutoSize = true;
      this.cbRunUntilOverride.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.cbRunUntilOverride.Enabled = false;
      this.cbRunUntilOverride.Location = new System.Drawing.Point(16, 154);
      this.cbRunUntilOverride.Name = "cbRunUntilOverride";
      this.cbRunUntilOverride.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.cbRunUntilOverride.Size = new System.Drawing.Size(113, 17);
      this.cbRunUntilOverride.TabIndex = 24;
      this.cbRunUntilOverride.Tag = "PropertyChange";
      this.cbRunUntilOverride.Text = "Run Until Override";
      this.cbRunUntilOverride.UseVisualStyleBackColor = true;
      this.cbRunUntilOverride.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // lblProcessorVersion
      // 
      this.lblProcessorVersion.AutoSize = true;
      this.lblProcessorVersion.Location = new System.Drawing.Point(225, 58);
      this.lblProcessorVersion.Name = "lblProcessorVersion";
      this.lblProcessorVersion.Size = new System.Drawing.Size(92, 13);
      this.lblProcessorVersion.TabIndex = 21;
      this.lblProcessorVersion.Text = "Processor Version";
      // 
      // txtProcessorVersion
      // 
      this.txtProcessorVersion.Location = new System.Drawing.Point(226, 75);
      this.txtProcessorVersion.Name = "txtProcessorVersion";
      this.txtProcessorVersion.Size = new System.Drawing.Size(188, 20);
      this.txtProcessorVersion.TabIndex = 4;
      this.txtProcessorVersion.Tag = "PropertyChange";
      this.txtProcessorVersion.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblPeriod
      // 
      this.lblPeriod.AutoSize = true;
      this.lblPeriod.Location = new System.Drawing.Point(13, 45);
      this.lblPeriod.Name = "lblPeriod";
      this.lblPeriod.Size = new System.Drawing.Size(37, 13);
      this.lblPeriod.TabIndex = 23;
      this.lblPeriod.Text = "Period";
      // 
      // cboPeriod
      // 
      this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPeriod.Enabled = false;
      this.cboPeriod.FormattingEnabled = true;
      this.cboPeriod.Items.AddRange(new object[] {
            "NULL"});
      this.cboPeriod.Location = new System.Drawing.Point(16, 62);
      this.cboPeriod.Name = "cboPeriod";
      this.cboPeriod.Size = new System.Drawing.Size(145, 21);
      this.cboPeriod.TabIndex = 22;
      this.cboPeriod.Tag = "PropertyChange";
      this.cboPeriod.TextChanged += new System.EventHandler(this.Action);
      // 
      // btnSave
      // 
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(17, 8);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 30;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(133, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 31;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // lblRunUntilOffsetMinutes
      // 
      this.lblRunUntilOffsetMinutes.AutoSize = true;
      this.lblRunUntilOffsetMinutes.Location = new System.Drawing.Point(13, 91);
      this.lblRunUntilOffsetMinutes.Name = "lblRunUntilOffsetMinutes";
      this.lblRunUntilOffsetMinutes.Size = new System.Drawing.Size(122, 13);
      this.lblRunUntilOffsetMinutes.TabIndex = 27;
      this.lblRunUntilOffsetMinutes.Text = "Run Until Offset Minutes";
      // 
      // txtRunUntilOffsetMinutes
      // 
      this.txtRunUntilOffsetMinutes.Enabled = false;
      this.txtRunUntilOffsetMinutes.Location = new System.Drawing.Point(15, 107);
      this.txtRunUntilOffsetMinutes.Name = "txtRunUntilOffsetMinutes";
      this.txtRunUntilOffsetMinutes.Size = new System.Drawing.Size(145, 20);
      this.txtRunUntilOffsetMinutes.TabIndex = 23;
      this.txtRunUntilOffsetMinutes.Tag = "PropertyChange";
      this.txtRunUntilOffsetMinutes.TextChanged += new System.EventHandler(this.Action);
      this.txtRunUntilOffsetMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunUntilOffsetMinutes_KeyPress);
      // 
      // chkTrackHistory
      // 
      this.chkTrackHistory.AutoSize = true;
      this.chkTrackHistory.Location = new System.Drawing.Point(740, 89);
      this.chkTrackHistory.Name = "chkTrackHistory";
      this.chkTrackHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.chkTrackHistory.Size = new System.Drawing.Size(89, 17);
      this.chkTrackHistory.TabIndex = 27;
      this.chkTrackHistory.Tag = "PropertyChange";
      this.chkTrackHistory.Text = "Track History";
      this.chkTrackHistory.UseVisualStyleBackColor = true;
      this.chkTrackHistory.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // btnNewTaskSchedule
      // 
      this.btnNewTaskSchedule.Location = new System.Drawing.Point(691, 8);
      this.btnNewTaskSchedule.Name = "btnNewTaskSchedule";
      this.btnNewTaskSchedule.Size = new System.Drawing.Size(138, 25);
      this.btnNewTaskSchedule.TabIndex = 32;
      this.btnNewTaskSchedule.Tag = "NewTaskSchedule";
      this.btnNewTaskSchedule.Text = "New Task Schedule";
      this.btnNewTaskSchedule.UseVisualStyleBackColor = true;
      this.btnNewTaskSchedule.Click += new System.EventHandler(this.Action);
      // 
      // pnlBottom
      // 
      this.pnlBottom.Controls.Add(this.btnSave);
      this.pnlBottom.Controls.Add(this.btnNewTaskSchedule);
      this.pnlBottom.Controls.Add(this.btnCancel);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 482);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(844, 40);
      this.pnlBottom.TabIndex = 31;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.grpBxRunUntilParameters);
      this.pnlTop.Controls.Add(this.chkSuppressNotificationsOnSuccess);
      this.pnlTop.Controls.Add(this.btnViewTaskParameters);
      this.pnlTop.Controls.Add(this.txtTaskName);
      this.pnlTop.Controls.Add(this.lblTaskName);
      this.pnlTop.Controls.Add(this.lblObjectTypeName);
      this.pnlTop.Controls.Add(this.txtObjectTypeName);
      this.pnlTop.Controls.Add(this.lblCatalogEntry);
      this.pnlTop.Controls.Add(this.txtCatalogEntry);
      this.pnlTop.Controls.Add(this.lblCatalogName);
      this.pnlTop.Controls.Add(this.txtCatalogName);
      this.pnlTop.Controls.Add(this.lblProcessorName);
      this.pnlTop.Controls.Add(this.txtProcessorName);
      this.pnlTop.Controls.Add(this.chkTrackHistory);
      this.pnlTop.Controls.Add(this.lblProcessorVersion);
      this.pnlTop.Controls.Add(this.txtProcessorVersion);
      this.pnlTop.Controls.Add(this.chkIsLongRunning);
      this.pnlTop.Controls.Add(this.cboProcessorType);
      this.pnlTop.Controls.Add(this.chkIsActive);
      this.pnlTop.Controls.Add(this.lblStoredProcedureName);
      this.pnlTop.Controls.Add(this.lblProcessorType);
      this.pnlTop.Controls.Add(this.txtStoredProcedureName);
      this.pnlTop.Controls.Add(this.lblAssemblyName);
      this.pnlTop.Controls.Add(this.txtAssemblyName);
      this.pnlTop.Controls.Add(this.lblAssemblyLocation);
      this.pnlTop.Controls.Add(this.txtAssemblyLocation);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(844, 252);
      this.pnlTop.TabIndex = 32;
      // 
      // chkSuppressNotificationsOnSuccess
      // 
      this.chkSuppressNotificationsOnSuccess.Location = new System.Drawing.Point(617, 107);
      this.chkSuppressNotificationsOnSuccess.Name = "chkSuppressNotificationsOnSuccess";
      this.chkSuppressNotificationsOnSuccess.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.chkSuppressNotificationsOnSuccess.Size = new System.Drawing.Size(212, 19);
      this.chkSuppressNotificationsOnSuccess.TabIndex = 28;
      this.chkSuppressNotificationsOnSuccess.Tag = "PropertyChange";
      this.chkSuppressNotificationsOnSuccess.Text = "Suppress Notifications On Success";
      this.chkSuppressNotificationsOnSuccess.UseVisualStyleBackColor = true;
      this.chkSuppressNotificationsOnSuccess.CheckedChanged += new System.EventHandler(this.Action);
      // 
      // grpBxRunUntilParameters
      // 
      this.grpBxRunUntilParameters.Controls.Add(this.cboPeriod);
      this.grpBxRunUntilParameters.Controls.Add(this.cbRunUntilTask);
      this.grpBxRunUntilParameters.Controls.Add(this.cbRunUntilOverride);
      this.grpBxRunUntilParameters.Controls.Add(this.txtRunUntilOffsetMinutes);
      this.grpBxRunUntilParameters.Controls.Add(this.lblPeriod);
      this.grpBxRunUntilParameters.Controls.Add(this.lblRunUntilOffsetMinutes);
      this.grpBxRunUntilParameters.Location = new System.Drawing.Point(435, 13);
      this.grpBxRunUntilParameters.Name = "grpBxRunUntilParameters";
      this.grpBxRunUntilParameters.Size = new System.Drawing.Size(173, 182);
      this.grpBxRunUntilParameters.TabIndex = 31;
      this.grpBxRunUntilParameters.TabStop = false;
      this.grpBxRunUntilParameters.Text = "Run Until Parameters";
      // 
      // btnViewTaskParameters
      // 
      this.btnViewTaskParameters.Location = new System.Drawing.Point(691, 194);
      this.btnViewTaskParameters.Name = "btnViewTaskParameters";
      this.btnViewTaskParameters.Size = new System.Drawing.Size(138, 36);
      this.btnViewTaskParameters.TabIndex = 29;
      this.btnViewTaskParameters.Tag = "ViewTaskParameters";
      this.btnViewTaskParameters.Text = "View Task Parameters";
      this.btnViewTaskParameters.UseVisualStyleBackColor = true;
      this.btnViewTaskParameters.Click += new System.EventHandler(this.Action);
      // 
      // txtAssemblyName
      // 
      this.txtAssemblyName.Location = new System.Drawing.Point(17, 165);
      this.txtAssemblyName.Name = "txtAssemblyName";
      this.txtAssemblyName.Size = new System.Drawing.Size(188, 20);
      this.txtAssemblyName.TabIndex = 7;
      this.txtAssemblyName.Tag = "PropertyChange";
      this.txtAssemblyName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblAssemblyName
      // 
      this.lblAssemblyName.AutoSize = true;
      this.lblAssemblyName.Location = new System.Drawing.Point(14, 149);
      this.lblAssemblyName.Name = "lblAssemblyName";
      this.lblAssemblyName.Size = new System.Drawing.Size(82, 13);
      this.lblAssemblyName.TabIndex = 14;
      this.lblAssemblyName.Text = "Assembly Name";
      // 
      // txtCatalogName
      // 
      this.txtCatalogName.Location = new System.Drawing.Point(226, 165);
      this.txtCatalogName.Name = "txtCatalogName";
      this.txtCatalogName.Size = new System.Drawing.Size(188, 20);
      this.txtCatalogName.TabIndex = 8;
      this.txtCatalogName.Tag = "PropertyChange";
      this.txtCatalogName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblCatalogName
      // 
      this.lblCatalogName.AutoSize = true;
      this.lblCatalogName.Location = new System.Drawing.Point(225, 149);
      this.lblCatalogName.Name = "lblCatalogName";
      this.lblCatalogName.Size = new System.Drawing.Size(74, 13);
      this.lblCatalogName.TabIndex = 6;
      this.lblCatalogName.Text = "Catalog Name";
      // 
      // txtCatalogEntry
      // 
      this.txtCatalogEntry.Location = new System.Drawing.Point(17, 210);
      this.txtCatalogEntry.Name = "txtCatalogEntry";
      this.txtCatalogEntry.Size = new System.Drawing.Size(188, 20);
      this.txtCatalogEntry.TabIndex = 9;
      this.txtCatalogEntry.Tag = "PropertyChange";
      this.txtCatalogEntry.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblCatalogEntry
      // 
      this.lblCatalogEntry.AutoSize = true;
      this.lblCatalogEntry.Location = new System.Drawing.Point(17, 194);
      this.lblCatalogEntry.Name = "lblCatalogEntry";
      this.lblCatalogEntry.Size = new System.Drawing.Size(70, 13);
      this.lblCatalogEntry.TabIndex = 6;
      this.lblCatalogEntry.Text = "Catalog Entry";
      // 
      // txtObjectTypeName
      // 
      this.txtObjectTypeName.Location = new System.Drawing.Point(225, 210);
      this.txtObjectTypeName.Name = "txtObjectTypeName";
      this.txtObjectTypeName.Size = new System.Drawing.Size(370, 20);
      this.txtObjectTypeName.TabIndex = 10;
      this.txtObjectTypeName.Tag = "PropertyChange";
      this.txtObjectTypeName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblObjectTypeName
      // 
      this.lblObjectTypeName.AutoSize = true;
      this.lblObjectTypeName.Location = new System.Drawing.Point(225, 194);
      this.lblObjectTypeName.Name = "lblObjectTypeName";
      this.lblObjectTypeName.Size = new System.Drawing.Size(189, 13);
      this.lblObjectTypeName.TabIndex = 6;
      this.lblObjectTypeName.Text = "Object Type Name (w/full namespace)";
      // 
      // frmScheduledTask
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 542);
      this.ControlBox = false;
      this.Controls.Add(this.gvTaskSchedules);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.lblStatus);
      this.Name = "frmScheduledTask";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Scheduled Task";
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskSchedules)).EndInit();
      this.ctxMenuTaskSchedule.ResumeLayout(false);
      this.pnlBottom.ResumeLayout(false);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.grpBxRunUntilParameters.ResumeLayout(false);
      this.grpBxRunUntilParameters.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblTaskName;
    private System.Windows.Forms.TextBox txtTaskName;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.CheckBox chkIsLongRunning;
    private System.Windows.Forms.Label lblProcessorName;
    private System.Windows.Forms.TextBox txtProcessorName;
    private System.Windows.Forms.Label lblAssemblyLocation;
    private System.Windows.Forms.TextBox txtAssemblyLocation;
    private System.Windows.Forms.TextBox txtStoredProcedureName;
    private System.Windows.Forms.Label lblStoredProcedureName;
    private System.Windows.Forms.Label lblProcessorType;
    private System.Windows.Forms.ComboBox cboProcessorType;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.DataGridView gvTaskSchedules;
    private System.Windows.Forms.CheckBox cbRunUntilTask;
    private System.Windows.Forms.CheckBox cbRunUntilOverride;
    private System.Windows.Forms.Label lblProcessorVersion;
    private System.Windows.Forms.TextBox txtProcessorVersion;
    private System.Windows.Forms.Label lblPeriod;
    private System.Windows.Forms.ComboBox cboPeriod;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblRunUntilOffsetMinutes;
    private System.Windows.Forms.TextBox txtRunUntilOffsetMinutes;
    private System.Windows.Forms.CheckBox chkTrackHistory;
    private System.Windows.Forms.Button btnNewTaskSchedule;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.ContextMenuStrip ctxMenuTaskSchedule;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTaskScheduleDelete;
    private System.Windows.Forms.Button btnViewTaskParameters;
    private System.Windows.Forms.GroupBox grpBxRunUntilParameters;
    private System.Windows.Forms.CheckBox chkSuppressNotificationsOnSuccess;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label lblObjectTypeName;
    private System.Windows.Forms.TextBox txtObjectTypeName;
    private System.Windows.Forms.Label lblCatalogEntry;
    private System.Windows.Forms.TextBox txtCatalogEntry;
    private System.Windows.Forms.Label lblCatalogName;
    private System.Windows.Forms.TextBox txtCatalogName;
    private System.Windows.Forms.Label lblAssemblyName;
    private System.Windows.Forms.TextBox txtAssemblyName;
  }
}