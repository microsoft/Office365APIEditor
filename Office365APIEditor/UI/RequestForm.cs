// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Office365APIEditor.SampleRequestDefinition;
using ScintillaNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET_FindReplaceDialog;

namespace Office365APIEditor
{
    public partial class RequestForm : Form
    {
        // Declare variable for FindReplace dialog
        FindReplace findReplaceDialog;

        ClientInformation clientInfo;

        string originalJsonResponse = "";
        string indentedJsonResponse = "";
        string decodedJsonResponse = "";
        string indentedAndDecodedJsonResponse = "";

        RunHistory runHistory = new RunHistory();

        int hoveredIndex = -1;

        // JSON style text editor
        Scintilla scintilla_RequestBody;
        Scintilla scintilla_ResponseBody;

        // Sample request
        List<SampleRequest> sampleRequests;

        public RequestForm()
        {
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            button_RefreshToken.Enabled = false;
            button_Run.Enabled = false;
            button_ViewTokenInfo.Enabled = false;

            // Change window title
            Text = Util.GenerateWindowTitle("Editor");

            // Restore window location, size and state.

            var savedBounds = Properties.Settings.Default.RequestFormBounds;
            var savedWindowState = Properties.Settings.Default.RequestFormWindowState;

            if (savedWindowState == FormWindowState.Minimized)
            {
                // We should not restore window location, size and state.
                savedBounds = new Rectangle(0, 0, 0, 0);
                savedWindowState = FormWindowState.Normal;
            }
            else if (savedWindowState == FormWindowState.Maximized)
            {
                // We should not restore window size.
                savedBounds = new Rectangle(new Point(savedBounds.Location.X + 8, savedBounds.Y + 8), new Size(Width, Height));
            }

            if (!savedBounds.IsEmpty)
            {
                // We should not restore window size and location if it is out of screens.

                foreach (Screen screen in Screen.AllScreens)
                {
                    if (screen.WorkingArea.Contains(savedBounds))
                    {
                        Bounds = savedBounds;
                        break;
                    }
                }
            }

            WindowState = savedWindowState;

            // Enable Ctrl+A short cut key for all textbox.
            AddKeyDownEvent(this);
            
            // Load Run History
            try
            {
                string runHistoryFilePath = Util.RunHistoryPath;
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

            // JSON style editor setting.

            // Request Body
            scintilla_RequestBody = new Scintilla();
            tabPage_Body.Controls.Add(scintilla_RequestBody);

            scintilla_RequestBody.Dock = DockStyle.Fill;
            InitSyntaxColoring(scintilla_RequestBody);
            scintilla_RequestBody.ReadOnly = false;

            // Response Body
            scintilla_ResponseBody = new Scintilla();
            tabPage2.Controls.Add(scintilla_ResponseBody);

            scintilla_ResponseBody.Dock = DockStyle.Fill;
            InitSyntaxColoring(scintilla_ResponseBody);
            scintilla_ResponseBody.ReadOnly = true;

            // Folding
            scintilla_ResponseBody.Lexer = Lexer.Json;

            // Instruct the lexer to calculate folding
            scintilla_ResponseBody.SetProperty("fold", "1");
            scintilla_ResponseBody.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            scintilla_ResponseBody.Margins[2].Type = MarginType.Symbol;
            scintilla_ResponseBody.Margins[2].Mask = Marker.MaskFolders;
            scintilla_ResponseBody.Margins[2].Sensitive = true;
            scintilla_ResponseBody.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                scintilla_ResponseBody.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                scintilla_ResponseBody.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            scintilla_ResponseBody.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla_ResponseBody.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla_ResponseBody.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla_ResponseBody.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla_ResponseBody.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla_ResponseBody.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla_ResponseBody.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            scintilla_ResponseBody.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            // Enable URL Interaction
            scintilla_ResponseBody.Styles[Style.Json.Uri].Hotspot = true;
            scintilla_ResponseBody.HotspotClick += new EventHandler<HotspotClickEventArgs>(this.scintilla_HotspotClick);

            // Create instance of FindReplace with reference to a ScintillaNET control.
            findReplaceDialog = new FindReplace(scintilla_ResponseBody); // For WinForms

            // Tie in FindReplace event
            findReplaceDialog.KeyPressed += findReplaceDialog_KeyPressed;

            // Tie in Scintilla event
            scintilla_ResponseBody.KeyDown += scintilla_KeyDown;

            // Load sample request

            List<SampleRequestDefinitionRoot> sampleRequestDefinitionLists = new List<SampleRequestDefinitionRoot>();
            string sampleRequestDirectory = Path.Combine(Application.StartupPath, "SampleRequest");

            if (Directory.Exists(sampleRequestDirectory))
            {
                string[] definitionFiles = Directory.GetFiles(sampleRequestDirectory, "*.json", SearchOption.TopDirectoryOnly);

                foreach (var definitionFilePath in definitionFiles)
                {
                    try
                    {
                        string rawJsonSampleRequest = "";

                        using (StreamReader reader = new StreamReader(definitionFilePath))
                        {
                            rawJsonSampleRequest = reader.ReadToEnd();
                        }

                        var sampleRequest = JsonConvert.DeserializeObject<SampleRequestDefinitionRoot>(rawJsonSampleRequest);
                        sampleRequestDefinitionLists.Add(sampleRequest);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            sampleRequests = new List<SampleRequest>();

            foreach (var sampleRequest in sampleRequestDefinitionLists)
            {
                var rootNode = new TreeNode(sampleRequest.DisplayName);

                foreach (var firstLevelCategory in sampleRequest.FirstLevelCategory)
                {
                    var firstLevelNode = new TreeNode(firstLevelCategory.DisplayName);

                    foreach (var secondLevelCategory in firstLevelCategory.SecondLevelCategory)
                    {
                        var secondLevelNode = new TreeNode(secondLevelCategory.DisplayName);

                        foreach (var thirdLevelCategory in secondLevelCategory.ThirdLevelCategory)
                        {
                            var thirdLevelNode = new TreeNode(thirdLevelCategory.DisplayName);

                            if (thirdLevelCategory.SampleRequest != null)
                            {
                                foreach (var sample in thirdLevelCategory.SampleRequest)
                                {
                                    // Check whether ID is duplicated or not.

                                    if (sampleRequests.Where(s => s.Id == sample.Id).Count() != 0)
                                    {
                                        MessageBox.Show("A Duplicated sample request ID was found in the definition file." + Environment.NewLine + sample.Id, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        var sampleNode = new TreeNode(sample.DisplayName) { Tag = sample.Id };
                                        thirdLevelNode.Nodes.Add(sampleNode);

                                        sampleRequests.Add(sample);
                                    }
                                }
                            }

                            if (thirdLevelCategory.FourthLevelCategory != null)
                            {
                                foreach (var fourthLevelCategory in thirdLevelCategory.FourthLevelCategory)
                                {
                                    var fourthLevelNode = new TreeNode(fourthLevelCategory.DisplayName);

                                    foreach (var sample in fourthLevelCategory.SampleRequest)
                                    {
                                        // Check whether ID is duplicated or not.

                                        if (sampleRequests.Where(s => s.Id == sample.Id).Count() != 0)
                                        {
                                            MessageBox.Show("A Duplicated sample request ID was found in the definition file." + Environment.NewLine + sample.Id, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            var sampleNode = new TreeNode(sample.DisplayName) { Tag = sample.Id };
                                            fourthLevelNode.Nodes.Add(sampleNode);

                                            sampleRequests.Add(sample);
                                        }
                                    }

                                    thirdLevelNode.Nodes.Add(fourthLevelNode);
                                }
                            }

                            secondLevelNode.Nodes.Add(thirdLevelNode);
                        }

                        firstLevelNode.Nodes.Add(secondLevelNode);
                    }

                    rootNode.Nodes.Add(firstLevelNode);
                }

                treeView_Example.Nodes.Add(rootNode);
            }
        }

        private void RequestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save window location and size.
            Properties.Settings.Default.RequestFormBounds = Bounds;
            Properties.Settings.Default.RequestFormWindowState = WindowState;
            Properties.Settings.Default.Save();
        }

        private void InitSyntaxColoring(Scintilla scintilla)
        {
            scintilla.Styles[Style.Default].Size = 12;
            scintilla.Styles[Style.Json.Default].ForeColor = Color.Silver;
            scintilla.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            scintilla.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            scintilla.Lexer = Lexer.Json;

        }

        private void AddKeyDownEvent(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textBoxControl = item as TextBox;

                    if (textBoxControl == textBox_Request)
                    {
                        textBoxControl.KeyDown += textBox_Request_KeyDown;
                    }
                    else
                    {
                        textBoxControl.KeyDown += TextBoxControl_KeyDown;
                    }
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
            await RunRequestAsync();
        }

        private async System.Threading.Tasks.Task RunRequestAsync()
        {
            Uri requestUri;

            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            string originalRequestBody = "";

            try
            {
                string replacedUri = textBox_Request.Text;

                if (Properties.Settings.Default.ReplacePlusSignInTheRequestURL)
                {
                    replacedUri = replacedUri.Replace("+", "%2B");
                }

                if (Properties.Settings.Default.ReplaceSharpSignInTheRequestURL)
                {
                    replacedUri = replacedUri.Replace("#", "%23");
                }

                requestUri = new Uri(replacedUri);
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

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.AllowAutoRedirect = Properties.Settings.Default.AllowAutoRedirect;
            request.UserAgent = Util.CustomUserAgent;
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
                if (checkBox_SendAsBinary.Checked)
                {
                    using (var streamWriter = request.GetRequestStream())
                    {
                        var binaryData = Convert.FromBase64String(scintilla_RequestBody.Text);
                        streamWriter.Write(binaryData, 0, binaryData.Length);
                    }
                }
                else
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        originalRequestBody = scintilla_RequestBody.Text;

                        streamWriter.Write(originalRequestBody);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }
            else if (radioButton_PATCH.Checked)
            {
                // Request if PATCH
                request.Method = "PATCH";

                // Build a body.
                if (checkBox_SendAsBinary.Checked)
                {
                    using (var streamWriter = request.GetRequestStream())
                    {
                        var binaryData = Convert.FromBase64String(scintilla_RequestBody.Text);
                        streamWriter.Write(binaryData, 0, binaryData.Length);
                    }
                }
                else
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        originalRequestBody = scintilla_RequestBody.Text;

                        streamWriter.Write(originalRequestBody);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }
            else if (radioButton_DELETE.Checked)
            {
                // Request is DELETE.
                request.Method = "DELETE";
            }
            else
            {
                // Request is PUT
                request.Method = "PUT";

                // Build a body.
                if (checkBox_SendAsBinary.Checked)
                {
                    using (var streamWriter = request.GetRequestStream())
                    {
                        var binaryData = Convert.FromBase64String(scintilla_RequestBody.Text);
                        streamWriter.Write(binaryData, 0, binaryData.Length);
                    }
                }
                else
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        originalRequestBody = scintilla_RequestBody.Text;

                        streamWriter.Write(originalRequestBody);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
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

                if (headerName.ToLower() == "content-type")
                {
                    // Content-Type header should be set as property.
                    request.ContentType = headerValue;
                    continue;
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
                // Change cursor and UI.
                Application.UseWaitCursor = true;
                scintilla_RequestBody.UseWaitCursor = true;
                scintilla_ResponseBody.UseWaitCursor = true;
                button_Run.Text = "Running...";
                button_Run.Enabled = false;

                // Logging
                if (checkBox_Logging.Checked)
                {
                    WriteRequestLog(request, originalRequestBody);
                }

                // Get a response and response stream.
                var response = (HttpWebResponse)await request.GetResponseAsync();

                bool isImageResponse = response.ContentType.Contains("image/jpeg") || response.ContentType.Contains("image/png");
                string contentDisposition = response.Headers.Get("content-disposition");
                bool isCsvResponse = (contentDisposition != null && Regex.IsMatch(contentDisposition, "^attachment; filename=\".*\\.csv\"$"));

                if (isImageResponse)
                {
                    // Response data is photo.

                    List<byte> byteList = new List<byte>();

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        for (; ; )
                        {
                            // read 1 byte from the stream
                            int data = responseStream.ReadByte();

                            if (data == -1)
                            {
                                // there is no data to read
                                break;
                            }

                            byteList.Add((byte)data);
                        }
                    }

                    byte[] binaryResponse = byteList.ToArray();
                    string base64Response = Convert.ToBase64String(binaryResponse);

                    // Create new bitmap object to display the response
                    MemoryStream memoryStream = new MemoryStream();
                    byte[] pictureData = binaryResponse;
                    memoryStream.Write(pictureData, 0, Convert.ToInt32(pictureData.Length));
                    Bitmap bitmapResponse = new Bitmap(memoryStream, false);
                    memoryStream.Dispose();

                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteResponseLog(response, base64Response);
                    }

                    // Display the results.
                    DisplayResponse(CreateStatusCodeString(response), response.Headers, base64Response);
                    pictureBox_Photo.Image = bitmapResponse;

                    // Add Run History
                    AddRunHistory(request, originalRequestHeaders, originalRequestBody, response, base64Response);
                }
                else if (isCsvResponse)
                {
                    // Response data is CSV.

                    string csvResponse = "";
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        csvResponse = reader.ReadToEnd();
                    }

                    // Create DataTable

                    StringReader stringReader = new StringReader(csvResponse);
                    string line = stringReader.ReadLine();

                    DataTable dataTable = new DataTable();
                    DataRow dataRow;

                    if (line != "")
                    {
                        // Create headers.
                        foreach (var column in line.Split(','))
                        {
                            dataTable.Columns.Add(column);
                        }

                        // Add rows.
                        while ((line = stringReader.ReadLine()) != null)
                        {
                            dataRow = dataTable.NewRow();
                            dataRow.ItemArray = line.Split(',');
                            dataTable.Rows.Add(dataRow);
                        }
                    }

                    // Logging
                    if (checkBox_Logging.Checked)
                    {
                        WriteResponseLog(response, csvResponse);
                    }

                    // Display the results.
                    DisplayResponse(CreateStatusCodeString(response), response.Headers, csvResponse);
                    dataGridView_CSV.DataSource = dataTable;

                    // Add Run History
                    AddRunHistory(request, originalRequestHeaders, originalRequestBody, response, csvResponse);
                }
                else
                {
                    // Response data is json.

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
                }

                // Save application setting.
                Properties.Settings.Default.Save();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    // Logging
                    WriteResponseErrorLog(ex.Message);

                    DisplayResponse("Error", null, ex.Message);

                    // Add Run History
                    AddRunHistory(request, originalRequestHeaders, originalRequestBody, null, ex.Message);
                }
                else
                {
                    string jsonResponse = "";
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
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
                WriteResponseErrorLog(ex.Message);

                DisplayResponse("Error", null, ex.Message);

                // Add Run History
                AddRunHistory(request, originalRequestHeaders, originalRequestBody, null, ex.Message);
            }
            finally
            {
                // Change cursor and UI.
                Application.UseWaitCursor = false;
                scintilla_RequestBody.UseWaitCursor = false;
                scintilla_ResponseBody.UseWaitCursor = false;
                button_Run.Text = "Run";
                button_Run.Enabled = true;
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

        private async void textBox_Request_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as TextBox).SelectAll();
            }

            // Enter
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                // Run request if it is possible.
                if (button_Run.Enabled)
                {
                    await RunRequestAsync();
                }
            }
        }

        private void button_ViewTokenInfo_Click(object sender, EventArgs e)
        {
            TokenViewer tokenViewer = new TokenViewer(clientInfo.Token)
            {
                Owner = this
            };
            tokenViewer.ShowDialog();
        }

        private async void button_RefreshToken_Click(object sender, EventArgs e)
        {
            // Request another access token with refresh token.

            originalJsonResponse = "";
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";

            string endPoint = $"https://login.microsoftonline.com/{clientInfo.TenantName.Replace("@", ".")}/oauth2/";

            // Build a POST body.
            string postBody = "";
            Hashtable tempTable = new Hashtable
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = clientInfo.Token.refresh_token
            };

            if (clientInfo.AuthType == AuthEndpoints.OAuthV1)
            {
                //string resourceURL = StartForm.GetResourceURL(clientInfo.ResourceUri);
                tempTable["resource"] = System.Web.HttpUtility.UrlEncode(clientInfo.ResourceUri);

                if (clientInfo.ClientID != "")
                {
                    // If _clientID has value, we're working with web app.
                    // So we have to add Client ID and Client Secret.
                    tempTable["client_id"] = clientInfo.ClientID;
                    tempTable["client_secret"] = System.Web.HttpUtility.UrlEncode(clientInfo.ClientSecret);
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
                    tempTable["client_secret"] = System.Web.HttpUtility.UrlEncode(clientInfo.ClientSecret);
                }
            }

            foreach (string key in tempTable.Keys)
            {
                postBody += String.Format("{0}={1}&", key, tempTable[key]);
            }
            byte[] postDataBytes = Encoding.ASCII.GetBytes(postBody);

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(endPoint + "token/");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataBytes.Length;
            request.UserAgent = Util.CustomUserAgent;

            try
            {
                // Change a cursor.
                Application.UseWaitCursor = true;
                scintilla_RequestBody.UseWaitCursor = true;
                scintilla_ResponseBody.UseWaitCursor = true;

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
                    WriteResponseErrorLog(ex.Message);

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
                WriteResponseErrorLog(ex.Message);

                DisplayResponse("Error", null, ex.Message);
            }
            finally
            {
                // Change cursor.
                Application.UseWaitCursor = false;
                scintilla_RequestBody.UseWaitCursor = false;
                scintilla_ResponseBody.UseWaitCursor = false;
            }
        }

