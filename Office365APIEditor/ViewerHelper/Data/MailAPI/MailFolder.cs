// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.MailAPI
{
    class MailFolder : OutlookRestApiBaseObject
    {
        private string displayName;
        private string parentFolderId;
        private int? childFolderCount;
        private int? unreadItemCount;
        private int? totalItemCount;

        protected MailFolder()
        {
        }

        public MailFolder(string JsonData) : base(JsonData)
        {
        }

        public static MailFolder CreateFromId(string ID)
        {
            MailFolder newMailFolder = new MailFolder()
            {
                Id = ID
            };

            return newMailFolder;
        }

        public string DisplayName {
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

        public string ParentFolderId {
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

        public int? ChildFolderCount {
            get
            {
                if (childFolderCount == null)
                {
                    childFolderCount = LoadPropertyFromRawJson("childFolderCount", new int?());
                }

                return childFolderCount;
            }

            set => childFolderCount = value;
        }

        public int? UnreadItemCount
        {
            get
            {
                if (unreadItemCount == null)
                {
                    unreadItemCount = LoadPropertyFromRawJson("unreadItemCount", new int?());
                }

                return unreadItemCount;
            }

            set => unreadItemCount = value;
        }

        public int? TotalItemCount
        {
            get
            {
                if (totalItemCount == null)
                {
                    totalItemCount = LoadPropertyFromRawJson("totalItemCount", new int?());
                }

                return totalItemCount;
            }

            set => totalItemCount = value;
        }
    }
}