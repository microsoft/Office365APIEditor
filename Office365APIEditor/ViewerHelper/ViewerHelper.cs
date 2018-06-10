// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.OData.ProxyExtensions;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    class ViewerHelper
    {
        private PublicClientApplication pca;
        private Microsoft.Identity.Client.IUser currentUser;
        OutlookServicesClient client;

        public ViewerHelper(PublicClientApplication PCA, Microsoft.Identity.Client.IUser CurrentUser)
        {
            pca = PCA;
            currentUser = CurrentUser;

            // TODO : Implement logging feature
            // Maybe in Util, not here.
            //client.Context.BuildingRequest += new EventHandler<BuildingRequestEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
            //client.Context.ReceivingResponse += new EventHandler<ReceivingResponseEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
        }

        private void RequestLogger(object eventSender, ReceivingResponseEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ResponseMessage.ToString();
        }

        private void RequestLogger(object eventSender, BuildingRequestEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ToString();
        }

        public async Task<IMailFolder> GetMsgFolderRootAsync()
        {
            // Get the folder ID of the parent folder of parent folder of Inbox.
            // It's MsgFolderRoot.

            // We can't get the Top of Information Store folder directly.
            // Following operation is available with v1.0 only.
            // https://outlook.office.com/api/v1.0/me/RootFolder

            client = await Util.GetOutlookServicesClientAsync(pca, currentUser);

            try
            {
                var inbox = await client.Me.MailFolders["Inbox"].ExecuteAsync(); // Inbox
                var topOfInformationStore = await client.Me.MailFolders[inbox.ParentFolderId].ExecuteAsync(); // Top of information store
                var msgFolderRoot = await client.Me.MailFolders[topOfInformationStore.ParentFolderId].ExecuteAsync(); // MsgFolderRoot

                return msgFolderRoot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Calendar>> GetCalendarFoldersAsync()
        {
            // Get all calendar folders in the mailbox.

            client = await Util.GetOutlookServicesClientAsync(pca, currentUser);

            List<Calendar> result = new List<Calendar>();

            try
            {
                var calendarFolderResults = await client.Me.Calendars
                .OrderBy(c => c.Name)
                .Take(100)
                .Select(c => new { c.Id, c.Name })
                .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in calendarFolderResults.CurrentPage)
                    {
                        result.Add(new Calendar() { Id = folder.Id, Name = folder.Name });
                    }

                    if (calendarFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        calendarFolderResults = await calendarFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MailFolder>> GetAllChildMailFolderAsync(string FolderId)
        {
            // Get all MailFolders in the specified folder.

            client = await Util.GetOutlookServicesClientAsync(pca, currentUser);

            List<MailFolder> result = new List<MailFolder>();

            try
            {
                var childMailFolderResults = await client.Me.MailFolders[FolderId].ChildFolders
                    .OrderBy(m => m.DisplayName)
                    .Take(100)
                    .Select(m => new { m.Id, m.DisplayName, m.ChildFolderCount })
                    .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in childMailFolderResults.CurrentPage)
                    {
                        result.Add(new MailFolder() { Id = folder.Id, DisplayName = folder.DisplayName, ChildFolderCount = folder.ChildFolderCount });
                    }

                    if (childMailFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        childMailFolderResults = await childMailFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContactFolder>> GetAllChildContactFolderAsync(string FolderId)
        {
            // Get all contact folders in the specified folder.

            client = await Util.GetOutlookServicesClientAsync(pca, currentUser);

            List<ContactFolder> result = new List<ContactFolder>();

            try
            {
                var childContactFolderResults = await client.Me.ContactFolders[FolderId].ChildFolders
                    .OrderBy(f => f.DisplayName)
                    .Take(100)
                    .Select(f => new { f.Id, f.DisplayName })
                    .ExecuteAsync();

                bool morePages = false;

                do
                {
                    foreach (var folder in childContactFolderResults.CurrentPage)
                    {
                        result.Add(new ContactFolder() { Id = folder.Id, DisplayName = folder.DisplayName });
                    }

                    if (childContactFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        childContactFolderResults = await childContactFolderResults.GetNextPageAsync();
                    }
                    else
                    {
                        morePages = false;
                    }
                } while (morePages);

                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
