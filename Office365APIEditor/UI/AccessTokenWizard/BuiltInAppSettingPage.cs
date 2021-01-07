// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.AccessTokenUtil;
using System;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class BuiltInAppSettingPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public BuiltInAppSettingPage()
        {
            InitializeComponent();
        }

        private void BuiltInAppSettingPage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;
        }

        private void Button_ScopeEditor_Click(object sender, EventArgs e)
        {
            string[] selectedScopes;

            if (textBox_Scopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_Scopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_Scopes.Text = scopes;
            }
        }

        public void NextButtonAction()
        {
            V2NativeAppUtil builtInAppUtil = new V2NativeAppUtil()
            {
                ClientID = Properties.Settings.Default.BuiltInAppClientId,
                RedirectUri = Properties.Settings.Default.BuiltInAppRedirectUri,
                Scopes = textBox_Scopes.Text,
                TenantName = "common"
            };

            ValidateResult validateResult = builtInAppUtil.Validate();

            if (validateResult.IsValid)
            {
                AcquireAccessTokenResult acquireAccessTokenResult = builtInAppUtil.AcquireAccessToken();

                if (acquireAccessTokenResult.Success == InteractiveResult.Fail)
                {
                    MessageBox.Show(acquireAccessTokenResult.ErrorMessage, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (acquireAccessTokenResult.Success == InteractiveResult.Cancel)
                {
                    return;
                }

                TokenResponse tokenResponse = acquireAccessTokenResult.Token;

                if (tokenResponse != null)
                {
                    wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.OAuthV2, Resources.None, Properties.Settings.Default.BuiltInAppClientId, "", textBox_Scopes.Text, Properties.Settings.Default.BuiltInAppRedirectUri));
                }
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, validateResult.ErrorMessage), "Office365APIEditor");
            }
        }
    }
}