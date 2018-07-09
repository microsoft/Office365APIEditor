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

        IUser currentUser;
        private ViewerHelper.ViewerHelper viewerHelper;

        List<FileAttachment> attachments;

        public SendMailForm()
        {
            InitializeComponent();
        }

        public SendMailForm(PublicClientApplication PCA, IUser CurrentUser)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = new FolderInfo();
            targetFolderDisplayName = null;
        }

        public SendMailForm(PublicClientApplication PCA, IUser CurrentUser, FolderInfo TargetFolderInfo, string TargetFolderDisplayName)
        {
            InitializeComponent();

            pca = PCA;
            currentUser = CurrentUser;
            targetFolder = TargetFolderInfo;
            targetFolderDisplayName = TargetFolderDisplayName;
        }

        private void SendMailForm_Load(object sender, EventArgs e)
        {
            attachments = new List<FileAttachment>();

            comboBox_Importance.SelectedIndex = 1;
            comboBox_BodyType.SelectedIndex = 0;
        }

        private async void Button_Send_ClickAsync(object sender, EventArgs e)
        {
            // Send new mail.

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

            NewEmailMessage newItem;

            try
            {
                newItem = CreateNewEmailMessageObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                await viewerHelper.SendMailAsync(newItem, checkBox_SaveToSentItems.Checked);
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
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            // Save new mail.

            viewerHelper = new ViewerHelper.ViewerHelper(pca, currentUser);

            NewEmailMessage newItem;

            try
            {
                newItem = CreateNewEmailMessageObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await viewerHelper.SaveDraftAsync(newItem);
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
