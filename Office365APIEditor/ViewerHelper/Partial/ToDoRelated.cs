// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.ToDoAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<ToDoTaskList>> GetAllToDoTaskListsAsync()
        {
            // Get all To Do lists.

            Uri URL;
            string idAttributeName;
            string displayNameAttributeName;

            URL = new Uri($"https://graph.microsoft.com/v1.0/me/todo/lists");
            idAttributeName = "id";
            displayNameAttributeName = "displayName";

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
            var toDoTaskLists = (JArray)jsonResponse.GetValue("value");

            List<ToDoTaskList> result = new List<ToDoTaskList>();

            foreach (var folder in toDoTaskLists)
            {
                string id = folder.Value<string>(idAttributeName);
                string displayName = folder.Value<string>(displayNameAttributeName);

                ToDoTaskList newToDoTaskList = ToDoTaskList.CreateFromId(id);
                newToDoTaskList.DisplayName = displayName;

                result.Add(newToDoTaskList);
            }

            return result;
        }

        public async Task<ToDoTaskList> GetToDoTaskListAsync(string ToDoTaskListId)
        {
            // Get the specified ToDoTaskList.

            Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/todo/lists/{ToDoTaskListId}");

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
            return new ToDoTaskList(stringResponse);
        }

        public async Task<PagedResponse<ToDoTask>> GetAllToDoTasksFirstPageAsync(string ToDoTaskListId)
        {
            // Get a page of ToDoTasks in the specified ToDoTaskList.

            Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/todo/lists/{ToDoTaskListId}/tasks?$orderby=CreatedDateTime desc&$top=20");
            return await InternalGetPagedToDoTasksAsync(URL);
        }

        public async Task<PagedResponse<ToDoTask>> GetAllToDoTasksPageAsync(string NextLink)
        {
            // Get a page of ToDoTasks using the specified NextLink.

            Uri URL = new Uri(NextLink);
            return await InternalGetPagedToDoTasksAsync(URL);
        }

        private async Task<PagedResponse<ToDoTask>> InternalGetPagedToDoTasksAsync(Uri URL)
        {
            var result = new PagedResponse<ToDoTask>
            {
                CurrentPage = new List<ToDoTask>()
            };

            try
            {
                // Get a response and response stream.
                string stringResponse = await SendGetRequestAsync(URL);

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var toDoTasks = (JArray)jsonResponse.GetValue("value");

                foreach (var toDoTask in toDoTasks)
                {
                    var serializedObject = new ToDoTask(JsonConvert.SerializeObject(toDoTask));
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

        public async Task<ToDoTask> GetToDoTaskAsync(string toDoTaskListId, string toDoTaskId)
        {
            Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/todo/lists/{toDoTaskListId}/tasks/{toDoTaskId}");
            string stringResponse = await SendGetRequestAsync(URL);
            return new ToDoTask(stringResponse);
        }
    }
}