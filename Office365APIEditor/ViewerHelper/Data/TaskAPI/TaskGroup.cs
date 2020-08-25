// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;

namespace Office365APIEditor.ViewerHelper.Data.TaskAPI
{
    [Obsolete]
    class TaskGroup : OutlookRestApiBaseObject
    {
        private string changeKey;
        private string groupKey;
        private bool? isDefaultGroup;
        private string name;

        protected TaskGroup()
        {
        }

        public TaskGroup(string JsonData) : base(JsonData)
        {
        }

        public static TaskGroup CreateFromId(string ID)
        {
            TaskGroup newTaskGroup = new TaskGroup()
            {
                Id = ID
            };

            return newTaskGroup;
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

        public string GroupKey
        {
            get
            {
                if (groupKey == null)
                {
                    groupKey = LoadPropertyFromRawJson<string>("groupKey", null);
                }

                return groupKey;
            }

            set => groupKey = value;
        }

        public bool? IsDefaultGroup
        {
            get
            {
                if (isDefaultGroup == null)
                {
                    isDefaultGroup = LoadPropertyFromRawJson<bool?>("isDefaultGroup", new bool?());
                }

                return isDefaultGroup;
            }

            set => isDefaultGroup = value;
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
    }
}