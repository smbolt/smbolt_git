namespace Org.OpsManager
{
  partial class frmScheduleElement
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScheduleElement));
      this.chkSecond = new System.Windows.Forms.CheckBox();
      this.chkOddDay = new System.Windows.Forms.CheckBox();
      this.chkWorkDay = new System.Windows.Forms.CheckBox();
      this.chkSaturday = new System.Windows.Forms.CheckBox();
      this.chkFriday = new System.Windows.Forms.CheckBox();
      this.chkWednesday = new System.Windows.Forms.CheckBox();
      this.chkMonday = new System.Windows.Forms.CheckBox();
      this.chkLast = new System.Windows.Forms.CheckBox();
      this.chkFifth = new System.Windows.Forms.CheckBox();
      this.chkThird = new System.Windows.Forms.CheckBox();
      this.chkFirst = new System.Windows.Forms.CheckBox();
      this.chkThursday = new System.Windows.Forms.CheckBox();
      this.chkTuesday = new System.Windows.Forms.CheckBox();
      this.chkSunday = new System.Windows.Forms.CheckBox();
      this.chkEvery = new System.Windows.Forms.CheckBox();
      this.chkFourth = new System.Windows.Forms.CheckBox();
      this.chkEvenDay = new System.Windows.Forms.CheckBox();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.chkIsClockAligned = new System.Windows.Forms.CheckBox();
      this.cboExecutionType = new System.Windows.Forms.ComboBox();
      this.lblExecutionType = new System.Windows.Forms.Label();
      this.lblStartDateTime = new System.Windows.Forms.Label();
      this.lblEndDateTime = new System.Windows.Forms.Label();
      this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
      this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
      this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
      this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
      this.lblFrequency = new System.Windows.Forms.Label();
      this.txtFrequency = new System.Windows.Forms.TextBox();
      this.lblScheduleElementPriority = new System.Windows.Forms.Label();
      this.txtScheduleElementPriority = new System.Windows.Forms.TextBox();
      this.lblIntervalType = new System.Windows.Forms.Label();
      this.cboIntervalType = new System.Windows.Forms.ComboBox();
      this.lblSpecificDays = new System.Windows.Forms.Label();
      this.chkExceptSpecificDays = new System.Windows.Forms.CheckBox();
      this.lblHolidayAction = new System.Windows.Forms.Label();
      this.cboHolidayAction = new System.Windows.Forms.ComboBox();
      this.lblPeriod = new System.Windows.Forms.Label();
      this.cboPeriod = new System.Windows.Forms.ComboBox();
      this.lblExecutionLimit = new System.Windows.Forms.Label();
      this.txtExecutionLimit = new System.Windows.Forms.TextBox();
      this.lblMaxRunTime = new System.Windows.Forms.Label();
      this.txtMaxRunTime = new System.Windows.Forms.TextBox();
      this.lblOccurrence = new System.Windows.Forms.Label();
      this.lblDayOfWeek = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.upDownSpecificDay = new System.Windows.Forms.NumericUpDown();
      this.lblSpecificDaysList = new System.Windows.Forms.Label();
      this.btnAddDay = new System.Windows.Forms.Button();
      this.btnRemoveDay = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.lblEndDateCover = new System.Windows.Forms.Label();
      this.lblStartTimeCover = new System.Windows.Forms.Label();
      this.lblEndTimeCover = new System.Windows.Forms.Label();
      this.lblStartDateCover = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.upDownSpecificDay)).BeginInit();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // chkSecond
      //
      this.chkSecond.AutoSize = true;
      this.chkSecond.Location = new System.Drawing.Point(276, 129);
      this.chkSecond.Name = "chkSecond";
      this.chkSecond.Size = new System.Drawing.Size(63, 17);
      this.chkSecond.TabIndex = 14;
      this.chkSecond.Tag = "OccurrenceChange";
      this.chkSecond.Text = "Second";
      this.chkSecond.UseVisualStyleBackColor = true;
      this.chkSecond.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkOddDay
      //
      this.chkOddDay.AutoSize = true;
      this.chkOddDay.Location = new System.Drawing.Point(385, 244);
      this.chkOddDay.Name = "chkOddDay";
      this.chkOddDay.Size = new System.Drawing.Size(68, 17);
      this.chkOddDay.TabIndex = 29;
      this.chkOddDay.Tag = "BasicPropertyChange";
      this.chkOddDay.Text = "Odd Day";
      this.chkOddDay.UseVisualStyleBackColor = true;
      this.chkOddDay.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkWorkDay
      //
      this.chkWorkDay.AutoSize = true;
      this.chkWorkDay.Location = new System.Drawing.Point(385, 198);
      this.chkWorkDay.Name = "chkWorkDay";
      this.chkWorkDay.Size = new System.Drawing.Size(74, 17);
      this.chkWorkDay.TabIndex = 27;
      this.chkWorkDay.Tag = "BasicPropertyChange";
      this.chkWorkDay.Text = "Work Day";
      this.chkWorkDay.UseVisualStyleBackColor = true;
      this.chkWorkDay.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkSaturday
      //
      this.chkSaturday.AutoSize = true;
      this.chkSaturday.Location = new System.Drawing.Point(385, 175);
      this.chkSaturday.Name = "chkSaturday";
      this.chkSaturday.Size = new System.Drawing.Size(68, 17);
      this.chkSaturday.TabIndex = 26;
      this.chkSaturday.Tag = "BasicPropertyChange";
      this.chkSaturday.Text = "Saturday";
      this.chkSaturday.UseVisualStyleBackColor = true;
      this.chkSaturday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkFriday
      //
      this.chkFriday.AutoSize = true;
      this.chkFriday.Location = new System.Drawing.Point(385, 152);
      this.chkFriday.Name = "chkFriday";
      this.chkFriday.Size = new System.Drawing.Size(54, 17);
      this.chkFriday.TabIndex = 25;
      this.chkFriday.Tag = "BasicPropertyChange";
      this.chkFriday.Text = "Friday";
      this.chkFriday.UseVisualStyleBackColor = true;
      this.chkFriday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkWednesday
      //
      this.chkWednesday.AutoSize = true;
      this.chkWednesday.Location = new System.Drawing.Point(385, 106);
      this.chkWednesday.Name = "chkWednesday";
      this.chkWednesday.Size = new System.Drawing.Size(83, 17);
      this.chkWednesday.TabIndex = 23;
      this.chkWednesday.Tag = "BasicPropertyChange";
      this.chkWednesday.Text = "Wednesday";
      this.chkWednesday.UseVisualStyleBackColor = true;
      this.chkWednesday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkMonday
      //
      this.chkMonday.AutoSize = true;
      this.chkMonday.Location = new System.Drawing.Point(385, 60);
      this.chkMonday.Name = "chkMonday";
      this.chkMonday.Size = new System.Drawing.Size(64, 17);
      this.chkMonday.TabIndex = 21;
      this.chkMonday.Tag = "BasicPropertyChange";
      this.chkMonday.Text = "Monday";
      this.chkMonday.UseVisualStyleBackColor = true;
      this.chkMonday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkLast
      //
      this.chkLast.AutoSize = true;
      this.chkLast.Location = new System.Drawing.Point(276, 221);
      this.chkLast.Name = "chkLast";
      this.chkLast.Size = new System.Drawing.Size(46, 17);
      this.chkLast.TabIndex = 18;
      this.chkLast.Tag = "OccurrenceChange";
      this.chkLast.Text = "Last";
      this.chkLast.UseVisualStyleBackColor = true;
      this.chkLast.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkFifth
      //
      this.chkFifth.AutoSize = true;
      this.chkFifth.Location = new System.Drawing.Point(276, 198);
      this.chkFifth.Name = "chkFifth";
      this.chkFifth.Size = new System.Drawing.Size(46, 17);
      this.chkFifth.TabIndex = 17;
      this.chkFifth.Tag = "OccurrenceChange";
      this.chkFifth.Text = "Fifth";
      this.chkFifth.UseVisualStyleBackColor = true;
      this.chkFifth.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkThird
      //
      this.chkThird.AutoSize = true;
      this.chkThird.Location = new System.Drawing.Point(276, 152);
      this.chkThird.Name = "chkThird";
      this.chkThird.Size = new System.Drawing.Size(50, 17);
      this.chkThird.TabIndex = 15;
      this.chkThird.Tag = "OccurrenceChange";
      this.chkThird.Text = "Third";
      this.chkThird.UseVisualStyleBackColor = true;
      this.chkThird.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkFirst
      //
      this.chkFirst.AutoSize = true;
      this.chkFirst.Location = new System.Drawing.Point(276, 106);
      this.chkFirst.Name = "chkFirst";
      this.chkFirst.Size = new System.Drawing.Size(45, 17);
      this.chkFirst.TabIndex = 13;
      this.chkFirst.Tag = "OccurrenceChange";
      this.chkFirst.Text = "First";
      this.chkFirst.UseVisualStyleBackColor = true;
      this.chkFirst.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkThursday
      //
      this.chkThursday.AutoSize = true;
      this.chkThursday.Location = new System.Drawing.Point(385, 129);
      this.chkThursday.Name = "chkThursday";
      this.chkThursday.Size = new System.Drawing.Size(70, 17);
      this.chkThursday.TabIndex = 24;
      this.chkThursday.Tag = "BasicPropertyChange";
      this.chkThursday.Text = "Thursday";
      this.chkThursday.UseVisualStyleBackColor = true;
      this.chkThursday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkTuesday
      //
      this.chkTuesday.AutoSize = true;
      this.chkTuesday.Location = new System.Drawing.Point(385, 83);
      this.chkTuesday.Name = "chkTuesday";
      this.chkTuesday.Size = new System.Drawing.Size(67, 17);
      this.chkTuesday.TabIndex = 22;
      this.chkTuesday.Tag = "BasicPropertyChange";
      this.chkTuesday.Text = "Tuesday";
      this.chkTuesday.UseVisualStyleBackColor = true;
      this.chkTuesday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkSunday
      //
      this.chkSunday.AutoSize = true;
      this.chkSunday.Location = new System.Drawing.Point(385, 37);
      this.chkSunday.Name = "chkSunday";
      this.chkSunday.Size = new System.Drawing.Size(62, 17);
      this.chkSunday.TabIndex = 20;
      this.chkSunday.Tag = "BasicPropertyChange";
      this.chkSunday.Text = "Sunday";
      this.chkSunday.UseVisualStyleBackColor = true;
      this.chkSunday.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkEvery
      //
      this.chkEvery.AutoSize = true;
      this.chkEvery.Location = new System.Drawing.Point(276, 244);
      this.chkEvery.Name = "chkEvery";
      this.chkEvery.Size = new System.Drawing.Size(53, 17);
      this.chkEvery.TabIndex = 19;
      this.chkEvery.Tag = "OccurrenceChange";
      this.chkEvery.Text = "Every";
      this.chkEvery.UseVisualStyleBackColor = true;
      this.chkEvery.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkFourth
      //
      this.chkFourth.AutoSize = true;
      this.chkFourth.Location = new System.Drawing.Point(276, 175);
      this.chkFourth.Name = "chkFourth";
      this.chkFourth.Size = new System.Drawing.Size(56, 17);
      this.chkFourth.TabIndex = 16;
      this.chkFourth.Tag = "OccurrenceChange";
      this.chkFourth.Text = "Fourth";
      this.chkFourth.UseVisualStyleBackColor = true;
      this.chkFourth.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkEvenDay
      //
      this.chkEvenDay.AutoSize = true;
      this.chkEvenDay.Location = new System.Drawing.Point(385, 221);
      this.chkEvenDay.Name = "chkEvenDay";
      this.chkEvenDay.Size = new System.Drawing.Size(73, 17);
      this.chkEvenDay.TabIndex = 28;
      this.chkEvenDay.Tag = "BasicPropertyChange";
      this.chkEvenDay.Text = "Even Day";
      this.chkEvenDay.UseVisualStyleBackColor = true;
      this.chkEvenDay.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkIsActive
      //
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(276, 37);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.Size = new System.Drawing.Size(67, 17);
      this.chkIsActive.TabIndex = 11;
      this.chkIsActive.Tag = "BasicPropertyChange";
      this.chkIsActive.Text = "Is Active";
      this.chkIsActive.UseVisualStyleBackColor = true;
      this.chkIsActive.CheckedChanged += new System.EventHandler(this.Action);
      //
      // chkIsClockAligned
      //
      this.chkIsClockAligned.AutoSize = true;
      this.chkIsClockAligned.Location = new System.Drawing.Point(276, 60);
      this.chkIsClockAligned.Name = "chkIsClockAligned";
      this.chkIsClockAligned.Size = new System.Drawing.Size(102, 17);
      this.chkIsClockAligned.TabIndex = 12;
      this.chkIsClockAligned.Tag = "BasicPropertyChange";
      this.chkIsClockAligned.Text = "Is Clock Aligned";
      this.chkIsClockAligned.UseVisualStyleBackColor = true;
      this.chkIsClockAligned.CheckedChanged += new System.EventHandler(this.Action);
      //
      // cboExecutionType
      //
      this.cboExecutionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboExecutionType.FormattingEnabled = true;
      this.cboExecutionType.Location = new System.Drawing.Point(18, 34);
      this.cboExecutionType.Name = "cboExecutionType";
      this.cboExecutionType.Size = new System.Drawing.Size(231, 21);
      this.cboExecutionType.TabIndex = 1;
      this.cboExecutionType.Tag = "ExecutionTypeChange";
      this.cboExecutionType.TextChanged += new System.EventHandler(this.Action);
      //
      // lblExecutionType
      //
      this.lblExecutionType.AutoSize = true;
      this.lblExecutionType.Location = new System.Drawing.Point(18, 18);
      this.lblExecutionType.Name = "lblExecutionType";
      this.lblExecutionType.Size = new System.Drawing.Size(81, 13);
      this.lblExecutionType.TabIndex = 20;
      this.lblExecutionType.Text = "Execution Type";
      //
      // lblStartDateTime
      //
      this.lblStartDateTime.AutoSize = true;
      this.lblStartDateTime.Location = new System.Drawing.Point(18, 74);
      this.lblStartDateTime.Name = "lblStartDateTime";
      this.lblStartDateTime.Size = new System.Drawing.Size(83, 13);
      this.lblStartDateTime.TabIndex = 21;
      this.lblStartDateTime.Text = "Start Date/Time";
      //
      // lblEndDateTime
      //
      this.lblEndDateTime.AutoSize = true;
      this.lblEndDateTime.Location = new System.Drawing.Point(139, 74);
      this.lblEndDateTime.Name = "lblEndDateTime";
      this.lblEndDateTime.Size = new System.Drawing.Size(80, 13);
      this.lblEndDateTime.TabIndex = 22;
      this.lblEndDateTime.Text = "End Date/Time";
      //
      // dtpStartTime
      //
      this.dtpStartTime.CustomFormat = "hh:mm tt";
      this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpStartTime.Location = new System.Drawing.Point(18, 116);
      this.dtpStartTime.Name = "dtpStartTime";
      this.dtpStartTime.ShowCheckBox = true;
      this.dtpStartTime.ShowUpDown = true;
      this.dtpStartTime.Size = new System.Drawing.Size(113, 20);
      this.dtpStartTime.TabIndex = 4;
      this.dtpStartTime.Tag = "DateTimePropertyChange";
      this.dtpStartTime.ValueChanged += new System.EventHandler(this.Action);
      //
      // dtpEndTime
      //
      this.dtpEndTime.CustomFormat = "hh:mm tt";
      this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpEndTime.Location = new System.Drawing.Point(137, 116);
      this.dtpEndTime.Name = "dtpEndTime";
      this.dtpEndTime.ShowCheckBox = true;
      this.dtpEndTime.ShowUpDown = true;
      this.dtpEndTime.Size = new System.Drawing.Size(113, 20);
      this.dtpEndTime.TabIndex = 5;
      this.dtpEndTime.Tag = "DateTimePropertyChange";
      this.dtpEndTime.ValueChanged += new System.EventHandler(this.Action);
      //
      // dtpStartDate
      //
      this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpStartDate.Location = new System.Drawing.Point(18, 90);
      this.dtpStartDate.Name = "dtpStartDate";
      this.dtpStartDate.ShowCheckBox = true;
      this.dtpStartDate.Size = new System.Drawing.Size(113, 20);
      this.dtpStartDate.TabIndex = 2;
      this.dtpStartDate.Tag = "DateTimePropertyChange";
      this.dtpStartDate.ValueChanged += new System.EventHandler(this.Action);
      //
      // dtpEndDate
      //
      this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpEndDate.Location = new System.Drawing.Point(137, 90);
      this.dtpEndDate.Name = "dtpEndDate";
      this.dtpEndDate.ShowCheckBox = true;
      this.dtpEndDate.Size = new System.Drawing.Size(113, 20);
      this.dtpEndDate.TabIndex = 3;
      this.dtpEndDate.Tag = "DateTimePropertyChange";
      this.dtpEndDate.ValueChanged += new System.EventHandler(this.Action);
      //
      // lblFrequency
      //
      this.lblFrequency.AutoSize = true;
      this.lblFrequency.Location = new System.Drawing.Point(502, 18);
      this.lblFrequency.Name = "lblFrequency";
      this.lblFrequency.Size = new System.Drawing.Size(86, 13);
      this.lblFrequency.TabIndex = 27;
      this.lblFrequency.Text = "Frequency (sec.)";
      //
      // txtFrequency
      //
      this.txtFrequency.Location = new System.Drawing.Point(502, 35);
      this.txtFrequency.Name = "txtFrequency";
      this.txtFrequency.Size = new System.Drawing.Size(124, 20);
      this.txtFrequency.TabIndex = 30;
      this.txtFrequency.Tag = "BasicPropertyChange";
      this.txtFrequency.TextChanged += new System.EventHandler(this.Action);
      this.txtFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Frequency_KeyPress);
      //
      // lblScheduleElementPriority
      //
      this.lblScheduleElementPriority.AutoSize = true;
      this.lblScheduleElementPriority.Location = new System.Drawing.Point(502, 64);
      this.lblScheduleElementPriority.Name = "lblScheduleElementPriority";
      this.lblScheduleElementPriority.Size = new System.Drawing.Size(127, 13);
      this.lblScheduleElementPriority.TabIndex = 29;
      this.lblScheduleElementPriority.Text = "Schedule Element Priority";
      //
      // txtScheduleElementPriority
      //
      this.txtScheduleElementPriority.Location = new System.Drawing.Point(502, 80);
      this.txtScheduleElementPriority.Name = "txtScheduleElementPriority";
      this.txtScheduleElementPriority.Size = new System.Drawing.Size(124, 20);
      this.txtScheduleElementPriority.TabIndex = 31;
      this.txtScheduleElementPriority.Tag = "BasicPropertyChange";
      this.txtScheduleElementPriority.TextChanged += new System.EventHandler(this.Action);
      this.txtScheduleElementPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerOnly_KeyPress);
      //
      // lblIntervalType
      //
      this.lblIntervalType.AutoSize = true;
      this.lblIntervalType.Location = new System.Drawing.Point(18, 152);
      this.lblIntervalType.Name = "lblIntervalType";
      this.lblIntervalType.Size = new System.Drawing.Size(69, 13);
      this.lblIntervalType.TabIndex = 31;
      this.lblIntervalType.Text = "Interval Type";
      //
      // cboIntervalType
      //
      this.cboIntervalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboIntervalType.FormattingEnabled = true;
      this.cboIntervalType.Location = new System.Drawing.Point(18, 168);
      this.cboIntervalType.Name = "cboIntervalType";
      this.cboIntervalType.Size = new System.Drawing.Size(231, 21);
      this.cboIntervalType.TabIndex = 6;
      this.cboIntervalType.Tag = "BasicPropertyChange";
      this.cboIntervalType.TextChanged += new System.EventHandler(this.Action);
      //
      // lblSpecificDays
      //
      this.lblSpecificDays.AutoSize = true;
      this.lblSpecificDays.Location = new System.Drawing.Point(18, 204);
      this.lblSpecificDays.Name = "lblSpecificDays";
      this.lblSpecificDays.Size = new System.Drawing.Size(72, 13);
      this.lblSpecificDays.TabIndex = 33;
      this.lblSpecificDays.Text = "Specific Days";
      //
      // chkExceptSpecificDays
      //
      this.chkExceptSpecificDays.AutoSize = true;
      this.chkExceptSpecificDays.Location = new System.Drawing.Point(21, 328);
      this.chkExceptSpecificDays.Name = "chkExceptSpecificDays";
      this.chkExceptSpecificDays.Size = new System.Drawing.Size(126, 17);
      this.chkExceptSpecificDays.TabIndex = 10;
      this.chkExceptSpecificDays.Tag = "BasicPropertyChange";
      this.chkExceptSpecificDays.Text = "All days except these";
      this.chkExceptSpecificDays.UseVisualStyleBackColor = true;
      this.chkExceptSpecificDays.CheckedChanged += new System.EventHandler(this.Action);
      //
      // lblHolidayAction
      //
      this.lblHolidayAction.AutoSize = true;
      this.lblHolidayAction.Location = new System.Drawing.Point(502, 199);
      this.lblHolidayAction.Name = "lblHolidayAction";
      this.lblHolidayAction.Size = new System.Drawing.Size(75, 13);
      this.lblHolidayAction.TabIndex = 35;
      this.lblHolidayAction.Text = "Holiday Action";
      //
      // cboHolidayAction
      //
      this.cboHolidayAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboHolidayAction.FormattingEnabled = true;
      this.cboHolidayAction.Items.AddRange(new object[] {
        "NULL"
      });
      this.cboHolidayAction.Location = new System.Drawing.Point(502, 215);
      this.cboHolidayAction.Name = "cboHolidayAction";
      this.cboHolidayAction.Size = new System.Drawing.Size(124, 21);
      this.cboHolidayAction.TabIndex = 34;
      this.cboHolidayAction.Tag = "BasicPropertyChange";
      this.cboHolidayAction.TextChanged += new System.EventHandler(this.Action);
      //
      // lblPeriod
      //
      this.lblPeriod.AutoSize = true;
      this.lblPeriod.Location = new System.Drawing.Point(502, 245);
      this.lblPeriod.Name = "lblPeriod";
      this.lblPeriod.Size = new System.Drawing.Size(37, 13);
      this.lblPeriod.TabIndex = 37;
      this.lblPeriod.Text = "Period";
      //
      // cboPeriod
      //
      this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPeriod.FormattingEnabled = true;
      this.cboPeriod.Items.AddRange(new object[] {
        "NULL"
      });
      this.cboPeriod.Location = new System.Drawing.Point(502, 261);
      this.cboPeriod.Name = "cboPeriod";
      this.cboPeriod.Size = new System.Drawing.Size(124, 21);
      this.cboPeriod.TabIndex = 35;
      this.cboPeriod.Tag = "BasicPropertyChange";
      this.cboPeriod.TextChanged += new System.EventHandler(this.Action);
      //
      // lblExecutionLimit
      //
      this.lblExecutionLimit.AutoSize = true;
      this.lblExecutionLimit.Location = new System.Drawing.Point(502, 156);
      this.lblExecutionLimit.Name = "lblExecutionLimit";
      this.lblExecutionLimit.Size = new System.Drawing.Size(78, 13);
      this.lblExecutionLimit.TabIndex = 39;
      this.lblExecutionLimit.Text = "Execution Limit";
      //
      // txtExecutionLimit
      //
      this.txtExecutionLimit.Location = new System.Drawing.Point(502, 169);
      this.txtExecutionLimit.Name = "txtExecutionLimit";
      this.txtExecutionLimit.Size = new System.Drawing.Size(124, 20);
      this.txtExecutionLimit.TabIndex = 33;
      this.txtExecutionLimit.Tag = "BasicPropertyChange";
      this.txtExecutionLimit.TextChanged += new System.EventHandler(this.Action);
      this.txtExecutionLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerOnly_KeyPress);
      //
      // lblMaxRunTime
      //
      this.lblMaxRunTime.AutoSize = true;
      this.lblMaxRunTime.Location = new System.Drawing.Point(502, 110);
      this.lblMaxRunTime.Name = "lblMaxRunTime";
      this.lblMaxRunTime.Size = new System.Drawing.Size(105, 13);
      this.lblMaxRunTime.TabIndex = 41;
      this.lblMaxRunTime.Text = "Max Run Time (sec.)";
      //
      // txtMaxRunTime
      //
      this.txtMaxRunTime.Location = new System.Drawing.Point(502, 126);
      this.txtMaxRunTime.Name = "txtMaxRunTime";
      this.txtMaxRunTime.Size = new System.Drawing.Size(124, 20);
      this.txtMaxRunTime.TabIndex = 32;
      this.txtMaxRunTime.Tag = "BasicPropertyChange";
      this.txtMaxRunTime.TextChanged += new System.EventHandler(this.Action);
      this.txtMaxRunTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerOnly_KeyPress);
      //
      // lblOccurrence
      //
      this.lblOccurrence.AutoSize = true;
      this.lblOccurrence.Location = new System.Drawing.Point(267, 84);
      this.lblOccurrence.Name = "lblOccurrence";
      this.lblOccurrence.Size = new System.Drawing.Size(63, 13);
      this.lblOccurrence.TabIndex = 43;
      this.lblOccurrence.Text = "Occurrence";
      //
      // lblDayOfWeek
      //
      this.lblDayOfWeek.AutoSize = true;
      this.lblDayOfWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDayOfWeek.Location = new System.Drawing.Point(382, 18);
      this.lblDayOfWeek.Name = "lblDayOfWeek";
      this.lblDayOfWeek.Size = new System.Drawing.Size(70, 13);
      this.lblDayOfWeek.TabIndex = 44;
      this.lblDayOfWeek.Text = "Day of Week";
      this.lblDayOfWeek.Click += new System.EventHandler(this.lblDayOfWeek_Click);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 397);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(649, 18);
      this.lblStatus.TabIndex = 45;
      this.lblStatus.Text = "Status";
      //
      // btnSave
      //
      this.btnSave.Location = new System.Drawing.Point(18, 360);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 36;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(137, 360);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 37;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // upDownSpecificDay
      //
      this.upDownSpecificDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.upDownSpecificDay.Location = new System.Drawing.Point(18, 227);
      this.upDownSpecificDay.Maximum = new decimal(new int[] {
        31,
        0,
        0,
        0
      });
      this.upDownSpecificDay.Minimum = new decimal(new int[] {
        1,
        0,
        0,
        0
      });
      this.upDownSpecificDay.Name = "upDownSpecificDay";
      this.upDownSpecificDay.Size = new System.Drawing.Size(37, 23);
      this.upDownSpecificDay.TabIndex = 7;
      this.upDownSpecificDay.Tag = "UpDownChange";
      this.upDownSpecificDay.Value = new decimal(new int[] {
        1,
        0,
        0,
        0
      });
      this.upDownSpecificDay.ValueChanged += new System.EventHandler(this.Action);
      //
      // lblSpecificDaysList
      //
      this.lblSpecificDaysList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSpecificDaysList.Location = new System.Drawing.Point(18, 253);
      this.lblSpecificDaysList.Name = "lblSpecificDaysList";
      this.lblSpecificDaysList.Size = new System.Drawing.Size(231, 72);
      this.lblSpecificDaysList.TabIndex = 51;
      this.lblSpecificDaysList.Tag = "SpecificDaysChange";
      this.lblSpecificDaysList.TextChanged += new System.EventHandler(this.Action);
      //
      // btnAddDay
      //
      this.btnAddDay.Location = new System.Drawing.Point(61, 227);
      this.btnAddDay.Name = "btnAddDay";
      this.btnAddDay.Size = new System.Drawing.Size(91, 23);
      this.btnAddDay.TabIndex = 8;
      this.btnAddDay.Tag = "AddDay";
      this.btnAddDay.Text = "Add Day";
      this.btnAddDay.UseVisualStyleBackColor = true;
      this.btnAddDay.Click += new System.EventHandler(this.Action);
      //
      // btnRemoveDay
      //
      this.btnRemoveDay.Location = new System.Drawing.Point(158, 227);
      this.btnRemoveDay.Name = "btnRemoveDay";
      this.btnRemoveDay.Size = new System.Drawing.Size(91, 23);
      this.btnRemoveDay.TabIndex = 9;
      this.btnRemoveDay.Tag = "RemoveDay";
      this.btnRemoveDay.Text = "Remove Day";
      this.btnRemoveDay.UseVisualStyleBackColor = true;
      this.btnRemoveDay.Click += new System.EventHandler(this.Action);
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.lblEndDateCover);
      this.pnlMain.Controls.Add(this.lblStartTimeCover);
      this.pnlMain.Controls.Add(this.lblEndTimeCover);
      this.pnlMain.Controls.Add(this.lblStartDateCover);
      this.pnlMain.Controls.Add(this.cboExecutionType);
      this.pnlMain.Controls.Add(this.lblExecutionType);
      this.pnlMain.Controls.Add(this.btnCancel);
      this.pnlMain.Controls.Add(this.lblSpecificDaysList);
      this.pnlMain.Controls.Add(this.btnSave);
      this.pnlMain.Controls.Add(this.btnRemoveDay);
      this.pnlMain.Controls.Add(this.lblStartDateTime);
      this.pnlMain.Controls.Add(this.lblPeriod);
      this.pnlMain.Controls.Add(this.cboPeriod);
      this.pnlMain.Controls.Add(this.txtExecutionLimit);
      this.pnlMain.Controls.Add(this.txtMaxRunTime);
      this.pnlMain.Controls.Add(this.cboHolidayAction);
      this.pnlMain.Controls.Add(this.lblExecutionLimit);
      this.pnlMain.Controls.Add(this.lblHolidayAction);
      this.pnlMain.Controls.Add(this.lblDayOfWeek);
      this.pnlMain.Controls.Add(this.lblMaxRunTime);
      this.pnlMain.Controls.Add(this.btnAddDay);
      this.pnlMain.Controls.Add(this.lblOccurrence);
      this.pnlMain.Controls.Add(this.dtpStartDate);
      this.pnlMain.Controls.Add(this.dtpStartTime);
      this.pnlMain.Controls.Add(this.upDownSpecificDay);
      this.pnlMain.Controls.Add(this.lblEndDateTime);
      this.pnlMain.Controls.Add(this.dtpEndDate);
      this.pnlMain.Controls.Add(this.txtScheduleElementPriority);
      this.pnlMain.Controls.Add(this.dtpEndTime);
      this.pnlMain.Controls.Add(this.lblScheduleElementPriority);
      this.pnlMain.Controls.Add(this.cboIntervalType);
      this.pnlMain.Controls.Add(this.txtFrequency);
      this.pnlMain.Controls.Add(this.lblIntervalType);
      this.pnlMain.Controls.Add(this.lblFrequency);
      this.pnlMain.Controls.Add(this.lblSpecificDays);
      this.pnlMain.Controls.Add(this.chkOddDay);
      this.pnlMain.Controls.Add(this.chkEvenDay);
      this.pnlMain.Controls.Add(this.chkExceptSpecificDays);
      this.pnlMain.Controls.Add(this.chkWorkDay);
      this.pnlMain.Controls.Add(this.chkSaturday);
      this.pnlMain.Controls.Add(this.chkFriday);
      this.pnlMain.Controls.Add(this.chkThursday);
      this.pnlMain.Controls.Add(this.chkTuesday);
      this.pnlMain.Controls.Add(this.chkWednesday);
      this.pnlMain.Controls.Add(this.chkSunday);
      this.pnlMain.Controls.Add(this.chkIsActive);
      this.pnlMain.Controls.Add(this.chkMonday);
      this.pnlMain.Controls.Add(this.chkIsClockAligned);
      this.pnlMain.Controls.Add(this.chkEvery);
      this.pnlMain.Controls.Add(this.chkFourth);
      this.pnlMain.Controls.Add(this.chkFirst);
      this.pnlMain.Controls.Add(this.chkLast);
      this.pnlMain.Controls.Add(this.chkSecond);
      this.pnlMain.Controls.Add(this.chkThird);
      this.pnlMain.Controls.Add(this.chkFifth);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(649, 397);
      this.pnlMain.TabIndex = 55;
      //
      // lblEndDateCover
      //
      this.lblEndDateCover.Location = new System.Drawing.Point(155, 91);
      this.lblEndDateCover.Name = "lblEndDateCover";
      this.lblEndDateCover.Size = new System.Drawing.Size(93, 18);
      this.lblEndDateCover.TabIndex = 58;
      //
      // lblStartTimeCover
      //
      this.lblStartTimeCover.Location = new System.Drawing.Point(37, 117);
      this.lblStartTimeCover.Name = "lblStartTimeCover";
      this.lblStartTimeCover.Size = new System.Drawing.Size(93, 18);
      this.lblStartTimeCover.TabIndex = 57;
      //
      // lblEndTimeCover
      //
      this.lblEndTimeCover.Location = new System.Drawing.Point(155, 117);
      this.lblEndTimeCover.Name = "lblEndTimeCover";
      this.lblEndTimeCover.Size = new System.Drawing.Size(93, 18);
      this.lblEndTimeCover.TabIndex = 56;
      //
      // lblStartDateCover
      //
      this.lblStartDateCover.Location = new System.Drawing.Point(37, 91);
      this.lblStartDateCover.Name = "lblStartDateCover";
      this.lblStartDateCover.Size = new System.Drawing.Size(93, 18);
      this.lblStartDateCover.TabIndex = 52;
      //
      // frmScheduleElement
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(649, 415);
      this.ControlBox = false;
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmScheduleElement";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Tag = "FormShown";
      this.Text = "Schedule Element";
      this.VisibleChanged += new System.EventHandler(this.Action);
      ((System.ComponentModel.ISupportInitialize)(this.upDownSpecificDay)).EndInit();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkSecond;
    private System.Windows.Forms.CheckBox chkOddDay;
    private System.Windows.Forms.CheckBox chkWorkDay;
    private System.Windows.Forms.CheckBox chkSaturday;
    private System.Windows.Forms.CheckBox chkFriday;
    private System.Windows.Forms.CheckBox chkWednesday;
    private System.Windows.Forms.CheckBox chkMonday;
    private System.Windows.Forms.CheckBox chkLast;
    private System.Windows.Forms.CheckBox chkFifth;
    private System.Windows.Forms.CheckBox chkThird;
    private System.Windows.Forms.CheckBox chkFirst;
    private System.Windows.Forms.CheckBox chkThursday;
    private System.Windows.Forms.CheckBox chkTuesday;
    private System.Windows.Forms.CheckBox chkSunday;
    private System.Windows.Forms.CheckBox chkEvery;
    private System.Windows.Forms.CheckBox chkFourth;
    private System.Windows.Forms.CheckBox chkEvenDay;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.CheckBox chkIsClockAligned;
    private System.Windows.Forms.ComboBox cboExecutionType;
    private System.Windows.Forms.Label lblExecutionType;
    private System.Windows.Forms.Label lblStartDateTime;
    private System.Windows.Forms.Label lblEndDateTime;
    private System.Windows.Forms.DateTimePicker dtpStartTime;
    private System.Windows.Forms.DateTimePicker dtpEndTime;
    private System.Windows.Forms.DateTimePicker dtpStartDate;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.Label lblFrequency;
    private System.Windows.Forms.TextBox txtFrequency;
    private System.Windows.Forms.Label lblScheduleElementPriority;
    private System.Windows.Forms.TextBox txtScheduleElementPriority;
    private System.Windows.Forms.Label lblIntervalType;
    private System.Windows.Forms.ComboBox cboIntervalType;
    private System.Windows.Forms.Label lblSpecificDays;
    private System.Windows.Forms.CheckBox chkExceptSpecificDays;
    private System.Windows.Forms.Label lblHolidayAction;
    private System.Windows.Forms.ComboBox cboHolidayAction;
    private System.Windows.Forms.Label lblPeriod;
    private System.Windows.Forms.ComboBox cboPeriod;
    private System.Windows.Forms.Label lblExecutionLimit;
    private System.Windows.Forms.TextBox txtExecutionLimit;
    private System.Windows.Forms.Label lblMaxRunTime;
    private System.Windows.Forms.TextBox txtMaxRunTime;
    private System.Windows.Forms.Label lblOccurrence;
    private System.Windows.Forms.Label lblDayOfWeek;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.NumericUpDown upDownSpecificDay;
    private System.Windows.Forms.Label lblSpecificDaysList;
    private System.Windows.Forms.Button btnAddDay;
    private System.Windows.Forms.Button btnRemoveDay;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Label lblStartDateCover;
    private System.Windows.Forms.Label lblEndDateCover;
    private System.Windows.Forms.Label lblStartTimeCover;
    private System.Windows.Forms.Label lblEndTimeCover;
  }
}