// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.Office365.OutlookServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Attachments;
using Office365APIEditor.ViewerHelper.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    class ViewerHelper
    {
        private PublicClientApplication pca;
        private Microsoft.Identity.Client.IUser currentUser;
        OutlookServicesClient client;

        public ViewerHelper(PublicClientApplication PCA, Microsoft.Identity.Client.IUser CurrentUser)
        {
            pca = PCA;
            currentUser = CurrentUser;

            // TODO : Implement logging feature
            // Maybe in Util, not here.
            //client.Context.BuildingRequest += new EventHandler<BuildingRequestEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
            //client.Context.ReceivingResponse += new EventHandler<ReceivingResponseEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
        }

        private void RequestLogger(object eventSender, ReceivingResponseEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ResponseMessage.ToString();
        }

        private void RequestLogger(object eventSender, BuildingRequestEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ToString();
        }

        public async Task<IMailFolder> GetMsgFolderRootAsync()
        {
            // Get the folder ID of the parent folder of parent folder of Inbox.
            // It's MsgFolderRoot.

            // We can't get the Top of Information Store folder directly.
            // Following operation is available with v1.0 only.
            // https://outlook.office.com/api/v1.0/me/RootFolder

            client = Util.GetOutlookServicesClient(pca, currentUser);

            try
            {
                var inbox = await client.Me.MailFolders["Inbox"].ExecuteAsync(); // Inbox
                var topOfInformationStore = await client.Me.MailFolders[inbox.ParentFolderId].ExecuteAsync(); // Top of information store
                var msgFolderRoot = await client.Me.MailFolders[topOfInformationStore.ParentFolderId].ExecuteAsync(); // MsgFolderRoot

                return msgFolderRoot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NewEmailMessage> GetDraftMessageAsync(string draftItemId)
        {
            // Get the specified item as a draft item to send.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            try
            {
                var draftItem = await client.Me.Messages[draftItemId].ExecuteAsync();

                NewEmailMessage newEmailMessage = new NewEmailMessage();

                newEmailMessage.ToRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.ToRecipients);
                newEmailMessage.CcRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.CcRecipients);
                newEmailMessage.BccRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.BccRecipients);
                newEmailMessage.Subject = draftItem.Subject ?? "";
                newEmailMessage.BodyType = (draftItem.Body != null) ? (BodyType)Enum.ToObject(typeof(BodyType), (int)draftItem.Body.ContentType) : BodyType.Text;
                newEmailMessage.Body = (draftItem.Body != null && draftItem.Body.Content != null) ? draftItem.Body.Content : "";
                newEmailMessage.Importance = (Importance)Enum.ToObject(typeof(Importance), (int)draftItem.Importance);
                newEmailMessage.RequestDeliveryReceipt = draftItem.IsDeliveryReceiptRequested ?? false;
                newEmailMessage.RequestReadReceipt = draftItem.IsReadReceiptRequested ?? false;

                return newEmailMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MailAddressCollection ConvertRecipientIListToMailAddressCollection(IList<Recipient> recipients)
        {
            MailAddressCollection result = new MailAddressCollection();

            foreach (var recipient in recipients)
            {
                result.Add(new MailAddress(recipient.EmailAddress.Address, recipient.EmailAddress.Name));
            }

            return result;
        }

        public async Task<IMailFolder> GetDraftsFolderAsync()
        {
            // Get the folder ID of the Drafts.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            try
            {
                var drafts = await client.Me.MailFolders["Drafts"].ExecuteAsync();
                
                return drafts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Calendar>> GetCalendarFoldersAsync()
        {
            // Get all calendar folders in the mailbox.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<Calendar> result = new List<Calendar>();

            try
            {
                var calendarFolderResults = await client.Me.Calendars
                .OrderBy(c => c.Name)
                .Take(100)
                .Select(c => new { c.Id, c.Name })
                .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in calendarFolderResults.CurrentPage)
                    {
                        result.Add(new Calendar() { Id = folder.Id, Name = folder.Name });
                    }

                    if (calendarFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        calendarFolderResults = await calendarFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MailFolder>> GetAllChildMailFolderAsync(string FolderId)
        {
            // Get all MailFolders in the specified folder.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<MailFolder> result = new List<MailFolder>();

            try
            {
                var childMailFolderResults = await client.Me.MailFolders[FolderId].ChildFolders
                    .OrderBy(m => m.DisplayName)
                    .Take(100)
                    .Select(m => new { m.Id, m.DisplayName, m.ChildFolderCount })
                    .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in childMailFolderResults.CurrentPage)
                    {
                        result.Add(new MailFolder() { Id = folder.Id, DisplayName = folder.DisplayName, ChildFolderCount = folder.ChildFolderCount });
                    }

                    if (childMailFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        childMailFolderResults = await childMailFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContactFolder>> GetAllChildContactFolderAsync(string FolderId)
        {
            // Get all contact folders in the specified folder.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<ContactFolder> result = new List<ContactFolder>();

            try
            {
                var childContactFolderResults = await client.Me.ContactFolders[FolderId].ChildFolders
                    .OrderBy(f => f.DisplayName)
                    .Take(100)
                    .Select(f => new { f.Id, f.DisplayName })
                    .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in childContactFolderResults.CurrentPage)
                    {
                        result.Add(new ContactFolder() { Id = folder.Id, DisplayName = folder.DisplayName });
                    }

                    if (childContactFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        childContactFolderResults = await childContactFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IMailFolder> GetMailFolderAsync(string FolderId, string FolderDisplayName)
        {
            // Get the specified MailFolder

            client = Util.GetOutlookServicesClient(pca, currentUser);

            IMailFolder mailFolderResult;

            try
            {
                mailFolderResult = await client.Me.MailFolders[FolderId].ExecuteAsync();
            }
            catch (Microsoft.OData.Core.ODataErrorException ex)
            {
                // We know that we can't get RSS Feeds folder.
                // But we can get the folder using DisplayName Filter.

                if (ex.Error.ErrorCode == "ErrorItemNotFound")
                {
                    var tempResults = await client.Me.MailFolders
                        .Where(m => m.DisplayName == FolderDisplayName)
                        .Take(2)
                        .ExecuteAsync();

                    if (tempResults.CurrentPage.Count != 1)
                    {
                        // We have to get a unique folder.
                        throw ex;
                    }

                    mailFolderResult = tempResults.CurrentPage[0];
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mailFolderResult;
        }

        public async Task<IContactFolder> GetContactFolderAsync(string FolderId)
        {
            // Get the specified ContactFolder

            client = Util.GetOutlookServicesClient(pca, currentUser);

            IContactFolder contactFolderResult;

            try
            {
                contactFolderResult = await client.Me.ContactFolders[FolderId].ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contactFolderResult;
        }

        public async Task<ICalendar> GetCalendarAsync(string FolderId)
        {
            // Get the specified Calendar

            client = Util.GetOutlookServicesClient(pca, currentUser);

            ICalendar calendarResult;

            try
            {
                calendarResult = await client.Me.Calendars[FolderId].ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return calendarResult;
        }

        public async Task<List<MessageSummary>> GetMessageSummaryAsync(string FolderId)
        {
            // Get all message items in the specified folder.
            // The property of the item to get is very limited.

            // OutlookServicesClient will get an exception if message doesn't have Sender property.
            // So, we don't use OutlookServicesClient.
            
            Uri URL = new Uri(@"https://outlook.office.com/api/v2.0/me/MailFolders/" + FolderId + "/messages?$orderby=ReceivedDateTime desc&$top=500&$select=Id,Subject,Sender,ToRecipients,ReceivedDateTime,CreatedDateTime,SentDateTime,IsDraft");
            string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

            List<MessageSummary> result = new List<MessageSummary>();

            PagedResponse<MessageSummary> internalResult = await InternalGetMessageSummaryAsync(URL, accessToken);

            bool morePage = false;

            do
            {
                result.AddRange(internalResult.CurrentPage);

                if (internalResult.MorePage)
                {
                    morePage = true;
                    internalResult = await InternalGetMessageSummaryAsync(new Uri(internalResult.NextLink), accessToken);
                }
                else
                {
                    morePage = false;
                }
            } while (morePage);

            return result;
        }

        private async Task<PagedResponse<MessageSummary>> InternalGetMessageSummaryAsync(Uri URL, string accessToken)
        {
            // TODO : Implement Logging feature
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization:Bearer " + accessToken);
            request.Headers.Add("X-AnchorMailbox:" + currentUser);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");
            request.Method = "GET";

            List<MessageSummary> result = new List<MessageSummary>();

            try
            {
                // Get a response and response stream.
                var httpWebResponse = (HttpWebResponse)await request.GetResponseAsync();

                string stringResponse = "";
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    stringResponse = reader.ReadToEnd();
                }

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var messages = (JArray)jsonResponse.GetValue("value");

                foreach (var item in messages)
                {
                    string id = item.Value<string>("Id");
                    string subject = item.Value<string>("Subject");

                    JObject jObjectSender = item.Value<JObject>("Sender");
                    Recipient sender = new Recipient();

                    if (jObjectSender != null && jObjectSender.TryGetValue("EmailAddress", out JToken jTokenEmailAddress))
                    {
                        sender.EmailAddress = new EmailAddress() { Address = jTokenEmailAddress.Value<string>("Address"), Name = jTokenEmailAddress.Value<string>("Name") };
                    }

                    var jArrayToRecipients = (JArray)item.Value<JArray>("ToRecipients");
                    List<Recipient> toRecipients = new List<Recipient>();

                    if (jArrayToRecipients != null)
                    {
                        foreach (JObject toRecipient in jArrayToRecipients)
                        {
                            if (toRecipient.TryGetValue("EmailAddress", out jTokenEmailAddress))
                            {
                                Recipient newToRecipient = new Recipient
                                {
                                    EmailAddress = new EmailAddress() { Address = jTokenEmailAddress.Value<string>("Address"), Name = jTokenEmailAddress.Value<string>("Name") }
                                };

                                toRecipients.Add(newToRecipient);
                            }
                        }
                    }                   

                    DateTimeOffset? receivedDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("ReceivedDateTime"));
                    DateTimeOffset? createdDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("CreatedDateTime"));
                    DateTimeOffset? sentDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("SentDateTime"));
                    bool isDraft = item.Value<bool>("IsDraft");
                    
                    result.Add(new MessageSummary()
                    {
                        Id = id,
                        Subject = subject,
                        Sender = sender,
                        ToRecipients = toRecipients,
                        ReceivedDateTime = receivedDateTime,
                        CreatedDateTime = createdDateTime,
                        SentDateTime = sentDateTime,
                        IsDraft = isDraft
                    });
                }

                PagedResponse<MessageSummary> pagedResponse = new PagedResponse<MessageSummary>();

                var nextLink = (JValue)jsonResponse.GetValue("@odata.nextLink");
                if (nextLink != null)
                {
                    pagedResponse.MorePage = true;
                    pagedResponse.NextLink = nextLink.Value.ToString();
                }
                else
                {
                    pagedResponse.MorePage = false;
                }

                pagedResponse.CurrentPage = result;

                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContactSummary>> GetContactSummaryAsync(string FolderId)
        {
            // Get all contact items in the specified folder.
            // The property of the item to get is very limited.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<ContactSummary> result = new List<ContactSummary>();

            try
            {
                var internalResult = await client.Me.ContactFolders[FolderId].Contacts
                    .OrderByDescending(c => c.CreatedDateTime)
                    .Take(500)
                    .Select(c => new { c.Id, c.DisplayName, c.CreatedDateTime })
                    .ExecuteAsync();

                if (internalResult.CurrentPage.Count == 0)
                {
                    // No items in this folder.
                    return result;
                }

                bool morePages = false;

                do
                {
                    foreach (var item in internalResult.CurrentPage)
                    {
                        result.Add(new ContactSummary()
                        {
                            Id = item.Id,
                            DisplayName = item.DisplayName,
                            CreatedDateTime = item.CreatedDateTime
                        });
                    }

                    if (internalResult.MorePagesAvailable)
                    {
                        morePages = true;
                        internalResult = await internalResult.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public async Task<List<EventSummary>> GetEventSummaryAsync(string FolderId)
        {
            // Get all contact items in the specified folder.
            // The property of the item to get is very limited.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<EventSummary> result = new List<EventSummary>();

            try
            {
                var internalResults = await client.Me.Calendars[FolderId].Events
                    .OrderByDescending(e => e.CreatedDateTime)
                    .Take(500)
                    .Select(e => new { e.Id, e.Subject, e.Organizer, e.Attendees, e.Start, e.End, e.IsAllDay, e.CreatedDateTime })
                    .ExecuteAsync();

                if (internalResults.CurrentPage.Count == 0)
                {
                    // No items in this folder.
                    return result;
                }

                bool morePages = false;

                do
                {
                    foreach (var item in internalResults.CurrentPage)
                    {
                        result.Add(new EventSummary()
                        {
                            Id = item.Id,
                            Subject = item.Subject,
                            Organizer = item.Organizer,
                            Attendees = item.Attendees,
                            Start = item.Start,
                            End = item.End,
                            IsAllDay = item.IsAllDay,
                            CreatedDateTime = item.CreatedDateTime
                        });
                    }

                    if (internalResults.MorePagesAvailable)
                    {
                        morePages = true;
                        internalResults = await internalResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private DateTimeOffset? ConvertDateTimeToDateTimeOffset(DateTime dateTimeValue)
        {
            try
            {
                if (dateTimeValue == null)
                {
                    return null;
                }
                else
                {
                    if (dateTimeValue.Kind == DateTimeKind.Utc || dateTimeValue.Kind == DateTimeKind.Local)
                    {
                        return new DateTimeOffset(dateTimeValue);
                    }
                    else 
                    {
                        return new DateTimeOffset(dateTimeValue, new TimeSpan(0));
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AttachmentSummary>> GetAttachmentsAsync(FolderContentType folderContentType, string itemId)
        {
            // Get all attachments of the specified item.
            // The property of the item to get is very limited.

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<AttachmentSummary> result = new List<AttachmentSummary>();

            switch (folderContentType)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    try
                    {
                        var internalResult = await client.Me.Messages[itemId].Attachments
                            .OrderBy(a => a.Name)
                            .Take(500)
                            .Select(a => new { a.Id, a.Name, a.ContentType })
                            .ExecuteAsync();

                        if (internalResult.CurrentPage.Count == 0)
                        {
                            // No attachments for this item.
                            return result;
                        }

                        bool morePages = false;

                        do
                        {
                            foreach (var attachment in internalResult.CurrentPage)
                            {
                                result.Add(new AttachmentSummary()
                                {
                                    Id = attachment.Id,
                                    Name = attachment.Name,
                                    ContentType = attachment.ContentType
                                });
                            }

                            if (internalResult.MorePagesAvailable)
                            {
                                morePages = true;
                                internalResult = await internalResult.GetNextPageAsync();
                            }
                            else
                            {
                                morePages = false;
                            }
                        } while (morePages);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case FolderContentType.Contact:
                    // In the implementation of OutlookServicesClient, contact item does not have attachment.

                    break;
                case FolderContentType.Calendar:
                    try
                    {
                        var internalResult = await client.Me.Events[itemId].Attachments
                            .OrderBy(a => a.Name)
                            .Take(500)
                            .Select(a => new { a.Id, a.Name, a.ContentType })
                            .ExecuteAsync();

                        if (internalResult.CurrentPage.Count == 0)
                        {
                            // No attachments for this item.
                            return result;
                        }

                        bool morePages = false;

                        do
                        {
                            foreach (var attachment in internalResult.CurrentPage)
                            {
                                result.Add(new AttachmentSummary()
                                {
                                    Id = attachment.Id,
                                    Name = attachment.Name,
                                    ContentType = attachment.ContentType
                                });
                            }

                            if (internalResult.MorePagesAvailable)
                            {
                                morePages = true;
                                internalResult = await internalResult.GetNextPageAsync();
                            }
                            else
                            {
                                morePages = false;
                            }
                        } while (morePages);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case FolderContentType.DummyCalendarRoot:
                    break;
                default:
                    break;
            }

            return result;
        }

        public async Task<Dictionary<string, string>> GetAttachmentAsync(FolderContentType folderContentType, string itemId, string attachmentId)
        {
            // Get the specified item.

            Uri URL;

            switch (folderContentType)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    URL = new Uri("https://outlook.office.com/api/v2.0/me/messages/" + itemId + "/attachments/" + attachmentId);
                    break;
                case FolderContentType.Calendar:
                    URL = new Uri("https://outlook.office.com/api/v2.0/me/events/" + itemId + "/attachments/" + attachmentId);
                    break;
                default:
                    throw new Exception("FolderContentType must be Message, MsgFolderRoot or Calendar.");
            }

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var item in jsonResponse)
            {
                result.Add(item.Key, item.Value.Value<string>());
            }

            return result;
        }

        public async Task SendMailAsync(NewEmailMessage newItem, bool saveToSentItems) {
            // Send a mail.
            // SendMailAsync method of NuGet version OutlookServicesClient is not working, so we don't use that.
            
            Uri URL = new Uri("https://outlook.office.com/api/v2.0/Me/SendMail");

            string postData = CreateNewItemPostData(newItem, false);

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendPostRequestAsync(URL, accessToken, currentUser.DisplayableId, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task SendMailAsync(string draftItemId)
        {
            // Send a draft mail.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/messages/" + draftItemId + "/send");

            string postData = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendPostRequestAsync(URL, accessToken, currentUser.DisplayableId, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveDraftAsync(NewEmailMessage newItem)
        {
            // Save a new draft mail.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/Me/messages");

            string postData = CreateNewItemPostData(newItem, true);

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendPostRequestAsync(URL, accessToken, currentUser.DisplayableId, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateDraftAsync(string draftItemId, NewEmailMessage newItem)
        {
            // Update a new draft mail.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/Me/messages/" + draftItemId);

            string postData = CreateNewItemPostData(newItem, true);

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendPatchRequestAsync(URL, accessToken, currentUser.DisplayableId, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreateNewItemPostData(NewEmailMessage newItem, bool isDraft)
        {
            // Create POST data for sending or saving new item from NewEmailMessage.

            string postData = "";

            if (isDraft == false)
            {
                postData = @"{
    ""Message"": {
        ""ReplyTo"": [],
        ""ToRecipients"": [{ToRecipients}],
        ""CcRecipients"": [{CcRecipients}],
        ""BccRecipients"": [{BccRecipients}],
        ""Subject"": ""{Subject}"",
        ""Body"": {
            ""ContentType"": ""{ContentType}"",
            ""Content"": ""{Content}""
        },
        ""Importance"": ""{Importance}"",
        ""IsDeliveryReceiptRequested"": {IsDeliveryReceiptRequested},
        ""IsReadReceiptRequested"": {IsReadReceiptRequested},
        ""Categories"": [],
        ""Attachments"": [{Attachments}]
    },
    ""SaveToSentItems"": true
}";
            }
            else
            {
                 postData = @"{
        ""ReplyTo"": [],
        ""ToRecipients"": [{ToRecipients}],
        ""CcRecipients"": [{CcRecipients}],
        ""BccRecipients"": [{BccRecipients}],
        ""Subject"": ""{Subject}"",
        ""Body"": {
            ""ContentType"": ""{ContentType}"",
            ""Content"": ""{Content}""
        },
        ""Importance"": ""{Importance}"",
        ""IsDeliveryReceiptRequested"": {IsDeliveryReceiptRequested},
        ""IsReadReceiptRequested"": {IsReadReceiptRequested},
        ""Categories"": [],
        ""Attachments"": [{Attachments}]
    }";
            }

            string emailAddressTemplate = @"{
            ""EmailAddress"": {
                ""Address"": ""{Address}"",
                ""Name"": ""{DisplayName}""
            }
        }";

            string attachmentTemplate = @"{
            ""@odata.type"": ""#Microsoft.OutlookServices.FileAttachment"",
            ""Name"": ""{FileName}"",
            ""ContentBytes"": ""{ContentBytes}""
        }";

            if (newItem.ToRecipients != null && newItem.ToRecipients.Count != 0)
            {
                List<string> recipientList = new List<string>();
                foreach (var recipient in newItem.ToRecipients)
                {
                    string newResipient = emailAddressTemplate.Replace("{Address}", recipient.Address).Replace("{DisplayName}", Util.EscapeForJson(recipient.DisplayName));
                    recipientList.Add(newResipient);
                }

                string recipients = string.Join(", ", recipientList);

                postData = postData.Replace("{ToRecipients}", recipients);
            }
            else
            {
                postData = postData.Replace("{ToRecipients}", "");
            }

            if (newItem.CcRecipients != null && newItem.CcRecipients.Count != 0)
            {
                List<string> recipientList = new List<string>();
                foreach (var recipient in newItem.CcRecipients)
                {
                    string newResipient = emailAddressTemplate.Replace("{Address}", recipient.Address).Replace("{DisplayName}", Util.EscapeForJson(recipient.DisplayName));
                    recipientList.Add(newResipient);
                }

                string recipients = string.Join(", ", recipientList);

                postData = postData.Replace("{CcRecipients}", recipients);
            }
            else
            {
                postData = postData.Replace("{CcRecipients}", "");
            }

            if (newItem.BccRecipients != null && newItem.BccRecipients.Count != 0)
            {
                List<string> recipientList = new List<string>();
                foreach (var recipient in newItem.BccRecipients)
                {
                    string newResipient = emailAddressTemplate.Replace("{Address}", recipient.Address).Replace("{DisplayName}", Util.EscapeForJson(recipient.DisplayName));
                    recipientList.Add(newResipient);
                }

                string recipients = string.Join(", ", recipientList);

                postData = postData.Replace("{BccRecipients}", recipients);
            }
            else
            {
                postData = postData.Replace("{BccRecipients}", "");
            }

            postData = postData.Replace("{Subject}", Util.EscapeForJson(newItem.Subject));

            postData = postData.Replace("{ContentType}", newItem.BodyType.ToString());

            postData = postData.Replace("{Content}", Util.EscapeForJson(newItem.Body));

            postData = postData.Replace("{Importance}", newItem.Importance.ToString());

            postData = postData.Replace("{IsDeliveryReceiptRequested}", newItem.RequestDeliveryReceipt.ToString().ToLower());

            postData = postData.Replace("{IsReadReceiptRequested}", newItem.RequestReadReceipt.ToString().ToLower());

            if (newItem.Attachments != null && newItem.Attachments.Count != 0)
            {
                List<string> attachmentList = new List<string>();
                foreach (var attachment in newItem.Attachments)
                {
                    string newAttachment = attachmentTemplate.Replace("{FileName}", attachment.Name).Replace("{ContentBytes}", attachment.ContentBytes);
                    attachmentList.Add(newAttachment);
                }

                string attachments = string.Join(", ", attachmentList);

                postData = postData.Replace("{Attachments}", attachments);
            }
            else
            {
                postData = postData.Replace("{Attachments}", "");
            }

            postData = postData.Replace("{SaveToSentItems}", newItem.SaveToSentItems.ToString().ToLower());

            return postData;
        }



        public async Task<List<FocusedInboxOverride>> GetFocusedInboxOverridesAsync()
        {
            // Get the overrides that a user has set up to always classify messages from certain senders in specific ways.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides");

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var messages = (JArray)jsonResponse.GetValue("value");

            List<FocusedInboxOverride> result = new List<FocusedInboxOverride>();

            foreach (var item in messages)
            {
                string id = item.Value<string>("Id");
                string classifyAs = item.Value<string>("ClassifyAs");

                JObject jObjectSender = item.Value<JObject>("SenderEmailAddress");
                FocusedInboxOverrideSender sender = new FocusedInboxOverrideSender();

                if (jObjectSender != null && jObjectSender.TryGetValue("Address", out JToken jTokenAddress))
                {
                    sender.Address = jTokenAddress.Value<string>();
                }

                if (jObjectSender != null && jObjectSender.TryGetValue("Name", out JToken jTokenName))
                {
                    sender.Name = jTokenName.Value<string>();
                }

                result.Add(new FocusedInboxOverride
                {
                    Id = id,
                    ClassifyAs = (Classify)Enum.Parse(typeof(Classify), classifyAs, true),
                    SenderEmailAddress = sender
                });
            }

            return result;
        }

        public async Task AddOrUpdateFocusedInboxOverrideAsync(FocusedInboxOverride newOverride)
        {
            // Add or update a new focused inbox override setting.

            // PATCH request can update only ClassifyAs, and POST request can update both ClassifyAs and display name.
            // So, we use POST request when updating.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides");

            string postData = @"{
    ""ClassifyAs"": ""{Classify}"",
    ""SenderEmailAddress"": {
        ""Name"": ""{Name}"",
        ""Address"": ""{Address}""
    }
}";

            postData = postData.Replace("{Classify}", newOverride.ClassifyAs.ToString());

            postData = postData.Replace("{Name}", newOverride.SenderEmailAddress.Name);

            postData = postData.Replace("{Address}", newOverride.SenderEmailAddress.Address);

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendPostRequestAsync(URL, accessToken, currentUser.DisplayableId, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveFocusedInboxOverrideAsync(string FocusedInboxOverrideId)
        {
            // Remove a focused inbox override setting.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides('" + FocusedInboxOverrideId + "')");

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                string stringResponse = await Util.SendDeleteRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TaskGroup>> GetAllTaskGroupAsync()
        {
            // Get all TaskGroups.
            // The property of the item to get is very limited.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/taskgroups?$select=Id,Name");

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var taskGroups = (JArray)jsonResponse.GetValue("value");

            List<TaskGroup> result = new List<TaskGroup>();

            foreach (var group in taskGroups)
            {
                string id = group.Value<string>("Id");
                string name = group.Value<string>("Name");
                
                result.Add(new TaskGroup
                {
                    Id = id,
                    Name = name,
                });
            }

            return result;
        }

        public async Task<TaskGroup> GetTaskGroupAsync(string TaskGroupId)
        {
            // Get the specified TaskGroup.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/taskgroups/" + TaskGroupId);

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Convert JSON response.
            return JsonConvert.DeserializeObject<TaskGroup>(stringResponse);
        }

        public async Task<List<TaskFolder>> GetAllTaskFoldersAsync(string TaskGroupId)
        {
            // Get all TaskFolder in the specified TaskGroup.
            // The property of the item to get is very limited.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/taskgroups/" + TaskGroupId + "/taskfolders?$select=Id,Name");

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var taskFolders = (JArray)jsonResponse.GetValue("value");

            List<TaskFolder> result = new List<TaskFolder>();

            foreach (var folder in taskFolders)
            {
                string id = folder.Value<string>("Id");
                string name = folder.Value<string>("Name");

                result.Add(new TaskFolder
                {
                    Id = id,
                    Name = name,
                });
            }

            return result;
        }

        public async Task<TaskFolder> GetTaskFolderAsync(string TaskFolderId)
        {
            // Get the specified TaskFolder.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/taskfolders/" + TaskFolderId);

            string stringResponse = "";

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.DisplayableId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Convert JSON response.
            return JsonConvert.DeserializeObject<TaskFolder>(stringResponse);
        }

        internal async Task<List<TaskSummary>> GetTaskSummaryAsync(string FolderId)
        {
            // Get all task items in the specified folder.
            // The property of the item to get is very limited.

            Uri URL = new Uri(@"https://outlook.office.com/api/v2.0/me/TaskFolders/" + FolderId + "/tasks?$orderby=CreatedDateTime desc&$top=500&$select=Subject,HasAttachments,CreatedDateTime,LastModifiedDateTime,Status");
            string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

            List<TaskSummary> result = new List<TaskSummary>();

            PagedResponse<TaskSummary> internalResult = await InternalGetTaskSummaryAsync(URL, accessToken);

            bool morePage = false;

            do
            {
                result.AddRange(internalResult.CurrentPage);

                if (internalResult.MorePage)
                {
                    morePage = true;
                    internalResult = await InternalGetTaskSummaryAsync(new Uri(internalResult.NextLink), accessToken);
                }
                else
                {
                    morePage = false;
                }
            } while (morePage);

            return result;
        }

        private async Task<PagedResponse<TaskSummary>> InternalGetTaskSummaryAsync(Uri URL, string accessToken)
        {
            // TODO : Implement Logging feature
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization:Bearer " + accessToken);
            request.Headers.Add("X-AnchorMailbox:" + currentUser);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");
            request.Method = "GET";

            List<TaskSummary> result = new List<TaskSummary>();

            try
            {
                // Get a response and response stream.
                var httpWebResponse = (HttpWebResponse)await request.GetResponseAsync();

                string stringResponse = "";
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    stringResponse = reader.ReadToEnd();
                }

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var messages = (JArray)jsonResponse.GetValue("value");

                foreach (var item in messages)
                {
                    string id = item.Value<string>("Id");
                    string subject = item.Value<string>("Subject");
                    bool hasAttachments = item.Value<bool>("HasAttachments");
                    DateTimeOffset? createdDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("CreatedDateTime"));
                    DateTimeOffset? lastModifiedDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("LastModifiedDateTime"));
                    string status = item.Value<string>("Status");

                    result.Add(new TaskSummary()
                    {
                        Id = id,
                        Subject = subject,
                        HasAttachments = hasAttachments,
                        CreatedDateTime = createdDateTime,
                        LastModifiedDateTime = lastModifiedDateTime,
                        Status = status
                    });
                }

                PagedResponse<TaskSummary> pagedResponse = new PagedResponse<TaskSummary>();

                var nextLink = (JValue)jsonResponse.GetValue("@odata.nextLink");
                if (nextLink != null)
                {
                    pagedResponse.MorePage = true;
                    pagedResponse.NextLink = nextLink.Value.ToString();
                }
                else
                {
                    pagedResponse.MorePage = false;
                }

                pagedResponse.CurrentPage = result;

                return pagedResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}