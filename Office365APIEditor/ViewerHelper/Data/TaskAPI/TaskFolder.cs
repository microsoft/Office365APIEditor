// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;

namespace Office365APIEditor.ViewerHelper.Data.TaskAPI
{
    [Obsolete]
    class TaskFolder : OutlookRestApiBaseObject
    {
        private string changeKey;
        private bool? isDefaultFolder;
        private string name;
        private string parentGroupKey;

        protected TaskFolder()
        {
        }

        public TaskFolder(string JsonData) : base(JsonData)
        {
        }

        public static TaskFolder CreateFromId(string ID)
        {
            TaskFolder newTaskFolder = new TaskFolder()
            {
                Id = ID
            };

            return newTaskFolder;
        }

        public string ChangeKey
        {
            get
            {
                if (changeKey == null)
                {
                    changeKey = LoadPropertyFromRawJson<string>("changeKey", null);
                }

                return changeKey;
            }

            set => changeKey = value;
        }

        public bool? IsDefaultFolder
        {
            get
            {
                if (isDefaultFolder == null)
                {
                    isDefaultFolder = LoadPropertyFromRawJson<bool?>("isDefaultFolder", new bool?());
                }

                return isDefaultFolder;
            }

            set => isDefaultFolder = value;
        }

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

            set => name = value;
        }

        public string ParentGroupKey
        {
            get
            {
                if (parentGroupKey == null)
                {
                    parentGroupKey = LoadPropertyFromRawJson<string>("parentGroupKey", null);
                }

                return parentGroupKey;
            }

            set => parentGroupKey = value;
        }
    }
}