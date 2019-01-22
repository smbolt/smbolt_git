using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;

namespace Org.OpsManager
{
  public partial class frmScheduleElement : Form
  {
    bool _isNewElement;
    OpsData _opsData;
    bool _isInitLoad = true;
    bool _isFirstShowing = true;
    List<int> _specificDays = new List<int>();

    public frmScheduleElement(OpsData opsData, bool isNewElement)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewElement = isNewElement;

      InitializeElementFields();
      if (_isNewElement)
      {
        this.Text = "New Schedule Element";
        btnSave.Enabled = true;
      }
      else
      {
        this.Text = "Edit Schedule Element";
        SetScheduleElementData();
        btnSave.Enabled = false;
      }
      _isInitLoad = false;
      UpdateCboIntervalType();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          SaveToDb();
          break;

        case "BasicPropertyChange":
          CheckForChanges();
          break;

        case "ExecutionTypeChange":
          UpdateCboIntervalType();
          CheckForChanges();
          break;

        case "DateTimePropertyChange":
          UpdateCboIntervalType();
          ShowHideDateTimeProperties();
          CheckForChanges();
          break;

        case "OccurrenceChange":
          UpdateCboPeriod();
          CheckForChanges();
          break;

        case "UpDownChange":
          UpdateAddRemoveButtons();
          break;

        case "SpecificDaysChange":
          UpdateAddRemoveButtons();
          CheckForChanges();
          break;

        case "AddDay":
          AddDay();
          break;

        case "RemoveDay":
          RemoveDay();
          break;

        case "FormShown":
          SetDefaultDateValues();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.OK;
          break;

