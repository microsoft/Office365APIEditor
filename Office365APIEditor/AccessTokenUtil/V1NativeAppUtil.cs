// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.Collections.Generic;

namespace Office365APIEditor.AccessTokenUtil
{
    class V1NativeAppUtil : AccessTokenUtil
    {
        public string TenantName { get; set; }
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public Resources Resource { get; set; }

        public V1NativeAppUtil()
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
            else if (!TenantName.EndsWith(".onmicrosoft.com"))
            {
                errorList.Add("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com");
            }
            else if (!Util.IsValidUrl("https://login.windows.net/" + TenantName))
            {
                errorList.Add("Format of Tenant Name is invalid.\ne.g. contoso.onmicrosoft.com");
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

            return AcquireAuthorizationCode(getCodeForm);
        }

        private AcquireAccessTokenResult InternalAcquireAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&Resource=" + System.Web.HttpUtility.UrlEncode(Util.ConvertResourceEnumToUri(Resource)) +
                "&client_id=" + ClientID +
                "&code=" + AuthorizationCode +
                "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri);

            string endPoint = "https://login.microsoftonline.com/" + TenantName.Replace("@", ".") + "/oauth2/token";

            return AcquireAccessToken(postBody, endPoint);
        }
    }
}
