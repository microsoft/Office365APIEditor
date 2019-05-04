// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data
{
    public class EmailAddress
    {
        public EmailAddress(string address, string name)
        {
            Address = address;
            Name = name;
        }

        public string Address { get; set; }

        public string Name { get; set; }
    }
}