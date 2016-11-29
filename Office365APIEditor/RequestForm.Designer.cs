namespace Office365APIEditor
{
    partial class RequestForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.button_Run = new System.Windows.Forms.Button();
            this.textBox_Request = new System.Windows.Forms.TextBox();
            this.textBox_BasicAuthSMTPAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_BasicAuthPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton_GET = new System.Windows.Forms.RadioButton();
            this.radioButton_POST = new System.Windows.Forms.RadioButton();
            this.textBox_RequestBody = new System.Windows.Forms.TextBox();
            this.radioButton_PATCH = new System.Windows.Forms.RadioButton();
            this.radioButton_DELETE = new System.Windows.Forms.RadioButton();
            this.button_ViewTokenInfo = new System.Windows.Forms.Button();
            this.button_RefreshToken = new System.Windows.Forms.Button();
            this.checkBox_Decode = new System.Windows.Forms.CheckBox();
            this.checkBox_Indent = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_RequestHeaders = new System.Windows.Forms.TextBox();
            this.tabControl_HeadersAndBody = new System.Windows.Forms.TabControl();
            this.tabPage_Headers = new System.Windows.Forms.TabPage();
            this.tabPage_Body = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAccessTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox_Logging = new System.Windows.Forms.CheckBox();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_HeadersAndBody.SuspendLayout();
            this.tabPage_Headers.SuspendLayout();
            this.tabPage_Body.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Response :";
            // 
            // textBox_Result
            // 
            this.textBox_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Result.Location = new System.Drawing.Point(23, 30);
            this.textBox_Result.Multiline = true;
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Result.Size = new System.Drawing.Size(785, 248);
            this.textBox_Result.TabIndex = 7;
            this.textBox_Result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Result_KeyDown);
            // 
            // button_Run
            // 
            this.button_Run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Run.Location = new System.Drawing.Point(701, 174);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(107, 23);
            this.button_Run.TabIndex = 6;
            this.button_Run.Text = "Run";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // textBox_Request
            // 
            this.textBox_Request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Request.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastRequest", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Request.Location = new System.Drawing.Point(103, 31);
            this.textBox_Request.Multiline = true;
            this.textBox_Request.Name = "textBox_Request";
            this.textBox_Request.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Request.Size = new System.Drawing.Size(587, 36);
            this.textBox_Request.TabIndex = 3;
            this.textBox_Request.Text = global::Office365APIEditor.Properties.Settings.Default.LastRequest;
            this.textBox_Request.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Request_KeyDown);
            // 
            // textBox_BasicAuthSMTPAddress
            // 
            this.textBox_BasicAuthSMTPAddress.Location = new System.Drawing.Point(103, 5);
            this.textBox_BasicAuthSMTPAddress.Name = "textBox_BasicAuthSMTPAddress";
            this.textBox_BasicAuthSMTPAddress.Size = new System.Drawing.Size(253, 20);
            this.textBox_BasicAuthSMTPAddress.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "SMTP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Password";
            // 
            // textBox_BasicAuthPassword
            // 
            this.textBox_BasicAuthPassword.Location = new System.Drawing.Point(437, 5);
            this.textBox_BasicAuthPassword.Name = "textBox_BasicAuthPassword";
            this.textBox_BasicAuthPassword.Size = new System.Drawing.Size(253, 20);
            this.textBox_BasicAuthPassword.TabIndex = 2;
            this.textBox_BasicAuthPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Request";
            // 
            // radioButton_GET
            // 
            this.radioButton_GET.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_GET.AutoSize = true;
            this.radioButton_GET.Checked = true;
            this.radioButton_GET.Location = new System.Drawing.Point(103, 177);
            this.radioButton_GET.Name = "radioButton_GET";
            this.radioButton_GET.Size = new System.Drawing.Size(47, 17);
            this.radioButton_GET.TabIndex = 5;
            this.radioButton_GET.TabStop = true;
            this.radioButton_GET.Text = "GET";
            this.radioButton_GET.UseVisualStyleBackColor = true;
            // 
            // radioButton_POST
            // 
            this.radioButton_POST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_POST.AutoSize = true;
            this.radioButton_POST.Location = new System.Drawing.Point(156, 177);
            this.radioButton_POST.Name = "radioButton_POST";
            this.radioButton_POST.Size = new System.Drawing.Size(54, 17);
            this.radioButton_POST.TabIndex = 30;
            this.radioButton_POST.Text = "POST";
            this.radioButton_POST.UseVisualStyleBackColor = true;
            // 
            // textBox_RequestBody
            // 
            this.textBox_RequestBody.AcceptsReturn = true;
            this.textBox_RequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_RequestBody.Location = new System.Drawing.Point(3, 3);
            this.textBox_RequestBody.Multiline = true;
            this.textBox_RequestBody.Name = "textBox_RequestBody";
            this.textBox_RequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_RequestBody.Size = new System.Drawing.Size(573, 66);
            this.textBox_RequestBody.TabIndex = 4;
            this.textBox_RequestBody.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_RequestBody_KeyDown);
            // 
            // radioButton_PATCH
            // 
            this.radioButton_PATCH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_PATCH.AutoSize = true;
            this.radioButton_PATCH.Location = new System.Drawing.Point(216, 177);
            this.radioButton_PATCH.Name = "radioButton_PATCH";
            this.radioButton_PATCH.Size = new System.Drawing.Size(61, 17);
            this.radioButton_PATCH.TabIndex = 33;
            this.radioButton_PATCH.Text = "PATCH";
            this.radioButton_PATCH.UseVisualStyleBackColor = true;
            // 
            // radioButton_DELETE
            // 
            this.radioButton_DELETE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_DELETE.AutoSize = true;
            this.radioButton_DELETE.Location = new System.Drawing.Point(283, 177);
            this.radioButton_DELETE.Name = "radioButton_DELETE";
            this.radioButton_DELETE.Size = new System.Drawing.Size(67, 17);
            this.radioButton_DELETE.TabIndex = 34;
            this.radioButton_DELETE.Text = "DELETE";
            this.radioButton_DELETE.UseVisualStyleBackColor = true;
            // 
            // button_ViewTokenInfo
            // 
            this.button_ViewTokenInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ViewTokenInfo.Location = new System.Drawing.Point(701, 3);
            this.button_ViewTokenInfo.Name = "button_ViewTokenInfo";
            this.button_ViewTokenInfo.Size = new System.Drawing.Size(107, 23);
            this.button_ViewTokenInfo.TabIndex = 8;
            this.button_ViewTokenInfo.Text = "View token info";
            this.button_ViewTokenInfo.UseVisualStyleBackColor = true;
            this.button_ViewTokenInfo.Click += new System.EventHandler(this.button_ViewTokenInfo_Click);
            // 
            // button_RefreshToken
            // 
            this.button_RefreshToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RefreshToken.Location = new System.Drawing.Point(701, 32);
            this.button_RefreshToken.Name = "button_RefreshToken";
            this.button_RefreshToken.Size = new System.Drawing.Size(107, 23);
            this.button_RefreshToken.TabIndex = 9;
            this.button_RefreshToken.Text = "Refresh token";
            this.button_RefreshToken.UseVisualStyleBackColor = true;
            this.button_RefreshToken.Click += new System.EventHandler(this.button_RefreshToken_Click);
            // 
            // checkBox_Decode
            // 
            this.checkBox_Decode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Decode.AutoSize = true;
            this.checkBox_Decode.Checked = true;
            this.checkBox_Decode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Decode.Location = new System.Drawing.Point(744, 7);
            this.checkBox_Decode.Name = "checkBox_Decode";
            this.checkBox_Decode.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Decode.TabIndex = 35;
            this.checkBox_Decode.Text = "Decode";
            this.toolTip1.SetToolTip(this.checkBox_Decode, "Convert control characters and unicode sequences");
            this.checkBox_Decode.UseVisualStyleBackColor = true;
            this.checkBox_Decode.CheckedChanged += new System.EventHandler(this.checkBox_Decode_CheckedChanged);
            // 
            // checkBox_Indent
            // 
            this.checkBox_Indent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Indent.AutoSize = true;
            this.checkBox_Indent.Checked = true;
            this.checkBox_Indent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Indent.Location = new System.Drawing.Point(682, 7);
            this.checkBox_Indent.Name = "checkBox_Indent";
            this.checkBox_Indent.Size = new System.Drawing.Size(56, 17);
            this.checkBox_Indent.TabIndex = 36;
            this.checkBox_Indent.Text = "Indent";
            this.toolTip1.SetToolTip(this.checkBox_Indent, "Add indents to JSON response.");
            this.checkBox_Indent.UseVisualStyleBackColor = true;
            this.checkBox_Indent.CheckedChanged += new System.EventHandler(this.checkBox_Indent_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Method";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(591, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Format options : ";
            // 
            // textBox_RequestHeaders
            // 
            this.textBox_RequestHeaders.AcceptsReturn = true;
            this.textBox_RequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_RequestHeaders.Location = new System.Drawing.Point(3, 3);
            this.textBox_RequestHeaders.Multiline = true;
            this.textBox_RequestHeaders.Name = "textBox_RequestHeaders";
            this.textBox_RequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_RequestHeaders.Size = new System.Drawing.Size(573, 66);
            this.textBox_RequestHeaders.TabIndex = 40;
            // 
            // tabControl_HeadersAndBody
            // 
            this.tabControl_HeadersAndBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_HeadersAndBody.Controls.Add(this.tabPage_Headers);
            this.tabControl_HeadersAndBody.Controls.Add(this.tabPage_Body);
            this.tabControl_HeadersAndBody.Location = new System.Drawing.Point(103, 73);
            this.tabControl_HeadersAndBody.Name = "tabControl_HeadersAndBody";
            this.tabControl_HeadersAndBody.SelectedIndex = 0;
            this.tabControl_HeadersAndBody.Size = new System.Drawing.Size(587, 98);
            this.tabControl_HeadersAndBody.TabIndex = 41;
            // 
            // tabPage_Headers
            // 
            this.tabPage_Headers.Controls.Add(this.textBox_RequestHeaders);
            this.tabPage_Headers.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Headers.Name = "tabPage_Headers";
            this.tabPage_Headers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Headers.Size = new System.Drawing.Size(579, 72);
            this.tabPage_Headers.TabIndex = 0;
            this.tabPage_Headers.Text = "Headers";
            this.tabPage_Headers.UseVisualStyleBackColor = true;
            // 
            // tabPage_Body
            // 
            this.tabPage_Body.Controls.Add(this.textBox_RequestBody);
            this.tabPage_Body.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Body.Name = "tabPage_Body";
            this.tabPage_Body.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Body.Size = new System.Drawing.Size(579, 72);
            this.tabPage_Body.TabIndex = 1;
            this.tabPage_Body.Text = "Body";
            this.tabPage_Body.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(820, 24);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAccessTokenToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newAccessTokenToolStripMenuItem
            // 
            this.newAccessTokenToolStripMenuItem.Name = "newAccessTokenToolStripMenuItem";
            this.newAccessTokenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.newAccessTokenToolStripMenuItem.Text = "New Access Token...";
            this.newAccessTokenToolStripMenuItem.Click += new System.EventHandler(this.newAccessTokenToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_Logging);
            this.splitContainer1.Panel1.Controls.Add(this.button_ViewTokenInfo);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl_HeadersAndBody);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Request);
            this.splitContainer1.Panel1.Controls.Add(this.button_Run);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_BasicAuthSMTPAddress);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.button_RefreshToken);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_BasicAuthPassword);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.radioButton_DELETE);
            this.splitContainer1.Panel1.Controls.Add(this.radioButton_GET);
            this.splitContainer1.Panel1.Controls.Add(this.radioButton_PATCH);
            this.splitContainer1.Panel1.Controls.Add(this.radioButton_POST);
            this.splitContainer1.Panel1MinSize = 203;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_Result);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox_Indent);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox_Decode);
            this.splitContainer1.Panel2MinSize = 75;
            this.splitContainer1.Size = new System.Drawing.Size(820, 497);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 43;
            // 
            // checkBox_Logging
            // 
            this.checkBox_Logging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Logging.AutoSize = true;
            this.checkBox_Logging.Location = new System.Drawing.Point(701, 154);
            this.checkBox_Logging.Name = "checkBox_Logging";
            this.checkBox_Logging.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Logging.TabIndex = 42;
            this.checkBox_Logging.Text = "Logging";
            this.checkBox_Logging.UseVisualStyleBackColor = true;
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingOptionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // loggingOptionToolStripMenuItem
            // 
            this.loggingOptionToolStripMenuItem.Name = "loggingOptionToolStripMenuItem";
            this.loggingOptionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.loggingOptionToolStripMenuItem.Text = "&Logging Option...";
            this.loggingOptionToolStripMenuItem.Click += new System.EventHandler(this.loggingOptionToolStripMenuItem_Click);
            // 
            // RequestForm
            // 
            this.AcceptButton = this.button_Run;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 521);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(836, 560);
            this.Name = "RequestForm";
            this.Text = "Office365APIEditor - Editor";
            this.Load += new System.EventHandler(this.RequestForm_Load);
            this.tabControl_HeadersAndBody.ResumeLayout(false);
            this.tabPage_Headers.ResumeLayout(false);
            this.tabPage_Headers.PerformLayout();
            this.tabPage_Body.ResumeLayout(false);
            this.tabPage_Body.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Button button_Run;
        private System.Windows.Forms.TextBox textBox_Request;
        private System.Windows.Forms.TextBox textBox_BasicAuthSMTPAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_BasicAuthPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton_GET;
        private System.Windows.Forms.RadioButton radioButton_POST;
        private System.Windows.Forms.TextBox textBox_RequestBody;
        private System.Windows.Forms.RadioButton radioButton_PATCH;
        private System.Windows.Forms.RadioButton radioButton_DELETE;
        private System.Windows.Forms.Button button_ViewTokenInfo;
        private System.Windows.Forms.Button button_RefreshToken;
        private System.Windows.Forms.CheckBox checkBox_Decode;
        private System.Windows.Forms.CheckBox checkBox_Indent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBox_RequestHeaders;
        private System.Windows.Forms.TabControl tabControl_HeadersAndBody;
        private System.Windows.Forms.TabPage tabPage_Headers;
        private System.Windows.Forms.TabPage tabPage_Body;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAccessTokenToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBox_Logging;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingOptionToolStripMenuItem;
    }
}