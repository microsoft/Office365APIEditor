// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;
using System.Net.Mail;

namespace Office365APIEditor.ViewerHelper
{
    class NewEmailMessage
    {
        public MailAddressCollection ToRecipients;
        public MailAddressCollection CcRecipients;
        public MailAddressCollection BccRecipients;
        public string Subject;
        public BodyType BodyType;
        public string Body;
        public Importance Importance;
        public bool RequestDeliveryReceipt;
        public bool RequestReadReceipt;
        public bool SaveToSentItems;
        public List<object> Attachments;
    }

    public enum Importance
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    public enum BodyType
    {
        Text = 0,
        HTML = 1
    }
}
