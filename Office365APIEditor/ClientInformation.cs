using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ClientInformation(TokenResponse token, AuthEndpoints authType, Resources resource, string clientID, string clientSecret, string scopes, string redirectUri)
        {
            Token = token;
            AuthType = authType;
            Resource = resource;
            ClientID = clientID;
            ClientSecret = clientSecret;
            Scopes = scopes;
            RedirectUri = redirectUri;
        }

        public string ResourceUri
        {
            get
            {
                return Util.ConvertResourceEnumToUri(Resource);
            }
        }
    }

    public enum AuthEndpoints
    {
        Basic,
        OAuthV1,
        OAuthV2
    }
}
