// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace Office365APIEditor
{
    [Obsolete]
    public partial class StartForm : Form
    {
        // Return values
        private TokenResponse _tokenResponse;
        private string _resource = "";

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            radioButton_WebApp.Focus();
        }

        public DialogResult ShowDialog(out TokenResponse AccessToken, out string Resource, out string ClientID, out string ClientSecret, out string Scopes, out string RedirectUri, out bool UseV2Endpoint)
        {
            DialogResult result = this.ShowDialog();

            // Build return values.
            // Those values are used to acquire another access token.

            AccessToken = _tokenResponse;

            if (radioButton_WebApp.Checked)
            {
                Resource = _resource;
                ClientID = textBox_WebAppClientID.Text;
                ClientSecret = textBox_WebAppClientSecret.Text;
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;
            }
            else if (radioButton_NativeApp.Checked)
            {
                Resource = _resource;
                ClientID = "";
                ClientSecret = "";
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;
            }
            else if (radioButton_V2MobileApp.Checked)
            {
                Resource = "";
                ClientID = textBox_V2MobileAppClientID.Text;
                ClientSecret = "";
                Scopes = textBox_V2MobileAppScopes.Text;
                RedirectUri = textBox_V2MobileAppRedirectUri.Text;
                UseV2Endpoint = true;
            }
            else if (radioButton_V2WebApp.Checked)
            {
                Resource = "";
                ClientID = textBox_V2WebAppClientID.Text;
                ClientSecret = textBox_V2WebAppClientSecret.Text;
                Scopes = textBox_V2WebAppScopes.Text;
                RedirectUri = textBox_V2WebAppRedirectUri.Text;
                UseV2Endpoint = true;
            }
            else
            {
                Resource = "";
                ClientID = "";
                ClientSecret = "";
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;
            }

            return result;
        }

        public DialogResult ShowDialog(out ClientInformation ClientInfo)
        {
            DialogResult dialogResult = this.ShowDialog();

            // Build return values.
            // Those values are used to acquire another access token.

            TokenResponse AccessToken;
            string Resource;
            string ClientID;
            string ClientSecret;
            string Scopes;
            string RedirectUri;
            bool UseV2Endpoint;

            AuthEndpoints authEndpoint = new AuthEndpoints();

            if (_tokenResponse.access_token == null && _tokenResponse.id_token != null)
            {
                // Using OpenID Connect
                _tokenResponse.access_token = _tokenResponse.id_token;
            }

            AccessToken = _tokenResponse;

            if (radioButton_WebApp.Checked)
            {
                Resource = _resource;
                ClientID = textBox_WebAppClientID.Text;
                ClientSecret = textBox_WebAppClientSecret.Text;
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;

                authEndpoint = AuthEndpoints.OAuthV1;
            }
            else if (radioButton_NativeApp.Checked)
            {
                Resource = _resource;
                ClientID = "";
                ClientSecret = "";
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;

                authEndpoint = AuthEndpoints.OAuthV1;
            }
            else if (radioButton_V2MobileApp.Checked)
            {
                Resource = "";
                ClientID = textBox_V2MobileAppClientID.Text;
                ClientSecret = "";
                Scopes = textBox_V2MobileAppScopes.Text;
                RedirectUri = textBox_V2MobileAppRedirectUri.Text;
                UseV2Endpoint = true;

                authEndpoint = AuthEndpoints.OAuthV2;
            }
            else if (radioButton_V2WebApp.Checked)
            {
                Resource = "";
                ClientID = textBox_V2WebAppClientID.Text;
                ClientSecret = textBox_V2WebAppClientSecret.Text;
                Scopes = textBox_V2WebAppScopes.Text;
                RedirectUri = textBox_V2WebAppRedirectUri.Text;
                UseV2Endpoint = true;

                authEndpoint = AuthEndpoints.OAuthV2;
            }
            else
            {
                Resource = "";
                ClientID = "";
                ClientSecret = "";
                Scopes = "";
                RedirectUri = "";
                UseV2Endpoint = false;

                authEndpoint = AuthEndpoints.Basic;
            }

            Resources resource = new Resources();

            if (Resource == "Exchange Online")
            {
                resource = Resources.Outlook;
            }
            else if (Resource == "Microsoft Graph")
            {
                resource = Resources.Graph;
            }
            else if (Resource == "Office 365 Management API")
            {
                resource = Resources.Management;
            }
            else
            {
                resource = Resources.None;
            }

            ClientInformation result = new ClientInformation(AccessToken, authEndpoint, resource, ClientID, ClientSecret, Scopes, RedirectUri);

            ClientInfo = result;
            return dialogResult;
        }

        private void radioButton_WebApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_WebApp.Enabled = true;
            groupBox_NativeApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
            groupBox_V2MobileApp.Enabled = false;
            groupBox_V2WebApp.Enabled = false;
            groupBox_WebAppAppOnly.Enabled = false;
        }

        private void radioButton_NativeApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_NativeApp.Enabled = true;
            groupBox_WebApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
            groupBox_V2WebApp.Enabled = false;
            groupBox_V2MobileApp.Enabled = false;
            groupBox_WebAppAppOnly.Enabled = false;
        }

        private void radioButton_BasicAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_BasicAuth.Enabled = true;
            groupBox_NativeApp.Enabled = false;
            groupBox_WebApp.Enabled = false;
            groupBox_V2WebApp.Enabled = false;
            groupBox_V2MobileApp.Enabled = false;
            groupBox_WebAppAppOnly.Enabled = false;
        }
        
        private void radioButton_V2WebApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_WebApp.Enabled = false;
            groupBox_NativeApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
            groupBox_V2MobileApp.Enabled = false;
            groupBox_V2WebApp.Enabled = true;
            groupBox_WebAppAppOnly.Enabled = false;
        }

        private void radioButton_V2MobileApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_V2MobileApp.Enabled = true;
            groupBox_BasicAuth.Enabled = false;
            groupBox_NativeApp.Enabled = false;
            groupBox_V2WebApp.Enabled = false;
            groupBox_WebApp.Enabled = false;
            groupBox_WebAppAppOnly.Enabled = false;
        }

        private void radioButton_WebAppAppOnly_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_V2MobileApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
            groupBox_NativeApp.Enabled = false;
            groupBox_V2WebApp.Enabled = false;
            groupBox_WebApp.Enabled = false;
            groupBox_WebAppAppOnly.Enabled = true;
        }

        private void button_NativeAppAcquireAccessToken_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (CheckNativeAppParam() == false)
            {
                return;
            }

            _tokenResponse = AcquireAccessTokenOfNativeApp();


            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.", "Office365APIEditor");
            }
        }

        private void button_WebAppAcquireAccessToken_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (CheckWebAppParam() == false)
            {
                return;
            }

            string authorizationCode = AcquireWebAppAuthorizationCode();

            if (authorizationCode == "")
            {
                return;
            }

            _tokenResponse = AcquireAccessTokenOfWebApp(authorizationCode);

            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.", "Office365APIEditor");
            }
        }

        private void button_WebAppAppOnlyAcquireAccessToken_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (CheckWebAppAppOnlyParam() == false)
            {
                return;
            }

            _tokenResponse = AcquireAccessTokenOfWebAppAppOnly();

            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.", "Office365APIEditor");
            }
        }

        private void button_BasicAuthGoNext_Click(object sender, EventArgs e)
        {
            SaveSettings();

            _tokenResponse = new TokenResponse { access_token = "USEBASICBASIC" };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool IsValidUrl(string Uri)
        {
            try
            {
                new Uri(Uri);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckWebAppParam()
        {
            // Check the form for web app.

            if (textBox_WebAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_WebAppRedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!IsValidUrl(textBox_WebAppRedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_WebAppClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckNativeAppParam()
        {
            // Check the form for native app.

            if (textBox_NativeAppTenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (!textBox_NativeAppTenantName.Text.EndsWith(".onmicrosoft.com"))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "Office365APIEditor");
                return false;
            }
            else if (!IsValidUrl("https://login.windows.net/" + textBox_NativeAppTenantName.Text))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "Office365APIEditor");
                return false;
            }
            else if (textBox_NativeAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_NativeAppRedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!IsValidUrl(textBox_NativeAppRedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckV2MobileAppParam()
        {
            // Check the form for v2 mobile app.

            if (textBox_V2MobileAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_V2MobileAppScopes.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckV2WebAppParam()
        {
            // Check the form for v2 web app.

            if (textBox_V2WebAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (textBox_V2WebAppRedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "Office365APIEditor");
                return false;
            }
            else if (!IsValidUrl(textBox_V2WebAppRedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_V2WebAppScopes.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else if (textBox_V2WebAppClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckWebAppAppOnlyParam()
        {
            // Check the form for web app (App Only).

            if (textBox_WebAppAppOnlyTenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "Office365APIEditor");
                return false;
            }
            else if (textBox_WebAppAppOnlyClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "Office365APIEditor");
                return false;
            }
            else if (!File.Exists(textBox_WebAppAppOnlyCertPath.Text))
            {
                MessageBox.Show("The certificate path is invalid.", "Office365APIEditor");
                return false;
            }
            else if (textBox_WebAppAppOnlyCertPass.Text == "")
            {
                MessageBox.Show("Enter the scopes.", "Office365APIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string AcquireWebAppAuthorizationCode()
        {
            string Code = "";

            _resource = GetResourceNameForWebApp();
            GetCodeForm getCodeForm = new GetCodeForm(textBox_WebAppClientID.Text, textBox_WebAppRedirectUri.Text, GetResourceURL(_resource));

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

        private string AcquireV2MobileAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_V2MobileAppClientID.Text, textBox_V2MobileAppRedirectUri.Text, textBox_V2MobileAppScopes.Text, true);

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

        private string AcquireV2WebAppAuthorizationCode()
        {
            string Code = "";

            GetCodeForm getCodeForm = new GetCodeForm(textBox_V2WebAppClientID.Text, textBox_V2WebAppRedirectUri.Text, textBox_V2WebAppScopes.Text, true);

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

        private TokenResponse AcquireAccessTokenOfWebApp(string AuthorizationCode)
        {
            TokenResponse result = null;
            string accessToken = "";

            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBox_WebAppRedirectUri.Text) +
                "&client_id=" + textBox_WebAppClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_WebAppClientSecret.Text) +
                "&code=" + AuthorizationCode +
                "&resource=" + System.Web.HttpUtility.UrlEncode(GetResourceURL(GetResourceNameForWebApp()));

            System.Net.WebRequest request = System.Net.WebRequest.Create("https://login.microsoftonline.com/common/oauth2/token");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postBody);
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
                    accessToken = result.access_token;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Office365APIEditor");
            }

            return result;
        }

        private TokenResponse AcquireAccessTokenOfNativeApp()
        {
            // This URL is old.
            // string authority = "https://login.windows.net/" + textBox_NativeAppTenantName.Text;

            // So, use new one. (access point is same.)
            string authority = "https://login.microsoftonline.com/" + textBox_NativeAppTenantName.Text;

            string clientId = textBox_NativeAppClientID.Text;
            Uri redirectUri = new Uri(textBox_NativeAppRedirectUri.Text);
            _resource = GetResourceNameForNativeApp();
            string resourceName = GetResourceURL(_resource);

            AuthenticationResult authenticationResult = null;
            AuthenticationContext authenticationContext = new AuthenticationContext(authority, false);

            string errorMessage = null;
            try
            {
                // Show a Sign-in page of Office365.
                authenticationResult = authenticationContext.AcquireTokenAsync(resourceName, clientId, redirectUri, new PlatformParameters(PromptBehavior.Always)).Result;
                
                // If we use hardcoded user credential, use this method.
                // authenticationResult = authenticationContext.AcquireToken(resourceName, clientId, new UserCredential("SMTP Address", "password"));
            }
            catch (AggregateException ex)
            {
                errorMessage = ex.Message;

                if (ex.InnerException.Message == "User canceled authentication")
                {
                    errorMessage = "User canceled authentication";
                }

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
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                if (errorMessage != "User canceled authentication")
                {
                    MessageBox.Show(errorMessage, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return null;
            }
            else
            {
                return ConvertAuthenticationResultToTokenResponse(authenticationResult);
            }
        }

        private TokenResponse AcquireAccessTokenOfV2MobileApp(string AuthorizationCode)
        {
            TokenResponse result = null;
            string accessToken = "";

            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + textBox_V2MobileAppRedirectUri.Text +
                "&client_id=" + textBox_V2MobileAppClientID.Text +
                "&code=" + AuthorizationCode +
                "&scope=" + textBox_V2MobileAppScopes.Text;

            System.Net.WebRequest request = System.Net.WebRequest.Create("https://login.microsoftonline.com/common/oauth2/v2.0/token");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postBody);
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
                    accessToken = result.access_token;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Office365APIEditor");
            }

            return result;
        }
        
        private TokenResponse AcquireAccessTokenOfV2WebApp(string AuthorizationCode)
        {
            TokenResponse result = null;
            string accessToken = "";

            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(textBox_V2WebAppRedirectUri.Text) +
                "&client_id=" + textBox_V2WebAppClientID.Text +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(textBox_V2WebAppClientSecret.Text) +
                "&code=" + AuthorizationCode +
                "&scope=" + textBox_V2WebAppScopes.Text;

            System.Net.WebRequest request = System.Net.WebRequest.Create("https://login.microsoftonline.com/common/oauth2/v2.0/token");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postBody);
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
                    accessToken = result.access_token;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Office365APIEditor");
            }

            return result;
        }

        private TokenResponse AcquireAccessTokenOfWebAppAppOnly()
        {
            TokenResponse result = null;

            FileStream certFile = File.OpenRead(textBox_WebAppAppOnlyCertPath.Text);
            byte[] certBytes = new byte[certFile.Length];
            certFile.Read(certBytes, 0, (int)certFile.Length);
            var cert = new X509Certificate2(certBytes, textBox_WebAppAppOnlyCertPass.Text, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

            _resource = GetResourceNameForWebAppAppOnly();
            string resourceName = GetResourceURL(_resource);

            ClientAssertionCertificate cac = new ClientAssertionCertificate(textBox_WebAppAppOnlyClientID.Text, cert);
            AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + textBox_WebAppAppOnlyTenantName.Text, false);
            
            AuthenticationResult authenticationResult = null;
            
            string errorMessage = null;
            try
            {
                authenticationResult = authenticationContext.AcquireTokenAsync(resourceName, cac).Result;
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
                return ConvertAuthenticationResultToTokenResponse(authenticationResult);
            }
        }

        private string GetResourceNameForWebApp()
        {
            if (radioButton_WebAppExoResource.Checked)
            {
                return "Exchange Online";
            }
            else if (radioButton_WebAppGraphResource.Checked)
            {
                return "Microsoft Graph";
            }
            else
            {
                return "";
            }
        }

        private string GetResourceNameForNativeApp()
        {
            if (radioButton_NativeAppExoResource.Checked)
            {
                return "Exchange Online";
            }
            else if (radioButton_NativeAppGraphResource.Checked)
            {
                return "Microsoft Graph";
            }
            else
            {
                return "";
            }
        }

        private string GetResourceNameForWebAppAppOnly()
        {
            if (radioButton_WebAppAppOnlyExoResource.Checked)
            {
                return "Exchange Online";
            }
            else if (radioButton_WebAppAppOnlyGraphResource.Checked)
            {
                return "Microsoft Graph";
            }
            else if (radioButton_WebAppAppOnlyManagementResource.Checked)
            {
                return "Office 365 Management API";
            }
            else
            {
                return "";
            }
        }

        public static string GetResourceURL(string ResourceName)
        {
            switch (ResourceName)
            {
                case "Exchange Online":
                    return "https://outlook.office.com/";
                case "Microsoft Graph":
                    return "https://graph.microsoft.com/";
                case "Office 365 Management API":
                    return "https://manage.office.com";
                default:
                    return "";
            }
                
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

        public static TokenResponse ConvertAuthenticationResultToTokenResponse(AuthenticationResult value)
        {
            return new TokenResponse
            {
                token_type = value.AccessTokenType,
                expires_in = "",
                scope = "",
                expires_on = value.ExpiresOn.ToString(),
                not_before = "",
                resource = "",
                access_token = value.AccessToken,
                //refresh_token = value.RefreshToken,
                id_token = value.IdToken
            };
        }

        public void SaveSettings()
        {
            // Save settings.
            Properties.Settings.Default.Save();
        }

        private void button_V2MobileAppAcquireAccessToken_Click(object sender, EventArgs e)
        {
            // The best way to acquire an access token by v2 endpoint is using the Microsoft Authentication Library (MSAL).
            // But AuthenticationResult of MSAL doesn't contain refresh tokne.
            // So we can't use MSAL now.

            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (CheckV2MobileAppParam() == false)
            {
                return;
            }

            string authorizationCode = AcquireV2MobileAppAuthorizationCode();

            if (authorizationCode == "")
            {
                return;
            }

            _tokenResponse = AcquireAccessTokenOfV2MobileApp(authorizationCode);

            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.", "Office365APIEditor");
            }
        }

        private void button_V2MobileAppScopeEditor_Click(object sender, EventArgs e)
        {
            string scopes = "";
            string[] selectedScopes;

            if (textBox_V2MobileAppScopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_V2MobileAppScopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out scopes) == DialogResult.OK)
            {
                textBox_V2MobileAppScopes.Text = scopes;
            }
        }

        private void button_V2WebAppScopeEditor_Click(object sender, EventArgs e)
        {
            string scopes = "";
            string[] selectedScopes;

            if (textBox_V2WebAppScopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_V2WebAppScopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out scopes) == DialogResult.OK)
            {
                textBox_V2WebAppScopes.Text = scopes;
            }
        }

        private void button_V2WebAppAcquireAccessToken_Click(object sender, EventArgs e)
        {
            // The best way to acquire an access token by v2 endpoint is using the Microsoft Authentication Library (MSAL).
            // But AuthenticationResult of MSAL doesn't contain refresh tokne.
            // So we can't use MSAL now.

            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (CheckV2WebAppParam() == false)
            {
                return;
            }

            string authorizationCode = AcquireV2WebAppAuthorizationCode();

            if (authorizationCode == "")
            {
                return;
            }

            _tokenResponse = AcquireAccessTokenOfV2WebApp(authorizationCode);

            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.", "Office365APIEditor");
            }
        }

        private void button_WebAppAppOnlySelectSert_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (openFileDialog_PFX.ShowDialog() == DialogResult.OK)
            {
                textBox_WebAppAppOnlyCertPath.Text = openFileDialog_PFX.FileName;
            }
        }

        private void linkLabel_WebApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application.md");
        }

        private void linkLabel_NativeApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Native_application.md");
        }

        private void linkLabel_WebAppAppOnly_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/How_to_register_a_V1_Web_application_for_App_Only_Token.md");
        }

        private void radioButton_WebAppGraphResource_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_WebAppClientSecret_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton_WebAppExoResource_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox_WebAppClientID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_WebAppRedirectUri_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
