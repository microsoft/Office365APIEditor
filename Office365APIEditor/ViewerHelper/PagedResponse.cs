// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;

namespace Office365APIEditor.ViewerHelper
{
    class PagedResponse<T>
    {
        public bool MorePage;
        public string NextLink;
        public List<T> CurrentPage;
    }
}
