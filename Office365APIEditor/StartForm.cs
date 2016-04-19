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

namespace Office365APIEditor
{
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

        public DialogResult ShowDialog(out TokenResponse AccessToken, out string Resource, out string ClientID, out string ClientSecret)
        {
            DialogResult reult = this.ShowDialog();

            // Build return values.

            AccessToken = _tokenResponse;
            Resource = _resource;

            if (radioButton_WebApp.Checked)
            {
                ClientID = textBox_WebAppClientID.Text;
                ClientSecret = textBox_WebAppClientSecret.Text;
            }
            else
            {
                ClientID = "";
                ClientSecret = "";
            }

            return reult;
        }

        private void radioButton_WebApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_WebApp.Enabled = true;
            groupBox_NativeApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
        }

        private void radioButton_NativeApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_NativeApp.Enabled = true;
            groupBox_WebApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;
        }

        private void radioButton_BasicAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_BasicAuth.Enabled = true;
            groupBox_NativeApp.Enabled = false;
            groupBox_WebApp.Enabled = false;
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

            string authorizationCode = AcquireAuthorizationCode();

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

        private string AcquireAuthorizationCode()
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

        private TokenResponse AcquireAccessTokenOfWebApp(string AuthorizationCode)
        {
            TokenResponse result = null;
            string accessToken = "";

            // Build a POST body.
            string postBody = "";
            Hashtable tempTable = new Hashtable();

            tempTable["grant_type"] = "authorization_code";
            tempTable["code"] = AuthorizationCode;
            tempTable["redirect_uri"] = textBox_WebAppRedirectUri.Text;
            tempTable["client_id"] = textBox_WebAppClientID.Text;
            tempTable["client_secret"] = textBox_WebAppClientSecret.Text;

            foreach (string key in tempTable.Keys)
            {
                postBody += String.Format("{0}={1}&", key, tempTable[key]);
            }
            byte[] postDataBytes = Encoding.ASCII.GetBytes(postBody);

            System.Net.WebRequest request = System.Net.WebRequest.Create("https://login.windows.net/common/oauth2/token/");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataBytes.Length;

            try
            {
                // Get a RequestStream to POST a data.
                using (Stream reqestStream = request.GetRequestStream())
                {
                    reqestStream.Write(postDataBytes, 0, postDataBytes.Length);
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
            string authority = "https://login.windows.net/" + textBox_NativeAppTenantName.Text;
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
                authenticationResult = authenticationContext.AcquireToken(resourceName, clientId, redirectUri, PromptBehavior.Always);

                // If we use hardcoded user credential, use this method.
                // authenticationResult = authenticationContext.AcquireToken(resourceName, clientId, new UserCredential("SMTP Address", "password"));
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

        public static string GetResourceURL(string ResourceName)
        {
            switch (ResourceName)
            {
                case "Exchange Online":
                    return "https://outlook.office.com/";
                case "Microsoft Graph":
                    return "https://graph.microsoft.com/";
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
                refresh_token = value.RefreshToken,
                id_token = value.IdToken
            };
        }

        public void SaveSettings()
        {
            // Save settings.
            Properties.Settings.Default.Save();
        }
    }
}
