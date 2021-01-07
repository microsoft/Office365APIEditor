// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointAppOnlyByKeySettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V1EndpointAppOnlyByKeySettingPage()
        {
            InitializeComponent();
        }

        private void V1EndpointAppOnlyByKeySettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token_Key_Auth.md");

            foreach (string item in Util.ResourceNames)
            {
                comboBox_Resource.Items.Add(item);
            }

            comboBox_Resource.SelectedIndex = 1;
        }

        private void LinkLabel_Description_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        public void NextButtonAction()
        {
            if (ValidateV1WebAppAppOnlyByKeyParam())
            {
                TokenResponse tokenResponse = AcquireV1WebAppAppOnlyAccessTokenByKey();

                if (tokenResponse != null)
                {
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString()), textBox_ClientID.Text, "", "", ""));
                }
            }
        }

        private bool ValidateV1WebAppAppOnlyByKeyParam()
        {
            // Check the form for web app (App Only by certificate).

            if (textBox_TenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (textBox_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_ClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private TokenResponse AcquireV1WebAppAppOnlyAccessTokenByKey()
        {
            // Build a POST body.
            string postBody = "grant_type=client_credentials" +
                "&resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceNameToUri(comboBox_Resource.SelectedItem.ToString())) +
                "&client_id=" + textBox_ClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_ClientSecret.Text);

            return wizard.AcquireAccessToken(postBody, "https://login.microsoftonline.com/" + textBox_TenantName.Text + "/oauth2/token");
        }
    }
}