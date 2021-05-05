// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.Settings;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class SavedAppSelectorForm : Form
    {
        BindingList<AccessTokenWizardAppSetting> savedApps = new BindingList<AccessTokenWizardAppSetting>();

        public SavedAppSelectorForm()
        {
            InitializeComponent();
        }

        private void SavedAppSelectorForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            // Load saved apps.

            if (Properties.Settings.Default.AccessTokenWizardApps.SavedApps != null)
            {
                foreach (var item in Properties.Settings.Default.AccessTokenWizardApps.SavedApps)
                {
                    savedApps.Add(item);
                }

                if (savedApps.Count == 0)
                {
                    MessageBox.Show("No app is saved. You can save your app using the option window of Editor.");
                    DialogResult = DialogResult.Abort;
                    Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show("No app is saved.");
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            listBox_SavedApps.DataSource = savedApps;
            listBox_SavedApps.DisplayMember = "DisplayName";
            listBox_SavedApps.ValueMember = "DisplayName";

            listBox_SavedApps.SelectedIndex = 0;

            DisableAppDetails();
        }

        public DialogResult ShowDialog(out AccessTokenWizardAppSetting AppSetting)
        {
            DialogResult dialogResult = ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                AppSetting = null;
            }
            else
            {
                AppSetting = savedApps[listBox_SavedApps.SelectedIndex];
            }

            return dialogResult;
        }

        private void DisableAppDetails()
        {
            textBox_SavedAppDisplayName.ReadOnly = true;
            textBox_SavedAppApplicationId.ReadOnly = true;
            textBox_SavedAppTenantName.ReadOnly = true;
            textBox_SavedAppRedirectUri.ReadOnly = true;
            textBox_SavedAppClientSecret.ReadOnly = true;
            textBox_SavedAppScopes.ReadOnly = true;
            textBox_SavedAppResource.ReadOnly = true;
            textBox_SavedAppCertPath.ReadOnly = true;
            textBox_SavedAppCertPass.ReadOnly = true;
            textBox_SavedAppNote.ReadOnly = true;
        }

        private void ListBox_SavedApps_SelectedIndexChanged(object sender, EventArgs e)
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
            textBox_SavedAppResource.Text = selectedApp.Resource == Resources.None ? "" : Util.ConvertResourceEnumToResourceName(selectedApp.Resource);
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
            textBox_SavedAppResource.Text = "";
            textBox_SavedAppCertPath.Text = "";
            textBox_SavedAppCertPass.Text = "";
            textBox_SavedAppNote.Text = "";
        }
    }
}