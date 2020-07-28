// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Data;
using Office365APIEditor.ViewerHelper.Data.AttachmentAPI;
using Office365APIEditor.ViewerHelper.Data.MailAPI;

namespace Office365APIEditor.UI
{
    public partial class SendMailForm : Form
    {
        FolderInfo targetFolder;
        string targetFolderDisplayName;
        string draftItemId;

        private ViewerRequestHelper viewerRequestHelper;

        List<FileAttachment> attachments;

        public SendMailForm()
        {
            InitializeComponent();

            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
            draftItemId = "";
        }

        public SendMailForm(string DraftItemId)
        {
            InitializeComponent();

            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
            draftItemId = DraftItemId;
        }

        public SendMailForm(FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();

            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
            draftItemId = "";
        }

        private async void SendMailForm_LoadAsync(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            attachments = new List<FileAttachment>();

            // Display the window in the center of the parent window.
            Location = new Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);

            comboBox_Importance.SelectedIndex = 1;
            comboBox_BodyType.SelectedIndex = 0;

            if (draftItemId != "")
            {
                // Editing a draft item.

                button_Attachments.Enabled = false;

                // When sending a draft item, it must be saved to SentItems.
                checkBox_SaveToSentItems.Checked = true;
                checkBox_SaveToSentItems.Enabled = false;

                viewerRequestHelper = new ViewerRequestHelper();
                NewEmailMessage draftItem;

                try
                {
                    draftItem = await viewerRequestHelper.GetDraftMessageAsync(draftItemId);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                if (comboBox_Importance.InvokeRequired)
                {
                    comboBox_Importance.Invoke(new MethodInvoker(delegate
                    {
                        comboBox_Importance.SelectedIndex = (int)draftItem.Importance;
                    }));
                }
                else
                {
                    comboBox_Importance.SelectedIndex = (int)draftItem.Importance;
                }

                if (comboBox_Importance.InvokeRequired)
                {
                    comboBox_Importance.Invoke(new MethodInvoker(delegate
                    {
                        comboBox_Importance.SelectedIndex = (int)draftItem.Importance; ;
                    }));
                }
                else
                {
                    comboBox_Importance.SelectedIndex = (int)draftItem.Importance;
                }

                if (checkBox_RequestDeliveryReceipt.InvokeRequired)
                {
                    checkBox_RequestDeliveryReceipt.Invoke(new MethodInvoker(delegate
                    {
                        checkBox_RequestDeliveryReceipt.Checked = draftItem.IsDeliveryReceiptRequested;
                    }));
                }
                else
                {
                    checkBox_RequestDeliveryReceipt.Checked = draftItem.IsDeliveryReceiptRequested;
                }
                
                if (checkBox_RequestReadReceipt.InvokeRequired)
                {
                    checkBox_RequestReadReceipt.Invoke(new MethodInvoker(delegate
                    {
                        checkBox_RequestReadReceipt.Checked = draftItem.IsReadReceiptRequested;
                    }));
                }
                else
                {
                    checkBox_RequestReadReceipt.Checked = draftItem.IsReadReceiptRequested;
                }
                
                if (textBox_To.InvokeRequired)
                {
                    textBox_To.Invoke(new MethodInvoker(delegate
                    {
                        textBox_To.Text = RecipientsString(draftItem.ToRecipients);
                    }));
                }
                else
                {
                    textBox_To.Text = RecipientsString(draftItem.ToRecipients);
                }

                if (textBox_Cc.InvokeRequired)
                {
                    textBox_Cc.Invoke(new MethodInvoker(delegate
                    {
                        textBox_Cc.Text = RecipientsString(draftItem.CcRecipients);
                    }));
                }
                else
                {
                    textBox_Cc.Text = RecipientsString(draftItem.CcRecipients);
                }

                if (textBox_Bcc.InvokeRequired)
                {
                    textBox_Bcc.Invoke(new MethodInvoker(delegate
                    {
                        textBox_Bcc.Text = RecipientsString(draftItem.BccRecipients);
                    }));
                }
                else
                {
                    textBox_Bcc.Text = RecipientsString(draftItem.BccRecipients);
                }

                if (textBox_Subject.InvokeRequired)
                {
                    textBox_Subject.Invoke(new MethodInvoker(delegate
                    {
                        textBox_Subject.Text = draftItem.Subject;
                    }));
                }
                else
                {
                    textBox_Subject.Text = draftItem.Subject;
                }

                if (textBox_Body.InvokeRequired)
                {
                    textBox_Body.Invoke(new MethodInvoker(delegate
                    {
                        textBox_Body.Text = draftItem.Body.Content;
                    }));
                }
                else
                {
                    textBox_Body.Text = draftItem.Body.Content;
                }

                if (comboBox_BodyType.InvokeRequired)
                {
                    comboBox_BodyType.Invoke(new MethodInvoker(delegate
                    {
                        comboBox_BodyType.SelectedIndex = (int)draftItem.Body.ContentType;
                    }));
                }
                else
                {
                    comboBox_BodyType.SelectedIndex = (int)draftItem.Body.ContentType;
                }

                var attachList = await viewerRequestHelper.GetAllAttachmentsAsync(FolderContentType.Message, draftItemId);

                foreach (var attach in attachList)
                {
                    Dictionary<string, string> fullAttachInfo = await viewerRequestHelper.GetAttachmentAsync(FolderContentType.Message, draftItemId, attach.Id);

                    string tempType = "";
                    string tempName = "";
                    string tempContentBytes = "";
                    
                    foreach (KeyValuePair<string, string> item in fullAttachInfo)
                    {
                        if (item.Key.ToLowerInvariant() == "@odata.type")
                        {
                            tempType = (item.Value == null) ? "" : item.Value.ToString();
                        }
                        else if (item.Key.ToLowerInvariant() == "name")
                        {
                            tempName = (item.Value == null) ? "" : item.Value.ToString();
                        }
                        else if (item.Key.ToLowerInvariant() == "contentbytes")
                        {
                            tempContentBytes = (item.Value == null) ? "" : item.Value.ToString();
                        }
                    }

                    if (tempType == (Util.UseMicrosoftGraphInMailboxViewer ? "#microsoft.graph.fileAttachment" : "#Microsoft.OutlookServices.FileAttachment"))
                    {
                        // This is a FileAttachment

                        attachments.Add(FileAttachment.CreateFromContentBytes(tempName, tempContentBytes));
                    }
                }

                if (button_Attachments.InvokeRequired)
                {
                    button_Attachments.Invoke(new MethodInvoker(delegate
                    {
                        button_Attachments.Enabled = true;
                    }));
                }
                else
                {
                    button_Attachments.Enabled = true;
                }
            }
        }

