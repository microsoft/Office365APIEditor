namespace Office365APIEditor
{
    partial class AcquireViewerTokenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcquireViewerTokenForm));
            this.textBox_ClientID = new System.Windows.Forms.TextBox();
            this.button_UseMyApp = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_UseBuiltInApp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel_LearnMore = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // textBox_ClientID
            // 
            this.textBox_ClientID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Office365APIEditor.Properties.Settings.Default, "LastMailboxViewerClientID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_ClientID.Location = new System.Drawing.Point(110, 144);
            this.textBox_ClientID.Name = "textBox_ClientID";
            this.textBox_ClientID.Size = new System.Drawing.Size(364, 20);
            this.textBox_ClientID.TabIndex = 3;
            this.textBox_ClientID.Text = global::Office365APIEditor.Properties.Settings.Default.LastMailboxViewerClientID;
            this.textBox_ClientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_ClientID_KeyDownAsync);
            // 
            // button_UseMyApp
            // 
            this.button_UseMyApp.Location = new System.Drawing.Point(317, 170);
            this.button_UseMyApp.Name = "button_UseMyApp";
            this.button_UseMyApp.Size = new System.Drawing.Size(157, 23);
            this.button_UseMyApp.TabIndex = 4;
            this.button_UseMyApp.Text = "Use my application";
            this.button_UseMyApp.UseVisualStyleBackColor = true;
            this.button_UseMyApp.Click += new System.EventHandler(this.Button_UseMyApp_ClickAsync);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(12, 170);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application ID : ";
            // 
            // button_UseBuiltInApp
            // 
            this.button_UseBuiltInApp.Location = new System.Drawing.Point(399, 100);
            this.button_UseBuiltInApp.Name = "button_UseBuiltInApp";
            this.button_UseBuiltInApp.Size = new System.Drawing.Size(75, 23);
            this.button_UseBuiltInApp.TabIndex = 1;
            this.button_UseBuiltInApp.Text = "Sign in";
            this.button_UseBuiltInApp.UseVisualStyleBackColor = true;
            this.button_UseBuiltInApp.Click += new System.EventHandler(this.button_UseBuiltInApp_ClickAsync);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(428, 78);
            this.label3.TabIndex = 0;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // linkLabel_LearnMore
            // 
            this.linkLabel_LearnMore.AutoSize = true;
            this.linkLabel_LearnMore.Location = new System.Drawing.Point(93, 175);
            this.linkLabel_LearnMore.Name = "linkLabel_LearnMore";
            this.linkLabel_LearnMore.Size = new System.Drawing.Size(60, 13);
            this.linkLabel_LearnMore.TabIndex = 7;
            this.linkLabel_LearnMore.TabStop = true;
            this.linkLabel_LearnMore.Text = "Learn more";
            this.linkLabel_LearnMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LearnMore_LinkClicked);
            // 
            // AcquireViewerTokenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(489, 209);
            this.Controls.Add(this.linkLabel_LearnMore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_UseBuiltInApp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_UseMyApp);
            this.Controls.Add(this.textBox_ClientID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AcquireViewerTokenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application ID Setting";
            this.Load += new System.EventHandler(this.AcquireViewerTokenForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_ClientID;
        private System.Windows.Forms.Button button_UseMyApp;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_UseBuiltInApp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel_LearnMore;
    }
}