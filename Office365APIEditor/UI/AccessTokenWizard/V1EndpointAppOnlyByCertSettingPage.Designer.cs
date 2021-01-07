
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V1EndpointAppOnlyByCertSettingPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabel_Description = new System.Windows.Forms.LinkLabel();
            this.comboBox_Resource = new System.Windows.Forms.ComboBox();
            this.button_SelectCert = new System.Windows.Forms.Button();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox_CertPath = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox_CertPass = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_TenantName = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.openFileDialog_PFX = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // linkLabel_Description
            // 
            this.linkLabel_Description.AutoSize = true;
            this.linkLabel_Description.Location = new System.Drawing.Point(15, 8);
            this.linkLabel_Description.Name = "linkLabel_Description";
            this.linkLabel_Description.Size = new System.Drawing.Size(140, 13);
            this.linkLabel_Description.TabIndex = 200;
            this.linkLabel_Description.TabStop = true;
            this.linkLabel_Description.Text = "Fill out the form. Learn more.";
            this.linkLabel_Description.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Description_LinkClicked);
            // 
            // comboBox_Resource
            // 
            this.comboBox_Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resource.FormattingEnabled = true;
            this.comboBox_Resource.Location = new System.Drawing.Point(102, 134);
            this.comboBox_Resource.Name = "comboBox_Resource";
            this.comboBox_Resource.Size = new System.Drawing.Size(284, 21);
            this.comboBox_Resource.TabIndex = 5;
            // 
            // button_SelectCert
            // 
            this.button_SelectCert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SelectCert.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_SelectCert.Location = new System.Drawing.Point(309, 80);
            this.button_SelectCert.Name = "button_SelectCert";
            this.button_SelectCert.Size = new System.Drawing.Size(77, 23);
            this.button_SelectCert.TabIndex = 3;
            this.button_SelectCert.Text = "Browse...";
            this.button_SelectCert.UseVisualStyleBackColor = true;
            this.button_SelectCert.Click += new System.EventHandler(this.Button_SelectCert_Click);
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(3, 85);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(93, 13);
            this.label36.TabIndex = 70;
            this.label36.Text = "Certificate Path";
            this.label36.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_CertPath
            // 
            this.textBox_CertPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CertPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyCertPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_CertPath.Location = new System.Drawing.Point(102, 82);
            this.textBox_CertPath.Name = "textBox_CertPath";
            this.textBox_CertPath.Size = new System.Drawing.Size(165, 20);
            this.textBox_CertPath.TabIndex = 2;
            this.textBox_CertPath.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyCertPath;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(3, 111);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(93, 13);
            this.label32.TabIndex = 69;
            this.label32.Text = "Password";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_CertPass
            // 
            this.textBox_CertPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_CertPass.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyCertPass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_CertPass.Location = new System.Drawing.Point(102, 108);
            this.textBox_CertPass.Name = "textBox_CertPass";
            this.textBox_CertPass.PasswordChar = '*';
            this.textBox_CertPass.Size = new System.Drawing.Size(284, 20);
            this.textBox_CertPass.TabIndex = 4;
            this.textBox_CertPass.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyCertPass;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(3, 59);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 13);
            this.label33.TabIndex = 68;
            this.label33.Text = "Application ID";
            this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(3, 33);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(93, 13);
            this.label34.TabIndex = 67;
            this.label34.Text = "Tenant Name";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientID.Location = new System.Drawing.Point(102, 56);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientID.TabIndex = 1;
            this.textBox_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyClientID;
            // 
            // textBox_TenantName
            // 
            this.textBox_TenantName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastWebAppAppOnlyTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_TenantName.Location = new System.Drawing.Point(102, 30);
            this.textBox_TenantName.Name = "textBox_TenantName";
            this.textBox_TenantName.Size = new System.Drawing.Size(284, 20);
            this.textBox_TenantName.TabIndex = 0;
            this.textBox_TenantName.Tag = "";
            this.textBox_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastWebAppAppOnlyTenantName;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(3, 137);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(93, 13);
            this.label35.TabIndex = 66;
            this.label35.Text = "Resource";
            this.label35.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // openFileDialog_PFX
            // 
            this.openFileDialog_PFX.Filter = "Personal Information Exchange (*.pfx)|*.pfx";
            // 
            // V1EndpointAppOnlyByCertSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.comboBox_Resource);
            this.Controls.Add(this.button_SelectCert);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.textBox_CertPath);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.textBox_CertPass);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.textBox_ClientID);
            this.Controls.Add(this.textBox_TenantName);
            this.Controls.Add(this.label35);
            this.Name = "V1EndpointAppOnlyByCertSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_TenantName";
            this.Load += new System.EventHandler(this.V1EndpointAppOnlyByCertSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.ComboBox comboBox_Resource;
        private System.Windows.Forms.Button button_SelectCert;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox_CertPath;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox textBox_CertPass;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.TextBox textBox_TenantName;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.OpenFileDialog openFileDialog_PFX;
    }
}
