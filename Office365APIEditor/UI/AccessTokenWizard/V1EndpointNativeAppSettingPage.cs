// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.AccessTokenUtil;
using Office365APIEditor.Settings;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointNativeAppSettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V1EndpointNativeAppSettingPage()
        {
            InitializeComponent();
        }

        private void V1EndpointNativeAppSettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Native_application.md");

            foreach (string item in Util.ResourceNames)
            {
                comboBox_Resource.Items.Add(item);
            }

            var lastAppSetting = Properties.Settings.Default.AccessTokenWizardApps.LastApps.Find(AppSettingType.LastV1EndpointNativeApp);

            if (lastAppSetting != null)
            {
                textBox_TenantName.Text = lastAppSetting.TenantName;
                textBox_ClientID.Text = lastAppSetting.ApplicationId;
                textBox_RedirectUri.Text = lastAppSetting.RedirectUri;
                comboBox_Resource.SelectedIndex = lastAppSetting.Resource == Resources.None ? 1 : (int)lastAppSetting.Resource;
            }
            else
            {
                comboBox_Resource.SelectedIndex = 1;
            }
        }

        private void LinkLabel_Description_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        public void NextButtonAction()
        {
            V1NativeAppUtil v1NativeAppUtil = new V1NativeAppUtil()
            {
                TenantName = textBox_TenantName.Text,
                ClientID = textBox_ClientID.Text,
                RedirectUri = textBox_RedirectUri.Text,
                Resource = Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString())
            };

            ValidateResult validateResult = v1NativeAppUtil.Validate();

            if (validateResult.IsValid)
            {
                AcquireAccessTokenResult acquireAccessTokenResult = v1NativeAppUtil.AcquireAccessToken();

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
                    Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(AppSettingType.LastV1EndpointNativeApp, new AccessTokenWizardAppSetting()
                    {
                        DisplayName = AppSettingType.LastV1EndpointNativeApp.ToString(),
                        TenantName = textBox_TenantName.Text,
                        ApplicationId = textBox_ClientID.Text,
                        RedirectUri = textBox_RedirectUri.Text,
                        Resource = (Resources)Enum.ToObject(typeof(Resources), comboBox_Resource.SelectedIndex),
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
                            Resource = (Resources)Enum.ToObject(typeof(Resources), comboBox_Resource.SelectedIndex),
                        });

                        MessageBox.Show("Your app was added to your App Library as '" + newAppDisplayName + "'.", "Office365APIEditor");
                    }

                    Properties.Settings.Default.Save();

                    // Close wizard.
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString()), textBox_ClientID.Text, "", "", textBox_RedirectUri.Text));
                }
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
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
                comboBox_Resource.SelectedIndex = (int)savedApp.Resource;
            }
        }
    }
}