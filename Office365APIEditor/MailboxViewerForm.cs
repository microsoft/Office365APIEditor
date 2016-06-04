// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.Office365.OutlookServices;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.OData.Client;
using System.Drawing;

namespace Office365APIEditor
{
    public partial class MailboxViewerForm : Form
    {
        PublicClientApplication pca;
        AuthenticationResult ar;
        OutlookServicesClient client;

        string email;

        string inboxId;
        string topOfInformationStoreId;
        string msgFolderRootId;

        TreeNode msgFolderRootNode;

        bool doubleClicked = false;

        bool expandingNodeHasDummyNode = false;

        public MailboxViewerForm()
        {
            InitializeComponent();
        }

        private void MailboxViewerForm_Load(object sender, System.EventArgs e)
        {
            // Use MSAL and acquire access token.

            AcquireViewerTokenForm acuireViewerTokenForm = new AcquireViewerTokenForm();
            if (acuireViewerTokenForm.ShowDialog(out pca, out ar) != DialogResult.OK)
            {
                Close();
                return;
            }

            string token = ar.Token;
            email = ar.User.DisplayableId; // DisplayableId is UPN format.

            try
            {
                client = new OutlookServicesClient(new Uri("https://outlook.office.com/api/v2.0"),
                    () =>
                    {
                        return Task.Run(() =>
                        {
                            return token;
                        });
                    });
                
                client.Context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(
                    (eventSender, eventArgs) => InsertXAnchorMailboxHeader(eventSender, eventArgs, email));

                // フォルダー階層を動的に取得するパターン。

                // Get the root folder.
                GetRootFolder();

                // Get CalendarFolders (Calendars)
                GetCalendarFolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR retrieving folders: {0}", ex.Message, "Office365APIEditor"));
            }
        }

        private void InsertXAnchorMailboxHeader(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
        }

        private void GetRootFolder()
        {
            // https://outlook.office.com/api/v1.0/me/RootFolder/?$select=ParentFolderId

            // Get the folder ID of the parent folder of parent folder of Inbox.
            // It's MsgFolderRoot.

            // We can't get the Top of Information Store folder directly.
            // Following operation is available with v1.0 only.
            // https://outlook.office.com/api/v1.0/me/RootFolder

            var inbox = client.Me.MailFolders["Inbox"].ExecuteAsync().Result; // Inbox
            inboxId = inbox.Id;

            var topOfInformationStore = client.Me.MailFolders[inbox.ParentFolderId].ExecuteAsync().Result; // Top of information store
            topOfInformationStoreId = topOfInformationStore.Id;

            var msgFolderRoot = client.Me.MailFolders[topOfInformationStore.ParentFolderId].ExecuteAsync().Result; // MsgFolderRoot
            msgFolderRootId = msgFolderRoot.Id;

            TreeNode node = new TreeNode("MsgFolderRoot");
            node.Tag = new FolderInfo() { ID = msgFolderRoot.Id, Type = FolderContentType.Message, Expanded = false };
            node.ContextMenuStrip = contextMenuStrip_FolderTreeNode;        
            node.Nodes.Add(new TreeNode()); // Add a dummy node.
            
            msgFolderRootNode = node;

            if (treeView_Mailbox.InvokeRequired)
            {
                treeView_Mailbox.Invoke(new MethodInvoker(delegate { treeView_Mailbox.Nodes.Add(node); }));
            }
            else
            {
                treeView_Mailbox.Nodes.Add(node);
            }
        }

