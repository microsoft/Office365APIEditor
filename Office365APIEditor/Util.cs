// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                string workingFolder = Properties.Settings.Default.LogFolderPath;

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
                    "Office 365 Management API",
                    "Azure Resource Manager"
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
                case "Azure Resource Manager":
                    return "https://management.azure.com/";
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
                case "Azure Resource Manager":
                    return Resources.AzureRM;
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
                case Resources.AzureRM:
                    return "Azure Resource Manager";
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

        public static TokenResponse ConvertAuthenticationResultToTokenResponse(Microsoft.Identity.Client.AuthenticationResult value)
        {
            return new TokenResponse
            {
                token_type = "",
                expires_in = "",
                scope = string.Join(",", value.Scopes),
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

            Debug.WriteLine(sb.ToString());

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
            request.UserAgent = Util.CustomUserAgent;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + LocalTimeZoneId + "\"");

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
            request.UserAgent = Util.CustomUserAgent;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + LocalTimeZoneId + "\"");

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
            request.UserAgent = Util.CustomUserAgent;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + LocalTimeZoneId + "\"");

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
            request.UserAgent = Util.CustomUserAgent;
            request.ContentType = "application/json";

            request.Headers.Add("Authorization:Bearer " + AccessToken);

            request.Headers.Add("X-AnchorMailbox:" + MailAddress);
            request.Headers.Add("Prefer", "outlook.timezone=\"" + LocalTimeZoneId + "\"");

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

        public static string LocalTimeZoneId
        {
            get
            {
                // Get the local time zone info.
                // In the Windows sandbox environment, we cannot get the info correctly.
                // So we need to check the value.

                string localTimeZoneId = TimeZoneInfo.Local.Id;

                if (!System.Text.RegularExpressions.Regex.IsMatch(localTimeZoneId, "^[ -~]"))
                {
                    // localTimeZoneId contains special character.
                    return "UTC";
                }
                else
                {
                    return localTimeZoneId;
                }
            }
        }

        public static string[] MailboxViewerScopes()
        {
            if (UseMicrosoftGraphInMailboxViewer)
            {
                return new string[] { "https://graph.microsoft.com/Calendars.Read https://graph.microsoft.com/Calendars.ReadWrite https://graph.microsoft.com/Contacts.Read https://graph.microsoft.com/Contacts.ReadWrite https://graph.microsoft.com/Mail.Read https://graph.microsoft.com/Mail.ReadWrite https://graph.microsoft.com/Tasks.Read https://graph.microsoft.com/Tasks.ReadWrite https://graph.microsoft.com/Mail.Send https://graph.microsoft.com/User.ReadBasic.All" };
            }
            else
            {
                return new string[] { "https://outlook.office.com/Calendars.Read https://outlook.office.com/Calendars.ReadWrite https://outlook.office.com/Contacts.Read https://outlook.office.com/Contacts.ReadWrite https://outlook.office.com/Mail.Read https://outlook.office.com/Mail.ReadWrite https://outlook.office.com/tasks.read https://outlook.office.com/tasks.readwrite https://outlook.office.com/Mail.Send https://outlook.office.com/User.ReadBasic.All" };
            }
        }

        public static bool UseMicrosoftGraphInMailboxViewer => true;

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

                string defaultUserAgent = "Office365APIEditor/" + VersionString;

                if (Properties.Settings.Default.CustomUserAgentMode == 0)
                {
                    return defaultUserAgent;
                }
                else if (Properties.Settings.Default.CustomUserAgentMode == 1)
                {
                    return Properties.Settings.Default.CustomUserAgent;
                }
                else
                {
                    return defaultUserAgent;
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

        public static ReadOnlyCollection<string> GetCalendarApiSupportedTimeZones()
        {
            // https://docs.microsoft.com/en-us/graph/api/resources/datetimetimezone?view=graph-rest-1.0#additional-time-zones
            // As of 2020/07/16

            List<string> timeZones = new List<string>();

            timeZones.Add("Etc/GMT+12");
            timeZones.Add("Etc/GMT+11");
            timeZones.Add("Pacific/Honolulu");
            timeZones.Add("America/Anchorage");
            timeZones.Add("America/Santa_Isabel");
            timeZones.Add("America/Los_Angeles");
            timeZones.Add("America/Phoenix");
            timeZones.Add("America/Chihuahua");
            timeZones.Add("America/Denver");
            timeZones.Add("America/Guatemala");
            timeZones.Add("America/Chicago");
            timeZones.Add("America/Mexico_City");
            timeZones.Add("America/Regina");
            timeZones.Add("America/Bogota");
            timeZones.Add("America/New_York");
            timeZones.Add("America/Indiana/Indianapolis");
            timeZones.Add("America/Caracas");
            timeZones.Add("America/Asuncion");
            timeZones.Add("America/Halifax");
            timeZones.Add("America/Cuiaba");
            timeZones.Add("America/La_Paz");
            timeZones.Add("America/Santiago");
            timeZones.Add("America/St_Johns");
            timeZones.Add("America/Sao_Paulo");
            timeZones.Add("America/Argentina/Buenos_Aires");
            timeZones.Add("America/Cayenne");
            timeZones.Add("America/Godthab");
            timeZones.Add("America/Montevideo");
            timeZones.Add("America/Bahia");
            timeZones.Add("Etc/GMT+2");
            timeZones.Add("Atlantic/Azores");
            timeZones.Add("Atlantic/Cape_Verde");
            timeZones.Add("Africa/Casablanca");
            timeZones.Add("Etc/GMT");
            timeZones.Add("Europe/London");
            timeZones.Add("Atlantic/Reykjavik");
            timeZones.Add("Europe/Berlin");
            timeZones.Add("Europe/Budapest");
            timeZones.Add("Europe/Paris");
            timeZones.Add("Europe/Warsaw");
            timeZones.Add("Africa/Lagos");
            timeZones.Add("Africa/Windhoek");
            timeZones.Add("Europe/Bucharest");
            timeZones.Add("Asia/Beirut");
            timeZones.Add("Africa/Cairo");
            timeZones.Add("Asia/Damascus");
            timeZones.Add("Africa/Johannesburg");
            timeZones.Add("Europe/Kiev"); // Europe/Kyiv (Kiev)
            timeZones.Add("Europe/Istanbul");
            timeZones.Add("Asia/Jerusalem");
            timeZones.Add("Asia/Amman");
            timeZones.Add("Asia/Baghdad");
            timeZones.Add("Europe/Kaliningrad");
            timeZones.Add("Asia/Riyadh");
            timeZones.Add("Africa/Nairobi");
            timeZones.Add("Asia/Tehran");
            timeZones.Add("Asia/Dubai");
            timeZones.Add("Asia/Baku");
            timeZones.Add("Europe/Moscow");
            timeZones.Add("Indian/Mauritius");
            timeZones.Add("Asia/Tbilisi");
            timeZones.Add("Asia/Yerevan");
            timeZones.Add("Asia/Kabul");
            timeZones.Add("Asia/Karachi");
            timeZones.Add("Asia/Tashkent"); // Asia/Toshkent (Tashkent)
            timeZones.Add("Asia/Kolkata");
            timeZones.Add("Asia/Colombo");
            timeZones.Add("Asia/Kathmandu");
            timeZones.Add("Asia/Almaty"); // Asia/Astana (Almaty)
            timeZones.Add("Asia/Dhaka");
            timeZones.Add("Asia/Yekaterinburg");
            timeZones.Add("Asia/Rangoon"); // Asia/Yangon (Rangoon)
            timeZones.Add("Asia/Bangkok");
            timeZones.Add("Asia/Novosibirsk");
            timeZones.Add("Asia/Shanghai");
            timeZones.Add("Asia/Krasnoyarsk");
            timeZones.Add("Asia/Singapore");
            timeZones.Add("Australia/Perth");
            timeZones.Add("Asia/Taipei");
            timeZones.Add("Asia/Ulaanbaatar");
            timeZones.Add("Asia/Irkutsk");
            timeZones.Add("Asia/Tokyo");
            timeZones.Add("Asia/Seoul");
            timeZones.Add("Australia/Adelaide");
            timeZones.Add("Australia/Darwin");
            timeZones.Add("Australia/Brisbane");
            timeZones.Add("Australia/Sydney");
            timeZones.Add("Pacific/Port_Moresby");
            timeZones.Add("Australia/Hobart");
            timeZones.Add("Asia/Yakutsk");
            timeZones.Add("Pacific/Guadalcanal");
            timeZones.Add("Asia/Vladivostok");
            timeZones.Add("Pacific/Auckland");
            timeZones.Add("Etc/GMT-12");
            timeZones.Add("Pacific/Fiji");
            timeZones.Add("Asia/Magadan");
            timeZones.Add("Pacific/Tongatapu");
            timeZones.Add("Pacific/Apia");
            timeZones.Add("Pacific/Kiritimati");

            return new ReadOnlyCollection<string>(timeZones);
        }

        public static DialogResult ShowErrorWindow(Exception Exception, Form OwnerWindow)
        {
            UI.ErrorForm errorForm = new UI.ErrorForm(Exception, OwnerWindow)
            {
                Owner = OwnerWindow
            };
            return errorForm.ShowDialog();
        }

        public static string VersionString
        {
            get
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
                debugIndicator += " (DEBUG)";
#endif

                return friendlyVersion + debugIndicator;
            }
        }
    }

    public enum Resources
    {
        None = -1,
        Outlook = 0,
        Graph = 1,
        Management = 2,
        AzureRM = 3
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
