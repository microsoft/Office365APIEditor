// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

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

        AuthenticationResult ar;
        string email;

        string currentId = "";

        public FolderViewerForm(PublicClientApplication PCA, string UserEmailAddress, FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();
            
            pca = PCA;
            email = UserEmailAddress;
            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
        }

        private async void FolderViewerForm_Load(object sender, System.EventArgs e)
        {
            Text = targetFolder.Type.ToString() + " items in " + targetFolderDisplayName;

            client = await GetOutlookServiceClient();
            
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
                    if (await GetMessageItems() == false)
                    {
                        if (MessageBox.Show("TotalItemCount of this folder is not 0 but getting items of this folder was failed.\r\nDo you want to retry getting items as Contact item?", "Office365APIEditor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Retry as Contact item.

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
                case FolderContentType.DummyCalendarRoot:
                default:
                    break;
            }
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

        private async Task<OutlookServicesClient> GetOutlookServiceClient()
        {
            // Acquire access token again.
            try
            {
                ar = await pca.AcquireTokenSilentAsync(Office365APIEditorHelper.MailboxViewerScopes());
            }
            catch
            {
                try
                {
                    ar = await pca.AcquireTokenAsync(Office365APIEditorHelper.MailboxViewerScopes(), email, UiOptions.ForceLogin, "");
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Office365APIEditor");

                    return null;
                }
            }

            string token = ar.Token;

            OutlookServicesClient newClient = new OutlookServicesClient(new Uri("https://outlook.office.com/api/v2.0"),
                () =>
                {
                    return Task.Run(() =>
                    {
                        return token;
                    });
                });

            newClient.Context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(
                (eventSender, eventArgs) => InsertHeaders(eventSender, eventArgs, email));
            
            return newClient;
        }

        private void InsertHeaders(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
            e.RequestMessage.SetHeader("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");
        }

        private async Task<bool> GetMessageItems()
        {
            return await GetMessageItems(targetFolder.ID);
        }

        private async Task<bool> GetMessageItems(string TargetFolderID)
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
                        string subject = (item.Subject != null) ? item.Subject : "";
                        string sender = (item.Sender != null && item.Sender.EmailAddress != null && item.Sender.EmailAddress.Address != null) ? item.Sender.EmailAddress.Address : "";
                        string recipients = (item.ToRecipients != null) ? ConvertRecipientsListToString(item.ToRecipients) : "";

                        DataGridViewRow itemRow = new DataGridViewRow();
                        itemRow.Tag = item.Id;
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, sender, recipients, receivedDateTime, createdDateTime, sentDateTime });

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
                if (ex.Message == "No row can be added to a DataGridView control that does not have columns. Columns must be added first.")
                {
                    // Because we added columns first, it seems that this window was closed.
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

                        string displayName = (item.DisplayName != null) ? item.DisplayName : "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";

                        DataGridViewRow itemRow = new DataGridViewRow();
                        itemRow.Tag = item.Id;
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { displayName, createdDateTime });

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

                        string subject = (item.Subject != null) ? item.Subject : "";
                        string organizer = (item.Organizer != null && item.Organizer.EmailAddress != null && item.Organizer.EmailAddress.Address != null) ? item.Organizer.EmailAddress.Address : "";
                        string attendees = (item.Attendees != null) ? ConvertAttendeesListToString(item.Attendees) : "";
                        string start = (item.Start != null) ? item.Start.DateTime : "";
                        string end = (item.End != null) ? item.End.DateTime : "";
                        string isAllDay = (item.IsAllDay != null) ? item.IsAllDay.ToString() : "";
                        string createdDateTime = (item.CreatedDateTime != null) ? item.CreatedDateTime.Value.ToString("yyyy/MM/dd HH:mm/ss") : "";

                        DataGridViewRow itemRow = new DataGridViewRow();
                        itemRow.Tag = item.Id;
                        itemRow.CreateCells(dataGridView_ItemList, new object[] { subject, organizer, attendees, start, end, isAllDay, createdDateTime });

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

        private async void dataGridView_ItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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

            client = await GetOutlookServiceClient();

            // Reset rows.
            dataGridView_ItemProps.Rows.Clear();

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
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

        private async void GetMessageItemDetail(string ID)
        {
            // Get details of the item.
            var results = await client.Me.Messages[ID].ExecuteAsync();

            CreatePropTable(results);
        }

        private async void GetContactItemDetail(string ID)
        {
            // Get details of the contact item.
            var results = await client.Me.Contacts[ID].ExecuteAsync();

            CreatePropTable(results);
        }

        private async void GetCalendarItemDetail(string ID)
        {
            // Get details of the contact item.
            var results = await client.Me.Events[ID].ExecuteAsync();

            CreatePropTable(results);
        }

        private void CreatePropTable(object itemResult)
        {
            try
            {
                foreach (var prop in itemResult.GetType().GetProperties())
                {
                    DataGridViewRow propRow = new DataGridViewRow();

                    string propName = prop.Name;
                    string propValue = "";
                    string propType = "";

                    if (prop.GetIndexParameters().Length == 0)
                    {
                        try
                        {
                            var tempValue = prop.GetValue(itemResult);
                            propType = tempValue.GetType().ToString();

                            if (tempValue is DateTimeOffset)
                            {
                                DateTimeOffset dateTimeOffsetValue = (DateTimeOffset)tempValue;
                                propValue = dateTimeOffsetValue.DateTime.ToString("yyyy/MM/dd HH:mm:ss");
                            }
                            else if (tempValue is ItemBody)
                            {
                                ItemBody itemBodyValue = (ItemBody)tempValue;
                                propValue = itemBodyValue.Content;
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Recipient>)
                            {
                                Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Recipient> recipientValue = (Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Recipient>)tempValue;

                                foreach (Recipient recipient in recipientValue)
                                {
                                    propValue += recipient.EmailAddress.Name + "<" + recipient.EmailAddress.Address + ">; ";
                                }

                                propValue = propValue.TrimEnd(new char[] { ';', ' ' });
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.EntityCollectionImpl<Attachment>)
                            {
                                // To get the list of attachments, we have to send a request again.
                                // We don't do that and prepare an attachment view.

                                continue;
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<string>)
                            {
                                Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<string> stringValue = (Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<string>)tempValue;

                                foreach (string value in stringValue)
                                {
                                    propValue += value + "; ";
                                }

                                propValue = propValue.TrimEnd(new char[] { ';', ' ' });
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<EmailAddress>)
                            {
                                Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<EmailAddress> emailAddressValue = (Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<EmailAddress>)tempValue;

                                foreach (EmailAddress emailAddress in emailAddressValue)
                                {
                                    propValue += emailAddress.Name + "<" + emailAddress.Address + ">; ";
                                }

                                propValue = propValue.TrimEnd(new char[] { ';', ' ' });
                            }
                            else if (tempValue is PhysicalAddress)
                            {
                                PhysicalAddress physicalAddressValue = (PhysicalAddress)tempValue;
                                propValue = physicalAddressValue.PostalCode + " " + physicalAddressValue.Street + " " + physicalAddressValue.City + " " + physicalAddressValue.State + " " + physicalAddressValue.CountryOrRegion;
                            }
                            else if (tempValue is ResponseStatus)
                            {
                                ResponseStatus responseStatusValue = (ResponseStatus)tempValue;
                                
                                if (responseStatusValue.Time.HasValue)
                                {
                                    propValue = responseStatusValue.Time.Value.DateTime.ToString("yyyy/MM/dd HH:mm:ss") + " ";
                                }

                                propValue += responseStatusValue.Response.ToString();
                            }
                            else if (tempValue is DateTimeTimeZone)
                            {
                                DateTimeTimeZone dateTimeTimeZoneValue = (DateTimeTimeZone)tempValue;
                                propValue = dateTimeTimeZoneValue.TimeZone + " " + dateTimeTimeZoneValue.DateTime;
                            }
                            else if (tempValue is Location)
                            {
                                Location locationValue = (Location)tempValue;
                                propValue = locationValue.DisplayName;
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Attendee>)
                            {
                                Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Attendee> attendeeValue = (Microsoft.OData.ProxyExtensions.NonEntityTypeCollectionImpl<Attendee>)tempValue;

                                foreach (Recipient attendee in attendeeValue)
                                {
                                    propValue += attendee.EmailAddress.Name + "<" + attendee.EmailAddress.Address + ">; ";
                                }

                                propValue = propValue.TrimEnd(new char[] { ';', ' ' });
                            }
                            else if (tempValue is Recipient)
                            {
                                Recipient recipientValue = (Recipient)tempValue;
                                propValue = recipientValue.EmailAddress.Name + "<" + recipientValue.EmailAddress.Address + ">";
                            }
                            else if (tempValue is Microsoft.OData.ProxyExtensions.EntityCollectionImpl<Event>)
                            {
                                // I'm not sure what this prop is.
                                // This prop has a Nested structure.
                                continue;
                            }
                            else
                            {
                                propValue = tempValue.ToString();
                            }
                        }
                        catch
                        {
                            propValue = "";
                        }
                    }
                    else
                    {
                        propValue = "indexed";
                    }

                    propRow.CreateCells(dataGridView_ItemProps, new object[] { propName, propValue, propType });

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
            // Get the item ID of clicked row.
            string id = dataGridView_ItemList.Rows[e.RowIndex].Tag.ToString();

            MessageBox.Show(e.RowIndex.ToString(id) + " d");
        }
    }
}
