// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.ContactsAPI
{
    class Contact : OutlookRestApiBaseObject
    {
        private string createdDateTime;
        private string displayName;

        protected Contact()
        {
        }

        public Contact(string JsonData) : base(JsonData)
        {
        }

        public string CreatedDateTime
        {
            get
            {
                if (createdDateTime == null)
                {
                    createdDateTime = LoadPropertyFromRawJson<string>("createdDateTime", null);
                }

                return createdDateTime;
            }

            set => createdDateTime = value;
        }

        public string DisplayName
        {
            get
            {
                if (displayName == null)
                {
                    displayName = LoadPropertyFromRawJson<string>("displayName", null);
                }

                return displayName;
            }

            set => displayName = value;
        }
    }
}