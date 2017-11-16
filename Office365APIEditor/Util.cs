// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor
{
    public static class Util
    {
        public static bool IsValidUrl(string StringUri)
        {
            Uri temp;
            return Uri.TryCreate(StringUri, UriKind.Absolute, out temp);
        }

        public static List<string> ResourceNames
        {
            get
            {
                return new List<string>
                {
                    "Exchange Online",
                    "Microsoft Graph",
                    "Office 365 Management API"
                };
            }
        }

        public static string ConvertResourceNameToUri(string ResourceName)
        {
            switch (ResourceName)
            {
                case "Exchange Online":
                    return "https://outlook.office.com/";
                case "Microsoft Graph":
                    return "https://graph.microsoft.com/";
                case "Office 365 Management API":
                    return "https://manage.office.com";
                default:
                    return "";
            }
        }

        public static Resources ConvertResourceNameToResourceEnum(string ResourceName)
        {
            switch (ResourceName)
            {
                case "Exchange Online":
                    return Resources.Outlook;
                case "Microsoft Graph":
                    return Resources.Graph;
                case "Office 365 Management API":
                    return Resources.Management;
                default:
                    return Resources.None;
            }
        }

        public static string ConvertResourceEnumToResourceName(Resources Resource)
        {
            switch (Resource)
            {
                case Resources.Outlook:
                    return "Exchange Online";
                case Resources.Graph:
                    return "Microsoft Graph";
                case Resources.Management:
                    return "Office 365 Management API";
                case Resources.None:
                default:
                    return "";
            }
        }

        public static string ConvertResourceEnumToUri(Resources Resource)
        {
            return ConvertResourceNameToUri(ConvertResourceEnumToResourceName(Resource));
        }

        public static TokenResponse ConvertAuthenticationResultToTokenResponse(Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult value)
        {
            return new TokenResponse
            {
                token_type = value.AccessTokenType,
                expires_in = "",
                scope = "",
                expires_on = value.ExpiresOn.ToString(),
                not_before = "",
                resource = "",
                access_token = value.AccessToken,
                // refresh_token = value.RefreshToken,
                id_token = value.IdToken
            };
        }
        
        public static bool WriteCustomLog(string Title, string Message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Title);
            sb.AppendLine("DateTime : " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            sb.AppendLine(Message);

            return WriteLog(sb);
        }

        public static bool WriteLog(StringBuilder Message)
        {
            Message.AppendLine("");

            string logFilePath = "";

            string settingLofFilePath = Properties.Settings.Default.LogFolderPath;

            if (!Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                // Specified log folder path is not exsisting.
                return false;
            }

            if (Properties.Settings.Default.LogFileStyle == "Static")
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, "Office365APIEditor.log");
            }
            else if (Properties.Settings.Default.LogFileStyle == "DateTime")
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, DateTime.UtcNow.ToString("yyyyMMdd") + ".log");
            }
            else
            {
                logFilePath = Path.Combine(Properties.Settings.Default.LogFolderPath, "Office365APIEditor.log");
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    sw.Write(Message.ToString());
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static async Task<string> SendGetRequestAsync(Uri URL, string AccessToken, string MailAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");

            request.Method = "GET";

            try
            {
                // Get a response and response stream.
                var response = (HttpWebResponse)await request.GetResponseAsync();

                string jsonResponse = "";
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    jsonResponse = reader.ReadToEnd();
                }

                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> GetAccessToken(PublicClientApplication pca, Microsoft.Identity.Client.IUser CurrentUser)
        {
            // Acquire access token.
            // This method is designed for GetOutlookServiceClient(), so if you need new OutlookServiceClient and new Access Token, you should use GetOutlookServiceClient().

            Microsoft.Identity.Client.AuthenticationResult ar;
            
            try
            {
                ar = await pca.AcquireTokenSilentAsync(Office365APIEditorHelper.MailboxViewerScopes(), CurrentUser);
            }
            catch (Exception extemp)
            {
                try
                {
                    ar = await pca.AcquireTokenAsync(Office365APIEditorHelper.MailboxViewerScopes(), CurrentUser, UIBehavior.ForceLogin, "");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ar.AccessToken;
        }

        public static async Task<OutlookServicesClient> GetOutlookServiceClient(PublicClientApplication pca, Microsoft.Identity.Client.IUser CurrentUser)
        {
            // Acquire access token again.

            string token = await Util.GetAccessToken(pca, CurrentUser);

            OutlookServicesClient newClient = new OutlookServicesClient(new Uri("https://outlook.office.com/api/v2.0"),
                () =>
                {
                    return Task.Run(() =>
                    {
                        return token;
                    });
                });

            newClient.Context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(
                (eventSender, eventArgs) => InsertHeaders(eventSender, eventArgs, CurrentUser.DisplayableId));

            return newClient;
        }

        private static void InsertHeaders(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
            e.RequestMessage.SetHeader("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");
        }
    }

    public enum Resources
    {
        None,
        Outlook,
        Graph,
        Management
    }
}
