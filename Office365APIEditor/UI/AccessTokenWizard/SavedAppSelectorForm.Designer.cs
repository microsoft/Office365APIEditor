
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class SavedAppSelectorForm
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
            this.button_Select = new System.Windows.Forms.Button();
            this.textBox_SavedAppNote = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_SavedAppScopes = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppCertPath = new System.Windows.Forms.TextBox();
            this.textBox_SavedAppCertPass = new System.Windows.Forms.TextBox();
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
            this.listBox_SavedApps = new System.Windows.Forms.ListBox();
            this.textBox_SavedAppResource = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(643, 302);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 101;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_Select
            // 
            this.button_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Select.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Select.Location = new System.Drawing.Point(562, 302);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(75, 23);
            this.button_Select.TabIndex = 100;
            this.button_Select.Text = "Select";
            this.button_Select.UseVisualStyleBackColor = true;
            // 
            // textBox_SavedAppNote
            // 
            this.textBox_SavedAppNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppNote.Location = new System.Drawing.Point(416, 226);
            this.textBox_SavedAppNote.Multiline = true;
            this.textBox_SavedAppNote.Name = "textBox_SavedAppNote";
            this.textBox_SavedAppNote.Size = new System.Drawing.Size(302, 70);
            this.textBox_SavedAppNote.TabIndex = 127;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(413, 210);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 126;
            this.label15.Text = "Note";
            // 
            // textBox_SavedAppScopes
            // 
            this.textBox_SavedAppScopes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppScopes.Location = new System.Drawing.Point(416, 43);
            this.textBox_SavedAppScopes.Name = "textBox_SavedAppScopes";
            this.textBox_SavedAppScopes.Size = new System.Drawing.Size(302, 20);
            this.textBox_SavedAppScopes.TabIndex = 120;
            // 
            // textBox_SavedAppCertPath
            // 
            this.textBox_SavedAppCertPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppCertPath.Location = new System.Drawing.Point(416, 133);
            this.textBox_SavedAppCertPath.Name = "textBox_SavedAppCertPath";
            this.textBox_SavedAppCertPath.Size = new System.Drawing.Size(302, 20);
            this.textBox_SavedAppCertPath.TabIndex = 123;
            // 
            // textBox_SavedAppCertPass
            // 
            this.textBox_SavedAppCertPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppCertPass.Location = new System.Drawing.Point(416, 178);
            this.textBox_SavedAppCertPass.Name = "textBox_SavedAppCertPass";
            this.textBox_SavedAppCertPass.PasswordChar = '*';
            this.textBox_SavedAppCertPass.Size = new System.Drawing.Size(302, 20);
            this.textBox_SavedAppCertPass.TabIndex = 125;
            // 
            // textBox_SavedAppClientSecret
            // 
            this.textBox_SavedAppClientSecret.Location = new System.Drawing.Point(178, 226);
            this.textBox_SavedAppClientSecret.Name = "textBox_SavedAppClientSecret";
            this.textBox_SavedAppClientSecret.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppClientSecret.TabIndex = 119;
            // 
            // textBox_SavedAppRedirectUri
            // 
            this.textBox_SavedAppRedirectUri.Location = new System.Drawing.Point(178, 178);
            this.textBox_SavedAppRedirectUri.Name = "textBox_SavedAppRedirectUri";
            this.textBox_SavedAppRedirectUri.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppRedirectUri.TabIndex = 118;
            // 
            // textBox_SavedAppTenantName
            // 
            this.textBox_SavedAppTenantName.Location = new System.Drawing.Point(178, 133);
            this.textBox_SavedAppTenantName.Name = "textBox_SavedAppTenantName";
            this.textBox_SavedAppTenantName.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppTenantName.TabIndex = 117;
            // 
            // textBox_SavedAppApplicationId
            // 
            this.textBox_SavedAppApplicationId.Location = new System.Drawing.Point(178, 88);
            this.textBox_SavedAppApplicationId.Name = "textBox_SavedAppApplicationId";
            this.textBox_SavedAppApplicationId.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppApplicationId.TabIndex = 116;
            // 
            // textBox_SavedAppDisplayName
            // 
            this.textBox_SavedAppDisplayName.Location = new System.Drawing.Point(178, 43);
            this.textBox_SavedAppDisplayName.Name = "textBox_SavedAppDisplayName";
            this.textBox_SavedAppDisplayName.Size = new System.Drawing.Size(200, 20);
            this.textBox_SavedAppDisplayName.TabIndex = 115;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 114;
            this.label14.Text = "Saved Apps:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(413, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 113;
            this.label13.Text = "Certificate Password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(413, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 112;
            this.label12.Text = "Certificate Path";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(175, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 111;
            this.label11.Text = "Client Secret";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(413, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 110;
            this.label10.Text = "Scopes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(413, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 109;
            this.label9.Text = "Resource";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 108;
            this.label8.Text = "Redirect URI";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 107;
            this.label7.Text = "Tenant Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 106;
            this.label6.Text = "Application ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 105;
            this.label5.Text = "Display Name";
            // 
            // listBox_SavedApps
            // 
            this.listBox_SavedApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_SavedApps.FormattingEnabled = true;
            this.listBox_SavedApps.HorizontalScrollbar = true;
            this.listBox_SavedApps.Location = new System.Drawing.Point(15, 27);
            this.listBox_SavedApps.Name = "listBox_SavedApps";
            this.listBox_SavedApps.Size = new System.Drawing.Size(154, 290);
            this.listBox_SavedApps.TabIndex = 102;
            this.listBox_SavedApps.SelectedIndexChanged += new System.EventHandler(this.ListBox_SavedApps_SelectedIndexChanged);
            // 
            // textBox_SavedAppResource
            // 
            this.textBox_SavedAppResource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SavedAppResource.Location = new System.Drawing.Point(416, 88);
            this.textBox_SavedAppResource.Name = "textBox_SavedAppResource";
            this.textBox_SavedAppResource.Size = new System.Drawing.Size(302, 20);
            this.textBox_SavedAppResource.TabIndex = 121;
            // 
            // SavedAppSelectorForm
            // 
            this.AcceptButton = this.button_Select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(730, 337);
            this.Controls.Add(this.textBox_SavedAppResource);
            this.Controls.Add(this.textBox_SavedAppNote);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox_SavedAppScopes);
            this.Controls.Add(this.textBox_SavedAppCertPath);
            this.Controls.Add(this.textBox_SavedAppCertPass);
            this.Controls.Add(this.textBox_SavedAppClientSecret);
            this.Controls.Add(this.textBox_SavedAppRedirectUri);
            this.Controls.Add(this.textBox_SavedAppTenantName);
            this.Controls.Add(this.textBox_SavedAppApplicationId);
            this.Controls.Add(this.textBox_SavedAppDisplayName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox_SavedApps);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Select);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SavedAppSelectorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Saved App Selector";
            this.Load += new System.EventHandler(this.SavedAppSelectorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.TextBox textBox_SavedAppNote;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_SavedAppScopes;
        private System.Windows.Forms.TextBox textBox_SavedAppCertPath;
        private System.Windows.Forms.TextBox textBox_SavedAppCertPass;
        private System.Windows.Forms.TextBox textBox_SavedAppClientSecret;
        private System.Windows.Forms.TextBox textBox_SavedAppRedirectUri;
        private System.Windows.Forms.TextBox textBox_SavedAppTenantName;
        private System.Windows.Forms.TextBox textBox_SavedAppApplicationId;
        private System.Windows.Forms.TextBox textBox_SavedAppDisplayName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox_SavedApps;
        private System.Windows.Forms.TextBox textBox_SavedAppResource;
    }
}