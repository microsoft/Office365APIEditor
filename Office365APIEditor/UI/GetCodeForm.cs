// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class GetCodeForm : Form
    {
        private string redirectUrl;
        private string authenticationUrl;

        // Return value
        private string _acquiredCode = "";

        // Help URI to display if the authorization code could not be acquired.
        private Uri helpUri;

        public GetCodeForm(string ClientID, string RedirectUri, string ResourceOrScopeUri, bool IsV2 = false, string TenantName = "")
        {
            InternalInitialization(ClientID, RedirectUri, ResourceOrScopeUri, IsV2, TenantName, false);

            // Handle the HelpRequested event.
            HelpRequested += new HelpEventHandler(this.GetCodeForm_HelpRequested);
        }

        public GetCodeForm(string ClientID, string RedirectUri, string ResourceOrScopeUri, bool IsV2, bool AdminConsent)
        {
            if (IsV2 && AdminConsent)
            {
                InternalInitializationV2AdminConsent(ClientID, RedirectUri, ResourceOrScopeUri);
            }
            else
            {
                InternalInitialization(ClientID, RedirectUri, ResourceOrScopeUri, IsV2, "", AdminConsent);
            }
        }

        private void InternalInitialization(string ClientID, string RedirectUri, string ResourceOrScopeUri, bool IsV2 = false, string TenantName = "", bool AdminConsent = false)
        {
            InitializeComponent();

            redirectUrl = RedirectUri;

            // Build an URL of sign-in page.

            string endPoint = "https://login.microsoftonline.com/";

            if (TenantName == "")
            {
                endPoint += "common/oauth2";
            }
            else
            {
                endPoint += TenantName.Replace("@", ".") + "/oauth2";
            }

            if (IsV2 == true)
            {
                endPoint += "/v2.0";
                authenticationUrl = endPoint + "/authorize?" +
                    "response_type=code" +
                    "&client_id=" + ClientID +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) +
                    "&scope=" + System.Web.HttpUtility.UrlEncode(ResourceOrScopeUri) +
                    "&response_mode=query";

                if (AdminConsent)
                {
                    authenticationUrl += "&prompt=admin_consent";
                }
                else
                {
                    authenticationUrl += "&prompt=login";
                }
            }
            else
            {
                authenticationUrl = endPoint + "/authorize?" +
                    "resource=" + System.Web.HttpUtility.UrlEncode(ResourceOrScopeUri) +
                    "&response_type=code" +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) +
                    "&client_id=" + ClientID;

                if (AdminConsent)
                {
                    authenticationUrl += "&prompt=admin_consent";
                }
                else
                {
                    authenticationUrl += "&prompt=login";
                }

            }

            webBrowser1.DocumentTitleChanged += new EventHandler(webBrowser1_DocumentTitleChanged);
        }

        private void InternalInitializationV2AdminConsent(string ClientID, string RedirectUri, string Scope)
        {
            InitializeComponent();

            redirectUrl = RedirectUri;

            // Build an URL of sign-in page.

            string endPoint = "https://login.microsoftonline.com/";

            authenticationUrl = endPoint + "organizations/v2.0/adminconsent?" +
                    "&client_id=" + ClientID +
                    "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) +
                    "&scope=" + System.Web.HttpUtility.UrlEncode(Scope);

            webBrowser1.DocumentTitleChanged += new EventHandler(webBrowser1_DocumentTitleChanged);
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.Document.Title;
        }

        private void GetCodeForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            // Navigate to the sing-in page.
            webBrowser1.Navigate(authenticationUrl);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Check whether the URL is RedirectUrl.

            if (e.Url.AbsoluteUri.ToLower().StartsWith(redirectUrl.ToLower()))
            {
                if (e.Url.AbsoluteUri.Contains("code="))
                {
                    // Get the Authorization Code from a query string.

                    var queryString = e.Url.AbsoluteUri.Substring(e.Url.AbsoluteUri.IndexOf("?"));
                    NameValueCollection temp = System.Web.HttpUtility.ParseQueryString(queryString);
                    _acquiredCode = temp["code"];
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (e.Url.AbsoluteUri.Contains("admin_consent=True")) {
                        // V2 endpoint Admin Consent scenario

                        if (e.Url.AbsoluteUri.Contains("error="))
                        {
                            // Something is wrong
                            // User might canceled.

                            this.DialogResult = DialogResult.No;
                            this.Close();
                        }
                        else
                        {
                            _acquiredCode = "admin_consent=True";

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else if (_acquiredCode == "")
                    {
                        // Redirected to redirectUrl but we couldn't get the Authorization Code.

                        StringBuilder message = new StringBuilder();

                        message.AppendLine("Failed to get the authorization code.");
                        message.AppendLine("");

                        var queryString = e.Url.AbsoluteUri.Substring(e.Url.AbsoluteUri.IndexOf("?"));
                        NameValueCollection queries = System.Web.HttpUtility.ParseQueryString(queryString);

                        foreach (string queryName in queries)
                        {
                            message.AppendLine(queryName + ":");
                            message.AppendLine(System.Web.HttpUtility.UrlDecode(queries[queryName]));
                            message.AppendLine("");

                            if (queryName.ToLower() == "error_uri")
                            {
                                // Save the help Uri.

                                string errorUri = System.Web.HttpUtility.UrlDecode(queries[queryName]);

                                if (errorUri != null && errorUri != "")
                                {
                                    if (Uri.TryCreate(errorUri, UriKind.Absolute, out Uri result))
                                    {
                                        helpUri = result;
                                    }
                                    else
                                    {
                                        helpUri = null;
                                    }
                                }
                            }
                        }

                        if (helpUri != null)
                        {
                            // Show message box with "Help" button.
                            MessageBox.Show(message.ToString(), "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, true);
                        }
                        else
                        {
                            // Show message box without "Help" button.
                            MessageBox.Show(message.ToString(), "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        this.DialogResult = DialogResult.No;
                        this.Close();
                    }
                }
            }

            // If we couldn't get the Authorization Code, do nothing.
            // Authorization is in progress, or error messages is displayed on the page.
        }

        private void GetCodeForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            if (helpUri != null)
            {
                System.Diagnostics.Process.Start(helpUri.ToString());
            }
            else
            {
                MessageBox.Show("No help URL is available.", "Office365APIEditor");
            }
        }

        public DialogResult ShowDialog(out string code)
        {
            DialogResult result = this.ShowDialog();

            code = _acquiredCode;
            return result;
        }
    }
}