        private async void GetChildMailFolders(string FolderId, TreeNode FolderNode)
        {
            var childMailFolderResults = await client.Me.MailFolders[FolderId].ChildFolders
                .OrderBy(m => m.DisplayName)
                .Take(50)
                .Select(m => new { m.Id, m.DisplayName, m.ChildFolderCount })
                .ExecuteAsync();

            bool morePages = false;

            do
            {
                foreach (var folder in childMailFolderResults.CurrentPage)
                {
                    TreeNode node = new TreeNode(folder.DisplayName);
                    node.Tag = new FolderInfo() { ID = folder.Id, Type = FolderContentType.Message, Expanded = false };
                    node.ContextMenuStrip = contextMenuStrip_FolderTreeNode;

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
        }
        
        private async void GetChildContactFolders(string FolderId, TreeNode FolderNode)
        {
            FolderInfo folderInfo = (FolderInfo)FolderNode.Tag;

            var childContactFolderResults = await client.Me.ContactFolders[FolderId].ChildFolders
                .OrderBy(f => f.DisplayName)
                .Take(50)
                .Select(f => new { f.Id, f.DisplayName })
                .ExecuteAsync();

            bool morePages = false;

            if (childContactFolderResults.CurrentPage.Count == 0)
            {
                if (expandingNodeHasDummyNode)
                {
                    // Remove a dummy node.
                    FolderNode.Nodes[0].Remove();
                    expandingNodeHasDummyNode = false;
                }

                return;
            }

            do
            {
                foreach (var folder in childContactFolderResults.CurrentPage)
                {
                    TreeNode node = new TreeNode(folder.DisplayName);
                    node.Tag = new FolderInfo() { ID = folder.Id, Type = FolderContentType.Contact, Expanded = false };
                    node.ContextMenuStrip = contextMenuStrip_FolderTreeNode;
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
        }

        private void GetCalendarFolders()
        {
            // Calendar object has no ParentID or ChildFolders.
            // So we use DummyCalendarRoot node as a parent folder of calendar folders.
            // We can get all calendar folders in user's mailbox at once.

            // Make a dummy node.
            TreeNode dummyCalendarRootNode = new TreeNode("Calendar Folders (Dummy Folder)");
            dummyCalendarRootNode.Tag = new FolderInfo() { ID = "", Type = FolderContentType.DummyCalendarRoot };
            dummyCalendarRootNode.ContextMenuStrip = null;

            if (treeView_Mailbox.InvokeRequired)
            {
                treeView_Mailbox.Invoke(new MethodInvoker(delegate { treeView_Mailbox.Nodes.Add(dummyCalendarRootNode); }));
            }
            else
            {
                treeView_Mailbox.Nodes.Add(dummyCalendarRootNode);
            }

            var calendarFolderResults = client.Me.Calendars
                .OrderBy(c => c.Name)
                .Take(50)
                .Select(c => new { c.Id, c.Name })
                .ExecuteAsync()
                .Result;

            bool morePages = false;

            do
            {
                try
                {
                    foreach (var folder in calendarFolderResults.CurrentPage)
                    {
                        TreeNode node = new TreeNode(folder.Name);
                        node.Tag = new FolderInfo() { ID = folder.Id, Type = FolderContentType.Calendar };
                        node.ContextMenuStrip = contextMenuStrip_FolderTreeNode;

                        if (treeView_Mailbox.InvokeRequired)
                        {
                            treeView_Mailbox.Invoke(new MethodInvoker(delegate { dummyCalendarRootNode.Nodes.Add(node); }));
                        }
                        else
                        {
                            dummyCalendarRootNode.Nodes.Add(node);
                        }
                    }

                    if (calendarFolderResults.MorePagesAvailable)
                    {
                        morePages = true;
                        calendarFolderResults = calendarFolderResults.GetNextPageAsync().Result;
                    }
                    else
                    {
                        morePages = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } while (morePages);
        }

        private async Task<OutlookServicesClient> GetOutlookServiceClient()
        {
            // Acquire access token again.
            try
            {
                ar = await pca.AcquireTokenSilentAsync(Office365APIEditorHelper.MailboxViewerScopes());
            }
            catch
            {
                try
                {
                    ar = await pca.AcquireTokenAsync(Office365APIEditorHelper.MailboxViewerScopes(), email, UiOptions.ForceLogin, "");
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Office365APIEditor");

                    return null;
                }
            }

            string token = ar.Token;
            
            OutlookServicesClient newClient = new OutlookServicesClient(new Uri("https://outlook.office.com/api/v2.0"),
                () =>
                {
                    return Task.Run(() =>
                    {
                        return token;
                    });
                });

            newClient.Context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(
                (eventSender, eventArgs) => InsertXAnchorMailboxHeader(eventSender, eventArgs, email));

            return newClient;
        }

        private async void treeView_Mailbox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get new OutlookServiceClient.
            client = await GetOutlookServiceClient();
            if (client == null)
            {
                MessageBox.Show("Acquiring access token failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            FolderInfo info = (FolderInfo)treeView_Mailbox.SelectedNode.Tag;

            switch (info.Type)
            {
                case FolderContentType.Message:
                    GetMessageFolderProps(info.ID, treeView_Mailbox.SelectedNode.Text);
                    break;
                case FolderContentType.Contact:
                    GetContactFolderProps(info.ID, treeView_Mailbox.SelectedNode.Text);
                    break;
                case FolderContentType.Calendar:
                    GetCalendarFolderProps(info.ID, treeView_Mailbox.SelectedNode.Text);
                    break;
                default:
                    break;
            }
        }

        private async void GetMessageFolderProps(string FolderId, string FolderDisplayName)
        {
            // Get the folder.
            IMailFolder mailFolderResults = new MailFolder();

            try
            {
                mailFolderResults = await client.Me.MailFolders[FolderId].ExecuteAsync();
            }
            catch (Microsoft.OData.Core.ODataErrorException ex)
            {
                // We know that we can't get RSS Feeds folder.
                // But we can get the folder using DisplayName Filter.

                if (ex.Error.ErrorCode == "ErrorItemNotFound")
                {
                    var tempResults = await client.Me.MailFolders
                        .Where(m => m.DisplayName == FolderDisplayName)
                        .Take(2)
                        .ExecuteAsync();

                    if (tempResults.CurrentPage.Count != 1)
                    {
                        // We have to get a unique folder.
                        MessageBox.Show(ex.Message, "Office365APIEditor");
                        return;
                    }

                    mailFolderResults = tempResults.CurrentPage[0];
                }
                else
                {
                    MessageBox.Show(ex.Error.ErrorCode);
                    return;
                }
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
            propChildFolderCount.CreateCells(dataGridView_FolderProps, new object[] { "ChildFolderCount", mailFolderResults.ChildFolderCount.Value, mailFolderResults.ChildFolderCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propChildFolderCount);

            DataGridViewRow propDisplayName = new DataGridViewRow();
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "DisplayName", mailFolderResults.DisplayName, mailFolderResults.DisplayName.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", mailFolderResults.Id, mailFolderResults.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "ParentFolderId", mailFolderResults.ParentFolderId, mailFolderResults.ParentFolderId.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);

            DataGridViewRow propTotalItemCount = new DataGridViewRow();
            propTotalItemCount.CreateCells(dataGridView_FolderProps, new object[] { "TotalItemCount", mailFolderResults.TotalItemCount, mailFolderResults.TotalItemCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propTotalItemCount);

            DataGridViewRow propUnreadItemCount = new DataGridViewRow();
            propUnreadItemCount.CreateCells(dataGridView_FolderProps, new object[] { "UnreadItemCount", mailFolderResults.UnreadItemCount, mailFolderResults.UnreadItemCount.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propUnreadItemCount);
        }

        private async void GetContactFolderProps(string FolderId, string FolderDisplayName)
        {
            // Get the folder.
            IContactFolder contactFolderResults = new ContactFolder();

            try
            {
                contactFolderResults = await client.Me.ContactFolders[FolderId].ExecuteAsync();
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
            propDisplayName.CreateCells(dataGridView_FolderProps, new object[] { "DisplayName", contactFolderResults.DisplayName, contactFolderResults.DisplayName.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propDisplayName);

            DataGridViewRow propId = new DataGridViewRow();
            propId.CreateCells(dataGridView_FolderProps, new object[] { "Id", contactFolderResults.Id, contactFolderResults.Id.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propId);

            DataGridViewRow propParentFolderId = new DataGridViewRow();
            propParentFolderId.CreateCells(dataGridView_FolderProps, new object[] { "ParentFolderId", contactFolderResults.ParentFolderId, contactFolderResults.ParentFolderId.GetType().ToString() });
            dataGridView_FolderProps.Rows.Add(propParentFolderId);
        }

        private async void GetCalendarFolderProps(string FolderId, string FolderDisplayName)
        {
            // Get the folder.
            ICalendar calendarFolderResults = new Calendar();

            try
            {
                calendarFolderResults = await client.Me.Calendars[FolderId].ExecuteAsync();
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

            if (info.Type == FolderContentType.DummyCalendarRoot)
            {
                // This is a dummy node. We should do nothing.
                return;
            }

            // Open selected folder.
            OpenFolder(e.Node);
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

            if (folderInfo.Type != FolderContentType.Calendar && folderInfo.Type != FolderContentType.DummyCalendarRoot && folderInfo.Expanded == false)
            {
                expandingNodeHasDummyNode = true;

                GetChildMailFolders(folderInfo.ID, e.Node);
                GetChildContactFolders(folderInfo.ID, e.Node);

                folderInfo.Expanded = true;
                e.Node.Tag = folderInfo;
            }
        }

        private void OpenFolder(TreeNode SelectedNode)
        {
            // Open selected folder.
            FolderViewerForm folderViewerForm = new FolderViewerForm(pca, email, (FolderInfo)SelectedNode.Tag, SelectedNode.Text);
            folderViewerForm.Show();
        }
    }
}
