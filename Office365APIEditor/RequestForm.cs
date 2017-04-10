// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections;
using System.Drawing;
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

        string originalJsonResponse = "";
        string indentedJsonResponse = "";
        string decodedJsonResponse = "";
        string indentedAndDecodedJsonResponse = "";

        RunHistory runHistory = new RunHistory();

        int hoveredIndex = -1;

        public RequestForm()
        {
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            button_RefreshToken.Enabled = false;
            button_Run.Enabled = false;
            button_ViewTokenInfo.Enabled = false;

            // Enable Ctrl+A short cut key for all textbox.
            AddKeyDownEvent(this);
            
            // Load Run History
            try
            {
                string runHistoryFilePath = Path.Combine(Application.StartupPath, "RunHistory.xml");
                if (File.Exists(runHistoryFilePath))
                {
                    using (FileStream stream = new FileStream(runHistoryFilePath, FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(RunHistory));
                        runHistory = (RunHistory)serializer.Deserialize(stream);
                    }
                }
                else
                {
                    runHistory.RunInfo = new System.Collections.Generic.List<RunInformation>();
                }
            }
            catch(Exception ex)
            {
                runHistory.RunInfo = new System.Collections.Generic.List<RunInformation>();

                MessageBox.Show("The run history file could not be loaded." + Environment.NewLine + Environment.NewLine + ex.Message, "Office365APIEditor");
            }

            listBox_RunHistory.DrawMode = DrawMode.OwnerDrawFixed;
            listBox_RunHistory.ItemHeight = 80;

            for (int i = runHistory.RunInfo.Count -1 ; i >= 0 ; i--)
            {
                listBox_RunHistory.Items.Add(runHistory.RunInfo[i]);
            }
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

        private void RequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MailboxViewerForm parent = (MailboxViewerForm)this.Owner;
            parent.requestFormOpened = false;
        }

        private async void button_Run_Click(object sender, EventArgs e)
        {
            Uri requestUri;

            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            string originalRequestBody = "";

            try
            {
                requestUri = new Uri(textBox_Request.Text);
            }
            catch
            {
                requestUri = null;
            }
            
            if (requestUri == null)
            {
                MessageBox.Show("The supplied URI could not be correctly parsed.", "Office365APIEditor");
                return;
            }

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(textBox_Request.Text);
            request.AllowAutoRedirect = Properties.Settings.Default.AllowAutoRedirect;
            request.ContentType = "application/json";
            // ((HttpWebRequest)request).Accept = "application/json;odata.metadata=full;odata.streaming=true";
            // TODO: implement "Accept Header Editor"

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
                    originalRequestBody = textBox_RequestBody.Text;

                    streamWriter.Write(originalRequestBody);
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
                    originalRequestBody = textBox_RequestBody.Text;

                    streamWriter.Write(originalRequestBody);
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

            string originalRequestHeaders = "";

            foreach (DataGridViewRow item in dataGridView_RequestHeader.Rows)
            {
                string headerName = "";
                string headerValue = "";

                if (item.Cells[0].Value != null)
                {
                    headerName = item.Cells[0].Value.ToString();
                }

                if (item.Cells[1].Value != null)
                {
                    headerValue = item.Cells[1].Value.ToString();
                }

                if (headerName == "")
                {
                    if (headerValue == "")
                    {
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("Invalid header name.", "Office365APIEditor");
                        return;
                    }
                }

                string header = headerName + ": " + headerValue;
                originalRequestHeaders += header + Environment.NewLine;

                try
                {
                    request.Headers.Add(header);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor");
                    return;
                }
            }

            try
            {
                // Change cursor.
                Application.UseWaitCursor = true;

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteRequestLog(request, originalRequestBody);
                }

                // Get a response and response stream.
                var response = (HttpWebResponse)await request.GetResponseAsync();

                string jsonResponse = "";
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    jsonResponse = reader.ReadToEnd();
                }

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteResponseLog(response, jsonResponse);
                }

                // Display the results.
                DisplayResponse(CreateStatusCodeString(response), response.Headers, jsonResponse);

                // Add Run History
                AddRunHistory(request, originalRequestHeaders, originalRequestBody, response, jsonResponse);

                // Save application setting.
                Properties.Settings.Default.Save();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteCustomLog("Response", ex.Message);
                    }

                    DisplayResponse("Error", null, ex.Message);

                    // Add Run History
                    AddRunHistory(request, originalRequestHeaders, originalRequestBody, null, ex.Message);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteResponseLog(response, jsonResponse);
                    }

                    DisplayResponse(CreateStatusCodeString(response), ex.Response.Headers, jsonResponse);

                    // Add Run History
                    AddRunHistory(request, originalRequestHeaders, originalRequestBody, (HttpWebResponse)ex.Response, jsonResponse);
                }
            }
            catch (Exception ex)
            {
                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteCustomLog("Response", ex.Message);
                }

                DisplayResponse("Error", null, ex.Message);

                // Add Run History
                AddRunHistory(request, originalRequestHeaders, originalRequestBody, null, ex.Message);
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
                //string resourceURL = StartForm.GetResourceURL(clientInfo.ResourceUri);
                tempTable["resource"] = System.Web.HttpUtility.UrlEncode(clientInfo.ResourceUri);

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
                tempTable["redirect_uri"] = System.Web.HttpUtility.UrlEncode(clientInfo.RedirectUri);

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
                DisplayResponse(CreateStatusCodeString(response), response.Headers, jsonResponse);

                // Deserialize and get Access Token.
                clientInfo.ReplaceToken(AccessTokenWizard.Deserialize<TokenResponse>(jsonResponse));
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteCustomLog("Response", ex.Message);
                    }

                    DisplayResponse("Error", null, ex.Message);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteResponseLog((HttpWebResponse)ex.Response, jsonResponse);
                    }

                    DisplayResponse(CreateStatusCodeString(response), ex.Response.Headers, jsonResponse);
                }
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
                textBox_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
            }
        }

        private void checkBox_Decode_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                textBox_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
            }
        }

        private string CreateStatusCodeString(HttpWebResponse response)
        {
            return (int)response.StatusCode + " - " + response.StatusDescription;
        }

        public void DisplayResponse(string StatusCode, WebHeaderCollection Headers, string JsonResponse)
        {
            // Status code
            label_StatusCode.Text = StatusCode;

            // Header
            if (Headers == null)
            {
                textBox_ResponseHeaders.Text = "";
            }
            else
            {
                textBox_ResponseHeaders.Text = Headers.ToString();
            }

            // Body
            originalJsonResponse = JsonResponse;
            textBox_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);

            // Show Body tab
            tabControl_Response.SelectTab(1);
            textBox_ResponseBody.Select(0, 0);
        }

        public string ShapeJsonResponseIfNeeded(string Data)
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
            sb.AppendLine("StatusCode : " + CreateStatusCodeString(ResponseToLog));
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

            string settingLofFilePath = Properties.Settings.Default.LogFolderPath;

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

        private void AddRunHistory(WebRequest Request, string RequestHeader, string RequestBody, HttpWebResponse Response, string JsonResponse)
        {
            int maxHistoryCount = 30;

            // Keep only 30 history.
            while (runHistory.RunInfo.Count >= maxHistoryCount)
            {
                runHistory.RunInfo.RemoveAt(0);
            }

            string statusCode;
            string headers;

            if (Response == null)
            {
                statusCode = "Error";
                headers = "";
            }
            else
            {
                statusCode = Response.StatusCode.ToString();
                headers = Response.Headers.ToString();
            }

            RunInformation newRunInfo = new RunInformation
            {
                ExecutionID = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                RequestUrl = Request.RequestUri.ToString(),
                RequestMethod = Request.Method,
                RequestHeader = RequestHeader,
                RequestCompleteHeader = Request.Headers.ToString(),
                RequestBody = RequestBody,
                ResponseStatusCode = statusCode,
                ResponseHeader = headers,
                ResponseBody = JsonResponse
            };

            // Add new history.
            runHistory.RunInfo.Add(newRunInfo);

            // Update the listbox.

            listBox_RunHistory.Items.Clear();

            for (int i = runHistory.RunInfo.Count - 1; i >= 0; i--)
            {
                listBox_RunHistory.Items.Add(runHistory.RunInfo[i]);
            }

            // Save the file.

            try
            {
                FileStream stream = new FileStream(Path.Combine(Application.StartupPath, "RunHistory.xml"), FileMode.Create);

                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    // Serialize
                    System.Xml.Serialization.XmlSerializerNamespaces nameSpace = new System.Xml.Serialization.XmlSerializerNamespaces();
                    nameSpace.Add(string.Empty, string.Empty);
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(RunHistory));
                    serializer.Serialize(writer, runHistory, nameSpace);

                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The run history file could not be saved." + Environment.NewLine + Environment.NewLine + ex.Message, "Office365APIEditor");
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

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the Option window
            RequestFormOptionForm optionForm = new RequestFormOptionForm();
            optionForm.ShowDialog();
        }

        private void listBox_RunHistory_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the listbox manually.

            if (e.Index < 0)
            {
                return;
            }

            Brush brush;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // Drawing a selected item.
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected, e.ForeColor, SystemColors.Control);
                brush = Brushes.Black;
            }
            else
            {
                brush = Brushes.Gray;
            }

            e.DrawBackground();

            // Item to be drawed.
            var item = listBox_RunHistory.Items[e.Index] as RunInformation;

            var regularFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular);
            var boldFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);

            // Recrangle for current item.
            RectangleF rectItemBorder = new RectangleF(e.Bounds.Left + 3, e.Bounds.Top + 5, e.Bounds.Width - 6, e.Bounds.Height -10);
            
            // Height for 3 lines.
            float heightforThreeLine = e.Graphics.MeasureString("1" + Environment.NewLine + "2" + Environment.NewLine + "3", regularFont, e.Bounds.Width - 6).Height;

            // Recrangle for request URL of current item.
            RectangleF rectRequestUrlBorder = new RectangleF(e.Bounds.Left + 3, e.Bounds.Top + 5, e.Bounds.Width - 6, heightforThreeLine);

            // Draw
            e.Graphics.DrawString(item.RequestUrl, regularFont, Brushes.Black, rectRequestUrlBorder);
            e.Graphics.DrawString("Result : " + item.ResponseStatusCode, boldFont, brush, e.Bounds.Left + 3, e.Bounds.Top + 5 + heightforThreeLine);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectItemBorder));
        }

        private void listBox_RunHistory_Resize(object sender, EventArgs e)
        {
            listBox_RunHistory.Refresh();
        }

        private void listBox_RunHistory_MouseMove(object sender, MouseEventArgs e)
        {
            // Show tooltip on the ListBox.

            int newHoveredIndex = listBox_RunHistory.IndexFromPoint(e.Location);

            if (hoveredIndex != newHoveredIndex)
            {
                hoveredIndex = newHoveredIndex;

                if (hoveredIndex > -1)
                {
                    var selectedRunInfo = listBox_RunHistory.Items[hoveredIndex] as RunInformation;

                    toolTip1.Active = false;
                    toolTip1.SetToolTip(listBox_RunHistory, selectedRunInfo.RequestUrl);
                    toolTip1.Active = true;
                }
            }
        }

        private void listBox_RunHistory_MouseDown(object sender, MouseEventArgs e)
        {
            // Show the context menu if selected item is clicked by the right button.

            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            int clickedIndex = listBox_RunHistory.IndexFromPoint(e.Location);
            if (clickedIndex != ListBox.NoMatches && listBox_RunHistory.SelectedIndex == clickedIndex)
            {
                contextMenuStrip_RunHistory.Show(Cursor.Position);
                contextMenuStrip_RunHistory.Visible = true;
            }
            else
            {
                contextMenuStrip_RunHistory.Visible = false;
            }
        }

        private void listBox_RunHistory_DoubleClick(object sender, EventArgs e)
        {
            var selectedRunInfo = listBox_RunHistory.SelectedItem as RunInformation;

            ShowRunHistoryInMainPanel(selectedRunInfo);
        }

        private void showDetailsInMainPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRunInfo = listBox_RunHistory.SelectedItem as RunInformation;

            ShowRunHistoryInMainPanel(selectedRunInfo);
        }

        private void ShowRunHistoryInMainPanel(RunInformation runInfo)
        {
            // Show the details of selected run history.

            textBox_Request.Text = runInfo.RequestUrl;

            dataGridView_RequestHeader.Rows.Clear();

            string[] headers = runInfo.RequestHeader.Replace("\r", "").Split('\n');

            foreach (string header in headers)
            {
                if (header == "")
                {
                    continue;
                }

                string[] delimiter = { ": " };
                string[] headerParts = header.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                string headerName = "";
                string headerValue = "";

                if (headerParts.Length == 0)
                {
                    // invalid format
                    continue;
                }
                else if (headerParts.Length == 1)
                {
                    // header value is missing.
                    headerName = headerParts[0];
                }
                else if (headerParts.Length == 2)
                {
                    headerName = headerParts[0];
                    headerValue = headerParts[1];
                }
                else
                {
                    // Header value contains ": " .
                    headerName = headerParts[0];
                    headerValue = header.Remove(0, headerName.Length + 2);
                }

                dataGridView_RequestHeader.Rows.Add(headerName, headerValue);
            }

            textBox_RequestBody.Text = runInfo.RequestBody;

            switch (runInfo.RequestMethod.ToUpper())
            {
                case "GET":
                    radioButton_GET.Select();
                    break;
                case "POST":
                    radioButton_POST.Select();
                    break;
                case "PATCH":
                    radioButton_PATCH.Select();
                    break;
                case "DELETE":
                    radioButton_DELETE.Select();
                    break;
                default:
                    break;
            }

            label_StatusCode.Text = runInfo.ResponseStatusCode;
            textBox_ResponseHeaders.Text = runInfo.ResponseHeader.Replace("\n", Environment.NewLine);

            originalJsonResponse = runInfo.ResponseBody;
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";
            textBox_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
        }
    }
}
