
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class V1EndpointAppSelectionPage
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
            this.radioButton_AdminConsent = new System.Windows.Forms.RadioButton();
            this.radioButton_AppOnlyByKey = new System.Windows.Forms.RadioButton();
            this.radioButton_AppOnlyByCert = new System.Windows.Forms.RadioButton();
            this.radioButton_Native = new System.Windows.Forms.RadioButton();
            this.radioButton_Web = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButton_AdminConsent
            // 
            this.radioButton_AdminConsent.AutoSize = true;
            this.radioButton_AdminConsent.Location = new System.Drawing.Point(18, 127);
            this.radioButton_AdminConsent.Name = "radioButton_AdminConsent";
            this.radioButton_AdminConsent.Size = new System.Drawing.Size(352, 17);
            this.radioButton_AdminConsent.TabIndex = 4;
            this.radioButton_AdminConsent.TabStop = true;
            this.radioButton_AdminConsent.Text = "I need to complete ADMIN CONSENT before acquiring access token";
            this.radioButton_AdminConsent.UseVisualStyleBackColor = true;
            // 
            // radioButton_AppOnlyByKey
            // 
            this.radioButton_AppOnlyByKey.AutoSize = true;
            this.radioButton_AppOnlyByKey.Location = new System.Drawing.Point(18, 104);
            this.radioButton_AppOnlyByKey.Name = "radioButton_AppOnlyByKey";
            this.radioButton_AppOnlyByKey.Size = new System.Drawing.Size(251, 17);
            this.radioButton_AppOnlyByKey.TabIndex = 3;
            this.radioButton_AppOnlyByKey.TabStop = true;
            this.radioButton_AppOnlyByKey.Text = "Web app (Use App Only Token by client secret)";
            this.radioButton_AppOnlyByKey.UseVisualStyleBackColor = true;
            // 
            // radioButton_AppOnlyByCert
            // 
            this.radioButton_AppOnlyByCert.AutoSize = true;
            this.radioButton_AppOnlyByCert.Location = new System.Drawing.Point(18, 81);
            this.radioButton_AppOnlyByCert.Name = "radioButton_AppOnlyByCert";
            this.radioButton_AppOnlyByCert.Size = new System.Drawing.Size(240, 17);
            this.radioButton_AppOnlyByCert.TabIndex = 2;
            this.radioButton_AppOnlyByCert.TabStop = true;
            this.radioButton_AppOnlyByCert.Text = "Web app (Use App Only Token by certificate)";
            this.radioButton_AppOnlyByCert.UseVisualStyleBackColor = true;
            // 
            // radioButton_Native
            // 
            this.radioButton_Native.AutoSize = true;
            this.radioButton_Native.Location = new System.Drawing.Point(18, 58);
            this.radioButton_Native.Name = "radioButton_Native";
            this.radioButton_Native.Size = new System.Drawing.Size(226, 17);
            this.radioButton_Native.TabIndex = 1;
            this.radioButton_Native.TabStop = true;
            this.radioButton_Native.Text = "Public client/native (mobile && desktop) app";
            this.radioButton_Native.UseVisualStyleBackColor = true;
            // 
            // radioButton_Web
            // 
            this.radioButton_Web.AutoSize = true;
            this.radioButton_Web.Checked = true;
            this.radioButton_Web.Location = new System.Drawing.Point(18, 35);
            this.radioButton_Web.Name = "radioButton_Web";
            this.radioButton_Web.Size = new System.Drawing.Size(69, 17);
            this.radioButton_Web.TabIndex = 0;
            this.radioButton_Web.TabStop = true;
            this.radioButton_Web.Text = "Web app";
            this.radioButton_Web.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "My application has been registered as the";
            // 
            // V1EndpointAppSelectionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton_AdminConsent);
            this.Controls.Add(this.radioButton_AppOnlyByKey);
            this.Controls.Add(this.radioButton_AppOnlyByCert);
            this.Controls.Add(this.radioButton_Native);
            this.Controls.Add(this.radioButton_Web);
            this.Controls.Add(this.label2);
            this.Name = "V1EndpointAppSelectionPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.Tag = "";
            this.Load += new System.EventHandler(this.V1EndpointAppSelectionPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_AdminConsent;
        private System.Windows.Forms.RadioButton radioButton_AppOnlyByKey;
        private System.Windows.Forms.RadioButton radioButton_AppOnlyByCert;
        private System.Windows.Forms.RadioButton radioButton_Native;
        private System.Windows.Forms.RadioButton radioButton_Web;
        private System.Windows.Forms.Label label2;
    }
}
