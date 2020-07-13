// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    [DataContract]
    class Location
    {
        [DataMember(Name = "displayName")]
        public string DisplayName;
    }
}