using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class RequestForm : Form
    {
        bool useBasicAuth = false;
        TokenResponse _tokenResponse = null;
        string _resource = "";
        string _clientID = "";
        string _clientSecret = "";

        public RequestForm()
        {
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            if (startForm.ShowDialog(out _tokenResponse, out _resource, out _clientID, out _clientSecret) == DialogResult.OK)
            {
                if (_tokenResponse.access_token.StartsWith("USEBASICBASIC"))
                {
                    useBasicAuth = true;
                    textBox_BasicAuthSMTPAddress.Enabled = true;
                    textBox_BasicAuthPassword.Enabled = true;
                    button_ViewTokenInfo.Enabled = false;
                }
                else
                {
                    useBasicAuth = false;
                    textBox_BasicAuthSMTPAddress.Enabled = false;
                    textBox_BasicAuthSMTPAddress.Text = "OAuth (" + _resource + ")";
                    textBox_BasicAuthPassword.Enabled = false;
                    textBox_BasicAuthPassword.Text = "OAuth (" + _resource + ")";
                    textBox_BasicAuthPassword.UseSystemPasswordChar = false;
                    button_ViewTokenInfo.Enabled = true;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            //文字コードを指定する
            Encoding enc = Encoding.Default;

            System.Net.WebRequest req = System.Net.WebRequest.Create(textBox_Request.Text);

            if (useBasicAuth == true)
            {
                // Basic authentication
                string cred = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(textBox_BasicAuthSMTPAddress.Text + ":" + textBox_BasicAuthPassword.Text));
                req.Headers.Add("Authorization:Basic " + cred);
            }
            else
            {
                // OAuth authentication
                req.Headers.Add("Authorization:Bearer " + _tokenResponse.access_token);
            }

            if (radioButton_GET.Checked)
            {
                // Request is GET.
                req.Method = "GET";
                req.ContentType = "application/json";
            }
            else if (radioButton_POST.Checked)
            {
                // Request is POST.
                req.Method = "POST";
                req.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    string json = textBox_RequestBody.Text;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else if (radioButton_PATCH.Checked)
            {
                // Request if PATCH
                req.Method = "PATCH";
                req.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    string json = textBox_RequestBody.Text;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else
            {
                // Request is DELETE.
                req.Method = "DELETE";
                req.ContentType = "application/json";
            }
            
            try
            {
                // Change cursor.
                this.Cursor = Cursors.WaitCursor;

                //サーバーからの応答を受信するためのWebResponseを取得
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
                //応答データを受信するためのStreamを取得
                string jsonResponse = "";
                using (System.IO.Stream resStream = res.GetResponseStream())
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(resStream, enc);
                    jsonResponse = sr.ReadToEnd();
                }

                // Display the results.
                textBox_Result.Text = "StatusCode : " + res.StatusCode.ToString() + "\r\n\r\n";
                textBox_Result.Text += "Response Header : \r\n" + res.Headers.ToString() + "\r\n\r\n";

                // JSON をパースして読みやすくする。
                textBox_Result.Text = parseJson(jsonResponse);

                // Save application setting.
                Properties.Settings.Default.Save();
            }
            catch (System.Net.WebException ex)
            {
                textBox_Result.Text = ex.Message + "\r\n\r\nResponse Headers : \r\n" + ex.Response.Headers.ToString();
            }
            catch (Exception ex)
            {
                textBox_Result.Text = ex.Message;
            }
            finally
            {
                // Change cursor.
                this.Cursor = Cursors.Default;
            }
        }

        private void textBox_Request_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Request.SelectAll();
            }
        }

        private void textBox_RequestBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_RequestBody.SelectAll();
            }
        }

        private void textBox_Result_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Result.SelectAll();
            }
        }

        private void button_ViewTokenInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_tokenResponse.Format(), "Office365APIEditor");
        }

        private void button_RefreshToken_Click(object sender, EventArgs e)
        {
            // Request another access token with refresh token.

            //文字コードを指定する
            Encoding enc = Encoding.Default;

            // string resourceURL = StartForm.GetResourceURL(_resource);
            string resourceURL = StartForm.GetResourceURL(_resource);

            //POST送信する文字列を作成
            string param = "";
            Hashtable ht = new Hashtable();

            ht["grant_type"] = "refresh_token";
            ht["refresh_token"] = _tokenResponse.refresh_token;
            ht["resource"] = System.Web.HttpUtility.UrlEncode(resourceURL);

            if (_clientID != "")
            {
                // If _clientID has value, we're working with web app.
                ht["client_id"] = _clientID;
                ht["client_secret"] = _clientSecret;
            }
            
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
                // Change a cursor.
                this.Cursor = Cursors.WaitCursor;

                //データをPOST送信するためのStreamを取得
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    //送信するデータを書き込む
                    reqStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                //サーバーからの応答を受信するためのWebResponseを取得
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
                //応答データを受信するためのStreamを取得
                string jsonResponse = "";
                using (System.IO.Stream resStream = res.GetResponseStream())
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(resStream, enc);
                    jsonResponse = sr.ReadToEnd();
                }

                // Display the results.
                textBox_Result.Text = "StatusCode : " + res.StatusCode.ToString() + "\r\n\r\n";
                textBox_Result.Text += "Response Header : \r\n" + res.Headers.ToString() + "\r\n\r\n";

                // JSON をパースして読みやすくする。
                textBox_Result.Text = parseJson(jsonResponse);

                // デシリアライズして Access Token を取得
                _tokenResponse = StartForm.Deserialize<TokenResponse>(jsonResponse);
            }
            catch (System.Net.WebException ex)
            {
                textBox_Result.Text = ex.Message + "\r\n\r\nResponse Headers : \r\n" + ex.Response.Headers.ToString();
            }
            catch (Exception ex)
            {
                textBox_Result.Text = ex.Message;
            }
            finally
            {
                // Change cursor.
                this.Cursor = Cursors.Default;
            }
        }

        public string parseJson(string Data)
        {
            TextElementEnumerator textEnum = StringInfo.GetTextElementEnumerator(Data);
            StringBuilder parsedData = new StringBuilder();

            Int32 indentCount = 0;

            while (true)
            {
                if (textEnum.MoveNext() == false)
                {
                    break;
                }

                if (textEnum.Current.ToString() == ",")
                {
                    parsedData.Append(textEnum.Current + "\r\n" + CreateTabString(indentCount));
                }
                else if (textEnum.Current.ToString() == "{")
                {
                    indentCount += 1;
                    parsedData.Append(textEnum.Current + "\r\n" + CreateTabString(indentCount));
                }
                else if (textEnum.Current.ToString() == "}")
                {
                    indentCount -= 1;
                    parsedData.Append(textEnum.Current);
                }
                else
                {
                    parsedData.Append(textEnum.Current);
                }
            }

            return parsedData.ToString();
        }

        private string CreateTabString(Int32 Length)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Length; i++)
            {
                result.Append("\t");
            }

            return result.ToString();
        }

    }
}
