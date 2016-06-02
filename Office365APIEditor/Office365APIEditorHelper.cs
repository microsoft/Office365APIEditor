// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor
{
    public class Office365APIEditorHelper
    {
        public static string[] MailboxViewerScopes()
        {
            return new string[] { "offline_access https://outlook.office.com/mail.read https://outlook.office.com/contacts.read https://outlook.office.com/calendars.read" };
        }
    }

    public struct FolderInfo
    {
        public string ID;
        public FolderContentType Type;
    }

    public enum FolderContentType
    {
        Message,
        Contact,
        Calendar,
        DummyCalendarRoot
    }
}
