// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.ContactsAPI
{
    class ContactFolder : MicrosoftGraphBaseObject
    {
        private string displayName;
        private string parentFolderId;

        protected ContactFolder()
        {
        }

        public ContactFolder(string JsonData) : base(JsonData)
        {
        }

        public static ContactFolder CreateFromId(string ID)
        {
            ContactFolder newContactFolder = new ContactFolder()
            {
                Id = ID
            };

            return newContactFolder;
        }

        public string DisplayName
        {
            get
            {
                if (displayName == null)
                {
                    displayName = LoadPropertyFromRawJson<string>("displayName", null);
                }

                return displayName;
            }

            set => displayName = value;
        }

        public string ParentFolderId
        {
            get
            {
                if (parentFolderId == null)
                {
                    parentFolderId = LoadPropertyFromRawJson<string>("parentFolderId", null);
                }

                return parentFolderId;
            }

            set => parentFolderId = value;
        }
    }
}