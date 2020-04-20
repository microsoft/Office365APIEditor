// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.TaskAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<TaskGroup>> GetAllTaskGroupsAsync()
        {
            // Get all TaskGroups.
            // The property of the item to get is very limited.

            Uri URL;
            string idAttributeName;
            string nameAttributeName;

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                URL = new Uri($"https://graph.microsoft.com/beta/me/outlook/taskgroups?$top=1000&$select=Id,Name");
                idAttributeName = "id";
                nameAttributeName = "name";
            }
            else
            {
                URL = new Uri("https://outlook.office.com/api/v2.0/me/taskgroups?$top=1000&$select=Id,Name");
                idAttributeName = "Id";
                nameAttributeName = "Name";
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

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var taskGroups = (JArray)jsonResponse.GetValue("value");

            List<TaskGroup> result = new List<TaskGroup>();

            foreach (var group in taskGroups)
            {
                string id = group.Value<string>(idAttributeName);
                string name = group.Value<string>(nameAttributeName);

                var newTaskGroup = TaskGroup.CreateFromId(id);
                newTaskGroup.Name = name;

                result.Add(newTaskGroup);
            }

            return result;
        }

        public async Task<TaskGroup> GetTaskGroupAsync(string TaskGroupId)
        {
            // Get the specified TaskGroup.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/beta/me/outlook/taskgroups/{TaskGroupId}") : new Uri($"https://outlook.office.com/api/v2.0/me/taskgroups/{TaskGroupId}");

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
            return new TaskGroup(stringResponse);
        }

        public async Task<List<TaskFolder>> GetAllTaskFoldersAsync(string TaskGroupId)
        {
            // Get all TaskFolder in the specified TaskGroup.
            // The property of the item to get is very limited.

            Uri URL;
            string idAttributeName;
            string nameAttributeName;

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                URL = new Uri($"https://graph.microsoft.com/beta/me/outlook/taskgroups/{TaskGroupId}/taskfolders?$select=Id,Name");
                idAttributeName = "id";
                nameAttributeName = "name";
            }
            else
            {
                URL = new Uri($"https://outlook.office.com/api/v2.0/me/taskgroups/{TaskGroupId}/taskfolders?$select=Id,Name");
                idAttributeName = "Id";
                nameAttributeName = "Name";
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

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var taskFolders = (JArray)jsonResponse.GetValue("value");

            List<TaskFolder> result = new List<TaskFolder>();

            foreach (var folder in taskFolders)
            {
                string id = folder.Value<string>(idAttributeName);
                string name = folder.Value<string>(nameAttributeName);

                TaskFolder newTaskFolder = TaskFolder.CreateFromId(id);
                newTaskFolder.Name = name;

                result.Add(newTaskFolder);
            }

            return result;
        }

        public async Task<TaskFolder> GetTaskFolderAsync(string TaskFolderId)
        {
            // Get the specified TaskFolder.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/beta/me/outlook/taskfolders/{TaskFolderId}") : new Uri($"https://outlook.office.com/api/v2.0/me/taskfolders/{TaskFolderId}");

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
            return new TaskFolder(stringResponse);
        }

        public async Task<PagedResponse<Data.TaskAPI.Task>> GetAllTasksFirstPageAsync(string FolderId)
        {
            // Get a page of task items in the specified folder.
            // The property of the item to get is very limited.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/beta/me/outlook/taskfolders/{FolderId}/tasks?$orderby=CreatedDateTime desc&$top=20&$select=Id,Subject,HasAttachments,CreatedDateTime,LastModifiedDateTime,Status") : new Uri($"https://outlook.office.com/api/v2.0/me/TaskFolders/{FolderId}/tasks?$orderby=CreatedDateTime desc&$top=20&$select=Id,Subject,HasAttachments,CreatedDateTime,LastModifiedDateTime,Status");
            return await InternalGetPagedTasksAsync(URL);
        }

        public async Task<PagedResponse<Data.TaskAPI.Task>> GetAllTasksPageAsync(string NextLink)
        {
            // Get a page of task items using the specified NextLink.
            // The property of the item to get is very limited.

            Uri URL = new Uri(NextLink);
            return await InternalGetPagedTasksAsync(URL);
        }

        private async Task<PagedResponse<Data.TaskAPI.Task>> InternalGetPagedTasksAsync(Uri URL)
        {
            var result = new PagedResponse<Data.TaskAPI.Task>
            {
                CurrentPage = new List<Data.TaskAPI.Task>()
            };

            try
            {
                // Get a response and response stream.
                string stringResponse = await SendGetRequestAsync(URL);

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var tasks = (JArray)jsonResponse.GetValue("value");

                foreach (var item in tasks)
                {
                    var serializedObject = new Data.TaskAPI.Task(JsonConvert.SerializeObject(item));
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

        public async Task<Data.TaskAPI.Task> GetTaskAsync(string ItemId)
        {
            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/beta/me/outlook/tasks/{ItemId}") : new Uri($"https://outlook.office.com/api/v2.0/me/tasks/{ItemId}");
            string stringResponse = await SendGetRequestAsync(URL);
            return new Data.TaskAPI.Task(stringResponse);
        }
    }
}