// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;

namespace Office365APIEditor.ViewerHelper
{
    // Subset of Microsoft.Office365.OutlookServices.IMessage
    class MessageSummary
    {
        public string Id;
        public string Subject;
        public Recipient Sender;
        public IList<Recipient> ToRecipients;
        public DateTimeOffset? ReceivedDateTime;
        public DateTimeOffset? CreatedDateTime;
        public DateTimeOffset? SentDateTime;
    }
}
