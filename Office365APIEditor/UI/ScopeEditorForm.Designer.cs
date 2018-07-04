namespace Office365APIEditor
{
    partial class ScopeEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeEditorForm));
            this.checkedListBox_Scopes = new System.Windows.Forms.CheckedListBox();
            this.textBox_NewScope = new System.Windows.Forms.TextBox();
            this.button_AddScope = new System.Windows.Forms.Button();
            this.button_RemoveScope = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ScopePreview = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_SelectAll = new System.Windows.Forms.Button();
            this.button_DeselectAll = new System.Windows.Forms.Button();
            this.button_SelectDefaultValues = new System.Windows.Forms.Button();
            this.button_RemoveCustomValues = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox_Scopes
            // 
            this.checkedListBox_Scopes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox_Scopes.CheckOnClick = true;
            this.checkedListBox_Scopes.FormattingEnabled = true;
            this.checkedListBox_Scopes.Location = new System.Drawing.Point(12, 38);
            this.checkedListBox_Scopes.Name = "checkedListBox_Scopes";
            this.checkedListBox_Scopes.ScrollAlwaysVisible = true;
            this.checkedListBox_Scopes.Size = new System.Drawing.Size(457, 274);
            this.checkedListBox_Scopes.TabIndex = 2;
            this.checkedListBox_Scopes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_Scopes_ItemCheck);
            // 
            // textBox_NewScope
            // 
            this.textBox_NewScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_NewScope.Location = new System.Drawing.Point(12, 12);
            this.textBox_NewScope.Name = "textBox_NewScope";
            this.textBox_NewScope.Size = new System.Drawing.Size(457, 20);
            this.textBox_NewScope.TabIndex = 0;
            this.textBox_NewScope.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_NewScope_KeyDown);
            // 
            // button_AddScope
            // 
            this.button_AddScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AddScope.Location = new System.Drawing.Point(475, 10);
            this.button_AddScope.Name = "button_AddScope";
            this.button_AddScope.Size = new System.Drawing.Size(138, 23);
            this.button_AddScope.TabIndex = 1;
            this.button_AddScope.Text = "Add";
            this.button_AddScope.UseVisualStyleBackColor = true;
            this.button_AddScope.Click += new System.EventHandler(this.button_AddScope_Click);
            // 
            // button_RemoveScope
            // 
            this.button_RemoveScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RemoveScope.Location = new System.Drawing.Point(475, 39);
            this.button_RemoveScope.Name = "button_RemoveScope";
            this.button_RemoveScope.Size = new System.Drawing.Size(138, 23);
            this.button_RemoveScope.TabIndex = 3;
            this.button_RemoveScope.Text = "Remove";
            this.button_RemoveScope.UseVisualStyleBackColor = true;
            this.button_RemoveScope.Click += new System.EventHandler(this.button_RemoveScope_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Preview : ";
            // 
            // textBox_ScopePreview
            // 
            this.textBox_ScopePreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ScopePreview.Location = new System.Drawing.Point(12, 343);
            this.textBox_ScopePreview.Multiline = true;
            this.textBox_ScopePreview.Name = "textBox_ScopePreview";
            this.textBox_ScopePreview.ReadOnly = true;
            this.textBox_ScopePreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ScopePreview.Size = new System.Drawing.Size(601, 69);
            this.textBox_ScopePreview.TabIndex = 8;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(457, 418);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(538, 418);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 10;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_SelectAll
            // 
            this.button_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SelectAll.Location = new System.Drawing.Point(475, 97);
            this.button_SelectAll.Name = "button_SelectAll";
            this.button_SelectAll.Size = new System.Drawing.Size(138, 23);
            this.button_SelectAll.TabIndex = 5;
            this.button_SelectAll.Text = "Select all";
            this.button_SelectAll.UseVisualStyleBackColor = true;
            this.button_SelectAll.Click += new System.EventHandler(this.button_SelectAll_Click);
            // 
            // button_DeselectAll
            // 
            this.button_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DeselectAll.Location = new System.Drawing.Point(475, 126);
            this.button_DeselectAll.Name = "button_DeselectAll";
            this.button_DeselectAll.Size = new System.Drawing.Size(138, 23);
            this.button_DeselectAll.TabIndex = 6;
            this.button_DeselectAll.Text = "Deselect all";
            this.button_DeselectAll.UseVisualStyleBackColor = true;
            this.button_DeselectAll.Click += new System.EventHandler(this.button_DeselectAll_Click);
            // 
            // button_SelectDefaultValues
            // 
            this.button_SelectDefaultValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SelectDefaultValues.Location = new System.Drawing.Point(475, 155);
            this.button_SelectDefaultValues.Name = "button_SelectDefaultValues";
            this.button_SelectDefaultValues.Size = new System.Drawing.Size(138, 23);
            this.button_SelectDefaultValues.TabIndex = 7;
            this.button_SelectDefaultValues.Text = "Select default values";
            this.button_SelectDefaultValues.UseVisualStyleBackColor = true;
            this.button_SelectDefaultValues.Click += new System.EventHandler(this.button_SelectDefaultValues_Click);
            // 
            // button_RemoveCustomValues
            // 
            this.button_RemoveCustomValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RemoveCustomValues.Location = new System.Drawing.Point(475, 68);
            this.button_RemoveCustomValues.Name = "button_RemoveCustomValues";
            this.button_RemoveCustomValues.Size = new System.Drawing.Size(138, 23);
            this.button_RemoveCustomValues.TabIndex = 4;
            this.button_RemoveCustomValues.Text = "Remove custom values";
            this.button_RemoveCustomValues.UseVisualStyleBackColor = true;
            this.button_RemoveCustomValues.Click += new System.EventHandler(this.Button_RemoveCustomValues_Click);
            // 
            // ScopeEditorForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(625, 453);
            this.Controls.Add(this.button_RemoveCustomValues);
            this.Controls.Add(this.button_SelectDefaultValues);
            this.Controls.Add(this.button_DeselectAll);
            this.Controls.Add(this.button_SelectAll);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_ScopePreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_RemoveScope);
            this.Controls.Add(this.button_AddScope);
            this.Controls.Add(this.textBox_NewScope);
            this.Controls.Add(this.checkedListBox_Scopes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(641, 492);
            this.Name = "ScopeEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scope Editor";
            this.Load += new System.EventHandler(this.ScopeEditorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_Scopes;
        private System.Windows.Forms.TextBox textBox_NewScope;
        private System.Windows.Forms.Button button_AddScope;
        private System.Windows.Forms.Button button_RemoveScope;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ScopePreview;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_SelectAll;
        private System.Windows.Forms.Button button_DeselectAll;
        private System.Windows.Forms.Button button_SelectDefaultValues;
        private System.Windows.Forms.Button button_RemoveCustomValues;
    }
}