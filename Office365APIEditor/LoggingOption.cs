// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class LoggingOption : Form
    {
        public LoggingOption()
        {
            InitializeComponent();
        }
        
        private void LoggingOption_Load(object sender, EventArgs e)
        {
            // Load saved settings.

            if (System.IO.Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                textBox_LogFolderPath.Text = Properties.Settings.Default.LogFolderPath;
            }
            else
            {
                textBox_LogFolderPath.Text = Application.StartupPath;
            }
            

            if (Properties.Settings.Default.LogFileStyle == "Static")
            {
                radioButton_Static.Checked = true;
            }
            else if (Properties.Settings.Default.LogFileStyle == "DateTime")
            {
                radioButton_DateTime.Checked = true;
            }
            else
            {
                radioButton_Static.Checked = true;
            }
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(textBox_LogFolderPath.Text))
            {
                folderBrowserDialog1.SelectedPath = textBox_LogFolderPath.Text;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = Application.StartupPath;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_LogFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(textBox_LogFolderPath.Text))
            {
                // Save settings.
                Properties.Settings.Default.LogFolderPath = textBox_LogFolderPath.Text;
                if (radioButton_Static.Checked)
                {
                    Properties.Settings.Default.LogFileStyle = "Static";
                }
                else
                {
                    Properties.Settings.Default.LogFileStyle = "DateTime";
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The specified log folder path is not existing.", "Office365APIEditor");
            }
        }
    }
}
