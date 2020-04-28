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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_LogFolderPathBrowse
            // 
            this.button_LogFolderPathBrowse.Location = new System.Drawing.Point(327, 19);
            this.button_LogFolderPathBrowse.Name = "button_LogFolderPathBrowse";
            this.button_LogFolderPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.button_LogFolderPathBrowse.TabIndex = 16;
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
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(346, 288);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 20;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(265, 288);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 19;
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
            this.radioButton_DateTime.TabIndex = 18;
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
            this.radioButton_Static.TabIndex = 17;
            this.radioButton_Static.TabStop = true;
            this.radioButton_Static.Text = "Static";
            this.radioButton_Static.UseVisualStyleBackColor = true;
            // 
            // textBox_LogFolderPath
            // 
            this.textBox_LogFolderPath.Location = new System.Drawing.Point(9, 21);
            this.textBox_LogFolderPath.Name = "textBox_LogFolderPath";
            this.textBox_LogFolderPath.Size = new System.Drawing.Size(312, 20);
            this.textBox_LogFolderPath.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Static);
            this.groupBox1.Controls.Add(this.radioButton_DateTime);
            this.groupBox1.Location = new System.Drawing.Point(9, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 68);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log file name format";
            // 
            // checkBox_AllowAutoRedirect
            // 
            this.checkBox_AllowAutoRedirect.AutoSize = true;
            this.checkBox_AllowAutoRedirect.Location = new System.Drawing.Point(9, 122);
            this.checkBox_AllowAutoRedirect.Name = "checkBox_AllowAutoRedirect";
            this.checkBox_AllowAutoRedirect.Size = new System.Drawing.Size(113, 17);
            this.checkBox_AllowAutoRedirect.TabIndex = 67;
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
            this.textBox_CustomUserAgent.Location = new System.Drawing.Point(107, 46);
            this.textBox_CustomUserAgent.Name = "textBox_CustomUserAgent";
            this.textBox_CustomUserAgent.Size = new System.Drawing.Size(275, 20);
            this.textBox_CustomUserAgent.TabIndex = 70;
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
            this.comboBox_CustomUserAgentStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CustomUserAgentStyle.FormattingEnabled = true;
            this.comboBox_CustomUserAgentStyle.Location = new System.Drawing.Point(107, 19);
            this.comboBox_CustomUserAgentStyle.Name = "comboBox_CustomUserAgentStyle";
            this.comboBox_CustomUserAgentStyle.Size = new System.Drawing.Size(275, 21);
            this.comboBox_CustomUserAgentStyle.TabIndex = 72;
            this.comboBox_CustomUserAgentStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_CustomUserAgentStyle_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBox_CustomUserAgentStyle);
            this.groupBox2.Controls.Add(this.textBox_CustomUserAgent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 101);
            this.groupBox2.TabIndex = 73;
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
            this.checkBox_ReplacePlusSignInTheRequestURL.TabIndex = 74;
            this.checkBox_ReplacePlusSignInTheRequestURL.Text = "Replace \"+\" in the request URL with \"%2B\".";
            this.checkBox_ReplacePlusSignInTheRequestURL.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(419, 280);
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
            this.tabPage1.Size = new System.Drawing.Size(411, 254);
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
            this.tabPage2.Size = new System.Drawing.Size(411, 254);
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
            this.checkBox_ReplaceSharpSignInTheRequestURL.TabIndex = 75;
            this.checkBox_ReplaceSharpSignInTheRequestURL.Text = "Replace \"#\" in the request URL with \"%23\".";
            this.checkBox_ReplaceSharpSignInTheRequestURL.UseVisualStyleBackColor = true;
            // 
            // RequestFormOptionForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(428, 316);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
    }
}