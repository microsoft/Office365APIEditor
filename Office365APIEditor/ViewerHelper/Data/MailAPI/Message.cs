// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;

namespace Office365APIEditor.ViewerHelper.Data.MailAPI
{
    class Message : MicrosoftGraphBaseObject
    {
        private IList<Recipient> bccRecipients;
        private ItemBody body;
        private IList<Recipient> ccRecipients;
        private string createdDateTime;
        private string importance;
        private bool? isDeliveryReceiptRequested;
        private bool? isDraft;
        private bool? isReadReceiptRequested;
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

        public IList<Recipient> BccRecipients
        {
            get
            {
                if (bccRecipients == null)
                {
                    bccRecipients = LoadPropertyFromRawJson("bccRecipients", new List<Recipient>());
                }

                return bccRecipients;
            }

            set => bccRecipients = value;
        }

        public ItemBody Body
        {
            get
            {
                if (body == null)
                {
                    body = LoadPropertyFromRawJson("body", new ItemBody());
                }

                return body;
            }

            set => body = value;
        }

        public IList<Recipient> CcRecipients
        {
            get
            {
                if (ccRecipients == null)
                {
                    ccRecipients = LoadPropertyFromRawJson("ccRecipients", new List<Recipient>());
                }

                return ccRecipients;
            }

            set => ccRecipients = value;
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

        public string Importance
        {
            get
            {
                if (importance == null)
                {
                    importance = LoadPropertyFromRawJson<string>("importance", null);
                }

                return importance;
            }

            set => importance = value;
        }

        public bool? IsDeliveryReceiptRequested
        {
            get
            {
                if (isDeliveryReceiptRequested == null)
                {
                    isDeliveryReceiptRequested = LoadPropertyFromRawJson("isDeliveryReceiptRequested", new bool?());
                }

                return isDeliveryReceiptRequested;
            }

            set => isDeliveryReceiptRequested = value;
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

        public bool? IsReadReceiptRequested
        {
            get
            {
                if (isReadReceiptRequested == null)
                {
                    isReadReceiptRequested = LoadPropertyFromRawJson("isReadReceiptRequested", new bool?());
                }

                return isReadReceiptRequested;
            }

            set => isReadReceiptRequested = value;
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