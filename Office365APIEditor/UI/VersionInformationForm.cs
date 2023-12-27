// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class VersionInformationForm : Form
    {
        public VersionInformationForm()
        {
            InitializeComponent();
        }

        private void VersionInformation_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            label_Version.Text += Util.VersionString;

            // Read "3rd party notice.txt".
            richTextBox_3rdPartyNotice.Text = Properties.Resources._3rd_party_notice;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox_3rdPartyNotice_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // Open the clicked URL.
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {
            }
        }
    }
}
