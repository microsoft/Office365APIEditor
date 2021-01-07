
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V2EndpointAppOnlyByKeySettingPage
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
            this.textBox_ClientSecret = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox_TenantName = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.textBox_ClientId = new System.Windows.Forms.TextBox();
            this.comboBox_Resource = new System.Windows.Forms.ComboBox();
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
            // textBox_ClientSecret
            // 
            this.textBox_ClientSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientSecret.Location = new System.Drawing.Point(102, 114);
            this.textBox_ClientSecret.Name = "textBox_ClientSecret";
            this.textBox_ClientSecret.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientSecret.TabIndex = 3;
            this.textBox_ClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientSecret;
            // 
            // label41
            // 
            this.label41.Location = new System.Drawing.Point(3, 117);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(93, 13);
            this.label41.TabIndex = 67;
            this.label41.Text = "Client secret";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TenantName
            // 
            this.textBox_TenantName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppAppOnlyByPasswordForMicrosoftGraphTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_TenantName.Location = new System.Drawing.Point(102, 34);
            this.textBox_TenantName.Name = "textBox_TenantName";
            this.textBox_TenantName.Size = new System.Drawing.Size(284, 20);
            this.textBox_TenantName.TabIndex = 0;
            this.textBox_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphTenantName;
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(3, 37);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(93, 13);
            this.label43.TabIndex = 66;
            this.label43.Text = "Tenant Name";
            this.label43.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(3, 63);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(93, 13);
            this.label44.TabIndex = 65;
            this.label44.Text = "Application ID";
            this.label44.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(3, 90);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(93, 13);
            this.label45.TabIndex = 64;
            this.label45.Text = "Scope";
            this.label45.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientId
            // 
            this.textBox_ClientId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientId.Location = new System.Drawing.Point(102, 60);
            this.textBox_ClientId.Name = "textBox_ClientId";
            this.textBox_ClientId.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientId.TabIndex = 1;
            this.textBox_ClientId.Text = global::Office365APIEditor.Properties.Settings.Default.LastV2WebAppAppOnlyByPasswordForMicrosoftGraphClientID;
            // 
            // comboBox_Resource
            // 
            this.comboBox_Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resource.FormattingEnabled = true;
            this.comboBox_Resource.Location = new System.Drawing.Point(102, 87);
            this.comboBox_Resource.Name = "comboBox_Resource";
            this.comboBox_Resource.Size = new System.Drawing.Size(284, 21);
            this.comboBox_Resource.TabIndex = 2;
            // 
            // V2EndpointAppOnlyByKeySettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_Resource);
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.textBox_ClientSecret);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.textBox_TenantName);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.textBox_ClientId);
            this.Name = "V2EndpointAppOnlyByKeySettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Load += new System.EventHandler(this.V2EndpointAppOnlyByKeySettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.TextBox textBox_ClientSecret;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textBox_TenantName;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox textBox_ClientId;
        private System.Windows.Forms.ComboBox comboBox_Resource;
    }
}
