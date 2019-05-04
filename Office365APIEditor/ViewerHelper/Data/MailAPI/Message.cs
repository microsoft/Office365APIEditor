// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;

namespace Office365APIEditor.ViewerHelper.Data.MailAPI
{
    class Message : OutlookRestApiBaseObject
    {
        private string createdDateTime;
        private bool? isDraft;
        private string receivedDateTime;
        private Recipient sender;
        private string sentDateTime;
        private string subject;
        private IList<Recipient> toRecipients;

        protected Message()
        {
        }

        public Message(string JsonData) : base(JsonData)
        {
        }

        public string CreatedDateTime
        {
            get
            {
                if ( createdDateTime == null)
                {
                    createdDateTime = LoadPropertyFromRawJson<string>("createdDateTime", null);
                }

                return createdDateTime;
            }

            set => createdDateTime = value;
        }

        public bool? IsDraft
        {
            get
            {
                if (isDraft == null)
                {
                    isDraft = LoadPropertyFromRawJson<bool?>("isDraft", new bool?());
                }

                return isDraft;
            }

            set => isDraft = value;
        }

        public string ReceivedDateTime
        {
            get
            {
                if (receivedDateTime == null)
                {
                    receivedDateTime = LoadPropertyFromRawJson<string>("receivedDateTime", null);
                }

                return receivedDateTime;
            }

            set => receivedDateTime = value;
        }

        public Recipient Sender
        {
            get
            {
                if (sender == null)
                {
                    sender = LoadPropertyFromRawJson<Recipient>("sender", null);
                }

                return sender;
            }

            set => sender = value;
        }

        public string SentDateTime
        {
            get
            {
                if (sentDateTime == null)
                {
                    sentDateTime = LoadPropertyFromRawJson<string>("sentDateTime", null);
                }

                return sentDateTime;
            }

            set => sentDateTime = value;
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

        public IList<Recipient> ToRecipients
        {
            get
            {
                if (toRecipients == null)
                {
                    toRecipients = LoadPropertyFromRawJson("toRecipients", new List<Recipient>());
                }

                return toRecipients;
            }

            set => toRecipients = value;
        }
    }
}