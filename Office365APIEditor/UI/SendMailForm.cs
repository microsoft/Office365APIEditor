// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Microsoft.Identity.Client;
using Office365APIEditor.ViewerHelper;
using Office365APIEditor.ViewerHelper.Attachments;

namespace Office365APIEditor.UI
{
    public partial class SendMailForm : Form
    {
        PublicClientApplication pca;
        FolderInfo targetFolder;
        string targetFolderDisplayName;
        string draftItemId;

        IAccount currentUser;
        private ViewerHelper.ViewerHelper viewerHelper;

        List<FileAttachment> attachments;

        public SendMailForm(PublicClientApplication PCA, IAccount CurrentUser)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
            draftItemId = "";
        }

        public SendMailForm(PublicClientApplication PCA, IAccount CurrentUser, string DraftItemId)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
            draftItemId = DraftItemId;
        }

        public SendMailForm(PublicClientApplication PCA, IAccount CurrentUser, FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
            draftItemId = "";
        }

        private async void SendMailForm_LoadAsync(object sender, EventArgs e)
        {
            attachments = new List<FileAttachment>();

            comboBox_Importance.SelectedIndex = 1;
            comboBox_BodyType.SelectedIndex = 0;

            if (draftItemId != "")
            {
                // Editing a draft item.

                button_Attachments.Enabled = false;

                // When sending a draft item, it must be saved to SentItems.
                checkBox_SaveToSentItems.Checked = true;
                checkBox_SaveToSentItems.Enabled = false;

                viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);
                var draftItem = await viewerHelper.GetDraftMessageAsync(draftItemId);

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
                        checkBox_RequestDeliveryReceipt.Checked = draftItem.RequestDeliveryReceipt; ;
                    }));
                }
                else
                {
                    checkBox_RequestDeliveryReceipt.Checked = draftItem.RequestDeliveryReceipt;
                }
                
                if (checkBox_RequestReadReceipt.InvokeRequired)
                {
                    checkBox_RequestReadReceipt.Invoke(new MethodInvoker(delegate
                    {
                        checkBox_RequestReadReceipt.Checked = draftItem.RequestReadReceipt;
                    }));
                }
                else
                {
                    checkBox_RequestReadReceipt.Checked = draftItem.RequestReadReceipt;
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
                        textBox_Body.Text = draftItem.Body;
                    }));
                }
                else
                {
                    textBox_Body.Text = draftItem.Body;
                }

                if (comboBox_BodyType.InvokeRequired)
                {
                    comboBox_BodyType.Invoke(new MethodInvoker(delegate
                    {
                        comboBox_BodyType.SelectedIndex = (int)draftItem.BodyType;
                    }));
                }
                else
                {
                    comboBox_BodyType.SelectedIndex = (int)draftItem.BodyType;
                }

                var attachList = await viewerHelper.GetAttachmentsAsync(FolderContentType.Message, draftItemId);

                foreach (var attach in attachList)
                {
                    Dictionary<string, string> fullAttachInfo = await viewerHelper.GetAttachmentAsync(FolderContentType.Message, draftItemId, attach.Id);

                    string tempType = "";
                    string tempName = "";
                    string tempContentBytes = "";
                    
                    foreach (KeyValuePair<string, string> item in fullAttachInfo)
                    {
                        if (item.Key == "@odata.type")
                        {
                            tempType = (item.Value == null) ? "" : item.Value.ToString();
                        }
                        else if (item.Key == "Name")
                        {
                            tempName = (item.Value == null) ? "" : item.Value.ToString();
                        }
                        else if (item.Key == "ContentBytes")
                        {
                            tempContentBytes = (item.Value == null) ? "" : item.Value.ToString();
                        }
                    }

                    if (tempType == "#Microsoft.OutlookServices.FileAttachment")
                    {
                        // This is a FileAttachment

                        attachments.Add(new FileAttachment(tempName, tempContentBytes));
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

        private string RecipientsString(MailAddressCollection mailAddresses)
        {
            List<string> mails = new List<string>();

            foreach (var item in mailAddresses)
            {
                mails.Add(item.Address);
            }

            return string.Join("; ", mails.ToArray());
        }

        private async void Button_Send_ClickAsync(object sender, EventArgs e)
        {
            // Send new mail.

            Enabled = false;

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

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
                    await viewerHelper.SendMailAsync(newItem, checkBox_SaveToSentItems.Checked);
                }
                else
                {
                    // This is a draft message.
                    // Update then send it.

                    await viewerHelper.UpdateDraftAsync(draftItemId, newItem);
                    await viewerHelper.SendMailAsync(draftItemId);
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

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

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
                    await viewerHelper.SaveDraftAsync(newItem);
                }
                else
                {
                    // This is a draft message.
                    await viewerHelper.UpdateDraftAsync(draftItemId, newItem);
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
                MailAddressCollection toMailAddresses = new MailAddressCollection();

                if (textBox_To.Text != "")
                {
                    foreach (var recipient in textBox_To.Text.Split(','))
                    {
                        toMailAddresses.Add(recipient);
                    }
                }

                newItem.ToRecipients = toMailAddresses;

                MailAddressCollection ccMailAddresses = new MailAddressCollection();

                if (textBox_Cc.Text != "")
                {
                    foreach (var recipient in textBox_Cc.Text.Split(','))
                    {
                        ccMailAddresses.Add(recipient);
                    }
                }

                newItem.CcRecipients = ccMailAddresses;

                MailAddressCollection bccMailAddresses = new MailAddressCollection();

                if (textBox_Bcc.Text != "")
                {
                    foreach (var recipient in textBox_Bcc.Text.Split(','))
                    {
                        bccMailAddresses.Add(recipient);
                    }
                }

                newItem.BccRecipients = bccMailAddresses;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid recipients. " + ex.Message, ex);
            }

            newItem.Subject = textBox_Subject.Text;
            newItem.BodyType = (BodyType)comboBox_BodyType.SelectedIndex;
            newItem.Body = textBox_Body.Text;
            newItem.Importance = (Importance)comboBox_Importance.SelectedIndex;
            newItem.RequestDeliveryReceipt = checkBox_RequestDeliveryReceipt.Checked;
            newItem.RequestReadReceipt = checkBox_RequestReadReceipt.Checked;
            newItem.Attachments = attachments;
            newItem.SaveToSentItems = checkBox_SaveToSentItems.Checked;

            return newItem;
        }

        private void Button_Attachments_Click(object sender, EventArgs e)
        {
            NewAttachmentForm newAttachmentForm = new NewAttachmentForm(attachments);
            
            if (newAttachmentForm.ShowDialog(out List<FileAttachment> newAttachments) == DialogResult.OK)
            {
                attachments = newAttachments;
            }
        }
    }
}
