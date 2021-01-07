// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V1EndpointAppSelectionPage : UserControl, IAccessTokenWizardPage
    {
        public V1EndpointAppSelectionPage()
        {
            InitializeComponent();
        }

        public void NextButtonAction()
        {
            AccessTokenWizardForm.PageIndex pageToShow = AccessTokenWizardForm.PageIndex.None;

            if (radioButton_Web.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointWebAppSettingPage;
            }
            else if (radioButton_Native.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointNativeAppSettingPage;
            }
            else if (radioButton_AppOnlyByCert.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointAppOnlyByCertSettingPage;
            }
            else if (radioButton_AppOnlyByKey.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointAppOnlyByKeySettingPage;
            }
            else if (radioButton_AdminConsent.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointAdminConsentSettingPage;
            }

            var wizard = (AccessTokenWizardForm)Parent;
            wizard.ShowPage(pageToShow);
        }
    }
}