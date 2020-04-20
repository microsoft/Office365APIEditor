// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data.AttachmentAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<AttachmentBase>> GetAllAttachmentsAsync(FolderContentType folderContentType, string itemId)
        {
            // Get all attachments of the specified item.
            // The property of the item to get is very limited.

            Uri URL;

            List<AttachmentBase> result = new List<AttachmentBase>();

            switch (folderContentType)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/messages/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType") : new Uri($"https://outlook.office.com/api/v2.0/me/messages/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType");
                    break;
                case FolderContentType.Calendar:
                    URL = Util.UseMicrosoftGraphInMailboxViewer? new Uri($"https://graph.microsoft.com/v1.0/me/events/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType") : new Uri($"https://outlook.office.com/api/v2.0/me/events/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType");
                    break;
                case FolderContentType.Task:
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/beta/me/outlook/tasks/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType") : new Uri($"https://outlook.office.com/api/v2.0/me/tasks/{itemId}/attachments/?$Top=1000&$select=Id,Name,ContentType");
                    break;
                case FolderContentType.Contact:
                    // contact item (Contact API) does not have attachment.
                default:
                    return new List<AttachmentBase>();
            }

            try
            {
                string stringResponse = await SendGetRequestAsync(URL);

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var attachments = (JArray)jsonResponse.GetValue("value");

                foreach (var item in attachments)
                {
                    var serializedObject = new AttachmentBase(JsonConvert.SerializeObject(item));
                    result.Add(serializedObject);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/beta/me/messages/" + itemId + "/attachments/" + attachmentId) : new Uri("https://outlook.office.com/api/v2.0/me/messages/" + itemId + "/attachments/" + attachmentId);
                    break;
                case FolderContentType.Calendar:
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/beta/me/events/" + itemId + "/attachments/" + attachmentId) : new Uri("https://outlook.office.com/api/v2.0/me/events/" + itemId + "/attachments/" + attachmentId);
                    break;
                case FolderContentType.Task:
                    URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri("https://graph.microsoft.com/beta/me/outlook/tasks/" + itemId + "/attachments/" + attachmentId) : new Uri("https://outlook.office.com/api/v2.0/me/tasks/" + itemId + "/attachments/" + attachmentId);
                    break;
                default:
                    throw new Exception("FolderContentType must be Message, MsgFolderRoot, Drafts, Calendar or Task.");
            }

            string stringResponse = "";

            try
            {
                stringResponse = await SendGetRequestAsync(URL);
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

        public async Task<string> GetAttachmentRawContentAsync(FolderContentType folderContentType, string itemId, string attachmentId)
        {
            // Get the raw contents of the specified attachment.

            if (!Util.UseMicrosoftGraphInMailboxViewer)
            {
                throw new NotImplementedException();
            }

            Uri URL;

            switch (folderContentType)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    URL = new Uri("https://graph.microsoft.com/v1.0/me/messages/" + itemId + "/attachments/" + attachmentId + "/$value");
                    break;
                case FolderContentType.Calendar:
                    URL = new Uri("https://graph.microsoft.com/v1.0/me/events/" + itemId + "/attachments/" + attachmentId + "/$value");
                    break;
                case FolderContentType.Task:
                    URL = new Uri("https://graph.microsoft.com/beta/me/outlook/tasks/" + itemId + "/attachments/" + attachmentId + "/$value");
                    break;
                default:
                    throw new Exception("FolderContentType must be Message, MsgFolderRoot, Drafts, Calendar or Task.");
            }

            string stringResponse = "";

            try
            {
                stringResponse = await SendGetRequestAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stringResponse;
        }
    }
}