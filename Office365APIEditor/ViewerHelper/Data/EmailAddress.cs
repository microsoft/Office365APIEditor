// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data
{
    [DataContract]
    public class EmailAddress
    {
        public EmailAddress(string address, string name)
        {
            Address = address;
            Name = name;
        }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}