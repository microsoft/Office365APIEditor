// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class RequestForm : Form
    {
        ClientInformation clientInfo;

        string originalResponseHeaders = "";
        string originalJsonResponse = "";
        string indentedJsonResponse = "";
        string decodedJsonResponse = "";
        string indentedAndDecodedJsonResponse = "";

        public RequestForm()
        {
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            button_RefreshToken.Enabled = false;
            button_Run.Enabled = false;
            button_ViewTokenInfo.Enabled = false;

            AddKeyDownEvent(this);
        }

        private void AddKeyDownEvent(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBoxControl = item as TextBox;
                    textBoxControl.KeyDown += TextBoxControl_KeyDown;
                    System.Diagnostics.Debug.WriteLine(textBoxControl.Name);
                }

                AddKeyDownEvent(item);
                
            }
        }

        private async void button_Run_Click(object sender, EventArgs e)
        {
            originalResponseHeaders = "";
            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            WebRequest request = WebRequest.Create(textBox_Request.Text);
            request.ContentType = "application/json";

            if (clientInfo.AuthType == AuthEndpoints.Basic)
            {
                // Basic authentication

                if (textBox_BasicAuthSMTPAddress.Text == "")
                {
                    MessageBox.Show("Enter your SMTP address", "Office365APIEditor");
                    textBox_BasicAuthSMTPAddress.Focus();
                    return;
                }

                if (textBox_BasicAuthPassword.Text == "")
                {
                    MessageBox.Show("Enter your password", "Office365APIEditor");
                    textBox_BasicAuthPassword.Focus();
                    return;
                }

                string credential = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(textBox_BasicAuthSMTPAddress.Text + ":" + textBox_BasicAuthPassword.Text));
                request.Headers.Add("Authorization:Basic " + credential);
            }
            else
            {
                // OAuth authentication
                request.Headers.Add("Authorization:Bearer " + clientInfo.Token.access_token);
            }

            if (radioButton_GET.Checked)
            {
                // Request is GET.
                request.Method = "GET";
            }
            else if (radioButton_POST.Checked)
            {
                // Request is POST.
                request.Method = "POST";

                // Build a body.
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string body = textBox_RequestBody.Text;

                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else if (radioButton_PATCH.Checked)
            {
                // Request if PATCH
                request.Method = "PATCH";

                // Build a body.
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string body = textBox_RequestBody.Text;

                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            else
            {
                // Request is DELETE.
                request.Method = "DELETE";
            }

            // Add headers
            foreach (string header in textBox_RequestHeaders.Lines)
            {
                request.Headers.Add(header);
            }

            try
            {
                // Change cursor.
                Application.UseWaitCursor = true;

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteRequestLog(request, textBox_RequestBody.Text);
                }

                // Get a response and response stream.
                // System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                var response = (HttpWebResponse)await request.GetResponseAsync();

                string jsonResponse = "";
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteResponseLog(response, jsonResponse);
                }

                // Display the results.
                DisplayResponse(response.StatusCode.ToString(), response.Headers, jsonResponse);

                // Save application setting.
                Properties.Settings.Default.Save();
            }
            catch (WebException ex)
            {
                string jsonResponse = "";
                using (Stream responseStream = ex.Response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteResponseLog((HttpWebResponse)ex.Response, jsonResponse);
                }

                DisplayResponse(((HttpWebResponse)ex.Response).StatusCode.ToString(), ex.Response.Headers, jsonResponse);
            }
            catch (Exception ex)
            {
                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteCustomLog("Response", ex.Message);
                }

                DisplayResponse("Error", null, ex.Message);
            }
            finally
            {
                // Change cursor.
                Application.UseWaitCursor = false;
            }
        }

        private void TextBoxControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as TextBox).SelectAll();
            }
        }

        private void button_ViewTokenInfo_Click(object sender, EventArgs e)
        {
            TokenViewer tokenViewer = new TokenViewer(clientInfo.Token);
            tokenViewer.ShowDialog();
        }

        private async void button_RefreshToken_Click(object sender, EventArgs e)
        {
            // Request another access token with refresh token.

            originalResponseHeaders = "";
            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            string endPoint = "https://login.microsoftonline.com/common/oauth2/";

            // Build a POST body.
            string postBody = "";
            Hashtable tempTable = new Hashtable();

            tempTable["grant_type"] = "refresh_token";
            tempTable["refresh_token"] = clientInfo.Token.refresh_token;

            if (clientInfo.AuthType == AuthEndpoints.OAuthV1)
            {
                string resourceURL = StartForm.GetResourceURL(clientInfo.ResourceUri);
                tempTable["resource"] = System.Web.HttpUtility.UrlEncode(resourceURL);

                if (clientInfo.ClientID != "")
                {
                    // If _clientID has value, we're working with web app.
                    // So we have to add Client ID and Client Secret.
                    tempTable["client_id"] = clientInfo.ClientID;
                    tempTable["client_secret"] = clientInfo.ClientSecret;
                }
            }
            else
            {
                endPoint += "v2.0/";
                tempTable["scope"] = clientInfo.Scopes;
                tempTable["client_id"] = clientInfo.ClientID;
                tempTable["redirect_uri"] = clientInfo.ResourceUri;

                if (clientInfo.ClientID != "")
                {
                    // If _clientID has value, we're working with web app.
                    // So we have to add Client Secret.
                    tempTable["client_secret"] = clientInfo.ClientSecret;
                }
            }

            foreach (string key in tempTable.Keys)
            {
                postBody += String.Format("{0}={1}&", key, tempTable[key]);
            }
            byte[] postDataBytes = Encoding.ASCII.GetBytes(postBody);

            WebRequest request = WebRequest.Create(endPoint + "token/");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataBytes.Length;

            try
            {
                // Change a cursor.
                Application.UseWaitCursor = true;

                // Get a RequestStream to POST a data.
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(postDataBytes, 0, postDataBytes.Length);
                }
                
                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteRequestLog(request, postBody);
                }

                string jsonResponse = "";

                // System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                var response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteResponseLog(response, jsonResponse);
                }

                // Display the results.
                DisplayResponse(response.StatusCode.ToString(), response.Headers, jsonResponse);

                // Deserialize and get Access Token.
                clientInfo.ReplaceToken(StartForm.Deserialize<TokenResponse>(jsonResponse));
            }
            catch (WebException ex)
            {
                string jsonResponse = "";
                using (Stream responseStream = ex.Response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteResponseLog((HttpWebResponse)ex.Response, jsonResponse);
                }

                DisplayResponse(((HttpWebResponse)ex.Response).StatusCode.ToString(), ex.Response.Headers, jsonResponse);
            }
            catch (Exception ex)
            {
                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteCustomLog("Response", ex.Message);
                }

                DisplayResponse("Error", null, ex.Message);
            }
            finally
            {
                // Change cursor.
                Application.UseWaitCursor = false;
            }
        }

        private void checkBox_Indent_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                textBox_ResponseBody.Text = shapeJsonResponseIfNeeded(originalJsonResponse);
            }
        }

        private void checkBox_Decode_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                textBox_ResponseBody.Text = shapeJsonResponseIfNeeded(originalJsonResponse);
            }
        }

        public void DisplayResponse(string StatusCode, WebHeaderCollection Headers, string JsonResponse)
        {
            // Status code
            label_StatusCode.Text = StatusCode;

            // Header
            if (Headers == null)
            {
                originalResponseHeaders = "";
                textBox_ResponseHeaders.Text = originalResponseHeaders;
            }
            else
            {
                originalResponseHeaders = Headers.ToString();
                textBox_ResponseHeaders.Text = originalResponseHeaders;
            }

            // Body
            originalJsonResponse = JsonResponse;
            textBox_ResponseBody.Text = shapeJsonResponseIfNeeded(originalJsonResponse);

            // Show Body tab
            tabControl_Response.SelectTab(1);
            textBox_ResponseBody.Select(0, 0);
        }

        public string shapeJsonResponseIfNeeded(string Data)
        {
            // Check the status of checkbox and shape the JSON Response.

            string result = Data;

            if (checkBox_Indent.Checked && checkBox_Decode.Checked)
            {
                if (indentedAndDecodedJsonResponse == "")
                {
                    indentedAndDecodedJsonResponse = DecodeJsonResponse(parseJsonResponse(result));
                }

                result = indentedAndDecodedJsonResponse;
            }
            else if (checkBox_Indent.Checked)
            {
                if (indentedJsonResponse == "")
                {
                    indentedJsonResponse = parseJsonResponse(result);
                }

                result = indentedJsonResponse;
            }
            else if (checkBox_Decode.Checked)
            {
                if (decodedJsonResponse == "")
                {
                    decodedJsonResponse = DecodeJsonResponse(result);
                }

                result = decodedJsonResponse;
            }

            return result;
        }

        public static string parseJsonResponse(string Data)
        {
            string tabString = "\t";

            int indentCount = 0;
            int quoteCount = 0;
            var result = from c in Data
                         let quotes = (c == '"') ? quoteCount++ : quoteCount
                         let lineBreak = (c == ',' && quotes % 2 == 0) ? c + Environment.NewLine + string.Concat(Enumerable.Repeat(tabString, indentCount)) : null
                         let openChar = (c == '{' || c == '[') ? c + Environment.NewLine + string.Concat(Enumerable.Repeat(tabString, ++indentCount)) : c.ToString()
                         let closeChar = (c == '}' || c == ']') ? Environment.NewLine + string.Concat(Enumerable.Repeat(tabString, --indentCount)) + c : c.ToString()
                         select (lineBreak == null) ? (openChar.Length > 1) ? openChar : closeChar : lineBreak;

            return string.Concat(result);
        }

        public static string DecodeJsonResponse(string jsonResponse)
        {
            // Convert unicode style character to string.
            // Then, convert to their unescaped form.

            Hashtable CharTable = new Hashtable();

            Regex unicodeExpression = new Regex(@"(\\u){1}[0-9a-fA-F]{4}");

            for (Match matchedUniCode = unicodeExpression.Match(jsonResponse); matchedUniCode.Success; matchedUniCode = matchedUniCode.NextMatch())
            {
                string unicodeValue = matchedUniCode.Groups[0].Value.Replace(@"\u", "");

                if (!CharTable.ContainsKey(unicodeValue))
                {
                    char[] charArray = unicodeValue.ToCharArray();
                    int intValue1 = Convert.ToByte(charArray[0].ToString(), 16) * 16 + Convert.ToByte(charArray[1].ToString(), 16);
                    int intValue2 = Convert.ToByte(charArray[2].ToString(), 16) * 16 + Convert.ToByte(charArray[3].ToString(), 16);

                    string encodedValue = Encoding.Unicode.GetString(new byte[] { (byte)intValue2, (byte)intValue1 });

                    CharTable.Add(unicodeValue, encodedValue);
                }
            }

            string result = jsonResponse;

            foreach (string key in CharTable.Keys)
            {
                result = result.Replace("\\u" + key, CharTable[key].ToString());
            }

            return Regex.Unescape(result);
        }

        private void WriteRequestLog(WebRequest RequestToLog, string Body)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Request");
            sb.AppendLine("DateTime : " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            sb.AppendLine("Method : " + RequestToLog.Method);
            sb.AppendLine("URL : " + RequestToLog.RequestUri.ToString());
            sb.AppendLine("Header : ");
            foreach (var header in RequestToLog.Headers.AllKeys)
            {
                sb.AppendLine("  " + header + " : " + RequestToLog.Headers.Get(header));
            }

            if (RequestToLog.Method == "POST" || RequestToLog.Method == "PATCH")
            {
                sb.AppendLine("Body : ");
                sb.AppendLine("  " + Body.Replace(Environment.NewLine, Environment.NewLine + "  "));
            }

            WriteLog(sb);
        }

        private void WriteResponseLog(HttpWebResponse ResponseToLog, string ResponseBody)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Response");
            sb.AppendLine("DateTime : " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            sb.AppendLine("StatusCode : " + ResponseToLog.StatusCode.ToString());
            sb.AppendLine("Header : ");
            foreach (var header in ResponseToLog.Headers.AllKeys)
            {
                sb.AppendLine("  " + header + " : " + ResponseToLog.Headers.Get(header));
            }

            sb.AppendLine("Body : ");
            sb.AppendLine("  " + ResponseBody.Replace(Environment.NewLine, Environment.NewLine + "  "));

            WriteLog(sb);
        }

        private void WriteCustomLog(string Title, string Message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Title);
            sb.AppendLine("DateTime : " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            sb.AppendLine(Message);

            WriteLog(sb);
        }

        private void WriteLog(StringBuilder Message)
        {
            Message.AppendLine("");

            string logFilePath = "";

            if (!Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                // Specified log folder path is not exsisting.
                MessageBox.Show("The Specified log folder path is not exsisting.", "Office365APIEditor");
                return;
            }

            if (Properties.Settings.Default.LogFileStyle == "Static")
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, "Office365APIEditor.log");
            }
            else if (Properties.Settings.Default.LogFileStyle == "DateTime")
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, DateTime.UtcNow.ToString("yyyyMMdd") + ".log");
            }
            else
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, "Office365APIEditor.log");
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true, Encoding.Default))
                {
                    sw.Write(Message.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to write the log.\r\n\r\n" + ex.Message, "Office365APIEditor");
            }
        }

        private void newAccessTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientInformation newClientInfo;

            AccessTokenWizard accessTokenWizard = new AccessTokenWizard();
            if (accessTokenWizard.ShowDialog(out newClientInfo) == DialogResult.OK)
            {
                // Override current info.
                clientInfo = newClientInfo;

                if (clientInfo.AuthType == AuthEndpoints.Basic)
                {
                    // Basic auth

                    textBox_BasicAuthSMTPAddress.Enabled = true;
                    textBox_BasicAuthSMTPAddress.Text = "";
                    textBox_BasicAuthPassword.Enabled = true;
                    textBox_BasicAuthPassword.Text = "";
                    textBox_BasicAuthPassword.UseSystemPasswordChar = true;
                }
                else if (clientInfo.AuthType == AuthEndpoints.OAuthV1)
                {
                    // OAuth V1

                    textBox_BasicAuthSMTPAddress.Enabled = false;
                    textBox_BasicAuthSMTPAddress.Text = "OAuth (V1 Endpoint)";
                    textBox_BasicAuthPassword.Enabled = false;
                    textBox_BasicAuthPassword.Text = "OAuth (V1 Endpoint)";
                    textBox_BasicAuthPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    // OAuth V2

                    textBox_BasicAuthSMTPAddress.Enabled = false;
                    textBox_BasicAuthSMTPAddress.Text = "OAuth (V2 Endpoint)";
                    textBox_BasicAuthPassword.Enabled = false;
                    textBox_BasicAuthPassword.Text = "OAuth (V2 Endpoint)";
                    textBox_BasicAuthPassword.UseSystemPasswordChar = false;
                }

                button_ViewTokenInfo.Enabled = !string.IsNullOrEmpty(clientInfo.Token.access_token);
                button_RefreshToken.Enabled = !string.IsNullOrEmpty(clientInfo.Token.refresh_token);
                button_Run.Enabled = true;

                // Select the Body page.
                tabControl_Request.SelectTab(1);
            }
        }

        private void loggingOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the LoggingOptionWindow
            LoggingOption loggingOption = new LoggingOption();
            loggingOption.ShowDialog();
        }
    }
}
