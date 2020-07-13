// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data
{
    [DataContract]
    class Recipient
    {
        public Recipient()
        {
        }

        public Recipient(EmailAddress EmailAddress)
        {
            this.EmailAddress = EmailAddress;
        }

        public Recipient(string Name, string Address)
        {
            EmailAddress = new EmailAddress(Address, Name);
        }
        
        [DataMember(Name = "emailAddress")]
        public EmailAddress EmailAddress { get; set; }
    }
}