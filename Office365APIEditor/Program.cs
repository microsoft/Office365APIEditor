// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
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
            Util.WriteCustomLog("Office365APIEditor Closing Log", "Exit. Code 20");
            Environment.Exit(0);
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Save the contents of the log temporarily until the initialization of the setting is completed.
            StringBuilder startupLog = new StringBuilder();
            startupLog.AppendLine("Start.");

            // Load previous settings.
            if (Properties.Settings.Default.NeedUpgrade == true)
            {
                startupLog.AppendLine("Setting upgrade is required.");

                try
                {
                    Properties.Settings.Default.Upgrade();
                    startupLog.AppendLine("Setting was upgraded.");
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

            // Check the command line switches.
            startupLog.AppendLine("CommandLine : " + Environment.CommandLine);
            string[] switches = Environment.GetCommandLineArgs();

            foreach (string command in switches)
            {
                if (command.ToLower() == ("/NoSetting").ToLower())
                {
                    // Reset all settings.
                    startupLog.AppendLine("Setting will be reset.");
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                    startupLog.AppendLine("Setting was saved.");
                }
                else if(command.ToLower() == ("/NoHistory").ToLower())
                {
                    // Remove Run History file.
                    startupLog.AppendLine("The history file will be delete.");
                    if (File.Exists(Path.Combine(Util.DefaultApplicationPath, "RunHistory.xml")))
                    {
                        try
                        {
                            File.Delete(Path.Combine(Util.DefaultApplicationPath, "RunHistory.xml"));
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
                else if (command.ToLower() == ("/CustomDefinedScopes").ToLower())
                {
                    // Reset CustomDefinedScopes setting.
                    startupLog.AppendLine("CustomDefinedScopes will be reset.");
                    Properties.Settings.Default.CustomDefinedScopes = null;
                    Properties.Settings.Default.Save();
                    startupLog.AppendLine("CustomDefinedScopes setting was saved.");
                }
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
            Util.WriteCustomLog("Office365APIEditor startup log", startupLog.ToString());

            // Create the MyApplicationContext, that derives from ApplicationContext,
            // that manages when the application should exit.

            MyApplicationContext context = new MyApplicationContext();

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
                Util.WriteCustomLog("Office365APIEditor Closing Log", "Exit. Code 10");
            }
        }
    }
}
