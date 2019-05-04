// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper.Data.TaskAPI
{
    class Task : OutlookRestApiBaseObject
    {
        private string createdDateTime;
        private bool? hasAttachments;
        private string lastModifiedDateTime;
        private string status;
        private string subject;

        protected Task()
        {
        }

        public Task(string JsonData) : base(JsonData)
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

        public bool? HasAttachments
        {
            get
            {
                if (hasAttachments == null)
                {
                    hasAttachments = LoadPropertyFromRawJson("hasAttachments", new bool?());
                }

                return hasAttachments;
            }

            set => hasAttachments = value;
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

        public string Subject
        {
            get
            {
                if (subject == null)
                {
                    subject = LoadPropertyFromRawJson<string>("subject", null);
                }

                return subject;
            }

            set => subject = value;
        }
    }
}