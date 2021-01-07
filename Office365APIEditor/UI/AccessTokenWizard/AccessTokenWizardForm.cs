// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class AccessTokenWizardForm : Form
    {
        List<IAccessTokenWizardPage> pages;
        PageIndex currentPageIndex;
        List<PageIndex> previousPages;

        ClientInformation clientInfo;

        internal enum PageIndex
        {
            None = -1,
            WelcomePage = 0,
            V1EndpointAppSelectionPage = 1,
            V2EndpointAppSelectionPage = 2,
            BuiltInAppOrBasicAuthSelectionPage = 3,
            SharePointOnlineAppOnlySettingPage = 4,
            V1EndpointWebAppSettingPage = 5,
            V1EndpointNativeAppSettingPage = 6,
            V1EndpointAppOnlyByCertSettingPage = 7,
            V1EndpointAppOnlyByKeySettingPage = 8,
            V1EndpointAdminConsentSettingPage = 9,
            V2EndpointWebAppSettingPage = 10,
            V2EndpointNativeAppSettingPage = 11,
            V2EndpointAppOnlyByCertSettingPage = 12,
            V2EndpointAppOnlyByKeySettingPage = 13,
            V2EndpointAdminConsentSettingPage = 14,
            BuiltInAppSettingPage = 15
        }

        public AccessTokenWizardForm()
        {
            InitializeComponent();
        }

        private void AccessTokenWizardForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            Size = new Size(433, 286);
            CenterToParent();

            // Initialize pages.

            WelcomePage welcomePage = new WelcomePage();
            V1EndpointAppSelectionPage v1EndpointAppSelectionPage = new V1EndpointAppSelectionPage();
            V2EndpointAppSelectionPage v2EndpointAppSelectionPage = new V2EndpointAppSelectionPage();
            BuiltInAppOrBasicAuthSelectionPage builtInAppOrBasicAuthSelectionPage = new BuiltInAppOrBasicAuthSelectionPage();
            SharePointOnlineAppOnlySettingPage sharePointOnlineAppOnlySettingPage = new SharePointOnlineAppOnlySettingPage();
            V1EndpointWebAppSettingPage v1EndpointWebAppSettingPage = new V1EndpointWebAppSettingPage();
            V1EndpointNativeAppSettingPage v1EndpointNativeAppSettingPage = new V1EndpointNativeAppSettingPage();
            V1EndpointAppOnlyByCertSettingPage v1EndpointAppOnlyByCertSettingPage = new V1EndpointAppOnlyByCertSettingPage();
            V1EndpointAppOnlyByKeySettingPage v1EndpointAppOnlyByKeySettingPage = new V1EndpointAppOnlyByKeySettingPage();
            V1EndpointAdminConsentSettingPage v1EndpointAdminConsentSettingPage = new V1EndpointAdminConsentSettingPage();
            V2EndpointWebAppSettingPage v2EndpointWebAppSettingPage = new V2EndpointWebAppSettingPage();
            V2EndpointNativeAppSettingPage v2EndpointNativeAppSettingPage = new V2EndpointNativeAppSettingPage();
            V2EndpointAppOnlyByCertSettingPage v2EndpointAppOnlyByCertSettingPage = new V2EndpointAppOnlyByCertSettingPage();
            V2EndpointAppOnlyByKeySettingPage v2EndpointAppOnlyByKeySettingPage = new V2EndpointAppOnlyByKeySettingPage();
            V2EndpointAdminConsentSettingPage v2EndpointAdminConsentSettingPage = new V2EndpointAdminConsentSettingPage();
            BuiltInAppSettingPage builtInAppSettingPage = new BuiltInAppSettingPage();

            pages = new List<IAccessTokenWizardPage>
            {
                welcomePage,
                v1EndpointAppSelectionPage,
                v2EndpointAppSelectionPage,
                builtInAppOrBasicAuthSelectionPage,
                sharePointOnlineAppOnlySettingPage,
                v1EndpointWebAppSettingPage,
                v1EndpointNativeAppSettingPage,
                v1EndpointAppOnlyByCertSettingPage,
                v1EndpointAppOnlyByKeySettingPage,
                v1EndpointAdminConsentSettingPage,
                v2EndpointWebAppSettingPage,
                v2EndpointNativeAppSettingPage,
                v2EndpointAppOnlyByCertSettingPage,
                v2EndpointAppOnlyByKeySettingPage,
                v2EndpointAdminConsentSettingPage,
                builtInAppSettingPage
            };

            foreach (Control page in pages)
            {
                page.Enabled = false;
                page.Location = new Point(1000, 0);
                Controls.Add(page);
            }

            previousPages = new List<PageIndex>();
            currentPageIndex = PageIndex.None;

            button_Back.BringToFront();
            button_Next.BringToFront();
            button_Cancel.BringToFront();

            // To to the first page.
            ShowPage(PageIndex.WelcomePage);

            button_Back.Enabled = false;
        }

        public DialogResult ShowDialog(out ClientInformation ClientInfo)
        {
            DialogResult dialogResult = ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                ClientInfo = new ClientInformation(null, AuthEndpoints.Basic, Resources.None, "", "", "", "");
            }

            ClientInfo = clientInfo;
            return dialogResult;
        }

        internal void ShowPage(PageIndex PageIndexToShow)
        {
            previousPages.Add(currentPageIndex);
            currentPageIndex = PageIndexToShow;

            Control currentPage = (Control)pages[(int)currentPageIndex];

            currentPage.Top = 0;
            currentPage.Left = 0;
            currentPage.Enabled = true;
            currentPage.Dock = DockStyle.Top;
            currentPage.Height = 200;
            currentPage.Visible = true;

            if (currentPage.Tag != null && currentPage.Tag.ToString() != "")
            {
                try
                {
                    currentPage.Controls[currentPage.Tag.ToString()].Focus();
                }
                catch
                {

                }
            }

            if (previousPages.Count != 1)
            {
                Control previousPage = (Control)pages[(int)previousPages[previousPages.Count - 1]];

                previousPage.Dock = DockStyle.None;
                previousPage.Left = 1000;
                previousPage.Enabled = false;
                previousPage.Visible = false;
            }
        }

        private void Button_Next_Click(object sender, EventArgs e)
        {
            pages[(int)currentPageIndex].NextButtonAction();
            button_Back.Enabled = true;
        }

        private void Button_Back_Click(object sender, EventArgs e)
        {
            Control previousPage = (Control)pages[(int)previousPages[previousPages.Count - 1]];

            previousPage.Top = 0;
            previousPage.Left = 0;
            previousPage.Enabled = true;
            previousPage.Dock = DockStyle.Top;
            previousPage.Height = 200;
            previousPage.Visible = true;

            Control currentPage = (Control)pages[(int)currentPageIndex];
            currentPage.Dock = DockStyle.None;
            currentPage.Left = 1000;
            currentPage.Enabled = false;
            currentPage.Visible = false;

            currentPageIndex = previousPages[previousPages.Count - 1];
            previousPages.RemoveAt(previousPages.Count - 1);

            if (previousPages.Count == 1)
            {
                button_Back.Enabled = false;
            }
        }

        internal TokenResponse AcquireAccessToken(string PostBody, string EndPointUrl)
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

        internal void CloseWizard(ClientInformation clientInformation)
        {
            // Save settings.
            Properties.Settings.Default.Save();

            // Close window.
            clientInfo = clientInformation;
            DialogResult = DialogResult.OK;
            Close();
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
    }
}