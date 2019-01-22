namespace DictionaryManager
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.txtSpellCheckLimit = new System.Windows.Forms.TextBox();
      this.txtSourceLetterB = new System.Windows.Forms.TextBox();
      this.txtSourceLetterA = new System.Windows.Forms.TextBox();
      this.ckLimitSpellCheck = new System.Windows.Forms.CheckBox();
      this.ckFilterSpacesAndBadSpelling = new System.Windows.Forms.CheckBox();
      this.ckInDiagnosticsMode = new System.Windows.Forms.CheckBox();
      this.ckSimpleList = new System.Windows.Forms.CheckBox();
      this.lblFindResult = new System.Windows.Forms.Label();
      this.txtSearchText = new System.Windows.Forms.TextBox();
      this.lblDctB = new System.Windows.Forms.Label();
      this.lblDctA = new System.Windows.Forms.Label();
      this.cboDctB = new System.Windows.Forms.ComboBox();
      this.cboDctA = new System.Windows.Forms.ComboBox();
      this.btnCompareBA = new System.Windows.Forms.Button();
      this.btnFindInB = new System.Windows.Forms.Button();
      this.btnFindInA = new System.Windows.Forms.Button();
      this.btnCombine = new System.Windows.Forms.Button();
      this.btnCompareAB = new System.Windows.Forms.Button();
      this.btnValidateEntries = new System.Windows.Forms.Button();
      this.btnLoadDctB = new System.Windows.Forms.Button();
      this.btnRandomizeFromFile = new System.Windows.Forms.Button();
      this.btnRandomize = new System.Windows.Forms.Button();
      this.btnRunFunctional = new System.Windows.Forms.Button();
      this.btnClearReports = new System.Windows.Forms.Button();
      this.btnSpellCheck = new System.Windows.Forms.Button();
      this.btnGetVariants = new System.Windows.Forms.Button();
      this.btnGetAutoCorrectEntries = new System.Windows.Forms.Button();
      this.btnLoadToTriNode = new System.Windows.Forms.Button();
      this.btnLoadDctA = new System.Windows.Forms.Button();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsReloadCbo = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageReports = new System.Windows.Forms.TabPage();
      this.txtReports = new System.Windows.Forms.TextBox();
      this.ctxMenuAddToDictionary = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabPageDctA = new System.Windows.Forms.TabPage();
      this.txtDctA = new System.Windows.Forms.TextBox();
      this.tabPageDctB = new System.Windows.Forms.TabPage();
      this.txtDctB = new System.Windows.Forms.TextBox();
      this.pnlTop.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageReports.SuspendLayout();
      this.ctxMenuAddToDictionary.SuspendLayout();
      this.tabPageDctA.SuspendLayout();
      this.tabPageDctB.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.txtSpellCheckLimit);
      this.pnlTop.Controls.Add(this.txtSourceLetterB);
      this.pnlTop.Controls.Add(this.txtSourceLetterA);
      this.pnlTop.Controls.Add(this.ckLimitSpellCheck);
      this.pnlTop.Controls.Add(this.ckFilterSpacesAndBadSpelling);
      this.pnlTop.Controls.Add(this.ckInDiagnosticsMode);
      this.pnlTop.Controls.Add(this.ckSimpleList);
      this.pnlTop.Controls.Add(this.lblFindResult);
      this.pnlTop.Controls.Add(this.txtSearchText);
      this.pnlTop.Controls.Add(this.lblDctB);
      this.pnlTop.Controls.Add(this.lblDctA);
      this.pnlTop.Controls.Add(this.cboDctB);
      this.pnlTop.Controls.Add(this.cboDctA);
      this.pnlTop.Controls.Add(this.btnCompareBA);
      this.pnlTop.Controls.Add(this.btnFindInB);
      this.pnlTop.Controls.Add(this.btnFindInA);
      this.pnlTop.Controls.Add(this.btnCombine);
      this.pnlTop.Controls.Add(this.btnCompareAB);
      this.pnlTop.Controls.Add(this.btnValidateEntries);
      this.pnlTop.Controls.Add(this.btnLoadDctB);
      this.pnlTop.Controls.Add(this.btnRandomizeFromFile);
      this.pnlTop.Controls.Add(this.btnRandomize);
      this.pnlTop.Controls.Add(this.btnRunFunctional);
      this.pnlTop.Controls.Add(this.btnClearReports);
      this.pnlTop.Controls.Add(this.btnSpellCheck);
      this.pnlTop.Controls.Add(this.btnGetVariants);
      this.pnlTop.Controls.Add(this.btnGetAutoCorrectEntries);
      this.pnlTop.Controls.Add(this.btnLoadToTriNode);
      this.pnlTop.Controls.Add(this.btnLoadDctA);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1195, 120);
      this.pnlTop.TabIndex = 0;
      //
      // txtSpellCheckLimit
      //
      this.txtSpellCheckLimit.Location = new System.Drawing.Point(1073, 37);
      this.txtSpellCheckLimit.Name = "txtSpellCheckLimit";
      this.txtSpellCheckLimit.Size = new System.Drawing.Size(110, 20);
      this.txtSpellCheckLimit.TabIndex = 6;
      this.txtSpellCheckLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // txtSourceLetterB
      //
      this.txtSourceLetterB.Location = new System.Drawing.Point(951, 36);
      this.txtSourceLetterB.Name = "txtSourceLetterB";
      this.txtSourceLetterB.Size = new System.Drawing.Size(25, 20);
      this.txtSourceLetterB.TabIndex = 6;
      //
      // txtSourceLetterA
      //
      this.txtSourceLetterA.Location = new System.Drawing.Point(951, 9);
      this.txtSourceLetterA.Name = "txtSourceLetterA";
      this.txtSourceLetterA.Size = new System.Drawing.Size(25, 20);
      this.txtSourceLetterA.TabIndex = 6;
      //
      // ckLimitSpellCheck
      //
      this.ckLimitSpellCheck.AutoSize = true;
      this.ckLimitSpellCheck.Checked = true;
      this.ckLimitSpellCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckLimitSpellCheck.Location = new System.Drawing.Point(1080, 63);
      this.ckLimitSpellCheck.Name = "ckLimitSpellCheck";
      this.ckLimitSpellCheck.Size = new System.Drawing.Size(107, 17);
      this.ckLimitSpellCheck.TabIndex = 5;
      this.ckLimitSpellCheck.Text = "Limit Spell Check";
      this.ckLimitSpellCheck.UseVisualStyleBackColor = true;
      //
      // ckFilterSpacesAndBadSpelling
      //
      this.ckFilterSpacesAndBadSpelling.AutoSize = true;
      this.ckFilterSpacesAndBadSpelling.Location = new System.Drawing.Point(565, 65);
      this.ckFilterSpacesAndBadSpelling.Name = "ckFilterSpacesAndBadSpelling";
      this.ckFilterSpacesAndBadSpelling.Size = new System.Drawing.Size(229, 17);
      this.ckFilterSpacesAndBadSpelling.TabIndex = 5;
      this.ckFilterSpacesAndBadSpelling.Text = "Filter Embedded Spaces and Bad Spellings";
      this.ckFilterSpacesAndBadSpelling.UseVisualStyleBackColor = true;
      //
      // ckInDiagnosticsMode
      //
      this.ckInDiagnosticsMode.AutoSize = true;
      this.ckInDiagnosticsMode.Location = new System.Drawing.Point(835, 65);
      this.ckInDiagnosticsMode.Name = "ckInDiagnosticsMode";
      this.ckInDiagnosticsMode.Size = new System.Drawing.Size(108, 17);
      this.ckInDiagnosticsMode.TabIndex = 5;
      this.ckInDiagnosticsMode.Text = "DiagnosticsMode";
      this.ckInDiagnosticsMode.UseVisualStyleBackColor = true;
      //
      // ckSimpleList
      //
      this.ckSimpleList.AutoSize = true;
      this.ckSimpleList.Location = new System.Drawing.Point(473, 65);
      this.ckSimpleList.Name = "ckSimpleList";
      this.ckSimpleList.Size = new System.Drawing.Size(76, 17);
      this.ckSimpleList.TabIndex = 5;
      this.ckSimpleList.Text = "Simple List";
      this.ckSimpleList.UseVisualStyleBackColor = true;
      //
      // lblFindResult
      //
      this.lblFindResult.Location = new System.Drawing.Point(638, 35);
      this.lblFindResult.Name = "lblFindResult";
      this.lblFindResult.Size = new System.Drawing.Size(179, 21);
      this.lblFindResult.TabIndex = 4;
      this.lblFindResult.Text = "Find result";
      this.lblFindResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtSearchText
      //
      this.txtSearchText.Location = new System.Drawing.Point(638, 8);
      this.txtSearchText.Name = "txtSearchText";
      this.txtSearchText.Size = new System.Drawing.Size(179, 20);
      this.txtSearchText.TabIndex = 3;
      //
      // lblDctB
      //
      this.lblDctB.AutoSize = true;
      this.lblDctB.Location = new System.Drawing.Point(12, 39);
      this.lblDctB.Name = "lblDctB";
      this.lblDctB.Size = new System.Drawing.Size(64, 13);
      this.lblDctB.TabIndex = 2;
      this.lblDctB.Text = "Dictionary B";
      //
      // lblDctA
      //
      this.lblDctA.AutoSize = true;
      this.lblDctA.Location = new System.Drawing.Point(12, 12);
      this.lblDctA.Name = "lblDctA";
      this.lblDctA.Size = new System.Drawing.Size(64, 13);
      this.lblDctA.TabIndex = 2;
      this.lblDctA.Text = "Dictionary A";
      //
      // cboDctB
      //
      this.cboDctB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDctB.FormattingEnabled = true;
      this.cboDctB.Location = new System.Drawing.Point(84, 36);
      this.cboDctB.Name = "cboDctB";
      this.cboDctB.Size = new System.Drawing.Size(172, 21);
      this.cboDctB.TabIndex = 1;
      //
      // cboDctA
      //
      this.cboDctA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDctA.FormattingEnabled = true;
      this.cboDctA.Location = new System.Drawing.Point(84, 8);
      this.cboDctA.Name = "cboDctA";
      this.cboDctA.Size = new System.Drawing.Size(172, 21);
      this.cboDctA.TabIndex = 1;
      //
      // btnCompareBA
      //
      this.btnCompareBA.Location = new System.Drawing.Point(400, 35);
      this.btnCompareBA.Name = "btnCompareBA";
      this.btnCompareBA.Size = new System.Drawing.Size(110, 23);
      this.btnCompareBA.TabIndex = 0;
      this.btnCompareBA.Tag = "COMPARE_BA";
      this.btnCompareBA.Text = "Compare B->A";
      this.btnCompareBA.UseVisualStyleBackColor = true;
      this.btnCompareBA.Click += new System.EventHandler(this.Action);
      //
      // btnFindInB
      //
      this.btnFindInB.Location = new System.Drawing.Point(522, 34);
      this.btnFindInB.Name = "btnFindInB";
      this.btnFindInB.Size = new System.Drawing.Size(110, 23);
      this.btnFindInB.TabIndex = 0;
      this.btnFindInB.Tag = "FIND_IN_B";
      this.btnFindInB.Text = "Find in B";
      this.btnFindInB.UseVisualStyleBackColor = true;
      this.btnFindInB.Click += new System.EventHandler(this.Action);
      //
      // btnFindInA
      //
      this.btnFindInA.Location = new System.Drawing.Point(522, 6);
      this.btnFindInA.Name = "btnFindInA";
      this.btnFindInA.Size = new System.Drawing.Size(110, 23);
      this.btnFindInA.TabIndex = 0;
      this.btnFindInA.Tag = "FIND_IN_A";
      this.btnFindInA.Text = "Find in A";
      this.btnFindInA.UseVisualStyleBackColor = true;
      this.btnFindInA.Click += new System.EventHandler(this.Action);
      //
      // btnCombine
      //
      this.btnCombine.Location = new System.Drawing.Point(835, 7);
      this.btnCombine.Name = "btnCombine";
      this.btnCombine.Size = new System.Drawing.Size(110, 23);
      this.btnCombine.TabIndex = 0;
      this.btnCombine.Tag = "COMBINE_AB";
      this.btnCombine.Text = "Combine";
      this.btnCombine.UseVisualStyleBackColor = true;
      this.btnCombine.Click += new System.EventHandler(this.Action);
      //
      // btnCompareAB
      //
      this.btnCompareAB.Location = new System.Drawing.Point(400, 6);
      this.btnCompareAB.Name = "btnCompareAB";
      this.btnCompareAB.Size = new System.Drawing.Size(110, 23);
      this.btnCompareAB.TabIndex = 0;
      this.btnCompareAB.Tag = "COMPARE_AB";
      this.btnCompareAB.Text = "Compare A->B";
      this.btnCompareAB.UseVisualStyleBackColor = true;
      this.btnCompareAB.Click += new System.EventHandler(this.Action);
      //
      // btnValidateEntries
      //
      this.btnValidateEntries.Location = new System.Drawing.Point(1073, 7);
      this.btnValidateEntries.Name = "btnValidateEntries";
      this.btnValidateEntries.Size = new System.Drawing.Size(110, 23);
      this.btnValidateEntries.TabIndex = 0;
      this.btnValidateEntries.Tag = "VALIDATE_ENTRIES";
      this.btnValidateEntries.Text = "Validate Entries";
      this.btnValidateEntries.UseVisualStyleBackColor = true;
      this.btnValidateEntries.Click += new System.EventHandler(this.Action);
      //
      // btnLoadDctB
      //
      this.btnLoadDctB.Location = new System.Drawing.Point(262, 35);
      this.btnLoadDctB.Name = "btnLoadDctB";
      this.btnLoadDctB.Size = new System.Drawing.Size(121, 23);
      this.btnLoadDctB.TabIndex = 0;
      this.btnLoadDctB.Tag = "LOAD_DCT_B";
      this.btnLoadDctB.Text = "Load Dictionary";
      this.btnLoadDctB.UseVisualStyleBackColor = true;
      this.btnLoadDctB.Click += new System.EventHandler(this.Action);
      //
      // btnRandomizeFromFile
      //
      this.btnRandomizeFromFile.Location = new System.Drawing.Point(262, 63);
      this.btnRandomizeFromFile.Name = "btnRandomizeFromFile";
      this.btnRandomizeFromFile.Size = new System.Drawing.Size(121, 23);
      this.btnRandomizeFromFile.TabIndex = 0;
      this.btnRandomizeFromFile.Tag = "RANDOMIZE_FROM_FILE";
      this.btnRandomizeFromFile.Text = "Randomize from File";
      this.btnRandomizeFromFile.UseVisualStyleBackColor = true;
      this.btnRandomizeFromFile.Click += new System.EventHandler(this.Action);
      //
      // btnRandomize
      //
      this.btnRandomize.Location = new System.Drawing.Point(186, 63);
      this.btnRandomize.Name = "btnRandomize";
      this.btnRandomize.Size = new System.Drawing.Size(70, 23);
      this.btnRandomize.TabIndex = 0;
      this.btnRandomize.Tag = "RANDOMIZE";
      this.btnRandomize.Text = "Randomize";
      this.btnRandomize.UseVisualStyleBackColor = true;
      this.btnRandomize.Click += new System.EventHandler(this.Action);
      //
      // btnRunFunctional
      //
      this.btnRunFunctional.Location = new System.Drawing.Point(835, 92);
      this.btnRunFunctional.Name = "btnRunFunctional";
      this.btnRunFunctional.Size = new System.Drawing.Size(132, 23);
      this.btnRunFunctional.TabIndex = 0;
      this.btnRunFunctional.Tag = "RUN_FUNCTIONAL";
      this.btnRunFunctional.Text = "Run Functional";
      this.btnRunFunctional.UseVisualStyleBackColor = true;
      this.btnRunFunctional.Click += new System.EventHandler(this.Action);
      //
      // btnClearReports
      //
      this.btnClearReports.Location = new System.Drawing.Point(1073, 92);
      this.btnClearReports.Name = "btnClearReports";
      this.btnClearReports.Size = new System.Drawing.Size(110, 23);
      this.btnClearReports.TabIndex = 0;
      this.btnClearReports.Tag = "CLEAR_REPORTS";
      this.btnClearReports.Text = "Clear Reports";
      this.btnClearReports.UseVisualStyleBackColor = true;
      this.btnClearReports.Click += new System.EventHandler(this.Action);
      //
      // btnSpellCheck
      //
      this.btnSpellCheck.Location = new System.Drawing.Point(400, 92);
      this.btnSpellCheck.Name = "btnSpellCheck";
      this.btnSpellCheck.Size = new System.Drawing.Size(110, 23);
      this.btnSpellCheck.TabIndex = 0;
      this.btnSpellCheck.Tag = "SPELL_CHECK";
      this.btnSpellCheck.Text = "Spell Check";
      this.btnSpellCheck.UseVisualStyleBackColor = true;
      this.btnSpellCheck.Click += new System.EventHandler(this.Action);
      //
      // btnGetVariants
      //
      this.btnGetVariants.Location = new System.Drawing.Point(638, 92);
      this.btnGetVariants.Name = "btnGetVariants";
      this.btnGetVariants.Size = new System.Drawing.Size(132, 23);
      this.btnGetVariants.TabIndex = 0;
      this.btnGetVariants.Tag = "GET_VARIANTS";
      this.btnGetVariants.Text = "Get Variants";
      this.btnGetVariants.UseVisualStyleBackColor = true;
      this.btnGetVariants.Click += new System.EventHandler(this.Action);
      //
      // btnGetAutoCorrectEntries
      //
      this.btnGetAutoCorrectEntries.Location = new System.Drawing.Point(4, 92);
      this.btnGetAutoCorrectEntries.Name = "btnGetAutoCorrectEntries";
      this.btnGetAutoCorrectEntries.Size = new System.Drawing.Size(132, 23);
      this.btnGetAutoCorrectEntries.TabIndex = 0;
      this.btnGetAutoCorrectEntries.Tag = "GET_AUTO_CORRECT_ENTRIES";
      this.btnGetAutoCorrectEntries.Text = "Get AutoCorrect Entries";
      this.btnGetAutoCorrectEntries.UseVisualStyleBackColor = true;
      this.btnGetAutoCorrectEntries.Click += new System.EventHandler(this.Action);
      //
      // btnLoadToTriNode
      //
      this.btnLoadToTriNode.Location = new System.Drawing.Point(84, 63);
      this.btnLoadToTriNode.Name = "btnLoadToTriNode";
      this.btnLoadToTriNode.Size = new System.Drawing.Size(101, 23);
      this.btnLoadToTriNode.TabIndex = 0;
      this.btnLoadToTriNode.Tag = "LOAD_TO_TRINODE";
      this.btnLoadToTriNode.Text = "Load to TriNode";
      this.btnLoadToTriNode.UseVisualStyleBackColor = true;
      this.btnLoadToTriNode.Click += new System.EventHandler(this.Action);
      //
      // btnLoadDctA
      //
      this.btnLoadDctA.Location = new System.Drawing.Point(262, 7);
      this.btnLoadDctA.Name = "btnLoadDctA";
      this.btnLoadDctA.Size = new System.Drawing.Size(121, 23);
      this.btnLoadDctA.TabIndex = 0;
      this.btnLoadDctA.Tag = "LOAD_DCT_A";
      this.btnLoadDctA.Text = "Load Dictionary";
      this.btnLoadDctA.UseVisualStyleBackColor = true;
      this.btnLoadDctA.Click += new System.EventHandler(this.Action);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuOptions
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1195, 24);
      this.mnuMain.TabIndex = 1;
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
      // mnuOptions
      //
      this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuOptionsReloadCbo
      });
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(61, 20);
      this.mnuOptions.Text = "Options";
      //
      // mnuOptionsReloadCbo
      //
      this.mnuOptionsReloadCbo.Name = "mnuOptionsReloadCbo";
      this.mnuOptionsReloadCbo.Size = new System.Drawing.Size(201, 22);
      this.mnuOptionsReloadCbo.Tag = "RELOAD_CBO";
      this.mnuOptionsReloadCbo.Text = "Reload Combo Box Lists";
      this.mnuOptionsReloadCbo.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.Location = new System.Drawing.Point(0, 640);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1195, 19);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 144);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1195, 496);
      this.pnlMain.TabIndex = 3;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageReports);
      this.tabMain.Controls.Add(this.tabPageDctA);
      this.tabMain.Controls.Add(this.tabPageDctB);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(110, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1195, 496);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 0;
      //
      // tabPageReports
      //
      this.tabPageReports.Controls.Add(this.txtReports);
      this.tabPageReports.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.tabPageReports.Location = new System.Drawing.Point(4, 22);
      this.tabPageReports.Name = "tabPageReports";
      this.tabPageReports.Size = new System.Drawing.Size(1187, 470);
      this.tabPageReports.TabIndex = 2;
      this.tabPageReports.Text = "Reports";
      this.tabPageReports.UseVisualStyleBackColor = true;
      //
      // txtReports
      //
      this.txtReports.ContextMenuStrip = this.ctxMenuAddToDictionary;
      this.txtReports.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtReports.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtReports.Location = new System.Drawing.Point(0, 0);
      this.txtReports.Multiline = true;
      this.txtReports.Name = "txtReports";
      this.txtReports.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtReports.Size = new System.Drawing.Size(1187, 470);
      this.txtReports.TabIndex = 1;
      this.txtReports.WordWrap = false;
      //
      // ctxMenuAddToDictionary
      //
      this.ctxMenuAddToDictionary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.addToDictionaryToolStripMenuItem
      });
      this.ctxMenuAddToDictionary.Name = "ctxMenuAddToDictionary";
      this.ctxMenuAddToDictionary.Size = new System.Drawing.Size(167, 26);
      this.ctxMenuAddToDictionary.Tag = "";
      this.ctxMenuAddToDictionary.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuAddToDictionary_Opening);
      //
      // addToDictionaryToolStripMenuItem
      //
      this.addToDictionaryToolStripMenuItem.Name = "addToDictionaryToolStripMenuItem";
      this.addToDictionaryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.addToDictionaryToolStripMenuItem.Tag = "ADD_TO_DICTIONARY";
      this.addToDictionaryToolStripMenuItem.Text = "&Add to dictionary";
      this.addToDictionaryToolStripMenuItem.Click += new System.EventHandler(this.Action);
      //
      // tabPageDctA
      //
      this.tabPageDctA.Controls.Add(this.txtDctA);
      this.tabPageDctA.Location = new System.Drawing.Point(4, 22);
      this.tabPageDctA.Name = "tabPageDctA";
      this.tabPageDctA.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDctA.Size = new System.Drawing.Size(1187, 549);
      this.tabPageDctA.TabIndex = 0;
      this.tabPageDctA.Text = "Dictionary A";
      this.tabPageDctA.UseVisualStyleBackColor = true;
      //
      // txtDctA
      //
      this.txtDctA.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDctA.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDctA.Location = new System.Drawing.Point(3, 3);
      this.txtDctA.Multiline = true;
      this.txtDctA.Name = "txtDctA";
      this.txtDctA.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDctA.Size = new System.Drawing.Size(1181, 543);
      this.txtDctA.TabIndex = 0;
      this.txtDctA.WordWrap = false;
      //
      // tabPageDctB
      //
      this.tabPageDctB.Controls.Add(this.txtDctB);
      this.tabPageDctB.Location = new System.Drawing.Point(4, 22);
      this.tabPageDctB.Name = "tabPageDctB";
      this.tabPageDctB.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDctB.Size = new System.Drawing.Size(1187, 549);
      this.tabPageDctB.TabIndex = 1;
      this.tabPageDctB.Text = "Dictionary B";
      this.tabPageDctB.UseVisualStyleBackColor = true;
      //
      // txtDctB
      //
      this.txtDctB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDctB.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDctB.Location = new System.Drawing.Point(3, 3);
      this.txtDctB.Multiline = true;
      this.txtDctB.Name = "txtDctB";
      this.txtDctB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDctB.Size = new System.Drawing.Size(1181, 543);
      this.txtDctB.TabIndex = 1;
      this.txtDctB.WordWrap = false;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1195, 659);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Dictionary Manager";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageReports.ResumeLayout(false);
      this.tabPageReports.PerformLayout();
      this.ctxMenuAddToDictionary.ResumeLayout(false);
      this.tabPageDctA.ResumeLayout(false);
      this.tabPageDctA.PerformLayout();
      this.tabPageDctB.ResumeLayout(false);
      this.tabPageDctB.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Button btnValidateEntries;
    private System.Windows.Forms.Button btnLoadDctA;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageDctA;
    private System.Windows.Forms.TextBox txtDctA;
    private System.Windows.Forms.TabPage tabPageDctB;
    private System.Windows.Forms.ComboBox cboDctA;
    private System.Windows.Forms.Label lblDctB;
    private System.Windows.Forms.Label lblDctA;
    private System.Windows.Forms.ComboBox cboDctB;
    private System.Windows.Forms.Button btnLoadDctB;
    private System.Windows.Forms.Button btnCompareBA;
    private System.Windows.Forms.Button btnCompareAB;
    private System.Windows.Forms.Label lblFindResult;
    private System.Windows.Forms.TextBox txtSearchText;
    private System.Windows.Forms.Button btnFindInB;
    private System.Windows.Forms.Button btnFindInA;
    private System.Windows.Forms.TabPage tabPageReports;
    private System.Windows.Forms.TextBox txtReports;
    private System.Windows.Forms.TextBox txtDctB;
    private System.Windows.Forms.Button btnCombine;
    private System.Windows.Forms.CheckBox ckSimpleList;
    private System.Windows.Forms.TextBox txtSourceLetterB;
    private System.Windows.Forms.TextBox txtSourceLetterA;
    private System.Windows.Forms.ToolStripMenuItem mnuOptions;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsReloadCbo;
    private System.Windows.Forms.TextBox txtSpellCheckLimit;
    private System.Windows.Forms.CheckBox ckLimitSpellCheck;
    private System.Windows.Forms.CheckBox ckFilterSpacesAndBadSpelling;
    private System.Windows.Forms.Button btnLoadToTriNode;
    private System.Windows.Forms.CheckBox ckInDiagnosticsMode;
    private System.Windows.Forms.Button btnRandomize;
    private System.Windows.Forms.Button btnRandomizeFromFile;
    private System.Windows.Forms.Button btnGetAutoCorrectEntries;
    private System.Windows.Forms.Button btnGetVariants;
    private System.Windows.Forms.Button btnRunFunctional;
    private System.Windows.Forms.Button btnClearReports;
    private System.Windows.Forms.Button btnSpellCheck;
    private System.Windows.Forms.ContextMenuStrip ctxMenuAddToDictionary;
    private System.Windows.Forms.ToolStripMenuItem addToDictionaryToolStripMenuItem;
  }
}

