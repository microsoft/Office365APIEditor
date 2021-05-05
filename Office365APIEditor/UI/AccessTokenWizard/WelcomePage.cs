// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class WelcomePage : UserControl, IAccessTokenWizardPage
    {
        AccessTokenWizardForm wizard;

        public WelcomePage()
        {
            InitializeComponent();
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            wizard = (AccessTokenWizardForm)Parent;

            // Initialize link.
            linkLabel_Description.Links.Add(linkLabel_Description.Text.IndexOf("Learn more"), 10, "https://github.com/Microsoft/Office365APIEditor/blob/master/tutorials/Acquire_new_access_token_for_Editor_Mode.md");

            // Enable DoubleClick event of radio buttons.
            MethodInfo methodInfo = typeof(RadioButton).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);

            List<RadioButton> radioButtons = new List<RadioButton>
            {
                radioButton_V1Endpoint,
                radioButton_V2Endpoint,
                radioButton_BuiltInAppOrBasicAuth,
                radioButton_SharePointOnlineAppOnly
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

        private void LinkLabel_Description_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        public void NextButtonAction()
        {
            AccessTokenWizardForm.PageIndex pageToShow;

            if (radioButton_V1Endpoint.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V1EndpointAppSelectionPage;
            }
            else if (radioButton_V2Endpoint.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.V2EndpointAppSelectionPage;
            }
            else if (radioButton_BuiltInAppOrBasicAuth.Checked)
            {
                pageToShow = AccessTokenWizardForm.PageIndex.BuiltInAppOrBasicAuthSelectionPage;
            }
            else
            {
                pageToShow = AccessTokenWizardForm.PageIndex.SharePointOnlineAppOnlySettingPage;
            }

            wizard.ShowPage(pageToShow);
        }
    }
}