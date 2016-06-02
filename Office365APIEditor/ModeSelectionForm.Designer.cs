namespace Office365APIEditor
{
    partial class ModeSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeSelectionForm));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_EditorMode = new System.Windows.Forms.RadioButton();
            this.radioButton_MailboxViewerMode = new System.Windows.Forms.RadioButton();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a mode.";
            // 
            // radioButton_EditorMode
            // 
            this.radioButton_EditorMode.AutoSize = true;
            this.radioButton_EditorMode.Location = new System.Drawing.Point(15, 39);
            this.radioButton_EditorMode.Name = "radioButton_EditorMode";
            this.radioButton_EditorMode.Size = new System.Drawing.Size(190, 17);
            this.radioButton_EditorMode.TabIndex = 2;
            this.radioButton_EditorMode.TabStop = true;
            this.radioButton_EditorMode.Text = "Request and response editor mode";
            this.radioButton_EditorMode.UseVisualStyleBackColor = true;
            // 
            // radioButton_MailboxViewerMode
            // 
            this.radioButton_MailboxViewerMode.AutoSize = true;
            this.radioButton_MailboxViewerMode.Location = new System.Drawing.Point(15, 62);
            this.radioButton_MailboxViewerMode.Name = "radioButton_MailboxViewerMode";
            this.radioButton_MailboxViewerMode.Size = new System.Drawing.Size(124, 17);
            this.radioButton_MailboxViewerMode.TabIndex = 3;
            this.radioButton_MailboxViewerMode.TabStop = true;
            this.radioButton_MailboxViewerMode.Text = "Mailbox viewer mode";
            this.radioButton_MailboxViewerMode.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(193, 108);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 4;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Close
            // 
            this.button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Close.Location = new System.Drawing.Point(274, 109);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 5;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mailbox viewer mode uses only v2 features of Office 365 API.";
            // 
            // ModeSelectionForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Close;
            this.ClientSize = new System.Drawing.Size(361, 143);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.radioButton_MailboxViewerMode);
            this.Controls.Add(this.radioButton_EditorMode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModeSelectionForm";
            this.Text = "Office365APIEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_EditorMode;
        private System.Windows.Forms.RadioButton radioButton_MailboxViewerMode;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Label label2;
    }
}