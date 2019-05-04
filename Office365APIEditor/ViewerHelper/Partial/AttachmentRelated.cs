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

            client = Util.GetOutlookServicesClient(pca, currentUser);

            List<AttachmentBase> result = new List<AttachmentBase>();

            switch (folderContentType)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    try
                    {
                        var internalResult = await client.Me.Messages[itemId].Attachments
                            .OrderBy(a => a.Name)
                            .Take(1000)
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
                                result.Add(AttachmentBase.CreateFromIdNameContentType(attachment.Id, attachment.Name, attachment.ContentType));
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
                            .Take(1000)
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
                                result.Add(AttachmentBase.CreateFromIdNameContentType(attachment.Id, attachment.Name, attachment.ContentType));
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
                case FolderContentType.DummyCalendarGroupRoot:
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
                stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.Username);
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