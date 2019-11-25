// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Office365APIEditor
{
    public static class Util
    {
        public const string LatestVersionUri = "https://office365apieditor.azurewebsites.net/latestmsi.txt";
        public const string LatestMsiUri = "https://office365apieditor.azurewebsites.net/installers/Setup.msi";
        public const string LatestZipUri = "https://office365apieditor.azurewebsites.net/installers/Office365APIEditor.zip";

        // DefaultApplicationPath is used as the default log path.
        public static string DefaultApplicationPath
        {
            get
            {
                string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Office365APIEditor");

                if (!Directory.Exists(result))
                {
                    Directory.CreateDirectory(result);
                }

                return result;
            }
        }

        public static string RunHistoryPath
        {
            get
            {
                string workingFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Office365APIEditor");

                if (!Directory.Exists(workingFolder))
                {
                    Directory.CreateDirectory(workingFolder);
                }

                return Path.Combine(workingFolder, "RunHistory.xml");
            }
        }

        public static bool IsMsiDeployed
        {
            get
            {
                string programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Replace(" (x86)", "");

                return Application.StartupPath.StartsWith(programFilesPath);
            }
        }

        public static bool IsValidUrl(string StringUri)
        {
            return Uri.TryCreate(StringUri, UriKind.Absolute, out Uri temp);
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

        public static T Deserialize<T>(string json)
        {
            T result;

            using (var memoryStream = new MemoryStream())
            {
                byte[] jsonByteArray = Encoding.UTF8.GetBytes(json);

                memoryStream.Write(jsonByteArray, 0, jsonByteArray.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    result = (T)serializer.ReadObject(jsonReader);
                }
            }

            return result;
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
        
        public static bool WriteSystemLog(string Title, string Message)
        {
            // Write log if SystemLogging flag is true.

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Title);
            sb.AppendLine("DateTime : " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            sb.AppendLine(Message);

            if (Properties.Settings.Default.SystemLogging)
            {
                return WriteLog(sb);
            }
            else
            {
                return true;
            }
        }

        public static bool WriteLog(StringBuilder Message)
        {
            // Write log.

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
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");

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

        public static async Task<string> SendPostRequestAsync(Uri URL, string AccessToken, string MailAddress, string PostData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");

            request.Method = "POST";

            // Build a body.
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string originalRequestBody = PostData;

                streamWriter.Write(originalRequestBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

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

        internal static async Task<string> SendDeleteRequestAsync(Uri URL, string AccessToken, string MailAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");

            request.Method = "DELETE";

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

        public static async Task<string> SendPatchRequestAsync(Uri URL, string AccessToken, string MailAddress, string PostData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");

            request.Method = "PATCH";

            // Build a body.
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string originalRequestBody = PostData;

                streamWriter.Write(originalRequestBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

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

        public static void InsertHeaders(object sender, SendingRequest2EventArgs e, string email)
        {
            e.RequestMessage.SetHeader("X-AnchorMailbox", email);
            e.RequestMessage.SetHeader("Prefer", "outlook.timezone=\"" + System.TimeZoneInfo.Local.Id + "\"");
        }

        public static string[] MailboxViewerScopes()
        {
            return new string[] { "https://outlook.office.com/Calendars.Read https://outlook.office.com/Calendars.ReadWrite https://outlook.office.com/Contacts.Read https://outlook.office.com/Contacts.ReadWrite https://outlook.office.com/Mail.Read https://outlook.office.com/Mail.ReadWrite https://outlook.office.com/tasks.read https://outlook.office.com/tasks.readwrite https://outlook.office.com/Mail.Send https://outlook.office.com/User.ReadBasic.All" };
        }

        public static string EscapeForJson(string originalString)
        {
            string quotedString = System.Web.Helpers.Json.Encode(originalString);
            return quotedString.Substring(1, quotedString.Length - 2);
        }

        public static string CustomUserAgent
        {
            get
            {
                // Custom UserAgent is a preview feature.
                // We don't change all of UserAgent because we don't want to use urlmon.dll or ActiveX to change UserAgent of WebBrowser control.

                if (Properties.Settings.Default.CustomUserAgentMode == 0)
                {
                    return "";
                }
                else if (Properties.Settings.Default.CustomUserAgentMode == 1)
                {
                    return Properties.Settings.Default.CustomUserAgent;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string GenerateWindowTitle(string FunctionName)
        {
            Version productVersion = Version.Parse(Application.ProductVersion);
            string friendlyVersion = "";

            if (productVersion.Revision == 0)
            {
                friendlyVersion = productVersion.ToString(3);
            }
            else
            {
                friendlyVersion = productVersion.ToString(4);
            }

            string debugIndicator = "";

#if DEBUG
            debugIndicator += "[DEBUG]";
#endif

            return $"Office365APIEditor - {FunctionName} - {friendlyVersion} {debugIndicator}";
        }
    }

    public enum Resources
    {
        None,
        Outlook,
        Graph,
        Management
    }

    public struct FolderInfo
    {
        public string ID;
        public FolderContentType Type;
        public bool Expanded;
    }

    public enum FolderContentType
    {
        Message,
        Contact,
        Calendar,
        DummyCalendarGroupRoot,
        CalendarGroup,
        MsgFolderRoot,
        DummyTaskGroupRoot,
        TaskGroup,
        Task,
        Drafts
    }
}
