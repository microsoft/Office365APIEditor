namespace Office365APIEditor
{
    partial class AccessTokenWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessTokenWizard));
            this.panel_Page00 = new System.Windows.Forms.Panel();
            this.radioButton_Page00_BuiltInAppOrBasicAuth = new System.Windows.Forms.RadioButton();
            this.radioButton_Page00_AppRegistrationPortal = new System.Windows.Forms.RadioButton();
            this.radioButton_Page00_MicrosoftAzurePortal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_Next = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel_Page01 = new System.Windows.Forms.Panel();
            this.radioButton_Page01_V1AdminConsent = new System.Windows.Forms.RadioButton();
            this.radioButton_Page01_V1AppOnlyByKey = new System.Windows.Forms.RadioButton();
            this.radioButton_Page01_V1AppOnlyByCert = new System.Windows.Forms.RadioButton();
            this.radioButton_Page01_V1Native = new System.Windows.Forms.RadioButton();
            this.radioButton_Page01_V1Web = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_Page03 = new System.Windows.Forms.Panel();
            this.comboBox_Page03_Resource = new System.Windows.Forms.ComboBox();
            this.linkLabel_Page03_WebApp = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Page03_ClientSecret = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Page03_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_Page03_RedirectUri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_Page02 = new System.Windows.Forms.Panel();
            this.radioButton_Page02_V2Mobile = new System.Windows.Forms.RadioButton();
            this.radioButton_Page02_V2Web = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_Page04 = new System.Windows.Forms.Panel();
            this.comboBox_Page04_Resource = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.linkLabel_Page04_NativeApp = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_Page04_RedirectUri = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_Page04_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_Page04_TenantName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel_Page05 = new System.Windows.Forms.Panel();
            this.linkLabel_linkLabel_Page05_WebAppAppOnly = new System.Windows.Forms.LinkLabel();
            this.comboBox_Page05_Resource = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button_Page05_SelectCert = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox_Page05_CertPath = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox_Page05_CertPass = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox_Page05_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_Page05_TenantName = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.panel_Page06 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_Page06_ClientSecret = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.button_Page06_ScopeEditor = new System.Windows.Forms.Button();
            this.textBox_Page06_Scopes = new System.Windows.Forms.TextBox();
            this.textBox_Page06_ClientID = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox_Page06_RedirectUri = new System.Windows.Forms.TextBox();
            this.panel_Page07 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.button_Page07_ScopeEditor = new System.Windows.Forms.Button();
            this.textBox_Page07_Scopes = new System.Windows.Forms.TextBox();
            this.textBox_Page07_ClientID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox_Page07_RedirectUri = new System.Windows.Forms.TextBox();
            this.panel_Page08 = new System.Windows.Forms.Panel();
            this.linkLabel_Page08_WebAppAppOnlyByKey = new System.Windows.Forms.LinkLabel();
            this.comboBox_Page08_Resource = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_Page08_ClientSecret = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_Page08_TenantName = new System.Windows.Forms.TextBox();
            this.textBox_Page08_ClientID = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.openFileDialog_PFX = new System.Windows.Forms.OpenFileDialog();
            this.panel_Page09 = new System.Windows.Forms.Panel();
            this.comboBox_Page09_Resource = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox_Page09_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_Page09_RedirectUri = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.panel_Page10 = new System.Windows.Forms.Panel();
            this.radioButton_Page10_BasicAuth = new System.Windows.Forms.RadioButton();
            this.radioButton_Page10_BuiltInApp = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.panel_Page11 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.button_Page11_ScopeEditor = new System.Windows.Forms.Button();
            this.textBox_Page11_Scopes = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.panel_Page00.SuspendLayout();
            this.panel_Page01.SuspendLayout();
            this.panel_Page03.SuspendLayout();
            this.panel_Page02.SuspendLayout();
            this.panel_Page04.SuspendLayout();
            this.panel_Page05.SuspendLayout();
            this.panel_Page06.SuspendLayout();
            this.panel_Page07.SuspendLayout();
            this.panel_Page08.SuspendLayout();
            this.panel_Page09.SuspendLayout();
            this.panel_Page10.SuspendLayout();
            this.panel_Page11.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Page00
            // 
            this.panel_Page00.Controls.Add(this.radioButton_Page00_BuiltInAppOrBasicAuth);
            this.panel_Page00.Controls.Add(this.radioButton_Page00_AppRegistrationPortal);
            this.panel_Page00.Controls.Add(this.radioButton_Page00_MicrosoftAzurePortal);
            this.panel_Page00.Controls.Add(this.label1);
            this.panel_Page00.Location = new System.Drawing.Point(12, 12);
            this.panel_Page00.Name = "panel_Page00";
            this.panel_Page00.Size = new System.Drawing.Size(400, 170);
            this.panel_Page00.TabIndex = 0;
            this.panel_Page00.Tag = "radioButton_Page00_MicrosoftAzurePortal";
            // 
            // radioButton_Page00_BuiltInAppOrBasicAuth
            // 
            this.radioButton_Page00_BuiltInAppOrBasicAuth.AutoSize = true;
            this.radioButton_Page00_BuiltInAppOrBasicAuth.Location = new System.Drawing.Point(18, 135);
            this.radioButton_Page00_BuiltInAppOrBasicAuth.Name = "radioButton_Page00_BuiltInAppOrBasicAuth";
            this.radioButton_Page00_BuiltInAppOrBasicAuth.Size = new System.Drawing.Size(324, 30);
            this.radioButton_Page00_BuiltInAppOrBasicAuth.TabIndex = 3;
            this.radioButton_Page00_BuiltInAppOrBasicAuth.TabStop = true;
            this.radioButton_Page00_BuiltInAppOrBasicAuth.Text = "I have not registered the application.\r\nOffice365APIEditor will use the built-in " +
    "application or basic auth.";
            this.radioButton_Page00_BuiltInAppOrBasicAuth.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page00_AppRegistrationPortal
            // 
            this.radioButton_Page00_AppRegistrationPortal.AutoSize = true;
            this.radioButton_Page00_AppRegistrationPortal.Location = new System.Drawing.Point(18, 86);
            this.radioButton_Page00_AppRegistrationPortal.Name = "radioButton_Page00_AppRegistrationPortal";
            this.radioButton_Page00_AppRegistrationPortal.Size = new System.Drawing.Size(305, 43);
            this.radioButton_Page00_AppRegistrationPortal.TabIndex = 2;
            this.radioButton_Page00_AppRegistrationPortal.TabStop = true;
            this.radioButton_Page00_AppRegistrationPortal.Text = "v2.0 Endpoint\r\nIf you registered your application in App Registration Portal, \r\ns" +
    "elect this option.";
            this.radioButton_Page00_AppRegistrationPortal.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page00_MicrosoftAzurePortal
            // 
            this.radioButton_Page00_MicrosoftAzurePortal.AutoSize = true;
            this.radioButton_Page00_MicrosoftAzurePortal.Checked = true;
            this.radioButton_Page00_MicrosoftAzurePortal.Location = new System.Drawing.Point(18, 37);
            this.radioButton_Page00_MicrosoftAzurePortal.Name = "radioButton_Page00_MicrosoftAzurePortal";
            this.radioButton_Page00_MicrosoftAzurePortal.Size = new System.Drawing.Size(300, 43);
            this.radioButton_Page00_MicrosoftAzurePortal.TabIndex = 1;
            this.radioButton_Page00_MicrosoftAzurePortal.TabStop = true;
            this.radioButton_Page00_MicrosoftAzurePortal.Text = "v1.0 Endpoint\r\nIf you registered your application in Microsoft Azure Portal, \r\nse" +
    "lect this option.";
            this.radioButton_Page00_MicrosoftAzurePortal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Which OAuth endpoint do you use?";
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(174, 187);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(75, 23);
            this.button_Back.TabIndex = 100;
            this.button_Back.Text = "Back";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_Next
            // 
            this.button_Next.Location = new System.Drawing.Point(255, 187);
            this.button_Next.Name = "button_Next";
            this.button_Next.Size = new System.Drawing.Size(75, 23);
            this.button_Next.TabIndex = 101;
            this.button_Next.Text = "Next";
            this.button_Next.UseVisualStyleBackColor = true;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(337, 188);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 102;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // panel_Page01
            // 
            this.panel_Page01.Controls.Add(this.radioButton_Page01_V1AdminConsent);
            this.panel_Page01.Controls.Add(this.radioButton_Page01_V1AppOnlyByKey);
            this.panel_Page01.Controls.Add(this.radioButton_Page01_V1AppOnlyByCert);
            this.panel_Page01.Controls.Add(this.radioButton_Page01_V1Native);
            this.panel_Page01.Controls.Add(this.radioButton_Page01_V1Web);
            this.panel_Page01.Controls.Add(this.label2);
            this.panel_Page01.Location = new System.Drawing.Point(429, 12);
            this.panel_Page01.Name = "panel_Page01";
            this.panel_Page01.Size = new System.Drawing.Size(400, 170);
            this.panel_Page01.TabIndex = 4;
            this.panel_Page01.Tag = "radioButton_Page01_V1Web";
            // 
            // radioButton_Page01_V1AdminConsent
            // 
            this.radioButton_Page01_V1AdminConsent.AutoSize = true;
            this.radioButton_Page01_V1AdminConsent.Location = new System.Drawing.Point(18, 129);
            this.radioButton_Page01_V1AdminConsent.Name = "radioButton_Page01_V1AdminConsent";
            this.radioButton_Page01_V1AdminConsent.Size = new System.Drawing.Size(352, 17);
            this.radioButton_Page01_V1AdminConsent.TabIndex = 5;
            this.radioButton_Page01_V1AdminConsent.TabStop = true;
            this.radioButton_Page01_V1AdminConsent.Text = "I need to complete ADMIN CONSENT before acquiring access token";
            this.radioButton_Page01_V1AdminConsent.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page01_V1AppOnlyByKey
            // 
            this.radioButton_Page01_V1AppOnlyByKey.AutoSize = true;
            this.radioButton_Page01_V1AppOnlyByKey.Location = new System.Drawing.Point(18, 106);
            this.radioButton_Page01_V1AppOnlyByKey.Name = "radioButton_Page01_V1AppOnlyByKey";
            this.radioButton_Page01_V1AppOnlyByKey.Size = new System.Drawing.Size(239, 17);
            this.radioButton_Page01_V1AppOnlyByKey.TabIndex = 4;
            this.radioButton_Page01_V1AppOnlyByKey.TabStop = true;
            this.radioButton_Page01_V1AppOnlyByKey.Text = "Web app / API (Use App Only Token by key)";
            this.radioButton_Page01_V1AppOnlyByKey.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page01_V1AppOnlyByCert
            // 
            this.radioButton_Page01_V1AppOnlyByCert.AutoSize = true;
            this.radioButton_Page01_V1AppOnlyByCert.Location = new System.Drawing.Point(18, 83);
            this.radioButton_Page01_V1AppOnlyByCert.Name = "radioButton_Page01_V1AppOnlyByCert";
            this.radioButton_Page01_V1AppOnlyByCert.Size = new System.Drawing.Size(268, 17);
            this.radioButton_Page01_V1AppOnlyByCert.TabIndex = 3;
            this.radioButton_Page01_V1AppOnlyByCert.TabStop = true;
            this.radioButton_Page01_V1AppOnlyByCert.Text = "Web app / API (Use App Only Token by certificate)";
            this.radioButton_Page01_V1AppOnlyByCert.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page01_V1Native
            // 
            this.radioButton_Page01_V1Native.AutoSize = true;
            this.radioButton_Page01_V1Native.Location = new System.Drawing.Point(18, 60);
            this.radioButton_Page01_V1Native.Name = "radioButton_Page01_V1Native";
            this.radioButton_Page01_V1Native.Size = new System.Drawing.Size(77, 17);
            this.radioButton_Page01_V1Native.TabIndex = 2;
            this.radioButton_Page01_V1Native.TabStop = true;
            this.radioButton_Page01_V1Native.Text = "Native app";
            this.radioButton_Page01_V1Native.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page01_V1Web
            // 
            this.radioButton_Page01_V1Web.AutoSize = true;
            this.radioButton_Page01_V1Web.Checked = true;
            this.radioButton_Page01_V1Web.Location = new System.Drawing.Point(18, 37);
            this.radioButton_Page01_V1Web.Name = "radioButton_Page01_V1Web";
            this.radioButton_Page01_V1Web.Size = new System.Drawing.Size(97, 17);
            this.radioButton_Page01_V1Web.TabIndex = 1;
            this.radioButton_Page01_V1Web.TabStop = true;
            this.radioButton_Page01_V1Web.Text = "Web app / API";
            this.radioButton_Page01_V1Web.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "My application has been registered as the";
            // 
            // panel_Page03
            // 
            this.panel_Page03.Controls.Add(this.comboBox_Page03_Resource);
            this.panel_Page03.Controls.Add(this.linkLabel_Page03_WebApp);
            this.panel_Page03.Controls.Add(this.label4);
            this.panel_Page03.Controls.Add(this.textBox_Page03_ClientSecret);
            this.panel_Page03.Controls.Add(this.label10);
            this.panel_Page03.Controls.Add(this.label5);
            this.panel_Page03.Controls.Add(this.label6);
            this.panel_Page03.Controls.Add(this.textBox_Page03_ClientID);
            this.panel_Page03.Controls.Add(this.textBox_Page03_RedirectUri);
            this.panel_Page03.Controls.Add(this.label3);
            this.panel_Page03.Location = new System.Drawing.Point(12, 239);
            this.panel_Page03.Name = "panel_Page03";
            this.panel_Page03.Size = new System.Drawing.Size(400, 170);
            this.panel_Page03.TabIndex = 6;
            this.panel_Page03.Tag = "textBox_Page03_ClientID";
            // 
            // comboBox_Page03_Resource
            // 
            this.comboBox_Page03_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Page03_Resource.FormattingEnabled = true;
            this.comboBox_Page03_Resource.Location = new System.Drawing.Point(102, 84);
            this.comboBox_Page03_Resource.Name = "comboBox_Page03_Resource";
            this.comboBox_Page03_Resource.Size = new System.Drawing.Size(295, 21);
            this.comboBox_Page03_Resource.TabIndex = 4;
            // 
            // linkLabel_Page03_WebApp
            // 
            this.linkLabel_Page03_WebApp.AutoSize = true;
            this.linkLabel_Page03_WebApp.Location = new System.Drawing.Point(102, 10);
            this.linkLabel_Page03_WebApp.Name = "linkLabel_Page03_WebApp";
            this.linkLabel_Page03_WebApp.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_Page03_WebApp.TabIndex = 1;
            this.linkLabel_Page03_WebApp.TabStop = true;
            this.linkLabel_Page03_WebApp.Text = "How to register applications.";
            this.linkLabel_Page03_WebApp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Page03_WebApp_LinkClicked);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Application ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page03_ClientSecret
            // 
            this.textBox_Page03_ClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page03_ClientSecret.Location = new System.Drawing.Point(102, 110);
            this.textBox_Page03_ClientSecret.Name = "textBox_Page03_ClientSecret";
            this.textBox_Page03_ClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page03_ClientSecret.TabIndex = 5;
            this.textBox_Page03_ClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientSecret;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(3, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "Reply URL";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(3, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Key";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(3, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Resource";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page03_ClientID
            // 
            this.textBox_Page03_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page03_ClientID.Location = new System.Drawing.Point(102, 32);
            this.textBox_Page03_ClientID.Name = "textBox_Page03_ClientID";
            this.textBox_Page03_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page03_ClientID.TabIndex = 2;
            this.textBox_Page03_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppClientID;
            // 
            // textBox_Page03_RedirectUri
            // 
            this.textBox_Page03_RedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page03_RedirectUri.Location = new System.Drawing.Point(102, 58);
            this.textBox_Page03_RedirectUri.Name = "textBox_Page03_RedirectUri";
            this.textBox_Page03_RedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page03_RedirectUri.TabIndex = 3;
            this.textBox_Page03_RedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppRedirectURL;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Fill out the form.";
            // 
            // panel_Page02
            // 
            this.panel_Page02.Controls.Add(this.radioButton_Page02_V2Mobile);
            this.panel_Page02.Controls.Add(this.radioButton_Page02_V2Web);
            this.panel_Page02.Controls.Add(this.label7);
            this.panel_Page02.Location = new System.Drawing.Point(835, 12);
            this.panel_Page02.Name = "panel_Page02";
            this.panel_Page02.Size = new System.Drawing.Size(400, 170);
            this.panel_Page02.TabIndex = 6;
            this.panel_Page02.Tag = "radioButton_Page02_V2Web";
            // 
            // radioButton_Page02_V2Mobile
            // 
            this.radioButton_Page02_V2Mobile.AutoSize = true;
            this.radioButton_Page02_V2Mobile.Location = new System.Drawing.Point(18, 60);
            this.radioButton_Page02_V2Mobile.Name = "radioButton_Page02_V2Mobile";
            this.radioButton_Page02_V2Mobile.Size = new System.Drawing.Size(110, 17);
            this.radioButton_Page02_V2Mobile.TabIndex = 2;
            this.radioButton_Page02_V2Mobile.Text = "Native application";
            this.radioButton_Page02_V2Mobile.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page02_V2Web
            // 
            this.radioButton_Page02_V2Web.AutoSize = true;
            this.radioButton_Page02_V2Web.Checked = true;
            this.radioButton_Page02_V2Web.Location = new System.Drawing.Point(18, 37);
            this.radioButton_Page02_V2Web.Name = "radioButton_Page02_V2Web";
            this.radioButton_Page02_V2Web.Size = new System.Drawing.Size(102, 17);
            this.radioButton_Page02_V2Web.TabIndex = 1;
            this.radioButton_Page02_V2Web.TabStop = true;
            this.radioButton_Page02_V2Web.Text = "Web application";
            this.radioButton_Page02_V2Web.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "My application has been registered as the";
            // 
            // panel_Page04
            // 
            this.panel_Page04.Controls.Add(this.comboBox_Page04_Resource);
            this.panel_Page04.Controls.Add(this.label8);
            this.panel_Page04.Controls.Add(this.linkLabel_Page04_NativeApp);
            this.panel_Page04.Controls.Add(this.label13);
            this.panel_Page04.Controls.Add(this.textBox_Page04_RedirectUri);
            this.panel_Page04.Controls.Add(this.label12);
            this.panel_Page04.Controls.Add(this.label11);
            this.panel_Page04.Controls.Add(this.textBox_Page04_ClientID);
            this.panel_Page04.Controls.Add(this.textBox_Page04_TenantName);
            this.panel_Page04.Controls.Add(this.label14);
            this.panel_Page04.Location = new System.Drawing.Point(429, 239);
            this.panel_Page04.Name = "panel_Page04";
            this.panel_Page04.Size = new System.Drawing.Size(400, 170);
            this.panel_Page04.TabIndex = 7;
            this.panel_Page04.Tag = "textBox_Page04_TenantName";
            // 
            // comboBox_Page04_Resource
            // 
            this.comboBox_Page04_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Page04_Resource.FormattingEnabled = true;
            this.comboBox_Page04_Resource.Location = new System.Drawing.Point(102, 110);
            this.comboBox_Page04_Resource.Name = "comboBox_Page04_Resource";
            this.comboBox_Page04_Resource.Size = new System.Drawing.Size(295, 21);
            this.comboBox_Page04_Resource.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Fill out the form.";
            // 
            // linkLabel_Page04_NativeApp
            // 
            this.linkLabel_Page04_NativeApp.AutoSize = true;
            this.linkLabel_Page04_NativeApp.Location = new System.Drawing.Point(102, 10);
            this.linkLabel_Page04_NativeApp.Name = "linkLabel_Page04_NativeApp";
            this.linkLabel_Page04_NativeApp.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_Page04_NativeApp.TabIndex = 1;
            this.linkLabel_Page04_NativeApp.TabStop = true;
            this.linkLabel_Page04_NativeApp.Text = "How to register applications.";
            this.linkLabel_Page04_NativeApp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Page04_NativeApp_LinkClicked);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(3, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Redirect URI";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page04_RedirectUri
            // 
            this.textBox_Page04_RedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppRedirectURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page04_RedirectUri.Location = new System.Drawing.Point(102, 84);
            this.textBox_Page04_RedirectUri.Name = "textBox_Page04_RedirectUri";
            this.textBox_Page04_RedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page04_RedirectUri.TabIndex = 4;
            this.textBox_Page04_RedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppRedirectURL;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Location = new System.Drawing.Point(3, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Application ID";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Location = new System.Drawing.Point(3, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 42;
            this.label11.Text = "Tenant Name";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page04_ClientID
            // 
            this.textBox_Page04_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page04_ClientID.Location = new System.Drawing.Point(102, 58);
            this.textBox_Page04_ClientID.Name = "textBox_Page04_ClientID";
            this.textBox_Page04_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page04_ClientID.TabIndex = 3;
            this.textBox_Page04_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppClientID;
            // 
            // textBox_Page04_TenantName
            // 
            this.textBox_Page04_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastNativeAppTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page04_TenantName.Location = new System.Drawing.Point(102, 32);
            this.textBox_Page04_TenantName.Name = "textBox_Page04_TenantName";
            this.textBox_Page04_TenantName.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page04_TenantName.TabIndex = 2;
            this.textBox_Page04_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastNativeAppTenantName;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(3, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Resource";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel_Page05
            // 
            this.panel_Page05.Controls.Add(this.linkLabel_linkLabel_Page05_WebAppAppOnly);
            this.panel_Page05.Controls.Add(this.comboBox_Page05_Resource);
            this.panel_Page05.Controls.Add(this.label16);
            this.panel_Page05.Controls.Add(this.button_Page05_SelectCert);
            this.panel_Page05.Controls.Add(this.label36);
            this.panel_Page05.Controls.Add(this.textBox_Page05_CertPath);
            this.panel_Page05.Controls.Add(this.label32);
            this.panel_Page05.Controls.Add(this.textBox_Page05_CertPass);
            this.panel_Page05.Controls.Add(this.label33);
            this.panel_Page05.Controls.Add(this.label34);
            this.panel_Page05.Controls.Add(this.textBox_Page05_ClientID);
            this.panel_Page05.Controls.Add(this.textBox_Page05_TenantName);
            this.panel_Page05.Controls.Add(this.label35);
            this.panel_Page05.Location = new System.Drawing.Point(835, 239);
            this.panel_Page05.Name = "panel_Page05";
            this.panel_Page05.Size = new System.Drawing.Size(400, 170);
            this.panel_Page05.TabIndex = 8;
            this.panel_Page05.Tag = "textBox_Page05_TenantName";
            // 
            // linkLabel_linkLabel_Page05_WebAppAppOnly
            // 
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.AutoSize = true;
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.Location = new System.Drawing.Point(102, 10);
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.Name = "linkLabel_linkLabel_Page05_WebAppAppOnly";
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.TabIndex = 1;
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.TabStop = true;
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.Text = "How to register applications.";
            this.linkLabel_linkLabel_Page05_WebAppAppOnly.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Page05_WebAppAppOnly_LinkClicked);
            // 
            // comboBox_Page05_Resource
            // 
            this.comboBox_Page05_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Page05_Resource.FormattingEnabled = true;
            this.comboBox_Page05_Resource.Location = new System.Drawing.Point(102, 136);
            this.comboBox_Page05_Resource.Name = "comboBox_Page05_Resource";
            this.comboBox_Page05_Resource.Size = new System.Drawing.Size(295, 21);
            this.comboBox_Page05_Resource.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 13);
            this.label16.TabIndex = 59;
            this.label16.Text = "Fill out the form.";
            // 
            // button_Page05_SelectCert
            // 
            this.button_Page05_SelectCert.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Page05_SelectCert.Location = new System.Drawing.Point(320, 82);
            this.button_Page05_SelectCert.Name = "button_Page05_SelectCert";
            this.button_Page05_SelectCert.Size = new System.Drawing.Size(77, 23);
            this.button_Page05_SelectCert.TabIndex = 5;
            this.button_Page05_SelectCert.Text = "Browse...";
            this.button_Page05_SelectCert.UseVisualStyleBackColor = true;
            this.button_Page05_SelectCert.Click += new System.EventHandler(this.button_Page05_SelectCert_Click);
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.Location = new System.Drawing.Point(3, 87);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(93, 13);
            this.label36.TabIndex = 58;
            this.label36.Text = "Certificate Path";
            this.label36.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page05_CertPath
            // 
            this.textBox_Page05_CertPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyCertPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page05_CertPath.Location = new System.Drawing.Point(102, 84);
            this.textBox_Page05_CertPath.Name = "textBox_Page05_CertPath";
            this.textBox_Page05_CertPath.Size = new System.Drawing.Size(212, 20);
            this.textBox_Page05_CertPath.TabIndex = 4;
            this.textBox_Page05_CertPath.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyCertPath;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.Location = new System.Drawing.Point(3, 113);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(93, 13);
            this.label32.TabIndex = 57;
            this.label32.Text = "Password for cert";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page05_CertPass
            // 
            this.textBox_Page05_CertPass.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyCertPass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page05_CertPass.Location = new System.Drawing.Point(102, 110);
            this.textBox_Page05_CertPass.Name = "textBox_Page05_CertPass";
            this.textBox_Page05_CertPass.PasswordChar = '*';
            this.textBox_Page05_CertPass.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page05_CertPass.TabIndex = 6;
            this.textBox_Page05_CertPass.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyCertPass;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.Location = new System.Drawing.Point(3, 61);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 13);
            this.label33.TabIndex = 56;
            this.label33.Text = "Application ID";
            this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.Location = new System.Drawing.Point(3, 35);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(93, 13);
            this.label34.TabIndex = 55;
            this.label34.Text = "Tenant Name";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page05_ClientID
            // 
            this.textBox_Page05_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page05_ClientID.Location = new System.Drawing.Point(102, 58);
            this.textBox_Page05_ClientID.Name = "textBox_Page05_ClientID";
            this.textBox_Page05_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page05_ClientID.TabIndex = 3;
            this.textBox_Page05_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyClientID;
            // 
            // textBox_Page05_TenantName
            // 
            this.textBox_Page05_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page05_TenantName.Location = new System.Drawing.Point(102, 32);
            this.textBox_Page05_TenantName.Name = "textBox_Page05_TenantName";
            this.textBox_Page05_TenantName.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page05_TenantName.TabIndex = 2;
            this.textBox_Page05_TenantName.Tag = "";
            this.textBox_Page05_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyTenantName;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.Location = new System.Drawing.Point(3, 139);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(93, 13);
            this.label35.TabIndex = 54;
            this.label35.Text = "Resource";
            this.label35.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel_Page06
            // 
            this.panel_Page06.Controls.Add(this.label9);
            this.panel_Page06.Controls.Add(this.textBox_Page06_ClientSecret);
            this.panel_Page06.Controls.Add(this.label26);
            this.panel_Page06.Controls.Add(this.button_Page06_ScopeEditor);
            this.panel_Page06.Controls.Add(this.textBox_Page06_Scopes);
            this.panel_Page06.Controls.Add(this.textBox_Page06_ClientID);
            this.panel_Page06.Controls.Add(this.label29);
            this.panel_Page06.Controls.Add(this.label30);
            this.panel_Page06.Controls.Add(this.label31);
            this.panel_Page06.Controls.Add(this.textBox_Page06_RedirectUri);
            this.panel_Page06.Location = new System.Drawing.Point(12, 415);
            this.panel_Page06.Name = "panel_Page06";
            this.panel_Page06.Size = new System.Drawing.Size(400, 170);
            this.panel_Page06.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Fill out the form.";
            // 
            // textBox_Page06_ClientSecret
            // 
            this.textBox_Page06_ClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page06_ClientSecret.Location = new System.Drawing.Point(102, 144);
            this.textBox_Page06_ClientSecret.Name = "textBox_Page06_ClientSecret";
            this.textBox_Page06_ClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page06_ClientSecret.TabIndex = 5;
            this.textBox_Page06_ClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppClientSecret;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.Location = new System.Drawing.Point(3, 147);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(93, 13);
            this.label26.TabIndex = 45;
            this.label26.Text = "Password";
            this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button_Page06_ScopeEditor
            // 
            this.button_Page06_ScopeEditor.Location = new System.Drawing.Point(102, 114);
            this.button_Page06_ScopeEditor.Name = "button_Page06_ScopeEditor";
            this.button_Page06_ScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_Page06_ScopeEditor.TabIndex = 4;
            this.button_Page06_ScopeEditor.Text = "Scope editor...";
            this.button_Page06_ScopeEditor.UseVisualStyleBackColor = true;
            this.button_Page06_ScopeEditor.Click += new System.EventHandler(this.button_Page06_ScopeEditor_Click);
            // 
            // textBox_Page06_Scopes
            // 
            this.textBox_Page06_Scopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page06_Scopes.Location = new System.Drawing.Point(102, 89);
            this.textBox_Page06_Scopes.Name = "textBox_Page06_Scopes";
            this.textBox_Page06_Scopes.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page06_Scopes.TabIndex = 3;
            this.textBox_Page06_Scopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppScopes;
            // 
            // textBox_Page06_ClientID
            // 
            this.textBox_Page06_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page06_ClientID.Location = new System.Drawing.Point(102, 36);
            this.textBox_Page06_ClientID.Name = "textBox_Page06_ClientID";
            this.textBox_Page06_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page06_ClientID.TabIndex = 1;
            this.textBox_Page06_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppClientID;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.Location = new System.Drawing.Point(3, 39);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(93, 13);
            this.label29.TabIndex = 38;
            this.label29.Text = "Application ID";
            this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.Location = new System.Drawing.Point(3, 65);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(93, 13);
            this.label30.TabIndex = 37;
            this.label30.Text = "Redirect URL";
            this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.Location = new System.Drawing.Point(3, 92);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(93, 13);
            this.label31.TabIndex = 36;
            this.label31.Text = "Scopes";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page06_RedirectUri
            // 
            this.textBox_Page06_RedirectUri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppRedirectUri", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page06_RedirectUri.Location = new System.Drawing.Point(102, 62);
            this.textBox_Page06_RedirectUri.Name = "textBox_Page06_RedirectUri";
            this.textBox_Page06_RedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page06_RedirectUri.TabIndex = 2;
            this.textBox_Page06_RedirectUri.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppRedirectUri;
            // 
            // panel_Page07
            // 
            this.panel_Page07.Controls.Add(this.label15);
            this.panel_Page07.Controls.Add(this.button_Page07_ScopeEditor);
            this.panel_Page07.Controls.Add(this.textBox_Page07_Scopes);
            this.panel_Page07.Controls.Add(this.textBox_Page07_ClientID);
            this.panel_Page07.Controls.Add(this.label22);
            this.panel_Page07.Controls.Add(this.label23);
            this.panel_Page07.Controls.Add(this.label25);
            this.panel_Page07.Controls.Add(this.textBox_Page07_RedirectUri);
            this.panel_Page07.Location = new System.Drawing.Point(429, 415);
            this.panel_Page07.Name = "panel_Page07";
            this.panel_Page07.Size = new System.Drawing.Size(400, 170);
            this.panel_Page07.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 48;
            this.label15.Text = "Fill out the form.";
            // 
            // button_Page07_ScopeEditor
            // 
            this.button_Page07_ScopeEditor.Location = new System.Drawing.Point(102, 114);
            this.button_Page07_ScopeEditor.Name = "button_Page07_ScopeEditor";
            this.button_Page07_ScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_Page07_ScopeEditor.TabIndex = 4;
            this.button_Page07_ScopeEditor.Text = "Scope editor...";
            this.button_Page07_ScopeEditor.UseVisualStyleBackColor = true;
            this.button_Page07_ScopeEditor.Click += new System.EventHandler(this.button_Page07_ScopeEditor_Click);
            // 
            // textBox_Page07_Scopes
            // 
            this.textBox_Page07_Scopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2MobileAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page07_Scopes.Location = new System.Drawing.Point(102, 88);
            this.textBox_Page07_Scopes.Name = "textBox_Page07_Scopes";
            this.textBox_Page07_Scopes.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page07_Scopes.TabIndex = 3;
            this.textBox_Page07_Scopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2MobileAppScopes;
            // 
            // textBox_Page07_ClientID
            // 
            this.textBox_Page07_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2MobileAppClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page07_ClientID.Location = new System.Drawing.Point(102, 36);
            this.textBox_Page07_ClientID.Name = "textBox_Page07_ClientID";
            this.textBox_Page07_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page07_ClientID.TabIndex = 1;
            this.textBox_Page07_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2MobileAppClientID;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.Location = new System.Drawing.Point(3, 39);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(93, 13);
            this.label22.TabIndex = 42;
            this.label22.Text = "Application ID";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Location = new System.Drawing.Point(3, 65);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(93, 13);
            this.label23.TabIndex = 41;
            this.label23.Text = "Redirect URI";
            this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.Location = new System.Drawing.Point(3, 91);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(93, 13);
            this.label25.TabIndex = 40;
            this.label25.Text = "Scopes";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page07_RedirectUri
            // 
            this.textBox_Page07_RedirectUri.Enabled = false;
            this.textBox_Page07_RedirectUri.Location = new System.Drawing.Point(102, 62);
            this.textBox_Page07_RedirectUri.Name = "textBox_Page07_RedirectUri";
            this.textBox_Page07_RedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page07_RedirectUri.TabIndex = 2;
            this.textBox_Page07_RedirectUri.Text = "urn:ietf:wg:oauth:2.0:oob";
            // 
            // panel_Page08
            // 
            this.panel_Page08.Controls.Add(this.linkLabel_Page08_WebAppAppOnlyByKey);
            this.panel_Page08.Controls.Add(this.comboBox_Page08_Resource);
            this.panel_Page08.Controls.Add(this.label17);
            this.panel_Page08.Controls.Add(this.textBox_Page08_ClientSecret);
            this.panel_Page08.Controls.Add(this.label18);
            this.panel_Page08.Controls.Add(this.label19);
            this.panel_Page08.Controls.Add(this.label20);
            this.panel_Page08.Controls.Add(this.textBox_Page08_TenantName);
            this.panel_Page08.Controls.Add(this.textBox_Page08_ClientID);
            this.panel_Page08.Controls.Add(this.label21);
            this.panel_Page08.Location = new System.Drawing.Point(835, 415);
            this.panel_Page08.Name = "panel_Page08";
            this.panel_Page08.Size = new System.Drawing.Size(400, 170);
            this.panel_Page08.TabIndex = 8;
            // 
            // linkLabel_Page08_WebAppAppOnlyByKey
            // 
            this.linkLabel_Page08_WebAppAppOnlyByKey.AutoSize = true;
            this.linkLabel_Page08_WebAppAppOnlyByKey.Location = new System.Drawing.Point(102, 10);
            this.linkLabel_Page08_WebAppAppOnlyByKey.Name = "linkLabel_Page08_WebAppAppOnlyByKey";
            this.linkLabel_Page08_WebAppAppOnlyByKey.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_Page08_WebAppAppOnlyByKey.TabIndex = 43;
            this.linkLabel_Page08_WebAppAppOnlyByKey.TabStop = true;
            this.linkLabel_Page08_WebAppAppOnlyByKey.Text = "How to register applications.";
            this.linkLabel_Page08_WebAppAppOnlyByKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Page08_WebAppAppOnlyByKey_LinkClicked);
            // 
            // comboBox_Page08_Resource
            // 
            this.comboBox_Page08_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Page08_Resource.FormattingEnabled = true;
            this.comboBox_Page08_Resource.Location = new System.Drawing.Point(102, 88);
            this.comboBox_Page08_Resource.Name = "comboBox_Page08_Resource";
            this.comboBox_Page08_Resource.Size = new System.Drawing.Size(295, 21);
            this.comboBox_Page08_Resource.TabIndex = 48;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Location = new System.Drawing.Point(3, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 13);
            this.label17.TabIndex = 52;
            this.label17.Text = "Tenant Name";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page08_ClientSecret
            // 
            this.textBox_Page08_ClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyByKeyClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page08_ClientSecret.Location = new System.Drawing.Point(102, 115);
            this.textBox_Page08_ClientSecret.Name = "textBox_Page08_ClientSecret";
            this.textBox_Page08_ClientSecret.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page08_ClientSecret.TabIndex = 49;
            this.textBox_Page08_ClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyByKeyClientSecret;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.Location = new System.Drawing.Point(3, 65);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 13);
            this.label18.TabIndex = 51;
            this.label18.Text = "Application ID";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.Location = new System.Drawing.Point(3, 118);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(93, 13);
            this.label19.TabIndex = 53;
            this.label19.Text = "Key";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Location = new System.Drawing.Point(3, 91);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "Resource";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page08_TenantName
            // 
            this.textBox_Page08_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyByKeyTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page08_TenantName.Location = new System.Drawing.Point(102, 36);
            this.textBox_Page08_TenantName.Name = "textBox_Page08_TenantName";
            this.textBox_Page08_TenantName.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page08_TenantName.TabIndex = 46;
            this.textBox_Page08_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyByKeyTenantName;
            // 
            // textBox_Page08_ClientID
            // 
            this.textBox_Page08_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyByKeyClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page08_ClientID.Location = new System.Drawing.Point(102, 62);
            this.textBox_Page08_ClientID.Name = "textBox_Page08_ClientID";
            this.textBox_Page08_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page08_ClientID.TabIndex = 47;
            this.textBox_Page08_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyByKeyClientID;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 10);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 13);
            this.label21.TabIndex = 44;
            this.label21.Text = "Fill out the form.";
            // 
            // openFileDialog_PFX
            // 
            this.openFileDialog_PFX.Filter = "Personal Information Exchange (*.pfx)|*.pfx";
            // 
            // panel_Page09
            // 
            this.panel_Page09.Controls.Add(this.comboBox_Page09_Resource);
            this.panel_Page09.Controls.Add(this.label24);
            this.panel_Page09.Controls.Add(this.label27);
            this.panel_Page09.Controls.Add(this.label37);
            this.panel_Page09.Controls.Add(this.textBox_Page09_ClientID);
            this.panel_Page09.Controls.Add(this.textBox_Page09_RedirectUri);
            this.panel_Page09.Controls.Add(this.label38);
            this.panel_Page09.Location = new System.Drawing.Point(12, 591);
            this.panel_Page09.Name = "panel_Page09";
            this.panel_Page09.Size = new System.Drawing.Size(400, 170);
            this.panel_Page09.TabIndex = 44;
            this.panel_Page09.Tag = "textBox_Page03_ClientID";
            // 
            // comboBox_Page09_Resource
            // 
            this.comboBox_Page09_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Page09_Resource.FormattingEnabled = true;
            this.comboBox_Page09_Resource.Location = new System.Drawing.Point(102, 84);
            this.comboBox_Page09_Resource.Name = "comboBox_Page09_Resource";
            this.comboBox_Page09_Resource.Size = new System.Drawing.Size(295, 21);
            this.comboBox_Page09_Resource.TabIndex = 4;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.Location = new System.Drawing.Point(3, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(93, 13);
            this.label24.TabIndex = 42;
            this.label24.Text = "Application ID";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.Location = new System.Drawing.Point(3, 61);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 13);
            this.label27.TabIndex = 41;
            this.label27.Text = "Reply URL";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.Location = new System.Drawing.Point(3, 87);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(93, 13);
            this.label37.TabIndex = 40;
            this.label37.Text = "Resource";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_Page09_ClientID
            // 
            this.textBox_Page09_ClientID.Location = new System.Drawing.Point(102, 32);
            this.textBox_Page09_ClientID.Name = "textBox_Page09_ClientID";
            this.textBox_Page09_ClientID.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page09_ClientID.TabIndex = 2;
            // 
            // textBox_Page09_RedirectUri
            // 
            this.textBox_Page09_RedirectUri.Location = new System.Drawing.Point(102, 58);
            this.textBox_Page09_RedirectUri.Name = "textBox_Page09_RedirectUri";
            this.textBox_Page09_RedirectUri.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page09_RedirectUri.TabIndex = 3;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(15, 10);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(81, 13);
            this.label38.TabIndex = 0;
            this.label38.Text = "Fill out the form.";
            // 
            // panel_Page10
            // 
            this.panel_Page10.Controls.Add(this.radioButton_Page10_BasicAuth);
            this.panel_Page10.Controls.Add(this.radioButton_Page10_BuiltInApp);
            this.panel_Page10.Controls.Add(this.label28);
            this.panel_Page10.Location = new System.Drawing.Point(429, 591);
            this.panel_Page10.Name = "panel_Page10";
            this.panel_Page10.Size = new System.Drawing.Size(400, 170);
            this.panel_Page10.TabIndex = 103;
            this.panel_Page10.Tag = "radioButton_Page02_V2Web";
            // 
            // radioButton_Page10_BasicAuth
            // 
            this.radioButton_Page10_BasicAuth.AutoSize = true;
            this.radioButton_Page10_BasicAuth.Location = new System.Drawing.Point(18, 60);
            this.radioButton_Page10_BasicAuth.Name = "radioButton_Page10_BasicAuth";
            this.radioButton_Page10_BasicAuth.Size = new System.Drawing.Size(75, 17);
            this.radioButton_Page10_BasicAuth.TabIndex = 2;
            this.radioButton_Page10_BasicAuth.Text = "Basic auth";
            this.radioButton_Page10_BasicAuth.UseVisualStyleBackColor = true;
            // 
            // radioButton_Page10_BuiltInApp
            // 
            this.radioButton_Page10_BuiltInApp.AutoSize = true;
            this.radioButton_Page10_BuiltInApp.Checked = true;
            this.radioButton_Page10_BuiltInApp.Location = new System.Drawing.Point(18, 37);
            this.radioButton_Page10_BuiltInApp.Name = "radioButton_Page10_BuiltInApp";
            this.radioButton_Page10_BuiltInApp.Size = new System.Drawing.Size(202, 17);
            this.radioButton_Page10_BuiltInApp.TabIndex = 1;
            this.radioButton_Page10_BuiltInApp.TabStop = true;
            this.radioButton_Page10_BuiltInApp.Text = "Office365APIEditor built-in application";
            this.radioButton_Page10_BuiltInApp.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(15, 10);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(137, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "Which do you want to use?";
            // 
            // panel_Page11
            // 
            this.panel_Page11.Controls.Add(this.label39);
            this.panel_Page11.Controls.Add(this.button_Page11_ScopeEditor);
            this.panel_Page11.Controls.Add(this.textBox_Page11_Scopes);
            this.panel_Page11.Controls.Add(this.label42);
            this.panel_Page11.Location = new System.Drawing.Point(835, 591);
            this.panel_Page11.Name = "panel_Page11";
            this.panel_Page11.Size = new System.Drawing.Size(400, 170);
            this.panel_Page11.TabIndex = 49;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(15, 10);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(235, 13);
            this.label39.TabIndex = 48;
            this.label39.Text = "Select scopes you need for the Microsoft Graph.";
            // 
            // button_Page11_ScopeEditor
            // 
            this.button_Page11_ScopeEditor.Location = new System.Drawing.Point(102, 56);
            this.button_Page11_ScopeEditor.Name = "button_Page11_ScopeEditor";
            this.button_Page11_ScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_Page11_ScopeEditor.TabIndex = 4;
            this.button_Page11_ScopeEditor.Text = "Scope editor...";
            this.button_Page11_ScopeEditor.UseVisualStyleBackColor = true;
            this.button_Page11_ScopeEditor.Click += new System.EventHandler(this.button_Page11_ScopeEditor_Click);
            // 
            // textBox_Page11_Scopes
            // 
            this.textBox_Page11_Scopes.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastBuiltInAppScopes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Page11_Scopes.Location = new System.Drawing.Point(102, 32);
            this.textBox_Page11_Scopes.Name = "textBox_Page11_Scopes";
            this.textBox_Page11_Scopes.Size = new System.Drawing.Size(295, 20);
            this.textBox_Page11_Scopes.TabIndex = 3;
            this.textBox_Page11_Scopes.Text = global::Office365APIEditor.Properties.Settings.Default.LastBuiltInAppScopes;
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.Location = new System.Drawing.Point(3, 35);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(93, 13);
            this.label42.TabIndex = 40;
            this.label42.Text = "Scopes";
            this.label42.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AccessTokenWizard
            // 
            this.AcceptButton = this.button_Next;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(1243, 873);
            this.Controls.Add(this.panel_Page11);
            this.Controls.Add(this.panel_Page10);
            this.Controls.Add(this.panel_Page09);
            this.Controls.Add(this.panel_Page08);
            this.Controls.Add(this.panel_Page07);
            this.Controls.Add(this.panel_Page06);
            this.Controls.Add(this.panel_Page05);
            this.Controls.Add(this.panel_Page04);
            this.Controls.Add(this.panel_Page02);
            this.Controls.Add(this.panel_Page03);
            this.Controls.Add(this.panel_Page01);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.button_Next);
            this.Controls.Add(this.panel_Page00);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccessTokenWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Office365APIEditor - Access Token Wizard";
            this.Load += new System.EventHandler(this.AccessTokenWizard_Load);
            this.panel_Page00.ResumeLayout(false);
            this.panel_Page00.PerformLayout();
            this.panel_Page01.ResumeLayout(false);
            this.panel_Page01.PerformLayout();
            this.panel_Page03.ResumeLayout(false);
            this.panel_Page03.PerformLayout();
            this.panel_Page02.ResumeLayout(false);
            this.panel_Page02.PerformLayout();
            this.panel_Page04.ResumeLayout(false);
            this.panel_Page04.PerformLayout();
            this.panel_Page05.ResumeLayout(false);
            this.panel_Page05.PerformLayout();
            this.panel_Page06.ResumeLayout(false);
            this.panel_Page06.PerformLayout();
            this.panel_Page07.ResumeLayout(false);
            this.panel_Page07.PerformLayout();
            this.panel_Page08.ResumeLayout(false);
            this.panel_Page08.PerformLayout();
            this.panel_Page09.ResumeLayout(false);
            this.panel_Page09.PerformLayout();
            this.panel_Page10.ResumeLayout(false);
            this.panel_Page10.PerformLayout();
            this.panel_Page11.ResumeLayout(false);
            this.panel_Page11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Page00;
        private System.Windows.Forms.RadioButton radioButton_Page00_BuiltInAppOrBasicAuth;
        private System.Windows.Forms.RadioButton radioButton_Page00_AppRegistrationPortal;
        private System.Windows.Forms.RadioButton radioButton_Page00_MicrosoftAzurePortal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_Next;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Panel panel_Page01;
        private System.Windows.Forms.RadioButton radioButton_Page01_V1Native;
        private System.Windows.Forms.RadioButton radioButton_Page01_V1Web;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_Page03;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel_Page03_WebApp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Page03_ClientSecret;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Page03_ClientID;
        private System.Windows.Forms.TextBox textBox_Page03_RedirectUri;
        private System.Windows.Forms.ComboBox comboBox_Page03_Resource;
        private System.Windows.Forms.Panel panel_Page02;
        private System.Windows.Forms.RadioButton radioButton_Page02_V2Mobile;
        private System.Windows.Forms.RadioButton radioButton_Page02_V2Web;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_Page04;
        private System.Windows.Forms.Panel panel_Page05;
        private System.Windows.Forms.Panel panel_Page06;
        private System.Windows.Forms.Panel panel_Page07;
        private System.Windows.Forms.Panel panel_Page08;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel linkLabel_Page04_NativeApp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_Page04_RedirectUri;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Page04_ClientID;
        private System.Windows.Forms.TextBox textBox_Page04_TenantName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBox_Page04_Resource;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_Page06_ClientSecret;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button button_Page06_ScopeEditor;
        private System.Windows.Forms.TextBox textBox_Page06_Scopes;
        private System.Windows.Forms.TextBox textBox_Page06_ClientID;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox_Page06_RedirectUri;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_Page07_ScopeEditor;
        private System.Windows.Forms.TextBox textBox_Page07_Scopes;
        private System.Windows.Forms.TextBox textBox_Page07_ClientID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox_Page07_RedirectUri;
        private System.Windows.Forms.ComboBox comboBox_Page05_Resource;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_Page05_SelectCert;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox_Page05_CertPath;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox textBox_Page05_CertPass;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBox_Page05_ClientID;
        private System.Windows.Forms.TextBox textBox_Page05_TenantName;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.OpenFileDialog openFileDialog_PFX;
        private System.Windows.Forms.RadioButton radioButton_Page01_V1AppOnlyByCert;
        private System.Windows.Forms.LinkLabel linkLabel_linkLabel_Page05_WebAppAppOnly;
        private System.Windows.Forms.RadioButton radioButton_Page01_V1AppOnlyByKey;
        private System.Windows.Forms.ComboBox comboBox_Page08_Resource;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_Page08_ClientSecret;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_Page08_TenantName;
        private System.Windows.Forms.TextBox textBox_Page08_ClientID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RadioButton radioButton_Page01_V1AdminConsent;
        private System.Windows.Forms.Panel panel_Page09;
        private System.Windows.Forms.ComboBox comboBox_Page09_Resource;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBox_Page09_ClientID;
        private System.Windows.Forms.TextBox textBox_Page09_RedirectUri;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.LinkLabel linkLabel_Page08_WebAppAppOnlyByKey;
        private System.Windows.Forms.Panel panel_Page10;
        private System.Windows.Forms.RadioButton radioButton_Page10_BasicAuth;
        private System.Windows.Forms.RadioButton radioButton_Page10_BuiltInApp;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel_Page11;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button button_Page11_ScopeEditor;
        private System.Windows.Forms.TextBox textBox_Page11_Scopes;
        private System.Windows.Forms.Label label42;
    }
}