// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;

namespace Office365APIEditor.ViewerHelper
{
    // Subset of Microsoft.Office365.OutlookServices.IMessage
    class ContactSummary
    {
        public string Id;
        public string DisplayName;
        public DateTimeOffset? CreatedDateTime;
    }
}
