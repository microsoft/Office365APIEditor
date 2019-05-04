// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data
{
    public class ItemBody
    {
        public ItemBody()
        {
        }

        public ItemBody(string contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }

        public string ContentType;
        public string Content;
    }
}