        private string RecipientsString(List<Recipient> mailAddresses)
        {
            List<string> temp = new List<string>();

            foreach (var recipient in mailAddresses)
            {
                temp.Add(recipient.EmailAddress.Address);
            }

            return string.Join("; ", temp.ToArray());
        }

        private async void Button_Send_ClickAsync(object sender, EventArgs e)
        {
            // Send new mail.

            Enabled = false;

            viewerRequestHelper = new ViewerRequestHelper();

            NewEmailMessage newItem;

            try
            {
                newItem = CreateNewEmailMessageObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }
            
            try
            {
                if (draftItemId == "")
                {
                    await viewerRequestHelper.SendMailAsync(newItem, checkBox_SaveToSentItems.Checked);
                }
                else
                {
                    // This is a draft message.
                    // Update then send it.

                    await viewerRequestHelper.UpdateDraftAsync(draftItemId, newItem);
                    await viewerRequestHelper.SendMailAsync(draftItemId);
                }

                Close();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    MessageBox.Show(response.StatusCode.ToString() + Environment.NewLine + jsonResponse + Environment.NewLine, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            // Save new mail.

            Enabled = false;

            viewerRequestHelper = new ViewerRequestHelper();

            NewEmailMessage newItem;

            try
            {
                newItem = CreateNewEmailMessageObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }

            try
            {
                if (draftItemId == "")
                {
                    await viewerRequestHelper.SaveDraftAsync(newItem);
                }
                else
                {
                    // This is a draft message.
                    await viewerRequestHelper.UpdateDraftAsync(draftItemId, newItem);
                }
                
                Close();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    MessageBox.Show(response.StatusCode.ToString() + Environment.NewLine + jsonResponse + Environment.NewLine, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }

        private NewEmailMessage CreateNewEmailMessageObject()
        {
            NewEmailMessage newItem = new NewEmailMessage();

            try
            {
                List<Recipient> toMailAddresses = new List<Recipient>();

                if (textBox_To.Text != "")
                {
                    foreach (var recipient in textBox_To.Text.Split(';'))
                    {
                        toMailAddresses.Add(new Recipient("", recipient.Trim()));
                    }
                }

                newItem.ToRecipients = toMailAddresses;

                List<Recipient> ccMailAddresses = new List<Recipient>();

                if (textBox_Cc.Text != "")
                {
                    foreach (var recipient in textBox_Cc.Text.Split(';'))
                    {
                        ccMailAddresses.Add(new Recipient("", recipient.Trim()));
                    }
                }

                newItem.CcRecipients = ccMailAddresses;

                List<Recipient> bccMailAddresses = new List<Recipient>();

                if (textBox_Bcc.Text != "")
                {
                    foreach (var recipient in textBox_Bcc.Text.Split(';'))
                    {
                        bccMailAddresses.Add(new Recipient("", recipient.Trim()));
                    }
                }

                newItem.BccRecipients = bccMailAddresses;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid recipients. " + ex.Message, ex);
            }

            newItem.Subject = textBox_Subject.Text;
            newItem.Body.ContentType = (BodyType)comboBox_BodyType.SelectedIndex;
            newItem.Body.Content = textBox_Body.Text;
            newItem.Importance = (Importance)comboBox_Importance.SelectedIndex;
            newItem.IsDeliveryReceiptRequested = checkBox_RequestDeliveryReceipt.Checked;
            newItem.IsReadReceiptRequested = checkBox_RequestReadReceipt.Checked;
            newItem.Attachments = attachments;

            return newItem;
        }

        private void Button_Attachments_Click(object sender, EventArgs e)
        {
            NewAttachmentForm newAttachmentForm = new NewAttachmentForm(attachments);
            
            if (newAttachmentForm.ShowDialog(out List<ViewerHelper.Data.AttachmentAPI.FileAttachment> newAttachments) == DialogResult.OK)
            {
                attachments = newAttachments;
            }
        }
    }
}