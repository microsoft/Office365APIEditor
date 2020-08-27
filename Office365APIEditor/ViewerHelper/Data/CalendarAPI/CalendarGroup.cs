// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    class CalendarGroup : MicrosoftGraphBaseObject
    {
        private string name;
        private string changeKey;
        private string classId;

        protected CalendarGroup()
        {
        }

        public CalendarGroup(string JsonData) : base(JsonData)
        {
        }

        public static CalendarGroup CreateFromId(string ID)
        {
            CalendarGroup newCalendarGroup = new CalendarGroup()
            {
                Id = ID
            };

            return newCalendarGroup;
        }

        public string Name
        {
            get
            {
                if (name == null)
                {
                    name = LoadPropertyFromRawJson<string>("name", null);
                }

                return name;
            }

            set => name = value;
        }

        public string ChangeKey
        {
            get
            {
                if (changeKey == null)
                {
                    changeKey = LoadPropertyFromRawJson<string>("changeKey", null);
                }

                return changeKey;
            }

            set => changeKey = value;
        }

        public string ClassId
        {
            get
            {
                if (classId == null)
                {
                    classId = LoadPropertyFromRawJson<string>("classId", null);
                }

                return classId;
            }

            set => classId = value;
        }
    }
}