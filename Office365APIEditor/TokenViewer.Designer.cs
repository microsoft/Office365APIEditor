namespace Office365APIEditor
{
    partial class TokenViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenViewer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox_IdToken = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox_AccessToken = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_RefreshToken = new System.Windows.Forms.TextBox();
            this.button_AccessTokenDetail = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Export = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button_Import = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(618, 350);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox_IdToken);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(610, 324);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ID Token";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox_IdToken
            // 
            this.textBox_IdToken.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IdToken.Location = new System.Drawing.Point(8, 6);
            this.textBox_IdToken.Multiline = true;
            this.textBox_IdToken.Name = "textBox_IdToken";
            this.textBox_IdToken.ReadOnly = true;
            this.textBox_IdToken.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_IdToken.Size = new System.Drawing.Size(596, 283);
            this.textBox_IdToken.TabIndex = 5;
            this.textBox_IdToken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_IdToken_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox_AccessToken);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Access Token";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_AccessToken
            // 
            this.textBox_AccessToken.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_AccessToken.Location = new System.Drawing.Point(8, 6);
            this.textBox_AccessToken.Multiline = true;
            this.textBox_AccessToken.Name = "textBox_AccessToken";
            this.textBox_AccessToken.ReadOnly = true;
            this.textBox_AccessToken.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_AccessToken.Size = new System.Drawing.Size(596, 283);
            this.textBox_AccessToken.TabIndex = 4;
            this.textBox_AccessToken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_AccessToken_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox_RefreshToken);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(610, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Refresh Token";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_RefreshToken
            // 
            this.textBox_RefreshToken.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RefreshToken.Location = new System.Drawing.Point(8, 6);
            this.textBox_RefreshToken.Multiline = true;
            this.textBox_RefreshToken.Name = "textBox_RefreshToken";
            this.textBox_RefreshToken.ReadOnly = true;
            this.textBox_RefreshToken.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_RefreshToken.Size = new System.Drawing.Size(596, 283);
            this.textBox_RefreshToken.TabIndex = 5;
            this.textBox_RefreshToken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_RefreshToken_KeyDown);
            // 
            // button_AccessTokenDetail
            // 
            this.button_AccessTokenDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_AccessTokenDetail.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_AccessTokenDetail.Location = new System.Drawing.Point(446, 356);
            this.button_AccessTokenDetail.Name = "button_AccessTokenDetail";
            this.button_AccessTokenDetail.Size = new System.Drawing.Size(75, 23);
            this.button_AccessTokenDetail.TabIndex = 5;
            this.button_AccessTokenDetail.Text = "Detail";
            this.button_AccessTokenDetail.UseVisualStyleBackColor = true;
            this.button_AccessTokenDetail.Click += new System.EventHandler(this.button_AccessTokenDetail_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Close.Location = new System.Drawing.Point(527, 356);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 3;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Export
            // 
            this.button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Export.Location = new System.Drawing.Point(13, 356);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(75, 23);
            this.button_Export.TabIndex = 6;
            this.button_Export.Text = "Export";
            this.button_Export.UseVisualStyleBackColor = true;
            this.button_Export.Click += new System.EventHandler(this.button_Export_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML (*.xml)|*.xml";
            // 
            // button_Import
            // 
            this.button_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Import.Location = new System.Drawing.Point(94, 356);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(75, 23);
            this.button_Import.TabIndex = 7;
            this.button_Import.Text = "Import";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML (*.xml)|*.xml";
            // 
            // TokenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Close;
            this.ClientSize = new System.Drawing.Size(614, 391);
            this.Controls.Add(this.button_Import);
            this.Controls.Add(this.button_Export);
            this.Controls.Add(this.button_AccessTokenDetail);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_Close);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(630, 430);
            this.Name = "TokenViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Token Viewer";
            this.Load += new System.EventHandler(this.TokenViewer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_AccessTokenDetail;
        private System.Windows.Forms.TextBox textBox_AccessToken;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox_RefreshToken;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox_IdToken;
        private System.Windows.Forms.Button button_Export;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}