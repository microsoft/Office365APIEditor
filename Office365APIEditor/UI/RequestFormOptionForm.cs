// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.Settings;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class RequestFormOptionForm : Form
    {
        readonly string defaultNewAppDisplayName = "NewAppSetting";

        BindingList<AccessTokenWizardAppSetting> savedApps = new BindingList<AccessTokenWizardAppSetting>();

        public RequestFormOptionForm()
        {
            InitializeComponent();
        }

        private void RequestFormOptionForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            // Load saved settings.

            //
            // Load General tab.
            //

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

            //
            // Load Encode tab.
            //

            checkBox_ReplacePlusSignInTheRequestURL.Checked = Properties.Settings.Default.ReplacePlusSignInTheRequestURL;
            checkBox_ReplaceSharpSignInTheRequestURL.Checked = Properties.Settings.Default.ReplaceSharpSignInTheRequestURL;

            //
            // Load App Library tab.
            //

            foreach (string item in Util.ResourceNames)
            {
                comboBox_SavedAppResource.Items.Add(item);
            }

            if (Properties.Settings.Default.AccessTokenWizardApps.SavedApps != null)
            {
                foreach (var item in Properties.Settings.Default.AccessTokenWizardApps.SavedApps)
                {
                    savedApps.Add(item);
                }
            }

            listBox_SavedApps.DataSource = savedApps;
            listBox_SavedApps.DisplayMember = "DisplayName";
            listBox_SavedApps.ValueMember = "DisplayName";

            if (listBox_SavedApps.Items.Count > 0)
            {
                listBox_SavedApps.SelectedIndex = 0;
            }
            else
            {
                listBox_SavedApps.SelectedIndex = -1;

                button_RemoveApp.Enabled = false;

                DisableAppDetails();
            }

            // Select the first textbox.
            textBox_LogFolderPath.Select();
        }

        private void DisableAppDetails()
        {
            textBox_SavedAppDisplayName.Enabled = false;
            textBox_SavedAppApplicationId.Enabled = false;
            textBox_SavedAppTenantName.Enabled = false;
            textBox_SavedAppRedirectUri.Enabled = false;
            textBox_SavedAppClientSecret.Enabled = false;
            textBox_SavedAppScopes.Enabled = false;
            button_SavedAppScopeEditor.Enabled = false;
            comboBox_SavedAppResource.Enabled = false;
            textBox_SavedAppCertPath.Enabled = false;
            button_SavedAppSelectCert.Enabled = false;
            textBox_SavedAppCertPass.Enabled = false;
            textBox_SavedAppNote.Enabled = false;
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
            // We do not check values of saved apps, because it will be done when using the apps.

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

                Properties.Settings.Default.AccessTokenWizardApps.SavedApps.Clear();

                foreach (var item in savedApps)
                {
                    Properties.Settings.Default.AccessTokenWizardApps.SavedApps.AddForce(item);
                }

                Properties.Settings.Default.Save();

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ListBox_SavedApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count == 0)
            {
                ResetAppDetails();
            }
            else
            {
                if (listBox_SavedApps.SelectedIndex == -1 || listBox_SavedApps.SelectedItem == null)
                {
                    // Sometihg is wrong.
                    return;
                }
                else if (!(listBox_SavedApps.SelectedItem is AccessTokenWizardAppSetting setting))
                {
                    // Sometihg is wrong.
                    return;
                }
                else if (savedApps[listBox_SavedApps.SelectedIndex].DisplayName != setting.DisplayName)
                {
                    // Sometihg is wrong.
                    return;
                }

                ShowSelectedApp();
            }
        }

        private void ShowSelectedApp()
        {
            var selectedApp = (AccessTokenWizardAppSetting)listBox_SavedApps.SelectedItem;

            if (selectedApp == null)
            {
                MessageBox.Show("Failed to load the app setting.");
                ResetAppDetails();
                return;
            }

            textBox_SavedAppDisplayName.Text = selectedApp.DisplayName;
            textBox_SavedAppApplicationId.Text = selectedApp.ApplicationId;
            textBox_SavedAppTenantName.Text = selectedApp.TenantName;
            textBox_SavedAppRedirectUri.Text = selectedApp.RedirectUri;
            textBox_SavedAppClientSecret.Text = selectedApp.ClientSecret;
            textBox_SavedAppScopes.Text = selectedApp.Scopes;
            comboBox_SavedAppResource.SelectedIndex = selectedApp.Resource == Resources.None ? 1 : (int)selectedApp.Resource;
            textBox_SavedAppCertPath.Text = selectedApp.CertificatePath;
            textBox_SavedAppCertPass.Text = selectedApp.CertificatePassword;
            textBox_SavedAppNote.Text = selectedApp.Note;
        }

        private void ResetAppDetails()
        {
            textBox_SavedAppDisplayName.Text = "";
            textBox_SavedAppApplicationId.Text = "";
            textBox_SavedAppTenantName.Text = "";
            textBox_SavedAppRedirectUri.Text = "";
            textBox_SavedAppClientSecret.Text = "";
            textBox_SavedAppScopes.Text = "";
            comboBox_SavedAppResource.SelectedIndex = -1;
            textBox_SavedAppCertPath.Text = "";
            textBox_SavedAppCertPass.Text = "";
            textBox_SavedAppNote.Text = "";
        }

        private AccessTokenWizardAppSetting FindApp(string displayName)
        {
            // Search apps by DisplayName and return the first one.

            foreach (var app in savedApps)
            {
                if (app.DisplayName == displayName)
                {
                    return app;
                }
            }

            return null;
        }

        private void Button_AddApp_Click(object sender, EventArgs e)
        {
            // Create new app setting and add it to savedApps and UI.

            var tempApp = new AccessTokenWizardAppSetting() { DisplayName = FindNewAppDisplayName() };

            savedApps.Add(tempApp);
            int index = listBox_SavedApps.Items.Count - 1;
            listBox_SavedApps.SelectedIndex = index;

            if (index == 0)
            {
                button_RemoveApp.Enabled = true;

                textBox_SavedAppDisplayName.Enabled = true;
                textBox_SavedAppApplicationId.Enabled = true;
                textBox_SavedAppTenantName.Enabled = true;
                textBox_SavedAppRedirectUri.Enabled = true;
                textBox_SavedAppClientSecret.Enabled = true;
                textBox_SavedAppScopes.Enabled = true;
                button_SavedAppScopeEditor.Enabled = true;
                comboBox_SavedAppResource.Enabled = true;
                textBox_SavedAppCertPath.Enabled = true;
                button_SavedAppSelectCert.Enabled = true;
                textBox_SavedAppCertPass.Enabled = true;
                textBox_SavedAppNote.Enabled = true;

                // Force raise ListBox_SavedApps_SelectedIndexChanged event, because listBox_SavedApps.SelectedIndex has not been changed in this case.
                ShowSelectedApp();
            }
        }

        private string FindNewAppDisplayName()
        {
            for (int i = 1; i < 100; i++)
            {
                string tempName = defaultNewAppDisplayName + i.ToString();
                
                if (FindApp(tempName) == null)
                {
                    return tempName;
                }
            }

            return (new Guid()).ToString();
        }

        private void Button_RemoveApp_Click(object sender, EventArgs e)
        {
            if (listBox_SavedApps.SelectedItem == null)
            {
                button_RemoveApp.Enabled = false;
            }
            else
            {
                savedApps.RemoveAt(listBox_SavedApps.SelectedIndex);

                button_RemoveApp.Enabled = (savedApps.Count != 0);

                if (savedApps.Count == 0)
                {
                    ResetAppDetails();
                    DisableAppDetails();
                }
                else
                {
                    // Force raise ListBox_SavedApps_SelectedIndexChanged event, because listBox_SavedApps.SelectedIndex has not been changed in this case.
                    ShowSelectedApp();
                }
            }
        }

        private void TextBox_SavedAppDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                string newDisplayName = ((TextBox)sender).Text;

                if (newDisplayName == "")
                {
                    MessageBox.Show("The display name of the app cannot be blank.", "Office365APIEditor");
                    ((TextBox)sender).Text = savedApps[listBox_SavedApps.SelectedIndex].DisplayName;
                }
                else
                {
                    savedApps[listBox_SavedApps.SelectedIndex].DisplayName = newDisplayName;

                    // Update the listBox.
                    listBox_SavedApps.DataSource = null;
                    listBox_SavedApps.DataSource = savedApps;
                    listBox_SavedApps.DisplayMember = "DisplayName";
                    listBox_SavedApps.ValueMember = "DisplayName";
                }
            }
        }

        private void TextBox_SavedAppApplicationId_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].ApplicationId = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppTenantName_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].TenantName = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppRedirectUri_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].RedirectUri = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppClientSecret_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].ClientSecret = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppScopes_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].Scopes = ((TextBox)sender).Text;
            }
        }

        private void ComboBox_SavedAppResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].Resource = Util.ConvertResourceNameToResourceEnum(((ComboBox)sender).SelectedItem.ToString());
            }
        }

        private void TextBox_SavedAppCertPath_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].CertificatePath = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppCertPass_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].CertificatePassword = ((TextBox)sender).Text;
            }
        }

        private void TextBox_SavedAppNote_TextChanged(object sender, EventArgs e)
        {
            if (listBox_SavedApps.Items.Count != 0 && listBox_SavedApps.SelectedIndex != -1)
            {
                savedApps[listBox_SavedApps.SelectedIndex].Note = ((TextBox)sender).Text;
            }
        }

        private void Button_SavedAppScopeEditor_Click(object sender, EventArgs e)
        {
            string[] selectedScopes;

            if (textBox_SavedAppScopes.Text == "")
            {
                selectedScopes = null;
            }
            else
            {
                selectedScopes = textBox_SavedAppScopes.Text.Split(' ');
            }

            ScopeEditorForm scopeEditor = new ScopeEditorForm(selectedScopes);

            if (scopeEditor.ShowDialog(out string scopes) == DialogResult.OK)
            {
                textBox_SavedAppScopes.Text = scopes;
            }
        }

        private void Button_SavedAppSelectCert_Click(object sender, EventArgs e)
        {
            if (openFileDialog_SavedAppPFX.ShowDialog() == DialogResult.OK)
            {
                textBox_SavedAppCertPath.Text = openFileDialog_SavedAppPFX.FileName;
            }
        }
    }
}