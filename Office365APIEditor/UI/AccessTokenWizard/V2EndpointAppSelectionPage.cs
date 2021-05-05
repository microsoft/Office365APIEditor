// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class V2EndpointAppSelectionPage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public V2EndpointAppSelectionPage()
        {
            InitializeComponent();
        }

        private void V2EndpointAppSelectionPage_Load(object sender, System.EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Enable DoubleClick event of radio buttons.
            MethodInfo methodInfo = typeof(RadioButton).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);

            List<RadioButton> radioButtons = new List<RadioButton>
            {
                radioButton_Web,
                radioButton_Native,
                radioButton_AppOnlyByCert,
                radioButton_AppOnlyByKey,
                radioButton_AdminConsent
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

            wizard.ShowPage(pageToShow);
        }
    }
}