// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class CheckUpdateForm : Form
    {
        public CheckUpdateForm()
        {
            InitializeComponent();
        }

        private void CheckUpdateForm_Load(object sender, EventArgs e)
        {
            StartLatestVersionCheck();            
        }

        private void StartLatestVersionCheck()
        {
            // If the latest release check has already finished, use the result.

            if (!string.IsNullOrEmpty(Properties.Settings.Default.NewerInstallerPath))
            {
                label_CheckResult.Text = "You can update to the latest version when you close Office365APIEditor.";
            }
            else
            {
                // Get the build number of the latest release using the other thread
                // Download is not performed because another thread is also performing download and there is a possibility of conflict.

                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpWebRequest versionRequest = WebRequest.CreateHttp(Util.LatestVersionUri);
                versionRequest.Method = "GET";
                versionRequest.ContentType = "text/plain";

                try
                {
                    IAsyncResult r = versionRequest.BeginGetResponse(new AsyncCallback(VersionCheckResponseCallback), versionRequest);
                }
                catch
                {
                    label_CheckResult.Text = "Failed to get the latest release information.";
                }
            }
        }

        private void VersionCheckResponseCallback(IAsyncResult ar)
        {
            try
            {
                HttpWebRequest versionRequest = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse versionResponse = (HttpWebResponse)versionRequest.EndGetResponse(ar);

                string latestVersionString = "";

                using (Stream stream = versionResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        latestVersionString = streamReader.ReadToEnd().Trim();
                    }
                }

                Version.TryParse(latestVersionString, out Version latestVersion);

                if (latestVersion != null && latestVersion.CompareTo(Version.Parse(Application.ProductVersion)) > 0)
                {
                    // Newer version is available
                    UpdateStatus("An update was found and now downloading it. Check it again later.");
                }
                else
                {
                    // Newer version is not available
                    UpdateStatus("There are currently no updates available.");
                }
            }
            catch (Exception)
            {
                UpdateStatus("Failed to get the latest release information.");
            }
        }

        private void UpdateStatus(string message)
        {
            if (label_CheckResult.InvokeRequired)
            {
                label_CheckResult.Invoke(new MethodInvoker(delegate { label_CheckResult.Text = message; }));
            }
            else
            {
                label_CheckResult.Text = message;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
