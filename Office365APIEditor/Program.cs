// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
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
            Environment.Exit(0);
        }

        [STAThread]
        static void Main(string[] args)
        {
            // Load previous settings.
            if (Properties.Settings.Default.NeedUpgrade == true)
            {
                try
                {
                    Properties.Settings.Default.Upgrade();
                }
                catch
                {
                }
                finally
                {
                    Properties.Settings.Default.NeedUpgrade = false;
                    Properties.Settings.Default.Save();
                }
            }

            // Check the command line switches.
            string[] switches = Environment.GetCommandLineArgs();
            foreach (string command in switches)
            {
                if (command.ToLower() == ("/NoSetting").ToLower())
                {
                    // Reset all settings.
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                }
                else if(command.ToLower() == ("/NoHistory").ToLower())
                {
                    // Remove Run History file.
                    if (File.Exists(Path.Combine(Application.StartupPath, "RunHistory.xml")))
                    {
                        try
                        {
                            File.Delete(Path.Combine(Application.StartupPath, "RunHistory.xml"));
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Office365APIEditor");
                        }
                    }
                }
            }

            // Set default log folder path.
            if (!Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                Properties.Settings.Default.LogFolderPath = Application.StartupPath;
                Properties.Settings.Default.Save();
            }

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
                if (ex.InnerException == null)
                {
                    MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.InnerException.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
                // Write error log.
                try
                {
                    string filePath = Path.Combine(Application.StartupPath, "Error.txt");

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
            }
        }

    }
}
