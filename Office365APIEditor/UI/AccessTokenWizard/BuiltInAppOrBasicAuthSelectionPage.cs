// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class BuiltInAppOrBasicAuthSelectionPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public BuiltInAppOrBasicAuthSelectionPage()
        {
            InitializeComponent();
        }

        private void BuiltInAppOrBasicAuthSelectionPage_Load(object sender, System.EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Enable DoubleClick event of radio buttons.
            MethodInfo methodInfo = typeof(RadioButton).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);

            List<RadioButton> radioButtons = new List<RadioButton>
            {
                radioButton_BuiltInApp,
                radioButton_PreAcquiredAccessToken,
                radioButton_BasicAuth
            };

            foreach (var item in radioButtons)
            {
                if (methodInfo != null)
                {
                    methodInfo.Invoke(item, new object[] { ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true });
                }
                item.MouseDoubleClick += RadioButton_MouseDoubleClick;
            }
        }

        private void RadioButton_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            wizard.GoToNextPage();
        }

        public void NextButtonAction()
        {
            if (radioButton_BuiltInApp.Checked)
            {
                // Built-in app
                wizard.ShowPage(AccessTokenWizardForm.PageIndex.BuiltInAppSettingPage);
            }
            else if (radioButton_PreAcquiredAccessToken.Checked)
            {
                // Pre-acquired access token
                wizard.ShowPage(AccessTokenWizardForm.PageIndex.PreAcquiredAccessTokenSettingPage);
            }
            else if (radioButton_BasicAuth.Checked)
            {
                // Basic auth
                wizard.CloseWizard(new ClientInformation(new TokenResponse(), AuthEndpoints.Basic, Resources.None, "", "", "", ""));
            }
        }
    }
}