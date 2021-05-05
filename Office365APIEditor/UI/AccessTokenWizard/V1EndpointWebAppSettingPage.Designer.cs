
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V1EndpointWebAppSettingPage
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
            this.linkLabel_Description = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ClientSecret = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.textBox_RedirectUri = new System.Windows.Forms.TextBox();
            this.button_LoadSavedApp = new System.Windows.Forms.Button();
            this.checkBox_SaveAsNewApp = new System.Windows.Forms.CheckBox();
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
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Application ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_ClientSecret
            // 
            this.textBox_ClientSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ClientSecret.Location = new System.Drawing.Point(102, 108);
            this.textBox_ClientSecret.Name = "textBox_ClientSecret";
            this.textBox_ClientSecret.Size = new System.Drawing.Size(284, 20);
            this.textBox_ClientSecret.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Redirect URI";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Client secret";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Resource";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // button_LoadSavedApp
            // 
            this.button_LoadSavedApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LoadSavedApp.Location = new System.Drawing.Point(263, 3);
            this.button_LoadSavedApp.Name = "button_LoadSavedApp";
            this.button_LoadSavedApp.Size = new System.Drawing.Size(123, 23);
            this.button_LoadSavedApp.TabIndex = 201;
            this.button_LoadSavedApp.Text = "Load Saved App";
            this.button_LoadSavedApp.UseVisualStyleBackColor = true;
            this.button_LoadSavedApp.Click += new System.EventHandler(this.Button_LoadSavedApp_Click);
            // 
            // checkBox_SaveAsNewApp
            // 
            this.checkBox_SaveAsNewApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_SaveAsNewApp.AutoSize = true;
            this.checkBox_SaveAsNewApp.Location = new System.Drawing.Point(18, 180);
            this.checkBox_SaveAsNewApp.Name = "checkBox_SaveAsNewApp";
            this.checkBox_SaveAsNewApp.Size = new System.Drawing.Size(242, 17);
            this.checkBox_SaveAsNewApp.TabIndex = 202;
            this.checkBox_SaveAsNewApp.Text = "Save this app to the app library as a new app.";
            this.checkBox_SaveAsNewApp.UseVisualStyleBackColor = true;
            // 
            // V1EndpointWebAppSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_SaveAsNewApp);
            this.Controls.Add(this.button_LoadSavedApp);
            this.Controls.Add(this.comboBox_Resource);
            this.Controls.Add(this.linkLabel_Description);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_ClientSecret);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_ClientID);
            this.Controls.Add(this.textBox_RedirectUri);
            this.Name = "V1EndpointWebAppSettingPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "textBox_ClientID";
            this.Load += new System.EventHandler(this.V1EndpointWebAppSettingPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Resource;
        private System.Windows.Forms.LinkLabel linkLabel_Description;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ClientSecret;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.TextBox textBox_RedirectUri;
        private System.Windows.Forms.Button button_LoadSavedApp;
        private System.Windows.Forms.CheckBox checkBox_SaveAsNewApp;
    }
}
