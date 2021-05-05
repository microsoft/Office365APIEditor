namespace Office365APIEditor
{
    partial class RequestFormOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestFormOptionForm));
            this.button_LogFolderPathBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.radioButton_DateTime = new System.Windows.Forms.RadioButton();
            this.radioButton_Static = new System.Windows.Forms.RadioButton();
            this.textBox_LogFolderPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_AllowAutoRedirect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CustomUserAgent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_CustomUserAgentStyle = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_ReplacePlusSignInTheRequestURL = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox_ReplaceSharpSignInTheRequestURL = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox_SavedAppNote = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_SavedAppScopes = new System.Windows.Forms.TextBox();
            this.button_SavedAppSelectCert = new System.Windows.Forms.Button();
            this.textBox_SavedAppCertPath = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppCertPass = new System.Windows.Forms.TextBox();
            this.comboBox_SavedAppResource = new System.Windows.Forms.ComboBox();
            this.button_SavedAppScopeEditor = new System.Windows.Forms.Button();
            this.textBox_SavedAppClientSecret = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppRedirectUri = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppTenantName = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppApplicationId = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppDisplayName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_RemoveApp = new System.Windows.Forms.Button();
            this.button_AddApp = new System.Windows.Forms.Button();
            this.listBox_SavedApps = new System.Windows.Forms.ListBox();
            this.openFileDialog_SavedAppPFX = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_LogFolderPathBrowse
            // 
            this.button_LogFolderPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LogFolderPathBrowse.Location = new System.Drawing.Point(641, 19);
            this.button_LogFolderPathBrowse.Name = "button_LogFolderPathBrowse";
            this.button_LogFolderPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_LogFolderPathBrowse.TabIndex = 1;
            this.button_LogFolderPathBrowse.Text = "Browse";
            this.button_LogFolderPathBrowse.UseVisualStyleBackColor = true;
            this.button_LogFolderPathBrowse.Click += new System.EventHandler(this.button_LogFolderPathBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Log folder path :";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(660, 315);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 99;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(579, 315);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 98;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // radioButton_DateTime
            // 
            this.radioButton_DateTime.AutoSize = true;
            this.radioButton_DateTime.Location = new System.Drawing.Point(6, 42);
            this.radioButton_DateTime.Name = "radioButton_DateTime";
            this.radioButton_DateTime.Size = new System.Drawing.Size(71, 17);
            this.radioButton_DateTime.TabIndex = 4;
            this.radioButton_DateTime.TabStop = true;
            this.radioButton_DateTime.Text = "DateTime";
            this.radioButton_DateTime.UseVisualStyleBackColor = true;
            // 
            // radioButton_Static
            // 
            this.radioButton_Static.AutoSize = true;
            this.radioButton_Static.Location = new System.Drawing.Point(6, 19);
            this.radioButton_Static.Name = "radioButton_Static";
            this.radioButton_Static.Size = new System.Drawing.Size(52, 17);
            this.radioButton_Static.TabIndex = 3;
            this.radioButton_Static.TabStop = true;
            this.radioButton_Static.Text = "Static";
            this.radioButton_Static.UseVisualStyleBackColor = true;
            // 
            // textBox_LogFolderPath
            // 
            this.textBox_LogFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_LogFolderPath.Location = new System.Drawing.Point(9, 21);
            this.textBox_LogFolderPath.Name = "textBox_LogFolderPath";
            this.textBox_LogFolderPath.Size = new System.Drawing.Size(626, 20);
            this.textBox_LogFolderPath.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton_Static);
            this.groupBox1.Controls.Add(this.radioButton_DateTime);
            this.groupBox1.Location = new System.Drawing.Point(9, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 68);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log file name format";
            // 
            // checkBox_AllowAutoRedirect
            // 
            this.checkBox_AllowAutoRedirect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_AllowAutoRedirect.AutoSize = true;
            this.checkBox_AllowAutoRedirect.Location = new System.Drawing.Point(9, 122);
            this.checkBox_AllowAutoRedirect.Name = "checkBox_AllowAutoRedirect";
            this.checkBox_AllowAutoRedirect.Size = new System.Drawing.Size(113, 17);
            this.checkBox_AllowAutoRedirect.TabIndex = 5;
            this.checkBox_AllowAutoRedirect.Text = "Allow auto redirect";
            this.checkBox_AllowAutoRedirect.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "UserAgent value : ";
            // 
            // textBox_CustomUserAgent
            // 
            this.textBox_CustomUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CustomUserAgent.Location = new System.Drawing.Point(107, 46);
            this.textBox_CustomUserAgent.Name = "textBox_CustomUserAgent";
            this.textBox_CustomUserAgent.Size = new System.Drawing.Size(589, 20);
            this.textBox_CustomUserAgent.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Style : ";
            // 
            // comboBox_CustomUserAgentStyle
            // 
            this.comboBox_CustomUserAgentStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_CustomUserAgentStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CustomUserAgentStyle.FormattingEnabled = true;
            this.comboBox_CustomUserAgentStyle.Location = new System.Drawing.Point(107, 19);
            this.comboBox_CustomUserAgentStyle.Name = "comboBox_CustomUserAgentStyle";
            this.comboBox_CustomUserAgentStyle.Size = new System.Drawing.Size(589, 21);
            this.comboBox_CustomUserAgentStyle.TabIndex = 7;
            this.comboBox_CustomUserAgentStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_CustomUserAgentStyle_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBox_CustomUserAgentStyle);
            this.groupBox2.Controls.Add(this.textBox_CustomUserAgent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(707, 101);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UserAgent format";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "* This is a preview feature.";
            // 
            // checkBox_ReplacePlusSignInTheRequestURL
            // 
            this.checkBox_ReplacePlusSignInTheRequestURL.AutoSize = true;
            this.checkBox_ReplacePlusSignInTheRequestURL.Location = new System.Drawing.Point(6, 6);
            this.checkBox_ReplacePlusSignInTheRequestURL.Name = "checkBox_ReplacePlusSignInTheRequestURL";
            this.checkBox_ReplacePlusSignInTheRequestURL.Size = new System.Drawing.Size(236, 17);
            this.checkBox_ReplacePlusSignInTheRequestURL.TabIndex = 9;
            this.checkBox_ReplacePlusSignInTheRequestURL.Text = "Replace \"+\" in the request URL with \"%2B\".";
            this.checkBox_ReplacePlusSignInTheRequestURL.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 307);
            this.tabControl1.TabIndex = 75;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button_LogFolderPathBrowse);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.textBox_LogFolderPath);
            this.tabPage1.Controls.Add(this.checkBox_AllowAutoRedirect);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(725, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox_ReplaceSharpSignInTheRequestURL);
            this.tabPage2.Controls.Add(this.checkBox_ReplacePlusSignInTheRequestURL);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(725, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Encode";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_ReplaceSharpSignInTheRequestURL
            // 
            this.checkBox_ReplaceSharpSignInTheRequestURL.AutoSize = true;
            this.checkBox_ReplaceSharpSignInTheRequestURL.Location = new System.Drawing.Point(6, 29);
            this.checkBox_ReplaceSharpSignInTheRequestURL.Name = "checkBox_ReplaceSharpSignInTheRequestURL";
            this.checkBox_ReplaceSharpSignInTheRequestURL.Size = new System.Drawing.Size(236, 17);
            this.checkBox_ReplaceSharpSignInTheRequestURL.TabIndex = 10;
            this.checkBox_ReplaceSharpSignInTheRequestURL.Text = "Replace \"#\" in the request URL with \"%23\".";
            this.checkBox_ReplaceSharpSignInTheRequestURL.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox_SavedAppNote);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.textBox_SavedAppScopes);
            this.tabPage3.Controls.Add(this.button_SavedAppSelectCert);
            this.tabPage3.Controls.Add(this.textBox_SavedAppCertPath);
            this.tabPage3.Controls.Add(this.textBox_SavedAppCertPass);
            this.tabPage3.Controls.Add(this.comboBox_SavedAppResource);
            this.tabPage3.Controls.Add(this.button_SavedAppScopeEditor);
            this.tabPage3.Controls.Add(this.textBox_SavedAppClientSecret);
            this.tabPage3.Controls.Add(this.textBox_SavedAppRedirectUri);
            this.tabPage3.Controls.Add(this.textBox_SavedAppTenantName);
            this.tabPage3.Controls.Add(this.textBox_SavedAppApplicationId);
            this.tabPage3.Controls.Add(this.textBox_SavedAppDisplayName);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.button_RemoveApp);
            this.tabPage3.Controls.Add(this.button_AddApp);
            this.tabPage3.Controls.Add(this.listBox_SavedApps);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(725, 281);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "App Library";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox_SavedAppNote
            // 
            this.textBox_SavedAppNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppNote.Location = new System.Drawing.Point(410, 220);
            this.textBox_SavedAppNote.Multiline = true;
            this.textBox_SavedAppNote.Name = "textBox_SavedAppNote";
            this.textBox_SavedAppNote.Size = new System.Drawing.Size(302, 53);
            this.textBox_SavedAppNote.TabIndex = 24;
            this.textBox_SavedAppNote.TextChanged += new System.EventHandler(this.TextBox_SavedAppNote_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(407, 204);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Note";
            // 
            // textBox_SavedAppScopes
            // 
            this.textBox_SavedAppScopes.Location = new System.Drawing.Point(410, 37);
            this.textBox_SavedAppScopes.Name = "textBox_SavedAppScopes";
            this.textBox_SavedAppScopes.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppScopes.TabIndex = 18;
            this.textBox_SavedAppScopes.TextChanged += new System.EventHandler(this.TextBox_SavedAppScopes_TextChanged);
            // 
            // button_SavedAppSelectCert
            // 
            this.button_SavedAppSelectCert.Location = new System.Drawing.Point(616, 125);
            this.button_SavedAppSelectCert.Name = "button_SavedAppSelectCert";
            this.button_SavedAppSelectCert.Size = new System.Drawing.Size(77, 23);
            this.button_SavedAppSelectCert.TabIndex = 22;
            this.button_SavedAppSelectCert.Text = "Browse...";
            this.button_SavedAppSelectCert.UseVisualStyleBackColor = true;
            this.button_SavedAppSelectCert.Click += new System.EventHandler(this.Button_SavedAppSelectCert_Click);
            // 
            // textBox_SavedAppCertPath
            // 
            this.textBox_SavedAppCertPath.Location = new System.Drawing.Point(410, 127);
            this.textBox_SavedAppCertPath.Name = "textBox_SavedAppCertPath";
            this.textBox_SavedAppCertPath.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppCertPath.TabIndex = 21;
            this.textBox_SavedAppCertPath.TextChanged += new System.EventHandler(this.TextBox_SavedAppCertPath_TextChanged);
            // 
            // textBox_SavedAppCertPass
            // 
            this.textBox_SavedAppCertPass.Location = new System.Drawing.Point(410, 172);
            this.textBox_SavedAppCertPass.Name = "textBox_SavedAppCertPass";
            this.textBox_SavedAppCertPass.PasswordChar = '*';
            this.textBox_SavedAppCertPass.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppCertPass.TabIndex = 23;
            this.textBox_SavedAppCertPass.TextChanged += new System.EventHandler(this.TextBox_SavedAppCertPass_TextChanged);
            // 
            // comboBox_SavedAppResource
            // 
            this.comboBox_SavedAppResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SavedAppResource.FormattingEnabled = true;
            this.comboBox_SavedAppResource.Location = new System.Drawing.Point(410, 82);
            this.comboBox_SavedAppResource.Name = "comboBox_SavedAppResource";
            this.comboBox_SavedAppResource.Size = new System.Drawing.Size(200, 21);
            this.comboBox_SavedAppResource.TabIndex = 20;
            this.comboBox_SavedAppResource.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SavedAppResource_SelectedIndexChanged);
            // 
            // button_SavedAppScopeEditor
            // 
            this.button_SavedAppScopeEditor.Location = new System.Drawing.Point(616, 35);
            this.button_SavedAppScopeEditor.Name = "button_SavedAppScopeEditor";
            this.button_SavedAppScopeEditor.Size = new System.Drawing.Size(96, 23);
            this.button_SavedAppScopeEditor.TabIndex = 19;
            this.button_SavedAppScopeEditor.Text = "Scope editor...";
            this.button_SavedAppScopeEditor.UseVisualStyleBackColor = true;
            this.button_SavedAppScopeEditor.Click += new System.EventHandler(this.Button_SavedAppScopeEditor_Click);
            // 
            // textBox_SavedAppClientSecret
            // 
            this.textBox_SavedAppClientSecret.Location = new System.Drawing.Point(172, 220);
            this.textBox_SavedAppClientSecret.Name = "textBox_SavedAppClientSecret";
            this.textBox_SavedAppClientSecret.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppClientSecret.TabIndex = 17;
            this.textBox_SavedAppClientSecret.TextChanged += new System.EventHandler(this.TextBox_SavedAppClientSecret_TextChanged);
            // 
            // textBox_SavedAppRedirectUri
            // 
            this.textBox_SavedAppRedirectUri.Location = new System.Drawing.Point(172, 172);
            this.textBox_SavedAppRedirectUri.Name = "textBox_SavedAppRedirectUri";
            this.textBox_SavedAppRedirectUri.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppRedirectUri.TabIndex = 16;
            this.textBox_SavedAppRedirectUri.TextChanged += new System.EventHandler(this.TextBox_SavedAppRedirectUri_TextChanged);
            // 
            // textBox_SavedAppTenantName
            // 
            this.textBox_SavedAppTenantName.Location = new System.Drawing.Point(172, 127);
            this.textBox_SavedAppTenantName.Name = "textBox_SavedAppTenantName";
            this.textBox_SavedAppTenantName.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppTenantName.TabIndex = 15;
            this.textBox_SavedAppTenantName.TextChanged += new System.EventHandler(this.TextBox_SavedAppTenantName_TextChanged);
            // 
            // textBox_SavedAppApplicationId
            // 
            this.textBox_SavedAppApplicationId.Location = new System.Drawing.Point(172, 82);
            this.textBox_SavedAppApplicationId.Name = "textBox_SavedAppApplicationId";
            this.textBox_SavedAppApplicationId.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppApplicationId.TabIndex = 14;
            this.textBox_SavedAppApplicationId.TextChanged += new System.EventHandler(this.TextBox_SavedAppApplicationId_TextChanged);
            // 
            // textBox_SavedAppDisplayName
            // 
            this.textBox_SavedAppDisplayName.Location = new System.Drawing.Point(172, 37);
            this.textBox_SavedAppDisplayName.Name = "textBox_SavedAppDisplayName";
            this.textBox_SavedAppDisplayName.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppDisplayName.TabIndex = 13;
            this.textBox_SavedAppDisplayName.TextChanged += new System.EventHandler(this.TextBox_SavedAppDisplayName_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Saved Apps:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(407, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Certificate Password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(407, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Certificate Path";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(169, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Client Secret";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(407, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Scopes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(407, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Resource";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Redirect URI";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(169, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Tenant Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(169, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Application ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Display Name";
            // 
            // button_RemoveApp
            // 
            this.button_RemoveApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_RemoveApp.Location = new System.Drawing.Point(88, 250);
            this.button_RemoveApp.Name = "button_RemoveApp";
            this.button_RemoveApp.Size = new System.Drawing.Size(75, 23);
            this.button_RemoveApp.TabIndex = 2;
            this.button_RemoveApp.Text = "Remove";
            this.button_RemoveApp.UseVisualStyleBackColor = true;
            this.button_RemoveApp.Click += new System.EventHandler(this.Button_RemoveApp_Click);
            // 
            // button_AddApp
            // 
            this.button_AddApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AddApp.Location = new System.Drawing.Point(7, 250);
            this.button_AddApp.Name = "button_AddApp";
            this.button_AddApp.Size = new System.Drawing.Size(75, 23);
            this.button_AddApp.TabIndex = 1;
            this.button_AddApp.Text = "Add";
            this.button_AddApp.UseVisualStyleBackColor = true;
            this.button_AddApp.Click += new System.EventHandler(this.Button_AddApp_Click);
            // 
            // listBox_SavedApps
            // 
            this.listBox_SavedApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_SavedApps.FormattingEnabled = true;
            this.listBox_SavedApps.HorizontalScrollbar = true;
            this.listBox_SavedApps.Location = new System.Drawing.Point(9, 21);
            this.listBox_SavedApps.Name = "listBox_SavedApps";
            this.listBox_SavedApps.Size = new System.Drawing.Size(154, 212);
            this.listBox_SavedApps.TabIndex = 0;
            this.listBox_SavedApps.SelectedIndexChanged += new System.EventHandler(this.ListBox_SavedApps_SelectedIndexChanged);
            // 
            // openFileDialog_SavedAppPFX
            // 
            this.openFileDialog_SavedAppPFX.Filter = "Personal Information Exchange (*.pfx)|*.pfx";
            // 
            // RequestFormOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(742, 343);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequestFormOptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.RequestFormOptionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_LogFolderPathBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.RadioButton radioButton_DateTime;
        private System.Windows.Forms.RadioButton radioButton_Static;
        private System.Windows.Forms.TextBox textBox_LogFolderPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_AllowAutoRedirect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_CustomUserAgent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_CustomUserAgentStyle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_ReplacePlusSignInTheRequestURL;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox_ReplaceSharpSignInTheRequestURL;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBox_SavedApps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_RemoveApp;
        private System.Windows.Forms.Button button_AddApp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_SavedAppTenantName;
        private System.Windows.Forms.TextBox textBox_SavedAppApplicationId;
        private System.Windows.Forms.TextBox textBox_SavedAppDisplayName;
        private System.Windows.Forms.TextBox textBox_SavedAppClientSecret;
        private System.Windows.Forms.TextBox textBox_SavedAppRedirectUri;
        private System.Windows.Forms.Button button_SavedAppScopeEditor;
        private System.Windows.Forms.ComboBox comboBox_SavedAppResource;
        private System.Windows.Forms.Button button_SavedAppSelectCert;
        private System.Windows.Forms.TextBox textBox_SavedAppCertPath;
        private System.Windows.Forms.TextBox textBox_SavedAppCertPass;
        private System.Windows.Forms.TextBox textBox_SavedAppScopes;
        private System.Windows.Forms.TextBox textBox_SavedAppNote;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.OpenFileDialog openFileDialog_SavedAppPFX;
    }
}