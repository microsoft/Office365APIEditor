
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class WelcomePage
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
            this.components = new System.ComponentModel.Container();
            this.linkLabel_Description = new System.Windows.Forms.LinkLabel();
            this.radioButton_SharePointOnlineAppOnly = new System.Windows.Forms.RadioButton();
            this.radioButton_BuiltInAppOrBasicAuth = new System.Windows.Forms.RadioButton();
            this.radioButton_V2Endpoint = new System.Windows.Forms.RadioButton();
            this.radioButton_V1Endpoint = new System.Windows.Forms.RadioButton();
            this.toolTip_RadioButtonDescription = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // linkLabel_Description
            // 
            this.linkLabel_Description.AutoSize = true;
            this.linkLabel_Description.Location = new System.Drawing.Point(15, 8);
            this.linkLabel_Description.Name = "linkLabel_Description";
            this.linkLabel_Description.Size = new System.Drawing.Size(235, 13);
            this.linkLabel_Description.TabIndex = 200;
            this.linkLabel_Description.TabStop = true;
            this.linkLabel_Description.Text = "Which OAuth endpoint do you use? Learn more.";
            this.linkLabel_Description.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Description_LinkClicked);
            // 
            // radioButton_SharePointOnlineAppOnly
            // 
            this.radioButton_SharePointOnlineAppOnly.AutoSize = true;
            this.radioButton_SharePointOnlineAppOnly.Location = new System.Drawing.Point(18, 101);
            this.radioButton_SharePointOnlineAppOnly.Name = "radioButton_SharePointOnlineAppOnly";
            this.radioButton_SharePointOnlineAppOnly.Size = new System.Drawing.Size(311, 17);
            this.radioButton_SharePointOnlineAppOnly.TabIndex = 3;
            this.radioButton_SharePointOnlineAppOnly.Text = "I have registered the SharePoint Online App-Only REST API.";
            this.toolTip_RadioButtonDescription.SetToolTip(this.radioButton_SharePointOnlineAppOnly, "Select this option if you registered the application using SharePoint Online Port" +
        "al.");
            this.radioButton_SharePointOnlineAppOnly.UseVisualStyleBackColor = true;
            // 
            // radioButton_BuiltInAppOrBasicAuth
            // 
            this.radioButton_BuiltInAppOrBasicAuth.AutoSize = true;
            this.radioButton_BuiltInAppOrBasicAuth.Location = new System.Drawing.Point(18, 78);
            this.radioButton_BuiltInAppOrBasicAuth.Name = "radioButton_BuiltInAppOrBasicAuth";
            this.radioButton_BuiltInAppOrBasicAuth.Size = new System.Drawing.Size(197, 17);
            this.radioButton_BuiltInAppOrBasicAuth.TabIndex = 2;
            this.radioButton_BuiltInAppOrBasicAuth.Text = "I have not registered the application.";
            this.toolTip_RadioButtonDescription.SetToolTip(this.radioButton_BuiltInAppOrBasicAuth, "Select this option if you want to use Office365APIEditor built-in application, th" +
        "e pre-acquired access token or basic auth.");
            this.radioButton_BuiltInAppOrBasicAuth.UseVisualStyleBackColor = true;
            // 
            // radioButton_V2Endpoint
            // 
            this.radioButton_V2Endpoint.AutoSize = true;
            this.radioButton_V2Endpoint.Checked = true;
            this.radioButton_V2Endpoint.Location = new System.Drawing.Point(18, 55);
            this.radioButton_V2Endpoint.Name = "radioButton_V2Endpoint";
            this.radioButton_V2Endpoint.Size = new System.Drawing.Size(91, 17);
            this.radioButton_V2Endpoint.TabIndex = 1;
            this.radioButton_V2Endpoint.TabStop = true;
            this.radioButton_V2Endpoint.Text = "v2.0 Endpoint";
            this.toolTip_RadioButtonDescription.SetToolTip(this.radioButton_V2Endpoint, "Select this option if your application is registered with Azure Active Directory " +
        "and you want to dynamically acquire an access token.");
            this.radioButton_V2Endpoint.UseVisualStyleBackColor = true;
            // 
            // radioButton_V1Endpoint
            // 
            this.radioButton_V1Endpoint.AutoSize = true;
            this.radioButton_V1Endpoint.Location = new System.Drawing.Point(18, 32);
            this.radioButton_V1Endpoint.Name = "radioButton_V1Endpoint";
            this.radioButton_V1Endpoint.Size = new System.Drawing.Size(91, 17);
            this.radioButton_V1Endpoint.TabIndex = 0;
            this.radioButton_V1Endpoint.Text = "v1.0 Endpoint";
            this.toolTip_RadioButtonDescription.SetToolTip(this.radioButton_V1Endpoint, "Select this option if your application is registered with Azure Active Directory " +
        "and you want to use the configured permissions.");
            this.radioButton_V1Endpoint.UseVisualStyleBackColor = true;
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.radioButton_SharePointOnlineAppOnly);
            this.Controls.Add(this.radioButton_BuiltInAppOrBasicAuth);
            this.Controls.Add(this.radioButton_V2Endpoint);
            this.Controls.Add(this.radioButton_V1Endpoint);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "";
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.RadioButton radioButton_SharePointOnlineAppOnly;
        private System.Windows.Forms.RadioButton radioButton_BuiltInAppOrBasicAuth;
        private System.Windows.Forms.RadioButton radioButton_V2Endpoint;
        private System.Windows.Forms.RadioButton radioButton_V1Endpoint;
        private System.Windows.Forms.ToolTip toolTip_RadioButtonDescription;
    }
}
