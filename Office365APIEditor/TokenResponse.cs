// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor
{
    public class TokenResponse
    {
        public string token_type;
        public string expires_in;
        public string scope;
        public string expires_on;
        public string not_before;
        public string resource;
        public string access_token;
        public string refresh_token;
        public string id_token;

        public string Format()
        {
            return "access_token : " + access_token + "\r\n" +
                   "expires_in : " + expires_in + "\r\n" +
                   "expires_on : " + expires_on + "\r\n" +
                   "refresh_token : " + refresh_token + "\r\n" +
                   "resource : " + resource + "\r\n" +
                   "scope : " + scope + "\r\n" +
                   "token_type : " + token_type ;
        }
    }
}
