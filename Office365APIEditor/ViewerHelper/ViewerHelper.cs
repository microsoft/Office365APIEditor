// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.Office365.OutlookServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            
            Uri URL = new Uri(@"https://outlook.office.com/api/v2.0/me/MailFolders/" + FolderId + "/messages?$orderby=ReceivedDateTime desc&$top=500&$select=Id,Subject,Sender,ToRecipients,ReceivedDateTime,CreatedDateTime,SentDateTime");
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
                    DateTimeOffset? CreatedDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("CreatedDateTime"));
                    DateTimeOffset? SentDateTime = ConvertDateTimeToDateTimeOffset(item.Value<DateTime>("SentDateTime"));

                    result.Add(new MessageSummary()
                    {
                        Id = id,
                        Subject = subject,
                        Sender = sender,
                        ToRecipients = toRecipients,
                        ReceivedDateTime = receivedDateTime,
                        CreatedDateTime = CreatedDateTime,
                        SentDateTime = SentDateTime
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
    }
}
