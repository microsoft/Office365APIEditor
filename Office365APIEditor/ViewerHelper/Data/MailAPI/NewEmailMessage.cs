// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.MailAPI
{
    [DataContract(Name = "message")]
    class NewEmailMessage
    {
        public NewEmailMessage()
        {
            Body = new ItemBody();
        }

        [DataMember(Name = "toRecipients")]
        public List<Recipient> ToRecipients;

        [DataMember(Name = "ccRecipients")]
        public List<Recipient> CcRecipients;

        [DataMember(Name = "bccRecipients")]
        public List<Recipient> BccRecipients;

        [DataMember(Name = "subject")]
        public string Subject;

        [DataMember(Name = "body")]
        public ItemBody Body;

        public Importance Importance;

        [DataMember(Name = "importance")]
        private string ImportanceString
        {
            get
            {
                return Importance.ToString();
            }
            set
            {
                Importance = (Importance)Enum.Parse(typeof(Importance), value);
            }
        }

        [DataMember(Name = "isDeliveryReceiptRequested")]
        public bool IsDeliveryReceiptRequested;

        [DataMember(Name = "isReadReceiptRequested")]
        public bool IsReadReceiptRequested;

        [DataMember(Name = "attachments", EmitDefaultValue = false)]
        public List<AttachmentAPI.FileAttachment> Attachments;
    }

    public enum Importance
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    [DataContract]
    public enum BodyType
    {
        [EnumMember(Value = "text")]
        Text = 0,

        [EnumMember(Value = "html")]
        HTML = 1
    }
}