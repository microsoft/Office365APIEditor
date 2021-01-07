
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V2EndpointAdminConsentSettingPage
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
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox_RedirectUri = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.comboBox_Resource = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(15, 8);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(81, 13);
            this.label46.TabIndex = 52;
            this.label46.Text = "Fill out the form.";
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(3, 63);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(93, 13);
            this.label47.TabIndex = 51;
            this.label47.Text = "Redirect URI";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_RedirectUri
            // 
            this.textBox_RedirectUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RedirectUri.Location = new System.Drawing.Point(102, 60);
            this.textBox_RedirectUri.Name = "textBox_RedirectUri";
            this.textBox_RedirectUri.Size = new System.Drawing.Size(284, 20);
            this.textBox_RedirectUri.TabIndex = 1;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(3, 37);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(93, 13);
            this.label48.TabIndex = 50;
            this.label48.Text = "Application ID";
            this.label48.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientID.Location = new System.Drawing.Point(102, 34);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientID.TabIndex = 0;
            // 
            // comboBox_Resource
            // 
            this.comboBox_Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resource.FormattingEnabled = true;
            this.comboBox_Resource.Location = new System.Drawing.Point(102, 86);
            this.comboBox_Resource.Name = "comboBox_Resource";
            this.comboBox_Resource.Size = new System.Drawing.Size(284, 21);
            this.comboBox_Resource.TabIndex = 65;
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(3, 89);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(93, 13);
            this.label45.TabIndex = 66;
            this.label45.Text = "Scope";
            this.label45.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // V2EndpointAdminConsentSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_Resource);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.textBox_RedirectUri);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.textBox_ClientID);
            this.Name = "V2EndpointAdminConsentSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_ClientID";
            this.Load += new System.EventHandler(this.V2EndpointAdminConsentSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox textBox_RedirectUri;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.ComboBox comboBox_Resource;
        private System.Windows.Forms.Label label45;
    }
}
