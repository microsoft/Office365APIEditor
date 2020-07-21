// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<MailFolder>> GetAllChildMailFoldersAsync(string FolderId)
        {
            // Get all MailFolders in the specified folder.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/mailfolders/{FolderId}/childfolders?$Top=1000&$select=Id,DisplayName,ChildFolderCount") : new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}/childfolders?$Top=1000&$select=Id,DisplayName,ChildFolderCount");

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
                Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/mailfolders/{FolderId}") : new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}");
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

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                try
                {
                    // Top of information store
                    Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/mailfolders/msgfolderroot");
                    string rawJson = await SendGetRequestAsync(URL);
                    var topOfInformationStore = new MailFolder(rawJson);

                    // MsgFolderRoot
                    URL = new Uri($"https://graph.microsoft.com/v1.0/me/mailfolders/{topOfInformationStore.ParentFolderId}/?$select=id,parentFolderId");
                    rawJson = await SendGetRequestAsync(URL);
                    var msgFolderRoot = new MailFolder(rawJson);

                    return msgFolderRoot;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
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
        }

        public async Task<MailFolder> GetDraftsFolderAsync()
        {
            // Get the folder ID of the Drafts.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/v1.0/me/mailfolders/drafts/?$select=id") : new Uri("https://outlook.office.com/api/v2.0/me/mailfolders/drafts/?$select=id");

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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/mailfolders/{FolderId}/messages/?$select=id") : new Uri($"https://outlook.office.com/api/v2.0/me/mailfolders/{FolderId}/messages/?$select=id");

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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/MailFolders/{FolderId}/messages?$orderby=ReceivedDateTime desc&$top=20&$select=Id,Subject,Sender,ToRecipients,ReceivedDateTime,CreatedDateTime,SentDateTime,IsDraft") : new Uri($"https://outlook.office.com/api/v2.0/me/MailFolders/{FolderId}/messages?$orderby=ReceivedDateTime desc&$top=20&$select=Id,Subject,Sender,ToRecipients,ReceivedDateTime,CreatedDateTime,SentDateTime,IsDraft");
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
            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/messages/{ItemId}") : new Uri($"https://outlook.office.com/api/v2.0/me/messages/{ItemId}");
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
                    ToRecipients = new List<Recipient>(draftItem.ToRecipients),
                    CcRecipients = new List<Recipient>(draftItem.CcRecipients),
                    BccRecipients = new List<Recipient>(draftItem.BccRecipients),
                    Subject = draftItem.Subject ?? "",
                    Body = new ItemBody(),
                    Importance = (draftItem.Importance != null) ? (Importance)Enum.Parse(typeof(Importance), draftItem.Importance, true) : Importance.Normal,
                    IsDeliveryReceiptRequested = (draftItem.IsDeliveryReceiptRequested != null && draftItem.IsDeliveryReceiptRequested.HasValue) ? draftItem.IsDeliveryReceiptRequested.Value : false,
                    IsReadReceiptRequested = (draftItem.IsReadReceiptRequested != null && draftItem.IsReadReceiptRequested.HasValue) ? draftItem.IsReadReceiptRequested.Value : false
                };

                newEmailMessage.Body.ContentType = (draftItem.Body != null) ? draftItem.Body.ContentType : BodyType.Text;
                newEmailMessage.Body.Content = (draftItem.Body != null && draftItem.Body.Content != null) ? draftItem.Body.Content : "";

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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/v1.0/Me/SendMail") : new Uri("https://outlook.office.com/api/v2.0/Me/SendMail");

            string postData = CreatePostDataToSendNewMessageOnTheFly(newItem, saveToSentItems);

            try
            {
                string stringResponse = await SendPostRequestAsync(URL, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatePostDataToSendNewMessageOnTheFly(NewEmailMessage newItem, bool saveToSentItems)
        {
            string postData = @"{
    ""Message"": {messageJson},
    ""SaveToSentItems"": ""{SaveToSentItems}""
}";

            string messageJson = CreatePostDataToSave(newItem);

            postData = postData.Replace("{messageJson}", messageJson);

            postData = postData.Replace("{SaveToSentItems}", saveToSentItems.ToString().ToLower());

            return postData;
        }

        public async Task SendMailAsync(string draftItemId)
        {
            // Send a draft mail.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/messages/{draftItemId}/send") : new Uri($"https://outlook.office.com/api/v2.0/me/messages/{draftItemId}/send");

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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/v1.0/me/messages") : new Uri("https://outlook.office.com/api/v2.0/Me/messages");

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

            string result;

            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(NewEmailMessage));
                serializer.WriteObject(stream, newItem);
                result = Encoding.UTF8.GetString(stream.ToArray());
            }

            return result;
        }

        public async Task UpdateDraftAsync(string draftItemId, NewEmailMessage newItem)
        {
            // Update a draft mail.

            try
            {
                // First, update the draft item.

                Uri URL;
                URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/Me/messages/{draftItemId}") : new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}");

                string postData = CreatePostDataToUpdateDraft(newItem);

                // Remove attachment information to reduce the size.
                string postDataWithoutAttachment = Regex.Replace(postData, @"\""attachments\"":\[.*\],", @"""attachments"":[],", RegexOptions.Singleline);

                string result = await SendPatchRequestAsync(URL, postDataWithoutAttachment);

                // Then, remove all attachment once.

                var currentAttachments = await GetAllAttachmentsAsync(FolderContentType.Message, draftItemId);

                foreach (var attach in currentAttachments)
                {
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/Me/messages/{draftItemId}/attachments/{attach.Id}") : new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}/attachments/{attach.Id}");
                    result = await SendDeleteRequestAsync(URL);
                }

                foreach (var attach in newItem.Attachments)
                {
                    if (attach is Data.AttachmentAPI.FileAttachment)
                    {
                        using (var stream = new MemoryStream())
                        {
                            var serializer = new DataContractJsonSerializer(typeof(Data.AttachmentAPI.FileAttachment));
                            serializer.WriteObject(stream, attach);
                            postData = Encoding.UTF8.GetString(stream.ToArray());
                        }
                    }

                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/Me/messages/{draftItemId}/attachments/") : new Uri($"https://outlook.office.com/api/v2.0/Me/messages/{draftItemId}/attachments/");
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

            return CreatePostDataToSave(newItem);
        }

        public async Task<List<FocusedInboxOverride>> GetFocusedInboxOverridesAsync()
        {
            // Get the overrides that a user has set up to always classify messages from certain senders in specific ways.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/v1.0/me/inferenceClassification/overrides") : new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides");

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

            string idAttributeName;
            string classifyAsAttributeName;
            string senderEmailAddressAttributeName;
            string addressAttributeName;
            string nameAttributeNmae;

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                idAttributeName = "id";
                classifyAsAttributeName = "classifyAs";
                senderEmailAddressAttributeName = "senderEmailAddress";
                addressAttributeName = "address";
                nameAttributeNmae = "name";
            }
            else
            {
                idAttributeName = "Id";
                classifyAsAttributeName = "ClassifyAs";
                senderEmailAddressAttributeName = "SenderEmailAddress";
                addressAttributeName = "Address";
                nameAttributeNmae = "Name";
            }

            foreach (var item in messages)
            {
                string id = item.Value<string>(idAttributeName);
                string classifyAs = item.Value<string>(classifyAsAttributeName);

                JObject jObjectSender = item.Value<JObject>(senderEmailAddressAttributeName);
                FocusedInboxOverrideSender sender = new FocusedInboxOverrideSender();

                if (jObjectSender != null && jObjectSender.TryGetValue(addressAttributeName, out JToken jTokenAddress))
                {
                    sender.Address = jTokenAddress.Value<string>();
                }

                if (jObjectSender != null && jObjectSender.TryGetValue(nameAttributeNmae, out JToken jTokenName))
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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/v1.0/me/inferenceClassification/overrides") : new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides");

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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ?
                new Uri("https://graph.microsoft.com/v1.0/me/inferenceClassification/overrides('" + FocusedInboxOverrideId + "')") :
                new Uri("https://outlook.office.com/api/v2.0/me/InferenceClassification/Overrides('" + FocusedInboxOverrideId + "')");

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