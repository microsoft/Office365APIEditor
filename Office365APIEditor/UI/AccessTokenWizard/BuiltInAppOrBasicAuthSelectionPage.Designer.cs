
namespace Office365APIEditor.UI.AccessTokenWizard
{
    partial class BuiltInAppOrBasicAuthSelectionPage
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
            this.radioButton_BasicAuth = new System.Windows.Forms.RadioButton();
            this.radioButton_BuiltInApp = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.radioButton_PreAcquiredAccessToken = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButton_BasicAuth
            // 
            this.radioButton_BasicAuth.AutoSize = true;
            this.radioButton_BasicAuth.Location = new System.Drawing.Point(18, 81);
            this.radioButton_BasicAuth.Name = "radioButton_BasicAuth";
            this.radioButton_BasicAuth.Size = new System.Drawing.Size(75, 17);
            this.radioButton_BasicAuth.TabIndex = 2;
            this.radioButton_BasicAuth.Text = "Basic auth";
            this.radioButton_BasicAuth.UseVisualStyleBackColor = true;
            // 
            // radioButton_BuiltInApp
            // 
            this.radioButton_BuiltInApp.AutoSize = true;
            this.radioButton_BuiltInApp.Checked = true;
            this.radioButton_BuiltInApp.Location = new System.Drawing.Point(18, 35);
            this.radioButton_BuiltInApp.Name = "radioButton_BuiltInApp";
            this.radioButton_BuiltInApp.Size = new System.Drawing.Size(202, 17);
            this.radioButton_BuiltInApp.TabIndex = 0;
            this.radioButton_BuiltInApp.TabStop = true;
            this.radioButton_BuiltInApp.Text = "Office365APIEditor built-in application";
            this.radioButton_BuiltInApp.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(15, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(137, 13);
            this.label28.TabIndex = 3;
            this.label28.Text = "Which do you want to use?";
            // 
            // radioButton_PreAcquiredAccessToken
            // 
            this.radioButton_PreAcquiredAccessToken.AutoSize = true;
            this.radioButton_PreAcquiredAccessToken.Location = new System.Drawing.Point(18, 58);
            this.radioButton_PreAcquiredAccessToken.Name = "radioButton_PreAcquiredAccessToken";
            this.radioButton_PreAcquiredAccessToken.Size = new System.Drawing.Size(152, 17);
            this.radioButton_PreAcquiredAccessToken.TabIndex = 1;
            this.radioButton_PreAcquiredAccessToken.Text = "Pre-acquired access token";
            this.radioButton_PreAcquiredAccessToken.UseVisualStyleBackColor = true;
            // 
            // BuiltInAppOrBasicAuthSelectionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton_PreAcquiredAccessToken);
            this.Controls.Add(this.radioButton_BasicAuth);
            this.Controls.Add(this.radioButton_BuiltInApp);
            this.Controls.Add(this.label28);
            this.Name = "BuiltInAppOrBasicAuthSelectionPage";
            this.Size = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_BasicAuth;
        private System.Windows.Forms.RadioButton radioButton_BuiltInApp;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.RadioButton radioButton_PreAcquiredAccessToken;
    }
}
