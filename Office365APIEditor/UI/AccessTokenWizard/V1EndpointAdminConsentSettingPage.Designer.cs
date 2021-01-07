
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V1EndpointAdminConsentSettingPage
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
            this.comboBox_Resource = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_RedirectUri = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Resource
            // 
            this.comboBox_Resource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Resource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resource.FormattingEnabled = true;
            this.comboBox_Resource.Location = new System.Drawing.Point(102, 82);
            this.comboBox_Resource.Name = "comboBox_Resource";
            this.comboBox_Resource.Size = new System.Drawing.Size(284, 21);
            this.comboBox_Resource.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(3, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(93, 13);
            this.label24.TabIndex = 49;
            this.label24.Text = "Application ID";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(3, 59);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(93, 13);
            this.label27.TabIndex = 48;
            this.label27.Text = "Redirect URI";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(3, 85);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(93, 13);
            this.label37.TabIndex = 47;
            this.label37.Text = "Resource";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientID.Location = new System.Drawing.Point(102, 30);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientID.TabIndex = 0;
            // 
            // textBox_RedirectUri
            // 
            this.textBox_RedirectUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RedirectUri.Location = new System.Drawing.Point(102, 56);
            this.textBox_RedirectUri.Name = "textBox_RedirectUri";
            this.textBox_RedirectUri.Size = new System.Drawing.Size(284, 20);
            this.textBox_RedirectUri.TabIndex = 1;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(15, 8);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(81, 13);
            this.label38.TabIndex = 43;
            this.label38.Text = "Fill out the form.";
            // 
            // V1EndpointAdminConsentSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_Resource);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.textBox_ClientID);
            this.Controls.Add(this.textBox_RedirectUri);
            this.Controls.Add(this.label38);
            this.Name = "V1EndpointAdminConsentSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Load += new System.EventHandler(this.V1EndpointAdminConsentSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Resource;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.TextBox textBox_RedirectUri;
        private System.Windows.Forms.Label label38;
    }
}
