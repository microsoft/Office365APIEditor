// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            // First of all, we have to get an access token.

            StartForm startForm = new StartForm();
            if (startForm.ShowDialog(out _tokenResponse, out _resource, out _clientID, out _clientSecret) == DialogResult.OK)
            {
                if (_tokenResponse.access_token.StartsWith("USEBASICBASIC"))
                {
                    // Basic auth

                    useBasicAuth = true;
                    textBox_BasicAuthSMTPAddress.Enabled = true;
                    textBox_BasicAuthPassword.Enabled = true;
                    button_ViewTokenInfo.Enabled = false;
                    button_RefreshToken.Enabled = false;
                }
                else
                {
                    // OAuth

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
            originalResponseHeaders = "";
            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            System.Net.WebRequest request = System.Net.WebRequest.Create(textBox_Request.Text);
            request.ContentType = "application/json";

            if (useBasicAuth == true)
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
                request.Headers.Add("Authorization:Bearer " + _tokenResponse.access_token);
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
            
            try
            {
                // Change cursor.
                this.Cursor = Cursors.WaitCursor;

                // Get a response and response stream.
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                string jsonResponse = "";
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Display the results.
                originalResponseHeaders = "StatusCode : " + response.StatusCode.ToString() + "\r\n\r\n";
                originalResponseHeaders += "Response Header : \r\n" + response.Headers.ToString() + "\r\n\r\n";

                // Shape the JSON data.
                originalJsonResponse = jsonResponse;
                textBox_Result.Text = originalResponseHeaders + shapeJsonResponseIfNeeded(originalJsonResponse);

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
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Request.SelectAll();
            }
        }

        private void textBox_RequestBody_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_RequestBody.SelectAll();
            }
        }

        private void textBox_Result_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
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

            originalResponseHeaders = "";
            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            string resourceURL = StartForm.GetResourceURL(_resource);

            // Build a POST body.
            string postBody = "";
            Hashtable tempTable = new Hashtable();

            tempTable["grant_type"] = "refresh_token";
            tempTable["refresh_token"] = _tokenResponse.refresh_token;
            tempTable["resource"] = System.Web.HttpUtility.UrlEncode(resourceURL);

            if (_clientID != "")
            {
                // If _clientID has value, we're working with web app.
                // So we have to add Client ID and Client Secret.
                tempTable["client_id"] = _clientID;
                tempTable["client_secret"] = _clientSecret;
            }
            
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
                // Change a cursor.
                this.Cursor = Cursors.WaitCursor;

                // Get a RequestStream to POST a data.
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(postDataBytes, 0, postDataBytes.Length);
                }

                string jsonResponse = "";

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    jsonResponse = reader.ReadToEnd();
                }

                // Display the results.
                originalResponseHeaders = "StatusCode : " + response.StatusCode.ToString() + "\r\n\r\n";
                originalResponseHeaders += "Response Header : \r\n" + response.Headers.ToString() + "\r\n\r\n";

                originalJsonResponse = jsonResponse;

                // Parse the JSON data.
                textBox_Result.Text = originalResponseHeaders + shapeJsonResponseIfNeeded(originalJsonResponse);

                // Deserialize and get Access Token.
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

        private void checkBox_Indent_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                textBox_Result.Text = originalResponseHeaders + shapeJsonResponseIfNeeded(originalJsonResponse);
            }
        }

        private void checkBox_Decode_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                textBox_Result.Text = originalResponseHeaders + shapeJsonResponseIfNeeded(originalJsonResponse);
            }
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

        public string parseJsonResponse(string Data)
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
                    // If ',' appreared, add new line.
                    parsedData.Append(textEnum.Current + "\r\n" + CreateTabString(indentCount));
                }
                else if (textEnum.Current.ToString() == "{")
                {
                    // If '{' appreared, add new new line and increment indentCount by 1.
                    indentCount += 1;
                    parsedData.Append(textEnum.Current + "\r\n" + CreateTabString(indentCount));
                }
                else if (textEnum.Current.ToString() == "}")
                {
                    // If '}' appreared, decrement indentCount by 1.
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
    }
}
