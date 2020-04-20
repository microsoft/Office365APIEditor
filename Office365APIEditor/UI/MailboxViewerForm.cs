// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.UI;
using Office365APIEditor.UI.FocusedInbox;
using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class MailboxViewerForm : Form
    {
        ViewerRequestHelper viewerRequestHelper;

        MailFolder draftsFolder;

        bool doubleClicked = false;

        bool expandingNodeHasDummyNode = false;

        public bool requestFormOpened = false;

        public MailboxViewerForm()
        {
            InitializeComponent();
        }

        private void MailboxViewerForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            closeSessionToolStripMenuItem.Enabled = false;
            windowToolStripMenuItem.Visible = false;

            // Change window title
            Text = Util.GenerateWindowTitle("Mailbox Viewer");

            // Restore window location, size and state.

            var savedBounds = Properties.Settings.Default.MailboxViewerFormBounds;
            var savedWindowState = Properties.Settings.Default.MailboxViewerFormWindowState;

            if (savedWindowState == FormWindowState.Minimized)
            {
                // We should not restore window location, size and state.
                savedBounds = new Rectangle(0, 0, 0, 0);
                savedWindowState = FormWindowState.Normal;
            }
            else if (savedWindowState == FormWindowState.Maximized)
            {
                // We should not restore window size.
                savedBounds = new Rectangle(new Point(savedBounds.Location.X + 8, savedBounds.Y + 8), new Size(Width, Height));
            }

            if (!savedBounds.IsEmpty)
            {
                // We should not restore window size and location if it is out of screens.

                foreach (Screen screen in Screen.AllScreens)
                {
                    if (screen.WorkingArea.Contains(savedBounds))
                    {
                        Bounds = savedBounds;
                        break;
                    }
                }
            }

            WindowState = savedWindowState;
        }

        private void MailboxViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save window location and size.
            Properties.Settings.Default.MailboxViewerFormBounds = Bounds;
            Properties.Settings.Default.MailboxViewerFormWindowState = WindowState;
            Properties.Settings.Default.Save();
        }

        private bool Prepare()
        {
            // Use MSAL and acquire access token.

            AcquireViewerTokenForm acuireViewerTokenForm = new AcquireViewerTokenForm();
            if (acuireViewerTokenForm.ShowDialog(out Global.pca, out Global.LastAuthenticationResult) != DialogResult.OK)
            {
                return false;
            }

            Global.currentUser = Global.LastAuthenticationResult.Account;

            try
            {
                viewerRequestHelper = new ViewerRequestHelper();

                // Get the Drafts folder.
                GetDraftsFolderAsync();

                // Get the root folder.
                PrepareMsgFolderRootAsync();
                
                // Create Calendar dummy root folder.
                PrepareCalendarGroupRootFolder();

                // Create TaskGroup dummy root folder.
                PrepareTaskGroupRootFolder();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR retrieving folders: {0}", ex.Message, "Office365APIEditor"));

                return false;
            }
        }

        private async void GetDraftsFolderAsync()
        {
            draftsFolder = await viewerRequestHelper.GetDraftsFolderAsync();
        }

        private async void PrepareMsgFolderRootAsync()
        {
            // Get MsgFolderRoot and add it to the tree.

            try
            {
                var msgFolderRoot = await viewerRequestHelper.GetMsgFolderRootAsync();

                TreeNode node = new TreeNode("MsgFolderRoot")
                {
                    Tag = new FolderInfo() { ID = msgFolderRoot.Id, Type = FolderContentType.MsgFolderRoot, Expanded = false },
                    ContextMenuStrip = contextMenuStrip_FolderTreeNode
                };
                node.Nodes.Add(new TreeNode()); // Add a dummy node.

                if (treeView_Mailbox.InvokeRequired)
                {
                    treeView_Mailbox.Invoke(new MethodInvoker(delegate { treeView_Mailbox.Nodes.Add(node); }));
                }
                else
                {
                    treeView_Mailbox.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PrepareChildMailFoldersAsync(string FolderId, TreeNode FolderNode)
        {
            // Get all child MailFolder of specified folder, and add them to the tree.

            var childMailFolders = await viewerRequestHelper.GetAllChildMailFoldersAsync(FolderId);

            foreach (var folder in childMailFolders)
            {
                FolderContentType folderContentType;

                if (folder.Id == draftsFolder.Id)
                {
                    // This folder is the Drafts folder.
                    folderContentType = FolderContentType.Drafts;
                }
                else
                {
                    folderContentType = FolderContentType.Message;
                }

                TreeNode node = new TreeNode(folder.DisplayName)
                {
                    Tag = new FolderInfo() { ID = folder.Id, Type = folderContentType, Expanded = false },
                    ContextMenuStrip = contextMenuStrip_FolderTreeNode
                };

                if (folder.ChildFolderCount >= 1)
                {
                    node.Nodes.Add(new TreeNode()); // Add a dummy node.
                }

                if (treeView_Mailbox.InvokeRequired)
                {
                    treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                        FolderNode.Nodes.Add(node);
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }));
                }
                else
                {
                    FolderNode.Nodes.Add(node);
                    if (expandingNodeHasDummyNode)
                    {
                        // Remove a dummy node.
                        FolderNode.Nodes[0].Remove();
                        expandingNodeHasDummyNode = false;
                    }
                }
            }
        }
        
        private async void PrepareChildContactFoldersAsync(string FolderId, TreeNode FolderNode)
        {
            // Get all child contact folders of specified folder, and add them to the tree.

            var childContactFolders = await viewerRequestHelper.GetAllChildContactFoldersAsync(FolderId);

            if (childContactFolders.Count == 0)
            {
                if (expandingNodeHasDummyNode)
                {
                    // Remove a dummy node.
                    FolderNode.Nodes[0].Remove();
                    expandingNodeHasDummyNode = false;
                }

                return;
            }

            foreach (var folder in childContactFolders)
            {
                TreeNode node = new TreeNode(folder.DisplayName)
                {
                    Tag = new FolderInfo() { ID = folder.Id, Type = FolderContentType.Contact, Expanded = false },
                    ContextMenuStrip = contextMenuStrip_FolderTreeNode
                };
                node.Nodes.Add(new TreeNode()); // Add a dummy node.

                if (treeView_Mailbox.InvokeRequired)
                {
                    treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                        FolderNode.Nodes.Add(node);
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }));
                }
                else
                {
                    FolderNode.Nodes.Add(node);
                    if (expandingNodeHasDummyNode)
                    {
                        // Remove a dummy node.
                        FolderNode.Nodes[0].Remove();
                        expandingNodeHasDummyNode = false;
                    }
                }
            }
        }

        private void PrepareCalendarGroupRootFolder()
        {
            // Calendar object has no ParentID or ChildFolders.
            // So we use DummyCalendarRoot node as a parent folder of calendar folders.

            // Make a dummy node.
            TreeNode dummyCalendarRootNode = new TreeNode("Calendar Folders (Dummy Folder)")
            {
                Tag = new FolderInfo() { ID = "", Type = FolderContentType.DummyCalendarGroupRoot },
                ContextMenuStrip = null
            };
            dummyCalendarRootNode.Nodes.Add(new TreeNode()); // Add a dummy node.

            if (treeView_Mailbox.InvokeRequired)
            {
                treeView_Mailbox.Invoke(new MethodInvoker(delegate { treeView_Mailbox.Nodes.Add(dummyCalendarRootNode); }));
            }
            else
            {
                treeView_Mailbox.Nodes.Add(dummyCalendarRootNode);
            }
        }

        private async void PrepareCalendarGroupsAsync(TreeNode FolderNode)
        {
            // Get all calendar groups, and add them to the tree.

            try
            {
                var calendarGroups = await viewerRequestHelper.GetAllCalendarGroupsAsync();

                foreach (var calendarGroup in calendarGroups)
                {
                    TreeNode node = new TreeNode(calendarGroup.Name)
                    {
                        Tag = new FolderInfo() { ID = calendarGroup.Id, Type = FolderContentType.CalendarGroup },
                        ContextMenuStrip = null
                    };
                    node.Nodes.Add(new TreeNode()); // Add a dummy node.

                    if (treeView_Mailbox.InvokeRequired)
                    {
                        treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                            FolderNode.Nodes.Add(node);
                            if (expandingNodeHasDummyNode)
                            {
                                // Remove a dummy node.
                                FolderNode.Nodes[0].Remove();
                                expandingNodeHasDummyNode = false;
                            }
                        }));
                    }
                    else
                    {
                        FolderNode.Nodes.Add(node);
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PrepareCalendarFoldersAsync(string CalendarGroupId, TreeNode FolderNode)
        {
            // Get all Calendars, and add them to the tree.

            try
            {
                var calendars = await viewerRequestHelper.GetAllCalendarFoldersAsync(CalendarGroupId);

                if (calendars.Count == 0)
                {
                    if (treeView_Mailbox.InvokeRequired)
                    {
                        treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                            if (expandingNodeHasDummyNode)
                            {
                                // Remove a dummy node.
                                FolderNode.Nodes[0].Remove();
                                expandingNodeHasDummyNode = false;
                            }
                        }));
                    }
                    else
                    {
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }
                }
                else
                {
                    foreach (var calendar in calendars)
                    {
                        TreeNode node = new TreeNode(calendar.Name)
                        {
                            Tag = new FolderInfo() { ID = calendar.Id, Type = FolderContentType.Calendar },
                            ContextMenuStrip = contextMenuStrip_FolderTreeNode
                        };

                        if (treeView_Mailbox.InvokeRequired)
                        {
                            treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                                FolderNode.Nodes.Add(node);
                                if (expandingNodeHasDummyNode)
                                {
                                    // Remove a dummy node.
                                    FolderNode.Nodes[0].Remove();
                                    expandingNodeHasDummyNode = false;
                                }
                            }));
                        }
                        else
                        {
                            FolderNode.Nodes.Add(node);
                            if (expandingNodeHasDummyNode)
                            {
                                // Remove a dummy node.
                                FolderNode.Nodes[0].Remove();
                                expandingNodeHasDummyNode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrepareTaskGroupRootFolder()
        {
            // TaskGroup object has no ParentID or ChildFolders.
            // So we use DummyTaskGroupRoot node as a parent folder of TaskGroups.

            // Make a dummy node.
            string dummyNodeName = Util.UseMicrosoftGraphInMailboxViewer ? "Task Groups (Beta) (Dummy Folder)" : "Task Groups (Dummy Folder)";
            TreeNode dummyTaskGroupRootNode = new TreeNode(dummyNodeName)
            {
                Tag = new FolderInfo() { ID = "", Type = FolderContentType.DummyTaskGroupRoot },
                ContextMenuStrip = null
            };
            dummyTaskGroupRootNode.Nodes.Add(new TreeNode()); // Add a dummy node.

            if (treeView_Mailbox.InvokeRequired)
            {
                treeView_Mailbox.Invoke(new MethodInvoker(delegate { treeView_Mailbox.Nodes.Add(dummyTaskGroupRootNode); }));
            }
            else
            {
                treeView_Mailbox.Nodes.Add(dummyTaskGroupRootNode);
            }
        }

        private async void PrepareTaskGroupsAsync(TreeNode FolderNode)
        {
            // Get all TaskGroups, and add them to the tree.

            var taskGroups = await viewerRequestHelper.GetAllTaskGroupsAsync();

            foreach (var group in taskGroups)
            {
                TreeNode node = new TreeNode(group.Name)
                {
                    Tag = new FolderInfo() { ID = group.Id, Type = FolderContentType.TaskGroup, Expanded = false },
                };
                node.Nodes.Add(new TreeNode()); // Add a dummy node.

                if (treeView_Mailbox.InvokeRequired)
                {
                    treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                        FolderNode.Nodes.Add(node);
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }));
                }
                else
                {
                    FolderNode.Nodes.Add(node);
                    if (expandingNodeHasDummyNode)
                    {
                        // Remove a dummy node.
                        FolderNode.Nodes[0].Remove();
                        expandingNodeHasDummyNode = false;
                    }
                }
            }
        }

        private async void PrepareTaskFoldersAsync(string TaskGroupId, TreeNode FolderNode)
        {
            // Get all TaskFolders of specified TaskGroup, and add them to the tree.

            var taskFolders = await viewerRequestHelper.GetAllTaskFoldersAsync(TaskGroupId);

            if (taskFolders.Count == 0)
            {
                if (treeView_Mailbox.InvokeRequired)
                {
                    treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }));
                }
                else
                {
                    if (expandingNodeHasDummyNode)
                    {
                        // Remove a dummy node.
                        FolderNode.Nodes[0].Remove();
                        expandingNodeHasDummyNode = false;
                    }
                }
            }
            else
            {
                foreach (var folder in taskFolders)
                {
                    TreeNode node = new TreeNode(folder.Name)
                    {
                        Tag = new FolderInfo() { ID = folder.Id, Type = FolderContentType.Task, Expanded = true },
                        ContextMenuStrip = contextMenuStrip_FolderTreeNode
                    };

                    if (treeView_Mailbox.InvokeRequired)
                    {
                        treeView_Mailbox.Invoke(new MethodInvoker(delegate {
                            FolderNode.Nodes.Add(node);
                            if (expandingNodeHasDummyNode)
                            {
                                // Remove a dummy node.
                                FolderNode.Nodes[0].Remove();
                                expandingNodeHasDummyNode = false;
                            }
                        }));
                    }
                    else
                    {
                        FolderNode.Nodes.Add(node);
                        if (expandingNodeHasDummyNode)
                        {
                            // Remove a dummy node.
                            FolderNode.Nodes[0].Remove();
                            expandingNodeHasDummyNode = false;
                        }
                    }
                }
            }
        }

        private async void treeView_Mailbox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Show information of selected folder.

            // Reset DataGrid.
            for (int i = dataGridView_FolderProps.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView_FolderProps.Rows.RemoveAt(i);
            }

            for (int i = dataGridView_FolderProps.Columns.Count - 1; i >= 0; i--)
            {
                dataGridView_FolderProps.Columns.RemoveAt(i);
            }

            if (treeView_Mailbox.InvokeRequired)
            {
                // Another thread is working. We should do nothing.
                return;
            }

            // Get folder props.    

            if (treeView_Mailbox.SelectedNode == null || treeView_Mailbox.SelectedNode.Tag == null)
            {
                // Perhaps the user chose another folder by using the keyboard.
                return;
            }

            FolderInfo info = (FolderInfo)treeView_Mailbox.SelectedNode.Tag;

            ViewerHelper.Data.OutlookRestApiBaseObject outlookFolder;

            try
            {
                switch (info.Type)
                {
                    case FolderContentType.Message:
                    case FolderContentType.MsgFolderRoot:
                    case FolderContentType.Drafts:
                        outlookFolder = await viewerRequestHelper.GetMailFolderAsync(info.ID);
                        break;
                    case FolderContentType.Contact:
                        outlookFolder = await viewerRequestHelper.GetContactFolderAsync(info.ID);
                        break;
                    case FolderContentType.CalendarGroup:
                        outlookFolder = await viewerRequestHelper.GetCalendarGroupAsync(info.ID);
                        break;
                    case FolderContentType.Calendar:
                        outlookFolder = await viewerRequestHelper.GetCalendarAsync(info.ID);
                        break;
                    case FolderContentType.TaskGroup:
                        outlookFolder = await viewerRequestHelper.GetTaskGroupAsync(info.ID);
                        break;
                    case FolderContentType.Task:
                        outlookFolder = await viewerRequestHelper.GetTaskFolderAsync(info.ID);
                        break;
                    default:
                        return;
                }

                // Add columns.
                dataGridView_FolderProps.Columns.Add("Property", "Property");
                dataGridView_FolderProps.Columns.Add("Value", "Value");
                dataGridView_FolderProps.Columns.Add("Type", "Type");

                // Add rows.
                foreach (var item in outlookFolder.GetRawProperties())
                {
                    DataGridViewRow propTotalItemCount = new DataGridViewRow();
                    propTotalItemCount.CreateCells(dataGridView_FolderProps, new object[] { item.Key, (item.Value == null) ? "" : item.Value.ToString(), (item.Value == null) ? "null" : item.Value.GetType().ToString() });
                    dataGridView_FolderProps.Rows.Add(propTotalItemCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }
        }

        private void treeView_Mailbox_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView_Mailbox.SelectedNode = e.Node;
            }
        }

        private void treeView_Mailbox_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FolderInfo info = (FolderInfo)treeView_Mailbox.SelectedNode.Tag;

            if (info.Type == FolderContentType.MsgFolderRoot || info.Type == FolderContentType.Message || info.Type == FolderContentType.Drafts || info.Type == FolderContentType.Contact || info.Type == FolderContentType.Calendar || info.Type == FolderContentType.Task)
            {
                // This is not a dummy node.
                // Open selected folder.
                OpenFolder(e.Node);
            }
        }

        private void ToolStripMenuItem_OpenContentTable_Click(object sender, EventArgs e)
        {
            // Open selected folder.
            OpenFolder(treeView_Mailbox.SelectedNode);
        }

        private void treeView_Mailbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                doubleClicked = true;
            }
            else
            {
                doubleClicked = false;
            }
        }

        private void treeView_Mailbox_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            // Disable collapsing after double click.
            e.Cancel = doubleClicked;
            doubleClicked = false;
        }

        private void treeView_Mailbox_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (doubleClicked)
            {
                // Disable expanding after double clock.
                e.Cancel = true;
                doubleClicked = false;

                return;
            }
            
            // Get child folders.

            FolderInfo folderInfo = (FolderInfo)e.Node.Tag;            

            if (folderInfo.Expanded == false)
            {
                if (folderInfo.Type == FolderContentType.DummyCalendarGroupRoot)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareCalendarGroupsAsync(e.Node);

                    folderInfo.Expanded = true;
                    e.Node.Tag = folderInfo;
                }
                else if (folderInfo.Type == FolderContentType.CalendarGroup)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareCalendarFoldersAsync(folderInfo.ID, e.Node);

                    folderInfo.Expanded = true;
                    e.Node.Tag = folderInfo;
                }
                else if (folderInfo.Type == FolderContentType.DummyTaskGroupRoot)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareTaskGroupsAsync(e.Node);

                    folderInfo.Expanded = true;
                    e.Node.Tag = folderInfo;
                }
                else if (folderInfo.Type == FolderContentType.TaskGroup)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareTaskFoldersAsync(folderInfo.ID, e.Node);

                    folderInfo.Expanded = true;
                    e.Node.Tag = folderInfo;
                }
                else if (folderInfo.Type == FolderContentType.Contact || folderInfo.Type == FolderContentType.Message || folderInfo.Type == FolderContentType.Drafts || folderInfo.Type == FolderContentType.MsgFolderRoot)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareChildMailFoldersAsync(folderInfo.ID, e.Node);
                    PrepareChildContactFoldersAsync(folderInfo.ID, e.Node);

                    folderInfo.Expanded = true;
                    e.Node.Tag = folderInfo;
                }
            }
        }

        private void OpenFolder(TreeNode SelectedNode)
        {
            // Open selected folder.
            FolderViewerForm folderViewerForm = new FolderViewerForm((FolderInfo)SelectedNode.Tag, SelectedNode.Text);
            folderViewerForm.Show();
        }

        private void newSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Prepare())
            {
                // New session stated
                Activate();
                newSessionToolStripMenuItem.Enabled = false;
                closeSessionToolStripMenuItem.Enabled = true;
                windowToolStripMenuItem.Visible = true;
            }
        }

        private void newEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (requestFormOpened)
            {
                MessageBox.Show("Editor window is already opened.", "Office365APIEditor");
            }
            else
            {
                requestFormOpened = true;
                RequestForm requestForm = new RequestForm
                {
                    Owner = this
                };
                requestForm.Show(this);
            }
        }

        private void closeSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentSession();

            newSessionToolStripMenuItem.Enabled = true;
            closeSessionToolStripMenuItem.Enabled = false;
            windowToolStripMenuItem.Visible = false;
        }

        private void CloseCurrentSession()
        {
            Global.pca = null;
            Global.currentUser = null;
            Global.LastAuthenticationResult = null;
            viewerRequestHelper = null;
            dataGridView_FolderProps.Rows.Clear();
            dataGridView_FolderProps.Columns.Clear();
            treeView_Mailbox.Nodes.Clear();
        }

        private void accessTokenViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TokenViewer tokenView = new TokenViewer
            {
                Owner = this
            };
            tokenView.Show(this);
        }

        private void NewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_Mailbox.SelectedNode != null)
            {
                TreeNode selectedNode = treeView_Mailbox.SelectedNode;
                SendMailForm sendMailForm = new SendMailForm((FolderInfo)selectedNode.Tag, selectedNode.Text);
                sendMailForm.Show(this);
            }
            else
            {
                SendMailForm sendMailForm = new SendMailForm();
                sendMailForm.Show(this);
            }
        }

        private void FocusedInboxOverridesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FocusedInboxOverrideForm focusedInboxOverrideForm = new FocusedInboxOverrideForm();
            focusedInboxOverrideForm.Show(this);
        }

        private void versionInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VersionInformationForm versionInformation = new VersionInformationForm
            {
                Owner = this
            };
            versionInformation.ShowDialog();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdateForm checkUpdateForm = new CheckUpdateForm
            {
                Owner = this
            };
            checkUpdateForm.ShowDialog();
        }
    }
}