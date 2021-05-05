// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.AccessTokenUtil;
using Office365APIEditor.Settings;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V2EndpointNativeAppSettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V2EndpointNativeAppSettingPage()
        {
            InitializeComponent();
        }

        private void V2EndpointNativeAppSettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V2_Native_application.md");

            var lastAppSetting = Properties.Settings.Default.AccessTokenWizardApps.LastApps.Find(AppSettingType.LastV2EndpointNativeApp);

            if (lastAppSetting != null)
            {
                textBox_TenantName.Text = lastAppSetting.TenantName;
                textBox_ClientID.Text = lastAppSetting.ApplicationId;
                textBox_RedirectUri.Text = lastAppSetting.RedirectUri;
                textBox_Scopes.Text = lastAppSetting.Scopes;
            }
        }

        private void LinkLabel_Description_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        public void NextButtonAction()
        {
            V2NativeAppUtil v2NativeAppUtil = new V2NativeAppUtil()
            {
                TenantName = textBox_TenantName.Text,
                ClientID = textBox_ClientID.Text,
                RedirectUri = textBox_RedirectUri.Text,
                Scopes = textBox_Scopes.Text
            };

            ValidateResult validateResult = v2NativeAppUtil.Validate();

            if (validateResult.IsValid)
            {
                AcquireAccessTokenResult acquireAccessTokenResult = v2NativeAppUtil.AcquireAccessToken();

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
                    // Save settings.
                    Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(AppSettingType.LastV2EndpointNativeApp, new AccessTokenWizardAppSetting()
                    {
                        DisplayName = AppSettingType.LastV2EndpointNativeApp.ToString(),
                        TenantName = textBox_TenantName.Text,
                        ApplicationId = textBox_ClientID.Text,
                        RedirectUri = textBox_RedirectUri.Text,
                        Scopes = textBox_Scopes.Text
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
                            RedirectUri = textBox_RedirectUri.Text,
                            Scopes = textBox_Scopes.Text
                        });

                        MessageBox.Show("Your app was added to your App Library as '" + newAppDisplayName + "'.", "Office365APIEditor");
                    }

                    Properties.Settings.Default.Save();

                    // Close wizard.
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_ClientID.Text, "", textBox_Scopes.Text, textBox_RedirectUri.Text, textBox_TenantName.Text));
                }
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
            }
        }

        private void Button_ScopeEditor_Click(object sender, EventArgs e)
        {
            wizard.DialogResult = DialogResult.None;

            string[] selectedScopes;

            if (textBox_Scopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_Scopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_Scopes.Text = scopes;
            }
        }

        private void Button_LoadSavedApp_Click(object sender, EventArgs e)
        {
            AccessTokenWizardAppSetting savedApp = wizard.LoadSavedApp();

            if (savedApp != null)
            {
                textBox_TenantName.Text = savedApp.TenantName;
                textBox_ClientID.Text = savedApp.ApplicationId;
                textBox_RedirectUri.Text = savedApp.RedirectUri;
                textBox_Scopes.Text = savedApp.Scopes;
            }
        }
    }
}