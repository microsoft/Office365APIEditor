// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Office365APIEditor.AccessTokenUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Office365APIEditor
{
    public partial class AccessTokenWizard : Form
    {
        List<Panel> Pages;
        PageIndex currentPageIndex;
        List<PageIndex> previousPages;

        ClientInformation clientInfo;

        private enum PageIndex
        {
            None = -1,
            Page00_PortalSelection = 0,
            Page01_MicrosoftAzurePortalAppSelection = 1,
            Page02_AppRegistrationPortalAppSelection = 2,
            Page03_V1WebAppOptionForm = 3,
            Page04_V1NativeAppOptionForm = 4,
            Page05_V1AppOnlyByCertOptionForm = 5,
            Page06_V2WebAppOptionForm = 6,
            Page07_V2MobileAppOptionForm = 7,
            Page08_V1AppOnlyByKeyOptionForm = 8,
            Page09_V1AdminConsentOptionForm = 9,
            Page10_BuiltInAppOrBasicAuthSelection = 10,
            Page11_BuiltInAppOptionForm = 11,
            Page12_V2AppOnlyByPasswordForMicrosoftGraphOptionForm = 12,
            Page13_V2AdminConsentOptionForm = 13
        }

        public AccessTokenWizard()
        {
            InitializeComponent();
        }

        private void AccessTokenWizard_Load(object sender, EventArgs e)
        {
            // Initialize objects.

            Pages = new List<Panel>
            {
                panel_Page00,
                panel_Page01,
                panel_Page02,
                panel_Page03,
                panel_Page04,
                panel_Page05,
                panel_Page06,
                panel_Page07,
                panel_Page08,
                panel_Page09,
                panel_Page10,
                panel_Page11,
                panel_Page12,
                panel_Page13
            };

            previousPages = new List<PageIndex>();
            currentPageIndex = PageIndex.None;

            // Initialize UI.

            Pages.ForEach(p => p.Enabled = false);

            // Go to the first page.
            ShowPage(PageIndex.Page00_PortalSelection);

            button_Back.Enabled = false;

            foreach (string item in Util.ResourceNames)
            {
                comboBox_Page03_Resource.Items.Add(item);
                comboBox_Page04_Resource.Items.Add(item);
                comboBox_Page05_Resource.Items.Add(item);
                comboBox_Page08_Resource.Items.Add(item);
                comboBox_Page09_Resource.Items.Add(item);
            }

            comboBox_Page03_Resource.SelectedIndex = 1;
            comboBox_Page04_Resource.SelectedIndex = 1;
            comboBox_Page05_Resource.SelectedIndex = 1;
            comboBox_Page08_Resource.SelectedIndex = 1;
            comboBox_Page09_Resource.SelectedIndex = 1;

            Size = new Size(433, 286);
            CenterToParent();
        }

        public DialogResult ShowDialog(out ClientInformation ClientInfo)
        {
            DialogResult dialogResult = this.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                ClientInfo = new ClientInformation(null, AuthEndpoints.Basic, Resources.None, "", "", "", "");
            }

            ClientInfo = clientInfo;
            return dialogResult;
        }

        private void ShowPage(PageIndex PageIndexToShow)
        {
            previousPages.Add(currentPageIndex);
            currentPageIndex = PageIndexToShow;

            Pages[(int)currentPageIndex].Top = 12;
            Pages[(int)currentPageIndex].Left = 12;
            Pages[(int)currentPageIndex].Enabled = true;

            if (Pages[(int)currentPageIndex].Tag != null && Pages[(int)currentPageIndex].Tag.ToString() != "")
            {
                try
                {
                    Pages[(int)currentPageIndex].Controls[Pages[(int)currentPageIndex].Tag.ToString()].Focus();
                }
                catch
                {

                }
            }

            if (previousPages.Count != 1)
            {
                Pages[(int)previousPages[previousPages.Count - 1]].Left = 1000;
                Pages[(int)previousPages[previousPages.Count - 1]].Enabled = false;
            }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            Pages[(int)previousPages[previousPages.Count - 1]].Top = 12;
            Pages[(int)previousPages[previousPages.Count - 1]].Left = 12;
            Pages[(int)previousPages[previousPages.Count - 1]].Enabled = true;

            Pages[(int)currentPageIndex].Left = 1000;
            Pages[(int)currentPageIndex].Enabled = false;

            if (Pages[(int)currentPageIndex].Tag != null && Pages[(int)currentPageIndex].Tag.ToString() != "")
            {
                try
                {
                    Pages[(int)currentPageIndex].Controls[Pages[(int)currentPageIndex].Tag.ToString()].Focus();
                }
                catch
                {

                }
            }

            currentPageIndex = previousPages[previousPages.Count - 1];
            previousPages.RemoveAt(previousPages.Count - 1);

            if (previousPages.Count == 1)
            {
                button_Back.Enabled = false;
            }
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            ValidateResult validateResult;
            AcquireAccessTokenResult acquireAccessTokenResult;

            switch (currentPageIndex)
            {
                case PageIndex.Page00_PortalSelection:
                    // Portal selection page

                    if (radioButton_Page00_MicrosoftAzurePortal.Checked)
                    {
                        // V1 endpoint
                        // Go to the next page.
                        ShowPage(PageIndex.Page01_MicrosoftAzurePortalAppSelection);
                    }
                    else if (radioButton_Page00_AppRegistrationPortal.Checked)
                    {
                        // V2 endpoint
                        // Go to the next page.
                        ShowPage(PageIndex.Page02_AppRegistrationPortalAppSelection);
                    }
                    else
                    {
                        // The user has no application
                        // Go to the next page.
                        // Create a return value and close this window.
                        ShowPage(PageIndex.Page10_BuiltInAppOrBasicAuthSelection);
                    }

                    break;
                case PageIndex.Page01_MicrosoftAzurePortalAppSelection:
                    // App selection page for V1 auth endpoint.

                    if (radioButton_Page01_V1Web.Checked)
                    {
                        // V1 endpoint Web App
                        // Go to the next page.
                        ShowPage(PageIndex.Page03_V1WebAppOptionForm);
                    }
                    else if (radioButton_Page01_V1Native.Checked)
                    {
                        // V1 endpoint Native App
                        // Go to the next page.
                        ShowPage(PageIndex.Page04_V1NativeAppOptionForm);
                    }
                    else if (radioButton_Page01_V1AppOnlyByCert.Checked)
                    {
                        // V1 endpoint Web App (App Only Token by certificate)
                        // Go to the next page.
                        ShowPage(PageIndex.Page05_V1AppOnlyByCertOptionForm);
                    }
                    else if (radioButton_Page01_V1AppOnlyByKey.Checked)
                    {
                        // V1 endpoint Web App (App Only Token by Key)
                        // Go to the next page.
                        ShowPage(PageIndex.Page08_V1AppOnlyByKeyOptionForm);
                    }
                    else
                    {
                        // V1 endpoint Admin Consent
                        // Go to the next page.
                        ShowPage(PageIndex.Page09_V1AdminConsentOptionForm);
                    }

                    break;
                case PageIndex.Page02_AppRegistrationPortalAppSelection:
                    // App selection page for V2 auth endpoint.

                    if (radioButton_Page02_V2Web.Checked)
                    {
                        // V2 endpoint Web App
                        // Go to the next page.
                        ShowPage(PageIndex.Page06_V2WebAppOptionForm);
                    }
                    else if (radioButton_Page02_V2Mobile.Checked)
                    {
                        // V2 endpoint Mobile App
                        // Go to the next page.
                        ShowPage(PageIndex.Page07_V2MobileAppOptionForm);
                    }
                    else if (radioButton_Page02_V2AdminConsent.Checked)
                    {
                        // V2 endpoint, but need admin consent
                        // Go to the next page.
                        ShowPage(PageIndex.Page13_V2AdminConsentOptionForm);
                    }
                    else if (radioButton_Page02_V2WebAppOnlyForMicrosoftGraph.Checked)
                    {
                        // V2 endpoint Web App (App Only Token by password for Microsoft Graph)
                        // Go to the next page.
                        ShowPage(PageIndex.Page12_V2AppOnlyByPasswordForMicrosoftGraphOptionForm);
                    }

                    break;
                case PageIndex.Page03_V1WebAppOptionForm:
                    // Option form for V1 auth endpoint Web App

                    V1WebAppUtil v1WebAppUtil = new V1WebAppUtil()
                    {
                        ClientID = textBox_Page03_ClientID.Text,
                        RedirectUri = textBox_Page03_RedirectUri.Text,
                        Resource = Util.ConvertResourceNameToResourceEnum(comboBox_Page03_Resource.SelectedItem.ToString()),
                        ClientSecret = textBox_Page03_ClientSecret.Text
                    };

                    validateResult = v1WebAppUtil.Validate();

                    if (validateResult.IsValid)
                    {
                        acquireAccessTokenResult = v1WebAppUtil.AcquireAccessToken();

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
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page03_Resource.SelectedItem.ToString()), textBox_Page03_ClientID.Text, textBox_Page03_ClientSecret.Text, "", textBox_Page03_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
                    }                    

                    break;

                case PageIndex.Page04_V1NativeAppOptionForm:
                    // Option form for V1 auth endpoint Native App

                    V1NativeAppUtil v1NativeAppUtil = new V1NativeAppUtil()
                    {
                        TenantName = textBox_Page04_TenantName.Text,
                        ClientID = textBox_Page04_ClientID.Text,
                        RedirectUri = textBox_Page04_RedirectUri.Text,
                        Resource = Util.ConvertResourceNameToResourceEnum(comboBox_Page04_Resource.SelectedItem.ToString())
                    };

                    validateResult = v1NativeAppUtil.Validate();

                    if (validateResult.IsValid)
                    {
                        acquireAccessTokenResult = v1NativeAppUtil.AcquireAccessToken();

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
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page04_Resource.SelectedItem.ToString()), textBox_Page04_ClientID.Text, "", "", textBox_Page04_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
                    }                    

                    break;

                case PageIndex.Page05_V1AppOnlyByCertOptionForm:
                    // Option form for V1 auth endpoint Web App App only token by certificate

                    if (ValidateV1WebAppAppOnlyByCertParam())
                    {
                        TokenResponse tokenResponse = AcquireV1WebAppAppOnlyAccessTokenByCert();

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page05_Resource.SelectedText), textBox_Page05_ClientID.Text, "", "", "");
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;
                case PageIndex.Page06_V2WebAppOptionForm:
                    // Option form for V2 auth endpoint Web App

                    V2WebAppUtil v2WebAppUtil = new V2WebAppUtil()
                    {
                        TenantName = textBox_Page06_TenantName.Text,
                        ClientID = textBox_Page06_ClientID.Text,
                        RedirectUri = textBox_Page06_RedirectUri.Text,
                        Scopes = textBox_Page06_Scopes.Text,
                        ClientSecret = textBox_Page06_ClientSecret.Text
                    };

                    validateResult = v2WebAppUtil.Validate();

                    if (validateResult.IsValid)
                    {
                        acquireAccessTokenResult = v2WebAppUtil.AcquireAccessToken();

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
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_Page06_ClientID.Text, textBox_Page06_ClientSecret.Text, textBox_Page06_Scopes.Text, textBox_Page06_RedirectUri.Text, textBox_Page06_TenantName.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
                    }

                    break;

                case PageIndex.Page07_V2MobileAppOptionForm:
                    // Option form for V2 auth endpoint Web App

                    V2MobileAppUtil v2MobileAppUtil = new V2MobileAppUtil()
                    {
                        TenantName = textBox_Page07_TenantName.Text,
                        ClientID = textBox_Page07_ClientID.Text,
                        RedirectUri = textBox_Page07_RedirectUri.Text,
                        Scopes = textBox_Page07_Scopes.Text
                    };

                    validateResult = v2MobileAppUtil.Validate();

                    if (validateResult.IsValid)
                    {
                        acquireAccessTokenResult = v2MobileAppUtil.AcquireAccessToken();

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
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_Page07_ClientID.Text, "", textBox_Page07_Scopes.Text, textBox_Page07_RedirectUri.Text, textBox_Page07_TenantName.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
                    }

                    break;

                case PageIndex.Page08_V1AppOnlyByKeyOptionForm:
                    // Option form for V1 auth endpoint Web App App only token by Key

                    if (ValidateV1WebAppAppOnlyByKeyParam())
                    {
                        TokenResponse tokenResponse = AcquireV1WebAppAppOnlyAccessTokenByKey();

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page08_Resource.SelectedText), textBox_Page08_ClientID.Text, "", "", "");
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;

                case PageIndex.Page09_V1AdminConsentOptionForm:
                    // Option form for V1 Admin Consent

                    if (ValidateV1AdminConsentParam())
                    {
                        string authorizationCode = AcquireV1AdminConsentAuthorizationCode();

                        if (authorizationCode == "")
                        {
                            return;
                        }

                        SaveSettings();
                        MessageBox.Show("Admin Consent completed.", "Office365APIEditor");
                    }
                    break;

                case PageIndex.Page10_BuiltInAppOrBasicAuthSelection:
                    // Mode selection page for built-in app or basic auth.

                    if (radioButton_Page10_BuiltInApp.Checked)
                    {
                        // Built-in app
                        // Go to the next page.
                        ShowPage(PageIndex.Page11_BuiltInAppOptionForm);
                    }
                    else
                    {
                        // Basic auth
                        clientInfo = new ClientInformation(new TokenResponse(), AuthEndpoints.Basic, Resources.None, "", "", "", "");
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;

                case PageIndex.Page11_BuiltInAppOptionForm:
                    // Option form for the built-in app.
                    
                    V2MobileAppUtil builtInAppUtil = new V2MobileAppUtil()
                    {
                        ClientID = Properties.Settings.Default.BuiltInAppClientId,
                        RedirectUri = Properties.Settings.Default.BuiltInAppRedirectUri,
                        Scopes = textBox_Page11_Scopes.Text
                    };

                    validateResult = builtInAppUtil.Validate();

                    if (validateResult.IsValid)
                    {
                        acquireAccessTokenResult = builtInAppUtil.AcquireAccessToken();

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
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, Properties.Settings.Default.BuiltInAppClientId, "", textBox_Page11_Scopes.Text, Properties.Settings.Default.BuiltInAppRedirectUri);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
                    }

                    break;

                case PageIndex.Page12_V2AppOnlyByPasswordForMicrosoftGraphOptionForm:
                    // Option form for V2 auth endpoint Web App App only token by password for Microsoft Graph

                    if (ValidateV2WebAppAppOnlyByPasswordForMicrosoftGraphParam())
                    {
                        TokenResponse tokenResponse = AcquireV2WebAppAppOnlyAccessTokenByPasswordForMicrosoftGraph();

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.Graph, textBox_Page12_ClientId.Text, textBox_Page12_ClientSecret.Text, textBox_Page12_Scopes.Text, "");
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;

                case PageIndex.Page13_V2AdminConsentOptionForm:
                    // Option form for V2 Admin Consent

                    if (ValidateV2AdminConsentParam())
                    {
                        string authorizationCode = AcquireV2AdminConsentAuthorizationCode();

                        if (authorizationCode == "")
                        {
                            return;
                        }

                        SaveSettings();
                        MessageBox.Show("Admin Consent completed.", "Office365APIEditor");
                    }
                    
                    break;

                case PageIndex.None:
                default:
                    break;
            }

            button_Back.Enabled = true;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #region Code For V1 auth endpoint App Only Token by cert

        private void button_Page05_SelectCert_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (openFileDialog_PFX.ShowDialog() == DialogResult.OK)
            {
                textBox_Page05_CertPath.Text = openFileDialog_PFX.FileName;
            }
        }

        private bool ValidateV1WebAppAppOnlyByCertParam()
        {
            // Check the form for web app (App Only by certificate).

            if (textBox_Page05_TenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page05_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (!File.Exists(textBox_Page05_CertPath.Text))
            {
                MessageBox.Show("The certificate path is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page05_CertPass.Text == "")
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
                FileStream certFile = File.OpenRead(textBox_Page05_CertPath.Text);
                byte[] certBytes = new byte[certFile.Length];
                certFile.Read(certBytes, 0, (int)certFile.Length);
                var cert = new X509Certificate2(certBytes, textBox_Page05_CertPass.Text, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                string resource = Util.ConvertResourceNameToUri(comboBox_Page05_Resource.SelectedItem.ToString());

                ClientAssertionCertificate cac = new ClientAssertionCertificate(textBox_Page05_ClientID.Text, cert);
                AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + textBox_Page05_TenantName.Text, false);

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

        #endregion

        #region Code For V1 auth endpoint App Only Token by Key

        private bool ValidateV1WebAppAppOnlyByKeyParam()
        {
            // Check the form for web app (App Only by certificate).

            if (textBox_Page08_TenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page08_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page08_ClientSecret.Text == "")
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
                "&resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceNameToUri(comboBox_Page08_Resource.SelectedItem.ToString())) +
                "&client_id=" + textBox_Page08_ClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_Page08_ClientSecret.Text);

            return AcquireAccessToken(postBody, "https://login.microsoftonline.com/" + textBox_Page08_TenantName.Text + "/oauth2/token");
        }

        #endregion

        #region Code for V1 Admin Consent

        private bool ValidateV1AdminConsentParam()
        {
            if (textBox_Page09_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page09_RedirectUri.Text == "")
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

            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page09_ClientID.Text, textBox_Page09_RedirectUri.Text, Util.ConvertResourceNameToUri(comboBox_Page09_Resource.SelectedItem.ToString()), false, true);

            if (getCodeForm.ShowDialog(out string Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }

        #endregion

        #region Code for V2 auth endpoint Web App

        private void button_Page06_ScopeEditor_Click(object sender, EventArgs e)
        {
            string[] selectedScopes;

            if (textBox_Page06_Scopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_Page06_Scopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_Page06_Scopes.Text = scopes;
            }
        }

        #endregion

        #region Code for V2 auth endpoint Mobile App

        private void button_Page07_ScopeEditor_Click(object sender, EventArgs e)
        {
            string[] selectedScopes;

            if (textBox_Page07_Scopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_Page07_Scopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_Page07_Scopes.Text = scopes;
            }
        }

        #endregion

        #region Code for built-in App

        private void button_Page11_ScopeEditor_Click(object sender, EventArgs e)
        {
            string[] selectedScopes;

            if (textBox_Page11_Scopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_Page11_Scopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_Page11_Scopes.Text = scopes;
            }
        }

        #endregion

        #region Code for V2 Admin Consent

        private bool ValidateV2AdminConsentParam()
        {
            if (textBox_Page13_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Application ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page13_RedirectUri.Text == "")
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
            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page13_ClientID.Text, textBox_Page13_RedirectUri.Text, "", true, true);

            if (getCodeForm.ShowDialog(out string Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Unexpected response.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }

        #endregion

        #region Code For V2 auth endpoint App Only Token by Password for Microsoft Graph

        private bool ValidateV2WebAppAppOnlyByPasswordForMicrosoftGraphParam()
        {
            // Check the form for web app (App Only by password for Microsoft Graph).

            if (textBox_Page12_TenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page12_ClientId.Text == "")
            {
                MessageBox.Show("Enter the Application ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page12_ClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private TokenResponse AcquireV2WebAppAppOnlyAccessTokenByPasswordForMicrosoftGraph()
        {
            // Build a POST body.
            string postBody = "client_id=" + textBox_Page12_ClientId.Text +
                "&scope=" + System.Web.HttpUtility.UrlEncode(textBox_Page12_Scopes.Text) +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_Page12_ClientSecret.Text) +
                "&grant_type=client_credentials";

            return AcquireAccessToken(postBody, "https://login.microsoftonline.com/" + textBox_Page12_TenantName.Text + "/oauth2/v2.0/token");
        }

        #endregion

        private void linkLabel_Page03_WebApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application.md");
        }

        private void linkLabel_Page04_NativeApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Native_application.md");
        }

        private void linkLabel_Page05_WebAppAppOnly_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token.md");
        }

        private void linkLabel_Page08_WebAppAppOnlyByKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token_Key_Auth.md");
        }

        private TokenResponse AcquireAccessToken(string PostBody, string EndPointUrl)
        {
            TokenResponse result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPointUrl);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = Util.CustomUserAgent;

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(PostBody);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize and get an Access Token.
                    result = Deserialize<TokenResponse>(jsonResponse);
                }
            }
            catch (WebException ex)
            {
                WebResponse response = ex.Response;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    string jsonResponse = reader.ReadToEnd();

                    MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace + "\r\n\r\n" + jsonResponse, "Office365APIEditor");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Office365APIEditor");
            }

            return result;
        }

        public static T Deserialize<T>(string json)
        {
            T result;

            using (var memoryStream = new MemoryStream())
            {
                byte[] jsonByteArray = Encoding.UTF8.GetBytes(json);

                memoryStream.Write(jsonByteArray, 0, jsonByteArray.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    result = (T)serializer.ReadObject(jsonReader);
                }
            }

            return result;
        }

        public void SaveSettings()
        {
            // Save settings.
            Properties.Settings.Default.Save();
        }
    }
}