        case "Exit":
          Environment.Exit(0);
          break;
      }
    }

    private void SaveToDb()
    {
      if (ValidUI())
      {
        DialogResult result = DialogResult.Yes;

        if ((chkFirst.Checked || chkSecond.Checked || chkThird.Checked || chkFourth.Checked || chkFifth.Checked || chkLast.Checked || chkEvery.Checked)
            && cboPeriod.Text == "NULL")
        {
          MessageBox.Show("There must be a Period Context if any occurrence is selected.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (cboExecutionType.Text == "RunOnFrequency" && (txtFrequency.Text.IsBlank() || txtFrequency.Text.ToInt32() <= 0))
        {
          MessageBox.Show("RunOnFrequency Tasks must have a Frequency greater than 0.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (_opsData.Environment == "Prod")
          result = MessageBox.Show("Save these changes to the TaskScheduleElements table in the Task Scheduling database?",
                                   "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.No)
          return;

        if (_isNewElement)
        {
          _opsData.CurrentScheduleElement.CreatedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentScheduleElement.CreatedDate = DateTime.Now;
        }
        else
        {
          _opsData.CurrentScheduleElement.TaskScheduleElementId = _opsData.CurrentScheduleElement.TaskScheduleElementId;
          _opsData.CurrentScheduleElement.ModifiedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentScheduleElement.ModifiedDate = DateTime.Now;
        }

        _opsData.CurrentScheduleElement.TaskScheduleId = _opsData.CurrentTaskSchedule.TaskScheduleId;

        _opsData.CurrentScheduleElement.TaskExecutionType = cboExecutionType.Text.ToEnum<TaskExecutionType>(TaskExecutionType.NotSet);

        if (cboIntervalType.Enabled && cboIntervalType.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.IntervalType = cboIntervalType.Text.ToEnum<IntervalType>(IntervalType.NotSet);

        if (cboHolidayAction.Enabled && cboHolidayAction.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.HolidayActions = cboHolidayAction.Text.ToEnum<HolidayActions>(HolidayActions.NotSet);

        if (cboPeriod.Enabled && cboPeriod.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.PeriodContexts = cboPeriod.Text.ToEnum<PeriodContexts>(PeriodContexts.NotSet);

        _opsData.CurrentScheduleElement.StartDate = dtpStartDate.Checked ? dtpStartDate.Value.Date : (DateTime?)null;
         
                _opsData.CurrentScheduleElement.StartTime = dtpStartTime.Checked ? dtpStartTime.Value.TimeOfDay : (TimeSpan?)null;
         
                _opsData.CurrentScheduleElement.EndDate = dtpEndDate.Checked ? dtpEndDate.Value.Date : (DateTime?)null;
         
                _opsData.CurrentScheduleElement.EndTime = dtpEndTime.Checked ? dtpEndTime.Value.TimeOfDay : (TimeSpan?)null;

        if (lblSpecificDaysList.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.SpecificDays = lblSpecificDaysList.Text;

        if (txtFrequency.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.FrequencySeconds = txtFrequency.Text.ToDecimal();

        if (txtScheduleElementPriority.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.ScheduleElementPriority = txtScheduleElementPriority.Text.ToInt32();

        if (txtMaxRunTime.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.MaxRunTimeSeconds = txtMaxRunTime.Text.ToInt32();

        if (txtExecutionLimit.Text.IsNotBlank())
          _opsData.CurrentScheduleElement.ExecutionLimit = txtExecutionLimit.Text.ToInt32();

        _opsData.CurrentScheduleElement.ExceptSpecificDays = chkExceptSpecificDays.Checked;
        _opsData.CurrentScheduleElement.IsActive = chkIsActive.Checked;
        _opsData.CurrentScheduleElement.IsClockAligned = chkIsClockAligned.Checked;
        _opsData.CurrentScheduleElement.First = chkFirst.Checked;
        _opsData.CurrentScheduleElement.Second = chkSecond.Checked;
        _opsData.CurrentScheduleElement.Third = chkThird.Checked;
        _opsData.CurrentScheduleElement.Fourth = chkThird.Checked;
        _opsData.CurrentScheduleElement.Fifth = chkFifth.Checked;
        _opsData.CurrentScheduleElement.Every = chkEvery.Checked;
        _opsData.CurrentScheduleElement.Last = chkLast.Checked;
        _opsData.CurrentScheduleElement.OnSunday = chkSunday.Checked;
        _opsData.CurrentScheduleElement.OnMonday = chkMonday.Checked;
        _opsData.CurrentScheduleElement.OnTuesday = chkTuesday.Checked;
        _opsData.CurrentScheduleElement.OnWednesday = chkWednesday.Checked;
        _opsData.CurrentScheduleElement.OnThursday = chkThursday.Checked;
        _opsData.CurrentScheduleElement.OnFriday = chkFriday.Checked;
        _opsData.CurrentScheduleElement.OnSaturday = chkSaturday.Checked;
        _opsData.CurrentScheduleElement.OnWorkDays = chkWorkDay.Checked;
        _opsData.CurrentScheduleElement.OnEvenDays = chkEvenDay.Checked;
        _opsData.CurrentScheduleElement.OnOddDays = chkOddDay.Checked;

        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          if (_isNewElement)
            repo.InsertTaskScheduleElement(_opsData.CurrentScheduleElement);
          else
            repo.UpdateTaskScheduleElement(_opsData.CurrentScheduleElement);
        }
        this.DialogResult = DialogResult.None;
        btnSave.Enabled = false;
      }
    }

    private bool ValidUI()
    {
      if (cboIntervalType.Enabled && cboIntervalType.Text.IsBlank())
      {
        MessageBox.Show("Please select valid Start and End Date/Time combination and Interval Type or uncheck all Dates and Times.",
                        "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      if (cboIntervalType.Text == "DailyInterval" && dtpStartTime.Value.TimeOfDay > dtpEndTime.Value.TimeOfDay)
      {
        MessageBox.Show("Please select a Start Time that is before your End Time when creating a Daily Interval",
                        "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      if (dtpStartDate.Checked && dtpEndDate.Checked && dtpStartDate.Value.Date > dtpEndDate.Value.Date)
      {
        MessageBox.Show("Please select a Start Date that is before the End Date.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      if (cboIntervalType.Text == "SingleSpan" && dtpStartDate.Checked && dtpEndDate.Checked && dtpStartTime.Checked && dtpEndTime.Checked)
      {
        DateTime startDateTime = dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay;
        DateTime endDateTime = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;
        if (startDateTime > endDateTime)
        {
          MessageBox.Show("Please select a Start Date and Time before the End Date and Time.",
                          "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return false;
        }
      }

      return true;
    }

    private void SetScheduleElementData()
    {
      try
      {
        PeriodContexts periodContext = _opsData.CurrentScheduleElement.PeriodContexts;
        HolidayActions holidayAction = _opsData.CurrentScheduleElement.HolidayActions;
        IntervalType intervalType = _opsData.CurrentScheduleElement.IntervalType;
        TaskExecutionType executionType = _opsData.CurrentScheduleElement.TaskExecutionType;

        cboPeriod.Text = periodContext == PeriodContexts.NotSet ? "" : periodContext.ToString();
        cboHolidayAction.Text = holidayAction == HolidayActions.NotSet ? "NULL" : holidayAction.ToString();
        cboIntervalType.Text = intervalType == IntervalType.NotSet ? "" : intervalType.ToString();
        cboExecutionType.Text = executionType == TaskExecutionType.NotSet ? "" : executionType.ToString();

        chkFirst.Checked = _opsData.CurrentScheduleElement.First;
        chkSecond.Checked = _opsData.CurrentScheduleElement.Second;
        chkThird.Checked = _opsData.CurrentScheduleElement.Third;
        chkFourth.Checked = _opsData.CurrentScheduleElement.Fourth;
        chkFifth.Checked = _opsData.CurrentScheduleElement.Fifth;
        chkEvery.Checked = _opsData.CurrentScheduleElement.Every;
        chkLast.Checked = _opsData.CurrentScheduleElement.Last;
        chkSunday.Checked = _opsData.CurrentScheduleElement.OnSunday;
        chkMonday.Checked = _opsData.CurrentScheduleElement.OnMonday;
        chkTuesday.Checked = _opsData.CurrentScheduleElement.OnTuesday;
        chkWednesday.Checked = _opsData.CurrentScheduleElement.OnWednesday;
        chkThursday.Checked = _opsData.CurrentScheduleElement.OnThursday;
        chkFriday.Checked = _opsData.CurrentScheduleElement.OnFriday;
        chkSaturday.Checked = _opsData.CurrentScheduleElement.OnSaturday;
        chkWorkDay.Checked = _opsData.CurrentScheduleElement.OnWorkDays;
        chkEvenDay.Checked = _opsData.CurrentScheduleElement.OnEvenDays;
        chkOddDay.Checked = _opsData.CurrentScheduleElement.OnOddDays;
        chkIsActive.Checked = _opsData.CurrentScheduleElement.IsActive;
        chkIsClockAligned.Checked = _opsData.CurrentScheduleElement.IsClockAligned;
        chkExceptSpecificDays.Checked = _opsData.CurrentScheduleElement.ExceptSpecificDays;

        txtFrequency.Text = _opsData.CurrentScheduleElement.FrequencySeconds.ToString();
        txtScheduleElementPriority.Text = _opsData.CurrentScheduleElement.ScheduleElementPriority.ToString();
        txtMaxRunTime.Text = _opsData.CurrentScheduleElement.MaxRunTimeSeconds.ToString();
        txtExecutionLimit.Text = _opsData.CurrentScheduleElement.ExecutionLimit.ToString();

        lblSpecificDaysList.Text = _opsData.CurrentScheduleElement.SpecificDays;
        if (lblSpecificDaysList.Text.IsNotBlank())
          _specificDays = lblSpecificDaysList.Text.Split(',').Select(Int32.Parse).ToList();

        if (_opsData.CurrentScheduleElement.StartDate != null)
        {
          dtpStartDate.Checked = true;
          dtpStartDate.Value = _opsData.CurrentScheduleElement.StartDate.ToDateTime();
          lblStartDateCover.Visible = false;
        }
        else
        {
          dtpStartDate.Checked = false;
          lblStartDateCover.Visible = true;
        }

        if (_opsData.CurrentScheduleElement.StartTime != null)
        {
          dtpStartTime.Checked = true;
          dtpStartTime.Text = _opsData.CurrentScheduleElement.StartTime.ToString();
          lblStartTimeCover.Visible = false;
        }
        else
        {
          dtpStartTime.Checked = false;
          lblStartTimeCover.Visible = true;
        }

        if (_opsData.CurrentScheduleElement.EndDate != null)
        {
          dtpEndDate.Checked = true;
          dtpEndDate.Value = _opsData.CurrentScheduleElement.EndDate.ToDateTime();
          lblEndDateCover.Visible = false;
        }
        else
        {
          dtpEndDate.Checked = false;
          lblEndDateCover.Visible = true;
        }

        if (_opsData.CurrentScheduleElement.EndTime != null)
        {
          dtpEndTime.Checked = true;
          dtpEndTime.Text = _opsData.CurrentScheduleElement.EndTime.ToString();
          lblEndTimeCover.Visible = false;
        }
        else
        {
          dtpEndTime.Checked = false;
          lblEndTimeCover.Visible = true;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to set Schedule Elements info", ex);
      }
    }

    private void CheckForChanges()
    {
      if (_isFirstShowing)
        return;

      var element = _opsData.CurrentScheduleElement;

      if (chkFirst.Checked != element.First || chkSecond.Checked != element.Second || chkThird.Checked != element.Third ||
          chkFourth.Checked != element.Fourth || chkFifth.Checked != element.Fifth || chkEvery.Checked != element.Every ||
          chkLast.Checked != element.Last || chkSunday.Checked != element.OnSunday || chkMonday.Checked != element.OnMonday ||
          chkTuesday.Checked != element.OnTuesday || chkWednesday.Checked != element.OnWednesday || chkThursday.Checked != element.OnThursday ||
          chkFriday.Checked != element.OnFriday || chkSaturday.Checked != element.OnSaturday || chkWorkDay.Checked != element.OnWorkDays ||
          chkEvenDay.Checked != element.OnEvenDays || chkOddDay.Checked != element.OnOddDays || chkIsActive.Checked != element.IsActive ||
          chkIsClockAligned.Checked != element.IsClockAligned || chkExceptSpecificDays.Checked != element.ExceptSpecificDays ||

          txtFrequency.Text.Trim() != element.FrequencySeconds.ToString() ||
          txtScheduleElementPriority.Text.Trim() != element.ScheduleElementPriority.ToString() ||
          txtMaxRunTime.Text.Trim() != element.MaxRunTimeSeconds.ToString() ||
          txtExecutionLimit.Text.Trim() != element.ExecutionLimit.ToString() ||

          cboPeriod.Enabled == (element.PeriodContexts == PeriodContexts.NotSet) ||
          (cboPeriod.Enabled && cboPeriod.Text.Trim() != element.PeriodContexts.ToString()) ||

          cboHolidayAction.Text != element.HolidayActions.ToString() ||
          cboExecutionType.Text != element.TaskExecutionType.ToString() ||

          cboIntervalType.Enabled == (element.IntervalType == IntervalType.NotSet) ||
          (cboIntervalType.Enabled && cboIntervalType.Text != element.IntervalType.ToString()) ||

          lblSpecificDaysList.Text.GetStringValueOrNull() != element.SpecificDays ||

          dtpStartDate.Checked != element.StartDate.HasValue || dtpEndDate.Checked != element.EndDate.HasValue ||
          dtpStartTime.Checked != element.StartTime.HasValue || dtpEndTime.Checked != element.EndTime.HasValue ||

          (dtpStartDate.Checked && element.StartDate.HasValue && dtpStartDate.Value.Date != element.StartDate) ||
          (dtpStartTime.Checked && element.StartTime.HasValue && dtpStartTime.Value.TimeOfDay != element.StartTime) ||
          (dtpEndDate.Checked && element.EndDate.HasValue && dtpEndDate.Value.Date != element.EndDate) ||
          (dtpEndTime.Checked && element.EndTime.HasValue && dtpEndTime.Value.TimeOfDay != element.EndTime)
         )
      {
        btnSave.Enabled = true;
      }
      else btnSave.Enabled = false;
    }

    private void UpdateCboIntervalType()
    {
      if (_isInitLoad)
        return;

      if (cboExecutionType.Text != "RunOnceAt")
      {
        //No specified intervals
        if (!dtpStartDate.Checked && !dtpStartTime.Checked && !dtpEndDate.Checked && !dtpEndTime.Checked)
        {
          cboIntervalType.Items.Clear();
          cboIntervalType.Enabled = false;
        }
        //DailyInterval or SingleSpan
        else if (dtpStartDate.Checked && dtpStartTime.Checked && dtpEndDate.Checked && dtpEndTime.Checked)
        {
          cboIntervalType.Enabled = true;
          if (!cboIntervalType.Items.Contains("DailyInterval"))
            cboIntervalType.Items.Add("DailyInterval");
          if (!cboIntervalType.Items.Contains("SingleSpan"))
            cboIntervalType.Items.Add("SingleSpan");
        }
        //DailyInterval only
        else if (dtpStartTime.Checked && dtpEndTime.Checked)
        {
          cboIntervalType.Enabled = true;
          if (!cboIntervalType.Items.Contains("DailyInterval"))
            cboIntervalType.Items.Add("DailyInterval");
          if (cboIntervalType.Items.Contains("SingleSpan"))
            cboIntervalType.Items.Remove("SingleSpan");
        }
        //Invalid Selection
        else if ((dtpStartDate.Checked && !dtpStartTime.Checked && !dtpEndDate.Checked && dtpEndTime.Checked) ||
                 (!dtpStartDate.Checked && dtpStartTime.Checked && dtpEndDate.Checked && !dtpEndTime.Checked) ||
                 (!dtpStartDate.Checked && dtpStartTime.Checked && !dtpEndDate.Checked && !dtpEndTime.Checked) ||
                 (!dtpStartDate.Checked && !dtpStartTime.Checked && !dtpEndDate.Checked && dtpEndTime.Checked))
        {
          cboIntervalType.Enabled = true;
          cboIntervalType.Items.Clear();
        }
        //SingleSpan only
        else
        {
          cboIntervalType.Enabled = true;
          if (cboIntervalType.Items.Contains("DailyInterval"))
            cboIntervalType.Items.Remove("DailyInterval");
          if (!cboIntervalType.Items.Contains("SingleSpan"))
            cboIntervalType.Items.Add("SingleSpan");
        }
      }
      else
      {
        cboIntervalType.Items.Clear();
        cboIntervalType.Enabled = false;
      }
    }

    private void ShowHideDateTimeProperties()
    {
      if (!_isFirstShowing)
      {
        if (dtpStartDate.Checked)
          lblStartDateCover.Visible = false;
        else
          lblStartDateCover.Visible = true;

        if (dtpStartTime.Checked)
          lblStartTimeCover.Visible = false;
        else
          lblStartTimeCover.Visible = true;

        if (dtpEndDate.Checked)
          lblEndDateCover.Visible = false;
        else
          lblEndDateCover.Visible = true;

        if (dtpEndTime.Checked)
          lblEndTimeCover.Visible = false;
        else
          lblEndTimeCover.Visible = true;
      }
    }

    private void UpdateCboPeriod()
    {
      if (chkFirst.Checked || chkSecond.Checked || chkThird.Checked || chkFourth.Checked || chkFifth.Checked || chkEvery.Checked || chkLast.Checked)
        cboPeriod.Enabled = true;
      else
        cboPeriod.Enabled = false;
    }

    private void UpdateAddRemoveButtons()
    {
      int inputDay = upDownSpecificDay.Value.ToInt32();

      if (_specificDays.Contains(inputDay))
      {
        btnAddDay.Enabled = false;
        btnRemoveDay.Enabled = true;
      }
      else
      {
        btnAddDay.Enabled = true;
        btnRemoveDay.Enabled = false;
      }
    }

    private void AddDay()
    {
      int newDay = upDownSpecificDay.Value.ToInt32();
      _specificDays.Add(newDay);
      ListSpecificDays();
    }

    private void RemoveDay()
    {
      int dayToRemove = upDownSpecificDay.Value.ToInt32();
      _specificDays.Remove(dayToRemove);
      ListSpecificDays();
    }

    private void ListSpecificDays()
    {
      bool isFirstDay = true;
      StringBuilder sb = new StringBuilder();
      _specificDays.Sort();
      lblSpecificDaysList.Text.Clear();

      foreach (var day in _specificDays)
      {
        if (isFirstDay)
          isFirstDay = false;
        else sb.Append(',');
        sb.Append(day);
      }

      lblSpecificDaysList.Text = sb.ToString();
    }

    private void SetDefaultDateValues()
    {
      if (_isFirstShowing)
      {
        if (!_opsData.CurrentScheduleElement.StartDate.HasValue)
        {
          dtpStartDate.Checked = true;
          dtpStartDate.Value = DateTime.Today;
          dtpStartDate.Checked = _isNewElement;
        }

        if (!_opsData.CurrentScheduleElement.EndDate.HasValue)
        {
          dtpEndDate.Checked = true;
          dtpEndDate.Value = DateTime.Today.AddDays(1);
          dtpEndDate.Checked = _isNewElement;
        }

        if (!_opsData.CurrentScheduleElement.StartTime.HasValue)
        {
          dtpStartTime.Checked = true;
          dtpStartTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
          dtpStartTime.Checked = _isNewElement;
        }

        if (!_opsData.CurrentScheduleElement.EndTime.HasValue)
        {
          dtpEndTime.Checked = true;
          dtpEndTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
          dtpEndTime.Checked = _isNewElement;
        }
        _isFirstShowing = false;
        ShowHideDateTimeProperties();
      }
    }

    private void Frequency_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        e.Handled = true;

      bool containsPeriod = (sender as TextBox).Text.IndexOf('.') > -1;

      if (e.KeyChar == '.' && containsPeriod)
        e.Handled = true;

      if (containsPeriod && char.IsDigit(e.KeyChar) && ((sender as TextBox).Text + e.KeyChar.ToString()).ToDecimal() < (decimal)0.1)
        e.Handled = true;
    }

    private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        e.Handled = true;
    }

    private void InitializeElementFields()
    {
      try
      {
        foreach (PeriodContexts period in Enum.GetValues(typeof(PeriodContexts)))
          if (period != PeriodContexts.NotSet)
            cboPeriod.Items.Add(period.ToString());
        cboPeriod.SelectedIndex = 0;

        foreach (HolidayActions holidayAction in Enum.GetValues(typeof(HolidayActions)))
          if (holidayAction != HolidayActions.NotSet)
            cboHolidayAction.Items.Add(holidayAction.ToString());
        cboHolidayAction.SelectedIndex = 0;

        foreach (TaskExecutionType executionType in Enum.GetValues(typeof(TaskExecutionType)))
          if (executionType != TaskExecutionType.NotSet)
            cboExecutionType.Items.Add(executionType.ToString());
        cboExecutionType.SelectedIndex = 0;

        foreach (IntervalType intervalType in Enum.GetValues(typeof(IntervalType)))
          if (intervalType != IntervalType.NotSet)
            cboIntervalType.Items.Add(intervalType.ToString());
        cboIntervalType.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the element fields" + g.crlf2 +
                        "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Initialization Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void lblDayOfWeek_Click(object sender,EventArgs e) {

    }
  }
}
