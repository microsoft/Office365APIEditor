// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.ContactsAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<ContactFolder>> GetAllChildContactFoldersAsync(string FolderId)
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
                        var newContactFolder = ContactFolder.CreateFromId(folder.Id);
                        newContactFolder.DisplayName = folder.DisplayName;

                        result.Add(newContactFolder);
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

        public async Task<ContactFolder> GetContactFolderAsync(string FolderId)
        {
            // Get the specified ContactFolder

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

                Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/contactfolders/{FolderId}");
                string rawJson = await Util.SendGetRequestAsync(URL, accessToken, currentUser.Username);
                var contactFolder = new ContactFolder(rawJson);

                return contactFolder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<Contact>> GetAllContactsFirstPageAsync(string FolderId)
        {
            // Get all contact items in the specified folder.
            // The property of the item to get is very limited.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/ContactFolders/{FolderId}/contacts?$orderby=CreatedDateTime desc&$top=20&$select=Id,DisplayName,CreatedDateTime");
            return await InternalGetPagedContactsAsync(URL);
        }

        public async Task<PagedResponse<Contact>> GetAllContactsPageAsync(string NextLink)
        {
            // Get all contact items usingthe specified NextLink.
            // The property of the item to get is very limited.

            Uri URL = new Uri(NextLink);
            return await InternalGetPagedContactsAsync(URL);
        }

        private async Task<PagedResponse<Contact>> InternalGetPagedContactsAsync(Uri URL)
        {
            string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

            var result = new PagedResponse<Contact>
            {
                CurrentPage = new List<Contact>()
            };

            try
            {
                // Get a response and response stream.
                string stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.Username);

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var contacts = (JArray)jsonResponse.GetValue("value");

                foreach (var item in contacts)
                {
                    var serializedObject = new Contact(JsonConvert.SerializeObject(item));
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

        public async Task<Contact> GetContactAsync(string ItemId)
        {
            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/contacts/{ItemId}");

            string accessToken = await Util.GetAccessTokenAsync(Global.pca, Global.currentUser);
            string stringResponse = await Util.SendGetRequestAsync(URL, accessToken, Global.currentUser.Username);
            return new Contact(stringResponse);
        }
    }
}