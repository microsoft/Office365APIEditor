// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    class Calendar : MicrosoftGraphBaseObject
    {
        private string name;
        private string changeKey;
        private string color;

        protected Calendar()
        {
        }

        public Calendar(string JsonData) : base(JsonData)
        {
        }

        public static Calendar CreateFromId(string ID)
        {
            Calendar newCalendar = new Calendar()
            {
                Id = ID
            };

            return newCalendar;
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

        public string Color
        {
            get
            {
                if (color == null)
                {
                    color = LoadPropertyFromRawJson<string>("color", null);
                }

                return color;
            }

            set => color = value;
        }
    }
}