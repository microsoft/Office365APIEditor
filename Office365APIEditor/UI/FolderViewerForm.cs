// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Codeplex.Data;
using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class FolderViewerForm : Form
    {
        PublicClientApplication pca;
        FolderInfo targetFolder;
        string targetFolderDisplayName;

        OutlookServicesClient client;

        Microsoft.Identity.Client.IUser currentUser;

        string currentId = "";

        private bool isFormClosing = false;

        public FolderViewerForm(PublicClientApplication PCA, Microsoft.Identity.Client.IUser CurrentUser, FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();
            
            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
        }

        private async void FolderViewerForm_Load(object sender, System.EventArgs e)
        {
            if (targetFolder.Type != FolderContentType.MsgFolderRoot)
            {
                Text = targetFolder.Type.ToString() + " items in " + targetFolderDisplayName;
            }
            else
            {
                Text = FolderContentType.Message.ToString() + " items in " + targetFolderDisplayName;
            }
            
            client = await Util.GetOutlookServicesClientAsync(pca, currentUser);

            if (client == null)
            {
                MessageBox.Show("Authentication failure.", "Office365APIEditor");
            }

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
                    // Add columns.
                    PrepareMessageItemListColumns();

                    // Get items.
                    if (await GetMessageItemsAsync() == false)
                    {
                        if (MessageBox.Show("TotalItemCount of this folder is not 0 but getting items of this folder was failed.\r\nDo you want to retry getting items as Contact item?", "Office365APIEditor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Retry as Contact item.

                            targetFolder.Type = FolderContentType.Contact;

                            // Reset DataGrid.
                            if (dataGridView_ItemList.InvokeRequired)
                            {
                                dataGridView_ItemList.Invoke(new MethodInvoker(delegate
                                {
                                    for (int i = dataGridView_ItemList.Columns.Count - 1; i >= 0; i--)
                                    {
                                        dataGridView_ItemList.Columns.RemoveAt(i);
                                    }
                                }));
                            }
                            else
                            {
                                for (int i = dataGridView_ItemList.Columns.Count - 1; i >= 0; i--)
                                {
                                    dataGridView_ItemList.Columns.RemoveAt(i);
                                }
                            }

                            PrepareContactItemListColumns();
                            GetContactItems();
                        }
                    }

                    break;
                case FolderContentType.Contact:
                    // Add columns.
                    PrepareContactItemListColumns();
                    
                    // Get items.
                    GetContactItems();

                    break;
                case FolderContentType.Calendar:
                    // Add columns.
                    PrepareCalendarItemListColumns();

                    GetCalendarItems();

                    break;
                case FolderContentType.MsgFolderRoot:
                    // Add columns.
                    PrepareMessageItemListColumns();

                    // Get items.
                    GetMessageItemsInMsgFolderRoot();

                    break;
                case FolderContentType.DummyCalendarRoot:
                default:
                    break;
            }
        }

        private void FolderViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Change the flag to avoid unnecessary error message.
            isFormClosing = true;
        }

        private void PrepareMessageItemListColumns()
        {
            if (dataGridView_ItemList.InvokeRequired)
            {
                dataGridView_ItemList.Invoke(new MethodInvoker(delegate
                {
                    dataGridView_ItemList.Columns.Add("Subject", "Subject");
                    dataGridView_ItemList.Columns.Add("Sender", "Sender");
                    dataGridView_ItemList.Columns.Add("ToRecipients", "ToRecipients");
                    dataGridView_ItemList.Columns.Add("ReceivedDateTime", "ReceivedDateTime");
                    dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
                    dataGridView_ItemList.Columns.Add("SentDateTime", "SentDateTime");
                }));
            }
            else
            {
                dataGridView_ItemList.Columns.Add("Subject", "Subject");
                dataGridView_ItemList.Columns.Add("Sender", "Sender");
                dataGridView_ItemList.Columns.Add("ToRecipients", "ToRecipients");
                dataGridView_ItemList.Columns.Add("ReceivedDateTime", "ReceivedDateTime");
                dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
                dataGridView_ItemList.Columns.Add("SentDateTime", "SentDateTime");
            }
        }

        private void PrepareContactItemListColumns()
        {
            if (dataGridView_ItemList.InvokeRequired)
            {
                dataGridView_ItemList.Invoke(new MethodInvoker(delegate
                {
                    dataGridView_ItemList.Columns.Add("DisplayName", "DisplayName");
                    dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
                }));
            }
            else
            {
                dataGridView_ItemList.Columns.Add("DisplayName", "DisplayName");
                dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
            }
        }

        private void PrepareCalendarItemListColumns()
        {
            if (dataGridView_ItemList.InvokeRequired)
            {
                dataGridView_ItemList.Invoke(new MethodInvoker(delegate
                {
                    dataGridView_ItemList.Columns.Add("Subject", "Subject");
                    dataGridView_ItemList.Columns.Add("Organizer", "Organizer");
                    dataGridView_ItemList.Columns.Add("Attendees", "Attendees");
                    dataGridView_ItemList.Columns.Add("Start", "Start");
                    dataGridView_ItemList.Columns.Add("End", "End");
                    dataGridView_ItemList.Columns.Add("IsAllDay", "IsAllDay");
                    dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
                }));
            }
            else
            {
                dataGridView_ItemList.Columns.Add("Subject", "Subject");
                dataGridView_ItemList.Columns.Add("Organizer", "Organizer");
                dataGridView_ItemList.Columns.Add("Attendees", "Attendees");
                dataGridView_ItemList.Columns.Add("Start", "Start");
                dataGridView_ItemList.Columns.Add("End", "End");
                dataGridView_ItemList.Columns.Add("IsAllDay", "IsAllDay");
                dataGridView_ItemList.Columns.Add("CreatedDateTime", "CreatedDateTime");
            }
        }

        private void InsertHeaders(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
            e.RequestMessage.SetHeader("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");
        }

        // GetMessageItems for MsgFolderRoot
        private async void GetMessageItemsInMsgFolderRoot()
        {
            // I don't know why but items in MsgFolderRoot doesn't have Sender prop.

            string TargetFolderID = targetFolder.ID;

            try
            {
                var results = await client.Me.MailFolders[TargetFolderID].Messages
                    .OrderByDescending(m => m.ReceivedDateTime)
                    .Take(50)
                    .Select(m => new { m.Id, m.Subject, /*m.Sender, */m.ToRecipients, m.ReceivedDateTime, m.CreatedDateTime, m.SentDateTime })
                    .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var item in results.CurrentPage)
                    {
                        // Add new row.
                        string receivedDateTime = (item.ReceivedDateTime != null) ? item.ReceivedDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";
                        string sentDateTime = (item.SentDateTime != null) ? item.SentDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                        string subject = item.Subject ?? "";
                        string sender = ""; // (item.Sender != null && item.Sender.EmailAddress != null && item.Sender.EmailAddress.Address != null) ? item.Sender.EmailAddress.Address : "";
                        string recipients = (item.ToRecipients != null) ? ConvertRecipientsListToString(item.ToRecipients) : "";

                        DataGridViewRow itemRow = new DataGridViewRow
                        {
                            Tag = item.Id
                        };
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, sender, recipients, receivedDateTime, createdDateTime, sentDateTime });
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

                    if (results.MorePagesAvailable)
                    {
                        morePages = true;
                        results = await results.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);
            }
            catch (DataServiceClientException ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
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
                // TODO: 途中で画面を閉じて Row を追加できない場合。
            }
        }

        private async Task<bool> GetMessageItemsAsync()
        {
            return await GetMessageItemsAsync(targetFolder.ID);
        }

        // GetMessageItems for the non-root folders.
        private async Task<bool> GetMessageItemsAsync(string TargetFolderID)
        {
            // Return false if TotalItemCount is not 0 and we could not get items.
            bool succeed = false;

            try
            {
                var results = await client.Me.MailFolders[TargetFolderID].Messages
                    .OrderByDescending(m => m.ReceivedDateTime)
                    .Take(50)
                    .Select(m => new { m.Id, m.Subject, m.Sender, m.ToRecipients, m.ReceivedDateTime, m.CreatedDateTime, m.SentDateTime })
                    .ExecuteAsync();

                if (results.CurrentPage.Count == 0)
                {
                    // No items in this folder.

                    // Check whether TotalItemCount is 0.

                    IMailFolder mailFolderResults = new MailFolder();

                    try
                    {
                        mailFolderResults = await client.Me.MailFolders[TargetFolderID].ExecuteAsync();
                    }
                    catch (Microsoft.OData.Core.ODataErrorException ex)
                    {
                        // We know that we can't get RSS Feeds folder.
                        // But we can get the folder using DisplayName Filter.

                        if (ex.Error.ErrorCode == "ErrorItemNotFound")
                        {
                            var tempResults = await client.Me.MailFolders
                                .Where(m => m.DisplayName == targetFolderDisplayName)
                                .Take(2)
                                .ExecuteAsync();

                            if (tempResults.CurrentPage.Count != 1)
                            {
                                // We have to get a unique folder.
                                MessageBox.Show(ex.Message, "Office365APIEditor");
                                return true;
                            }

                            mailFolderResults = tempResults.CurrentPage[0];
                        }
                        else
                        {
                            MessageBox.Show(ex.Error.ErrorCode);
                            return true;
                        }
                    }

                    if (mailFolderResults.TotalItemCount == 0)
                    {
                        return true;
                    }
                    else
                    {
                        // TotalItemCount is not 0 but we could not get items.
                        return false;
                    }
                }

                bool morePages = false;

                do
                {
                    foreach (var item in results.CurrentPage)
                    {
                        // Add new row.
                        string receivedDateTime = (item.ReceivedDateTime != null) ? item.ReceivedDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";
                        string sentDateTime = (item.SentDateTime != null) ? item.SentDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                        string subject = item.Subject ?? "";
                        string sender = (item.Sender != null && item.Sender.EmailAddress != null && item.Sender.EmailAddress.Address != null) ? item.Sender.EmailAddress.Address : "";
                        string recipients = (item.ToRecipients != null) ? ConvertRecipientsListToString(item.ToRecipients) : "";

                        DataGridViewRow itemRow = new DataGridViewRow
                        {
                            Tag = item.Id
                        };
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, sender, recipients, receivedDateTime, createdDateTime, sentDateTime });
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

                    if (results.MorePagesAvailable)
                    {
                        morePages = true;
                        results = await results.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                succeed = true;
            }
            catch (DataServiceClientException ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                succeed = true;
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
                
                succeed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().FullName + "\r\n" + ex.Message, "Office365APIEditor");
                succeed = true;

                // TODO: 途中で画面を閉じて Row を追加できない場合。
            }

            return succeed;
        }
        private async void GetContactItems()
        {
            try
            {
                var results = await client.Me.ContactFolders[targetFolder.ID].Contacts
                    .OrderByDescending(c => c.CreatedDateTime)
                    .Take(50)
                    .Select(c => new { c.Id, c.DisplayName, c.CreatedDateTime })
                    .ExecuteAsync();

                if (results.CurrentPage.Count == 0)
                {
                    // No items in this folder.
                    return;
                }

                bool morePages = false;
                
                do
                {
                    foreach (var item in results.CurrentPage)
                    {
                        // Add new row.

                        string displayName = item.DisplayName ?? "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";

                        DataGridViewRow itemRow = new DataGridViewRow
                        {
                            Tag = item.Id
                        };
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { displayName, createdDateTime });
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

                    if (results.MorePagesAvailable)
                    {
                        morePages = true;
                        results = await results.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }
        }

        private async void GetCalendarItems()
        {
            try
            {
                var results = await client.Me.Calendars[targetFolder.ID].Events
                    .OrderByDescending(e => e.CreatedDateTime)
                    .Take(50)
                    .Select(e => new { e.Id, e.Subject, e.Organizer, e.Attendees, e.Start, e.End, e.IsAllDay, e.CreatedDateTime})
                    .ExecuteAsync();

                if (results.CurrentPage.Count == 0)
                {
                    // No items in this folder.
                    return;
                }

                bool morePages = false;

                do
                {
                    foreach (var item in results.CurrentPage)
                    {
                        // Add new row.

                        string subject = item.Subject ?? "";
                        string organizer = (item.Organizer != null && item.Organizer.EmailAddress != null && item.Organizer.EmailAddress.Address != null) ? item.Organizer.EmailAddress.Address : "";
                        string attendees = (item.Attendees != null) ? ConvertAttendeesListToString(item.Attendees) : "";
                        string start = (item.Start != null) ? item.Start.DateTime : "";
                        string end = (item.End != null) ? item.End.DateTime : "";
                        string isAllDay = (item.IsAllDay != null) ? item.IsAllDay.ToString() : "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";

                        DataGridViewRow itemRow = new DataGridViewRow
                        {
                            Tag = item.Id
                        };
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, organizer, attendees, start, end, isAllDay, createdDateTime });
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

                    if (results.MorePagesAvailable)
                    {
                        morePages = true;
                        results = await results.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }
        }

        private string ConvertRecipientsListToString(IList<Recipient> RecipientsList)
        {
            StringBuilder result = new StringBuilder();

            foreach (Recipient address in RecipientsList)
            {
                result.Append(address.EmailAddress.Address + "; ");
            }

            return result.ToString().Trim(' ', ';');
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

        private void dataGridView_ItemList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Select the row for the context menu.
            dataGridView_ItemList.Rows[e.RowIndex].Selected = true;
        }

        private void dataGridView_ItemList_CellClick(object sender, DataGridViewCellEventArgs e)
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

            // We don't need to get new OutlookServiceClient because we don't use that.
            // client = await GetOutlookServiceClient();

            // Reset rows.
            dataGridView_ItemProps.Rows.Clear();
            foreach (DataGridViewColumn col in dataGridView_ItemProps.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                    GetMessageItemDetail(id);
                    break;
                case FolderContentType.Contact:
                    GetContactItemDetail(id);
                    break;
                case FolderContentType.Calendar:
                    GetCalendarItemDetail(id);
                    break;
                case FolderContentType.DummyCalendarRoot:
                    break;
                default:
                    break;
            }
        }

        private void GetMessageItemDetail(string ID)
        {
            // Get details of the message item.
            GetItemDetail(new Uri("https://outlook.office.com/api/v2.0/me/messages/" + ID));
        }

        private void GetContactItemDetail(string ID)
        {
            // Get details of the contact item.
            GetItemDetail(new Uri("https://outlook.office.com/api/v2.0/me/contacts/" + ID));
        }

        private void GetCalendarItemDetail(string ID)
        {
            // Get details of the contact item.
            GetItemDetail(new Uri("https://outlook.office.com/api/v2.0/me/events/" + ID));
        }

        private async void GetItemDetail(Uri URL)
        {
            // We know that we can use OutlookServicesClient but this library doesn't include InferenceClassification and other new props.

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string result = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
                var jsonResult = DynamicJson.Parse(result);

                CreatePropTable2(jsonResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
            }
        }

        private void CreatePropTable2(dynamic itemResult)
        {
            DynamicJson dynamicJsonObject = itemResult;
            
            try
            {
                foreach (KeyValuePair<string, object> item in itemResult)
                {
                    if (!dynamicJsonObject.IsDefined(item.Key))
                    {
                        // Exclude non dynamic props.
                        continue;
                    }

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

        private void dataGridView_ItemList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do nothing.
        }

        private void contextMenuStrip_ItemList_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (dataGridView_ItemList.SelectedRows.Count == 0);
        }

        private void ToolStripMenuItem_DisplayAttachments_Click(object sender, EventArgs e)
        {
            if (dataGridView_ItemList.SelectedRows.Count == 0)
            {
                return;
            }

            AttachmentViewerForm attachmentViewer = new AttachmentViewerForm(pca, currentUser, targetFolder, dataGridView_ItemList.SelectedRows[0].Tag.ToString(), dataGridView_ItemList.SelectedRows[0].Cells[0].Value.ToString());
            attachmentViewer.Show();
        }

        private void dataGridView_ItemProps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
    }
}
