using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        AuthenticationResult ar;
        string email;

        public AttachmentViewerForm(PublicClientApplication PCA, string UserEmailAddress, FolderInfo TargetFolderInfo, string TargetItemID, string TargetItemSubject)
        {
            InitializeComponent();

            pca = PCA;
            email = UserEmailAddress;
            targetFolder = TargetFolderInfo;
            targetItemId = TargetItemID;
            targetItemSubject = TargetItemSubject;
        }

        private async void AttachmentViewerForm_Load(object sender, EventArgs e)
        {
            Text = "Attachments for '" + targetItemSubject + "'";

            client = await GetOutlookServiceClient();

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
                                
                                DataGridViewRow itemRow = new DataGridViewRow();
                                itemRow.Tag = attachment.Id;
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
                (eventSender, eventArgs) => InsertHeaders(eventSender, eventArgs, email));

            return newClient;
        }

        private void InsertHeaders(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
            e.RequestMessage.SetHeader("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");
        }
    }
}
