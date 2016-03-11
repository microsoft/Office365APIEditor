using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
