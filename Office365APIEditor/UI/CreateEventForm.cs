// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.CalendarAPI;
using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class CreateEventForm : Form
    {
        FolderInfo targetFolder;
        string targetFolderDisplayName;

        private ViewerRequestHelper viewerRequestHelper;

        List<ViewerHelper.Data.AttachmentAPI.AttachmentBase> attachments;

        public CreateEventForm()
        {
            InitializeComponent();

            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
        }
        
        public CreateEventForm(FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();

            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
        }

        private async void CreateEventForm_LoadAsync(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;
            
            attachments = new List<ViewerHelper.Data.AttachmentAPI.AttachmentBase>();

            // Display the window in the center of the parent window.
            Location = new Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);

            viewerRequestHelper = new ViewerRequestHelper();

            groupBox_Recurring_Pattern.Enabled = false;
            groupBox_Recurring_Range.Enabled = false;

            radioButton_Recurring_Pattern_Daily.Checked = true;

            groupBox_Recurring_Pattern_Weekly.Enabled = false;
            groupBox_Recurring_Pattern_Monthly.Enabled = false;
            groupBox_Recurring_Pattern_Yearly.Enabled = false;

            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Add("First");
            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Add("Second");
            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Add("Third");
            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Add("Fourth");
            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Add("Last");

            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Add("First");
            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Add("Second");
            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Add("Third");
            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Add("Fourth");
            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Add("Last");

            for (int i = 1; i <= 100 ; i++)
            {
                comboBox_Recurring_Pattern_Daily_Interval.Items.Add(i);
                comboBox_Recurring_Pattern_Weekly_Interval.Items.Add(i);
                comboBox_Recurring_Pattern_Monthly_Interval.Items.Add(i);
                comboBox_Recurring_Pattern_Yearly_Interval.Items.Add(i);
                comboBox_Recurring_Range_NumberOfConcurrences.Items.Add(i);
            }

            comboBox_Recurring_Pattern_Daily_Interval.SelectedItem = 1;
            comboBox_Recurring_Pattern_Weekly_Interval.SelectedItem = 1;
            comboBox_Recurring_Pattern_Monthly_Interval.SelectedItem = 1;
            comboBox_Recurring_Pattern_Yearly_Interval.SelectedItem = 1;
            comboBox_Recurring_Range_NumberOfConcurrences.SelectedItem = 10;

            radioButton_Recurring_Pattern_Monthly_MonthlyPattern.Checked = true;
            ToggleMonthlyPatternUI(true);

            for (int i = 1; i <= 31; i++)
            {
                comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.Items.Add(i);
                comboBox_Recurring_Pattern_Yearly_Absolute_Day.Items.Add(i);
            }

            for (int i = 1; i <= 12; i++)
            {
                string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i - 1];
                comboBox_Recurring_Pattern_Yearly_Absolute_Month.Items.Add(monthName);
                comboBox_Recurring_Pattern_Yearly_Relative_Month.Items.Add(monthName);
            }
 
            comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.SelectedItem = 1;

            radioButton_Recurring_Pattern_Yearly_OnSpecificDay.Checked = true;
            ToggleYearlyPatternUI(true);

            radioButton_Recurring_Range_NoEnd.Checked = true;

            DateTime now = DateTime.Now; // Use local time;
            DateTime tempStartDateTime = now;

            if (tempStartDateTime.Minute < 30)
            {
                tempStartDateTime = tempStartDateTime.AddMinutes(30 - tempStartDateTime.Minute);
            }
            else if (tempStartDateTime.Minute == 30)
            {
                tempStartDateTime = tempStartDateTime.AddMinutes(30);
            }
            else if (30 < tempStartDateTime.Minute && tempStartDateTime.Minute < 60)
            {
                tempStartDateTime = tempStartDateTime.AddMinutes(60 - tempStartDateTime.Minute);
            }

            dateTimePicker_Basic_StartDate.Value = tempStartDateTime;
            dateTimePicker_Basic_StartTime.Value = tempStartDateTime;

            DateTime tempEndDateTime = tempStartDateTime.AddMinutes(30);
            dateTimePicker_Basic_EndDate.Value = tempEndDateTime;
            dateTimePicker_Basic_EndTime.Value = tempEndDateTime;

            string localTimeZone = Util.LocalTimeZoneId;

            // Load all system time zones and add them to the combo box.
            var timeZoneCollection = TimeZoneInfo.GetSystemTimeZones();
            foreach (var timeZone in timeZoneCollection)
            {
                var matches = Regex.Matches(timeZone.DisplayName, @"^\(UTC.*\)\s");

                if (matches.Count == 1)
                {
                    comboBox_Basic_TimeZone.Items.Add($"{matches[0]} {timeZone.Id}");
                }
                else
                {
                    comboBox_Basic_TimeZone.Items.Add(timeZone.Id);
                }

                if (timeZone.Id == localTimeZone)
                {
                    // Select local time zone.
                    comboBox_Basic_TimeZone.SelectedIndex = comboBox_Basic_TimeZone.Items.Count - 1;
                }
            }

            // Load all calendar API supported time zones and add them to the combo box.
            foreach (var timeZone in Util.GetCalendarApiSupportedTimeZones())
            {
                comboBox_Basic_TimeZone.Items.Add(timeZone);
            }

            comboBox_BodyType.SelectedIndex = 1; // HTML
            comboBox_Sensitivity.SelectedIndex = 0; // Normal

            comboBox_Recurring_Range_NumberOfConcurrences.Enabled = false;
            dateTimePicker_Recurring_Range_EndByDate.Enabled = false;

            comboBox_OnlineMeeting_Provider.Items.Add("None");
            comboBox_OnlineMeeting_Provider.SelectedIndex = 0;

            // Get allowedOnlineMeetingProviders

            try
            {
                var allowedOnlineMeetingProviders = await viewerRequestHelper.GetAllowdOnlineMeetingProvidersAsync();

                if (allowedOnlineMeetingProviders == null || allowedOnlineMeetingProviders.Count == 0)
                {
                    throw new Exception("Microsoft Graph request was succeeded, but allowedOnlineMeetingProviders property is null or empty.");
                }

                if (allowedOnlineMeetingProviders.Count == 1 && allowedOnlineMeetingProviders[0] == "unknown")
                {
                    throw new Exception("Microsoft Graph request was succeeded, but allowedOnlineMeetingProviders property contains only 'unknown'");
                }

                foreach (var provider in allowedOnlineMeetingProviders)
                {
                    if (provider != "unknown")
                    {
                        comboBox_OnlineMeeting_Provider.Items.Add(provider);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get allowed online meeting providers." + Environment.NewLine + Environment.NewLine + ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            textBox_Basic_Title.Focus();
        }

        private void DateTimePicker_Basic_StartDate_ValueChanged(object sender, EventArgs e)
        {
            // dateTimePicker_Recurring_Range_Start and dateTimePicker_Basic_StartDate need to be same.

            var newStartDate = ((DateTimePicker)sender).Value;
            var oldStartDate = dateTimePicker_Recurring_Range_Start.Value;

            if (newStartDate.Date != oldStartDate.Date)
            {
                dateTimePicker_Recurring_Range_Start.Value = newStartDate;
            }

            UpdateRecurringSetting(newStartDate);
        }

        private void UpdateRecurringSetting(DateTime newStartDate)
        {
            // Update reccuring settings

            if (!checkBox_Recurring_IsRecurring.Checked)
            {
                checkBox_Recurring_Pattern_Weekly_Sunday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Monday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Tuesday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Wednesday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Thursday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Friday.Checked = false;
                checkBox_Recurring_Pattern_Weekly_Saturday.Checked = false;

                if (comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.Items.Count == 31)
                {
                    comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.SelectedItem = newStartDate.Day;
                }

                var tempDayOfWeekIndex = (int)(Math.Ceiling(newStartDate.Day / 7.0) - 1.0);
                
                if (newStartDate.AddDays(7).Month != newStartDate.Month)
                {
                    tempDayOfWeekIndex = 4; // Last
                }

                if (comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Items.Count == 5)
                {
                    comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.SelectedIndex = tempDayOfWeekIndex;
                }

                checkBox_Recurring_Pattern_Monthly_Sunday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Monday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Tuesday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Wednesday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Thursday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Friday.Checked = false;
                checkBox_Recurring_Pattern_Monthly_Saturday.Checked = false;

                if (comboBox_Recurring_Pattern_Yearly_Absolute_Month.Items.Count == 12)
                {
                    comboBox_Recurring_Pattern_Yearly_Absolute_Month.SelectedIndex = newStartDate.Month - 1;
                }

                if (comboBox_Recurring_Pattern_Yearly_Absolute_Day.Items.Count == 31)
                {
                    comboBox_Recurring_Pattern_Yearly_Absolute_Day.SelectedIndex = newStartDate.Day - 1;
                }

                if (comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Items.Count == 5)
                {
                    comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.SelectedIndex = tempDayOfWeekIndex;
                }

                if (comboBox_Recurring_Pattern_Yearly_Relative_Month.Items.Count == 12)
                {
                    comboBox_Recurring_Pattern_Yearly_Relative_Month.SelectedIndex = newStartDate.Month - 1;
                }

                checkBox_Recurring_Pattern_Yearly_Sunday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Monday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Tuesday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Wednesday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Thursday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Friday.Checked = false;
                checkBox_Recurring_Pattern_Yearly_Saturday.Checked = false;

                switch (newStartDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        checkBox_Recurring_Pattern_Weekly_Sunday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Sunday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Sunday.Checked = true;
                        break;
                    case DayOfWeek.Monday:
                        checkBox_Recurring_Pattern_Weekly_Monday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Monday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Monday.Checked = true;
                        break;
                    case DayOfWeek.Tuesday:
                        checkBox_Recurring_Pattern_Weekly_Tuesday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Tuesday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Tuesday.Checked = true;
                        break;
                    case DayOfWeek.Wednesday:
                        checkBox_Recurring_Pattern_Weekly_Wednesday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Wednesday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Wednesday.Checked = true;
                        break;
                    case DayOfWeek.Thursday:
                        checkBox_Recurring_Pattern_Weekly_Thursday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Thursday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Thursday.Checked = true;
                        break;
                    case DayOfWeek.Friday:
                        checkBox_Recurring_Pattern_Weekly_Friday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Friday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Friday.Checked = true;
                        break;
                    case DayOfWeek.Saturday:
                        checkBox_Recurring_Pattern_Weekly_Saturday.Checked = true;
                        checkBox_Recurring_Pattern_Monthly_Saturday.Checked = true;
                        checkBox_Recurring_Pattern_Yearly_Saturday.Checked = true;
                        break;
                    default:
                        break;
                }
            }

            if (dateTimePicker_Recurring_Range_EndByDate.Value.Date < newStartDate)
            {
                // The end date of recurring must be in the future.
                dateTimePicker_Recurring_Range_EndByDate.MinDate = newStartDate;
                dateTimePicker_Recurring_Range_EndByDate.Value = newStartDate;
            }
        }

        private void CheckBox_Basic_IsAllDay_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllDay = ((CheckBox)sender).Checked;

            dateTimePicker_Basic_StartTime.Enabled = !isAllDay;
            dateTimePicker_Basic_EndDate.Enabled = !isAllDay;
            dateTimePicker_Basic_EndTime.Enabled = !isAllDay;
        }

        private void CheckBox_Recurring_IsRecurring_CheckedChanged(object sender, EventArgs e)
        {
            bool isRecurring = ((CheckBox)sender).Checked;

            groupBox_Recurring_Pattern.Enabled = isRecurring;
            groupBox_Recurring_Range.Enabled = isRecurring;
        }

        private void DateTimePicker_Recurring_Range_Start_ValueChanged(object sender, EventArgs e)
        {
            // dateTimePicker_Recurring_Range_Start and dateTimePicker_Basic_StartDate need to be same.

            var newStartDate = ((DateTimePicker)sender).Value;
            var oldStartDate = dateTimePicker_Basic_StartDate.Value;

            if (newStartDate.Date != oldStartDate.Date)
            {
                dateTimePicker_Basic_StartDate.Value = newStartDate;
            }

            UpdateRecurringSetting(newStartDate);
        }

        private void radioButton_Recurring_Pattern_Daily_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Recurring_Pattern_Daily.Enabled = radioButton_Recurring_Pattern_Daily.Checked;
        }

        private void radioButton_Recurring_Pattern_Weekly_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Recurring_Pattern_Weekly.Enabled = radioButton_Recurring_Pattern_Weekly.Checked;
        }

        private void radioButton_Recurring_Pattern_Monthly_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Recurring_Pattern_Monthly.Enabled = radioButton_Recurring_Pattern_Monthly.Checked;
        }

        private void radioButton_Recurring_Pattern_Yearly_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_Recurring_Pattern_Yearly.Enabled = radioButton_Recurring_Pattern_Yearly.Checked;
        }

        private void radioButton_Recurring_Pattern_Monthly_MonthlyPattern_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMonthlyPatternUI(true);
        }

        private void radioButton_Recurring_Pattern_Monthly_RelativeMonthlyPattern_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMonthlyPatternUI(false);
        }

        private void ToggleMonthlyPatternUI(bool enableMonthlyPattern)
        {
            comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.Enabled = enableMonthlyPattern;
            label_Recurring_Pattern_Monthly_DayOfTheMonth.Enabled = enableMonthlyPattern;

            comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.Enabled = !enableMonthlyPattern;
            label_Recurring_Pattern_Monthly_DayOfWeekIndex.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Sunday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Monday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Tuesday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Wednesday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Thursday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Friday.Enabled = !enableMonthlyPattern;
            checkBox_Recurring_Pattern_Monthly_Saturday.Enabled = !enableMonthlyPattern;
        }

        private void radioButton_Recurring_Pattern_Yearly_OnSpecificDay_CheckedChanged(object sender, EventArgs e)
        {
            ToggleYearlyPatternUI(true);
        }

        private void radioButton_Recurring_Pattern_Yearly_RelativeYearlyPattern_CheckedChanged(object sender, EventArgs e)
        {
            ToggleYearlyPatternUI(false);
        }

        private void ToggleYearlyPatternUI(bool enableOnSpecificDay)
        {
            comboBox_Recurring_Pattern_Yearly_Absolute_Month.Enabled = enableOnSpecificDay;
            label_Recurring_Pattern_Yearly_Absolute_Date.Enabled = enableOnSpecificDay;
            comboBox_Recurring_Pattern_Yearly_Absolute_Day.Enabled = enableOnSpecificDay;

            label_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Enabled = !enableOnSpecificDay;
            comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.Enabled = !enableOnSpecificDay;
            label_Recurring_Pattern_Yearly_Relative_Month.Enabled = !enableOnSpecificDay;
            comboBox_Recurring_Pattern_Yearly_Relative_Month.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Sunday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Monday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Tuesday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Wednesday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Thursday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Friday.Enabled = !enableOnSpecificDay;
            checkBox_Recurring_Pattern_Yearly_Saturday.Enabled = !enableOnSpecificDay;
        }

        private void radioButton_Recurring_Range_EndAfter_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_Recurring_Range_NumberOfConcurrences.Enabled = ((RadioButton)sender).Checked;
        }

        private void radioButton_Recurring_Range_EndBy_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_Recurring_Range_EndByDate.Enabled = ((RadioButton)sender).Checked;
        }

        private async void Button_Save_ClickAsync(object sender, EventArgs e)
        {
            // Save new event.

            Enabled = false;

            viewerRequestHelper = new ViewerRequestHelper();

            NewEvent newItem;

            try
            {
                newItem = CreateNewEventObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }

            try
            {
                await viewerRequestHelper.CreateEventAsync(newItem);

                Close();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    MessageBox.Show(response.StatusCode.ToString() + Environment.NewLine + jsonResponse + Environment.NewLine, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }

        private NewEvent CreateNewEventObject()
        {
            NewEvent newItem = new NewEvent();

            newItem.Subject = textBox_Basic_Title.Text;

            try
            {
                if (textBox_Basic_RequiredAttendees.Text != "")
                {
                    foreach (var recipient in textBox_Basic_RequiredAttendees.Text.Split(';'))
                    {
                        newItem.Attendees.Add(new Attendee("", recipient.Trim()) { Type = "required" });
                    }
                }

                if (textBox_Basic_OptionalAttendees.Text != "")
                {
                    foreach (var recipient in textBox_Basic_OptionalAttendees.Text.Split(';'))
                    {
                        newItem.Attendees.Add(new Attendee("", recipient.Trim()) { Type = "optional" });
                    }
                }

                if (textBox_Basic_ResourceAttendees.Text != "")
                {
                    foreach (var recipient in textBox_Basic_ResourceAttendees.Text.Split(';'))
                    {
                        newItem.Attendees.Add(new Attendee("", recipient.Trim()) { Type = "resource" });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid attendees. " + ex.Message, ex);
            }

            newItem.Location.DisplayName = textBox_Basic_Location.Text;
            newItem.IsAllDay = checkBox_Basic_IsAllDay.Checked;

            string selectedTimeZone = comboBox_Basic_TimeZone.SelectedItem.ToString();
            var matches = Regex.Matches(selectedTimeZone, @"^\(UTC.*\)\s");

            if (matches.Count != 0)
            {
                // selectedTimeZone contains offset information.
                selectedTimeZone = selectedTimeZone.Remove(0, matches[0].Length + 1);
            }

            newItem.Start = new DateTimeAndTimeZone()
            {
                DateTime = new DateTime(
                    dateTimePicker_Basic_StartDate.Value.Year,
                    dateTimePicker_Basic_StartDate.Value.Month,
                    dateTimePicker_Basic_StartDate.Value.Day,
                    dateTimePicker_Basic_StartTime.Value.Hour,
                    dateTimePicker_Basic_StartTime.Value.Minute,
                    0).ToString("yyyy-MM-ddTHH:mm:ss"),
                TimeZone = selectedTimeZone
            };

            newItem.End = new DateTimeAndTimeZone()
            {
                DateTime = new DateTime(
                    dateTimePicker_Basic_EndDate.Value.Year,
                    dateTimePicker_Basic_EndDate.Value.Month,
                    dateTimePicker_Basic_EndDate.Value.Day,
                    dateTimePicker_Basic_EndTime.Value.Hour,
                    dateTimePicker_Basic_EndTime.Value.Minute,
                    0).ToString("yyyy-MM-ddTHH:mm:ss"),
                TimeZone = selectedTimeZone
            };

            newItem.Body.ContentType = (BodyType)comboBox_BodyType.SelectedIndex;
            newItem.Body.Content = textBox_Body.Text;
            newItem.Sensitivity = (Sensitivity)comboBox_Sensitivity.SelectedIndex;
            //newItem.Importance = (Importance)comboBox_Importance.SelectedIndex;
            //newItem.Attachments = attachments;

            // Recurring
            if (checkBox_Recurring_IsRecurring.Checked)
            {
                newItem.Recurrence = new PatternedRecurrence();

                if (radioButton_Recurring_Pattern_Daily.Checked)
                {
                    // Daily
                    newItem.Recurrence.Pattern.Type = RecurrencePatternType.Daily;
                    newItem.Recurrence.Pattern.Interval = (int)comboBox_Recurring_Pattern_Daily_Interval.SelectedItem;
                }
                else if (radioButton_Recurring_Pattern_Weekly.Checked)
                {
                    // Weekly
                    newItem.Recurrence.Pattern.Type = RecurrencePatternType.Weekly;
                    newItem.Recurrence.Pattern.Interval = (int)comboBox_Recurring_Pattern_Weekly_Interval.SelectedItem;

                    List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

                    if (checkBox_Recurring_Pattern_Weekly_Sunday.Checked) daysOfWeek.Add(DayOfWeek.Sunday);
                    if (checkBox_Recurring_Pattern_Weekly_Monday.Checked) daysOfWeek.Add(DayOfWeek.Monday);
                    if (checkBox_Recurring_Pattern_Weekly_Thursday.Checked) daysOfWeek.Add(DayOfWeek.Thursday);
                    if (checkBox_Recurring_Pattern_Weekly_Wednesday.Checked) daysOfWeek.Add(DayOfWeek.Wednesday);
                    if (checkBox_Recurring_Pattern_Weekly_Thursday.Checked) daysOfWeek.Add(DayOfWeek.Thursday);
                    if (checkBox_Recurring_Pattern_Weekly_Friday.Checked) daysOfWeek.Add(DayOfWeek.Friday);
                    if (checkBox_Recurring_Pattern_Weekly_Saturday.Checked) daysOfWeek.Add(DayOfWeek.Saturday);

                    if (daysOfWeek.Count == 0)
                    {
                        throw new Exception("For the weekly recurring event, select at least one day of the week.");
                    }

                    newItem.Recurrence.Pattern.DaysOfWeek = daysOfWeek.ToArray(); ;

                    newItem.Recurrence.Pattern.FirstDayOfWeek = DayOfWeek.Sunday;
                }
                else if (radioButton_Recurring_Pattern_Monthly.Checked)
                {
                    // Monthly

                    newItem.Recurrence.Pattern.Interval = (int)comboBox_Recurring_Pattern_Monthly_Interval.SelectedItem;

                    if (radioButton_Recurring_Pattern_Monthly_MonthlyPattern.Checked)
                    {
                        // Absolute Monthly
                        newItem.Recurrence.Pattern.Type = RecurrencePatternType.AbsoluteMonthly;
                        newItem.Recurrence.Pattern.DayOfMonth = (int)comboBox_Recurring_Pattern_Monthly_DayOfTheMonth.SelectedItem;
                    }
                    else
                    {
                        // Relative Monthly
                        newItem.Recurrence.Pattern.Type = RecurrencePatternType.RelativeMonthly;
                        newItem.Recurrence.Pattern.Index = (WeekIndex)comboBox_Recurring_Pattern_Monthly_DayOfWeekIndex.SelectedIndex;

                        List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

                        if (checkBox_Recurring_Pattern_Monthly_Sunday.Checked) daysOfWeek.Add(DayOfWeek.Sunday);
                        if (checkBox_Recurring_Pattern_Monthly_Monday.Checked) daysOfWeek.Add(DayOfWeek.Monday);
                        if (checkBox_Recurring_Pattern_Monthly_Tuesday.Checked) daysOfWeek.Add(DayOfWeek.Tuesday);
                        if (checkBox_Recurring_Pattern_Monthly_Wednesday.Checked) daysOfWeek.Add(DayOfWeek.Wednesday);
                        if (checkBox_Recurring_Pattern_Monthly_Thursday.Checked) daysOfWeek.Add(DayOfWeek.Thursday);
                        if (checkBox_Recurring_Pattern_Monthly_Friday.Checked) daysOfWeek.Add(DayOfWeek.Friday);
                        if (checkBox_Recurring_Pattern_Monthly_Saturday.Checked) daysOfWeek.Add(DayOfWeek.Saturday);

                        if (daysOfWeek.Count == 0)
                        {
                            throw new Exception("For the Monthly recurring event, select at least one day of the week.");
                        }

                        newItem.Recurrence.Pattern.DaysOfWeek = daysOfWeek.ToArray();
                    }
                }
                else
                {
                    // Yearly

                    newItem.Recurrence.Pattern.Interval = (int)comboBox_Recurring_Pattern_Yearly_Interval.SelectedItem;

                    if (radioButton_Recurring_Pattern_Yearly_OnSpecificDay.Checked)
                    {
                        // Absolute Yearly
                        newItem.Recurrence.Pattern.Type = RecurrencePatternType.AbsoluteYearly;
                        newItem.Recurrence.Pattern.DayOfMonth = (int)comboBox_Recurring_Pattern_Yearly_Absolute_Day.SelectedItem;
                        newItem.Recurrence.Pattern.Month = comboBox_Recurring_Pattern_Yearly_Absolute_Month.SelectedIndex + 1;
                    }
                    else
                    {
                        // Relative Yearly
                        newItem.Recurrence.Pattern.Type = RecurrencePatternType.RelativeYearly;
                        newItem.Recurrence.Pattern.Index = (WeekIndex)Enum.Parse(typeof(WeekIndex), comboBox_Recurring_Pattern_Yearly_Relative_DayOfWeekIndex.SelectedItem.ToString());

                        List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

                        if (checkBox_Recurring_Pattern_Yearly_Sunday.Checked) daysOfWeek.Add(DayOfWeek.Sunday);
                        if (checkBox_Recurring_Pattern_Yearly_Monday.Checked) daysOfWeek.Add(DayOfWeek.Monday);
                        if (checkBox_Recurring_Pattern_Yearly_Tuesday.Checked) daysOfWeek.Add(DayOfWeek.Tuesday);
                        if (checkBox_Recurring_Pattern_Yearly_Wednesday.Checked) daysOfWeek.Add(DayOfWeek.Wednesday);
                        if (checkBox_Recurring_Pattern_Yearly_Thursday.Checked) daysOfWeek.Add(DayOfWeek.Thursday);
                        if (checkBox_Recurring_Pattern_Yearly_Friday.Checked) daysOfWeek.Add(DayOfWeek.Friday);
                        if (checkBox_Recurring_Pattern_Yearly_Saturday.Checked) daysOfWeek.Add(DayOfWeek.Saturday);

                        if (daysOfWeek.Count == 0)
                        {
                            throw new Exception("For the Yearly recurring event, select at least one day of the week.");
                        }

                        newItem.Recurrence.Pattern.DaysOfWeek = daysOfWeek.ToArray();

                        newItem.Recurrence.Pattern.Month = comboBox_Recurring_Pattern_Yearly_Relative_Month.SelectedIndex + 1;
                    }
                }

                newItem.Recurrence.Range = new RecurrenceRange();

                newItem.Recurrence.Range.StartDate = new DateTime(
                    dateTimePicker_Recurring_Range_Start.Value.Year,
                    dateTimePicker_Recurring_Range_Start.Value.Month,
                    dateTimePicker_Recurring_Range_Start.Value.Day,
                    0, 0, 0);
                newItem.Recurrence.Range.RecurrenceTimeZone = selectedTimeZone;

                if (radioButton_Recurring_Range_NoEnd.Checked)
                {
                    // No End
                    newItem.Recurrence.Range.Type = RecurrenceRangeType.noEnd;
                }
                else if (radioButton_Recurring_Range_EndAfter.Checked)
                {
                    // Numbered
                    newItem.Recurrence.Range.Type = RecurrenceRangeType.numbered;
                    newItem.Recurrence.Range.NumberOfOccurrences = (int)comboBox_Recurring_Range_NumberOfConcurrences.SelectedItem;
                }
                else
                {
                    // End Date
                    newItem.Recurrence.Range.Type = RecurrenceRangeType.endDate;
                    newItem.Recurrence.Range.EndDate = new DateTime(
                    dateTimePicker_Recurring_Range_EndByDate.Value.Year,
                    dateTimePicker_Recurring_Range_EndByDate.Value.Month,
                    dateTimePicker_Recurring_Range_EndByDate.Value.Day,
                    0, 0, 0);
                }
            }
            else
            {
                newItem.Recurrence = null;
            }

            if (comboBox_OnlineMeeting_Provider.Items.Count > 0 && comboBox_OnlineMeeting_Provider.SelectedItem.ToString().ToLower() != "unknown" && comboBox_OnlineMeeting_Provider.SelectedItem.ToString().ToLower() != "none")
            {
                // This is an online meeting.

                newItem.IsOnlineMeeting = true;
                newItem.OnlineMeetingProvider = comboBox_OnlineMeeting_Provider.SelectedItem.ToString();
            }
            else {
                newItem.IsOnlineMeeting = false;
                newItem.OnlineMeetingProvider = null;
            }

            return newItem;
        }
    }
}