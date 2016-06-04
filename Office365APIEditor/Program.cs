// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    class MyApplicationContext : ApplicationContext
    {
        public MyApplicationContext()
        {
            string mode = "";

            ModeSelectionForm modeSelectionForm = new ModeSelectionForm();
            modeSelectionForm.ShowDialog(out mode);
            
            if (mode == "Editor")
            {
                RequestForm requestForm = new RequestForm();

                requestForm.FormClosed += new FormClosedEventHandler(OnFormClosed);
                requestForm.Show();
            }
            else if (mode == "MailboxViewer")
            {
                MailboxViewerForm mailboxViewerForm = new MailboxViewerForm();

                mailboxViewerForm.FormClosed += new FormClosedEventHandler(OnFormClosed);
                mailboxViewerForm.Show();
            }
            else
            {
                Environment.Exit(0);
            }
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
                MessageBox.Show(ex.InnerException.Message);
            }
        }

     }
}
