// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;

namespace Office365APIEditor.AccessTokenUtil
{
    class V2WebAppUtil : AccessTokenUtil
    {
        public string TenantName { get; set; }
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public string Scopes { get; set; }
        public string ClientSecret { get; set; }

        public V2WebAppUtil()
        {
        }

        public ValidateResult Validate()
        {
            // Check properties.

            bool result = false;
            List<string> errorList = new List<string>();

            if (TenantName == "")
            {
                errorList.Add("Enter the Tenant Name.");
            }

            if (ClientID == "")
            {
                errorList.Add("Enter the Client ID.");
            }

            if (RedirectUri == "")
            {
                errorList.Add("Enter the Redirect URL.");
            }
            else if (!Util.IsValidUrl(RedirectUri))
            {
                errorList.Add("Format of Redirect URL is invalid.");
            }

            if (Scopes == "")
            {
                errorList.Add("Enter the scopes.");
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
            GetCodeForm getCodeForm = new GetCodeForm(ClientID, RedirectUri, Scopes, true, TenantName);

            return AcquireAuthorizationCode(getCodeForm);
        }

        private AcquireAccessTokenResult InternalAcquireAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) +
                "&client_id=" + ClientID +
                "&client_secret=" + System.Web.HttpUtility.UrlEncode(ClientSecret) +
                "&code=" + AuthorizationCode +
                "&scope=" + Scopes;

            string endPoint = $"https://login.microsoftonline.com/{TenantName}/oauth2/v2.0/token";

            return AcquireAccessToken(postBody, endPoint);
        }
    }
}
