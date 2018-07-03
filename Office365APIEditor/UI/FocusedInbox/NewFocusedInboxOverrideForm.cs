// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.FocusedInbox
{
    public partial class NewFocusedInboxOverrideForm : Form
    {
        private bool editMode = false;
        private FocusedInboxOverride originalOverride;
        private FocusedInboxOverride newOverride;

        public NewFocusedInboxOverrideForm()
        {
            // Create mode.

            InitializeComponent();
            editMode = false;
        }

        public NewFocusedInboxOverrideForm(FocusedInboxOverride OriginalOverride)
        {
            // Edit mode.

            InitializeComponent();
            originalOverride = OriginalOverride;
            editMode = true;
        }

        private void NewFocusedInboxOverrideForm_Load(object sender, EventArgs e)
        {
            if (editMode)
            {
                // Edit mode.
                comboBox_Classify.SelectedIndex = (int)originalOverride.ClassifyAs;
                textBox_Name.Text = originalOverride.SenderEmailAddress.Name;
                textBox_Address.Text = originalOverride.SenderEmailAddress.Address;
                textBox_Address.Enabled = false;
            }
            else
            {
                // Create mode.
                comboBox_Classify.SelectedIndex = 0;
            }
        }

        public DialogResult ShowDialog(out FocusedInboxOverride newFocusedInboxOverride)
        {
            DialogResult result = ShowDialog();

            newFocusedInboxOverride = newOverride;
            return result;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (FocusedInboxOverrideSender.TryCreate(textBox_Name.Text, textBox_Address.Text, out FocusedInboxOverrideSender newSender))
            {
                if (editMode)
                {
                    newOverride = new FocusedInboxOverride() { Id = originalOverride.Id, ClassifyAs = (Classify)comboBox_Classify.SelectedIndex, SenderEmailAddress = newSender };

                    if (newOverride.ClassifyAs == originalOverride.ClassifyAs && newOverride.SenderEmailAddress.Name == originalOverride.SenderEmailAddress.Name)
                    {
                        // Nothing have changed.
                        DialogResult = DialogResult.Cancel;
                        Close();
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    newOverride = new FocusedInboxOverride() { ClassifyAs = (Classify)comboBox_Classify.SelectedIndex, SenderEmailAddress = newSender };
                    DialogResult = DialogResult.OK;
                    Close();
                }

            } else
            {
                MessageBox.Show("Invalid address format.", "Office365APIEditor");
                DialogResult = DialogResult.None;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}