// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Office365APIEditor.Settings;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointAppOnlyByCertSettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V1EndpointAppOnlyByCertSettingPage()
        {
            InitializeComponent();
        }

        private void V1EndpointAppOnlyByCertSettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token.md");

            foreach (string item in Util.ResourceNames)
            {
                comboBox_Resource.Items.Add(item);
            }

            var lastAppSetting = Properties.Settings.Default.AccessTokenWizardApps.LastApps.Find(AppSettingType.LastV1EndpointAppOnlyByCertApp);

            if (lastAppSetting != null)
            {
                textBox_TenantName.Text = lastAppSetting.TenantName;
                textBox_ClientID.Text = lastAppSetting.ApplicationId;
                textBox_CertPath.Text = lastAppSetting.CertificatePath;
                textBox_CertPass.Text = lastAppSetting.CertificatePassword;
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

        private void Button_SelectCert_Click(object sender, EventArgs e)
        {
            wizard.DialogResult = DialogResult.None;

            if (openFileDialog_PFX.ShowDialog() == DialogResult.OK)
            {
                textBox_CertPath.Text = openFileDialog_PFX.FileName;
            }
        }

        public void NextButtonAction()
        {
            if (ValidateV1WebAppAppOnlyByCertParam())
            {
                TokenResponse tokenResponse = AcquireV1WebAppAppOnlyAccessTokenByCert();

                if (tokenResponse != null)
                {
                    // Save settings.
                    Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(AppSettingType.LastV1EndpointAppOnlyByCertApp, new AccessTokenWizardAppSetting()
                    {
                        DisplayName = AppSettingType.LastV1EndpointAppOnlyByCertApp.ToString(),
                        TenantName = textBox_TenantName.Text,
                        ApplicationId = textBox_ClientID.Text,
                        CertificatePath = textBox_CertPath.Text,
                        CertificatePassword = textBox_CertPass.Text,
                        Resource = (Resources)Enum.ToObject(typeof(Resources), comboBox_Resource.SelectedIndex)
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
                            CertificatePath = textBox_CertPath.Text,
                            CertificatePassword = textBox_CertPass.Text,
                            Resource = (Resources)Enum.ToObject(typeof(Resources), comboBox_Resource.SelectedIndex)
                        });

                        MessageBox.Show("Your app was added to your App Library as '" + newAppDisplayName + "'.", "Office365APIEditor");
                    }

                    Properties.Settings.Default.Save();

                    // Close wizard.
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Resource.SelectedItem.ToString()), textBox_ClientID.Text, "", "", ""));
                }
            }
        }

        private bool ValidateV1WebAppAppOnlyByCertParam()
        {
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
            else if (!File.Exists(textBox_CertPath.Text))
            {
                MessageBox.Show("The certificate path is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_CertPass.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private TokenResponse AcquireV1WebAppAppOnlyAccessTokenByCert()
        {
            try
            {
                var cert = new X509Certificate2(textBox_CertPath.Text, textBox_CertPass.Text);

                string resource = Util.ConvertResourceNameToUri(comboBox_Resource.SelectedItem.ToString());

                ClientAssertionCertificate cac = new ClientAssertionCertificate(textBox_ClientID.Text, cert);
                AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + textBox_TenantName.Text, false);

                AuthenticationResult authenticationResult = null;

                string errorMessage = null;
                try
                {
                    authenticationResult = authenticationContext.AcquireTokenAsync(resource, cac).Result;
                }
                catch (AdalException ex)
                {
                    errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\nInnerException : " + ex.InnerException.Message;
                    }
                }
                catch (ArgumentException ex)
                {
                    errorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\nInnerException : " + ex.InnerException.Message;
                    }
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else
                {
                    return Util.ConvertAuthenticationResultToTokenResponse(authenticationResult);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void Button_LoadSavedApp_Click(object sender, EventArgs e)
        {
            AccessTokenWizardAppSetting savedApp = wizard.LoadSavedApp();

            if (savedApp != null)
            {
                textBox_TenantName.Text = savedApp.TenantName;
                textBox_ClientID.Text = savedApp.ApplicationId;
                textBox_CertPath.Text = savedApp.CertificatePath;
                textBox_CertPass.Text = savedApp.CertificatePassword;
                comboBox_Resource.SelectedIndex = (int)savedApp.Resource;
            }
        }
    }
}