// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 


namespace Office365APIEditor.ViewerHelper.Data
{
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
            EmailAddress = new EmailAddress(Name, Address);
        }

        public EmailAddress EmailAddress { get; set; }
    }
}