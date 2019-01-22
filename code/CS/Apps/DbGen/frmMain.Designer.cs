namespace Org.DbGen
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
      this.txtOut = new System.Windows.Forms.TextBox();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.tabTopControls = new System.Windows.Forms.TabControl();
      this.tabPageDbGen = new System.Windows.Forms.TabPage();
      this.cboTables = new System.Windows.Forms.ComboBox();
      this.btnCheckExists = new System.Windows.Forms.Button();
      this.btnGenerateCode = new System.Windows.Forms.Button();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.btnGetTableData = new System.Windows.Forms.Button();
      this.txtDbClassesPath = new System.Windows.Forms.TextBox();
      this.btnTestTableRecord = new System.Windows.Forms.Button();
      this.txtColumn = new System.Windows.Forms.TextBox();
      this.btnLoopGenericObject = new System.Windows.Forms.Button();
      this.txtSeriesTimes = new System.Windows.Forms.TextBox();
      this.btnGetBusinessObject = new System.Windows.Forms.Button();
      this.lblSeriesTimes = new System.Windows.Forms.Label();
      this.btnLoopBusinessObject = new System.Windows.Forms.Button();
      this.txtLoopCount = new System.Windows.Forms.TextBox();
      this.btnLoopCodedQuery = new System.Windows.Forms.Button();
      this.lblValue = new System.Windows.Forms.Label();
      this.btnRunSeries = new System.Windows.Forms.Button();
      this.lblTimes = new System.Windows.Forms.Label();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.lblDbClassesPath = new System.Windows.Forms.Label();
      this.ckRefreshProject = new System.Windows.Forms.CheckBox();
      this.lblColumn = new System.Windows.Forms.Label();
      this.lblTables = new System.Windows.Forms.Label();
      this.tabPageSchema = new System.Windows.Forms.TabPage();
      this.ckSqlAuth2 = new System.Windows.Forms.CheckBox();
      this.ckSqlAuth1 = new System.Windows.Forms.CheckBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.lblUserID = new System.Windows.Forms.Label();
      this.lblSqlAuth = new System.Windows.Forms.Label();
      this.lblDatabase = new System.Windows.Forms.Label();
      this.lblInstance = new System.Windows.Forms.Label();
      this.txtPassword2 = new System.Windows.Forms.TextBox();
      this.txtUserID2 = new System.Windows.Forms.TextBox();
      this.txtInstance2 = new System.Windows.Forms.TextBox();
      this.txtDb2 = new System.Windows.Forms.TextBox();
      this.txtPassword1 = new System.Windows.Forms.TextBox();
      this.txtUserID1 = new System.Windows.Forms.TextBox();
      this.txtDb1 = new System.Windows.Forms.TextBox();
      this.txtInstance1 = new System.Windows.Forms.TextBox();
      this.btnGetSchema2 = new System.Windows.Forms.Button();
      this.btnGetSchema1 = new System.Windows.Forms.Button();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageMain = new System.Windows.Forms.TabPage();
      this.tabPageSecondary = new System.Windows.Forms.TabPage();
      this.txtOut2 = new System.Windows.Forms.TextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.tabPageCompare = new System.Windows.Forms.TabPage();
      this.btnCompareSchemas = new System.Windows.Forms.Button();
      this.browserCompare = new System.Windows.Forms.WebBrowser();
      this.pnlTop.SuspendLayout();
      this.tabTopControls.SuspendLayout();
      this.tabPageDbGen.SuspendLayout();
      this.tabPageSchema.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageMain.SuspendLayout();
      this.tabPageSecondary.SuspendLayout();
      this.tabPageCompare.SuspendLayout();
      this.SuspendLayout();
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Lucida Console", 9F);
      this.txtOut.Location = new System.Drawing.Point(3, 3);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1391, 583);
      this.txtOut.TabIndex = 0;
      this.txtOut.WordWrap = false;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.tabTopControls);
      this.pnlTop.Controls.Add(this.mnuMain);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1405, 220);
      this.pnlTop.TabIndex = 1;
      //
      // tabTopControls
      //
      this.tabTopControls.Controls.Add(this.tabPageDbGen);
      this.tabTopControls.Controls.Add(this.tabPageSchema);
      this.tabTopControls.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabTopControls.ItemSize = new System.Drawing.Size(120, 18);
      this.tabTopControls.Location = new System.Drawing.Point(0, 24);
      this.tabTopControls.Name = "tabTopControls";
      this.tabTopControls.SelectedIndex = 0;
      this.tabTopControls.Size = new System.Drawing.Size(1405, 196);
      this.tabTopControls.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabTopControls.TabIndex = 9;
      //
      // tabPageDbGen
      //
      this.tabPageDbGen.Controls.Add(this.cboTables);
      this.tabPageDbGen.Controls.Add(this.btnCheckExists);
      this.tabPageDbGen.Controls.Add(this.btnGenerateCode);
      this.tabPageDbGen.Controls.Add(this.txtValue);
      this.tabPageDbGen.Controls.Add(this.btnGetTableData);
      this.tabPageDbGen.Controls.Add(this.txtDbClassesPath);
      this.tabPageDbGen.Controls.Add(this.btnTestTableRecord);
      this.tabPageDbGen.Controls.Add(this.txtColumn);
      this.tabPageDbGen.Controls.Add(this.btnLoopGenericObject);
      this.tabPageDbGen.Controls.Add(this.txtSeriesTimes);
      this.tabPageDbGen.Controls.Add(this.btnGetBusinessObject);
      this.tabPageDbGen.Controls.Add(this.lblSeriesTimes);
      this.tabPageDbGen.Controls.Add(this.btnLoopBusinessObject);
      this.tabPageDbGen.Controls.Add(this.txtLoopCount);
      this.tabPageDbGen.Controls.Add(this.btnLoopCodedQuery);
      this.tabPageDbGen.Controls.Add(this.lblValue);
      this.tabPageDbGen.Controls.Add(this.btnRunSeries);
      this.tabPageDbGen.Controls.Add(this.lblTimes);
      this.tabPageDbGen.Controls.Add(this.btnClearDisplay);
      this.tabPageDbGen.Controls.Add(this.lblDbClassesPath);
      this.tabPageDbGen.Controls.Add(this.ckRefreshProject);
      this.tabPageDbGen.Controls.Add(this.lblColumn);
      this.tabPageDbGen.Controls.Add(this.lblTables);
      this.tabPageDbGen.Location = new System.Drawing.Point(4, 22);
      this.tabPageDbGen.Name = "tabPageDbGen";
      this.tabPageDbGen.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDbGen.Size = new System.Drawing.Size(1397, 170);
      this.tabPageDbGen.TabIndex = 0;
      this.tabPageDbGen.Text = "Db Gen";
      this.tabPageDbGen.UseVisualStyleBackColor = true;
      //
      // cboTables
      //
      this.cboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTables.FormattingEnabled = true;
      this.cboTables.Location = new System.Drawing.Point(172, 29);
      this.cboTables.Name = "cboTables";
      this.cboTables.Size = new System.Drawing.Size(223, 21);
      this.cboTables.TabIndex = 1;
      //
      // btnCheckExists
      //
      this.btnCheckExists.Location = new System.Drawing.Point(401, 30);
      this.btnCheckExists.Name = "btnCheckExists";
      this.btnCheckExists.Size = new System.Drawing.Size(223, 23);
      this.btnCheckExists.TabIndex = 8;
      this.btnCheckExists.Text = "Check If Exists";
      this.btnCheckExists.UseVisualStyleBackColor = true;
      this.btnCheckExists.Click += new System.EventHandler(this.btnCheckExists_Click);
      //
      // btnGenerateCode
      //
      this.btnGenerateCode.Location = new System.Drawing.Point(8, 28);
      this.btnGenerateCode.Name = "btnGenerateCode";
      this.btnGenerateCode.Size = new System.Drawing.Size(139, 23);
      this.btnGenerateCode.TabIndex = 0;
      this.btnGenerateCode.Text = "Generate Code";
      this.btnGenerateCode.UseVisualStyleBackColor = true;
      this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
      //
      // txtValue
      //
      this.txtValue.Location = new System.Drawing.Point(401, 75);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(223, 20);
      this.txtValue.TabIndex = 7;
      //
      // btnGetTableData
      //
      this.btnGetTableData.Location = new System.Drawing.Point(668, 28);
      this.btnGetTableData.Name = "btnGetTableData";
      this.btnGetTableData.Size = new System.Drawing.Size(159, 23);
      this.btnGetTableData.TabIndex = 0;
      this.btnGetTableData.Text = "Get Generic Object";
      this.btnGetTableData.UseVisualStyleBackColor = true;
      this.btnGetTableData.Click += new System.EventHandler(this.btnGetTableData_Click);
      //
      // txtDbClassesPath
      //
      this.txtDbClassesPath.Location = new System.Drawing.Point(172, 118);
      this.txtDbClassesPath.Name = "txtDbClassesPath";
      this.txtDbClassesPath.Size = new System.Drawing.Size(655, 20);
      this.txtDbClassesPath.TabIndex = 6;
      //
      // btnTestTableRecord
      //
      this.btnTestTableRecord.Location = new System.Drawing.Point(833, 115);
      this.btnTestTableRecord.Name = "btnTestTableRecord";
      this.btnTestTableRecord.Size = new System.Drawing.Size(159, 23);
      this.btnTestTableRecord.TabIndex = 0;
      this.btnTestTableRecord.Text = "Write TestTable Record";
      this.btnTestTableRecord.UseVisualStyleBackColor = true;
      this.btnTestTableRecord.Click += new System.EventHandler(this.btnTestTableRecord_Click);
      //
      // txtColumn
      //
      this.txtColumn.Location = new System.Drawing.Point(172, 75);
      this.txtColumn.Name = "txtColumn";
      this.txtColumn.Size = new System.Drawing.Size(223, 20);
      this.txtColumn.TabIndex = 6;
      //
      // btnLoopGenericObject
      //
      this.btnLoopGenericObject.Location = new System.Drawing.Point(833, 28);
      this.btnLoopGenericObject.Name = "btnLoopGenericObject";
      this.btnLoopGenericObject.Size = new System.Drawing.Size(159, 23);
      this.btnLoopGenericObject.TabIndex = 0;
      this.btnLoopGenericObject.Text = "Loop Generic Object";
      this.btnLoopGenericObject.UseVisualStyleBackColor = true;
      this.btnLoopGenericObject.Click += new System.EventHandler(this.btnLoopGenericObject_Click);
      //
      // txtSeriesTimes
      //
      this.txtSeriesTimes.Location = new System.Drawing.Point(1177, 87);
      this.txtSeriesTimes.Name = "txtSeriesTimes";
      this.txtSeriesTimes.Size = new System.Drawing.Size(58, 20);
      this.txtSeriesTimes.TabIndex = 4;
      this.txtSeriesTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      //
      // btnGetBusinessObject
      //
      this.btnGetBusinessObject.Location = new System.Drawing.Point(668, 57);
      this.btnGetBusinessObject.Name = "btnGetBusinessObject";
      this.btnGetBusinessObject.Size = new System.Drawing.Size(159, 23);
      this.btnGetBusinessObject.TabIndex = 0;
      this.btnGetBusinessObject.Text = "Get Business Object";
      this.btnGetBusinessObject.UseVisualStyleBackColor = true;
      this.btnGetBusinessObject.Click += new System.EventHandler(this.btnGetBusinessObject_Click);
      //
      // lblSeriesTimes
      //
      this.lblSeriesTimes.AutoSize = true;
      this.lblSeriesTimes.Location = new System.Drawing.Point(1241, 90);
      this.lblSeriesTimes.Name = "lblSeriesTimes";
      this.lblSeriesTimes.Size = new System.Drawing.Size(35, 13);
      this.lblSeriesTimes.TabIndex = 3;
      this.lblSeriesTimes.Text = "Times";
      //
      // btnLoopBusinessObject
      //
      this.btnLoopBusinessObject.Location = new System.Drawing.Point(833, 57);
      this.btnLoopBusinessObject.Name = "btnLoopBusinessObject";
      this.btnLoopBusinessObject.Size = new System.Drawing.Size(159, 23);
      this.btnLoopBusinessObject.TabIndex = 0;
      this.btnLoopBusinessObject.Text = "Loop Business Object";
      this.btnLoopBusinessObject.UseVisualStyleBackColor = true;
      this.btnLoopBusinessObject.Click += new System.EventHandler(this.btnLoopBusinessObject_Click);
      //
      // txtLoopCount
      //
      this.txtLoopCount.Location = new System.Drawing.Point(1010, 30);
      this.txtLoopCount.Name = "txtLoopCount";
      this.txtLoopCount.Size = new System.Drawing.Size(58, 20);
      this.txtLoopCount.TabIndex = 4;
      this.txtLoopCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      //
      // btnLoopCodedQuery
      //
      this.btnLoopCodedQuery.Location = new System.Drawing.Point(833, 86);
      this.btnLoopCodedQuery.Name = "btnLoopCodedQuery";
      this.btnLoopCodedQuery.Size = new System.Drawing.Size(159, 23);
      this.btnLoopCodedQuery.TabIndex = 0;
      this.btnLoopCodedQuery.Text = "Loop Coded Query";
      this.btnLoopCodedQuery.UseVisualStyleBackColor = true;
      this.btnLoopCodedQuery.Click += new System.EventHandler(this.btnLoopCodedQuery_Click);
      //
      // lblValue
      //
      this.lblValue.AutoSize = true;
      this.lblValue.Location = new System.Drawing.Point(398, 59);
      this.lblValue.Name = "lblValue";
      this.lblValue.Size = new System.Drawing.Size(34, 13);
      this.lblValue.TabIndex = 3;
      this.lblValue.Text = "Value";
      //
      // btnRunSeries
      //
      this.btnRunSeries.Location = new System.Drawing.Point(1010, 86);
      this.btnRunSeries.Name = "btnRunSeries";
      this.btnRunSeries.Size = new System.Drawing.Size(159, 23);
      this.btnRunSeries.TabIndex = 0;
      this.btnRunSeries.Text = "Run Test Series";
      this.btnRunSeries.UseVisualStyleBackColor = true;
      this.btnRunSeries.Click += new System.EventHandler(this.btnRunSeries_Click);
      //
      // lblTimes
      //
      this.lblTimes.AutoSize = true;
      this.lblTimes.Location = new System.Drawing.Point(1074, 33);
      this.lblTimes.Name = "lblTimes";
      this.lblTimes.Size = new System.Drawing.Size(35, 13);
      this.lblTimes.TabIndex = 3;
      this.lblTimes.Text = "Times";
      //
      // btnClearDisplay
      //
      this.btnClearDisplay.Location = new System.Drawing.Point(1197, 28);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(139, 23);
      this.btnClearDisplay.TabIndex = 0;
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.btnClearDisplay_Click);
      //
      // lblDbClassesPath
      //
      this.lblDbClassesPath.AutoSize = true;
      this.lblDbClassesPath.Location = new System.Drawing.Point(169, 102);
      this.lblDbClassesPath.Name = "lblDbClassesPath";
      this.lblDbClassesPath.Size = new System.Drawing.Size(117, 13);
      this.lblDbClassesPath.TabIndex = 3;
      this.lblDbClassesPath.Text = "Database Classes Path";
      //
      // ckRefreshProject
      //
      this.ckRefreshProject.AutoSize = true;
      this.ckRefreshProject.Location = new System.Drawing.Point(172, 142);
      this.ckRefreshProject.Name = "ckRefreshProject";
      this.ckRefreshProject.Size = new System.Drawing.Size(91, 17);
      this.ckRefreshProject.TabIndex = 2;
      this.ckRefreshProject.Text = "Refresh Code";
      this.ckRefreshProject.UseVisualStyleBackColor = true;
      //
      // lblColumn
      //
      this.lblColumn.AutoSize = true;
      this.lblColumn.Location = new System.Drawing.Point(169, 59);
      this.lblColumn.Name = "lblColumn";
      this.lblColumn.Size = new System.Drawing.Size(42, 13);
      this.lblColumn.TabIndex = 3;
      this.lblColumn.Text = "Column";
      //
      // lblTables
      //
      this.lblTables.AutoSize = true;
      this.lblTables.Location = new System.Drawing.Point(169, 12);
      this.lblTables.Name = "lblTables";
      this.lblTables.Size = new System.Drawing.Size(39, 13);
      this.lblTables.TabIndex = 3;
      this.lblTables.Text = "Tables";
      //
      // tabPageSchema
      //
      this.tabPageSchema.Controls.Add(this.ckSqlAuth2);
      this.tabPageSchema.Controls.Add(this.ckSqlAuth1);
      this.tabPageSchema.Controls.Add(this.lblPassword);
      this.tabPageSchema.Controls.Add(this.lblUserID);
      this.tabPageSchema.Controls.Add(this.lblSqlAuth);
      this.tabPageSchema.Controls.Add(this.lblDatabase);
      this.tabPageSchema.Controls.Add(this.lblInstance);
      this.tabPageSchema.Controls.Add(this.txtPassword2);
      this.tabPageSchema.Controls.Add(this.txtUserID2);
      this.tabPageSchema.Controls.Add(this.txtInstance2);
      this.tabPageSchema.Controls.Add(this.txtDb2);
      this.tabPageSchema.Controls.Add(this.txtPassword1);
      this.tabPageSchema.Controls.Add(this.txtUserID1);
      this.tabPageSchema.Controls.Add(this.txtDb1);
      this.tabPageSchema.Controls.Add(this.txtInstance1);
      this.tabPageSchema.Controls.Add(this.btnCompareSchemas);
      this.tabPageSchema.Controls.Add(this.btnGetSchema2);
      this.tabPageSchema.Controls.Add(this.btnGetSchema1);
      this.tabPageSchema.Location = new System.Drawing.Point(4, 22);
      this.tabPageSchema.Name = "tabPageSchema";
      this.tabPageSchema.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSchema.Size = new System.Drawing.Size(1397, 170);
      this.tabPageSchema.TabIndex = 1;
      this.tabPageSchema.Text = "Schema Compare";
      this.tabPageSchema.UseVisualStyleBackColor = true;
      //
      // ckSqlAuth2
      //
      this.ckSqlAuth2.AutoSize = true;
      this.ckSqlAuth2.Location = new System.Drawing.Point(622, 67);
      this.ckSqlAuth2.Name = "ckSqlAuth2";
      this.ckSqlAuth2.Size = new System.Drawing.Size(15, 14);
      this.ckSqlAuth2.TabIndex = 3;
      this.ckSqlAuth2.Tag = "MANAGE_SQLAUTH_VISIBILITY";
      this.ckSqlAuth2.UseVisualStyleBackColor = true;
      this.ckSqlAuth2.CheckedChanged += new System.EventHandler(this.Action);
      //
      // ckSqlAuth1
      //
      this.ckSqlAuth1.AutoSize = true;
      this.ckSqlAuth1.Location = new System.Drawing.Point(622, 38);
      this.ckSqlAuth1.Name = "ckSqlAuth1";
      this.ckSqlAuth1.Size = new System.Drawing.Size(15, 14);
      this.ckSqlAuth1.TabIndex = 3;
      this.ckSqlAuth1.Tag = "MANAGE_SQLAUTH_VISIBILITY";
      this.ckSqlAuth1.UseVisualStyleBackColor = true;
      this.ckSqlAuth1.CheckedChanged += new System.EventHandler(this.Action);
      //
      // lblPassword
      //
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(795, 16);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(53, 13);
      this.lblPassword.TabIndex = 2;
      this.lblPassword.Text = "Password";
      //
      // lblUserID
      //
      this.lblUserID.AutoSize = true;
      this.lblUserID.Location = new System.Drawing.Point(664, 16);
      this.lblUserID.Name = "lblUserID";
      this.lblUserID.Size = new System.Drawing.Size(43, 13);
      this.lblUserID.TabIndex = 2;
      this.lblUserID.Text = "User ID";
      //
      // lblSqlAuth
      //
      this.lblSqlAuth.AutoSize = true;
      this.lblSqlAuth.Location = new System.Drawing.Point(604, 16);
      this.lblSqlAuth.Name = "lblSqlAuth";
      this.lblSqlAuth.Size = new System.Drawing.Size(53, 13);
      this.lblSqlAuth.TabIndex = 2;
      this.lblSqlAuth.Text = "SQL Auth";
      //
      // lblDatabase
      //
      this.lblDatabase.AutoSize = true;
      this.lblDatabase.Location = new System.Drawing.Point(394, 16);
      this.lblDatabase.Name = "lblDatabase";
      this.lblDatabase.Size = new System.Drawing.Size(53, 13);
      this.lblDatabase.TabIndex = 2;
      this.lblDatabase.Text = "Database";
      //
      // lblInstance
      //
      this.lblInstance.AutoSize = true;
      this.lblInstance.Location = new System.Drawing.Point(194, 16);
      this.lblInstance.Name = "lblInstance";
      this.lblInstance.Size = new System.Drawing.Size(72, 13);
      this.lblInstance.TabIndex = 2;
      this.lblInstance.Text = "SQL Instance";
      //
      // txtPassword2
      //
      this.txtPassword2.Location = new System.Drawing.Point(798, 65);
      this.txtPassword2.Name = "txtPassword2";
      this.txtPassword2.PasswordChar = '*';
      this.txtPassword2.Size = new System.Drawing.Size(125, 20);
      this.txtPassword2.TabIndex = 1;
      //
      // txtUserID2
      //
      this.txtUserID2.Location = new System.Drawing.Point(667, 65);
      this.txtUserID2.Name = "txtUserID2";
      this.txtUserID2.Size = new System.Drawing.Size(125, 20);
      this.txtUserID2.TabIndex = 1;
      //
      // txtInstance2
      //
      this.txtInstance2.Location = new System.Drawing.Point(194, 65);
      this.txtInstance2.Name = "txtInstance2";
      this.txtInstance2.Size = new System.Drawing.Size(197, 20);
      this.txtInstance2.TabIndex = 1;
      //
      // txtDb2
      //
      this.txtDb2.Location = new System.Drawing.Point(397, 65);
      this.txtDb2.Name = "txtDb2";
      this.txtDb2.Size = new System.Drawing.Size(197, 20);
      this.txtDb2.TabIndex = 1;
      //
      // txtPassword1
      //
      this.txtPassword1.Location = new System.Drawing.Point(798, 35);
      this.txtPassword1.Name = "txtPassword1";
      this.txtPassword1.PasswordChar = '*';
      this.txtPassword1.Size = new System.Drawing.Size(125, 20);
      this.txtPassword1.TabIndex = 1;
      //
      // txtUserID1
      //
      this.txtUserID1.Location = new System.Drawing.Point(667, 35);
      this.txtUserID1.Name = "txtUserID1";
      this.txtUserID1.Size = new System.Drawing.Size(125, 20);
      this.txtUserID1.TabIndex = 1;
      //
      // txtDb1
      //
      this.txtDb1.Location = new System.Drawing.Point(397, 35);
      this.txtDb1.Name = "txtDb1";
      this.txtDb1.Size = new System.Drawing.Size(197, 20);
      this.txtDb1.TabIndex = 1;
      //
      // txtInstance1
      //
      this.txtInstance1.Location = new System.Drawing.Point(194, 35);
      this.txtInstance1.Name = "txtInstance1";
      this.txtInstance1.Size = new System.Drawing.Size(197, 20);
      this.txtInstance1.TabIndex = 1;
      //
      // btnGetSchema2
      //
      this.btnGetSchema2.Location = new System.Drawing.Point(34, 63);
      this.btnGetSchema2.Name = "btnGetSchema2";
      this.btnGetSchema2.Size = new System.Drawing.Size(139, 23);
      this.btnGetSchema2.TabIndex = 0;
      this.btnGetSchema2.Tag = "GET_SCHEMA_2";
      this.btnGetSchema2.Text = "Get Schema 2";
      this.btnGetSchema2.UseVisualStyleBackColor = true;
      this.btnGetSchema2.Click += new System.EventHandler(this.Action);
      //
      // btnGetSchema1
      //
      this.btnGetSchema1.Location = new System.Drawing.Point(34, 34);
      this.btnGetSchema1.Name = "btnGetSchema1";
      this.btnGetSchema1.Size = new System.Drawing.Size(139, 23);
      this.btnGetSchema1.TabIndex = 0;
      this.btnGetSchema1.Tag = "GET_SCHEMA_1";
      this.btnGetSchema1.Text = "Get Schema 1";
      this.btnGetSchema1.UseVisualStyleBackColor = true;
      this.btnGetSchema1.Click += new System.EventHandler(this.Action);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1405, 24);
      this.mnuMain.TabIndex = 10;
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
      this.mnuFileExit.Tag = "EXIT";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageMain);
      this.tabMain.Controls.Add(this.tabPageSecondary);
      this.tabMain.Controls.Add(this.tabPageCompare);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(120, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 220);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1405, 615);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 2;
      //
      // tabPageMain
      //
      this.tabPageMain.Controls.Add(this.txtOut);
      this.tabPageMain.Location = new System.Drawing.Point(4, 22);
      this.tabPageMain.Name = "tabPageMain";
      this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageMain.Size = new System.Drawing.Size(1397, 589);
      this.tabPageMain.TabIndex = 0;
      this.tabPageMain.Text = "Primary Output";
      this.tabPageMain.UseVisualStyleBackColor = true;
      //
      // tabPageSecondary
      //
      this.tabPageSecondary.Controls.Add(this.txtOut2);
      this.tabPageSecondary.Controls.Add(this.textBox1);
      this.tabPageSecondary.Location = new System.Drawing.Point(4, 22);
      this.tabPageSecondary.Name = "tabPageSecondary";
      this.tabPageSecondary.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSecondary.Size = new System.Drawing.Size(1397, 589);
      this.tabPageSecondary.TabIndex = 1;
      this.tabPageSecondary.Text = "Secondary Output";
      this.tabPageSecondary.UseVisualStyleBackColor = true;
      //
      // txtOut2
      //
      this.txtOut2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut2.Font = new System.Drawing.Font("Lucida Console", 9F);
      this.txtOut2.Location = new System.Drawing.Point(3, 3);
      this.txtOut2.Multiline = true;
      this.txtOut2.Name = "txtOut2";
      this.txtOut2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut2.Size = new System.Drawing.Size(1391, 583);
      this.txtOut2.TabIndex = 2;
      this.txtOut2.WordWrap = false;
      //
      // textBox1
      //
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.textBox1.Location = new System.Drawing.Point(3, 3);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(1391, 583);
      this.textBox1.TabIndex = 1;
      this.textBox1.WordWrap = false;
      //
      // tabPageCompare
      //
      this.tabPageCompare.Controls.Add(this.browserCompare);
      this.tabPageCompare.Location = new System.Drawing.Point(4, 22);
      this.tabPageCompare.Name = "tabPageCompare";
      this.tabPageCompare.Size = new System.Drawing.Size(1397, 589);
      this.tabPageCompare.TabIndex = 2;
      this.tabPageCompare.Text = "Comparison";
      this.tabPageCompare.UseVisualStyleBackColor = true;
      //
      // btnCompareSchemas
      //
      this.btnCompareSchemas.Location = new System.Drawing.Point(34, 92);
      this.btnCompareSchemas.Name = "btnCompareSchemas";
      this.btnCompareSchemas.Size = new System.Drawing.Size(139, 23);
      this.btnCompareSchemas.TabIndex = 0;
      this.btnCompareSchemas.Tag = "COMPARE_SCHEMAS";
      this.btnCompareSchemas.Text = "Compare Schemas";
      this.btnCompareSchemas.UseVisualStyleBackColor = true;
      this.btnCompareSchemas.Click += new System.EventHandler(this.Action);
      //
      // browserCompare
      //
      this.browserCompare.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browserCompare.Location = new System.Drawing.Point(0, 0);
      this.browserCompare.Margin = new System.Windows.Forms.Padding(2);
      this.browserCompare.MinimumSize = new System.Drawing.Size(15, 16);
      this.browserCompare.Name = "browserCompare";
      this.browserCompare.Size = new System.Drawing.Size(1397, 589);
      this.browserCompare.TabIndex = 2;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1405, 835);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTop);
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Tag = "MAIN_FORM_SHOWN";
      this.Text = "DbGen";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.Action);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabTopControls.ResumeLayout(false);
      this.tabPageDbGen.ResumeLayout(false);
      this.tabPageDbGen.PerformLayout();
      this.tabPageSchema.ResumeLayout(false);
      this.tabPageSchema.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageMain.ResumeLayout(false);
      this.tabPageMain.PerformLayout();
      this.tabPageSecondary.ResumeLayout(false);
      this.tabPageSecondary.PerformLayout();
      this.tabPageCompare.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnGenerateCode;
    private System.Windows.Forms.CheckBox ckRefreshProject;
    private System.Windows.Forms.ComboBox cboTables;
    private System.Windows.Forms.Button btnGetTableData;
    private System.Windows.Forms.Label lblTables;
    private System.Windows.Forms.Button btnClearDisplay;
    private System.Windows.Forms.Button btnGetBusinessObject;
    private System.Windows.Forms.Button btnLoopBusinessObject;
    private System.Windows.Forms.Button btnLoopGenericObject;
    private System.Windows.Forms.TextBox txtLoopCount;
    private System.Windows.Forms.Label lblTimes;
    private System.Windows.Forms.Button btnLoopCodedQuery;
    private System.Windows.Forms.TextBox txtSeriesTimes;
    private System.Windows.Forms.Label lblSeriesTimes;
    private System.Windows.Forms.Button btnRunSeries;
    private System.Windows.Forms.Button btnTestTableRecord;
    private System.Windows.Forms.Button btnCheckExists;
    private System.Windows.Forms.TextBox txtValue;
    private System.Windows.Forms.TextBox txtColumn;
    private System.Windows.Forms.Label lblValue;
    private System.Windows.Forms.Label lblColumn;
    private System.Windows.Forms.TextBox txtDbClassesPath;
    private System.Windows.Forms.Label lblDbClassesPath;
    private System.Windows.Forms.Button btnGetSchema1;
    private System.Windows.Forms.TabControl tabTopControls;
    private System.Windows.Forms.TabPage tabPageDbGen;
    private System.Windows.Forms.TabPage tabPageSchema;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageMain;
    private System.Windows.Forms.TabPage tabPageSecondary;
    private System.Windows.Forms.TextBox txtOut2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TabPage tabPageCompare;
    private System.Windows.Forms.Button btnGetSchema2;
    private System.Windows.Forms.CheckBox ckSqlAuth2;
    private System.Windows.Forms.CheckBox ckSqlAuth1;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblUserID;
    private System.Windows.Forms.Label lblSqlAuth;
    private System.Windows.Forms.Label lblDatabase;
    private System.Windows.Forms.Label lblInstance;
    private System.Windows.Forms.TextBox txtPassword2;
    private System.Windows.Forms.TextBox txtUserID2;
    private System.Windows.Forms.TextBox txtInstance2;
    private System.Windows.Forms.TextBox txtDb2;
    private System.Windows.Forms.TextBox txtPassword1;
    private System.Windows.Forms.TextBox txtUserID1;
    private System.Windows.Forms.TextBox txtDb1;
    private System.Windows.Forms.TextBox txtInstance1;
    private System.Windows.Forms.Button btnCompareSchemas;
    private System.Windows.Forms.WebBrowser browserCompare;
  }
}

