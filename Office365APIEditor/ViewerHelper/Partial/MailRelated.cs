// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<MailFolder>> GetAllChildMailFoldersAsync(string FolderId)
        {
            // Get all MailFolders in the specified folder.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}/childfolders?$Top=1000&$select=Id,DisplayName,ChildFolderCount");

            List<MailFolder> result = new List<MailFolder>();

            try
            {
                string stringResponse = await SendGetRequestAsync(URL);

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var folders = (JArray)jsonResponse.GetValue("value");

                foreach (var item in folders)
                {
                    var serializedObject = new MailFolder(JsonConvert.SerializeObject(item));
                    result.Add(serializedObject);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<MailFolder> GetMailFolderAsync(string FolderId)
        {
            // Get the specified MailFolder

            try
            {
                Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}");
                string rawJson = await SendGetRequestAsync(URL);
                var mailFolder = new MailFolder(rawJson);

                return mailFolder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MailFolder> GetMsgFolderRootAsync()
        {
            // Get the folder ID of the parent folder of parent folder of Inbox.
            // It's MsgFolderRoot.

            // We can't get the Top of Information Store folder directly.
            // Following operation is available with v1.0 only.
            // https://outlook.office.com/api/v1.0/me/RootFolder

            try
            {
                // Inbox
                Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/mailfolders/inbox/?$select=id,parentFolderId");
                string rawJson = await SendGetRequestAsync(URL);
                var inbox = new MailFolder(rawJson);

                // Top of information store
                URL = new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{inbox.ParentFolderId}/?$select=id,parentFolderId");
                rawJson = await SendGetRequestAsync(URL);
                var topOfInformationStore = new MailFolder(rawJson);

                // MsgFolderRoot
                URL = new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{topOfInformationStore.ParentFolderId}/?$select=id,parentFolderId");
                rawJson = await SendGetRequestAsync(URL);
                var msgFolderRoot = new MailFolder(rawJson);

                return msgFolderRoot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MailFolder> GetDraftsFolderAsync()
        {
            // Get the folder ID of the Drafts.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/mailfolders/drafts/?$select=id");

            string stringResponse = "";

            try
            {
                stringResponse = await SendGetRequestAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new MailFolder(stringResponse);
        }

        public async Task<bool> HasActualMessageItemAsync(string FolderId)
        {
            // Check whether the specified folder has message items.
            // Some folders has only contact items.

            // Get the folder ID of the Drafts.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}/messages/?$select=id");

            string stringResponse = "";

            bool result = false;

            try
            {
                stringResponse = await SendGetRequestAsync(URL);

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var messages = (JArray)jsonResponse.GetValue("value");


                if (messages.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        
        public async Task<PagedResponse<Message>> GetAllMessagesFirstPageAsync(string FolderId)
        {
            // Get the first page of message items in the specified folder.
            // The property of the item to get is very limited.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/MailFolders/{FolderId}/messages?$orderby=ReceivedDateTime desc&$top=20&$select=Id,Subject,Sender,ToRecipients,ReceivedDateTime,CreatedDateTime,SentDateTime,IsDraft");
            return await InternalGetPagedMessagesAsync(URL);
        }

        public async Task<PagedResponse<Message>> GetAllMessagesPageAsync(string NextLink)
        {
            // Get a page of message items using the specified NextLink.
            // The property of the item to get is very limited.

            Uri URL = new Uri(NextLink);
            return await InternalGetPagedMessagesAsync(URL);
        }

        private async Task<PagedResponse<Message>> InternalGetPagedMessagesAsync(Uri URL)
        {
            var result = new PagedResponse<Message>
            {
                CurrentPage = new List<Message>()
            };

            try
            {
                // Get a response and response stream.
                string stringResponse = await SendGetRequestAsync(URL);

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var messages = (JArray)jsonResponse.GetValue("value");

                foreach (var item in messages)
                {
                    var serializedObject = new Message(JsonConvert.SerializeObject(item));
                    result.CurrentPage.Add(serializedObject);
                }

                var nextLink = (JValue)jsonResponse.GetValue("@odata.nextLink");
                if (nextLink != null)
                {
                    result.MorePage = true;
                    result.NextLink = nextLink.Value.ToString();
                }
                else
                {
                    result.MorePage = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Message> GetMessageAsync(string ItemId)
        {
            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/messages/{ItemId}");
            string stringResponse = await SendGetRequestAsync(URL);
            return new Message(stringResponse);
        }

        public async Task<NewEmailMessage> GetDraftMessageAsync(string draftItemId)
        {
            // Get the specified item as a draft item to send.

            try
            {
                var draftItem = await GetMessageAsync(draftItemId);

                NewEmailMessage newEmailMessage = new NewEmailMessage
                {
                    ToRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.ToRecipients),
                    CcRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.CcRecipients),
                    BccRecipients = ConvertRecipientIListToMailAddressCollection(draftItem.BccRecipients),
                    Subject = draftItem.Subject ?? "",
                    BodyType = (draftItem.Body != null) ? (BodyType)Enum.Parse(typeof(BodyType), draftItem.Body.ContentType, true) : BodyType.Text,
                    Body = (draftItem.Body != null && draftItem.Body.Content != null) ? draftItem.Body.Content : "",
                    Importance = (draftItem.Importance != null) ? (Importance)Enum.Parse(typeof(Importance), draftItem.Importance, true) : Importance.Normal,
                    RequestDeliveryReceipt = (draftItem.IsDeliveryReceiptRequested != null && draftItem.IsDeliveryReceiptRequested.HasValue) ? draftItem.IsDeliveryReceiptRequested.Value : false,
                    RequestReadReceipt = (draftItem.IsReadReceiptRequested != null && draftItem.IsReadReceiptRequested.HasValue) ? draftItem.IsReadReceiptRequested.Value : false
                };

                return newEmailMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendMailAsync(NewEmailMessage newItem, bool saveToSentItems)
        {
            // Send a mail.
            // SendMailAsync method of NuGet version OutlookServicesClient is not working, so we don't use that.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/Me/SendMail");

            string postData = CreatePostDataToSendNewMessageOnTheFly(newItem);

            try
            {
                string stringResponse = await SendPostRequestAsync(URL, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatePostDataToSendNewMessageOnTheFly(NewEmailMessage newItem)
        {
            // Create POST data for sending a new message on the fly.

            string postData = @"{
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
    ""SaveToSentItems"": ""{SaveToSentItems}""
}";

            postData = CreateMessageBase(newItem, postData);

            postData = postData.Replace("{SaveToSentItems}", newItem.SaveToSentItems.ToString().ToLower());

            return postData;
        }

        public async Task SendMailAsync(string draftItemId)
        {
            // Send a draft mail.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/messages/{draftItemId}/send");

            try
            {
                string stringResponse = await SendPostRequestAsync(URL, "");
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

            string postData = CreatePostDataToSave(newItem);

            try
            {
                string stringResponse = await SendPostRequestAsync(URL, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatePostDataToSave(NewEmailMessage newItem)
        {
            // Create POST data for sending or saving new item from NewEmailMessage.

            string postData = @"{
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

            postData = CreateMessageBase(newItem, postData);

            return postData;
        }

        public async Task UpdateDraftAsync(string draftItemId, NewEmailMessage newItem)
        {
            // Update a draft mail.

            try
            {
                // First, update the draft item.

                Uri URL;
                URL = new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}");

                string postData = CreatePostDataToUpdateDraft(newItem);

                string result = await SendPatchRequestAsync(URL, postData);

                // Then, remove all attachment once.

                var currentAttachments = await GetAllAttachmentsAsync(FolderContentType.Message, draftItemId);

                foreach (var attach in currentAttachments)
                {
                    URL = new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}/attachments/{attach.Id}");
                    result = await SendDeleteRequestAsync(URL);
                }

                // Finally, upload new attachments.

                string attachmentTemplate = @"{
            ""@odata.type"": ""#Microsoft.OutlookServices.FileAttachment"",
            ""Name"": ""{FileName}"",
            ""ContentBytes"": ""{ContentBytes}""
        }";

                foreach (var attach in newItem.Attachments)
                {
                    if (attach is Data.AttachmentAPI.FileAttachment)
                    {
                        postData = attachmentTemplate.Replace("{FileName}", attach.Name);
                        postData = postData.Replace("{ContentBytes}", ((Data.AttachmentAPI.FileAttachment)attach).ContentBytes);
                    }

                    URL = new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}/attachments/");
                    result = await SendPostRequestAsync(URL, postData);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatePostDataToUpdateDraft(NewEmailMessage newItem)
        {
            // Create POST data for sending or saving new item from NewEmailMessage.

            string postData = @"{
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
        ""Categories"": []
    }";

            postData = CreateMessageBase(newItem, postData);

            return postData;
        }

        private string CreateMessageBase(NewEmailMessage newItem, string basePostData)
        {
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

                basePostData = basePostData.Replace("{ToRecipients}", recipients);
            }
            else
            {
                basePostData = basePostData.Replace("{ToRecipients}", "");
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

                basePostData = basePostData.Replace("{CcRecipients}", recipients);
            }
            else
            {
                basePostData = basePostData.Replace("{CcRecipients}", "");
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

                basePostData = basePostData.Replace("{BccRecipients}", recipients);
            }
            else
            {
                basePostData = basePostData.Replace("{BccRecipients}", "");
            }

            basePostData = basePostData.Replace("{Subject}", Util.EscapeForJson(newItem.Subject));

            basePostData = basePostData.Replace("{ContentType}", newItem.BodyType.ToString());

            basePostData = basePostData.Replace("{Content}", Util.EscapeForJson(newItem.Body));

            basePostData = basePostData.Replace("{Importance}", newItem.Importance.ToString());

            basePostData = basePostData.Replace("{IsDeliveryReceiptRequested}", newItem.RequestDeliveryReceipt.ToString().ToLower());

            basePostData = basePostData.Replace("{IsReadReceiptRequested}", newItem.RequestReadReceipt.ToString().ToLower());

            if (newItem.Attachments != null && newItem.Attachments.Count != 0)
            {
                List<string> attachmentList = new List<string>();
                foreach (var attachment in newItem.Attachments)
                {
                    if (attachment is Data.AttachmentAPI.FileAttachment)
                    {
                        string newAttachment = attachmentTemplate.Replace("{FileName}", attachment.Name).Replace("{ContentBytes}", ((Data.AttachmentAPI.FileAttachment)attachment).ContentBytes);
                        attachmentList.Add(newAttachment);
                    }
                }

                string attachments = string.Join(", ", attachmentList);

                basePostData = basePostData.Replace("{Attachments}", attachments);
            }
            else
            {
                basePostData = basePostData.Replace("{Attachments}", "");
            }

            return basePostData;
        }

        public async Task<List<FocusedInboxOverride>> GetFocusedInboxOverridesAsync()
        {
            // Get the overrides that a user has set up to always classify messages from certain senders in specific ways.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides");

            string stringResponse = "";

            try
            {
                stringResponse = await SendGetRequestAsync(URL);
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
                string stringResponse = await SendPostRequestAsync(URL, postData);
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
                string stringResponse = await SendDeleteRequestAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}