// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.CalendarAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<CalendarGroup>> GetAllCalendarGroupsAsync()
        {
            // Get all calendar groups.
            // The property of the item to get is very limited.

            Uri URL = new Uri("https://outlook.office.com/api/v2.0/me/calendargroups?$top=100&$select=Id,Name");

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

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var calendarGroups = (JArray)jsonResponse.GetValue("value");

            List<CalendarGroup> result = new List<CalendarGroup>();

            foreach (var group in calendarGroups)
            {
                string id = group.Value<string>("Id");
                string name = group.Value<string>("Name");

                var newCalendarGroup = CalendarGroup.CreateFromId(id);
                newCalendarGroup.Name = name;

                result.Add(newCalendarGroup);
            }

            return result;
        }

        public async Task<List<Calendar>> GetAllCalendarFoldersAsync(string CalendarGroupId)
        {
            // Get all calendars in the specified calendar group.
            // The property of the item to get is very limited.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/calendargroups/{CalendarGroupId}/calendars?$top=1000&$select=Id,Name");

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

            // Convert JSON response.

            var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
            var calendars = (JArray)jsonResponse.GetValue("value");

            List<Calendar> result = new List<Calendar>();

            foreach (var folder in calendars)
            {
                string id = folder.Value<string>("Id");
                string name = folder.Value<string>("Name");

                var newCalendar = Calendar.CreateFromId(id);
                newCalendar.Name = name;

                result.Add(newCalendar);
            }

            return result;
        }

        public async Task<CalendarGroup> GetCalendarGroupAsync(string CalendarGroupId)
        {
            // Get the specified calendar group.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/calendargroups/{CalendarGroupId}");

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

            var calendarGroup = new CalendarGroup(stringResponse);

            return calendarGroup;
        }

        public async Task<Calendar> GetCalendarAsync(string FolderId)
        {
            // Get the specified Calendar

            try
            {
                string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

                Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/calendars/{FolderId}");
                string rawJson = await Util.SendGetRequestAsync(URL, accessToken, currentUser.Username);
                var calendar = new Calendar(rawJson);

                return calendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<Event>> GetAllEventsFirstPageAsync(string FolderId)
        {
            // Get a page of event items in the specified folder.
            // The property of the item to get is very limited.

            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/calendars/{FolderId}/events?$orderby=CreatedDateTime desc&$top=20&$select=Id,Subject,Organizer,Attendees,Start,End,IsAllDay,CreatedDateTime");
            return await InternalGetPagedEventsAsync(URL);
        }

        public async Task<PagedResponse<Event>> GetAllEventsPageAsync(string NextLink)
        {
            // Get a page of event items using the specified NextLink.
            // The property of the item to get is very limited.

            Uri URL = new Uri(NextLink);
            return await InternalGetPagedEventsAsync(URL);
        }

        private async Task<PagedResponse<Event>> InternalGetPagedEventsAsync(Uri URL)
        {
            string accessToken = await Util.GetAccessTokenAsync(pca, currentUser);

            var result = new PagedResponse<Event>
            {
                CurrentPage = new List<Event>()
            };

            try
            {
                // Get a response and response stream.
                string stringResponse = await Util.SendGetRequestAsync(URL, accessToken, currentUser.Username);

                // Convert JSON response.

                var jsonResponse = (JObject)JsonConvert.DeserializeObject(stringResponse);
                var messages = (JArray)jsonResponse.GetValue("value");

                foreach (var item in messages)
                {
                    var serializedObject = new Event(JsonConvert.SerializeObject(item));
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

        public async Task<Event> GetEventAsync(string ItemId)
        {
            Uri URL = new Uri($"https://outlook.office.com/api/v2.0/me/events/{ItemId}");

            string accessToken = await Util.GetAccessTokenAsync(Global.pca, Global.currentUser);
            string stringResponse = await Util.SendGetRequestAsync(URL, accessToken, Global.currentUser.Username);
            return new Event(stringResponse);
        }
    }
}