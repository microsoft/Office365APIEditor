
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class BuiltInAppSettingPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label39 = new System.Windows.Forms.Label();
            this.button_ScopeEditor = new System.Windows.Forms.Button();
            this.textBox_Scopes = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(15, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(124, 13);
            this.label39.TabIndex = 52;
            this.label39.Text = "Select scopes you need.";
            // 
            // button_ScopeEditor
            // 
            this.button_ScopeEditor.Location = new System.Drawing.Point(102, 54);
            this.button_ScopeEditor.Name = "button_ScopeEditor";
            this.button_ScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_ScopeEditor.TabIndex = 1;
            this.button_ScopeEditor.Text = "Scope editor...";
            this.button_ScopeEditor.UseVisualStyleBackColor = true;
            this.button_ScopeEditor.Click += new System.EventHandler(this.Button_ScopeEditor_Click);
            // 
            // textBox_Scopes
            // 
            this.textBox_Scopes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Scopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastBuiltInAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Scopes.Location = new System.Drawing.Point(102, 30);
            this.textBox_Scopes.Name = "textBox_Scopes";
            this.textBox_Scopes.Size = new System.Drawing.Size(284, 20);
            this.textBox_Scopes.TabIndex = 0;
            this.textBox_Scopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastBuiltInAppScopes;
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(3, 33);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(93, 13);
            this.label42.TabIndex = 51;
            this.label42.Text = "Scopes";
            this.label42.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BuiltInAppSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label39);
            this.Controls.Add(this.button_ScopeEditor);
            this.Controls.Add(this.textBox_Scopes);
            this.Controls.Add(this.label42);
            this.Name = "BuiltInAppSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_Scopes";
            this.Load += new System.EventHandler(this.BuiltInAppSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button button_ScopeEditor;
        private System.Windows.Forms.TextBox textBox_Scopes;
        private System.Windows.Forms.Label label42;
    }
}
