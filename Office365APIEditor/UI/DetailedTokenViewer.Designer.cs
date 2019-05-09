namespace Office365APIEditor
{
    partial class DetailedTokenViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedTokenViewer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_Header = new System.Windows.Forms.DataGridView();
            this.Type_Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description_Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value_Header = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_Header = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_Claim = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_Claim = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox_Signature = new System.Windows.Forms.TextBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.checkBox_AdvancedView = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Header)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Claim)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(618, 350);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_Header);
            this.tabPage1.Controls.Add(this.textBox_Header);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Header";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Header
            // 
            this.dataGridView_Header.AllowUserToAddRows = false;
            this.dataGridView_Header.AllowUserToDeleteRows = false;
            this.dataGridView_Header.AllowUserToOrderColumns = true;
            this.dataGridView_Header.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Header.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type_Header,
            this.Description_Header,
            this.Value_Header});
            this.dataGridView_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Header.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Header.Name = "dataGridView_Header";
            this.dataGridView_Header.ReadOnly = true;
            this.dataGridView_Header.Size = new System.Drawing.Size(604, 318);
            this.dataGridView_Header.TabIndex = 1;
            // 
            // Type_Header
            // 
            this.Type_Header.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Type_Header.HeaderText = "Type";
            this.Type_Header.Name = "Type_Header";
            this.Type_Header.ReadOnly = true;
            this.Type_Header.Width = 54;
            // 
            // Description_Header
            // 
            this.Description_Header.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Description_Header.HeaderText = "Description";
            this.Description_Header.Name = "Description_Header";
            this.Description_Header.ReadOnly = true;
            this.Description_Header.Width = 83;
            // 
            // Value_Header
            // 
            this.Value_Header.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value_Header.HeaderText = "Value";
            this.Value_Header.Name = "Value_Header";
            this.Value_Header.ReadOnly = true;
            // 
            // textBox_Header
            // 
            this.textBox_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Header.Location = new System.Drawing.Point(3, 3);
            this.textBox_Header.Multiline = true;
            this.textBox_Header.Name = "textBox_Header";
            this.textBox_Header.ReadOnly = true;
            this.textBox_Header.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Header.Size = new System.Drawing.Size(604, 318);
            this.textBox_Header.TabIndex = 0;
            this.textBox_Header.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Header_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_Claim);
            this.tabPage2.Controls.Add(this.textBox_Claim);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(610, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Claim";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Claim
            // 
            this.dataGridView_Claim.AllowUserToAddRows = false;
            this.dataGridView_Claim.AllowUserToDeleteRows = false;
            this.dataGridView_Claim.AllowUserToOrderColumns = true;
            this.dataGridView_Claim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Claim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Description,
            this.Value});
            this.dataGridView_Claim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Claim.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Claim.Name = "dataGridView_Claim";
            this.dataGridView_Claim.ReadOnly = true;
            this.dataGridView_Claim.Size = new System.Drawing.Size(604, 318);
            this.dataGridView_Claim.TabIndex = 1;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 54;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 83;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // textBox_Claim
            // 
            this.textBox_Claim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Claim.Location = new System.Drawing.Point(3, 3);
            this.textBox_Claim.Multiline = true;
            this.textBox_Claim.Name = "textBox_Claim";
            this.textBox_Claim.ReadOnly = true;
            this.textBox_Claim.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Claim.Size = new System.Drawing.Size(604, 318);
            this.textBox_Claim.TabIndex = 1;
            this.textBox_Claim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Claim_KeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox_Signature);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(610, 324);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Signature";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox_Signature
            // 
            this.textBox_Signature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Signature.Location = new System.Drawing.Point(3, 3);
            this.textBox_Signature.Multiline = true;
            this.textBox_Signature.Name = "textBox_Signature";
            this.textBox_Signature.ReadOnly = true;
            this.textBox_Signature.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Signature.Size = new System.Drawing.Size(604, 318);
            this.textBox_Signature.TabIndex = 1;
            this.textBox_Signature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Signature_KeyDown);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Close.Location = new System.Drawing.Point(527, 356);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 4;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // checkBox_AdvancedView
            // 
            this.checkBox_AdvancedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_AdvancedView.AutoSize = true;
            this.checkBox_AdvancedView.Location = new System.Drawing.Point(12, 362);
            this.checkBox_AdvancedView.Name = "checkBox_AdvancedView";
            this.checkBox_AdvancedView.Size = new System.Drawing.Size(101, 17);
            this.checkBox_AdvancedView.TabIndex = 5;
            this.checkBox_AdvancedView.Text = "Advanced View";
            this.checkBox_AdvancedView.UseVisualStyleBackColor = true;
            this.checkBox_AdvancedView.CheckedChanged += new System.EventHandler(this.checkBox_AdvancedView_CheckedChanged);
            // 
            // DetailedTokenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Close;
            this.ClientSize = new System.Drawing.Size(614, 391);
            this.Controls.Add(this.checkBox_AdvancedView);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(630, 430);
            this.Name = "DetailedTokenViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Token Details";
            this.Load += new System.EventHandler(this.DetailedTokenViewer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Header)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Claim)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox_Header;
        private System.Windows.Forms.TextBox textBox_Claim;
        private System.Windows.Forms.TextBox textBox_Signature;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.CheckBox checkBox_AdvancedView;
        private System.Windows.Forms.DataGridView dataGridView_Claim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridView dataGridView_Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description_Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value_Header;
    }
}