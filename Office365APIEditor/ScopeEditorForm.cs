// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Linq;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class ScopeEditorForm : Form
    {
        string[] initialCheckedScopes;
        bool changingAllStatus = false;

        public ScopeEditorForm(string[] CheckedScopes)
        {
            InitializeComponent();

            // When this form is showen, we check scopes which identified by CheckedScopes.
            // If CheckedScopes is null, we use DefaultScopes.

            if (CheckedScopes == null)
            {
                initialCheckedScopes = new string[0];
            }
            else
            {
                initialCheckedScopes = CheckedScopes;
            }
        }

        private void ScopeEditorForm_Load(object sender, EventArgs e)
        {
            changingAllStatus = true;

            foreach (string scope in Properties.Settings.Default.PredefinedScopes)
            {
                checkedListBox_Scopes.Items.Add(scope);

                if (initialCheckedScopes.Contains(scope))
                {
                    checkedListBox_Scopes.SetItemChecked(checkedListBox_Scopes.Items.Count - 1, true);
                    textBox_ScopePreview.Text += scope + " ";
                }
            }

            if (Properties.Settings.Default.CustomDefinedScopes != null && Properties.Settings.Default.CustomDefinedScopes.Count >= 1)
                foreach (string scope in Properties.Settings.Default.CustomDefinedScopes)
                {
                    checkedListBox_Scopes.Items.Add(scope);

                    if (initialCheckedScopes.Contains(scope))
                    {
                        checkedListBox_Scopes.SetItemChecked(checkedListBox_Scopes.Items.Count - 1, true);
                        textBox_ScopePreview.Text += scope + " ";
                    }
                }

            textBox_ScopePreview.Text = textBox_ScopePreview.Text.TrimEnd(' ');

            changingAllStatus = false;

            checkedListBox_Scopes.SelectedIndex = 0;
        }

        public DialogResult ShowDialog(out string Scopes)
        {
            DialogResult result = this.ShowDialog();

            Scopes = textBox_ScopePreview.Text;

            return result;
        }

        private void textBox_NewScope_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddScope();
            }
        }

        private void button_AddScope_Click(object sender, EventArgs e)
        {
            AddScope();
        }

        private void AddScope()
        {
            checkedListBox_Scopes.Items.Add(textBox_NewScope.Text);

            int newScopeIndex = checkedListBox_Scopes.Items.Count - 1;

            checkedListBox_Scopes.SelectedIndex = newScopeIndex ;
            checkedListBox_Scopes.SetItemChecked(newScopeIndex, true);
        }

        private void button_RemoveScope_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            string selectedScope = checkedListBox_Scopes.SelectedItem.ToString();

            if (IsPredefinedScope(selectedScope))
            {
                MessageBox.Show("You can't remove predefined scope.", "Office365APIEditor");
                return;
            }

            textBox_NewScope.Text = selectedScope;
            checkedListBox_Scopes.Items.Remove(checkedListBox_Scopes.SelectedItem);

            textBox_ScopePreview.Text = "";

            foreach (string scope in Properties.Settings.Default.DefaultScopes)
            {
                textBox_ScopePreview.Text += scope + " ";
            }

            textBox_ScopePreview.Text = textBox_ScopePreview.Text.TrimEnd(' ');
        }

        private void button_SelectAll_Click(object sender, EventArgs e)
        {
            selectAll(true);
        }

        private void button_DeselectAll_Click(object sender, EventArgs e)
        {
            selectAll(false);
        }
        
        private void button_SelectDefaultValues_Click(object sender, EventArgs e)
        {
            changingAllStatus = true;

            for (int i = 0; i < checkedListBox_Scopes.Items.Count; i++)
            {
                if (Properties.Settings.Default.DefaultScopes.Contains(checkedListBox_Scopes.Items[i].ToString()))
                {
                    checkedListBox_Scopes.SetItemChecked(i, true);
                }
                else
                {
                    checkedListBox_Scopes.SetItemChecked(i, false);
                }
            }

            foreach (string scope in Properties.Settings.Default.DefaultScopes)
            {
                textBox_ScopePreview.Text += scope + " ";
            }

            textBox_ScopePreview.Text = textBox_ScopePreview.Text.TrimEnd(' ');

            changingAllStatus = false;
        }

        private bool IsPredefinedScope(string Scope)
        {
            bool result = false;

            foreach (string predefinedScope in Properties.Settings.Default.PredefinedScopes)
            {
                if (Scope == predefinedScope)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void checkedListBox_Scopes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (changingAllStatus)
            {
                return;
            }

            // ItemCheck event will fire before the item changes.
            // So we have to use BeginInvoke as a workaround.

            textBox_ScopePreview.Text = "";

            BeginInvoke((MethodInvoker)delegate
            {
                foreach (var item in checkedListBox_Scopes.CheckedItems)
                {
                    textBox_ScopePreview.Text += item.ToString() + " ";
                }

                textBox_ScopePreview.Text = textBox_ScopePreview.Text.TrimEnd(' ');
            });
        }
        void selectAll(bool Selected)
        {
            changingAllStatus = true;

            for (int i = 0; i < checkedListBox_Scopes.Items.Count; i++)
            {
                checkedListBox_Scopes.SetItemChecked(i, Selected);
            }

            if (Selected)
            {
                foreach (var item in checkedListBox_Scopes.CheckedItems)
                {
                    textBox_ScopePreview.Text += item.ToString() + " ";
                }

                textBox_ScopePreview.Text = textBox_ScopePreview.Text.TrimEnd(' ');
            }
            else
            {
                textBox_ScopePreview.Text = "";
            }

            changingAllStatus = false;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            // Save custome defined scopes.

            System.Collections.Specialized.StringCollection customeDefinedScopes = new System.Collections.Specialized.StringCollection();

            foreach (var scope in checkedListBox_Scopes.Items)
            {
                if (!IsPredefinedScope(scope.ToString()))
                {
                    customeDefinedScopes.Add(scope.ToString());
                }
            }

            Properties.Settings.Default.CustomDefinedScopes = customeDefinedScopes;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}
