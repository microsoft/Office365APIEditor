namespace Office365APIEditor
{
    partial class StartForm
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_AcquireAccessToken = new System.Windows.Forms.Button();
            this.groupBox_NativeApp = new System.Windows.Forms.GroupBox();
            this.radioButton_NativeAppGraphResource = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_NativeAppRedirectUri = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_NativeAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_NativeAppTenantName = new System.Windows.Forms.TextBox();
            this.radioButton_NativeAppExoResource = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_WebApp = new System.Windows.Forms.GroupBox();
            this.radioButton_WebAppGraphResource = new System.Windows.Forms.RadioButton();
            this.textBox_WebAppCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_WebAppGetCode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_WebAppClientSecret = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton_WebAppExoResource = new System.Windows.Forms.RadioButton();
            this.textBox_WebAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_WebAppRedirectUri = new System.Windows.Forms.TextBox();
            this.radioButton_NativeApp = new System.Windows.Forms.RadioButton();
            this.radioButton_WebApp = new System.Windows.Forms.RadioButton();
            this.radioButton_BasicAuth = new System.Windows.Forms.RadioButton();
            this.groupBox_BasicAuth = new System.Windows.Forms.GroupBox();
            this.textBox_BasicAuthSMTPAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_NativeApp.SuspendLayout();
            this.groupBox_WebApp.SuspendLayout();
            this.groupBox_BasicAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(560, 378);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 33;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_AcquireAccessToken
            // 
            this.button_AcquireAccessToken.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_AcquireAccessToken.Location = new System.Drawing.Point(424, 378);
            this.button_AcquireAccessToken.Name = "button_AcquireAccessToken";
            this.button_AcquireAccessToken.Size = new System.Drawing.Size(130, 23);
            this.button_AcquireAccessToken.TabIndex = 32;
            this.button_AcquireAccessToken.Text = "Acquire Access Token";
            this.button_AcquireAccessToken.UseVisualStyleBackColor = true;
            this.button_AcquireAccessToken.Click += new System.EventHandler(this.button_AcquireAccessToken_Click);
            // 
            // groupBox_NativeApp
            // 
            this.groupBox_NativeApp.Controls.Add(this.radioButton_NativeAppGraphResource);
            this.groupBox_NativeApp.Controls.Add(this.label13);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppRedirectUri);
            this.groupBox_NativeApp.Controls.Add(this.label12);
            this.groupBox_NativeApp.Controls.Add(this.label11);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppClientID);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppTenantName);
            this.groupBox_NativeApp.Controls.Add(this.radioButton_NativeAppExoResource);
            this.groupBox_NativeApp.Controls.Add(this.label14);
            this.groupBox_NativeApp.Enabled = false;
            this.groupBox_NativeApp.Location = new System.Drawing.Point(14, 188);
            this.groupBox_NativeApp.Name = "groupBox_NativeApp";
            this.groupBox_NativeApp.Size = new System.Drawing.Size(621, 127);
            this.groupBox_NativeApp.TabIndex = 31;
            this.groupBox_NativeApp.TabStop = false;
            this.groupBox_NativeApp.Text = "Native application options";
            // 
            // radioButton_NativeAppGraphResource
            // 
            this.radioButton_NativeAppGraphResource.AutoSize = true;
            this.radioButton_NativeAppGraphResource.Location = new System.Drawing.Point(220, 97);
            this.radioButton_NativeAppGraphResource.Name = "radioButton_NativeAppGraphResource";
            this.radioButton_NativeAppGraphResource.Size = new System.Drawing.Size(100, 17);
            this.radioButton_NativeAppGraphResource.TabIndex = 30;
            this.radioButton_NativeAppGraphResource.Text = "Microsoft Graph";
            this.radioButton_NativeAppGraphResource.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Redirect URL";
            // 
            // textBox_NativeAppRedirectUri
            // 
            this.textBox_NativeAppRedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppRedirectUri.Location = new System.Drawing.Point(108, 71);
            this.textBox_NativeAppRedirectUri.Name = "textBox_NativeAppRedirectUri";
            this.textBox_NativeAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppRedirectUri.TabIndex = 29;
            this.textBox_NativeAppRedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppRedirectURL;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(55, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Client ID";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Tenant Name";
            // 
            // textBox_NativeAppClientID
            // 
            this.textBox_NativeAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppClientID.Location = new System.Drawing.Point(108, 45);
            this.textBox_NativeAppClientID.Name = "textBox_NativeAppClientID";
            this.textBox_NativeAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppClientID.TabIndex = 27;
            this.textBox_NativeAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppClientID;
            // 
            // textBox_NativeAppTenantName
            // 
            this.textBox_NativeAppTenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppTenantName.Location = new System.Drawing.Point(108, 19);
            this.textBox_NativeAppTenantName.Name = "textBox_NativeAppTenantName";
            this.textBox_NativeAppTenantName.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppTenantName.TabIndex = 18;
            this.textBox_NativeAppTenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppTenantName;
            // 
            // radioButton_NativeAppExoResource
            // 
            this.radioButton_NativeAppExoResource.AutoSize = true;
            this.radioButton_NativeAppExoResource.Checked = true;
            this.radioButton_NativeAppExoResource.Location = new System.Drawing.Point(108, 97);
            this.radioButton_NativeAppExoResource.Name = "radioButton_NativeAppExoResource";
            this.radioButton_NativeAppExoResource.Size = new System.Drawing.Size(106, 17);
            this.radioButton_NativeAppExoResource.TabIndex = 19;
            this.radioButton_NativeAppExoResource.TabStop = true;
            this.radioButton_NativeAppExoResource.Text = "Exchange Online";
            this.radioButton_NativeAppExoResource.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(49, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Resource";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Choose a type of your application.";
            // 
            // groupBox_WebApp
            // 
            this.groupBox_WebApp.Controls.Add(this.radioButton_WebAppGraphResource);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppCode);
            this.groupBox_WebApp.Controls.Add(this.label6);
            this.groupBox_WebApp.Controls.Add(this.button_WebAppGetCode);
            this.groupBox_WebApp.Controls.Add(this.label2);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppClientSecret);
            this.groupBox_WebApp.Controls.Add(this.label10);
            this.groupBox_WebApp.Controls.Add(this.label5);
            this.groupBox_WebApp.Controls.Add(this.label4);
            this.groupBox_WebApp.Controls.Add(this.radioButton_WebAppExoResource);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppClientID);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppRedirectUri);
            this.groupBox_WebApp.Location = new System.Drawing.Point(14, 32);
            this.groupBox_WebApp.Name = "groupBox_WebApp";
            this.groupBox_WebApp.Size = new System.Drawing.Size(621, 150);
            this.groupBox_WebApp.TabIndex = 29;
            this.groupBox_WebApp.TabStop = false;
            this.groupBox_WebApp.Text = "Web application options";
            // 
            // radioButton_WebAppGraphResource
            // 
            this.radioButton_WebAppGraphResource.AutoSize = true;
            this.radioButton_WebAppGraphResource.Location = new System.Drawing.Point(220, 71);
            this.radioButton_WebAppGraphResource.Name = "radioButton_WebAppGraphResource";
            this.radioButton_WebAppGraphResource.Size = new System.Drawing.Size(100, 17);
            this.radioButton_WebAppGraphResource.TabIndex = 25;
            this.radioButton_WebAppGraphResource.Text = "Microsoft Graph";
            this.radioButton_WebAppGraphResource.UseVisualStyleBackColor = true;
            // 
            // textBox_WebAppCode
            // 
            this.textBox_WebAppCode.Enabled = false;
            this.textBox_WebAppCode.Location = new System.Drawing.Point(108, 120);
            this.textBox_WebAppCode.Name = "textBox_WebAppCode";
            this.textBox_WebAppCode.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppCode.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Authorization Code";
            // 
            // button_WebAppGetCode
            // 
            this.button_WebAppGetCode.Location = new System.Drawing.Point(409, 118);
            this.button_WebAppGetCode.Name = "button_WebAppGetCode";
            this.button_WebAppGetCode.Size = new System.Drawing.Size(166, 23);
            this.button_WebAppGetCode.TabIndex = 22;
            this.button_WebAppGetCode.Text = "Acquire Authentication Code";
            this.button_WebAppGetCode.UseVisualStyleBackColor = true;
            this.button_WebAppGetCode.Click += new System.EventHandler(this.button_WebAppGetCode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Client ID";
            // 
            // textBox_WebAppClientSecret
            // 
            this.textBox_WebAppClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppClientSecret.Location = new System.Drawing.Point(108, 94);
            this.textBox_WebAppClientSecret.Name = "textBox_WebAppClientSecret";
            this.textBox_WebAppClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppClientSecret.TabIndex = 21;
            this.textBox_WebAppClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientSecret;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Redirect URL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Client Secret";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Resource";
            // 
            // radioButton_WebAppExoResource
            // 
            this.radioButton_WebAppExoResource.AutoSize = true;
            this.radioButton_WebAppExoResource.Checked = true;
            this.radioButton_WebAppExoResource.Location = new System.Drawing.Point(108, 71);
            this.radioButton_WebAppExoResource.Name = "radioButton_WebAppExoResource";
            this.radioButton_WebAppExoResource.Size = new System.Drawing.Size(106, 17);
            this.radioButton_WebAppExoResource.TabIndex = 19;
            this.radioButton_WebAppExoResource.TabStop = true;
            this.radioButton_WebAppExoResource.Text = "Exchange Online";
            this.radioButton_WebAppExoResource.UseVisualStyleBackColor = true;
            // 
            // textBox_WebAppClientID
            // 
            this.textBox_WebAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppClientID.Location = new System.Drawing.Point(108, 19);
            this.textBox_WebAppClientID.Name = "textBox_WebAppClientID";
            this.textBox_WebAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppClientID.TabIndex = 18;
            this.textBox_WebAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientID;
            // 
            // textBox_WebAppRedirectUri
            // 
            this.textBox_WebAppRedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppRedirectUri.Location = new System.Drawing.Point(108, 45);
            this.textBox_WebAppRedirectUri.Name = "textBox_WebAppRedirectUri";
            this.textBox_WebAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppRedirectUri.TabIndex = 17;
            this.textBox_WebAppRedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppRedirectURL;
            // 
            // radioButton_NativeApp
            // 
            this.radioButton_NativeApp.AutoSize = true;
            this.radioButton_NativeApp.Location = new System.Drawing.Point(293, 9);
            this.radioButton_NativeApp.Name = "radioButton_NativeApp";
            this.radioButton_NativeApp.Size = new System.Drawing.Size(110, 17);
            this.radioButton_NativeApp.TabIndex = 27;
            this.radioButton_NativeApp.Text = "Native application";
            this.radioButton_NativeApp.UseVisualStyleBackColor = true;
            this.radioButton_NativeApp.CheckedChanged += new System.EventHandler(this.radioButton_NativeApp_CheckedChanged);
            // 
            // radioButton_WebApp
            // 
            this.radioButton_WebApp.AutoSize = true;
            this.radioButton_WebApp.Checked = true;
            this.radioButton_WebApp.Location = new System.Drawing.Point(184, 9);
            this.radioButton_WebApp.Name = "radioButton_WebApp";
            this.radioButton_WebApp.Size = new System.Drawing.Size(102, 17);
            this.radioButton_WebApp.TabIndex = 28;
            this.radioButton_WebApp.TabStop = true;
            this.radioButton_WebApp.Text = "Web application";
            this.radioButton_WebApp.UseVisualStyleBackColor = true;
            this.radioButton_WebApp.CheckedChanged += new System.EventHandler(this.radioButton_WebApp_CheckedChanged);
            // 
            // radioButton_BasicAuth
            // 
            this.radioButton_BasicAuth.AutoSize = true;
            this.radioButton_BasicAuth.Location = new System.Drawing.Point(409, 9);
            this.radioButton_BasicAuth.Name = "radioButton_BasicAuth";
            this.radioButton_BasicAuth.Size = new System.Drawing.Size(225, 17);
            this.radioButton_BasicAuth.TabIndex = 34;
            this.radioButton_BasicAuth.Text = "Non application (Use basic authentication)";
            this.radioButton_BasicAuth.UseVisualStyleBackColor = true;
            this.radioButton_BasicAuth.CheckedChanged += new System.EventHandler(this.radioButton_BasicAuth_CheckedChanged);
            // 
            // groupBox_BasicAuth
            // 
            this.groupBox_BasicAuth.Controls.Add(this.textBox_BasicAuthSMTPAddress);
            this.groupBox_BasicAuth.Controls.Add(this.label3);
            this.groupBox_BasicAuth.Enabled = false;
            this.groupBox_BasicAuth.Location = new System.Drawing.Point(14, 321);
            this.groupBox_BasicAuth.Name = "groupBox_BasicAuth";
            this.groupBox_BasicAuth.Size = new System.Drawing.Size(620, 51);
            this.groupBox_BasicAuth.TabIndex = 35;
            this.groupBox_BasicAuth.TabStop = false;
            this.groupBox_BasicAuth.Text = "Basic authentication options";
            // 
            // textBox_BasicAuthSMTPAddress
            // 
            this.textBox_BasicAuthSMTPAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "BasicAuthSmtpAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_BasicAuthSMTPAddress.Location = new System.Drawing.Point(108, 19);
            this.textBox_BasicAuthSMTPAddress.Name = "textBox_BasicAuthSMTPAddress";
            this.textBox_BasicAuthSMTPAddress.Size = new System.Drawing.Size(295, 20);
            this.textBox_BasicAuthSMTPAddress.TabIndex = 2;
            this.textBox_BasicAuthSMTPAddress.Text = global::Office365APIEditor.Properties.Settings.Default.BasicAuthSmtpAddress;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "SMTP Address";
            // 
            // StartForm
            // 
            this.AcceptButton = this.button_AcquireAccessToken;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(647, 410);
            this.Controls.Add(this.groupBox_BasicAuth);
            this.Controls.Add(this.radioButton_BasicAuth);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_AcquireAccessToken);
            this.Controls.Add(this.groupBox_NativeApp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_WebApp);
            this.Controls.Add(this.radioButton_NativeApp);
            this.Controls.Add(this.radioButton_WebApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Text = "Welcome";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.groupBox_NativeApp.ResumeLayout(false);
            this.groupBox_NativeApp.PerformLayout();
            this.groupBox_WebApp.ResumeLayout(false);
            this.groupBox_WebApp.PerformLayout();
            this.groupBox_BasicAuth.ResumeLayout(false);
            this.groupBox_BasicAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_AcquireAccessToken;
        private System.Windows.Forms.GroupBox groupBox_NativeApp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_NativeAppRedirectUri;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_NativeAppClientID;
        private System.Windows.Forms.TextBox textBox_NativeAppTenantName;
        private System.Windows.Forms.RadioButton radioButton_NativeAppExoResource;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_WebApp;
        private System.Windows.Forms.TextBox textBox_WebAppCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_WebAppGetCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_WebAppClientSecret;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton_WebAppExoResource;
        private System.Windows.Forms.TextBox textBox_WebAppClientID;
        private System.Windows.Forms.TextBox textBox_WebAppRedirectUri;
        private System.Windows.Forms.RadioButton radioButton_NativeApp;
        private System.Windows.Forms.RadioButton radioButton_WebApp;
        private System.Windows.Forms.RadioButton radioButton_BasicAuth;
        private System.Windows.Forms.GroupBox groupBox_BasicAuth;
        private System.Windows.Forms.TextBox textBox_BasicAuthSMTPAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_NativeAppGraphResource;
        private System.Windows.Forms.RadioButton radioButton_WebAppGraphResource;
    }
}