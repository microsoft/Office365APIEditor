// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.Settings;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class SharePointOnlineAppOnlySettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public SharePointOnlineAppOnlySettingPage()
        {
            InitializeComponent();
        }

        private void SharePointOnlineAppOnlySettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_Web_application_for_SharePoint_Online_App-Only_Token.md");

            var lastAppSetting = Properties.Settings.Default.AccessTokenWizardApps.LastApps.Find(AppSettingType.LastSharePointOnlineAppOnlyApp);

            if (lastAppSetting != null)
            {
                textBox_TenantName.Text = lastAppSetting.TenantName;
                textBox_ClientID.Text = lastAppSetting.ApplicationId;
                textBox_ClientSecret.Text = lastAppSetting.ClientSecret;
            }
        }

        private void LinkLabel_Description_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        public void NextButtonAction()
        {
            if (ValidateSharePointOnlineAppOnlyByKeyParam())
            {
                TokenResponse tokenResponse = AcquireSharePointOnlineAppOnlyAccessTokenByKey();

                if (tokenResponse != null)
                {
                    // Save settings.
                    Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(AppSettingType.LastSharePointOnlineAppOnlyApp, new AccessTokenWizardAppSetting()
                    {
                        DisplayName = AppSettingType.LastSharePointOnlineAppOnlyApp.ToString(),
                        TenantName = textBox_TenantName.Text,
                        ApplicationId = textBox_ClientID.Text,
                        ClientSecret = textBox_ClientSecret.Text
                    });

                    if (checkBox_SaveAsNewApp.Checked)
                    {
                        // Save this app to the app library.
                        string newAppDisplayName = Properties.Settings.Default.AccessTokenWizardApps.SavedApps.FindNewAppDisplayName();

                        Properties.Settings.Default.AccessTokenWizardApps.SavedApps.AddForce(new AccessTokenWizardAppSetting()
                        {
                            DisplayName = newAppDisplayName,
                            TenantName = textBox_TenantName.Text,
                            ApplicationId = textBox_ClientID.Text,
                            ClientSecret = textBox_ClientSecret.Text
                        });

                        MessageBox.Show("Your app was added to your App Library as '" + newAppDisplayName + "'.", "Office365APIEditor");
                    }

                    Properties.Settings.Default.Save();

                    // Close wizard.
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Resources.None, textBox_ClientID.Text, "", "", ""));
                }
            }
        }

        private bool ValidateSharePointOnlineAppOnlyByKeyParam()
        {
            // Check the form for SharePoint Online App-Only REST API Microsoft Azure Access Control Service application..

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

        private TokenResponse AcquireSharePointOnlineAppOnlyAccessTokenByKey()
        {
            string SharePointPrincipal = "00000003-0000-0ff1-ce00-000000000000";
            string EndPointPrefix = textBox_TenantName.Text.Substring(0, textBox_TenantName.Text.IndexOf('.'));

            // Build a POST body.
            string postBody = "grant_type=client_credentials" +
                "&resource=" + System.Web.HttpUtility.UrlEncode(SharePointPrincipal + "/" + EndPointPrefix + ".sharepoint.com@" + textBox_TenantName.Text) +
                "&client_id=" + textBox_ClientID.Text + System.Web.HttpUtility.UrlEncode("@") + textBox_TenantName.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_ClientSecret.Text);

            return wizard.AcquireAccessToken(postBody, "https://accounts.accesscontrol.windows.net/" + textBox_TenantName.Text + "/tokens/OAuth/2");
        }

        private void Button_LoadSavedApp_Click(object sender, EventArgs e)
        {
            AccessTokenWizardAppSetting savedApp = wizard.LoadSavedApp();

            if (savedApp != null)
            {
                textBox_TenantName.Text = savedApp.TenantName;
                textBox_ClientID.Text = savedApp.ApplicationId;
                textBox_ClientSecret.Text = savedApp.ClientSecret;
            }
        }
    }
}