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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox_Indent = new System.Windows.Forms.CheckBox();
            this.checkBox_Decode = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAccessTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBox_RunHistory = new System.Windows.Forms.ListBox();
            this.label_Line = new System.Windows.Forms.Label();
            this.label_StatusCode = new System.Windows.Forms.Label();
            this.checkBox_Logging = new System.Windows.Forms.CheckBox();
            this.tabControl_Response = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox_ResponseHeaders = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_ResponseBody = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pictureBox_Photo = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView_CSV = new System.Windows.Forms.DataGridView();
            this.button_ViewTokenInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl_Request = new System.Windows.Forms.TabControl();
            this.tabPage_Headers = new System.Windows.Forms.TabPage();
            this.dataGridView_RequestHeader = new System.Windows.Forms.DataGridView();
            this.HeaderNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeaderValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_Body = new System.Windows.Forms.TabPage();
            this.textBox_RequestBody = new System.Windows.Forms.TextBox();
            this.textBox_Request = new System.Windows.Forms.TextBox();
            this.button_Run = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_BasicAuthSMTPAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_RefreshToken = new System.Windows.Forms.Button();
            this.textBox_BasicAuthPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton_DELETE = new System.Windows.Forms.RadioButton();
            this.radioButton_GET = new System.Windows.Forms.RadioButton();
            this.radioButton_PATCH = new System.Windows.Forms.RadioButton();
            this.radioButton_POST = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip_RunHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsInMainPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel_SaveCsvResponse = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl_Response.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Photo)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CSV)).BeginInit();
            this.tabControl_Request.SuspendLayout();
            this.tabPage_Headers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RequestHeader)).BeginInit();
            this.tabPage_Body.SuspendLayout();
            this.contextMenuStrip_RunHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_Indent
            // 
            this.checkBox_Indent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Indent.AutoSize = true;
            this.checkBox_Indent.Checked = true;
            this.checkBox_Indent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Indent.Location = new System.Drawing.Point(669, 235);
            this.checkBox_Indent.Name = "checkBox_Indent";
            this.checkBox_Indent.Size = new System.Drawing.Size(56, 17);
            this.checkBox_Indent.TabIndex = 58;
            this.checkBox_Indent.Text = "Indent";
            this.toolTip1.SetToolTip(this.checkBox_Indent, "Add indents to JSON response.");
            this.checkBox_Indent.UseVisualStyleBackColor = true;
            this.checkBox_Indent.CheckedChanged += new System.EventHandler(this.checkBox_Indent_CheckedChanged);
            // 
            // checkBox_Decode
            // 
            this.checkBox_Decode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Decode.AutoSize = true;
            this.checkBox_Decode.Checked = true;
            this.checkBox_Decode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Decode.Location = new System.Drawing.Point(737, 235);
            this.checkBox_Decode.Name = "checkBox_Decode";
            this.checkBox_Decode.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Decode.TabIndex = 57;
            this.checkBox_Decode.Text = "Decode";
            this.toolTip1.SetToolTip(this.checkBox_Decode, "Convert control characters and unicode sequences");
            this.checkBox_Decode.UseVisualStyleBackColor = true;
            this.checkBox_Decode.CheckedChanged += new System.EventHandler(this.checkBox_Decode_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.optionToolStripMenuItem.Text = "&Option...";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.OptionToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.listBox_RunHistory);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.label_Line);
            this.splitContainer2.Panel2.Controls.Add(this.label_StatusCode);
            this.splitContainer2.Panel2.Controls.Add(this.checkBox_Logging);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl_Response);
            this.splitContainer2.Panel2.Controls.Add(this.button_ViewTokenInfo);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl_Request);
            this.splitContainer2.Panel2.Controls.Add(this.checkBox_Indent);
            this.splitContainer2.Panel2.Controls.Add(this.textBox_Request);
            this.splitContainer2.Panel2.Controls.Add(this.checkBox_Decode);
            this.splitContainer2.Panel2.Controls.Add(this.button_Run);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.textBox_BasicAuthSMTPAddress);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.button_RefreshToken);
            this.splitContainer2.Panel2.Controls.Add(this.textBox_BasicAuthPassword);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton_DELETE);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton_GET);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton_PATCH);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton_POST);
            this.splitContainer2.Panel2MinSize = 820;
            this.splitContainer2.Size = new System.Drawing.Size(1020, 497);
            this.splitContainer2.SplitterDistance = 196;
            this.splitContainer2.TabIndex = 44;
            // 
            // listBox_RunHistory
            // 
            this.listBox_RunHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_RunHistory.FormattingEnabled = true;
            this.listBox_RunHistory.Location = new System.Drawing.Point(0, 0);
            this.listBox_RunHistory.Name = "listBox_RunHistory";
            this.listBox_RunHistory.Size = new System.Drawing.Size(196, 497);
            this.listBox_RunHistory.TabIndex = 0;
            this.listBox_RunHistory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_RunHistory_DrawItem);
            this.listBox_RunHistory.DoubleClick += new System.EventHandler(this.listBox_RunHistory_DoubleClick);
            this.listBox_RunHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_RunHistory_MouseDown);
            this.listBox_RunHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBox_RunHistory_MouseMove);
            this.listBox_RunHistory.Resize += new System.EventHandler(this.listBox_RunHistory_Resize);
            // 
            // label_Line
            // 
            this.label_Line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Line.Location = new System.Drawing.Point(20, 224);
            this.label_Line.Name = "label_Line";
            this.label_Line.Size = new System.Drawing.Size(780, 2);
            this.label_Line.TabIndex = 65;
            // 
            // label_StatusCode
            // 
            this.label_StatusCode.AutoSize = true;
            this.label_StatusCode.Location = new System.Drawing.Point(84, 236);
            this.label_StatusCode.Name = "label_StatusCode";
            this.label_StatusCode.Size = new System.Drawing.Size(0, 13);
            this.label_StatusCode.TabIndex = 62;
            // 
            // checkBox_Logging
            // 
            this.checkBox_Logging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Logging.AutoSize = true;
            this.checkBox_Logging.Location = new System.Drawing.Point(694, 175);
            this.checkBox_Logging.Name = "checkBox_Logging";
            this.checkBox_Logging.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Logging.TabIndex = 64;
            this.checkBox_Logging.Text = "Logging";
            this.checkBox_Logging.UseVisualStyleBackColor = true;
            // 
            // tabControl_Response
            // 
            this.tabControl_Response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Response.Controls.Add(this.tabPage1);
            this.tabControl_Response.Controls.Add(this.tabPage2);
            this.tabControl_Response.Controls.Add(this.tabPage3);
            this.tabControl_Response.Controls.Add(this.tabPage4);
            this.tabControl_Response.Location = new System.Drawing.Point(22, 259);
            this.tabControl_Response.Name = "tabControl_Response";
            this.tabControl_Response.SelectedIndex = 0;
            this.tabControl_Response.Size = new System.Drawing.Size(779, 226);
            this.tabControl_Response.TabIndex = 61;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox_ResponseHeaders);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(771, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Headers";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_ResponseHeaders
            // 
            this.textBox_ResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_ResponseHeaders.Location = new System.Drawing.Point(3, 3);
            this.textBox_ResponseHeaders.Multiline = true;
            this.textBox_ResponseHeaders.Name = "textBox_ResponseHeaders";
            this.textBox_ResponseHeaders.ReadOnly = true;
            this.textBox_ResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ResponseHeaders.Size = new System.Drawing.Size(765, 194);
            this.textBox_ResponseHeaders.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_ResponseBody);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(771, 200);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Body";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_ResponseBody
            // 
            this.textBox_ResponseBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_ResponseBody.Location = new System.Drawing.Point(3, 3);
            this.textBox_ResponseBody.Multiline = true;
            this.textBox_ResponseBody.Name = "textBox_ResponseBody";
            this.textBox_ResponseBody.ReadOnly = true;
            this.textBox_ResponseBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_ResponseBody.Size = new System.Drawing.Size(765, 194);
            this.textBox_ResponseBody.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pictureBox_Photo);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(771, 200);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Preview";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pictureBox_Photo
            // 
            this.pictureBox_Photo.Location = new System.Drawing.Point(6, 6);
            this.pictureBox_Photo.Name = "pictureBox_Photo";
            this.pictureBox_Photo.Size = new System.Drawing.Size(128, 128);
            this.pictureBox_Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Photo.TabIndex = 9;
            this.pictureBox_Photo.TabStop = false;
            this.pictureBox_Photo.Visible = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.linkLabel_SaveCsvResponse);
            this.tabPage4.Controls.Add(this.dataGridView_CSV);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(771, 200);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "CSV";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView_CSV
            // 
            this.dataGridView_CSV.AllowUserToAddRows = false;
            this.dataGridView_CSV.AllowUserToDeleteRows = false;
            this.dataGridView_CSV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_CSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CSV.Location = new System.Drawing.Point(3, 28);
            this.dataGridView_CSV.Name = "dataGridView_CSV";
            this.dataGridView_CSV.ReadOnly = true;
            this.dataGridView_CSV.Size = new System.Drawing.Size(765, 178);
            this.dataGridView_CSV.TabIndex = 0;
            this.dataGridView_CSV.Visible = false;
            // 
            // button_ViewTokenInfo
            // 
            this.button_ViewTokenInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ViewTokenInfo.Location = new System.Drawing.Point(694, 12);
            this.button_ViewTokenInfo.Name = "button_ViewTokenInfo";
            this.button_ViewTokenInfo.Size = new System.Drawing.Size(107, 23);
            this.button_ViewTokenInfo.TabIndex = 48;
            this.button_ViewTokenInfo.Text = "View token info";
            this.button_ViewTokenInfo.UseVisualStyleBackColor = true;
            this.button_ViewTokenInfo.Click += new System.EventHandler(this.button_ViewTokenInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Response :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(569, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Format options : ";
            // 
            // tabControl_Request
            // 
            this.tabControl_Request.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl_Request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Request.Controls.Add(this.tabPage_Headers);
            this.tabControl_Request.Controls.Add(this.tabPage_Body);
            this.tabControl_Request.Location = new System.Drawing.Point(101, 82);
            this.tabControl_Request.Multiline = true;
            this.tabControl_Request.Name = "tabControl_Request";
            this.tabControl_Request.SelectedIndex = 0;
            this.tabControl_Request.Size = new System.Drawing.Size(587, 110);
            this.tabControl_Request.TabIndex = 63;
            // 
            // tabPage_Headers
            // 
            this.tabPage_Headers.Controls.Add(this.dataGridView_RequestHeader);
            this.tabPage_Headers.Location = new System.Drawing.Point(23, 4);
            this.tabPage_Headers.Name = "tabPage_Headers";
            this.tabPage_Headers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Headers.Size = new System.Drawing.Size(560, 102);
            this.tabPage_Headers.TabIndex = 0;
            this.tabPage_Headers.Text = "Headers";
            this.tabPage_Headers.UseVisualStyleBackColor = true;
            // 
            // dataGridView_RequestHeader
            // 
            this.dataGridView_RequestHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_RequestHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HeaderNameCol,
            this.HeaderValueCol});
            this.dataGridView_RequestHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_RequestHeader.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_RequestHeader.Name = "dataGridView_RequestHeader";
            this.dataGridView_RequestHeader.Size = new System.Drawing.Size(554, 96);
            this.dataGridView_RequestHeader.TabIndex = 45;
            // 
            // HeaderNameCol
            // 
            this.HeaderNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HeaderNameCol.HeaderText = "Name";
            this.HeaderNameCol.Name = "HeaderNameCol";
            // 
            // HeaderValueCol
            // 
            this.HeaderValueCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HeaderValueCol.HeaderText = "Value";
            this.HeaderValueCol.Name = "HeaderValueCol";
            // 
            // tabPage_Body
            // 
            this.tabPage_Body.Controls.Add(this.textBox_RequestBody);
            this.tabPage_Body.Location = new System.Drawing.Point(23, 4);
            this.tabPage_Body.Name = "tabPage_Body";
            this.tabPage_Body.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Body.Size = new System.Drawing.Size(560, 102);
            this.tabPage_Body.TabIndex = 1;
            this.tabPage_Body.Text = "Body";
            this.tabPage_Body.UseVisualStyleBackColor = true;
            // 
            // textBox_RequestBody
            // 
            this.textBox_RequestBody.AcceptsReturn = true;
            this.textBox_RequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_RequestBody.Location = new System.Drawing.Point(3, 3);
            this.textBox_RequestBody.Multiline = true;
            this.textBox_RequestBody.Name = "textBox_RequestBody";
            this.textBox_RequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_RequestBody.Size = new System.Drawing.Size(554, 96);
            this.textBox_RequestBody.TabIndex = 4;
            // 
            // textBox_Request
            // 
            this.textBox_Request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Request.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastRequest", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_Request.Location = new System.Drawing.Point(101, 40);
            this.textBox_Request.Multiline = true;
            this.textBox_Request.Name = "textBox_Request";
            this.textBox_Request.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Request.Size = new System.Drawing.Size(587, 36);
            this.textBox_Request.TabIndex = 45;
            this.textBox_Request.Text = global::Office365APIEditor.Properties.Settings.Default.LastRequest;
            // 
            // button_Run
            // 
            this.button_Run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Run.Location = new System.Drawing.Point(694, 195);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(107, 23);
            this.button_Run.TabIndex = 47;
            this.button_Run.Text = "Run";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Method";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "SMTP Address";
            // 
            // textBox_BasicAuthSMTPAddress
            // 
            this.textBox_BasicAuthSMTPAddress.Location = new System.Drawing.Point(101, 14);
            this.textBox_BasicAuthSMTPAddress.Name = "textBox_BasicAuthSMTPAddress";
            this.textBox_BasicAuthSMTPAddress.Size = new System.Drawing.Size(253, 20);
            this.textBox_BasicAuthSMTPAddress.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Password";
            // 
            // button_RefreshToken
            // 
            this.button_RefreshToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RefreshToken.Location = new System.Drawing.Point(694, 41);
            this.button_RefreshToken.Name = "button_RefreshToken";
            this.button_RefreshToken.Size = new System.Drawing.Size(107, 23);
            this.button_RefreshToken.TabIndex = 49;
            this.button_RefreshToken.Text = "Refresh token";
            this.button_RefreshToken.UseVisualStyleBackColor = true;
            this.button_RefreshToken.Click += new System.EventHandler(this.button_RefreshToken_Click);
            // 
            // textBox_BasicAuthPassword
            // 
            this.textBox_BasicAuthPassword.Location = new System.Drawing.Point(435, 14);
            this.textBox_BasicAuthPassword.Name = "textBox_BasicAuthPassword";
            this.textBox_BasicAuthPassword.Size = new System.Drawing.Size(253, 20);
            this.textBox_BasicAuthPassword.TabIndex = 44;
            this.textBox_BasicAuthPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Request";
            // 
            // radioButton_DELETE
            // 
            this.radioButton_DELETE.AutoSize = true;
            this.radioButton_DELETE.Location = new System.Drawing.Point(281, 198);
            this.radioButton_DELETE.Name = "radioButton_DELETE";
            this.radioButton_DELETE.Size = new System.Drawing.Size(67, 17);
            this.radioButton_DELETE.TabIndex = 56;
            this.radioButton_DELETE.Text = "DELETE";
            this.radioButton_DELETE.UseVisualStyleBackColor = true;
            // 
            // radioButton_GET
            // 
            this.radioButton_GET.AutoSize = true;
            this.radioButton_GET.Checked = true;
            this.radioButton_GET.Location = new System.Drawing.Point(101, 198);
            this.radioButton_GET.Name = "radioButton_GET";
            this.radioButton_GET.Size = new System.Drawing.Size(47, 17);
            this.radioButton_GET.TabIndex = 46;
            this.radioButton_GET.TabStop = true;
            this.radioButton_GET.Text = "GET";
            this.radioButton_GET.UseVisualStyleBackColor = true;
            // 
            // radioButton_PATCH
            // 
            this.radioButton_PATCH.AutoSize = true;
            this.radioButton_PATCH.Location = new System.Drawing.Point(214, 198);
            this.radioButton_PATCH.Name = "radioButton_PATCH";
            this.radioButton_PATCH.Size = new System.Drawing.Size(61, 17);
            this.radioButton_PATCH.TabIndex = 55;
            this.radioButton_PATCH.Text = "PATCH";
            this.radioButton_PATCH.UseVisualStyleBackColor = true;
            // 
            // radioButton_POST
            // 
            this.radioButton_POST.AutoSize = true;
            this.radioButton_POST.Location = new System.Drawing.Point(154, 198);
            this.radioButton_POST.Name = "radioButton_POST";
            this.radioButton_POST.Size = new System.Drawing.Size(54, 17);
            this.radioButton_POST.TabIndex = 54;
            this.radioButton_POST.Text = "POST";
            this.radioButton_POST.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip_RunHistory
            // 
            this.contextMenuStrip_RunHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsInMainPanelToolStripMenuItem});
            this.contextMenuStrip_RunHistory.Name = "contextMenuStrip_RunHistory";
            this.contextMenuStrip_RunHistory.Size = new System.Drawing.Size(216, 26);
            // 
            // showDetailsInMainPanelToolStripMenuItem
            // 
            this.showDetailsInMainPanelToolStripMenuItem.Name = "showDetailsInMainPanelToolStripMenuItem";
            this.showDetailsInMainPanelToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showDetailsInMainPanelToolStripMenuItem.Text = "Show details in main panel";
            this.showDetailsInMainPanelToolStripMenuItem.Click += new System.EventHandler(this.showDetailsInMainPanelToolStripMenuItem_Click);
            // 
            // linkLabel_SaveCsvResponse
            // 
            this.linkLabel_SaveCsvResponse.AutoSize = true;
            this.linkLabel_SaveCsvResponse.Location = new System.Drawing.Point(7, 7);
            this.linkLabel_SaveCsvResponse.Name = "linkLabel_SaveCsvResponse";
            this.linkLabel_SaveCsvResponse.Size = new System.Drawing.Size(91, 13);
            this.linkLabel_SaveCsvResponse.TabIndex = 1;
            this.linkLabel_SaveCsvResponse.TabStop = true;
            this.linkLabel_SaveCsvResponse.Text = "Save this CSV file";
            this.linkLabel_SaveCsvResponse.Visible = false;
            this.linkLabel_SaveCsvResponse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SaveCsvResponse_LinkClicked);
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 521);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1036, 560);
            this.Name = "RequestForm";
            this.Text = "Office365APIEditor - Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RequestForm_FormClosed);
            this.Load += new System.EventHandler(this.RequestForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl_Response.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Photo)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CSV)).EndInit();
            this.tabControl_Request.ResumeLayout(false);
            this.tabPage_Headers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RequestHeader)).EndInit();
            this.tabPage_Body.ResumeLayout(false);
            this.tabPage_Body.PerformLayout();
            this.contextMenuStrip_RunHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAccessTokenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBox_RunHistory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RunHistory;
        private System.Windows.Forms.ToolStripMenuItem showDetailsInMainPanelToolStripMenuItem;
        private System.Windows.Forms.Label label_StatusCode;
        private System.Windows.Forms.CheckBox checkBox_Logging;
        private System.Windows.Forms.TabControl tabControl_Response;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox_ResponseHeaders;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox_ResponseBody;
        private System.Windows.Forms.Button button_ViewTokenInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl_Request;
        private System.Windows.Forms.TabPage tabPage_Headers;
        private System.Windows.Forms.TabPage tabPage_Body;
        private System.Windows.Forms.TextBox textBox_RequestBody;
        private System.Windows.Forms.CheckBox checkBox_Indent;
        private System.Windows.Forms.TextBox textBox_Request;
        private System.Windows.Forms.CheckBox checkBox_Decode;
        private System.Windows.Forms.Button button_Run;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_BasicAuthSMTPAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_RefreshToken;
        private System.Windows.Forms.TextBox textBox_BasicAuthPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton_DELETE;
        private System.Windows.Forms.RadioButton radioButton_GET;
        private System.Windows.Forms.RadioButton radioButton_PATCH;
        private System.Windows.Forms.RadioButton radioButton_POST;
        private System.Windows.Forms.Label label_Line;
        private System.Windows.Forms.DataGridView dataGridView_RequestHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeaderNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeaderValueCol;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox_Photo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView_CSV;
        private System.Windows.Forms.LinkLabel linkLabel_SaveCsvResponse;
    }
}