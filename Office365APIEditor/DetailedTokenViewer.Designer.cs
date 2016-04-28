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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox_Header = new System.Windows.Forms.TextBox();
            this.textBox_Claim = new System.Windows.Forms.TextBox();
            this.textBox_Signature = new System.Windows.Forms.TextBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.tabPage1.Controls.Add(this.textBox_Header);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Header";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_Claim);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(610, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Claim";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // DetailedTokenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Close;
            this.ClientSize = new System.Drawing.Size(614, 391);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(630, 430);
            this.Name = "DetailedTokenViewer";
            this.Text = "Token Details";
            this.Load += new System.EventHandler(this.DetailedTokenViewer_Load);
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

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox_Header;
        private System.Windows.Forms.TextBox textBox_Claim;
        private System.Windows.Forms.TextBox textBox_Signature;
        private System.Windows.Forms.Button button_Close;
    }
}