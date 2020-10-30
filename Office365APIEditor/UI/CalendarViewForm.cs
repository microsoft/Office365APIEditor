// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class CalendarViewForm : Form
    {
        FolderInfo targetFolder;
        string targetFolderDisplayName;

        string currentId = "";

        private bool isFormClosing = false;
        private ViewerRequestHelper viewerRequestHelper;

        private int defaultDurationInDay = 7;

        protected CalendarViewForm()
        {
            InitializeComponent();

            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
        }

        public CalendarViewForm(FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();

            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
        }

        private async void CalendarViewForm_LoadAsync(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            Text = "Calendar view of " + targetFolderDisplayName;

            // Display the window in the center of the parent window.
            Location = new Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);

            viewerRequestHelper = new ViewerRequestHelper();

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

            dateTimePicker_StartDate.Value = tempStartDateTime;
            dateTimePicker_StartTime.Value = tempStartDateTime;

            DateTime tempEndDateTime = tempStartDateTime.AddDays(defaultDurationInDay);
            dateTimePicker_EndDate.Value = tempEndDateTime;
            dateTimePicker_EndTime.Value = tempEndDateTime;

            // Add columns.
            PrepareCalendarItemListColumns();

            // Get items.
            await LoadAllEventsAsync();
        }

        private void CalendarViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Change the flag to avoid unnecessary error message.
            isFormClosing = true;
        }

        private void PrepareCalendarItemListColumns()
        {
            if (dataGridView_ItemList.InvokeRequired)
            {
                dataGridView_ItemList.Invoke(new MethodInvoker(delegate
                {
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Subject", HeaderText = "Subject", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Organizer", HeaderText = "Organizer", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Attendees", HeaderText = "Attendees", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "StartUtc", HeaderText = "Start (UTC)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EndUtc", HeaderText = "End (UTC)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "StartLocal", HeaderText = "Start (Local)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EndLocal", HeaderText = "End (Local)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                    dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IsAllDay", HeaderText = "IsAllDay", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 60 });
                }));
            }
            else
            {
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Subject", HeaderText = "Subject", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Organizer", HeaderText = "Organizer", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Attendees", HeaderText = "Attendees", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 100 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "StartUtc", HeaderText = "Start (UTC)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EndUtc", HeaderText = "End (UTC)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "StartLocal", HeaderText = "Start (Local)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EndLocal", HeaderText = "End (Local)", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 120 });
                dataGridView_ItemList.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IsAllDay", HeaderText = "IsAllDay", Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, Frozen = false, MinimumWidth = 60 });
            }
        }

        private async Task<bool> LoadAllEventsAsync()
        {
            UpdateStatusLabel("Loading all items...");

            DateTime startDateTime = new DateTime(
                dateTimePicker_StartDate.Value.Year,
                dateTimePicker_StartDate.Value.Month,
                dateTimePicker_StartDate.Value.Day,
                dateTimePicker_StartTime.Value.Hour,
                dateTimePicker_StartTime.Value.Minute,
                0);

            DateTime endDateTime = new DateTime(
                dateTimePicker_EndDate.Value.Year,
                dateTimePicker_EndDate.Value.Month,
                dateTimePicker_EndDate.Value.Day,
                dateTimePicker_EndTime.Value.Hour,
                dateTimePicker_EndTime.Value.Minute,
                0);

            try
            {
                var events = await viewerRequestHelper.GetCalendarViewFirstPageAsync(targetFolder.ID, startDateTime.ToUniversalTime(), endDateTime.ToUniversalTime());

                do
                {
                    ShowEvents(events.CurrentPage);

                    if (!events.MorePage)
                    {
                        break;
                    }

                    events = await viewerRequestHelper.GetAllEventsPageAsync(events.NextLink);
                } while (events.CurrentPage.Count > 0);

                UpdateStatusLabel("Loaded all items.");

                return true;
            }
            catch(Exception ex)
            {
                Util.ShowErrorWindow(ex, this);
                return false;
            }
        }

        private void UpdateStatusLabel(string Status)
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new MethodInvoker(delegate
                {
                    toolStripStatusLabel_Status.Text = Status;
                }));
            }
            else
            {
                toolStripStatusLabel_Status.Text = Status;
            }            
        }

        private void ShowEvents(List<ViewerHelper.Data.CalendarAPI.Event> events)
        {
            // Show all events in List.

            try
            {
                foreach (var item in events)
                {
                    // Add new row.

                    string subject = item.Subject ?? "";
                    string organizer = (item.Organizer != null && item.Organizer.EmailAddress != null && item.Organizer.EmailAddress.Address != null) ? item.Organizer.EmailAddress.Address : "";
                    string attendees = (item.Attendees != null) ? ConvertAttendeesListToString(item.Attendees) : "";
                    DateTime startUtc = (item.Start != null) ? item.Start.ToUniversalTime() : DateTime.MinValue;
                    DateTime endUtc = (item.End != null) ? item.End.ToUniversalTime() : DateTime.MinValue;
                    DateTime startLocal = startUtc.ToLocalTime();
                    DateTime endLocal = endUtc.ToLocalTime();
                    string isAllDay = (item.IsAllDay != null) ? item.IsAllDay.ToString() : "";

                    DataGridViewRow itemRow = new DataGridViewRow
                    {
                        Tag = item.Id
                    };
                    itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, organizer, attendees, startUtc, endUtc, startLocal, endLocal, isAllDay });
                    itemRow.ContextMenuStrip = contextMenuStrip_ItemList;

                    if (dataGridView_ItemList.InvokeRequired)
                    {
                        dataGridView_ItemList.Invoke(new MethodInvoker(delegate { dataGridView_ItemList.Rows.Add(itemRow); }));
                    }
                    else
                    {
                        dataGridView_ItemList.Rows.Add(itemRow);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                if (isFormClosing)
                {
                    // It seems that this window was closed.
                    // Do nothing.
                }
                else
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().FullName + "\r\n" + ex.Message, "Office365APIEditor");
            }
        }

        private string ConvertAttendeesListToString(IList<Attendee> AttendeesList)
        {
            StringBuilder result = new StringBuilder();

            foreach (Attendee address in AttendeesList)
            {
                result.Append(address.EmailAddress.Address + "; ");
            }

            return result.ToString().Trim(' ', ';');
        }

        private void DataGridView_ItemList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Select the row for the context menu.
            dataGridView_ItemList.Rows[e.RowIndex].Selected = true;
        }

        private async void DataGridView_ItemList_CellClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Get the item ID of clicked row.
            string id = dataGridView_ItemList.Rows[e.RowIndex].Tag.ToString();

            if (currentId == id)
            {
                return;
            }
            else
            {
                currentId = id;
            }

            // Reset rows.
            dataGridView_ItemProps.Rows.Clear();
            foreach (DataGridViewColumn col in dataGridView_ItemProps.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            CreatePropTable(await viewerRequestHelper.GetEventAsync(id));
        }

        private void CreatePropTable(MicrosoftGraphBaseObject OutlookItem)
        {
            var properties = OutlookItem.GetRawProperties();

            try
            {
                foreach (KeyValuePair<string, string> item in properties)
                {
                    DataGridViewRow propRow = new DataGridViewRow();

                    string valueString = (item.Value == null) ? "" : item.Value.ToString();

                    propRow.CreateCells(dataGridView_ItemProps, new object[] { item.Key, valueString, "Dynamic" });

                    if (dataGridView_ItemProps.InvokeRequired)
                    {
                        dataGridView_ItemProps.Invoke(new MethodInvoker(delegate
                        {
                            dataGridView_ItemProps.Rows.Add(propRow);
                        }));
                    }
                    else
                    {
                        dataGridView_ItemProps.Rows.Add(propRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
            }
        }

        private void DataGridView_ItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do nothing.
        }

        private void DataGridView_ItemProps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Get the value of double-clicked row.
            object name = dataGridView_ItemProps.Rows[e.RowIndex].Cells[0].Value;
            string nameString = (name == null) ? "" : name.ToString();

            object value = dataGridView_ItemProps.Rows[e.RowIndex].Cells[1].Value;
            string valueString = (value == null) ? "" : value.ToString();


            PropertyViewerForm propertyViewer = new PropertyViewerForm(nameString, valueString)
            {
                Owner = this
            };
            propertyViewer.Show();
        }

        private async void Button_Refresh_ClickAsync(object sender, EventArgs e)
        {
            // Remove all items.
            dataGridView_ItemList.Rows.Clear();
            dataGridView_ItemProps.Rows.Clear();

            // Reset currentId;
            currentId = "";

            // Get items.
            await LoadAllEventsAsync();
        }

        private void DisplayAttachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayAttachments();
        }

        private void DisplayAttachments()
        {
            if (dataGridView_ItemList.SelectedRows.Count == 0)
            {
                return;
            }

            AttachmentViewerForm attachmentViewer = new AttachmentViewerForm(targetFolder, dataGridView_ItemList.SelectedRows[0].Tag.ToString(), dataGridView_ItemList.SelectedRows[0].Cells[0].Value.ToString());
            attachmentViewer.Show();
        }

        private void DateTimePicker_StartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_StartDate.Value.CompareTo(dateTimePicker_EndDate.Value) > 0)
            {
                // New start date is later then the end date.
                dateTimePicker_EndDate.Value = dateTimePicker_StartDate.Value.AddDays(defaultDurationInDay);

            }
        }
    }
}