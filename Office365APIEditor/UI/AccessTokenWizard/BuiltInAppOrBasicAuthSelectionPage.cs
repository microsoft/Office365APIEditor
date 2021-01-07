// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class BuiltInAppOrBasicAuthSelectionPage : UserControl, IAccessTokenWizardPage
    {
        public BuiltInAppOrBasicAuthSelectionPage()
        {
            InitializeComponent();
        }

        public void NextButtonAction()
        {
            var wizard = (AccessTokenWizardForm)Parent;

            if (radioButton_BasicAuth.Checked)
            {
                // Basic auth
                wizard.CloseWizard(new ClientInformation(new TokenResponse(), AuthEndpoints.Basic, Resources.None, "", "", "", ""));
            }
            else
            {
                // Built-in app
                wizard.ShowPage(AccessTokenWizardForm.PageIndex.BuiltInAppSettingPage);
            }
        }
    }
}