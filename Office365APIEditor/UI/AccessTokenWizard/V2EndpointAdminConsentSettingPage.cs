// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V2EndpointAdminConsentSettingPage : UserControl, IAccessTokenWizardPage
    {
        public V2EndpointAdminConsentSettingPage()
        {
            InitializeComponent();
        }

        private void V2EndpointAdminConsentSettingPage_Load(object sender, EventArgs e)
        {
            foreach (string item in Util.ResourceNames)
            {
                comboBox_Resource.Items.Add(item);
            }

            comboBox_Resource.SelectedIndex = 1;
        }

        public void NextButtonAction()
        {
            if (ValidateV2AdminConsentParam())
            {
                string authorizationCode = AcquireV2AdminConsentAuthorizationCode();

                if (authorizationCode == "")
                {
                    return;
                }

                MessageBox.Show("Admin Consent completed.", "Office365APIEditor");
            }
        }

        private bool ValidateV2AdminConsentParam()
        {
            if (textBox_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Application ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_RedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireV2AdminConsentAuthorizationCode()
        {
            string scope = Util.ConvertResourceNameToUri(comboBox_Resource.SelectedItem.ToString());

            if (!scope.EndsWith("/"))
            {
                scope += "/";
            }

            scope += ".default";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_ClientID.Text, textBox_RedirectUri.Text, scope, true, true);

            if (getCodeForm.ShowDialog(out string Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Unexpected response.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }
    }
}