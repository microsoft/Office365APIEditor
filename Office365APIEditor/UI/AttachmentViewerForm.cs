// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Data.AttachmentAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class AttachmentViewerForm : Form
    {
        FolderInfo targetFolder;
        string targetItemId;
        string targetItemSubject;

        private ViewerRequestHelper viewerRequestHelper;

        string currentId = "";

        public AttachmentViewerForm(FolderInfo TargetFolderInfo, string TargetItemID, string TargetItemSubject)
        {
            InitializeComponent();

            targetFolder = TargetFolderInfo;
            targetItemId = TargetItemID;
            targetItemSubject = TargetItemSubject;
        }

        private async void AttachmentViewerForm_Load(object sender, EventArgs e)
        {
            Text = "Attachments for '" + targetItemSubject + "'";

            viewerRequestHelper = new ViewerRequestHelper();

            List<AttachmentBase> result = new List<AttachmentBase>(); ;

            switch (targetFolder.Type)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    try
                    {
                        result = await viewerRequestHelper.GetAllAttachmentsAsync(FolderContentType.Message, targetItemId);
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
                        result = await viewerRequestHelper.GetAllAttachmentsAsync(FolderContentType.Calendar, targetItemId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                case FolderContentType.Task:
                    try
                    {
                        result = await viewerRequestHelper.GetAllAttachmentsAsync(FolderContentType.Task, targetItemId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                case FolderContentType.DummyCalendarGroupRoot:
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
                itemRow.ContextMenuStrip = contextMenuStrip_AttachmentList;

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

        private async void dataGridView_AttachmentList_CellClick(object sender, DataGridViewCellEventArgs e)
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

            await ShowAttachmentDetailAsync(attachmentId);
        }

        private async Task<bool> ShowAttachmentDetailAsync(string attachmentId)
        {
            try
            {
                var attachment = await viewerRequestHelper.GetAttachmentAsync(targetFolder.Type, targetItemId, attachmentId);
                CreatePropTable(attachment);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return false;
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

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string attachmentId = "";

            if (dataGridView_AttachmentList.SelectedRows == null || dataGridView_AttachmentList.SelectedRows.Count == 0)
            {
                if (dataGridView_AttachmentList.SelectedCells == null || dataGridView_AttachmentList.SelectedCells.Count == 0)
                {
                    // Attachment is not selected.
                    return;
                }
                else
                {
                    // Cell is selected but row is not selected.
                    
                    // Select the row.
                    dataGridView_AttachmentList.Rows[dataGridView_AttachmentList.SelectedCells[0].RowIndex].Selected = true;

                    // Get the attachment ID of the selected row.
                    attachmentId = dataGridView_AttachmentList.SelectedRows[0].Tag.ToString();

                    currentId = attachmentId;

                    // Reset rows.
                    dataGridView_ItemProps.Rows.Clear();
                    foreach (DataGridViewColumn col in dataGridView_ItemProps.Columns)
                    {
                        col.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }

                    // Display the details of the selected attachment.
                    await ShowAttachmentDetailAsync(attachmentId);
                }
            }

            attachmentId = dataGridView_AttachmentList.SelectedRows[0].Tag.ToString();

            // Get the type of attachment.

            AttachmentType attachmentType = AttachmentType.FileAttachment;

            try
            {
                attachmentType = GetAttachmentTypeOfSelectedAttachment();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }

            switch (attachmentType)
            {
                case AttachmentType.ItemAttachment:
                    MessageBox.Show("The selected attachment is Item Attachment. You can not download this type of attachment using Office365APIEditor", "Office365APIEditor");

                    // The request to get MIME content of itemAttachment will be like below.
                    // https://outlook.office.com/api/beta/Users('6fc42d08-123b-405e-904d-545882e8922f@6d046331-5ea5-4306-87ae-8d51f3dcc71e')/Messages('AAMkAGYxOTczODY2LTQwYzktNDFmYS05ZTIzLWZmNjAxYmM1MWYwZABGAAAAAACmFAp715xPRpcdN7o1X1D7BwDKF8masRMzQ4BmqIbV6OsxAAAAAAEMAADKF8masRMzQ4BmqIbV6OsxAAM85mvEAAA=')/Attachments('AAMkAGYxOTczODY2LTQwYzktNDFmYS05ZTIzLWZmNjAxYmM1MWYwZABGAAAAAACmFAp715xPRpcdN7o1X1D7BwDKF8masRMzQ4BmqIbV6OsxAAAAAAEMAADKF8masRMzQ4BmqIbV6OsxAAM85mvEAAABEgAQAJHp5fRvE4ZIjU_j3_4mUYI=')/$value

                    break;
                case AttachmentType.FileAttachment:
                    saveFileDialog1.FileName = dataGridView_AttachmentList.SelectedRows[0].Cells[0].Value.ToString();

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string rawContentBytes = "";

                        try
                        {
                            rawContentBytes = GetContentBytesOfSelectedAttachment();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Office365APIEditor");
                            return;
                        }

                        try
                        {
                            byte[] bytes = Convert.FromBase64String(rawContentBytes);

                            using (FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write))
                            {
                                fileStream.Write(bytes, 0, bytes.Length);
                            }

                            MessageBox.Show("The attachment file was saved successfully.", "Office365APIEditor");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    break;
                case AttachmentType.ReferenceAttachment:
                    string sourceUrl = "";

                    try
                    {
                        sourceUrl = GetSourceUrlOfSelectedAttachment();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor");
                        return;
                    }

                    try
                    {
                        Process.Start(sourceUrl);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                default:
                    break;
            }
        }

        private AttachmentType GetAttachmentTypeOfSelectedAttachment()
        {
            bool found = false;
            AttachmentType attachmentType = AttachmentType.FileAttachment;

            foreach (DataGridViewRow prop in dataGridView_ItemProps.Rows)
            {
                if (prop.Cells[0].Value.ToString() == "@odata.type")
                {
                    if (prop.Cells[1].Value.ToString() == "#Microsoft.OutlookServices.FileAttachment")
                    {
                        found = true;
                        attachmentType = AttachmentType.FileAttachment;
                    }
                    else if (prop.Cells[1].Value.ToString() == "#Microsoft.OutlookServices.ItemAttachment")
                    {
                        found = true;
                        attachmentType = AttachmentType.ItemAttachment;
                    }
                    else if (prop.Cells[1].Value.ToString() == "#Microsoft.OutlookServices.ReferenceAttachment")
                    {
                        found = true;
                        attachmentType = AttachmentType.ReferenceAttachment;
                    }

                    break;
                }
            }

            if (!found)
            {
                throw new Exception("The specified attachment does not have appropriate @odata.type property.");
            }

            return attachmentType;
        }

        private string GetContentBytesOfSelectedAttachment()
        {
            bool found = false;
            string contentBytes = "";

            foreach (DataGridViewRow prop in dataGridView_ItemProps.Rows)
            {
                if (prop.Cells[0].Value.ToString().ToLowerInvariant() == "contentBytes".ToLowerInvariant())
                {
                    found = true;
                    contentBytes = prop.Cells[1].Value.ToString();

                    break;
                }
            }

            if (!found)
            {
                throw new Exception("The specified attachment does not have appropriate contentBytes property.");
            }

            return contentBytes;
        }

        private string GetSourceUrlOfSelectedAttachment()
        {
            bool found = false;
            string sourceUrl = "";

            foreach (DataGridViewRow prop in dataGridView_ItemProps.Rows)
            {
                if (prop.Cells[0].Value.ToString().ToLowerInvariant() == "sourceUrl".ToLowerInvariant())
                {
                    found = true;
                    sourceUrl = prop.Cells[1].Value.ToString();

                    break;
                }
            }

            if (!found)
            {
                throw new Exception("The specified attachment does not have appropriate sourceUrl property.");
            }

            return sourceUrl;
        }

        private void dataGridView_ItemProps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                // A header was double clicked.
                return;
            }

            // Get the value of double-clicked row.
            object name = dataGridView_ItemProps.Rows[e.RowIndex].Cells[0].Value;
            string nameString = (name == null) ? "" : name.ToString();

            object value = dataGridView_ItemProps.Rows[e.RowIndex].Cells[1].Value;
            string valueString = (value == null) ? "" : value.ToString();


            PropertyViewerForm propertyViewer = new PropertyViewerForm(nameString, valueString)
            {
                Owner = this
            };
            propertyViewer.Show();
        }
    }
}