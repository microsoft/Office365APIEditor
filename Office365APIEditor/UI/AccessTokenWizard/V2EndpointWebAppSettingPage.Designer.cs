
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V2EndpointWebAppSettingPage
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
            this.linkLabel_Description = new System.Windows.Forms.LinkLabel();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox_TenantName = new System.Windows.Forms.TextBox();
            this.textBox_ClientSecret = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.button_ScopeEditor = new System.Windows.Forms.Button();
            this.textBox_Scopes = new System.Windows.Forms.TextBox();
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox_RedirectUri = new System.Windows.Forms.TextBox();
            this.checkBox_SaveAsNewApp = new System.Windows.Forms.CheckBox();
            this.button_LoadSavedApp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabel_Description
            // 
            this.linkLabel_Description.AutoSize = true;
            this.linkLabel_Description.Location = new System.Drawing.Point(15, 8);
            this.linkLabel_Description.Name = "linkLabel_Description";
            this.linkLabel_Description.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_Description.TabIndex = 200;
            this.linkLabel_Description.TabStop = true;
            this.linkLabel_Description.Text = "Fill out the form. Learn more.";
            this.linkLabel_Description.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Description_LinkClicked);
            // 
            // label49
            // 
            this.label49.Location = new System.Drawing.Point(3, 33);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(93, 13);
            this.label49.TabIndex = 66;
            this.label49.Text = "Tenant Name";
            this.label49.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TenantName
            // 
            this.textBox_TenantName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TenantName.Location = new System.Drawing.Point(102, 30);
            this.textBox_TenantName.Name = "textBox_TenantName";
            this.textBox_TenantName.Size = new System.Drawing.Size(284, 20);
            this.textBox_TenantName.TabIndex = 0;
            // 
            // textBox_ClientSecret
            // 
            this.textBox_ClientSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientSecret.Location = new System.Drawing.Point(102, 160);
            this.textBox_ClientSecret.Name = "textBox_ClientSecret";
            this.textBox_ClientSecret.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientSecret.TabIndex = 5;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(3, 163);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(93, 13);
            this.label26.TabIndex = 64;
            this.label26.Text = "Client secret";
            this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button_ScopeEditor
            // 
            this.button_ScopeEditor.Location = new System.Drawing.Point(102, 132);
            this.button_ScopeEditor.Name = "button_ScopeEditor";
            this.button_ScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_ScopeEditor.TabIndex = 4;
            this.button_ScopeEditor.Text = "Scope editor...";
            this.button_ScopeEditor.UseVisualStyleBackColor = true;
            this.button_ScopeEditor.Click += new System.EventHandler(this.Button_ScopeEditor_Click);
            // 
            // textBox_Scopes
            // 
            this.textBox_Scopes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Scopes.Location = new System.Drawing.Point(102, 108);
            this.textBox_Scopes.Name = "textBox_Scopes";
            this.textBox_Scopes.Size = new System.Drawing.Size(284, 20);
            this.textBox_Scopes.TabIndex = 3;
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientID.Location = new System.Drawing.Point(102, 56);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientID.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(3, 59);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 13);
            this.label29.TabIndex = 63;
            this.label29.Text = "Application ID";
            this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(3, 85);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(93, 13);
            this.label30.TabIndex = 62;
            this.label30.Text = "Redirect URI";
            this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(3, 111);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(93, 13);
            this.label31.TabIndex = 61;
            this.label31.Text = "Scopes";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_RedirectUri
            // 
            this.textBox_RedirectUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RedirectUri.Location = new System.Drawing.Point(102, 82);
            this.textBox_RedirectUri.Name = "textBox_RedirectUri";
            this.textBox_RedirectUri.Size = new System.Drawing.Size(284, 20);
            this.textBox_RedirectUri.TabIndex = 2;
            // 
            // checkBox_SaveAsNewApp
            // 
            this.checkBox_SaveAsNewApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_SaveAsNewApp.AutoSize = true;
            this.checkBox_SaveAsNewApp.Location = new System.Drawing.Point(18, 183);
            this.checkBox_SaveAsNewApp.Name = "checkBox_SaveAsNewApp";
            this.checkBox_SaveAsNewApp.Size = new System.Drawing.Size(242, 17);
            this.checkBox_SaveAsNewApp.TabIndex = 204;
            this.checkBox_SaveAsNewApp.Text = "Save this app to the app library as a new app.";
            this.checkBox_SaveAsNewApp.UseVisualStyleBackColor = true;
            // 
            // button_LoadSavedApp
            // 
            this.button_LoadSavedApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LoadSavedApp.Location = new System.Drawing.Point(263, 3);
            this.button_LoadSavedApp.Name = "button_LoadSavedApp";
            this.button_LoadSavedApp.Size = new System.Drawing.Size(123, 23);
            this.button_LoadSavedApp.TabIndex = 203;
            this.button_LoadSavedApp.Text = "Load Saved App";
            this.button_LoadSavedApp.UseVisualStyleBackColor = true;
            this.button_LoadSavedApp.Click += new System.EventHandler(this.Button_LoadSavedApp_Click);
            // 
            // V2EndpointWebAppSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_SaveAsNewApp);
            this.Controls.Add(this.button_LoadSavedApp);
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.textBox_TenantName);
            this.Controls.Add(this.textBox_ClientSecret);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.button_ScopeEditor);
            this.Controls.Add(this.textBox_Scopes);
            this.Controls.Add(this.textBox_ClientID);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.textBox_RedirectUri);
            this.Name = "V2EndpointWebAppSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_TenantName";
            this.Load += new System.EventHandler(this.V2EndpointWebAppSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox textBox_TenantName;
        private System.Windows.Forms.TextBox textBox_ClientSecret;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button button_ScopeEditor;
        private System.Windows.Forms.TextBox textBox_Scopes;
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox_RedirectUri;
        private System.Windows.Forms.CheckBox checkBox_SaveAsNewApp;
        private System.Windows.Forms.Button button_LoadSavedApp;
    }
}
