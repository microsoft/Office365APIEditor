// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.AccessTokenUtil;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointWebAppSettingPage : UserControl, IAccessTokenWizardPage
    {
        public V1EndpointWebAppSettingPage()
        {
            InitializeComponent();
        }

        private void V1EndpointWebAppSettingPage_Load(object sender, EventArgs e)
        {
            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application.md");

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
            V1WebAppUtil v1WebAppUtil = new V1WebAppUtil()
            {
                ClientID = textBox_ClientID.Text,
                RedirectUri = textBox_RedirectUri.Text,
                Resource = Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString()),
                ClientSecret = textBox_ClientSecret.Text
            };

            ValidateResult validateResult = v1WebAppUtil.Validate();

            if (validateResult.IsValid)
            {
                AcquireAccessTokenResult acquireAccessTokenResult = v1WebAppUtil.AcquireAccessToken();

                if (acquireAccessTokenResult.Success == InteractiveResult.Fail)
                {
                    MessageBox.Show(acquireAccessTokenResult.ErrorMessage, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (acquireAccessTokenResult.Success == InteractiveResult.Cancel)
                {
                    return;
                }

                TokenResponse tokenResponse = acquireAccessTokenResult.Token;

                if (tokenResponse != null)
                {
                    // Close wizard.
                    AccessTokenWizardForm wizard = (AccessTokenWizardForm)Parent;
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString()), textBox_ClientID.Text, textBox_ClientSecret.Text, "", textBox_RedirectUri.Text));
                }
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
            }
        }
    }
}