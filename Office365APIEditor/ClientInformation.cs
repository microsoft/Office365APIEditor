// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor
{
    public class ClientInformation
    {
        public TokenResponse Token { get; private set; }
        public AuthEndpoints AuthType { get; private set; }
        public Resources Resource { get; private set; }
        public string ClientID { get; private set; }
        public string ClientSecret { get; private set; }
        public string Scopes { get; private set; }
        public string RedirectUri { get; private set; }
        public string TenantName { get; private set; }

        public ClientInformation(TokenResponse token, AuthEndpoints authType, Resources resource, string clientID, string clientSecret, string scopes, string redirectUri, string tenantName = "common")
        {
            Token = token;
            AuthType = authType;
            Resource = resource;
            ClientID = clientID;
            ClientSecret = clientSecret;
            Scopes = scopes;
            RedirectUri = redirectUri;
            TenantName = tenantName;
        }

        public string ResourceUri
        {
            get
            {
                return Util.ConvertResourceEnumToUri(Resource);
            }
        }

        public void ReplaceToken(TokenResponse newToken)
        {
            if (newToken.access_token == null && newToken.id_token != null)
            {
                // Using OpenID Connect
                newToken.access_token = newToken.id_token;
            }

            Token = newToken;
        }
    }

    public enum AuthEndpoints
    {
        Basic,
        OAuthV1,
        OAuthV2
    }
}
