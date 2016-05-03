// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class GetCodeForm : Form
    {
        private string redirectUrl;
        private string authenticationUrl;

        // Return value
        private string _acquiredCode = "";

        public GetCodeForm(string ClientID, string RedirectUri, string ResourceOrScopeUri, bool IsV2 = false)
        {
            InitializeComponent();

            redirectUrl = RedirectUri;

            // Build an URL of sign-in page.

            string endPoint = "https://login.microsoftonline.com/common/oauth2";

            if (IsV2 == true)
            {
                endPoint += "/v2.0";
                authenticationUrl = endPoint + "/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) + "&scope=" + System.Web.HttpUtility.UrlEncode(ResourceOrScopeUri) + "&response_mode=query&prompt =login";
            }
            else
            {
                authenticationUrl = endPoint + "/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) + "&resource=" + System.Web.HttpUtility.UrlEncode(ResourceOrScopeUri) + "&prompt=login";
            }

            webBrowser1.DocumentTitleChanged += new EventHandler(webBrowser1_DocumentTitleChanged);
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.Document.Title;
        }

        private void GetCodeForm_Load(object sender, EventArgs e)
        {
            // Navigate to the sing-in page.
            webBrowser1.Navigate(authenticationUrl);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Check whether the URL is RedirectUrl.

            if ((e.Url.AbsoluteUri.ToLower().StartsWith(redirectUrl.ToLower())) && (e.Url.AbsoluteUri.Contains("code")))
            {
                // Get the Authorization Code from a query string.

                var queryString = e.Url.AbsoluteUri.Substring(e.Url.AbsoluteUri.IndexOf("?"));
                NameValueCollection temp = System.Web.HttpUtility.ParseQueryString(queryString);
                _acquiredCode = temp["code"];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            // If we couldn't get the Authorization Code, do nothing.
            // Authorization is in progress, or error messages is displayed on the page.
        }

        public DialogResult ShowDialog(out string code)
        {
            DialogResult result = this.ShowDialog();

            code = _acquiredCode;
            return result;
        }
    }
}
