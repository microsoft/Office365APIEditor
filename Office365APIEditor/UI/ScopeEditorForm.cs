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
            Icon = Properties.Resources.DefaultIcon;

            PrepareScopes(false);
        }

        private void PrepareScopes(bool DefaultScopesOnly)
        {
            changingAllStatus = true;

            // To sort scopes alphabetically, use Sorted prop.
            checkedListBox_Scopes.Sorted = true;

            foreach (string scope in Properties.Settings.Default.PredefinedScopes)
            {
                if (initialCheckedScopes.Contains(scope))
                {
                    checkedListBox_Scopes.Items.Add(scope, true);
                    textBox_ScopePreview.Text += scope + " ";
                }
                else
                {
                    checkedListBox_Scopes.Items.Add(scope, false);
                }
            }

            if (DefaultScopesOnly == false && Properties.Settings.Default.CustomDefinedScopes != null && Properties.Settings.Default.CustomDefinedScopes.Count >= 1)
            { 
                foreach (string scope in Properties.Settings.Default.CustomDefinedScopes)
                {
                    if (initialCheckedScopes.Contains(scope))
                    {
                        checkedListBox_Scopes.Items.Add(scope, true);
                        // checkedListBox_Scopes.SetItemChecked(checkedListBox_Scopes.Items.Count - 1, true);
                        textBox_ScopePreview.Text += scope + " ";
                    }
                    else
                    {
                        checkedListBox_Scopes.Items.Add(scope, false);
                    }
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
            if (textBox_NewScope.Text == "")
            {
                MessageBox.Show("Enter the name of scope.", "Office365APIEditor");
                checkedListBox_Scopes.SelectedItem = textBox_NewScope.Text;

                return;
            }

            if (checkedListBox_Scopes.Items.Contains(textBox_NewScope.Text))
            {
                MessageBox.Show("The specified scope is already exist.", "Office365APIEditor");
                checkedListBox_Scopes.SelectedItem = textBox_NewScope.Text;

                return;
            }

            int newScopeIndex = checkedListBox_Scopes.Items.Add(textBox_NewScope.Text);

            checkedListBox_Scopes.SelectedIndex = newScopeIndex ;
            checkedListBox_Scopes.SetItemChecked(newScopeIndex, true);
        }

        private void button_RemoveScope_Click(object sender, EventArgs e)
        {
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

            textBox_ScopePreview.Text = "";

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

        private void Button_RemoveCustomValues_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to remove all custom scopes?", "Office365APIEditor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Once remove all scopes and reload only predefined scopes.

                initialCheckedScopes = textBox_ScopePreview.Text.Split(' ');

                changingAllStatus = true;

                checkedListBox_Scopes.Items.Clear();

                PrepareScopes(true);

                changingAllStatus = false;
            }
        }
    }
}