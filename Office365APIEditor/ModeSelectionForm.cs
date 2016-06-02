// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class ModeSelectionForm : Form
    {
        string mode = "";

        public ModeSelectionForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(out string Mode)
        {
            this.ShowDialog();

            Mode = mode;
        }

        private void button_OK_Click(object sender, System.EventArgs e)
        {
            if (radioButton_EditorMode.Checked)
            {
                mode = "Editor";
            }
            else
            {
                mode = "MailboxViewer";
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
