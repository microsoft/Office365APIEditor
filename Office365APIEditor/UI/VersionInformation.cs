// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class VersionInformation : Form
    {
        public VersionInformation()
        {
            InitializeComponent();
        }

        private void VersionInformation_Load(object sender, EventArgs e)
        {
            Version productVersion = Version.Parse(Application.ProductVersion);
            string friendlyVersion = "";

            if (productVersion.Revision == 0)
            {
                friendlyVersion = productVersion.ToString(3);
            }
            else
            {
                friendlyVersion = productVersion.ToString(4);
            }

            string debugIndicator = "";

#if DEBUG
            debugIndicator += " (DEBUG)";
#endif

            label_Version.Text += friendlyVersion + debugIndicator;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
