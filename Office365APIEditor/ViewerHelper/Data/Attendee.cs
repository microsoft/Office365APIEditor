// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data
{
    class Attendee : Recipient
    {
        public Attendee()
        {
        }

        public Attendee(EmailAddress EmailAddress) : base(EmailAddress)
        {
        }

        public Attendee(string Name, string Address) : base(Name, Address)
        {
        }

        public ResponseStatus Status { get; set; }

        public string Type { get; set; }
    }
}