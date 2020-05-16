// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class RequestFormOptionForm : Form
    {
        public RequestFormOptionForm()
        {
            InitializeComponent();
        }

        private void RequestFormOptionForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            // Load saved settings.

            if (System.IO.Directory.Exists(Properties.Settings.Default.LogFolderPath))
            {
                textBox_LogFolderPath.Text = Properties.Settings.Default.LogFolderPath;
            }
            else
            {
                textBox_LogFolderPath.Text = Util.DefaultApplicationPath;
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

            checkBox_AllowAutoRedirect.Checked = Properties.Settings.Default.AllowAutoRedirect;

            comboBox_CustomUserAgentStyle.Items.Add("Use system default UserAgent");
            comboBox_CustomUserAgentStyle.Items.Add("Use custom UserAgent");
            //comboBox_CustomUserAgentStyle.Items.Add("Add custom UserAgent value as prefix");
            //comboBox_CustomUserAgentStyle.Items.Add("Add custom UserAgent value as suffix");
            
            if (Properties.Settings.Default.CustomUserAgentMode == 0)
            {
                comboBox_CustomUserAgentStyle.SelectedIndex = 0;
                textBox_CustomUserAgent.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CustomUserAgentMode >= comboBox_CustomUserAgentStyle.Items.Count)
                {
                    comboBox_CustomUserAgentStyle.SelectedIndex = 0;
                    textBox_CustomUserAgent.Enabled = false;
                }
                else
                {
                    comboBox_CustomUserAgentStyle.SelectedIndex = Properties.Settings.Default.CustomUserAgentMode;
                    textBox_CustomUserAgent.Enabled = true;
                }
            }

            textBox_CustomUserAgent.Text = Properties.Settings.Default.CustomUserAgent;

            checkBox_ReplacePlusSignInTheRequestURL.Checked = Properties.Settings.Default.ReplacePlusSignInTheRequestURL;
            checkBox_ReplaceSharpSignInTheRequestURL.Checked = Properties.Settings.Default.ReplaceSharpSignInTheRequestURL;

            textBox_LogFolderPath.Select();
        }

        private void button_LogFolderPathBrowse_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(textBox_LogFolderPath.Text))
            {
                folderBrowserDialog1.SelectedPath = textBox_LogFolderPath.Text;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = Util.DefaultApplicationPath;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_LogFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void comboBox_CustomUserAgentStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_CustomUserAgent.Enabled = (comboBox_CustomUserAgentStyle.SelectedIndex != 0);
        }

        private bool ValidateValues()
        {
            // Check all settings before closing this window.

            bool result = false;

            if (System.IO.Directory.Exists(textBox_LogFolderPath.Text))
            {
                if (textBox_CustomUserAgent.Enabled == true && string.IsNullOrEmpty(textBox_CustomUserAgent.Text))
                {
                    MessageBox.Show("Enter the UserAgnet.", "Office365APIEditor");
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                MessageBox.Show("The specified log folder path is not existing.", "Office365APIEditor");
            }

            return result;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (ValidateValues() == true)
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

                Properties.Settings.Default.AllowAutoRedirect = checkBox_AllowAutoRedirect.Checked;

                Properties.Settings.Default.CustomUserAgentMode = comboBox_CustomUserAgentStyle.SelectedIndex;
                if (comboBox_CustomUserAgentStyle.SelectedIndex != 0)
                {
                    Properties.Settings.Default.CustomUserAgent = textBox_CustomUserAgent.Text;
                }

                Properties.Settings.Default.ReplacePlusSignInTheRequestURL = checkBox_ReplacePlusSignInTheRequestURL.Checked;
                Properties.Settings.Default.ReplaceSharpSignInTheRequestURL = checkBox_ReplaceSharpSignInTheRequestURL.Checked;

                Properties.Settings.Default.Save();

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
