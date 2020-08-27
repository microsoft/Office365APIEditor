// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.ToDoAPI
{
    class ToDoTask : MicrosoftGraphBaseObject
    {
        private string createdDateTime;
        private string lastModifiedDateTime;
        private string status;
        private string title;

        protected ToDoTask()
        {
        }

        public ToDoTask(string JsonData) : base(JsonData)
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

        public string LastModifiedDateTime
        {
            get
            {
                if (lastModifiedDateTime == null)
                {
                    lastModifiedDateTime = LoadPropertyFromRawJson<string>("lastModifiedDateTime", null);
                }

                return lastModifiedDateTime;
            }

            set => lastModifiedDateTime = value;
        }

        public string Status
        {
            get
            {
                if (status == null)
                {
                    status = LoadPropertyFromRawJson<string>("status", null);
                }

                return status;
            }

            set => status = value;
        }

        public string Title
        {
            get
            {
                if (title == null)
                {
                    title = LoadPropertyFromRawJson<string>("title", null);
                }

                return title;
            }

            set => title = value;
        }
    }
}