// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    public static class Global
    {
        public static IPublicClientApplication pca;
        public static IAccount currentUser;
        public static AuthenticationResult LastAuthenticationResult;

        private static async Task<AuthenticationResult> InternalGetAccessTokenAsync()
        {
            // Acquire access token.

            AuthenticationResult result;

            try
            {
                var user = await pca.GetAccountAsync(currentUser.HomeAccountId.Identifier);
                result = await pca.AcquireTokenSilent(Util.MailboxViewerScopes(), user).ExecuteAsync();
            }
            catch (Exception)
            {
                try
                {
                    result = await pca.AcquireTokenInteractive(Util.MailboxViewerScopes()).WithAccount(currentUser).WithPrompt(Prompt.ForceLogin).ExecuteAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            // Return cached access token.
            // Get access token again if LastAuthenticationResult is null or expired.

            if (LastAuthenticationResult == null || LastAuthenticationResult.ExpiresOn.UtcDateTime < DateTime.UtcNow.AddMinutes(-5))
            {
                LastAuthenticationResult = await InternalGetAccessTokenAsync();
            }
            
            return LastAuthenticationResult.AccessToken;
        }
    }
}