// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V2EndpointAppOnlyByCertSettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V2EndpointAppOnlyByCertSettingPage()
        {
            InitializeComponent();
        }

        private void V2EndpointAppOnlyByCertSettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V2_Web_application_for_App_Only_Token_Cert_Auth.md");

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
            if (ValidateV2WebAppAppOnlyByCertParam())
            {
                TokenResponse tokenResponse = AcquireV2WebAppAppOnlyAccessTokenByCert();

                if (tokenResponse != null)
                {
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_ClientID.Text, "", tokenResponse.scope, "", textBox_TenantName.Text));
                }
            }
        }

        private bool ValidateV2WebAppAppOnlyByCertParam()
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

        private TokenResponse AcquireV2WebAppAppOnlyAccessTokenByCert()
        {
            try
            {
                var cert = new X509Certificate2(textBox_CertPath.Text, textBox_CertPass.Text);

                string scope = Util.ConvertResourceNameToUri(comboBox_Resource.SelectedItem.ToString());

                if (!scope.EndsWith("/"))
                {
                    scope += "/";
                }

                scope += ".default";

                IConfidentialClientApplication cca = ConfidentialClientApplicationBuilder.Create(textBox_ClientID.Text)
                    .WithCertificate(cert)
                    .WithTenantId(textBox_TenantName.Text)
                    .Build();

                AuthenticationResult authenticationResult = null;

                string errorMessage = null;
                try
                {
                    authenticationResult = cca.AcquireTokenForClient(new string[] { scope }).ExecuteAsync().Result;
                }
                catch (MsalServiceException ex)
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
    }
}