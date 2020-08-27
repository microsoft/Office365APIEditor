// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.Data.ToDoAPI
{
    class ToDoTaskList : MicrosoftGraphBaseObject
    {
        private string displayName;

        protected ToDoTaskList()
        {
        }

        public ToDoTaskList(string JsonData) : base(JsonData)
        {
        }

        public static ToDoTaskList CreateFromId(string ID)
        {
            ToDoTaskList newToDoTaskList = new ToDoTaskList()
            {
                Id = ID
            };

            return newToDoTaskList;
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
    }
}