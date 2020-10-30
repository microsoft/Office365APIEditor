// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.CalendarAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public async Task<List<CalendarGroup>> GetAllCalendarGroupsAsync()
        {
            // Get all calendar groups.
            // The property of the item to get is very limited.

            Uri URL;
            string idAttributeName;
            string nameAttributeName;

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                URL = new Uri("https://graph.microsoft.com/v1.0/me/calendargroups?$top=100&$select=Id,Name");
                idAttributeName = "id";
                nameAttributeName = "name";
            }
            else
            {
                URL = new Uri("https://outlook.office.com/api/v2.0/me/calendargroups?$top=100&$select=Id,Name");
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
            var calendarGroups = (JArray)jsonResponse.GetValue("value");

            List<CalendarGroup> result = new List<CalendarGroup>();

            foreach (var group in calendarGroups)
            {
                string id = group.Value<string>(idAttributeName);
                string name = group.Value<string>(nameAttributeName);

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

            Uri URL;
            string idAttributeName;
            string nameAttributeName;

            if (Util.UseMicrosoftGraphInMailboxViewer)
            {
                URL = new Uri($"https://graph.microsoft.com/v1.0/me/calendargroups/{CalendarGroupId}/calendars?$top=1000&$select=Id,Name");
                idAttributeName = "id";
                nameAttributeName = "name";
            }
            else
            {
                URL = new Uri($"https://outlook.office.com/api/v2.0/me/calendargroups/{CalendarGroupId}/calendars?$top=1000&$select=Id,Name");
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
            var calendars = (JArray)jsonResponse.GetValue("value");

            List<Calendar> result = new List<Calendar>();

            foreach (var folder in calendars)
            {
                string id = folder.Value<string>(idAttributeName);
                string name = folder.Value<string>(nameAttributeName);

                var newCalendar = Calendar.CreateFromId(id);
                newCalendar.Name = name;

                result.Add(newCalendar);
            }

            return result;
        }

        public async Task<CalendarGroup> GetCalendarGroupAsync(string CalendarGroupId)
        {
            // Get the specified calendar group.

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/calendargroups/{CalendarGroupId}") : new Uri($"https://outlook.office.com/api/v2.0/me/calendargroups/{CalendarGroupId}");

            string stringResponse = "";

            try
            {
                stringResponse = await SendGetRequestAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var calendarGroup = new CalendarGroup(stringResponse);

            return calendarGroup;
        }

        public async Task<Calendar> GetDefaultCalendarAsync()
        {
            // Get the default Calendar

            try
            {
                Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/calendar");
                string rawJson = await SendGetRequestAsync(URL);
                var calendar = new Calendar(rawJson);

                return calendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Calendar> GetCalendarAsync(string FolderId)
        {
            // Get the specified Calendar

            try
            {
                Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/calendars/{FolderId}") : new Uri($"https://outlook.office.com/api/v2.0/me/calendars/{FolderId}");
                string rawJson = await SendGetRequestAsync(URL);
                var calendar = new Calendar(rawJson);

                return calendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAllowdOnlineMeetingProvidersAsync()
        {
            // Get allowedOnlineMeetingProviders property.

            if (!Util.UseMicrosoftGraphInMailboxViewer)
            {
                throw new NotImplementedException("UseMicrosoftGraphInMailboxViewer must be true.");
            }

            try
            {
                Uri URL = new Uri("https://graph.microsoft.com/v1.0/me/calendar?$select=allowedOnlineMeetingProviders");
                string rawJson = await SendGetRequestAsync(URL);
                var jsonResponse = (JObject)JsonConvert.DeserializeObject(rawJson);

                List<string> result = new List<string>();

                foreach ( var provider in jsonResponse.Property("allowedOnlineMeetingProviders").Values())
                {
                    result.Add(provider.Value<string>());
                }

                return result;
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

            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/calendars/{FolderId}/events?$orderby=CreatedDateTime desc&$top=20&$select=Id,Subject,Organizer,Attendees,Start,End,IsAllDay,CreatedDateTime") : new Uri($"https://outlook.office.com/api/v2.0/me/calendars/{FolderId}/events?$orderby=CreatedDateTime desc&$top=20&$select=Id,Subject,Organizer,Attendees,Start,End,IsAllDay,CreatedDateTime");
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
            var result = new PagedResponse<Event>
            {
                CurrentPage = new List<Event>()
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

        public async Task<PagedResponse<Event>> GetCalendarViewFirstPageAsync(string FolderId, DateTime StartDateTimeInUtc, DateTime EndDateTimeInUtc)
        {
            // Get a CalendarView page in the specified folder.
            // The property of the item to get is very limited.

            string start = StartDateTimeInUtc.ToString("yyyy-MM-ddTHH:mm:ss");
            string end = EndDateTimeInUtc.ToString("yyyy-MM-ddTHH:mm:ss");

            Uri URL = new Uri($"https://graph.microsoft.com/v1.0/me/calendars/{FolderId}/calendarView?startDateTime={start}&endDateTime={end}&$top=20&$select=Id,Subject,Organizer,Attendees,Start,End,IsAllDay");
            return await InternalGetPagedEventsAsync(URL);
        }

        public async Task<Event> GetEventAsync(string ItemId)
        {
            Uri URL = Util.UseMicrosoftGraphInMailboxViewer ? new Uri($"https://graph.microsoft.com/v1.0/me/events/{ItemId}") : new Uri($"https://outlook.office.com/api/v2.0/me/events/{ItemId}");

            string stringResponse = await SendGetRequestAsync(URL);
            return new Event(stringResponse);
        }

        public async Task CreateEventAsync(NewEvent newItem)
        {
            // Create an event.

            if (!Util.UseMicrosoftGraphInMailboxViewer)
            {
                throw new NotImplementedException();
            }

            Uri URL = new Uri("https://graph.microsoft.com/v1.0/me/events");

            string postData = CreatePostDataToCreateNewEvent(newItem);

            try
            {
                string stringResponse = await SendPostRequestAsync(URL, postData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreatePostDataToCreateNewEvent(NewEvent newItem)
        {
            string result;

            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(NewEvent));
                serializer.WriteObject(stream, newItem);
                result = Encoding.UTF8.GetString(stream.ToArray());
            }

            return result;
        }
    }
}