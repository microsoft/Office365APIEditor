// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V2EndpointAppSelectionPage : UserControl, IAccessTokenWizardPage
    {
        public V2EndpointAppSelectionPage()
        {
            InitializeComponent();
        }

        public void NextButtonAction()
        {
            AccessTokenWizardForm.PageIndex pageToShow = AccessTokenWizardForm.PageIndex.None;

            if (radioButton_Web.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointWebAppSettingPage;
            }
            else if (radioButton_Native.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointNativeAppSettingPage;
            }
            else if (radioButton_AppOnlyByCert.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointAppOnlyByCertSettingPage;
            }
            else if (radioButton_AppOnlyByKey.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointAppOnlyByKeySettingPage;
            }
            else if (radioButton_AdminConsent.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointAdminConsentSettingPage;
            }

            var wizard = (AccessTokenWizardForm)Parent;
            wizard.ShowPage(pageToShow);
        }
    }
}