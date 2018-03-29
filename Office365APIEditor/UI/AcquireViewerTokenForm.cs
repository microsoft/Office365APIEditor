// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using System;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class AcquireViewerTokenForm : Form
    {
        PublicClientApplication _pca;
        AuthenticationResult _ar;

        public AcquireViewerTokenForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(out PublicClientApplication pca, out AuthenticationResult ar)
        {
            DialogResult result = ShowDialog();

            pca = _pca;
            ar = _ar;

            return result;
        }

        private async void button_AcquireAccessToken_Click(object sender, EventArgs e)
        {
            if (textBox_ClientID.Text == "")
            {
                MessageBox.Show("Enter the Application ID.", "Office365APIEditor");
                return;
            }

            await AcquireAccessTokenAsync(textBox_ClientID.Text);
        }

        private void AcquireViewerTokenForm_Load(object sender, EventArgs e)
        {
            linkLabel_Portal.Text = "Enter the Application ID of your application registered in Application Registration Portal as a mobile (native) application.";
            int startIndex = linkLabel_Portal.Text.IndexOf("Application Registration Portal", 0, linkLabel_Portal.Text.Length);
            linkLabel_Portal.Links.Add(startIndex, ("Application Registration Portal").Length, "https://apps.dev.microsoft.com/");
        }

        private void linkLabel_Portal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private async void button_UseBuiltInApp_ClickAsync(object sender, EventArgs e)
        {
            await AcquireAccessTokenAsync(Properties.Settings.Default.BuiltInAppClientId);
        }

        private async System.Threading.Tasks.Task AcquireAccessTokenAsync(string ClientId)
        {
            Cursor = Cursors.WaitCursor;

            string[] scopes = Util.MailboxViewerScopes();

            _pca = new PublicClientApplication(ClientId);

            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                stringBuilder.AppendLine("MSAL - AcquireTokenAsync");
                stringBuilder.AppendLine("Application ID : " + ClientId);
                stringBuilder.AppendLine("Scope : " + string.Join(",", scopes));

                _ar = await _pca.AcquireTokenAsync(scopes, "", UIBehavior.ForceLogin, "");

                stringBuilder.AppendLine("Result : Success");
                stringBuilder.AppendLine("AccessToken : " + (_ar.AccessToken ?? ""));
                stringBuilder.AppendLine("ExpiresOn : " + _ar.ExpiresOn.ToString());
                stringBuilder.AppendLine("IdToken : " + (_ar.IdToken ?? ""));
                stringBuilder.AppendLine("Scope : " + string.Join(",", _ar.Scopes));
                stringBuilder.AppendLine("UniqueId : " + (_ar.UniqueId ?? ""));
                stringBuilder.AppendLine("DisplayableId : " + (_ar.User.DisplayableId ?? ""));
                stringBuilder.AppendLine("Identifier : " + (_ar.User.Identifier ?? ""));
                stringBuilder.AppendLine("Name : " + (_ar.User.Name ?? ""));

                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Cursor = Cursors.Default;
                Close();
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine("Result : Fail");
                stringBuilder.AppendLine("Message : " + ex.Message);
                Cursor = Cursors.Default;

                if (ex.Message != "User canceled authentication")
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor");
                }
            }

            Util.WriteCustomLog("AcquireViewerTokenForm", stringBuilder.ToString());
        }
    }
}
