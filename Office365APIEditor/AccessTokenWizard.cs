// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            Page05_V1AppOnlyOptionForm = 5,
            Page06_V2WebAppOptionForm = 6,
            Page07_V2MobileAppOptionForm = 7,
            Page08_ConfirmationForm = 8
        }

        public AccessTokenWizard()
        {
            InitializeComponent();
        }

        private void AccessTokenWizard_Load(object sender, EventArgs e)
        {
            // Initialize objects.

            Pages = new List<Panel>();
            Pages.Add(panel_Page00);
            Pages.Add(panel_Page01);
            Pages.Add(panel_Page02);
            Pages.Add(panel_Page03);
            Pages.Add(panel_Page04);
            Pages.Add(panel_Page05);
            Pages.Add(panel_Page06);
            Pages.Add(panel_Page07);
                        
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
            }

            comboBox_Page03_Resource.SelectedIndex = 0;
            comboBox_Page04_Resource.SelectedIndex = 0;
            comboBox_Page05_Resource.SelectedIndex = 0;

            Size = new Size(433, 256);
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
                        // Create a return value and close this window.
                        clientInfo = new ClientInformation(new TokenResponse(), AuthEndpoints.Basic, Resources.None, "", "", "", "");
                        DialogResult = DialogResult.OK;
                        Close();
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
                    else
                    {
                        // V1 endpoint Web App (App Only Token)
                        // Go to the next page.
                        ShowPage(PageIndex.Page05_V1AppOnlyOptionForm);
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
                    else
                    {
                        // V2 endpoint Mobile App
                        // Go to the next page.
                        ShowPage(PageIndex.Page07_V2MobileAppOptionForm);
                    }

                    break;
                case PageIndex.Page03_V1WebAppOptionForm:
                    // Option form for V1 auth endpoint Web App

                    if (ValidateV1WebAppParam())
                    {
                        string authorizationCode = AcquireV1WebAppAuthorizationCode();

                        if (authorizationCode == "")
                        {
                            return;
                        }

                        TokenResponse tokenResponse = AcquireV1WebAppAccessToken(authorizationCode);

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page03_Resource.SelectedText), textBox_Page03_ClientID.Text, textBox_Page03_ClientSecret.Text, "", textBox_Page03_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;

                case PageIndex.Page04_V1NativeAppOptionForm:
                    // Option form for V1 auth endpoint Native App

                    if (ValidateV1NativeAppParam())
                    {
                        string authorizationCode = AcquireV1NativeAppAuthorizationCode();
                        
                        if (authorizationCode == "")
                        {
                            return;
                        }

                        TokenResponse tokenResponse = AcquireV1NativeAppAccessToken(authorizationCode);

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV1, Util.ConvertResourceNameToResourceEnum(comboBox_Page04_Resource.SelectedText), textBox_Page04_ClientID.Text, "", "", textBox_Page04_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;

                case PageIndex.Page05_V1AppOnlyOptionForm:
                    // Option form for V1 auth endpoint Web App App only token

                    if (ValidateV1WebAppAppOnlyParam())
                    {
                        TokenResponse tokenResponse = AcquireV1WebAppAppOnlyAccessToken();

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

                    if (ValidateV2WebAppParam())
                    {
                        string authorizationCode = AcquireV2WebAppAuthorizationCode();

                        if (authorizationCode == "")
                        {
                            return;
                        }

                        TokenResponse tokenResponse = AcquireV2WebAppAccessToken(authorizationCode);

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_Page06_ClientID.Text, textBox_Page06_ClientSecret.Text, textBox_Page06_Scopes.Text, textBox_Page06_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    break;

                case PageIndex.Page07_V2MobileAppOptionForm:
                    // Option form for V2 auth endpoint Web App

                    if (ValidateV2MobileAppParam())
                    {
                        string authorizationCode = AcquireV2MobileAppAuthorizationCode();

                        if (authorizationCode == "")
                        {
                            return;
                        }

                        TokenResponse tokenResponse = AcquireV2MobileAppAccessToken(authorizationCode);

                        if (tokenResponse != null)
                        {
                            SaveSettings();

                            // Create a return value and close this window.
                            clientInfo = new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, textBox_Page07_ClientID.Text, "", textBox_Page07_Scopes.Text, textBox_Page07_RedirectUri.Text);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
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

        #region Code for V1 auth endpoint Web App

        private bool ValidateV1WebAppParam()
        {
            // Check the form for web app.

            if (textBox_Page03_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page03_RedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!Util.IsValidUrl(textBox_Page03_RedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page03_ClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireV1WebAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page03_ClientID.Text, textBox_Page03_RedirectUri.Text, Util.ConvertResourceNameToUri(comboBox_Page03_Resource.SelectedItem.ToString()));

            if (getCodeForm.ShowDialog(out Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }

        private TokenResponse AcquireV1WebAppAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBox_Page03_RedirectUri.Text) +
                "&client_id=" + textBox_Page03_ClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_Page03_ClientSecret.Text) +
                "&code=" + AuthorizationCode +
                "&resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceNameToUri(comboBox_Page03_Resource.SelectedItem.ToString()));

            return AcquireAccessToken(postBody, "https://login.microsoftonline.com/common/oauth2/token");
        }

        #endregion

        #region Code for V1 auth endpoint Native App
        
        private bool ValidateV1NativeAppParam()
        {
            // Check the form for native app.

            if (textBox_Page04_TenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (!textBox_Page04_TenantName.Text.EndsWith(".onmicrosoft.com"))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "Office365APIEditor");
                return false;
            }
            else if (!Util.IsValidUrl("https://login.windows.net/" + textBox_Page04_TenantName.Text))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page04_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page04_RedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!Util.IsValidUrl(textBox_Page04_RedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireV1NativeAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page04_ClientID.Text, textBox_Page04_RedirectUri.Text, Util.ConvertResourceNameToUri(comboBox_Page04_Resource.SelectedItem.ToString()));

            if (getCodeForm.ShowDialog(out Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return Code;
        }

        private TokenResponse AcquireV1NativeAppAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&Resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceNameToUri(comboBox_Page04_Resource.SelectedItem.ToString())) +
                "&client_id=" + textBox_Page04_ClientID.Text +
                "&code=" + AuthorizationCode +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBox_Page04_RedirectUri.Text);

            string endPoint = "https://login.microsoftonline.com/" + textBox_Page04_TenantName.Text.Replace("@", ".") + "/oauth2/token";

            return AcquireAccessToken(postBody, endPoint);
        }

        #endregion

        #region Code For V1 auth endpoint App Only Token

        private void button_Page05_SelectCert_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (openFileDialog_PFX.ShowDialog() == DialogResult.OK)
            {
                textBox_Page05_CertPath.Text = openFileDialog_PFX.FileName;
            }
        }

        private bool ValidateV1WebAppAppOnlyParam()
        {
            // Check the form for web app (App Only).

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

        private TokenResponse AcquireV1WebAppAppOnlyAccessToken()
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

        #endregion

        #region Code for V2 auth endpoint Web App

        private void button_Page06_ScopeEditor_Click(object sender, EventArgs e)
        {
            string scopes = "";
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

            if (scopeEditor.ShowDialog(out scopes) == DialogResult.OK)
            {
                textBox_Page06_Scopes.Text = scopes;
            }
        }

        private bool ValidateV2WebAppParam()
        {
            // Check the form for v2 web app.

            if (textBox_Page06_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page06_RedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!Util.IsValidUrl(textBox_Page06_RedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page06_Scopes.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page06_ClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireV2WebAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page06_ClientID.Text, textBox_Page06_RedirectUri.Text, textBox_Page06_Scopes.Text, true);

            if (getCodeForm.ShowDialog(out Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("authentication_canceled: User canceled authentication", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Code;
        }

        private TokenResponse AcquireV2WebAppAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBox_Page06_RedirectUri.Text) +
                "&client_id=" + textBox_Page06_ClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_Page06_ClientSecret.Text) +
                "&code=" + AuthorizationCode +
                "&scope=" + textBox_Page06_Scopes.Text;

            string endPoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

            return AcquireAccessToken(postBody, endPoint);
        }

        #endregion

        #region Code for V2 auth endpoint Mobile App

        private void button_Page07_ScopeEditor_Click(object sender, EventArgs e)
        {
            string scopes = "";
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

            if (scopeEditor.ShowDialog(out scopes) == DialogResult.OK)
            {
                textBox_Page07_Scopes.Text = scopes;
            }
        }

        private bool ValidateV2MobileAppParam()
        {
            // Check the form for v2 mobile app.

            if (textBox_Page07_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_Page07_Scopes.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireV2MobileAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_Page07_ClientID.Text, textBox_Page07_RedirectUri.Text, textBox_Page07_Scopes.Text, true);

            if (getCodeForm.ShowDialog(out Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("authentication_canceled: User canceled authentication", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Code;
        }

        private TokenResponse AcquireV2MobileAppAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + textBox_Page07_RedirectUri.Text +
                "&client_id=" + textBox_Page07_ClientID.Text +
                "&code=" + AuthorizationCode +
                "&scope=" + textBox_Page07_Scopes.Text;

            string endPoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

            return AcquireAccessToken(postBody, endPoint);
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

        private void linkLabel_linkLabel_Page05_WebAppAppOnly_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token.md");
        }

        private TokenResponse AcquireAccessToken(string PostBody, string EndPointUrl)
        {
            TokenResponse result = null;

            System.Net.WebRequest request = System.Net.WebRequest.Create(EndPointUrl);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(PostBody);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                System.Net.WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize and get an Access Token.
                    result = Deserialize<TokenResponse>(jsonResponse);
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
