// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;
using System.Windows.Forms;

namespace Office365APIEditor.AccessTokenUtil
{
    class V1WebAppUtil : AccessTokenUtil
    {
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public Resources Resource { get; set; }
        public string ClientSecret { get; set; }

        public V1WebAppUtil()
        {
        }

        public ValidateResult Validate()
        {
            // Check properties.

            bool result = false;
            List<string> errorList = new List<string>();

            if (ClientID == "")
            {
                errorList.Add("Enter the Client ID.");
            }

            if (RedirectUri == "")
            {
                errorList.Add("Enter the Redirect URL.");
            }

            if (!Util.IsValidUrl(RedirectUri))
            {
                errorList.Add("Format of Redirect URL is invalid.");
            }

            if (ClientSecret == "")
            {
                errorList.Add("Enter the Client Secret.");
            }

            if (errorList.Count == 0)
            {
                result = true;
            }

            return new ValidateResult() { IsValid = result, ErrorMessage = errorList.ToArray() };
        }

        public AcquireAccessTokenResult AcquireAccessToken()
        {
            AcquireAuthorizationCodeResult acquireAuthorizationCodeResult = AcquireAuthorizationCode();

            if (acquireAuthorizationCodeResult.Success == InteractiveResult.Success)
            {
                return InternalAcquireAccessToken(acquireAuthorizationCodeResult.AuthorizationCode);
            }
            else if (acquireAuthorizationCodeResult.Success == InteractiveResult.Fail)
            {
                return new AcquireAccessTokenResult()
                {
                    Success = InteractiveResult.Fail,
                    Token = null,
                    ErrorMessage = acquireAuthorizationCodeResult.ErrorMessage
                };
            }
            else
            {
                // User closed the auth window.

                return new AcquireAccessTokenResult()
                {
                    Success = InteractiveResult.Cancel,
                    Token = null,
                    ErrorMessage = ""
                };
            }
        }

        private AcquireAuthorizationCodeResult AcquireAuthorizationCode()
        {
            GetCodeForm getCodeForm = new GetCodeForm(ClientID, RedirectUri, Util.ConvertResourceEnumToUri(Resource));

            if (getCodeForm.ShowDialog(out string Code) == DialogResult.OK)
            {
                if (Code != "")
                {
                    return new AcquireAuthorizationCodeResult()
                    {
                        Success = InteractiveResult.Success,
                        AuthorizationCode = Code,
                        ErrorMessage = ""
                    };
                }
                else
                {
                    return new AcquireAuthorizationCodeResult()
                    {
                        Success = InteractiveResult.Fail,
                        AuthorizationCode = Code,
                        ErrorMessage = "Getting Authorization Code was failed."
                    };
                }
            }
            else
            {
                return new AcquireAuthorizationCodeResult()
                {
                    Success = InteractiveResult.Cancel,
                    AuthorizationCode = Code,
                    ErrorMessage = "The user canceled the authentication window."
                };
            }
        }

        private AcquireAccessTokenResult InternalAcquireAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) +
                "&client_id=" + ClientID +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(ClientSecret) +
                "&code=" + AuthorizationCode +
                "&resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceEnumToUri(Resource));

            return AcquireAccessToken(postBody, "https://login.microsoftonline.com/common/oauth2/token");
        }
    }
}