        private void checkBox_Indent_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                scintilla_ResponseBody.ReadOnly = false;
                scintilla_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
                scintilla_ResponseBody.ReadOnly = true;
            }
        }

        private void checkBox_Decode_CheckedChanged(object sender, EventArgs e)
        {
            if (originalJsonResponse != "")
            {
                scintilla_ResponseBody.ReadOnly = false;
                scintilla_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
                scintilla_ResponseBody.ReadOnly = true;
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

            bool isImage = false;
            bool isCSV = false;

            // Header
            if (Headers == null)
            {
                textBox_ResponseHeaders.Text = "";
            }
            else
            {
                textBox_ResponseHeaders.Text = Headers.ToString();

                try
                {
                    string contentType = Headers.GetValues("Content-Type")[0];
                    if (contentType == "image/jpeg" || contentType == "image/png")
                    {
                        isImage = true;
                    }

                    string contentDisposition = Headers.GetValues("content-disposition")[0];
                    if (Regex.IsMatch(contentDisposition, "^attachment; filename=\".*\\.csv\"$"))
                    {
                        isCSV = true;
                    }
                }
                catch { }
            }

            // Body
            originalJsonResponse = JsonResponse;
            scintilla_ResponseBody.ReadOnly = false;
            scintilla_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
            scintilla_ResponseBody.ReadOnly = true;

            // Show the picturebox in Preview tab if response is image.
            pictureBox_Photo.Visible = isImage;

            // Show the CSV DataGridView and Link if response is CSV.
            dataGridView_CSV.Visible = isCSV;
            linkLabel_SaveCsvResponse.Visible = isCSV;

            // Show appropriate tab
            if (isImage)
            {
                tabControl_Response.SelectTab(2);
            }
            else if (isCSV)
            {
                tabControl_Response.SelectTab(3);
            }
            else
            {
                tabControl_Response.SelectTab(1);
                scintilla_ResponseBody.SetEmptySelection(0);
            }
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
            if (Data == "")
            {
                return "";
            }
            else
            {
                try
                {
                    dynamic parsedJson = JsonConvert.DeserializeObject(Data);
                    return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                }
                catch
                {
                    return Data;
                }
            }
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

            try
            {
                result = Regex.Unescape(result);
            }
            catch
            {
                // If jsonResponse is not a JSON format text, Regex.Unescape will fail.
                // Ignore it and return.
            }

            return result;
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

            if (RequestToLog.Method == "POST" || RequestToLog.Method == "PATCH" || RequestToLog.Method == "PUT")
            {
                sb.AppendLine("Body : ");
                sb.AppendLine("  " + Body.Replace(Environment.NewLine, Environment.NewLine + "  "));
            }

            Util.WriteLog(sb);
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

            Util.WriteLog(sb);
        }

        private void WriteResponseErrorLog(string ErrorMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Response");
            sb.AppendLine(ErrorMessage);

            if (checkBox_Logging.Checked)
            {
                Util.WriteLog(sb);
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
            if (Convert.ToBoolean(runHistory.RunInfo.Count) &&
                    runHistory.RunInfo.Last().RequestUrl == newRunInfo.RequestUrl &&
                    runHistory.RunInfo.Last().ResponseBody == newRunInfo.ResponseBody)
                runHistory.RunInfo.RemoveAt(runHistory.RunInfo.Count - 1);
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
                FileStream stream = new FileStream(Util.RunHistoryPath, FileMode.Create);

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
            AccessTokenWizard accessTokenWizard = new AccessTokenWizard();
            if (accessTokenWizard.ShowDialog(out ClientInformation newClientInfo) == DialogResult.OK)
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

            scintilla_RequestBody.Text = runInfo.RequestBody;

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
                case "PUT":
                    radioButton_PUT.Select();
                    break;
                default:
                    break;
            }

            label_StatusCode.Text = runInfo.ResponseStatusCode;
            textBox_ResponseHeaders.Text = runInfo.ResponseHeader.Replace("\n", Environment.NewLine);

            bool isImage = false;
            bool isCsv = false;

            if (textBox_ResponseHeaders.Text.Contains("Content-Type: image/jpeg") || textBox_ResponseHeaders.Text.Contains("Content-Type: image/png"))
            {
                isImage = true;
            }

            isCsv = Regex.Match(textBox_ResponseHeaders.Text, "attachment; filename=\".*\\.csv\"", RegexOptions.None).Success;

            originalJsonResponse = runInfo.ResponseBody;
            indentedJsonResponse = "";
            decodedJsonResponse = "";
            indentedAndDecodedJsonResponse = "";
            scintilla_ResponseBody.ReadOnly = false;
            scintilla_ResponseBody.Text = ShapeJsonResponseIfNeeded(originalJsonResponse);
            scintilla_ResponseBody.ReadOnly = true;

            if (isImage) {
                // Create new bitmap object to display the response
                byte[] binaryResponse = Convert.FromBase64String(originalJsonResponse);                
                MemoryStream memoryStream = new MemoryStream();
                byte[] pictureData = binaryResponse;
                memoryStream.Write(pictureData, 0, Convert.ToInt32(pictureData.Length));
                Bitmap bitmapResponse = new Bitmap(memoryStream, false);
                memoryStream.Dispose();
                pictureBox_Photo.Image = bitmapResponse;

                // Show Preview tab
                tabControl_Response.SelectTab(2);
            }
            else if (isCsv)
            {
                // Create DataTable

                StringReader stringReader = new StringReader(originalJsonResponse);
                string line = stringReader.ReadLine();

                DataTable dataTable = new DataTable();
                DataRow dataRow;

                if (line != "")
                {
                    // Create headers.
                    foreach (var column in line.Split(','))
                    {
                        dataTable.Columns.Add(column);
                    }

                    // Add rows.
                    while ((line = stringReader.ReadLine()) != null)
                    {
                        dataRow = dataTable.NewRow();
                        dataRow.ItemArray = line.Split(',');
                        dataTable.Rows.Add(dataRow);
                    }
                }

                dataGridView_CSV.DataSource = dataTable;

                // Show CSV tab
                tabControl_Response.SelectTab(3);
            }
            else
            {
                // Show Body tab
                tabControl_Response.SelectTab(1);
                scintilla_ResponseBody.SetEmptySelection(0);
            }

            // Show the picturebox in preview tab if response is image.
            pictureBox_Photo.Visible = isImage;

            // Show the DataGridView and Link in CSV tab if response is CSV.
            dataGridView_CSV.Visible = isCsv;
            linkLabel_SaveCsvResponse.Visible = isCsv;
        }

        private void linkLabel_SaveCsvResponse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string fileName = "";

            var contentDispositionHeader = Regex.Match(textBox_ResponseHeaders.Text, "attachment; filename=\".*\\.csv\"", RegexOptions.None);

            if (contentDispositionHeader.Success == true)
            {
                fileName = contentDispositionHeader.Value.Substring(contentDispositionHeader.Value.IndexOf('"')).Trim('"');
            }
            else
            {
                fileName = "CsvResponse.csv";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = fileName,
                Filter = "CSV (*.csv)|*.csv|All Files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;

                using (Stream csvStream = saveFileDialog.OpenFile())
                {
                    using (StreamWriter streamWriter = new StreamWriter(csvStream))
                    {
                        streamWriter.Write(originalJsonResponse);
                    }
                }
            }
        }

        private void treeView_Example_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
        }

        private void treeView_Example_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
        }

        private void treeView_Example_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView_Example.SelectedNode = e.Node;
            }
        }

        private void treeView_Example_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void treeView_Example_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.GetNodeCount(true) == 0)
            {
                // Show the selected example
                DisplayExample(e.Node.Tag.ToString());
            }
        }

        private void treeView_Example_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (treeView_Example.SelectedNode?.GetNodeCount(true) == 0)
                {
                    // Show the selected example
                    DisplayExample(treeView_Example.SelectedNode.Tag.ToString());
                }
            }
        }

        private void DisplayExample(string exampleId)
        {
            // Get the sample and show it.

            var sample = sampleRequests.Where(s => s.Id == exampleId).First();

            if (sample != null)
            {
                textBox_Request.Text = RunBuiltInFunction(sample.URI);

                dataGridView_RequestHeader.Rows.Clear();

                foreach (Header header in sample.Header)
                {
                    if (header == null)
                    {
                        continue;
                    }

                    string headerName = header.Name;
                    string headerValue = header.Value;

                    dataGridView_RequestHeader.Rows.Add(headerName, headerValue);
                }

                scintilla_RequestBody.Text = DecodeJsonResponse(RunBuiltInFunction(sample.Body));
                scintilla_RequestBody.CurrentPosition = 0;

                switch (sample.Method.ToUpper())
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
                    case "PUT":
                        radioButton_PUT.Select();
                        break;
                    default:
                        break;
                }
            }

            treeView_Example.Focus();
        }

        private string RunBuiltInFunction(string requestBody)
        {
            var result = requestBody;
            var utcNow = DateTime.UtcNow;

            if (requestBody.Contains("${DateTime2HourBefore}"))
            {
                result = result.Replace("${DateTime2HourBefore}", utcNow.AddHours(-2).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime1HourBefore}"))
            {
                result = result.Replace("${DateTime1HourBefore}", utcNow.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime1HourLater}"))
            {
                result = result.Replace("${DateTime1HourLater}", utcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime2HourLater}"))
            {
                result = result.Replace("${DateTime2HourLater}", utcNow.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime1DayLater12}"))
            {
                result = result.Replace("${DateTime1DayLater12}", utcNow.AddDays(1).ToString("yyyy-MM-dd") + "T12:00:00");
            }

            if (requestBody.Contains("${DateTime1DayLater14}"))
            {
                result = result.Replace("${DateTime1DayLater14}", utcNow.AddDays(1).ToString("yyyy-MM-dd") + "T14:00:00");
            }

            if (requestBody.Contains("${DateTime1DayAnd1HourLater}"))
            {
                result = result.Replace("${DateTime1DayAnd1HourLater}", utcNow.AddDays(1).AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime1DayAnd2HourLater}"))
            {
                result = result.Replace("${DateTime1DayAnd2HourLater}", utcNow.AddDays(1).AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            if (requestBody.Contains("${DateTime1MonthLater12}"))
            {
                result = result.Replace("${DateTime1MonthLater12}", utcNow.AddMonths(1).ToString("yyyy-MM-dd") + "T12:00:00");
            }

            if (requestBody.Contains("${DateToday}"))
            {
                result = result.Replace("${DateToday}", utcNow.ToString("yyyy-MM-dd"));
            }

            if (requestBody.Contains("${Date3MonthLater}"))
            {
                result = result.Replace("${Date3MonthLater}", utcNow.AddMonths(3).ToString("yyyy-MM-dd"));
            }

            if (requestBody.Contains("${Guid}"))
            {
                result = result.Replace("${Guid}", Guid.NewGuid().ToString());
            }

            return result;
        }

        private void checkBox_SendAsBinary_CheckedChanged(object sender, EventArgs e)
        {
            button_CreateBinaryDataFromFile.Enabled = checkBox_SendAsBinary.Checked;
        }

        private void button_CreateBinaryDataFromFile_Click(object sender, EventArgs e)
        {
            // Create binary data from the selected file and convert to base64, then set it to the request body.

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fileStream = File.OpenRead(openFileDialog1.FileName))
                {
                    var bs = new byte[fileStream.Length];
                    fileStream.Read(bs, 0, (int)fileStream.Length);

                    scintilla_RequestBody.Text = Convert.ToBase64String(bs);
                }

                // Show Request Body tab and focus on the textbox
                tabControl_Request.SelectedIndex = 1;
                scintilla_RequestBody.Focus();
                scintilla_RequestBody.CurrentPosition = 0;
            }
        }

        private void tabControl_LeftNavi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_LeftNavi.SelectedTab.Text == "Example")
            {
                treeView_Example.Focus();
            }
        }

        private async void scintilla_HotspotClick(object sender, HotspotClickEventArgs e)
        {
            var text = scintilla_ResponseBody.Lines[scintilla_ResponseBody.LineFromPosition(e.Position)].Text;
            Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\.\@#_~%$]*)*", RegexOptions.IgnoreCase);
            MatchCollection matches = urlRx.Matches(text);

            if (Convert.ToBoolean(matches.Count))
            {
                if (button_Run.Enabled)
                {
                    if (ResourceOrScopeMatch(matches[0].Value))
                    {
                        textBox_Request.Text = matches[0].Value;
                        await RunRequestAsync();
                    }
                    else
                    {
                        var result = MessageBox.Show(
                            "You may not have an access token for the following requests:" + Environment.NewLine + Environment.NewLine +
                            matches[0].Value + Environment.NewLine + Environment.NewLine +
                            "Do you want to open it in your default browser?" + Environment.NewLine +
                            "If you click [No], office365APIEditor will run the request.",
                            "Office365APIEditor",
                            MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(matches[0].Value);
                        }
                        else
                        {
                            textBox_Request.Text = matches[0].Value;
                            await RunRequestAsync();
                        }
                    }
                }
                else
                {
                    var result = MessageBox.Show(
                        "You can't run the following request because you don't have an access token." + Environment.NewLine + Environment.NewLine +
                        matches[0].Value + Environment.NewLine + Environment.NewLine +
                        "Do you want to open it in your default browser?",
                        "Office365APIEditor",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(matches[0].Value);
                    }
                }
            }
        }

        private bool ResourceOrScopeMatch(string URL)
        {
            if (clientInfo != null)
            {
                switch (clientInfo.Resource)
                {
                    case Resources.Outlook:
                        return URL.StartsWith("https://outlook.office.com/api", StringComparison.InvariantCultureIgnoreCase);
                    case Resources.Graph:
                        return URL.StartsWith("https://graph.microsoft.com/", StringComparison.InvariantCultureIgnoreCase);
                    case Resources.Management:
                        return URL.StartsWith("https://manage.office.com/api/", StringComparison.InvariantCultureIgnoreCase);
                    case Resources.AzureRM:
                        return URL.StartsWith("https://management.azure.com/", StringComparison.InvariantCultureIgnoreCase);
                    case Resources.None:
                        if ((URL.StartsWith("https://outlook.office.com/api", StringComparison.InvariantCultureIgnoreCase) && clientInfo.Scopes.Contains("https://outlook.office.com/")) ||
                            (URL.StartsWith("https://graph.microsoft.com/", StringComparison.InvariantCultureIgnoreCase) && clientInfo.Scopes.Contains("https://graph.microsoft.com/")) ||
                            (URL.StartsWith("https://manage.office.com/api/", StringComparison.InvariantCultureIgnoreCase) && clientInfo.Scopes.Contains("https://manage.office.com/")) ||
                            (URL.StartsWith("https://management.azure.com/", StringComparison.InvariantCultureIgnoreCase) && clientInfo.Scopes.Contains("https://management.azure.com/")))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void scintilla_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                findReplaceDialog.ShowFind();
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.F3)
            {
                findReplaceDialog.Window.FindPrevious();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                findReplaceDialog.Window.FindNext();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                findReplaceDialog.ShowReplace();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                findReplaceDialog.ShowIncrementalSearch();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                GoTo MyGoTo = new GoTo((ScintillaNET.Scintilla)sender);
                MyGoTo.ShowGoToDialog();
                e.SuppressKeyPress = true;
            }
        }

        private void findReplaceDialog_KeyPressed(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            scintilla_KeyDown(sender, e);
        }
    }
}