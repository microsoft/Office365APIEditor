// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Net.Mail;

namespace Office365APIEditor.ViewerHelper
{
    public class FocusedInboxOverride
    {
        public string Id;
        public Classify ClassifyAs;
        public FocusedInboxOverrideSender SenderEmailAddress;
    }

    public enum Classify
    {
        Focused,
        Other
    }

    public class FocusedInboxOverrideSender
    {
        public string Name;
        public string Address;

        public FocusedInboxOverrideSender()
        {
        }

        public FocusedInboxOverrideSender(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public static bool TryCreate(string name, string address, out FocusedInboxOverrideSender result)
        {
            // Validate the email address.

            try
            {
                MailAddress mailAddress = new MailAddress(address);

                result = new FocusedInboxOverrideSender(name, address);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}