// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Office365APIEditor.ViewerHelper;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Office365APIEditor.UI.FocusedInbox
{
    public partial class FocusedInboxOverrideForm : Form
    {
        PublicClientApplication pca;
        IUser currentUser;

        private ViewerHelper.ViewerHelper viewerHelper;

        private List<FocusedInboxOverride> originalOverrides = new List<FocusedInboxOverride>();

        private List<string> overridesToBeChanged = new List<string>();
        private List<string> overridesToBeRemoved = new List<string>();

        public FocusedInboxOverrideForm(PublicClientApplication PCA, IUser CurrentUser)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
        }

        private async void FocusedInboxOverrideForm_LoadAsync(object sender, EventArgs e)
        {
            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

            try
            {
                // Get all focused inbox overrides.
                originalOverrides = await viewerHelper.GetFocusedInboxOverridesAsync();

                // Show current setting.

                if (dataGridView_FocusedInboxOverrides.InvokeRequired)
                {
                    dataGridView_FocusedInboxOverrides.Invoke(new MethodInvoker(delegate
                    {
                        dataGridView_FocusedInboxOverrides.Columns.Add("Id", "Id");
                        dataGridView_FocusedInboxOverrides.Columns.Add("ClassifyAs", "ClassifyAs");
                        dataGridView_FocusedInboxOverrides.Columns.Add("DisplayName", "DisplayName");
                        dataGridView_FocusedInboxOverrides.Columns.Add("EmailAddress", "EmailAddress");
                    }));
                }
                else
                {
                    dataGridView_FocusedInboxOverrides.Columns.Add("Id", "Id");
                    dataGridView_FocusedInboxOverrides.Columns.Add("ClassifyAs", "ClassifyAs");
                    dataGridView_FocusedInboxOverrides.Columns.Add("DisplayName", "DisplayName");
                    dataGridView_FocusedInboxOverrides.Columns.Add("EmailAddress", "EmailAddress");
                }

                foreach (var item in originalOverrides)
                {
                    DataGridViewRow itemRow = new DataGridViewRow();

                    itemRow.CreateCells(dataGridView_FocusedInboxOverrides, new object[] { item.Id, item.ClassifyAs.ToString(), item.SenderEmailAddress.Name, item.SenderEmailAddress.Address });

                    if (dataGridView_FocusedInboxOverrides.InvokeRequired)
                    {
                        dataGridView_FocusedInboxOverrides.Invoke(new MethodInvoker(delegate { dataGridView_FocusedInboxOverrides.Rows.Add(itemRow); }));
                    }
                    else
                    {
                        dataGridView_FocusedInboxOverrides.Rows.Add(itemRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            NewFocusedInboxOverrideForm newFocusedInboxOverrideForm = new NewFocusedInboxOverrideForm();

            if (newFocusedInboxOverrideForm.ShowDialog(out FocusedInboxOverride newOverride) == DialogResult.OK)
            {
                DataGridViewRow itemRow = new DataGridViewRow();

                itemRow.CreateCells(dataGridView_FocusedInboxOverrides, new object[] { newOverride.Id, newOverride.ClassifyAs.ToString(), newOverride.SenderEmailAddress.Name, newOverride.SenderEmailAddress.Address });

                if (dataGridView_FocusedInboxOverrides.InvokeRequired)
                {
                    dataGridView_FocusedInboxOverrides.Invoke(new MethodInvoker(delegate { dataGridView_FocusedInboxOverrides.Rows.Add(itemRow); }));
                }
                else
                {
                    dataGridView_FocusedInboxOverrides.Rows.Add(itemRow);
                }
            }
        }

        private void Button_Edit_Click(object sender, EventArgs e)
        {
            if(dataGridView_FocusedInboxOverrides.RowCount >= 1 && dataGridView_FocusedInboxOverrides.SelectedRows.Count == 1)
            {
                // something selected.

                DataGridViewRow selectedRow = dataGridView_FocusedInboxOverrides.SelectedRows[0];

                FocusedInboxOverride selectedOverride = new FocusedInboxOverride() {
                    Id = selectedRow.Cells[0].Value == null ? "" : selectedRow.Cells[0].Value.ToString(),
                    ClassifyAs = (Classify)Enum.Parse(typeof(Classify), selectedRow.Cells[1].Value.ToString()),
                    SenderEmailAddress = new FocusedInboxOverrideSender(
                        selectedRow.Cells[2].Value == null ? "" : selectedRow.Cells[2].Value.ToString(),
                        selectedRow.Cells[3].Value.ToString())
                };

                NewFocusedInboxOverrideForm newFocusedInboxOverrideForm = new NewFocusedInboxOverrideForm(selectedOverride);

                if (newFocusedInboxOverrideForm.ShowDialog(out FocusedInboxOverride newOverride) == DialogResult.OK)
                {
                    selectedRow.Cells[1].Value = newOverride.ClassifyAs.ToString();
                    selectedRow.Cells[2].Value = newOverride.SenderEmailAddress.Name;
                    selectedRow.Cells[3].Value = newOverride.SenderEmailAddress.Address;

                    if (newOverride.Id != "")
                    {
                        // This override is not a new one because it have ID.

                        if (!overridesToBeChanged.Contains(newOverride.Id))
                        {
                            overridesToBeChanged.Add(newOverride.Id);
                        }                        
                    }
                }
            }
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (dataGridView_FocusedInboxOverrides.RowCount >= 1 && dataGridView_FocusedInboxOverrides.SelectedRows.Count == 1)
            {
                // something selected.

                DataGridViewRow selectedRow = dataGridView_FocusedInboxOverrides.SelectedRows[0];

                string selectedOverrideId = selectedRow.Cells[0].Value == null ? "" : selectedRow.Cells[0].Value.ToString();

                if (selectedOverrideId != "")
                {
                    overridesToBeRemoved.Add(selectedOverrideId);
                }

                dataGridView_FocusedInboxOverrides.Rows.Remove(selectedRow);
            }
        }

        private async void Button_OK_ClickAsync(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

            if (overridesToBeRemoved.Count != 0)
            {
                // A part of original override was removed.

                foreach (var item in overridesToBeRemoved)
                {
                    try
                    {
                        await viewerHelper.RemoveFocusedInboxOverrideAsync(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView_FocusedInboxOverrides.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[0].Value.ToString() == "")
                {
                    // This is a new entry
                    // Need to add new settings.

                    FocusedInboxOverride newOverride = new FocusedInboxOverride
                    {
                        ClassifyAs = (Classify)Enum.Parse(typeof(Classify), row.Cells[1].Value.ToString()),
                        SenderEmailAddress = new FocusedInboxOverrideSender(row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString())
                    };

                    try
                    {
                        await viewerHelper.AddOrUpdateFocusedInboxOverrideAsync(newOverride);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (overridesToBeChanged.Contains(row.Cells[0].Value.ToString())) {
                    // This override is changed.

                    FocusedInboxOverride newOverride = new FocusedInboxOverride
                    {
                        ClassifyAs = (Classify)Enum.Parse(typeof(Classify), row.Cells[1].Value.ToString()),
                        SenderEmailAddress = new FocusedInboxOverrideSender(row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString())
                    };

                    try
                    {
                        await viewerHelper.AddOrUpdateFocusedInboxOverrideAsync(newOverride);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            UseWaitCursor = false;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
