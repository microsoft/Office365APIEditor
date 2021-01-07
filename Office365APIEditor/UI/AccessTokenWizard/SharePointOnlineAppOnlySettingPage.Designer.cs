
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class SharePointOnlineAppOnlySettingPage
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
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.linkLabel_Description = new System.Windows.Forms.LinkLabel();
            this.label51 = new System.Windows.Forms.Label();
            this.textBox_ClientSecret = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox_TenantName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastSpoAppOnlyByKeyClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientID.Location = new System.Drawing.Point(102, 60);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientID.TabIndex = 1;
            this.textBox_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastSpoAppOnlyByKeyClientID;
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
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(3, 37);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(93, 13);
            this.label51.TabIndex = 61;
            this.label51.Text = "Tenant Name";
            this.label51.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientSecret
            // 
            this.textBox_ClientSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientSecret.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastSpoAppOnlyByKeyClientSecret", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientSecret.Location = new System.Drawing.Point(102, 86);
            this.textBox_ClientSecret.Name = "textBox_ClientSecret";
            this.textBox_ClientSecret.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientSecret.TabIndex = 2;
            this.textBox_ClientSecret.Text = global::Office365APIEditor.Properties.Settings.Default.LastSpoAppOnlyByKeyClientSecret;
            // 
            // label53
            // 
            this.label53.Location = new System.Drawing.Point(3, 89);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(93, 13);
            this.label53.TabIndex = 62;
            this.label53.Text = "Client secret";
            this.label53.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label54
            // 
            this.label54.Location = new System.Drawing.Point(3, 63);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(93, 13);
            this.label54.TabIndex = 60;
            this.label54.Text = "Client ID";
            this.label54.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_TenantName
            // 
            this.textBox_TenantName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TenantName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastSpoAppOnlyByKeyTenantName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_TenantName.Location = new System.Drawing.Point(102, 34);
            this.textBox_TenantName.Name = "textBox_TenantName";
            this.textBox_TenantName.Size = new System.Drawing.Size(284, 20);
            this.textBox_TenantName.TabIndex = 0;
            this.textBox_TenantName.Text = global::Office365APIEditor.Properties.Settings.Default.LastSpoAppOnlyByKeyTenantName;
            // 
            // SharePointOnlineAppOnlySettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_ClientID);
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.textBox_ClientSecret);
            this.Controls.Add(this.label53);
            this.Controls.Add(this.label54);
            this.Controls.Add(this.textBox_TenantName);
            this.Name = "SharePointOnlineAppOnlySettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_TenantName";
            this.Load += new System.EventHandler(this.SharePointOnlineAppOnlySettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox textBox_ClientSecret;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox textBox_TenantName;
    }
}
