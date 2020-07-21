// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper.Data.AttachmentAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class NewAttachmentForm : Form
    {
        private List<FileAttachment> attachments;

        public NewAttachmentForm(List<FileAttachment> Attachments)
        {
            attachments = Attachments;

            InitializeComponent();
        }

        private void NewAttachmentForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            foreach (var attachment in attachments)
            {
                listBox_Attachments.Items.Add(attachment.Name);
            }
        }

        public DialogResult ShowDialog(out List<FileAttachment> SelectedAttachments)
        {
            DialogResult result = ShowDialog();

            SelectedAttachments = attachments;
            return result;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialog1.FileNames)
                {
                    listBox_Attachments.Items.Add(System.IO.Path.GetFileName(file));
                    attachments.Add(FileAttachment.CreateFromFilePath(file));
                }
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (listBox_Attachments.SelectedIndex >= 0)
            {
                bool lastItemRemoved = (listBox_Attachments.SelectedIndex == listBox_Attachments.Items.Count - 1);
                int removedIndex = listBox_Attachments.SelectedIndex;

                attachments.RemoveAt(listBox_Attachments.SelectedIndex);
                listBox_Attachments.Items.RemoveAt(listBox_Attachments.SelectedIndex);

                if (listBox_Attachments.Items.Count != 0)
                {
                    if (listBox_Attachments.Items.Count == 1)
                    {
                        listBox_Attachments.SelectedIndex = 0;
                    }
                    else if (lastItemRemoved)
                    {
                        listBox_Attachments.SelectedIndex = listBox_Attachments.Items.Count - 1;
                    }
                    else if (listBox_Attachments.Items.Count > removedIndex)
                    {
                        listBox_Attachments.SelectedIndex = removedIndex;
                    }
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            // Create new attachment collection.

            try
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}