// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Office365APIEditor.ViewerHelper;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class AttachmentViewerForm : Form
    {
        PublicClientApplication pca;
        FolderInfo targetFolder;
        string targetItemId;
        string targetItemSubject;

        IUser currentUser;

        private ViewerHelper.ViewerHelper viewerHelper;

        string currentId = "";

        public AttachmentViewerForm(PublicClientApplication PCA, IUser CurrentUser, FolderInfo TargetFolderInfo, string TargetItemID, string TargetItemSubject)
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

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

            List<AttachmentSummary> result = new List<AttachmentSummary>(); ;

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                    try
                    {
                        result = await viewerHelper.GetAttachmentsAsync(FolderContentType.Message, targetItemId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                case FolderContentType.Contact:
                    // In the implementation of OutlookServicesClient, contact item does not have attachment.

                    break;
                case FolderContentType.Calendar:
                    try
                    {
                        result = await viewerHelper.GetAttachmentsAsync(FolderContentType.Calendar, targetItemId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                case FolderContentType.DummyCalendarRoot:
                    break;
                default:
                    break;
            }

            foreach (var attachment in result)
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
        }

        private void dataGridView_AttachmentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Get the attachment ID of clicked row.
            string attachmentId = dataGridView_AttachmentList.Rows[e.RowIndex].Tag.ToString();

            if (currentId == attachmentId)
            {
                return;
            }
            else
            {
                currentId = attachmentId;
            }

            // Reset rows.
            dataGridView_ItemProps.Rows.Clear();
            foreach (DataGridViewColumn col in dataGridView_ItemProps.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            ShowAttachmentDetailAsync(attachmentId);
        }

        private async void ShowAttachmentDetailAsync(string attachmentId)
        {
            try
            {
                var attachment = await viewerHelper.GetAttachmentAsync(targetFolder.Type, targetItemId, attachmentId);
                CreatePropTable(attachment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
            }
        }

        private void CreatePropTable(Dictionary<string, string> attachment)
        {
            try
            {
                foreach (KeyValuePair<string, string> item in attachment)
                {
                    DataGridViewRow propRow = new DataGridViewRow();

                    string valueString = (item.Value == null) ? "" : item.Value.ToString();

                    propRow.CreateCells(dataGridView_ItemProps, new object[] { item.Key, valueString, "Dynamic" });

                    if (dataGridView_ItemProps.InvokeRequired)
                    {
                        dataGridView_ItemProps.Invoke(new MethodInvoker(delegate
                        {
                            dataGridView_ItemProps.Rows.Add(propRow);
                        }));
                    }
                    else
                    {
                        dataGridView_ItemProps.Rows.Add(propRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
            }
        }

        private void dataGridView_AttachmentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do nothing.
        }

        private void dataGridView_AttachmentList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Select the row for the context menu.
            dataGridView_AttachmentList.Rows[e.RowIndex].Selected = true;
        }
    }
}
