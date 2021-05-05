// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    class MyApplicationContext : ApplicationContext
    {
        public MyApplicationContext()
        {
            MailboxViewerForm mailboxViewerForm = new MailboxViewerForm();
            mailboxViewerForm.FormClosed += new FormClosedEventHandler(OnFormClosed);
            mailboxViewerForm.Show();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Util.WriteSystemLog("Office365APIEditor Closing Log", "Exit. Code 20");

#if !DEBUG
            if (!string.IsNullOrEmpty(Properties.Settings.Default.NewerInstallerPath) && File.Exists(Properties.Settings.Default.NewerInstallerPath) && Util.IsMsiDeployed)
            {
                // Newer installer is available

                if (MessageBox.Show("Do you want to install the latest version of Office365APIEditor?", "Office365APIEditor", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process msi = System.Diagnostics.Process.Start(Properties.Settings.Default.NewerInstallerPath);

                        Properties.Settings.Default.NewerInstallerPath = "";
                        Properties.Settings.Default.Save();
                    }
                    catch
                    {
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Properties.Settings.Default.NewerInstallerPath) && Properties.Settings.Default.NewerInstallerPath == Util.LatestZipUri)
            {
                if (MessageBox.Show("Do you want to download the latest version of Office365APIEditor?", "Office365APIEditor", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process zip = System.Diagnostics.Process.Start(Properties.Settings.Default.NewerInstallerPath);

                        Properties.Settings.Default.NewerInstallerPath = "";
                        Properties.Settings.Default.Save();
                    }
                    catch
                    {
                    }
                }
            }
#endif

            Environment.Exit(0);
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Save the contents of the log temporarily until the initialization of the setting is completed.
            StringBuilder startupLog = new StringBuilder();
            startupLog.AppendLine("Start.");

            // Load previous settings.

            // Default value of NeedUpgrade is TRUE.
            // If NeedUpgrade is TRUE, it means that the application has been upgraded.
            bool needUpgrade = Properties.Settings.Default.NeedUpgrade;

            if (needUpgrade == true)
            {
                startupLog.AppendLine("Setting upgrade is required.");

                try
                {
                    Properties.Settings.Default.Upgrade();
                    startupLog.AppendLine("Previous setting was loaded.");

                    UpgradeLastApps(startupLog);
                }
                catch
                {
                    startupLog.AppendLine("Setting was not upgraded.");
                }
                finally
                {
                    Properties.Settings.Default.NeedUpgrade = false;
                    Properties.Settings.Default.Save();
                    startupLog.AppendLine("Setting was saved.");
                }
            }

            // Turn off SystemLogging flag.
            startupLog.AppendLine("Disable SystemLogging as default.");
            Properties.Settings.Default.SystemLogging = false;
            Properties.Settings.Default.Save();

            // Check the command line switches.
            startupLog.AppendLine("CommandLine : " + Environment.CommandLine);
            string[] switches = Environment.GetCommandLineArgs();

            bool systemLogging = false;

            foreach (string command in switches)
            {
                if (command.ToLower() == ("/ResetSettings").ToLower())
                {
                    // Reset all settings.
                    startupLog.AppendLine("Setting will be reset.");
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                    startupLog.AppendLine("Setting was saved.");
                }
                else if(command.ToLower() == ("/RemoveHistory").ToLower())
                {
                    // Remove Run History file.
                    startupLog.AppendLine("The history file will be delete.");
                    if (File.Exists(Util.RunHistoryPath))
                    {
                        try
                        {
                            File.Delete(Util.RunHistoryPath);
                            startupLog.AppendLine("The history file was deleted.");
                        }
                        catch(Exception ex)
                        {
                            startupLog.AppendLine("The history file was not deleted.");
                            MessageBox.Show(ex.Message.ToString(), "Office365APIEditor");
                        }
                    }
                    else
                    {
                        startupLog.AppendLine("The history file does not exist.");
                    }
                }
                else if (command.ToLower() == ("/ResetCustomDefinedScopes").ToLower())
                {
                    // Reset CustomDefinedScopes setting.
                    startupLog.AppendLine("CustomDefinedScopes will be reset.");
                    Properties.Settings.Default.CustomDefinedScopes = null;
                    Properties.Settings.Default.Save();
                    startupLog.AppendLine("CustomDefinedScopes setting was saved.");
                }
                else if (command.ToLower() == ("/EnableSystemLogging").ToLower())
                {
                    // Turn on SystemLogging flag later.
                    systemLogging = true;
                }
            }

            if (systemLogging)
            {
                // Turn on SystemLogging flag.
                startupLog.AppendLine("Enable SystemLogging.");
                Properties.Settings.Default.SystemLogging = true;
                Properties.Settings.Default.Save();
            }

            // Set default log folder path.
            startupLog.AppendLine("Current log folder : " + Properties.Settings.Default.LogFolderPath);
            if (!Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                startupLog.AppendLine("The log folder does not exist and will be reset.");
                Properties.Settings.Default.LogFolderPath = Util.DefaultApplicationPath;
                Properties.Settings.Default.Save();
                startupLog.AppendLine("New log folder : "+ Properties.Settings.Default.LogFolderPath);
                startupLog.AppendLine("Setting was saved.");
            }

            // Write startup log.
            Util.WriteSystemLog("Office365APIEditor startup log", startupLog.ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create the MyApplicationContext, that derives from ApplicationContext,
            // that manages when the application should exit.

            MyApplicationContext context = new MyApplicationContext();

            // Start checking the latest version, and download the latest installer if it is available
            StartLatestVersionCheck();

            // Run the application with the specific context. It will exit when
            // all forms are closed.
            try
            {
                Application.Run(context);
            }
            catch (Exception ex)
            {
                // Write error log.
                try
                {
                    string filePath = Path.Combine(Util.DefaultApplicationPath, "Error.txt");

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(
                            "Date :" + DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss") + Environment.NewLine +
                            "Message :" + ex.Message + Environment.NewLine +
                            "StackTrace :" + ex.StackTrace + Environment.NewLine +
                            "-----------------------------------------------------------------------------" + Environment.NewLine
                            );
                    }
                }
                catch
                {
                }

                if (ex.InnerException == null)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.InnerException.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            finally
            {
                Util.WriteSystemLog("Office365APIEditor Closing Log", "Exit. Code 10");
            }
        }

        private static void UpgradeLastApps(StringBuilder startupLog)
        {
            // Migrate individual last app settings to the AccessTokenWizardApps setting.

            string migratedString = "THIS SETTING IS MIGRATED";

            if (Properties.Settings.Default.AccessTokenWizardApps == null)
            {
                Properties.Settings.Default.AccessTokenWizardApps = new Settings.AccessTokenWizardAppContainer();
            }

            if (Properties.Settings.Default.AccessTokenWizardApps.LastApps == null)
            {
                Properties.Settings.Default.AccessTokenWizardApps.LastApps = new Settings.AccessTokenWizardAppSettingCollection();
            }

            if (Properties.Settings.Default.AccessTokenWizardApps.SavedApps == null)
            {
                Properties.Settings.Default.AccessTokenWizardApps.SavedApps = new Settings.AccessTokenWizardAppSettingCollection();
            }

            // Upgrade LastV1EndpointWebApp
            if (Properties.Settings.Default.LastWebAppClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV1EndpointWebApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV1EndpointWebApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV1EndpointWebApp.ToString(),
                        ApplicationId = Properties.Settings.Default.LastWebAppClientID,
                        RedirectUri = Properties.Settings.Default.LastWebAppRedirectURL,
                        Resource = Resources.Graph,
                        ClientSecret = Properties.Settings.Default.LastWebAppClientSecret
                    });

                Properties.Settings.Default.LastWebAppClientID = migratedString;
            }

            // Upgrade LastV1EndpointNativeApp
            if (Properties.Settings.Default.LastNativeAppClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV1EndpointNativeApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV1EndpointNativeApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV1EndpointNativeApp.ToString(),
                        TenantName = Properties.Settings.Default.LastNativeAppTenantName,
                        ApplicationId = Properties.Settings.Default.LastNativeAppClientID,
                        RedirectUri = Properties.Settings.Default.LastNativeAppRedirectURL,
                        Resource = Resources.Graph
                    });

                Properties.Settings.Default.LastNativeAppClientID = migratedString;
            }

            // Upgrade LastV1EndpointAppOnlyByCertApp
            if (Properties.Settings.Default.LastWebAppAppOnlyClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV1EndpointAppOnlyByCertApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV1EndpointAppOnlyByCertApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV1EndpointAppOnlyByCertApp.ToString(),
                        TenantName = Properties.Settings.Default.LastWebAppAppOnlyTenantName,
                        ApplicationId = Properties.Settings.Default.LastWebAppAppOnlyClientID,
                        CertificatePath = Properties.Settings.Default.LastWebAppAppOnlyCertPath,
                        CertificatePassword = Properties.Settings.Default.LastWebAppAppOnlyCertPass,
                        Resource = Resources.Graph
                    });

                Properties.Settings.Default.LastWebAppAppOnlyClientID = migratedString;
            }

            // Upgrade LastV1EndpointAppOnlyByKeyApp
            if (Properties.Settings.Default.LastWebAppAppOnlyByKeyClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV1EndpointAppOnlyByKeyApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV1EndpointAppOnlyByKeyApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV1EndpointAppOnlyByKeyApp.ToString(),
                        TenantName = Properties.Settings.Default.LastWebAppAppOnlyByKeyTenantName,
                        ApplicationId = Properties.Settings.Default.LastWebAppAppOnlyByKeyClientID,
                        Resource = Resources.Graph,
                        ClientSecret = Properties.Settings.Default.LastWebAppAppOnlyByKeyClientSecret
                    });

                Properties.Settings.Default.LastWebAppAppOnlyByKeyClientID = migratedString;
            }

            // Upgrade LastV2EndpointWebApp
            if (Properties.Settings.Default.LastV2WebAppClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV2EndpointWebApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV2EndpointWebApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV2EndpointWebApp.ToString(),
                        TenantName = Properties.Settings.Default.LastV2WebAppTenantName,
                        ApplicationId = Properties.Settings.Default.LastV2WebAppClientID,
                        RedirectUri = Properties.Settings.Default.LastV2WebAppRedirectUri,
                        Scopes = Properties.Settings.Default.LastV2WebAppScopes,
                        ClientSecret = Properties.Settings.Default.LastV2WebAppClientSecret
                    });

                Properties.Settings.Default.LastV2WebAppClientID = migratedString;
            }

            // Upgrade LastV2EndpointNativeApp
            if (Properties.Settings.Default.LastV2MobileAppClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV2EndpointNativeApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV2EndpointNativeApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV2EndpointNativeApp.ToString(),
                        TenantName = Properties.Settings.Default.LastV2MobileAppTenantName,
                        ApplicationId = Properties.Settings.Default.LastV2MobileAppClientID,
                        RedirectUri = Properties.Settings.Default.LastV2MobileAppRedirectUri,
                        Scopes = Properties.Settings.Default.LastV2MobileAppScopes
                    });

                Properties.Settings.Default.LastV2MobileAppClientID = migratedString;
            }

            // Upgrade LastV2EndpointAppOnlyByCertApp
            if (Properties.Settings.Default.LastV2WebAppAppOnlyByCertClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV2EndpointAppOnlyByCertApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV2EndpointAppOnlyByCertApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV2EndpointAppOnlyByCertApp.ToString(),
                        TenantName = Properties.Settings.Default.LastV2WebAppAppOnlyByCertTenantName,
                        ApplicationId = Properties.Settings.Default.LastV2WebAppAppOnlyByCertClientID,
                        CertificatePath = Properties.Settings.Default.LastV2WebAppAppOnlyByCertCertPath,
                        CertificatePassword = Properties.Settings.Default.LastV2WebAppAppOnlyByCertCertPass,
                        Resource = Resources.Graph
                    });

                Properties.Settings.Default.LastV2WebAppAppOnlyByCertClientID = migratedString;
            }

            // Upgrade LastV2EndpointAppOnlyByKeyApp
            if (Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastV2EndpointAppOnlyByKeyApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastV2EndpointAppOnlyByKeyApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastV2EndpointAppOnlyByKeyApp.ToString(),
                        TenantName = Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphTenantName,
                        ApplicationId = Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientID,
                        Resource = Resources.Graph,
                        ClientSecret = Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientSecret
                    });

                Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientID = migratedString;
            }

            // Upgrade LastSharePointOnlineAppOnlyApp
            if (Properties.Settings.Default.LastSpoAppOnlyByKeyClientID != migratedString)
            {
                startupLog.AppendLine("Upgrade LastSharePointOnlineAppOnlyApp");

                Properties.Settings.Default.AccessTokenWizardApps.LastApps.Update(Settings.AppSettingType.LastSharePointOnlineAppOnlyApp,
                    new Settings.AccessTokenWizardAppSetting
                    {
                        DisplayName = Settings.AppSettingType.LastSharePointOnlineAppOnlyApp.ToString(),
                        TenantName = Properties.Settings.Default.LastSpoAppOnlyByKeyTenantName,
                        ApplicationId = Properties.Settings.Default.LastSpoAppOnlyByKeyClientID,
                        ClientSecret = Properties.Settings.Default.LastSpoAppOnlyByKeyClientSecret
                    });

                Properties.Settings.Default.LastSpoAppOnlyByKeyClientID = migratedString;
            }
        }

        private static void StartLatestVersionCheck()
        {
            // Get the build number of the latest release using the other thread
            // and download the latest installer if it is available

            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest versionRequest = WebRequest.CreateHttp(Util.LatestVersionUri);
            versionRequest.Method = "GET";
            versionRequest.ContentType = "text/plain";

            try
            {
                IAsyncResult r = versionRequest.BeginGetResponse(new AsyncCallback(VersionCheckResponseCallback), versionRequest);
            }
            catch
            {
            }
        }

        private static void VersionCheckResponseCallback(IAsyncResult ar)
        {
            try
            {
                HttpWebRequest versionRequest = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse versionResponse = (HttpWebResponse)versionRequest.EndGetResponse(ar);

                string latestVersionString = "";

                using (Stream stream = versionResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        latestVersionString = streamReader.ReadToEnd().Trim();
                    }
                }

                Version.TryParse(latestVersionString, out Version latestVersion);

                if (latestVersion != null && latestVersion.CompareTo(Version.Parse(Application.ProductVersion)) > 0)
                {
                    // Newer version is available

                    if (Util.IsMsiDeployed)
                    {
                        string newerMsiDirectory = Path.Combine(Path.GetTempPath(), "Office365APIEditor");
                        if (!Directory.Exists(newerMsiDirectory))
                        {
                            Directory.CreateDirectory(newerMsiDirectory);
                        }

                        string newerMsiPath = Path.Combine(newerMsiDirectory, "Setup.msi");

                        WebClient wc = new WebClient();
                        wc.DownloadFile(Util.LatestMsiUri, newerMsiPath);

                        Properties.Settings.Default.NewerInstallerPath = newerMsiPath;
                    }
                    else
                    {
                        Properties.Settings.Default.NewerInstallerPath = Util.LatestZipUri;
                    }
                }
                else
                {
                    Properties.Settings.Default.NewerInstallerPath = "";
                }
            }
            catch(Exception)
            {
                Properties.Settings.Default.NewerInstallerPath = "";
            }
            finally
            {
                Properties.Settings.Default.Save();
            }
        }
    }
}
