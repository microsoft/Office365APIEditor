// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.Office365.OutlookServices;
using Office365APIEditor.UI;
using Office365APIEditor.UI.FocusedInbox;
using Office365APIEditor.ViewerHelper.Tasks;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class MailboxViewerForm : Form
    {
        PublicClientApplication pca;
        AuthenticationResult ar;
        ViewerHelper.ViewerHelper viewerHelper;

        // Current user's info.
        IAccount currentUser;

        IMailFolder draftsFolder;

        bool doubleClicked = false;

        bool expandingNodeHasDummyNode = false;

        public bool requestFormOpened = false;

        public MailboxViewerForm()
        {
            InitializeComponent();
        }

        private void MailboxViewerForm_Load(object sender, System.EventArgs e)
        {
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
            if (acuireViewerTokenForm.ShowDialog(out pca, out ar) != DialogResult.OK)
            {
                return false;
            }

            string token = ar.AccessToken;
            currentUser = ar.Account;

            try
            {
                viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

                // Get the Drafts folder.
                GetDraftsFolderAsync();

                // Get the root folder.
                PrepareMsgFolderRootAsync();
                
                // Create Calendar dummy root folder.
                PrepareCalendarRootFolder();

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
            draftsFolder = await viewerHelper.GetDraftsFolderAsync();
        }

        private async void PrepareMsgFolderRootAsync()
        {
            // Get MsgFolderRoot and add it to the tree.

            try
            {
                var msgFolderRoot = await viewerHelper.GetMsgFolderRootAsync();

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

            var childMailFolders = await viewerHelper.GetAllChildMailFolderAsync(FolderId);

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

            var childContactFolders = await viewerHelper.GetAllChildContactFolderAsync(FolderId);

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

        private void PrepareCalendarRootFolder()
        {
            // Calendar object has no ParentID or ChildFolders.
            // So we use DummyCalendarRoot node as a parent folder of calendar folders.

            // Make a dummy node.
            TreeNode dummyCalendarRootNode = new TreeNode("Calendar Folders (Dummy Folder)")
            {
                Tag = new FolderInfo() { ID = "", Type = FolderContentType.DummyCalendarRoot },
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

        private async void PrepareCalendarFoldersAsync(TreeNode FolderNode)
        {
            // Get all Calendars, and add them to the tree.

            try
            {
                var calendars = await viewerHelper.GetCalendarFoldersAsync();

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
            TreeNode dummyTaskGroupRootNode = new TreeNode("Task Groups (Dummy Folder)")
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

            var taskGroups = await viewerHelper.GetAllTaskGroupAsync();

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

            var taskFolders = await viewerHelper.GetAllTaskFoldersAsync(TaskGroupId);

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

        private void treeView_Mailbox_AfterSelect(object sender, TreeViewEventArgs e)
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

            switch (info.Type)
            {
                case FolderContentType.Message:
                case FolderContentType.MsgFolderRoot:
                case FolderContentType.Drafts:
                    GetMessageFolderProps(info.ID, treeView_Mailbox.SelectedNode.Text);
                    break;
                case FolderContentType.Contact:
                    GetContactFolderProps(info.ID);
                    break;
                case FolderContentType.Calendar:
                    GetCalendarFolderProps(info.ID);
                    break;
                case FolderContentType.TaskGroup:
                    GetTaskGroupPropsAsync(info.ID);
                    break;
                case FolderContentType.Task:
                    GetTaskFolderPropsAsync(info.ID);
                    break;
                default:
                    break;
            }
        }

        private async void GetMessageFolderProps(string FolderId, string FolderDisplayName)
        {
            // Get the folder.
            IMailFolder mailFolderResult = new MailFolder();

            try
            {
                mailFolderResult = await viewerHelper.GetMailFolderAsync(FolderId, FolderDisplayName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }

            // Add columns.
            dataGridView_FolderProps.Columns.Add("Property", "Property");
            dataGridView_FolderProps.Columns.Add("Value", "Value");
            dataGridView_FolderProps.Columns.Add("Type", "Type");

            // Add rows.

            DataGridViewRow propChildFolderCount = new DataGridViewRow();
            propChildFolderCount.CreateCells(dataGridView_FolderProps, new object[] { "ChildFolderCount", mailFolderResult.ChildFolderCount.Value, mailFolderResult.ChildFolderCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propChildFolderCount);

            DataGridViewRow propDisplayName = new DataGridViewRow();
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "DisplayName", mailFolderResult.DisplayName, mailFolderResult.DisplayName.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", mailFolderResult.Id, mailFolderResult.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "ParentFolderId", mailFolderResult.ParentFolderId, mailFolderResult.ParentFolderId.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);

            DataGridViewRow propTotalItemCount = new DataGridViewRow();
            propTotalItemCount.CreateCells(dataGridView_FolderProps, new object[] { "TotalItemCount", mailFolderResult.TotalItemCount, mailFolderResult.TotalItemCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propTotalItemCount);

            DataGridViewRow propUnreadItemCount = new DataGridViewRow();
            propUnreadItemCount.CreateCells(dataGridView_FolderProps, new object[] { "UnreadItemCount", mailFolderResult.UnreadItemCount, mailFolderResult.UnreadItemCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propUnreadItemCount);
        }

        private async void GetContactFolderProps(string FolderId)
        {
            // Get the folder.
            IContactFolder contactFolderResult = new ContactFolder();

            try
            {
                contactFolderResult = await viewerHelper.GetContactFolderAsync(FolderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }


            // Add columns.
            dataGridView_FolderProps.Columns.Add("Property", "Property");
            dataGridView_FolderProps.Columns.Add("Value", "Value");
            dataGridView_FolderProps.Columns.Add("Type", "Type");

            // Add rows.

            DataGridViewRow propDisplayName = new DataGridViewRow();
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "DisplayName", contactFolderResult.DisplayName, contactFolderResult.DisplayName.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", contactFolderResult.Id, contactFolderResult.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "ParentFolderId", contactFolderResult.ParentFolderId, contactFolderResult.ParentFolderId.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);
        }

        private async void GetCalendarFolderProps(string FolderId)
        {
            // Get the folder.
            ICalendar calendarFolderResults = new Calendar();

            try
            {
                calendarFolderResults = await viewerHelper.GetCalendarAsync(FolderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }

            // Add columns.
            dataGridView_FolderProps.Columns.Add("Property", "Property");
            dataGridView_FolderProps.Columns.Add("Value", "Value");
            dataGridView_FolderProps.Columns.Add("Type", "Type");

            // Add rows.

            DataGridViewRow propChangeKey = new DataGridViewRow();
            propChangeKey.CreateCells(dataGridView_FolderProps, new object[] { "ChangeKey", calendarFolderResults.ChangeKey, calendarFolderResults.ChangeKey.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propChangeKey);

            DataGridViewRow propColor = new DataGridViewRow();
            propColor.CreateCells(dataGridView_FolderProps, new object[] { "Color", calendarFolderResults.Color.ToString(), calendarFolderResults.Color.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propColor);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", calendarFolderResults.Id, calendarFolderResults.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propName = new DataGridViewRow();
            propName.CreateCells(dataGridView_FolderProps, new object[] { "Name", calendarFolderResults.Name, calendarFolderResults.Name.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propName);
        }

        private async void GetTaskGroupPropsAsync(string TaskGroupId)
        {
            // Get the folder.
            TaskGroup taskGroupResult = new TaskGroup();

            try
            {
                taskGroupResult = await viewerHelper.GetTaskGroupAsync(TaskGroupId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }

            // Add columns.
            dataGridView_FolderProps.Columns.Add("Property", "Property");
            dataGridView_FolderProps.Columns.Add("Value", "Value");
            dataGridView_FolderProps.Columns.Add("Type", "Type");

            // Add rows.

            DataGridViewRow propChildFolderCount = new DataGridViewRow();
            propChildFolderCount.CreateCells(dataGridView_FolderProps, new object[] { "ChangeKey", taskGroupResult.ChangeKey, taskGroupResult.ChangeKey.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propChildFolderCount);

            DataGridViewRow propDisplayName = new DataGridViewRow();
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "GroupKey", taskGroupResult.GroupKey, taskGroupResult.GroupKey.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", taskGroupResult.Id, taskGroupResult.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "IsDefaultGroup", taskGroupResult.IsDefaultGroup, taskGroupResult.IsDefaultGroup.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);

            DataGridViewRow propTotalItemCount = new DataGridViewRow();
            propTotalItemCount.CreateCells(dataGridView_FolderProps, new object[] { "Name", taskGroupResult.Name, taskGroupResult.Name.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propTotalItemCount);
        }

        private async void GetTaskFolderPropsAsync(string TaskFolderId)
        {
            // Get the folder.
            TaskFolder taskFolderResult = new TaskFolder();

            try
            {
                taskFolderResult = await viewerHelper.GetTaskFolderAsync(TaskFolderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor");
                return;
            }

            // Add columns.
            dataGridView_FolderProps.Columns.Add("Property", "Property");
            dataGridView_FolderProps.Columns.Add("Value", "Value");
            dataGridView_FolderProps.Columns.Add("Type", "Type");

            // Add rows.

            DataGridViewRow propChildFolderCount = new DataGridViewRow();
            propChildFolderCount.CreateCells(dataGridView_FolderProps, new object[] { "ChangeKey", taskFolderResult.ChangeKey, taskFolderResult.ChangeKey.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propChildFolderCount);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", taskFolderResult.Id, taskFolderResult.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "IsDefaultFolder", taskFolderResult.IsDefaultFolder, taskFolderResult.IsDefaultFolder.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);

            DataGridViewRow propTotalItemCount = new DataGridViewRow();
            propTotalItemCount.CreateCells(dataGridView_FolderProps, new object[] { "Name", taskFolderResult.Name, taskFolderResult.Name.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propTotalItemCount);
            
            DataGridViewRow propDisplayName = new DataGridViewRow();
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "ParentGroupKey", taskFolderResult.ParentGroupKey, taskFolderResult.ParentGroupKey.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);
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
                if (folderInfo.Type == FolderContentType.DummyCalendarRoot)
                {
                    expandingNodeHasDummyNode = true;

                    PrepareCalendarFoldersAsync(e.Node);

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
            FolderViewerForm folderViewerForm = new FolderViewerForm(pca, currentUser, (FolderInfo)SelectedNode.Tag, SelectedNode.Text);
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
            viewerHelper = null;
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
                SendMailForm sendMailForm = new SendMailForm(pca, currentUser, (FolderInfo)selectedNode.Tag, selectedNode.Text);
                sendMailForm.Show(this);
            }
            else
            {
                SendMailForm sendMailForm = new SendMailForm(pca, currentUser);
                sendMailForm.Show(this);
            }
        }

        private void FocusedInboxOverridesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FocusedInboxOverrideForm focusedInboxOverrideForm = new FocusedInboxOverrideForm(pca, currentUser);
            focusedInboxOverrideForm.Show(this);
        }

        private void versionInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VersionInformationForm versionInformation = new VersionInformationForm();
            versionInformation.Owner = this;
            versionInformation.ShowDialog();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdateForm checkUpdateForm = new CheckUpdateForm();
            checkUpdateForm.Owner = this;
            checkUpdateForm.ShowDialog();
        }
    }
}
