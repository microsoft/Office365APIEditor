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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_NativeAppAcquireAccessToken = new System.Windows.Forms.Button();
            this.groupBox_NativeApp = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.radioButton_NativeAppGraphResource = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.radioButton_NativeAppExoResource = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox_WebApp = new System.Windows.Forms.GroupBox();
            this.button_WebAppAcquireAccessToken = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButton_WebAppGraphResource = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton_WebAppExoResource = new System.Windows.Forms.RadioButton();
            this.radioButton_NativeApp = new System.Windows.Forms.RadioButton();
            this.radioButton_WebApp = new System.Windows.Forms.RadioButton();
            this.radioButton_BasicAuth = new System.Windows.Forms.RadioButton();
            this.groupBox_BasicAuth = new System.Windows.Forms.GroupBox();
            this.button_BasicAuthGoNext = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button_V2MobileAppAcquireAccessToken = new System.Windows.Forms.Button();
            this.radioButton_V2MobileApp = new System.Windows.Forms.RadioButton();
            this.groupBox_V2MobileApp = new System.Windows.Forms.GroupBox();
            this.button_V2MobileAppScopeEditor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox_V2WebApp = new System.Windows.Forms.GroupBox();
            this.button_V2WebAppScopeEditor = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.button_V2WebAppAcquireAccessToken = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.radioButton_V2WebApp = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox_V2MobileAppRedirectUri = new System.Windows.Forms.TextBox();
            this.textBox_V2WebAppClientSecret = new System.Windows.Forms.TextBox();
            this.textBox_V2WebAppScopes = new System.Windows.Forms.TextBox();
            this.textBox_V2WebAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_V2WebAppRedirectUri = new System.Windows.Forms.TextBox();
            this.textBox_V2MobileAppScopes = new System.Windows.Forms.TextBox();
            this.textBox_V2MobileAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_NativeAppRedirectUri = new System.Windows.Forms.TextBox();
            this.textBox_NativeAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_NativeAppTenantName = new System.Windows.Forms.TextBox();
            this.textBox_WebAppClientSecret = new System.Windows.Forms.TextBox();
            this.textBox_WebAppClientID = new System.Windows.Forms.TextBox();
            this.textBox_WebAppRedirectUri = new System.Windows.Forms.TextBox();
            this.groupBox_NativeApp.SuspendLayout();
            this.groupBox_WebApp.SuspendLayout();
            this.groupBox_BasicAuth.SuspendLayout();
            this.groupBox_V2MobileApp.SuspendLayout();
            this.groupBox_V2WebApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(956, 551);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 100;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_NativeAppAcquireAccessToken
            // 
            this.button_NativeAppAcquireAccessToken.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_NativeAppAcquireAccessToken.Location = new System.Drawing.Point(9, 158);
            this.button_NativeAppAcquireAccessToken.Name = "button_NativeAppAcquireAccessToken";
            this.button_NativeAppAcquireAccessToken.Size = new System.Drawing.Size(130, 23);
            this.button_NativeAppAcquireAccessToken.TabIndex = 14;
            this.button_NativeAppAcquireAccessToken.Text = "Acquire Access Token";
            this.button_NativeAppAcquireAccessToken.UseVisualStyleBackColor = true;
            this.button_NativeAppAcquireAccessToken.Click += new System.EventHandler(this.button_NativeAppAcquireAccessToken_Click);
            // 
            // groupBox_NativeApp
            // 
            this.groupBox_NativeApp.Controls.Add(this.label20);
            this.groupBox_NativeApp.Controls.Add(this.label19);
            this.groupBox_NativeApp.Controls.Add(this.label18);
            this.groupBox_NativeApp.Controls.Add(this.radioButton_NativeAppGraphResource);
            this.groupBox_NativeApp.Controls.Add(this.label13);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppRedirectUri);
            this.groupBox_NativeApp.Controls.Add(this.button_NativeAppAcquireAccessToken);
            this.groupBox_NativeApp.Controls.Add(this.label12);
            this.groupBox_NativeApp.Controls.Add(this.label11);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppClientID);
            this.groupBox_NativeApp.Controls.Add(this.textBox_NativeAppTenantName);
            this.groupBox_NativeApp.Controls.Add(this.radioButton_NativeAppExoResource);
            this.groupBox_NativeApp.Controls.Add(this.label14);
            this.groupBox_NativeApp.Enabled = false;
            this.groupBox_NativeApp.Location = new System.Drawing.Point(26, 270);
            this.groupBox_NativeApp.Name = "groupBox_NativeApp";
            this.groupBox_NativeApp.Size = new System.Drawing.Size(485, 190);
            this.groupBox_NativeApp.TabIndex = 31;
            this.groupBox_NativeApp.TabStop = false;
            this.groupBox_NativeApp.Text = "Native application settings";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(175, 130);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(10, 13);
            this.label20.TabIndex = 34;
            this.label20.Text = " ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 142);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 13);
            this.label19.TabIndex = 34;
            this.label19.Text = "Step 2 : Acquire Access Token";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Step 1 : Fill out the form.";
            // 
            // radioButton_NativeAppGraphResource
            // 
            this.radioButton_NativeAppGraphResource.AutoSize = true;
            this.radioButton_NativeAppGraphResource.Location = new System.Drawing.Point(290, 110);
            this.radioButton_NativeAppGraphResource.Name = "radioButton_NativeAppGraphResource";
            this.radioButton_NativeAppGraphResource.Size = new System.Drawing.Size(100, 17);
            this.radioButton_NativeAppGraphResource.TabIndex = 13;
            this.radioButton_NativeAppGraphResource.Text = "Microsoft Graph";
            this.radioButton_NativeAppGraphResource.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(100, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Redirect URL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(125, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Client ID";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(100, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Tenant Name";
            // 
            // radioButton_NativeAppExoResource
            // 
            this.radioButton_NativeAppExoResource.AutoSize = true;
            this.radioButton_NativeAppExoResource.Checked = true;
            this.radioButton_NativeAppExoResource.Location = new System.Drawing.Point(178, 110);
            this.radioButton_NativeAppExoResource.Name = "radioButton_NativeAppExoResource";
            this.radioButton_NativeAppExoResource.Size = new System.Drawing.Size(106, 17);
            this.radioButton_NativeAppExoResource.TabIndex = 12;
            this.radioButton_NativeAppExoResource.TabStop = true;
            this.radioButton_NativeAppExoResource.Text = "Exchange Online";
            this.radioButton_NativeAppExoResource.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(119, 112);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Resource";
            // 
            // groupBox_WebApp
            // 
            this.groupBox_WebApp.Controls.Add(this.button_WebAppAcquireAccessToken);
            this.groupBox_WebApp.Controls.Add(this.label16);
            this.groupBox_WebApp.Controls.Add(this.label17);
            this.groupBox_WebApp.Controls.Add(this.label9);
            this.groupBox_WebApp.Controls.Add(this.radioButton_WebAppGraphResource);
            this.groupBox_WebApp.Controls.Add(this.label6);
            this.groupBox_WebApp.Controls.Add(this.label2);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppClientSecret);
            this.groupBox_WebApp.Controls.Add(this.label10);
            this.groupBox_WebApp.Controls.Add(this.label5);
            this.groupBox_WebApp.Controls.Add(this.label4);
            this.groupBox_WebApp.Controls.Add(this.radioButton_WebAppExoResource);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppClientID);
            this.groupBox_WebApp.Controls.Add(this.textBox_WebAppRedirectUri);
            this.groupBox_WebApp.Location = new System.Drawing.Point(26, 35);
            this.groupBox_WebApp.Name = "groupBox_WebApp";
            this.groupBox_WebApp.Size = new System.Drawing.Size(485, 193);
            this.groupBox_WebApp.TabIndex = 29;
            this.groupBox_WebApp.TabStop = false;
            this.groupBox_WebApp.Text = "Web application settings";
            // 
            // button_WebAppAcquireAccessToken
            // 
            this.button_WebAppAcquireAccessToken.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_WebAppAcquireAccessToken.Location = new System.Drawing.Point(9, 159);
            this.button_WebAppAcquireAccessToken.Name = "button_WebAppAcquireAccessToken";
            this.button_WebAppAcquireAccessToken.Size = new System.Drawing.Size(130, 23);
            this.button_WebAppAcquireAccessToken.TabIndex = 7;
            this.button_WebAppAcquireAccessToken.Text = "Acquire Access Token";
            this.button_WebAppAcquireAccessToken.UseVisualStyleBackColor = true;
            this.button_WebAppAcquireAccessToken.Click += new System.EventHandler(this.button_WebAppAcquireAccessToken_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 143);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Step 2 : Acquire Access Token";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(181, 184);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = " ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Step 1 : Fill out the form.";
            // 
            // radioButton_WebAppGraphResource
            // 
            this.radioButton_WebAppGraphResource.AutoSize = true;
            this.radioButton_WebAppGraphResource.Location = new System.Drawing.Point(290, 84);
            this.radioButton_WebAppGraphResource.Name = "radioButton_WebAppGraphResource";
            this.radioButton_WebAppGraphResource.Size = new System.Drawing.Size(100, 17);
            this.radioButton_WebAppGraphResource.TabIndex = 5;
            this.radioButton_WebAppGraphResource.Text = "Microsoft Graph";
            this.radioButton_WebAppGraphResource.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Client ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(100, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Redirect URL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Client Secret";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Resource";
            // 
            // radioButton_WebAppExoResource
            // 
            this.radioButton_WebAppExoResource.AutoSize = true;
            this.radioButton_WebAppExoResource.Checked = true;
            this.radioButton_WebAppExoResource.Location = new System.Drawing.Point(178, 84);
            this.radioButton_WebAppExoResource.Name = "radioButton_WebAppExoResource";
            this.radioButton_WebAppExoResource.Size = new System.Drawing.Size(106, 17);
            this.radioButton_WebAppExoResource.TabIndex = 4;
            this.radioButton_WebAppExoResource.TabStop = true;
            this.radioButton_WebAppExoResource.Text = "Exchange Online";
            this.radioButton_WebAppExoResource.UseVisualStyleBackColor = true;
            // 
            // radioButton_NativeApp
            // 
            this.radioButton_NativeApp.AutoSize = true;
            this.radioButton_NativeApp.Location = new System.Drawing.Point(12, 247);
            this.radioButton_NativeApp.Name = "radioButton_NativeApp";
            this.radioButton_NativeApp.Size = new System.Drawing.Size(110, 17);
            this.radioButton_NativeApp.TabIndex = 8;
            this.radioButton_NativeApp.Text = "Native application";
            this.radioButton_NativeApp.UseVisualStyleBackColor = true;
            this.radioButton_NativeApp.CheckedChanged += new System.EventHandler(this.radioButton_NativeApp_CheckedChanged);
            // 
            // radioButton_WebApp
            // 
            this.radioButton_WebApp.AutoSize = true;
            this.radioButton_WebApp.Checked = true;
            this.radioButton_WebApp.Location = new System.Drawing.Point(12, 12);
            this.radioButton_WebApp.Name = "radioButton_WebApp";
            this.radioButton_WebApp.Size = new System.Drawing.Size(102, 17);
            this.radioButton_WebApp.TabIndex = 1;
            this.radioButton_WebApp.TabStop = true;
            this.radioButton_WebApp.Text = "Web application";
            this.radioButton_WebApp.UseVisualStyleBackColor = true;
            this.radioButton_WebApp.CheckedChanged += new System.EventHandler(this.radioButton_WebApp_CheckedChanged);
            // 
            // radioButton_BasicAuth
            // 
            this.radioButton_BasicAuth.AutoSize = true;
            this.radioButton_BasicAuth.Location = new System.Drawing.Point(12, 479);
            this.radioButton_BasicAuth.Name = "radioButton_BasicAuth";
            this.radioButton_BasicAuth.Size = new System.Drawing.Size(225, 17);
            this.radioButton_BasicAuth.TabIndex = 15;
            this.radioButton_BasicAuth.Text = "Non application (Use basic authentication)";
            this.radioButton_BasicAuth.UseVisualStyleBackColor = true;
            this.radioButton_BasicAuth.CheckedChanged += new System.EventHandler(this.radioButton_BasicAuth_CheckedChanged);
            // 
            // groupBox_BasicAuth
            // 
            this.groupBox_BasicAuth.Controls.Add(this.button_BasicAuthGoNext);
            this.groupBox_BasicAuth.Enabled = false;
            this.groupBox_BasicAuth.Location = new System.Drawing.Point(26, 502);
            this.groupBox_BasicAuth.Name = "groupBox_BasicAuth";
            this.groupBox_BasicAuth.Size = new System.Drawing.Size(485, 51);
            this.groupBox_BasicAuth.TabIndex = 35;
            this.groupBox_BasicAuth.TabStop = false;
            this.groupBox_BasicAuth.Text = "Basic authentication settings";
            // 
            // button_BasicAuthGoNext
            // 
            this.button_BasicAuthGoNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_BasicAuthGoNext.Location = new System.Drawing.Point(9, 19);
            this.button_BasicAuthGoNext.Name = "button_BasicAuthGoNext";
            this.button_BasicAuthGoNext.Size = new System.Drawing.Size(98, 23);
            this.button_BasicAuthGoNext.TabIndex = 16;
            this.button_BasicAuthGoNext.Text = "Go to next page";
            this.button_BasicAuthGoNext.UseVisualStyleBackColor = true;
            this.button_BasicAuthGoNext.Click += new System.EventHandler(this.button_BasicAuthGoNext_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = " ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 463);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = " ";
            // 
            // button_V2MobileAppAcquireAccessToken
            // 
            this.button_V2MobileAppAcquireAccessToken.Location = new System.Drawing.Point(9, 159);
            this.button_V2MobileAppAcquireAccessToken.Name = "button_V2MobileAppAcquireAccessToken";
            this.button_V2MobileAppAcquireAccessToken.Size = new System.Drawing.Size(130, 23);
            this.button_V2MobileAppAcquireAccessToken.TabIndex = 29;
            this.button_V2MobileAppAcquireAccessToken.Text = "Acquire Access Token";
            this.button_V2MobileAppAcquireAccessToken.UseVisualStyleBackColor = true;
            this.button_V2MobileAppAcquireAccessToken.Click += new System.EventHandler(this.button_V2MobileAppAcquireAccessToken_Click);
            // 
            // radioButton_V2MobileApp
            // 
            this.radioButton_V2MobileApp.AutoSize = true;
            this.radioButton_V2MobileApp.Location = new System.Drawing.Point(532, 282);
            this.radioButton_V2MobileApp.Name = "radioButton_V2MobileApp";
            this.radioButton_V2MobileApp.Size = new System.Drawing.Size(184, 17);
            this.radioButton_V2MobileApp.TabIndex = 24;
            this.radioButton_V2MobileApp.Text = "Mobile application (v2.0 endpoint)";
            this.radioButton_V2MobileApp.UseVisualStyleBackColor = true;
            this.radioButton_V2MobileApp.CheckedChanged += new System.EventHandler(this.radioButton_V2MobileApp_CheckedChanged);
            // 
            // groupBox_V2MobileApp
            // 
            this.groupBox_V2MobileApp.Controls.Add(this.button_V2MobileAppScopeEditor);
            this.groupBox_V2MobileApp.Controls.Add(this.textBox_V2MobileAppScopes);
            this.groupBox_V2MobileApp.Controls.Add(this.label1);
            this.groupBox_V2MobileApp.Controls.Add(this.textBox_V2MobileAppClientID);
            this.groupBox_V2MobileApp.Controls.Add(this.button_V2MobileAppAcquireAccessToken);
            this.groupBox_V2MobileApp.Controls.Add(this.label3);
            this.groupBox_V2MobileApp.Controls.Add(this.label15);
            this.groupBox_V2MobileApp.Controls.Add(this.label21);
            this.groupBox_V2MobileApp.Controls.Add(this.label22);
            this.groupBox_V2MobileApp.Controls.Add(this.label23);
            this.groupBox_V2MobileApp.Controls.Add(this.label25);
            this.groupBox_V2MobileApp.Controls.Add(this.textBox_V2MobileAppRedirectUri);
            this.groupBox_V2MobileApp.Enabled = false;
            this.groupBox_V2MobileApp.Location = new System.Drawing.Point(546, 305);
            this.groupBox_V2MobileApp.Name = "groupBox_V2MobileApp";
            this.groupBox_V2MobileApp.Size = new System.Drawing.Size(485, 193);
            this.groupBox_V2MobileApp.TabIndex = 54;
            this.groupBox_V2MobileApp.TabStop = false;
            this.groupBox_V2MobileApp.Text = "Mobile application (v2.0 endpoint) settings";
            // 
            // button_V2MobileAppScopeEditor
            // 
            this.button_V2MobileAppScopeEditor.Location = new System.Drawing.Point(178, 111);
            this.button_V2MobileAppScopeEditor.Name = "button_V2MobileAppScopeEditor";
            this.button_V2MobileAppScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_V2MobileAppScopeEditor.TabIndex = 28;
            this.button_V2MobileAppScopeEditor.Text = "Scope editor...";
            this.button_V2MobileAppScopeEditor.UseVisualStyleBackColor = true;
            this.button_V2MobileAppScopeEditor.Click += new System.EventHandler(this.button_V2MobileAppScopeEditor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Step 2 : Acquire Access Token";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = " ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(121, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Step 1 : Fill out the form.";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(138, 130);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(10, 13);
            this.label21.TabIndex = 30;
            this.label21.Text = " ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(125, 35);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "Client ID";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(100, 61);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(72, 13);
            this.label23.TabIndex = 15;
            this.label23.Text = "Redirect URL";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(129, 87);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(43, 13);
            this.label25.TabIndex = 14;
            this.label25.Text = "Scopes";
            // 
            // groupBox_V2WebApp
            // 
            this.groupBox_V2WebApp.Controls.Add(this.textBox_V2WebAppClientSecret);
            this.groupBox_V2WebApp.Controls.Add(this.label26);
            this.groupBox_V2WebApp.Controls.Add(this.button_V2WebAppScopeEditor);
            this.groupBox_V2WebApp.Controls.Add(this.textBox_V2WebAppScopes);
            this.groupBox_V2WebApp.Controls.Add(this.label24);
            this.groupBox_V2WebApp.Controls.Add(this.textBox_V2WebAppClientID);
            this.groupBox_V2WebApp.Controls.Add(this.button_V2WebAppAcquireAccessToken);
            this.groupBox_V2WebApp.Controls.Add(this.label27);
            this.groupBox_V2WebApp.Controls.Add(this.label28);
            this.groupBox_V2WebApp.Controls.Add(this.label29);
            this.groupBox_V2WebApp.Controls.Add(this.label30);
            this.groupBox_V2WebApp.Controls.Add(this.label31);
            this.groupBox_V2WebApp.Controls.Add(this.textBox_V2WebAppRedirectUri);
            this.groupBox_V2WebApp.Enabled = false;
            this.groupBox_V2WebApp.Location = new System.Drawing.Point(546, 35);
            this.groupBox_V2WebApp.Name = "groupBox_V2WebApp";
            this.groupBox_V2WebApp.Size = new System.Drawing.Size(485, 229);
            this.groupBox_V2WebApp.TabIndex = 56;
            this.groupBox_V2WebApp.TabStop = false;
            this.groupBox_V2WebApp.Text = "Web application (v2.0 endpoint) settings";
            // 
            // button_V2WebAppScopeEditor
            // 
            this.button_V2WebAppScopeEditor.Location = new System.Drawing.Point(178, 111);
            this.button_V2WebAppScopeEditor.Name = "button_V2WebAppScopeEditor";
            this.button_V2WebAppScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_V2WebAppScopeEditor.TabIndex = 21;
            this.button_V2WebAppScopeEditor.Text = "Scope editor...";
            this.button_V2WebAppScopeEditor.UseVisualStyleBackColor = true;
            this.button_V2WebAppScopeEditor.Click += new System.EventHandler(this.button_V2WebAppScopeEditor_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 180);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(155, 13);
            this.label24.TabIndex = 33;
            this.label24.Text = "Step 2 : Acquire Access Token";
            // 
            // button_V2WebAppAcquireAccessToken
            // 
            this.button_V2WebAppAcquireAccessToken.Location = new System.Drawing.Point(18, 196);
            this.button_V2WebAppAcquireAccessToken.Name = "button_V2WebAppAcquireAccessToken";
            this.button_V2WebAppAcquireAccessToken.Size = new System.Drawing.Size(130, 23);
            this.button_V2WebAppAcquireAccessToken.TabIndex = 23;
            this.button_V2WebAppAcquireAccessToken.Text = "Acquire Access Token";
            this.button_V2WebAppAcquireAccessToken.UseVisualStyleBackColor = true;
            this.button_V2WebAppAcquireAccessToken.Click += new System.EventHandler(this.button_V2WebAppAcquireAccessToken_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(121, 13);
            this.label27.TabIndex = 31;
            this.label27.Text = "Step 1 : Fill out the form.";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(181, 163);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(10, 13);
            this.label28.TabIndex = 30;
            this.label28.Text = " ";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(125, 35);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(47, 13);
            this.label29.TabIndex = 16;
            this.label29.Text = "Client ID";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(100, 61);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 13);
            this.label30.TabIndex = 15;
            this.label30.Text = "Redirect URL";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(129, 87);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(43, 13);
            this.label31.TabIndex = 14;
            this.label31.Text = "Scopes";
            // 
            // radioButton_V2WebApp
            // 
            this.radioButton_V2WebApp.AutoSize = true;
            this.radioButton_V2WebApp.Location = new System.Drawing.Point(532, 12);
            this.radioButton_V2WebApp.Name = "radioButton_V2WebApp";
            this.radioButton_V2WebApp.Size = new System.Drawing.Size(176, 17);
            this.radioButton_V2WebApp.TabIndex = 17;
            this.radioButton_V2WebApp.Text = "Web application (v2.0 endpoint)";
            this.radioButton_V2WebApp.UseVisualStyleBackColor = true;
            this.radioButton_V2WebApp.CheckedChanged += new System.EventHandler(this.radioButton_V2WebApp_CheckedChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(105, 143);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 35;
            this.label26.Text = "Client Secret";
            // 
            // textBox_V2MobileAppRedirectUri
            // 
            this.textBox_V2MobileAppRedirectUri.Enabled = false;
            this.textBox_V2MobileAppRedirectUri.Location = new System.Drawing.Point(178, 58);
            this.textBox_V2MobileAppRedirectUri.Name = "textBox_V2MobileAppRedirectUri";
            this.textBox_V2MobileAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2MobileAppRedirectUri.TabIndex = 26;
            this.textBox_V2MobileAppRedirectUri.Text = "urn:ietf:wg:oauth:2.0:oob";
            // 
            // textBox_V2WebAppClientSecret
            // 
            this.textBox_V2WebAppClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2WebAppClientSecret.Location = new System.Drawing.Point(178, 140);
            this.textBox_V2WebAppClientSecret.Name = "textBox_V2WebAppClientSecret";
            this.textBox_V2WebAppClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2WebAppClientSecret.TabIndex = 22;
            this.textBox_V2WebAppClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppClientSecret;
            // 
            // textBox_V2WebAppScopes
            // 
            this.textBox_V2WebAppScopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2WebAppScopes.Location = new System.Drawing.Point(178, 84);
            this.textBox_V2WebAppScopes.Name = "textBox_V2WebAppScopes";
            this.textBox_V2WebAppScopes.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2WebAppScopes.TabIndex = 20;
            this.textBox_V2WebAppScopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppScopes;
            // 
            // textBox_V2WebAppClientID
            // 
            this.textBox_V2WebAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2WebAppClientID.Location = new System.Drawing.Point(178, 32);
            this.textBox_V2WebAppClientID.Name = "textBox_V2WebAppClientID";
            this.textBox_V2WebAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2WebAppClientID.TabIndex = 18;
            this.textBox_V2WebAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppClientID;
            // 
            // textBox_V2WebAppRedirectUri
            // 
            this.textBox_V2WebAppRedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppRedirectUri", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2WebAppRedirectUri.Location = new System.Drawing.Point(178, 58);
            this.textBox_V2WebAppRedirectUri.Name = "textBox_V2WebAppRedirectUri";
            this.textBox_V2WebAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2WebAppRedirectUri.TabIndex = 19;
            this.textBox_V2WebAppRedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppRedirectUri;
            // 
            // textBox_V2MobileAppScopes
            // 
            this.textBox_V2MobileAppScopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2MobileAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2MobileAppScopes.Location = new System.Drawing.Point(178, 84);
            this.textBox_V2MobileAppScopes.Name = "textBox_V2MobileAppScopes";
            this.textBox_V2MobileAppScopes.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2MobileAppScopes.TabIndex = 27;
            this.textBox_V2MobileAppScopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2MobileAppScopes;
            // 
            // textBox_V2MobileAppClientID
            // 
            this.textBox_V2MobileAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2MobileAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_V2MobileAppClientID.Location = new System.Drawing.Point(178, 32);
            this.textBox_V2MobileAppClientID.Name = "textBox_V2MobileAppClientID";
            this.textBox_V2MobileAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_V2MobileAppClientID.TabIndex = 25;
            this.textBox_V2MobileAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2MobileAppClientID;
            // 
            // textBox_NativeAppRedirectUri
            // 
            this.textBox_NativeAppRedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppRedirectUri.Location = new System.Drawing.Point(178, 84);
            this.textBox_NativeAppRedirectUri.Name = "textBox_NativeAppRedirectUri";
            this.textBox_NativeAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppRedirectUri.TabIndex = 11;
            this.textBox_NativeAppRedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppRedirectURL;
            // 
            // textBox_NativeAppClientID
            // 
            this.textBox_NativeAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppClientID.Location = new System.Drawing.Point(178, 58);
            this.textBox_NativeAppClientID.Name = "textBox_NativeAppClientID";
            this.textBox_NativeAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppClientID.TabIndex = 10;
            this.textBox_NativeAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppClientID;
            // 
            // textBox_NativeAppTenantName
            // 
            this.textBox_NativeAppTenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_NativeAppTenantName.Location = new System.Drawing.Point(178, 32);
            this.textBox_NativeAppTenantName.Name = "textBox_NativeAppTenantName";
            this.textBox_NativeAppTenantName.Size = new System.Drawing.Size(295, 20);
            this.textBox_NativeAppTenantName.TabIndex = 9;
            this.textBox_NativeAppTenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppTenantName;
            // 
            // textBox_WebAppClientSecret
            // 
            this.textBox_WebAppClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppClientSecret.Location = new System.Drawing.Point(178, 107);
            this.textBox_WebAppClientSecret.Name = "textBox_WebAppClientSecret";
            this.textBox_WebAppClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppClientSecret.TabIndex = 6;
            this.textBox_WebAppClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientSecret;
            // 
            // textBox_WebAppClientID
            // 
            this.textBox_WebAppClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppClientID.Location = new System.Drawing.Point(178, 32);
            this.textBox_WebAppClientID.Name = "textBox_WebAppClientID";
            this.textBox_WebAppClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppClientID.TabIndex = 2;
            this.textBox_WebAppClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientID;
            // 
            // textBox_WebAppRedirectUri
            // 
            this.textBox_WebAppRedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_WebAppRedirectUri.Location = new System.Drawing.Point(178, 58);
            this.textBox_WebAppRedirectUri.Name = "textBox_WebAppRedirectUri";
            this.textBox_WebAppRedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_WebAppRedirectUri.TabIndex = 3;
            this.textBox_WebAppRedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppRedirectURL;
            // 
            // StartForm
            // 
            this.AcceptButton = this.button_NativeAppAcquireAccessToken;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(1044, 586);
            this.Controls.Add(this.groupBox_V2WebApp);
            this.Controls.Add(this.radioButton_V2WebApp);
            this.Controls.Add(this.groupBox_V2MobileApp);
            this.Controls.Add(this.radioButton_V2MobileApp);
            this.Controls.Add(this.groupBox_BasicAuth);
            this.Controls.Add(this.radioButton_BasicAuth);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.groupBox_NativeApp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox_WebApp);
            this.Controls.Add(this.radioButton_NativeApp);
            this.Controls.Add(this.radioButton_WebApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Text = "Check your application settings";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.groupBox_NativeApp.ResumeLayout(false);
            this.groupBox_NativeApp.PerformLayout();
            this.groupBox_WebApp.ResumeLayout(false);
            this.groupBox_WebApp.PerformLayout();
            this.groupBox_BasicAuth.ResumeLayout(false);
            this.groupBox_V2MobileApp.ResumeLayout(false);
            this.groupBox_V2MobileApp.PerformLayout();
            this.groupBox_V2WebApp.ResumeLayout(false);
            this.groupBox_V2WebApp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_NativeAppAcquireAccessToken;
        private System.Windows.Forms.GroupBox groupBox_NativeApp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_NativeAppRedirectUri;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_NativeAppClientID;
        private System.Windows.Forms.TextBox textBox_NativeAppTenantName;
        private System.Windows.Forms.RadioButton radioButton_NativeAppExoResource;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox_WebApp;
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
        private System.Windows.Forms.RadioButton radioButton_NativeAppGraphResource;
        private System.Windows.Forms.RadioButton radioButton_WebAppGraphResource;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button_WebAppAcquireAccessToken;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_BasicAuthGoNext;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_V2MobileAppAcquireAccessToken;
        private System.Windows.Forms.TextBox textBox_V2MobileAppClientID;
        private System.Windows.Forms.RadioButton radioButton_V2MobileApp;
        private System.Windows.Forms.GroupBox groupBox_V2MobileApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox_V2MobileAppRedirectUri;
        private System.Windows.Forms.TextBox textBox_V2MobileAppScopes;
        private System.Windows.Forms.Button button_V2MobileAppScopeEditor;
        private System.Windows.Forms.GroupBox groupBox_V2WebApp;
        private System.Windows.Forms.TextBox textBox_V2WebAppClientSecret;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button button_V2WebAppScopeEditor;
        private System.Windows.Forms.TextBox textBox_V2WebAppScopes;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox_V2WebAppClientID;
        private System.Windows.Forms.Button button_V2WebAppAcquireAccessToken;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox_V2WebAppRedirectUri;
        private System.Windows.Forms.RadioButton radioButton_V2WebApp;
    }
}