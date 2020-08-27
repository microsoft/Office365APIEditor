// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.AttachmentAPI
{
    [DataContract]
    public class AttachmentBase : MicrosoftGraphBaseObject
    {
        private string contentType;
        private string lastModifiedDateTime;
        private bool? isInline;
        private string name;
        private int? size;

        protected AttachmentBase()
        {
        }

        public AttachmentBase(string JsonData) : base(JsonData)
        {
        }

        public static AttachmentBase CreateFromIdNameContentType(string ID, string Name, string ContentType)
        {
            if (ID == null)
            {
                throw new ArgumentNullException("ID");
            }

            if (Name == null)
            {
                throw new ArgumentNullException("Name");
            }

            AttachmentBase attachmentBase = new AttachmentBase()
            {
                Id = ID,
                Name = Name,
                ContentType = ContentType,
            };

            return attachmentBase;
        }

        public string ContentType
        {
            get
            {
                if (contentType == null)
                {
                    contentType = LoadPropertyFromRawJson<string>("contentType", null);
                }

                return contentType;
            }

            protected set => contentType = value;
        }

        public string LastModifiedDateTime
        {
            get
            {
                if (lastModifiedDateTime == null)
                {
                    lastModifiedDateTime = LoadPropertyFromRawJson<string>("lastModifiedDateTime", null);
                }

                return lastModifiedDateTime;
            }

            protected set => lastModifiedDateTime = value;
        }

        public bool? IsInline
        {
            get
            {
                if (isInline == null)
                {
                    isInline = LoadPropertyFromRawJson("isInline", new bool?());
                }

                return isInline;
            }

            protected set => isInline = value;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get
            {
                if (name == null)
                {
                    name = LoadPropertyFromRawJson<string>("name", null);
                }

                return name;
            }

            protected set => name = value;
        }

        public int? Size
        {
            get
            {
                if (size == null)
                {
                    size = LoadPropertyFromRawJson<int?>("size", new int?());
                }

                return size;
            }

            protected set => size = value;
        }

        public virtual AttachmentType AttachmentType
        {
            get;
        }
    }
}