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
        // 戻り値用
        
        private TokenResponse _tokenResponse;
        private string _resource = "";

        public StartForm()
        {
            InitializeComponent();
        }
        
        private void StartForm_Load(object sender, EventArgs e)
        {
            // RequestForm で自動的に保存してしまうため、SMTP アドレスではなく OAuth になっている場合がある。
            // その場合は内容をクリアする。
            if (textBox_BasicAuthSMTPAddress.Text.StartsWith("OAuth"))
            {
                textBox_BasicAuthSMTPAddress.Text = "";
            }
        }

        public enum ResourceTypes
        {

        }

        public DialogResult ShowDialog(out TokenResponse AccessToken, out string Resource, out string ClientID, out string ClientSecret)
        {
            DialogResult reult = this.ShowDialog();
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

            button_AcquireAccessToken.Text = "Acquire Access Token";
        }

        private void radioButton_NativeApp_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_NativeApp.Enabled = true;
            groupBox_WebApp.Enabled = false;
            groupBox_BasicAuth.Enabled = false;

            button_AcquireAccessToken.Text = "Acquire Access Token";
        }

        private void radioButton_BasicAuth_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_BasicAuth.Enabled = true;
            groupBox_NativeApp.Enabled = false;
            groupBox_WebApp.Enabled = false;

            button_AcquireAccessToken.Text = "OK";
        }

        private void button_AcquireAccessToken_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            _tokenResponse = null;

            if (radioButton_BasicAuth.Checked)
            {
                //Basic auth の場合
                if (CheckBasicAuthParam() == false)
                {
                    return;
                }
                else
                {
                    SaveSettings();

                    _tokenResponse = new TokenResponse { access_token = "USEBASICBASIC" };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else if (radioButton_WebApp.Checked)
            {
                // Web app の場合
                if (CheckWebAppParam() == false)
                {
                    return;
                }
                else if (textBox_WebAppCode.Text == "")
                {
                    MessageBox.Show("Enter the Authorization Code.", "RESTAPIEditor");
                    return;
                }

                _tokenResponse = AcquireAccessTokenOfWebApp();
            }
            else
            {
                // Native app の場合
                if (CheckNativeAppParam() == false)
                {
                    return;
                }

                _tokenResponse = AcquireAccessTokenOfNativeApp();
            }

            if (_tokenResponse != null)
            {
                SaveSettings();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Acquiring Access Token was failed.");
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_WebAppGetCode_Click(object sender, EventArgs e)
        {
            if (CheckWebAppParam() == false)
            {
                return;
            }

            string Code = "";

            _resource = GetResourceNameForWebApp();
            GetCodeForm getCodeForm = new GetCodeForm(textBox_WebAppClientID.Text, textBox_WebAppRedirectUri.Text, GetResourceURL(_resource));
            if (getCodeForm.ShowDialog(out Code) == DialogResult.OK)
            {
                if (Code == "")
                {
                    MessageBox.Show("Getting Authorization Code was failed.", "RESTAPIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox_WebAppCode.Text = "";
                }
                else
                {
                    textBox_WebAppCode.Text = Code;
                }
            }
            else
            {
                MessageBox.Show("authentication_canceled: User canceled authentication", "RESTAPIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_WebAppCode.Text = "";
            }
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
            // Web app の入力値のチェック
            if (textBox_WebAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "RESTAPIEditor");
                return false;
            }
            else if (textBox_WebAppRedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "RESTAPIEditor");
                return false;
            }
            else if (!IsValidUrl(textBox_WebAppRedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "RESTAPIEditor");
                return false;
            }
            else if (textBox_WebAppClientSecret.Text == "")
            {
                MessageBox.Show("Enter the Client Secret.", "RESTAPIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckNativeAppParam()
        {
            // Native app の入力値チェック
            if (textBox_NativeAppTenantName.Text == "")
            {
                MessageBox.Show("Enter the Tenant Name.", "RESTAPIEditor");
                return false;
            }
            else if (!textBox_NativeAppTenantName.Text.EndsWith(".onmicrosoft.com"))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "RESTAPIEditor");
                return false;
            }
            else if (!IsValidUrl("https://login.windows.net/" + textBox_NativeAppTenantName.Text))
            {
                MessageBox.Show("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com", "RESTAPIEditor");
                return false;
            }
            else if (textBox_NativeAppClientID.Text == "")
            {
                MessageBox.Show("Enter the Client ID.", "RESTAPIEditor");
                return false;
            }
            else if (textBox_NativeAppRedirectUri.Text == "")
            {
                MessageBox.Show("Enter the Redirect URL.", "RESTAPIEditor");
                return false;
            }
            else if (!IsValidUrl(textBox_NativeAppRedirectUri.Text))
            {
                MessageBox.Show("Format of Redirect URL is invalid.", "RESTAPIEditor");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckBasicAuthParam()
        {
            try
            {
                System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(textBox_BasicAuthSMTPAddress.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Format of SMTP Address is invalid.", "Office 365 API Editor");
                return false;
            }
        }

        private TokenResponse AcquireAccessTokenOfWebApp()
        {
            TokenResponse result = null;
            string accessToken = "";

            //文字コードを指定する
            System.Text.Encoding enc = System.Text.Encoding.Default;

            //POST送信する文字列を作成
            string param = "";
            Hashtable ht = new Hashtable();

            ht["grant_type"] = "authorization_code";
            ht["code"] = textBox_WebAppCode.Text;
            ht["redirect_uri"] = textBox_WebAppRedirectUri.Text;
            ht["client_id"] = textBox_WebAppClientID.Text;
            ht["client_secret"] = textBox_WebAppClientSecret.Text;

            foreach (string k in ht.Keys)
            {
                param += String.Format("{0}={1}&", k, ht[k]);
            }
            byte[] postDataBytes = Encoding.ASCII.GetBytes(param);

            //WebRequestの作成
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://login.windows.net/common/oauth2/token/");
            //メソッドにPOSTを指定
            req.Method = "POST";
            //ContentTypeを"application/x-www-form-urlencoded"にする
            req.ContentType = "application/x-www-form-urlencoded";
            //POST送信するデータの長さを指定
            req.ContentLength = postDataBytes.Length;

            try
            {
                //データをPOST送信するためのStreamを取得
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    //送信するデータを書き込む
                    reqStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //サーバーからの応答を受信するためのWebResponseを取得
                System.Net.WebResponse res = req.GetResponse();
                //応答データを受信するためのStreamを取得
                using (System.IO.Stream resStream = res.GetResponseStream())
                {
                    //受信して表示
                    System.IO.StreamReader sr = new System.IO.StreamReader(resStream, enc);
                    string response = sr.ReadToEnd();

                    // デシリアライズして Access Token を取得
                    result = Deserialize<TokenResponse>(response);
                    accessToken = result.access_token;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
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
                // Office365 のサインインページを表示する
                authenticationResult = authenticationContext.AcquireToken(resourceName, clientId, redirectUri, PromptBehavior.Always);

                // 資格情報をハードコードする場合
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
                MessageBox.Show(errorMessage, "RESTAPIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            T returnValue;
            using (var memoryStream = new MemoryStream())
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                memoryStream.Write(jsonBytes, 0, jsonBytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    returnValue = (T)serializer.ReadObject(jsonReader);

                }
            }
            return returnValue;
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
