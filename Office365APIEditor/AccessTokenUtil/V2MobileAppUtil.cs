using System.Collections.Generic;

namespace Office365APIEditor.AccessTokenUtil
{
    class V2MobileAppUtil : AccessTokenUtil
    {
        public string ClientID { get; set; }
        public string RedirectUri { get; set; }
        public string Scopes { get; set; }

        public V2MobileAppUtil()
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

            if (Scopes == "")
            {
                errorList.Add("Enter the scopes.");
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
            GetCodeForm getCodeForm = new GetCodeForm(ClientID, RedirectUri, Scopes, true);

            return AcquireAuthorizationCode(getCodeForm);
        }

        private AcquireAccessTokenResult InternalAcquireAccessToken(string AuthorizationCode)
        {
            // Build a POST body.
            string postBody = "grant_type=authorization_code" +
                "&redirect_uri=" + RedirectUri +
                "&client_id=" + ClientID +
                "&code=" + AuthorizationCode +
                "&scope=" + Scopes;

            string endPoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

            return AcquireAccessToken(postBody, endPoint);
        }
    }
}
