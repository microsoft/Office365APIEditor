// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.Office365.OutlookServices;
using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class AttachmentViewerForm : Form
    {
        PublicClientApplication pca;
        FolderInfo targetFolder;
        string targetItemId;
        string targetItemSubject;

        OutlookServicesClient client;

        Microsoft.Identity.Client.IUser currentUser;

        public AttachmentViewerForm(PublicClientApplication PCA, Microsoft.Identity.Client.IUser CurrentUser, FolderInfo TargetFolderInfo, string TargetItemID, string TargetItemSubject)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = TargetFolderInfo;
            targetItemId = TargetItemID;
            targetItemSubject = TargetItemSubject;
        }

        private async void AttachmentViewerForm_Load(object sender, EventArgs e)
        {
            Text = "Attachments for '" + targetItemSubject + "'";

            client = Util.GetOutlookServicesClient(pca, currentUser);

            if (client == null)
            {
                MessageBox.Show("Authentication failure.", "Office365APIEditor");
            }

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
                    try
                    {
                        var results = await client.Me.Messages[targetItemId].Attachments
                            .OrderBy(a => a.Name)                            
                            .Take(50)
                            .Select(a => new { a.Id, a.Name, a.ContentType })
                            .ExecuteAsync();

                        if (results.CurrentPage.Count == 0)
                        {
                            // No attachments for this item.
                            return;
                        }

                        bool morePages = false;

                        do
                        {
                            foreach (var attachment in results.CurrentPage)
                            {
                                // Add new row.

                                string name = attachment.Name;
                                string id = attachment.Id;
                                string contentType = attachment.ContentType;

                                DataGridViewRow itemRow = new DataGridViewRow
                                {
                                    Tag = attachment.Id
                                };
                                itemRow.CreateCells(dataGridView_AttachmentList, new object[] { name, id, contentType });
                                // itemRow.ContextMenuStrip = contextMenuStrip_AttachmentList;

                                if (dataGridView_AttachmentList.InvokeRequired)
                                {
                                    dataGridView_AttachmentList.Invoke(new MethodInvoker(delegate { dataGridView_AttachmentList.Rows.Add(itemRow); }));
                                }
                                else
                                {
                                    dataGridView_AttachmentList.Rows.Add(itemRow);
                                }
                            }

                            if (results.MorePagesAvailable)
                            {
                                morePages = true;
                                results = await results.GetNextPageAsync();
                            }
                            else
                            {
                                morePages = false;
                            }
                        } while (morePages);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case FolderContentType.Contact:
                    break;
                case FolderContentType.Calendar:
                    break;
                case FolderContentType.DummyCalendarRoot:
                    break;
                default:
                    break;
            }
        }
    }
}
