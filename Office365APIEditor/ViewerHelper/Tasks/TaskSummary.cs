// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;

namespace Office365APIEditor.ViewerHelper.Tasks
{
    class TaskSummary
    {
        public string Id;
        public string Subject;
        public bool HasAttachments;
        public DateTimeOffset? CreatedDateTime;
        public DateTimeOffset? LastModifiedDateTime;
        public string Status;
    }
}
