// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointAdminConsentSettingPage : UserControl, IAccessTokenWizardPage
    {
        public V1EndpointAdminConsentSettingPage()
        {
            InitializeComponent();
        }

        private void V1EndpointAdminConsentSettingPage_Load(object sender, EventArgs e)
        {
            foreach (string item in Util.ResourceNames)
            {
                comboBox_Resource.Items.Add(item);
            }

            comboBox_Resource.SelectedIndex = 1;
        }

        public void NextButtonAction()
        {
            if (ValidateV1AdminConsentParam())
            {
                string authorizationCode = AcquireV1AdminConsentAuthorizationCode();

                if (authorizationCode == "")
                {
                    return;
                }

                MessageBox.Show("Admin Consent completed.", "Office365APIEditor");
            }
        }

        private bool ValidateV1AdminConsentParam()
        {
            if (textBox_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
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

        private string AcquireV1AdminConsentAuthorizationCode()
        {

            GetCodeForm getCodeForm = new GetCodeForm(textBox_ClientID.Text, textBox_RedirectUri.Text, Util.ConvertResourceNameToUri(comboBox_Resource.SelectedItem.ToString()), false, true);

            if (getCodeForm.ShowDialog(out string Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }
    }
}