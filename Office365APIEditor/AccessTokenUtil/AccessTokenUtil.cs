// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
using System.Text;

namespace Office365APIEditor.AccessTokenUtil
{
    public class AccessTokenUtil
    {
        protected AcquireAccessTokenResult AcquireAccessToken(string PostBody, string EndPointUrl)
        {
            InteractiveResult result = InteractiveResult.Fail;
            TokenResponse token = null;
            string errorMessage = "";

            System.Net.WebRequest request = System.Net.WebRequest.Create(EndPointUrl);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(PostBody);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                System.Net.WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    string jsonResponse = reader.ReadToEnd();

                    // Deserialize and get an Access Token.
                    token = Util.Deserialize<TokenResponse>(jsonResponse);
                }
            }
            catch (System.Net.WebException ex)
            {
                System.Net.WebResponse response = ex.Response;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                    string jsonResponse = reader.ReadToEnd();

                    errorMessage = ex.Message + "\r\n\r\n" + ex.StackTrace + "\r\n\r\n" + jsonResponse;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + "\r\n\r\n" + ex.StackTrace;
            }

            if (errorMessage == "")
            {
                result = InteractiveResult.Success;
            }

            return new AcquireAccessTokenResult()
            {
                Success = result,
                Token = token,
                ErrorMessage = errorMessage
            };
        }

    }

    public struct ValidateResult
    {
        public bool IsValid;
        public string[] ErrorMessage;
    }

    public enum InteractiveResult
    {
        Fail,
        Success,
        Cancel
    }

    public struct AcquireAuthorizationCodeResult
    {
        public InteractiveResult Success;
        public string AuthorizationCode;
        public string ErrorMessage;
    }

    public struct AcquireAccessTokenResult
    {
        public InteractiveResult Success;
        public TokenResponse Token;
        public string ErrorMessage;
    }
}
