// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data
{
    [DataContract]
    public class ItemBody
    {
        public ItemBody()
        {
        }

        public ItemBody(BodyType contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }

        public BodyType ContentType;

        [DataMember(Name = "content")]
        public string Content;

        [DataMember(Name = "contentType")]
        private string ContentTypeString
        {
            get { return ContentType.ToString().ToLower(); }
            set { ContentType = (BodyType)Enum.Parse(typeof(BodyType), value); }
        }
    }
}